using PolygonArcana.Settings;
using UnityEditor;
using UnityEngine;

namespace PolygonArcana
{
	using Layout = EditorGUILayout;

	[CustomEditor(typeof(AttackPattern))]
	public class AttackPatternEditor : Editor
	{
		private AttackPatternPreview preview;

		private void OnEnable()
		{
			preview = new(target as AttackPattern);
		}

		private void OnDisable()
		{
			preview.Dispose();
			preview = null;
		}

		public override void OnInspectorGUI()
		{
			Layout.LabelField("Hello world");
			base.OnInspectorGUI();
		}

		public override bool HasPreviewGUI()
			=> true;

		public override void OnPreviewGUI(Rect rect, GUIStyle background)
			=> preview.OnPreviewGUI(rect, background);
	}
}
