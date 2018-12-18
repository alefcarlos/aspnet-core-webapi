using System;

namespace Framework.Core.Helpers
{
    public static class CommonHelpers
    {
        public static T GetValueFromEnv<T>(string keyName, bool throwException = true)
        {
            if (string.IsNullOrWhiteSpace(keyName))
            {
                throw new ArgumentNullException(nameof(keyName), "Informe um nome de chave.");
            }

            var value = GetEnvironmentVariable(keyName, "keyname", throwException);

            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static string GetEnvironmentVariable(string envName, string paramName, bool throwException = true)
        {
            if (string.IsNullOrWhiteSpace(envName))
            {
                throw new ArgumentOutOfRangeException(paramName, string.Format("o parâmetro '{0}' deve ser informado (não pode ser vazio ou nulo).", paramName));
            }

            var value = Environment.GetEnvironmentVariable(envName);

            if (string.IsNullOrWhiteSpace(value) && throwException)
            {
                throw new ArgumentOutOfRangeException(paramName, string.Format("não existe ou está vazio o valor para a variável de ambiente '{0}'.", envName));
            }

            return value;
        }
    }
}
