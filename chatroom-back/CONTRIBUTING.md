# ChatRoom - Contributing

## Setup

### Dependencies
To develop ChatRoom, you will need the following installed :
- [Latest LTS version of .NET SDK](https://dotnet.microsoft.com/en-us/download) (`winget install "microsoft.dotnet.sdk.8"`)
- [Latest LTS version of Node JS](https://nodejs.org/en/download/package-manager) 
	- Install PNPM (`npm install -g pnpm`)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) *(Also requires WSL 2 if using Windows)*

The following are recommended for debug purposes : 
- [Insomnia](https://insomnia.rest/download) (`winget install "insomnia.insomnia"`) *(or any other HTTP client)*


## Build / Run
If you use Rider, no additional steps are required. Both launch profiles are available out of the box.
Assuming a valid environment, both API and Web projects should build just fine.

### Web
Before running the Angular project, make sure all dependencies are correctly restored :
```bash
pnpm install
```

