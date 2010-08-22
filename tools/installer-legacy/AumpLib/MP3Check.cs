///////////////////////////////////////////////////////////////////////////////
//
// MP3Check -- Verify MP3 stream in a file
// 
// Searches through a given file and determines in a reasonable manner whether
// or not it is an MP3 file. Does this by scanning for the first header, then
// calculating the location of the 2nd, then 3rd. If it cannot do this, the
// check fails and the file is determined not be an MP3. If it succeeds, the
// file is determined to be an MP3.
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
	using System.Text;
	using System.IO;

	
	public class MP3Check
	{

		
		// Internal variables

		private string inputFile;
		private int filePart = 1;
		public MP3Details mp3Info;


		
		// Structure for CheckDetails()
		
		public struct MP3Details
		{
			public int bitrate;
			public int frequency;
		}


		// Constructor the first
		// Use default filePart--i.e.
		// search through the entire 
		// file.

		public MP3Check(string inputFile)
		{
			this.inputFile = inputFile;
			this.mp3Info = new MP3Details();
		}


		// Constructor the second
		// filePart specified--search
		// through a portion of mp3
		// file

		public MP3Check(string inputFile, int filePart)
		{
			this.inputFile = inputFile;
			this.filePart = filePart;
			this.mp3Info = new MP3Details();
		}
		

		
		
		// Ensure file is mp3

		public bool
		Check()
		{
			
			if (!File.Exists(inputFile)) 
				throw new FileNotFoundException(inputFile + 
						" was not found"); 
			
			// Open filename:
			
			FileStream fs = new FileStream (inputFile, 
					FileMode.Open, FileAccess.Read);

			//file position:

			int filePosition = 0; 
			byte[] mpegBytes = new byte[4];
			uint mpegHeader;


			// Read through 'filePart' portion
			// of the file before quitting

			do
			{

				fs.Position = filePosition;
				fs.Read(mpegBytes,0,4);
				filePosition++;

				
				// convert byte array into 
				// ulong

				mpegHeader = SetHeader(mpegBytes);

				
				// Test 1 for valid values in 
				// header (see MP3 header 
				// documentation for details)

				if ( IsValidHeader(mpegHeader) )
				{				

					
					// Put MP3 details in MP3Details structure:
					// Parse out information from header

					int layer = (int)((mpegHeader>>17) & 3);
					int bitIndex = (int)((mpegHeader>>12) & 15);
					int freqIndex = (int)((mpegHeader>>10) & 3);
					int pad = (int)((mpegHeader>>9) & 1);
					int version = (int)((mpegHeader>>19) & 3);

					// Determine bitrate from index
					
					try
					{
						mp3Info.bitrate = 
							MapBitrate(layer, version, bitIndex) * 1000;		
					}
					catch (IndexOutOfRangeException e)
					{
						mp3Info.bitrate = 1;	
					}

					
					// Determine frequency from index

					try
					{
						mp3Info.frequency = 
							CalculateFrequency(freqIndex, version);
					}
					catch (IndexOutOfRangeException e)
					{
						mp3Info.frequency = 1;
					}

					

					
					// Set position for next header:

					fs.Position = fs.Position +
						CalculateFrameSize(mpegHeader) - 4;
				
					
					// Read 2nd header:

					fs.Read(mpegBytes,0,4);
					mpegHeader = SetHeader(mpegBytes);
					

					if (IsValidHeader(mpegHeader))
					{

						
						// Read 3rd header:

						fs.Position = fs.Position +
							CalculateFrameSize(mpegHeader) - 4;

						fs.Read(mpegBytes,0,4);	
						mpegHeader = SetHeader(mpegBytes);						

	
						if (IsValidHeader(mpegHeader))
						{
							fs.Close();
							return true;
						}

						// Reset to original file 
						// position+1000, continue:

						else
						{
							filePosition = (int)fs.Position+1000;
							fs.Position = filePosition;
						}
						
					}

					// Reset to original file 
					// position+1000, continue:
					
					else 
					{
						filePosition = (int)fs.Position+1000;
						fs.Position = filePosition;
					}
					
			
				}
			

			}
			while (fs.Position <= (fs.Length/filePart));

			fs.Close();
			return false;
			
		}


		

	
		
		// Ensures that mpegHeader could be
		// a valid MP3 header
		
		private bool
		IsValidHeader(uint mpegHeader)
		{
			

			if ( 
				// Frame sync:
				
				(((mpegHeader>>21) & 2047) == 2047) &&
					
				// MPEG audio version id:
				
				((((mpegHeader>>19) & 3) & 3) != 1) &&
				
				// Layer description:
				
				((((mpegHeader>>17) & 3) & 3) != 0) &&
				
				// Bitrate index:
			
				((((mpegHeader>>12) & 15) & 15) != 0) &&
				((((mpegHeader>>12) & 15) & 15) != 15) &&

				// Sampling rate frequencyuency:

				((((mpegHeader>>10) & 3) & 3) != 3) &&

				// Emphasis:

				(((mpegHeader & 3) & 3) != 2)						
			)
			{				
				return true;						
			}
			else
			{
				return false;
			}


		}
		

		// Calculate frame size
		
		private int
		CalculateFrameSize(uint mpegHeader)
		{

			int layer = (int)((mpegHeader>>17) & 3);
			int bitIndex = (int)((mpegHeader>>12) & 15);
			int freqIndex = (int)((mpegHeader>>10) & 3);
			int pad = (int)((mpegHeader>>9) & 1);
			int version = (int)((mpegHeader>>19) & 3);

			int bitrate;
			int frequency;

			
			// Determine bitrate from
			// index

			try
			{
				bitrate = MapBitrate(layer, version, bitIndex) * 1000;		
			}
			catch (IndexOutOfRangeException e)
			{
				bitrate = 1;	
			}
			
			
			// Determine frequency from
			// index

			try
			{
				frequency = CalculateFrequency(freqIndex, version);
			}
			catch (IndexOutOfRangeException e)
			{
				frequency = 1;
			}


			// Determine frame size. Forumula
			// based on description of MP3 
			// header found at:
			//
			// http://www.id3.org/mp3frame.html

			if (pad == 0) 
			{
				return (int)(144 * bitrate  / frequency);
			}
			else
			{
				return (int)( (144 * bitrate / frequency) + 1 );
			}
					

		}
		

		// Return bitrate from index

		private int
		MapBitrate(int layer, int version, int bits)
		{

			int[,] bitrate = new int[5,16]{
				
				// Ver 1 Layer I:

				{
					0, 32, 64, 96, 128, 
					160, 192, 224, 256, 288, 
					320, 352, 384, 416, 448, 
					0
				},
				
				// Ver 1 Layer II:

				{
					0, 32, 48, 56, 64, 
					80, 96, 112, 128, 160, 
					192, 224, 256, 320, 384, 
					0
				},

				// Ver 1 Layer III:
				
				{
					0, 32, 40, 48, 56, 
					64, 80, 96, 112, 128, 
					160, 192, 224, 256, 320, 
					0
				},

				// Ver 2 Layer I:

				{
					0, 32, 48, 56, 64, 
					80, 96, 112, 128, 144, 
					160, 176, 192, 224, 256, 
					0
				},

				// Ver 2 Layer II/III:

				{
					0, 8, 16, 24, 32, 
					40, 48, 56, 64, 80, 
					96, 112, 128, 144, 160, 
					0
				}

			};


			// return 0 if bitrange 
			// is too big

			if (bits > 15) 
			{
				throw new IndexOutOfRangeException(
						"bit index incorrect size");
			}
				

			// Lookup and return bitrate.
			// Note: 'layer' and 'version'
			// correspond to the bit
			// values (translated to int)
			// that are mapped to 'MPEG
			// Layer' and 'MPEG Audio Version'
			// ID as described in the
			// MPEG header. Their values
			// might seem counter-intuitive.

			// Ver 1 Layer I:

			if ((version == 3) && (layer == 3))
			{
				return bitrate[0,bits];
			}

			// Ver 1 Layer II:

			else if ((version == 3) && (layer == 2))
			{
				return bitrate[1,bits];
			}

			// Ver 1 Layer III:

			else if ((version == 3) && (layer == 1))
			{
				return bitrate[2,bits];
			}

			// Ver 2 Layer I:

			else if (((version == 2) || (version == 0)) && (layer == 3))
			{
				return bitrate[3,bits];
			}

			// Ver 2 Layer II/III:

			else if (((version == 2) || (version == 0)) && (layer < 3))
			{
				return bitrate[4,bits];
			}				
			
			else
			{
				return 0;
			}


		}


		// Calculate frequency from 
		// index

		private int
		CalculateFrequency(int freqIndex, int version)
		{

			
			// Frequency table based on
			// MP3 header description

			int[,] frequency = new int[3,3]
			{
				
				{
					// MPEG1
					44100,48000,32000 
				},

				{	// MPEG2
					22050, 24000, 16000 
				},

				{	// MPEG2.5
					11025, 12000, 8000 
				}

			};


			// Return 0 if frequency index
			// is too big
			
			if (freqIndex > 2) 
			{
				throw new IndexOutOfRangeException(
						"frequency index incorrect size");
			}
			

			// MPEG-1:

			if (version == 3)
			{
				return frequency[0,freqIndex];
			}

			// MPEG-2:

			else if ((version == 2))
			{
				return frequency[1,freqIndex];
			}
			else if ((version == 0))
			{
				return frequency[2,freqIndex];
			}
			else
			{
				return 0;
			}
			

		}


		// String header into single
		// uint value (BitConverter
		// class didn't work because
		// of endian-ness)

		private uint
		SetHeader(byte[] mpegBytes)
		{					
		
			return (uint)(((mpegBytes[0] & 255) << 24) | 
						((mpegBytes[1] & 255) << 16) | 
						((mpegBytes[2] & 255) <<  8) | 
						((mpegBytes[3] & 255)));

		}


		private void
		OutputHeader(uint mpegHeader)
		{
			Console.WriteLine("sync: "+
			(int)((mpegHeader>>21) & 2047)+" "+
			"version: "+
			(int)((mpegHeader>>19) & 3)+" "+
			"layer: "+
			(int)((mpegHeader>>17) & 3)+" "+
			"bitrate: "+
			(int)((mpegHeader>>12) & 15)+" "+
			"freq: "+	
			(int)((mpegHeader>>10) & 3)+" "+
			"emp: "+
			(int)((mpegHeader & 3) & 3)+" "+
			"framesz: "+
			CalculateFrameSize(mpegHeader)
			);					
		}	


	} 


} 
