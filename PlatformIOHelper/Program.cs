namespace PlatformIOHelper;

public class Program
{
    public static void Main()
    {
        Console.WriteLine();
        Console.WriteLine("----------========== PlatformIO Tools ==========----------");
        Console.WriteLine("Version: 0.1");
        Console.WriteLine();
        Console.WriteLine("[1] - Upload via USB then prompt to open serial monitor");
        Console.WriteLine("[2] - Upload via USB then prompt to open serial monitor");
        Console.WriteLine();
        Console.Write("Please press a key to make a selection: ");
        
        var userKeyResponse = Console.ReadKey();

        Console.WriteLine();
        Console.WriteLine("User responded: " + userKeyResponse);
    }    
}
