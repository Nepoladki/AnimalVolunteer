docker-compose up -d

dotnet-ef database drop -f -c WriteDbContext 		-p .\src\Volunteers\AnimalVolunteer.Volunteers.Infrastructure\     -s .\src\Web
dotnet-ef database drop -f -c WriteDbContext 		-p .\src\Species\AnimalVolunteer.Species.Infrastructure\    -s .\src\Web\


dotnet-ef migrations remove -c WriteDbContext 		-p .\src\Volunteers\AnimalVolunteer.Volunteers.Infrastructure\     -s .\src\Web
dotnet-ef migrations remove -c WriteDbContext 		-p .\src\Species\AnimalVolunteer.Species.Infrastructure\    -s .\src\Web


dotnet-ef migrations add Volunteers_init -c WriteDbContext 	-p .\src\Volunteers\AnimalVolunteer.Volunteers.Infrastructure\     -s .\src\Web
dotnet-ef migrations add Species_init -c WriteDbContext 	-p .\src\Species\AnimalVolunteer.Species.Infrastructure\    -s .\src\Web


dotnet-ef database update -c WriteDbContext 		-p .\src\Volunteers\AnimalVolunteer.Volunteers.Infrastructure\     -s .\src\Web
dotnet-ef database update -c WriteDbContext 		-p .\src\Species\AnimalVolunteer.Species.Infrastructure\    -s .\src\Web


pause