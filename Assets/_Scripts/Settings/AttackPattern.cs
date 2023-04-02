using SF = UnityEngine.SerializeField;
using UnityEngine;
using PolygonArcana.Essentials;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PolygonArcana.Settings
{
	[CreateAssetMenu(
		menuName = ("Settings/" + nameof(AttackPattern)),
		fileName = nameof(AttackPattern)
	)]
	public class AttackPattern : ASettings
	{
		[SF] AttackPoint[] points;

		public IEnumerable<Ray2D> Points => points.Select(p => p.ToRay());

		[Serializable]
		private struct AttackPoint
		{
			public Vector2 Position;
			public float Rotation;

			public Ray2D ToRay()
			{
				return new(
					Position,
					Quaternion.Euler(0f, 0f, Rotation) * Vector2.right
				);
			}

			// public static implicit operator Ray2D(AttackPoint from) => from.ToRay();
		}
	}
}
