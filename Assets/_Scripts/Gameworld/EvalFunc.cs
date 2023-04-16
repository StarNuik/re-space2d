using System;
using UnityEngine;
using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;

namespace PolygonArcana.Settings
{
	public enum EvalFunc
	{
		Keep,
		Clamp,
		Pingpong,
		Repeat,
	}

	public static class EvalFuncExtension
	{
		//> calculate a value between 0f - length
		public static float EvalTime(this EvalFunc @this, float time, float length)
		{
			return @this switch
			{
				EvalFunc.Keep => time,
				EvalFunc.Clamp => Mathf.Clamp(time, 0f, length),
				EvalFunc.Pingpong => Mathf.PingPong(time, length),
				EvalFunc.Repeat => Mathf.Repeat(time, length),
				_ => time,
			};
		}

		//> calculate a value between 0f - 1f
		public static float EvalF(this EvalFunc @this, float time, float length)
		{
			return @this.EvalTime(time, length) / length;
		}
	}
}
