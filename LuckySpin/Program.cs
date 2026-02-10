using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

/* Install Services using the builder.Services methods
 */

//Enable MVC and DIJ Services for this application
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<LuckySpin.Services.TextTransform>();
//TODO: After switching to the database, change this service to AddScoped<LluckySpin.Services.Repository>() 
builder.Services.AddSingleton<LuckySpin.Services.Repository>();
//TODO: Register the Database service

var app = builder.Build();


/* Middleware in the HTTP Request Pipeline
 */
app.UseStaticFiles();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Spinner/Error");
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/",
    defaults: new
    {
        controller = "Spinner",
        action = "Index"
    });

app.Run();


