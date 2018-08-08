using LSC.Cross.Transport;
using LSC.Domain.Service;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace LSC.Api.Controllers
{

    [RoutePrefix("api/v1/Animal")]
    public class AnimalController : ApiBaseController<AnimalService, AnimalTransport, int>
    {

        #region Overrides

        protected override IEnumerable<AnimalTransport> FiltroSwitch(string campo, string conteudo)
        {
            IEnumerable<AnimalTransport> ret = null;

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

        protected override AnimalTransport GetByKey(AnimalTransport request)
        {
            return GetByKey(request.AnimalId);
        }

        protected override AnimalTransport GetByKey(int key)
        {
            return _serv.GetByKey(key);
        }

        protected override string GetIdValue(AnimalTransport request)
        {
            return $"{request.AnimalId}";
        }

        protected override void RemoverEntidade(AnimalTransport entidade)
        {
            _serv.Remove(entidade);
        }

        protected override void SalvarEntidade(AnimalTransport entidade, bool isUpdate)
        {
            if (isUpdate)
                _serv.Update(entidade);
            else
                _serv.Add(entidade);
        }

        protected override string ValidaCampos(AnimalTransport entidade)
        {

            StringBuilder critica = new StringBuilder();

            if (string.IsNullOrWhiteSpace(entidade.Descricao))
                critica.Append(string.Format("{0}Descrição não informada", (critica.Length > 0 ? "|" : string.Empty)));

            return critica.ToString();

        }

        protected override bool ValidarId(AnimalTransport request)
        {
            return (request.AnimalId > 0);
        }

        #endregion

        #region Métodos Api

        [Route("Listar")]
        [HttpGet]
        public IEnumerable<AnimalTransport> Buscar()
        {
            return BuscarFiltro("lista", "lista");
        }

        [Route("{id:int}")]
        [HttpGet]
        public AnimalTransport Buscar(int id)
        {
            return Localizar(id);
        }

        [Route("")]
        [HttpPost]
        public HttpResponseMessage Incluir([FromBody]AnimalTransport request)
        {
            return Salvar(request);
        }

        [Route("")]
        [HttpPut]
        public HttpResponseMessage Alterar([FromBody]AnimalTransport request)
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
