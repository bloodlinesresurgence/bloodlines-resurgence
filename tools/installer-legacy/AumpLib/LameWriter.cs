///////////////////////////////////////////////////////////////////////////////
//
// LameWriter -- Derived BinaryWriter class using LameWrapper
//
// Uses the LameWrapper to interface to the lame_enc.dll and convert from
// WAV to MP3 sound formats. Provides overrides of the Write() and Close()
// methods of the BinaryWriter class.
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

	// alias--see LibsndfileWrapper.SF_INFO struct:

	using sf_count_t = System.Int64;	

	
#if PLATFORM_64
	using size_t = System.UInt64;
#else
	using size_t = System.UInt32;
#endif


	class LameWriter : BinaryWriter 
	{


		// Declarations

		private LibsndfileWrapper libSndFile = new LibsndfileWrapper();			
		private LibsndfileWrapper.SF_INFO soundInfo = 
			new LibsndfileWrapper.SF_INFO();

		private uint samples = 0;
		public int sampleSize = 0;			// used for read buffer 
											// in calling code
		private uint bufferSize = 0;
		private uint hLameStream = 0;
		private uint dwOut = 0;
		private byte[] mp3Buffer = null;	// mp3 buffer		
		public LameWrapper.BE_CONFIG beConfig = null;



		// Constructors hence


		// Write to stream

		public 
		LameWriter(Stream outputStream, string inputFile) :
		base(outputStream)   
		{

			
			// GetSoundFileType input file

			int rtn = libSndFile.GetSoundFileType(inputFile, ref soundInfo);	


			// Initialize

			beConfig = new LameWrapper.BE_CONFIG(soundInfo);
			try 
			{
			
				uint rslt = 
					LameWrapper.beInitStream(beConfig, 
							ref samples, ref bufferSize, ref hLameStream);
				
				if (rslt != LameWrapper.BE_ERR_SUCCESSFUL) 
				{
					throw new ApplicationException(
							string.Format(
								"LameWrapper.beInitStream failed with code: {0}",
							   	rslt));
				}
				
				// Samples are 2 bytes wide:
				
				sampleSize = (int)samples*2;			

				// MP3 buffer
				
				mp3Buffer = new byte[bufferSize];			

			}
			catch 
			{
				throw;
			} 
	
			
		}



		// Overload constructor: Send 
		// custom BE_CONFIG

		public 
		LameWriter(Stream outputStream, 
				string inputFile, LameWrapper.BE_CONFIG beConfig) :
		base(outputStream)   
		{

			// GetSoundFileType input file
			int rtn = libSndFile.GetSoundFileType(inputFile, ref soundInfo);	


			// Initialize

			try 
			{
			
				uint rslt = LameWrapper.beInitStream(beConfig, 
						ref samples, ref bufferSize, ref hLameStream);
				
				if (rslt != LameWrapper.BE_ERR_SUCCESSFUL) 
				{
					throw new ApplicationException(
							string.Format(
								"LameWrapper.beInitStream failed with code: {0}",
							   	rslt));
				}
				
				// Samples are 2 bytes wide:

				sampleSize = (int)samples*2;			
				
				// MP3 buffer:

				mp3Buffer = new byte[bufferSize];			

			}
			catch 
			{
				throw;
			} 			
	
			
		} 


		// Methods hence

			
		// Write method override

		public override void 
		Write(byte[] buffer, int index, int count) 
		{

			
			// Encode samples

			uint err = LameWrapper.EncodeChunk(
					hLameStream, buffer, mp3Buffer, ref dwOut);
			
			if (err != LameWrapper.BE_ERR_SUCCESSFUL) 
			{
				throw new ApplicationException(
						string.Format(
							"LameWrapper.EncodeChunk failed with code: {0}", 
							err));
			}
			
			
			// Write to the file	

			if (dwOut > 0) 
			{		
				base.Write(mp3Buffer,0,(int)dwOut);
			}

			
		} 		


		// Close method override

		public override void 
		Close() 
		{

			uint err = 0;


			// De-init stream

			err = LameWrapper.beDeinitStream(hLameStream, mp3Buffer, ref dwOut);
			
			if (err != LameWrapper.BE_ERR_SUCCESSFUL)
				throw new ApplicationException("beDeinitStream failed");

			
			// Write any bytes returned from deinit

			if (dwOut > 0) 
			{
				base.Write(mp3Buffer,0,(int)dwOut);						
			}		


			// Close stream
			
			LameWrapper.beCloseStream(hLameStream);
			base.Close();
			

		}


		
		// Write tags

		public void 
		WriteTags (string outputFile) 
		{


			// Write INFO/VBR

			uint err = LameWrapper.beWriteInfoTag(hLameStream, outputFile);
			if (err != LameWrapper.BE_ERR_SUCCESSFUL)
				throw new ApplicationException("WriteInfoTag failed");	
			
		} 

		
	} 


}


