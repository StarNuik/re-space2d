using System;
using PolygonArcana.Essentials;
using PolygonArcana.Settings;
using UnityEditor;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.Assertions;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana
{
	public class AttackPatternPreview : IDisposable
	{
		private bool isDisposed;
		private AttackPattern pattern;
		private PreviewRenderUtility previewUtility;

		public AttackPatternPreview(AttackPattern target)
		{
			Assert.IsNotNull(target);

			pattern = target;
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
				OnInput(e);
			else
				OnRepaint(rect, background);
		}

		private void OnInput(Event e)
		{}

		private void OnRepaint(Rect rect, GUIStyle background)
		{
			const string quadPath = "Quad.fbx";
			// const string materialPath = "Sprites-Default.mat";
			const string materialPath = "AttackPatternPoint.mat";
			const string spritePath = "Editor/triangle_directional";

			using (previewUtility.WithFrame(rect, background))
			{
				var camera = previewUtility.camera;
				camera.nearClipPlane = 0.1f;
				camera.farClipPlane = 100f;
				camera.orthographic = true;
				camera.orthographicSize = 5f;

				var camTransform = camera.transform;
				camTransform.rotation = Quaternion.identity;
				camTransform.position = camTransform.forward * -5f;


				previewUtility.DrawSprite(
					Resources.Load<Sprite>(spritePath),
					Vector2.zero,
					0f
				);

				previewUtility.Render();
			}
		}
	}
}
