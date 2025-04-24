using JobApplicationSystem.Extensions;
using Service.MapperProfiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers((opt) => opt.Filters.Add<ExceptionFilter>());
builder.Services.ConfigCors();
builder.Services.ConfigDbContext();
builder.Services.AddSwaggerWithAuthentication("Job Application System", "v1.0");
builder.Services.BusinessRepositories();
builder.Services.BusinessServices();

builder.Services.AddAutoMapper(typeof(MapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
