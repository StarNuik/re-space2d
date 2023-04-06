using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;
using PolygonArcana.Models;
using PolygonArcana.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonArcana.Services
{
	public class BulletsTickService : AMonoService<MainModel>
	{
		private List<Bullet> bullets => model.Bullets;

		private int _counter;
		private int counterAuto
		{
			get
			{
				var count = bullets.Count;
				var old = Mathf.Min(_counter, count - 1);

				_counter = (old + 1) % count;

				return old;
			}
		}

		private void FixedUpdate()
		{
			if (bullets.Count == 0) return;
			
			bullets[counterAuto].RareTick();
		}
	}
}
