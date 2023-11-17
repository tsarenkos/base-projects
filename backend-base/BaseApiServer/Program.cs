using Base.ApiServer.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors("BaseCorsPolicy");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.OAuthClientId("swagger-client");
    c.OAuthClientSecret(builder.Configuration["OpenIddictSettings:SwaggerOAuthClientSecret"]);
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();