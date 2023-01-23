using System.Diagnostics;
using System.Xml.Linq;

partial class Program
{
    static void SetHostName(string hostName)
    {
        RunPowerShellCommand($"Rename-Computer -NewName {hostName}");
    }

    static void InstallWinget()
    {
        // Run the first command to download the package
        RunPowerShellCommand("Invoke-WebRequest -Uri https://github.com/microsoft/winget-cli/releases/download/v1.3.2691/Microsoft.DesktopAppInstaller_8wekyb3d8bbwe.msixbundle -OutFile .\\MicrosoftDesktopAppInstaller_8wekyb3d8bbwe.msixbundle");

        // Run the second command to install the package
        RunPowerShellCommand("Add-AppxPackage .\\MicrosoftDesktopAppInstaller_8wekyb3d8bbwe.msixbundle");

        // Run the third command to remove the package
        RunPowerShellCommand("Remove-Item .\\MicrosoftDesktopAppInstaller_8wekyb3d8bbwe.msixbundle");
    }

    static void InstallPackages()
    {
        // Read the packages from the packages.xml file
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "packages.xml");

        Dictionary<string, string> packages = LoadXml(path);
        string acceptLicense = "--accept-source-agreements --accept-package-agreements";

        // Install every package via powershell
        foreach (var package in packages)
        {
            Console.WriteLine($"Installing {package.Key}");
            RunPowerShellCommand($"{package.Value} {acceptLicense}");
        }
    }

    /// <summary>
    /// Helper method to load an Xml file from a given path and return the content as dictionary.
    /// </summary>
    /// <param name="path">File path of the Xml.</param>
    /// <returns>Xml content as string dictionary</returns>
    private static Dictionary<string, string> LoadXml(string path)
    {
        // Create a dictionary to store the packages
        Dictionary<string, string> packages = new Dictionary<string, string>();

        foreach (var package in XDocument.Load(path).Descendants("package"))
        {
            // Check if the package has a name and command
            if (package.Attribute("name") == null || package.Attribute("command") == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid package entry in packages.xml");
                Console.ResetColor();
                continue;
            }

            string name = package.Attribute("name").Value;
            string url = package.Attribute("command").Value;

            // Add the package to the dictionary
            packages.Add(name, url);
        }

        return packages;
    }

    /// <summary>
    /// Helper method to execute raw strings as powershell commands.
    /// </summary>
    /// <param name="command">The command as raw string.</param>
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

    static void RebootSystem()
    {
        // Run the powershell command to reboot the system after 1 minute
        RunPowerShellCommand("Restart-Computer -Force -Delay 00:01");
    }
}
