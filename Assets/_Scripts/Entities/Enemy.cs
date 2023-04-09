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
	public class Enemy : MonoBehaviour, IPooled, IDamaged, IRareTickable
	{
		[Inject] PlayerModel playerModel;
		[Inject] ClassFactory classFactory;

		[SF] new Rigidbody2D rigidbody;

		private Settings.Enemy settings;
		private AAttackBehaviour attackPattern;

		private EnemyMovement movement;
		private EnemyRotation rotation;
		private EnemyAttack attack;
		private object attackState;

		private Location2D playerLoc => playerModel.Location;

		public bool EnabledByPool
		{
			get => gameObject.activeSelf;
			set => gameObject.SetActive(value);
		}

		//> if pooling becomes a reality, move most of this code
		//> to the constructor and add .Initialize() methods to the components
		public void Initialize(Location2D location, Settings.Enemy settings)
		{
			Assert.IsNotNull(settings);
			Assert.IsNotNull(settings.Attack);
			Assert.IsNotNull(settings.Movement);
			Assert.IsTrue(settings.Health > 0);
			Assert.IsTrue(settings.LinearSpeed > 0f);
			Assert.IsTrue(settings.AngularSpeed > 0f);
			
			this.settings = settings;
			attackPattern = settings.Attack;

			movement = classFactory.CreateDynamic<EnemyMovement>(
				rigidbody, settings.Movement, settings.LinearSpeed
			);
			rotation = classFactory.CreateDynamic<EnemyRotation>(
				rigidbody, settings.AngularSpeed
			);
			attack = classFactory.CreateDynamic<EnemyAttack>(
				rigidbody
			);
			attackState = attackPattern.NewState();

			movement.ChangeTo(location.Position);
			rotation.ChangeTo(location.Direction);
		}

		private void Awake()
		{
			EnabledByPool = false;
		}

		private void FixedUpdate()
		{
			movement.FixedTick();
			rotation.FixedTick();

			if (attackPattern.TryAttack(Time.time, attackState, out var pattern))
			{
				attack.SpawnBullets(pattern, settings.BulletInfo);
			}
		}

		//> jic, yk
		public void RareTick()
		{}

		public void TakeDamage(Location2D source, int damage)
		{
			Debug.Log("ow: " + damage);
		}
	}
}
