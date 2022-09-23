using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.TOHSoft.DragonFight3D.Game
{
    public class Game : MonoBehaviour
    {
        #region Properties

        // Property use private member
        private static Camera _mainCamera;
        public static Camera MainCamera
        {
            get
            {
                if (_mainCamera == null) _mainCamera = Camera.main;
                return _mainCamera;
            }
            set { _mainCamera = value; }
        }

        #endregion
    }
}
