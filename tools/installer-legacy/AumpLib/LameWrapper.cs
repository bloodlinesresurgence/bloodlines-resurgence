///////////////////////////////////////////////////////////////////////////////
//
// LameWrapper -- interface to the lame_enc.dll
//
// Direct connection to DLL using P/Invoke. Provides the data structures and 
// function mappings.
//  	
///////////////////////////////////////////////////////////////////////////////
//
// Copyright (C) 2004 J. A. Robson, http://www.arbingersys.com
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// 
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
//
///////////////////////////////////////////////////////////////////////////////

namespace Arbingersys.Audio.Aumplib 
{


	using System;
	using System.IO;
	using System.Runtime.InteropServices;

	// Alias; see LibsndfileWrapper.SF_INFO struct:

	using sf_count_t = System.Int64;	


#if PLATFORM_64
	using size_t = System.UInt64;
#else
	using size_t = System.UInt32;
#endif


	class LameWrapper 
	{

		// Data types hence

		
		public const int SKIP_WAV_HEADER = 44;
		
		
		// Variable bitrate

		public enum VBRMETHOD : int
	  	{
			VBR_METHOD_NONE			= -1,
			VBR_METHOD_DEFAULT	    =  0,
			VBR_METHOD_OLD			=  1,
			VBR_METHOD_NEW			=  2,
			VBR_METHOD_MTRH			=  3,
			VBR_METHOD_ABR			=  4
		} 


		// MPEG modes

		public enum mpeg_mode : uint 
		{
			STEREO = 0,
			JOINT_STEREO,
			DUAL_CHANNEL,   // LAME doesn't supports this!
			MONO,
			NOT_SET,
			MAX_INDICATOR   // Don't use this! It's used for sanity checks.
		}

	  
		// Quality presets	  

		public enum LAME_QUALITY_PRESET : int
	  	{
			LQP_NOPRESET			=-1,

			// QUALITY PRESETS

			LQP_NORMAL_QUALITY		= 0,
			LQP_LOW_QUALITY			= 1,
			LQP_HIGH_QUALITY		= 2,
			LQP_VOICE_QUALITY		= 3,
			LQP_R3MIX				= 4,
			LQP_VERYHIGH_QUALITY	= 5,
			LQP_STANDARD			= 6,
			LQP_FAST_STANDARD		= 7,
			LQP_EXTREME				= 8,
			LQP_FAST_EXTREME		= 9,
			LQP_INSANE				= 10,
			LQP_ABR					= 11,
			LQP_CBR					= 12,
			LQP_MEDIUM				= 13,
			LQP_FAST_MEDIUM			= 14,

			// NEW PRESET VALUES

			LQP_PHONE	= 1000,
			LQP_SW		= 2000,
			LQP_AM		= 3000,
			LQP_FM		= 4000,
			LQP_VOICE	= 5000,
			LQP_RADIO	= 6000,
			LQP_TAPE	= 7000,
			LQP_HIFI	= 8000,
			LQP_CD		= 9000,
			LQP_STUDIO	= 10000
	  } 


		// BE_CONFIG_MP3	  
		
		[StructLayout(LayoutKind.Sequential), Serializable]
		public struct MP3 
		{

			// 48000, 44100 and 32000 allowed

			public uint   dwSampleRate;		

			// BE_MP3_MODE_STEREO, BE_MP3_MODE_DUALCHANNEL, 
			// BE_MP3_MODE_MONO

			public byte	  byMode;			
			
			// 32, 40, 48, 56, 64, 80, 96, 112, 128, 160, 
			// 192, 224, 256 and 320 allowed

			public ushort	wBitrate;		
			
			public int	  bPrivate;		
			public int	  bCRC;
			public int	  bCopyright;
			public int	  bOriginal;
		} 


		// BE_CONFIG_LAME LAME header version 1
		
		[StructLayout(LayoutKind.Sequential, Size=327), Serializable]
		public struct LHV1 
		{
	
	
			public const uint MPEG1	= 1;
			public const uint MPEG2	= 0;

			// STRUCTURE INFORMATION
		
			public uint dwStructVersion;	
			public uint	dwStructSize;
		
			// BASIC ENCODER SETTINGS
		
			public uint	dwSampleRate;		// SAMPLERATE OF INPUT FILE
			public uint	dwReSampleRate;		// DOWNSAMPLERATE, 0=ENCODER DECIDES  
			public mpeg_mode mpgmode;		// STEREO, MONO
			public uint	dwBitrate;			// CBR bitrate, VBR min bitrate
			public uint	dwMaxBitrate;		// CBR ignored, VBR Max bitrate
			public LAME_QUALITY_PRESET nPreset;			// Quality preset
			public uint	dwMpegVersion;		// MPEG-1 OR MPEG-2
			public uint	dwPsyModel;			// FUTURE USE, SET TO 0
			public uint	dwEmphasis;			// FUTURE USE, SET TO 0
		
			// BIT STREAM SETTINGS
		
			public int bPrivate;			// Set Private Bit (TRUE/FALSE)
			public int bCRC;				// Insert CRC (TRUE/FALSE)
			public int bCopyright;			// Set Copyright Bit (TRUE/FALSE)
			public int bOriginal;			// Set Original Bit (TRUE/FALSE)
		
			// VBR STUFF
		
			public int bWriteVBRHeader;	// WRITE XING VBR HEADER (TRUE/FALSE)
			public int bEnableVBR;		// USE VBR ENCODING (TRUE/FALSE)
			public int nVBRQuality;		// VBR QUALITY 0..9
			public uint dwVbrAbr_bps;	// Use ABR in stead of nVBRQuality
			public VBRMETHOD nVbrMethod;
			public int bNoRes;			// Disable Bit resorvoir (TRUE/FALSE)
		
			// MISC SETTINGS
		
			public int bStrictIso;			// Use strict ISO encoding rules 
											// (TRUE/FALSE)

			// Quality Setting, HIGH BYTE should be 
			// NOT LOW byte, otherwhise quality=5:

			public ushort nQuality;			


			// Constructor:	

			public 
			LHV1(LibsndfileWrapper.SF_INFO soundInfo, uint mp3bitrate)
			{

				
				// Error if not WAV, 16bit		

				if (soundInfo.format != 
						((int)LibsndfileWrapper.soundFormat.SF_FORMAT_WAV |
						 (int)LibsndfileWrapper.soundFormat.SF_FORMAT_PCM_16) )
				{
					throw new ArgumentOutOfRangeException(
							"format", 
							"Only WAV 16 bit uncompressed supported. " +
							"You gave format " + soundInfo.format);
				}


			  dwStructVersion	= 1;
			  dwStructSize		= (uint)Marshal.SizeOf(typeof(BE_CONFIG));
			  
				    
			  // Handle sample rate		  
			   
  			  switch (soundInfo.samplerate)
			  {
				case 16000 :
				case 22050 :
				case 24000 :
				  dwMpegVersion		= MPEG2;
				  break;
				case 32000 :
				case 44100 :
				case 48000 :
				  dwMpegVersion		= MPEG1;
				  break;
				default :
				  throw new ArgumentOutOfRangeException(
						  "format", "Sample rate " +
						  soundInfo.samplerate + " not supported");
			  }

			  
			  dwSampleRate = (uint)soundInfo.samplerate;			
			  dwReSampleRate = 0;								
			  
		
		  	  // Handle channels

			  switch (soundInfo.channels)
			  {

				case 1 :
				  	mpgmode = mpeg_mode.MONO;
					break;
				case 2 :
					mpgmode = mpeg_mode.STEREO;
					break;	
				default:
					throw new ArgumentOutOfRangeException(
							"format", 
							"Invalid number of channels:" + soundInfo.channels);

			  }

			  
			  // Handle bit rate		  
			  
			  switch (mp3bitrate)
			  {
				  
				case 32 :
				case 40 :
				case 48 :
				case 56 :
				case 64 :
				case 80 :
				case 96 :
				case 112 :
				case 128 :

				// Allowed bit rates in MPEG1 and MPEG2:					

				case 160 :
				  break; 

				case 192 :
				case 224 :
				case 256 :

				// Allowed only in MPEG1:

				case 320 : 
				  if (dwMpegVersion	!= MPEG1)
				  {
					throw new ArgumentOutOfRangeException(
							"mp3bitrate", "Incompatible bit rate:"+mp3bitrate);
				  }
				  break;

				case 8 :
				case 16 :
				case 24 :

				// Allowed only in MPEG2:

				case 144 : 
				  if (dwMpegVersion	!= MPEG2)
				  {
					throw new ArgumentOutOfRangeException(
							"mp3bitrate", "Incompatible bit rate:"+mp3bitrate);
				  }
				  break;

				default :
				  throw new ArgumentOutOfRangeException(
						  "mp3bitrate", "Can't support bit rate");

			  }
			  
			  // MINIMUM BIT RATE:

			  dwBitrate	= mp3bitrate;					

			  // QUALITY PRESET SETTING: 

			  nPreset = LAME_QUALITY_PRESET.LQP_NORMAL_QUALITY;		

			  // USE DEFAULT PSYCHOACOUSTIC MODEL:

			  dwPsyModel = 0;					

			  // NO EMPHASIS TURNED ON:

			  dwEmphasis = 0;					

			  // SET ORIGINAL FLAG:

			  bOriginal = 1;					

			  bWriteVBRHeader	= 0;					

			  // No bit reservoir:

			  bNoRes = 0;					

			  bCopyright = 0;
			  bCRC = 0;
			  bEnableVBR = 0;
			  bPrivate = 0;
			  bStrictIso = 0;
			  dwMaxBitrate = 0;
			  dwVbrAbr_bps = 0;
			  nQuality = 0;
			  nVbrMethod = VBRMETHOD.VBR_METHOD_NONE;
			  nVBRQuality = 0;
		  
			  
			} 


		// End public struct LHV1:
		}


		
		// ACC structure	  
		
		[StructLayout(LayoutKind.Sequential), Serializable]
	  	public struct ACC
	  	{
			
			public uint	dwSampleRate;
			public byte	byMode;
			public ushort	wBitrate;
			public byte	byEncodingMethod;

	  	}

	  
		// 'unionizer' struct
	  	
		[StructLayout(LayoutKind.Explicit), Serializable]
	  	public class struct_un
	  	{
				
			[FieldOffset(0)] 
			public MP3 mp3;
			[FieldOffset(0)]
			public LHV1 lhv1;
			[FieldOffset(0)]
			public ACC acc;

			
			public 
			struct_un(LibsndfileWrapper.SF_INFO soundInfo, uint mp3bitrate)
			{			
				lhv1 = new LHV1(soundInfo, mp3bitrate);
			}
			
		}


		// BE_CONFIG struct selection	  
	  	
		[StructLayout(LayoutKind.Sequential), Serializable]
	  	public class BE_CONFIG
	  	{
		
			
			// Encoding formats
			
			public const uint BE_CONFIG_MP3	 = 0;
			public const uint BE_CONFIG_LAME = 256;

			public uint	dwConfig;	
			public struct_un union;

			
			// Constructors	
			
			public 
			BE_CONFIG(LibsndfileWrapper.SF_INFO soundInfo, uint mp3bitrate)
			{
				
				this.dwConfig = BE_CONFIG_LAME;
				this.union = new struct_un(soundInfo, mp3bitrate);
				
			}

			
			public 
			BE_CONFIG(LibsndfileWrapper.SF_INFO soundInfo) : 
			this(soundInfo, 128)
			{}
	
			
	  	} 
	   
		
		// Version struct 

		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
	  	public class BE_VERSION
		{ 
	
			public const uint BE_MAX_HOMEPAGE	= 256;
			public byte	byDLLMajorVersion;
			public byte	byDLLMinorVersion;
			public byte	byMajorVersion;
			public byte	byMinorVersion;
			
			// DLL Release date:
			
			public byte	byDay;
			public byte	byMonth;
			public ushort	wYear;
			
			// Homepage URL:
			// Sizeconst=BE_MAX_HOMEPAGE+1
			
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=257)]
			public string	zHomepage;	
			
			public byte	byAlphaLevel;
			public byte	byBetaLevel;
			public byte	byMMXEnabled;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=125)]
			public byte[]	btReserved;


			// Constructor		
			
			public BE_VERSION()
			{
			  btReserved = new byte[125];
			}
	
		}
	  
	

		// Error codes

		public const uint BE_ERR_SUCCESSFUL = 0;
		public const uint BE_ERR_INVALID_FORMAT = 1;
		public const uint BE_ERR_INVALID_FORMAT_PARAMETERS = 2;
		public const uint BE_ERR_NO_MORE_HANDLES = 3;
		public const uint BE_ERR_INVALID_HANDLE = 4;
	 
		
		
		// Method declarations hence

		
		[DllImport("lame_enc.dll")]
		public static extern uint 
		beInitStream(BE_CONFIG pbeConfig, ref uint dwSamples, 
				ref uint dwBufferSize, ref uint phbeStream);
	
		
		[DllImport("lame_enc.dll")]  
		public static extern uint 
		beEncodeChunk(uint hbeStream, uint nSamples, short[] pInSamples, 
				[In, Out] byte[] pOutput, ref uint pdwOutput);

		
		[DllImport("lame_enc.dll")]
		protected static extern uint 
		beEncodeChunk(uint hbeStream, uint nSamples, IntPtr pSamples, 
				[In, Out] byte[] pOutput, ref uint pdwOutput);

		
		
		// Wrap beEncodeChunk further,
		// handle garbage collection on
		// pointer

		public static uint 
		EncodeChunk(uint hbeStream, byte[] buffer, int index, 
				uint nBytes, byte[] pOutput, ref uint pdwOutput)
		{
			
			
  			uint res;
		  	GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

		  try
		  {

  			IntPtr ptr = (IntPtr)
				(handle.AddrOfPinnedObject().ToInt32()+index);

			res = beEncodeChunk(hbeStream, 
					nBytes/2, ptr, pOutput, ref pdwOutput);

		  }
		  finally
		  {

			  handle.Free();	

		  }
		  
		  return res;

		}


		// Overloaded EncodeChunk()
		
		public static uint 
		EncodeChunk(uint hbeStream, byte[] buffer, 
				byte[] pOutput, ref uint pdwOutput)
		{

  			return EncodeChunk(hbeStream, buffer, 
					0, (uint)buffer.Length, pOutput, ref pdwOutput);

		}

		
		[DllImport("lame_enc.dll")]
		public static extern uint 
		beDeinitStream(uint hbeStream, 
				[In, Out] byte[] pOutput, ref uint pdwOutput);

		
		[DllImport("lame_enc.dll")]
		public static extern uint 
		beCloseStream(uint hbeStream);

		
		[DllImport("lame_enc.dll")]
		public static extern void 
		beVersion([Out] BE_VERSION pbeVersion);
		
		
		[DllImport("lame_enc.dll", CharSet=CharSet.Ansi)]
		public static extern void 
		beWriteVBRHeader(string pszMP3FileName);
		
		
		[DllImport("lame_enc.dll")]
		public static extern uint
		beEncodeChunkFloatS16NI(uint hbeStream, uint nSamples, 
				[In]float[] buffer_l, [In]float[] buffer_r, 
				[In, Out]byte[] pOutput, ref uint pdwOutput);
		
		
		[DllImport("lame_enc.dll")]
		public static extern uint
		beFlushNoGap(uint hbeStream, 
				[In, Out]byte[] pOutput, ref uint pdwOutput);
		
		
		[DllImport("lame_enc.dll", CharSet=CharSet.Ansi)]
		public static extern uint
		beWriteInfoTag(uint hbeStream, string lpszFileName);		
	
		


	} 

	
}
