docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Password1" -p 1433:1433 -d mcr.microsoft.com/mssql/server

dotnet ef dbcontext scaffold "Server=localhost;Database=pitter;User Id=sa;Password=Password1;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -c PitterContext -o Models
dotnet ef database update

select post_id, username, date, content from content.repitts inner join content.Posts on post_id=Posts.ID inner join users.Users on Posts.User_ID=Users.ID;
