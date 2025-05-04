using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskManagementAPI.Services;
using FluentValidation;
using TaskManagementAPI.InputValidation.AuthControllerValidation;
using FluentValidation.AspNetCore;

namespace TaskManagementAPI.StartupConfig;

public static class DependencyInjectionExtentions
{
    public static void AddStandardServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        //builder.AddSwagerServices();
        builder.Services.AddSwaggerGen();
    }
    /*
     * public static void AddSwagerServices(this WebApplicationBuilder builder)
    {
        var securityScheme = new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Description = "JWT Authorization header info using bearer tokens",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT"
        };
        var securityRequiremet = new OpenApiSecurityRequirement
            {
               {
                  new OpenApiSecurityScheme
                  {
                      Reference=new OpenApiReference
                      {
                         Type=ReferenceType.SecurityScheme,
                         Id="bearerAuth"
                      }
                  },
                  new string[]{}
               }
            };
        builder.Services.AddSwaggerGen(opts =>
        {
            opts.AddSecurityDefinition(name: "bearerAuth", securityScheme);
            opts.AddSecurityRequirement(securityRequiremet);
        });
    }
     */
    public static void AddValidationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssemblyContaining<SignInDataValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<SignUpDataValidator>();
    }
    public static void AddCustomServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAuthService, SupabaseAuthService>();
    }
    public static void AddAuthServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization(opts =>
        opts.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()
        );

        builder.Services.AddAuthentication(defaultScheme: "Bearer").AddJwtBearer(opts => opts.TokenValidationParameters = new()
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration.GetValue<string>(key: "Authentication:Issuer"),
            ValidAudience = builder.Configuration.GetValue<string>(key: "Authentication:Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.
            GetBytes(builder.Configuration.GetValue<string>
            (key: "Authentication:SecretKey")!))
        });
    }
}
