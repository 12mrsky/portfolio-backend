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

// ✅ CORS (GLOBAL)
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

// ✅ CORS FIRST (IMPORTANT)
app.UseCors("AllowAll");

// Swagger (optional production)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS (Render pe optional)
app.UseHttpsRedirection();

// Static files (Angular build)
app.UseDefaultFiles();
app.UseStaticFiles();

// Authorization
app.UseAuthorization();

// Controllers (API)
app.MapControllers();

// SPA fallback (Angular routing)
app.MapFallbackToFile("index.html");


// =============================
// 🔹 RUN APP
// =============================
app.Run();