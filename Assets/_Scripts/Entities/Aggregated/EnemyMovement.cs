using PolygonArcana.Essentials;
using PolygonArcana.Models;
using PolygonArcana.Settings;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Entities
{
	public class EnemyMovement
	{
		[Inject] PlayerModel playerModel;

		private Rigidbody2D rigidbody;
		private AMovementBehaviour pattern;
		private float speed;
		private Transform transform;

		private Location2D location => transform.ToLocation2D();
		private Location2D playerLocation => playerModel.Location;

		public EnemyMovement(
			Rigidbody2D rigidbody,
			AMovementBehaviour pattern,
			float speed
		)
		{
			Assert.IsNotNull(pattern);
			Assert.IsNotNull(rigidbody);
			Assert.IsTrue(speed > 0f);

			this.rigidbody = rigidbody;
			this.pattern = pattern;
			this.speed = speed;

			transform = rigidbody.transform;
		}

		public void FixedTick()
		{
			var targetPos = pattern.TargetPosition(location, playerLocation);
			var nextPos = Vector3.MoveTowards(
				transform.position,
				targetPos,
				speed * Time.deltaTime
			);

			rigidbody.MovePosition(nextPos);
		}
	}
}
