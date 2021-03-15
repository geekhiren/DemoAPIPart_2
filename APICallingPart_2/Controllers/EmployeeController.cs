using APICallingPart_2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APICallingPart_2.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            using (var db = new My_DataBaseEntities())
            {
                return View(db.Employees.ToList());
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                using (var db = new My_DataBaseEntities())
                {
                    db.Employees.Add(employee);
                    db.SaveChanges();
                    ViewBag.Message = String.Format("Alert\n\n Data inserted successfully.");
                    return RedirectToAction("index");
                }
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            using (var db = new My_DataBaseEntities())
            {
                var tempdata = db.Employees.Where(a => a.Id.Equals(id)).FirstOrDefault();
                return View(tempdata);
            }
        }
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                using (var db = new My_DataBaseEntities())
                {
                    var empdata = db.Employees.Where(a => a.Id.Equals(employee.Id)).FirstOrDefault();
                    if(empdata != null)
                    {
                        empdata.name = employee.name;
                        empdata.City = employee.City;
                        empdata.Country = employee.Country;
                    }
                    db.Entry(empdata).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("index");
                }
            }
            return View();
        }
        public ActionResult Details(int id)
        {
            using (var db = new My_DataBaseEntities())
            {
                var tempdata = db.Employees.Where(a => a.Id.Equals(id)).FirstOrDefault();
                return View(tempdata);
            }
        }
        public ActionResult Delete(int id)
        {
            using (var db = new My_DataBaseEntities())
            {
                var tempdata = db.Employees.Where(a => a.Id.Equals(id)).FirstOrDefault();
                var temp = db.Employees.Remove(tempdata);
                
                db.SaveChanges();
                return RedirectToAction("index");
            }
        }
    }
}