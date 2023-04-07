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

		public IEnumerable<Location2D> Points => points.Select(p => p.ToLocation2D());

		public Location2D Evaluate(float t)
		{
			Assert.IsTrue(points.Length > 2);

			var (from, to) = Points.PairAroundF(t);
			var f = Mathf.Clamp01(t) * (points.Length - 1) % 1f;

			if (t == 0f || t == 1f)
				f = t;

			var location = Location2D.LerpUnclamped(from, to, f);
			
			return location;
		}
		[Serializable]
		private class MovementPoint
		{
			public Vector2 Position;

			public Vector2 Direction;

			public Ray2D ToRay()
			{
				return new(Position, Direction);
			}

			public Location2D ToLocation2D()
			{
				return new(Position, Direction);
			}
		}
	}
}
