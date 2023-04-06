using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;
using UnityEngine;

namespace PolygonArcana.Entities
{
	public interface IVisibilityInfo
	{
		Vector2 Position { get; }
		float BoundsRadius { get; }

		void Deconstruct(out Vector2 position, out float radius);
	}
}