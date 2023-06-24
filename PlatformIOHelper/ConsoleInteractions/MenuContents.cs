namespace PlatformIOHelper.ConsoleInteractions;

public static class MenuContents
{
    private static string MainMenuHeader =>
        @"
----------========== PlatformIO Tools ==========----------
Version: 0.1";
    
    public static string SetProjectPrompt =>
    $@"{MainMenuHeader}

PLATFORMIO PROJECT NOT SET:

Please enter the full path to the PlatformIO project that you'd like to work with:
(This should be the folder containing the platformio.ini file.)";
    
    public static string MainMenu =>
        $@"{MainMenuHeader}

[1] - Upload via USB 
[2] - Open serial monitor
[3] - Upload via HTTP
[4] - Subscribe to MQTT topic
[0] - Settings";
    
    public static string PioInstallNotFoundMenu =>
        @"WARNING: PIO.EXE NOT FOUND AT EXPECTED DEFAULT PATH: {defaultPioPath}

PLEASE CHOOSE AN OPTION:

[1] - Download and run PIO install script
[2] - Specify path to pio.exe manually (Will save for next session)
[3] - Ignore (Things will break and you will be re-prompted)
[0] - Exit";
    
    public static string SettingsMenu =>
        @"----------========== Settings ==========----------

[1] - Edit serial monitor arguments 
[2] - Edit IP address(es) that Upload via HTTP should offer
[3] - Edit MQTT topic(s) that Subscribe to MQTT topic should offer
[4] - Edit path to pio.exe
[0] - Back";
}