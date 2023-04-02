using System;
using System.Collections.Generic;
using System.Linq;
using PolygonArcana.Essentials;
using PolygonArcana.Settings;
using UnityEditor;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.Assertions;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana
{
	using PointsCollection = IEnumerable<Ray2D>;

	public class AttackPatternPreview : IDisposable
	{
		const float previewMargin = 0.25f;

		private Lazy<Sprite> pointSprite
			= ResourcesExtension.LoadLazy<Sprite>("Editor/triangle_directional_2");
		
		private Lazy<Sprite> targetSprite
			= ResourcesExtension.LoadLazy<Sprite>("Editor/triangle_filled_2");

		private bool isDisposed;
		private AttackPattern targetPattern;
		private PreviewRenderUtility previewUtility;


		public AttackPatternPreview(AttackPattern target)
		{
			Assert.IsNotNull(target);

			targetPattern = target;
			previewUtility = new();

			isDisposed = false;
		}

		~AttackPatternPreview()
			=> Dispose();
		
		public void Dispose()
		{
			if (isDisposed) return;

			previewUtility.Cleanup();
			isDisposed = true;
		}

		public void OnPreviewGUI(Rect rect, GUIStyle background)
		{
			var e = Event.current;

			if (!ShaderUtil.hardwareSupportsRectRenderTexture)
			{
				return;
			}

			if (e.type != EventType.Repaint)
			{
				OnInput(e);
				return;
			}

			var points = targetPattern.Points;
			if (points.Count() == 0) return;

			OnRepaint(rect, background, points);
		}

		private void OnInput(Event e)
		{}

		private void OnRepaint(Rect rect, GUIStyle background, PointsCollection points)
		{
			using (previewUtility.WithFrame(rect, background))
			{
				SetupCamera(
					previewUtility.camera,
					RectOfPoints(points),
					0.25f
				);

				previewUtility.DrawSprite(
					targetSprite.Value,
					Vector2.zero,
					90f
				);

				foreach (var (position, direction) in points)
				{
					var rotation = Vector2.SignedAngle(Vector2.right, direction);

					previewUtility.DrawSprite(
						pointSprite.Value,
						position,
						rotation,
						0.5f
					);
				}

				previewUtility.Render();
			}
		}

		private void SetupCamera(Camera camera, Rect includeRect, float margin = 0f)
		{
			var bounds = new Bounds(Vector3.zero, Vector3.one * 10f);

			var defaultRect = new Rect(Vector2.one * -0.5f, Vector2.one);
			var sumRect = defaultRect.Encapsulate(includeRect);

			camera.WithOrthoBounds(bounds);
			Debug.Log(includeRect);
			camera.OrthoFitToRect(sumRect.WithMargin(previewMargin));
		}

		private Rect RectOfPoints(PointsCollection points)
		{
			var first = points.First();

			var result = new Rect(
				first.origin,
				Vector2.zero
			);

			foreach (var (position, _) in points)
			{
				result = result.Encapsulate(position);
			}

			return result;
		}
	}
}
