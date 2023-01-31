using VerifyDeposits.Extensions;

var builder = WebApplication.CreateBuilder(args);

//Services e Controllers
builder.AddRepositories();
builder.Services.AddControllers();

//Swagger
builder.AddSwagger();

//Database
builder.AddPersistence();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();