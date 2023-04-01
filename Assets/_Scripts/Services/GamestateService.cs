using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;
using PolygonArcana.Models;

namespace PolygonArcana.Services
{
	public class GamestateService : AService<MainModel>
	{
		public void DEBUG_SwitchTo(Gamestate state)
		{
			model.Gamestate.Set(state);
		}

		public void DEBUG_NextState()
		{
			DEBUG_SwitchTo(Next(model.Gamestate));
		}

		private Gamestate Next(Gamestate current)
		{
			return current switch
			{
				Gamestate.Splash => Gamestate.Play,
				Gamestate.Play => Gamestate.End,
				Gamestate.End => Gamestate.Splash,
				_ => Gamestate.Splash,
			};
		}
	}
}
