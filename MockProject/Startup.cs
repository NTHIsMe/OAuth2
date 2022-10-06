using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MockProjectta;

namespace MockProject
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders().AddDefaultUI();
			services.Configure<IdentityOptions>(opt =>
			{
				opt.Password.RequiredLength = 5;
				opt.Password.RequireLowercase = true;
				opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
				opt.Lockout.MaxFailedAccessAttempts = 5;

			});
			services.AddAuthentication()
				.AddFacebook(options =>
				{
					options.ClientId = "583855119888050";
					options.ClientSecret = "2e0e50fd71adaf19b0413dbf42176844";
				})
				.AddGoogle(options =>
				{
					options.ClientId = "1086407282498-prs9qn4t9lpcvisvjit8i44emhn5nfhg.apps.googleusercontent.com";
					options.ClientSecret = "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";
				})
				.AddMicrosoftAccount(options =>
				{
					options.ClientSecret = "f548Q~9OHPKUdY6Fe6m_vG7aTNSwdXEgqKj2FauG";
					options.ClientId = "4dc159f0-0538-4c0d-8415-a3db3eb0656a";
				});
			services.AddAuthorization(options =>
			{
				options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
				options.AddPolicy("UserAndAdmin", policy => policy.RequireRole("Admin").RequireRole("User"));
			});


			services.AddControllersWithViews();
			services.AddRazorPages();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
				endpoints.MapRazorPages();
			});
		}
	}
}
