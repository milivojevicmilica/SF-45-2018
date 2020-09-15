CREATE TABLE [dbo].[Korisnici] (
    [IdKorisnik]    INT           IDENTITY (1, 1) NOT NULL,
    [Ime]           NVARCHAR (25) NOT NULL,
    [Prezime]       NVARCHAR (25) NOT NULL,
    [KorisnickoIme] NVARCHAR (50) NOT NULL,
    [Email]         NVARCHAR (30) NOT NULL,
    [TipKorisnika]  NVARCHAR (15) NOT NULL,
    [Lozinka]       NVARCHAR (50) NOT NULL,
    [Active]        BIT           NOT NULL,
    CONSTRAINT [PK_Korisnici] PRIMARY KEY CLUSTERED ([IdKorisnik] ASC),
    UNIQUE NONCLUSTERED ([KorisnickoIme] ASC)
);

SET IDENTITY_INSERT [dbo].[Korisnici] ON
INSERT INTO [dbo].[Korisnici] ([IdKorisnik], [Ime], [Prezime], [KorisnickoIme], [Email], [TipKorisnika], [Lozinka], [Active]) VALUES (1, N'Pero', N'Peric', N'Pero69', N'Pero69@gmail.com', N'ADMIN', N'asdf123', 1)
INSERT INTO [dbo].[Korisnici] ([IdKorisnik], [Ime], [Prezime], [KorisnickoIme], [Email], [TipKorisnika], [Lozinka], [Active]) VALUES (2, N'Dragan', N'Stanisic', N'DraganS', N'Gandra88@hotmail.com', N'ADMIN', N'asdf123', 1)
INSERT INTO [dbo].[Korisnici] ([IdKorisnik], [Ime], [Prezime], [KorisnickoIme], [Email], [TipKorisnika], [Lozinka], [Active]) VALUES (3, N'Djuro', N'Djuric', N'Djuro43', N'Djuro43@hotmail.com', N'PROFESOR', N'asdf123', 1)
INSERT INTO [dbo].[Korisnici] ([IdKorisnik], [Ime], [Prezime], [KorisnickoIme], [Email], [TipKorisnika], [Lozinka], [Active]) VALUES (4, N'Gavro', N'Garic', N'Gavro22', N'Gavro22@ninjamail.net', N'PROFESOR', N'asdf123', 1)
INSERT INTO [dbo].[Korisnici] ([IdKorisnik], [Ime], [Prezime], [KorisnickoIme], [Email], [TipKorisnika], [Lozinka], [Active]) VALUES (5, N'Mario', N'Maric', N'MMario', N'MMario@hotmail.com', N'PROFESOR', N'asdf123', 1)
INSERT INTO [dbo].[Korisnici] ([IdKorisnik], [Ime], [Prezime], [KorisnickoIme], [Email], [TipKorisnika], [Lozinka], [Active]) VALUES (6, N'Drazen', N'Drazic', N'Drazo69', N'CicaDraza@gmail.net', N'PROFESOR', N'asdf123', 1)
INSERT INTO [dbo].[Korisnici] ([IdKorisnik], [Ime], [Prezime], [KorisnickoIme], [Email], [TipKorisnika], [Lozinka], [Active]) VALUES (7, N'Dimitrije', N'Tucovic', N'Dimke', N'DT1991@gmail.net', N'ASISTENT', N'asdf123', 1)
INSERT INTO [dbo].[Korisnici] ([IdKorisnik], [Ime], [Prezime], [KorisnickoIme], [Email], [TipKorisnika], [Lozinka], [Active]) VALUES (8, N'Petar', N'Petrovic', N'Petar96', N'Petar96@hotmail.net', N'ASISTENT', N'asdf123', 1)
INSERT INTO [dbo].[Korisnici] ([IdKorisnik], [Ime], [Prezime], [KorisnickoIme], [Email], [TipKorisnika], [Lozinka], [Active]) VALUES (9, N'Nikola', N'Djuricko', N'Nidza22', N'Niki234@gmail.com', N'ASISTENT', N'asdf123', 1)
SET IDENTITY_INSERT [dbo].[Korisnici] OFF

CREATE TABLE [dbo].[Administratori] (
    [IdAdmin] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([IdAdmin] ASC),
    CONSTRAINT [FK_Admin] FOREIGN KEY ([IdAdmin]) REFERENCES [dbo].[Korisnici] ([IdKorisnik])
);

INSERT INTO [dbo].[Administratori] ([IdAdmin]) VALUES (2)

CREATE TABLE [dbo].[Ustanove] (
    [IdUstanova]             INT           IDENTITY (1, 1) NOT NULL,
    [Naziv]                  NVARCHAR (50) NULL,
    [Lokacija]               NVARCHAR (50) NULL,
    [Active]                 BIT           NULL,
    [MaksimalanBrojUcionica] INT           NULL,
    PRIMARY KEY CLUSTERED ([IdUstanova] ASC)
);

SET IDENTITY_INSERT [dbo].[Ustanove] ON
INSERT INTO [dbo].[Ustanove] ([IdUstanova], [Naziv], [Lokacija], [Active], [MaksimalanBrojUcionica]) VALUES (1, N'Jugodrvo', N'Bulevar Oslobodjenja 23', 1, 25)
INSERT INTO [dbo].[Ustanove] ([IdUstanova], [Naziv], [Lokacija], [Active], [MaksimalanBrojUcionica]) VALUES (2, N'FTN', N'Ulica Gde Su Fakulteti 223', 1, 40)
INSERT INTO [dbo].[Ustanove] ([IdUstanova], [Naziv], [Lokacija], [Active], [MaksimalanBrojUcionica]) VALUES (3, N'Park Citya', N'Narodnog Fronta 691', 1, 15)
SET IDENTITY_INSERT [dbo].[Ustanove] OFF

CREATE TABLE [dbo].[Profesori] (
    [IdProfesor]           INT NOT NULL,
    [DodeljeniAsistentId] INT NULL,
    [UstanovaId]           INT NULL,
    PRIMARY KEY CLUSTERED ([IdProfesor] ASC),
    CONSTRAINT [fk_korisnici_profesori] FOREIGN KEY ([IdProfesor]) REFERENCES [dbo].[Korisnici] ([IdKorisnik]),
    CONSTRAINT [fk_profesor_ustanova] FOREIGN KEY ([UstanovaId]) REFERENCES [dbo].[Ustanove] ([IdUstanova])
);

INSERT INTO [dbo].[Profesori] ([IdProfesor], [DodeljeniAsistentId], [UstanovaId]) VALUES (3, NULL, 1)
INSERT INTO [dbo].[Profesori] ([IdProfesor], [DodeljeniAsistentId], [UstanovaId]) VALUES (4, NULL, 1)
INSERT INTO [dbo].[Profesori] ([IdProfesor], [DodeljeniAsistentId], [UstanovaId]) VALUES (5, NULL, 2)
INSERT INTO [dbo].[Profesori] ([IdProfesor], [DodeljeniAsistentId], [UstanovaId]) VALUES (6, NULL, 3)




CREATE TABLE [dbo].[Asistenti] (
    [IdAsistent]         INT NOT NULL,
    [DodeljeniProfesor] INT NULL,
    [UstanovaId]         INT NULL,
    PRIMARY KEY CLUSTERED ([IdAsistent] ASC),
    CONSTRAINT [fk_korisnici_asistenti] FOREIGN KEY ([IdAsistent]) REFERENCES [dbo].[Korisnici] ([IdKorisnik]),
    CONSTRAINT [fk_dodeljeni_profesor] FOREIGN KEY ([DodeljeniProfesor]) REFERENCES [dbo].[Profesori] ([IdProfesor]),
    CONSTRAINT [fk_asistent_ustanova] FOREIGN KEY ([UstanovaId]) REFERENCES [dbo].[Ustanove] ([IdUstanova])
);

INSERT INTO [dbo].[Asistenti] ([IdAsistent], [DodeljeniProfesor], [UstanovaId]) VALUES (7, 3, 1)
INSERT INTO [dbo].[Asistenti] ([IdAsistent], [DodeljeniProfesor], [UstanovaId]) VALUES (8, 5, 2)
INSERT INTO [dbo].[Asistenti] ([IdAsistent], [DodeljeniProfesor], [UstanovaId]) VALUES (9, 6, 3)


CREATE TABLE [dbo].[Ucionice] (
    [IdUcionica]             INT           IDENTITY (1, 1) NOT NULL,
    [BrojUcionice]           NCHAR (10)    NULL,
    [BrojMesta]             INT           NULL,
    [TipUcionice]            NVARCHAR (50) NULL,
    [UstanovaGdeSeNalaziId] INT           NULL,
    [Active]                 BIT           NULL,
    PRIMARY KEY CLUSTERED ([IdUcionica] ASC),
    CONSTRAINT [fk_ucionica_ustanova] FOREIGN KEY ([UstanovaGdeSeNalaziId]) REFERENCES [dbo].[Ustanove] ([IdUstanova])
);

SET IDENTITY_INSERT [dbo].[Ucionice] ON
INSERT INTO [dbo].[Ucionice] ([IdUcionica], [BrojUcionice], [BrojMesta], [TipUcionice], [UstanovaGdeSeNalaziId], [Active]) VALUES (1, N'A1        ', 50, N'SARACUNARIMA', 1, 1)
INSERT INTO [dbo].[Ucionice] ([IdUcionica], [BrojUcionice], [BrojMesta], [TipUcionice], [UstanovaGdeSeNalaziId], [Active]) VALUES (2, N'A2        ', 50, N'SARACUNARIMA', 1, 1)
INSERT INTO [dbo].[Ucionice] ([IdUcionica], [BrojUcionice], [BrojMesta], [TipUcionice], [UstanovaGdeSeNalaziId], [Active]) VALUES (3, N'A3        ', 50, N'SARACUNARIMA', 1, 1)
INSERT INTO [dbo].[Ucionice] ([IdUcionica], [BrojUcionice], [BrojMesta], [TipUcionice], [UstanovaGdeSeNalaziId], [Active]) VALUES (4, N'A4        ', 50, N'SARACUNARIMA', 1, 1)
INSERT INTO [dbo].[Ucionice] ([IdUcionica], [BrojUcionice], [BrojMesta], [TipUcionice], [UstanovaGdeSeNalaziId], [Active]) VALUES (5, N'A5        ', 50, N'SARACUNARIMA', 1, 1)
INSERT INTO [dbo].[Ucionice] ([IdUcionica], [BrojUcionice], [BrojMesta], [TipUcionice], [UstanovaGdeSeNalaziId], [Active]) VALUES (6, N'B1        ', 100, N'BEZRACUNARA', 1, 1)
INSERT INTO [dbo].[Ucionice] ([IdUcionica], [BrojUcionice], [BrojMesta], [TipUcionice], [UstanovaGdeSeNalaziId], [Active]) VALUES (7, N'B2        ', 100, N'BEZRACUNARA', 1, 1)
INSERT INTO [dbo].[Ucionice] ([IdUcionica], [BrojUcionice], [BrojMesta], [TipUcionice], [UstanovaGdeSeNalaziId], [Active]) VALUES (8, N'B3        ', 100, N'BEZRACUNARA', 1, 1)
INSERT INTO [dbo].[Ucionice] ([IdUcionica], [BrojUcionice], [BrojMesta], [TipUcionice], [UstanovaGdeSeNalaziId], [Active]) VALUES (9, N'B4        ', 100, N'BEZRACUNARA', 1, 1)
INSERT INTO [dbo].[Ucionice] ([IdUcionica], [BrojUcionice], [BrojMesta], [TipUcionice], [UstanovaGdeSeNalaziId], [Active]) VALUES (10, N'A1        ', 50, N'SARACUNARIMA', 2, 1)
INSERT INTO [dbo].[Ucionice] ([IdUcionica], [BrojUcionice], [BrojMesta], [TipUcionice], [UstanovaGdeSeNalaziId], [Active]) VALUES (11, N'A3        ', 50, N'SARACUNARIMA', 2, 1)
INSERT INTO [dbo].[Ucionice] ([IdUcionica], [BrojUcionice], [BrojMesta], [TipUcionice], [UstanovaGdeSeNalaziId], [Active]) VALUES (12, N'A2        ', 50, N'SARACUNARIMA', 2, 1)
INSERT INTO [dbo].[Ucionice] ([IdUcionica], [BrojUcionice], [BrojMesta], [TipUcionice], [UstanovaGdeSeNalaziId], [Active]) VALUES (13, N'A4        ', 50, N'SARACUNARIMA', 2, 1)
INSERT INTO [dbo].[Ucionice] ([IdUcionica], [BrojUcionice], [BrojMesta], [TipUcionice], [UstanovaGdeSeNalaziId], [Active]) VALUES (14, N'B1        ', 100, N'BEZRACUNARA', 2, 1)
INSERT INTO [dbo].[Ucionice] ([IdUcionica], [BrojUcionice], [BrojMesta], [TipUcionice], [UstanovaGdeSeNalaziId], [Active]) VALUES (15, N'B2        ', 100, N'BEZRACUNARA', 2, 1)
INSERT INTO [dbo].[Ucionice] ([IdUcionica], [BrojUcionice], [BrojMesta], [TipUcionice], [UstanovaGdeSeNalaziId], [Active]) VALUES (16, N'B3        ', 100, N'BEZRACUNARA', 2, 1)
INSERT INTO [dbo].[Ucionice] ([IdUcionica], [BrojUcionice], [BrojMesta], [TipUcionice], [UstanovaGdeSeNalaziId], [Active]) VALUES (17, N'B5        ', 100, N'BEZRACUNARA', 2, 1)
INSERT INTO [dbo].[Ucionice] ([IdUcionica], [BrojUcionice], [BrojMesta], [TipUcionice], [UstanovaGdeSeNalaziId], [Active]) VALUES (18, N'A1        ', 50, N'SARACUNARIMA', 3, 1)
INSERT INTO [dbo].[Ucionice] ([IdUcionica], [BrojUcionice], [BrojMesta], [TipUcionice], [UstanovaGdeSeNalaziId], [Active]) VALUES (19, N'A2        ', 25, N'SARACUNARIMA', 3, 1)
INSERT INTO [dbo].[Ucionice] ([IdUcionica], [BrojUcionice], [BrojMesta], [TipUcionice], [UstanovaGdeSeNalaziId], [Active]) VALUES (20, N'A3        ', 25, N'SARACUNARIMA', 3, 1)
INSERT INTO [dbo].[Ucionice] ([IdUcionica], [BrojUcionice], [BrojMesta], [TipUcionice], [UstanovaGdeSeNalaziId], [Active]) VALUES (21, N'A4        ', 25, N'SARACUNARIMA', 3, 1)
INSERT INTO [dbo].[Ucionice] ([IdUcionica], [BrojUcionice], [BrojMesta], [TipUcionice], [UstanovaGdeSeNalaziId], [Active]) VALUES (22, N'B1        ', 100, N'BEZRACUNARA', 3, 1)
INSERT INTO [dbo].[Ucionice] ([IdUcionica], [BrojUcionice], [BrojMesta], [TipUcionice], [UstanovaGdeSeNalaziId], [Active]) VALUES (23, N'B2        ', 100, N'BEZRACUNARA', 3, 1)
INSERT INTO [dbo].[Ucionice] ([IdUcionica], [BrojUcionice], [BrojMesta], [TipUcionice], [UstanovaGdeSeNalaziId], [Active]) VALUES (24, N'B3        ', 100, N'BEZRACUNARA', 3, 1)
SET IDENTITY_INSERT [dbo].[Ucionice] OFF


CREATE TABLE [dbo].[Termini] (
    [IdTermin]              INT           IDENTITY (1, 1) NOT NULL,
    [VremeZauzecaPocetak] DATETIME2 (7) NOT NULL,
    [VremeZauzecaKraj]    DATETIME2 (7) NOT NULL,
    [DaniUNedelji]          NVARCHAR (20) NOT NULL,
    [TipNastave]            NVARCHAR (20) NOT NULL,
    [ZaduzeniPredavacId]    INT           NOT NULL,
    [UstanovaId]            INT           NOT NULL,
    [UcionicaId]            INT           NOT NULL,
    [Active]                BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([IdTermin] ASC),
    CONSTRAINT [fk_Termin_Ucionica] FOREIGN KEY ([UcionicaId]) REFERENCES [dbo].[Ucionice] ([IdUcionica]),
    CONSTRAINT [fk_Termin_Ustanova] FOREIGN KEY ([UstanovaId]) REFERENCES [dbo].[Ustanove] ([IdUstanova]),
    CONSTRAINT [fk_Termini_Korisnik] FOREIGN KEY ([ZaduzeniPredavacId]) REFERENCES [dbo].[Korisnici] ([IdKorisnik])
);


CREATE TABLE [dbo].[Raspored] (
    [IdRaspored] INT IDENTITY (1, 1) NOT NULL,
    [UstanovaId] INT NOT NULL,
    [TerminId]   INT NOT NULL,
    [UcionicaId] INT NOT NULL,
    [KorisnikId] INT NOT NULL,
    [Active]     BIT NOT NULL,
    PRIMARY KEY CLUSTERED ([IdRaspored] ASC),
    CONSTRAINT [fk_raspored_ustanova] FOREIGN KEY ([UstanovaId]) REFERENCES [dbo].[Ustanove] ([IdUstanova]),
    CONSTRAINT [fk_raspored_korisnik] FOREIGN KEY ([KorisnikId]) REFERENCES [dbo].[Korisnici] ([IdKorisnik]),
    CONSTRAINT [fk_raspored_ucionica] FOREIGN KEY ([UcionicaId]) REFERENCES [dbo].[Ucionice] ([IdUcionica]),
    CONSTRAINT [fk_raspored_termin] FOREIGN KEY ([TerminId]) REFERENCES [dbo].[Termini] ([IdTermin])
);







