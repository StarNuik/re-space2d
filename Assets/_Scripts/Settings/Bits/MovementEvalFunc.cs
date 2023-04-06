using System;
using UnityEngine;
using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;

namespace PolygonArcana.Settings
{
	using EvalFunc = MovementEvalFunc;

	public enum MovementEvalFunc
	{
		Keep,
		Clamp,
		Pingpong,
		Repeat,
	}

	public static class MovementEvalFuncExtension
	{
		public static float GetF(this EvalFunc @this, float time, float length)
		{
			float f = time / length;
			return @this switch
			{
				EvalFunc.Keep => f,
				EvalFunc.Clamp => Mathf.Clamp(f, 0f, 1f),
				EvalFunc.Pingpong => Mathf.PingPong(f, 1f),
				EvalFunc.Repeat => Mathf.Repeat(f, 1f),
				_ => f,
			};
		}

		// public static Ray2D Evaluate(this EvalFunc @this, Ray2D from, Ray2D to, float time, float length)
		// {
		// 	return Ray2DExtension.Interpolate(
		// 		from,
		// 		to,
		// 		@this.Evaluate(time, length),
		// 		Vector2.LerpUnclamped,
		// 		Vector2Extension.SlerpUnclamped
		// 	);
		// }
	}
}
