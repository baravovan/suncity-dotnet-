# Suncity 

### Project description

Aplication is used to get information about cities time of sunrise/sunset according to It's coordinates. Two services were implemented:
1. city, which provide opportunities: 
  - to add city      to the DB
  - to edit city    in the  DB
  - to delete city from the DB
2. eventtime, which provides opportunity to get information about time of sunrise/sunset
  - to get info
  - Parameters: 
  - action=sunset | sunset | both
  - date=today | yyyy-mm-dd (example 2020-03-03)
  - city (example city=TestCity)
  
### Prerequisites 
- Docker
- Git

### Run 
to Start: 
 - install Docker on your PC https://docs.docker.com/get-docker/;
 - clone repository
 - from app directory (where Dockerfile is placed) start terminal and run command
```
docker build -t aspnetapp .
```
This will build docker image
- after finishing type next command 
```
docker run -it --rm -p 5000:80 --name sun_city  aspnetapp
```
It will start application. 

When in your browser try http://localhost:5000.

SunCity is developed by [Volodymyr Baranov] 2020.
