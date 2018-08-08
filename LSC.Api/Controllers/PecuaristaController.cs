using LSC.Cross.Transport;
using LSC.Domain.Service;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace LSC.Api.Controllers
{

    [RoutePrefix("api/v1/Pecuarista")]
    public class PecuaristaController : ApiBaseController<PecuaristaService, PecuaristaTransport, int>
    {

        #region Overrides

        protected override IEnumerable<PecuaristaTransport> FiltroSwitch(string campo, string conteudo)
        {

            IEnumerable<PecuaristaTransport> ret = null;

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

        protected override PecuaristaTransport GetByKey(PecuaristaTransport request)
        {
            return GetByKey(request.PecuaristaId);
        }

        protected override PecuaristaTransport GetByKey(int key)
        {
            return _serv.GetByKey(key);
        }

        protected override string GetIdValue(PecuaristaTransport request)
        {
            return $"{request.PecuaristaId}";
        }

        protected override void RemoverEntidade(PecuaristaTransport entidade)
        {
            _serv.Remove(entidade);
        }

        protected override void SalvarEntidade(PecuaristaTransport entidade, bool isUpdate)
        {
            if (isUpdate)
                _serv.Update(entidade);
            else
                _serv.Add(entidade);
        }

        protected override string ValidaCampos(PecuaristaTransport entidade)
        {

            StringBuilder critica = new StringBuilder();

            if (string.IsNullOrWhiteSpace(entidade.Nome))
                critica.Append(string.Format("{0}Nome não informado", (critica.Length > 0 ? "|" : string.Empty)));

            return critica.ToString();

        }

        protected override bool ValidarId(PecuaristaTransport request)
        {
            return (request.PecuaristaId > 0);
        }

        #endregion

        #region Métodos Api

        [Route("Listar")]
        [HttpGet]
        public IEnumerable<PecuaristaTransport> Buscar()
        {
            return BuscarFiltro("lista", "lista");
        }

        [Route("{id:int}")]
        [HttpGet]
        public PecuaristaTransport Buscar(int id)
        {
            return Localizar(id);
        }

        [Route("")]
        [HttpPost]
        public HttpResponseMessage Incluir([FromBody]PecuaristaTransport request)
        {
            return Salvar(request);
        }

        [Route("")]
        [HttpPut]
        public HttpResponseMessage Alterar([FromBody]PecuaristaTransport request)
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
