var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger/OpenAPI deste�ini ekleyin
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Uygulamay� olu�turun
var app = builder.Build();

// Geli�tirme ortam�nda Swagger'� etkinle�tirin
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTP y�nlendirmesi i�in HTTPS'yi zorunlu hale getirin
app.UseHttpsRedirection();

// Yetkilendirme yap�land�rmas� (Authentication yap�lmad��� i�in �u an basit yetkilendirme ekledik)
app.UseAuthorization();

// Statik dosyalar� sunun (wwwroot klas�r�n� kullanmak istiyorsan�z)
app.UseStaticFiles();

// Controller'lar� route edin
app.MapControllers();

// Uygulamay� ba�lat�n
app.Run();
