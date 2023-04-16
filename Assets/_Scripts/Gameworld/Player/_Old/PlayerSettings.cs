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
	public class PlayerSettings : ASettings, IPlayerSetup
	{
		[field: SerializeField]
		public int TMP_Health { get; private set; }
		
		[field: SerializeField]
		public float TMP_MoveSpeed { get; private set; }

		[field: SerializeField]
		public AttackPattern TMP_AttackPattern { get; private set; }

		[field: SerializeField]
		public float AttackPeriod { get; private set; }

		[field: SerializeField]
		public BulletSetupStatic BulletInfo { get; private set; }

		public AttackPattern AttackPattern => TMP_AttackPattern;
		public int MaxHealth => TMP_Health;
		public float MoveSpeed => TMP_MoveSpeed;
		public IBulletSetup BulletSetup => BulletInfo;
	}
}
