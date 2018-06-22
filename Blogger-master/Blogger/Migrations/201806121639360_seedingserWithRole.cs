namespace Blogger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedingserWithRole : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'fd000f4b-0c3a-4bc7-a32c-8eb1fd712167', N'Admin@Blogger.com', 0, N'AGEe+VlchY8bMCdec14yTmo69HrSumUlC1N56IpGkCZxyTr+jE9ibJ1epc/IlKxc+w==', N'59bd4993-6c97-456d-a33f-1a0b394a083d', NULL, 0, 0, NULL, 1, 0, N'Admin@Blogger.com')
                 INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'8f22bdcf-db85-4c66-ac7b-a310646dcd24', N'canManagePost')
                  INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fd000f4b-0c3a-4bc7-a32c-8eb1fd712167', N'8f22bdcf-db85-4c66-ac7b-a310646dcd24')

               ");
        }
        
        public override void Down()
        {
        }
    }
}
