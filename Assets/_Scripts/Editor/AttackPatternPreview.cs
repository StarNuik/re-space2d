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
			using (previewUtility.WithFrame(rect, background))
			{
				//
			}
		}
	}
}
