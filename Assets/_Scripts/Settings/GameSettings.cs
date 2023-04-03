using UnityEngine;
using PolygonArcana.Essentials;
using PolygonArcana.Views;
using System;

namespace PolygonArcana.Settings
{
	//< move me out into a separate file
	[Serializable]
	public class BulletColouring
	{
		[field: SerializeField]
		public Gradient Gradient { get; private set; }

		[field: SerializeField]
		public Vector2Int DamageRange { get; private set; }
	}

	[CreateAssetMenu(
		menuName = ("Settings/" + nameof(GameSettings)),
		fileName = nameof(GameSettings)
	)]
	public class GameSettings : ASettings
	{
		[field: SerializeField]
		public BulletView BulletPrefab { get; private set; }
		
		[field: SerializeField]
		public BulletColouring NpcBulletsColors { get; private set; }

	}
}
