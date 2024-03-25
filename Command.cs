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
        static AddInId AddInId = new AddInId(new Guid("22F028DA-A3D0-41EC-8999-67B640EF1B0E"));
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            RevitAPI.Initialize(commandData);
            var handler = new ModelessWindowHandler();
            var viewModel = new ModelessViewModel(handler);
            var view = new ModelessView(viewModel);
            viewModel.CloseRequest += (s, e) => view.Close();
            view.Show();
            return Result.Succeeded;
        }
    }
}
