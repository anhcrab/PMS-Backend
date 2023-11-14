using DataAccess;
using DataAccess.Entities;
using DataAccess.Repositories.DepartmentRepository;
using DataAccess.Repositories.EmployeeRepository;
using DataAccess.Repositories.RoleRepository;
using DataAccess.Repositories.UserRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using pms;
using pms.Services.Services.AuthService;
using System.Text;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions {
    ContentRootPath = Directory.GetCurrentDirectory(),
    WebRootPath = AppDomain.CurrentDomain.BaseDirectory + "terus-content"
});

// Add services to the container.

builder.Services.AddControllers().RegisterModules(builder.Environment);
builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
    opts.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PMS", Version = "v1"
    });

    opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please Enter a valid Token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    opts.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Connection Database
builder.Services
    .AddDbContext<MySqlDbContext>(
        opts => opts.UseMySQL(
            builder.Configuration.GetConnectionString("mysql"), 
            b => b.MigrationsAssembly("DataAccess")
        )
   );

// Identity Config
builder.Services
    .AddIdentity<User, Role>()
    .AddEntityFrameworkStores<MySqlDbContext>()
    .AddDefaultTokenProviders();

// Authentication Config
builder.Services.AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opts =>
{
    opts.SaveToken = true;
    opts.RequireHttpsMetadata = false;
    opts.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

// Register Services
//builder.Services.AddScoped<IRoleRepository, RoleRepositoryImpl>();
builder.Services.AddScoped<IUserRepository, UserRepositoryImpl>();
builder.Services.AddScoped<ISignUpService, SignUpServiceImpl>();
builder.Services.AddScoped<IRoleRepository, RoleRepositoryImpl>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepositoryImpl>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepositoryImpl>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();