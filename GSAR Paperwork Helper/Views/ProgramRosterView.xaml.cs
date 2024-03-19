using GSAR_Paperwork_Helper.ViewModels;
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
        ProgramRosterViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new ProgramRosterViewModel();
            DataContext = viewModel;
        }
    }
}