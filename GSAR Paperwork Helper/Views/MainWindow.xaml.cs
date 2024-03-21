using GSAR_Paperwork_Helper.Models;
using GSAR_Paperwork_Helper.Utilities;
using GSAR_Paperwork_Helper.ViewModels;
using iText.Kernel.XMP.Impl.XPath;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;

namespace GSAR_Paperwork_Helper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel viewModel;


        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainWindowViewModel();
            DataContext = viewModel;

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel != null && viewModel.currentProgram != null)
            {

                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "1-Program Plan"; // Default file name
                dlg.DefaultExt = ".pdf"; // Default file extension
                dlg.Filter = "PDF documents (.pdf)|*.pdf"; // Filter files by extension

                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process save file dialog box results
                if (result == true)
                {
                    // Save document
                    string filename = dlg.FileName;

                    byte[] programPlan = PDFFiller.fillProgramPlan(viewModel.currentProgram);
                    if (programPlan != null) { SavePDF(programPlan, filename); }
                   
                }
            }


           
        }

     

        private void SavePDF(byte[] pdfData, string filename, bool autoStart = true)
        {
            if (pdfData != null)
            {
                try
                {
                    File.WriteAllBytes(filename, pdfData);
                    if (filename != null && autoStart)
                    {
                        System.Diagnostics.Process process = new System.Diagnostics.Process();
                        process.StartInfo = new ProcessStartInfo(filename)
                        {
                            UseShellExecute = true
                        };
                        process.Start();
                        process.WaitForExit();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("There was an error trying to save " + filename + " please verify the path is accessible.");

                }
            }
        }

        private void miSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(viewModel.currentProgram.FilePath))
            {
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "GSAR Program Plan " + viewModel.currentProgram.ProgramPlanDate.ToString("yyyy-MM-dd"); // Default file name
                dlg.DefaultExt = ".xml"; // Default file extension
                dlg.Filter = "Extensible Markup Language|*.xml"; // Filter files by extension

                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();
                if (result == true)
                {
                    // Save document
                   viewModel.currentProgram.FilePath = dlg.FileName;

                }
            }

            SaveFile();

            
        }

        private void SaveFile(bool notifyOnSave = true)
        {
            if (!string.IsNullOrEmpty(viewModel.currentProgram.FilePath))
            {
                try
                {

                    System.Xml.XmlWriterSettings ws = new System.Xml.XmlWriterSettings();
                    ws.NewLineHandling = System.Xml.NewLineHandling.Entitize;

                    var path = viewModel.currentProgram.FilePath;
                    XmlSerializer ser = new XmlSerializer(typeof(GSARProgram));
                    using (System.Xml.XmlWriter wr = System.Xml.XmlWriter.Create(path, ws))
                    {
                        ser.Serialize(wr, viewModel.currentProgram);
                    }

                    //System.IO.FileStream file = System.IO.File.Create(path);

                    //writer.Serialize(file, CurrentTask);
                    //file.Close();

                }
                catch (IOException)
                {
                    if (notifyOnSave)
                    {
                        MessageBox.Show("The file has NOT been saved.  It may be open in another program, or the disk may be full.");

                    }
                }
                catch (System.UnauthorizedAccessException)
                {
                    if (notifyOnSave)
                    {
                        MessageBox.Show("A program on your system, typically a virus scanner, is preventing files from being saved to " + viewModel.currentProgram.FilePath + ". Please select a different folder to save to.");
                    }
                }
                catch (Exception ex)
                {
                    if (notifyOnSave)
                    {
                        MessageBox.Show("The file has NOT been saved.  An error has been encountered, please report the following:" + ex.Message);

                    }

                }

            }
        }

        private void openFile(string filename = null)
        {
           
            if (!string.IsNullOrEmpty(filename))
            {
                //they've chosen a file, try to open it.
                bool ProceedWithOpen = true;
                try
                {
                    XmlSerializer reader = new XmlSerializer(typeof(GSARProgram));
                    using (StreamReader file = new StreamReader(filename))
                    {
                        using (XmlReader xr = XmlReader.Create(file, new XmlReaderSettings() { DtdProcessing = DtdProcessing.Prohibit }))
                        {
                            GSARProgram testTaskDeserialize = (GSARProgram)reader.Deserialize(xr);
                            if(testTaskDeserialize != null)
                            {
                                viewModel.SetNewGSARProgram(testTaskDeserialize);
                            }
                                //setRecentFiles();
                            
                        }
                        file.Close();
                       
                        // minimiseAllCollapsablePanels();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was an error loading the task: " + ex.ToString());
                }

               
            }
        }

        private void miExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void miOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                openFile(openFileDialog.FileName);
            }
        }

        private void mi1701_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel != null && viewModel.currentProgram != null)
            {

                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "3-GSAR Class Roster Form EMRG-1701"; // Default file name
                dlg.DefaultExt = ".pdf"; // Default file extension
                dlg.Filter = "PDF documents (.pdf)|*.pdf"; // Filter files by extension

                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process save file dialog box results
                if (result == true)
                {
                    // Save document
                    string filename = dlg.FileName;

                    byte[] programPlan = PDFFiller.fillClassRoster(viewModel.currentProgram, viewModel.Emrg1701);
                    if (programPlan != null) { SavePDF(programPlan, filename); }

                }
            }
        }

        private void mi1702_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel != null && viewModel.currentProgram != null)
            {

                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "3-GSAR Class Roster Form EMRG-1702"; // Default file name
                dlg.DefaultExt = ".pdf"; // Default file extension
                dlg.Filter = "PDF documents (.pdf)|*.pdf"; // Filter files by extension

                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process save file dialog box results
                if (result == true)
                {
                    // Save document
                    string filename = dlg.FileName;

                    byte[] programPlan = PDFFiller.fillClassRoster(viewModel.currentProgram, viewModel.Emrg1702);
                    if (programPlan != null) { SavePDF(programPlan, filename); }

                }
            }
        }

        private void mi1704_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel != null && viewModel.currentProgram != null)
            {

                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "3-GSAR Class Roster Form EMRG-1704"; // Default file name
                dlg.DefaultExt = ".pdf"; // Default file extension
                dlg.Filter = "PDF documents (.pdf)|*.pdf"; // Filter files by extension

                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process save file dialog box results
                if (result == true)
                {
                    // Save document
                    string filename = dlg.FileName;

                    byte[] programPlan = PDFFiller.fillClassRoster(viewModel.currentProgram, viewModel.Emrg1704);
                    if (programPlan != null) { SavePDF(programPlan, filename); }

                }
            }
        }

        private void mi1705_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel != null && viewModel.currentProgram != null)
            {

                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "3-GSAR Class Roster Form EMRG-1705"; // Default file name
                dlg.DefaultExt = ".pdf"; // Default file extension
                dlg.Filter = "PDF documents (.pdf)|*.pdf"; // Filter files by extension

                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process save file dialog box results
                if (result == true)
                {
                    // Save document
                    string filename = dlg.FileName;

                    byte[] programPlan = PDFFiller.fillClassRoster(viewModel.currentProgram, viewModel.Emrg1705);
                    if (programPlan != null) { SavePDF(programPlan, filename); }

                }
            }
        }

        private void mi1706_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel != null && viewModel.currentProgram != null)
            {

                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "3-GSAR Class Roster Form EMRG-1706"; // Default file name
                dlg.DefaultExt = ".pdf"; // Default file extension
                dlg.Filter = "PDF documents (.pdf)|*.pdf"; // Filter files by extension

                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process save file dialog box results
                if (result == true)
                {
                    // Save document
                    string filename = dlg.FileName;

                    byte[] programPlan = PDFFiller.fillClassRoster(viewModel.currentProgram, viewModel.Emrg1706);
                    if (programPlan != null) { SavePDF(programPlan, filename); }

                }
            }
        }

        private void mi1200_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel != null && viewModel.currentProgram != null)
            {

                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "3-GSAR Class Roster Form EMRG-1200"; // Default file name
                dlg.DefaultExt = ".pdf"; // Default file extension
                dlg.Filter = "PDF documents (.pdf)|*.pdf"; // Filter files by extension

                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process save file dialog box results
                if (result == true)
                {
                    // Save document
                    string filename = dlg.FileName;

                    byte[] programPlan = PDFFiller.fillClassRoster(viewModel.currentProgram, viewModel.Emrg1200);
                    if (programPlan != null) { SavePDF(programPlan, filename); }

                }
            }
        }

        private void miAllRosters_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel != null && viewModel.currentProgram != null)
            {

                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "3-GSAR Class Roster - all courses"; // Default file name
                dlg.DefaultExt = ".pdf"; // Default file extension
                dlg.Filter = "PDF documents (.pdf)|*.pdf"; // Filter files by extension

                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process save file dialog box results
                if (result == true)
                {
                    // Save document
                    string filename = dlg.FileName;
                    string[] fileNames = new string[viewModel.currentProgram.courses.Count];

                    string tempPath = System.IO.Path.GetTempPath();
                    int fileIndex = 0;
                    foreach(Course c in viewModel.currentProgram.courses)
                    {
                        string tempFileName = System.IO.Path.GetTempFileName();
                        string path = System.IO.Path.Combine(tempPath, tempFileName);
                        SavePDF(PDFFiller.fillClassRoster(viewModel.currentProgram, c), path, false);
                        fileNames[fileIndex] = path;
                        fileIndex++;
                    }                    




                    if (fileIndex > 0)
                    {
                        FileAccessTools.SimpleMerge(fileNames, filename);
                        System.Diagnostics.Process process = new System.Diagnostics.Process();
                        process.StartInfo = new ProcessStartInfo(filename)
                        {
                            UseShellExecute = true
                        };
                        process.Start();
                    }

                }
            }
        }

        private void miProgramCompletion_Click(object sender, RoutedEventArgs e)
        {

        }

        private void miNewProgram_Click(object sender, RoutedEventArgs e)
        {
            GSARProgram program = new                 GSARProgram();
            program.SetUpNewProgram();
            viewModel.SetNewGSARProgram(program);

        }
    }
}