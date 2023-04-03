using UnityEngine;
using PolygonArcana.Essentials;
using NaughtyAttributes;
using System;
using PolygonArcana.Entities;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Settings
{
	[CreateAssetMenu(
		menuName = ("Settings/" + nameof(PlayerSettings)),
		fileName = nameof(PlayerSettings)
	)]
	public class PlayerSettings : ASettings
	{
		[field: SerializeField]
		public int TMP_Health { get; private set; }
		
		[field: SerializeField]
		public int TMP_MoveSpeed { get; private set; }

		[field: SerializeField]
		public AttackPattern TMP_AttackPattern { get; private set; }

		[field: SerializeField]
		public float AttackPeriod { get; private set; }

		[field: SerializeField]
		public BulletSetup BulletInfo { get; private set; }
		
		[Serializable]
		public struct BulletSetup : Bullet.ISetupInfo
		{
			[SF] BulletColouring Colouring;
			
			[Layer]
			[SF] int Layer;
			
			[SF] int Damage;

			[SF] float Speed;

			public void Deconstruct(out BulletColouring colouring, out int layer, out int damage, out float speed)
			{
				colouring = Colouring;
				layer = Layer;
				damage = Damage;
				speed = Speed;
			}
		}
	}
}
