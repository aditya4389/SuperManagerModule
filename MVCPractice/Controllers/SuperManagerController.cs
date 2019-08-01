﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;

namespace MVCPractice.Controllers
{
    public class SuperManagerController : Controller
    {
        //
        // GET: /SuperManager/

        public ActionResult Index()
        {
            ////string userId = Convert.ToString(Session["userId"]);
            //BankEntities1 dbContext = new BankEntities1();
            ////Customer customer = dbContext.Customers.Single(x => x.userId == userId);
            //List<Manager> managers = dbContext.Managers.ToList();
            ////Session["medal"] = null;
            //return View(managers);
            if (Session["mgrId"] != null)
            {
                Session["mgrId"] = null;
                return View();
            }
            return View();
        }

        public ActionResult ManagersList()
        {
            BankEntities1 newObj = new BankEntities1();
            List<Manager> managers = newObj.Managers.ToList();
            return View(managers);
        }

        public ActionResult ManageManager()
        {
            return View();
        }

        [HttpPost]

        public ActionResult ManageManager(int managerId)
        {
            BankEntities1 dbContext = new BankEntities1();
            Manager managers = dbContext.Managers.Single(x => x.managerId == managerId);
            ViewBag.mgrId = managerId;
            return View(managers);
        }

        public ActionResult AddManager()
        {
            return View();
        }

         [HttpPost]
        public ActionResult AddManager(string name, string branch, string address, string phno, string mail)
        {

            try
            {

                SuperManagerClass obj = new SuperManagerClass();

                string res = obj.addManager(name, branch, address, phno, mail);
                ViewBag.result = res;

            }

            catch (Exception exp)
            {
                ViewBag.Error = "Exception " + exp;
            }
            return RedirectToAction("Index");
        }

        
        public ActionResult EditManager(int id)
         {
             ViewBag.mgrId = id;
             Session["mgrId"] = id;
             BankEntities1 newObj = new BankEntities1();
             try
             {
                 SuperManagerClass superManagerClass = new SuperManagerClass();
                 List<string> branchesList = superManagerClass.getNonAssignedBranches();
             }
             catch (Exception e)
             {
                 ViewBag.Error = "Exception " + e;
             }
             Manager obj = newObj.Managers.Single(x => x.managerId == id);
            return View(obj);
         }

        [HttpPost]
        public ActionResult EditManager(string managerName, string address, string phoneNo, string emailId, string branchId)
        {
            int mgrId =(int)Session["mgrId"];
            try 
            {
                SuperManagerClass obj = new SuperManagerClass();

                string res = obj.assignToZero(mgrId);
                //ViewBag.result = res;



                string resu = obj.editManager(mgrId, managerName, address, phoneNo, emailId, branchId);
                ViewBag.result1 = resu;

            }

            catch (Exception exp)
            {
                ViewBag.Error = "Exception " + exp;
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteManager(int id)
        {
            ViewBag.mgrId = id;
            Session["mgrId"] = id;
            //try
            //{

            //    SuperManagerClass obj = new SuperManagerClass();

            //    string res = obj.deleteManager(id);
            //    ViewBag.result = res;

            //}

            //catch (Exception exp)
            //{
            //    ViewBag.Error = "Exception " + exp;
            //}
            return View();
        }

        [HttpPost]

        public ActionResult DeleteManager()
        {
            int mgrId = (int)Session["mgrId"];
            try
            {

                SuperManagerClass obj = new SuperManagerClass();

                string res = obj.deleteManager(mgrId);
                ViewBag.result = res;

            }

            catch (Exception exp)
            {
                ViewBag.Error = "Exception " + exp;
            }
            return RedirectToAction("Index");
        }


    }
}