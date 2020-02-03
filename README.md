This solution allows users to view commits of a certain GitHub repository. There are 5 projects included in the solution. 
See below on how to use each of the projects.

## CommitViewer
This is the main project of the solution. It is a console app that asks the user for input regarding the repository and other stuff depending on the state of the app.
To run this project simply cd into it and run the command: <b>"dotnet run"</b>. The program will first try to list commits using the GitHub API.
The program will ask for the GitHub username and repository name on startup. If the user provides valid arguments and the GitHub API is available it will fetch the commits for the given username and repository name. 
If for some reason, the application is not able to fetch the commits (repository not found, github api is down, etc) the program will rely on operating system processes and git commands to do it.


In order to run the application in this state, git will have to be installed on the user's machine. Furthermore, a git environment variable must be set on the system's environment variables with key (variable name): <b>GIT</b> and the variable value pointing to <b>git.exe</b>.<br>
If no environment variable is set the application will throw an InvalidOperationException and exit, providing information to the user on what to do (i.e setting the environment variable).


## CommitViewer.Test
This is the test project for the previously mentioned project. To run the tests simply cd into the project folder and run the command: <b>"dotnet test"</b>.<br>


## GitHubClient
This is the client that the CommitViewer project uses when retrieving commits from the GitHub API. 
It's a simple client that can be used in any project (as a NuGet package).


## WebApi
This is the web api that uses the flow mechanism implemented in the CommitViewer project (the fallback system). 
It has a paginated method for commits. It uses Swagger and Swashbuckle. To run the web api simply cd into the project and run the command: <b>"dotnet run"</b>.<br>
The api is listening on port 5000 by default. To use swagger UI go to: <b>http://localhost:5000/commitviewer/api</b>


## Domain
This is a shared domain project that every single project above uses. It contains the models and the utilities needed to make all the others work.


## Questions
Please send me an email if you have any questions: tiagomartinho.soares@gmail.com
