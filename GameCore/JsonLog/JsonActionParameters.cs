namespace JsonLog
{
	public class JsonActionParameters
	{
		public int[] Start { get; set; } = Array.Empty<int>();
		public int[] End { get; set; } = Array.Empty<int>();
		public int[] Neighbors { get; set; } = Array.Empty<int>();
		public string Tech { get; set; } = string.Empty;
		public string Building { get; set; } = string.Empty;
		public string Troop { get; set; } = string.Empty;
	}
}
