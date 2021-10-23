USE VegetablesFruits

CREATE TABLE Products(
[Id] int PRIMARY KEY IDENTITY(1,1),
[Name] nvarchar(100) NOT NULL,
[Type] int NOT NULL CHECK(Type = 1 OR Type = 2),
)

CREATE TABLE Colors(
[Id] int PRIMARY KEY IDENTITY(1,1),
[Name] nvarchar(20) NOT NULL
)

CREATE TABLE Colories(
[Id] int PRIMARY KEY IDENTITY(1,1),
[Count] int NOT NULL
)

CREATE TABLE ColorsColoriesProducts(
[Id] int PRIMARY KEY IDENTITY(1,1),
[ProductId] int FOREIGN KEY REFERENCES Products(Id),
[ColorId] int FOREIGN KEY REFERENCES Colors(Id),
[ColorieId] int FOREIGN KEY REFERENCES Colories(Id)
)

