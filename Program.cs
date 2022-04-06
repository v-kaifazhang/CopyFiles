using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace CopyFiles
{
    class Program
    {
        static string accountFileName = "Test_Summary_" + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + ".txt";
        static string packageFileName = "Test_Summary_Package_" + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + ".txt";

        static string logFileName = "CopyFiles_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
        static void Main(string[] args)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string logFile = path + "\\" + logFileName;


            string sourceFilePath = ConfigurationManager.AppSettings["SourceFilePath"];
            string sharedFilePath = ConfigurationManager.AppSettings["SharedFilePath"];

            string accountFile = sourceFilePath + "\\" + accountFileName;
            string packageFile = sourceFilePath + "\\" + packageFileName;

            string accountInfo = string.Empty;
            string packageInfo = string.Empty;
            using (StreamReader sr = new StreamReader(accountFile))
            {
                accountInfo = sr.ReadToEnd();
            }
            using(StreamReader sr=new StreamReader(packageFile))
            {
                packageInfo = sr.ReadToEnd();
            }
            try
            {
                File.WriteAllText(sharedFilePath + "\\" + accountFileName, accountInfo);
                File.WriteAllText(sharedFilePath + "\\" + packageFileName, packageInfo);
            }
            catch (Exception ex) 
            {
                File.AppendAllText(logFile, DateTime.Now.ToString("yyyy-MM-dd HHmmss, ")+"Error: "+ex.ToString());
            }
        }


        public static void WriteLogToFile(string fileFullPath, string info)
        {
            File.AppendAllLines(fileFullPath, new System.Collections.Generic.List<string>() { info });
        }
    }

    
}
