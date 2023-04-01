using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;

namespace PolygonArcana.Models
{
	public enum Gamestate
	{
		Splash,
		Play,
		End
	}

	public class MainModel : AModel
	{
		public IChange<Gamestate> Gamestate { get; } = new(); 
	}
}
