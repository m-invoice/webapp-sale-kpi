var builder = WebApplication.CreateBuilder(args);

var corsPolicy = "AllowUi";
builder.Services.AddCors(o =>
{
    o.AddPolicy(corsPolicy, p => p
        .WithOrigins("http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// chạy http thì comment để khỏi redirect https
// app.UseHttpsRedirection();

app.UseCors(corsPolicy);

app.MapControllers();
app.Run();
