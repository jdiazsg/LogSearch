# LogSearch

LogSearch is an exercise exposing a REST GET endpoint that retrieves a log content by specifying the name of the file, the number of lines to return and optionally a keyword to return matches

## Design Considerations
In summary the endpoint does the following:
- Open the log file
- Read line by line and filters only lines matching the keyword until it has enough lines specified by the numberOfResults parameter
- Return the matching lines 

The complicated part is that as the log files are written with the last entries at the end and as the requirement is to return the last matching lines then it should read from the end of the file until it has the matching lines. In order to do that I had to do two things:
- Implement logic to open the file with a Stream by starting at the end of the file, read char by char until it detect a file break. My first try was doing this but when I tried with a BIF file it was really slow then I found out that reading char by char from the file stream is not good for performance so I implemented a buffer
- The buffer reads by chunks from the file but only returns character by character from the end of the file.


## Technical Details
The project is built with .NET 7 Web API.
The main endpoint code is on the Controllers\LogController.cs class 

## Pre requisites
- .NET Core runtime version 7

## Usage

- On a terminal file navigate to the project root
and use the dotnet build, dotnet run commands. Reference can be found here https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-build

- curl -X 'GET' \
  'http://localhost:5155/Log?fileName=unixlog.txt&keyword=secserv&numberOfResults=3' \
  -H 'accept: text/plain'

- It also exposes a UI on the address http://localhost:5155/swagger/index.html

## Final notes
- This file was created in a hurry as it took me several hours to complete the exercise so it's not polished
- The application when executed it hosts a swagger UI. I was going to create a simple UI for it but the swagger one suited me fine