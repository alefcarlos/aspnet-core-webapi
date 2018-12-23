using System;
using System.Threading.Tasks;

namespace Framework.MessageBroker
{
    public interface ISubscriber
    {
        void StartConsume<T>(Func<T, bool> factory, Func<BaseMessage, T> msgBinder = null, TaskCreationOptions runningOpt = TaskCreationOptions.None) where T : BaseMessage;
    }
}