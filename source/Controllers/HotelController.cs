using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using source.Models;

namespace source.Controllers;

public class HotelController : Controller
{
    private readonly ILogger<HotelController> _logger;

    public HotelController(ILogger<HotelController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

   

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
