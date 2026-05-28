using HCM.DataAccess.Data;
using HCM.DataAccess.Repository;
using HCM.DataAccess.Repository.IRepository;
using HCM.Models.Models;
using HCM.Services.Implementations;
using HCM.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ILedgerService, LedgerService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IComponentService, ComponentService>();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Admin}/{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    // Apply migrations automatically (optional, but recommended)

    // Seed initial data
    SeedData(context);
}

app.Run();





void SeedData(ApplicationDbContext context)
{
    if (!context.Accounts.Any())
    {
        context.Accounts.AddRange(
            new Account { AccountName = "Main Account", AccountType = "Cr" },
            new Account { AccountName = "Secondary Account", AccountType = "Cr" }
        );
    }

    if (!context.LedgerEntries.Any())
    {
        context.LedgerEntries.AddRange(
            new LedgerEntry { AccountId = 1, Credit = 1000.00m, Description = "Initial deposit", EntryDate = DateTime.Now },
            new LedgerEntry { AccountId = 1, Credit = -200.00m, Description = "Office supplies", EntryDate = DateTime.Now },
            new LedgerEntry { AccountId = 2, Debit = 500.00m, Description = "Client payment", EntryDate = DateTime.Now },
            new LedgerEntry { AccountId = 2, Debit = -150.00m, Description = "Software subscription", EntryDate = DateTime.Now }
        );
    }

    context.SaveChanges();
}
