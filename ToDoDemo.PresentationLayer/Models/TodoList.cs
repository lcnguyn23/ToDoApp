using ToDoDemo.DomainModels;

namespace ToDoDemo.PresentationLayer.Models
{
    public class TodoList
    {
        public IList<TodoTask> Data { get; set; }
    }
}
