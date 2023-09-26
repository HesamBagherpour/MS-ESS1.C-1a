namespace Runtime.Singleton
{
    public class Singleton<T> where T : Singleton<T>, new()
    {
        private static T _instance;
        public static T Instance => _instance ?? InitInstance();

        private static T InitInstance()
        {
            _instance = new T();
            _instance.Initialize();
            return _instance;
        }

        protected virtual void Initialize( )
        {

        }
    }
}