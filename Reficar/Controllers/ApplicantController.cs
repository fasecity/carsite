using PagedList;
using Reficar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Reficar.Controllers
{
    public class ApplicantController : Controller
    {
        // GET: seeApplicant
        [Authorize(Users = "mo@test.com")]
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
                    sortMusic = sortMusic.OrderBy(m => m.FirstName);
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
        public async System.Threading.Tasks.Task<ActionResult> Apply(Applicant viewModel)
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

            ///////////////////////////email///////////////////////////////
            var body = "<p>Email From new client: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress("audiotrash112@gmail.com"));  // replace with valid value recipient
            message.From = new MailAddress("audiotrash112@gmail.com");  // replace with valid value sender
            message.Subject = "Your email subject";
            message.Body = string.Format(body, viewModel.Email, viewModel.FirstName, viewModel.PhoneNumber, viewModel.DateTime, viewModel.CarMake,
                viewModel.CarYear, viewModel.Province, viewModel.CarInsurance );
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "",  // replace with valid value
                    Password = ""  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = " smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }
            //////////////////////////////////////////////////////////////

            return RedirectToAction("About", "Home");
        }



        /////////////////////////////

    }
}
