using System;
using System.Collections.Generic;
using Convey.Logging.CQRS;
using Services.Score.Application.Events.External;
using Services.Score.Application.Exceptions;

namespace Services.Score.Infrastructure.Logging
{
    internal sealed class MessageToLogTemplateMapper : IMessageToLogTemplateMapper
    {
        private static IReadOnlyDictionary<Type, HandlerLogTemplate> MessageTemplates 
            => new Dictionary<Type, HandlerLogTemplate>
            {
                {
                    typeof(UserCreated),     
                    new HandlerLogTemplate
                    {
                        After = "Created user score with for user: {UserId}.",
                        OnError = new Dictionary<Type, string>
                        {
                            {
                                typeof(UserScoreAlreadyExistsException), "User score with id: {UserId} already exists."
                            }
                        }
                    }
                },
                {
                    typeof(RunCompleted),     
                    new HandlerLogTemplate
                    {
                        After = "Added score to user: {UserId} for completing run: {RunId}.",
                        OnError = new Dictionary<Type, string>
                        {
                            {
                                typeof(UserScoreNotFoundException), "User score with id: {UserId} was not found."
                            }
                        }
                    }
                },
                {
                    typeof(RouteStatusChanged),
                    new HandlerLogTemplate
                    {
                        After = "Added score to user: {UserId} if run: {RunId} changed status to: Valid. Route status: {CurrentStatus}.",
                        OnError = new Dictionary<Type, string>
                        {
                            {
                                typeof(RouteNotFoundException), "Route with id: {RouteId} was not found."
                            },
                            {
                                typeof(UserScoreNotFoundException), "User score with id: {UserId} was not found."
                            }
                        }

                    }
                }
            };
        
        public HandlerLogTemplate Map<TMessage>(TMessage message) where TMessage : class
        {
            var key = message.GetType();
            return MessageTemplates.TryGetValue(key, out var template) ? template : null;
        }
    }
}