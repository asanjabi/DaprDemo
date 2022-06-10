# Important
- Use your MSDN account for the most reliable experience.  If you get the message to login while getting tokens, but you are alreadly logged in, check conditional access policies in your tenant.
- Make sure docker-compose project is your startup project
- You will need to have docker desktop installed
- After you run the project first time, connect to one of the containers (not onf of the Dapr ones) and:
    - use az login to login to Azure
    - use az account set -n "<your account name>" to set the right account.


# Azure Resources Required
- Azure SQL database - make sure to update the connection string in the WeatherForecast/appsettings.json to point to your database
- Application Insights instance, update the instrumentation key in the .env file (copy from env-template). If you change values in the environment you might have to rebuild the containers, easiest way is to clean the docker-compose project and rebuild the solution.


# Notes
- If you run into odd issues with images or configuration:
    - Right click on docker-compose and click on "clean"
    - Also you can try "docker builder prune" to clean up build residues

- You can get to the local Zipkin server at http://localhost:5411/ once the containers are running


