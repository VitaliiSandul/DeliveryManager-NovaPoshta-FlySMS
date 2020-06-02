--create database NovaPoshta;
--go

--use NovaPoshta;
--go

--create table Delivery
--(
--	DeliveryId int primary key identity(1,1),	
--	DeliveryDescription varchar(200),
--	DateOper Date,
--	Price money not null,
--	CustomerPhone varchar(15) not null,
--	CustomerName varchar(100) not null,
--	City varchar(100) not null,
--	NumStorage int,
--	WeightMax float,
--	TTN varchar(14),
--	DateArrival Date
--);

--insert into Delivery(DeliveryDescription, DateOper, Price, CustomerPhone, CustomerName, City, NumStorage)
--values ('Шампунь', '2020-05-15', 70, '0983331229','Сандул Виталий','Вишневое', 5),
--('Мыло', '2020-05-16',20, '0983331229','Сандул Виталий','Вишневое', 5),
--('Порошок', '2020-05-17',50, '0983331229','Сандул Виталий','Вишневое', 5),
--('Книга Программирование', '2020-05-18', 200, '0983331229','Сандул Виталий','Вишневое', 5);