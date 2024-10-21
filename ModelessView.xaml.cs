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
        public ModelessView(ModelessViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        private void Unsubscribe(object sender, EventArgs e)
        {
            Command.ActionHandler.Raise(_ =>
            {
                RevitAPI.UiApplication.SelectionChanged -= HandleSelection;
    }
}
