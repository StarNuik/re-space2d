using PolygonArcana.Services;
using UnityEngine.Assertions;
using Zenject;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Entities
{
	public class EnemyHealth
	{
		[Inject] EnemiesLifetimeService enemiesLifetime;

		private Enemy main;
		private int health;

		public EnemyHealth(Enemy main)
		{
			Assert.IsNotNull(main);

			this.main = main;
		}

		public void Initialize(int health)
		{
			Assert.IsTrue(health > 0);

			this.health = health;
		}

		public void TakeDamage(int damage)
		{
			health -= damage;

			if (health >= 0) return;

			enemiesLifetime.Return(main);
		}
	}
}
