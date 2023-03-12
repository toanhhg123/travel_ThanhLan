using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using source.Models;

namespace source.Controllers;

public class HomeController : Controller
{
    private readonly TravelContext _Dbcontext;
    private readonly IToastNotification _toastNotification;

    public HomeController(TravelContext context, IToastNotification toastNotification)
    {
        _Dbcontext = context;
        _toastNotification = toastNotification;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.CategoryTours = await _Dbcontext.CategoryTours.ToListAsync();
        ViewBag.tours = await _Dbcontext.Tours.Select(x => new Tour
        {
            id = x.id,
            time = x.time,
            title = x.title,
            location = x.location,
            mainImg = x.mainImg,
            price = x.price,
        }).Take(5).ToListAsync();

        ViewBag.hotels = await _Dbcontext.Hotels.Select(x => new Hotel
        {
            id = x.id,
            time = x.time,
            title = x.title,
            location = x.location,
            mainImg = x.mainImg,
            price = x.price,
        }).Take(5).ToListAsync();

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
