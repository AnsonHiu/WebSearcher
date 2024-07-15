# WebSearcher
An application with 2 supporting project libraries to query the internet that returns matching urls.

> [!WARNING]
> - If using Google's custom search Api note that the api has a limit of 10,000 queries per day. Once reached a "Too Many Requests" error will return.
> - Update the query property under [KeywordSearchAndFilterService](Domain/Services/KeywordSearchAndFilterService.cs), e.g. `var query = new KeywordSearchQuery(keyword, 5)` to take in a max count can limit the number of queries sent per search (default is 100)


## Logging
Log file location can be found under `{ProjectDirectory}/App_Data/Logs`


## Project Structure
Project structure loosely follows clean architecture with MVVM
- https://medium.com/@ajliberatore/android-clean-architecture-mvvm-4df18933fa9
- https://github.com/jasontaylordev/CleanArchitecture

The project dependencies flow downwards, connected by interfaces.
Each project also manages their own dependencies, which are all ultimately called in Application Startup.

### Application
The Application project implements the MVVM model and deals with UI logic

### Domain
The Domain project contains use cases used by the application project (business logic)
- the services under domain creates separation between business logic and actions (CQRS). 
- This makes the actions (CQRS layer) reusable by different business logic.

### Data
The Data project deals with retrieving data (usually from the database, or calling external sources to return data used for calculation)

## Outstanding issues
- [ ] Setup AWS Secrets Manager for sensitive config data,

## Possible Improvements
 - [ ] Implement GoogleApiFactory to return a singleton ApiService instance
 - [ ] Adding cancel button (cancellation token)
 - [ ] Loading icon on search
 - [ ] Bind Enter key to search
 - [ ] Consider using State pattern for search button, but could be over engineered
 - [ ] Search button can use a spinning logo while loading