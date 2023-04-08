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
	public class Enemy : MonoBehaviour, IPooled
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

		public void Initialize(Location2D location, Settings.Enemy settings)
		{
			Assert.IsNotNull(settings);
			Assert.IsNotNull(settings.Attack);
			Assert.IsNotNull(settings.Movement);
			
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
	}
}
