using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading.Tasks;
using System.IO;

// c:\>pushd \\colmdfs\users$ ... Maps to z:

namespace WinSandMDI_2019a.Forms // WAsyncFileCopy1
{
    public partial class Form1 : Form
    {
        c_Files cFiles = null;

        public Form1()
        {
            InitializeComponent();
            cFiles = new c_Files();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonInputFileSelect_Click(object sender, EventArgs e)
        {
            // Display a dialog box to select a file to encrypt.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (textBoxInputWorkFolder.Text != "")
                openFileDialog1.InitialDirectory = textBoxInputWorkFolder.Text;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fName = openFileDialog1.FileName;
                if (fName != null)
                {
                    FileInfo fInfo = new FileInfo(fName);
                    // Pass the file name without the path.
                    string name = fInfo.FullName;
                    //EncryptFile(name);
                    textBoxInputFile.Text = name;
                }
            }
        }
        private void buttonInputFileAdd_Click(object sender, EventArgs e)
        {
            if (textBoxInputFile.Text != "")
            {
                cFiles.lstcFile.Add(new c_File(textBoxInputFile.Text, textBoxOutputWorkFolder.Text));
            }
        } 
        private void buttonOutputFileSelect_Click(object sender, EventArgs e)
        {
            // Display a dialog box to select a file to encrypt.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (textBoxInputWorkFolder.Text != "")
                openFileDialog1.InitialDirectory = textBoxOutputWorkFolder.Text;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fName = openFileDialog1.FileName;
                if (fName != null)
                {
                    FileInfo fInfo = new FileInfo(fName);
                    // Pass the file name without the path.
                    string name = fInfo.FullName;
                    //EncryptFile(name);
                    textBoxOutputFile.Text = name;
                }
            }
        }

        private async void buttonCopyFile_Click(object sender, EventArgs e)
        {
            buttonCopyFile.BackColor = Color.Pink;
            if(Directory.Exists(textBoxOutputWorkFolder.Text) == false)
            {
                MessageBox.Show("Need valid output folder to copy too.");
                buttonCopyFile.BackColor = Color.Peru;
                return;
            }

            //cFiles.copyAllFiles();
            //Task task = cFiles.copyAllFiles();
            Task task = new Task(new Action(cFiles.copyAllFiles)); //, TaskCreationOptions.LongRunning);
            task.Start();
            /*
            foreach (c_File cFile in lstcFile)
            {
                string strOutFolderFile = cFile.strOutputFolder + "\\" + cFile.strFileNameNoExtension + "." + cFile.strFileExtension;
                File.Copy(cFile.strInFileName, strOutFolderFile);

                //Task task = Task.Run((Action)cFile.doCopyFilesAsync);

                //Task task = new Task(new Action(cMR.processCompleteMailingReq), TaskCreationOptions.LongRunning);
                //Task task = new Task(new Action(cFile.doCopyFilesAsync));
                //Task task = cFile.doCopyFilesAsync();
                //task.Start();
                await cFile.doCopyFilesAsync();
            }*/
            buttonCopyFile.BackColor = Color.LightSteelBlue;
        }

        // From https://msdn.microsoft.com/en-us/library/kztecsys(v=vs.110).aspx
        //private async void Button_Click(object sender, RoutedEventArgs e)
        public async Task doCopyFilesAsync(StreamReader Source, StreamWriter Destination)
        {
            string UserDirectory = @"c:\Users\exampleuser\";

            using (StreamReader SourceReader = File.OpenText(UserDirectory + "BigFile.txt"))
            {
                using (StreamWriter DestinationWriter = File.CreateText(UserDirectory + "CopiedFile.txt"))
                {
                    await CopyFilesAsync(SourceReader, DestinationWriter);
                }
            }
        }
        public async Task CopyFilesAsync(StreamReader Source, StreamWriter Destination)
        {
            char[] buffer = new char[0x1000];
            int numRead;
            while ((numRead = await Source.ReadAsync(buffer, 0, buffer.Length)) != 0)
            {
                await Destination.WriteAsync(buffer, 0, numRead);
            }
        }
    }
    /*
     * This is making the assumption that the potential problem is copying files from the the [Jobs] server
     * File copying is not expected to be a problem to or from the Application Server or the NetSat Server
     * So any aysnc file copying is gooing to be from the [Jobs] server.
    */

    public class c_Files
    {
        public List<c_File> lstcFile = null;
        public c_Files ()
        { lstcFile = new List<c_File>(); }

        //public async void copyAllFiles()
        public void copyAllFiles()
        {
            foreach (c_File cFile in lstcFile)
            {
                string strOutFolderFile = cFile.strOutputFolder + "\\" + cFile.strFileNameNoExtension + "." + cFile.strFileExtension;
                try
                {
                    File.Copy(cFile.strInFileName, strOutFolderFile);
                }
                catch (Exception ex)
                {
                    return;
                }

                //Task task = Task.Run((Action)cFile.doCopyFilesAsync);

                //Task task = new Task(new Action(cMR.processCompleteMailingReq), TaskCreationOptions.LongRunning);
                //Task task = new Task(new Action(cFile.doCopyFilesAsync));
                //Task task = cFile.doCopyFilesAsync();
                //task.Start();
                //await cFile.doCopyFilesAsync();
            }
        }
    }

    public class c_File
    {
        public c_File (string pstrInfileName, string pstrOutputFolder)
        {   
            strInFileName = pstrInfileName;
            strFileExtension = Path.GetExtension(strInFileName);
            //strFileExtension = Path.GetFullPath(strInFileName);
            strFileNameNoExtension = Path.GetFileNameWithoutExtension(strInFileName);
            iDoesFileExist = 0; // unknown
            bShouldCopiedFileBeDeleted = false;
            bShouldCreatedFileBeDeleted = false;
            strOutputFolder = pstrOutputFolder;
        }
        
        // From database
        public string strInFileName;
        public string strFileExtension { get; set; }
        public string strFileFolder { get; set; }
        public string strFileNameNoExtension { get; set; }
        //public string strFileNameNoExtension { get; set; }

        public string strOutputFolder { get; set; }
        public string strOutFileName { get; set; }
        public string strOutFileNameTif { get; set; }
        public long lFileSize { get; set; }
        // 0=unknown, 1=exists, -1=Not Exist
        public int iDoesFileExist { get; set; }

        public bool bShouldCopiedFileBeDeleted { get; set; }
        public bool bShouldCreatedFileBeDeleted { get; set; }

        public bool bDoesFileExist()
        {
            if (File.Exists(strInFileName) == true)
            {
                iDoesFileExist = 1;
                return true;
            }
            else
            {
                iDoesFileExist = -1;
                return false;
            }
        }

        // Uses threads - http://www.codeproject.com/Tips/530253/Async-File-Copy-in-Csharp
        // this is from   https://msdn.microsoft.com/en-us/library/kztecsys(v=vs.110).aspx     
        public async Task doCopyFilesAsync() //StreamReader Source, StreamWriter Destination)
        {
            //string UserDirectory = @"c:\Users\exampleuser\";

            //using (StreamReader SourceReader = File.OpenText(UserDirectory + "BigFile.txt"))
            using (StreamReader SourceReader = File.OpenText(strInFileName))
            {
                //using (StreamWriter DestinationWriter = File.CreateText(UserDirectory + "CopiedFile.txt"))
                using (StreamWriter DestinationWriter = File.CreateText(strOutFileName))
                {
                    await CopyFilesAsync(SourceReader, DestinationWriter);
                }
            }
        }

        public async Task CopyFilesAsync(StreamReader Source, StreamWriter Destination)
        {
            char[] buffer = new char[0x1000];
            int numRead;
            while ((numRead = await Source.ReadAsync(buffer, 0, buffer.Length)) != 0)
            {
                await Destination.WriteAsync(buffer, 0, numRead);
            }
        } 
    }



}
