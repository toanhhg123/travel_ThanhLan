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
    public class HotelController : Controller
    {
        private readonly TravelContext _DbContext;

        private readonly IToastNotification _toastNotification;


        public HotelController(TravelContext dBContext, IToastNotification toastNotification)
        {
            _DbContext = dBContext;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {

            var tours = await _DbContext.Hotels.ToListAsync();
            return View(tours);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CategoryTours = _DbContext.CategoryTours.ToList();
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> Create([Bind] Hotel hotel, IFormFile mainImg)
        {
            try
            {
                hotel.mainImg = HandleFile.UploadSingleFile(mainImg);
                await _DbContext.Hotels.AddAsync(hotel);
                await _DbContext.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Success");
                return RedirectToAction("index");
            }
            catch (System.Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
                return View(hotel);
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var hotel = await _DbContext.Hotels.Include(x => x.HotelImgs).FirstOrDefaultAsync(x => x.id == id);
                if (hotel == null) throw new Exception("Không thể xoá Tour");

                HandleFile.DeleteFile(hotel.mainImg);
                hotel.HotelImgs.ForEach(x => HandleFile.DeleteFile(x.src));

                _DbContext.Hotels.Remove(hotel);
                await _DbContext.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("success");

                return RedirectToAction("index");

            }
            catch (System.Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
                return RedirectToAction("index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {

            try
            {
                var hotel = await _DbContext.Hotels.Include(x => x.HotelImgs).FirstOrDefaultAsync(x => x.id == id);
                if (hotel == null)
                    throw new Exception("not found hotel");
                return View(hotel);
            }
            catch (System.Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImg(HotelImg img, string redirectUrl)
        {
            try
            {

                _DbContext.HotelImages.Remove(img);
                await _DbContext.SaveChangesAsync();
                HandleFile.DeleteFile(img.src);
                _toastNotification.AddSuccessToastMessage("succcess");



            }
            catch (System.Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
            }
            return Redirect(redirectUrl);

        }

        [HttpPost]
        public async Task<IActionResult> AddImg(string hotelId, string redirectUrl, HotelImg hotelImage, IFormFile img)
        {

            try
            {
                if (hotelId == null || redirectUrl == null || hotelImage == null || img == null)
                    throw new Exception("du lieu khong hop le");
                var hotel = await _DbContext.Hotels.FirstOrDefaultAsync(x => x.id == hotelId);
                if (hotel == null) throw new Exception("khong tim that hotel");

                hotelImage.src = HandleFile.UploadSingleFile(img);
                hotelImage.Hotel = hotel;
                Console.WriteLine(redirectUrl);
                await _DbContext.HotelImages.AddAsync(hotelImage);
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
        public async Task<IActionResult> Update(Hotel hotel, string redirectUrl)
        {
            redirectUrl = redirectUrl ?? "/admin/hotel";
            try
            {
                var hotelDb = await _DbContext.Hotels.FirstOrDefaultAsync(x => x.id == hotel.id);
                if (hotelDb == null) throw new Exception("not fount hotel will update");




                _DbContext.Entry(hotelDb).CurrentValues.SetValues(hotel);
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