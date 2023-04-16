using UnityEngine;
using UnityEngine.Assertions;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Entities
{
	public class PlayerRotation
	{
		private Rigidbody2D rigidbody;
		private IJoystick joystick;

		public PlayerRotation(Rigidbody2D rigidbody)
		{
			Assert.IsNotNull(rigidbody);

			this.rigidbody = rigidbody;
		}

		public void Initialize(IJoystick joystick)
		{
			Assert.IsNotNull(joystick);

			this.joystick = joystick;
		}

		public void FixedTick()
		{
			var hasAttack = joystick.Attack != Vector2Int.zero;
			var targetStick = hasAttack ? joystick.Attack : joystick.Movement;

			if (targetStick == Vector2Int.zero) return;

			var direction = (Vector2)targetStick;
			var angle = Vector2.SignedAngle(Vector2.right, direction);

			rigidbody.MoveRotation(angle);
		}
	}
}
