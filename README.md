# BankEase

**BankEase** is a WPF-based banking application built using **.NET 8** and **C# 12.0**. It provides a user-friendly interface for essential banking operations such as user registration, account management, and transaction handling. The app follows the **MVVM (Model-View-ViewModel)** architecture and utilizes **dependency injection** via `Microsoft.Extensions.DependencyInjection`. Data persistence is achieved using **XML-based services**, ensuring a lightweight and file-based approach.

## ✨ Key Features

- 🔐 **User and Admin Roles**  
  Separate interfaces and privileges tailored for users and administrators.

- 🏦 **Account Management**  
  Create, update, and delete accounts with associated user data.

- 💳 **Transaction Handling**  
  Perform deposits, withdrawals, and money transfers seamlessly.

- 🔄 **Navigation and State Management**  
  Utilizes `ApplicationCache` and `IWindowNavigation` for efficient window handling and session management.

- ✅ **Validation & Error Handling**  
  Ensures secure operations through input validation and comprehensive exception management.

## 🛠 Technologies Used

- [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- C# 12.0
- WPF (Windows Presentation Foundation)
- MVVM Pattern
- XML for Data Storage
- Dependency Injection (`Microsoft.Extensions.DependencyInjection`)

## 📁 Project Structure
BankEase/ ├── Core/ ├── Helper/ ├── Media/ ├── Models/ ├── Respository/ ├── ResourceDictionary/ ├── ViewModels/ ├── Views/ ├── Services/  ├── App.xaml ├── AssemblyInfo.cs

## 🚀 Getting Started

1. **Clone the repository**
    ```bash
    git clone https://github.com/yourusername/BankEase.git
    ```

2. **Open the solution in Visual Studio**  
   Make sure you have .NET 8 SDK installed.

3. **Run the application**  
   Press `F5` or use the "Start Debugging" option in Visual Studio.

## 🧪 Example Screenshots

> ![Homepage](https://github.com/user-attachments/assets/19fd6eeb-d252-4f6f-b0be-3d254eec3754)
> ![mainview](https://github.com/user-attachments/assets/359b4eb1-9581-4d69-afee-ab7e90453a8e)

## 📌 Notes

- All data is stored in local XML files. No external database is required.  
- Designed for educational or prototype purposes. For production use, consider switching to a secure, scalable database.

## 📄 License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

Feel free to fork, contribute, or suggest features. 💼
