using UnityEngine;

namespace Game.Framework.Pool
{
    /// <summary>
    /// Implements a pool for component types.
    /// </summary>
    /// <typeparam name="T">Specifies the component type to pool.</typeparam>
    public abstract class ComponentPoolSO<T> : PoolSO<T> where T : Component
    {
        private Transform _poolRoot;
        private Transform PoolRoot
        {
            get
            {
                if (_poolRoot == null)
                {
                    _poolRoot = new GameObject(name).transform;
                    _poolRoot.SetParent(_parent);
                }
                return _poolRoot;
            }
        }

        private Transform _parent;

        /// <summary>
        /// Set the parent of the pool to transform <param name="t">.
        /// </summary>
        /// <param name="t">The tranform of which pool parents to.</param>
        /// <remarks>NOTE: Setting the parent to an object marked DontDestroyOnLoad will effectively make this pool DontDestroyOnLoad.<br/>
		/// This can only be circumvented by manually destroying the object or its parent or by setting the parent to an object not marked DontDestroyOnLoad.</remarks>
        public void SetParent(Transform t)
        {
            _parent = t;
            PoolRoot.SetParent(_parent);
        }

        public override T Request()
        {
            T member = base.Request();
            member.gameObject.SetActive(true);
            return member;
        }

        public override void Return(T member)
        {
            member.transform.SetParent(PoolRoot, false);
            member.gameObject.SetActive(false);
            base.Return(member);
        }

        protected override T Create()
        {
            T member = base.Create();
            member.transform.SetParent(PoolRoot, false);
            member.gameObject.SetActive(false);
            return member;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            if (_poolRoot != null)
            {
#if UNITY_EDITOR
                DestroyImmediate(_poolRoot.gameObject);
#else
				Destroy(_poolRoot.gameObject);
#endif
            }
        }
    }
}
