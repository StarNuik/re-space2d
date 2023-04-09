using System;
using NaughtyAttributes;
using UnityEngine;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Settings
{
	[Serializable]
	public struct BulletSetupStatic : IBulletSetup
	{
		[Layer]
		[SF] int Layer;
		[SF] float Speed;
		[SF] int Damage;
		[SF] Color Color;

		public void Deconstruct(out int layer, out float speed, out int damage, out Color color)
		{
			layer = Layer;
			speed = Speed;
			damage = Damage;
			color = Color;
		}
	}
}
