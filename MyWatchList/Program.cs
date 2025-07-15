// Removed the incorrect using directive for Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore
// and replaced it with the correct namespace for the DatabaseDeveloperExceptionFilter.

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics;

using MyWatchList.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddDatabaseDeveloperExceptionFilter();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    //app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();