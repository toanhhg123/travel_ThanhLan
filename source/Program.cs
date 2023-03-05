using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using source.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMvc().AddNToastNotifyToastr(new ToastrOptions()
{

    ProgressBar = false,
    PositionClass = ToastPositions.BottomRight
});
builder.Services.AddDbContext<TravelContext>(
    o => o.UseSqlServer(builder.Configuration.GetConnectionString("sql")));

builder.Services.AddAuthentication("TRAVEL")
    .AddCookie("TRAVEL", options =>
    {
        options.AccessDeniedPath = new PathString("/auth/Forbidden");
        options.ExpireTimeSpan = TimeSpan.FromMinutes(1200);
        options.LoginPath = new PathString("/auth/Login");
        options.SlidingExpiration = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}




app.UseNToastNotify();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
       {
           endpoints.MapControllerRoute(
              name: "areas",
              pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );
       });
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();



