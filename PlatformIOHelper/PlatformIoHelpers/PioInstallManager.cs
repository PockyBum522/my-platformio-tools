using System.Diagnostics;
using PlatformIOHelper.ConsoleInteractions;

namespace PlatformIOHelper.PlatformIoHelpers;

public class PioInstallManager
{
    private readonly ConsoleMenuUserInteractor _consoleMenuUserInteractor;

    public PioInstallManager(ConsoleMenuUserInteractor consoleMenuUserInteractor)
    {
        _consoleMenuUserInteractor = consoleMenuUserInteractor;
    }

    public string GetPioExecutablePath()
    {
        var userProfileFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        
        var defaultPioPath =
            Path.Join(
                userProfileFolder,
                ".platformio",
                "penv",
                "Scripts",
                "pio.exe");

        return defaultPioPath;
    }
    
    public async Task CheckPioInstallation()
    {
        if (File.Exists(GetPioExecutablePath())) return;
        
        // Otherwise, if we didn't find it at the expected path
        var userResponseKeyChar = 
            _consoleMenuUserInteractor.GetValidUserSelectionForShownMenu(MenuContents.PioInstallNotFoundMenu);

        switch (userResponseKeyChar)
        {
            case '1':
                await RunTemporarilySavedPioInstallScript();
                break;
            
            case '2':
                throw new NotImplementedException();
                
            case '3':
                break;
            
            case '0':
                Environment.Exit(0);
                break;
        }
    }

    private async Task RunTemporarilySavedPioInstallScript()
    {
        var fullDestinationPath =
            Path.Join(
                Path.GetTempPath(),
                "get-platformio.py");
        
        // Just in case we've done this before...
        File.Delete(fullDestinationPath);

        await DownloadFileToLocal(
            @"https://raw.githubusercontent.com/platformio/platformio-core-installer/master/get-platformio.py",
            fullDestinationPath);

        StartPythonScript(fullDestinationPath);
    }

    private static async Task DownloadFileToLocal(string url, string destinationPath)
    {
        using var client = new HttpClient();

        using var result = await client.GetAsync(url);
        
        if (!result.IsSuccessStatusCode) throw new HttpRequestException();
        
        var fileAsBytes = await result.Content.ReadAsByteArrayAsync();
        
        await File.WriteAllBytesAsync(destinationPath, fileAsBytes);
    }

    private void StartPythonScript(string fullDestinationPath)
    {
        var pythonDefaultPath = @"C:\Python311\python.exe";

        if (!File.Exists(pythonDefaultPath))
            throw new FileNotFoundException($"Python not found at: {pythonDefaultPath}");

        var pythonProcess = Process.Start(pythonDefaultPath, fullDestinationPath);

        pythonProcess.WaitForExit();
    }
}