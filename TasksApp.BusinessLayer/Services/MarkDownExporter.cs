using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TasksApp.BusinessLogic.Interfaces;
using TasksApp.DataAccess;

namespace TasksApp.BusinessLogic.Services
{
    public class MarkDownExporter : ITasksExporter
    {
        private ICategoriesService _categoriesService;
        private ITasksPresenter _tasksPresenter;
        private IProjectsService _projectsService;
        public MarkDownExporter(ICategoriesService categoriesService, ITasksPresenter tasksPresenter, IProjectsService projectsService)
        {
            _categoriesService = categoriesService;
            _tasksPresenter = tasksPresenter;
            _projectsService = projectsService;
        }
        public void Export(string dirPath)
        {
            if (Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath + "\\TasksBackup\\ActiveProjects");
                Directory.CreateDirectory(dirPath + "\\TasksBackup\\Archive");

                var archivePath = dirPath + "\\TasksBackup\\Archive\\";
                var projectsPath = dirPath + "\\TasksBackup\\ActiveProjects\\";

                var projectsList = _projectsService.GetProjectsList();
                foreach(var projectInfo in projectsList)
                {
                    var lines = new List<string>() { $"# {projectInfo.Name}", "----------" };
                    var project = _tasksPresenter.GetProjectWithTasks(projectInfo.Id);
                    foreach (var task in project.Tasks)
                    {
                        lines.Add($"- [ ] {task.Text} ({task.StartTime}-{task.EndTime}) 📅 " + task.DueTo.ToString("yyyy-MM-dd"));
                    }
                    // write to file

                    var projectPath = projectsPath;
                    if (project.IsArchived)
                        projectPath = archivePath;
                    projectPath += $"{SanitizeFileName(project.Name)}.md";
                    File.Create(projectPath).Close();
                    File.WriteAllLines(projectPath, lines.ToArray());
                }
            }
            else throw new Exception("Directory not found: " +  dirPath);
        }

        public void Import(string path)
        {
            throw new NotImplementedException();
        }

        private static string SanitizeFileName(string fileName)
        {
            string invalidChars = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex invalidCharsRegex = new Regex($"[{Regex.Escape(invalidChars)}]");
            var res = invalidCharsRegex.Replace(fileName, "");
            return res.Replace(" ", "_");
        }
    }
}
