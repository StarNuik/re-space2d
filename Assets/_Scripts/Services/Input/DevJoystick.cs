using SF = UnityEngine.SerializeField;
using UnityEngine;
using PolygonArcana.Services;
using Zenject;
using PolygonArcana.Models;
using System.Collections.Generic;
using System;
using TMPro;
using PolygonArcana.Essentials;
using NaughtyAttributes;
using PolygonArcana.Settings;

namespace PolygonArcana._Test
{
	public class DevJoystick : AMonoService
	{
		[SF] TMP_Text label;
		[Layer]
		[SF] int bulletsLayer;

		[Inject] MainModel mainModel;

		[Inject] GamestateService gamestate;
		[Inject] BulletsLifetimeService bulletsLifetime;

		[Inject] PlayerSettings playerSettings;

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
				(KeyCode.KeypadPlus, NextGamestate, "next gamestate"),
				(KeyCode.KeypadEnter, SpawnBullet, "spawn a bullet"),
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

		private void SpawnBullet()
		{
			bulletsLifetime.Take(
				Vector2.zero,
				UnityEngine.Random.insideUnitCircle,
				playerSettings.BulletInfo
			);
		}
	}
}
