using UnityEngine;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Essentials
{
	public static class TransformExtension2
	{
		public static void SetLookDirection2D(this Transform @this, Vector2 direction)
		{
			@this.rotation = Quaternion.FromToRotation(
				Vector3.right,
				(Vector3)direction
			);
		}
	}
}
