using System.Diagnostics;

partial class Program
{
    // Set the hostname of the device
    static void SetHostName(string hostName)
    {
        // Run the powershell command to set the hostname
        RunPowerShellCommand($"Rename-Computer -NewName {hostName}");
    }

    // Install winget
    static void InstallWinget()
    {
        // Run the first command to download the package
        RunPowerShellCommand("Invoke-WebRequest -Uri https://github.com/microsoft/winget-cli/releases/download/v1.3.2691/Microsoft.DesktopAppInstaller_8wekyb3d8bbwe.msixbundle -OutFile .\\MicrosoftDesktopAppInstaller_8wekyb3d8bbwe.msixbundle");

        // Run the second command to install the package
        RunPowerShellCommand("Add-AppxPackage .\\MicrosoftDesktopAppInstaller_8wekyb3d8bbwe.msixbundle");

        // Run the third command to remove the package
        RunPowerShellCommand("Remove-Item .\\MicrosoftDesktopAppInstaller_8wekyb3d8bbwe.msixbundle");
    }

    // Install all software packages
    static void InstallPackages()
    {
        // Set the execution policy to unrestricted (required for the install script)
        RunPowerShellCommand("Set-ExecutionPolicy -ExecutionPolicy Unrestricted -Scope CurrentUser");

        // Path for the install script in the output directory
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Install-Packages.ps1");

        // Run the powershell command to install all packages
        RunPowerShellCommand($"& '{path}'");

        // Set the execution policy back to restricted (recommended)
        RunPowerShellCommand("Set-ExecutionPolicy -ExecutionPolicy Restricted -Scope CurrentUser");
    }

    // Helper method for running powershell commands from C#
    private static void RunPowerShellCommand(string command)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = command,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };

        process.Start();

        // Get the exit code of the process
        process.WaitForExit();
        int exitCode = process.ExitCode;

        // Check if the exit code is 0 (operation successful)
        if (exitCode == 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Successfully performed command: {command}");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Failed to execute command: {command}");
            Console.WriteLine($"Reason: {process.StandardOutput.ReadToEnd()}");
            Console.ResetColor();
        }
    }

    static void ApplyGitConfig(string name, string email)
    {
        // Open the gitconfig file
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", ".gitconfig");
        string gitconfig = File.ReadAllText(path);

        // Replace the placeholders with the user's values
        gitconfig = gitconfig.Replace("{{name}}", name);
        gitconfig = gitconfig.Replace("{{email}}", email);

        // Write the gitconfig file to the user's home directory
        string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        File.WriteAllText(Path.Combine(home, ".gitconfig"), gitconfig);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Successfully applied gitconfig");
        Console.ResetColor();
    }

    static void CopyTerminalSettings()
    {
        RunPowerShellCommand("Copy-Item -Path .\\Resources\\settings.json -Destination $env:USERPROFILE\\AppData\\Local\\Packages\\Microsoft.WindowsTerminal_8wekyb3d8bbwe\\LocalState\\settings.json -Force");
    }
}
