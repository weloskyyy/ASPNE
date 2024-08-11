using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TodoListApp.Models;

namespace TodoListApp.Models
{
    public class TodoItem
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsComplete { get; set; }
    }
}


namespace TodoListApp.Controllers
{
    public class TodoController : Controller
    {
        private static List<TodoItem> todoItems = new List<TodoItem>();

        public IActionResult Index()
        {
            return View(todoItems);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TodoItem item)
        {
            if (ModelState.IsValid)
            {
                item.Id = todoItems.Count > 0 ? todoItems.Max(t => t.Id) + 1 : 1;
                todoItems.Add(item);
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        public IActionResult Complete(int id)
        {
            var item = todoItems.FirstOrDefault(t => t.Id == id);
            if (item != null)
            {
                item.IsComplete = true;
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var item = todoItems.FirstOrDefault(t => t.Id == id);
            if (item != null)
            {
                todoItems.Remove(item);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
