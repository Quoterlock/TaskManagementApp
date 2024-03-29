﻿using Microsoft.EntityFrameworkCore;
using System.Windows;
using TasksApp.BusinessLogic.Interfaces;
using TasksApp.BusinessLogic.Models;
using TasksApp.BusinessLogic.Services;
using TasksApp.BusinessLogic.Services.Adapters;
using TasksApp.DataAccess;
using TasksApp.DataAccess.Entities;
using TasksApp.DataAccess.Interfaces;
using TasksApp.DataAccess.Repositories;
using TasksApp.UI.Pages;
using TasksApp.UI.Services;
using TasksApp.UI.Windows;

namespace TasksApp.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServicesContainer services;
        public MainWindow()
        {
            InitializeComponent();
            services = new ServicesContainer();
            SetupBuilder.CheckFiles();

            var path = Properties.Settings.Default.databasePath;
            if (string.IsNullOrEmpty(path))
                path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\TasksApp\\Tasks.db";
            var connectionString = @"Data Source=" + path;

            var dbContext = new AppDbContext(connectionString);

            services.Bind<ITasksRepository, TasksRepository>(dbContext);
            services.Bind<IProjectsRepository, ProjectsRepository>(dbContext);
            services.Bind<ICategoriesRepository, CategoriesRepository>(dbContext);
            services.Bind<IScheduleSchemeRepository, ScheduleSchemeRepository>(dbContext);
            services.Bind<IScheduleTasksRepository, ScheduleTasksRepository>(dbContext);

            services.Bind<IAdapterME<TaskModel, TaskEntity>, TaskAdapter>();
            services.Bind<IAdapterME<ProjectModel, ProjectEntity>, ProjectAdapter>();
            services.Bind<IAdapterME<CategoryModel, CategoryEntity>, CategoryAdapter>();
            services.Bind<IAdapterME<ScheduleBlockModel, ScheduleItemEntity>, ScheduleBlockAdapter>();
            services.Bind<IAdapterME<ScheduleTaskModel, ScheduleTaskEntity>, ScheduleTaskAdapter>();

            services.Bind<IProjectsService, ProjectsService>(
                services.Get<IProjectsRepository>(),
                services.Get<IAdapterME<ProjectModel, ProjectEntity>>());

            services.Bind<ITasksService, TasksService>(
                services.Get<ITasksRepository>(),
                services.Get<IProjectsService>(),
                services.Get<IAdapterME<TaskModel, TaskEntity>>());

            services.Bind<ICategoriesService, CategoriesService>(
                services.Get<ICategoriesRepository>(), 
                services.Get<IProjectsService>(),
                services.Get<IAdapterME<CategoryModel, CategoryEntity>>());

            services.Bind<IScheduleService, ScheduleService>(
                services.Get<IScheduleSchemeRepository>(),
                services.Get<IScheduleTasksRepository>(),
                services.Get<IAdapterME<ScheduleBlockModel, ScheduleItemEntity>>(),
                services.Get<IAdapterME<ScheduleTaskModel, ScheduleTaskEntity>>());

            services.Bind<ITasksPresenter, TasksPresenter>(
                services.Get<ITasksService>(),
                services.Get<IProjectsService>(),
                services.Get<IScheduleService>());


            // home page as a default
            mainFrame.Content = new HomePage(services);
            mainFrame.NavigationService.RemoveBackEntry();
        }

        private void calendarBtn_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new PresentationPage(services);
            mainFrame.NavigationService.RemoveBackEntry(); // clear history
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            var createTaskDialog = new NewTaskWindow(services, string.Empty, DateTime.MinValue);
            if (services.Get<IProjectsService>().GetProjectsList().Count > 0)
                createTaskDialog.ShowDialog();
            else
                MessageBox.Show("Create any project first");
        }

        private void listBtn_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new ListPage(services);
            mainFrame.NavigationService.RemoveBackEntry();
        }

        private void scheduleBtn_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new SchedulePage(services);
            mainFrame.NavigationService.RemoveBackEntry();
        }

        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new HomePage(services);
            mainFrame.NavigationService.RemoveBackEntry();
        }

        private void settingsBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SettingsWindow(services);
            dialog.ShowDialog();
            if (dialog.IsModified)
            {
                MessageBox.Show("Re-boot app to apply changes. So, we'll do it now...");
                this.Close();
            }
        }
    }
}
