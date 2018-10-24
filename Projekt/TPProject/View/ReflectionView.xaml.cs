using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
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
using TPProject.ViewModel;

namespace TPProject.View
{
    /// <summary>
    /// Interaction logic for ReflexionView.xaml
    /// </summary>
    public partial class ReflexionView : Page
    {
        private ReflectionViewModel _reflectionViewModel = new ReflectionViewModel();

        public ReflexionView()
        {
            InitializeComponent();
            DataContext = _reflectionViewModel;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if((bool)dialog.ShowDialog())
            {
                _reflectionViewModel.FileName = dialog.FileName;
            }
        }
    }
}
