///////////////////////////////////////////////////////////////////////////////
//
// LibsndfileWrapper -- Wrapper class to libsndfile library
//
// Provides a P/Invoke wrapper into the libsndfile library. This library can
// be downloaded at http://www.mega-nerd.com/libsndfile/. It can convert many
// sound formats. The class interfaces functions and data structures, and
// provides default methods for conversion.
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
	using System.Runtime.InteropServices;
	using sf_count_t = System.Int64;	// Alias; see SF_INFO struct

	
#if PLATFORM_64
	using size_t = System.UInt64;
#else
	using size_t = System.UInt32;
#endif
	

	
	class LibsndfileWrapper 
	{

	
		// Type declarations hence
	
		public const sf_count_t BUFFER_LEN = 4096;		


		
		// 'Globals' for use with the convert methods below		

		private string 	inputFile;
		private string 	outputFile;
		private SF_INFO soundInfo = new SF_INFO();
		private IntPtr 	pInputFile;
		private IntPtr 	pOutputFile;
		private soundFormat formatType;
		private float[] conversionBuffer = new float[BUFFER_LEN];
				


		// Sound file formats

		public enum soundFormat 
		{
			// Microsoft WAV format (little endian):
			SF_FORMAT_WAV			= 0x010000,		
			
			// Apple/SGI AIFF format (big endian):
			SF_FORMAT_AIFF			= 0x020000,
			
			// Sun/NeXT AU format (big endian):
			SF_FORMAT_AU			= 0x030000,
			
			// RAW PCM data:
			SF_FORMAT_RAW			= 0x040000,
			
			// Ensoniq PARIS file format:
			SF_FORMAT_PAF			= 0x050000,		

			// Amiga IFF / SVX8 / SV16 format:
			SF_FORMAT_SVX			= 0x060000,		
			
			// Sphere NIST format:
			SF_FORMAT_NIST			= 0x070000,		

			// VOC files:
			SF_FORMAT_VOC			= 0x080000,		

			// Berkeley/IRCAM/CARL:
			SF_FORMAT_IRCAM			= 0x0A0000,		
			
			// Sonic Foundry's 64 bit RIFF/WAV:
			SF_FORMAT_W64			= 0x0B0000,
			
			// Matlab (tm) V4.2 / GNU Octave 2.0:
			SF_FORMAT_MAT4			= 0x0C0000,		

			// Matlab (tm) V5.0 / GNU Octave 2.1:
			SF_FORMAT_MAT5			= 0x0D0000,	
			
			// Portable Voice Format:
			SF_FORMAT_PVF			= 0x0E0000,	
			
			// Fasttracker 2 Extended Instrument:
			SF_FORMAT_XI			= 0x0F0000,	
			
			// HMM Tool Kit format:
			SF_FORMAT_HTK			= 0x100000,		
			
			// Midi Sample Dump Standard:
			SF_FORMAT_SDS			= 0x110000,		
			
			// subtypes from here on.

			SF_FORMAT_PCM_S8		= 0x0001,		// Signed 8 bit data 
			SF_FORMAT_PCM_16		= 0x0002,		// Signed 16 bit data 
			SF_FORMAT_PCM_24		= 0x0003,		// Signed 24 bit data 
			SF_FORMAT_PCM_32		= 0x0004,		// Signed 32 bit data 

			// Unsigned 8 bit data (WAV and RAW only):
			SF_FORMAT_PCM_U8		= 0x0005,

			SF_FORMAT_FLOAT			= 0x0006,		// 32 bit float data 
			SF_FORMAT_DOUBLE		= 0x0007,		// 64 bit float data 

			SF_FORMAT_ULAW			= 0x0010,		// U-Law encoded. 
			SF_FORMAT_ALAW			= 0x0011,		// A-Law encoded. 
			SF_FORMAT_IMA_ADPCM		= 0x0012,		// IMA ADPCM. 
			SF_FORMAT_MS_ADPCM		= 0x0013,		// Microsoft ADPCM. 

			SF_FORMAT_GSM610		= 0x0020,		// GSM 6.10 encoding. 
			SF_FORMAT_VOX_ADPCM		= 0x0021,		// OKI / Dialogix ADPCM 

			// 32kbs G721 ADPCM encoding:
			SF_FORMAT_G721_32		= 0x0030,		

			// 24kbs G723 ADPCM encoding:
			SF_FORMAT_G723_24		= 0x0031,		

			// 40kbs G723 ADPCM encoding:
			SF_FORMAT_G723_40		= 0x0032,		

			// 12 bit Delta Width Variable Word encoding:
			SF_FORMAT_DWVW_12		= 0x0040, 		
			
			// 16 bit Delta Width Variable Word encoding:
			SF_FORMAT_DWVW_16		= 0x0041, 		

			// 24 bit Delta Width Variable Word encoding:
			SF_FORMAT_DWVW_24		= 0x0042, 		

			// N bit Delta Width Variable Word encoding:
			SF_FORMAT_DWVW_N		= 0x0043, 		

			// 8 bit differential PCM (XI only):
			SF_FORMAT_DPCM_8		= 0x0050,	
	
			// 16 bit differential PCM (XI only):
			SF_FORMAT_DPCM_16		= 0x0051,	
			
			// Endian-ness options.

			SF_ENDIAN_FILE			= 0x00000000,	// Default file endian-ness. 
			SF_ENDIAN_LITTLE		= 0x10000000,	// Force little endian-ness. 
			SF_ENDIAN_BIG			= 0x20000000,	// Force big endian-ness. 
			SF_ENDIAN_CPU			= 0x30000000,	// Force CPU endian-ness. 

			SF_FORMAT_SUBMASK		= 0x0000FFFF,
			SF_FORMAT_TYPEMASK		= 0x0FFF0000,
			SF_FORMAT_ENDMASK		= 0x30000000
		
		}


		// Modes and other	

		public enum fileMode
		{	
			SF_FALSE	= 0, 				// True and false 
			SF_TRUE		= 1,

			SFM_READ	= 0x10,				// Modes for opening files. 
			SFM_WRITE	= 0x20,
			SFM_RDWR	= 0x30
		}

		
		// All important SF_INFO structure

		[StructLayout(LayoutKind.Sequential)]
		public struct SF_INFO 
		{	

			// Used to be called samples.  
			// Changed to avoid confusion: 

			public sf_count_t	frames ;
			
			public int			samplerate ;
			public int			channels ;
			public int			format ;
			public int			sections ;
			public int			seekable ;

		};


		// Public error codes

		public enum errorCode
		{	
			SF_ERR_NO_ERROR     		= 0,
			SF_ERR_UNRECOGNISED_FORMAT	= 1,
			SF_ERR_SYSTEM				= 2
		};		





		// External function declarations hence
		
		[DllImport("libsndfile.dll")]
		public static extern IntPtr 
		sf_open ([MarshalAs(UnmanagedType.LPStr)] string path, 
				int mode, 
				ref SF_INFO soundInfo);
		

		[DllImport("libsndfile.dll")]
		public static extern IntPtr 
		sf_open_fd (int fd, int mode, 
				ref SF_INFO soundInfo, int close_desc);

		
		[DllImport("libsndfile.dll")]
		public static extern sf_count_t 
		sf_seek (IntPtr sndfile, 
				sf_count_t frames, int whence);	
		

		// 'IntPtr data' replaces 'void *data' in C functions

		[DllImport("libsndfile.dll")] 
		public static extern int 
		sf_command (IntPtr sndfile, 
				int command, IntPtr data, int datasize);
		
		
		[DllImport("libsndfile.dll")]
		public static extern int 
		sf_error (IntPtr sndfile);
		
		
		[DllImport("libsndfile.dll")]
		public static extern IntPtr 
		sf_strerror (IntPtr sndfile);
	
		
		[DllImport("libsndfile.dll")]
		public static extern IntPtr 
		sf_error_number	(int errnum);

		
		[DllImport("libsndfile.dll")]
		public static extern int 
		sf_perror (IntPtr sndfile);		

		
		[DllImport("libsndfile.dll")]
		public static extern int 
		sf_error_str (IntPtr sndfile, 
				[MarshalAs(UnmanagedType.LPStr)] string str, size_t len);
		
		
		[DllImport("libsndfile.dll")]
		public static extern int 
		sf_format_check (ref SF_INFO info);
		
		
		[DllImport("libsndfile.dll")]
		public static extern int 
		sf_close (IntPtr sndfile);    	

		
		[DllImport("libsndfile.dll")]
		public static extern sf_count_t 
		sf_read_float (IntPtr sndfile, 
				float[] ptr, sf_count_t items);
		
		
		[DllImport("libsndfile.dll")]
		public static extern sf_count_t 
		sf_write_float (IntPtr sndfile, 
				float[] ptr, sf_count_t items);		

		
		[DllImport("libsndfile.dll")]
		public static extern sf_count_t 
		sf_readf_short (IntPtr sndfile, 
				short[] ptr, sf_count_t frames);
		
		
		[DllImport("libsndfile.dll")]
		public static extern sf_count_t 
		sf_writef_short (IntPtr sndfile, 
				short[] ptr, sf_count_t frames);
		
		
		[DllImport("libsndfile.dll")]
		public static extern sf_count_t 
		sf_readf_int (IntPtr sndfile, 
				int[] ptr, sf_count_t frames);
		
		
		[DllImport("libsndfile.dll")]
		public static extern sf_count_t 
		sf_writef_int (IntPtr sndfile, 
				int[] ptr, sf_count_t frames);
		
		
		[DllImport("libsndfile.dll")]
		public static extern sf_count_t 
		sf_readf_float (IntPtr sndfile, 
				float[] ptr, sf_count_t frames);
		
		
		[DllImport("libsndfile.dll")]
		public static extern sf_count_t 
		sf_writef_float (IntPtr sndfile, 
				float[] ptr, sf_count_t frames);
		
		
		[DllImport("libsndfile.dll")]
		public static extern sf_count_t 
		sf_readf_double (IntPtr sndfile, 
				double[] ptr, 
				sf_count_t frames);
		
		
		[DllImport("libsndfile.dll")]
		public static extern sf_count_t 
		sf_writef_double (IntPtr sndfile, 
				double[] ptr, sf_count_t frames);
		
		
		[DllImport("libsndfile.dll")]
		public static extern sf_count_t 
		sf_read_short (IntPtr sndfile, 
				short[] ptr, sf_count_t items);
		
		
		[DllImport("libsndfile.dll")]
		public static extern sf_count_t 
		sf_write_short (IntPtr sndfile, 
				short[] ptr, sf_count_t items);
		
		
		[DllImport("libsndfile.dll")]
		public static extern sf_count_t 
		sf_read_int (IntPtr sndfile, 
				int[] ptr, sf_count_t items);
		
		
		[DllImport("libsndfile.dll")]
		public static extern sf_count_t 
		sf_write_int (IntPtr sndfile, 
				int[] ptr, sf_count_t items);

		
		[DllImport("libsndfile.dll")]
		public static extern sf_count_t 
		sf_read_double (IntPtr sndfile, 
				double[] ptr, sf_count_t items);

		
		[DllImport("libsndfile.dll")]
		public static extern sf_count_t 
		sf_write_double (IntPtr sndfile, 
				double[] ptr, sf_count_t items);
		
		


		
		// Class methods hence


		// Return information about sound file

		public int 
		GetSoundFileType(string filename, ref SF_INFO soundInfo) 
		{
					
			// Open file, pass it reference 
			// to SF_INFO structure

			IntPtr errorRef = sf_open (filename, 
					(int)fileMode.SFM_READ, ref soundInfo);

			int status = sf_error(errorRef);
			int c = sf_close(errorRef);

			
			// return success or error

			return status; 	
			
		}
		

		// Performs a conversion between two files with format 
		// specified in 'formatType'. Returns -1 on general error, 
		// otherwise something from errorCode. 0 return is desired

		public int 
		Initialize (string inputFile, 
			string outputFile, soundFormat formatType)
		{


			// Setup
			
			this.inputFile = inputFile; 			
			this.outputFile = outputFile;
			this.formatType = formatType;
			

			// Read in sound file to convert

			pInputFile = sf_open (inputFile, 
					(int)fileMode.SFM_READ, ref soundInfo);

		
			// Exit if error was thrown; 0 = NULL pointer		

			if ( (int)pInputFile == 0 ) 
			{
				return sf_error(pInputFile);
			}	


			// Set the file type for the output file

			soundInfo.format = (int)(formatType);
	
			
			// Check that SF_INFO is valid	

			if ( sf_format_check(ref soundInfo) == 0 ) 
			{
				return -1;
			}

			
			// Open output file	

			pOutputFile = sf_open (outputFile, 
					(int)fileMode.SFM_WRITE, ref soundInfo);


			// Exit if error was thrown			

			if ( (int)pOutputFile == 0 ) 
			{
				return sf_error(pOutputFile);
			}	

			return 0;	// Success

			
		} 
		

		
		// Initialize() overload:
		// no output filename specified

		public int 
		Initialize (string inputFile, soundFormat formatType) 
		{

			string outputFile = inputFile;	

			if (formatType >= soundFormat.SF_FORMAT_WAV && 
					formatType < soundFormat.SF_FORMAT_AIFF) 
			{
				outputFile = inputFile + ".wav";
			}
			else if (formatType >= soundFormat.SF_FORMAT_AIFF && 
					formatType < soundFormat.SF_FORMAT_AU) 
			{
				outputFile = inputFile + ".aiff";
			}
			else if (formatType >= soundFormat.SF_FORMAT_AU && 
					formatType < soundFormat.SF_FORMAT_RAW) 
			{
				outputFile = inputFile + ".au";
			}
			else if (formatType >= soundFormat.SF_FORMAT_RAW && 
					formatType < soundFormat.SF_FORMAT_PAF) 
			{
				outputFile = inputFile + ".raw";
			}
			else if (formatType >= soundFormat.SF_FORMAT_PAF && 
					formatType < soundFormat.SF_FORMAT_SVX) 
			{
				outputFile = inputFile + ".paf";
			}
			else if (formatType >= soundFormat.SF_FORMAT_SVX && 
					formatType < soundFormat.SF_FORMAT_NIST) 
			{
				outputFile = inputFile + ".svx";
			}
			else if (formatType >= soundFormat.SF_FORMAT_NIST && 
					formatType < soundFormat.SF_FORMAT_VOC) 
			{
				outputFile = inputFile + ".nist";
			}
			else if (formatType >= soundFormat.SF_FORMAT_VOC && 
					formatType < soundFormat.SF_FORMAT_IRCAM) 
			{
				outputFile = inputFile + ".voc";
			}
			else if (formatType >= soundFormat.SF_FORMAT_IRCAM && 
					formatType < soundFormat.SF_FORMAT_W64) 
			{
				outputFile = inputFile + ".ircam";
			}
			else if (formatType >= soundFormat.SF_FORMAT_W64 && 
					formatType < soundFormat.SF_FORMAT_MAT4) 
			{
				outputFile = inputFile + ".w64";
			}
			else if (formatType >= soundFormat.SF_FORMAT_MAT4 && 
					formatType < soundFormat.SF_FORMAT_PVF) 
			{
				outputFile = inputFile + ".mat4";
			}			
			else if (formatType >= soundFormat.SF_FORMAT_PVF && 
					formatType < soundFormat.SF_FORMAT_XI) 
			{
				outputFile = inputFile + ".pvf";
			}			
			else if (formatType >= soundFormat.SF_FORMAT_XI && 
					formatType < soundFormat.SF_FORMAT_HTK) 
			{
				outputFile = inputFile + ".xi";
			}			
			else if (formatType >= soundFormat.SF_FORMAT_HTK && 
					formatType < soundFormat.SF_FORMAT_SDS) 
			{
				outputFile = inputFile + ".htk";
			}			
			else if (formatType >= soundFormat.SF_FORMAT_SDS) 
			{
				outputFile = inputFile + ".sds";
			}
			else 
			{
				outputFile = inputFile + ".sout";
			}
			
			
			return Initialize (inputFile, outputFile, formatType);


		}

		
		// Read sound file, return error

		public long 
		Read() 
		{		
			return sf_read_float (pInputFile, conversionBuffer, BUFFER_LEN);
		} 
		

		// Write sound file, return error

		public long 
		Write(sf_count_t writeItems) 
		{		
			return sf_write_float (pOutputFile, conversionBuffer, writeItems);
		} 

		
		// Close up shop

		public void 
		Close() 
		{

			sf_close(pInputFile);
			sf_close(pOutputFile);

		} 
		
	

	} 



} 


