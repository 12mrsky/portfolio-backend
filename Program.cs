using Backend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// =============================
// 🔹 SERVICES
// =============================

// Controllers
builder.Services.AddControllers();

// PostgreSQL (Neon DB)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Swagger (API testing)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});


// =============================
// 🔹 BUILD APP
// =============================
var app = builder.Build();


// =============================
// 🔹 MIDDLEWARE PIPELINE
// =============================

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

// HTTPS
app.UseHttpsRedirection();

// ✅ SERVE ANGULAR FILES (VERY IMPORTANT 🔥)
app.UseDefaultFiles();   // loads index.html
app.UseStaticFiles();   // serves wwwroot

// CORS
app.UseCors("AllowAll");

// Authorization
app.UseAuthorization();

// Controllers (API)
app.MapControllers();

// ✅ SPA FALLBACK (VERY IMPORTANT 🔥)
app.MapFallbackToFile("index.html");


// =============================
// 🔹 RUN APP
// =============================
app.Run();