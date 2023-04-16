using UnityEngine;

namespace PolygonArcana.Settings
{
	public interface IBulletSetup
	{
		void Deconstruct(out int layer, out float speed, out int damage, out Color color);
	}
}
