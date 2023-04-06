using System;
using UnityEngine;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Essentials
{
	using Vector2ErpFunc = Func<Vector2, Vector2, float, Vector2>;
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

		public static Ray2D Interpolate(
			Ray2D from,
			Ray2D to,
			float t,
			Vector2ErpFunc positionFunc,
			Vector2ErpFunc directionFunc)
		{
			var (fPos, fDir) = from;
			var (tPos, tDir) = to;

			return new(
				positionFunc(fPos, tPos, t),
				directionFunc(fDir, tDir, t)
			);
		}

		// public static Ray2D LerpUnclamped(Ray2D from, Ray2D to, float t)
		// 	=> Interpolate(
		// 		from,
		// 		to,
		// 		t,
		// 		Vector2.LerpUnclamped,
		// 		Vector2.LerpUnclamped
		// 	);
		
		// public static Ray2D SlerpUnclamped(Ray2D from, Ray2D to, float t)
		// 	=> Interpolate(
		// 		from,
		// 		to,
		// 		t,
		// 		Vector2Extension.SlerpUnclamped,
		// 		Vector2Extension.SlerpUnclamped
		// 	);
	}
}
