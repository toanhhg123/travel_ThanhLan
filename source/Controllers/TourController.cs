using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using source.Models;

namespace source.Controllers;

public class TourController : Controller
{
    private readonly ILogger<TourController> _logger;
    private readonly TravelContext _Dbcontext;


    public TourController(ILogger<TourController> logger, TravelContext dbContext)
    {
        _Dbcontext = dbContext;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var tours = await _Dbcontext.Tours.Select(x => new Tour
            {
                id = x.id,
                time = x.time,
                title = x.title,
                location = x.location,
                mainImg = x.mainImg
            }).ToListAsync();
            return View(tours);
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        try
        {
            var tour = await _Dbcontext.Tours.Include(x => x.TourImages).FirstOrDefaultAsync(x => x.id == id);
            if (tour == null) throw new Exception("not found tour");
            return View(tour);
        }
        catch (System.Exception ex)
        {
            return Ok(ex.Message);
        }
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
