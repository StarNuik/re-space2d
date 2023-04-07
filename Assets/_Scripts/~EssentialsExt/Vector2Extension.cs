using UnityEngine;

namespace PolygonArcana.Essentials
{
	public static class Vector2Extension
	{
		public static Vector3 ToXY0(this Vector2 @this)
		{
			return (Vector3)@this;
		}

		// public static Vector2 SlerpUnclamped(Vector2 from, Vector2 to, float t)
		// {
		// 	return (Vector2)Vector3.SlerpUnclamped(from, to, t);
		// }

		// public static Vector2 Slerp(Vector2 from, Vector2 to, float t)
		// {
		// 	return (Vector2)Vector3.Slerp(from, to, t);
		// }
		
		public static Vector2 Slerp(Vector2 from, Vector2 to, float t)
			=> SlerpUnclamped(from, to, Mathf.Clamp01(t));

		//? https://keithmaggio.wordpress.com/2011/02/15/math-magician-lerp-slerp-and-nlerp/
		public static Vector2 SlerpUnclamped(Vector2 from, Vector2 to, float t)
		{
			var dot = Vector2.Dot(from, to);
			dot = Mathf.Clamp01(dot);
			var theta = Mathf.Acos(dot) * t;
			var relative = to - from * dot;
			var result = from * Mathf.Cos(theta) + relative * Mathf.Sin(theta);
			return result;
		}

	}
}
