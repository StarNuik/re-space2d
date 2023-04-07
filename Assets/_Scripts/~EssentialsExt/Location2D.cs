using System;
using UnityEngine;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Essentials
{
	public struct Location2D
	{
		private static Vector2 nullPosition => Vector2.zero;
		private static Vector2Norm nullDirection => Vector2Norm.right;

		public Vector2 Position { get; private set; }
		public Vector2Norm Direction { get; private set; }
		public Quaternion Rotation
			=> Quaternion.FromToRotation(
				Vector3.right,
				Direction.ToXY0()
			);

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

		public Location2D NoPosition()
		{
			return new(nullPosition, Direction);
		}

		public Location2D NoDirection()
		{
			return new(Position, nullDirection);
		}

		public Location2D Transform(Location2D other)
		{
			Matrix4x4 trs = Matrix4x4.TRS(Position, Rotation, Vector3.one);
			return new(
				trs.MultiplyPoint(other.Position),
				trs.MultiplyVector(other.Direction.ToXY0())
			);
		}

		public Location2D MoveTowards(Location2D to, float linearDelta, float angularDelta)
		{
			var nextPosition = MoveTowardsHelper(
				Position,
				to.Position,
				linearDelta,
				Vector2.Distance,
				Vector2.Lerp
			);

			var nextDirection = MoveTowardsHelper(
				Direction,
				to.Direction,
				angularDelta,
				Vector2.Angle,
				Vector2Extension.Slerp
			);

			return new(nextPosition, nextDirection);
		}

		private Vector2 MoveTowardsHelper(
			Vector2 from,
			Vector2 to,
			float delta,
			Func<Vector2, Vector2, float> distanceFunc,
			Func<Vector2, Vector2, float, Vector2> interpolationFunc
		)
		{
			var distance = distanceFunc(from, to);
			delta = Mathf.Min(delta, distance);
			var f = delta / distance;
			var next = interpolationFunc(from, to, f);
			return next;
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

		// public static Location2D operator+(Location2D left, Location2D right)
		// {
		// 	return new(
		// 		left.Position + right.Position,
		// 		left.Rotation * right.Rotation * Vector2.right
		// 	);
		// }
	}
}
