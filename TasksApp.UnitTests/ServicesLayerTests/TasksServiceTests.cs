using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksApp.BusinessLogic.Interfaces;
using TasksApp.BusinessLogic.Models;
using TasksApp.BusinessLogic.Services;
using TasksApp.DataAccess.Interfaces;
using TasksApp.DataAccess.Repositories;

namespace TasksApp.UnitTests.ServicesLayerTests
{
    public class TasksServiceTests
    {
        private ITasksRepository mockRepo;
        private ITasksService SUT;
        public TasksServiceTests() 
        {
            //mockRepo = new MockTasksRepository();
            //SUT = new TasksService(mockRepo);
        }

        [Fact]
        public void AddNewTaskWithNoText_Test()
        {
            //var task = new TaskModel { Id = "10" };
            //try
            //{
            //    SUT.AddTask(task);
            //    Assert.True(false);
            //} catch(Exception ex)
            //{
            //    Assert.True(true);
            //}   
        }
    }
}
