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
    public class OrderTransportController : Controller
    {
        private readonly TravelContext _DbContext;

        private readonly IToastNotification _toastNotification;


        public OrderTransportController(TravelContext dBContext, IToastNotification toastNotification)
        {
            _DbContext = dBContext;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index( bool isConfirm, int sort = 0, int pageIndex = 1, string search = "")
        {

            var orders = _DbContext.OrderTransports.Select(x => new OrderTransport()
            {
                IsConfirm = x.IsConfirm,
                id = x.id,
                name = x.name,
                email = x.email,
                phone = x.phone,
                createdAt = x.createdAt,
                Transport = new Transport()
                {
                    id = x.Transport.id,
                    title = x.Transport.title
                }
            }).Where(x =>  (x.email.ToLower().Contains(search.ToLower()) || x.phone.ToLower().Contains(search.ToLower())));
            if(isConfirm) orders = orders.Where(x => x.IsConfirm == isConfirm);
            if (orders == null) throw new Exception("not found !!");
            var ordersPagi = await PaginatedList<OrderTransport>.CreateAsync(orders, pageIndex, 10);
            return View(ordersPagi);
        }


        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {

            var orderTour = await _DbContext.OrderTransports.Include(x => x.Transport).FirstOrDefaultAsync(x => x.id == id);
            return View(orderTour);
        }



        [HttpPost]
        public async Task<IActionResult> Confirm(string id, string redirectUrl)
        {
            redirectUrl = redirectUrl ?? "";
            try
            {
                var order = await _DbContext.OrderTransports.FirstOrDefaultAsync(x => x.id == id);
                if (order != null)
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

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                 var order = await _DbContext.OrderTransports.FirstOrDefaultAsync(x => x.id == id);
                if(order == null) throw new Exception("Khong tim thay truong can xoa");
                _DbContext.OrderTransports.Remove(order);

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