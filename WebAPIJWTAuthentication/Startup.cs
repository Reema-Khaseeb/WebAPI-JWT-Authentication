using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPIJWTAuthentication;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    
    // Add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        // Add configuration
        services.AddControllers();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                // Retrieve the secret key from configuration
                var secretKey = Configuration["Authentication:SecretKey"];

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });

        services.AddAuthorization();
        services.AddSingleton<JwtTokenGenerator>();
    }

    // Configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app)
    {
        // Enable authentication
        app.UseAuthentication();

        // Add routing
        app.UseRouting();

        // Add authorization
        app.UseAuthorization();

        // Add the Controller to the API
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
