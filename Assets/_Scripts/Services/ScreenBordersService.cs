using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;
using Zenject;
using PolygonArcana.Settings;
using UnityEngine;
using PolygonArcana.Entities;
using System;

namespace PolygonArcana.Services
{
	using SourceBlock = System.ValueTuple<Camera, GameSettings>;

	public class ScreenBordersService : AMonoService
	{	
		[Inject] GameSettings settings;
		[Inject] Camera mainCamera;

		[SF] Camera GIZMOS_Camera;
		[SF] GameSettings GIZMOS_Settings;
		[SF] VisibilityInfoStatic GIZMOS_VisibilityEntity;

		private SourceBlock runtimeSource => (mainCamera, settings);
		private SourceBlock gizmosSource => (GIZMOS_Camera, GIZMOS_Settings);

		private bool IsInside(SourceBlock block, IVisibilityInfo info)
		{
			return TargetRect(block).OverlapsCircleDumb(info.Position, info.BoundsRadius);
		}

		private Rect TargetRect(SourceBlock block) => block.Item1.OrthoSizeToRect(block.Item2.ScreenBorderMargin);

		private void OnDrawGizmos()
		{
			var isInside = IsInside(gizmosSource, GIZMOS_VisibilityEntity);

			Gizmos.color = isInside ? Color.green : Color.red;
			GizmosExt.DrawCircle(
				GIZMOS_VisibilityEntity.Position,
				GIZMOS_VisibilityEntity.BoundsRadius,
				Vector3.up
			);

			Gizmos.color = Color.green;
			GizmosExt.DrawRect(Vector3.zero, TargetRect(gizmosSource), Vector3.up);
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
	}
}
