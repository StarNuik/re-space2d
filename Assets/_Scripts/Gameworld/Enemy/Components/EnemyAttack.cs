using PolygonArcana.Essentials;
using PolygonArcana.Services;
using PolygonArcana.Settings;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Entities
{
	public class EnemyAttack
	{
		[Inject] BulletsLifetimeService bulletsLifetime;
		
		private Transform transform;
		private AAttackBehaviour attackBehaviour;
		private IBulletSetup bulletSetup;
		private object attackState;
		private float initTimestamp;

		private float localTime => Time.time - initTimestamp;

		public EnemyAttack(Rigidbody2D rigidbody)
		{
			Assert.IsNotNull(rigidbody);

			transform = rigidbody.transform;
		}

		public void Initialize(AAttackBehaviour attack, IBulletSetup bulletSetup)
		{
			Assert.IsNotNull(attack);
			Assert.IsNotNull(bulletSetup);
			
			attackBehaviour = attack;
			this.bulletSetup = bulletSetup;
			initTimestamp = Time.time;
			attackState = attackBehaviour.NewState();
		}

		public void RareTick()
		{
			if (!attackBehaviour.TryAttack(localTime, attackState, out var pattern)) return;

			SpawnBullets(pattern, bulletSetup);
		}

		private void SpawnBullets(AttackPattern pattern, IBulletSetup bulletSetup)
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
