USE VegetablesFruits

CREATE TABLE Products(
[Id] int PRIMARY KEY IDENTITY(1,1),
[Name] nvarchar(100) NOT NULL,
[Type] int NOT NULL CONSTRAINT check_type CHECK(Type = 1 OR Type = 2),
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


INSERT INTO Products(Name, Type)
VALUES (N'Apple', 1)

INSERT INTO Products(Name, Type)
VALUES (N'Tomato', 2)

INSERT INTO Colors(Name)
VALUES (N'Yellow')

INSERT INTO Colors(Name)
VALUES (N'Red')

INSERT INTO Colories(Count)
VALUES (20)

INSERT INTO Colories(Count)
VALUES (30)

INSERT INTO Colories(Count)
VALUES (10)

INSERT INTO ColorsColoriesProducts(ProductId, ColorId, ColorieId)
VALUES (1, 1, 3)

INSERT INTO ColorsColoriesProducts(ProductId, ColorId, ColorieId)
VALUES (1, 2, 1)

INSERT INTO ColorsColoriesProducts(ProductId, ColorId, ColorieId)
VALUES (2, 2, 2)




