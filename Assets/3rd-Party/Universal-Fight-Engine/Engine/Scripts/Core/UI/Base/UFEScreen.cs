using UnityEngine;
using System.Collections.Generic;

namespace TOHDragonFight3D
{
    public class UFEScreen : MonoBehaviour
    {
        public bool canvasPreview = true;
        //public bool enableUFEInput = false;
        public GameObject firstSelectableGameObject = null;
        public bool hasFadeIn = true;
        public bool hasFadeOut = true;
        public bool wrapInput = true;

        #region protected definitions

        protected Camera mainCam
        {
            get
            {
                if (_mainCam == null) _mainCam = Camera.main;
                return _mainCam;
            }
        }
        Camera _mainCam;

        protected Camera playerCam
        {
            get
            {
                if (_playerCam == null) _playerCam = mainCam.transform.Find("Camera-Player").GetComponent<Camera>();
                return _playerCam;
            }
        }
        Camera _playerCam;

        protected float currentFieldOfView
        {
            get { return _currentFieldOfView; }
            set
            {
                _currentFieldOfView = value;
                mainCam.fieldOfView = _currentFieldOfView;
                playerCam.fieldOfView = _currentFieldOfView;
            }
        }
        float _currentFieldOfView;

        #endregion

        public virtual void DoFixedUpdate(
            IDictionary<InputReferences, InputEvents> player1PreviousInputs,
            IDictionary<InputReferences, InputEvents> player1CurrentInputs,
            IDictionary<InputReferences, InputEvents> player2PreviousInputs,
            IDictionary<InputReferences, InputEvents> player2CurrentInputs
        )
        { }

        public virtual bool IsVisible()
        {
            return this.gameObject.activeInHierarchy;
        }

        public virtual void OnHide() { }
        public virtual void OnShow()
        {
            //UFE.PauseGame(!enableUFEInput);
        }
        public virtual void SelectOption(int option, int player) { }
    }
}