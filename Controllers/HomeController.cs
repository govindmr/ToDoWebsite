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
    public class HomeController : Controller
    {
        ToDoListAdoRepository repository;
        public HomeController() { 

            repository = new ToDoListAdoRepository();
        
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
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
            return View(result);
        }

        public ActionResult GetTask(int id)
        {
            var result = repository.GetTask(id);
            return View(result);
        }

        public ActionResult Update(int id)
        {
            var result = repository.GetTask(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult Update(ToDOListEntity model)
        {
            if (model != null)
            {
                var result = repository.Update(model.Id, model);
            }
            return RedirectToAction("GetAllTasks");
        }

        public ActionResult Delete(int id)
        {
            var result = repository.DeleteTask(id);
            return RedirectToAction("GetAllTasks");
        }
       
    }
}