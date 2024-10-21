using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelessWindowSolution
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public static ActionEventHandler ActionHandler = new();
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            RevitAPI.Initialize(commandData);
            var viewModel = new ModelessViewModel();
            var view = new ModelessView(viewModel);
            viewModel.CloseRequest += (s, e) => view.Close();
            view.Show();
            return Result.Succeeded;
        }
    }
}
