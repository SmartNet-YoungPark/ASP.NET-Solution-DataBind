using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CRUD_OperationsInMVC;
using CRUD_OperationsInMVC.Models;

namespace CRUD_OperationsInMVC.Controllers
{
    public class BusinessObjectController : Controller
    {
        //Stored Procedure called, named GetAllMembers()
         public ActionResult Index()
            {
                MemberBusinessLayer memberBusinessLayer = new MemberBusinessLayer();
                List<Member> members = memberBusinessLayer.GetAllMembers();
                return View(members);
            }
        //Form binding
        [HttpGet]
       public ActionResult CreateForm()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateForm(FormCollection formCollection)
        {
            Member member = new Member();
            // Retrieve form data using form collection
            member.Name = formCollection["Name"];
            member.Gender = formCollection["Gender"];
            member.City = formCollection["City"];
            member.Salary = Convert.ToDecimal(formCollection["Salary"]);
            member.DateOfBirth = Convert.ToDateTime(formCollection["DateOfBirth"]);
            MemberBusinessLayer memberBusinessLayer = new MemberBusinessLayer();
            memberBusinessLayer.AddMember(member);
            return RedirectToAction("/Index");
        }
        //Data binding
        [HttpGet]
        [ActionName("Create")]
        public ActionResult Create_Get()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create_Post()
        {
            if (ModelState.IsValid)
            {
                MemberBusinessLayer memberBusinessLayer = new MemberBusinessLayer();
                Member member = new Member();
                //Data binding with UpdateModel or TryUpdateModel
                TryUpdateModel<Member>(member);
                memberBusinessLayer.AddMember(member);
                return RedirectToAction("/Index");
            }
            return View();
        }
        //Data binding
        [HttpGet]
        [ActionName("CreateParam")]
        public ActionResult CreateParam_Get()
        {
            return View();
        }
        [HttpPost]
        [ActionName("CreateParam")]
        public ActionResult CreateParam_Post(Member member)
        {
            if (ModelState.IsValid)
            {
                MemberBusinessLayer memberBusinessLayer = new MemberBusinessLayer();
                memberBusinessLayer.AddMember(member);
                return RedirectToAction("/Index");
            }
            else
            {
                return View();
            }
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            MemberBusinessLayer memberBusinessLayer = new MemberBusinessLayer();
            Member member = memberBusinessLayer.GetAllMembers().FirstOrDefault(emp => emp.ID == id);
            return View(member);
        }
        [HttpPost]
        public ActionResult Edit(Member member)
        {
            if (ModelState.IsValid)
            {
                MemberBusinessLayer memberBusinessLayer = new MemberBusinessLayer();
                memberBusinessLayer.UpdateMember(member);
                return RedirectToAction("/Index");
            }
            return View(member);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            MemberBusinessLayer memberBusinessLayer = new MemberBusinessLayer();
            Member member = memberBusinessLayer.GetAllMembers().FirstOrDefault(emp => emp.ID == id);
            return View(member);
        }
        [HttpPost]
        public ActionResult Delete(Member member)
        {
            if (ModelState.IsValid)
            {

                MemberBusinessLayer memberBusinessLayer = new MemberBusinessLayer();
                memberBusinessLayer.DeleteMember(member);
                return RedirectToAction("/Index");
            }

                return View(member);
            }

        [HttpGet]
        [ActionName("Edit_NoProperty")]
        public ActionResult Edit_NoProperty_Get(int id)
        {
            MemberBusinessLayer memberBusinessLayer = new MemberBusinessLayer();
            Member member = memberBusinessLayer.GetAllMembers().FirstOrDefault(emp => emp.ID == id);
            return View(member);
        }
        [HttpPost]
        [ActionName("Edit_NoProperty")]
        public ActionResult Edit_NoProperty_Post(Member mem)
        {
            MemberBusinessLayer memberBusinessLayer = new MemberBusinessLayer();
            Member member = memberBusinessLayer.GetAllMembers().FirstOrDefault(x => x.ID == mem.ID );
            //Get data from database, bind all columns except Name 
            //Because of voiding Name update
            UpdateModel(member, new string[] { "ID", "Gender", "City", "Salary", "DateOfBirth" });
            if (ModelState.IsValid)
            {
                memberBusinessLayer.UpdateMember(member);
                return RedirectToAction("/Index");
            }
                        
            return View(member);
        }
        [HttpGet]
        [ActionName("Edit_ExcludeProperty")]
        public ActionResult Edit_ExcludeProperty_Get(int id)
        {
            MemberBusinessLayer memberBusinessLayer = new MemberBusinessLayer();
            Member member = memberBusinessLayer.GetAllMembers().FirstOrDefault(x => x.ID == id);
            return View(member);
        }
        [HttpPost]
        [ActionName("Edit_ExcludeProperty")]
        public ActionResult Edit_ExcludeProperty_Post(int id)
        {
            MemberBusinessLayer memberBusinessLayer = new MemberBusinessLayer();
            Member member = memberBusinessLayer.GetAllMembers().Single(x => x.ID == id);
            //Overriding with excludeProperties
            //UpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties)  
            //Because of voiding Name update
            UpdateModel(member, null, null, new string[] { "Name" });
            if (ModelState.IsValid)
            {
                memberBusinessLayer.UpdateMember(member);
                return RedirectToAction("/Index");
            }
            return View(member);
        }
        [HttpGet]
        [ActionName("Edit_NoInterfaceProperty")]
        public ActionResult Edit_NoInterfaceProperty_Get(int id)
        {
            MemberBusinessLayer memberBusinessLayer = new MemberBusinessLayer();
            Member member = memberBusinessLayer.GetAllMembers().FirstOrDefault(x => x.ID == id);
            return View(member);
        }
        [HttpPost]
        [ActionName("Edit_NoInterfaceProperty")]
        public ActionResult Edit_NoInterfaceProperty_Pot(Member mem)
        {
            MemberBusinessLayer memberBusinessLayer = new MemberBusinessLayer();
            Member member = memberBusinessLayer.GetAllMembers().Single(x => x.ID == mem.ID);
            //Using IMember - No Name interface 
            //query database
            UpdateModel<IMember>(member);
            if (ModelState.IsValid)
            {
                memberBusinessLayer.UpdateMember(member);
                return RedirectToAction("/Index");
            }
            return View(member);
        }
        //if (ModelState.IsValid)
        //{
        //    foreach (string key in formCollection.AllKeys)
        //    {
        //        Response.Write("Key = " + key + "  ");
        //        Response.Write("Value = " + formCollection[key]);
        //        Response.Write("<br/>");
        //    }
        //}
        //return View();
    }

    // GET: BusinessObject
    //public ActionResult Index1()
    //{
    //    return View();
    //}
    //// GET: BusinessObject
    //public ViewResult Index()
    //{
    //    ViewData["Countries"] = new List<string>
    //    {
    //        "India",
    //        "US",
    //        "Canada",
    //        "Brazil",
    //        "Korea"
    //    };
    //    return View();


}