# TareaEFJ

Proyecto en **.NET 9**, con **PostgreSQL** y **pgAdmin**, todo orquestado con **Docker Compose**.  
La aplicación implementa una arquitectura limpia con capas (`Api`, `Application`, `Infrastructure`, `Domain`) y utiliza **Entity Framework Core** para la persistencia.

---

## 🏛️ Modelo de dominio (Entidades principales)

El proyecto implementa un esquema jerárquico de **países, regiones, ciudades, empresas y sucursales**, donde cada entidad se relaciona de manera clara:

---

### 🌍 Country  
Representa un país.  
- `Id` (Guid) → Identificador único  
- `Name` (string) → Nombre del país  
- Relación: Un país tiene muchas **Regions**

---

### 🗺️ Region  
Representa una división administrativa dentro de un país (ejemplo: departamento, estado, provincia).  
- `Id` (Guid) → Identificador único  
- `Name` (string) → Nombre de la región  
- `CountryId` (Guid) → Relación con su país  
- Relación: Una región tiene muchas **Cities**

---

### 🏙️ City  
Representa una ciudad dentro de una región.  
- `Id` (Guid) → Identificador único  
- `Name` (string) → Nombre de la ciudad  
- `RegionId` (Guid) → Relación con su región  
- Relación: Una ciudad puede tener varias **Branches**

---

### 🏢 Company  
Representa una empresa registrada en el sistema.  
- `Id` (Guid) → Identificador único  
- `Name` (string) → Nombre de la empresa  
- `Nit`
- `Address`
- `Email`
- Relación: Una empresa tiene muchas **Branches**

---

### 🏬 Branch  
Representa una sucursal de una empresa en una ciudad.  
- `Id` (Guid) → Identificador único  
- `NumeroComercial` 
- `Address`
- `Email`
- `ContactName`
- `Phone`
- `CompanyId` (Guid) → Relación con la empresa a la que pertenece  
- `CityId` (Guid) → Relación con la ciudad donde se ubica  

---

## 🚀 Requisitos

- [Docker](https://docs.docker.com/get-docker/)  
- [Docker Compose](https://docs.docker.com/compose/)  
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) (solo si vas a compilar fuera de contenedor)

---

## 🛠️ Estructura del proyecto
TareaEFJ/
│── Api/
│ ├── Api.csproj
│ └── Api.Dockerfile
│── Application/
│── Domain/
│── Infrastructure/
│── docker-compose.yml
└── README.md
---

## 🐳 Levantar los contenedores

Ejecutar desde la raíz del proyecto:

```bash
docker compose up -d --build

👩‍💻 Autor

Juliana Pallares
Proyecto académico / personal de aprendizaje en .NET, PostgreSQL y Docker.
