using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Voodoo.Visual.UI
{
    public class TapLeftRight : MonoBehaviour
    {
        public bool isAutomatic;

        public CanvasGroup leftImage;
        public CanvasGroup rightImage;

        public float delayAppear = 0.5f;

        [Range(0f,1f)]
        public float appearAlpha = 0.5f;


        private bool appearRight = true;
        private float time;
        
        void Start()
        {
            leftImage.alpha = 0;
            rightImage.alpha = 0;
            time = 0;
        }

        private void Update()
        {
            if (isAutomatic)
            {
                time += Time.deltaTime/delayAppear;
                
                if (rightImage.alpha >= appearAlpha ||leftImage.alpha >= appearAlpha || time>1)
                {
                    return;
                }
                
                if (appearRight)
                {
                    rightImage.alpha = Mathf.Lerp(0, appearAlpha, time);
                }
                else
                {
                    leftImage.alpha = Mathf.Lerp(0, appearAlpha, time);
                }
            }
            else
            {
                time += Time.deltaTime/delayAppear;       

                if (appearRight)
                {
                    if (rightImage.alpha >= appearAlpha)
                    {
                        rightImage.alpha = appearAlpha;
                        leftImage.alpha = 0;
                        appearRight = false;
                        time = 0;
                    }
                
                    rightImage.alpha = Mathf.Lerp(0, appearAlpha, time);

                }
                else
                {
                    if (leftImage.alpha >= appearAlpha)
                    {
                        leftImage.alpha = appearAlpha;
                        rightImage.alpha = 0;
                        appearRight = true;
                        time = 0;
                    }
                
                    leftImage.alpha = Mathf.Lerp(0, appearAlpha, time);

                }
            } 

        }

        public void AppearLeft()
        {
            if (isAutomatic == false)
            {
                return;
            }
            if (leftImage.alpha > 0)
            {
                return;
            }
            
            appearRight = false;
            time = 0;
            rightImage.alpha = 0;
        }

        public void AppearRight()
        {
            if (isAutomatic == false)
            {
                return;
            }

            if (rightImage.alpha > 0)
            {
                return;
            }
            
            appearRight = true;
            time = 0;
            leftImage.alpha = 0;
        }
    }
}