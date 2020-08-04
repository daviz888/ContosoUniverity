using ContosoUniversity.DAL;
using ContosoUniversity.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class HomeController : Controller
    {
        private SchoolContext db = new SchoolContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //Commenting out LINQ to shoq how to do same thing in SQL.
            //IQueryable<EnrollmentDateGroup> data = from student in db.Students
            //                                       group student by student.EnrollmentDate into dateGroup
            //                                       select new EnrollmentDateGroup()
            //                                       {
            //                                           EnrollmentDate = dateGroup.Key,
            //                                           StudentCount = dateGroup.Count()
            //                                        };

            //SQLD version 
            string query = "SELECT EnrollmentDate, COUNT(*) AS StudentCount "
                + "FROM Person "
                + "WHERE Discriminator = 'Student' "
                + "GROUP BY EnrollmentDate";
            IEnumerable<EnrollmentDateGroup> data = db.Database.SqlQuery<EnrollmentDateGroup>(query);
            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}