using System;
using System.IO;
using System.Windows.Forms;
using Arbingersys.Audio.Aumplib;


namespace TestLib
{

	public class TestForm : System.Windows.Forms.Form
	{
	   // Declarations hence


		private string inputFile = "Not set";
		private string outputFile = "Not set";


		private Label label1;

		private ComboBox comboBox1;
		private Label comboBox1Label;
		private Button convertButton;
		private static ProgressBar progressBar1;

		// Source file open button and label:

		private Button sourceFileButton;
		private Label sourceFileLabel;	  


		// Destination file save button and label:

		private Button destFileButton;
		private Label destFileLabel;	  	  


		string[] convertFormat = new string[] {"WAV", "MP3", "AU", "AIFF"};

		private Aumpel audioConverter = new Aumpel();
		private Aumpel.soundFormat inputFileFormat;
		private Aumpel.soundFormat outputFileFormat;


		// Constructor

		public TestForm()
		{
		  
		  // Create the objects
		  
		  this.label1 = new Label();
		  this.convertButton = new Button();
		  
		  progressBar1 = new ProgressBar();		  
		  
		  this.sourceFileButton = new Button();
		  this.sourceFileLabel = new Label();

		  this.destFileButton = new Button();
		  this.destFileLabel = new Label();	

		  this.comboBox1 = new ComboBox();
		  this.comboBox1Label = new Label();
	
		 

		  // Set the form's title
		  
		  this.Text = "TestForm";


		  // Set up the output label
		  
		  label1.Location = new System.Drawing.Point (10, 10);
		  label1.Text = "Select a file to convert and a destination.";
		  label1.Size = new System.Drawing.Size (216, 16);


		  // Set convert button location
		  
		  convertButton.Location = new System.Drawing.Point (300,80);
		  convertButton.Size = new System.Drawing.Size (65, 22);
		  convertButton.Text = "&Convert";
		  

		  // Set source file button location
		  
		  sourceFileButton.Location = new System.Drawing.Point (10,30);
		  sourceFileButton.Size = new System.Drawing.Size (100, 22);
		  sourceFileButton.Text = "&File to convert:";
		  

		  // Set source file label location

		  sourceFileLabel.Location = new System.Drawing.Point (10, 55);
		  sourceFileLabel.Size = new System.Drawing.Size (375, 22);
		  sourceFileLabel.Text = inputFile;		  


		  // Set destination file button location
		  
		  destFileButton.Location = new System.Drawing.Point (10,80);
		  destFileButton.Size = new System.Drawing.Size (100, 22);
		  destFileButton.Text = "&Save to:";
		  

		  // Set destination file label location

		  destFileLabel.Location = new System.Drawing.Point (10, 105);
		  destFileLabel.Size = new System.Drawing.Size (375, 22);
		  destFileLabel.Text = outputFile;	


		  // Conversion file types
		  
		  comboBox1Label.Location = new System.Drawing.Point (120,80);
		  comboBox1Label.Size = new System.Drawing.Size(90,16);
		  comboBox1Label.Text = "Convert to:";
		  
		  comboBox1.Location = new System.Drawing.Point (216,80);
		  comboBox1.Size = new System.Drawing.Size(75,10);
		  
		  this.comboBox1.Items.AddRange(convertFormat);		  



		  // Progress Bar

		  progressBar1.Location = new System.Drawing.Point(10, 145);
		  progressBar1.Size = new System.Drawing.Size(375, 22);
		  progressBar1.Minimum = 0;
		  progressBar1.Maximum = 100;		  

		  
		  
		  // Set up the event handlers
		  
		  sourceFileButton.Click += 
			  new System.EventHandler (this.sourceFileButton_Click);		 

		  destFileButton.Click += 
			  new System.EventHandler (this.destFileButton_Click);			  

		  convertButton.Click += 
			  new System.EventHandler (this.convertButton_Click);		  


		  
		  // Add the controls and set the client area
		  
		  this.AutoScaleBaseSize = new System.Drawing.Size (5, 13);
		  this.ClientSize = new System.Drawing.Size (400, 200);
		  this.Controls.Add (this.convertButton);		  
		  this.Controls.Add (this.sourceFileButton);
		  this.Controls.Add (this.sourceFileLabel);
		  this.Controls.Add (this.destFileButton);
		  this.Controls.Add (this.destFileLabel);		  
		  this.Controls.Add (this.label1);
		  this.Controls.Add (this.comboBox1);
		  this.Controls.Add (this.comboBox1Label);
		  this.Controls.Add (progressBar1);
	  

      }
		


		// Delegates for decoding

		public static int soundFileSize = 0;
		

		// Conversion callback (lame,libsndfile)
		
		private static void
		ReportStatus(int totalBytes, 
			int processedBytes, Aumpel aumpelObj)
		{
			progressBar1.Value = 
				(int)(((float)processedBytes/(float)totalBytes)*100);
		}


		// Decoding callback (madlldlib)
		
		private static bool 
		ReportStatusMad(uint frameCount, uint byteCount, 
				ref MadlldlibWrapper.mad_header mh) 
		{

			progressBar1.Value = 
				(int)(((float)byteCount/(float)soundFileSize)*100);

			return true;
		}


		  
		private void
		ShowExceptionMsg(Exception e)
		{
			MessageBox.Show("Exception: " + e.Message, 
				"Exception!", MessageBoxButtons.OK);
		}


	  // Event handlers hence
	  
	  protected void
	  sourceFileButton_Click (object sender, System.EventArgs e)
	  {
		    
			// Show Open File dialog

			OpenFileDialog openFile = new OpenFileDialog();
			openFile.Filter  = "MP3 (*.mp3)|*.mp3|WAV (*.wav)|" + 
				"*.wav|All Files (*.*)|*.*";

			openFile.FileName = "" ;
			openFile.CheckFileExists = true;
			openFile.CheckPathExists = true;


			if ( openFile.ShowDialog() != DialogResult.OK )
			return;


			// Set input file formation 

			try
			{
				inputFileFormat = 
					audioConverter.CheckSoundFormat(openFile.FileName);
			}
			catch(Exception ex)
			{
				ShowExceptionMsg(ex);
				return;
			}

			sourceFileLabel.Text = inputFile = openFile.FileName;
			
	  }	


	  protected void
	  destFileButton_Click (object sender, System.EventArgs e)
	  {
		    
			// Show Open File dialog

			SaveFileDialog saveFile = new SaveFileDialog();
			saveFile.Filter  = "MP3 (*.mp3)|*.mp3|" +
				"WAV (*.wav)|*.wav|" +
				"AIFF (*.aiff)|*.aiff|" +
				"AU (*.au)|*.au|" +
				"All Files (*.*)|*.*";


			if ( saveFile.ShowDialog() != DialogResult.OK )
				return;


			// Set input file formation 

			destFileLabel.Text = outputFile = saveFile.FileName;
			
	  }


	  protected void
	  convertButton_Click (object sender, System.EventArgs e)
	  {	  

		  // Set conversion type

		  switch((string)comboBox1.SelectedItem)
		  {
			  case "WAV":
				  outputFileFormat = Aumpel.soundFormat.WAV;
			      break;
			  case "MP3":
				  outputFileFormat = Aumpel.soundFormat.MP3;
			      break;
			  case "AU":
				  outputFileFormat = Aumpel.soundFormat.AU;
			      break;
			  case "AIFF":
				  outputFileFormat = Aumpel.soundFormat.AIFF;
			      break;
			  default:
				  MessageBox.Show("You must select a type to convert to.", 
						  "Error", MessageBoxButtons.OK);
				  return;
		  }


		  // Convert to MP3

		  if ( (int)outputFileFormat == (int)Aumpel.soundFormat.MP3 )
		  {

			  try
			  {
				  
				  Aumpel.Reporter defaultCallback = 
					  new Aumpel.Reporter(ReportStatus);
				  
				  audioConverter.Convert(inputFile, 
						  (int)inputFileFormat, outputFile, 
						  (int)outputFileFormat, defaultCallback);
				  
				  progressBar1.Value = 0;
				  
				  destFileLabel.Text = outputFile = "";
				  sourceFileLabel.Text = inputFile = "";
				  
				  MessageBox.Show("Conversion finished.", 
						  "Done.", MessageBoxButtons.OK);

			  }
			  catch (Exception ex)
			  {
				  ShowExceptionMsg(ex);
				  return;
			  }
	  
		  }

		  // From MP3 (using named pipe):

		  else if ( (int)inputFileFormat == (int)Aumpel.soundFormat.MP3 )
		  {

			  try
			  {
				  
				  MadlldlibWrapper.Callback defaultCallback = 
					  new MadlldlibWrapper.Callback(ReportStatusMad);

				  // Determine file size
				  FileInfo fi = new FileInfo(inputFile);		
				  soundFileSize = (int)fi.Length;

				  audioConverter.Convert(inputFile, 
						  outputFile, outputFileFormat, defaultCallback);				  
				  progressBar1.Value = 0;
				  
				  destFileLabel.Text = outputFile = "";
				  sourceFileLabel.Text = inputFile = "";
				  
				  MessageBox.Show("Conversion finished.", 
						  "Done.", MessageBoxButtons.OK);

			  }
			  catch (Exception ex)
			  {
				  ShowExceptionMsg(ex);
				  return;
			  }			  
			  
		  }

		  // Non-MP3 soundfile conversion:

		  else
		  {
			  
			  try		  
			  {

				  Aumpel.Reporter defaultCallback = 
					  new Aumpel.Reporter(ReportStatus);
			  
				  audioConverter.Convert(inputFile, 
						  (int)inputFileFormat, 
						  outputFile, 
						  (int)(outputFileFormat | Aumpel.soundFormat.PCM_16), 
						  defaultCallback);
				  
				  progressBar1.Value = 0;
				  
				  destFileLabel.Text = outputFile = "";
				  sourceFileLabel.Text = inputFile = "";
				  
				  MessageBox.Show("Conversion finished.", 
						  "Done.", MessageBoxButtons.OK);
				  
			  }
			  catch (Exception ex)
			  {
				  ShowExceptionMsg(ex);
				  return;
			  }			  

		  }
		  		
		  
		
	  }	  
	  
		
      // Run the app

      public static void Main( ) 
      {
         Application.Run(new TestForm( ));
      }

 
	}


}

