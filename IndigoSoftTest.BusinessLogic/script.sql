CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;
CREATE TABLE "IpAddresses" (
    "Id" uuid NOT NULL,
    "Ip" text NOT NULL,
    "IpAddressVersion" integer NOT NULL,
    CONSTRAINT "PK_IpAddresses" PRIMARY KEY ("Id")
);

CREATE TABLE "Users" (
    "UserId" numeric(20,0) NOT NULL,
    CONSTRAINT "PK_Users" PRIMARY KEY ("UserId")
);

CREATE TABLE "UserIps" (
    "Id" uuid NOT NULL,
    "UserId" numeric(20,0) NOT NULL,
    "IpAddressId" uuid NOT NULL,
    "ConnectionDate" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_UserIps" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_UserIps_IpAddresses_IpAddressId" FOREIGN KEY ("IpAddressId") REFERENCES "IpAddresses" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_UserIps_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("UserId") ON DELETE CASCADE
);

CREATE INDEX "IX_IpAddresses_Ip" ON "IpAddresses" ("Ip");

CREATE INDEX "IX_UserIps_IpAddressId" ON "UserIps" ("IpAddressId");

CREATE INDEX "IX_UserIps_UserId" ON "UserIps" ("UserId");

CREATE INDEX "IX_Users_UserId" ON "Users" ("UserId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250430062419_InitMigration', '9.0.4');

COMMIT;

