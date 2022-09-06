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
        public static IServiceProvider ServiceProvider { get; set; }

#if DEBUG
        private static string sqlbaseDir = Common.GetApplicationPath();
#else
        private static string sqlbaseDir = AppDomain.CurrentDomain.BaseDirectory;
#endif

        static void ConfigureServices()
        {

            var services = new ServiceCollection();
            services.AddDbContext<AppDbContext>(options =>
             //options.UseSqlite(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString)
             options.UseSqlite("Data Source=" + Path.Combine(sqlbaseDir, "AppData\\OpcDB.db")));

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            ServiceProvider = services.BuildServiceProvider();
        }      

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

            ConfigureServices();
            InitApp();

            Application.Run(new Main());
            //Application.Run(new Form3());
        }

        private static void InitApp()
        {
            //if (!Directory.Exists(PatientPicturesManager.ScreensDirectory))
            //{
            //    Directory.CreateDirectory(PatientPicturesManager.ScreensDirectory);
            //}

            //if (!Directory.Exists(PatientPicturesManager.TempScreensDirectory))
            //{
            //    Directory.CreateDirectory(PatientPicturesManager.TempScreensDirectory);
            //}

            string backupsDirectoryPath = Path.Combine(
              Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
              "PatientsBackUps");

            string thisMonthBackupDirectory = Path.Combine(backupsDirectoryPath, $"{DateTime.Today:yyyy.MM}");

            if (!Directory.Exists(thisMonthBackupDirectory))
            {
                Directory.CreateDirectory(thisMonthBackupDirectory);
            }

            var match = new Regex(@"Data Source=(.+);?")
              .Match(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);

            if (!match.Success)
            {
                throw new ArgumentException("²ÎÊý´íÎó");
            }

            string databasePath = match.Groups[1].Value;
            string databaseFileName = Path.GetFileNameWithoutExtension(databasePath);
            string databaseExtension = Path.GetExtension(databasePath);

            string migrationBackupFilePath = Path.Combine(backupsDirectoryPath,
              GenerateDBBackupFileName(databaseFileName, databaseExtension, migrationBackup: true));

            var context = ServiceProvider.GetService<AppDbContext>();

            bool firstMigration = context.Database.GetAppliedMigrations().Count() == 0;

            if (!firstMigration && !File.Exists(migrationBackupFilePath))
            {
                File.Copy(databasePath, migrationBackupFilePath);
            }

            context.Database.Migrate();

            if (!firstMigration)
            {
                File.Delete(migrationBackupFilePath);
            }

            string standardBackupFilePath = Path.Combine(thisMonthBackupDirectory,
              GenerateDBBackupFileName(databaseFileName, databaseExtension, migrationBackup: false));

            File.Copy(databasePath, standardBackupFilePath);

            string prevMonthBackupDirectory = Path.Combine(backupsDirectoryPath, $"{DateTime.Today.AddMonths(-1):yyyy.MM}");

            if (Directory.Exists(prevMonthBackupDirectory))
            {
                string archiveFilePath = Path.Combine(backupsDirectoryPath, $"{Path.GetFileName(prevMonthBackupDirectory)}.zip");

                if (!File.Exists(archiveFilePath))
                {
                    ZipFile.CreateFromDirectory(prevMonthBackupDirectory, archiveFilePath);
                    Directory.Delete(prevMonthBackupDirectory, recursive: true);
                }
            }
        }

        private static string GenerateDBBackupFileName(string oldFileName, string extension, bool migrationBackup)
        {
            string backupNamePostfix = migrationBackup ? "_MigrationBackUp" : $"_{DateTime.Now:ddMMyyyyHHmmss}";

            return oldFileName + backupNamePostfix + extension;
        }
    }
}