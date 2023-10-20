using Asp.Versioning;
using Asp.Versioning.Conventions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApiVersioning(opts =>
{
    opts.ReportApiVersions = true;
    opts.ApiVersionReader = new UrlSegmentApiVersionReader();
    opts.DefaultApiVersion = new ApiVersion(1);
    opts.AssumeDefaultVersionWhenUnspecified = true;
})
    .AddMvc(opts =>
    {
        opts.Conventions.Add(new VersionByNamespaceConvention());
        //opts.Conventions.Controller<api.Controllers.v1.TrySomethingController>().HasApiVersion(new Asp.Versioning.ApiVersion(1));
        //opts.Conventions.Controller<api.Controllers.v2.TrySomethingController>().HasApiVersion(new Asp.Versioning.ApiVersion(2));
        //opts.Conventions.Controller<api.Controllers.WeatherForecastController>().HasApiVersion(new Asp.Versioning.ApiVersion(1));
    })
    .AddApiExplorer(opts => 
    {
        opts.GroupNameFormat = "'v'V"; // using "'v'VVV" formart means “‘v’major[.minor][-status]”
        opts.SubstituteApiVersionInUrl = true;
    });


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
    opts.SwaggerDoc("v1", new OpenApiInfo() { Title = "API v1", Version = "v1" });
    opts.SwaggerDoc("v2", new OpenApiInfo() { Title = "API v2", Version = "v2" });
    opts.ResolveConflictingActions(apiDesc => apiDesc.First());
    opts.CustomSchemaIds(x => x.FullName);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opts =>
    {
        opts.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        opts.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
