# RaceDay Event Management System — Part 2

## Source of truth
This implementation is based on the supplied RaceDay Part 1 SQL script, ERD and API Endpoint Plan. The six database tables are `Users`, `Events`, `Categories`, `Enrolments`, `Results` and `Payments`. The Part 1 endpoint plan intentionally has no Payments endpoints.

**Authentication decision must be confirmed:** the endpoint plan says login returns a JWT, while the lecturer guide discussed server-side sessions. This starter's current controller/base-controller implementation uses server-side ASP.NET Core session cookies to follow the guide. It does **not** implement JWT yet. Do not claim JWT support in a submission unless you convert the authentication and authorization implementation to JWT and update tests/docs.

## Prerequisites
- Visual Studio 2022 (purple icon), with **ASP.NET and web development** workload
- .NET 8 SDK
- SQL Server Express or LocalDB
- SQL Server Management Studio (SSMS)
- Git for Windows (for your own repository push)

## Open in Visual Studio
1. Extract the ZIP.
2. Open `RaceDay_Part2/RaceDay.sln` using **File → Open → Project/Solution**.
3. Wait for NuGet restore.
4. Build using **Build → Build Solution**.

## SQL Server connection
Edit `RaceDay.Api/appsettings.json`. The included example uses SQL Server Express:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.\\SQLEXPRESS;Database=RaceDayDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
}
```

If using LocalDB, use:
`Server=(localdb)\\MSSQLLocalDB;Database=RaceDayDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true`

Check the actual instance name in SSMS; do not guess. Never commit real passwords or production secrets.

## Entity Framework Core migrations (Code First)

The project now includes an initial migration in `RaceDay.Api/Migrations/20261014090000_InitialRaceDayDatabase.cs`. It creates the six tables from the Part 1 SQL design (`Users`, `Events`, `Categories`, `Enrolments`, `Results`, and `Payments`), including the main foreign keys, unique indexes, check constraints and defaults.

Open **Tools → NuGet Package Manager → Package Manager Console**. Set **Default project** to `RaceDay.Api`, then run:

```powershell
Update-Database
```

Alternatively, open a terminal in the solution folder and run:

```powershell
dotnet restore
dotnet ef database update
```

If `dotnet ef` is not installed, install the matching .NET 8 tool:

```powershell
dotnet tool install --global dotnet-ef --version 8.0.11
```

Make sure the connection string in `RaceDay.Api/appsettings.json` points to your SQL Server instance before applying the migration. EF migrations create/update the database; do not also run `RaceDay_Database.sql` against the same database unless you deliberately intend to drop and recreate it. The supplied SQL script begins by dropping `RaceDayDB`, so do not run it on a database with data you need to keep.

**Important:** this environment does not have the .NET SDK or SQL Server installed, so I could not compile the project, apply the migration, or run tests here. After extracting the ZIP, run `dotnet build` and `dotnet test` locally and review any errors before submission.

## Run the API and Swagger
1. Set `RaceDay.Api` as Startup Project.
2. Press F5.
3. Open the URL shown in the console/output with `/swagger` appended.
4. Register an Organiser, log in, create an event and category; register a Participant, log in, enrol; log in as the Organiser to record a result; log in as the Participant to view the result.
5. Session cookie must be retained between requests when using this session implementation.

## Endpoint plan
The implemented controller routes are intended to cover:
- `POST /api/auth/register`, `POST /api/auth/login`, `POST /api/auth/logout`
- `GET/PUT /api/users/me`
- `GET /api/events`, `GET /api/events/{id}`, `POST/PUT/DELETE /api/events/{id}`
- `GET/POST /api/events/{eventId}/categories`, `PUT/DELETE /api/categories/{id}`
- `POST /api/enrolments`, `GET /api/enrolments/me`, `GET /api/events/{eventId}/enrolments`, `DELETE /api/enrolments/{id}`
- `POST /api/enrolments/{enrolmentId}/results`, `PUT /api/results/{id}`, `GET /api/results/me`, `GET /api/events/{eventId}/results`

Check each route in Swagger. Do not assume every endpoint passes until you have run it locally. Organisers must only change their own events/categories/results; Participants must only see/cancel their own enrolments and view their own results. Category must belong to the selected event.

## Run tests
From the solution root:
```powershell
dotnet restore
dotnet build
dotnet test
```
In Visual Studio, use **Test → Test Explorer → Run All Tests**. The included tests are starter tests; add and run HTTP integration tests for successful registration/login, duplicate email, invalid credentials, unauthenticated requests, role rejection, event ownership, duplicate enrolment, category/event mismatch and result creation before submission.

## Git install and push to your existing repository
Check installation:
```powershell
git --version
```
If missing, install Git for Windows from https://git-scm.com/download/win, then restart Visual Studio.

Set identity once (replace examples):
```powershell
git config --global user.name "Your Name"
git c
```

Open **View → Terminal** in Visual Studio and `cd` to the extracted `RaceDay_Part2` folder. Check whether it is already a Git repository:
```powershell
git status
```
If not a repository:
```powershell
git init
git add .
git status
git commit -m "Initial RaceDay Part 2 API"
```
Before committing, ensure no secrets or local credentials are staged.

Copy HTTPS URL from your already-created GitHub repository, then run (replace URL):
```powershell
git remote add origin https://github.com/YOUR-USERNAME/YOUR-REPOSITORY.git
git branch -M main
git push -u origin main
```
If `origin` already exists, inspect it with `git remote -v`; correct it using:
```powershell
git remote set-url origin https://github.com/YOUR-USERNAME/YOUR-REPOSITORY.git
git push -u origin main
```

For later changes:
```powershell
git status
git add .
git commit -m "Describe the change made"
git push
```

## GitHub Actions / CI
After pushing, open the repository's **Actions** tab. Inspect the workflow result. A green workflow run is the evidence that the configured build/tests succeeded. If it fails, read the log, fix locally, run `dotnet test`, commit and push the correction. The student is responsible for the repository, commit history, screenshot/evidence, video and final submission.

## Rubric completion checklist
- [ ] Correct project and deliverables organised
- [ ] GitHub repository and meaningful commits
- [ ] GitHub Actions build/test passes
- [ ] Secure password hashing and required authentication method
- [ ] Role-based access and ownership checks
- [ ] All Part 1 planned endpoints tested .
- [ ] Six tables match Part 1 SQL/ERD, including Payments
- [ ] EF Core migration created and applied 
- [ ] Swagger tested
- [ ] Automated tests cover success and failure cases
- [ ] README and setup instructions complete
- [ ] Student records and submits their own presentation/video and GitHub evidence




YOUTUBE LINK 
https://youtu.be/F8c5yTN45Mo

