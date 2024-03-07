# Audio Device Catalog Application

## Description:
This application serves as a catalog for audio devices, including speakers and similar equipment. It follows the design pattern known as the layered architecture.

## Features:
Device and Producers list management: Browse through a list of audio devices. Add, edit or remove them.

Search functionality: Search for specific devices or producers by name, brand, or other attributes.

## Layered Architecture:
The system is divided into distinct layers, each with its specific responsibilities. These layers include:

**Presentation Layer**: Handles user interface interactions and presentation logic. Project includes two alternative presentation layers. One is a web application created using ASP.NET Core framework and follows the MVC design pattern. The other is a mobile/desktop application created using .NET MAUI framework and follows the MVVM design pattern.

**Business Logic Layer**: Contains the business logic and allows the Presentation Layer to interact with the database using the same methods regardless of used database technology.

**Data Access Layer**: Represents business objects and contains implementation of database operations for a specific technology.

**Interfaces Layer and Core Layer**: Contain interfaces and enumerated types common for all layers.

The Data Acces Object is loaded using late binding (via System.Reflections), which allows to load different DAOs without recompiling the app by simply changing the library name in the app's configuration file.
