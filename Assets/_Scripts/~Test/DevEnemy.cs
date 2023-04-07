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
		[SF] AMovementBehaviour movementBehaviour;
		[SF] float linearSpeed;
		[SF] float angularSpeed;

		private EnemyMovement movement;
		private EnemyRotation rotation;

		private Location2D playerLoc => playerModel.Location;

		private void Awake()
		{
			movement = classFactory.CreateDynamic<EnemyMovement>(rigidbody, movementBehaviour, linearSpeed);
			rotation = classFactory.CreateDynamic<EnemyRotation>(rigidbody, angularSpeed);
		}

		private void FixedUpdate()
		{
			movement.FixedTick();
			rotation.FixedTick();
		}
	}
}
