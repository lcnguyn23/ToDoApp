using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoDemo.BusinessLogicLayer;
using ToDoDemo.DataAccessLayer.SQLServer;
using ToDoDemo.DomainModels;
using ToDoDemo.PresentationLayer.Models;
namespace ToDoDemo.PresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var data = CommonDataService.ListOfTasks();
            var model = new TodoList()
            {
                Data = data
            };
            return View(model);
        }

        public IActionResult Add(string taskName) 
        {
            int taskId = CommonDataService.AddTask(taskName);
            if (taskId > 0)
            {
                return RedirectToAction("Index");
            }
            ViewBag.ErrorMessage = "Không thể thêm task mới";
            return View();
        }

        [HttpGet]
        public IActionResult UpdateStatus(int taskId, bool newStatus)
        {
            bool success = CommonDataService.UpdateTaskStatus(taskId, newStatus);
            if (success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest("Không thể cập nhật trạng thái của công việc.");
            }
        }

        public IActionResult Update(int taskId, string taskName)
        {
            bool success = CommonDataService.UpdateTask(taskId, taskName);
            if (success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest("Không thể cập nhật công việc.");
            };
        }
        public IActionResult Delete(int taskId)
        {
            bool success = CommonDataService.DeleteTask(taskId);
            if (success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest("Không thể xóa công việc.");
            };
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}