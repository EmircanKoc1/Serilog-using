namespace Serilog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.Seq(builder.Configuration.GetConnectionString("Seq"))
                .WriteTo.File("log.txt")
                .MinimumLevel.Information()
                .Enrich.WithProperty("AppName","Serilog.API")
                .CreateLogger();

            builder.Logging.AddSerilog();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
