# 3-Tier Architecture Code Generator (C# & SQL)

## 📌 Overview

A powerful desktop tool built with C# that automatically generates **SQL Stored Procedures, Data Access Layer, Business Layer, DTOs, and Enums** from a single SQL `CREATE TABLE` query.

The goal of this project is to **accelerate backend development** and enforce clean architecture by generating production-ready code in seconds.

---

## 🚀 Features

### 🔹 SQL Processing

* Parses `CREATE TABLE` queries using Regex
* Detects:

  * Columns
  * Data types
  * Primary keys
  * Foreign keys
  * Custom `FIND BY` logic

---

### 🔹 Code Generation

Automatically generates:

* ✅ Stored Procedures (CRUD)
* ✅ Data Access Layer (ADO.NET)
* ✅ Business Layer Classes
* ✅ DTO Classes
* ✅ Enum Types (for tinyint columns)

---

### 🔹 Smart Mapping

* SQL types → C# types
* `tinyint` → Enum conversion
* Nullable handling
* Reference table detection

---

## 🧠 Architecture

### 🔹 Parser Layer

* `CreateTableParser`
  Responsible for analyzing SQL and building a structured model of the table 

---

### 🔹 Context Layer

* `CodeGenerationContext`
  Stores all extracted metadata:
* Table
* Columns
* Enums
* Procedures
* Relationships

---

### 🔹 Generators

#### 🟢 Stored Procedure Generator

* Generates full CRUD SQL procedures automatically 

#### 🟢 Data Access Generator

* Produces ADO.NET code with:

  * SqlConnection
  * SqlCommand
  * SqlDataReader
* Handles null values and type mapping 

#### 🟢 Business Generator

* Generates business classes with:

  * CRUD operations
  * DTO mapping
  * Lazy loading for references 

#### 🟢 DTO Generator

* Builds DTO objects dynamically
* Maps SQL types to C# types
* Supports enum mapping

---

## ▶️ How to Use

1. Enter a SQL `CREATE TABLE` query
2. Click **Execute**
3. The tool generates:

   * SQL Procedures
   * Data Access Layer
   * Business Classes
   * DTOs
   * Enums

---

## ⚙️ Technologies Used

* C#
* .NET
* Windows Forms
* ADO.NET
* Regex
* OOP Principles

---

## 💡 Key Highlights

* Full automation of backend layers
* Clean separation of concerns
* Reusable and extensible architecture
* Reduces development time significantly

---

## 📸 Screenshots

<img width="1443" height="842" alt="Screenshot 2026-05-02 161850" src="https://github.com/user-attachments/assets/88d31855-79d5-432f-9429-40bda64e5f2c" />

---

## 👨‍💻 Author

Ahmed Ismail

---

## ⭐ Future Improvements

* Add UI for editing generated code
* Export to files (.cs, .sql)
