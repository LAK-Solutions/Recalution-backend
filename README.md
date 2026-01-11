# Recalution

**Recalution** is a minimal flash-card learning app focused on clarity, speed, and full freedom—without subscriptions, paywalls, or unnecessary complexity.

## Problem We Are Solving
- Existing apps are overloaded (e.g., Anki learning curve)
- Others lock basic behavior behind payment (e.g., Quizlet)
- Users who just want to create cards and repeat them are forced into complex systems

We want control, simplicity, and ownership for our users.

## Goals
- Users can create flash cards and review them repeatedly in a simple, predictable way  
- Users can start using the app as a named profile to associate their flash cards and progress  
- Minimal feature surface: no settings pages, no modes, no plugins  
- Personal usability: the app is good enough for the creators to use daily for real learning  

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- PostgreSQL database

### Setup

1. Navigate to the Infrastructure project folder:
```bash
cd Recalution.Infrastructure
```

2. Apply migrations and update the database:
```bash
dotnet tool run dotnet-ef database update
```

3. To create a new migration:
```bash
dotnet ef migrations add InitialCreate
```

4. To check code formatting:
```bash
dotnet format
```

## Contributing

We use **type-based branching** to keep development organized and coherent.

### Branch Naming
Use the format: `<type>/<issue-number>-<short-description>`

**Type options:**
- `feature` → new features
- `bugfix` → bug fixes
- `chore` → maintenance tasks

**Rules:**
1. Use lowercase letters only.  
2. Separate words with hyphens (`-`).  
3. Keep names concise and descriptive.

**Examples:**  
`feature/12-add-transaction-crud`  
`bugfix/34-fix-budget-validation`  
`hotfix/45-correct-login-error`  
`chore/78-update-dependencies`  

### Workflow
1. Make sure there is a GitHub issue for your work.
2. Create a new branch from `dev`:
```bash
git checkout dev
git pull
git checkout -b issue-12/add-transaction-crud
```
3. Commit your changes with clear messages:
```
<type>(<scope>): <description>
```
4. Push the branch and open a Pull Request linking the issue (e.g., `Closes #12`).
5. Wait for review and approval before merging.
