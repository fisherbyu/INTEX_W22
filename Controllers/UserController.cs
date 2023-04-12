using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BYU_EGYPT_INTEX.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BYU_EGYPT_INTEX.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var users = _unitOfWork.User.GetUsers();
            return View(users);
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}

