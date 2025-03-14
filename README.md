# mail_API Project

## Overview

The `mail_API` project is an ASP.NET Core Web API designed to manage Brazilian postal code (CEP) information, using a external API. The API provides endpoints to fetch details of a specific CEP and to insert new address data by CEP.

## Endpoints

### GET /api/cep/{cep}

Fetches information for a specific CEP.

- **Summary**: Get information for a specific CEP
- **Responses**:
  - `200 OK`: Returns the CEP data.
  - `404 Not Found`: CEP not found.
  - `500 Internal Server Error`: Internal error occurred.

**Example Request**:
 GET /api/cep/01001000
```JSON
{
  "cep": "01001-000",
  "logradouro": "Praça da Sé",
  "complemento": "lado ímpar",
  "bairro": "Sé",
  "localidade": "São Paulo",
  "uf": "SP",
  "ibge": "3550308",
  "gia": "1004",
  "ddd": "11",
  "siafi": "7107"
}

```
### POST /api/cep
Inserts a new address by CEP.

**Summary**: Insert a new address by CEP

- **Responses**:
  - `200 OK`: Data successfully inserted.
  - `404 Not Found`: Invalid CEP.
  - `500 Internal Server Error`: Internal error occurred while saving data.
  
```JSON
{
  "message": "Data successfully saved"
}
```

### Getting Started

- Clone the repository:
```bash
git clone https://github.com/your-username/mail_API.git
cd mail_API
Set up the database connection:
Add your database connection string to the configuration file (e.g., appsettings.json).
```

📝 Note: This project also includes API documentation with Swagger, which is a handy tool for visualizing and testing the API. 😊
