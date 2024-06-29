using System;
using System.Web.Mvc;
using ExamPaperDistributionSystem.Models;
using ExamPaperDistributionSystem.Services;

namespace ExamPaperDistributionSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserService _userService;

        private readonly RoleService _roleService;
        public AdminController()
        {
            _userService = new UserService();
            _roleService = new RoleService();
        }

        // GET: /admin/index
        public ActionResult Index()
        {
            // Check if user is authenticated and has admin role
            var user = Session["User"] as User;
            if (user == null || user.Role.Name != "admin")
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        // GET: /admin/manage-users
        public ActionResult ManageUsers()
        {
            // Check if user is authenticated and has admin role
            var user = Session["User"] as User;
            if (user == null || user.Role.Name != "admin")
            {
                return RedirectToAction("Login", "Account");
            }

            // Get list of users from database
            var users = _userService.GetAllUsers();
            return View(users);
        }

        // GET: /admin/manage-roles
        public ActionResult ManageRoles()
        {
            // Check if user is authenticated and has admin role
            var user = Session["User"] as User;
            if (user == null || user.Role.Name != "admin")
            {
                return RedirectToAction("Login", "Account");
            }

            // Get list of roles from database
            var roles = _roleService.GetAllRoles();
            return View(roles);
        }

        // POST: /admin/create-user
        [HttpPost]
        public ActionResult CreateUser(User newUser)
        {
            // Check if user is authenticated and has admin role
            var user = Session["User"] as User;
            if (user == null || user.Role.Name != "admin")
            {
                return RedirectToAction("Login", "Account");
            }

            // Create the user in the database
            _userService.AddUser(newUser);

            return RedirectToAction("ManageUsers");
        }

        // POST: /admin/create-role
        [HttpPost]
        public ActionResult CreateRole(Role newRole)
        {
            // Check if user is authenticated and has admin role
            var user = Session["User"] as User;
            if (user == null || user.Role.Name != "admin")
            {
                return RedirectToAction("Login", "Account");
            }

            // Create the role in the database
            _roleService.AddRole(newRole);

            return RedirectToAction("ManageRoles");
        }
    }
}
