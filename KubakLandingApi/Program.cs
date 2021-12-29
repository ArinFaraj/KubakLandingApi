var builder = WebApplication.CreateBuilder(args);


var supabase = builder.Configuration.GetSection("Supabase");
var url = supabase.GetValue<string>("Url");
var key = supabase.GetValue<string>("Key");
var envKey = System.Environment.GetEnvironmentVariable("Key");
if(envKey != null && envKey != "")
{
    key = envKey;
}
await Supabase.Client.InitializeAsync(url, key);
// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
 
// Configure the HTTP request pipeline.
//  if (app.Environment.IsDevelopment())
//  {
    app.UseSwagger();
    app.UseSwaggerUI();
// }
app.UseAuthorization();

app.MapControllers();

app.Run();
