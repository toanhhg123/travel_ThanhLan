using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using source.Models;
using NToastNotify;

namespace source.Controllers;

public class TransportController : Controller
{
    private readonly ILogger<TransportController> _logger;
    private readonly TravelContext _Dbcontext;
    private readonly IToastNotification _toastNotification;


    public TransportController(ILogger<TransportController> logger, TravelContext dbContext, IToastNotification toastNotification)
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
            var data = await _Dbcontext.Transports.Select(x => new Transport
            {
                id = x.id,
                time = x.time,
                title = x.title,
                location = x.location,
                mainImg = x.mainImg,
                price = x.price
            }).ToListAsync();

            return View(data);
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
            var transport = await _Dbcontext.Transports.Include(x => x.TransportImages).FirstOrDefaultAsync(x => x.id == id);
            if (transport == null) throw new Exception("not found transport");
            return View(transport);
        }
        catch (System.Exception ex)
        {
            return Ok(ex.Message);
        }
    }


     [HttpPost]
    public async Task<IActionResult> Order(OrderTransport order,
         string redirectUrl,
         string id)
    {
        redirectUrl = redirectUrl ?? "/transport";
        try
        {
            var transport = await _Dbcontext.Transports.FirstOrDefaultAsync(x => x.id == id);
            if(transport == null) throw new Exception("Không tìm thấy hotel Du lịch cần đặt");
           
            order.Transport = transport;
            await _Dbcontext.OrderTransports.AddAsync(order);
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
