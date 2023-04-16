using UnityEngine;
using PolygonArcana.Essentials;

namespace PolygonArcana.Settings
{
	[CreateAssetMenu(
		menuName = ("Settings/" + nameof(GuiSettings)),
		fileName = nameof(GuiSettings)
	)]
	public class GuiSettings : ASettings
	{
		[field: SerializeField]
		public int Field { get; private set; }
	}
}
