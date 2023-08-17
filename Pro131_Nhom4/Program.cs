
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Microsoft.IdentityModel.Tokens;
using Pro131_Nhom4.Data;
using Pro131_Nhom4.IService;
using Pro131_Nhom4.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IBillService,BillServices>();
builder.Services.AddScoped<IUserService, UserServices>();
builder.Services.AddScoped<IColorService, ColorServices>();
builder.Services.AddScoped<IRankService, RankService>();
builder.Services.AddScoped<IBillDetailsService, BillDetailsServices>();
builder.Services.AddScoped<IVoucherService, VoucherServices>();
builder.Services.AddScoped<IBillStatusServices, BillStatusServices>();
builder.Services.AddScoped<ICartDetailsService, CartDetailsService>();
builder.Services.AddScoped<IRegisterService, RegisterServices>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ILoginService, LoginServices>();
builder.Services.AddScoped<ISizeService, SizeService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICRUDFavoriteProductService, CRUDFavoriteProductService>();
builder.Services.AddCors(c => c.AddPolicy("corspocy",
	build => build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader())
);
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<Mydb>()/*.AddDefaultTokenProviders()*/; 
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
    };
});

//builder.Services.AddDbContext<Mydb>(options =>
//    options.UseSqlServer("MyCS"));    
// Add services to the container.
builder.Services.AddDbContext<Mydb>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyCS"));
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("corspocy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
