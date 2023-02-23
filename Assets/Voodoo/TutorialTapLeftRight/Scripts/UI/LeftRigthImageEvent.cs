using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Voodoo.Visual.UI
{

    public class LeftRigthImageEvent : MonoBehaviour
    {
        public TapLeftRight tapLeftRight;

        /// <summary>
        /// Trigger by animation
        /// </summary>
        public void AppearLeft()
        {
            tapLeftRight.AppearLeft();
        }

        /// <summary>
        /// Trigger by animation
        /// </summary>
        public void AppearRight()
        {
            tapLeftRight.AppearRight();
        }

    }

}