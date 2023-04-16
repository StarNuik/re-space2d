using SF = UnityEngine.SerializeField;
using UnityEngine;
using PolygonArcana.Essentials;
using PolygonArcana.Settings;
using PolygonArcana.Factories;
using PolygonArcana.Models;
using Zenject;
using UnityEngine.Assertions;

namespace PolygonArcana.Entities
{
	public class EnemyEntity : MonoBehaviour, IPooled, IDamaged, IRareTickable
	{
		[Inject] ClassFactory classFactory;

		[SF] new Rigidbody2D rigidbody;

		private EnemyMovement movement;
		private EnemyRotation rotation;
		private EnemyAttack attack;
		private EnemyHealth health;
		private object attackState;

		public bool EnabledByPool
		{
			get => gameObject.activeSelf;
			set => gameObject.SetActive(value);
		}

		private void Awake()
		{
			movement = classFactory.CreateDynamic<EnemyMovement>(rigidbody);
			rotation = classFactory.CreateDynamic<EnemyRotation>(rigidbody);
			attack = classFactory.CreateDynamic<EnemyAttack>(rigidbody);
			health = classFactory.CreateDynamic<EnemyHealth>(this);

			EnabledByPool = false;
		}

		public void Initialize(Location2D location, Settings.Enemy settings)
		{
			Assert.IsNotNull(settings);

			movement.Initialize(settings.Movement, settings.LinearSpeed);
			rotation.Initialize(settings.AngularSpeed);
			attack.Initialize(settings.Attack, settings.BulletInfo);
			health.Initialize(settings.Health);

			movement.ChangeTo(location.Position);
			rotation.ChangeTo(location.Direction);
		}

		private void FixedUpdate()
		{
			if (!EnabledByPool) return;

			movement.FixedTick();
			rotation.FixedTick();
		}

		public void RareTick()
		{
			//> jic, yk
			if (!EnabledByPool) return;
			
			attack.RareTick();
		}

		public void TakeDamage(Location2D source, int damage)
		{
			if (!EnabledByPool) return;

			health.TakeDamage(damage);
		}
	}
}
