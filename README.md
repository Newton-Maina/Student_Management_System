# Student Management System

A Windows Forms (WinForms) application developed in C# for managing student records, course information, and administrative tasks.

## 📋 Features

-   **Authentication System**: Secure login for administrators.
-   **Dashboard**: Real-time overview of total enrolled students, available courses, and graduated students.
-   **Student Management**: 
    -   Register new students with details (Name, Gender, Address, Grade, Photo).
    -   Update existing student records.
    -   Manage student status (Enrolled, Inactive, Graduated).
-   **Course Management**: Add and manage available courses.

## 🛠 Tech Stack

-   **Language**: C#
-   **Framework**: .NET Framework 4.7.2
-   **UI**: Windows Forms (WinForms)
-   **Database**: Microsoft SQL Server (LocalDB) / `.mdf` file
-   **Data Access**: System.Data.SqlClient (ADO.NET)

## ⚙️ Prerequisites

-   Visual Studio 2019 or later.
-   .NET Framework 4.7.2 SDK.
-   SQL Server / LocalDB installed (usually comes with Visual Studio).

## 🚀 Setup & Installation

### 1. Clone the Repository
```bash
git clone <repository-url>
```

### 2. Database Setup
1.  Open the solution file `Student_Management_System_Group 1.sln` in Visual Studio.
2.  Locate the `SQLQuery.sql` file in the solution explorer.
3.  Execute the script in your local SQL Server instance to create the necessary tables (`users`, `students`, `courses`) and insert the default admin user.
    -   *Note: Ensure you are connected to the correct database instance where you want the tables created.*

### 3. ⚠️ CRITICAL: Update Connection Strings
The application currently uses **hardcoded connection strings** pointing to a specific user's directory. You **must** update these paths to run the application on your machine.

Open the following files and replace the path `C:\Users\maina newton\OneDrive\Documents\db_student_data.mdf` with the actual path to your database file (or use a standard LocalDB connection string):

-   `Student_Management_System_Group 1/LoginForm.cs`
-   `Student_Management_System_Group 1/DashboardForm.cs`
-   `Student_Management_System_Group 1/AddStudentsForm.cs`
-   `Student_Management_System_Group 1/AddStudentData.cs`
-   `Student_Management_System_Group 1/AddCourseData.cs`

**Example of a generic LocalDB string:**
```csharp
Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\students_data.mdf;Integrated Security=True;Connect Timeout=30
```
*(Note: Using `|DataDirectory|` requires the `.mdf` file to be in the output directory, e.g., `bin/Debug`)*.

### 4. Build and Run
1.  Build the solution (Ctrl + Shift + B).
2.  Press `F5` or click **Start** to run the application.

## 👤 Usage

**Default Admin Credentials:**
-   **Username**: `admin`
-   **Password**: `admin123`

## 🤝 Contributors

-   [Newton-Maina](https://github.com/Newton-Maina)

## 📄 License
This project is open-source.