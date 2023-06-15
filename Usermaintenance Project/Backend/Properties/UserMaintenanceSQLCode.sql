use master
go

If exists (select * from sys.databases where name='UserMaintenanceWeb')
	drop database UserMaintenanceWeb	
	
go

create database UserMaintenanceWeb
go

Use UserMaintenanceWeb
go

-- -----------------------------------------------------
-- Table tblUserSQ
-- -----------------------------------------------------
Create table tblUserSQ(
	usrSQ varchar(200) not null) 
go

Alter table tblUserSQ add constraint PK_tblUserSQ_usrSQ primary key(usrSQ)
go

-- -----------------------------------------------------
-- Table tblGICountry
-- -----------------------------------------------------
Create table tblGICountry(
	countryId int not null identity(1,1),
	countryName varchar(100),
	countryStatus bit)
go

Alter table tblGICountry add constraint PK_tblGICountry_countryId primary key(countryId)
go

-- -----------------------------------------------------
-- Table tblGIState
-- -----------------------------------------------------
Create table tblGIState(
	stateId int not null identity(1,1),
	stateName varchar(100),
	countryId int,
	stateStatus bit)
go

Alter table tblGIState add constraint PK_tblGIState_stateId primary key(stateId)
go

Alter table tblGIState add constraint FK_tblGIState_countryId foreign key(countryId) references tblGICountry(countryId) on delete cascade on update cascade
go

-- -----------------------------------------------------
-- Table tblGICity
-- -----------------------------------------------------
Create table tblGICity(
	cityId int not null identity(1,1),
	cityName varchar(100),
	stateId int,
	cityStatus bit)
go

Alter table tblGICity add constraint PK_tblGICity_cityId primary key(cityId)
go

Alter table tblGICity add constraint FK_tblGICity_stateId foreign key(stateId) references tblGIState(stateId) on delete cascade on update cascade
go

-- -----------------------------------------------------
-- Table tblUser
-- -----------------------------------------------------
Create table tblUser(
	usrId varchar(10) not null, 
	usrPwd varchar(10),
	usrSQ varchar(200),
	usrSA varchar(50),
	usrType varchar(10),	
	usrLastLogin Datetime,
	usrStatus varchar(10),--active, inactive
	)
go

Alter table tblUser add constraint PK_tblUser_usrId primary key(usrId)
go
-- -----------------------------------------------------
-- Table tblUserDetail
-- -----------------------------------------------------
Create table tblUserDetail(
	usrId varchar(10) not null, 
	usrName varchar(100),
	usrDOB datetime,
	usrGender varchar(10),--male, female
	usrEmail varchar(100),
	usrStreet varchar(100),
	usrCountry int,
	usrState int,
	usrCity int,
	usrPIN varchar(10),
	usrMobile varchar(10))
go

Alter table tblUserDetail add constraint PK_tblUserDetail_usrId primary key(usrId)
go
Alter table tblUserDetail add constraint FK_tblUserDetail_usrId foreign key(usrId) references tblUser(usrId) on delete cascade on update cascade
go
Alter table tblUserDetail add constraint FK_tblUserDetail_usrCountry foreign key(usrCountry) references tblGICountry(countryId) on delete no action on update no action
go
ALTER TABLE dbo.tblUserDetail NOCHECK CONSTRAINT FK_tblUserDetail_usrCountry
go
Alter table tblUserDetail add constraint FK_tblUserDetail_usrState foreign key(usrState) references tblGIState(stateId) on delete no action on update no action
go
ALTER TABLE dbo.tblUserDetail NOCHECK CONSTRAINT FK_tblUserDetail_usrState
go
Alter table tblUserDetail add constraint FK_tblUserDetail_usrCity foreign key(usrCity) references tblGICity(cityId) on delete no action on update no action
go
ALTER TABLE dbo.tblUserDetail NOCHECK CONSTRAINT FK_tblUserDetail_usrCity
go

-- -----------------------------------------------------
-- Insert values in the table tblUserSQ
-- -----------------------------------------------------
insert into tblUserSQ (usrSQ) values
('Who is your childhood hero?'),
('What is your pets name?'), 
('What is your favorite song?'),
('What is your place of birth?');
go
-- -----------------------------------------------------
-- Insert values in the table tblGICountry
-- -----------------------------------------------------
SET IDENTITY_INSERT tblGICountry ON
insert into tblGICountry (countryId,countryName,countryStatus)values
(1,'India',1),
(2,'USA',1),
(3,'UK',1),
(4,'Nepal',1),
(5,'Sri Lanka',1),
(6,'Bhutan',1),
(7,'Tibet',1),
(8,'Bangladesh',1),
(9,'Pakistan',0)
SET IDENTITY_INSERT tblGICountry OFF 
go
-- -----------------------------------------------------
-- Insert values in the table tblGIState
-- -----------------------------------------------------
SET IDENTITY_INSERT tblGIState ON
insert into tblGIState (stateId,stateName,countryId,stateStatus)values
(1,'Punjab',1,1),
(2,'Himachal Pradesh',1,1),
(3,'Gujarat',1,1),
(4,'Haryana',1,1),
(5,'Jammu and Kashmir',1,1),
(6,'Kerala',1,1),
(7,'Mizoram',1,1),
(8,'Rajasthan',1,1),
(9,'Tripura',1,1),
(10,'West Bengal',1,1),

(11,'Central',5,1),
(12,'Eastern',5,1),
(13,'North Central',5,1),
(14,'Northern',5,1),
(15,'North Western',5,1),
(16,'Sabaragamuwa',5,1),
(17,'Southern',5,1),
(18,'Uva',5,1),
(19,'Western',5,1),

(20,'Alabama',2,1),
(21,'Alaska',2,1),
(22,'California',2,1),
(23,'Colorado',2,1),
(24,'Florida',2,1),
(25,'Georgia',2,1),
(26,'Hawaii',2,1),
(27,'Indiana',2,1),
(28,'Maryland',2,1),
(29,'New Jersey',2,1),
(30,'New Mexico',2,1),
(31,'New York',2,1),
(32,'Pennsylvania',2,1),
(33,'Texas',2,1),
(34,'Virginia',2,1),
(35,'Washington',2,1)
SET IDENTITY_INSERT tblGIState OFF 
go
-- -----------------------------------------------------
-- Insert values in the table tblGICity
-- -----------------------------------------------------
SET IDENTITY_INSERT tblGICity ON
insert into tblGICity (cityId,cityName,stateId,cityStatus)values
(1,'Chandigarh',1,1),
(2,'Amritsar',1,1),
(3,'Barnala',1,1),
(4,'Bathinda',1,1),
(5,'Faridkot',1,1),
(6,'Fazilka',1,1),
(7,'Hoshiarpur',1,1),
(8,'Jalandhar',1,1),
(9,'Ludhiana',1,1),
(10,'Pathankot',1,1),
(11,'Rupnagar',1,1),
(12,'Ajitgarh (Mohali)',1,1),    

(13,'Shimla',2,1),
(14,'Kangra',2,1),
(15,'Hamirpur',2,1)   

SET IDENTITY_INSERT tblGICity OFF 
go
-- -----------------------------------------------------
-- Insert values in the table tblUser
-- -----------------------------------------------------
insert into tblUser (usrId,usrPwd,usrSQ,usrSA,usrType,usrLastLogin,usrStatus)values
('admin','admin','Who is your childhood hero?','superman','admin',null,'active'),
('u101','user1','Who is your childhood hero?','heman','user',null,'active'),
('u102','user2','What is your pets name?','pepper','user',null,'active'),
('u103','user3','What is your place of birth?','delhi','user',null,'active');
go
-- -----------------------------------------------------
-- Insert values in the table tblUserDetail
-- -----------------------------------------------------
insert into tblUserDetail(usrId,usrName,usrDOB,usrGender,usrEmail,usrStreet,usrCountry,usrState,usrCity,usrPIN,usrMobile)values
('admin','Matin','1987-01-01','Male','martin@yahoo.com','#145 Sector 90A',1,1,1,'160090','9541256894'),
('u101','Harry','1986-09-05','Male','harry101@gmail.com','#25 Sector 10A',1,1,1,'160010','8745874589'),
('u102','Jestin','1984-11-09','Male','justin91@gmail.com','#91 Sector 15A',1,1,1,'160015','7895698546'),
('u103','Maria','1983-07-06','Female','maria198@ymail.com','#88 Sector 25A',1,1,1,'160025','7325489568')
go