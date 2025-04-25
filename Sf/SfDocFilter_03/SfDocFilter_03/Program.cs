using SfDocFilter.Configuration;

using SfDocFilter_03.Components;

using Syncfusion.Blazor;
using Syncfusion.Blazor.Popups;

namespace SfDocFilter_03
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Resolve the wwwroot path
            var wwwRootPath = builder.Environment.WebRootPath;
            
            // Replace the placeholder in appsettings.json
            builder.Configuration["ConnectionStrings:SqlContextSqlite"] =
                builder.Configuration["ConnectionStrings:SqlContextSqlite"]!
                    .Replace("{WwwRoot}", wwwRootPath);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

			// Add Syncfusion Blazor service
			builder.Services.AddSyncfusionBlazor();

			// Add Syncfusion Blazor service 29.1.36
			builder.Services.AddScoped<SfDialogService>();

			//---------------------------------------------------------------------------------------------------------------------		
			// builder.Services.AddDalMock();
			// builder.Services.AddDalSql();			
			builder.Services.AddDalSqlite();
			//---------------------------------------------------------------------------------------------------------------------

            var app = builder.Build();

            // Add Syncfusion Blazor service 29.1.36
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("");

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
