# BlogSystem

A simple **Blog API** (Posts + Comments) built with **.NET** to practice and demonstrate **Clean Architecture**, **CQRS (MediatR)**, and **Async / Background Job patterns** — inspired by a hands-on backend engineering learning track.

---

## Features

- **Posts & Comments** — basic CRUD via CQRS commands (MediatR)

- **Clean Architecture** — strict separation between Domain, Application, Infrastructure, and API layers

- **Async all the way down** — every I/O call uses `async`/`await` with `CancellationToken` propagated end-to-end

- **In-process background lane** — `System.Threading.Channels` + `BackgroundService` for fast, volatile background work

- **Durable job lane** — [Hangfire](https://www.hangfire.io/) backed by SQL Server for retries, persistence, and a monitoring dashboard

- **Idempotent job design** — background jobs check current state before acting, safe to retry

---

## Architecture

```

BlogSystem/

├── BlogSystem.Domain/           # Entities only — zero dependencies

│   └── Entities/

│       ├── Post.cs

│       └── Comment.cs

│

├── BlogSystem.Application/      # Interfaces, CQRS Commands & Handlers

│   ├── Interfaces/

│   │   ├── IPostRepository.cs

│   │   ├── ICommentRepository.cs

│   │   ├── IEmailNotifier.cs

│   │   └── IBackgroundTaskQueue.cs

│   ├── Posts/Commands/CreatePost/

│   ├── Comments/Commands/CreateComment/

│   └── BackgroundJobs/

│       └── CommentCreatedWork.cs

│

├── BlogSystem.Infrastructure/    # EF Core, repositories, background workers

│   ├── Data/

│   │   └── BlogDbContext.cs

│   ├── Repositories/

│   ├── Email/

│   │   └── FakeEmailNotifier.cs

│   ├── Background/

│   │   ├── ChannelBackgroundTaskQueue.cs

│   │   ├── CommentNotificationWorker.cs

│   │   └── CommentJobs.cs

│   └── DependencyInjection.cs

│

└── BlogSystem.API/                # Controllers, composition root

    ├── Controllers/

    │   ├── PostsController.cs

    │   └── CommentsController.cs

    └── Program.cs

```

**Dependency rule:** `Domain` ← `Application` ← `Infrastructure` ← `API`.

The Domain layer knows nothing about EF Core, Hangfire, or MediatR — it's plain C# entities.

---

## Tech Stack

| Layer | Technology |

|---|---|

| Language / Runtime | C# / .NET |

| API | ASP.NET Core Web API |

| CQRS / Mediator | [MediatR](https://github.com/jbogard/MediatR) |

| ORM | Entity Framework Core |

| Database | SQL Server |

| In-process background queue | `System.Threading.Channels` + `BackgroundService` |

| Durable background jobs | [Hangfire](https://www.hangfire.io/) (SQL Server storage) |

|---|---|

---

##  Hangfire Dashboard

```

http://localhost:<port>/hangfire

```

