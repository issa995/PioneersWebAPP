using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using System.Linq.Dynamic;
using System.IO;

namespace WebApplication.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            DBContext db = new DBContext();
            var model = db.Informations.ToList();
            return View(model);
        }

        public ActionResult AddAndEdit(int ID=0)
        {
            DBContext db = new DBContext();
            ViewBag.l = db.Informations.Select(x => new {
                id = x.ID,
            n="jordan"
            }).ToList();
             
           
            var edit = db.Informations.Where(x => x.ID == ID).FirstOrDefault();
            return View(edit);
        }
        public ActionResult Save(Information info, HttpPostedFileBase file)
        {
            DBContext db = new DBContext();

            if (ModelState.IsValid)
            {
                if (info.ID == 0)
                {
                   
                    db.Informations.Add(info);
                }
                else
                {
               var edit = db.Informations.Where(x => x.ID == info.ID).First();
                    edit.CustomerName = info.CustomerName;
                    edit.Country = info.Country;
                    edit.City = info.City;
                    edit.Address = info.Address;
                    edit.TaxNumber = info.TaxNumber;
                    edit.Active = info.Active;

                    //for image
                    //string ImagePath = string.Empty;
                    //if (file != null)
                    //    {
                    //        ImagePath = Path.Combine(Server.MapPath("~/Images/Authors"), file.FileName);
                    //        file.SaveAs(ImagePath);
                    //        ImagePath = "~/Images/Authors/" + file.FileName;
                    //        edit.Logo = ImagePath;

                    //    }


                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("AddAndEdit", info);
        }
        public ActionResult Delete(int Id)
        {
            DBContext db = new DBContext();
            var delete = db.Informations.Find(Id);
            db.Informations.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult LoadData()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
            using (var db = new DBContext())
            {    
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
   
                var model = db.Informations.Select(x => new {
                    ID = x.ID,
                    CustomerName = x.CustomerName,
                    Country = x.Country,
                    City = x.City,
                    Address = x.Address,
                    TaxNumber = x.TaxNumber,
                    Active = x.Active
                }).ToList();

                
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    model = model.OrderBy(sortColumn + " " + sortColumnDir).ToList();
                }
                 
                if (!string.IsNullOrEmpty(searchValue))
                {
                    model = model.Where(m => m.CustomerName.Contains(searchValue)).ToList();
                }

               
                recordsTotal = model.Count();
                
                var data = model.Skip(skip).Take(pageSize).ToList();    
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
        }
    }
    }



