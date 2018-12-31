using System;

namespace Framework.MessageBroker
{
    public interface ISubscriber
    {
        /// <summary>
        /// Inicia um consumer de uma determinada mensagem.
        /// As configura��es de Exchanges e Queues ser�o feitas automaticamete.
        /// </summary>
        /// <typeparam name="T">Tipo da mensagem. Deve herdar de <see cref="BaseMessage"/></typeparam>
        /// <param name="factory">Delegate do m�todo de consumo.</param>
        /// <param name="msgBinder">Delegate do bind da mesagem. Bind padr�o � da seguinte forma: converte byte[] em json.</param>
        /// <returns></returns>
        IExchangeOptions StartConsume<T>(Func<T, bool> factory, Func<byte[], T> msgBinder = null) where T : BaseMessage;
    }
}