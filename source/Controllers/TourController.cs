using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using source.Models;
using NToastNotify;

namespace source.Controllers;

public class TourController : Controller
{
    private readonly ILogger<TourController> _logger;
    private readonly TravelContext _Dbcontext;
    private readonly IToastNotification _toastNotification;


    public TourController(ILogger<TourController> logger, TravelContext dbContext, IToastNotification toastNotification)
    {
        _Dbcontext = dbContext;
        _logger = logger;
        _toastNotification = toastNotification;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string search, string category)
    {
        try
        {

            var tours =  _Dbcontext.Tours.Select(x => new Tour
            {
                id = x.id,
                time = x.time,
                title = x.title,
                location = x.location,
                mainImg = x.mainImg,
                price = x.price,
                categoryTour = x.categoryTour
            });

            if(category != null)
                tours = tours.Where(x => x.categoryTour.name.ToLower().Contains(category.ToLower()));

            if (search != null)
                tours = tours.Where(x => x.title.ToLower().Contains(search.ToLower()));

            ViewBag.categoryTours = await _Dbcontext.CategoryTours.ToListAsync();
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


    [HttpPost]
    public async Task<IActionResult> OrderTour(OrderTour orderTour,
        string redirectUrl,
        string tourId)
    {
        redirectUrl = redirectUrl ?? "/tour";
        try
        {
            var tour = await _Dbcontext.Tours.FirstOrDefaultAsync(x => x.id == tourId);
            if (tour == null) throw new Exception("Không tìm thấy tour Du lịch cần đặt");

            orderTour.Tour = tour;
            await _Dbcontext.OrderTours.AddAsync(orderTour);
            await _Dbcontext.SaveChangesAsync();
            _toastNotification.AddSuccessToastMessage("dat tour thanh cong !!!");
            return Redirect(redirectUrl);
        }
        catch (System.Exception)
        {
            _toastNotification.AddErrorToastMessage("đặt tour không thành công vui lòng nhập đầy đủ thông tin");
            return Redirect(redirectUrl);
        }
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
