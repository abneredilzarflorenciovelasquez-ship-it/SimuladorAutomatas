using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimuladorAutomatas.Logica;

namespace SimuladorAutomatas
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LogService.Info("APP", "Aplicacion iniciada");
            Application.ApplicationExit += delegate
            {
                LogService.Info("APP", "Aplicacion cerrada");
            };
            Application.Run(new Form1());
        }
    }
}
