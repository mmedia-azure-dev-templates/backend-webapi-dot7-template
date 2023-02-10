USE [Jiban]
GO

INSERT INTO [web].[UserInformations]
           ([Id]
      ,[UserId]
      ,[TypeDocument]
      ,[Nacionality]
      ,[Ndocument]
      ,[Gender]
      ,[CivilStatus]
      ,[BirthDate]
      ,[EntryDate]
      ,[DepartureDate]
      ,[Hired]
      ,[ImgUrl]
      ,[CurriculumUrl]
      ,[Mobile]
      ,[Phone]
      ,[PrimaryStreet]
      ,[SecondaryStreet]
      ,[Numeration]
      ,[Reference]
      ,[Provincia]
      ,[Canton]
      ,[Parroquia]
      ,[Notes]
      ,[DateCreated]
      ,[DateUpdated])
     select 
NEWID() as Id, 
t1.Id as UserId
      ,
        CASE
                WHEN [CatTypeDocument] = 2 THEN 'Cedula'
                WHEN [CatTypeDocument] = 3 THEN 'Ruc'
                WHEN [CatTypeDocument] = 4 THEN 'Pasaporte'
                WHEN [CatTypeDocument] = 18 THEN 'Dni'
                ELSE 'Pasaporte'
        END AS TypeDocument
      ,
      CASE
                WHEN [CatNacionality] = 20 THEN 'Ecuatoriana'
                WHEN [CatNacionality] = 21 THEN 'Cubana'
                WHEN [CatNacionality] = 22 THEN 'Venezolana'
                WHEN [CatNacionality] = 23 THEN 'Colombiana'
                WHEN [CatNacionality] = 24 THEN 'Peruana'
                WHEN [CatNacionality] = 25 THEN 'Boliviana'
                WHEN [CatNacionality] = 35 THEN 'Francesa'
                ELSE 'Ecuatoriana'
        END AS Nacionality

      ,[t2].[Ndocument]
      ,
        CASE
                WHEN [CatGender] = 27 THEN 'Masculino'
                WHEN [CatGender] = 28 THEN 'Femenino'
                ELSE 'Masculino'
        END AS Gender
      ,
        CASE
                WHEN [CatCivilStatus] = 30 THEN 'Soltero'
                WHEN [CatCivilStatus] = 31 THEN 'Casado'
                WHEN [CatCivilStatus] = 32 THEN 'Divorciado'
                WHEN [CatCivilStatus] = 43 THEN 'Viudo'
                WHEN [CatCivilStatus] = 43 THEN 'Union Libre'
                ELSE 'Soltero'
        END AS CivilStatus
      ,[t2].[BirthDate]
      ,[t2].[EntryDate]
      ,[t2].[DepartureDate]
      ,[t2].[Hired]
      ,[t2].[ImgUrl]
      ,[t2].[CurriculumUrl]
      ,[t2].[Mobile]
      ,[t2].[Phone]
      ,
        CASE
          WHEN [Address] IS NULL THEN 'Necesita registrar calle primaria'
          ELSE [Address]
        END AS PrimaryStreet
      ,
        CASE
          WHEN [Address] IS NULL THEN 'Necesita registrar calle secundaria'
          ELSE [Address]
        END AS SecondaryStreet
      ,
        CASE
          WHEN [Address] IS NULL THEN 'Necesita registrar numeracion'
          ELSE [Address]
        END AS Numeration
      ,
        CASE
          WHEN [Address] IS NULL THEN 'Necesita registrar referencia'
          ELSE [Address]
        END AS Reference
      ,
        CASE
          WHEN [UbcProvincia] IS NULL THEN 17
          ELSE [UbcProvincia]
        END AS UbcProvincia
      ,
        CASE
          WHEN [UbcCanton] IS NULL THEN 203
          ELSE [UbcCanton]
        END AS UbcCanton
      ,
        CASE
          WHEN [UbcParroquia] IS NULL THEN 1332
          ELSE [UbcParroquia]
        END AS UbcParroquia
      ,CASE
          WHEN [Notes] IS NULL THEN 'MARKET'
          ELSE [Notes]
        END AS Notes
        , GETDATE()
        , GETDATE()
from dbo.AspNetUsers as t1
join tmp.Identifications as t2
on t1.LegacyId = t2.UserId
order by t1.LegacyId
GO