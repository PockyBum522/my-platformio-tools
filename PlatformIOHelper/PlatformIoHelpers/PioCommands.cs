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
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true  
        };

        var uploadProcess = new Process()
        {
            StartInfo = processStartInfo
        };

        if (uploadProcess is null) throw new NullReferenceException();

        Console.WriteLine($"Starting: pio {argumentsAsString}");
        Console.WriteLine();
        Console.WriteLine($"ON PROJECT: {_projectPath}");
        Console.WriteLine();
        
        uploadProcess.Start();
        
        while (!uploadProcess.HasExited)
        {
            if (Console.KeyAvailable)
                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                    break;

            // We don't want to block with this
            ReadLineIfAvailable(uploadProcess);
        }

        Console.WriteLine();
        Console.WriteLine();

        Console.WriteLine("Finished!");

        Console.WriteLine();
        Console.WriteLine();
    }

    private async Task ReadLineIfAvailable(Process uploadProcess)
    {
        var outLine = uploadProcess.StandardOutput.ReadLine();
            
        // There are deadlock conditions if you try to read both stdoutput and stderr one and then the other from the process.
        // https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.processstartinfo.redirectstandardoutput?view=net-7.0
            
        //var errorLine = uploadProcess.StandardError.ReadLine();
            
        Console.WriteLine(outLine);
        //Console.WriteLine($"ERR: {errorLine}");
    }
}