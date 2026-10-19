# UniversidadAPI

Proyecto base ASP.NET Core Web API para .NET 8, basado en el diagrama de clases de Curso, Estudiante, Docente y Asignacion.

## Incluye
- Modelos: Curso, Estudiante, Docente y Asignacion.
- AppDbContext de Entity Framework Core.
- Un controlador CRUD por cada modelo.
- Swagger/OpenAPI para probar los endpoints.
- SQL Server LocalDB como cadena de conexión predeterminada.

## Antes de ejecutar
1. Abre `UniversidadAPI.csproj` en Visual Studio 2022.
2. Verifica que tengas instalado el SDK de .NET 8.
3. Si no tienes SQL Server LocalDB, cambia `ConnectionStrings:Conexion` en `appsettings.json` por la cadena de conexión de tu instancia de SQL Server.
4. Restaura los paquetes NuGet.
5. En Visual Studio, abre **Tools > NuGet Package Manager > Package Manager Console** y selecciona `UniversidadAPI` como proyecto predeterminado.
6. Ejecuta:
   ```powershell
   Add-Migration CrearTablas
   Update-Database
   ```
7. Ejecuta con F5. Si Swagger no abre automáticamente, utiliza la dirección HTTPS que muestra Visual Studio y agrega `/swagger`.

## Endpoints
- `/api/Curso`
- `/api/Estudiante`
- `/api/Docente`
- `/api/Asignacion`

Cada controlador tiene GET (lista), GET por id, POST, PUT y DELETE.

## Nota
La cadena de conexión incluida usa SQL Server LocalDB. Puede requerir ajuste según la instalación de SQL Server del equipo. Las migraciones y la ejecución real deben verificarse en Visual Studio en el equipo de destino.
