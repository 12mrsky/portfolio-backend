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

// ✅ CORS FIRST
app.UseCors("AllowAll");

// Swagger (ENABLE ALWAYS for debugging)
app.UseSwagger();
app.UseSwaggerUI();

// HTTPS
app.UseHttpsRedirection();

// ✅ IMPORTANT: MAP API FIRST
app.MapControllers();

// ✅ THEN serve Angular files
app.UseDefaultFiles();
app.UseStaticFiles();

// Authorization
app.UseAuthorization();

// ✅ Angular fallback LAST (VERY IMPORTANT)
app.MapFallbackToFile("index.html");

// =============================
// 🔹 RUN APP
// =============================
app.Run();