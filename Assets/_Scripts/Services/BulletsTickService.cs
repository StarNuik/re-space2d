using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;
using PolygonArcana.Models;
using PolygonArcana.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonArcana.Services
{
	//> no lag - no optimization
	public class BulletsTickService : AMonoService<MainModel>
	{
		private List<Bullet> bullets => model.Bullets;

		// private int _counter;
		// private int counterAuto
		// {
		// 	get
		// 	{
		// 		var count = bullets.Count;
		// 		var old = Mathf.Min(_counter, count - 1);

		// 		_counter = old - 1;
		// 		if (_counter < 0)
		// 			_counter = bullets.Count - 1;

		// 		return old;
		// 	}
		// }

		private void FixedUpdate()
		{
			for (int i = bullets.Count - 1; i >= 0; i--)
			{
				bullets[i].RareTick();
			}
			// if (bullets.Count == 0) return;
			
			// var target = bullets[counterAuto];
			// target.RareTick();

			// #if UNITY_EDITOR
			// {
			// 	GIZMOS_TickPosition = target.transform.position;
			// }
			// #endif
		}

		// #if UNITY_EDITOR
		// private Vector3 GIZMOS_TickPosition;

		// private void OnDrawGizmos()
		// {
		// 	if (!Application.isPlaying) return;
			
		// 	if (bullets.Count == 0) return;
		// 	Gizmos.color = Color.green;
		// 	GizmosExt.DrawCircle(GIZMOS_TickPosition, 1f, Vector3.up);
		// }
		// #endif
	}
}
