using UnityEngine;


namespace JevLogin
{
    public sealed class UnitMotor : IMotor
    {
        #region Fields

        private Transform _instance;
        private CharacterController _characterController;
        private Transform _head;
        private Vector2 _input;
        private Vector3 _moveVector;
        private Quaternion _cameraTargetRotation;
        private Quaternion _characterTargetRotation;

        private float _gravityForce;
        private float _jumpPower = 10;
        private float _speedMove = 10;

        public float XSensivity = 2f;
        public float YSensivity = 2f;
        public float MaximumX = 90f;
        public float MinimumX = -90f;
        public float SmoothTime = 5f;
        public bool Smooth;
        public bool ClampVerticalRotation = true;

        #endregion


        #region ClassLifeCycles

        public UnitMotor(CharacterController instance)
        {
            _instance = instance.transform;
            _characterController = instance;
            _head = Camera.main.transform;

            _characterTargetRotation = _instance.localRotation;
            _cameraTargetRotation = _head.localRotation;
        }

        #endregion


        #region Methods

        public void Move()
        {
            CharacterMove();
            GamingGravity();

            LookRotation(_instance, _head);
        }

        private void CharacterMove()
        {
            if (_characterController.isGrounded)
            {
                _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                Vector3 desiredMove = _instance.forward * _input.y + _instance.right * _input.x;
                _moveVector.x = desiredMove.x * _speedMove;
                _moveVector.z = desiredMove.z * _speedMove;
            }

            _moveVector.y = _gravityForce;
            _characterController.Move(_moveVector * Time.deltaTime);
        }

        private void GamingGravity()
        {
            if (!_characterController.isGrounded)
            {
                _gravityForce -= 30 * Time.deltaTime;
            }
            else
            {
                _gravityForce = -1;
            }

            if (Input.GetKeyDown(KeyCode.Space) && _characterController.isGrounded)
            {
                _gravityForce = _jumpPower;
            }
        }

        private void LookRotation(Transform charachter, Transform camera)
        {
            float yRot = Input.GetAxis("Mouse X") * XSensivity;
            float xRot = Input.GetAxis("Mouse Y") * YSensivity;

            _characterTargetRotation *= Quaternion.Euler(0f, yRot, 0f);
            _cameraTargetRotation *= Quaternion.Euler(-xRot, 0f, 0f);

            if (ClampVerticalRotation)
            {
                _cameraTargetRotation = ClampRotationArroundXAxis(_cameraTargetRotation);
            }

            if (Smooth)
            {
                charachter.localRotation = Quaternion.Slerp(charachter.localRotation, _characterTargetRotation, SmoothTime * Time.deltaTime);
                camera.localRotation = Quaternion.Slerp(camera.localRotation, _cameraTargetRotation, SmoothTime * Time.deltaTime);
            }
            else
            {
                charachter.localRotation = _characterTargetRotation;
                camera.localRotation = _cameraTargetRotation;
            }
        }

        private Quaternion ClampRotationArroundXAxis(Quaternion cameraTargetRotation)
        {
            cameraTargetRotation.x /= cameraTargetRotation.w;
            cameraTargetRotation.y /= cameraTargetRotation.w;
            cameraTargetRotation.z /= cameraTargetRotation.w;
            cameraTargetRotation.w /= 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(cameraTargetRotation.x);
            angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);
            cameraTargetRotation.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            return cameraTargetRotation;
        }

        #endregion
    }
}
