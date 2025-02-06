# WebApplicationPOE1

## Overview

WebApplicationPOE1 is a web-based application designed to manage products, transactions, and client orders. It provides functionality for users to view available products, add new products, place orders, and review transaction history. The application is built using ASP.NET Core MVC with Entity Framework for database management.

## Features

- **Home Page:** Displays general information and navigation links.
- **Product Management:** Add and view products.
- **Order Management:** Place orders and view past transactions.
- **User Authentication:** Retrieves the current user's identity.
- **Transaction Tracking:** Stores and retrieves transaction history.

## Prerequisites

Before running the application, ensure you have the following installed:

- .NET Core SDK
- SQL Server
- Visual Studio or any preferred IDE

## Installation

1. Clone the repository:
  
  ```sh
  git clone https://github.com/your-repository/WebApplicationPOE1.git
  ```
  
2. Navigate to the project directory:
  
  ```sh
  cd WebApplicationPOE1
  ```
  
3. Configure the database connection in `appsettings.json`:
  
  ```json
  "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=Kumal_DB;Trusted_Connection=True;MultipleActiveResultSets=true;"
  }
  ```
  
4. Apply database migrations:
  
  ```sh
  dotnet ef database update
  ```
  
5. Run the application:
  
  ```sh
  dotnet run
  ```
  

## Usage

### Adding a Product

1. Navigate to `AddProduct`.
2. Fill in the product details and submit.
3. The product will be saved to the database.

### Placing an Order

1. View available products in the `MyWork` section.
2. Select a product and place an order by specifying the quantity.
3. The order will be saved and viewable in the `Orders` section.

### Viewing Transactions

1. Navigate to `Orders` or `ClientOrders`.
2. View all past transactions and order details.
