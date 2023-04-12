using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OPC.Data;
using OPC.Repository;
using Serilog;
using System.Configuration;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace OPCForm
{
    internal static class Program
    {   
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .Enrich.FromLogContext()
               .WriteTo.Console()
               .WriteTo.Async(c => c.File($"Logs/.txt", rollingInterval: RollingInterval.Day))
               .CreateLogger();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize(); 
          
            Application.Run(new FormOPC());
            //Application.Run(new Form3());
        }   
    }
}