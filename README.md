# Angular Syncfusion Grid Client with Web API Adaptor

An Angular client application built to interact with backend Web APIs. 

## Repository Description 

An Angular-based frontend application that acts as a client-side adaptor for consuming, managing, and visualizing data from RESTful Web APIs. The project is structured to demonstrate clean API integration patterns, modular Angular architecture, and scalable frontend development practices.

##  Overview

**WebapiadaptorClient** is a frontend Angular application designed to consume and display data from RESTful Web APIs. The application is structured for scalability, maintainability, and ease of development, making it suitable for enterprise and integration-focused use cases.

## Prerequisites

- **Angular** 17.2.3  
- **TypeScript**
- **HTML / SCSS**
- **Node.js**
- **Angular CLI**

## Key Features

- Data binding using Syncfusion EJ2 Angular Grid
- Server-driven data consumption via RESTful Web APIs
- Paging, sorting, and filtering capabilities
- Inline editing with primary key support
- Toolbar actions for common grid operations
- Configurable and extensible column defi


## Running the Application

1. Clone the project repository
    ```
    git clone <repo_link>
    ```
2. Navigate to the client application and install dependencies
    ```
    cd WebApiAdaptor.Client
    npm i
    ```
3. Navigate to the server project and build the application
    ```
    cd ../WebApiAdaptor.Server
    dotnet build
    ```
4. Run the server
    ```
    dotnet run
    ```

## Reference Documentation

- [Getting started with EJ2 Angular](https://ej2.syncfusion.com/angular/documentation/grid/getting-started)
- [Editing](https://ej2.syncfusion.com/angular/documentation/grid/editing/edit)
- [Angular Grid API](https://ej2.syncfusion.com/angular/documentation/api/grid/index-default)