using SF = UnityEngine.SerializeField;
using UnityEngine;
using PolygonArcana.Settings;
using PolygonArcana.Essentials;
using Zenject;
using PolygonArcana.Models;
using PolygonArcana.Factories;
using PolygonArcana.Entities;

namespace PolygonArcana._Test
{
	public class DevEnemy : MonoBehaviour
	{
		[Inject] PlayerModel playerModel;
		[Inject] ClassFactory classFactory;

		[SF] new Rigidbody2D rigidbody;
		[SF] AMovementBehaviour movementPattern;
		[SF] float linearSpeed;
		[SF] float angularSpeed;
		[SF] AAttackBehaviour attackPattern;

		private EnemyMovement movement;
		private EnemyRotation rotation;
		private EnemyAttack attack;
		private object attackState;

		private Location2D playerLoc => playerModel.Location;

		private void Awake()
		{
			movement = classFactory.CreateDynamic<EnemyMovement>(rigidbody, movementPattern, linearSpeed);
			rotation = classFactory.CreateDynamic<EnemyRotation>(rigidbody, angularSpeed);
			attackState = attackPattern.NewState();
		}

		private void FixedUpdate()
		{
			movement.FixedTick();
			rotation.FixedTick();

			if (attackPattern.TryAttack(Time.time, attackState, out var pattern))
			{
				// attack.SpawnBullets(pattern);
			}
		}
	}
}
