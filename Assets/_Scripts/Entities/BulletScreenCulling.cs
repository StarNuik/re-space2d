using PolygonArcana.Services;
using UnityEngine;
using Zenject;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Entities
{
	public class BulletScreenCulling
	{
		private Bullet main;
		private Rigidbody2D rigidbody;
		private float cullingRadius;

		[Inject] ScreenBoundsService screenBounds;
		[Inject] BulletsLifetimeService bulletsLifetime;

		public BulletScreenCulling(
			Bullet main,
			Rigidbody2D rigidbody,
			float cullingRadius
		)
		{
			this.main = main;
			this.rigidbody = rigidbody;
			this.cullingRadius = cullingRadius;
		}

		public void RareTick()
		{
			var info = new Collider()
			{
				Position = rigidbody.position,
				BoundsRadius = cullingRadius,
			};

			var isOnScreen = screenBounds.IsInside(info);

			if (!isOnScreen)
			{
				bulletsLifetime.Return(main);
			}
		}

		private struct Collider : IVisibilityInfo
		{
			public Vector2 Position { get; set; }
			public float BoundsRadius { get; set; }

			public void Deconstruct(out Vector2 position, out float radius)
			{
				position = Position;
				radius = BoundsRadius;
			}
		}
	}
}
