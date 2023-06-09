using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;
using System.Collections.Generic;
using PolygonArcana.Entities;

namespace PolygonArcana.Models
{
	public enum Gamestate
	{
		Splash,
		Play,
		End
	}

	public class MainModel : AModel
	{
		public IChange<Gamestate> Gamestate { get; } = new();
		public IChange<List<BulletEntity>> Bullets { get; } = new(new());
		public IChange<List<EnemyEntity>> Enemies { get; } = new(new());
	}
}
