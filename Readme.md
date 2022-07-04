## TruckPlan
##### Truck Plan is a project that is tracking and proccessing GPS data of the truck voyage. The project is using serverless API that is also an AWS Lambda function. With this way there is no need to use an API gateway while our API is running on the cloud.

##### What is out of scope?
1. Security(Authorization and Authentication) was ignored.
2. Deployment (build & release pipelines, terraform & terragrunt files) was ignored.
3. Unit tests were not implemented.
4. Mappings (usage of automapper) was ignored.
5. I didn't configure appsettings for different environments(test, uat, prod).