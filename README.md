# Instrucciones para Levantar el Proyecto

Este documento tiene como propósito establecer los pasos necesarios para levantar el proyecto.

## Paso 1: Generar una Key de GitHub

Para aumentar la capacidad máxima de los requests hacia la API de GitHub, es necesario generar un token personal de acceso (PAT). Puedes seguir esta [guía oficial de GitHub](https://docs.github.com/es/authentication/keeping-your-account-and-data-secure/managing-your-personal-access-tokens) para crear tu token.

Una vez que hayas generado el token, debes colocarlo en el archivo `appSettings` que se encuentra en la raíz del proyecto.

## Paso 2: Configurar la Base de Datos

1. Genera una base de datos vacía.
2. Obtén la cadena de conexión (connection string) de tu base de datos.
3. Reemplaza la cadena de conexión en el archivo `appSettings` con la tuya.

## Paso 3: Instalar Paquetes y Compilar el Proyecto

Para poder ejecutar las herramientas de Entity Framework (EF), es necesario instalar los paquetes y compilar el proyecto. A continuación, se explica cómo realizar una migración y actualización con Entity Framework. Si ya sabes cómo realizar estos pasos, puedes saltar a la siguiente sección.

1. Navega a la ruta del proyecto donde se encuentra el archivo `UxDebt.csproj`.
2. Ejecuta los siguientes comandos en la consola:

    ```sh
    cd "ruta/donde/se/encuentre/UxDebt.csproj"
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```
