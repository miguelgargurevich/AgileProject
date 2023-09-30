-- drop table ApplicationRoles
-- drop table AspNetUserClaims
-- drop table AspNetUserLogins
-- drop table AspNetUserRoles
-- drop table AspNetUserTokens
--     drop table AspNetUsers
--    drop table  CalendarBE
--    drop table  CalendarTypeBE
--    drop table  EventTypeBE

-- insert into CalendarBE 
-- select * from Calendar

 
--     drop table   AspNetRoleClaims
--     drop table   AspNetRoles
--     drop table   Calendar
--     drop table   CalendarType
--     drop table   EventType
--     drop table   usercalendartype







BEGIN
	DECLARE @guid uniqueidentifier = NEWID();
insert into AspNetUsers (Id,Nombres,ApellidoPaterno,ApellidoMaterno,UserName,Email,EmailConfirmed,PasswordHash,PhoneNumber,PhoneNumberConfirmed,FechaNacimiento)
                        values 
                        (@guid,
                        'webmaster',
                        '',
                        '',
                        'webmaster',
                        'webmaster',
                        1,
                        'YQBzAGQAYQBzAGQA',  --asdasd
                        null,
                        0,
                        '2023-05-30')
END



insert into EventType(name,color) values ('birthday','#B28DFF')
insert into EventType(name,color) values ('vacation','#FFABAB')
insert into EventType(name,color) values ('session','#B0F2C2')
-- insert into [dbo].[EventType] values (3,'Session','#B0F2C2')

insert into AspNetRoles values (NULL,'ADM','Administrator');
insert into AspNetRoles values (NULL,'PO','Product Owner');
insert into AspNetRoles values (NULL,'OPE','Operator');
insert into AspNetRoles values (NULL,'TEAM','Team Member');
insert into AspNetRoles values (NULL,'NEW','New Role');
insert into AspNetRoles values (NULL,'ARQ','Arquitect');
insert into AspNetRoles values (NULL,'DEV','Developer');
insert into AspNetRoles values (NULL,'ATC','Agile Team Coach');
insert into AspNetRoles values (NULL,'SEC','Security Analyst');
insert into AspNetRoles values (NULL,'LT','Tech Lead');
insert into AspNetRoles values (NULL,'DEVOPS','Devops Analyst');
insert into AspNetRoles values (NULL,'CHL','Chapter Lead');
insert into AspNetRoles values (NULL,'TRL','Tribe Lead');


insert into CalendarType values ('Los Trovadores Criollos','Squad',1);
insert into CalendarType values ('AcselX','Chapter',1);
insert into CalendarType values ('Los Hermanos Zañartu','Squad',1);


insert into usercalendartype values ('webmaster',1,null,null,null)
insert into usercalendartype values ('webmaster',2,null,null,null)
insert into usercalendartype values ('webmaster',3,null,null,null)

insert into usercalendartype (username,CalendarTypeId,Name,GroupType)
select UserName, 1, null,null from dbo.[AspNetUsers] a where a.email not in ('webmaster')


insert into [dbo].[Calendar](title,start,[end],description,eventtypeid,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('cumple Miguel','2023-06-25 00:00:00.000','2023-06-26 00:00:00.000','para mi cumple quiero una cena en chillis',1,1,'Los Trovadores Criollos','2023-04-18 02:46:24.247',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,start,[end],description,eventtypeid,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('vaca Miguel','2023-05-15 12:00:00.000','2023-06-03 12:00:00.000','',2,1,'Los Trovadores Criollos','2023-04-18 15:11:19.517',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,start,[end],description,eventtypeid,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Cumpleaños Cristhian Gutarra','2023-12-15 12:00:00.000','2023-12-16 12:00:00.000','',1,1,'Los Trovadores Criollos','2023-04-18 15:11:59.647',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,start,[end],description,eventtypeid,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Cumple de Mariana','2023-08-26 12:00:00.000','2023-08-27 12:00:00.000','Trattoria!!!',1,1,'Los Trovadores Criollos','2023-04-18 15:20:55.583',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,start,[end],description,eventtypeid,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Vacaciones MARIANA FLOR','2023-10-09 12:00:00.000','2023-10-14 12:00:00.000','',2,1,'Los Trovadores Criollos','2023-04-18 15:22:45.013',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,start,[end],description,eventtypeid,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Cumpleaños Ricardo','2023-08-24 12:00:00.000','2023-08-25 12:00:00.000','Buffet en Rodizzio',1,1,'Los Trovadores Criollos','2023-04-18 15:29:26.543',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,start,[end],description,eventtypeid,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Cumpleaños Jesus Andy','2023-08-02 12:00:00.000','2023-08-03 12:00:00.000','',1,1,'Los Trovadores Criollos','2023-04-18 15:30:54.747',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,start,[end],description,eventtypeid,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Vacaciones Ricardo','2023-08-21 00:00:00.000','2023-08-26 00:00:00.000','',2,1,'Los Trovadores Criollos','2023-04-18 15:36:20.303',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,start,[end],description,eventtypeid,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Cumple de Harry','2023-09-10 00:00:00.000','2023-09-11 00:00:00.000','',1,1,'Los Trovadores Criollos','2023-04-20 17:01:22.263',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,start,[end],description,eventtypeid,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Cumpleaños Marco Pirela','2023-05-30 12:00:00.000','2023-05-31 12:00:00.000','',1,1,'Los Trovadores Criollos','2023-04-18 15:54:36.197',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,start,[end],description,eventtypeid,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Vacaciones Marco Pirela','2023-06-05 00:00:00.000','2023-06-10 00:00:00.000','',2,1,'Los Trovadores Criollos','2023-04-18 15:59:53.277',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,start,[end],description,eventtypeid,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('HB José Rázuri','2024-01-23 12:00:00.000','2024-01-24 12:00:00.000','Pollito',1,1,'Los Trovadores Criollos','2023-04-18 16:01:33.010',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,start,[end],description,eventtypeid,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Vacaciones MARIANA FLOR','2023-12-26 12:00:00.000','2024-01-12 12:00:00.000','',2,1,'Los Trovadores Criollos','2023-04-24 20:52:00.257',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,start,[end],description,eventtypeid,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Cumpleaños de Jean Reyes','2023-11-02 12:00:00.000','2023-11-03 12:00:00.000','',1,1,'Los Trovadores Criollos','2023-05-02 20:49:50.797',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,start,[end],description,eventtypeid,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Cumpleaños Moylan Loo','2023-11-03 12:00:00.000','2023-11-04 12:00:00.000','',1,1,'Los Trovadores Criollos','2023-05-02 20:50:26.793',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,start,[end],description,eventtypeid,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Cumpleaños Antony Neyra','2023-06-15 12:00:00.000','2023-06-15 23:00:00.000','',1,1,'Los Trovadores Criollos','2023-05-02 20:56:34.313',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,start,[end],description,eventtypeid,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Vacaciones Yahir','2023-07-17 12:00:00.000','2023-07-23 12:00:00.000','Vacaciones Yahir',2,1,'Los Trovadores Criollos','2023-06-15 15:16:16.690',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,start,[end],description,eventtypeid,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Vacaciones Jorge','2023-07-19 12:00:00.000','2023-08-01 12:00:00.000','',2,1,'Los Trovadores Criollos','2023-07-11 14:40:15.497',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,start,[end],description,eventtypeid,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Vacaciones Jorge','2023-11-21 12:00:00.000','2023-12-07 12:00:00.000','',2,1,'Los Trovadores Criollos','2023-07-11 14:40:47.793',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,start,[end],description,eventtypeid,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Vacaciones Jorge','2023-06-12 12:00:00.000','2023-06-20 12:00:00.000','',2,1,'Los Trovadores Criollos','2023-07-11 14:41:15.480',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,start,[end],description,eventtypeid,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Cumpleaños Baudy','2023-08-16 12:00:00.000','2023-08-16 23:59:00.000','',1,1,'Los Trovadores Criollos','2023-07-14 15:51:52.300',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,start,[end],description,eventtypeid,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Cumple de Jorge','2023-12-16 00:00:00.000','2023-12-17 00:00:00.000','',1,1, 'Los Trovadores Criollos','2023-08-17 17:45:34.207',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,start,[end],description,eventtypeid,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('HB José Rázuri','2024-01-23 12:00:00.000','2024-01-24 12:00:00.000','Pollito',1,2,'Chapter AcselX','2023-08-17 21:07:48.860',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,start,[end],description,eventtypeid,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Cumpleaños Billy','2023-08-21 12:00:00.000','2023-08-22 12:00:00.000','',1,1,'Los Trovadores Criollos','2023-08-21 19:30:05.223',1,'mflor@pacifico.com.pe','E598B608-5818-4566-B865-33C843E887A2')
insert into [dbo].[Calendar](title,start,[end],description,eventtypeid,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Sprint Retrospective - Q3S5','2023-09-19 14:00:00.000','2023-09-19 15:30:00.000','',3,1,'Los Trovadores Criollos','2023-09-19 15:03:15.077',0,'webmaster','0AE9BFB8-A7CA-437C-A203-E41CD1B7B219')

/*
insert into [dbo].[Calendar](title,startdate,enddate,description,eventtypeid,eventtypename,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Sprint Retrospective - Q3S5','2023-09-19 14:00:00.000','2023-09-19 15:30:00.000',"Mural:
https://app.mural.co/t/ayllu6528/m/ayllu6528/1695151049809/145d51a04208b2fd4839b0ce8e82756c3a56d5e9?sender=u62f81034f0f69618b8640045",3,'Session',1,'Los Trovadores Criollos','2023-09-19 15:03:15.077',0,'webmaster','0AE9BFB8-A7CA-437C-A203-E41CD1B7B219')
*/

INSERT INTO AspNetUserRoles
SELECT Id, 1, NULL, NULL, NULL, NULL
FROM AspNetUsers
WHERE UserName IN ('webmaster');


INSERT INTO AspNetUserRoles
SELECT Id, 11, NULL, NULL, NULL, NULL
FROM AspNetUsers
WHERE UserName NOT IN ('webmaster');


-- Insert 1
INSERT INTO AspNetUsers (
    Id,
    Email,
    EmailConfirmed,
    PasswordHash,
    UserName,
    PhoneNumber,
    PhoneNumberConfirmed,
    Nombres,
    ApellidoPaterno,
    ApellidoMaterno,
    FechaNacimiento
) VALUES (
    '0FCCE089-F2F2-4CC9-BF3F-E7120E55A1B0',
    'jesus.torres@mdp.com.pe',
    1,
    'UABhAHMAcwB3AG8AcgBkADEAIQA=',
    'jesus.torres@mdp.com.pe',
    NULL,
    0,
    'Jesús Andy',
    'Torres',
    'Mendoza',
    '2023-08-02'
);

-- Insert 2
INSERT INTO AspNetUsers (
    Id,
    Email,
    EmailConfirmed,
    PasswordHash,
    UserName,
    PhoneNumber,
    PhoneNumberConfirmed,
    Nombres,
    ApellidoPaterno,
    ApellidoMaterno,
    FechaNacimiento
) VALUES (
    '1376263B-C3BA-485B-BF98-F211202DBBA7',
    'jespinoza@pacifico.com.pe',
    1,
    'UABhAHMAcwB3AG8AcgBkADEAIQA=',
    'jespinoza@pacifico.com.pe',
    NULL,
    0,
    'Jorge Wilson',
    'Espinoza',
    'Chamaya',
    '2023-12-16'
);

-- Insert 3
INSERT INTO AspNetUsers (
    Id,
    Email,
    EmailConfirmed,
    PasswordHash,
    UserName,
    PhoneNumber,
    PhoneNumberConfirmed,
    Nombres,
    ApellidoPaterno,
    ApellidoMaterno,
    FechaNacimiento
) VALUES (
    '4904607F-83F2-4B19-9D8A-9FAA761FAE52',
    'marco.pirela@inetum.com',
    1,
    'UABhAHMAcwB3AG8AcgBkADEAIQA=',
    'marco.pirela@inetum.com',
    NULL,
    0,
    'Marco Antonio',
    'Pierla',
    'Navarro',
    '2023-05-30'
);

-- Insert 4
INSERT INTO AspNetUsers (
    Id,
    Email,
    EmailConfirmed,
    PasswordHash,
    UserName,
    PhoneNumber,
    PhoneNumberConfirmed,
    Nombres,
    ApellidoPaterno,
    ApellidoMaterno,
    FechaNacimiento
) VALUES (
    '5629D776-02C6-4D93-98A9-1B1D3313F906',
    'isaipalavicinih@pacifico.com.pe',
    1,
    'UABhAHMAcwB3AG8AcgBkADEAIQA=',
    'isaipalavicinih@pacifico.com.pe',
    NULL,
    0,
    'Isai Baudy',
    'Palavicini',
    'Hinostroza',
    '2023-08-16'
);

-- Insert 5
INSERT INTO AspNetUsers (
    Id,
    Email,
    EmailConfirmed,
    PasswordHash,
    UserName,
    PhoneNumber,
    PhoneNumberConfirmed,
    Nombres,
    ApellidoPaterno,
    ApellidoMaterno,
    FechaNacimiento
) VALUES (
    '604628FC-FCC1-4A63-974D-1BF21904E841',
    'carlajohnstonc@pacifico.com.pe',
    1,
    'UABAAHMAcwB3AG8AcgBkAA==',
    'carlajohnstonc@pacifico.com.pe',
    NULL,
    0,
    'Carla Denise',
    'Johnston',
    'de la Cuba',
    '1900-01-01'
);

-- Insert 6
INSERT INTO AspNetUsers (
    Id,
    Email,
    EmailConfirmed,
    PasswordHash,
    UserName,
    PhoneNumber,
    PhoneNumberConfirmed,
    Nombres,
    ApellidoPaterno,
    ApellidoMaterno,
    FechaNacimiento
) VALUES (
    '70CF156E-BE17-4B35-B127-9F3892D32D46',
    'aneyra@soaint.com',
    1,
    'UABhAHMAcwB3AG8AcgBkADEAIQA=',
    'aneyra@soaint.com',
    NULL,
    0,
    'Antony',
    'Neyra',
    'Hidalgo',
    '2023-06-15'
);

-- Insert 7
INSERT INTO AspNetUsers (
    Id,
    Email,
    EmailConfirmed,
    PasswordHash,
    UserName,
    PhoneNumber,
    PhoneNumberConfirmed,
    Nombres,
    ApellidoPaterno,
    ApellidoMaterno,
    FechaNacimiento
) VALUES (
    '81557B10-8F72-49A3-A866-58526FBE3E3E',
    'billy.fernandez_consultor@mdp.com.pe',
    1,
    'UABhAHMAcwB3AG8AcgBkADEAIQA=',
    'billy.fernandez_consultor@mdp.com.pe',
    NULL,
    0,
    'Billy George',
    'Fernández',
    'Quillay',
    '2023-08-21'
);

-- Insert 8
INSERT INTO AspNetUsers (
    Id,
    Email,
    EmailConfirmed,
    PasswordHash,
    UserName,
    PhoneNumber,
    PhoneNumberConfirmed,
    Nombres,
    ApellidoPaterno,
    ApellidoMaterno,
    FechaNacimiento
) VALUES (
    'B1F5DE01-A60A-4A33-9EF9-8E43B5E20E3D',
    'julioyanaricoc@pacifico.com.pe',
    1,
    'UABhAHMAcwB3AG8AcgBkADEAIQA=',
    'julioyanaricoc@pacifico.com.pe',
    NULL,
    0,
    'Julio Cesar',
    'Yanarico',
    'Cayo',
    '2023-08-21'
);

-- Insert 9
INSERT INTO AspNetUsers (
    Id,
    Email,
    EmailConfirmed,
    PasswordHash,
    UserName,
    PhoneNumber,
    PhoneNumberConfirmed,
    Nombres,
    ApellidoPaterno,
    ApellidoMaterno,
    FechaNacimiento
) VALUES (
    'B9B789F6-D5B3-4671-B53F-BED967E13F3F',
    'cristhian.gutarra@tivit.com',
    1,
    'UABhAHMAcwB3AG8AcgBkADEAIQA=',
    'cristhian.gutarra@tivit.com',
    NULL,
    0,
    'Cristhian',
    'Gutarra',
    'Leigh',
    '2023-12-15'
);

-- Insert 10
INSERT INTO AspNetUsers (
    Id,
    Email,
    EmailConfirmed,
    PasswordHash,
    UserName,
    PhoneNumber,
    PhoneNumberConfirmed,
    Nombres,
    ApellidoPaterno,
    ApellidoMaterno,
    FechaNacimiento
) VALUES (
    'D81F9976-7E57-4F24-A884-4E614F750FB4',
    'rmelendm@nttdata.com',
    1,
    'UABhAHMAcwB3AG8AcgBkADEAIQA=',
    'rmelendm@nttdata.com',
    NULL,
    0,
    'Ricardo Anthony',
    'Melendres',
    'Martel',
    '2023-08-24'
);

-- Insert 11
INSERT INTO AspNetUsers (
    Id,
    Email,
    EmailConfirmed,
    PasswordHash,
    UserName,
    PhoneNumber,
    PhoneNumberConfirmed,
    Nombres,
    ApellidoPaterno,
    ApellidoMaterno,
    FechaNacimiento
) VALUES (
    'D99957BB-B94D-4DB2-8BED-1B4FC17C51A8',
    'JoseRazuriO@pacifico.com.pe',
    1,
    'UABhAHMAcwB3AG8AcgBkADEAIQA=',
    'JoseRazuriO@pacifico.com.pe',
    NULL,
    0,
    'José Carlos',
    'Razuri',
    'Ozorio',
    '2024-01-23'
);

-- Insert 12
INSERT INTO AspNetUsers (
    Id,
    Email,
    EmailConfirmed,
    PasswordHash,
    UserName,
    PhoneNumber,
    PhoneNumberConfirmed,
    Nombres,
    ApellidoPaterno,
    ApellidoMaterno,
    FechaNacimiento
) VALUES (
    'E598B608-5818-4566-B865-33C843E887A2',
    'mflor@pacifico.com.pe',
    1,
    'UABhAHMAcwB3AG8AcgBkADEAIQA=',
    'mflor@pacifico.com.pe',
    NULL,
    0,
    'Mariana Milagros',
    'Flor',
    'Espinoza',
    '2023-08-26'
);

-- Insert 13
INSERT INTO AspNetUsers (
    Id,
    Email,
    EmailConfirmed,
    PasswordHash,
    UserName,
    PhoneNumber,
    PhoneNumberConfirmed,
    Nombres,
    ApellidoPaterno,
    ApellidoMaterno,
    FechaNacimiento
) VALUES (
    'ED1D9E00-76ED-467C-94FE-2F817A9F4089',
    'miguel.fernandez-gargurevich@inetum.com',
    1,
    'YQBzAGQAYQBzAGQA',
    'miguel.fernandez-gargurevich@inetum.com',
    NULL,
    0,
    'Miguel',
    'Fernandez',
    'Gargurevich',
    '1981-06-25'
);

-- Insert 14
INSERT INTO AspNetUsers (
    Id,
    Email,
    EmailConfirmed,
    PasswordHash,
    UserName,
    PhoneNumber,
    PhoneNumberConfirmed,
    Nombres,
    ApellidoPaterno,
    ApellidoMaterno,
    FechaNacimiento
) VALUES (
    'F789CE1D-4279-47D5-83B9-13F21E6CDBAE',
    'harry.huanilo@mdp.com.pe',
    1,
    'UABhAHMAcwB3AG8AcgBkADEAIQA=',
    'harry.huanilo@mdp.com.pe',
    NULL,
    0,
    'Harry Maiker',
    'Huanilo',
    'Mori',
    '2023-09-10'
);
