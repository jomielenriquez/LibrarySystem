# Library System
## Overview
-The Library Management System is a web application developed using the .NET MVC Framework. The system is designed to manage user accounts, roles, book listings, and borrowing records. It is structured into four projects, each serving a distinct purpose within the application.

### Project Structure
#### **1. LibrarySystem (MVC Project)**
This is the main project of the application, implemented using the .NET MVC Framework. It contains the user interface and handles the presentation layer of the application. The project is divided into the following sections:

 - **User Account:** Manages user login and registration. User passwords are encrypted using MD5 encryption.
 - **User Role:**  Manages different roles assigned to users (e.g., admin, student).
 - **List of Books:** Displays the collection of books available in the library.
 - **Borrowing Records:** Tracks the borrowing  and returning of books by users.

#### **2. LibrarySystem.Data (Data Access Layer)**
This project is responsible for data access and database interactions. It includes:

 - Entity Framework models and context for database operations.
 - Repositories for managing data transactions and CRUD operations.

#### **3. LibrarySytem.Service (Business logic Layer)**
The business logic layer contains the core functionality and rules of the application. It includes:

 - Services that implement the business rules and interact with the data access layer.
 - Methods for managing users, roles, books, and borrowing records.

#### **4. LibrarySystem.Web (Custom Tag Helpers)**
This project is a class library that contains custom tag helpers used within the MVC views. These helpers simplify view rendering and enhance the functionality of the user interface.

## Project Details
### LibrarySystem (MVC Project)

#### User Account
 - **Login:** Allow users to log in their credentials. MDS5 encryption is used to secure passwords
    ![alt text](https://github.com/jomielenriquez/LibrarySystem/blob/main/Images/Login.png)