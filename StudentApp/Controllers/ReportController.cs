using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentApp.Models; // Đảm bảo đúng namespace Models của em
using Microsoft.Reporting.WebForms;
using System.IO;

namespace StudentApp.Controllers
{
    public class ReportController : Controller
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // Trang để chọn môn học
        public ActionResult Index()
        {
            ViewBag.Courses = new SelectList(db.Courses, "CourseID", "Title");
            return View();
        }

        [HttpPost]
        public ActionResult Generate(int selectedCourseID)
        {
            var reportPath = Path.Combine(Server.MapPath("~/Reports"), "StudentReport.rdlc");
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = reportPath;

            // Lấy dữ liệu theo cách thủ công để tránh lỗi Include
            var data = (from e in db.Enrollments
                        join s in db.Students on e.StudentID equals s.StudentID
                        where e.CourseID == selectedCourseID
                        select new
                        {
                            StudentID = e.StudentID,
                            FirstName = s.FirstName,
                            LastName = s.LastName,
                            Grade = e.Grade
                        }).ToList();

            localReport.DataSources.Add(new ReportDataSource("StudentDataSet", data));

            var reportBytes = localReport.Render("PDF");
            return File(reportBytes, "application/pdf", "StudentReport.pdf");
        }
    }
}