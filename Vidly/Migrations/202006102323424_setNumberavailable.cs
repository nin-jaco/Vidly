namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setNumberavailable : DbMigration
    {
        public override void Up()
        {
            Sql("update Movies set NumberAvailable = NumberInStock");
        }
        
        public override void Down()
        {
        }
    }
}
