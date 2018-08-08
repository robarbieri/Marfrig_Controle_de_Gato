using System;
using System.Configuration;

namespace LSC.Cross.Tools
{
    public static class ReadConfig
    {

        #region Configuracoes

        public static int GetIntParameter(string name)
        {
            int retValue;
            string paramValue = ConfigurationManager.AppSettings[name];

            if (int.TryParse(paramValue, out retValue))
                return retValue;

            throw new InvalidCastException($"O valor '{paramValue}' do parâmetro '{name}' não é inteiro");

        }

        public static double GetDoubleParameter(string name)
        {
            double retValue;
            string paramValue = ConfigurationManager.AppSettings[name];

            if (double.TryParse(paramValue, out retValue))
                return retValue;

            throw new InvalidCastException($"O valor '{paramValue}' do parâmetro '{name}' não é numérico");

        }

        public static DateTime GetDateTimeParameter(string name)
        {
            DateTime retValue;
            string paramValue = ConfigurationManager.AppSettings[name];

            if (DateTime.TryParse(paramValue, out retValue))
                return retValue;

            throw new InvalidCastException($"O valor '{paramValue}' do parâmetro '{name}' não é data/hora");

        }

        public static bool GetBoolParameter(string name)
        {
            bool retValue;
            string paramValue = ConfigurationManager.AppSettings[name];

            if (bool.TryParse(paramValue, out retValue))
                return retValue;

            throw new InvalidCastException($"O valor '{paramValue}' do parâmetro '{name}' não é booleano");

        }

        public static string GetStringParameter(string name)
        {
            string value = ConfigurationManager.AppSettings[name];

            if (value == null)
                throw new ArgumentException($"Falha na leitura do parâmetro '{name}'");

            return value;
        }

        #endregion

    }

}
