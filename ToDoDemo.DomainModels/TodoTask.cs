using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoDemo.DomainModels
{
    public class TodoTask
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }

        public bool Status { get; set; }

    }
}
