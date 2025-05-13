using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskManagementAPI.Services;
using FluentValidation;
using TaskManagementAPI.InputValidation.AuthControllerValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Supabase;
using TaskManagementDataAccessLayer.UserData;
using TaskManagementBusinessLayer.Users;
using TaskManagementDataAccessLayer.CustomSupabaseClient;

namespace TaskManagementAPI.StartupConfig;

public static class DependencyInjectionExtentions
{
    public static void AddStandardServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.AddSwaggerServices();
    }
    public static void AddSwaggerServices(this WebApplicationBuilder builder)
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
    public static void AddValidationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssemblyContaining<SignInDataValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<SignUpDataValidator>();
    }
    public static void AddCustomServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ISupabaseClient, SupabaseClient>();
        builder.Services.AddScoped<ITokenAccessor, TokenAccessor>();
        builder.Services.AddHttpClient("SupabaseClient", Client =>
        {
            Client.BaseAddress = new Uri(builder.Configuration["Supabase:URL"]!);
        });
        builder.Services.AddScoped<Supabase.Client>(_ => new Supabase.Client(
            builder.Configuration["Supabase:URL"]!,
            builder.Configuration["Supabase:AnonKey"],
            new SupabaseOptions{AutoConnectRealtime = true}
            ));
        builder.Services.AddScoped<IAuthService, SupabaseAuthService>();
        builder.Services.AddScoped<IUserData, UserData>();
        builder.Services.AddScoped<IUser, User>();
    }
    public static void AddAuthServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization(opts =>
        opts.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()
        );

        builder.Services.AddAuthentication(defaultScheme: "Bearer").AddJwtBearer(opts => opts.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration.GetValue<string>(key: "Authentication:Issuer"),
            ValidAudience = builder.Configuration.GetValue<string>(key: "Authentication:Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.
            GetBytes(builder.Configuration.GetValue<string>
            (key: "Authentication:SecretKey")!))
        });
    }
}