﻿using System;
using System.Linq;
using System.Reflection;

namespace Command.Infrastructure
{
    public class CommandHandler
    {
        private const  string NotRecognizedCommand = "command was not recognized.";
        private readonly ICommandController controller;

        public CommandHandler(ICommandController controller)
        {
            this.controller = controller;
        }

        public string Execute(string command)
        {
            var executedCommand = CreateCommand(command);

            if (executedCommand == null)
            {
                return NotRecognizedCommand;
            }

            return Call(executedCommand);
        }

        private string Call(Models.Command command)
        {
            var handler = TryGetHandler(command.ExecutedCommand);
            if (handler == null)
            {
                return NotRecognizedCommand;
            }

            return (string) handler.Invoke(controller, new object[] {command});
        }

        private MethodInfo TryGetHandler(string command)
        {
            return controller.GetType()
                .GetMethods()
                .FirstOrDefault(x => x.Name.ToUpper() == command.ToUpper());
        }

        private Models.Command CreateCommand(string command)
        {
            try
            {
                command = command.Trim();
                var spaceIndex = command.IndexOf(" ", StringComparison.InvariantCulture);
                var executedCommand = command.Substring(0, spaceIndex);
                var args = command.Substring(spaceIndex, command.Length - spaceIndex);
                return new Models.Command(executedCommand.Trim(), args.Trim());
            }
            catch (Exception)
            {
                return null;
            }
            
        }
    }
}
