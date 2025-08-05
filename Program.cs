using NotificationBackend.Hubs;
using NotificationBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000") // Replace with your frontend's URL
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); // Required for SignalR
    });
});

// Add services to the container
builder.Services.AddSingleton<NotificationService>();
builder.Services.AddSignalR(); // Add SignalR
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowAll"); // Apply the CORS policy

app.UseAuthorization();

app.MapControllers();
app.MapHub<NotificationHub>("/notificationHub"); // Map the SignalR hub

app.Run();