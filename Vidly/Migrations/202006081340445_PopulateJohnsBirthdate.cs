namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateJohnsBirthdate : DbMigration
    {
        public override void Up()
        {
            Sql("update Customers set DateOfBirth = '1980-01-01' where Id = 1");
        }
        
        public override void Down()
        {
        }
    }
}
