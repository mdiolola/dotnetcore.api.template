# dotnetcore.api.template
Project of template for most useful library to get started with .net core API.

##Dependency
Swagger
Entity Framework
RDS (MsSQL)

## How to create project from template
```sh
# clone the repo
git clone git@github.com:mdiolola/dotnetcore.api.template.git
# change directory
cd dotnetcore.api.template
# install template
dotnet new -i .
# help
dotnet new {template name} -h
# change directory
cd ..
# create a project from template
# -n=Your Project Name
# -ld=Your Local Docker image name
dotnet new {template name} -n {projectName} -ld {dockerimagename}
# change directory
cd {projectName}
# make sure build works
dotnet build {projectName}.sln

#Manually change the DB name, username, password
This can be found in /src/{projectname}/appsettings.{environment}.json 
I highly suggess to delete the migrations folder and create a new migration.
add migration {migration name}


## Project Layout
The basic layout of the project is as follows:
* `src`: Main project source files
* `src/{ProjectName}/Dockerfile`: Dockerfile used to build apps docker image in codebuild
* `src/{ProjectName}/Dockerfile.local`: Dockerfile used to build image for local testing
* `src/test`: Main project test files
* `docker*`: definition files created by Visual Studio for docker IDE testing