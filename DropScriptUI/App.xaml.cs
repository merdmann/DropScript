using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;

namespace UIDropScript
{
    /// <summary>
    /// This contains provides the application data
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Trace.AutoFlush = true;
            Trace.Indent();

            Trace.WriteLine("Starting Application");

            base.OnStartup(e);
            ApplicationData.Load();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            UIDropScript.Properties.Settings.Default.Save();
            ApplicationData.Save();

            Trace.WriteLine("Stoping application");

            Trace.Unindent();
            Trace.Close();
        }
    }
}
