create table Customer (
   Id                   INTEGER        PRIMARY KEY           autoincrement,
   FirstName            text           not null,
   LastName             text           not null,
   City                 text           ,
   Country              text           ,
   Phone                text           
);

create table "Order" (
   Id                   INTEGER       PRIMARY KEY            autoincrement,
   OrderDate            text          not null,
   OrderNumber          text          ,
   CustomerId           INTEGER       not null,
   TotalAmount          real          
);

create table OrderItem (
   Id                   INTEGER        PRIMARY KEY           autoincrement,
   OrderId              INTEGER        not null,
   ProductId            INTEGER        not null,
   UnitPrice            real           not null default 0,
   Quantity             INTEGER        not null default 1
);

create table Product (
   Id                   INTEGER        PRIMARY KEY           ,
   ProductName          text           not null,
   SupplierId           INTEGER        not null,
   UnitPrice            real           ,
   Package              text           null,
   IsDiscontinued       bit            not null default 0
);

create table Supplier (
   Id                   INTEGER       PRIMARY KEY          autoincrement,
   CompanyName          text          not null,
   ContactName          text          ,
   ContactTitle         text          ,
   City                 text          ,
   Country              text          ,
   Phone                text          ,
   Fax                  text          
);

