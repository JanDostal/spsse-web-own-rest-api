# App description

- This application runs as a web application containing a REST API and Swagger documentation that allows API calls
- The API works with students and classes and there are basic CRUD operations, but also more advanced functions

## Development info

- App was being developed in the period July 2021
- App was uploaded from school repository to this repository in May 2022, which was at the end in the last year of secondary school

## Instructions for starting app

1. Open App in **Visual Studio**
2. Press **start button (green arrow)** on the main Visual Studio toolbar
    - You don't need to run command ***Update-Database***, this application does it automatically

# App design

## Diagram

![Conceptual schema](/plan/IMG_20210701_113127.jpg)

## Data models

### Classes

Action | Method | Endpoint | Result
---- | ------ | -------- | --------
Create a new class | POST | api/classes + body | Created, Bad Request
Delete class | DELETE | api/classes/{classId} | No content, Not found, Bad request
Edit class | PUT | api/classes/{classId} + body | Ok, No content, Not found, Bad request
Get class by id | GET | api/classes/{classId} | Trida - Ok, Not found, Bad request
Get list of classes by parameters | GET | api/classes[grade?][educationLevel?][codeDesignation?] | List\<Trida\> - Ok, No content, Bad request
Get a list of classes that have already ended, by education level | GET | api/classes/ended{educationLevel} | List\<Trida\> - Ok, No content, Bad request
Get the number of students in a class by class id | GET | api/classes/{classId}/students/count | int - Ok, Not found, Bad request
Get a list of students, who have turned 18 years old, by class id | GET | api/classes/{classId}/students/adult | List\<Student\> - Ok, No content, Not found, Bad request
Get a list of students in a given class by class id | GET | api/classes/{classId}/students | List\<Student\> - Ok, No content, Not found, Bad request

#### POST method example
```
{
  "kodoveOznaceni": "P",
  "datumVzniku": "2018-09-03T18:29:32.032Z",
  "datumUkonceni": "2022-06-30T18:29:32.032Z",
  "urovenVzdelani": 2
}
```

#### PUT method example
```
{
  "kodoveOznaceni": "S",
  "datumVzniku": "2019-09-03T18:29:32.032Z",
  "datumUkonceni": "2023-06-30T18:29:32.032Z",
  "urovenVzdelani": 3
}
```

### Students
Action | Method | Endpoint | Result
---- | ------ | -------- | --------
Create a new student | POST | api/students + body | Created, Bad Request
Delete student | DELETE | api/students/{studentId} | No content, Not found, Bad request
Edit student | PUT | api/students/{studentId} + body | Ok, Not found, Bad request
Get student by student id | GET | api/students/{studentId} | Student - Ok, Not found, Bad request
Get students by parameters | GET | api/students[age?][gender?] | List\<Student\> - Ok, No content, Bad request

#### POST method example
```
{
  "jmeno": "Jan",
  "prijmeni": "Vomáčka",
  "telefonniCislo": "607232423",
  "pohlavi": -1,
  "datumNarozeni": "2005-03-23T18:34:33.803Z",
  "tridaId": 1
}
```

#### PUT method example
```
{
  "jmeno": "Cyril",
  "prijmeni": "Borec",
  "telefonniCislo": "603242432",
  "pohlavi": -1,
  "datumNarozeni": "2009-03-23T18:34:33.803Z",
  "tridaId": 2
}
```
