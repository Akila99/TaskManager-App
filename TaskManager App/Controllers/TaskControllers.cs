using Microsoft.AspNetCore.Mvc;

namespace TaskManager_App.Controllers
{
    public class TaskControllers : Controller
    {
        public string Index()
        {
            return "This is my default action...";
        }
        // 
        // GET: /HelloWorld/Welcome/ 
        public string Welcome()
        {
            return "This is the Welcome action method...";
        }
    }
}
