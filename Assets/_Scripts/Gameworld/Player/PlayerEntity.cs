using SF = UnityEngine.SerializeField;
using UnityEngine;
using PolygonArcana.Essentials;
using Zenject;
using PolygonArcana.Factories;
using PolygonArcana.Settings;

namespace PolygonArcana.Entities
{
	public class PlayerEntity : MonoBehaviour, IPooled, IDamaged
	{
		[SF] new Rigidbody2D rigidbody;

		[Inject] ClassFactory classFactory;

		private PlayerMovement movement;
		private PlayerRotation rotation;
		private PlayerAttack attack;

		public bool EnabledByPool
		{
			get => gameObject.activeSelf;
			set => gameObject.SetActive(value);
		}

		public void Initialize(IJoystick joystick, IPlayerSetup setup)
		{
			movement.Initialize(joystick, setup.MoveSpeed);
			rotation.Initialize(joystick);
			attack.Initialize(joystick, setup.AttackPattern, setup.BulletSetup, setup.AttackPeriod);
		}

		public void TakeDamage(Location2D source, int damage)
		{
			throw new();
		}

		private void Awake()
		{
			movement = classFactory.CreateDynamic<PlayerMovement>(rigidbody);
			rotation = classFactory.CreateDynamic<PlayerRotation>(rigidbody);
			attack = classFactory.CreateDynamic<PlayerAttack>(rigidbody);

			EnabledByPool = false;
			Debug.Log("PlayerEntity.Awake()");
		}

		private void FixedUpdate()
		{
			movement.FixedTick();
			rotation.FixedTick();
			attack.FixedTick();
		}
	}
}
