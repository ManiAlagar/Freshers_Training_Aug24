using Microsoft.EntityFrameworkCore;
using Student_CRUD_API.Entity.DBContext;
using Student_CRUD_API.Repository.Implementation;
using Student_CRUD_API.Repository.Interface;
using Student_CRUD_API.Services.Implementation;
using Student_CRUD_API.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IStudentService,StudentService>();
builder.Services.AddTransient<IStudentRepostiory,StudentRepostiory>();

var connectionString = builder.Configuration.GetConnectionString("SQLConnection");
builder.Services.AddDbContext<StudentContext>(x => x.UseSqlServer(connectionString));


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
