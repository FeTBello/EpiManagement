using EpiManagement.Web.Services;

var builder = WebApplication.CreateBuilder(args);

//Container.
builder.Services.AddControllersWithViews();

//HttpClient
builder.Services.AddHttpClient<EpiApiService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5000/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

var app = builder.Build();

//HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Epis}/{action=Index}/{id?}");

app.Run("http://127.0.0.1:5001");

