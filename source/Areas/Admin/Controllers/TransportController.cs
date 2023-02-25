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
    // [Authorize(Roles = "ADMIN")]
    public class TransportController : Controller
    {
        private readonly TravelContext _DbContext;

        private readonly IToastNotification _toastNotification;


        public TransportController(TravelContext dBContext, IToastNotification toastNotification)
        {
            _DbContext = dBContext;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {

            var data = await _DbContext.Transports.Select(x => new Transport
            {
                id = x.id,
                time = x.time,
                title = x.title,
                location = x.location,
                mainImg = x.mainImg,
                price = x.price,
                openTime = x.openTime
            }).ToListAsync();

            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> Create([Bind] Transport transport, IFormFile mainImg)
        {
            try
            {
                transport.mainImg = HandleFile.UploadSingleFile(mainImg);
                await _DbContext.Transports.AddAsync(transport);
                await _DbContext.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Success");
                return RedirectToAction("index");
            }
            catch (System.Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
                return View(transport);
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
                var data = await _DbContext.Transports.Include(x => x.TransportImages).FirstOrDefaultAsync(x => x.id == id);
                if (data == null)
                    throw new Exception("not found hotel");
                return View(data);
            }
            catch (System.Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImg(TransportImage img, string redirectUrl)
        {
            try
            {

                _DbContext.TransportImages.Remove(img);
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
        public async Task<IActionResult> AddImg(string transportId, string redirectUrl, TransportImage transportImage, IFormFile img)
        {

            try
            {
                if (transportId == null || redirectUrl == null || transportImage == null || img == null)
                    throw new Exception("du lieu khong hop le");
                var transport = await _DbContext.Transports.FirstOrDefaultAsync(x => x.id == transportId);
                if (transport == null) throw new Exception("khong tim that hotel");

                transportImage.src = HandleFile.UploadSingleFile(img);
                transportImage.Transport = transport;
                Console.WriteLine(redirectUrl);
                await _DbContext.TransportImages.AddAsync(transportImage);
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
        public async Task<IActionResult> Update(Transport transport, string redirectUrl)
        {
            redirectUrl = redirectUrl ?? "/admin/hotel";
            try
            {
                var transportDb = await _DbContext.Transports.FirstOrDefaultAsync(x => x.id == transport.id);
                if (transportDb == null) throw new Exception("not fount hotel will update");




                _DbContext.Entry(transportDb).CurrentValues.SetValues(transport);
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