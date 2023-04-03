using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;
using UnityEngine;
using PolygonArcana.Entities;
using PolygonArcana.Settings;

namespace PolygonArcana.Views
{
	public class BulletView : AView
	{
		[SF] new Rigidbody2D rigidbody;
		[SF] SpriteRenderer spriteRenderer;

		public Rigidbody2D Rigidbody => rigidbody;

		public void Refresh(Bullet.ISetupInfo setupInfo)
		{
			var (colouring, _, damage, _) = setupInfo;
			var fColor = Mathf.Lerp(colouring.DamageRange.x, colouring.DamageRange.y, damage);
			var color = colouring.Gradient.Evaluate(fColor);
			spriteRenderer.color = color;
		}
	}
}
