using StudentApp.Models;
using System.Linq;
using System.Web.Mvc;

public class AccountController : Controller
{
    private SchoolDBEntities db = new SchoolDBEntities();

    // Hiển thị Form Login
    public ActionResult Login()
    {
        return View();
    }

    // Xử lý khi nhấn nút Đăng nhập
    [HttpPost]
    public ActionResult Login(string username, string password)
    {
        var user = db.tblUser.FirstOrDefault(u => u.Username == username && u.Password == password);
        if (user != null)
        {
            // Nếu đúng, chuyển hướng sang trang danh sách sinh viên
            return RedirectToAction("Index", "Students");
        }
        ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu!";
        return View();
    }
}