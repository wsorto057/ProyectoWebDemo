using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyectowebDemo.Models;
using System.Data.SqlClient;

namespace proyectowebDemo.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        public ActionResult Index()
        {
            //BDModel db = new BDModel();
            //var data = db.Database.SqlQuery<Department>("exec select_Department");
            return View();
        }

        public ActionResult ViewDepartments()
        {
            BDModel db = new BDModel();
            var data = db.Database.SqlQuery<Department>("exec select_Department");
            return View(data);
        }


        public ActionResult CreateDepartment()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult CreateDepartment(Department collection)
        {
            try
            {
                BDModel db = new BDModel();
                SqlParameter param1 = new SqlParameter("@name", collection.Name);
                SqlParameter param2 = new SqlParameter("@description", collection.Description);
            
                var output = db.Database.SqlQuery<Employee>("exec insert_department @name, @description", param1, param2).SingleOrDefaultAsync();

                //return View("Employee/Index");
                 return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            BDModel db = new BDModel();
            SqlParameter param1 = new SqlParameter("@id", id);
            var data = db.Database.SqlQuery<Department>("exec select_department_by_id @id", param1).First();
            return View(data);
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Department collection)
        {
            try
            {
                BDModel db = new BDModel();
                SqlParameter param1 = new SqlParameter("@id", collection.DepartmentID);
                SqlParameter param2 = new SqlParameter("@name", collection.Name);
                SqlParameter param3 = new SqlParameter("@description", collection.Description);
              

                var output = db.Database.SqlQuery<Department>("exec update_department @id, @name, @description", param1, param2, param3).SingleOrDefaultAsync();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Delete(int id)
        {
            BDModel db = new BDModel();
            SqlParameter param1 = new SqlParameter("@id", id);
            var data = db.Database.SqlQuery<Department>("exec select_department_by_id @id", param1).First();
            return View(data);
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Department collection)
        {
            try
            {
                BDModel db = new BDModel();
                SqlParameter param1 = new SqlParameter("@id", collection.DepartmentID);
                var output = db.Database.SqlQuery<Department>("exec delete_department @id", param1).SingleOrDefaultAsync();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}