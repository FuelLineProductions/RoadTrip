using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using RoadTrip.Components;
using RoadTrip.Components.Account;
using RoadTrip.Data;
using RoadTrip.Hubs;
using RoadTrip.RoadTripDb.Database;
using RoadTrip.RoadTripDb.Database.Models;
using RoadTrip.RoadTripDb.Repos;
using RoadTrip.RoadTripServices.RoadTripServices.Services;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using IQuizRepo = RoadTrip.RoadTripDb.Repos.IQuizRepo;

using Microsoft.AspNetCore.Authentication.JwtBearer;
namespace RoadTrip
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<IdentityUserAccessor>();
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
            builder.Services.AddMudServices();

            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = IdentityConstants.ApplicationScheme;
                    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                })
                .AddJwtBearer(options =>
                {
                    // Configure the Authority to the expected value for
                    // the authentication provider. This ensures the token
                    // is appropriately validated.
                    options.Authority = "https://localhost:7138"; // TODO: Update URL (Make appsettings var)

                    // We have to hook the OnMessageReceived event in order to
                    // allow the JWT authentication handler to read the access
                    // token from the query string when a WebSocket or 
                    // Server-Sent Events request comes in.

                    // Sending the access token in the query string is required when using WebSockets or ServerSentEvents
                    // due to a limitation in Browser APIs. We restrict it to only calls to the
                    // SignalR hub in this code.
                    // See https://docs.microsoft.com/aspnet/core/signalr/security#access-token-logging
                    // for more information about security considerations when using
                    // the query string to transmit the access token.
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["QuizHubAccess"];

                            // If the request is for our hub...
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                (path.StartsWithSegments("/QuizHub")))
                            {
                                // Read the token out of the query string
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                })
                .AddIdentityCookies()
                ;

            //builder.Services.AddAuthorization(options =>
            //{
                
            //});

            var connectionString = builder.Configuration.GetConnectionString("RoadTripIdentityDb") ?? throw new InvalidOperationException("Connection string 'RoadTripIdentityDb' not found.");
            builder.Services.AddDbContext<RoadTripIdentityDbContext>(options =>
                options.UseSqlServer(connectionString), ServiceLifetime.Transient);
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentityCore<RoadTripUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<RoadTripIdentityDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            builder.Services.AddSingleton<IEmailSender<RoadTripUser>, IdentityNoOpEmailSender>();

            var dataDbConnectionString = builder.Configuration.GetConnectionString("RoadTripDb") ?? throw new InvalidOperationException("Connection string 'RoadTripDb' not found.");
            builder.Services.AddDbContext<RoadTripDbContext>(
                options => options.UseSqlServer(dataDbConnectionString), ServiceLifetime.Transient);

            builder.Services.AddTransient<IBaseRepo<HostAppUser>, BaseRepo<HostAppUser>>();
            builder.Services.AddTransient<IHostAppUserRepo, HostAppUserRepo>();

            builder.Services.AddTransient<IBaseRepo<GuestAppUser>, BaseRepo<GuestAppUser>>();
            builder.Services.AddTransient<IGuestAppUserRepo, GuestAppUserRepo>();

            builder.Services.AddTransient<IBaseRepo<SubscriptionTier>, BaseRepo<SubscriptionTier>>();
            builder.Services.AddTransient<ISubscriptionTierRepo, SubscriptionTierRepo>();

            builder.Services.AddTransient<IBaseRepo<IndividualSubscription>, BaseRepo<IndividualSubscription>>();
            builder.Services.AddTransient<IIndividualSubscriptionRepo, IndividualSubscriptionRepo>();

            builder.Services.AddTransient<IBaseRepo<GroupSubscription>, BaseRepo<GroupSubscription>>();
            builder.Services.AddTransient<IGroupSubscriptionRepo, GroupSubscriptionRepo>();

            builder.Services.AddTransient<IBaseRepo<Room>, BaseRepo<Room>>();
            builder.Services.AddTransient<IRoomRepo, RoomRepo>();

            builder.Services.AddTransient<IBaseRepo<FuelType>, BaseRepo<FuelType>>();
            builder.Services.AddTransient<IFuelTypeRepo, FuelTypeRepo>();

            builder.Services.AddTransient<IBaseRepo<Question>, BaseRepo<Question>>();
            builder.Services.AddTransient<IQuestionRepo, QuestionRepo>();

            builder.Services.AddTransient<IBaseRepo<Quiz>, BaseRepo<Quiz>>();
            builder.Services.AddTransient<IQuizRepo, QuizRepo>();

            builder.Services.AddTransient<IBaseRepo<Vehicle>, BaseRepo<Vehicle>>();
            builder.Services.AddTransient<IVehicleRepo, VehicleRepo>();

            builder.Services.AddTransient<IBaseRepo<ActiveQuizProgress>, BaseRepo<ActiveQuizProgress>>();
            builder.Services.AddTransient<IActiveQuizProgressRepo, ActiveQuizProgressRepo>();

            builder.Services.AddTransient<IBaseRepo<GuestRequestJoinQuiz>, BaseRepo<GuestRequestJoinQuiz>>();
            builder.Services.AddTransient<IGuestQuizJoinRepo, GuestQuizJoinRepo>();

            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IQuizService, QuizService>();

            builder.Services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                      new[] { "application/octet-stream" });
            });

            var logDB = builder.Configuration.GetConnectionString("LogDb");
            var sinkOpts = new MSSqlServerSinkOptions { TableName = "Logs" };
            var columnOpts = new ColumnOptions();

            var path = builder.Configuration.GetSection("LogFile").Value ?? $"{Directory.GetCurrentDirectory}{Path.DirectorySeparatorChar}logs{Path.DirectorySeparatorChar}Log-.txt";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(path, rollingInterval: RollingInterval.Day)
                .WriteTo.MSSqlServer
                (
                    connectionString: logDB,
                    sinkOptions: sinkOpts,
                    appConfiguration: builder.Configuration,
                    columnOptions: columnOpts
                )
                .CreateLogger();

            var app = builder.Build();
            app.UseResponseCompression();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();
            app.MapHub<QuizHub>("/QuizHub");

            // Add additional endpoints required by the Identity /Account Razor components.
            app.MapAdditionalIdentityEndpoints();

            app.Run();
        }
    }
}