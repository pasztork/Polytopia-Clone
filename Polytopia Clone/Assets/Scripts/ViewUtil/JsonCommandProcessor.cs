using System;
using System.Collections.Generic;
using System.Text.Json;

namespace ViewUtil
{
    public class JsonCommandProcessor
    {
        private LogView.JsonActionObject _command = null;

        public IDictionary<string, Func<bool>> Actions { get; } = new Dictionary<string, Func<bool>>();

        public bool Process(string commandString)
        {
            _command = JsonSerializer.Deserialize<LogView.JsonActionObject>(commandString);
            return VerifyCommandExists() && Actions[_command.Action]();
        }

        private bool VerifyCommandExists() => Actions.ContainsKey(_command.Action);
    }
}