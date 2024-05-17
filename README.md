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
    _This image shows the login page where user can input their username and password_

 - **Registration** New users can be added by providing the necessary details.
    ![alt text](https://github.com/jomielenriquez/LibrarySystem/blob/main/Images/Registration.png)
    _This image shows how the admin can register new users or people who can bollow books_

#### User Role
 - **Role List Screen** Displays all roles.
    ![alt text](https://github.com/jomielenriquez/LibrarySystem/blob/main/Images/RoleList.png)
    _This image shows how the user roles will be displayed_

 - **Role Assignment:** Admins can assign roles to users in the registration.
    ![alt text](https://github.com/jomielenriquez/LibrarySystem/blob/main/Images/RoleAssign.png)
    _This image shows how admin can assing role to user in the registration_

 - **Role Management:** Admins can create, update, or delete roles.
    ![alt text](https://github.com/jomielenriquez/LibrarySystem/blob/main/Images/RoleManagement.png)
    _This image shows how admins can create, update, or delete roles_

#### List of Books
 - **Book Listing** Dispalys all books in the library with details like title, author, and quantity.
    ![alt text](https://github.com/jomielenriquez/LibrarySystem/blob/main/Images/BookListing.png)
    _This image shows the book's list screen where users can browse any books in the database._

 - **Book Search:** Allows users to search for books by various criteria.
    ![alt text](https://github.com/jomielenriquez/LibrarySystem/blob/main/Images/BookSearching.png)
    _This image shows how users can search for books_

#### Borrowing Records
 - **Record Borrowed Books** Admin can record borrowed books.
    ![alt text](https://github.com/jomielenriquez/LibrarySystem/blob/main/Images/RecordBorrowedBooks.png)
    _This image shows how admins can records borrowed books. Admins can select the borrowes username in the borrower dropdown._

 - **Borrowing History** Users can view their borrowing history.
    ![alt text](https://github.com/jomielenriquez/LibrarySystem/blob/main/Images/BorrowingHistory.png)
    _This image shows the list of all the books that was borrowed._

#### LibrarySystem.Data (Data Access Layer)
 - **DbContext** Defines the database context for Entity Framework.
 - **Entities** Contains the entity classes representing the database tables.
 - **Repositories** Provides generic and specific repositories for data operations.

#### LibrarySystem.Service (Business Logic Layer)
 - **UserService:** Manages user-related operations such as login, registration, and profile updates.
 - **RoleService:** Manages roles and role assignments.
 - **BookService:** Handles operations related to books, such as listing, searching, and managing book details.
 - **BorrowingService:** Manages borrowing and returning of books, including borrowing history.

#### LibrarySystem.Web (Custom Tag Helpers)
 - **Custom TagHelpers:** Contains custom tag helpers to extend the functionality of HTML elements in Razor views. Examples include:

## Security
 - User passwords are encrypted using MD5 encryption to ensure data security during login and registration processes.