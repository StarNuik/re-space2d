using PolygonArcana.Essentials;
using UnityEngine;
using UnityEngine.Assertions;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Entities
{
	public class PlayerMovement
	{
		private Rigidbody2D rigidbody;
		private IJoystick joystick;
		private float speed;

		public PlayerMovement(Rigidbody2D rigidbody)
		{
			Assert.IsNotNull(rigidbody);

			this.rigidbody = rigidbody;
		}

		public void Initialize(IJoystick joystick, float speed)
		{
			Assert.IsNotNull(joystick);

			this.joystick = joystick;
			this.speed = speed;
		}

		public void FixedTick()
		{
			var delta = (Vector2Norm)joystick.Movement * speed * Time.deltaTime;
			rigidbody.MovePosition(rigidbody.position + delta);
		}
	}
}
