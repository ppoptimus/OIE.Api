dotnet restore

dotnet ef migrations add InitialIdentity -c ApplicationIdentityDbContext -p ../Infrastructure/Infrastructure.csproj -s KTZ.Api.csproj -o ../Infrastructure/Identity/Migrations

rm -rf ../Infrastructure/data/Migrations
dotnet ef migrations add InitialData -c ApplicationDbContext -p ../Infrastructure/Infrastructure.csproj -s KTZ.Api.csproj -o ../Infrastructure/data/Migrations


#migration data application context
./re-migrate.sh


<!-- dotnet ef database update -c catalogcontext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj
dotnet ef database update -c appidentitydbcontext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj

dotnet ef migrations add {MigrationName}
dotnet ef migrations remove
dotnet ef database update 

dotnet ef migrations add InitialIdentity -c ApplicationIdentityDbContext -p ../Infrastructure/Infrastructure.csproj -s KTZ.Api.csproj -o ../Infrastructure/Identity/Migrations

dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Migrations/IdentityServer/ConfigurationDb
dotnet ef migrations add InitialIdentityServerDbMigration -c ApplicationDbContext -o Migrations/IdentityServer/ApplicationDb -->



New
    LANGUAGE
    PUBLISH
        START
        EXPIRE
    CONTENT
        TITLE
        TEXT
        PICTURE
    SHOW OPTION
        MODULE (Front Page, Thai equity, Delivertives, ...)

STATIC Content
    MODULE (Front Page, Thai equity, Delivertives, ...)
    TH.SHOW
    TH.TITLE
    TH.LINK
    TH.CONTENT

    EN.SHOW
    En.TITLE
    EN.LINK
    EN.CONTENT

    
New Media
Traning



MENU
    ID
    MODULE
    LINK
    LINK-TYPE เชื่อมไปไหนเช่น Html Content


Html Content
    ID