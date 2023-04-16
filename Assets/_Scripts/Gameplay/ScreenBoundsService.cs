using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;
using Zenject;
using PolygonArcana.Settings;
using UnityEngine;
using PolygonArcana.Entities;
using System;

namespace PolygonArcana.Services
{
	public class ScreenBoundsService : AMonoService
	{	
		[Inject] GameSettings settings;
		[Inject] Camera mainCamera;

		[SF] bool GIZMOS_DrawGizmos;
		[SF] Camera GIZMOS_Camera;
		[SF] GameSettings GIZMOS_Settings;
		[SF] VisibilityInfoStatic GIZMOS_VisibilityEntity;

		public bool IsInside(IVisibilityInfo info)
		{
			var rect = mainCamera.OrthoSizeToRect(settings.ScreenBorderMargin);
			return rect.OverlapsCircleDumb(info.Position, info.BoundsRadius);
		}

		#region Gizmos
		private bool GIZMOS_IsInside() => GIZMOS_TargetRect().OverlapsCircleDumb(GIZMOS_VisibilityEntity.Position, GIZMOS_VisibilityEntity.BoundsRadius);
		
		private Rect GIZMOS_TargetRect() => GIZMOS_Camera.OrthoSizeToRect(GIZMOS_Settings.ScreenBorderMargin);

		private void OnDrawGizmos()
		{
			if (!GIZMOS_DrawGizmos) return;
			
			var isInside = GIZMOS_IsInside();

			Gizmos.color = isInside ? Color.green : Color.red;
			GizmosExt.DrawCircle(
				GIZMOS_VisibilityEntity.Position,
				GIZMOS_VisibilityEntity.BoundsRadius,
				Vector3.up
			);

			Gizmos.color = Color.green;
			GizmosExt.DrawRect(Vector3.zero, GIZMOS_TargetRect(), Vector3.up);
		}

		[Serializable]
		private struct VisibilityInfoStatic : IVisibilityInfo
		{
			[field: SerializeField]
			public Vector2 Position { get; private set; }

			[field: SerializeField]
			public float BoundsRadius { get; set; }

			public void Deconstruct(out Vector2 position, out float radius)
			{
				position = Position;
				radius = BoundsRadius;
			}
		}
		#endregion
	}
}
