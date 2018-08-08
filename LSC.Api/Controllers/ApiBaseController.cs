using LSC.Api.Models;
using LSC.Cross.Interface;
using LSC.Cross.Tools;
using LSC.Domain.Interface;
using LSC.Domain.Interop;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace LSC.Api.Controllers
{

    public abstract class ApiBaseController<TService, TTransport, TTypeKey> : ApiController
        where TService : IServiceDomain
        where TTransport : ILSCTransport
        where TTypeKey : struct
    {


        #region Constantes

        const string DUPLICATE_ENTRY = "Não é possível gravar registros duplicados (Primary/Unique Key)";
        const string FOREIGN_KEY_ERROR = "Registro relacionado não encontrado (Foreign Key)";
        const string DELETE_FOREIGN_KEY_ERROR = "Registro não pode ser excluído por estar em uso (FK)";
        const string SERVER_OFF = "O servidor de banco de dados não foi encontrado ou não estava acessível";

        #endregion

        #region Objetos/Variáveis locais

        protected DomainInterop _dmn;
        protected TService _serv;

        #endregion

        #region Construtores

        public ApiBaseController()
        {
            _dmn = new DomainInterop();
            _serv = _dmn.CreateInstance<TService>();
        }

        #endregion

        #region Propriedades

        public string VerboHttp
        {
            get { return Request.Method.Method; }
        }

        public string MetodoHttp
        {
            get
            {
                return HttpContext.Current.Request.Path;
            }
        }

        #endregion
        
        #region Métodos Abstratos

        protected abstract TTransport GetByKey(TTransport request);

        protected abstract TTransport GetByKey(TTypeKey key);

        protected abstract string ValidaCampos(TTransport entidade);

        protected abstract IEnumerable<TTransport> FiltroSwitch(string campo, string conteudo);

        protected abstract void SalvarEntidade(TTransport entidade, bool isUpdate);

        protected abstract bool ValidarId(TTransport request);

        protected abstract void RemoverEntidade(TTransport entidade);

        protected abstract string GetIdValue(TTransport request);

        #endregion

        #region Métodos Locais
        
        protected HttpResponseException TratamentoFinalException(Exception ex)
        {

            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            string msgException = ex.Message;

            if (ex is SqlException)
            {

                int sqlErrNumber = ((SqlException)ex).Number;

                if (sqlErrNumber == 2)
                {
                    msgException = SERVER_OFF;
                }
                else
                {
                    if (GetHttpVerbFromString(HttpContext.Current.Request.HttpMethod) == HttpVerbs.Delete)
                    {
                        if (sqlErrNumber == 547)
                        {
                            msgException = DELETE_FOREIGN_KEY_ERROR;
                            httpStatusCode = HttpStatusCode.Conflict;
                        }
                    }
                    else
                    {
                        if (sqlErrNumber == 2601 || sqlErrNumber == 2627 || sqlErrNumber == 547)
                        {
                            msgException = (sqlErrNumber == 547 ? FOREIGN_KEY_ERROR : DUPLICATE_ENTRY);
                            httpStatusCode = HttpStatusCode.Conflict;
                        }
                    }
                }
            }

            return CriaHttpResponseException(httpStatusCode, msgException);
        }

        protected HttpVerbs GetHttpVerbFromString(string strHttpVerb)
        {

            HttpVerbs verbo;

            if (!Enum.TryParse<HttpVerbs>(HttpContext.Current.Request.HttpMethod, out verbo))
            {

                switch (HttpContext.Current.Request.HttpMethod.ToUpper())
                {
                    case "GET": verbo = HttpVerbs.Get; break;
                    case "POST": verbo = HttpVerbs.Post; break;
                    case "PUT": verbo = HttpVerbs.Put; break;
                    case "DELETE": verbo = HttpVerbs.Delete; break;
                    case "HEAD": verbo = HttpVerbs.Head; break;
                    case "PATCH": verbo = HttpVerbs.Patch; break;
                    case "OPTIONS": verbo = HttpVerbs.Head; break;
                }

                return verbo;

            }
            throw new InvalidOperationException();

        }

        protected HttpResponseException CriaHttpResponseException(HttpStatusCode code, string message)
        {
            return CriaHttpResponseException(code, ((int)code).ToString(), message);
        }

        protected HttpResponseException CriaHttpResponseException(HttpStatusCode code, string message, string detail)
        {

            HttpResponseMessage resp =
                Request.CreateResponse<DefaultException>
                (
                    code,
                    new DefaultException(message, detail)
                );

            return new HttpResponseException(resp);

        }

        protected HttpResponseMessage CriaHttpSimpleResponse(HttpStatusCode code, string detail)
        {
            return
                Request.CreateResponse<SimpleMessage>
                (
                    code,
                    new SimpleMessage(((int)code).ToString(), detail)
                );
        }
        
        protected void VerificaRequestNula<TTransport>(TTransport request)
        {
            // Validando instância da request
            if (request == null)
            {
                throw CriaHttpResponseException(HttpStatusCode.BadRequest, "Nenhuma informação de requisisão foi enviada");
            }
        }
        
        protected IEnumerable<TTransport> BuscarFiltro(string campo, string conteudo)
        {

            return ExecutaOperacao(() =>
            {

                IEnumerable<TTransport> ret = null;

                if (string.IsNullOrWhiteSpace(campo) || string.IsNullOrWhiteSpace(conteudo))
                    throw CriaHttpResponseException(HttpStatusCode.NotFound, "Parametros de filtrangem inválida");

                ret = FiltroSwitch(campo, conteudo);

                if (ret.Count() == 0)
                    throw CriaHttpResponseException(HttpStatusCode.NotFound, "Nenhum registro encontrado");

                return ret;

            });

        }

        protected TRetorno ExecutaOperacao<TRetorno>(Func<TRetorno> operacao)
        {
            try
            {

                return operacao();

            }
            catch (HttpResponseException) { throw; }
            catch (Exception ex) { throw TratamentoFinalException(Exceptions.GetRootException(ex, true)); }
        }

        #endregion

        #region Métodos Api

        protected TTransport Localizar(TTypeKey key)
        {

            return ExecutaOperacao<TTransport>(() =>
            {

                TTransport resp = Activator.CreateInstance<TTransport>();

                resp = GetByKey(key);
                if (resp == null)
                {
                    throw CriaHttpResponseException(HttpStatusCode.NotFound, string.Format("Nenhum registro com ID {0} foi localizado", key));
                }

                return resp;

            });

        }
        
        protected HttpResponseMessage Salvar([FromBody]TTransport request)
        {

            return ExecutaOperacao<HttpResponseMessage>(() =>
            {

                VerificaRequestNula(request);

                // Validando Dados
                string validacao = ValidaCampos(request);
                if (!string.IsNullOrWhiteSpace(validacao))
                {
                    throw CriaHttpResponseException(HttpStatusCode.NotFound, string.Format("Falha na validação: {0}", validacao));
                }

                using (var t = _dmn.BeginTransaction())
                {

                    HttpResponseMessage ret = null;

                    if (GetHttpVerbFromString(HttpContext.Current.Request.HttpMethod) == HttpVerbs.Post) // POST => Inclusão
                    {

                        // Incluindo Registro
                        SalvarEntidade(request, false);
                        _dmn.SaveChanges();

                        // Devolvendo mensagem de Created para cliente
                        ret = Request.CreateResponse<TTransport>(HttpStatusCode.Created, request);

                    }
                    else // PUT => Alteração
                    {

                        // Validar Id
                        if (!ValidarId(request))
                        {
                            throw CriaHttpResponseException(HttpStatusCode.BadRequest, "Falha na validação: Id não informado ou inválido");
                        }

                        TTransport registro = Activator.CreateInstance<TTransport>();

                        // Buscar registro na base
                        registro = GetByKey(request);
                        if (registro == null)
                        {
                            throw CriaHttpResponseException(HttpStatusCode.NotFound, string.Format("Registro de Id {0} não encontrado", GetIdValue(request)));
                        }

                        // Alterando registro 
                        SalvarEntidade(request, true);
                        _dmn.SaveChanges();

                        ret = CriaHttpSimpleResponse(HttpStatusCode.OK, "Registro alterado com sucesso");

                    }

                    t.Commit(); // Finalizando transação

                    return ret;

                }

            });

        }

        /// <summary>
        /// Exclui um registro da base de dados
        /// </summary>
        /// <param name="id">Id do registro a ser excluído</param>
        /// <returns>Um HttpResponseMessage com o resultado da operação</returns>
        protected HttpResponseMessage Remover(string id)
        {

            return ExecutaOperacao<HttpResponseMessage>(() =>
            {

                TTypeKey idReg = Activator.CreateInstance<TTypeKey>();
                TTransport reg = Activator.CreateInstance<TTransport>();

                // Validando id
                if (!Misc.TryCast<TTypeKey>(id, out idReg))
                {

                    throw CriaHttpResponseException(HttpStatusCode.NotFound, "Id informado inválido (não inteiro)");

                }

                // Buscando registros na base
                reg = GetByKey(idReg);
                if (reg == null)
                    throw CriaHttpResponseException(HttpStatusCode.NotFound, string.Format("Nenhum registro com ID {0} foi localizado", id));

                // Excluindo o registro
                using (var t = _dmn.BeginTransaction())
                {
                    RemoverEntidade(reg);
                    t.Commit();
                }

                return CriaHttpSimpleResponse(HttpStatusCode.OK, $"Registro Id {id} excluído com sucesso");

            });

        }

        #endregion

        #region Override

        protected override void Dispose(bool disposing)
        {
            _dmn.Dispose();
            base.Dispose(disposing);
        }

        #endregion

    }
}
