using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LSC.Cross.Tools
{

    /// <summary>
    /// Fornece métodos utilitários para tratamento de exceções
    /// </summary>
    public static class Exceptions
    {

        #region Métodos públicos

 
        public static Exception GetRootException(Exception ex)
        {
            return GetRootException(ex, false);
        }

        public static Exception GetRootException(Exception ex, bool stopWhenSqlException)
        {
            if (ex.InnerException == null || (stopWhenSqlException && ex is SqlException))
                return ex;
            else
                return GetRootException(ex.InnerException, stopWhenSqlException);
        }

        public static Exception GetSqlException(Exception ex)
        {
            Exception rootEx = GetRootException(ex, true);
            return ((rootEx is SqlException) ? rootEx : ex);
        }

        public static IDictionary<string, string> DetailsException(Exception ex)
        {
            return DetailsException(ex, false);
        }

        public static IDictionary<string, string> DetailsException(Exception ex, bool useInnerException)
        {

            Exception tmpEx = ((useInnerException) ? GetRootException(ex, true) : ex);

            IDictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("Message", tmpEx.Message);
            dic.Add("Source", tmpEx.Source);
            dic.Add("StackTrace", tmpEx.StackTrace);
            if (tmpEx is SqlException)
            {
                dic.Add("Number", ((SqlException)tmpEx).Number.ToString());
                dic.Add("Server", ((SqlException)tmpEx).Server);
                dic.Add("Procedure", ((SqlException)tmpEx).Procedure);
                dic.Add("LineNumber", ((SqlException)tmpEx).LineNumber.ToString());
            }

            tmpEx = null;

            return dic;

        }

        #endregion

    }
}
