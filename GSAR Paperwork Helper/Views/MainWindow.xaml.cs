﻿using GSAR_Paperwork_Helper.Models;
using GSAR_Paperwork_Helper.Utilities;
using GSAR_Paperwork_Helper.ViewModels;
using iText.Kernel.XMP.Impl.XPath;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
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
            viewModel.currentProgram.PropertyChanged += CurrentProgram_PropertyChanged;
            GSARProgram newprog = new GSARProgram();
            newprog.SetUpNewProgram();
            viewModel.SetNewGSARProgram(newprog);
        }

        private void CurrentProgram_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel.currentProgram.FilePath))
            {
                SaveFile(false);
            }
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
                        //process.WaitForExit();
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

                    if (notifyOnSave)
                    {
                        MessageBox.Show("Saved! It will automatically save from now on as changes are made.");
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
                    string[] fileNames = new string[viewModel.currentProgram.Courses.Count];

                    string tempPath = System.IO.Path.GetTempPath();
                    int fileIndex = 0;
                    foreach(Course c in viewModel.currentProgram.Courses)
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
            if (viewModel != null && viewModel.currentProgram != null)
            {

                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "4-GSAR Program Completion Roster - " + DateTime.Now.ToString("yyyy-MM-dd"); // Default file name
                dlg.DefaultExt = ".pdf"; // Default file extension
                dlg.Filter = "PDF documents (.pdf)|*.pdf"; // Filter files by extension

                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process save file dialog box results
                if (result == true)
                {
                    // Save document
                    string filename = dlg.FileName;

                    byte[] programPlan = PDFFiller.fillCompletion(viewModel.currentProgram);
                    if (programPlan != null) { SavePDF(programPlan, filename); }

                }
            }
        }

        private void miNewProgram_Click(object sender, RoutedEventArgs e)
        {
            GSARProgram program = new                 GSARProgram();
            program.SetUpNewProgram();
            viewModel.SetNewGSARProgram(program);

        }

        private void miAbout_Click(object sender, RoutedEventArgs e)
        {
            Views.AboutWindow aboutWindow = new Views.AboutWindow();
            aboutWindow.Show();
        }

        private void miStudentListCSV_Click(object sender, RoutedEventArgs e)
        {
            ExportToCSV();
        }

        private void ExportToCSV()
        {

            string downloadsPath = KnownFolders.GetPath(KnownFolder.Downloads);
            downloadsPath = System.IO.Path.Combine(downloadsPath, "Student List " + viewModel.currentProgram.ProgramPlanDate.Year + "-" + viewModel.currentProgram.ProgramPlanDate.Month + "-" + viewModel.currentProgram.ProgramPlanDate.Day + ".csv");
            string delimiter = ",";
            StringBuilder csv = new StringBuilder();
            //header row
            csv.Append("Name"); csv.Append(delimiter);
            csv.Append("Email"); csv.Append(delimiter);
            csv.Append("DOB"); csv.Append(delimiter);
            csv.Append(Environment.NewLine);
            foreach (Personnel p in viewModel.currentProgram.Students.Where(o=>!string.IsNullOrEmpty(o.FirstName)))
            {
                
                csv.Append("\""); csv.Append(p.FirstName.EscapeQuotes()); csv.Append(" "); csv.Append(p.LastName.EscapeQuotes()); csv.Append("\""); csv.Append(delimiter);
                csv.Append("\""); csv.Append(p.EmailAddress.EscapeQuotes()); csv.Append("\""); csv.Append(delimiter);
                csv.Append("\""); csv.Append(p.DateOfBirth.ToShortDateString()); csv.Append("\""); csv.Append(delimiter);
                csv.Append(Environment.NewLine);
            }
            


            try
                {
                    System.IO.File.WriteAllText(downloadsPath, csv.ToString());
                OpenWithDefaultProgram(downloadsPath);
                   
                        //System.Diagnostics.Process.Start(downloadsPath);
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sorry, there was a problem writing to the file.  Please report this error: " + ex.ToString());
                }
            
        }

        public static void OpenWithDefaultProgram(string path)
        {
            using Process fileopener = new Process();

            fileopener.StartInfo.FileName = "explorer";
            fileopener.StartInfo.Arguments = "\"" + path + "\"";
            fileopener.Start();
        }

        private void Delete_Selected_Students_Click(object sender, RoutedEventArgs e)
        {
            List<Personnel> SelectedStudents = new List<Personnel>();
            if(StudentsDataGrid.SelectedItems != null)
            {
                foreach (Personnel p in StudentsDataGrid.SelectedItems)
                {
                    SelectedStudents.Add(p);
                }

                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you want to delete " + SelectedStudents.Count + " students?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)

                {
                    foreach (Personnel p in SelectedStudents)
                    {
                        viewModel.RemoveStudent(p);
                    }
                }
            }
        }
    }
}