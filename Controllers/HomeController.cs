using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private static List<Student> StudentList = new List<Student>();

        public IActionResult Index()
        {
            var chromeProcesses = Process.GetProcessesByName("brave");

            var processList = new List<string>();
            foreach (var process in chromeProcesses)
            {
                processList.Add($"PID: {process.Id}, Memory: {process.WorkingSet64 / 1024 / 1024} MB");
            }
            ViewBag.ProcessList = processList;

            string sessionId = HttpContext.Session.Id;
            ViewBag.SessionID = sessionId;
            return View();
        }

        [HttpPost]
        public IActionResult Submit(Student student)
        {
            if (ModelState.IsValid)
            {
                StudentList.Add(student);
                ViewBag.Message = "Form submitted successfully!";
                return View("Index");
            }

            return View("Index", student);
        }

        public IActionResult ViewStudents()
        {
            
            return View(StudentList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
