using System;
using System.Collections.Generic;
using System.Text.Json;

namespace ViewUtil
{
    public abstract class JsonCommandProcessorBase
    {
        private LogView.JsonActionObject _command = null;

        protected readonly IDictionary<string, Func<bool>> actions = new Dictionary<string, Func<bool>>();

        public bool Process(string commandString)
        {
            _command = JsonSerializer.Deserialize<LogView.JsonActionObject>(commandString);
            return VerifyCommandExists() && actions[_command.Action]();
        }

        private bool VerifyCommandExists() => actions.ContainsKey(_command.Action);
    }
}