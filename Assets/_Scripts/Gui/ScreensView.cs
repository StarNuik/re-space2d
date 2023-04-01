using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;
using PolygonArcana.Models;
using UnityEngine;
using System.Linq;
using UnityEngine.Assertions;
using OneLine;

namespace PolygonArcana.Views
{
	public class ScreensView : AView<MainModel>
	{
		[OneLine]
		[SF] ScreenEntry[] screens;

		private RectTransform openScreen;

		private void Awake()
		{
			Assert.IsNotNull(screens);
			Assert.IsTrue(screens.All(e => e.Screen != null));
			Assert.IsTrue(screens.Any(e => e.State == Gamestate.Splash));
			Assert.IsTrue(screens.Any(e => e.State == Gamestate.Play));
			Assert.IsTrue(screens.Any(e => e.State == Gamestate.End));

			model.Gamestate.OnChanged += OnGamestateChanged;
			OnGamestateChanged();
		}

		private void OnGamestateChanged()
		{
			if (openScreen != null)
			{
				openScreen.gameObject.SetActive(false);
			}

			//> throws if no suitable entry found - intended behaviour
			var (_, nextScreen) = screens.First(e => e.State == model.Gamestate);
			
			nextScreen.gameObject.SetActive(true);
			openScreen = nextScreen;
		}

		[System.Serializable]
		private struct ScreenEntry
		{
			public Gamestate State;
			public RectTransform Screen;

			public void Deconstruct(out Gamestate state, out RectTransform screen)
			{
				state = State;
				screen = Screen;
			}
		}
	}
}
