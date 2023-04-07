using UnityEngine;
namespace PolygonArcana.Essentials
{
	public struct Vector2Norm
	{
		private Vector2 Value { get; set; }

		public float x => Value.x;
		public float y => Value.y;

		public Vector2Norm(Vector2 value) => Value = value.normalized;

		public Vector3 ToXY0() => new(x, y, 0f);

		public static implicit operator Vector2(Vector2Norm from) => from.Value;
	
		public static explicit operator Vector2Norm(Vector2 from) => new(from);
	}
}
