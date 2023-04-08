using UnityEngine;
using PolygonArcana.Essentials;

namespace PolygonArcana.Settings
{
	[CreateAssetMenu(
		menuName = ("Settings/" + nameof(Enemy)),
		fileName = nameof(Enemy)
	)]
	public class Enemy : ASettings
	{
		[field: SerializeField]
		public int Score { get; private set; }
		
		[field: SerializeField]
		public float LinearSpeed { get; private set; }
		
		[field: SerializeField]
		public float AngularSpeed { get; private set; }
		
		[field: SerializeField]
		public AMovementBehaviour Movement { get; private set; }
		
		[field: SerializeField]
		public AAttackBehaviour Attack { get; private set; }
		
		[field: SerializeField]
		public BulletSetupStatic BulletInfo { get; private set; }
	}
}
