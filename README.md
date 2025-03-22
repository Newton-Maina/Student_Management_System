# Setup Instructions for Project Initialization

## 1. Clone the Repository

- Open **Visual Studio**.
- Navigate to `File > Clone Repository`.
- Enter the repository URL and click **Clone**.

## 2. Add a New Database Connection

- In Visual Studio, click on the **View** menu at the top.
- Select **Server Explorer**.
- In **Server Explorer**, right-click on **Data Connections** and select **Add Connection**.
- Choose your database server type (e.g., SQL Server) and enter the details of your database server.
- Click **OK** to establish the connection.

## 3. Update SQL Connection in the Code

- In **Solution Explorer**, open each `.cs` file in the project.
- Locate the **SQL connection string** in the code (usually within `App.config`, `Web.config`, or directly in the `.cs` files).
- Update the connection string to point to the newly created database (the one you just connected to in Server Explorer).
- Save all `.cs` files after making the changes.

## 4. Add SQL Query to the Project

- In **Server Explorer**, locate the **SQL Query** that needs to be executed.
- Drag the **SQL Query** from **Server Explorer** and drop it into the **work panel**.
- Review the SQL code to ensure it's correct and ready to be run.
- Right-click on the SQL Query and select **Execute** to run it.

## 5. Run the Project

- After the database has been initialized, click on **Start** in Visual Studio (or press `F5`) to run the project.
- The application should now be connected to the initialized database and ready to use.

---

## 🤝 Connect with Me

- [Website](https://codesbynewton.com/)
- [LinkedIn](https://www.linkedin.com/in/newton-maina-gatiba/)
- [Instagram](https://www.instagram.com/deceptive_j.i.n.g.e.r/)
- [Email](mailto:newtonmainag@gmail.com)

⭐️ From [Newton-Maina](https://github.com/Newton-Maina)

---
