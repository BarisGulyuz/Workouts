namespace Workouts
{
    public class WorstContainer
    {
        static Dictionary<Type, Type> Dependencies = new Dictionary<Type, Type>();
        public WorstContainer()
        {
            Dependencies.Add(typeof(IA), typeof(A));
            Dependencies.Add(typeof(IC), typeof(D));
        }

        public T GetIstance<T>()
        {
            var constructorInfo = typeof(T).GetConstructors()[0];

            object[] parameters = null;

            if (constructorInfo.GetParameters().Length > 0)
            {
                parameters = new object[constructorInfo.GetParameters().Length];
                int posisition = 0;
                foreach (var parameter in constructorInfo.GetParameters().OrderBy(x => x.Position))
                {
                    Type parameterType = parameter.ParameterType;
                    Dependencies.TryGetValue(parameterType, out Type targetObj);

                    ArgumentNullException.ThrowIfNull(targetObj);

                    var getInstanceMethod = typeof(WorstContainer).GetMethod(nameof(GetIstance))
                                                                  .MakeGenericMethod(targetObj);

                    parameters[posisition] = getInstanceMethod.Invoke(this, null);
                }
            }
            return (T)Activator.CreateInstance(typeof(T), parameters);
        }
    }

    #region C

    public interface IC
    {
        void C();
    }
    public class C : IC
    {
        void IC.C()
        {
            Console.WriteLine($" {nameof(C)} --> IC interfaceinden implent alınan C methodu çalıştı");
        }
    }

    #endregion

    #region D

    public class D : IC
    {
        void IC.C()
        {
            Console.WriteLine($" {nameof(D)} -->  IC interfaceinden implent alınan C methodu çalıştı");
        }
    }

    #endregion

    #region A
    public interface IA
    {
        void A();
    }
    public class A : IA
    {
        private readonly IC _c;

        public A(IC c)
        {
            _c = c;
        }

        void IA.A()
        {
            Console.WriteLine("IA interfaceinden implent alınan A methodu çalıştı");
            Console.WriteLine("C methodu tetiklenmeye çalışıyor....");
            _c.C();
        }
    }

    #endregion

    #region B
    public class B
    {
        private readonly IA _a;

        public B(IA a)
        {
            _a = a;
        }

        public void AMethod() => _a.A();
    }

    #endregion


}
