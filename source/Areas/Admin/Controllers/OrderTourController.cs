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
    [Authorize]

    // [Authorize(Roles = "ADMIN")]
    public class OrderTourController : Controller
    {
        private readonly TravelContext _DbContext;

        private readonly IToastNotification _toastNotification;


        public OrderTourController(TravelContext dBContext, IToastNotification toastNotification)
        {
            _DbContext = dBContext;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index( bool isConfirm ,int sort = 0, int pageIndex = 1, string search = "")
        {
             
            Console.WriteLine(search);
            var tours =    _DbContext.OrderTours.Select(x => new OrderTour(){
                IsConfirm = x.IsConfirm,
                id = x.id,
                name = x.name,
                email = x.email,
                phone = x.phone,
                createdAt = x.createdAt,
                Tour = new Tour(){
                    id = x.Tour.id,
                    title = x.Tour.title
                }
            }).Where(x => (x.email.ToLower().Contains(search.ToLower()) || x.phone.ToLower().Contains(search.ToLower())));
            if(tours == null) throw new Exception("not found !!");

            if(isConfirm){
                tours = tours.Where(x => x.IsConfirm == isConfirm);
            }
            var orderTours =  await PaginatedList<OrderTour>.CreateAsync(tours,pageIndex,10);
            return View(orderTours);
        }


        [HttpGet]
         public async Task<IActionResult> Details(string id)
        {

            var orderTour = await _DbContext.OrderTours.Include(x => x.Tour).FirstOrDefaultAsync(x => x.id == id);
            return View(orderTour);
        }

      

        [HttpPost]
        public async Task<IActionResult> Confirm(string id, string redirectUrl)
        {
            redirectUrl = redirectUrl ?? "";
            try
            {
                var orderTour = await _DbContext.OrderTours.FirstOrDefaultAsync(x => x.id == id);
                if(orderTour != null)
                    orderTour.IsConfirm = !orderTour.IsConfirm;
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
                var order = await _DbContext.OrderTours.FirstOrDefaultAsync(x => x.id == id);
                if(order == null) throw new Exception("Khong tim thay tour can xoa");
                _DbContext.OrderTours.Remove(order);

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