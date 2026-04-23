# 🛒 E-Commerce Backend API

## 📌 Overview

This project is a **RESTful Web API** built using **ASP.NET Core** that provides the backend for an e-commerce system.
It supports user authentication, product management, cart operations, and order processing.

The project follows a **clean layered architecture** to ensure scalability, maintainability, and separation of concerns.

---

## 🚀 Features

* 🔐 User Authentication & Authorization (JWT)
* 🛍️ Product Management (Create, Update, Delete, View)
* 📦 Category Management
* 🛒 Shopping Cart System
* 📑 Order Management
* 🖼️ Image Upload Support
* 📄 Pagination & Filtering
* ✅ Input Validation using FluentValidation
* 🧩 Clean Architecture (API, BLL, DAL, Common)

---

## 🛠️ Tech Stack

* ASP.NET Core Web API (.NET 10)
* Entity Framework Core
* SQL Server
* JWT Authentication
* FluentValidation

---

## 📂 Project Structure

The solution is divided into multiple layers:

* **Project.API**

  * Handles HTTP requests (Controllers)
  * Entry point of the application

* **Project.BLL**

  * Business Logic Layer
  * Contains Managers, DTOs, Validators

* **Project.DAL**

  * Data Access Layer
  * Contains DbContext, Repositories, Unit of Work

* **Project.Common**

  * Shared utilities
  * Pagination, Filtering, General Responses

---

## ⚙️ How to Run the Project

1. Clone the repository:

   ```bash
   git clone <(https://github.com/Hanna-adel/E-Commerce-API.git)>
   ```

2. Open the solution in **Visual Studio**

3. Update the connection string in:
appsettings.json

Example:
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=ECommerceDB;Trusted_Connection=True;TrustServerCertificate=True;"
}

4. Apply migrations:

   ```bash
   Add-Migration InitialCreate
   Update-Database
   ```

5. Run the project

6. Open Swagger:

   ```
   https://localhost:<port>/swagger
   ```

---

## 🔐 Authentication

* Uses **JWT (JSON Web Token)**
* After login/register, use the token in:

  ```
  Authorization: Bearer <your-token>
  ```

---

## 📡 Main API Endpoints

### 🔑 Auth

* `POST /api/auth/register`
* `POST /api/auth/login`

### 📦 Products

* `GET /api/products`
* `POST /api/products`
* `PUT /api/products/{id}`
* `DELETE /api/products/{id}`

### 🗂️ Categories

* `GET /api/categories`
* `GET /api/categories/{id}`
* `POST /api/categories`
* `PUT /api/categories/{id}`
* `DELETE /api/categories/{id}`

### 🛒 Cart

* `GET /api/cart`
* `POST /api/cart`
* `PUT /api/cart`
* `DELETE /api/cart/{productId}`

### 📑 Orders

* `POST /api/orders`
* `GET /api/orders`
* `GET /api/orders/{id}`

### 🖼️ Images

* `POST /api/images/upload`
* `POST /api/products/:id/image`
* `POST /api/categories/:id/image`

---

## 🧠 Key Concepts Used

* Repository Pattern
* Unit of Work Pattern
* DTOs (Data Transfer Objects)
* Dependency Injection
* Validation Layer
* Pagination & Filtering

---

## 👤 Author

**Hana Adel**

---


## Demo
* https://drive.google.com/drive/folders/1nWsQBFANWik0f3yLjtTSW2ABmFJ6GUdr?usp=sharing


---
