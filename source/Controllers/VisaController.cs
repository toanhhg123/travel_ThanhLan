using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using source.Models;
using NToastNotify;

namespace source.Controllers;

public class VisaController : Controller
{
    private readonly ILogger<VisaController> _logger;
    private readonly TravelContext _Dbcontext;
    private readonly IToastNotification _toastNotification;
    public VisaController(ILogger<VisaController> logger, TravelContext dbContext, IToastNotification toastNotification)
    {
        _Dbcontext = dbContext;
        _logger = logger;
        _toastNotification = toastNotification;
    }


    [HttpGet]
    public async Task<IActionResult> Index(string search)
    {
        try
        {
            var visas =  _Dbcontext.Visas.Select(x => new Visa
            {
                id = x.id,
                title = x.title,
                mainImg = x.mainImg
            });
            if(search != null)
                visas = visas.Where(x => x.title.ToLower().Contains(search.ToLower()));
            var data = await visas.ToListAsync();
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
            var Visa = await _Dbcontext.Visas.FirstOrDefaultAsync(x => x.id == id);
            if (Visa == null) throw new Exception("not found visa");
            return View(Visa);
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
