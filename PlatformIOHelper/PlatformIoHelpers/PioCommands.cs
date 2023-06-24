using System.Diagnostics;

namespace PlatformIOHelper.PlatformIoHelpers;

public class PioCommands
{
    private readonly PioInstallManager _pioInstallManager;
    private readonly string _projectPath;

    public PioCommands(PioInstallManager pioInstallManager, string projectPath)
    {
        _pioInstallManager = pioInstallManager;
        _projectPath = projectPath;
    }
    
    public void RunPioExecutableWithArgs(string argumentsAsString)
    {
        Console.WriteLine();
        Console.WriteLine();
        
        var pioExecutablePath = _pioInstallManager.GetPioExecutablePath();

        var processStartInfo = new ProcessStartInfo()
        {
            WorkingDirectory = _projectPath,
            FileName = pioExecutablePath,
            Arguments = argumentsAsString,
            UseShellExecute = true,
            CreateNoWindow = true
        };

        var uploadProcess = Process.Start(processStartInfo);

        if (uploadProcess is null) throw new NullReferenceException();

        uploadProcess.WaitForExit();
        
        Console.WriteLine();
        Console.WriteLine();
    }
}