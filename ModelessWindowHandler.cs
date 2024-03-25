using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelessWindowSolution
{
    public class ModelessWindowHandler : IExternalEventHandler
    {
        
        private readonly ExternalEvent _extenalEvent;
        public ModelessWindowHandler()
        {
            _extenalEvent = ExternalEvent.Create(this);
        }

        public ModelessViewModel ViewModel { get; set; }
        public void Execute(UIApplication app)
        {
            try
            {
                var parameter = ViewModel.ActiveElement.get_Parameter(BuiltInParameter.ALL_MODEL_MARK);
                if (parameter != null)
                {
                    using (var transaction = new Transaction(RevitAPI.Document, "Замена значения параметра"))
                    {
                        transaction.Start();
                        parameter.Set(ViewModel.MarkValue);
                        transaction.Commit();
                    }
                }
            }
            catch (Exception exception)
            {
                TaskDialog.Show("Error", exception.Message + exception.StackTrace);
            }
            finally
            {
                RevitAPI.UiApplication.SelectionChanged -= ViewModel.HandleSelection;
            }
        }

        public void Raise()
        {
            _extenalEvent.Raise();
        }

        public string GetName()
        {
            return nameof(ModelessWindowHandler);
        }
    }
}
