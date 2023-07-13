using UnityEngine;

namespace UI.Menu
{
	[CreateAssetMenu(menuName = "ThemeSettings")]
	[System.Serializable]
	public class ThemedUIData : ScriptableObject {
		[System.Serializable]
		public class Custom1{
			[Header("Text")]	
			public Color graphic1;
			public Color32 text1;
		}

		[System.Serializable]
		public class Custom2{
			[Header("Text")]	
			public Color graphic2;
			public Color32 text2;
		}

		[Header("PRESETS")]
		public Custom1 custom1;
		public Custom2 custom2;
		

		[HideInInspector]
		public Color currentColor;
		[HideInInspector]
		public Color32 textColor;
	}
}