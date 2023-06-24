namespace PlatformIOHelper.ConsoleInteractions;

public class ConsoleMenuUserInteractor
{
    
    public char GetValidUserSelectionForShownMenu(string menuString)
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine(menuString);

        var validKeys = GetValidOptionKeysFromMenuString(menuString);

        var userResponseIsValid = false;
        var thisIsFirstAttempt = true;

        char userResponseKeyChar;
        
        do
        {
            if (!thisIsFirstAttempt)
                Console.WriteLine("Invalid selection, try again...");
            
            Console.Write("Please press a key to make a selection: ");
            
            userResponseKeyChar = Console.ReadKey().KeyChar;

            thisIsFirstAttempt = false;
            
            Console.WriteLine();
            Console.WriteLine();
            
            foreach (var validKeyChar in validKeys)
            {
                if (validKeyChar != userResponseKeyChar) continue;

                // Otherwise
                userResponseIsValid = true;
                break;
            }
        } 
        while (!userResponseIsValid);

        return userResponseKeyChar;
    }

    private List<char> GetValidOptionKeysFromMenuString(string menuString)
    {
        var returnList = new List<char>();
        
        foreach (var line in menuString.Split(Environment.NewLine))
        {
            if (!line.StartsWith('[')) continue;
            
            // Otherwise:
            returnList.Add(line[1]);
        }

        return returnList;
    }

    public void OpenSettings()
    {
        var userResponseKeyChar = '.';

        while (userResponseKeyChar != '0')
        {
            userResponseKeyChar =
                GetValidUserSelectionForShownMenu(MenuContents.SettingsMenu);

            HandleSettingsOption(userResponseKeyChar);
        }
    }

    private void HandleSettingsOption(char userResponseKeyChar)
    {
        throw new NotImplementedException();
    }
}