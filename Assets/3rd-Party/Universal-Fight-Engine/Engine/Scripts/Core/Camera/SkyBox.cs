using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace TOHDragonFight3D
{
    public class SkyBox : MonoBehaviour
    {
        public Material skyMat;

        #region MonoBehaviour callbacks

        private void Awake()
        {
            ChangeSkyBox();
        }

        #endregion MonoBehaviour callbacks

        #region Context Public Methods

        [ContextMenu("ChangeSkyBox")]
        public void ChangeSkyBox()
        {
            RenderSettings.skybox = skyMat;
        }

        #endregion
    }
}
