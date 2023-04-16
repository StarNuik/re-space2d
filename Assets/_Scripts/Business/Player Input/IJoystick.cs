using PolygonArcana.Essentials;
using UnityEngine;

namespace PolygonArcana
{
	public interface IJoystick
	{
		Vector2Int Movement { get; }
		Vector2Int Attack { get; }
		BoolTrigger SubmitTrigger { get; }
		BoolTrigger CancelTrigger { get; }
	}
}
