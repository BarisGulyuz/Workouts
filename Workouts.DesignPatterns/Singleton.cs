
namespace Workouts.DesignPatterns
{
    //services.AddSingleton<SingletonClass>();
    /// <summary>
    /// Workouts.Program.cs sınıfında kullanıldı/kullanılacak
    /// </summary>
    public sealed class SingletonClass : ISingletonClass
    {
        int istanceCount = 0;
        private SingletonClass()
        {
            istanceCount += 1;
        }

        private static SingletonClass _singletonObj;
        public static SingletonClass Instance => _singletonObj ?? (_singletonObj = new SingletonClass());

        public int Method1() => istanceCount;
    }
    public sealed class Singleton : ISingletonClass
    {
        private static volatile Singleton instance;
        private static object syncRoot = new object();

        private Singleton() { }

        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Singleton();
                    }
                }

                return instance;


            }
        }

        public int Method1() => 1;
    }
    public sealed class MySingleton
    {
        private static readonly Lazy<MySingleton> _mySingleton = new Lazy<MySingleton>(() => CreateInstance(), true);

        private MySingleton() { }

        public static MySingleton Instance => _mySingleton.Value;

        private static MySingleton CreateInstance()
        {
            return new MySingleton();
        }
    }
    public sealed class Singleton_
    {
        private static Singleton_ instance = new Singleton_();

        // Explicit static constructor to force the C#
        // compiler not to mark the type as beforefieldinit
        static Singleton_() { }

        private Singleton_() { }

        public static Singleton_ Instance => instance;
    }
    public interface ISingletonClass
    {
        int Method1();
    }

}


