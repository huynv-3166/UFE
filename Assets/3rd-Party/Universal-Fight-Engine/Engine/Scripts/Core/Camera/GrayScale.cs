using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace TOHDragonFight3D
{
    public class GrayScale : MonoBehaviour
    {
        public static GrayScale Instance;
        [SerializeField] PostProcessVolume grayscale;

        ControlsScript cScript1;
        ControlsScript cScript2;

        private void Start()
        {
            cScript1 = UFE.GetControlsScript(1);
            cScript2 = UFE.GetControlsScript(2);
        }

        private void FixedUpdate()
        {
            //Check if any one is playing skill
            if (cScript1 != null && cScript2 != null)
            {
                grayscale.weight = (cScript1.IsReadingSkill || cScript2.IsReadingSkill) ? 1 : 0;
            }
        }
    }
}
