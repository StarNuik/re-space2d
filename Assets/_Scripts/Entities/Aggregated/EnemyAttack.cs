using PolygonArcana.Essentials;
using PolygonArcana.Services;
using PolygonArcana.Settings;
using UnityEngine;
using Zenject;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Entities
{
	public class EnemyAttack
	{
		[Inject] BulletsLifetimeService bulletsLifetime;
		
		private Transform transform;

		public EnemyAttack(
			Rigidbody2D rigidbody
		)
		{
			transform = rigidbody.transform;
		}

		public void SpawnBullets(AttackPattern pattern, IBulletSettup bulletSetup)
		{
			var global = transform.ToLocation2D();

			foreach (Location2D local in pattern.Points)
			{
				var (position, direction) = global.Transform(local);
				bulletsLifetime.Take(position, direction, bulletSetup);
			}
		}
	}
}
