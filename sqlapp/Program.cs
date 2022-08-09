using sqlapp.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Endpoint=https://azureappconfig2022w.azconfig.io;Id=Dln0-l8-s0:LPXkEJe/yUKdZ6C5s2sT;Secret=3PzPQPjSDgymYObRFUvmPVey7TzVfHt4Zbqtc+dvszU=";
builder.Host.ConfigureAppConfiguration(builder =>
{
    builder.AddAzureAppConfiguration(connectionString);
});

builder.Services.AddTransient<IProductService, ProductService>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
