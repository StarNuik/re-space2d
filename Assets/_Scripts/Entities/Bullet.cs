using SF = UnityEngine.SerializeField;
using UnityEngine;
using PolygonArcana.Essentials;
using PolygonArcana.Settings;
using PolygonArcana.Factories;
using Zenject;

namespace PolygonArcana.Entities
{
	public class Bullet : MonoBehaviour, IPooled, IRareTickable
	{
		[Inject] ClassFactory classFactory;

		[SF] new Rigidbody2D rigidbody;
		[SF] SpriteRenderer spriteRenderer;
		[SF] float cullingRadius;

		private int damage;

		private BulletMovement movement;
		private BulletScreenCulling culling;
		private BulletCollisions collisions;

		public bool EnabledByPool
		{
			get => gameObject.activeSelf;
			set => gameObject.SetActive(value);
		}

		private void Awake()
		{
			movement = classFactory.CreateDynamic<BulletMovement>(
				rigidbody
			);
			culling = classFactory.CreateDynamic<BulletScreenCulling>(
				this, rigidbody, cullingRadius
			);
			collisions = classFactory.CreateDynamic<BulletCollisions>(
				this, rigidbody
			);

			//> limit bullets lifetime activity to IPooled
			EnabledByPool = false;
		}

		public void Initialize(
			Vector2 position,
			Vector2 direction,
			IBulletSetup setup
		)
		{
			var (layer, speed, damage, color) = setup;

			movement.Initialize(position, direction, speed);
			collisions.Initialize(damage);

			gameObject.layer = layer;
			spriteRenderer.color = color;

			this.damage = damage;
		}

		private void FixedUpdate()
		{
			if (!EnabledByPool) return;

			movement.FixedTick();
		}

		public void RareTick()
		{
			culling.RareTick();
		}

		private void OnTriggerEnter2D(Collider2D collider)
		{
			if (!EnabledByPool) return;

			collisions.OnEnter(collider);
		}

		#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			if (Application.isPlaying) return;

			Gizmos.color = Color.yellow;
			GizmosExt.DrawCircle(rigidbody.position, cullingRadius, Vector3.up);
		}
		#endif
	}
}
