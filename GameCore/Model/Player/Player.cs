namespace Model
{
	public class Player
	{
		public static JsonProductionRate BaseProduction { get; set; } = null;

		public event Action<Player, TileBase, BuildingBase> OnStartingCitySpawned;
		public event Action<Player> OnEliminated;
		public event Action<BuildingBase> OnBuildCreated;
		public event Action<TroopBase> OnTroopTrained;
		public event Action<TileBase, TileBase> OnTroopMoved;
		public event Action<TroopBase, TileBase, List<TileBase>> OnTroopAttacked;
		public event Action<TroopBase, TileBase, List<TileBase>> OnBuildingAttacked;
		public event Action OnTurnEnded;
		public event Action<TechTreeItemBase> OnTechLearned;
		public event Action<TroopBase, TroopBase> OnAttackMissed;

		public ResourceContainer ResourceContainer { get; private set; } = new ResourceContainer();

		public Dictionary<string, TechTreeItemBase> Techs { get; set; } = new();
		public BonusProperty BonusProperty { get; set; } = new BonusProperty();

		public int StartingCityRange { get; set; }
		public IList<BuildingBase> Buildings { get; } = new List<BuildingBase>();
		public IList<TroopBase> Troops { get; } = new List<TroopBase>();
		public ISet<TileBase> AvailableTiles { get; } = new HashSet<TileBase>();

		public string Name { get; set; } = string.Empty;

		public IList<string> AvailableBuildings { get; set; } = new List<string>();
		public IList<string> AvailableTroops { get; set; } = new List<string>();

		public Player(string name)
		{
			Name = name;
			GameManager.Get<TurnManagerBase>().PlayerCreated(this);
			GameManager.Players.Add(this);
			GameManager.Get<TechTreeManagerBase>().ConnectTree(Techs);
		}

		public void StartTurn()
		{
			ResourceContainer.StartTurn();
		}

		public void EndTurn()
		{
			OnTurnEnded?.Invoke();
		}

		public bool Build(TroopBase troop, BuildingBase building)
		{
			RequirementsListBase requirements = building.Requirements;
			troop.FillRequirements(requirements);
			bool requirementsMet = requirements.RequirementsMet(ResourceContainer, BonusProperty.BuildingDiscount, AvailableTiles, troop.Tile);
			if (!requirementsMet || !Troops.Contains(troop))
			{
				building.StopProduction();
				return false;
			}

			bool built = troop.Tile.SetBuildingOnTop(building, this);
			if (!built)
			{
				building.StopProduction();
				return false;
			}

			building.Tile = troop.Tile;
			ResourceContainer -= (building.Cost * (1f - BonusProperty.BuildingDiscount));
			AddBuilding(building);
			building.ApplyAllPropertyBonus(this);
			troop.Kill();
			OnBuildCreated?.Invoke(building);
			return true;
		}

		public bool Train(BuildingBase building, TroopBase troop)
		{
			if (!Buildings.Contains(building) ||
				!ResourceContainer.HasEnoughFor(troop.Cost))
				return false;

			bool trained = building.TrainTroop(troop);
			if (!trained)
				return false;

			ResourceContainer -= troop.Cost;
			Troops.Add(troop);
			if (BonusProperty.HealAmount > 0)
				GameManager.Get<TurnManagerBase>().OnTurnStarted += troop.Heal;

			troop.ApplyAllPropertyBonus(this);
			OnTroopTrained?.Invoke(troop);
			return true;
		}

		public bool MoveTroop(TroopBase troop, TileBase target)
		{
			if (!Troops.Contains(troop))
				return false;

			TileBase from = troop.Tile;
			bool moved = troop.Move(target);
			if (moved)
				OnTroopMoved?.Invoke(from, target);

			return moved;
		}

		public bool Attack(TroopBase attacker, TroopBase target)
		{
			if (Troops.Contains(attacker) && Troops.Contains(target) || !Troops.Contains(attacker))
				return false;

			List<TileBase> result = attacker.Attack(target);
			if (result != null)
			{
				var targetTile = result[0];
				result.RemoveAt(0);
				OnTroopAttacked?.Invoke(attacker, targetTile, result);
			}

			return result != null;
		}

		public bool Attack(TroopBase attacker, BuildingBase target)
		{
			if (Troops.Contains(attacker) && Buildings.Contains(target) || !Troops.Contains(attacker))
				return false;
			List<TileBase> result = attacker.Attack(target);
			if (result != null)
			{
				var targetTile = result[0];
				result.RemoveAt(0);
				OnBuildingAttacked?.Invoke(attacker, targetTile, result);
			}

			return result != null;
		}

		public void SetupStartingPosition()
		{
			BuildingBase city = new City(this);
			city.Producers.Add(new FoodProducer(ResourceContainer, BaseProduction.Food));
			city.Producers.Add(new MaterialProducer(ResourceContainer, BaseProduction.Material));
			city.Producers.Add(new MoneyProducer(ResourceContainer, BaseProduction.Money));
			TileBase tile = GameManager.Get<MapManagerBase>().GetStartingTile();
			city.Tile = tile;
			city.Player = this;
			tile.SetBuildingOnTop(city, this);
			AvailableTiles.Add(tile);
			AddBuilding(city);
			OnBuildCreated?.Invoke(city);
			OnStartingCitySpawned?.Invoke(this, tile, city);
		}

		public void AddBuilding(BuildingBase building)
		{
			Buildings.Add(building);
			AvailableTiles.UnionWith(building.GetTilesInRange());
		}

		public void RemoveBuilding(BuildingBase building)
		{
			Buildings.Remove(building);
			GetAllAvailableTiles();
			building.DestroyEveryThingInRange(AvailableTiles);

			if (Buildings.Count == 0)
				OnEliminated.Invoke(this);
		}

		private void GetAllAvailableTiles()
		{
			AvailableTiles.Clear();
			foreach (var building in Buildings)
			{
				AvailableTiles.UnionWith(building.GetTilesInRange());
			}
		}

		public bool UnlockTech(TechTreeItemBase techToLearn)
		{
			if (!ResourceContainer.HasEnoughFor(techToLearn.TechTreeItemProperty.Cost))
				return false;

			if (!Techs[techToLearn.HashCode].TechTreeItemProperty.IsUnlocked)
			{
				Techs[techToLearn.HashCode].TechTreeItemProperty.IsUnlocked = true;
				Techs[techToLearn.HashCode].ActivateEffect(this);
				ResourceContainer -= techToLearn.TechTreeItemProperty.Cost;
				OnTechLearned?.Invoke(techToLearn);
				return true;
			}
			return false;
		}

		public void RaiseOnAttackMissed(TroopBase attacker, TroopBase target) =>
			OnAttackMissed?.Invoke(attacker, target);
	}
}
