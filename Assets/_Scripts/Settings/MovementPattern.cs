using UnityEngine;
using PolygonArcana.Essentials;
using System;
using Unity.Mathematics;
using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace PolygonArcana.Settings
{
	[CreateAssetMenu(
		menuName = ("Settings/" + nameof(MovementPattern)),
		fileName = nameof(MovementPattern)
	)]
	public class MovementPattern : ASettings
	{
		[SF] MovementPoint[] points =
		{
			new() { Position = Vector2.right, Direction = Vector2.right},
			new() { Position = Vector2.left, Direction = Vector2.left},
		};

		public IEnumerable<Ray2D> Points => points.Select(p => p.ToRay());


		public (Ray2D from, Ray2D to) GetPair(float t)
		{
			Assert.IsTrue(points.Length >= 2);

			t = AutoScaledF(t);
			var iLower = Mathf.FloorToInt(t);
			var iHigher = Mathf.CeilToInt(t);

			//> idk how to make this pretty
			if (iLower == iHigher)
			{
				iHigher++;
				if (iHigher == points.Length)
				{
					iLower--;
					iHigher--;
				}
			}

			return (points[iLower].ToRay(), points[iHigher].ToRay());
		}

		public Ray2D Evaluate(float t)
		{
			Assert.IsTrue(points.Length > 2);

			var (from, to) = GetPair(t);
			var f = AutoScaledF(t) % 1f;
			var location = Ray2DExtension.Interpolate(
				from,
				to,
				f,
				Vector2.LerpUnclamped,
				Vector2Extension.SlerpUnclamped
			);
			return location;
		}

		private float AutoScaledF(float f) => ScaledF(f, points.Length - 1);
		private float ScaledF(float f, float scale) => Mathf.Clamp01(f) * scale;

		[Serializable]
		private class MovementPoint
		{
			public Vector2 Position;

			public Vector2 Direction;

			public Ray2D ToRay()
			{
				return new(Position, Direction);
			}
		}
	}
}
