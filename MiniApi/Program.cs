using Microsoft.AspNetCore.Mvc;
using MiniApi.CustomMiddleware;
using MiniApi.DI;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .SelectMany(x => x.Value.Errors.Select(e => new
            {
                Field = x.Key,
                Message = e.ErrorMessage
            }))
            .ToList();

        return new BadRequestObjectResult(errors);
    };
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServicesRegistration();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<GlobalExceptionHandler>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
