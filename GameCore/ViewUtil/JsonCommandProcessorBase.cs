using System.Text.Json;

namespace ViewUtil
{
    public abstract class JsonCommandProcessorBase
	{
		private JsonLog.JsonActionObject _command = new();
		protected JsonLog.JsonActionObject Command => _command;

		protected readonly IDictionary<string, Func<string>> actions = new Dictionary<string, Func<string>>();

        protected static readonly string OK = "OK";

        protected static readonly string ERROR = "ERROR";

        private class Response
        {
            public string Status { get; set; } = string.Empty;
        }

        public abstract string GetAvailableActions(JsonLog.JsonActionObject jsonCommand);

		public (bool, string) Process(JsonLog.JsonActionObject jsonCommand)
		{
			_command = jsonCommand;
			if (!VerifyCommandExists()) { return ResponseOk(); }
			string result = actions[_command.Action]();
			return result == OK ? ResponseOk() : ResponseError();
        }

		private (bool, string) ResponseOk()
		{
			return (true, JsonSerializer.Serialize(new Response { Status = OK }));
		}

		private (bool, string) ResponseError()
		{
			return (false, JsonSerializer.Serialize(new Response { Status = ERROR }));
		}

		private bool VerifyCommandExists()
		{
			if (actions.ContainsKey(_command.Action)) { return true; }
			Console.WriteLine($"Invalid command: {_command.Action}");
			return false;
		}
	}
}