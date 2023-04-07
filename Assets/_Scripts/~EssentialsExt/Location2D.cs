using System;
using UnityEngine;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Essentials
{
	public struct Location2D
	{
		private Vector2 Position { get; set; }
		private Vector2Norm Direction { get; set; }

		public Location2D(Vector2 position, Vector2Norm direction)
		{
			Position = position;
			Direction = direction;
		}

		public Location2D(Vector2 position, Vector2 direction)
		{
			Position = position;
			Direction = (Vector2Norm)direction;
		}

		public Location2D(Vector2 position, Quaternion rotation)
		{
			Position = position;
			Direction = (Vector2Norm)(rotation * Vector3.right).ToXY();
		}

		public Location2D(Ray2D ray)
		{
			Position = ray.origin;
			Direction = (Vector2Norm)ray.direction;
		}

		public Location2D AddOffset(Vector2 offset)
		{
			return new(this.Position + offset, this.Direction);
		}

		public Location2D AddOffsetOf(Location2D other)
		{
			return AddOffset(other.Position);
		}

		public void Deconstruct(out Vector2 position, out Vector2Norm direction)
		{
			position = Position;
			direction = Direction;
		}

		public static Location2D LerpUnclamped(Location2D from, Location2D to, float t)
		{
			return new(
				Vector2.LerpUnclamped(from.Position, to.Position, t),
				Vector2Extension.SlerpUnclamped(from.Direction, to.Direction, t)
			);
		}

		// public static operator+(Location2D left, Location2D right)
	}
}
