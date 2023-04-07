using UnityEngine;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Essentials
{
	public static class TransformExtension2
	{
		public static void SetLookDirection2D(this Transform @this, Vector2 direction)
			=> @this.SetLookDirection2D((Vector2Norm)direction);
	
		public static void SetLookDirection2D(this Transform @this, Vector2Norm direction)
		{
			@this.rotation = Quaternion.FromToRotation(
				Vector3.right,
				direction.ToXY0()
			);
		}

		public static void SetLocation2D(this Transform @this, Location2D location)
		{
			var (position, direction) = location;

			@this.position = position;
			@this.SetLookDirection2D(direction);
		}

		public static Location2D ToLocation2D(this Transform @this)
		{
			return new(
				@this.position,
				@this.rotation * Vector2.right
			);
		}

		public static void MoveTowards2D(this Transform @this, Location2D to, float linearDelta, float angularDelta)
			=> @this.SetLocation2D(
				@this
					.ToLocation2D()
					.MoveTowards(to, linearDelta, angularDelta)
			);
	}
}
