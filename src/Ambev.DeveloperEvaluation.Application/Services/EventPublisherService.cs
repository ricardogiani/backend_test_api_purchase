using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Events
{
    public class EventPublisherService : IEventPublisherService
    {
        private readonly ILogger<EventPublisherService> _logger;

        private readonly IMapper _mapper;
        

        public EventPublisherService(ILogger<EventPublisherService> logger,  IMapper mapper)
        {
            _logger = logger;            
            _mapper = mapper;
        }

        public async Task PublishAsync<TEvent>(TEvent @event, EventPublisherType eventType) where TEvent : class
        {
            _logger.LogInformation($"Publish EventType:{eventType}, eventBody: {@event}");

            //throw new NotImplementedException();
        }
    }
}