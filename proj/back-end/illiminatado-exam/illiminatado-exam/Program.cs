using illiminatado_exam.Data;
using illiminatado_exam.Data.Seed;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


//ARMAND ADDED : Database Connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//ARMAND ADDED : API
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", p =>
        p.AllowAnyOrigin()
         .AllowAnyHeader()
         .AllowAnyMethod());
});

var app = builder.Build();
//ARMAND ADDED : SEEDER
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbSeeder.Seed(context);
}

//ARMAND ADDED : Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowAll");
app.Run();
