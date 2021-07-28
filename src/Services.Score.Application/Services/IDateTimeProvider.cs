using System;

namespace Services.Score.Application.Services
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}