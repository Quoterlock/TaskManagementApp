using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksApp.BusinessLogic.Interfaces
{
    public interface ITasksExporter
    {
        void Export(string path);
        void Import(string path);
    }
}
