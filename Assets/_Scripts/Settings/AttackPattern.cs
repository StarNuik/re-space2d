using SF = UnityEngine.SerializeField;
using UnityEngine;
using PolygonArcana.Essentials;
using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine.Internal;

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
		private class AttackPoint
		{
			public Vector2 Position;

			[Range(0f, 1f)]
			public float Rotation = 0.5f;

			public Ray2D ToRay()
			{
				var actualRotation = Mathf.Lerp(270f, -90f, Rotation);
				return new(
					Position,
					Quaternion.Euler(0f, 0f, actualRotation) * Vector2.right
				);
			}

			// public static implicit operator Ray2D(AttackPoint from) => from.ToRay();
		}
	}
}
