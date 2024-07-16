using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDOListModel;
using AdoRepositories;
using System.Reflection;
using Newtonsoft.Json;
using System.Data.Entity.Migrations;

namespace ToDoWebsite.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        ToDoListAdoRepository repository;
        public HomeController() { 

            repository = new ToDoListAdoRepository();
        
        }

        // GET: Home
        public ActionResult Index()
        {
            var result = repository.GetAllTasks();
            return View(result);
        }
        [Authorize(Roles = "Admin, Customer")]
        public ActionResult Create()
        {
           return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Customer")]
        public ActionResult Create(ToDOListEntity model)
        {
            if (ModelState.IsValid)
            {
                if (model != null)
                {
                    var result = repository.CreateTasks(model);
                    if (result > 0)
                    {
                        ModelState.Clear();
                        ViewBag.IsSuccess = "Success";
                    }
                    else
                    {
                        ViewBag.IsSuccess = "Failed";
                    }
                }
            }
            return View();
        }

        public ActionResult GetAllTasks()
        {
            var result = repository.GetAllTasks();
            var results = result.OrderBy(x => x.SortOrder);
            return View(results);
        }

        public ActionResult GetTask(int id)
        {
            var result = repository.GetTask(id);
            return View(result);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Update(int id)
        {
            var result = repository.GetTask(id);
            return View(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Update(ToDOListEntity model)
        {
            if (model != null)
            {
                var result = repository.Update(model.Id, model);
            }
            return RedirectToAction("GetAllTasks");
        }

        [HttpPost]
        public ActionResult UpdateSortOrder(ToDOListEntity[] model)
        {
            if (model != null)
            {
                var result = repository.UpdateSortOrder(model);
            }
            return RedirectToAction("GetAllTasks");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var result = repository.DeleteTask(id);
            return RedirectToAction("GetAllTasks");
        }
       
    }
}