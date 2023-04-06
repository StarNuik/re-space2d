using UnityEngine;

namespace PolygonArcana.Essentials
{
	public static class Vector2Extension
	{
		public static Vector3 ToXY0(this Vector2 @this)
		{
			return (Vector3)@this;
		}

		public static Vector2 SlerpUnclamped(Vector2 from, Vector2 to, float t)
		{
			return (Vector2)Vector3.SlerpUnclamped(from, to, t);
		}

		public static Vector2 Slerp(Vector2 from, Vector2 to, float t)
		{
			return (Vector2)Vector3.Slerp(from, to, t);
		}
	}
}
