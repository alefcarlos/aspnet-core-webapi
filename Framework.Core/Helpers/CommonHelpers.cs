using System;

namespace Framework.Core.Helpers
{
    public static class CommonHelpers
    {
        public static T GetValueFromEnv<T>(string keyName, bool throwException = true)
        {
            var value = GetEnvironmentVariable(keyName, throwException);

            if (string.IsNullOrWhiteSpace(value))
                return default(T);

            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static string GetEnvironmentVariable(string envName, bool throwException = true)
        {
            if (string.IsNullOrWhiteSpace(envName))
            {
                throw new ArgumentNullException(nameof(envName), $"o parâmetro {nameof(envName)} deve ser informado (não pode ser vazio ou nulo).");
            }

            var value = Environment.GetEnvironmentVariable(envName);

            if (string.IsNullOrWhiteSpace(value) && throwException)
            {
                throw new ArgumentNullException(nameof(envName), $"Não foi encontrado valor para env {envName}.");
            }

            return value;
        }
    }
}
