using FaustWeb;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(Program));

builder.ConfigureDbConnection();
builder.Services.RegisterServices();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureAuth();
builder.ConfigureEmail();
builder.Services.ConfigureSwagger();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    c.RoutePrefix = "Swagger";
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.CreateDefaultIdentityAsync();

await app.RunAsync();
