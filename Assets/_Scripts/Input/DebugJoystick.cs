using SF = UnityEngine.SerializeField;
using UnityEngine;
using PolygonArcana.Services;
using Zenject;
using PolygonArcana.Models;
using System.Collections.Generic;
using System;

namespace PolygonArcana
{
	public class DebugJoystick : MonoBehaviour
	{
		[Inject] GamestateService gamestate;
		[Inject] MainModel mainModel;

		private struct Pair
		{
			public KeyCode Key;
			public Action Action;
		}

		private List<(KeyCode, Action)> bindings;

		private void Awake()
		{
			bindings = new()
			{
				(KeyCode.KeypadPlus, NextGamestate)
			};

			mainModel.Gamestate.OnChanged += () =>
			{
				Debug.Log(mainModel.Gamestate.Value);
			};
		}

		private void Update()
		{
			foreach (var (key, action) in bindings)
			{
				if (Input.GetKeyDown(key))
				{
					action();
				}
			}
		}

		private void NextGamestate()
		{
			gamestate.DEBUG_NextState();
		}
	}
}
