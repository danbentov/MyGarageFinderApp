using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using MyGarageFinderApp.ViewModels;
using MyGarageFinderApp.Views;
using TasksManagementApp.Services;

namespace MyGarageFinderApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .RegisterDataServices()
                .RegisterPages()
                .RegisterViewModels()
                ;

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public static MauiAppBuilder RegisterPages(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<AddCarView>();
            builder.Services.AddTransient<AddReviewView>();
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<ChatView>();
            builder.Services.AddTransient<GarageProfileView>();
            builder.Services.AddTransient<GarageRegisterView>();
            builder.Services.AddTransient<GaragesHomePageView>();
            builder.Services.AddTransient<GaragesMapView>();
            builder.Services.AddTransient<LoginView>();
            builder.Services.AddTransient<MyAppoitmentsView>();
            builder.Services.AddTransient<MyCarsProfileView>();
            builder.Services.AddTransient<MyChatsView>();
            builder.Services.AddTransient<ProfileView>();
            builder.Services.AddTransient<RegisterView>();
            builder.Services.AddTransient<RepairHistoryView>();
            builder.Services.AddTransient<StatisticsView>();
            builder.Services.AddTransient<UsersListView>();
            return builder;
        }

        public static MauiAppBuilder RegisterDataServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<MyGarageFinderWebAPIProxy>();
            return builder;
        }
        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<AddCarViewModel>();
            builder.Services.AddTransient<AddReviewViewModel>();
            builder.Services.AddTransient<AppShellView>();
            builder.Services.AddTransient<ChatViewModel>();
            builder.Services.AddTransient<GarageProfileViewModel>();
            builder.Services.AddTransient<GarageRegisterViewModel>();
            builder.Services.AddTransient<GaragesHomePageViewModel>();
            builder.Services.AddTransient<GaragesMapViewModel>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<MyAppoitmentsViewModel>();
            builder.Services.AddTransient<MyCarsProfileViewModel>();
            builder.Services.AddTransient<MyChatsViewModel>();
            builder.Services.AddTransient<ProfileViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<RepairHistoryViewModel>();
            builder.Services.AddTransient<StatisticsViewModel>();
            builder.Services.AddTransient<UsersListViewModel>();
            return builder;
        }
    }
}
