

BEGIN
	DECLARE @guid uniqueidentifier = NEWID();
insert into AspNetUsers (Id,AccessFailedCount,ConcurrencyStamp,Email,EmailConfirmed,LockoutEnabled,LockoutEnd,NormalizedEmail,NormalizedUserName,PasswordHash,PhoneNumber,PhoneNumberConfirmed,SecurityStamp,
                        TwoFactorEnabled,UserName,Nombres,ApellidoPaterno,ApellidoMaterno,FechaNacimiento) values (@guid,
                        0,
                        '22522292-f147-487c-af39-8e3809d255db',
                        'webmaster',
                        1,
                        1,
                        null,
                        'webmaster',
                        'webmaster',
                        'YQBzAGQAYQBzAGQA',  --asdasd
                        null,
                        0,
                        '',
                        0,
                        'webmaster',
                        'webmaster',
                        '',
                        '',
                        null
                        )
END



insert into EventType(id,name,color) values (1,'birthday','#B28DFF')
insert into EventType(id,name,color) values (2,'vacation','#FFABAB')
insert into [dbo].[EventType] values (3,'Session','#B0F2C2')

insert into AspNetRoles values (0,NULL,'ADM','Administrator');
insert into AspNetRoles values (1,NULL,'PO','Product Owner');
insert into AspNetRoles values (10,NULL,'OPE','Operator');
insert into AspNetRoles values (11,NULL,'TEAM','Team Member');
insert into AspNetRoles values (12,NULL,'NEW','New Role');
insert into AspNetRoles values (2,NULL,'ARQ','Arquitect');
insert into AspNetRoles values (3,NULL,'DEV','Developer');
insert into AspNetRoles values (4,NULL,'ATC','Agile Team Coach');
insert into AspNetRoles values (5,NULL,'SEC','Security Analyst');
insert into AspNetRoles values (6,NULL,'LT','Tech Lead');
insert into AspNetRoles values (7,NULL,'DEVOPS','Devops Analyst');
insert into AspNetRoles values (8,NULL,'CHL','Chapter Lead');
insert into AspNetRoles values (9,NULL,'TRL','Tribe Lead');


insert into CalendarType values ('Los Trovadores Criollos','Squad');
insert into CalendarType values ('AcselX','Chapter');
insert into CalendarType values ('Los Hermanos Zañartu','Squad');


insert into usercalendartype values ('webmaster',1)
insert into usercalendartype values ('webmaster',2)
insert into usercalendartype values ('webmaster',3)

insert into usercalendartype (username,CalendarTypeId)
select UserName, 1 from dbo.[AspNetUsers] a where a.email not in ('webmaster')

insert into AspNetUsers (Id,AccessFailedCount,ConcurrencyStamp,Email,EmailConfirmed,LockoutEnabled,LockoutEnd,NormalizedEmail,NormalizedUserName,PasswordHash,PhoneNumber,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,UserName,Nombres,ApellidoPaterno,ApellidoMaterno,FechaNacimiento)
values ('0FCCE089-F2F2-4CC9-BF3F-E7120E55A1B0',0,'22522292-f147-487c-af39-8e3809d255db','jesus.torres@mdp.com.pe',1,1,NULL,'jesus.torres@mdp.com.pe','jesus.torres@mdp.com.pe','UABhAHMAcwB3AG8AcgBkADEAIQA=',NULL,0,'5UBJN54F7PXR6KDYQDEM74K7DZRP6VHQ',0,'jesus.torres@mdp.com.pe','Jesús Andy','Torres','Mendoza','2023-08-02')

insert into AspNetUsers (Id,AccessFailedCount,ConcurrencyStamp,Email,EmailConfirmed,LockoutEnabled,LockoutEnd,NormalizedEmail,NormalizedUserName,PasswordHash,PhoneNumber,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,UserName,Nombres,ApellidoPaterno,ApellidoMaterno,FechaNacimiento)
values ('1376263B-C3BA-485B-BF98-F211202DBBA7',0,'22522292-f147-487c-af39-8e3809d255db','jespinoza@pacifico.com.pe',1,1,NULL,'jespinoza@pacifico.com.pe','jespinoza@pacifico.com.pe','UABhAHMAcwB3AG8AcgBkADEAIQA=',NULL,0,'5UBJN54F7PXR6KDYQDEM74K7DZRP6VHQ',0,'jespinoza@pacifico.com.pe','Jorge Wilson','Espinoza','Chamaya','2023-12-16')

insert into AspNetUsers (Id,AccessFailedCount,ConcurrencyStamp,Email,EmailConfirmed,LockoutEnabled,LockoutEnd,NormalizedEmail,NormalizedUserName,PasswordHash,PhoneNumber,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,UserName,Nombres,ApellidoPaterno,ApellidoMaterno,FechaNacimiento)
values ('4904607F-83F2-4B19-9D8A-9FAA761FAE52',0,'22522292-f147-487c-af39-8e3809d255db','marco.pirela@inetum.com',1,1,NULL,'marco.pirela@inetum.com','marco.pirela@inetum.com','UABhAHMAcwB3AG8AcgBkADEAIQA=',NULL,0,'5UBJN54F7PXR6KDYQDEM74K7DZRP6VHQ',0,'marco.pirela@inetum.com','Marco Antonio ','Pierla','Navarro','2023-05-30')

insert into AspNetUsers (Id,AccessFailedCount,ConcurrencyStamp,Email,EmailConfirmed,LockoutEnabled,LockoutEnd,NormalizedEmail,NormalizedUserName,PasswordHash,PhoneNumber,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,UserName,Nombres,ApellidoPaterno,ApellidoMaterno,FechaNacimiento)
values ('5629D776-02C6-4D93-98A9-1B1D3313F906',0,'22522292-f147-487c-af39-8e3809d255db','isaipalavicinih@pacifico.com.pe',1,1,NULL,'isaipalavicinih@pacifico.com.pe','isaipalavicinih@pacifico.com.pe','UABhAHMAcwB3AG8AcgBkADEAIQA=',NULL,0,'5UBJN54F7PXR6KDYQDEM74K7DZRP6VHQ',0,'isaipalavicinih@pacifico.com.pe','Isai Baudy','Palavicini','Hinostroza','2023-08-16')

insert into AspNetUsers (Id,AccessFailedCount,ConcurrencyStamp,Email,EmailConfirmed,LockoutEnabled,LockoutEnd,NormalizedEmail,NormalizedUserName,PasswordHash,PhoneNumber,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,UserName,Nombres,ApellidoPaterno,ApellidoMaterno,FechaNacimiento)
values ('604628FC-FCC1-4A63-974D-1BF21904E841',0,'22522292-f147-487c-af39-8e3809d255db','carlajohnstonc@pacifico.com.pe',1,1,NULL,'carlajohnstonc@pacifico.com.pe','carlajohnstonc@pacifico.com.pe','UABAAHMAcwB3AG8AcgBkAA==',NULL,0,'5UBJN54F7PXR6KDYQDEM74K7DZRP6VHQ',0,'carlajohnstonc@pacifico.com.pe','Carla Denise','Johnston','de la Cuba','1900-01-01')

insert into AspNetUsers (Id,AccessFailedCount,ConcurrencyStamp,Email,EmailConfirmed,LockoutEnabled,LockoutEnd,NormalizedEmail,NormalizedUserName,PasswordHash,PhoneNumber,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,UserName,Nombres,ApellidoPaterno,ApellidoMaterno,FechaNacimiento)
values ('70CF156E-BE17-4B35-B127-9F3892D32D46',0,'22522292-f147-487c-af39-8e3809d255db','aneyra@soaint.com',1,1,NULL,'aneyra@soaint.com','aneyra@soaint.com','UABhAHMAcwB3AG8AcgBkADEAIQA=',NULL,0,'5UBJN54F7PXR6KDYQDEM74K7DZRP6VHQ',0,'aneyra@soaint.com','Antony','Neyra','Hidalgo','2023-06-15')

insert into AspNetUsers (Id,AccessFailedCount,ConcurrencyStamp,Email,EmailConfirmed,LockoutEnabled,LockoutEnd,NormalizedEmail,NormalizedUserName,PasswordHash,PhoneNumber,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,UserName,Nombres,ApellidoPaterno,ApellidoMaterno,FechaNacimiento)
values ('81557B10-8F72-49A3-A866-58526FBE3E3E',0,'22522292-f147-487c-af39-8e3809d255db','billy.fernandez_consultor@mdp.com.pe',1,1,NULL,'billy.fernandez_consultor@mdp.com.pe','billy.fernandez_consultor@mdp.com.pe','UABhAHMAcwB3AG8AcgBkADEAIQA=',NULL,0,'5UBJN54F7PXR6KDYQDEM74K7DZRP6VHQ',0,'billy.fernandez_consultor@mdp.com.pe','Billy George','Fernández','Quillay','2023-08-21')

insert into AspNetUsers (Id,AccessFailedCount,ConcurrencyStamp,Email,EmailConfirmed,LockoutEnabled,LockoutEnd,NormalizedEmail,NormalizedUserName,PasswordHash,PhoneNumber,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,UserName,Nombres,ApellidoPaterno,ApellidoMaterno,FechaNacimiento)
values ('B1F5DE01-A60A-4A33-9EF9-8E43B5E20E3D',0,'22522292-f147-487c-af39-8e3809d255db','julioyanaricoc@pacifico.com.pe',1,1,NULL,'julioyanaricoc@pacifico.com.pe','julioyanaricoc@pacifico.com.pe','UABhAHMAcwB3AG8AcgBkADEAIQA=',NULL,0,'5UBJN54F7PXR6KDYQDEM74K7DZRP6VHQ',0,'julioyanaricoc@pacifico.com.pe','Julio Cesar','Yanarico','Cayo',NULL)

insert into AspNetUsers (Id,AccessFailedCount,ConcurrencyStamp,Email,EmailConfirmed,LockoutEnabled,LockoutEnd,NormalizedEmail,NormalizedUserName,PasswordHash,PhoneNumber,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,UserName,Nombres,ApellidoPaterno,ApellidoMaterno,FechaNacimiento)
values ('B9B789F6-D5B3-4671-B53F-BED967E13F3F',0,'22522292-f147-487c-af39-8e3809d255db','cristhian.gutarra@tivit.com',1,1,NULL,'cristhian.gutarra@tivit.com','cristhian.gutarra@tivit.com','UABhAHMAcwB3AG8AcgBkADEAIQA=',NULL,0,'5UBJN54F7PXR6KDYQDEM74K7DZRP6VHQ',0,'cristhian.gutarra@tivit.com','Cristhian','Gutarra','Leigh','2023-12-15')

insert into AspNetUsers (Id,AccessFailedCount,ConcurrencyStamp,Email,EmailConfirmed,LockoutEnabled,LockoutEnd,NormalizedEmail,NormalizedUserName,PasswordHash,PhoneNumber,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,UserName,Nombres,ApellidoPaterno,ApellidoMaterno,FechaNacimiento)
values ('D81F9976-7E57-4F24-A884-4E614F750FB4',0,'22522292-f147-487c-af39-8e3809d255db','rmelendm@nttdata.com',1,1,NULL,'rmelendm@nttdata.com','rmelendm@nttdata.com','UABhAHMAcwB3AG8AcgBkADEAIQA=',NULL,0,'5UBJN54F7PXR6KDYQDEM74K7DZRP6VHQ',0,'rmelendm@nttdata.com','Ricardo Anthony','Melendres','Martel','2023-08-24')

insert into AspNetUsers (Id,AccessFailedCount,ConcurrencyStamp,Email,EmailConfirmed,LockoutEnabled,LockoutEnd,NormalizedEmail,NormalizedUserName,PasswordHash,PhoneNumber,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,UserName,Nombres,ApellidoPaterno,ApellidoMaterno,FechaNacimiento)
values ('D99957BB-B94D-4DB2-8BED-1B4FC17C51A8',0,'22522292-f147-487c-af39-8e3809d255db','JoseRazuriO@pacifico.com.pe',1,1,NULL,'JoseRazuriO@pacifico.com.pe','JoseRazuriO@pacifico.com.pe','UABhAHMAcwB3AG8AcgBkADEAIQA=',NULL,0,'5UBJN54F7PXR6KDYQDEM74K7DZRP6VHQ',0,'JoseRazuriO@pacifico.com.pe','José Carlos','Razuri','Ozorio','2024-01-23')

insert into AspNetUsers (Id,AccessFailedCount,ConcurrencyStamp,Email,EmailConfirmed,LockoutEnabled,LockoutEnd,NormalizedEmail,NormalizedUserName,PasswordHash,PhoneNumber,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,UserName,Nombres,ApellidoPaterno,ApellidoMaterno,FechaNacimiento)
values ('E598B608-5818-4566-B865-33C843E887A2',0,'22522292-f147-487c-af39-8e3809d255db','mflor@pacifico.com.pe',1,1,NULL,'mflor@pacifico.com.pe','mflor@pacifico.com.pe','UABhAHMAcwB3AG8AcgBkADEAIQA=',NULL,0,'5UBJN54F7PXR6KDYQDEM74K7DZRP6VHQ',0,'mflor@pacifico.com.pe','Mariana Milagros','Flor','Espinoza','2023-08-26')

insert into AspNetUsers (Id,AccessFailedCount,ConcurrencyStamp,Email,EmailConfirmed,LockoutEnabled,LockoutEnd,NormalizedEmail,NormalizedUserName,PasswordHash,PhoneNumber,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,UserName,Nombres,ApellidoPaterno,ApellidoMaterno,FechaNacimiento)
values ('ED1D9E00-76ED-467C-94FE-2F817A9F4089',0,'22522292-f147-487c-af39-8e3809d255db','miguel.fernandez-gargurevich@inetum.com',1,1,NULL,'miguel.fernandez-gargurevich@inetum.com','miguel.fernandez-gargurevich@inetum.com','YQBzAGQAYQBzAGQA',NULL,0,'5UBJN54F7PXR6KDYQDEM74K7DZRP6VHQ',0,'miguel.fernandez-gargurevich@inetum.com','Miguel','Fernandez','Gargurevich','1981-06-25')

insert into AspNetUsers (Id,AccessFailedCount,ConcurrencyStamp,Email,EmailConfirmed,LockoutEnabled,LockoutEnd,NormalizedEmail,NormalizedUserName,PasswordHash,PhoneNumber,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,UserName,Nombres,ApellidoPaterno,ApellidoMaterno,FechaNacimiento)
values ('F789CE1D-4279-47D5-83B9-13F21E6CDBAE',0,'22522292-f147-487c-af39-8e3809d255db','harry.huanilo@mdp.com.pe',1,1,NULL,'harry.huanilo@mdp.com.pe','harry.huanilo@mdp.com.pe','UABhAHMAcwB3AG8AcgBkADEAIQA=',NULL,0,'5UBJN54F7PXR6KDYQDEM74K7DZRP6VHQ',0,'harry.huanilo@mdp.com.pe','Harry Maiker','Huanilo','Mori','2023-09-10')


insert into AspNetUsers (Id,AccessFailedCount,ConcurrencyStamp,Email,EmailConfirmed,LockoutEnabled,LockoutEnd,NormalizedEmail,NormalizedUserName,PasswordHash,PhoneNumber,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,UserName,Nombres,ApellidoPaterno,ApellidoMaterno,FechaNacimiento)
values ('W88957BB-4279-47D5-83B9-13F21E6CDBAE',0,'22522292-f147-487c-af39-8e3809d255db','Leoncio.sanchez-montanez@inetum.com',1,1,NULL,'Leoncio.sanchez-montanez@inetum.com','Leoncio.sanchez-montanez@inetum.com','UABhAHMAcwB3AG8AcgBkADEAIQA=',NULL,0,'5UBJN54F7PXR6KDYQDEM74K7DZRP6VHQ',0,'Leoncio.sanchez-montanez@inetum.com','Leoncio','Sanchez','Montanez','2023-01-01')





insert into AspNetUserRoles (UserId, RoleId) 
select Id, 11 from dbo.[AspNetUsers] a where a.email not in ('webmaster')

insert into AspNetUserRoles values
((select Id from AspNetUserRoles where username = 'webmaster'),0)

insert into [dbo].[Calendar](title,startdate,enddate,description,eventtypeid,eventtypename,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('cumple Miguel','2023-06-25 00:00:00.000','2023-06-26 00:00:00.000','para mi cumple quiero una cena en chillis',1,'birthday',1,'Los Trovadores Criollos','2023-04-18 02:46:24.247',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,startdate,enddate,description,eventtypeid,eventtypename,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('vaca Miguel','2023-05-15 12:00:00.000','2023-06-03 12:00:00.000','',2,'vacation',1,'Los Trovadores Criollos','2023-04-18 15:11:19.517',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,startdate,enddate,description,eventtypeid,eventtypename,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Cumpleaños Cristhian Gutarra','2023-12-15 12:00:00.000','2023-12-16 12:00:00.000','',1,'birthday',1,'Los Trovadores Criollos','2023-04-18 15:11:59.647',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,startdate,enddate,description,eventtypeid,eventtypename,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Cumple de Mariana','2023-08-26 12:00:00.000','2023-08-27 12:00:00.000','Trattoria!!!',1,'birthday',1,'Los Trovadores Criollos','2023-04-18 15:20:55.583',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,startdate,enddate,description,eventtypeid,eventtypename,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Vacaciones MARIANA FLOR','2023-10-09 12:00:00.000','2023-10-14 12:00:00.000','',2,'vacation',1,'Los Trovadores Criollos','2023-04-18 15:22:45.013',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,startdate,enddate,description,eventtypeid,eventtypename,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Cumpleaños Ricardo','2023-08-24 12:00:00.000','2023-08-25 12:00:00.000','Buffet en Rodizzio',1,'birthday',1,'Los Trovadores Criollos','2023-04-18 15:29:26.543',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,startdate,enddate,description,eventtypeid,eventtypename,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Cumpleaños Jesus Andy','2023-08-02 12:00:00.000','2023-08-03 12:00:00.000','',1,'birthday',1,'Los Trovadores Criollos','2023-04-18 15:30:54.747',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,startdate,enddate,description,eventtypeid,eventtypename,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Vacaciones Ricardo','2023-08-21 00:00:00.000','2023-08-26 00:00:00.000','',2,'vacation',1,'Los Trovadores Criollos','2023-04-18 15:36:20.303',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,startdate,enddate,description,eventtypeid,eventtypename,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Cumple de Harry','2023-09-10 00:00:00.000','2023-09-11 00:00:00.000','',1,'birthday',1,'Los Trovadores Criollos','2023-04-20 17:01:22.263',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,startdate,enddate,description,eventtypeid,eventtypename,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Cumpleaños Marco Pirela','2023-05-30 12:00:00.000','2023-05-31 12:00:00.000','',1,'birthday',1,'Los Trovadores Criollos','2023-04-18 15:54:36.197',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,startdate,enddate,description,eventtypeid,eventtypename,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Vacaciones Marco Pirela','2023-06-05 00:00:00.000','2023-06-10 00:00:00.000','',2,'vacation',1,'Los Trovadores Criollos','2023-04-18 15:59:53.277',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,startdate,enddate,description,eventtypeid,eventtypename,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('HB José Rázuri','2024-01-23 12:00:00.000','2024-01-24 12:00:00.000','Pollito',1,'birthday',1,'Los Trovadores Criollos','2023-04-18 16:01:33.010',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,startdate,enddate,description,eventtypeid,eventtypename,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Vacaciones MARIANA FLOR','2023-12-26 12:00:00.000','2024-01-12 12:00:00.000','',2,'vacation',1,'Los Trovadores Criollos','2023-04-24 20:52:00.257',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,startdate,enddate,description,eventtypeid,eventtypename,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Cumpleaños de Jean Reyes','2023-11-02 12:00:00.000','2023-11-03 12:00:00.000','',1,'birthday',1,'Los Trovadores Criollos','2023-05-02 20:49:50.797',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,startdate,enddate,description,eventtypeid,eventtypename,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Cumpleaños Moylan Loo','2023-11-03 12:00:00.000','2023-11-04 12:00:00.000','',1,'birthday',1,'Los Trovadores Criollos','2023-05-02 20:50:26.793',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,startdate,enddate,description,eventtypeid,eventtypename,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Cumpleaños Antony Neyra','2023-06-15 12:00:00.000','2023-06-15 23:00:00.000','',1,'birthday',1,'Los Trovadores Criollos','2023-05-02 20:56:34.313',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,startdate,enddate,description,eventtypeid,eventtypename,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Vacaciones Yahir','2023-07-17 12:00:00.000','2023-07-23 12:00:00.000','Vacaciones Yahir',2,'vacation',1,'Los Trovadores Criollos','2023-06-15 15:16:16.690',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,startdate,enddate,description,eventtypeid,eventtypename,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Vacaciones Jorge','2023-07-19 12:00:00.000','2023-08-01 12:00:00.000','',2,'vacation',1,'Los Trovadores Criollos','2023-07-11 14:40:15.497',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,startdate,enddate,description,eventtypeid,eventtypename,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Vacaciones Jorge','2023-11-21 12:00:00.000','2023-12-07 12:00:00.000','',2,'vacation',1,'Los Trovadores Criollos','2023-07-11 14:40:47.793',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,startdate,enddate,description,eventtypeid,eventtypename,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Vacaciones Jorge','2023-06-12 12:00:00.000','2023-06-20 12:00:00.000','',2,'vacation',1,'Los Trovadores Criollos','2023-07-11 14:41:15.480',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,startdate,enddate,description,eventtypeid,eventtypename,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Cumpleaños Baudy','2023-08-16 12:00:00.000','2023-08-16 23:59:00.000','',1,'birthday',1,'Los Trovadores Criollos','2023-07-14 15:51:52.300',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,startdate,enddate,description,eventtypeid,eventtypename,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Cumple de Jorge','2023-12-16 00:00:00.000','2023-12-17 00:00:00.000','',1,'birthday',1, 'Los Trovadores Criollos','2023-08-17 17:45:34.207',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,startdate,enddate,description,eventtypeid,eventtypename,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('HB José Rázuri','2024-01-23 12:00:00.000','2024-01-24 12:00:00.000','Pollito',1,'birthday',2,'Chapter AcselX','2023-08-17 21:07:48.860',1,'miguel.fernandez-gargurevich@inetum.com','ED1D9E00-76ED-467C-94FE-2F817A9F4089')
insert into [dbo].[Calendar](title,startdate,enddate,description,eventtypeid,eventtypename,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Cumpleaños Billy','2023-08-21 12:00:00.000','2023-08-22 12:00:00.000','',1,'birthday',1,'Los Trovadores Criollos','2023-08-21 19:30:05.223',1,'mflor@pacifico.com.pe','E598B608-5818-4566-B865-33C843E887A2')
insert into [dbo].[Calendar](title,startdate,enddate,description,eventtypeid,eventtypename,calendartypeid,calendartypename,dateCreate,allday,UserName,userCreate) values
('Sprint Retrospective - Q3S5','2023-09-19 14:00:00.000','2023-09-19 15:30:00.000',"Mural:
https://app.mural.co/t/ayllu6528/m/ayllu6528/1695151049809/145d51a04208b2fd4839b0ce8e82756c3a56d5e9?sender=u62f81034f0f69618b8640045",3,'Session',1,'Los Trovadores Criollos','2023-09-19 15:03:15.077',0,'webmaster','0AE9BFB8-A7CA-437C-A203-E41CD1B7B219')

