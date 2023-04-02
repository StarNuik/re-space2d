using UnityEngine;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Essentials
{
	public static class Ray2DExtension
	{
		public static void Deconstruct(
			this Ray2D @this,
			out Vector2 origin,
			out Vector2 direction
		)
		{
			origin = @this.origin;
			direction = @this.direction;
		}
	}
}
