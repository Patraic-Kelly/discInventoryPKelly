/*----------discInventoryPK.sql------------------\
|Author:Patraic Kelly       					  \
|Creation Date:3/3/21		    				  
|Modified Date:3/5/21							  
|LOG:											  
|3/3/21 PK - Wrote database drop&creation
|		wrote table creators
|		 for Borrower, Rental, Artist, ArtistType.
|3/5/21 PK- Added login and user creation
|		 Wrote, and added columns for:
|		 Genre, MediaType, DiscStatus, Disc,
|		 & DiscHasArtist Tables
|
|3/11/21-3/12/21 PK- Project 3 - Inserted Row Data
|						     	 into all tables.
|
|				Connected lookups with Disc via
|				Rental and DiscHasArtist Tables.
| Project 4
|
|3/17/21 PK -
|			-Added Variance to Customer Phone num as per feedback from last week
|			-3. Created SELECT to display Individual artists
|			-4. Created VIEW to display Individual artists   
|
|3/19/21 PK - Added ISNULL() condition to #3
|			-5. Show discs in database and band names
|			-6. Rewrite previous with NOT IN, using ViewIndividualArtists
|			-7. Show Borrowed discs and their borrowers
|			-8. Show number of times a disc has been borrowed
|			-9. Show discs that have not been returned
|												  /
\-----------------------------------------------*/

USE master;
go
DROP DATABASE IF EXISTS discInventoryPK;   
go
CREATE DATABASE discInventoryPK;
go

IF SUSER_ID('discAdmin') IS NULL
	CREATE LOGIN discAdmin
	WITH PASSWORD = 'TempPa$$w0rd',
	DEFAULT_DATABASE = discInventoryPK;

USE discInventoryPK;

CREATE USER discAdmin;
ALTER ROLE db_datareader
	ADD MEMBER discAdmin;
go

CREATE TABLE Genre          --Lookup
(
	GenreID		INT			NOT NULL IDENTITY PRIMARY KEY,
	GenreDesc	VARCHAR(80) NOT NULL
);

CREATE TABLE MediaType		--Lookup
(
	MediaID		INT			NOT NULL IDENTITY PRIMARY KEY,
	MediaDesc	VARCHAR(80) NOT NULL
);

CREATE TABLE DiscStatus		--Lookup
(
	StatusID	INT			NOT NULL IDENTITY PRIMARY KEY,
	StatusDesc	VARCHAR(80) NOT NULL
);

CREATE TABLE Disc			--Entity
(
	DiscID		INT			NOT NULL IDENTITY PRIMARY KEY,
	DiscName	VARCHAR(80)	NOT NULL,
	ReleaseDate DATE		NOT NULL,
	StatusID	INT			NOT NULL REFERENCES DiscStatus(StatusID),
	MediaID		INT			NOT NULL REFERENCES MediaType(MediaID),
	GenreID		INT			NOT NULL REFERENCES Genre(GenreID)
);

CREATE TABLE ArtistType		--Lookup
(
	ArtistTypeID	INT		NOT NULL IDENTITY PRIMARY KEY,
	ArtistTypeDesc	VARCHAR(80) NOT NULL
);

CREATE TABLE Artist			--Entity
(
	ArtistID	INT			NOT NULL IDENTITY PRIMARY KEY,
	ArtistName	VARCHAR(80)	NOT NULL,
	ArtistLastName VARCHAR(80)	NULL,
	ArtistTypeID INT		NOT NULL REFERENCES ArtistType(ArtistTypeID)
);

CREATE TABLE DiscHasArtist	--Relationship
(
	DiscHasArtistID	INT		NOT NULL IDENTITY PRIMARY KEY,
	DiscID		INT			NOT NULL REFERENCES Disc(DiscID),
	ArtistID	INT			NOT NULL REFERENCES Artist(ArtistID)
);

CREATE TABLE Borrower		--Entity
(
	BorrowerID	INT			NOT NULL IDENTITY PRIMARY KEY,
	fName		NVARCHAR(80) NOT NULL,
	lName		NVARCHAR(80) NOT NULL,
	Phone		VARCHAR(15) NOT NULL,
	Email		VARCHAR(80) NULL
);

CREATE TABLE Rental			--Relationship/lookup
(
	RentalID	INT			NOT NULL IDENTITY PRIMARY KEY,
	BorrowDate	DATETIME2	NOT NULL,
	DueDate		DATETIME2	NOT NULL DEFAULT (GETDATE() +30),
	ReturnDate	DATETIME2	NULL,
	BorrowerID	INT			NOT NULL REFERENCES Borrower(BorrowerID),
	DiscID		INT			NOT NULL REFERENCES Disc(DiscID)
);

--PROJECT 3 
	--Refactored CREATE TABLE commands from proj 2 to disclude 
	--unique designations for the time being.
	
	--Insert row data for all tables 

--Artist type
INSERT INTO [dbo].[ArtistType]
			(ArtistTypeDesc)
	VALUES 
		('Solo'),
		('Group')
GO
--Media Type
INSERT INTO [dbo].[MediaType]
           ([MediaDesc])
     VALUES
           ('CD'),
		   ('Vinyl'),
		   ('Cassette'),
		   ('DVD'),
		   ('Digital')
GO

--Genre
INSERT INTO [dbo].[Genre]
           ([GenreDesc])
     VALUES
           ('Rock'),
		   ('Pop'),
		   ('Pop Rock'),
		   ('ElectroPop'),
		   ('TripHop'),
		   ('HipHop'),
		   ('ProgressiveRock'),
		   ('Rap'),
		   ('Folk'),
		   ('Punk'),
		   ('Classical'),
		   ('Indie'),
		   ('Downtempo'),
		   ('DarkWave'),
		   ('Folk Rock')
GO
--Disc Status
INSERT INTO [dbo].[DiscStatus]
           ([StatusDesc])
     VALUES
           ('Available'),
		   ('Loan'),
		   ('Damaged'),
		   ('Missing'),
		   ('Discontinued')
GO

		--Insert 20 Artist Data entries
INSERT Artist (ArtistName, ArtistLastName, ArtistTypeID)
VALUES ('Garbage', NULL, 2) -- 1 name Group
	  ,('Splashdown', NULL, 2) 
	  ,('Goldfrapp', NULL, 2)
	  ,('Nine Inch Nails', NULL, 2) -- greater than 2 words
	  ,('Pilotredsun', NULL, 1)
	  ,('Joanna', 'Newsom', 1)
	  ,('Sia', NULL, 1) -- Single name single artist
	  ,('Zero 7', NULL, 2) -- 2 word group
	  ,('Rush', NULL, 2)
	  ,('Ani', 'DiFranco', 1)
	  ,('The Capricorns', NULL, 2)
	  ,('Yelle', NULL, 2)
	  ,('Prince Rama', NULL, 2)
	  ,('Vamm', NULL, 2)
	  ,('Little Boots', NULL, 1)
	  ,('Britney', 'Spears', 1)
	  ,('Dead Can Dance', NULL, 2)
	  ,('The Birthday Massacre', NULL, 2)
	  ,('Cat Power', NULL, 1)
	  ,('Ladytron', NULL, 2)

--Insert Disc row data into Disc Table
INSERT Disc (DiscName, ReleaseDate, GenreID, StatusID, MediaID)
VALUES ('Moon Pix', '9/22/1998', 12, 1, 1) -- Two words
	  ,('When It Falls', '5/1/2004', 13, 1, 2) -- More than two words
	  ,('You Are Free', '2/18/2003', 12, 1, 1)
	  ,('Blueshift', '1/1/1999', 3, 1, 1) -- One word
	  ,('Achievement', '3/29/2016', 13, 1, 5)
	  ,('Pop-Up', '9/3/2007', 4, 1, 1)
	  ,('The Serpent''s Egg', '12/24/1988', 14, 4, 2)
	  ,('Light & Magic', '9/17/2002', 4, 1, 1)
	  ,('Imperfectly', '6/19/1992', 15, 1, 1)
	  ,('A Farewell To Kings', '9/1/1977', 7, 1, 2)
	  ,('Imagica Demos', '10/28/2000', 14, 4, 1)	  
	  ,('In The Zone', '4/6/2006', 2, 1, 1)
	  ,('In The Zone', '1/1/2001', 4, 1, 1)
	  ,('Vamm', '11/1/2013', 9, 1, 1)
	  ,('Hands', '6/8/2009', 4, 1, 1)
	  ,('Ys', '11/14/2006', 15, 1, 1)
	  ,('Felt Mountain', '9/11/2000', 4, 1, 1)
	  ,('Garbage', '8/15/1995', 3, 1, 1)
	  ,('XTREME NOW', '3/4/2016', 4, 1, 2)
	  ,('Pretty Hate Machine', '10/20/1989', 4, 1, 1)

--Update 1 row using WHERE
UPDATE Disc
SET DiscName = 'Redshift'
WHERE DiscID = 4;

--Insert Borrower Row Data
INSERT Borrower (fName, lName, Phone, Email)
VALUES 
('Sæwine', 'Hutmacher', '555-117-0263', 'Shutmacher@aaa.com'),
('Jarl', 'Rivers', '410-555-2277', NULL),	  
('Urist', 'McUrist', '307-555-6985', NULL),
('Kipling', 'Poulin', '501-555-9436', NULL),
('Caroline', 'Merckx', '609-555-2707', NULL),
('Tadala', 'Szekeres', '615-555-6140', NULL),	
('Cyrillus', 'Stroud', '555-846-7839', 'StroudDynamics@something.net'),
('Lea', 'Smith', '775-403-5551', NULL),
('Eileifr', 'Berg', '123-123-1234', NULL),
('Yonatan', 'Abbadelli', '614-555-7315', NULL),
('Berto', 'Albrecktsson', '303-555-3938', 'Bert@aaa.com'),
('Jaylin', 'Derby', '307-555-7887', NULL),
('Blandine', 'Skovgaard', '208-555-8940', NULL),
('Doretta', 'Alexanderson', '701-555-4648', NULL),
('Martina', 'Ivers', '455-559-2550', NULL),
('Aarón', 'Aveskamp', '225-555-8078', 'AAAVE@aaa.com'),	  
('Bryson', 'Markov', '334-555-5062', NULL),
('Lalla', 'Macauley', '406-555-3665', NULL),
('Ruben', 'Tarantino', '515-555-8554', NULL),
('Pratibha', 'Beyersdorf', '202-555-2139', NULL)

-- Delete Borrower ID 13 as selected by WHERE
DELETE Borrower
WHERE BorrowerID = 13;

--Insert data rows into Rental to link Borrower with Disc
INSERT Rental (DiscID, BorrowerID, BorrowDate, DueDate, ReturnDate)
VALUES
(7, 18, '7/8/2017', '8/8/2017', '8/8/2017'),	--7,18 iteration 1
(3, 14, '7/12/2017', '8/12/2017', '8/14/2017'),	--Disc 3 iteration 1
(2, 4, '9/1/2017', '10/1/2017', '10/1/2017'),	--Borrower 4 iteration 1
(7, 18, '9/12/2017', '10/12/2017', NULL),		--7,18 iteration 2
(13, 7, '1/8/2017', '2/8/2017', '2/9/2017'),
(20, 9, '1/20/2018', '2/20/2018', '12/4/2020'),
(9, 1, '4/18/2018', '5/18/2018', '6/18/2018'),
(10, 3, '4/19/2018', '5/19/2018', '5/19/2018'),
(3, 8, '5/2/2018', '6/2/2018', '6/10/2018'),	--Disc 3 iteration 2
(16, 4, '5/2/2018', '6/2/2018', '6/2/2018'),	--Borrower 4 iteration 2
(12, 12, '8/5/2018', '9/5/2018', '9/7/2018'),
(4, 16, '8/16/2018', '9/16/2018', '9/16/2018'),
(6, 19, '4/9/2019', '5/9/2019', '5/9/2019'),
(14, 6, '6/6/2019', '7/6/2019', '7/6/2019'),
(8, 11, '7/8/2019', '8/8/2019', '8/8/2019'),
(5, 15, '9/10/2019', '10/10/2019', '10/12/2019'),
(1, 2, '10/9/2019', '11/9/2019', '11/9/2019'),
(15, 17, '12/1/2019', '1/1/2020', '1/1/2020'),
(11, 10, '4/6/2020', '5/6/2020', NULL),
(17, 12, '5/8/2020', '6/8/2020', '6/8/2020')

--Insert Data Rows into DiscHasArtist to link Artist with Disc
INSERT INTO DiscHasArtist (DiscID, ArtistID)
VALUES
(1,19), --/2 discs to a single artist
(3,19), --\--------------------------
(18,1),
(2,7), --/Single disc to two artists
(2,8), --\--------------------------
(4,2),
(8,20),
(17,3),
(5,5),
(16,6),
(10,9),
(9,10),
(13,11),
(6,12),
(19,13),
(14,14),
(15,15),
(12,16),
(7,17),
(11,18)

--Select which uses a WHERE to pull unreturned Discs
SELECT BorrowerID AS BorrowerID, DiscID AS DiscID, convert(varchar, BorrowDate, 101) AS BorrowDate, ReturnDate AS ReturnDate
FROM Rental
WHERE ReturnDate IS NULL;
GO

-- PROJECT 4 

USE discInventoryPK
GO

	-- 3. Show discs with individual artists.
SELECT DiscName AS 'Disc  Name', CONVERT(VARCHAR, ReleaseDate, 101) AS 'Release Date', 
ArtistName AS 'Artist First Name', ISNULL(ArtistLastName, ' ') AS 'Artist Last Name' 
FROM Disc d
JOIN DiscHasArtist da ON d.DiscID = da.DiscID
JOIN Artist a ON da.ArtistID = a.ArtistID
WHERE ArtistTypeID = 1
ORDER BY ArtistLastName, ArtistName
GO

	-- 4. Create VIEW for individual artists
DROP VIEW IF EXISTS ViewIndividualArtists
GO
CREATE VIEW ViewIndividualArtists
AS
SELECT ArtistID, ArtistName, ArtistLastName 
FROM Artist
WHERE ArtistTypeID = 1
GO

SELECT ArtistName, ISNULL(ArtistLastName, ' ') AS 'Last Name'
FROM ViewIndividualArtists
ORDER BY ArtistLastName, ArtistName
GO

	-- 5. Show Discs in database and any associated Groups
SELECT DiscName AS 'Disc Name', CONVERT(VARCHAR, ReleaseDate, 101) AS 'Release Date', ArtistName AS 'Band Name'
FROM Disc d
JOIN DiscHasArtist da ON d.DiscID = da.DiscID
JOIN Artist a ON da.ArtistID = a.ArtistID
WHERE ArtistTypeID = 2
ORDER BY ArtistName, DiscName
GO

	-- 6. Re-write the previous query using ViewIndividualArtists

SELECT DiscName AS 'Disc Name', CONVERT(VARCHAR, ReleaseDate, 101) AS 'Release Date', ArtistName AS 'Band Name'
FROM Disc d
JOIN DiscHasArtist da ON d.DiscID = da.DiscID
JOIN Artist a ON da.ArtistID = a.ArtistID
WHERE a.ArtistID NOT IN 
	(SELECT ArtistID
	FROM ViewIndividualArtists)
ORDER BY ArtistName, DiscName
GO

	-- 7. Show Borrowed Discs and who has them

SELECT fName AS 'First', lname AS 'Last', DiscName AS 'Disc Name', CONVERT(VARCHAR, BorrowDate, 101) AS 'Borrowed Date',
ISNULL(CONVERT(VARCHAR, ReturnDate, 101), 'Not Returned') AS 'Returned'
FROM Borrower b
JOIN Rental r ON b.BorrowerID = r.BorrowerID
JOIN Disc d ON r.DiscID = d.DiscID
ORDER BY lName, fName, BorrowDate, ReturnDate
GO

	-- 8. Show number of times a disc has been borrowed

SELECT d.DiscID AS 'Disc ID', DiscName, COUNT(BorrowDate) AS 'Times Borrowed'
FROM Disc d
JOIN Rental r ON d.DiscID = r.DiscID
GROUP BY d.DiscID, DiscName
ORDER BY d.DiscID

	-- 9. Show discs which have not yet been returned

SELECT DiscName, CONVERT(VARCHAR, BorrowDate, 101) AS 'Borrowed Date', 
ISNULL(CONVERT(VARCHAR, ReturnDate, 101), 'Not Returned') AS 'Returned', lName AS 'Last Name'
FROM Disc d
JOIN Rental r ON d.DiscID = r.DiscID
JOIN Borrower b ON b.BorrowerID = r.BorrowerID
WHERE ReturnDate IS NULL
ORDER BY BorrowDate, lName, DiscName
;
