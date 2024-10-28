var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger/OpenAPI desteðini ekleyin
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Uygulamayý oluþturun
var app = builder.Build();

// Geliþtirme ortamýnda Swagger'ý etkinleþtirin
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTP yönlendirmesi için HTTPS'yi zorunlu hale getirin
app.UseHttpsRedirection();

// Yetkilendirme yapýlandýrmasý (Authentication yapýlmadýðý için þu an basit yetkilendirme ekledik)
app.UseAuthorization();

// Statik dosyalarý sunun (wwwroot klasörünü kullanmak istiyorsanýz)
app.UseStaticFiles();

// Controller'larý route edin
app.MapControllers();

// Uygulamayý baþlatýn
app.Run();
