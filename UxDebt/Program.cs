using Microsoft.EntityFrameworkCore;
using UxDebt.Context;
using UxDebt.Services;
using UxDebt.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:3001") // Reemplaza con la URL de tu frontend
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<UxDebtContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext"))
);

builder.Services.AddHttpClient<IGitService, GitService>();
builder.Services.AddScoped(typeof(IGitService), typeof(GitService));
builder.Services.AddScoped(typeof(IIssueService), typeof(IssueService));
builder.Services.AddScoped(typeof(IRepositoryService), typeof(RepositoryService));
builder.Services.AddScoped(typeof(ITagService), typeof(TagService));

var app = builder.Build();


//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<UxDebtContext>();
//    context.Database.Migrate();
//}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
