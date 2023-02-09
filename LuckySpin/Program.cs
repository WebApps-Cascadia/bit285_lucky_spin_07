﻿using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Enable MVC and DIJ Services for this application
builder.Services.AddMvc();
builder.Services.AddTransient<LuckySpin.Services.TextTransform>();
//TODO: Initialize the connection and DBC options for your particular OS
var connection = builder.Configuration.GetConnectionString("LuckySpinDbMac");
builder.Services.AddDbContext<LuckySpin.Models.LuckySpinDbc>( options =>
    options.UseSqlite(connection));

var app = builder.Build();


/* Middleware in the HTTP Request Pipeline
 */
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id}",
    defaults: new
    {
        controller = "Spinner",
        action = "Index",
        id = 0
    });

app.Run();