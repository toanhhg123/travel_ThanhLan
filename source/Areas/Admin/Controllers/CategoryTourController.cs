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
    [Authorize]

    // [Authorize(Roles = "ADMIN")]
    public class CategoryTourController : Controller
    {
        private readonly TravelContext _DbContext;

        private readonly IToastNotification _toastNotification;


        public CategoryTourController(TravelContext dBContext, IToastNotification toastNotification)
        {
            _DbContext = dBContext;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {

            try
            {
                var accounts = await _DbContext.CategoryTours.ToListAsync();
                return View(accounts);
            }
            catch (System.Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> Create([Bind] CategoryTour categoryTour)
        {
            try
            {

                if (!ModelState.IsValid)
                    throw new Exception("Bạn cần nhập đầy đủ tất cả các trường !!!");

                await _DbContext.CategoryTours.AddAsync(categoryTour);
                await _DbContext.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Tạo mới thành công !!");
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
                return View(categoryTour);
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {

                var categoryTour = await _DbContext.CategoryTours.FirstOrDefaultAsync(x => x.id == id);
                if (categoryTour == null) throw new Exception("Khong tim duoc category nay !!!");

                _DbContext.CategoryTours.Remove(categoryTour);
                _toastNotification.AddSuccessToastMessage("thành công !!");

                await _DbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
                return RedirectToAction("index");
            }
        }

        public async Task<IActionResult> Details(string id)
        {
            try
            {

                var categoryTour = await _DbContext.CategoryTours.FirstOrDefaultAsync(x => x.id == id);
                if (categoryTour == null) throw new Exception("Khong tim duoc category nay !!!");

                return View(categoryTour);
            }
            catch (System.Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
                return RedirectToAction("index");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Update(CategoryTour category, string redirectUrl)
        {

            redirectUrl = redirectUrl ?? "/admin/categorytour";
            try
            {
                if (!ModelState.IsValid) throw new Exception("is valid body");
                var dataDb = await _DbContext.CategoryTours
                                        .FirstOrDefaultAsync(x => x.id == category.id);
                if (dataDb == null) throw new Exception("dataDbing not found");

                _DbContext.Entry(dataDb).CurrentValues.SetValues(category);
                await _DbContext.SaveChangesAsync();

                _toastNotification.AddSuccessToastMessage("update Success");
                return Redirect(redirectUrl);
            }
            catch (System.Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
                return Redirect(redirectUrl);
            }
        }
    }
}