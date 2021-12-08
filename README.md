# DotNet7Project
## Description

Project created for classes in university for course named "Web Services .NET". Whole application is written with usage of ASP.NET Core 5 technology.

Repository contains example applications which allows data gathering and visualization used for weather stations. Whole project is intended for use with Docker deployed on university cluster (SWARM).

Data is generated from various sensors such as humidity sensor (air humidity measured in percent), pressure sensor (pressure measured in hPa), temperature sensor (temperature measured in degrees celsius and fahrenheit) and wind sensor (wind speed measured in km/h and direction measured in degrees). Generated data is sent to message queue, from which API application service reads messages, converts them into data and stores them in database. API application also serves as connector to data stored in database for GUI application., which aim is to visualize stored data.

## Project Structure

The whole project consists of three application:
- "applicationGenSensorData" - application that serves as simulation of sensors, which sends data to message queue - it is meant to run on local machine, not deployed on docker
- "applicationApi" - application that provides API endpoints for data operation used in GUI application and it also contains service responsible for getting messages from message queue, converting them to data and uploading them to database
- "applicationGui" - application that represents graphical interface of the whole application, uses API application for access to all data stored in database

In case of "applicationApi" and "applicationGui" there is also attached "Dockerfile", which is used to create images for Docker deployment. Project also contains "docker-compose.yml" file, which is used to deploy previously created application images onto the cluster, using configured in file composition.  
Docker compose
The RabbitMQ was chosen as implementation of message queue, while MongoDB (which instance was available on university cluster) was chosen as database for the project.

## Ports

GUI HTTP Port 1758

API HTTP Port 17584

RabbitMQ HTTP Port 5672

RabbitMQ HTTPS Port 15672

MongoDB GUI Port 8081
