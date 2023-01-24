var builder = WebApplication.CreateBuilder(args);

/* Install Services using the builder.Services methods
 */

//Enable MVC and DIJ Services for this application
builder.Services.AddMvc();
builder.Services.AddTransient<LuckySpin.Services.TextTransform>();
builder.Services.AddSingleton<LuckySpin.Services.Repository>();


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


