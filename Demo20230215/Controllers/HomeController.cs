using Demo20230215.Models;
using Demo20230215.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using static Demo20230215.Models.EmployeeModel;
using static System.Net.Mime.MediaTypeNames;

namespace Demo20230215.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            EmpMethods empMethods = new EmpMethods();
            List<EmployeeModel> empList = empMethods.GetEmpData();
            return View(empList);
        }

        public PartialViewResult _EmpData(string EmpNo = "")
        {
            List<EmployeeModel> empList = new List<EmployeeModel>();
            if (Session["EmpData"] == null)
            {
                EmpMethods empMethods = new EmpMethods();
                empList = empMethods.GetEmpData();
                Session["EmpData"] = empList;
            }
            else
            {
                empList = Session["EmpData"] as List<EmployeeModel>;
            }

            if(string.IsNullOrEmpty(EmpNo)) 
            {
                return PartialView(empList);
            }
            var result = new List<EmployeeModel>();
            //foreach(var item in empList) 
            //{
            //if (item.EmpNo == EmpNo)
            //    {
            //        result.Add(item);
            //    }
            //} 以上幾行註解為匿名舉派作法 作用等於下面這行
            return PartialView(empList.Where(x=>x.EmpNo==EmpNo).ToList());
            //上面那行可拆成 linQ作法
            //result = (from x in empList
            //          where x.EmpNo
            //          select x).ToList();
        }

        public ActionResult About()
        {
            Session["D1"] = "Hello Session";
            ViewBag.D1 = "Hello ViewBag";
            Session["D1"] = "Hello Session";
            TempData["D1"] = "HelloTempDate";
            //Session["D1"] = "Hello Session";
            //Session["D1"] = "Hello Session";
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        public ActionResult Edit(string EmpNo)
        {
            MessageInfo message = new MessageInfo();
            //判斷輸入參數
            if (string.IsNullOrEmpty(EmpNo))
            {
                return View(new EmployeeModel());
                //return Json(new MessageInfo() { IsSuccess = false, Msg= "無輸入參數"});
            }
            if (Session["EmpData"] == null)
            {
                return View(new EmployeeModel());
            }
            EmployeeModel result = ((List<EmployeeModel>)Session["EmpData"]).Where(x => x.EmpNo == EmpNo).FirstOrDefault();

            return View(result);
        }
        [HttpPost]
        public JsonResult EditPost(EmployeeModel empData)
        {
            MessageInfo messageInfo = new MessageInfo() { IsSuccess = true,Msg =""};
            //檢核資料為回家作業

            if (Session["EmpData"] == null)
            {
                messageInfo.IsSuccess = false;
                messageInfo.Msg = "No Data";
                return Json(messageInfo);
            }

            List<EmployeeModel> result = (List<EmployeeModel>)Session["EmpData"];//new List<EmployeeModel>();

            if(result.Where(x=>x.EmpNo == empData.EmpNo).Count() == 0)
            {
                messageInfo.IsSuccess = false;
                messageInfo.Msg = "No Data";
                return Json(messageInfo);
            }
            var emp = result.Where(x=>x.EmpNo ==empData.EmpNo).FirstOrDefault();
            emp.Name = empData.Name;
            emp.Ext= empData.Ext;
            Session["EmpData"] = result;
            return Json(messageInfo);

        }
        [HttpPost]
        
        public JsonResult DeletePost(string EmpNo)
        {
            MessageInfo messageInfo = new MessageInfo() { IsSuccess = true, Msg = "" };
            if (Session["EmpData"] == null)
            {
                messageInfo.IsSuccess = false;
                messageInfo.Msg = "查無資料";
                return Json(messageInfo);
            }
            List<EmployeeModel> empDatas = Session["EmpData"] as List<EmployeeModel>;
            if (empDatas.Where(x=>x.EmpNo == EmpNo).Count() == 0)
            {
                messageInfo.IsSuccess = false;
                messageInfo.Msg = "查無資料";
                return Json(messageInfo);
            }

            empDatas.Remove(empDatas.Where(x=>x.EmpNo==EmpNo).FirstOrDefault());
            Session["EmpData"] = empDatas;

            return Json(messageInfo);
        }

        public ActionResult Insert1()
        {
            return View();
        }

        public JsonResult Save1(EmployeeModel employees) 
        {
            MessageInfo messageInfo = new MessageInfo() { IsSuccess = true, Msg = "" };
            List<EmployeeModel> result = (List<EmployeeModel>)Session["EmpData"];
            EmpMethods empMethods = new EmpMethods();
            var emplist = empMethods.InsertEmpData(result, employees.EmpNo, employees.Name, employees.Ext);
            Session["EmpData"] = emplist; //存回DB的概念

            return Json(messageInfo);
        }

        public JsonResult Save2(string EmpNo , string Name , int Ext)
        {
            MessageInfo messageInfo = new MessageInfo() { IsSuccess = true, Msg = "" };
            List<EmployeeModel> result = (List<EmployeeModel>)Session["EmpData"];
            EmpMethods empMethods = new EmpMethods();
            var emplist = empMethods.InsertEmpData(result, EmpNo, Name, Ext);
            Session["EmpData"] = emplist; //存回DB的概念

            return Json(messageInfo);
        }
    }
}