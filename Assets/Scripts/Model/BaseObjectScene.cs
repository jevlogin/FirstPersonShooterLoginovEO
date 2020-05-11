using UnityEngine;


namespace JevLogin
{
    public abstract class BaseObjectScene : MonoBehaviour
    {
        #region Fields

        [HideInInspector] public Rigidbody Rigidbody;
        [HideInInspector] public Transform Transform;

        private Color _color;

        private bool _isVisible;
        private int _layer;

        #endregion


        #region Properties

        /// <summary>
        /// Name object
        /// </summary>
        public string Name
        {
            get => gameObject.name;
            set => gameObject.name = value;
        }

        /// <summary>
        /// Layer object
        /// </summary>
        public int Layer
        {
            get => _layer;
            set
            {
                _layer = value;
                AskLayer(Transform, _layer);
            }
        }

        /// <summary>
        /// Color material this object
        /// </summary>
        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                AskColor(transform, _color);
            }
        }

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                RendererSetActive(transform);
                if (transform.childCount <= 0) return;

                foreach (Transform item in transform)
                {
                    RendererSetActive(item);
                }
            }
        }

        #endregion


        #region UnityMethods

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Transform = transform;
        }

        #endregion


        #region Methods

        private void RendererSetActive(Transform renderer)
        {
            if (renderer.gameObject.TryGetComponent<Renderer>(out var component))
            {
                component.enabled = _isVisible;
            }
        }

        private void AskLayer(Transform objectTransform, int layer)
        {
            objectTransform.gameObject.layer = layer;
            if (objectTransform.childCount <= 0)
            {
                return;
            }

            foreach (Transform child in objectTransform)
            {
                AskLayer(child, layer);
            }
        }

        private void AskColor(Transform objectTransform, Color color)
        {
            foreach (var currentMaterial in objectTransform.GetComponent<Renderer>().materials)
            {
                currentMaterial.color = color;
            }
            if (objectTransform.childCount <= 0)
            {
                return;
            }
            foreach (Transform item in objectTransform)
            {
                AskColor(item, color);
            }
        }

        /// <summary>
        /// Выключает физику у объекта и его дочерних объектов (потомков)
        /// </summary>
        public void DisableRigidBody()
        {
            var rigidBodies = GetComponentsInChildren<Rigidbody>();
            foreach (var rigidBody in rigidBodies)
            {
                rigidBody.isKinematic = true;
            }
        }

        /// <summary>
        /// Включает физику у объекта и его дочерних объектов (потомков)
        /// </summary>
        /// <param name="force"></param>
        public void EnableRigidBody(float force)
        {
            EnableRigidBody();
            Rigidbody.AddForce(transform.forward * force);
        }

        /// <summary>
        /// Включает физику у объекта и его дочерних объектов (потомков)
        /// </summary>
        private void EnableRigidBody()
        {
            var rigidBodies = GetComponentsInChildren<Rigidbody>();
            foreach (var rigidBody in rigidBodies)
            {
                rigidBody.isKinematic = false;
            }
        }

        /// <summary>
        /// Замораживает или размораживает физическую трансформацию объекта
        /// </summary>
        /// <param name="rigidbodyConstraints"> Трансформацию которую надо заморозить</param>
        public void ConstraintsRigidBody(RigidbodyConstraints rigidbodyConstraints)
        {
            var rigidBodies = GetComponentsInChildren<Rigidbody>();
            foreach (var rigidBody in rigidBodies)
            {
                rigidBody.constraints = rigidbodyConstraints;
            }
        }

        public void SetActive(bool value)
        {
            IsVisible = value;
            if (TryGetComponent<Collider>(out var component))
            {
                component.enabled = value;
            }
        }

        #endregion
    }
}
