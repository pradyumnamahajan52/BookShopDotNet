# BookShopDotNet

## Overview
BookShopDotNet is a web application developed using **ASP.NET Core MVC** and **Entity Framework Core**, designed to manage a bookshop system where admins can add and manage sellers, and sellers can add and manage books. The system provides authentication using cookies and role-based access control (RBAC) to restrict access.

## Features
### Admin Features:
- Admin login/logout.
- Manage sellers (Add, Update, Delete sellers).
- View the list of all sellers.

### Seller Features:
- Seller login/logout.
- Manage books (Add, Update, Delete books).
- View the list of books they added.

## Technologies Used
- **Backend:** ASP.NET Core MVC, Entity Framework Core
- **Frontend:** Razor Views, Bootstrap (for styling)
- **Database:** Microsoft SQL Server (MSSQL)


## Database Schema
### Tables
#### `Users` Table
| Column      | Data Type  | Constraints           |
|------------|-----------|-----------------------|
| user_id    | int       | Primary Key (PK)      |
| username   | varchar   | Required, Unique      |
| email      | varchar   | Required              |
| mobile_no  | varchar   | 10 characters         |
| password   | varchar   | Required              |
| role       | varchar   | Admin / Seller        |
| createdAt  | datetime  | Default: current time |
| updatedAt  | datetime  | Nullable              |

#### `Books` Table
| Column     | Data Type  | Constraints                |
|-----------|-----------|----------------------------|
| book_id   | int       | Primary Key (PK)          |
| book_name | varchar   | Required                   |
| author    | varchar   | Required                   |
| category  | varchar   | Optional                   |
| price     | int       | Required                   |
| user_id   | int       | Foreign Key (FK) -> Users |
| createdAt | datetime  | Default: current time     |
| updatedAt | datetime  | Nullable                  |

## API Endpoints
### **Authentication Routes**
- `GET /Home/UserLogin` → Show login page
- `POST /Home/UserLogin` → Process login
- `GET /Home/LogoutUser` → Logout user

### **Admin Routes**
- `GET /Home/Admin` → View all sellers
- `GET /Home/newSeller` → Add seller form
- `POST /Home/newSeller` → Create new seller
- `GET /Home/editSeller/{id}` → Edit seller form
- `POST /Home/editSeller` → Update seller details
- `GET /Home/deleteSeller/{id}` → Delete seller

### **Seller Routes**
- `GET /Home/Seller` → View seller's books
- `GET /Home/AddBook` → Add book form
- `POST /Home/AddBook` → Create new book
- `GET /Home/EditBook/{id}` → Edit book form
- `POST /Home/EditBook` → Update book details
- `GET /Home/DeleteBook/{id}` → Delete book

## Usage
### Admin Login
1. Visit `/Home/UserLogin`
2. Enter **Admin** credentials and log in
3. Manage sellers (add, update, delete)

### Seller Login
1. Visit `/Home/UserLogin`
2. Enter **Seller** credentials and log in
3. Manage books (add, update, delete)

## Author
Developed by **Pradyumna Mahajan**

