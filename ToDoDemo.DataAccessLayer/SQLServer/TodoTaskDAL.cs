using Azure;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoDemo.DomainModels;

namespace ToDoDemo.DataAccessLayer.SQLServer
{
    public class TodoTaskDAL : _BaseDAL, ICommonDAL<TodoTask>
    {
        public TodoTaskDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(string taskName)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"
                                    insert into Todo(TaskName,Status)
                                    values(@TaskName,@Status);
                                    select @@identity;
                                ";
                var parameters = new
                {
                    TaskName = taskName,
                    Status = 1,
                };
                id = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return id;
        }

        public bool Delete(int id)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = "delete from Todo where TaskId = @TaskId";
                var parameters = new { TaskId = id };
                result = connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public TodoTask? Get(int id)
        {
            TodoTask? data = null;
            using (var connection = OpenConnection())
            {
                var sql = "select * from Todo where TaskId = @TaskId";
                var parameters = new { TaskId = id };
                data = connection.QueryFirstOrDefault<TodoTask>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return data;
        }

        public IList<TodoTask> List(string searchValue = "")
        {
            List<TodoTask> data;
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @"select	*
	                            from	Todo 
	                            where	(@searchValue = N'') or (TaskName like @searchValue)
                            ";
                var parameters = new
                {
                    searchValue = searchValue
                };
                data = (connection.Query<TodoTask>(sql: sql, param: parameters, commandType: CommandType.Text)).ToList();
                connection.Close();
            }
            if (data == null)
                data = new List<TodoTask>();
            return data;
        }

        public bool Update(int taskId, string taskName)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"if not exists(select * from Todo where TaskId <> @TaskId and TaskName = @TaskName)
                                begin
                                    update Todo 
                                    set TaskName = @TaskName
                                    where TaskId = @TaskId
                                end";
                var parameters = new
                {
                    TaskId = taskId,
                    TaskName = taskName
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
            }
            return result;
        }

        public bool UpdateStatus(int taskId, bool status)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"
                                    update Todo 
                                    set Status = @Status
                                    where TaskId = @TaskId
                               ";
                var parameters = new
                {
                    TaskId = taskId,
                    Status = status == true ? 1 : 0,
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
            }
            return result;
        }
    }
}
