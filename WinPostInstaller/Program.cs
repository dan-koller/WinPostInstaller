// Default values for the arguments
string? hostname = null;
string? gitname = null;
string? gitmail = null;

// Print the command line arguments
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
if (hostname != null)
{
    Console.WriteLine($"Setting hostname to {hostname}");
    hostname = hostname.Trim();
    SetHostName(hostname);
}

// Install package manager (winget)
Console.WriteLine("Installing winget package manager");
InstallWinget();

// Install packages
Console.WriteLine("Installing packages");
InstallPackages();

// Configure git if the user has specified the parameters
if (gitname != null && gitmail != null)
{
    // Copy the gitconfig and apply settings
    Console.WriteLine($"Applying gitconfig for user {gitname}, {gitmail}");
    ApplyGitConfig(gitname, gitmail);
}

// Copy terminal settings
CopyTerminalSettings();

// Finish the installation
Console.WriteLine("Installation finished! Please reboot the device.\nPress any key to exit...");
