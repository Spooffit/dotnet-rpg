# dotnet-rpg

<div align="left" id="badges">
  <a href="https://github.com/Spooffit/dotnet-rpg/actions/workflows/dotnet.yml">
    <img src="https://github.com/Spooffit/dotnet-rpg/actions/workflows/dotnet.yml/badge.svg?branch=main" alt="Build Badge"/>
  </a>
  <a href="https://github.com/Spooffit/dotnet-rpg/actions/workflows/codeql.yml">
    <img src="https://github.com/Spooffit/dotnet-rpg/actions/workflows/codeql.yml/badge.svg" alt="CodeQL Badge"/>
  </a>
  <a href="https://codecov.io/gh/Spooffit/dotnet-rpg">
    <img src="https://codecov.io/gh/Spooffit/dotnet-rpg/branch/main/graph/badge.svg?token=ARYTHG802I" alt="Codecov Badge"/>
  </a>
  <a href="https://github.com/Spooffit/dotnet-rpg/blob/main/LICENSE">
    <img src="https://img.shields.io/badge/license-Unlicense-blue.svg" alt="License Badge"/>
  </a>
  <a href="https://codeclimate.com/github/Spooffit/dotnet-rpg/maintainability">
    <img src="https://api.codeclimate.com/v1/badges/e25d3122e82a569ee3f3/maintainability" alt="Snyk Badge"/>
  </a>
</div>

<br>

Study project. Simple Web API.

<hr>

<h2><img src="https://raw.githubusercontent.com/Tarikul-Islam-Anik/Animated-Fluent-Emojis/master/Emojis/Animals/Jellyfish.png" alt="Jellyfish" width="25" height="25" /> Stack</h2>

* [ASP.NET 7](https://learn.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-7.0)
* [Entity Framework 7](https://github.com/dotnet/efcore)
* [AutoMapper](https://github.com/AutoMapper/AutoMapper)
* [FluentValidation](https://github.com/FluentValidation/FluentValidation)
* [Sqlite](https://www.sqlite.org/index.html)
* [xUnit](https://github.com/xunit/xunit), [FluentAssertions](https://github.com/fluentassertions/fluentassertions), [Moq](https://github.com/moq/moq), [AutoFixture](https://github.com/AutoFixture/AutoFixture/)

<h2><img src="https://raw.githubusercontent.com/Tarikul-Islam-Anik/Animated-Fluent-Emojis/master/Emojis/Travel%20and%20places/Comet.png" alt="Comet" width="25" height="25" />
 API Reference</h2>

#### Get a list of all Characters

```https
  GET /api/Character
```

#### Get a Character by id

```https
  GET /api/Character/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | Id of Character to fetch |

#### Create a new Character

```https
  POST /api/Character
```

Request body JSON object

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `name`      | `string` | Name of Character to post |
| `hitPoints`      | `int` | Property of Character to post |
| `strength`      | `int` | Property of Character to post |
| `defense`      | `int` | Property of Character to post |
| `intelligence`      | `int` | Property of Character to post |
| `class`      | `enum` | Class of Character to post |

#### Update a Character

```https
  PUT /api/Character
```

Request body JSON object

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | Id of Character to post |
| `name`      | `string` | Name of Character to post |
| `hitPoints`      | `int` |Property of Character to post |
| `strength`      | `int` | Property of Character to post |
| `defense`      | `int` | Property of Character to post |
| `intelligence`      | `int` | Property of Character to post |
| `class`      | `enum` | Class of Character to post |


#### Delete a Character by id

```https
  DELETE /api/Character/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | Id of Character to delete |

<h2><img src="https://raw.githubusercontent.com/Tarikul-Islam-Anik/Animated-Fluent-Emojis/master/Emojis/Travel%20and%20places/Rocket.png" alt="Rocket" width="25" height="25" /> Run Locally</h2>

Clone the project

```bash
  git clone https://github.com/Spooffit/dotnet-rpg.git
```

Install dependencies

```bash
  dotnet restore
```

Start the server

```bash
  dotnet build
```


<h2><img src="https://raw.githubusercontent.com/Tarikul-Islam-Anik/Animated-Fluent-Emojis/master/Emojis/Animals/Hatching%20Chick.png" alt="Hatching Chick" width="25" height="25" /> Running Tests</h2>

To run tests, run the following command

```bash
  dotnet test
```
<h2><img src="https://raw.githubusercontent.com/Tarikul-Islam-Anik/Animated-Fluent-Emojis/master/Emojis/Symbols/Hamsa.png" alt="Hamsa" width="25" height="25" /> License</h2>

[Unlicense](https://github.com/Spooffit/dotnet-rpg/blob/main/LICENSE)

