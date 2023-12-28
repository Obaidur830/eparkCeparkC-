create table Product(
proID int primary key identity,
pName varchar(50),
pCatID int,
pBarcode varchar(50),
pCost float,
pPrice float,
pImage image
);