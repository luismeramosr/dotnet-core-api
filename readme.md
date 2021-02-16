Ejecuta dotnet restore
Cambiar appsettings.json con tu cadena
dotnet ef dbcontext scaffold "Server=localhost;Port=3307;Database=DB_PAMYS;User=root;Password=root;TreatTinyAsBoolean=true;" "Pomelo.EntityFrameworkCore.MySql" -o Models