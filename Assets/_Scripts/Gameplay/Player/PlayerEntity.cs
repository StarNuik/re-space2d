using SF = UnityEngine.SerializeField;
using UnityEngine;
using PolygonArcana.Essentials;

namespace PolygonArcana.Entities
{
	public class PlayerEntity : MonoBehaviour, IPooled, IDamaged
	{
		public bool EnabledByPool
		{
			get => gameObject.activeSelf;
			set => gameObject.SetActive(value);
		}

		public void TakeDamage(Location2D source, int damage)
		{
			throw new();
		}
	}
}
