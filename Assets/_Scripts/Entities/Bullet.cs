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

		private float speed;
		private int damage;

		private Vector2 Position
		{
			get => rigidbody.position;
			set => rigidbody.MovePosition(value);
		}

		private Vector2 Direction
		{
			get => (Vector2)transform.right;
			set => transform.LookInDirection2D(value);
		}

		public bool EnabledByPool
		{
			get => gameObject.activeSelf;
			set => gameObject.SetActive(value);
		}

		//> limit bullets lifetime activity to IPooled
		private void Awake()
		{
			EnabledByPool = false;
		}

		public void Initialize(
			Vector2 position,
			Vector2 direction,
			IBulletSettup setup
		)
		{
			//> rigidbody.MovePosition doesnt like large movements
			transform.position = position;
			Direction = direction;

			var (layer, speed, damage, color) = setup;
			this.damage = damage;
			this.speed = speed;

			gameObject.layer = layer;
			spriteRenderer.color = color;
		}

		private void FixedUpdate()
		{
			//> for clarity
			if (!EnabledByPool) return;

			var delta = Direction * speed;
			Position += delta * Time.deltaTime;
		}
	}
}
