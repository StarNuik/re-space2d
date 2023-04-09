using PolygonArcana.Essentials;
using PolygonArcana.Services;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Entities
{
	public class BulletCollisions
	{
		[Inject] BulletsLifetimeService bulletsLifetime;

		private Bullet main;
		private Transform transform;
		private int damage;

		public BulletCollisions(Bullet main, Rigidbody2D rigidbody)
		{
			Assert.IsNotNull(rigidbody);

			transform = rigidbody.transform;
			this.main = main;
		}

		public void Initialize(int damage)
		{
			Assert.IsTrue(damage > 0);

			this.damage = damage;
		}

		public void OnEnter(Collider2D collider)
		{
			if (collider.attachedRigidbody.TryGetComponent<IDamaged>(out var damaged))
			{
				damaged.TakeDamage(
					transform.ToLocation2D(),
					damage
				);
			}

			bulletsLifetime.Return(main);
		}
	}
}
