using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PixelCrushers.DialogueSystem {

	public static class UITools {

		public static int GetAnimatorNameHash(AnimatorStateInfo animatorStateInfo) {
			#if UNITY_4_3 || UNITY_4_5 || UNITY_4_6
			return animatorStateInfo.nameHash;
			#else
			return animatorStateInfo.fullPathHash;
			#endif
		}

		/// <summary>
		/// Dialogue databases use Texture2D for actor portraits. Unity UI uses sprites.
		/// UnityUIDialogueUI converts textures to sprites. This dictionary contains
		/// converted sprites so we don't need to reconvert them every single time we
		/// want to show an actor's portrait.
		/// </summary>
		public static Dictionary<Texture2D, Sprite> spriteCache = new Dictionary<Texture2D, Sprite>();

		public static void ClearSpriteCache() {
			spriteCache.Clear();
		}

		public static Sprite CreateSprite(Texture2D texture) {
			if (texture == null) return null;
			if (spriteCache.ContainsKey(texture)) return spriteCache[texture];
			var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
			spriteCache.Add(texture, sprite);
			return sprite;
			//---Was:
			//return (texture != null) 
			//	? Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero)
			//		: null;
		}

		public static string GetUIFormattedText(FormattedText formattedText) {
			if (formattedText == null) {
				return string.Empty;
			} else if (formattedText.italic) {
				return "<i>" + formattedText.text + "</i>";
			} else {
				return formattedText.text;
			}
		}
		
	}

}
