using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoDemo.DataAccessLayer
{
    public interface ICommonDAL<T> where T : class
    {
        IList<T> List(string searchValue = "");

        int Add(string taskName);

        bool Update(int taskId, string taskName);

        bool Delete(int id);

        T? Get(int id);

        bool UpdateStatus(int taskId, bool status);
    }
}
