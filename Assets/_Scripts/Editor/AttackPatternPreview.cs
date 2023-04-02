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
		private Lazy<Sprite> pointSprite
			= ResourcesExtension.LoadLazy<Sprite>("Editor/triangle_directional");
		
		private Lazy<Sprite> targetSprite
			= ResourcesExtension.LoadLazy<Sprite>("Editor/triangle_filled");

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
			//> consts
			camera.nearClipPlane = 0.1f;
			camera.farClipPlane = 10f;
			camera.orthographic = true;

			var camTransform = camera.transform;
			camTransform.rotation = Quaternion.identity;
			camTransform.position = camTransform.forward * -5f;

			// //> get rect
			var defaultRect = new Rect(
				Vector2.one * -0.5f,
				Vector2.one
			);
			var sumRect = defaultRect.Encapsulate(includeRect);

			// //> set camera to encapsulate the rect
			camTransform.position += (Vector3)sumRect.center;

			//> the .orthographicSize property is actually
			//> a VERTICAL HALF size. these are the calculations
			//> that take this into account
			var maxDim = Mathf.Max(sumRect.width, sumRect.height);
			var vertSize = (camera.aspect > 1f) ? maxDim : (maxDim / camera.aspect);
			var halfSize = vertSize * 0.5f + margin * 0.5f;

			camera.orthographicSize = halfSize;
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
