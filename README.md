# 2020p3web-own-rest-api-JanDostal
## Schéma
![Konceptuální model](/plán/IMG_20210701_113127.jpg)
## Modely
### Třídy
Akce | Metoda | Endpoint | Výsledek
---- | ------ | -------- | --------
Vytvořit novou třídu | POST | api/classes + body | Created, Bad Request
Smazat třídu | DELETE | api/classes/{classId} | No content, Not found, Bad request
Upravit třídu | PUT | api/classes/{classId} + body | Ok, No content, Not found, Bad request
Získat třídu podle id | GET | api/classes/{classId} | Trida - Ok, Not found, Bad request
Získat seznam tříd podle parametrů | GET | api/classes[grade?][educationLevel?][codeDesignation?] | List\<Trida\> - Ok, No content, Bad request
Získat seznam tříd, které už byly ukončeny, podle úrovně vzdělání | GET | api/classes/ended{educationLevel} | List\<Trida\> - Ok, No content, Bad request
Získat počet studentů v třídě dle id třídy | GET | api/classes/{classId}/students/count | int - Ok, Not found, Bad request
Získat seznam studentů, kterým už bylo 18 let, dle id třídy | GET | api/classes/{classId}/students/adult | List\<Student\> - Ok, No content, Not found, Bad request
Získat seznam studentů v dané třídě dle id třídy | GET | api/classes/{classId}/students | List\<Student\> - Ok, No content, Not found, Bad request

#### POST metoda ukázka
```
{
  "kodoveOznaceni": "P",
  "datumVzniku": "2018-09-03T18:29:32.032Z",
  "datumUkonceni": "2022-06-30T18:29:32.032Z",
  "urovenVzdelani": 2
}
```

#### PUT metoda ukázka
```
{
  "kodoveOznaceni": "S",
  "datumVzniku": "2019-09-03T18:29:32.032Z",
  "datumUkonceni": "2023-06-30T18:29:32.032Z",
  "urovenVzdelani": 3
}
```

### Studenti
Akce | Metoda | Endpoint | Výsledek
---- | ------ | -------- | --------
Vytvořit nového studenta | POST | api/students + body | Created, Bad Request
Smazat studenta | DELETE | api/students/{studentId} | No content, Not found, Bad request
Upravit studenta | PUT | api/students/{studentId} + body | Ok, Not found, Bad request
Získat studenta podle id studenta | GET | api/students/{studentId} | Student - Ok, Not found, Bad request
Získat studenty podle parametrů | GET | api/students[age?][gender?] | List\<Student\> - Ok, No content, Bad request

#### POST metoda ukázka
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

#### PUT metoda ukázka
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
