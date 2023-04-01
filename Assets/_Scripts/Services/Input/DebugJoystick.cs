using SF = UnityEngine.SerializeField;
using UnityEngine;
using PolygonArcana.Services;
using Zenject;
using PolygonArcana.Models;
using System.Collections.Generic;
using System;
using TMPro;
using PolygonArcana.Essentials;

namespace PolygonArcana
{
	public class DebugJoystick : AMonoService
	{
		[SF] TMP_Text label;

		[Inject] GamestateService gamestate;
		[Inject] MainModel mainModel;

		private struct Pair
		{
			public KeyCode Key;
			public Action Action;
		}

		private List<(KeyCode key, Action action, string description)> bindings;

		private void Awake()
		{
			bindings = new()
			{
				(KeyCode.KeypadPlus, NextGamestate, "next gamestate")
			};

			if (label != null)
			{
				var text = "";
				foreach (var (key, _, desc) in bindings)
				{
					text += key.ToString() + " - " + desc + "\n";
				}
				label.text = text;
			}
		}

		private void Update()
		{
			foreach (var (key, action, _) in bindings)
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
