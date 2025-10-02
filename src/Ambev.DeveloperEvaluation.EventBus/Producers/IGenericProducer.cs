using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.EventBus.Producers
{
    public interface IGenericProducer<TValue>
    {
        Task ProduceAsync(TValue message);
    }
}