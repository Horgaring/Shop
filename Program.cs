using Microsoft.EntityFrameworkCore;


using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.HttpOverrides;
using System.Text.Json.Serialization;
using Serilog;
using Serilog.Events;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
try{
    Log.Information("Starting web host");
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "webapp",
            ValidateAudience = true,
            ValidAudience = "reactapp",
            RequireExpirationTime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KEYANYgfjfuthflmqnvbxw")),
            ValidateIssuerSigningKey = true,
        };
        options.Events = new JwtBearerEvents
      {
          OnMessageReceived = context =>
          {
              string? accessToken = context.Request.Query["access_token"];

              var path = context.HttpContext.Request.Path;
              if (!string.IsNullOrEmpty(accessToken) &&
                  (path.StartsWithSegments("/chat")))
              {
                  context.Token = accessToken;
              }
              return Task.CompletedTask;
          }
      };
});

builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSignalR(op => {
    if(builder.Environment.IsDevelopment()){
        op.EnableDetailedErrors = true;
    }
});
builder.Services.AddCors();
builder.Host.UseSerilog((cbx,lc) =>lc
    .WriteTo.File("./Logs/log.txt",LogEventLevel.Warning)
    .WriteTo.Console()
    );
builder.Services.AddScoped<IAccountService,AccountService>();
builder.Services.AddDbContext<dbcontextproduct>(op => op.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions
                .ReferenceHandler = ReferenceHandler.Preserve); 
var app = builder.Build();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
app.UseCors(op => op.AllowAnyOrigin().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers(); 
//app.UseHttpsRedirection();
app.MapHub<Habchat>("/chat");
app.UseSerilogRequestLogging();
Log.Logger.Information("|app run|");
app.Run();
}
catch (Exception ex){
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally{
    Log.CloseAndFlush();
}