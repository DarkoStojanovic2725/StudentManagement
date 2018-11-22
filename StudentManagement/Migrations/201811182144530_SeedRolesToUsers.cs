namespace StudentManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedRolesToUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'0774fe9d-4281-4f7a-98e7-a700f61d884d', N'darko@gmail.com', 0, N'AIUvwaI5gCREH3WWFbxXNM1NuBDjY3FzC49Egx5L5wzLK+Faw9vvPjzx3IYZGlLK0g==', N'8bcaba6f-5e46-4a4a-80db-1a43f091e110', NULL, 0, 0, NULL, 1, 0, N'darko@gmail.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'3ca0af66-3dcb-4821-bfdf-0f5d8eac6983', N'profesor@gmail.com', 0, N'AA57dhNQDp0FCXw09fDbrMIRVcGfXErwgAJ1wn5T7iLijpC+Vg9Bc32X8Uw/32ggIA==', N'7f0d6b94-eba2-473c-bd7c-12f093e605dc', NULL, 0, 0, NULL, 1, 0, N'profesor@gmail.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'76e3ff10-43b7-4adf-bad7-b1acfe61dd84', N'Profesor')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3ca0af66-3dcb-4821-bfdf-0f5d8eac6983', N'76e3ff10-43b7-4adf-bad7-b1acfe61dd84')

");
        }
        
        public override void Down()
        {
        }
    }
}
