using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyectowebDemo.Models;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace proyectowebDemo.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            BDModel db = new BDModel();
            var data = db.Database.SqlQuery<Employee>("exec select_employee");
            return View(data);
           
        }

        public ActionResult ViewEmployees()
        {
            BDModel db = new BDModel();
            var data = db.Database.SqlQuery<Employee>("exec select_employee");
            return View(data);
        }


        public ActionResult CreateEmployee()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult CreateEmployee(Employee collection)
        {
            try
            {
                BDModel db = new BDModel();
                SqlParameter param1 = new SqlParameter("@name", collection.Name);
                SqlParameter param2 = new SqlParameter("@position", collection.Position);
                SqlParameter param3 = new SqlParameter("@age", collection.Age);
                SqlParameter param4 = new SqlParameter("@salary", collection.Salary);
                var output = db.Database.SqlQuery<Employee>("exec insert_employee @name, @position, @age, @salary", param1, param2, param3, param4).SingleOrDefaultAsync();

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
            var data = db.Database.SqlQuery<Employee>("exec select_employee_by_id @id", param1).First();
            return View(data);
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Employee collection)
        {
            try
            {
                BDModel db = new BDModel();
                SqlParameter param1 = new SqlParameter("@id", collection.EmployeeID);
                SqlParameter param2 = new SqlParameter("@name", collection.Name);
                SqlParameter param3 = new SqlParameter("@position", collection.Position);
                SqlParameter param4 = new SqlParameter("@age", collection.Age);
                SqlParameter param5 = new SqlParameter("@salary", collection.Salary);

                var output = db.Database.SqlQuery<Employee>("exec update_employee @id, @name, @position, @age, @salary", param1, param2, param3,param4, param5).SingleOrDefaultAsync();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            BDModel db = new BDModel();
            SqlParameter param1 = new SqlParameter("@id", id);
            var data = db.Database.SqlQuery<Employee>("exec select_employee_by_id @id", param1).First();
            return View(data);
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Employee collection)
        {
            try
            {
                BDModel db = new BDModel();
                SqlParameter param1 = new SqlParameter("@id", collection.EmployeeID);
                var output = db.Database.SqlQuery<Employee>("exec delete_employee @id", param1).SingleOrDefaultAsync();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult export_report()
        {
            BDModel db = new BDModel();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "CrystalReport.rpt"));
            rd.SetDataSource(db.Employee.ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream,"application/pdf", "Employee_List.pdf");

            }
            catch
            {
                throw;
            }


           // return View(db.Employee.ToList());
        }

    }
}