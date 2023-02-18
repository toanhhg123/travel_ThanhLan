using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using source.Models;

namespace source.Controllers;

public class TourController : Controller
{
    private readonly ILogger<TourController> _logger;

    public TourController(ILogger<TourController> logger)
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
