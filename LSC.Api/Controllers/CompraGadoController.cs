using LSC.Cross.Transport;
using LSC.Domain.Service;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace LSC.Api.Controllers
{

    [RoutePrefix("api/v1/CompraGado")]
    public class CompraGadoController : ApiBaseController<CompraGadoService, CompraGadoTransport, int>
    {

        #region Overrides

        
        protected override IEnumerable<CompraGadoTransport> FiltroSwitch(string campo, string conteudo)
        {

            IEnumerable<CompraGadoTransport> ret = null;
            int pecuaristaId;
            DateTime dataInicial;
            DateTime dataFinal;

            switch (campo.ToLower())
            {
                case "lista":
                    ret = _serv.GetAll();
                    break;
                case "pecuarista":
                    pecuaristaId = int.Parse(conteudo);
                    ret = _serv.GetByPecuarista(pecuaristaId);
                    break;
                case "periodo":
                    string[] datas = conteudo.Split(',');
                    dataInicial = DateTime.Parse(datas[0]);
                    dataFinal = DateTime.Parse(datas[1]);
                    ret = _serv.GetByPeriodo(dataInicial, dataFinal);
                    break;
                case "pecuarista_periodo":
                    string[] parametros = conteudo.Split(',');
                    pecuaristaId = int.Parse(parametros[0]);
                    dataInicial = DateTime.Parse(parametros[1]);
                    dataFinal = DateTime.Parse(parametros[2]);
                    ret = _serv.GetByPeriodo(dataInicial, dataFinal);
                    break;
                default:
                    throw CriaHttpResponseException(HttpStatusCode.NotFound, "Campo de filtro inválido");
            }

            return ret;

        }

        protected override CompraGadoTransport GetByKey(CompraGadoTransport request)
        {
            return GetByKey(request.CompraGadoId);
        }

        protected override CompraGadoTransport GetByKey(int key)
        {
            return _serv.GetByKey(key);
        }

        protected override string GetIdValue(CompraGadoTransport request)
        {
            return $"{request.CompraGadoId}";
        }

        protected override void RemoverEntidade(CompraGadoTransport entidade)
        {
            _serv.RemoveComplete(entidade);
        }

        protected override void SalvarEntidade(CompraGadoTransport entidade, bool isUpdate)
        {
            if (isUpdate)
                _serv.UpdateComplete(entidade);
            else
                _serv.AddComplete(entidade);
        }

        protected override string ValidaCampos(CompraGadoTransport entidade)
        {

            StringBuilder critica = new StringBuilder();

            if (entidade.PecuaristaId <= 0)
                critica.Append(string.Format("{0}Id do pecuarista inválido", (critica.Length > 0 ? "|" : string.Empty)));

            if (entidade.CompraGadoItens.Count.Equals(0))
                critica.Append(string.Format("{0}A compra deve conter ao menos um item", (critica.Length > 0 ? "|" : string.Empty)));
            else
            {
                int pos = 0;
                foreach (CompraGadoItemTransport i in entidade.CompraGadoItens)
                {

                    if (i.AnimalId <= 0)
                        critica.Append($"{(critica.Length > 0 ? "|" : string.Empty)}Id do animal inválido - item {pos}");

                    if (i.Quantidade <= 0)
                        critica.Append($"{(critica.Length > 0 ? "|" : string.Empty)}Quantidade inválida - item {pos}");

                    if (i.Preco < 0)
                        critica.Append($"{(critica.Length > 0 ? "|" : string.Empty)}Preço inválido - item {pos}");

                    pos++;
                }
            }

            return critica.ToString();

        }

        protected override bool ValidarId(CompraGadoTransport request)
        {
            return (request.CompraGadoId > 0);
        }

        #endregion

        #region Métodos Api

        [Route("Listar")]
        [HttpGet]
        public IEnumerable<CompraGadoTransport> Buscar()
        {
            return BuscarFiltro("lista", "lista");
        }

        [Route("Listar/Pecuarista/{pecuaristaId:int}")]
        [HttpGet]
        public IEnumerable<CompraGadoTransport> BuscarPecuarista(int pecuaristaId)
        {
            return BuscarFiltro("pecuarista", pecuaristaId.ToString());
        }

        [Route("Listar/Periodo/{dataInicial}/{dataFinal}")]
        [HttpGet]
        public IEnumerable<CompraGadoTransport> BuscarPeriodo(string dataInicial, string dataFinal)
        {
            return BuscarFiltro("periodo", $"{dataInicial},{dataFinal}");
        }

        [Route("Listar/PeriodoPecuarista/{pecuaristaId:int}/{dataInicial}/{dataFinal}")]
        [HttpGet]
        public IEnumerable<CompraGadoTransport> BuscarPeriodo(int pecuaristaId, string dataInicial, string dataFinal)
        {
            return BuscarFiltro("pecuarista_periodo", $"{pecuaristaId},{dataInicial},{dataFinal}");
        }

        [Route("{id:int}")]
        [HttpGet]
        public CompraGadoTransport Buscar(int id)
        {
            return Localizar(id);
        }

        [Route("")]
        [HttpPost]
        public HttpResponseMessage Incluir([FromBody]CompraGadoTransport request)
        {
            return Salvar(request);
        }

        [Route("")]
        [HttpPut]
        public HttpResponseMessage Alterar([FromBody]CompraGadoTransport request)
        {
            return Salvar(request);
        }

        [Route("{id:int}")]
        [HttpDelete]
        public HttpResponseMessage Excluir(string id)
        {
            return Remover(id);
        }

        #endregion

    }

}
