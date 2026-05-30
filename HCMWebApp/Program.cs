
using HCM.DataAccess.Data;
using HCM.DataAccess.Repository;
using HCM.DataAccess.Repository.IRepository;
using HCM.Models.Models;
using HCM.Services.Implementations;
using HCM.Services.Interfaces;
using HCM.Services.PayrollClient.Implementations;
using HCM.Services.PayrollClient.Interfaces;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// keep UnitOfWork & other services for parts still in the monolith
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ILedgerService, LedgerService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

// Register HttpClient for Payroll.Api (configure Services:PayrollApi in appsettings or env)
builder.Services.AddHttpClient<IPayrollClient, PayrollClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:PayrollApi"] ?? "https://localhost:7224/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient<IComponentService, ComponentServiceRemote>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:PayrollApi"] ?? "https://localhost:7224/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient<IEmployeeClient, EmployeeClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:PayrollApi"] ?? "https://localhost:7224/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});
builder.Services.AddHttpClient<ICandidateClient, CandidateClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:PayrollApi"] ?? "https://localhost:7224/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});
builder.Services.AddHttpClient<IJobsClient, JobsClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:PayrollApi"] ?? "https://localhost:7224/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient<ILedgerClient, LedgerClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:PayrollApi"] ?? "https://localhost:7224/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient<IAccountClient, AccountClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:PayrollApi"] ?? "https://localhost:7224/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient<IRequisiteClient, RequisiteClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:PayrollApi"] ?? "https://localhost:7224/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});


// Register remote adapter as the IComponentService implementation
builder.Services.AddScoped<IComponentService, ComponentServiceRemote>();

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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