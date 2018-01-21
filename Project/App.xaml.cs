using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DataBaseLayer;

namespace Project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnExit(System.Windows.ExitEventArgs e)
        {
            DataBaseConnector.GetInstance()?.StopConnection();       
        }
    }
}
