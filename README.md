# Notification Backend

This project is a .NET Core application designed to send notifications to all stores. It provides a RESTful API for managing and dispatching notifications.

## Project Structure

- **Controllers**
  - `NotificationController.cs`: Handles HTTP requests related to notifications.
  
- **Models**
  - `Notification.cs`: Defines the structure of a notification.

- **Services**
  - `NotificationService.cs`: Contains the logic for sending notifications.

- **Data**
  - `ApplicationDbContext.cs`: Manages the database connection and interactions with the notifications table.

- **Program.cs**: Entry point of the application.

- **Startup.cs**: Configures services and the application's request pipeline.

- **appsettings.json**: Contains configuration settings for the application.

## Setup Instructions

1. **Clone the repository**:
   ```
   git clone <repository-url>
   cd NotificationBackend
   ```

2. **Install dependencies**:
   Ensure you have the .NET SDK installed. Run the following command to restore the dependencies:
   ```
   dotnet restore
   ```

3. **Configure the database**:
   Update the `appsettings.json` file with your database connection string.

4. **Run the application**:
   Use the following command to start the application:
   ```
   dotnet run
   ```

5. **Access the API**:
   The API will be available at `http://localhost:5000/api/notifications`.

## Usage Guidelines

- **Send Notification**:
  To send a notification, make a POST request to `/api/notifications/send` with the notification data in the request body.

- **Notification Structure**:
  The notification should include the following fields:
  - `Title`: The title of the notification.
  - `Message`: The content of the notification.
  - `Timestamp`: The time the notification was created.

## Contributing

Contributions are welcome! Please submit a pull request or open an issue for any enhancements or bug fixes.