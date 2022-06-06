# Important
- Use your MSDN account for the most reliable experience
- Make sure docker-compose project is your startup project
- You will need to have docker desktop installed
- After you run the project first time, connect to one of the containers (not Dapr) and:
    - use az login to login to Azure
    - use az account set -n "<your account name>" to set the right account.



# Azure Resources Required
- Azure SQL database - make sure to update the connection string in the WeatherForecast/appsettings.json to point to your database
 