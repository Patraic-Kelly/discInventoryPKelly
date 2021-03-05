/*----------discInventoryPK.sql------------------\
|Author:Patraic Kelly       					  \
|Creation Date:3/3/21		    				  |
|Modified Date:3/5/21							  |
|LOG:											  |
|3/3/21- Wrote database drop&creation			  |
|		 wrote table creators					  |
|		 for Borrower, Rental, Artist, ArtistType.| 
|3/5/21- Added login and user creation			  |
|		 Wrote, and added columns for:			  |
|		 Genre, MediaType, DiscStatus, Disc,      |
|		 & DiscHasArtist Tables					  /
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
	UNIQUE (StatusID, MediaID, GenreID)
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
	UNIQUE (ArtistTypeID)
);

CREATE TABLE DiscHasArtist	--Relationship
(
	DiscHasArtistID	INT		NOT NULL IDENTITY PRIMARY KEY,
	DiscID		INT			NOT NULL REFERENCES Disc(DiscID),
	ArtistID	INT			NOT NULL REFERENCES Artist(ArtistID)
	UNIQUE (DiscID, ArtistID)
);

CREATE TABLE Borrower		--Entity
(
	BorrowerID	INT			NOT NULL IDENTITY PRIMARY KEY,
	fName		VARCHAR(80) NOT NULL,
	lName		VARCHAR(80) NOT NULL,
	Phone		VARCHAR(15) NOT NULL,
	Email		VARCHAR(80)
);

CREATE TABLE Rental			--Relationship/lookup
(
	RentalID	INT			NOT NULL IDENTITY PRIMARY KEY,
	BorrowDate	DATETIME2	NOT NULL,
	DueDate		DATETIME2	NOT NULL DEFAULT (GETDATE() +30),
	ReturnDate	DATETIME2	NULL,
	BorrowerID	INT			NOT NULL REFERENCES Borrower(BorrowerID),
	DiscID		INT			NOT NULL REFERENCES Disc(DiscID)
	UNIQUE (DiscID, BorrowerID)
);
