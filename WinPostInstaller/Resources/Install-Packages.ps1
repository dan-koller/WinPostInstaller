Write-Host "Installing essential software..."
# Install basic programs
winget install -e --id Google.Chrome
winget install -e --id Google.Drive
Write-Host "...done!"

Write-Host "Installing utilities..."
# Install utility programs
winget install -e --id smartmontools.smartmontools
Write-Host "...done!"

Write-Host "Installing development tools..."
# Install development programs
winget install -e --id JetBrains.IntelliJIDEA.Community
winget install -e --id JetBrains.PyCharm.Community
winget install -e --id Microsoft.VisualStudio.2022.Community
winget install -e --id Microsoft.VisualStudioCode
winget install -e --id OpenJS.NodeJS.LTS
winget install -e --id Postman.Postman
winget install -e --id Git.Git
winget install -e --id Python
winget install -e --id GitHub.cli
Write-Host "...done!"
