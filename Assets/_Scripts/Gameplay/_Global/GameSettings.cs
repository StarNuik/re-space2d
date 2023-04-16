using UnityEngine;
using PolygonArcana.Essentials;
using PolygonArcana.Views;
using System;
using PolygonArcana.Entities;

namespace PolygonArcana.Settings
{
	[CreateAssetMenu(
		menuName = ("Settings/" + nameof(GameSettings)),
		fileName = nameof(GameSettings)
	)]
	public class GameSettings : ASettings
	{
		[field: SerializeField]
		public BulletEntity BulletPrefab { get; private set; }
		
		[field: SerializeField]
		public Entities.EnemyEntity EnemyPrefab { get; private set; }

		[field: SerializeField]
		public float ScreenBorderMargin { get; private set; }
	}
}
