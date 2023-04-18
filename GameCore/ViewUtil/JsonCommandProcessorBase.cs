using System.Text.Json;

namespace ViewUtil
{
    public abstract class JsonCommandProcessorBase
    {
        private JsonLog.JsonActionObject _command = null;

        protected JsonLog.JsonActionObject Command => _command;

        protected readonly IDictionary<string, Action> actions = new Dictionary<string, Action>();

        public void Process(string commandString)
        {
            _command = JsonSerializer.Deserialize<JsonLog.JsonActionObject>(commandString);
            VerifyCommandExists();
            actions[_command.Action]();
        }

        private void VerifyCommandExists()
        {
            if (!actions.ContainsKey(_command.Action))
            {
                throw new ArgumentOutOfRangeException("No such command exists!");
            }
        }
    }
}