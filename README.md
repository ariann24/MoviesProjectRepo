# Movies Project Repository
This will be an instruction on how to setup the database in the local computer, as well as on how to use the rest api's for both .net core and .net standard implementation.

## .Net Core Implementation Instructions
1. Open **Visual studio 2019 community**.
1. Click on the **tools** -> **NuGet Package Manager** -> **Package Manager Console**.
1. If "Migrations" are not available, run **add-migration migrationName** inside the Package Manager Console.
1. If "Migrations" are available, run only the **update-database**.
1. Open **Microsoft SQL** in your local and check if the database named **MoviesDB** already exist
1. Click run to test the API's

- Rest API's for .net core implementation usage
1. Post: http://localhost:63413/api/user/create_user - This api is used to create user account.

Form Body Sample:

{

    "FullName" : "ariann amores", 
    
    "EmailAddress" : "a1234@gmail.com", 
    
    "Password" : "ghjghj"
    
}



2. Post: http://localhost:63413/api/user/login - This is used to log in and checked if the user and password is authorized. This will return a token that will be used in the movies rest api.

Form Body Sample:

{
    
    "EmailAddress" : "a1234@gmail.com", 
    
    "Password" : "ghjghj"
    
}

3. Get: http://localhost:63413/api/movies - This will retrieve the list of movies that are not deleted. Use the token that is generated during log-in to access authorized api. If you are using Postman, **add the token in the authorization tab** with a bearer token type.

4. Post: http://localhost:63413/api/movies - This will create a data for movies. Use the token that is generated during log-in to access authorized api. If you are using Postman, **add the token in the authorization tab** with a bearer token type.

Form Body Sample:

{

    "MovieTitle" : "Ghostbusters Part 3", 
    
    "MovieDescription" : "sample description"
    
}

5. Put: http://localhost:63413/api/movies/{id} - This will update a data for movies based on the id. Use the token that is generated during log-in to access authorized api. If you are using Postman, **add the token in the authorization tab** with a bearer token type.

Form Body Sample:

{

    "MovieTitle" : "Ghostbusters", 
    
    "MovieDescription" : "sample description",
    
    "IsRented" : false
    
}

6. Delete: http://localhost:63413/api/movies/{id} - This will delete the movie data based on the id. This is soft delete. Use the token that is generated during log-in to access authorized api. If you are using Postman, **add the token in the authorization tab** with a bearer token type.


## .Net Standard Implementation Instructions
1. Open **Visual studio 2019 community**.
1. Open **Web.config**.
1. Find the **<connectionStrings> tag** with name **MoviesDBEntities**.
1. Change the **data source=** value, using your localhost server name.
1. Click run to test the API's
  
 **Note:** During the code first run in the .net core implementation, I just use the generated database and created a model entity in the .net standard implementation. If you are not able to run the database I included a .bak file in the repository. Please use it if incase.

- Rest API's for .net core implementation usage
1. Post: https://localhost:44371/api/user/create_user - This api is used to create user account.

Form Body Sample:

{

    "FullName" : "ariann amores", 
    
    "EmailAddress" : "a1234@gmail.com", 
    
    "Password" : "ghjghj"
    
}

2. Post: https://localhost:44371/api/user/login - This is used to log in and checked if the user and password is authorized. This will return a token that will be used in the movies rest api.

Form Body Sample:

{
    
    "EmailAddress" : "a1234@gmail.com", 
    
    "Password" : "ghjghj"
    
}

3. Get: https://localhost:44371/api/movies - This will retrieve the list of movies that are not deleted. Use the token that is generated during log-in to access authorized api. If you are using Postman, **add the token in the authorization tab** with a bearer token type.

4. Post: https://localhost:44371/api/movies - This will create a data for movies. Use the token that is generated during log-in to access authorized api. If you are using Postman, **add the token in the authorization tab** with a bearer token type.

Form Body Sample:

{

    "MovieTitle" : "Ghostbusters Part 3", 
    
    "MovieDescription" : "sample description"
    
}

5. Put: https://localhost:44371/api/movies/{id} - This will update a data for movies based on the id. Use the token that is generated during log-in to access authorized api. If you are using Postman, **add the token in the authorization tab** with a bearer token type.

Form Body Sample:

{

    "MovieTitle" : "Ghostbusters", 
    
    "MovieDescription" : "sample description",
    
    "IsRented" : false
    
}

6. Delete: https://localhost:44371/api/movies/{id} - This will delete the movie data based on the id. This is soft delete. Use the token that is generated during log-in to access authorized api. If you are using Postman, **add the token in the authorization tab** with a bearer token type.
