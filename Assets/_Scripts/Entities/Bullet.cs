using SF = UnityEngine.SerializeField;
using UnityEngine;
using PolygonArcana.Essentials;
using PolygonArcana.Views;
using PolygonArcana.Settings;
using System;

namespace PolygonArcana.Entities
{
	public class Bullet //? pretty much : AModel
	{
		public interface ISetupInfo
		{
			void Deconstruct(out BulletColouring colouring, out int layer, out int damage, out float speed);
		}

		public int Damage { get; private set; }
		public float Speed { get; private set; }
		public BulletColouring Colouring { get; private set; }

		public int Layer
		{
			get => View.gameObject.layer;
			set => View.gameObject.layer = value;
		}

		public Vector2 Position
		{
			get => View.Rigidbody.position;
			set => View.Rigidbody.MovePosition(value);
		}

		public Vector2 Direction
		{
			get => View.transform.right;
			set => View.transform.LookInDirection2D(value);
		}
		
		protected BulletView View { get; private set; }

		public Bullet(BulletView view)
		{
			View = view;
		}

		public void Initialize(
			Vector2 position,
			Vector2 direction,
			ISetupInfo setupInfo
		)
		{
			View.transform.position = position;
			Direction = direction;

			(_, Layer, Damage, Speed) = setupInfo;

			View.Refresh(setupInfo);
		}

		
	}
}
