# Task-Management-Web-API

## Description
This project is a Task Management RESTful API designed and implemented using ASP.NET Core Web API. It leverages Supabase as a backend-as-a-service (BaaS) platform, which provides reliable database hosting and authentication services.This is a robust and scalable Task Management RESTful API designed to streamline project and tasks organization. This API enables users to create and manage projects, each containing Goals and tasks that can be further broken down into subtasks. It tracks task and project status, priority, and overall progress to provide comprehensive workflow visibility.

## Features
- **User Authentication**: User registration,Sign in ,Sign out,Token Refresh and profile Management.
- **Team Management**: Administrate teams and their members with role assignments and streamlined management of team member invitations.
- **Project Management**: manage projects and their members, with integrated status and priority tracking to ensure organized project progress.
- **Goal Management**: Define and manage project goals with comprehensive status and priority tracking, including start and end date monitoring for effective timeline management.
- **Task Management**: Oversee goal-related tasks, including subtasks, with comprehensive management of statuses, priorities, timelines, and task category assignments for organized workflow.
- **Performance Management**:  Monitor goal performance by tracking tasks, completed and overdue tasks, total overdue time, and overall project progress for effective performance analysis including daily Report simulation.
- 
## Technical Features
- **Clean Architecture**:  The project is structured according to the principles of Clean Architecture, which promotes separation of concerns and a clear division of responsibilities.
- **SOLID Design Principles**:The code adheres to SOLID principles (Single Responsibility, Open-Closed, Liskov Substitution, Interface Segregation, and Dependency Inversion), making it easier to maintain and extend.
- **ASP.NET Core API**: The project includes an ASP.NET Core API project that serves as the API layer, handling HTTP requests and responses.
- **JWT for Token-based Authentication**: Effortlessly manage user sessions, authentication, and authorization with this state-of-the-art token-based approach.
- **CRUD Operations**: The project template provides a foundation for implementing complete CRUD (Create, Read, Update, Delete) operations on entities.
- **Dependency Injection**: The project utilizes the built-in dependency injection container in ASP.NET Core, making it easy to manage and inject dependencies throughout the application.
- **Data validation and error handling**: the projects Utilize a custom Middleware to handle Exceptions effectively (BadRequestException-NotFoundException-ForrbidenException ...ect).It also includes a custome service IValidateInpute to manage Input Validation.
- **Supabase Integration**: Managed PostgreSQL Database with full SQL support for complex queries and robust data management including Supabase Functions development and triggers implementation using PL/pgSQL .Authentication System supporting multiple methods such as email/password. Implemented Row-Level Security policies for the different tables in the system.
- **Data Structures and Algorithms**: Managed Tasks and Sub-Tasks by implmenting TaskGraph which is an AdjcenyList implemnted using a dictionary to keep track of tasks and parent tasks and handle Edge creation to link them and for space efficiency ,easy traversal and fast lookup.
 
## Technologies Used
- **ASP.NET Core 8 Web API** for API development.
- **Supabase** for Database Development (PostgreSQL) and Authentication
- C# 12 and PL/pgSQL

## How It Works
### 1. **Sign Up**
- A new user can sign up using their email and password and firstname and lastname.
- Upon registration, they will receive a confirmation email from **Supabase** to verify their account.

### 2. **Sign In**
- After email confirmation, the user can log in using their credentials(Emai and Password).

### 3. **Create a Team**
- After signing in, the user should create a **new team**.
- Once the team is created, they can **invite other users** by email (users must already have a profile in the system).
- Each invited user is assigned a role:
  - `enOwner`
  - `enTeamLead`
  - `enTeamMember`
  - `enGuest`
- Only team members with the role `enOwner` or `enTeamLead` are allowed to invite new members to the team.

### 4. **Create a Project**
- A user can create a **new project** under their team.
- They may choose to:
  - Add themselves as a **project member**, or
  - Remain only as the **project owner**.
- Similar to teams, only users with roles `enOwner` or `enTeamLead` within the project can invite other members.

### 5. **Set Project Goals**
- Once the project and its members are set up, users with roles `enOwner` or `enTeamLead` can begin creating **target goals** for the project.

### 6. **Manage Tasks**
- Any project member can create **tasks** and assign them either to themselves or to other project members.
    
## Endpoints
The API endpoints are fully documented using **Swagger**. You can explore and interact with them through the Swagger UI once the application is running.

![image alt](https://github.com/Toufik-Sam/Task-Management-Web-API-Supabase/blob/Master/images/swagger-image.PNG)

## License
[MIT License](LICENSE)

## Contact
[toufik.sam2022@gmail.com] - [www.linkedin.com/in/toufik-sam-bouafia-455773337]
