using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksApp.UI.Services
{
    public static class SetupBuilder
    {
        public static void CheckFiles()
        {
            var myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var dataDirPath = myDocumentsPath + "\\" + "TasksApp";
            if (!Directory.Exists(dataDirPath))
                Directory.CreateDirectory(dataDirPath);
            if (!File.Exists(dataDirPath + "\\" + "Tasks.db"))
                File.Copy("Files\\Tasks.db", dataDirPath + "\\" + "Tasks.db");
        }

        public static string GetDataFolderPath()
        {
            var myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return myDocumentsPath + "\\" + "TasksApp" + "\\";
        }
    }
}
