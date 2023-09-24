using System;
using System.Collections.Generic;
using System.IO;
using Util;

namespace Model
{
	public static class GameManager
	{
		public static event Action<IList<Player>, string> OnGameStarted;

		public static IList<Player> Players { get; } = new List<Player>();

		/// <summary>
		/// Should be set before game is started.
		/// By default the game is not deterministic.
		/// Must be set to true if used outside of Unity.
		/// </summary>
		public static bool IsGameplayDeterministic { get; set; } = false;

		private static readonly DependencyContainer dependencyContainer = new DependencyContainer();

		private static readonly string directory = $"{Directory.GetCurrentDirectory()}\\GameSettings";
		private static readonly string mapGenerationSettingsFilename = "MapGenerationSettings.json";
		private static readonly string propertiesSettingsFilename = "PropertiesSettings.json";
		public static string PropertiesSettingsFilepath { get => Path.Combine(directory, propertiesSettingsFilename); }

		public static Random Random { get; } = new Random(DateTime.Now.Millisecond);
		static GameManager()
		{
			dependencyContainer.Register<MapManagerBase, MapManager>();
			dependencyContainer.Register<MapGeneratorBase, MapGenerator>();
			dependencyContainer.Register<TurnManagerBase, TurnManager>();
			dependencyContainer.Register<BuildManagerBase, BuildManager>();
			dependencyContainer.Register<TechTreeManagerBase, TechTreeManager>();
            dependencyContainer.Register<TrainManagerBase, TrainManager>();
            MapSettingsLoader.Load(Path.Combine(directory, mapGenerationSettingsFilename));
			PropertiesLoader.Load(Path.Combine(directory, propertiesSettingsFilename));
		}

		public static void StartNew()
		{
			foreach (Player player in Players)
				player.SetupStartingPosition();

			OnGameStarted?.Invoke(Players, Get<MapManagerBase>().MapFilePath);
			dependencyContainer.Get<TurnManagerBase>().Start();
		}

		public static T Get<T>() => dependencyContainer.Get<T>();
	}
}