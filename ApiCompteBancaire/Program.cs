using ApiCompteBancaire.Data;
using DAL_Client_SQL_Server;
using DAL_Compte_Bancaire_SQL_Server;
using Entity_Compte_Bancaire;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApiCompteBancaire
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();
            
            builder.Services.AddScoped<ManipulationCompteBancaire, ManipulationCompteBancaire>();
            builder.Services.AddScoped<ManipulationTransaction, ManipulationTransaction>();
            builder.Services.AddScoped<IDepotCompteBancaire, DepotCompteBancaireSQL>();
            builder.Services.AddScoped<IDepotTransaction, DepotTransactionSQL>();

            builder.Services.AddSwaggerDocument();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
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

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "Default",
                pattern: "{controller=Home}/{action=Index}/{id?}/");
            
            app.MapRazorPages();

            app.Run();
        }
    }
}