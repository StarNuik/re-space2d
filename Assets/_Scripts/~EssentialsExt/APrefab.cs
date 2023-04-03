using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace PolygonArcana.Essentials
{
	//> for now, this is an overcomplication.
	//> the idea was to separate MobnoBehaviour's public fields
	//> out of the MVC context
	public abstract class APrefab<T> : MonoBehaviour
		where T : class, IOwnPrefab
	{
		public abstract void ConnectTo(T entity);
	}
}
