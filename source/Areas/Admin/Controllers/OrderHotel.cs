using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
    public class OrderHotelController : Controller
    {
        private readonly TravelContext _DbContext;

        private readonly IToastNotification _toastNotification;


        public OrderHotelController(TravelContext dBContext, IToastNotification toastNotification)
        {
            _DbContext = dBContext;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index(int sort = 0, bool isConfirm = false, int pageIndex = 1, string search = "")
        {
             
            Console.WriteLine(search);
            var orders =    _DbContext.OrderHotels.Select(x => new OrderHotel(){
                IsConfirm = x.IsConfirm,
                id = x.id,
                name = x.name,
                email = x.email,
                phone = x.phone,
                createdAt = x.createdAt,
                Hotel = new Hotel(){
                    id = x.Hotel.id,
                    title = x.Hotel.title
                }
            }).Where(x => x.IsConfirm == isConfirm && (x.email.ToLower().Contains(search.ToLower()) || x.phone.ToLower().Contains(search.ToLower())));
            if(orders == null) throw new Exception("not found !!");
            var ordersPagi =  await PaginatedList<OrderHotel>.CreateAsync(orders,pageIndex,10);
            return View(ordersPagi);
        }


        [HttpGet]
         public async Task<IActionResult> Details(string id)
        {

            var orderTour = await _DbContext.OrderHotels.Include(x => x.Hotel).FirstOrDefaultAsync(x => x.id == id);
            return View(orderTour);
        }

      

        [HttpPost]
        public async Task<IActionResult> Confirm(string id, string redirectUrl)
        {
            redirectUrl = redirectUrl ?? "";
            try
            {
                var order = await _DbContext.OrderHotels.FirstOrDefaultAsync(x => x.id == id);
                if(order != null)
                    order.IsConfirm = !order.IsConfirm;
                await _DbContext.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("update success");
                return Redirect(redirectUrl);
            }
            catch (System.Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
                 return Redirect(redirectUrl);
            }
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

                HandleFile.DeleteFile(tour.mainImg);
                tour.TourImages.ForEach(x => HandleFile.DeleteFile(x.src));

                _DbContext.Tours.Remove(tour);
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

     

      
    }
}