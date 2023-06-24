using PlatformIOHelper.ConsoleInteractions;
using PlatformIOHelper.PlatformIoHelpers;

namespace PlatformIOHelper;

public class Program
{
    private static readonly ConsoleMenuUserInteractor ConsoleMenuUserInteractor = new();
    private static readonly PioInstallManager PioInstallManager = new(ConsoleMenuUserInteractor);
    
    private static PioCommands? PioCommands;

    public static async Task Main()
    {
        await PioInstallManager.CheckPioInstallation();
        
        var projectPath = GetProjectPathFromUser();
        
        PioCommands = new(PioInstallManager, projectPath);
        
        while (true)
        {
            var userResponseKeyChar = PromptUserWithMainMenu();
            
            HandleLaunchingSelectedOption(userResponseKeyChar);
        }
        
        // ReSharper disable once FunctionNeverReturns because it's not supposed to
    }

    private static string GetProjectPathFromUser()
    {
        Console.WriteLine(MenuContents.SetProjectPrompt);
        
        return Console.ReadLine() ?? "";
    }

    private static char PromptUserWithMainMenu()
    {
        var userResponseKeyChar = 
            ConsoleMenuUserInteractor.GetValidUserSelectionForShownMenu(MenuContents.MainMenu);

        return userResponseKeyChar;
    }
    
    private static void HandleLaunchingSelectedOption(char userKeyResponse)
    {
        if (PioCommands is null) throw new NullReferenceException();
        
        switch (userKeyResponse)
        {
            case '1':
                PioCommands.RunPioExecutableWithArgs("run --target upload");
                break;

            case '2':
                // Show all com ports on system with extra info to user
                PioCommands.RunPioExecutableWithArgs("device list");            
                
                Console.WriteLine("Serial monitor attempting connection to COM port...");
                
                PioCommands.RunPioExecutableWithArgs("device monitor -b 115200");
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