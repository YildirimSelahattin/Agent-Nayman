using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voodoo.Visual.UI
{
	[RequireComponent(typeof(CanvasGroup))]
	public class TextTutorial : MonoBehaviour
	{
		public CanvasGroup canvasGroup;

		/// <summary>
		/// If the text should blink
		/// </summary>
		public bool isBlinking;

		/// <summary>
		/// The blink duration from visible state to invisible state.
		/// </summary>
		public float blinkTime;



		#region Blink

		private bool fadingIn;
		private float timer;
		private float tmpDeltaTime;

		#endregion



		// Update is called once per frame
		void Update()
		{
			if (isBlinking)
			{
				Blink();
			}
		}

		void Blink()
		{
			// Compute the proportional animation time between 0 and 1 for the Lerp below
			tmpDeltaTime = (Time.time - timer) / blinkTime;

			// Perform the proper color interpolation
			if (fadingIn)
			{
				// From invisible to visible
				canvasGroup.alpha = Mathf.Lerp(0f, 1f, tmpDeltaTime);
			}
			else
			{
				// From visible to invisible
				canvasGroup.alpha = Mathf.Lerp(1f, 0f, tmpDeltaTime);
			}

			if (tmpDeltaTime > 1)
			{
				// Reverse the fade when the Lerp is complete
				fadingIn = !fadingIn;
				timer = Time.time;
			}
		}


	}

}