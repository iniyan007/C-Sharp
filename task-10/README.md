# Building a Mini Microservice with ASP.NET Core

- Create a small RESTful API that manages a resource (e.g., Products, Orders, or Books) using ASP.NET Core.
- **Project Setup:**
    - Set up a new ASP.NET Core Web API project.
    - Configure routing and controllers.
- **Dependency Injection:**
    - Implement a service layer and register services using ASP.NET Core’s dependency injection.
- **Data Access:**
    - Use Entity Framework Core with an in-memory database (or SQLite) to perform CRUD operations.
- **Asynchronous Operations:**
    - Use `async`/`await` in your controller actions to handle database operations.
- **Error Handling and Logging:**
    - Implement middleware or filters for global exception handling.
    - Integrate basic logging to record actions and errors.
- **Testing and Documentation:**
    - Write unit tests for your controllers and services.
    - Document the API endpoints (using tools like Swagger).

- **Advanced Considerations (Optional):**
    - Incorporate design patterns such as Repository and Unit of Work.
    - Implement custom middleware for request/response logging or authentication.

## Output

- See `logs/booksapi20260418.log`

![Output](img/img1.png)
![Output](img/img2.png)
![Output](img/img3.png)
![Output](img/img4.png)