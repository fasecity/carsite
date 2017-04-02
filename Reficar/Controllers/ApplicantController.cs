using PagedList;
using Reficar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reficar.Controllers
{
    public class ApplicantController : Controller
    {
        // GET: seeApplicant
        public ActionResult SeeApplicant(string sortOrder, string searchString, int? page)
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            var ppldata = _context.Applicants.ToList();

            ViewBag.nameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.artistSort = String.IsNullOrEmpty(sortOrder) ? "artist_desc" : "";
            ViewBag.genreSort = String.IsNullOrEmpty(sortOrder) ? "genre_desc" : "";
            var sortMusic = from m in ppldata select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                sortMusic = sortMusic.Where(m => m.FirstName.Contains(searchString.ToLower())
                                       || m.LastName.Contains(searchString.ToLower()) || m.Email.Contains(searchString.ToLower()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    sortMusic = sortMusic.OrderByDescending(m => m.DateTime);
                    break;

                case "artist_desc":
                    sortMusic = sortMusic.OrderByDescending(m => m.FirstName);
                    break;

                case "genre_desc":
                    sortMusic = sortMusic.OrderByDescending(m => m.LastName);
                    break;


                default:
                    sortMusic = sortMusic.OrderBy(m => m.DateTime);
                    break;

            }
            return View(sortMusic.ToList().ToPagedList(page ?? 1, 10));
        }









        // GET: Applicant
        public ActionResult Apply()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Apply(Applicant viewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Apply", "Applicant");
            }
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                Applicant app = new Applicant();
                app.FirstName = viewModel.FirstName;
                app.LastName = viewModel.LastName;
                app.Age = viewModel.Age;
                app.PhoneNumber = viewModel.PhoneNumber;
                app.CarInsurance = viewModel.CarInsurance;
                app.Carkilometer = viewModel.Carkilometer;
                app.DesiredLoanAmmount = viewModel.DesiredLoanAmmount;
                app.DateTime = DateTime.Now;
                app.MonthlyIncome = viewModel.MonthlyIncome;
                app.CarMake = viewModel.CarMake;
                app.CarModel = viewModel.CarModel;
                app.CarYear = viewModel.CarYear;
                app.Email = viewModel.Email;
                app.Province = viewModel.Province;

                db.Applicants.Add(app);
                //save changes
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return RedirectToAction("About", "Home");
        }
    }
}