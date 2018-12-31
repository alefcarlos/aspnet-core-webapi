using System.Threading.Tasks;

namespace Framework.MessageBroker
{
    public interface IPublisher
    {
        /// <summary>
        /// Publica um mensagem de um determinado tipo.
        /// As configura��es de Exchanges e Queues ser�o feitas automaticamete.
        /// </summary>
        /// <typeparam name="T">Tipo da mensagem. Deve herdar de <see cref="BaseMessage"/></typeparam>
        /// <param name="model">Inst�nca do tipo.</param>
        void Publish<T>(T model) where T : BaseMessage;

        /// <summary>
        /// Publica um mensagem de um determinado tipo.
        /// As configura��es de Exchanges e Queues ser�o feitas automaticamete.
        /// </summary>
        /// <typeparam name="T">Tipo da mensagem. Deve herdar de <see cref="BaseMessage"/></typeparam>
        /// <param name="model">Inst�nca do tipo.</param>
        Task PublishAsync<T>(T model) where T : BaseMessage;

        /// <summary>
        /// Publica um mensagem de um determinado tipo, possibilitando especificar o nome da fila.
        /// As configura��es de Exchanges e Queues ser�o feitas automaticamete.
        /// </summary>
        /// <typeparam name="T">Tipo da mensagem. Deve herdar de <see cref="BaseMessage"/></typeparam>
        /// <param name="model">Inst�nca do tipo.</param>
        /// <param name="queueName">Nome da fila.</param>
        void Publish<T>(T model, string queueName) where T : BaseMessage;

        /// <summary>
        /// Publica um mensagem de um determinado tipo, possibilitando especificar o nome da fila.
        /// As configura��es de Exchanges e Queues ser�o feitas automaticamete.
        /// </summary>
        /// <typeparam name="T">Tipo da mensagem. Deve herdar de <see cref="BaseMessage"/></typeparam>
        /// <param name="model">Inst�nca do tipo.</param>
        /// <param name="queueName">Nome da fila.</param>
        Task PublishAsync<T>(T model, string queueName) where T : BaseMessage;
    }
}