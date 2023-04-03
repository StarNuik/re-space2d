using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;
using PolygonArcana.Entities;
using Zenject;
using PolygonArcana.Models;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace PolygonArcana.Services
{
	public class BulletsBehaviourService : AMonoService<MainModel>
	{
		private void FixedUpdate()
		{
			foreach (var bullet in model.Bullets.Value)
			{
				bullet.Position += bullet.Direction * bullet.Speed * Time.deltaTime;
			}
		}

		public void OnCollision()
		{}
	}
}
