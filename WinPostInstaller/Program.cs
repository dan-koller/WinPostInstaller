// Default values for the arguments
string hostname = "";
string gitname = "";
string gitmail = "";

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

// Print the cli args (DEBUG only)
Console.WriteLine($"{hostname}\n{gitname}\n{gitmail}\n");

// Set the hostname if the user specified it
if (hostname != null)
{
    hostname = hostname.Trim();
    SetHostName(hostname);
}

// Install package manager (winget)
InstallWinget();

// Install packages
InstallPackages();

// TODO: Apply local settings (git, terminal, etc.)

