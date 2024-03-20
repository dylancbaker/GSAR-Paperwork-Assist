using GSAR_Paperwork_Helper.Models;
using GSAR_Paperwork_Helper.Utilities;
using GSAR_Paperwork_Helper.ViewModels;
using GSAR_Paperwork_Helper.Views;
using iText.Kernel.XMP.Impl.XPath;
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

                    if (programPlan != null)
                    {
                        try
                        {
                            File.WriteAllBytes(filename, programPlan);
                            if (filename != null)
                            {
                                System.Diagnostics.Process process = new System.Diagnostics.Process();
                                process.StartInfo.FileName = filename;
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
            }


           
        }

       
    }
}