# Ice Cream Shop API

A comprehensive full-stack web application for managing an ice cream shop inventory with user authentication and role-based access control.

## 🚀 Key Features
- **RESTful API** with complete CRUD operations
- **JWT Authentication & Authorization** with Admin/User roles
- **Interactive Frontend** with dynamic content loading
- **Secure API endpoints** with token-based protection
- **Swagger API documentation** for easy testing
- **Custom middleware** for request logging
- **JSON file-based data persistence**

## 🛠️ Tech Stack
- **Backend:** ASP.NET Core 8 Web API
- **Authentication:** JWT Bearer tokens with role-based claims
- **Frontend:** Vanilla JavaScript, HTML5, CSS3
- **Documentation:** Swagger/OpenAPI 3.0
- **Architecture:** Clean Architecture with Services & Interfaces

## 📋 API Endpoints

### Ice Cream Management
- `GET /IceCream` - Get all ice creams (User)
- `POST /IceCream` - Add new ice cream
- `PUT /IceCream/{id}` - Update ice cream
- `DELETE /IceCream/{id}` - Delete ice cream

### User Management
- `GET /User` - Get all users (Admin only)
- `GET /User/{id}` - Get user by ID (User)
- `POST /User` - Create new user (Admin only)
- `PUT /User/{id}` - Update user (User)
- `DELETE /User/{id}` - Delete user (Admin only)
- `POST /login` - User authentication

## 🔐 Security Features
- JWT token-based authentication
- Role-based authorization (Admin/User policies)
- Protected API endpoints
- Secure user session management

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- Visual Studio 2022 or VS Code

### Installation
1. Clone the repository:
```bash
git clone https://github.com/[YOUR-USERNAME]/ice-cream-shop-api.git
```

2. Navigate to the project directory:
```bash
cd ice-cream-shop-api
```

3. Run the application:
```bash
dotnet run
```

4. Open your browser and navigate to:
- API: `https://localhost:7083`
- Swagger UI: `https://localhost:7083/swagger`
- Frontend: `https://localhost:7083/index.html`

## 📱 Usage
1. Login to get JWT token (Admin password: "1234")
2. Use the interactive frontend to manage ice cream inventory
3. Test API endpoints using Swagger UI
