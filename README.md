Este documento tiene como proposito establecer los pasos para levantar el proyecto.

Como primer paso hay que generar una Key de git para aumentar la capicidad maxima de los request hacia la API de Git adjunto link de guia.
https://docs.github.com/es/authentication/keeping-your-account-and-data-secure/managing-your-personal-access-tokens

Una vez tengamos el Token generado debemos colocarlo en el appSetting que se encuentra en el root del proyecto.

Generar una base de datos vacia, obtener el conecction string y pisar el conecction string del appSetting con el suyo.

Como tercer paso debemos instalar los paquetes y compilar el proyecto para poder ejecutar las herramientas de Entity Framework (EF). (a continuacion se explicara como hacer un migration y update con entity framework si usted sabe como realizar estos pasos puede saltear la siguiente seccion)
Buscar ruta del proyecto donde este el archivo: UxDebt.csproj
Ejecutar en consola los siguientes comandos:

cd "ruta de donde se encuentre UxDebt.csproj"
dotnet ef migrations add InitialCreate
dotnet ef database update
