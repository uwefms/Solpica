using FileServer.Exception;

using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;

namespace FileServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

			// important for file handling and upload and download of large files
			builder.Services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
           

            builder.Services.AddHttpClient();

            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseExceptionHandler(opt => { });

            app.UseHttpsRedirection();

			// Custom middleware for API key authentication. API keys can be used in Query string or in header
			app.UseMiddleware<CustomApiKeyMiddleware>();

            app.UseAuthorization();
                       
            app.MapControllers();

            /*
            	app.UseStaticFiles(); serves static files from the default wwwroot directory.
            	app.UseStaticFiles(new StaticFileOptions() { ... }); serves static files from the specified custom directory (Resources).
                If you remove app.UseStaticFiles();, you will no longer serve static files from the wwwroot directory, which might be necessary for your application.
            */

            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.Run();

		} // end of Main

	} // end of Program

} // end of namespace FileServer
