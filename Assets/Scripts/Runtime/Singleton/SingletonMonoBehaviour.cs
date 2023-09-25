using UnityEngine;

namespace Runtime.Singleton
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        private static T _instance;
        public static T Instance => _instance ? _instance : InitInstance();

        private static T InitInstance()
        {
            CreateInstance();
            _instance.Initialize();
            return _instance;
        }

        public static void CreateInstance(GameObject prefab = null, bool ifExistRecreate = false)
        {
            if (!_instance)
                _instance = GameObject.FindObjectOfType<T>();

            if (!ifExistRecreate)
            {
                if (_instance)
                {
                    _instance.Initialize();
                    return;
                }
            }
            else if (_instance)
                GameObject.Destroy(_instance.gameObject);

            if (prefab)
            {
                _instance = Instantiate(prefab).GetComponent<T>();
                return;
            }

            _instance = new GameObject(typeof(T).Name).AddComponent<T>();
        }

        protected virtual void Initialize()
        {

        }

        protected virtual void Awake()
        {
            if (_instance && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this as T;
            _instance?.Initialize();
        }

        private void OnDestroy()
        {
            if (_instance == this)
                _instance = null;
        }
    }
}