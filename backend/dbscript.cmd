docker-compose up -d

dotnet-ef database drop -f -c WriteDbContext 		-p .\src\Volunteers\AnimalVolunteer.Volunteers.Infrastructure\     -s .\src\Web
dotnet-ef database drop -f -c WriteDbContext		-p .\src\Species\AnimalVolunteer.Species.Infrastructure\    -s .\src\Web\
dotnet-ef database drop -f -c AuthDbContext		-p .\src\Accounts\AnimalVolunteer.Accounts.Infrastructure\    -s .\src\Web\


dotnet-ef migrations remove -c WriteDbContext 		-p .\src\Volunteers\AnimalVolunteer.Volunteers.Infrastructure\     -s .\src\Web
dotnet-ef migrations remove -c WriteDbContext 		-p .\src\Species\AnimalVolunteer.Species.Infrastructure\    -s .\src\Web
dotnet-ef migrations remove -c AuthDbContext		-p .\src\Accounts\AnimalVolunteer.Accounts.Infrastructure\    -s .\src\Web\


dotnet-ef migrations add Volunteers_init -c WriteDbContext 	-p .\src\Volunteers\AnimalVolunteer.Volunteers.Infrastructure\     -s .\src\Web
dotnet-ef migrations add Species_init -c WriteDbContext 	-p .\src\Species\AnimalVolunteer.Species.Infrastructure\    -s .\src\Web
dotnet-ef migrations add Accounts_init -c AuthDbContext		-p .\src\Accounts\AnimalVolunteer.Accounts.Infrastructure\    -s .\src\Web\

dotnet-ef database update -c WriteDbContext 		-p .\src\Volunteers\AnimalVolunteer.Volunteers.Infrastructure\     -s .\src\Web
dotnet-ef database update -c WriteDbContext 		-p .\src\Species\AnimalVolunteer.Species.Infrastructure\    -s .\src\Web
dotnet-ef database update -c AuthDbContext		-p .\src\Accounts\AnimalVolunteer.Accounts.Infrastructure\    -s .\src\Web\

pause