using Base.OpenIddict.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration, builder.Environment);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("BaseCorsPolicy");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(options =>
{
    options.MapControllers();
    options.MapDefaultControllerRoute();
});

app.UseWelcomePage();

app.Run();
