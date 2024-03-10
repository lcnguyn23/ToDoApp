using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoDemo.DataAccessLayer;
using ToDoDemo.DomainModels;

namespace ToDoDemo.BusinessLogicLayer
{
    public static class CommonDataService
    {
        private static readonly ICommonDAL<TodoTask> taskDB;

        static CommonDataService()
        {
            string connectionString = "server=TANLOC;user id=sa;password=12345;database=TodoApp;TrustServerCertificate=true";
            taskDB = new DataAccessLayer.SQLServer.TodoTaskDAL(connectionString);
        }

        public static List<TodoTask> ListOfTasks()
        {
            return taskDB.List().ToList();
        }

        public static int AddTask(string taskName)
        {
            return taskDB.Add(taskName);
        }

        public static bool UpdateTask(int taskId, string taskName)
        {
            return taskDB.Update(taskId, taskName);
        }

        public static bool DeleteTask(int id)
        {
            return taskDB.Delete(id);
        }

        public static TodoTask? GetTask(int id)
        {
            return taskDB.Get(id);
        }

        public static bool UpdateTaskStatus(int id, bool status)
        {
            return taskDB.UpdateStatus(id, status);
        }
    }
}
