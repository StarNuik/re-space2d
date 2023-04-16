using SF = UnityEngine.SerializeField;
using UnityEngine;
using PolygonArcana.Essentials;
using PolygonArcana.Settings;
using PolygonArcana.Factories;
using Zenject;
using PolygonArcana.Services;

namespace PolygonArcana.Entities
{
	public class BulletEntity : MonoBehaviour, IPooled, ICulled
	{
		[Inject] ClassFactory classFactory;
		[Inject] BulletsLifetimeService lifetime;

		[SF] new Rigidbody2D rigidbody;
		[SF] Rigidbody2DEvents rigidbodyEvents;
		[SF] Collider2D damageCollider;
		[SF] SpriteRenderer spriteRenderer;

		private BulletMovement movement;
		private BulletScreenCulling culling;
		private BulletCollisions collisions;
		
		private int damage;

		public bool EnabledByPool
		{
			get => gameObject.activeSelf;
			set => gameObject.SetActive(value);
		}

		public void OnOutOfScreen()
		{
			lifetime.Return(this);
		}

		private void Awake()
		{
			movement = classFactory.CreateDynamic<BulletMovement>(
				rigidbody
			);
			collisions = classFactory.CreateDynamic<BulletCollisions>(
				this, rigidbody
			);

			rigidbodyEvents.TriggerEnter2D += collisions.OnEnter;

			EnabledByPool = false;
		}

		public void Initialize(
			Vector2 position,
			Vector2 direction,
			IBulletSetup setup
		)
		{
			var (layer, speed, damage, color) = setup;

			movement.Initialize(position, (Vector2Norm)direction, speed);
			collisions.Initialize(damage);

			damageCollider.gameObject.layer = layer;
			spriteRenderer.color = color;

			this.damage = damage;
		}

		private void FixedUpdate()
		{
			if (!EnabledByPool) return;

			movement.FixedTick();
		}
	}
}
