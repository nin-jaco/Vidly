namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'64fb4bf4-e510-482b-9bdf-b02945da775c', N'bettasnack@gmail.com', 0, N'AIBQ/04Dewb0ScJgoi9t7VhVqzG6fH/g1bMEOOssZds9bDaSnSaidgYivUhlfu21OA==', N'51461d98-c748-435f-ac09-d4b96cdbf752', NULL, 0, 0, NULL, 1, 0, N'bettasnack@gmail.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'92f43c5d-a73d-4550-8df5-04c4d7b0cbe3', N'jaco.kleynhans@icloud.com', 0, N'AD4YLa+1YQMuEbyy6WrrT6JE2RlNIaKTkxI3TLN1jx5mw2S1uBkyxyZpYT+yaJcwyw==', N'ea16b60c-6f1c-4a9b-882d-b5548479a77d', NULL, 0, 0, NULL, 1, 0, N'jaco.kleynhans@icloud.com')
INSERT INTO [dbo].[AspNetRoles](Id,Name) values (N'a6ad3bc7-0eb1-473b-a3ab-cb183aaa0f43',	'CanManageMovies')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'92f43c5d-a73d-4550-8df5-04c4d7b0cbe3', N'a6ad3bc7-0eb1-473b-a3ab-cb183aaa0f43')
");
        }
        
        public override void Down()
        {
        }
    }
}
