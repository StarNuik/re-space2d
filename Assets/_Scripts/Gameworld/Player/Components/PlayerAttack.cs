using PolygonArcana.Essentials;
using PolygonArcana.Services;
using PolygonArcana.Settings;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Entities
{
	public class PlayerAttack
	{
		[Inject] BulletsLifetimeService bulletsLifetime;

		private Rigidbody2D rigidbody;
		private IJoystick joystick;
		private IBulletSetup bulletSetup;
		private AttackPattern pattern;
		private float attackPeriod;

		private float lastAttack = float.NegativeInfinity;

		public PlayerAttack(Rigidbody2D rigidbody)
		{
			Assert.IsNotNull(rigidbody);

			this.rigidbody = rigidbody;
		}

		public void Initialize(IJoystick joystick, AttackPattern pattern, IBulletSetup bulletSetup, float attackPeriod)
		{
			Assert.IsNotNull(joystick);
			Assert.IsNotNull(pattern);
			Assert.IsNotNull(bulletSetup);
			Assert.IsTrue(attackPeriod > 0f);

			this.joystick = joystick;
			this.pattern = pattern;
			this.bulletSetup = bulletSetup;
			this.attackPeriod = attackPeriod;
		}

		public void FixedTick()
		{
			var isAttacking = joystick.Attack != Vector2Int.zero;
			var attackCooledDown = Time.time >= lastAttack + attackPeriod;

			if (!isAttacking || !attackCooledDown) return;
			lastAttack = Time.time;

			var direction = (Vector2Norm)joystick.Attack;
			var location = new Location2D(rigidbody.position, direction);

			foreach (var bulletLocal in pattern.Points)
			{
				var global = location.Transform((Location2D)bulletLocal);

				bulletsLifetime.Take(global.Position, global.Direction, bulletSetup);
			}
		}
	}
}
