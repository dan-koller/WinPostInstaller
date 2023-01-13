Write-Host "Installing essential software..."
# Install basic programs
winget install -e --id Google.Chrome --accept-source-agreements --accept-package-agreements
winget install -e --id Google.Drive --accept-source-agreements --accept-package-agreements
Write-Host "...done!"

Write-Host "Installing utilities..."
# Install utility programs
winget install -e --id smartmontools.smartmontools --accept-source-agreements --accept-package-agreements
Write-Host "...done!"

Write-Host "Installing development tools..."
# Install development programs
winget install -e --id JetBrains.IntelliJIDEA.Community --accept-source-agreements --accept-package-agreements
winget install -e --id JetBrains.PyCharm.Community --accept-source-agreements --accept-package-agreements
winget install -e --id Microsoft.VisualStudio.2022.Community --accept-source-agreements --accept-package-agreements
winget install -e --id Microsoft.VisualStudioCode --accept-source-agreements --accept-package-agreements
winget install -e --id OpenJS.NodeJS.LTS --accept-source-agreements --accept-package-agreements
winget install -e --id Postman.Postman --accept-source-agreements --accept-package-agreements
winget install -e --id Git.Git --accept-source-agreements --accept-package-agreements
winget install -e --id Python --accept-source-agreements --accept-package-agreements
winget install -e --id GitHub.cli --accept-source-agreements --accept-package-agreements
Write-Host "...done!"
