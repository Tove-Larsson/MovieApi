# MovieApi

ASP.NET Core Web API (.NET 10) with Entity Framework Core and SQL Server, built as Övning 3 in the .NET course. The API manages movies, their details, reviews and actors.

## Tech stack

- ASP.NET Core Web API (controllers, OpenAPI)
- Entity Framework Core (code first, migrations)
- SQL Server (database name: `MovieDb`)

## Getting started

1. Clone the repository
2. Check the connection string in `appsettings.json` and change it to match your SQL Server instance.
3. Apply the migrations:

```bash
dotnet ef database update
```

   (or `Update-Database` in the Package Manager Console)

4. Run the project and open the OpenAPI page, or test with Postman. Local URL: `https://localhost:7041`

The database is seeded with three movies (The Lord of the Rings trilogy) with details, reviews, actors and actor roles. The seed is defined with `HasData` in `MovieApiContext`.

## Data model

- **Movie**: Id, Title, Year, Genre, Duration
- **MovieDetails**: Id, Synopsis, Language, Budget (1:1 with Movie)
- **Review**: Id, ReviewerName, Comment, Rating (1:M with Movie)
- **Actor**: Id, Name, BirthYear
- **MovieActor**: MovieId, ActorId, Role (join entity for the N:M relation between Movie and Actor, composite key via `[PrimaryKey]`)

## Endpoints

### Movies

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/movies` | List movies. Optional filters: `?genre=Fantasy&year=2001` |
| GET | `/api/movies/{id}` | Get one movie |
| GET | `/api/movies/{id}/details` | Movie with details, actors and reviews |
| POST | `/api/movies` | Create a movie |
| PUT | `/api/movies/{id}` | Update a movie |
| DELETE | `/api/movies/{id}` | Delete a movie |

### Actors

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/actors` | List actors |
| GET | `/api/actors/{id}` | Get one actor |
| POST | `/api/actors` | Create an actor |
| PUT | `/api/actors/{id}` | Update an actor |
| DELETE | `/api/actors/{id}` | Delete an actor |
| POST | `/api/movies/{movieId}/actors/{actorId}` | Link an existing actor to a movie (no role), leaving this for the assignment |
| POST | `/api/movies/{movieId}/actors` | Link an existing actor to a movie with a role (body below) |

Example body for `POST /api/movies/{movieId}/actors`:

```json
{
  "actorId": 4,
  "role": "Main antagonist"
}
```

### Reviews

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/movies/{movieId}/reviews` | List reviews for a movie |
| POST | `/api/movies/{movieId}/reviews` | Add a review to a movie |
| GET | `/api/reviews` | List all reviews (extra) |
| GET | `/api/reviews/{id}` | Get one review (extra) |
| PUT | `/api/reviews/{id}` | Update a review (extra) |
| DELETE | `/api/reviews/{id}` | Delete a review |

## DTOs and validation

The API uses DTOs instead of exposing the entities: `MovieDto`, `MovieDetailDto`, `MovieCreateDto`, `MovieUpdateDto`, `ActorDto`, `ActorCreateDto`, `ActorUpdateDto`, `ReviewDto`, `ReviewCreateDto`, `ReviewUpdateDto` and `MovieActorCreateDto`.

Validation uses data annotations (`[Required]`, `[Range]`, `[MaxLength]`), for example:

- Movie year between 1888 and 2026, duration between 1 and 1000
- Review rating between 1 and 5
- `ActorId` must be 1 or higher and `Role` is required (max 100 characters) when linking an actor with a role

## Status codes

- `200 OK`: successful GET
- `201 Created`: resource created (POST)
- `204 No Content`: successful PUT/DELETE, or actor linked to a movie
- `400 Bad Request`: validation errors
- `404 Not Found`: the movie, actor or review does not exist
- `409 Conflict`: the actor is already linked to the movie

## Not implemented extra things

- Filtering movies by actor name (`?actor=`)
- Reports with LINQ queries (`ReportsController`)
- Director, Genre and Country relations
- Fluent API configuration for `MovieActor` (the attribute is used instead)
- Seeding through `Extensions/SeedData` (`HasData` is used instead)
