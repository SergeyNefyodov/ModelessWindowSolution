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

namespace ModelessWindowSolution
{
    public partial class ModelessView : Window
    {
        private readonly ModelessViewModel _viewModel;
        public ModelessView(ModelessViewModel viewModel)
        {
            _viewModel = viewModel;
            DataContext = viewModel;
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.ActionHandler.Raise(_ => RevitAPI.UiApplication.SelectionChanged -= _viewModel.HandleSelection);
        }
    }
}
