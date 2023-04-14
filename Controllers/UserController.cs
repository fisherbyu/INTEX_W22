using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BYU_EGYPT_INTEX.Areas.Identity.Data;
using BYU_EGYPT_INTEX.Core.Repo;
using BYU_EGYPT_INTEX.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BYU_EGYPT_INTEX.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private ApplicationDbContext context { get; set; }
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserController(IUnitOfWork unitOfWork, SignInManager<ApplicationUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            var users = _unitOfWork.User.GetUsers();
         
            return View(users);

        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = _unitOfWork.User.GetUser(id);
            var roles = _unitOfWork.Role.GetRoles(); //Load up all roles

            var userRoles = await _signInManager.UserManager.GetRolesAsync(user); //Get roles assigned to user
            //Load up roles that match assigned roles into a list
            var roleItems = roles.Select(role =>
                new SelectListItem(
                    role.Name,
                    role.Id,
                    userRoles.Any(ur => ur.Contains(role.Name)))).ToList();

            var vm = new EditUserViewModel //Pass user and assigned roles to view model
            {
                User = user,
                Roles = roleItems
            };
            return View(vm);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = _unitOfWork.User.GetUser(id);
            var roles = _unitOfWork.Role.GetRoles();

            var userRoles = await _signInManager.UserManager.GetRolesAsync(user); //Get roles assigned to user

            var roleItems = roles.Select(role =>
                new SelectListItem(
                    role.Name,
                    role.Id,
                    userRoles.Any(ur => ur.Contains(role.Name)))).ToList();

            var vm = new DeleteUserViewModel //Pass user and assigned roles to view model
            {
                User = user,
                Roles = roleItems
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync(EditUserViewModel data)
        {
            var user = _unitOfWork.User.GetUser(data.User.Id);
            if(user == null)
            {
                return NotFound();
            }
            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);
            //Loop through roles in view model
            //Check if thr role is assigned in db

            var rolesToAdd = new List<string>();
            var rolesToDelete = new List<string>();

            foreach (var role in data.Roles)
            {
                var assignedInDb = userRoles.FirstOrDefault(ur => ur == role.Text);
                if(role.Selected)
                {
                    if(assignedInDb == null)
                    {
                        rolesToAdd.Add(role.Text);
                    }
                }
                else
                {
                    if(assignedInDb != null)
                    {
                        rolesToDelete.Add(role.Text);
                    }
                }
            }
            //Add Role
            if (rolesToAdd.Any())
            {
                await _signInManager.UserManager.AddToRolesAsync(user, rolesToAdd);
            }
            //Remove Role
            if (rolesToDelete.Any())
            {
                await _signInManager.UserManager.RemoveFromRolesAsync(user, rolesToDelete);
            }

            user.Email = data.User.Email;

            _unitOfWork.User.UpdateUser(user);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> OnPostDeleteAsync(EditUserViewModel data)
        {
            var user = _unitOfWork.User.GetUser(data.User.Id);
            if (user == null)
            {
                return NotFound();
            }
            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);

            var rolesToDelete = new List<string>();

            foreach (var role in data.Roles)
            {
                var assignedInDb = userRoles.FirstOrDefault(ur => ur == role.Text);
                if (assignedInDb != null)
                {
                    rolesToDelete.Add(role.Text);
                }
            }

            //Remove Role
            if (rolesToDelete.Any())
            {
                await _signInManager.UserManager.RemoveFromRolesAsync(user, rolesToDelete);
            }

            _unitOfWork.User.RemoveUser(user);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator, User")]
        public IActionResult ManageRecords()
        {
            return View();
        }
    }
}

