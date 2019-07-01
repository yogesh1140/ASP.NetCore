IMDBDummy


Developed using Angular and Asp.Net Core


  Tools Required :
  1. .Net Core SDK 2.1.300(x86)
  2. Nodejs (prefereably latest)
  3. angular-cli ~6.0.8(needs to be installed globally)
  4. bower (needs to be installed globally if try to publish)
  5. Visual studio 2017 v15.6+ if using method 3
  
Steps to Run:
Method 1: Pubish
  1. Change baseurl in below files in case not running with default configuration
    \ClientApp\app\movies\shared\movie.service.ts
    \ClientApp\app\movies\shared\upload.service.ts
    
  2. Set DB connection string in "appsettings.json"
  
  3. Execute in commandline "dotnet ef database update" in project folder
  
  4. Execute in commandline "dotnet publish -o destination directory>" in project folder
  
  5. Execute in commnadline "dotnet IMDBDummy.dll" in published folder
  
  6. Site will be available at localhost:5000(default configuration)
  
  Method 2: Manual Run
  
  1. Change baseurl in below files in case not running with default configuration
    \ClientApp\app\movies\shared\movie.service.ts
    \ClientApp\app\movies\shared\upload.service.ts
    
  2. Set DB connection string in "appsettings.json"
  
  3. Execute in commandline "dotnet ef database update" in project folder
  
  4. Execute in commandline "npm install" in project folder
  
  5. Execute in commandline "bower install" in project folder
  
  6. Execute in commandline "ng build" in project folder
  
  6. Execute in commnadline "dotnet IMDBDummy.dll" in published folder
  
  7. Site will be available at localhost:5000(default configuration)
  
  Method 3: Using Visual Studio to run
  
  1. Change baseurl in below files in case not running with default configuration
    \ClientApp\app\movies\shared\movie.service.ts
    \ClientApp\app\movies\shared\upload.service.ts
    
  2. Set DB connection string in "appsettings.json"
  
  3. Execute in commandline "dotnet ef database update" in project folder
  
  4. Build project
  
  4. Execute in commandline "ng build" in project folder
  
  5. Or you can skip steps 4 and 5 if publishing
  
   
  
    
  
  
    
