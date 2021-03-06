﻿using _Command = Command.Infrastructure.Models.Command;

namespace Git.Stats.Infrastructure.Services.Interfaces
{
    public interface IBetweenService : IService
    {
        string GetStatisticBetween(_Command command);
    }
}
