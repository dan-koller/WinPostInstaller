string hostname = "";
string gitname = "";
string gitmail = "";

for (int i = 0; i < args.Length; i++)
{
    switch (args[i])
    {
        case "--host":
            hostname = args[i + 1];
            break;
        case "--gitname":
            gitname = args[i + 1];
            break;
        case "--gitmail":
            gitmail = args[i + 1];
            break;
    }
}

// Set the hostname if the user specified it
if (!"".Equals(hostname))
{
    Console.WriteLine($"Setting hostname to {hostname}");
    hostname = hostname.Trim();
    SetHostName(hostname);
}

// Install winget package manager
Console.WriteLine("Installing winget package manager");
InstallWinget();

// Inform the user that the installation will now start
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("During installation, UAC prompts may appear. Please allow them to continue the installation.");
Console.ResetColor();

// Install packages
Console.WriteLine("Installing packages");
InstallPackages();

// Configure git settings if the user has specified the parameters
if (!"".Equals(gitname) && !"".Equals(gitmail))
{
    // Copy the gitconfig and apply settings
    Console.WriteLine($"Applying gitconfig for user {gitname}, {gitmail}");
    ApplyGitConfig(gitname, gitmail);
}

// Copy terminal settings
CopyTerminalSettings();

// Finish the installation
Console.WriteLine("Installation finished! The system will now reboot.\nPress any key to continue...");

// Wait for the user to press any key, then reboot the system
Console.ReadKey();
RebootSystem();
