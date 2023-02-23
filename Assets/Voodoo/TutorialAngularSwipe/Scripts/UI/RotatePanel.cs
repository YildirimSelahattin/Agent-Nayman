using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voodoo.Pattern
{
	public class RotatePanel : MonoBehaviour
	{
		public bool goRight;

		public float angleLeft;
		public float angleRight;
		public float timeTranslation;

		private float timer;
		private float tmpDeltaTime;

		// Start is called before the first frame update
		void Start()
		{
			timer = Time.time;
		}

		// Update is called once per frame
		void Update()
		{
			tmpDeltaTime = (Time.time - timer) / timeTranslation;

			if (goRight)
			{
				transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, angleLeft), Quaternion.Euler(0, 0, angleRight),
					tmpDeltaTime);

				if (transform.rotation == Quaternion.Euler(0, 0, angleRight))
				{
					goRight = false;
					timer = Time.time;
				}
			}
			else
			{
				transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, angleRight), Quaternion.Euler(0, 0, angleLeft),
					tmpDeltaTime);

				if (transform.rotation == Quaternion.Euler(0, 0, angleLeft))
				{
					goRight = true;
					timer = Time.time;
				}
			}
		}
	}
}

