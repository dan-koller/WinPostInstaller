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
        // Path for the install script in the output directory
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Install-Packages.ps1");

        // Run the powershell command to install all packages
        RunPowerShellCommand($"& '{path}'");
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
    }
}
