using BLL__Buisness_Logic_Layer_.Services.AuthorService;
using BLL__Buisness_Logic_Layer_.Services.BookServices;
using BLL__Buisness_Logic_Layer_.Services.CategoryServices;
using BLL__Buisness_Logic_Layer_.Services.IssueRecordService;
using DAL__Data_Access_Layer_.AppContext;
using DAL__Data_Access_Layer_.Models;
using DAL__Data_Access_Layer_.Repo.GenericRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(option =>
option.UseSqlServer(builder
						.Configuration
						.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<AppUser, IdentityRole<int>>(options =>
												options.Password.RequireDigit=true)
    .AddEntityFrameworkStores<ApplicationDbContext>();



builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IIssueRecordService, IssueRecordService>();


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

app.UseAuthentication();//Handle read of cookie 
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
