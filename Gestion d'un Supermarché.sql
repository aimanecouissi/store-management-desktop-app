CREATE DATABASE Supermarch�
USE Supermarch�

CREATE TABLE Client(
    CIN VARCHAR(8) PRIMARY KEY,
    Nom VARCHAR(50),
    Pr�nom VARCHAR(50),
    Age INT,
    Sexe VARCHAR(5),
    T�l�phone VARCHAR(10),
    Ville VARCHAR(100))

CREATE TABLE Employ�(
    ID VARCHAR(4) PRIMARY KEY,
    Nom VARCHAR(50),
    Pr�nom VARCHAR(50),
    Age INT,
    Sexe VARCHAR(5),
    T�l�phone VARCHAR(10),
    Email VARCHAR(100),
	MotPasse VARCHAR(100))

CREATE TABLE Produit(
    Code VARCHAR(12) PRIMARY KEY,
    Nom VARCHAR(50),
    Categorie VARCHAR(50),
    Marque VARCHAR(50),
    Quantit�Stock INT,
    Prix MONEY)

CREATE TABLE Commande(
    Numero INT PRIMARY KEY,
    CIN VARCHAR(8) FOREIGN KEY REFERENCES Client(CIN),
    ID VARCHAR(4) FOREIGN KEY REFERENCES Employ�(ID),
    PrixTotal Money DEFAULT 0,
    DateCommande DATE)

CREATE TABLE Details_Commande(
    Numero INT FOREIGN KEY REFERENCES Commande(Numero),
    Code VARCHAR(12) FOREIGN KEY REFERENCES Produit(Code),
    Quantit� INT,
    PRIMARY KEY(Numero, Code))
GO

CREATE TRIGGER T1 ON Details_Commande AFTER INSERT AS
BEGIN
    DECLARE @pp MONEY
    SET @pp = (SELECT Prix FROM Produit WHERE Code = (SELECT Code From inserted))
    UPDATE Commande
    SET PrixTotal += (SELECT Quantit� FROM inserted) * @pp
    WHERE Numero = (SELECT Numero FROM inserted)
    UPDATE Produit
    SET Quantit�Stock -= (SELECT Quantit� FROM inserted)
    WHERE Code = (SELECT Code FROM inserted)
END
GO

INSERT INTO Client(CIN,Nom,Pr�nom,Age,Sexe,T�l�phone,Ville) VALUES
    ('AB1','SMITH','ERWIN',11,'HOMME','0123456789','SIDI KACEM'),
    ('BC2','ACKERMAN','LEVI',12,'HOMME','0147258369','SIDI KACEM'),
    ('CD3','ACKERMAN','MIKASA',12,'FEMME','0258369147','RABAT'),
    ('DE4','YEAGER','EREN',13,'HOMME','0369147258','SIDI KACEM'),
    ('EF5','REISS','HISTORIA',13,'FEMME','0369258147','RABAT'),
    ('FG6','BRAUN','REINER',13,'HOMME','0258147369','KENITRA'),
    ('GH7','YEAGER','ZIK',14,'HOMME','0147369258','SIDI KACEM'),
    ('HI8','FINGER','PIECK',14,'FEMME','0456789123','RABAT'),
    ('IJ9','ARLERT','ARMIN',14,'HOMME','0789123456','KENITRA'),
    ('JK10','BLOUSE','SASHA',14,'FEMME','0987654321','CASABLANCA'),
    ('KL11','SPRINGER','CONNIE',15,'HOMME','0654321987','SIDI KACEM'),
    ('LM12','ZOE','HANJI',15,'FEMME','0321987654','RABAT'),
    ('MN13','KIRSTEIN','JEAN',15,'HOMME','0159357258','KENITRA'),
    ('NO14','LEONHART','ANNIE',15,'FEMME','0357159258','CASABLANCA'),
    ('OP15','HOOVER','BERTOLD',15,'HOMME','0753951852','FES')

INSERT INTO Employ�(ID,Nom,Pr�nom,Age,Sexe,T�l�phone,Email,MotPasse) VALUES
    ('0001','AZUMABITO','KIYOMI',21,'FEMME','0992017306','emp_1@gmail.com','123'),
    ('0002','BRAUN','GABI',22,'FEMME','0542977242','emp_2@gmail.com','456'),
    ('0003','ACKERMAN','KENNY',22,'HOMME','0989247925','emp_3@gmail.com','789'),
    ('0004','DREYSE','HITCH',23,'FEMME','0168733903','emp_4@gmail.com','012'),
    ('0005','BOTT','MARCO',23,'HOMME','0146988532','emp_5@gmail.com','345'),
    ('0006','TYBUR','LARA',23,'FEMME','0450250733','emp_6@gmail.com','678'),
    ('0007','FRITZ','DINA',24,'FEMME','0358108352','emp_7@gmail.com','901'),
    ('0008','FORSTER','FLOCH',24,'HOMME','0191041889','emp_8@gmail.com','234'),
    ('0009','YEAGER','CARLA',24,'FEMME','0232445126','emp_9@gmail.com','567'),
    ('0010','GALLIARD','MARCEL',24,'HOMME','0633607711','emp_10@gmail.com','890'),
    ('0011','YEAGER','FAY',25,'FEMME','0454479195','emp_11@gmail.com','123'),
    ('0012','GALLIARD','PORCO',25,'HOMME','0439001578','emp_12@gmail.com','456'),
    ('0013','CAROLINA','MINA',25,'FEMME','0684268050','emp_13@gmail.com','789'),
    ('0014','GRICE','FALCO',25,'HOMME','0162593748','emp_14@gmail.com','012'),
    ('0015','RAL','PETRA',25,'FEMME','0826278428','emp_15@gmail.com','345')

INSERT INTO Produit(Code,Nom,Categorie,Marque,Quantit�Stock,Prix) VALUES
    ('740951534760','BATTEUR','PETIT ELECTROM�NAGER','BOSCH',1000,100),
    ('890787931505','R�FRIG�RATEUR','GROS ELECTROM�NAGER','WHIRLPOOL',1000,100),
    ('197867149715','ASPIRATEUR','ENTRETIEN DE LA MAISON','BOSCH',2000,200),
    ('280855135578','T�L�VISEUR','TV ET PHOTO','SAMSUNG',1000,100),
    ('163734636914','CASQUE','AUDIO','JBL',2000,200),
    ('884116661998','MACBOOK','INFORMATIQUE','APPLE',3000,300),
    ('337507732408','IPHONE','SMARTPHONE ET TABLETTE','APPLE',1000,100),
    ('351280009575','MANETTE','JEUX','SONY',2000,200),
    ('287080470960','IPAD','SMARTPHONE ET TABLETTE','APPLE',3000,300),
    ('436934721543','NOTEBOOK','INFORMATIQUE','HP',4000,400),
    ('753420184989','CLIMATISEUR','CONFORT DE LA MAISON','SAMSUNG',1000,100),
    ('487635649170','BARRE DE SON','AUDIO','SAMSUNG',2000,200),
    ('771315949227','IMPRIMANTE','INFORMATIQUE','HP',3000,300),
    ('801242185358','PLAYSTATION','JEUX','SONY',4000,400),
    ('992017306989','CHARGEUR','INFORMATIQUE','LG',5000,500)