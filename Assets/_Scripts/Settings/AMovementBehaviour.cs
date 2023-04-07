using UnityEngine;
using PolygonArcana.Essentials;

namespace PolygonArcana.Settings
{
	public abstract class AMovementBehaviour : ASettings
	{
		public abstract Vector3 TargetPosition(Location2D unit, Location2D player);
	}
}
