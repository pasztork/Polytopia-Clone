using System.Text.Json;

namespace ViewUtil
{
	public abstract class JsonCommandProcessorBase
	{
		private JsonLog.JsonActionObject _command = new();

		protected JsonLog.JsonActionObject Command => _command;

		protected readonly IDictionary<string, Action> actions = new Dictionary<string, Action>();

		public bool Process(string commandString)
		{
			_command = JsonSerializer.Deserialize<JsonLog.JsonActionObject>(commandString) ??
				throw new ArgumentNullException(nameof(commandString));

			if (!VerifyCommandExists()) { return false; }
			actions[_command.Action]();
			return true;
		}

		private bool VerifyCommandExists()
		{
			if (actions.ContainsKey(_command.Action)) { return true; }
			Console.WriteLine($"Invalid command: {_command.Action}");
			return false;
		}
	}
}