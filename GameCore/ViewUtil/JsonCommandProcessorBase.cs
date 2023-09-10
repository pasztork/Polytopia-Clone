namespace ViewUtil
{
	public abstract class JsonCommandProcessorBase
	{
		private JsonLog.JsonActionObject _command = new();
		protected JsonLog.JsonActionObject Command => _command;

		protected readonly IDictionary<string, Action> actions = new Dictionary<string, Action>();

		public abstract string GetAvailableActions(JsonLog.JsonActionObject jsonCommand);

		public bool Process(JsonLog.JsonActionObject jsonCommand)
		{
			_command = jsonCommand;
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