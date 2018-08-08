using LSC.Cross.Transport;
using LSC.Domain.Interop;
using LSC.Domain.Service;
using LSC.Infra.Data;
using LSC.Infra.Repository.Entities;
using System;
using System.Data.Entity;

namespace LSC.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {

            using (DomainInterop dmn = new DomainInterop())
            {

                //TesteAnimal(dmn);
                //TestePecuarista(dmn);
                TesteCompra(dmn);

            }

        }

        private static void TesteCompra(DomainInterop dmn)
        {

            using (DbContextTransaction t = dmn.BeginTransaction())
            {

                CompraGadoTransport c;
                CompraGadoItemTransport i1;
                CompraGadoItemTransport i2;

                CompraGadoService sc = dmn.CreateInstance<CompraGadoService>();
                CompraGadoItemService si = dmn.CreateInstance<CompraGadoItemService>();

                c = new CompraGadoTransport()
                {
                    DataEntrega = DateTime.Now.AddDays(10),
                    PecuaristaId = 2
                };
                sc.Add(c);

                i1 = new CompraGadoItemTransport()
                {
                    CompraGadoId = c.CompraGadoId,
                    AnimalId = 1,
                    Preco = 110,
                    Quantidade = 10
                };
                i2 = new CompraGadoItemTransport()
                {
                    CompraGadoId = c.CompraGadoId,
                    AnimalId = 2,
                    Preco = 220,
                    Quantidade = 20
                };
                si.Add(i1);
                si.Add(i2);

                c.CompraGadoItens.Add(i1);
                c.CompraGadoItens.Add(i2);

                t.Commit();

            }

        }

        private static void TestePecuarista(DomainInterop dmn)
        {

            PecuaristaService serv = dmn.CreateInstance<PecuaristaService>();
            PecuaristaTransport p;
            int key;

            p = new PecuaristaTransport()
            {
                Nome = "RODRIGO RODRIGUES"
            };
            serv.Add(p);
            key = p.PecuaristaId;

            p = null;
            p = serv.GetByKey(key);
            p.Nome = "RODRIGO MOURA RODRIGUES";
            serv.Update(p);

            serv.Remove(p);

        }

        private static void TesteAnimal(DomainInterop dmn)
        {
            AnimalService serv = dmn.CreateInstance<AnimalService>();
            AnimalTransport a;
            int key;

            a = new AnimalTransport()
            {
                Descricao = $"Animal - Teste - {DateTime.Now}",
                Preco = 750
            };
            serv.Add(a);
            key = a.AnimalId;

            a = null;
            a = serv.GetByKey(key);
            a.Descricao = "Teste Alterado";
            a.Preco = 2300;
            serv.Update(a);

            serv.Remove(a);
        }
    }
}
