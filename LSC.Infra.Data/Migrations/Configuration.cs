namespace LSC.Infra.Data.Migrations
{
    using LSC.Infra.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LSC.Infra.Data.LSCContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(LSC.Infra.Data.LSCContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            Pecuarista p1 = new Pecuarista() { Nome = "JOAO JOSE" };
            Pecuarista p2 = new Pecuarista() { Nome = "JOSE MARIA" };
            Pecuarista p3 = new Pecuarista() { Nome = "MARIA JOAO" };

            context.Pecuarista.Add(p1);
            context.Pecuarista.Add(p2);
            context.Pecuarista.Add(p3);

            Animal a1 = new Animal() { Descricao = "ANIMAL A1", Preco = 600 };
            Animal a2 = new Animal() { Descricao = "ANIMAL B2", Preco = 750 };
            Animal a3 = new Animal() { Descricao = "ANIMAL C3", Preco = 900 };

            context.Animal.Add(a1);
            context.Animal.Add(a2);
            context.Animal.Add(a3);

            context.SaveChanges();

        }
    }
}
