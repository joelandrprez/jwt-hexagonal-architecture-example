# 🧪 SimulaProBackend – Autenticación con JWT y Arquitectura Hexagonal

Este es un proyecto de ejemplo desarrollado en **.NET 8.0**, estructurado con una **arquitectura hexagonal (Ports and Adapters)**. Implementa un sistema básico de autenticación utilizando **JWT (JSON Web Tokens)** en una WEB API.

---

## 🔧 Especificaciones del Proyecto

- 🖥 **Tipo de aplicación:** Aplicación de consola
- 🛠 **Framework:** .NET 8.0
- 🧱 **Arquitectura:** Hexagonal (Domain, Application, Infrastructure, Adapters)
- 🔐 **Autenticación:** JWT
- 💻 **Sistema operativo destino:** Multiplataforma (sin restricción)

---

## 📁 Estructura del Proyecto

<pre> ```text SimulaProBackend/ ├── Domain/ # Entidades, interfaces y lógica de negocio ├── Application/ # Casos de uso, DTOs, servicios de aplicación ├── Infrastructure/ # Repositorios, proveedores externos, JWT ├── Adapters/ # Interfaces de entrada/salida (ej. consola, API) ├── Program.cs # Punto de entrada de la app ├── README.md └── .gitignore ``` </pre>

---

## 🚀 Cómo ejecutar el proyecto

1. Clona el repositorio:
   ```bash
   git clone https://github.com/tuusuario/jwt-hexagonal-auth-example.git
   cd jwt-hexagonal-auth-example

---


## 🗄 Restaurar Base de Datos

Para restaurar la base de datos del proyecto, debes ejecutar los scripts SQL ubicados en la ruta:


### 🛠 Pasos para restaurar (usando SQL Server Management Studio o CLI)

1. Abre SQL Server Management Studio (SSMS) y conéctate a tu instancia.
2. Crea una nueva base de datos (por ejemplo: `SimuladorDB`).
3. Ejecuta los archivos SQL en el siguiente orden:
   - `SCHEMA_00.sql`
   - `DATA_00.sql`
   - `SP_00.sql`

> ⚠️ Asegúrate de ejecutar los scripts en ese orden para evitar errores de dependencia entre tablas, datos y procedimientos.

---

### 🧪 Alternativa: ejecutar con SQLCMD (opcional)

Si tienes `sqlcmd` instalado, puedes hacerlo por línea de comandos:

```bash
sqlcmd -S .\SQLEXPRESS -d SimuladorDB -i "Simulador.Backend.Infrastructure\SQL\SCHEMA_00.sql"
sqlcmd -S .\SQLEXPRESS -d SimuladorDB -i "Simulador.Backend.Infrastructure\SQL\DATA_00.sql"
sqlcmd -S .\SQLEXPRESS -d SimuladorDB -i "Simulador.Backend.Infrastructure\SQL\SP_00.sql"


