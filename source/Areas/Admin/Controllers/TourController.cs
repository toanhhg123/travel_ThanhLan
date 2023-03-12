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
    public class TourController : Controller
    {
        private readonly TravelContext _DbContext;

        private readonly IToastNotification _toastNotification;


        public TourController(TravelContext dBContext, IToastNotification toastNotification)
        {
            _DbContext = dBContext;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {

            var tours = await _DbContext.Tours.ToListAsync();
            return View(tours);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CategoryTours = _DbContext.CategoryTours.ToList();
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> Create([Bind] Tour tour, IFormFile mainImg, string categoryTour)
        {

            try
            {
                var category = await _DbContext.CategoryTours.FirstOrDefaultAsync(x => x.id == categoryTour);
                if (category == null) throw new Exception("Khong tim thay danh muc!!");


                tour.mainImg = HandleFile.UploadSingleFile(mainImg);
                tour.categoryTour = category;
                await _DbContext.AddAsync(tour);
                await _DbContext.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Success");
                return RedirectToAction("index");
            }
            catch (System.Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
                return View(tour);
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
            var tour = await _DbContext.Tours.Include(x => x.TourImages).Include(x => x.categoryTour).FirstOrDefaultAsync(x => x.id == id);
            ViewBag.CategoryTours = await _DbContext.CategoryTours.ToListAsync();
            if (tour == null)
                return BadRequest();
            return View(tour);
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
        public async Task<IActionResult> AddImg(string tourId, string redirectUrl, TourImage tourImage, IFormFile img)
        {

            try
            {
                if (tourId == null || redirectUrl == null || tourImage == null || img == null)
                    throw new Exception("du lieu khong hop le");
                var tour = await _DbContext.Tours.FirstOrDefaultAsync(x => x.id == tourId);
                if (tour == null) throw new Exception("khong tim that tour");

                tourImage.src = HandleFile.UploadSingleFile(img);
                tourImage.Tour = tour;
                Console.WriteLine(redirectUrl);
                await _DbContext.TourImages.AddAsync(tourImage);
                await _DbContext.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("add img success");


                return Redirect(redirectUrl);
            }
            catch (System.Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
                return Redirect(redirectUrl);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Update(Tour tour, string redirectUrl, string categoryTour)
        {
            redirectUrl = redirectUrl ?? "/admin/tour";
            try
            {
                var category = await _DbContext.CategoryTours.FirstOrDefaultAsync(x => x.id == categoryTour);
                var tourDb = await _DbContext.Tours.FirstOrDefaultAsync(x => x.id == tour.id);
                if (category == null) throw new Exception("not found category tour");
                if (tourDb == null) throw new Exception("not fount tour will update");

                tourDb.categoryTour = category;



                _DbContext.Entry(tourDb).CurrentValues.SetValues(tour);
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