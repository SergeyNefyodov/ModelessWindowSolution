using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

#nullable enable
namespace ModelessWindowSolution
{
    public class ActionEventHandler : IExternalEventHandler
    {
        private Action<UIApplication>? _action;
        private readonly ExternalEvent _extenalEvent;
        public ActionEventHandler()
        {
            _extenalEvent = ExternalEvent.Create(this);
        }

        public void Raise(Action<UIApplication> action)
        {
            if (_action is null) _action = action;
            else _action += action;

            Raise();
        }
        public void Execute(UIApplication app)
        {
            if (_action is null) return;

            try
            {
                _action(app);
            }
            finally
            {
                _action = null;
            }
        }

        private void Raise()
        {
            _extenalEvent.Raise();
        }

        public string GetName()
        {
            return nameof(ActionEventHandler);
        }
    }
}
