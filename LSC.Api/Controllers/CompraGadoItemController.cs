using LSC.Cross.Transport;
using LSC.Domain.Service;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace LSC.Api.Controllers
{

    [RoutePrefix("api/v1/CompraGadoItem")]
    public class CompraGadoItemController : ApiBaseController<CompraGadoItemService, CompraGadoItemTransport, int>
    {

        #region Overrides

        protected override IEnumerable<CompraGadoItemTransport> FiltroSwitch(string campo, string conteudo)
        {
            IEnumerable<CompraGadoItemTransport> ret = null;

            switch (campo.ToLower())
            {
                case "lista":
                    ret = _serv.GetAll();
                    break;
                default:
                    throw CriaHttpResponseException(HttpStatusCode.NotFound, "Campo de filtro inválido");
            }

            return ret;
        }

        protected override CompraGadoItemTransport GetByKey(CompraGadoItemTransport request)
        {
            return GetByKey(request.CompraGadoItemId);
        }

        protected override CompraGadoItemTransport GetByKey(int key)
        {
            return _serv.GetByKey(key);
        }

        protected override string GetIdValue(CompraGadoItemTransport transport)
        {
            return $"{transport.CompraGadoItemId}";
        }

        protected override void RemoverEntidade(CompraGadoItemTransport transport)
        {
            _serv.Remove(transport);
        }

        protected override void SalvarEntidade(CompraGadoItemTransport entidade, bool isUpdate)
        {
            if (isUpdate)
                _serv.Update(entidade);
            else
                _serv.Add(entidade);
        }

        protected override string ValidaCampos(CompraGadoItemTransport entidade)
        {

            StringBuilder critica = new StringBuilder();


            if (entidade.AnimalId <= 0)
                critica.Append($"{(critica.Length > 0 ? "|" : string.Empty)}Id do animal inválido");

            if (entidade.Quantidade <= 0)
                critica.Append($"{(critica.Length > 0 ? "|" : string.Empty)}Quantidade inválida");

            if (entidade.Preco < 0)
                critica.Append($"{(critica.Length > 0 ? "|" : string.Empty)}Preço inválido");


            return critica.ToString();

        }

        protected override bool ValidarId(CompraGadoItemTransport request)
        {
            return (request.CompraGadoItemId > 0);
        }

        #endregion

        #region Métodos Api

        [Route("Listar")]
        [HttpGet]
        public IEnumerable<CompraGadoItemTransport> Buscar()
        {
            return BuscarFiltro("lista", "lista");
        }

        [Route("{id:int}")]
        [HttpGet]
        public CompraGadoItemTransport Buscar(int id)
        {
            return Localizar(id);
        }

        [Route("")]
        [HttpPost]
        public HttpResponseMessage Incluir([FromBody]CompraGadoItemTransport request)
        {
            return Salvar(request);
        }

        [Route("")]
        [HttpPut]
        public HttpResponseMessage Alterar([FromBody]CompraGadoItemTransport request)
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
