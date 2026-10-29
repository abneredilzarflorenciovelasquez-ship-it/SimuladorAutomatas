using System;
using System.Diagnostics;
using System.IO;

namespace SimuladorAutomatas.Logica
{
    public static class LogService
    {
        private static readonly object Bloqueo = new object();
        private static string rutaArchivo;

        public static string GetLogFilePath()
        {
            if (!string.IsNullOrEmpty(rutaArchivo))
                return rutaArchivo;

            string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");

            try
            {
                Directory.CreateDirectory(carpeta);
                rutaArchivo = Path.Combine(carpeta, "simulador.log");
                return rutaArchivo;
            }
            catch (UnauthorizedAccessException)
            {
                string documentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                carpeta = Path.Combine(documentos, "SimuladorAutomatas", "logs");
                Directory.CreateDirectory(carpeta);
                rutaArchivo = Path.Combine(carpeta, "simulador.log");
                return rutaArchivo;
            }
        }

        public static void Info(string modulo, string mensaje)
        {
            Escribir("INFO", modulo, mensaje);
        }

        public static void Warning(string modulo, string mensaje)
        {
            Escribir("WARNING", modulo, mensaje);
        }

        public static void Error(string modulo, string mensaje)
        {
            Escribir("ERROR", modulo, mensaje);
        }

        public static void Critical(string modulo, string mensaje)
        {
            Escribir("CRITICAL", modulo, mensaje);
        }

        public static void Error(string modulo, string mensaje, Exception ex)
        {
            string detalle = mensaje + ": " + ex.Message;
            Escribir("ERROR", modulo, detalle);
        }

        private static void Escribir(string nivel, string modulo, string mensaje)
        {
            try
            {
                string ruta = GetLogFilePath();
                string moduloSeguro = string.IsNullOrWhiteSpace(modulo) ? "GENERAL" : modulo.Trim().ToUpperInvariant();
                string mensajeSeguro = (mensaje ?? string.Empty).Replace("\r", " ").Replace("\n", " ").Trim();
                string linea = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + nivel + " " + moduloSeguro + " " + mensajeSeguro;

                lock (Bloqueo)
                {
                    File.AppendAllText(ruta, linea + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("No se pudo escribir el log: " + ex.Message);
            }
        }
    }
}
