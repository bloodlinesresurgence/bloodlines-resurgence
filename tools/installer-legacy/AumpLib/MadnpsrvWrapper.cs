///////////////////////////////////////////////////////////////////////////////
//
// MadnpsrvWrapper -- Interface to pipe server of madlldlib
//
// Wrapper class to madnpsrv.exe, a command-line implementation of madlldlib 
// that passes decoding information via named pipes. 
//
// This class provides methods to start 'madnpsrv.exe' with appropriate,
// command-line parameters (execute madnpsrv.exe with parameters to see
// usage information), and then creates a connection to the named pipe 
// that madnpsrv.exe creates (see string pipe_srv below), parses the output
// and passes it along to the calling code.
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
	using System.Diagnostics;
	using System.Threading;		
	using System.Runtime.InteropServices;
	using System.Text.RegularExpressions;	

	// alias; see SF_INFO struct:

	using sf_count_t = System.Int64;	

	
#if PLATFORM_64
	using size_t = System.UInt64;
#else
	using size_t = System.UInt32;
#endif
	

	class MadnpsrvWrapper 
	{


		// Type declarations hence


		// To throw if named pipe is
		// already running
		
		public class 
		NPAlreadyRunning : System.ApplicationException 
		{
			public 
			NPAlreadyRunning ( string message ) : base ( message )
			{}
		}	

		private static bool isRunning = false;
		
		// Used to ensure thread safety

		static readonly object padlock = new object();


		// This process object is used to start
		// and stop the named pipe.

		private static Process startNP = new Process();


		// Always points at the mad named
		// pipe server file name:

		private static string exeFilename = "madnpsrv.exe";

		// Pipe name:

		private static string pipeName = "\\\\.\\pipe\\aumplibMadArmsLen";


		// Data types

		private static byte[] pipeMessage = new byte[256];
		private static byte[] bytesRead = new byte[4];
		private const int MAX_STRLEN = 260;		


		// Decoding flags

		public const int DEC_WAV = 1;
		public const int DEC_PCM = 0;


		// To kill a conversion in progress

		public bool killSwitch = false;




		// Method declarations hence		
		


		// Named pipe client P/Invoke calls
		// Establish connection:
		
		[DllImport("Kernel32.dll", SetLastError=true)]
		static extern IntPtr 
		CreateFile(
                string inputFilename,
                [MarshalAs(UnmanagedType.U4)]FileAccess fileaccess,
                [MarshalAs(UnmanagedType.U4)]FileShare fileshare,
                int securityattributes,
                [MarshalAs(UnmanagedType.U4)]FileMode creationdisposition,
                int flags,
                IntPtr template);

		// Read from NP connection:

		[DllImport("kernel32.dll", SetLastError=true)]
		public static extern bool 
		ReadFile(
			IntPtr hHandle,						// handle to file
			byte[] lpBuffer,					// data buffer
			uint nNumberOfBytesToRead,			// number of bytes to read
			byte[] lpNumberOfBytesRead,			// number of bytes read
			uint lpOverlapped					// overlapped buffer
			);	

		// Write to NP connection:
		
		[DllImport("kernel32.dll", SetLastError=true)]
		public static extern bool 
		WriteFile(
			IntPtr hHandle,						// handle to file
			byte[] lpBuffer,					// data buffer
			uint nNumberOfBytesToRead,			// number of bytes to read
			byte[] lpNumberOfBytesRead,			// number of bytes read
			uint lpOverlapped					// overlapped buffer
			);			

		// Close the NP Connection:

		[DllImport("kernel32.dll", SetLastError=true)]		
		public static extern bool
		CloseHandle(IntPtr hHandle);
		



		// Delegate function definition (analogous to 
		// a callback in C/C++. 'fcnt' refers to 
		// frame count, 'bcnt' to byte count

		public delegate void 
		Callback (uint frameCount, uint byteCount, MadnpsrvWrapper madNPObject);		


		// Constructor
		
		public
		MadnpsrvWrapper()
		{

			// Determine if NP is already
			// running. Throw exception if
			// it is.

			if (isRunning)			
			{
				throw new NPAlreadyRunning("Named Server " +
						pipeName + " already running");
			}


		}			
				

		// Start the Named Pipe (NP) server
		// process.

		public void
		StartPipe(string inFilename, string outFilename, int decodeType)
		{
			
			
			// Thread safety

			lock (padlock)
			{
				
				string defaultDecodeType = "wav";
				StringBuilder inputFilePath = new StringBuilder();
				StringBuilder outputFilePath = new StringBuilder();			
				inputFilePath.Capacity = MAX_STRLEN;
				outputFilePath.Capacity = MAX_STRLEN;

				startNP.StartInfo.FileName = exeFilename;

				
				// Change decoding flag if necessary
				
				if (decodeType == DEC_PCM) defaultDecodeType = "pcm";


				// Convert to short pathnames

				Aumpel.GetShortPathName(
						inFilename, inputFilePath, MAX_STRLEN);
				
				Aumpel.GetShortPathName(
						outFilename, outputFilePath, MAX_STRLEN);

				
				if (inputFilePath.Length > 0) 
					inFilename = inputFilePath.ToString();

				if (outputFilePath.Length > 0)
					outFilename = outputFilePath.ToString();
				
				// Setup parameters for pipe

				startNP.StartInfo.Arguments = 
					inFilename + " " + outFilename + " " + defaultDecodeType;


				// Hide window
					
				startNP.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;	


				// isRunning is set either T|F.
				// It is used as a check when other
				// instances are created (see 
				// constructor)

				isRunning = startNP.Start();
	
				
				// Sleep for 2 seconds (necessary for
				// the pipe to start)

				Thread.Sleep(2000);
				
			}


		}



		// Start up the client, read from 
		// the pipe.
		
		public void
		ReadPipe(Callback myCb)
		{


			// Thread safety:

			lock (padlock)
			{

					
				Encoding convertBytes = Encoding.ASCII;

				
				// Open pipe
			
				IntPtr pipeHandle = CreateFile(pipeName,
						FileAccess.ReadWrite,
						FileShare.ReadWrite,
						0,
						FileMode.Open,
						0, 
						IntPtr.Zero);


				byte[] sentMessage = new byte[256];
				sentMessage = convertBytes.GetBytes("a");

				

				// Read from pipe

				do
				{
					
					// Exit if cannot read from pipe (i.e.
					// EOF)
					
					if (!ReadFile(pipeHandle, pipeMessage, 256, bytesRead, 0)) 
					{
						break;
					}
					
					
					char[] convertPipeMsg = new char[256];

					convertBytes.GetChars(pipeMessage, 
							0, pipeMessage.Length, convertPipeMsg, 0);

					string outputStr = new string(convertPipeMsg);


					// Deletes \0 character by replacing it with 
					// the 'backspace' character

					outputStr = outputStr.Replace(Convert.ToChar(0), 
							Convert.ToChar(32) );

					outputStr = outputStr.Replace(" ", "");


					// Get numbers from named pipe string

					string[] units = Regex.Split(outputStr, ",");
					string frameCount = Regex.Replace(units[0], "fc=", "");
					string totalBytes = Regex.Replace(units[1], "tb=", "");
					uint iFrameCount = UInt32.Parse(frameCount);
					uint iTotalBytes = UInt32.Parse(totalBytes);


					// Pass to callback

					myCb(iFrameCount, iTotalBytes, this);

					
					// Handle killing of conversion:
					
					if (this.killSwitch)
					{
						sentMessage = convertBytes.GetBytes("k");
						WriteFile(pipeHandle, sentMessage, 256, bytesRead, 0);
						break;
					}
					else
					{
						WriteFile(pipeHandle, sentMessage, 256, bytesRead, 0);
					}


				} 
				while (BitConverter.ToInt32(bytesRead, 0) > 0);


				// Keep pipeHandle from being garbage 
				// collected

				GC.KeepAlive(pipeHandle);

				// Close the file handle:
			
				CloseHandle(pipeHandle);

				// Set 'running' flag back to 
				// false:

				isRunning = false;

			}


		}


		// Allows calling code to kill named pipe
		// (e.g. in the case of a 'Cancel' button.

		public void
		StopPipe() 
		{
			if (!startNP.HasExited) startNP.Kill();		

			// Set 'running' flag back to 
			// false:

			isRunning = false;			
		}

			
	} 


} 

