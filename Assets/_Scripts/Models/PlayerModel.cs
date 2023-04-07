using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;
using UnityEngine;

namespace PolygonArcana.Models
{
	public enum Playerstate
	{
		NotSpawned,
		Base,
		Dash,
		Destroyed,
	}

	public struct InputData
	{
		public Vector2Int Movement { get; }
		public Vector2Int Attack { get; }

		public InputData(Vector2Int movement, Vector2Int attack)
		{
			Movement = movement;
			Attack = attack;
		}
	}

	public class PlayerModel : AModel
	{
		public IChange<Location2D> Location { get; } = new();
		public IChange<InputData> Input { get; } = new();
		public IChange<Playerstate> State { get; } = new();
	}
}
