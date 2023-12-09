CREATE TABLE "ParentProducts" (
  "Id" int PRIMARY KEY,
  "CategoryId" int NOT NULL,
  "Description" nvarchar(500),
  "State" int NOT NULL DEFAULT 1,
  "CreatedAt" datettime NOT NULL DEFAULT 'now()',
  "UpdatedAt" datettime,
  "ProcessedBy" int NOT NULL DEFAULT 1
);

CREATE TABLE "Products" (
  "Id" int PRIMARY KEY,
  "ParentProductId" int,
  "Name" nvarchar(500),
  "Description" nvarchar(500),
  "State" int NOT NULL DEFAULT 1,
  "CreatedAt" datettime NOT NULL DEFAULT 'now()',
  "UpdatedAt" datettime,
  "ProcessedBy" int NOT NULL DEFAULT 1
);

CREATE TABLE "ProductAttributes" (
  "Id" int PRIMARY KEY,
  "ProductId" int,
  "AttributeId" int,
  "AttributeValueId" int,
  "AttributeName" nvarchar(500),
  "AttributeValueName" nvarchar(500),
  "State" int NOT NULL DEFAULT 1,
  "CreatedAt" datettime NOT NULL DEFAULT 'now()',
  "UpdatedAt" datettime,
  "ProcessedBy" int NOT NULL DEFAULT 1
);

CREATE TABLE "ProductPictures" (
  "Id" int PRIMARY KEY,
  "ProductId" int,
  "Order" int,
  "LocalPath" nvarchar(500),
  "CdnPath" nvarchar(500),
  "IsApproved" bool,
  "State" int NOT NULL DEFAULT 1,
  "CreatedAt" datettime NOT NULL DEFAULT 'now()',
  "UpdatedAt" datettime,
  "ProcessedBy" int NOT NULL DEFAULT 1
);

CREATE TABLE "ProductSellers" (
  "Id" int PRIMARY KEY,
  "ProductId" int,
  "SellerId" int,
  "Price" decimal
);

CREATE TABLE "Attributes" (
  "Id" int PRIMARY KEY,
  "Name" nvarchar(500),
  "State" int NOT NULL DEFAULT 1,
  "CreatedAt" datettime NOT NULL DEFAULT 'now()',
  "UpdatedAt" datettime,
  "ProcessedBy" int NOT NULL DEFAULT 1
);

CREATE TABLE "AttributeValues" (
  "Id" int PRIMARY KEY,
  "AttributeId" int,
  "Name" nvarchar(500),
  "State" int NOT NULL DEFAULT 1,
  "CreatedAt" datettime NOT NULL DEFAULT 'now()',
  "UpdatedAt" datettime,
  "ProcessedBy" int NOT NULL DEFAULT 1
);

CREATE TABLE "Categories" (
  "Id" int PRIMARY KEY,
  "Name" nvarchar(500),
  "ParentId" int,
  "State" int NOT NULL DEFAULT 1,
  "CreatedAt" datettime NOT NULL DEFAULT 'now()',
  "UpdatedAt" datettime,
  "ProcessedBy" int NOT NULL DEFAULT 1
);

CREATE TABLE "CategoryAttributes" (
  "Id" int PRIMARY KEY,
  "CategoryId" int,
  "AttributeId" int,
  "AttributeValueId" int,
  "IsRequired" bool,
  "State" int NOT NULL DEFAULT 1,
  "CreatedAt" datettime NOT NULL DEFAULT 'now()',
  "UpdatedAt" datettime,
  "ProcessedBy" int NOT NULL DEFAULT 1
);

CREATE TABLE "Sellers" (
  "Id" int PRIMARY KEY,
  "Name" nvarchar(500) NOT NULL,
  "Vkn" nvarchar(10) NOT NULL,
  "City" nvarchar(100) NOT NULL,
  "State" int NOT NULL DEFAULT 1,
  "CreatedAt" datettime NOT NULL DEFAULT 'now()',
  "UpdatedAt" datettime,
  "ProcessedBy" int NOT NULL DEFAULT 1
);

CREATE TABLE "Currency" (
  "Id" int PRIMARY KEY,
  "Name" nvarchar(50),
  "Abbrvation" nvarchar(50),
  "Value" decimal,
  "LiveValue" decimal,
  "State" int NOT NULL DEFAULT 1,
  "CreatedAt" datettime NOT NULL DEFAULT 'now()',
  "UpdatedAt" datettime,
  "ProcessedBy" int NOT NULL DEFAULT 1
);

CREATE TABLE "Users" (
  "Id" int PRIMARY KEY,
  "UserRoleId" int,
  "Name" nvarchar(500),
  "Email" nvarchar(500),
  "Password" varbinary(max),
  "State" int NOT NULL DEFAULT 1,
  "CreatedAt" datettime NOT NULL DEFAULT 'now()',
  "UpdatedAt" datettime,
  "ProcessedBy" int NOT NULL DEFAULT 1
);

CREATE TABLE "UserRoles" (
  "Id" int PRIMARY KEY,
  "Name" nvarchar(100),
  "State" int NOT NULL DEFAULT 1,
  "CreatedAt" datettime NOT NULL DEFAULT 'now()',
  "UpdatedAt" datettime,
  "ProcessedBy" int NOT NULL DEFAULT 1
);

CREATE TABLE "Permissions" (
  "Id" int PRIMARY KEY,
  "Name" nvarchar(100),
  "Key" nvarchar(100),
  "Description" nvarchar(500),
  "State" int NOT NULL DEFAULT 1,
  "CreatedAt" datettime NOT NULL DEFAULT 'now()',
  "UpdatedAt" datettime,
  "ProcessedBy" int NOT NULL DEFAULT 1
);

CREATE TABLE "UserRolePermission" (
  "Id" int PRIMARY KEY,
  "PermissionId" int,
  "UserRoleId" int,
  "State" int NOT NULL DEFAULT 1,
  "CreatedAt" datettime NOT NULL DEFAULT 'now()',
  "UpdatedAt" datettime,
  "ProcessedBy" int NOT NULL DEFAULT 1
);

CREATE TABLE "Sessions" (
  "Id" int PRIMARY KEY,
  "UserId" int,
  "TokenKey" nvarchar(50),
  "Token" nvarchar(1000),
  "ExpiresAt" datettime NOT NULL,
  "State" int NOT NULL DEFAULT 1,
  "CreatedAt" datettime NOT NULL DEFAULT 'now()',
  "UpdatedAt" datettime,
  "ProcessedBy" int NOT NULL DEFAULT 1
);

ALTER TABLE "Products" ADD FOREIGN KEY ("ParentProductId") REFERENCES "ParentProducts" ("Id");

ALTER TABLE "ProductAttributes" ADD FOREIGN KEY ("ProductId") REFERENCES "Products" ("Id");

ALTER TABLE "ProductAttributes" ADD FOREIGN KEY ("AttributeId") REFERENCES "Attributes" ("Id");

ALTER TABLE "ProductAttributes" ADD FOREIGN KEY ("AttributeValueId") REFERENCES "AttributeValues" ("Id");

ALTER TABLE "ProductPictures" ADD FOREIGN KEY ("ProductId") REFERENCES "Products" ("Id");

ALTER TABLE "ProductSellers" ADD FOREIGN KEY ("ProductId") REFERENCES "Products" ("Id");

ALTER TABLE "ProductSellers" ADD FOREIGN KEY ("SellerId") REFERENCES "Sellers" ("Id");

ALTER TABLE "AttributeValues" ADD FOREIGN KEY ("AttributeId") REFERENCES "Attributes" ("Id");

ALTER TABLE "CategoryAttributes" ADD FOREIGN KEY ("CategoryId") REFERENCES "Categories" ("Id");

ALTER TABLE "CategoryAttributes" ADD FOREIGN KEY ("AttributeId") REFERENCES "Attributes" ("Id");

ALTER TABLE "CategoryAttributes" ADD FOREIGN KEY ("AttributeValueId") REFERENCES "AttributeValues" ("Id");

ALTER TABLE "Users" ADD FOREIGN KEY ("UserRoleId") REFERENCES "UserRoles" ("Id");

ALTER TABLE "UserRolePermission" ADD FOREIGN KEY ("PermissionId") REFERENCES "Permissions" ("Id");

ALTER TABLE "UserRolePermission" ADD FOREIGN KEY ("UserRoleId") REFERENCES "UserRoles" ("Id");

ALTER TABLE "Sessions" ADD FOREIGN KEY ("UserId") REFERENCES "Users" ("Id");
