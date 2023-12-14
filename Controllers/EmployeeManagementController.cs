using SBIT3J_SuperSystem_Final.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace SBIT3J_SuperSystem_Final.Controllers
{
    [Authorize]

    public class EmployeeManagementController : Controller

    {
        private DatabaseConnectionEntities dt = new DatabaseConnectionEntities();


        // GET: EmployeeManagement
        public ActionResult Index()
        {

            return View();
        }

        /* ONGOING TIME IN AND TIME OUT */

        //public int GetLatestEmployeeID()
        //{
        //    using (DatabaseConnectionEntities dbModel = new DatabaseConnectionEntities())
        //    {
        //        int latestEmployeeID = (int)dbModel.EmployeeAccounts.Max(e => e.Employee_ID);
        //        return latestEmployeeID;
        //    }
        //}

        //public ActionResult HR_CreateAccount(int id, EmployeeAccount Empl)
        //{
        //    try
        //    {
        //        int latestEmployeeID = GetLatestEmployeeID();
        //        Empl.Employee_ID = latestEmployeeID;

        //        using (DatabaseConnectionEntities dbModel = new DatabaseConnectionEntities())
        //        {
        //            dbModel.EmployeeAccounts.Add(Empl);
        //            dbModel.SaveChanges();
        //        }

        //        return RedirectToAction("Hr_Employee_L");
        //    }

        //    catch
        //    {
        //        return View(Empl);
        //    }
        //}


        // GET: /EmployeeManagement/TimeInn
        public ActionResult TimeInn(int? id)
        {
            var routeValues = ControllerContext.RouteData.Values;
            return View(id);
        }


        // POST: /EmployeeManagement/RecordTimeIn
        [HttpPost]
        public ActionResult RecordTimeIn(EmployeeAccount model)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the Employee_ID from the submitted form
                int employeeId = (int)model.Employee_ID;

                // Find the employee account based on the employeeId
                var employeeAccount = dt.EmployeeAccounts.Find(employeeId);

                if (employeeAccount != null)
                {
                    // Record Time In for the employee
                    var attendance = new Employee_Attendance
                    {
                        Account_ID = employeeId,
                        Time_In = DateTime.Now
                        // You can add other properties as needed
                    };

                    dt.Employee_Attendance.Add(attendance);
                    dt.SaveChanges();

                    return RedirectToAction("Index"); // Redirect to appropriate page
                }
                else
                {
                    ModelState.AddModelError("Employee_ID", "Invalid Employee ID");
                }
            }

            // If there are errors, redisplay the form with the provided model
            return View("TimeInn", model);
        }


        // GET: /EmployeeManagement/TimeOutt
        public ActionResult TimeOutt(int id)
        {
            var employeeAccount = dt.EmployeeAccounts.Find(id);
            return View(employeeAccount);
        }

        // POST: /EmployeeManagement/RecordTimeOut
        [HttpPost]
        public ActionResult RecordTimeOut(int id)
        {
            var employeeAccount = dt.EmployeeAccounts.Find(id);

            // Record Time Out for the employee
            var attendance = dt.Employee_Attendance
                .Where(a => a.Account_ID == id && a.Time_Out == null)
                .FirstOrDefault();

            if (attendance != null)
            {
                attendance.Time_Out = DateTime.Now;
                // You can add other properties as needed
                dt.SaveChanges();
            }

            return RedirectToAction("TimeInn"); // Redirect to appropriate page
        }
    


    /*public ActionResult TimeOutt(int employeeId)
    {
        /*var timeLog = dt.Employee_Attendance.FirstOrDefault(t => t.Account_ID == employeeId && t.Time_Out == null);
        if (timeLog == null)
        {
            return Json(new { message = "Employee has not clocked in yet!" });
        }

        timeLog.Time_Out = DateTime.Now;
        timeLog.Total_Hour_Worked = Employee_Attendance.CalculateTotalHours(timeLog.Time_In, timeLog.Time_Out);
        dt.SaveChanges();
        return Json(new { message = "Time Out recorded successfully!", totalHours = timeLog.Total_Hour_Worked });
        return View();
    }*/



    public ActionResult Hr_Employee_L()
        {
            DatabaseConnectionEntities db = new DatabaseConnectionEntities();
            List<EmployeeInformation> emplist = db.EmployeeInformations.ToList();
            return View(db.EmployeeInformations.ToList());
        }
        public ActionResult Hr_Attendance_L()
        {
            DatabaseConnectionEntities db = new DatabaseConnectionEntities();
            List<Employee_Attendance> emplist = db.Employee_Attendance.ToList();
            //TimeSpan totalHours = Time_In - Time_Out;
            // = Math.Floor(totalHours.TotalHours); // Ignore minutes and seconds
            // Employee_Attendance. = totalHours;
            return View(db.Employee_Attendance.ToList());
        }
        public ActionResult Hr_RequestLeave()
        {
            DatabaseConnectionEntities dbe = new DatabaseConnectionEntities();
            List<Leave_Request> Lr = dbe.Leave_Request.ToList();

            return View(dbe.Leave_Request.ToList());
        }

        public ActionResult HR_Leave()
        {
            Leave_Request model = new Leave_Request();
            return View(model);
        }

        [HttpPost]
        public ActionResult HR_Leave(Leave_Request model)
        {
            Leave_Request dbe = new Leave_Request();
            if (ModelState.IsValid)
            {
                // Save the data to the database
                dt.Leave_Request.Add(model);
                dt.SaveChanges();

                // Redirect to a success page or perform other actions
                return RedirectToAction("Hr_RequestLeave");
            }

            // If the model state is not valid, return to the form with validation errors
            return View(model);
        }




        
        public ActionResult Create(EmployeeInformation dbe)
        {

            try
            {

                using (DatabaseConnectionEntities dbModel = new
                DatabaseConnectionEntities())

                {

                    dbModel.EmployeeInformations.Add(dbe);

                    dbModel.SaveChanges();

                }

                return RedirectToAction("Hr_Employee_L");

            }

            catch


            {

                return View(dbe);
            }

        }

        public ActionResult Details(int id)

        {

            using (DatabaseConnectionEntities dbModel = new
            DatabaseConnectionEntities())
            {
                return
              View(dbModel.EmployeeInformations.Where(x =>
              x.Employee_ID == id).FirstOrDefault());

            }

        }
        public ActionResult Edit(int id)
        {

            using (DatabaseConnectionEntities dbModel =
            new DatabaseConnectionEntities())

            {

                return
                View(dbModel.EmployeeInformations.Where(x
                => x.Employee_ID == id).FirstOrDefault());

            }

        }

        [HttpPost]

        public ActionResult Edit(int id, EmployeeInformation Empl)
        {

            try

            {

                using (DatabaseConnectionEntities dbModel = new
                DatabaseConnectionEntities())

                {

                    dbModel.Entry(Empl).State =
                    EntityState.Modified;

                    dbModel.SaveChanges();

                }

                return RedirectToAction("Hr_Employee_L");

            }

            catch

            {

                return View(Empl);

            }
        }
        public ActionResult Delete(int id)

        {

            using (DatabaseConnectionEntities dbModel = new
            DatabaseConnectionEntities())

            {

                return
                View(dbModel.EmployeeInformations.Where(x =>
                x.Employee_ID == id).FirstOrDefault());

            }

        }

        [HttpPost]
        public ActionResult Delete(int id, EmployeeInformation collection)

        {
            try
            {

                using (DatabaseConnectionEntities dbModel = new
                DatabaseConnectionEntities())

                {

                    EmployeeInformation Empl =
                    dbModel.EmployeeInformations.Where(x => x.Employee_ID ==
                    id).FirstOrDefault();

                    dbModel.EmployeeInformations.Remove(Empl);

                    dbModel.SaveChanges();

                }

                return RedirectToAction("Hr_Employee_L");

            }

            catch
            {

                return View();

            }
        }

        /*VVV-- Dito ako nagkakaproblema once na debug run mo siya, ayaw magsubmit ng data..
         * kasi kapag nagsubmit ka dapat mapupunta sa Employee List as indicator kung gumagana --VVV*/

        /* public ActionResult HR_CreateAccount(int id1)
         {

             try
             {
             id1 = new id;
                 using (DatabaseConnectionEntities dbModel = new
                 DatabaseConnectionEntities())

                 {
                     dbModel.EmployeeInformations.Add(id);
                     dbModel.SaveChanges();
                 }
                 return RedirectToAction("Hr_Employee_L");
             }
             catch
             {

                 return View(id1);
             }

         }*/
        /* public ActionResult HR_CreateAccount(int id, EmployeeAccount dbe)
         {
             try {


                 using (DatabaseConnectionEntities dbModel = new DatabaseConnectionEntities())
                 {
                     EmployeeAccount employee = dbModel.EmployeeAccounts.Where(x => x.Employee_ID ==
                     id).FirstOrDefault();
                     dbModel.EmployeeAccounts.Add(dbe);
                     dbModel.SaveChanges();
                 }
                 return RedirectToAction("Hr_Employee_L");
             }
             catch { 
                 return View(dbe); 
             }*/



        /*public ActionResult HR_CreateAccount(EmployeeAccount dbe)
        {
            try
            {
                using (DatabaseConnectionEntities dbModel = new
                DatabaseConnectionEntities())
                {
                    EmployeeAccount Empl =
                    dbModel.EmployeeAccounts.Where(x => x.Employee_ID ==
                    id).FirstOrDefault();
                    dbModel.EmployeeAccounts.Add(dbe);
                    dbModel.SaveChanges();
                }
                return RedirectToAction("Hr_Employee_L");
            }
            catch
            {
                return View(dbe);
            }

        }*/



        //public int GetLatestEmployeeID()
        //{
        //    using (DatabaseConnectionEntities dbModel = new DatabaseConnectionEntities())
        //    {
        //        int latestEmployeeID = (int)dbModel.EmployeeAccounts.Max(e => e.Employee_ID);
        //        return latestEmployeeID;
        //    }
        //}



        //public ActionResult HR_CreateAccount(EmployeeAccount dbea)
        //{
        //    try
        //    {
        //        int latestEmployeeID = GetLatestEmployeeID();
        //        dbea.Employee_ID = latestEmployeeID;

        //        using (DatabaseConnectionEntities dbModel = new DatabaseConnectionEntities())
        //        {
        //            dbModel.EmployeeAccounts.Add(dbea);
        //            dbModel.SaveChanges();
        //        }

        //        return RedirectToAction("Hr_Employee_L");
        //    }
        //    catch
        //    {
        //        return View(dbea);
        //    }
        //}


















        //public ActionResult HR_CreateAccount(int id, EmployeeAccount Empl)
        //{
        //    try
        //    {
        //        int latestEmployeeID = GetLatestEmployeeID();
        //        Empl.Employee_ID = latestEmployeeID;

        //        using (DatabaseConnectionEntities dbModel = new DatabaseConnectionEntities())
        //        {
        //            dbModel.EmployeeAccounts.Add(Empl);
        //            dbModel.SaveChanges();
        //        }

        //        return RedirectToAction("Hr_Employee_L");
        //    }

        //    catch
        //    {
        //        return View(Empl);
        //    }
        //}

        public ActionResult HR_CreateAccount(int id)
        {
            DatabaseConnectionEntities dbModel = new DatabaseConnectionEntities();
            return
                View(dbModel.EmployeeInformations.Where(x =>
                x.Employee_ID == id).FirstOrDefault());
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HR_CreateAccount(EmployeeAccount model)
        {
            if (ModelState.IsValid)
            {
                // check if employee has a list in the employeeInformation table
                using (var context = new DatabaseConnectionEntities())
                {
                    var employeeInfo = context.EmployeeInformations.FirstOrDefault(e => e.Employee_ID == model.Employee_ID);

                    if (employeeInfo != null)
                    {
                        // create employee account
                        context.EmployeeAccounts.Add(model);
                        context.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Employee does not have a list in the employeeInformation table.");
                    }
                }
            }

            return View(model);
        }





    }

}
