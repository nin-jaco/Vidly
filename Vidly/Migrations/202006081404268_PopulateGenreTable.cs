namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenreTable : DbMigration
    {
        public override void Up()
        {
            Sql("insert into Genres (Id,Name) values (1,'Comedy')");
            Sql("insert into Genres (Id,Name) values (2,'Action')");
            Sql("insert into Genres (Id,Name) values (3,'Family')");
            Sql("insert into Genres (Id,Name) values (4,'Romance')");
        }
        
        public override void Down()
        {
        }
    }
}
