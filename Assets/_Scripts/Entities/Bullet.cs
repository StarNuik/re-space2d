using SF = UnityEngine.SerializeField;
using UnityEngine;
using PolygonArcana.Essentials;
using PolygonArcana.Settings;
using PolygonArcana.Factories;
using Zenject;

namespace PolygonArcana.Entities
{
	public class Bullet : MonoBehaviour, IPooled
	{
		[Inject] ClassFactory classFactory;

		[SF] new Rigidbody2D rigidbody;
		[SF] SpriteRenderer spriteRenderer;
		[SF] float cullingRadius;

		private BulletMovement movement;
		private BulletScreenCulling culling;

		public bool EnabledByPool
		{
			get => gameObject.activeSelf;
			set => gameObject.SetActive(value);
		}

		//> limit bullets lifetime activity to IPooled
		private void Awake()
		{
			movement = classFactory.CreateDynamic<BulletMovement>(rigidbody);
			culling = classFactory.CreateDynamic<BulletScreenCulling>(
				this, rigidbody, cullingRadius
			);
			EnabledByPool = false;
		}

		public void Initialize(
			Vector2 position,
			Vector2 direction,
			IBulletSettup setup
		)
		{
			var (layer, speed, damage, color) = setup;

			movement.Initialize(position, direction, speed);

			gameObject.layer = layer;
			spriteRenderer.color = color;
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
