using Chat.Application.Infrastructure;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        byte[] secret = Convert.FromBase64String(builder.Configuration["JwtSecret"]);
        builder.Services
            .AddAuthentication(options => options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            //options.JsonSerializerOptions.WriteIndented = true;
        });

        builder.Services.AddControllers();
        builder.Services.AddDbContext<ChatContext>(opt =>
        {
            opt.UseSqlServer(
                builder.Configuration.GetConnectionString("SqlServer"),
                o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery));
        });

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });
        }

        var app = builder.Build();
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(builder.Configuration["SyncfusionKey"]);
        if (app.Environment.IsDevelopment())
        {
            using (var scope = app.Services.CreateScope())
            {
                using (var db = scope.ServiceProvider.GetRequiredService<ChatContext>())
                {
                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();
                    db.Seed();
                }
            }
            app.UseCors();
        }

        app.MapControllers();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseStaticFiles();
        app.MapFallbackToFile("index.html");
        app.Run();
    }
}