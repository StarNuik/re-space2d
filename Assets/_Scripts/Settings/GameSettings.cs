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
		public Bullet BulletPrefab { get; private set; }
	}
}
