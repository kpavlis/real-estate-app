# 🏠 Real Estate App

**Find, list, and manage properties with ease — built for the Greek real estate market.**

> ℹ️ This project is not open source and does not grant any usage rights.
> For usage terms and legal information, see [Code Ownership & Usage Terms](#-code-ownership--usage-terms).

## 📘 Overview

**Real Estate Management App**, a desktop application designed for Greek-speaking users to manage, browse, and interact with property listings for sale and rent. Whether you're a buyer, seller, or agent, this app makes real estate simple and intuitive! It simulates the functionality of a full-featured real estate platform by running entirely on a local database—making it ideal for offline use, development testing, and showcasing features in controlled environments. 💼

## ✨ Features

- 🏠 **Home Dashboard** with welcome message and FAQs
- 🔍 **Search Properties** by area, size, bedrooms, and price
- 🛒 **Buy or Rent** directly from listings
- 📝 **Add New Listings** with images and detailed descriptions
- ✏️ **Edit Listings**
- 🗑️ **Delete Properties** with admin controls
- 📊 **View Statistics & Reports** for property insights
- 📂 **Rental Application Archive** for tracking interest
- 📞 **Contact Owners** via phone or email
- 🔐 **Login/Register** for personalized access

## 🛠️ Technologies Used

- WindowsAppSDK / WinUI3
- XAML for the UI
- C# for Business Logic
- SQLite for the local database

## 🎯 Purpose

The purpose of this application is to provide a streamlined and intuitive platform for managing real estate listings in Greece. It empowers users to browse, register, edit, and remove properties with ease, while offering essential tools for agents and administrators to handle property data efficiently. Designed with clarity and simplicity in mind, the app bridges the gap between property owners and potential buyers or renters. **It is developed solely for academic and research purposes.**

## 🧰 Prerequisites

Before building and running this application, ensure you have the following installed:

- **Windows 10 version 1809 or later** (Windows 11 recommended)
- **Visual Studio 2022** (version 17.1 or newer)
- Installed Workloads:
  - .NET Desktop Development
  - Windows App SDK C# Templates
- **.NET SDK** (version 6)
- **Developer Mode** enabled in Windows

## 📦 Installation

Follow these steps to install and run the application:

1. **Clone the repository (or download and decompress the ZIP file)**
   ```bash
   git clone https://github.com/kpavlis/real-estate-app.git
   cd real-estate-app
   
2. **Open the project in Visual Studio 2022** using the `.sln` file
3. **Confirm that the following NuGet packages are installed:**
    - Microsoft.WindowsAppSDK (version **10.0.26100.x**)
    - Microsoft.Windows.SDK.BuildTools (version **1.8.x**)
    - System.Data.SQLite (version **2.x.x**)
    - SQLitePCLRaw.bundle_e_sqlite3 (version **3.x.x**)
4. **Verify Target Framework**
     In your `.csproj` file, ensure the framework is set correctly:
   
     ```xml
     <TargetFramework>net6.0-windows10.0.19041.0</TargetFramework>
   
6. **Run the application** as _Unpackaged app_


## 📷 Screenshots / Video

**_App Screens:_**
> <img width="250" height="160" alt="Soft_Tech_1" src="https://github.com/user-attachments/assets/39dc19de-12d5-4d31-af0f-2ad7331ab820" />
> <img width="250" height="160" alt="Soft_Tech_2" src="https://github.com/user-attachments/assets/15e8f41a-d204-4ae6-8831-84f8d3360b7a" />
> <img width="250" height="160" alt="Soft_Tech_3" src="https://github.com/user-attachments/assets/845a4245-1bff-42be-ae1a-aadb26c7f039" />
> <img width="250" height="160" alt="Soft_Tech_4" src="https://github.com/user-attachments/assets/37fa6197-ee20-4708-b555-7aec7ef34419" />
> <img width="250" height="160" alt="Soft_Tech_5" src="https://github.com/user-attachments/assets/06b85138-a164-4944-959b-52a1e4a5e5da" />
> <img width="250" height="160" alt="Soft_Tech_6" src="https://github.com/user-attachments/assets/a6b74ac2-215b-4067-a1ac-e4a4b1212d31" />
> <img width="250" height="160" alt="Soft_Tech_7" src="https://github.com/user-attachments/assets/beaee7a7-2277-422e-9825-78d5531538d0" />

**_Demo Video:_**

> https://github.com/user-attachments/assets/5742128c-de9b-486d-9f26-a01c2c0efe3c

# 🔒 Code Ownership & Usage Terms

This project was created and maintained by:

- Konstantinos Pavlis (@kpavlis)
- Theofanis Tzoumakas (@theofanistzoumakas)
- Matina Papadakou (@matinapap)
- Sotiria Lamprinidou (@SotiriaLamprinidou)

🚫 **Unauthorized use is strictly prohibited.**  
No part of this codebase may be copied, reproduced, modified, distributed, or used in any form without **explicit written permission** from the owners.

Any attempt to use, republish, or incorporate this code into other projects—whether commercial or non-commercial—without prior consent may result in legal action.

For licensing inquiries or collaboration requests, please contact via email: konstantinos1125 _at_ gmail.com .

© 2025 Konstantinos Pavlis, Theofanis Tzoumakas, Matina Papadakou, Sotiria Lamprinidou. All rights reserved.
