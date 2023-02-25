using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using source.Models;
using NToastNotify;

namespace source.Controllers;

public class HotelController : Controller
{
    private readonly ILogger<HotelController> _logger;
    private readonly TravelContext _Dbcontext;
    private readonly IToastNotification _toastNotification;


    public HotelController(ILogger<HotelController> logger, TravelContext dbContext, IToastNotification toastNotification)
    {
        _Dbcontext = dbContext;
        _logger = logger;
        _toastNotification = toastNotification;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var hotels = await _Dbcontext.Hotels.Select(x => new Hotel
            {
                id = x.id,
                time = x.time,
                title = x.title,
                location = x.location,
                mainImg = x.mainImg,
                price = x.price
            }).ToListAsync();

            return View(hotels);
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
            var hotel = await _Dbcontext.Hotels.Include(x => x.HotelImgs).FirstOrDefaultAsync(x => x.id == id);
            if (hotel == null) throw new Exception("not found hotel");
            return View(hotel);
        }
        catch (System.Exception ex)
        {
            return Ok(ex.Message);
        }
    }


     [HttpPost]
    public async Task<IActionResult> Order(OrderHotel orderHotel,
         string redirectUrl,
         string id)
    {
        redirectUrl = redirectUrl ?? "/hotel";
        try
        {
            var hotel = await _Dbcontext.Hotels.FirstOrDefaultAsync(x => x.id == id);
            if(hotel == null) throw new Exception("Không tìm thấy hotel Du lịch cần đặt");
           
            orderHotel.Hotel = hotel;
            await _Dbcontext.OrderHotels.AddAsync(orderHotel);
            await _Dbcontext.SaveChangesAsync();
            _toastNotification.AddSuccessToastMessage("dat hotel thanh cong !!!");
            return Redirect(redirectUrl);
        }
        catch (System.Exception)
        {
            _toastNotification.AddErrorToastMessage("đặt hotel không thành công vui lòng nhập đầy đủ thông tin");
            return Redirect(redirectUrl);
        }
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
