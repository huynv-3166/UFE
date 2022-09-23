using UnityEngine;
using System.Collections;

namespace TOHDragonFight3D
{
    public class CameraScript : MonoBehaviour
    {
        #region trackable definitions
        public bool cinematicFreeze;
        public Vector3 currentLookAtPosition;
        public Vector3 defaultCameraPosition;
        public float defaultDistance;
        public float freeCameraSpeed;
        public GameObject currentOwner;
        public bool killCamMove;
        public float movementSpeed;
        public float rotationSpeed;
        public float standardGroundHeight;
        public Vector3 targetPosition;
        public Quaternion targetRotation;
        public float targetFieldOfView;
        //public float playerDistance;
        //public Vector3 player1Position;
        //public Vector3 player2Position;
        #endregion

        public GameObject playerLight;
        public ControlsScript player1;
        public ControlsScript player2;
        
        public Transform dollyTransform;

        #region private definitions
        Camera mainCam;
        Camera playerCam;
        GrayScale grayScale;
        #endregion

        #region properties
        float currentFieldOfView
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

        void Start()
        {
            playerLight = GameObject.Find("Player Light");

            mainCam = Camera.main;
            playerCam = mainCam.transform.Find("Camera-Player").GetComponent<Camera>();
            currentFieldOfView = mainCam.fieldOfView;

           if (UFE.config.cameraOptions.calculateInitialDistance)
                ReCalculateInitialDistance();

            ResetCam();
            defaultDistance = Vector3.Distance(player1.transform.position, player2.transform.position);
            defaultCameraPosition = mainCam.transform.position;
            movementSpeed = UFE.config.cameraOptions.movementSpeed;
            rotationSpeed = UFE.config.cameraOptions.rotationSpeed;
            UFE.freeCamera = false;

            if (UFE.config.gameplayType != GameplayType._2DFighter)
            {
                GameObject dolly = new GameObject("Camera Dolly");
                dollyTransform = dolly.transform;
                dollyTransform.SetParent(UFE.gameEngine.transform);
            }

            grayScale = GrayScale.Instance;
        }

        void ReCalculateInitialDistance()
        {
            if (player1 == null || player2 == null) return;
            float maxHeight = player1.myInfo.height > player2.myInfo.height ?
                player1.myInfo.height : player2.myInfo.height;
            float minHeight = player1.myInfo.height < player2.myInfo.height ?
                player1.myInfo.height : player2.myInfo.height;

            UFE.config.cameraOptions.initialDistance.y = maxHeight;
            UFE.config.cameraOptions.initialRotation.y = maxHeight;

            UFE.config.cameraOptions.rotationOffSet.y = minHeight / 2;

            UFE.config.cameraOptions.initialDistance.z = -UFE.config.cameraOptions.minZoom - maxHeight;

            //    (maxHeight * 2f) / Mathf.Tan(Mathf.Deg2Rad * (UFE.config.cameraOptions.initialFieldOfView / 2f));
            //UFE.config.cameraOptions.minZoom = UFE.config.cameraOptions.initialDistance.z;
            //UFE.config.cameraOptions.maxZoom = 12 * UFE.config.cameraOptions.minZoom;
            //UFE.config.cameraOptions.rotationOffSet.y = (player1.myInfo.height + player2.myInfo.height) / 2f - 0.8f;
        }

        public void ResetCam()
        {
            mainCam.transform.position = UFE.config.cameraOptions.initialDistance;
            mainCam.transform.localRotation = Quaternion.Euler(UFE.config.cameraOptions.initialRotation);

            playerCam.transform.localPosition = Vector3.zero;
            playerCam.transform.localRotation = Quaternion.identity;

            currentFieldOfView = UFE.config.cameraOptions.initialFieldOfView;
        }

        public Vector3 LerpByDistance(Vector3 A, Vector3 B, float speed)
        {
            Vector3 P = speed * (float)UFE.fixedDeltaTime * Vector3.Normalize(B - A) + A;
            return P;
        }

        public void DoFixedUpdate()
        {
            if (killCamMove) return;
            if (player1 == null || player2 == null) return;

            //playerDistance = PlayerDistance();
            //player1Position = player1.transform.position;
            //player2Position = player2.transform.position;

            if (UFE.freeCamera)
            {
                if (UFE.config.gameplayType != GameplayType._2DFighter)
                {
                    Update3DPosition();
                }

                currentFieldOfView = Mathf.Lerp(currentFieldOfView, targetFieldOfView, (float)UFE.fixedDeltaTime * freeCameraSpeed * 1.8f);
                mainCam.transform.localPosition = Vector3.Lerp(mainCam.transform.localPosition, targetPosition, (float)UFE.fixedDeltaTime * freeCameraSpeed * 1.8f);
                mainCam.transform.localRotation = Quaternion.Slerp(mainCam.transform.localRotation, targetRotation, (float)UFE.fixedDeltaTime * freeCameraSpeed * 1.8f);
            }
            else
            {
                if (UFE.config.gameplayType == GameplayType._2DFighter)
                {
                    Vector3 newPosition = ((player1.transform.position + player2.transform.position) / 2) + UFE.config.cameraOptions.initialDistance;
                    float highestPos = player1.transform.position.y > player2.transform.position.y ? player1.transform.position.y : player2.transform.position.y;
                    if (highestPos >= UFE.config.cameraOptions.verticalThreshold)
                    {
                        if (UFE.config.cameraOptions.verticalPriority == VerticalPriority.AverageDistance)
                        {
                            newPosition.y += Mathf.Abs(player1.transform.position.y - player2.transform.position.y) / 2;
                        }
                        else if (UFE.config.cameraOptions.verticalPriority == VerticalPriority.HighestCharacter)
                        {
                            newPosition.y += highestPos;
                        }
                    }
                    else
                    {
                        newPosition.y = UFE.config.cameraOptions.initialDistance.y;
                    }


                    newPosition.x = Mathf.Clamp(newPosition.x,
                        (float)(UFE.config.selectedStage.position.x + UFE.config.selectedStage._leftBoundary + 8),
                        (float)(UFE.config.selectedStage.position.x + UFE.config.selectedStage._rightBoundary - 8));

                    // Zoom
                    if (UFE.config.cameraOptions.enableZoom)
                    {
                        newPosition.z = UFE.config.cameraOptions.initialDistance.z - Vector3.Distance(player1.transform.position, player2.transform.position) + defaultDistance;
                        newPosition.z = Mathf.Clamp(newPosition.z, -UFE.config.cameraOptions.maxZoom, -UFE.config.cameraOptions.minZoom);
                    }

                    currentFieldOfView = Mathf.Lerp(currentFieldOfView, UFE.config.cameraOptions.initialFieldOfView, (float)UFE.fixedDeltaTime * movementSpeed);
                    mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, newPosition, (float)UFE.fixedDeltaTime * movementSpeed);
                    mainCam.transform.localRotation = Quaternion.Slerp(mainCam.transform.localRotation, Quaternion.Euler(UFE.config.cameraOptions.initialRotation), (float)UFE.fixedDeltaTime * UFE.config.cameraOptions.movementSpeed);

                    if (mainCam.transform.localRotation == Quaternion.Euler(UFE.config.cameraOptions.initialRotation))
                        UFE.normalizedCam = true;
                }
                else
                {
                    if ((!UFE.config.characterRotationOptions.allowAirBorneSideSwitch || (player1.Physics.IsGrounded() && player2.Physics.IsGrounded())) &&
                        (player1.currentMove == null || !player1.currentMove.allowSideSwitch) &&
                        (player2.currentMove == null || !player2.currentMove.allowSideSwitch))
                        Update3DPosition();
                    currentFieldOfView = Mathf.Lerp(currentFieldOfView, UFE.config.cameraOptions.initialFieldOfView, (float)UFE.fixedDeltaTime * movementSpeed);
                }

                if (UFE.config.cameraOptions.enableLookAt)
                {
                    Vector3 newLookAtPosition = ((player1.transform.position + player2.transform.position) / 2) + UFE.config.cameraOptions.rotationOffSet;

                    if (UFE.config.cameraOptions.motionSensor != MotionSensor.None)
                    {
                        Vector3 acceleration = Input.acceleration;
                        if (UFE.config.cameraOptions.motionSensor == MotionSensor.Gyroscope && SystemInfo.supportsGyroscope) acceleration = Input.gyro.gravity;

#if UNITY_STANDALONE || UNITY_EDITOR
                        if (Input.mousePresent)
                        {
                            Vector3 mouseXY = new Vector3(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2, 0);
                            acceleration = mouseXY / 1000;
                        }
#endif
                        acceleration *= UFE.config.cameraOptions.motionSensibility;
                        newLookAtPosition -= acceleration;

                        Vector3 newPosition = mainCam.transform.position;
                        newPosition.y += acceleration.y;
                        mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, newPosition, (float)UFE.fixedDeltaTime * movementSpeed);
                    }


                    currentLookAtPosition = Vector3.Lerp(currentLookAtPosition, newLookAtPosition, (float)UFE.fixedDeltaTime * rotationSpeed);
                    mainCam.transform.LookAt(currentLookAtPosition, Vector3.up);
                }

                if (playerLight != null) playerLight.GetComponent<Light>().enabled = false;
            }

            //grayScale
        }

        public void Update3DPosition()
        {
            if (dollyTransform == null) return;
            // Update dolly transform to center around the 2 characters
            Vector3 dollyPos = Vector3.zero;

            Vector3 leftPlayerPos = player1.transform.position;
            Vector3 rightPlayerPos = player2.transform.position;

            if (player1.mirror > 0)
            {
                leftPlayerPos = player2.transform.position;
                rightPlayerPos = player1.transform.position;
            }

            dollyPos = ((leftPlayerPos + rightPlayerPos) / 2) + new Vector3(0, UFE.config.cameraOptions.height3d, 0);

            dollyTransform.position = dollyPos + dollyTransform.forward;
            dollyTransform.LookAt(dollyTransform.position);


            dollyTransform.LookAt(rightPlayerPos);

#if !UFE_LITE && !UFE_BASIC
            Vector3 distance = Vector3.zero;
            float zoomValue = 0;

            if (UFE.config.gameplayType == GameplayType._3DFighter)
            {
                // Zoom
                if (UFE.config.cameraOptions.enableZoom)
                {
                    zoomValue = UFE.config.cameraOptions.distance3d + Vector3.Distance(leftPlayerPos, rightPlayerPos) - defaultDistance;
                    if (currentOwner != null)
                        zoomValue = UFE.config.cameraOptions.distance3d - defaultDistance;
                    distance.x = Mathf.Clamp(zoomValue, UFE.config.cameraOptions.minZoom, UFE.config.cameraOptions.maxZoom);
                }

                // Update camera position to be perpendicular to the center dolly
                if (currentOwner != null)
                    dollyPos.x = currentOwner.transform.position.x;
                Vector3 newPosition = (Quaternion.Euler(0, dollyTransform.rotation.eulerAngles.y - UFE.config.cameraOptions.dollyAngle, 0) * distance) + dollyPos;
                mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, newPosition, (float)UFE.fixedDeltaTime * movementSpeed);
                mainCam.transform.LookAt(dollyTransform);
                if (currentOwner != null)
                    mainCam.transform.LookAt(currentOwner.transform.position + new Vector3(0, UFE.config.cameraOptions.height3d, 0));
            }
            /*else if (UFE.config.gameplayType == GameplayType._3DArena)
            {
                Vector3 newPosition = UFE.config.cameraOptions.initialDistance;
                if (UFE.config.cameraOptions.followCharacters)
                    newPosition += dollyTransform.position;

                // Zoom
                if (UFE.config.cameraOptions.enableZoom)
                {
                    zoomValue = Vector3.Distance(leftPlayerPos, rightPlayerPos) - defaultDistance;
                    zoomValue = Mathf.Clamp(zoomValue, UFE.config.cameraOptions.minZoom, UFE.config.cameraOptions.maxZoom);
                    newPosition = Vector3.MoveTowards(newPosition, dollyPos, -zoomValue);
                }

                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, newPosition, (float)UFE.fixedDeltaTime * movementSpeed);
                Camera.main.transform.localRotation = Quaternion.Slerp(Camera.main.transform.localRotation, Quaternion.Euler(UFE.config.cameraOptions.initialRotation), (float)UFE.fixedDeltaTime * UFE.config.cameraOptions.movementSpeed);
            }*/
#endif
        }

        public void MoveCameraToLocation(Vector3 targetPos, Vector3 targetRot, float targetFOV, float speed, GameObject owner)
        {
#if !UFE_LITE && !UFE_BASIC
            if (UFE.config.gameplayType == GameplayType._3DFighter)
                mainCam.transform.position = new Vector3(mainCam.transform.position.x, UFE.config.cameraOptions.height3d, -UFE.config.cameraOptions.distance3d);
#endif

            targetFieldOfView = targetFOV;
            targetPosition = targetPos;
            targetRotation = Quaternion.Euler(targetRot);
            freeCameraSpeed = speed;
            UFE.freeCamera = true;
            UFE.normalizedCam = false;
            currentOwner = owner;
            if (playerLight != null) playerLight.GetComponent<Light>().enabled = true;
        }

        public float PlayerDistance()
        {
            return (player1 != null && player2 != null) ?
                Vector3.Distance(player1.transform.position, player2.transform.position) : -1f;
        }

        public void DisableCam()
        {
            mainCam.enabled = false;
        }

        public void ReleaseCam()
        {
            mainCam.enabled = true;
            cinematicFreeze = false;
            UFE.freeCamera = false;
            currentOwner = null;
        }

        public void OverrideSpeed(float newMovement, float newRotation)
        {
            movementSpeed = newMovement;
            rotationSpeed = newRotation;
        }

        public void RestoreSpeed()
        {
            movementSpeed = UFE.config.cameraOptions.movementSpeed;
            rotationSpeed = UFE.config.cameraOptions.rotationSpeed;
        }

        public void SetCameraOwner(GameObject owner)
        {
            currentOwner = owner;
        }

        public GameObject GetCameraOwner()
        {
            return currentOwner;
        }

        public Vector3 GetRelativePosition(Transform origin, Vector3 position)
        {
            Vector3 distance = position - origin.position;
            Vector3 relativePosition = Vector3.zero;
            relativePosition.x = Vector3.Dot(distance, origin.right.normalized);
            relativePosition.y = Vector3.Dot(distance, origin.up.normalized);
            relativePosition.z = Vector3.Dot(distance, origin.forward.normalized);

            return relativePosition;
        }

        void OnDrawGizmos()
        {
            Vector3 cameraLeftBounds = mainCam.ViewportToWorldPoint(new Vector3(0, 0, -mainCam.transform.position.z));
            Vector3 cameraRightBounds = mainCam.ViewportToWorldPoint(new Vector3(1, 0, -mainCam.transform.position.z));

            cameraLeftBounds.x = mainCam.transform.position.x - ((float)UFE.config.cameraOptions._maxDistance / 2);
            cameraRightBounds.x = mainCam.transform.position.x + ((float)UFE.config.cameraOptions._maxDistance / 2);

            Gizmos.DrawLine(cameraLeftBounds, cameraLeftBounds + new Vector3(0, 15, 0));
            Gizmos.DrawLine(cameraRightBounds, cameraRightBounds + new Vector3(0, 15, 0));


            // 3D Position
            // Update CenterTransform
            /*if (UFE.config.gameplayType != GameplayType._2DFighter)
            {
                Vector3 newPos = ((player1.transform.position + player2.transform.position) / 2) + (new Vector3(0, UFE.config.cameraOptions.height3d, 0));
                Vector3 distance = new Vector3(UFE.config.cameraOptions.distance3d, 0, 0);
                Vector3 target = (Quaternion.Euler(0, dollyTransform.rotation.eulerAngles.y, 0) * distance) + newPos;
                Gizmos.color = Color.gray;
                Gizmos.DrawWireSphere(target, .3f);
            }*/
        }
    }
}