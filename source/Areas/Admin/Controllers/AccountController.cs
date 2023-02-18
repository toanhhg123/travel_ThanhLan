using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using source.Config;
using source.Models;
using source.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NToastNotify;

namespace source.Areas.Admin.Controllers
{
    [Area("Admin")]
    // [Authorize(Roles = "ADMIN")]
    public class AccountController : Controller
    {
        private readonly TravelContext _DbContext;

        private readonly IToastNotification _toastNotification;


        public AccountController(TravelContext dBContext, IToastNotification toastNotification)
        {
            _DbContext = dBContext;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {

            var accounts = await _DbContext.Accounts.Include(a => a.Role).ToListAsync();
            return View(accounts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.roles = _DbContext.Roles.ToList();
            return View();
        }


        // [HttpGet]
        // public async Task<IActionResult> SeedRoles()
        // {

        //     await _DbContext.Roles.ForEachAsync(x =>
        //     {
        //         _DbContext.Roles.Remove(x);
        //     });
        //     List<Role> roles = new List<Role>() {
        //         new Role() {RoleName = "ADMIN"},
        //         new Role() {RoleName = "USER"},

        //     };
        //     await _DbContext.Roles.AddRangeAsync(roles);
        //     await _DbContext.SaveChangesAsync();

        //     return Ok(roles);
        // }


        [HttpPost]
        public async Task<IActionResult> Create([Bind] UserRegister user, string role)
        {
            ViewBag.roles = _DbContext.Roles.ToList();

            if (!ModelState.IsValid)
            {
                return Ok(user);
            }


            var roleSelect = _DbContext.Roles.FirstOrDefault(r => r.id == role);
            Account account = new Account()
            {
                userName = user.UserName,
                email = user.Email,
                hashPassword = MD5Password.HashPass(user.Password),
                Role = roleSelect ?? new Role()
            };

            await _DbContext.Accounts.AddAsync(account);
            await _DbContext.SaveChangesAsync();
            _toastNotification.AddSuccessToastMessage("add user success");

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = _DbContext.Accounts.FirstOrDefault(x => x.id == id);
            if (user != null)
            {
                _DbContext.Accounts.Remove(user);
                await _DbContext.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Delete Success");
            }
            return RedirectToAction("index");
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await _DbContext.Accounts.Include(x => x.Role).FirstOrDefaultAsync(x => x.id == id);
            ViewBag.roles = await _DbContext.Roles.ToListAsync();
            if (user == null)
                return BadRequest();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, UserRegister user, string role)
        {
            var roles = await _DbContext.Roles.ToListAsync();
            ViewBag.roles = roles;

            var roleUser = await _DbContext.Roles.FirstOrDefaultAsync(x => x.id == role);
            var account = await _DbContext.Accounts.FirstOrDefaultAsync(x => x.id == id);
            if (roleUser != null && account != null)
            {
                account.userName = user.UserName;
                account.email = user.Email;
                account.hashPassword = MD5Password.HashPass(user.Password);
                account.Role = roleUser;
                await _DbContext.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Update User Success");
                return RedirectToAction("index");

            }

            return BadRequest();
        }
    }
}