using UnityEngine;

namespace PolygonArcana.Essentials
{
	public static class Vector2Extension
	{
		public static Vector3 ToXY0(this Vector2 @this)
		{
			return (Vector3)@this;
		}
	}
}
