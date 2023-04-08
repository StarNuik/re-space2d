using PolygonArcana.Essentials;
using PolygonArcana.Models;
using UnityEngine;
using Zenject;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Entities
{
	public class EnemyRotation
	{
		[Inject] PlayerModel playerModel;

		private Rigidbody2D rigidbody;
		private float speed;
		private Transform transform;

		private Location2D playerLocation => playerModel.Location;

		public EnemyRotation(Rigidbody2D rigidbody, float speed)
		{
			this.rigidbody = rigidbody;
			this.speed = speed;

			transform = rigidbody.transform;
		}

		public void ChangeTo(Vector2 direction)
		{
			transform.SetLookDirection2D(direction);
		}

		public void FixedTick()
		{
			var toPlayer = (transform.position.ToXY() - playerLocation.Position).normalized;
			var forward = transform.right.ToXY();

			var distance = Vector2.Angle(toPlayer, forward);
			var travelF = speed * Time.deltaTime / distance;
			var nextDir = Vector2Extension.Slerp(forward, toPlayer, travelF);
			transform.SetLookDirection2D(nextDir);
		}
	}
}
