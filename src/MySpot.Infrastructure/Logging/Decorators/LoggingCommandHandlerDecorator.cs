﻿using Humanizer;

using Microsoft.Extensions.Logging;

using MySpot.Application.Abstractions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Infrastructure.Logging.Decorators
{
    internal sealed class LoggingCommandHandlerDecorator<TCommand> : 
        ICommandHandler<TCommand> where TCommand : class, ICommand
    {
        private readonly ICommandHandler<TCommand> _commandHandler;
        private readonly ILogger<LoggingCommandHandlerDecorator<TCommand>> _logger;

        public LoggingCommandHandlerDecorator(ICommandHandler<TCommand> commandHandler, 
            ILogger<LoggingCommandHandlerDecorator<TCommand>> logger)
        {
            _commandHandler = commandHandler;
            _logger = logger;
        }

        public async Task HandleAsync(TCommand command)
        {
            var commandName = typeof(TCommand).Name.Underscore();
            _logger.LogInformation($"Started handling a command: '{commandName}'...");
            await _commandHandler.HandleAsync(command);
            _logger.LogInformation($"Completed handling a command: '{commandName}'...");
        }
    }
}
