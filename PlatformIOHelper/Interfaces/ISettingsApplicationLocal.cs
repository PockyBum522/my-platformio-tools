namespace PlatformIOHelper.Interfaces;

/// <summary>
/// Config.net interface that will be turned into a proxy object for settings pertaining to this application
/// (not library settings)
/// </summary>
public interface ISettingsApplicationLocal
{
    public string LastProjectDirectory { get; set; }

}