using PolygonArcana.Essentials;
using UnityEngine;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Entities
{
	public class BulletMovement
	{
		private Rigidbody2D rigidbody;
		private Transform transform;
		private Vector2 movementDelta;

		public BulletMovement(Rigidbody2D rigidbody)
		{
			this.rigidbody = rigidbody;
			this.transform = rigidbody.transform;
		}

		public void Initialize(
			Vector2 position,
			Vector2Norm direction,
			float speed
		)
		{
			//> Rigidbody2D.MovePosition doesnt like large movements
			transform.position = position.ToXY0();
			transform.SetLookDirection2D(direction);
			movementDelta = direction * speed;

			Debug.Log(movementDelta.magnitude);
		}

		public void FixedTick()
		{
			var delta = movementDelta * Time.deltaTime;
			rigidbody.MovePosition(rigidbody.position + delta);
		}
	}
}
