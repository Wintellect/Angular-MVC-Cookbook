namespace MvcAngular.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        MiddleName = c.String(maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Suffix = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Postal",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        LineOne = c.String(nullable: false, maxLength: 50),
                        LineTwo = c.String(maxLength: 50),
                        City = c.String(nullable: false, maxLength: 50),
                        StateProvince = c.String(maxLength: 50),
                        Country = c.String(nullable: false, maxLength: 50),
                        PostalCode = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.Phone",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        Number = c.String(nullable: false, maxLength: 40),
                        NumberType = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.Email",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        Address = c.String(nullable: false, maxLength: 254),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Email", new[] { "PersonId" });
            DropIndex("dbo.Phone", new[] { "PersonId" });
            DropIndex("dbo.Postal", new[] { "PersonId" });
            DropForeignKey("dbo.Email", "PersonId", "dbo.People");
            DropForeignKey("dbo.Phone", "PersonId", "dbo.People");
            DropForeignKey("dbo.Postal", "PersonId", "dbo.People");
            DropTable("dbo.Email");
            DropTable("dbo.Phone");
            DropTable("dbo.Postal");
            DropTable("dbo.People");
        }
    }
}
