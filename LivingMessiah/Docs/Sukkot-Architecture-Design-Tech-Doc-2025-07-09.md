# Sukkot Architecture Design Tech Doc
- ver 1,  2025-07-09

## Goal 
As I'm going through the process of upgrading and re-designing the LMM Web app (formerly known as `LivingMessiahBlazor.sln` and currently known as `LivingMessiah.sln`) 
a big challenge is to deal with Sukkot as a separate project / web app (e.g. Sukkot.LivingMessiah.com) eventually leaving LivingMessiah.com as a very static PWA that leverage the voluminous 'meta' SmartEnum<Foo> classes that lend itself to a PWA.
That means that the CRUD of Sukkot Registration is moved to `SukkotRegistration.csproj`

# Envisioned New Projects 
### 1. `LivingMessiahPWA.csproj`
a PWA that gets the most use year round, designed to be quick, informative and clean and able to run off-line
- **Special Events** are gotten from (i.e. readonly) via Azure Functions. maybe this is cached ... whatever...
- Currently the .Net 9 version is `LivingMessiah.csproj`

#### Events happening during Sukkot
- I don't think I want a separate app for this, it should be part of `LivingMessiahPWA.csproje`


### 2. `LivingMessiahAdmin.csproj`
This will do CRUD like management of everything that the whole system of what `LivingMessiah.com` requires.  

Designed for `admin` roles et. al.

Here are the related Features.

- **KeyDates**: This is an annual event sometime shortly after Sukkot where the calendar gets re calibrated.  There are many 'meta' SmartEnum<Foo> classes that have a dependency on this 30+ rows found in db table `LivingMessiah.KeyDate.Date`
- **SpecialEvents**: As non `Upcoming Feast` events comes along, they need to maintain so do them here
- **SukkotAdmin**: CRUD related stuff to maintain the Sukkot Registration process for the roles of `superuser` or `sukkot`
- **Contacts**: After each sukkot, a download of contact info need to be capture

### 3. Sql Server SDK database projects
- Maybe look at <mark>**Data API Builder**</mark>

#### 3.1 `SukkotDatabase.csproj`

#### 3.2 `SukkotDatabase.csproj`

### 4. `WindmillRanch.csproj`
- Url: WindmillRanch.LivingMessiah.com
- PWA, probably not because I only want one PWA
- Manage videos from the `BiblicalPermaculture` YouTube/Rumble channel
- Holds content for stuff going on at the WindmillRanch

# Sukkot Registration Details 
I'm getting a bug that saying a user is logged in but there email has not been verified but in fact it has.
I need to document how this process is currently working and see in the `RegistrationSteps` and the related Services makes this determination 

## Services
| Class                | Description |
|----------------------|-------------|
| Auth0                | constants            |
| DTO                  | Sukkot stuff
| Service              | see below   |
| SukkotExceptions     | see below   |
| SukkotService        | see below   |


#### `DTO.cs` v. `EntryFormVM`


![alt text](DTO-v-EntryFormVM.jpg)

#### `SecurityClaimsService.cs`
- GetEmail();
- GetRoles();
- GetUserName();
- IsUserAuthorized(string registrationEmail);
- RoleHasAdminOrSukkot();
- AdminOrSukkotOverride();
- IsVerified();

#### `Service.cs`
- this is to generic

```csharp
public interface IService
{
  string ExceptionMessage { get; set; }
  Task<EntryFormVM> GetById(int id);  
  Task<(int NewId, int SprocReturnValue, string ReturnMsg)> Create(EntryFormVM registration);
  Task<(int RowsAffected, int SprocReturnValue, string ReturnMsg)> Update(EntryFormVM registration);
}
```

#### `SukkotExceptions.cs`
```csharp
namespace  LivingMessiah.Features.Sukkot.Services;

RegistrationNotFoundException

UserNotAuthoirizedException

PaymentSummaryException

RegistratationException

StatusException

```

#### `SukkotService.cs`
```csharp
public interface ISukkotService
{
  string UserInterfaceMessage { get; set; }
  Task<vwRegistration> Details(int id, ClaimsPrincipal user, bool showPrintInstructionMessage = false);
  Task<vwRegistration> DeleteConfirmation(int id, ClaimsPrincipal user);

  Task<int> DeleteConfirmed(int id);
  Task<RegistrationSummary> Summary(int id, ClaimsPrincipal user);
  Task<IndexVM> GetRegistrationStep();
}
```
