using Config.Net;
using PlatformIOHelper.ConsoleInteractions;
using PlatformIOHelper.Interfaces;
using PlatformIOHelper.PlatformIoHelpers;

namespace PlatformIOHelper;

public class Program
{
    private static readonly ConsoleMenuUserInteractor ConsoleMenuUserInteractor = new();
    private static readonly PioInstallManager PioInstallManager = new(ConsoleMenuUserInteractor);
    
    private static PioCommands? _pioCommands;
    private static readonly ISettingsApplicationLocal SettingsApplicationLocal = InitializeApplicationConfiguration();

    public static async Task Main()
    {
        await PioInstallManager.CheckPioInstallation();
        
        SettingsApplicationLocal.LastProjectDirectory = GetProjectPathFromUser(SettingsApplicationLocal.LastProjectDirectory);
        
        _pioCommands = new(PioInstallManager, SettingsApplicationLocal.LastProjectDirectory);
        
        while (true)
        {
            var userResponseKeyChar = PromptUserWithMainMenu();
            
            HandleLaunchingSelectedOption(userResponseKeyChar);
        }
        
        // ReSharper disable once FunctionNeverReturns because it's not supposed to
    }
    
    private static ISettingsApplicationLocal InitializeApplicationConfiguration()
    {
        return 
            new ConfigurationBuilder<ISettingsApplicationLocal>()
                .UseIniFile(@"C:\users\public\public documents\pio-helper-settings.ini")
                .Build();
    }
    
    private static string GetProjectPathFromUser(string lastProjectDirectory)
    {
        Console.WriteLine(MenuContents.SetProjectPrompt);

        Console.Write($"[ {lastProjectDirectory} ]: ");
        
        var userResponse = Console.ReadLine() ?? lastProjectDirectory;

        if (string.IsNullOrWhiteSpace(userResponse))
            userResponse = lastProjectDirectory;

        return userResponse;
    }

    private static char PromptUserWithMainMenu()
    {
        var userResponseKeyChar = 
            ConsoleMenuUserInteractor.GetValidUserSelectionForShownMenu(MenuContents.MainMenu);

        return userResponseKeyChar;
    }
    
    private static void HandleLaunchingSelectedOption(char userKeyResponse)
    {
        if (_pioCommands is null) throw new NullReferenceException();
        
        switch (userKeyResponse)
        {
            case '1':
                _pioCommands.RunPioExecutableWithArgs("run --target upload");
                break;

            case '2':
                // Show all com ports on system with extra info to user
                _pioCommands.RunPioExecutableWithArgs("device list");            
                
                Console.WriteLine("Serial monitor attempting connection to COM port...");
                
                _pioCommands.RunPioExecutableWithArgs("device monitor -b 115200");
                break;
            
            case '0':
                ConsoleMenuUserInteractor.OpenSettings();
                break;

            default:
                Console.WriteLine("Selected option was invalid or not yet implemented");
                break;
        }
    }
}