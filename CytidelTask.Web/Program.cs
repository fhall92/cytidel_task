using CytidelTask.Data.Repositories;
using CytidelTask.TaskAPI.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<TaskRepository, TaskRepository>();
builder.Services.AddScoped<TaskLogger, TaskLogger>();
builder.Services.AddScoped<TasksController, TasksController>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

builder.Services.AddRazorPages();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapGet("/", async (HttpContext context) =>
{
	context.Response.Redirect("/Task/Tasks");
	await Task.CompletedTask;
});

app.Run();
