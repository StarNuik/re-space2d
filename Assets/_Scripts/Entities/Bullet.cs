using SF = UnityEngine.SerializeField;
using UnityEngine;
using PolygonArcana.Essentials;
using PolygonArcana.Views;
using PolygonArcana.Settings;
using System;
using UnityEngine.Pool;

namespace PolygonArcana.Entities
{
	public class Bullet : MonoBehaviour, IPooled
	{
		[SF] new Rigidbody2D rigidbody;
		[SF] SpriteRenderer spriteRenderer;

		private BulletMovement movement;

		public bool EnabledByPool
		{
			get => gameObject.activeSelf;
			set => gameObject.SetActive(value);
		}

		//> limit bullets lifetime activity to IPooled
		private void Awake()
		{
			movement = new(rigidbody);
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
		{}
	}
}
