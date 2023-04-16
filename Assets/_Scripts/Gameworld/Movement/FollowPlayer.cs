using UnityEngine;
using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;

namespace PolygonArcana.Settings
{
	[CreateAssetMenu(
		menuName = ("Settings/Movement Behaviours/" + nameof(FollowPlayer)),
		fileName = nameof(FollowPlayer)
	)]
	public class FollowPlayer : AMovementBehaviour
	{
		[Min(0.1f)]
		[SF] float radius;

		public override Vector3 TargetPosition(Location2D unit, Location2D player)
		{
			var toUnit = unit.Position - player.Position;
			var target = player.Position + toUnit.normalized * radius;
			return target;
		}
	}
}
