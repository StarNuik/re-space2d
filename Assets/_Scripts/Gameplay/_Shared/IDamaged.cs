using PolygonArcana.Essentials;

namespace PolygonArcana.Entities
{
	public interface IDamaged
	{
		void TakeDamage(Location2D source, int damage);
	}
}
