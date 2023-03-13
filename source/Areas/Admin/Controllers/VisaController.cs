using System.Security.Cryptography.X509Certificates;
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
    public class VisaController : Controller
    {
        private readonly TravelContext _DbContext;

        private readonly IToastNotification _toastNotification;


        public VisaController(TravelContext dBContext, IToastNotification toastNotification)
        {
            _DbContext = dBContext;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {

            var Visas = await _DbContext.Visas.ToListAsync();
            return View(Visas);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> Create([Bind] Visa visa, IFormFile mainImg)
        {

            try
            {


                visa.mainImg = HandleFile.UploadSingleFile(mainImg);
                await _DbContext.AddAsync(visa);
                await _DbContext.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Success");
                return RedirectToAction("index");
            }
            catch (System.Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
                return View(visa);
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var tour = await _DbContext.Tours.Include(x => x.TourImages).FirstOrDefaultAsync(x => x.id == id);
                if (tour == null) throw new Exception("Không thể xoá Tour");
                _DbContext.TourImages.RemoveRange(tour.TourImages);
                _DbContext.Tours.Remove(tour);
                await _DbContext.SaveChangesAsync();
                
                HandleFile.DeleteFile(tour.mainImg);
                tour.TourImages.ForEach(x => HandleFile.DeleteFile(x.src));
                _toastNotification.AddSuccessToastMessage("success");

                return RedirectToAction("index");

            }
            catch (System.Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
                return RedirectToAction("index");
            }
        }

        public async Task<IActionResult> Details(string id)
        {
            var visa = await _DbContext.Visas.FirstOrDefaultAsync(x => x.id == id);
            if (visa == null)   throw new Exception("not found visa");
            return View(visa);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImg(TourImage tourImage, string redirectUrl)
        {
            try
            {

                _DbContext.TourImages.Remove(tourImage);
                await _DbContext.SaveChangesAsync();
                HandleFile.DeleteFile(tourImage.src);
                _toastNotification.AddSuccessToastMessage("succcess");



            }
            catch (System.Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
            }
            return Redirect(redirectUrl);

        }

      


        [HttpPost]
        public async Task<IActionResult> Update(Visa visa, string redirectUrl)
        {
            redirectUrl = redirectUrl ?? "/admin/visa";
            try
            {
                var visaDb = await _DbContext.Visas.FirstOrDefaultAsync(x => x.id == visa.id);
                if (visaDb == null) throw new Exception("not fount visa will update");




                _DbContext.Entry(visaDb).CurrentValues.SetValues(visa);
                await _DbContext.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("update succcess");
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