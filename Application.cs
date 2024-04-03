using Autodesk.Revit.UI;
using Nice3point.Revit.Toolkit.External.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Configuration.Assemblies;
using System.Reflection;
using Autodesk.Revit.DB;

namespace ModelessWindowSolution
{
    public class Application : IExternalApplication
    {
        static AddInId AddInId = new AddInId(new Guid("22F028DA-A3D0-41EC-8999-67B640EF1B0E"));
        private readonly string assemblyPath = Assembly.GetExecutingAssembly().Location;
        public static ActionEventHandler ActionHandler { get; set; }
        private void SetupHandlers ()
        {
            ActionHandler = new ActionEventHandler();
        }

        public Result OnStartup(UIControlledApplication application)
        {
            SetupPanel(application);
            SetupHandlers();
            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        private void SetupPanel(UIControlledApplication application)
        {
            string tabName = "ModelessWindowSolution";
            application.CreateRibbonTab(tabName);
            RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, "ModelessWindowSolution");
            AddPushButton(ribbonPanel, "Button 1", assemblyPath, "ModelessWindowSolution.Command");
        }

        private PushButton AddPushButton(RibbonPanel ribbonPanel, string buttonName, string path, string linkToCommand)
        {
            var buttonData = new PushButtonData(buttonName, buttonName, path, linkToCommand);
            var button = ribbonPanel.AddItem(buttonData) as PushButton;
            return button;
        }
    }
}
