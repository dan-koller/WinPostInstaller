# WinPostInstaller

WinPostInstaller is a simple tool to install software packages and apply core settings (such as git, terminal, etc.) on Windows 10 or Windows 11. It is built around the [winget](https://github.com/microsoft/winget-cli) command line tool for package management.

## Features

-   Set the computer's hostname
-   Apply Git settings
-   Copy Windows Terminal settings
-   Install software packages

## Installation

The app is available as a cli-tool on Windows 10 and Windows 11. To get started, download the latest release from the [releases page](https://github.com/dan-koller/WinPostInstaller/releases/tag/v1.0.0) and extract the zip file.

## Usage

Open a new terminal window and navigate to the folder where you extracted the zip file. Then run the following command:

-   Default settings:
    ```powershell
    .\WinPostInstaller.exe\
    ```

#### You can also add flags for the hostname and git settings (optional):

-   Only set the hostname:

    ```powershell
    .\WinPostInstaller.exe --hostname MyComputer
    ```

-   Only set the git settings:

    ```powershell
    .\WinPostInstaller.exe --git-name "John Doe" --git-email "johndoe@<domain>.com"
    ```

-   Set the hostname and git settings:
    ```powershell
    .\WinPostInstaller.exe --hostname MyComputer --git-name "John Doe" --git-email "johndoe@<domain>.com"
    ```

---

You can also change the packages by modifying the `packages.xml` file, which is located in the `Resources` folder. Just follow the structure of the existing packages and add your own. You can find the package names on the [winget repository](https://github.com/microsoft/winget-pkgs) or sites like [winget.run](https://winget.run/).

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
