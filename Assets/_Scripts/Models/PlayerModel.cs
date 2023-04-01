using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;
using UnityEngine;

namespace PolygonArcana.Models
{
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
		public IChange<InputData> Input { get; } = new();
	}
}
