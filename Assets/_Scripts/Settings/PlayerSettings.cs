using UnityEngine;
using PolygonArcana.Essentials;

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
	}
}
