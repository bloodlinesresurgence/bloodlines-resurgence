///////////////////////////////////////////////////////////////////////////////
//
// Aumpel -- Primary interface to aumplib
//
// This class abstracts the other participant classes so that a single
// set of methods can be used to convert one sound file type to another.
//
// NOTE: Usage of the overload of the Convert() method in this class that 
// matches the parameters
// 
// 		Convert(
// 			string inputFile, 
// 			string outputFile, 
// 			soundFormat convertTo, 
// 			MadlldlibWrapper.Callback updateStatus
// 			)
//
// may infringe rights of the GPL license that is required on the 
// MadlldlibWrapper class if used in a closed source project. See the readme.txt
// file in this distribution for details.
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
	using System.Text;
	using System.Runtime.InteropServices;

	// alias; see SF_INFO struct:

	using sf_count_t = System.Int64;	

	
#if PLATFORM_64
	using size_t = System.UInt64;
#else
	using size_t = System.UInt32;
#endif

	
	class Aumpel
	{

		
		// Convert to short pathnames
		// (madlldlib)

		[DllImport("kernel32.dll", SetLastError=true, CharSet=CharSet.Auto)]
		[return:MarshalAs(UnmanagedType.U4)]
		public static extern int 
		GetShortPathName(
			[MarshalAs(UnmanagedType.LPTStr)] 
			string inputFilePath,
			[MarshalAs(UnmanagedType.LPTStr)] 
			StringBuilder outputFilePath,
			[MarshalAs(UnmanagedType.U4)] 
			int bufferSize);
		

		
		// Delegate for callback 

		public delegate void 
		Reporter(int totalBytes, int processedBytes, Aumpel aumpelObj);		


		// Flags for conversion hence

		// soundFormat values are taken mostly
		// from libsndfile's definition, 
		// since it handles the bulk of the
		// transformation types (although it
		// possibly will receive the slightest 
		// use, not having MP3 capabilities). I 
		// have added a value representing MP3 
		// for use with aumplib.
		
		public enum soundFormat : int 
		{

			// Main types supported
			
			WAV = LibsndfileWrapper.soundFormat.SF_FORMAT_WAV,
			AIFF = LibsndfileWrapper.soundFormat.SF_FORMAT_AIFF,
			AU = LibsndfileWrapper.soundFormat.SF_FORMAT_AU,
			RAW = LibsndfileWrapper.soundFormat.SF_FORMAT_RAW,
			PAF = LibsndfileWrapper.soundFormat.SF_FORMAT_PAF,
			SVX = LibsndfileWrapper.soundFormat.SF_FORMAT_SVX,
			NIST = LibsndfileWrapper.soundFormat.SF_FORMAT_NIST,
			VOC = LibsndfileWrapper.soundFormat.SF_FORMAT_VOC,
			IRCAM = LibsndfileWrapper.soundFormat.SF_FORMAT_IRCAM,
			W64 = LibsndfileWrapper.soundFormat.SF_FORMAT_W64,
			MAT4 = LibsndfileWrapper.soundFormat.SF_FORMAT_MAT4,
			MAT5 = LibsndfileWrapper.soundFormat.SF_FORMAT_MAT5,
			PVF = LibsndfileWrapper.soundFormat.SF_FORMAT_PVF,
			XI = LibsndfileWrapper.soundFormat.SF_FORMAT_XI,
			HTK = LibsndfileWrapper.soundFormat.SF_FORMAT_HTK,
			SDS = LibsndfileWrapper.soundFormat.SF_FORMAT_SDS,
			MP3 = 0x120000,
			
			// Subtypes hence

			PCM_S8 = LibsndfileWrapper.soundFormat.SF_FORMAT_PCM_S8,
			PCM_16 = LibsndfileWrapper.soundFormat.SF_FORMAT_PCM_16,
			PCM_24 = LibsndfileWrapper.soundFormat.SF_FORMAT_PCM_24,
			PCM_32 = LibsndfileWrapper.soundFormat.SF_FORMAT_PCM_32,

			PCM_U8 = LibsndfileWrapper.soundFormat.SF_FORMAT_PCM_U8,

			FLOAT = LibsndfileWrapper.soundFormat.SF_FORMAT_FLOAT,
			DOUBLE = LibsndfileWrapper.soundFormat.SF_FORMAT_DOUBLE,

			ULAW = LibsndfileWrapper.soundFormat.SF_FORMAT_ULAW,
			ALAW = LibsndfileWrapper.soundFormat.SF_FORMAT_ALAW,
			IMA_ADPCM = LibsndfileWrapper.soundFormat.SF_FORMAT_IMA_ADPCM,
			MS_ADPCM = LibsndfileWrapper.soundFormat.SF_FORMAT_MS_ADPCM,

			GSM610 = LibsndfileWrapper.soundFormat.SF_FORMAT_GSM610,
			VOX_ADPCM = LibsndfileWrapper.soundFormat.SF_FORMAT_VOX_ADPCM,

			G721_32 = LibsndfileWrapper.soundFormat.SF_FORMAT_G721_32,
			G723_24 = LibsndfileWrapper.soundFormat.SF_FORMAT_G723_24,
			G723_40 = LibsndfileWrapper.soundFormat.SF_FORMAT_G723_40,
			DWVW_12 = LibsndfileWrapper.soundFormat.SF_FORMAT_DWVW_12,
			DWVW_16 = LibsndfileWrapper.soundFormat.SF_FORMAT_DWVW_16,
			DWVW_24 = LibsndfileWrapper.soundFormat.SF_FORMAT_DWVW_24,
			DWVW_N = LibsndfileWrapper.soundFormat.SF_FORMAT_DWVW_N,

			DPCM_8 = LibsndfileWrapper.soundFormat.SF_FORMAT_DPCM_8,
			DPCM_16 = LibsndfileWrapper.soundFormat.SF_FORMAT_DPCM_16,

			// Endian-ness options:

			ENDIAN_FILE = LibsndfileWrapper.soundFormat.SF_ENDIAN_FILE,
			ENDIAN_LITTLE = LibsndfileWrapper.soundFormat.SF_ENDIAN_LITTLE,
			ENDIAN_BIG = LibsndfileWrapper.soundFormat.SF_ENDIAN_BIG,
			ENDIAN_CPU = LibsndfileWrapper.soundFormat.SF_ENDIAN_CPU,

			SUBMASK = LibsndfileWrapper.soundFormat.SF_FORMAT_SUBMASK,
			TYPEMASK = LibsndfileWrapper.soundFormat.SF_FORMAT_TYPEMASK,
			ENDMASK = LibsndfileWrapper.soundFormat.SF_FORMAT_ENDMASK
			
		}
		
		
		// Data types hence

		private soundFormat inputFileType;
		private soundFormat outputFileType;
		private StringBuilder inputFilePath = new StringBuilder();
		private StringBuilder outputFilePath = new StringBuilder();
		private const int MAX_STRLEN = 260;	
		private LibsndfileWrapper libSndFile = new LibsndfileWrapper();
		private LibsndfileWrapper.SF_INFO soundInfo = 
			new LibsndfileWrapper.SF_INFO();

		public LameWrapper.BE_CONFIG beConfig = null;

			
		// Kill conversion progress

		public bool killSwitch = false;



		// Methods hence

		
		// Constructor the first

		public
		Aumpel()
		{
			inputFilePath.Capacity = MAX_STRLEN;
			outputFilePath.Capacity = MAX_STRLEN;
		}
			


		// Initialize default settings
		// for MP3 (call to initialize
		// structure for customized MP3
		// settings). After this call,
		// settings are changed in the 
		// calling code. Takes the 
		// input WAV file.
		
		public void
		SetMP3(string inputFile)
		{

			// Get information about inputFile,
			// use to give beConfig initial
			// values:

			LibsndfileWrapper.SF_INFO soundInfo = 
				new LibsndfileWrapper.SF_INFO();

			LibsndfileWrapper libSndFile = new LibsndfileWrapper();
			libSndFile.GetSoundFileType(inputFile, ref soundInfo);
			beConfig = new LameWrapper.BE_CONFIG(soundInfo);	

		}

			
		// Given a file, return
		// the sound file format
		
		public soundFormat
		CheckSoundFormat(string inputFile)
		{
			

			// Verify existence of inputFilenm, throw
			// exception if not exist

			if (!System.IO.File.Exists(inputFile))
			{
				throw new FileNotFoundException(
						"Cannot find file " + inputFile);
			}			
			
			// Check for non-MP3 sound file

			LibsndfileWrapper libSndFile = new LibsndfileWrapper();
			LibsndfileWrapper.SF_INFO soundInfo = 
				new LibsndfileWrapper.SF_INFO();

			int i = libSndFile.GetSoundFileType(inputFile, ref soundInfo);


			// First check to see if it's an audio
			// format !MP3. Then check for MP3.
			// If no valid format found, throw
			// exception.
			
			if (soundInfo.format != 0)
			{
				return (soundFormat)soundInfo.format;
			}
			else
			{
				
				MP3Check verifyMP3 = new MP3Check(inputFile, 2);
						
				if (!verifyMP3.Check())
				{
					throw new Exception("Cannot determine sound file type: " +
						inputFile);

				}		
				else
				{
					return soundFormat.MP3;
				}
				
			}
	
		}
			

				
		// Conversion routine the first
		// (lame, libsndfile)
		//
		// Convert() default routine
		// Handles WAV->MP3 encoding,
		// !MP3->!MP3 audio encoding.
	
		public void
		Convert(string inputFile, int convertFrom,
				string outputFile, int convertTo, Reporter updateStatus)
		{


			// Verify existence of inputFilenm, throw
			// exception if not exist

			if (!System.IO.File.Exists(inputFile))
			{
				throw new FileNotFoundException(
						"Cannot find file " + inputFile);
			}
							
				
			// Input file information
			// (Set input file size)
				
			FileInfo fi = new FileInfo(inputFile);		
			int soundFileSize = (int)fi.Length;
			

			// Select conversion routines based
			// on input/output file types

			// If outfile = MP3 then use LameWriter
			// to encode, using the settings in the
			// structure mp3set (essentially the LHV1
			// struct from LameWrapper):

			if ((soundFormat)convertTo == soundFormat.MP3)
			{


				// File to convert from must be WAV	
				
				if ( !( (convertFrom >= (int)soundFormat.WAV) &&
							(convertFrom < (int)soundFormat.AIFF) ) )
				{
					throw new Exception(
							"Cannot encode to MP3 directly from this format (" 
							+ convertFrom + "). Convert to WAV first");
				}

				LameWriter mp3Writer = null;


				// Instantiate LameWriter object 
				// with output filename

				if (beConfig == null) 
				{

					// Use default MP3 output settings
					mp3Writer = new LameWriter( 
						new FileStream(outputFile, FileMode.Create), 
						inputFile);

				}
				else
				{
					
					// Use custom settings

					mp3Writer = new LameWriter( 
						new FileStream(outputFile, FileMode.Create), 
						inputFile, 
						beConfig);

				}



				// open input file for binary reading

				Stream s = new FileStream(inputFile, 
						FileMode.Open, FileAccess.Read);
				
				BinaryReader br = new BinaryReader(s); 


				// setup byte buffer -- use mp3Writer.sampleSize 
				// to ensure correct buffer size

				byte[] wavBuffer = new byte[mp3Writer.sampleSize];	


				// skip WAV header			

				s.Seek(LameWrapper.SKIP_WAV_HEADER, SeekOrigin.Begin);


				// write mp3 file			

				int index = 0;		
				int processedBytes = 0;

				while 
				((index = br.Read(wavBuffer, 0, mp3Writer.sampleSize)) > 0) 
				{
					
					processedBytes += mp3Writer.sampleSize;
					
					// Send to callback:

					updateStatus(soundFileSize, processedBytes, this);


					// Check for kill

					if (this.killSwitch) break;
						
					mp3Writer.Write(wavBuffer, 0, index);		

				}

				
				// Finish up			

				mp3Writer.Close();
				mp3Writer.WriteTags(outputFile);

			
			}

			// Assume libsndfile conversion:

			else
			{


				// Check and make sure we are not 
				// trying to decode MP3->WAV
				
				
				// Instantiate object

				LibsndfileWrapper libSndFile = new LibsndfileWrapper();
				LibsndfileWrapper.SF_INFO soundInfo = 
					new LibsndfileWrapper.SF_INFO();

				
				// Calculate total frames to convert
				
				int i = libSndFile.GetSoundFileType(inputFile, ref soundInfo);

				// Each frame is 16 bits:

				long totalFrames = soundInfo.frames*2;	

							
				// Initialize

				libSndFile.Initialize (inputFile, 
						outputFile, (LibsndfileWrapper.soundFormat)convertTo);


					
				// The main decoding loop
				
				long readCount; 
				long readIndex = 0; 
				
				while ( (readCount = libSndFile.Read()) > 0) 
				{
					
					readIndex += readCount;			

					
					// Send update to delegate.
					// Note that this is in
					// libsndfile specific frames 
					// rather than in actual bytes.
					// 
					// 	readIndex / totalFrames = 
					// 	percentage complete

					updateStatus((int)totalFrames, (int)readIndex, this);			
					
					
					// Check for kill

					if (this.killSwitch) break;
					
					
					// Write to file
					
					libSndFile.Write(readCount);
					
				}
				
				
				// Close up shop
				
				libSndFile.Close();

			}
			

		}
	


		// Conversion routine overload
		// (madlldlib)
		//
		// Convert() routine overload for
		// MP3 decoding using madlldlib. 
		// Expects 'inputFile' to be MP3.
		//
		// Note: Calling this method in
		// your code requires you to 
		// release your code under GPL
		// terms. See the readme.txt
		// file in this distribution
		// for details.
	
		public void
		Convert(string inputFile, string outputFile, 
				soundFormat convertTo, MadlldlibWrapper.Callback updateStatus)
		{		



			// Handle incorrect output types
			
			if ( !(convertTo == soundFormat.WAV) &&
				   !(convertTo == soundFormat.RAW) )
			{	
				throw new Exception(
						"Cannot convert from MP3 to this format directly: " +
						convertTo);
			}			

		
			
			// Check that file is MP3
			// Search through .5 of file
			// before quitting
			
			MP3Check verifyMP3 = new MP3Check(inputFile, 2);
			
			if (!verifyMP3.Check())
			{
				throw new Exception("Not a valid MP3 file: " +
						inputFile);
			}		


			// Convert to short pathnames

			inputFilePath.Capacity = MAX_STRLEN;
			outputFilePath.Capacity = MAX_STRLEN;
			GetShortPathName(inputFile, inputFilePath, MAX_STRLEN);
			GetShortPathName(outputFile, outputFilePath, MAX_STRLEN);			

			// Assign if returned path is not zero:

			if (inputFilePath.Length > 0) 
				inputFile = inputFilePath.ToString();

			if (outputFilePath.Length > 0)
				outputFile = outputFilePath.ToString();			
								
		
			// status/error message reporting. 
			// String length must be set 
			// explicitly

			StringBuilder status = new StringBuilder(); 
			status.Capacity=256;				
			
		
			// call the decoding function
		
			if (convertTo == soundFormat.WAV)
			{
				MadlldlibWrapper.DecodeMP3(inputFile, outputFile,
						MadlldlibWrapper.DEC_WAV, status, updateStatus);
			}

			else

			// Convert to PCM (raw):

			{
				MadlldlibWrapper.DecodeMP3(inputFile, outputFile, 
						MadlldlibWrapper.DEC_PCM, status, updateStatus);
			}


			// this prevents garbage collection
			// from occurring on callback

			GC.KeepAlive(updateStatus);				
				

		}



		// Conversion routine overload
		// (madNPObjectsrv)
		// 
		// Convert() routine overload for
		// MP3 decoding using madnpsrv.
		// Expects 'inputFile' to be MP3

		public void
		Convert(string inputFile, string outputFile, 
				soundFormat convertTo, MadnpsrvWrapper.Callback updateStatus)
		{


			// Handle incorrect output types
			
			if ( !(convertTo == soundFormat.WAV) &&
				   !(convertTo == soundFormat.RAW) )
			{	
				throw new Exception(
						"Cannot convert from MP3 to this format directly: " +
						convertTo);
			}			

		
			
			// Check that file is MP3
			// Search through 1/2 of file
			// before quitting
			
			MP3Check verifyMP3 = new MP3Check(inputFile, 2);
			
			if (!verifyMP3.Check())
			{
				throw new Exception("Not a valid MP3 file: " + inputFile);
			}			


			// Create class instance

			MadnpsrvWrapper madNPObject = new MadnpsrvWrapper();			
			

			// call the decoding function
		
			if (convertTo == soundFormat.WAV)
			{
				// Startup pipe server:
				madNPObject.StartPipe(inputFile, 
						outputFile, MadnpsrvWrapper.DEC_WAV);				
			}

			else

			// Convert to PCM (raw):

			{
				// Startup pipe server:
				madNPObject.StartPipe(inputFile, 
						outputFile, MadnpsrvWrapper.DEC_PCM);
			}
			
			
			// Call read function (passing delegate
			// function)

			madNPObject.ReadPipe(updateStatus);			
			
			
			// Kill the pipe server,
			// just in case it is still
			// running

			madNPObject.StopPipe();			

		}
		
		
	} 


} 



