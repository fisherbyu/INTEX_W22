using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BYU_EGYPT_INTEX.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BYU_EGYPT_INTEX.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext context { get; set; }
        public UserController(ApplicationDbContext temp)
        {
            context = temp;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            var users = context.Users.ToList();
         
            return View(users);

        }

        [Authorize(Roles = "Administrator")]
        public IActionResult EditUser()
        {
            return View();
        }
        [Authorize(Roles = "Administrator, User")]
        public IActionResult ManageRecords()
        {
            return View();
        }
    }
}

