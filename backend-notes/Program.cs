using backend_notes.Models;
using Data.Models;
using Microsoft.EntityFrameworkCore;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

//CORS


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000", "http://localhost:3001", "http://localhost:3000/Notes")
                            .AllowAnyHeader()  // Permite todos los encabezados
                            .AllowAnyMethod();  // Permite todos los métodos HTTP
                      });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DBContext
builder.Services.AddDbContext<notesContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("Connection")));

//Service Layer

builder.Services.AddScoped<Services.INoteService,Services.NoteService>();
builder.Services.AddScoped<Services.ITagService, Services.TagService>();
builder.Services.AddScoped<Services.IAccountService, Services.AccountService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
