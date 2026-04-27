# 🏛️ Histoire

Application web SaaS de découverte de l'histoire, permettant d'explorer des événements et personnages historiques à travers une carte mentale interactive de type Obsidian.

---

## ✨ Fonctionnalités

- Visualisation des événements et personnages historiques sous forme de graphe interactif
- Navigation entre les nœuds (événements, personnages) et leurs relations
- Architecture SaaS cloud multi-tenant

---

## 🛠️ Stack technique

### Frontend
- [React](https://react.dev) + [Vite](https://vite.dev)
- [react-force-graph](https://github.com/vasturiano/react-force-graph) — carte mentale interactive
- [Tailwind CSS](https://tailwindcss.com) — styling

### Backend
- [ASP.NET Core 8.0](https://learn.microsoft.com/en-us/aspnet/core) — API REST
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core) — ORM Code First
- [Npgsql](https://www.npgsql.org/efcore/) — provider PostgreSQL

### Base de données & Auth
- [Supabase](https://supabase.com) — PostgreSQL managé

### Hébergement
- Frontend → [Vercel](https://vercel.com)
- Backend → [Railway](https://railway.com)

---

## 🗂️ Structure du projet

```
Histoire/
├── Histoire.Api/               # Backend ASP.NET Core
│   ├── Controllers/            # Routes HTTP (PersonController, EventController)
│   ├── Models/                 # Entités EF Core (Person, Event)
│   ├── Dtos/                   # Data Transfer Objects
│   ├── Services/               # Logique métier
│   ├── AppDbContext.cs         # Configuration EF Core
│   ├── appsettings.json        # Configuration (sans secrets)
│   └── Program.cs              # Point d'entrée & configuration des services
│
└── histoire-front/             # Frontend React
    ├── src/
    │   ├── components/         # Composants React
    │   └── main.jsx            # Point d'entrée
    └── index.html
```

---

## 🗃️ Modèle de données

### Person
| Champ | Type | Description |
|---|---|---|
| Id | int | Clé primaire |
| FirstName | string | Prénom |
| LastName | string? | Nom de famille |
| DateofBirth | DateOnly | Date de naissance |
| DateofDeath | DateOnly? | Date de décès |
| PlaceofBirth | string? | Lieu de naissance |
| PlaceofDeath | string? | Lieu de décès |
| Biography | string? | Biographie |
| ImageUrl | string? | URL de l'image |

### Event
| Champ | Type | Description |
|---|---|---|
| Id | int | Clé primaire |
| Title | string | Titre |
| Description | string? | Description |
| DateStart | DateTime | Date de début |
| DateEnd | DateTime? | Date de fin |
| Location | string? | Lieu |
| Type | string | Type (Bataille, Période, Campagne...) |

> Les relations `Person` ↔ `Event` sont de type **many-to-many**, gérées par une table de jointure `EventPerson`.

---

## 🚀 Installation & lancement

### Prérequis
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org)
- Un projet [Supabase](https://supabase.com) actif

### Backend

```bash
cd Histoire.Api

# Restaurer les dépendances
dotnet restore

# Configurer les variables (copier et remplir)
cp appsettings.json appsettings.Development.json
```

Remplir `appsettings.Development.json` :

```json
{
  "Supabase": {
    "Url": "https://xxx.supabase.co",
    "Key": "votre-clé-anon"
  },
  "ConnectionStrings": {
    "DefaultConnection": "Host=...;Port=5432;Database=postgres;Username=postgres;Password=...;SSL Mode=Require;Trust Server Certificate=true"
  }
}
```

```bash
# Appliquer les migrations
dotnet ef database update

# Lancer l'API
dotnet run
```

L'API sera disponible sur `https://localhost:7067` avec Swagger sur `/swagger`.

### Frontend

```bash
cd histoire-front

# Installer les dépendances
npm install

# Lancer le serveur de développement
npm run dev
```

Le front sera disponible sur `http://localhost:5173`.

---

## 📡 Endpoints API

### Persons
| Méthode | Route | Description |
|---|---|---|
| GET | `/api/Person/GetPerson` | Récupérer toutes les personnes |
| GET | `/api/Person/{id}` | Récupérer une personne par id |
| GET | `/api/Person/{id}/events` | Récupérer les événements d'une personne |

### Events
| Méthode | Route | Description |
|---|---|---|
| GET | `/api/Event/GetEvent` | Récupérer tous les événements |
| GET | `/api/Event/{id}` | Récupérer un événement par id |
| GET | `/api/Event/{id}/persons` | Récupérer les personnes d'un événement |

---

## 📄 Licence

MIT