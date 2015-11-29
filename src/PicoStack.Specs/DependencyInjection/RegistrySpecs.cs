using NUnit.Framework;
using PicoStack.Core.DependencyInjection;

namespace PicoStack.Specs.DependencyInjection
{
    [TestFixture]
    public class RegistrySpecs
    {
        [Test]
        public void Resolving_concrete_type_resolves_all_dependencies()
        {
            var registry = new Registry();

            registry.Register<Interface1, Class1>();
            registry.Register<Interface2, Class2>();
            registry.Register<Interface3, Class3>();
            registry.Register<Interface4, Class4>();

            var instance = registry.Resolve<Interface1>();

            Assert.IsNotNull(instance);
            Assert.IsNotNull(instance.Class2);
            Assert.IsNotNull(instance.Class3);
            Assert.IsNotNull(instance.Class2.Class3);
            Assert.IsNotNull(instance.Class3.Class4);
        }

        private interface Interface1
        {
            Class2 Class2 { get; }
            Class3 Class3 { get; }
        }

        private interface Interface2
        {
        }

        private interface Interface3
        {
        } 

        private interface Interface4
        {
        } 

        private class Class1 : Interface1
        {
            public Class2 Class2 { get; }
            public Class3 Class3 { get; }

            // Default constructor should not be used, since there is another overload with
            // more parameters
            public Class1()
            {
                
            }

            public Class1(Class2 class2, Class3 class3)
            {
                Class2 = class2;
                Class3 = class3;
            }
        }

        private class Class2 : Interface2
        {
            public Class3 Class3 { get; }

            public Class2(Class3 class3)
            {
                Class3 = class3;
            }
        }

        private class Class3 : Interface3
        {
            public Class4 Class4 { get; }

            public Class3(Class4 class4)
            {
                Class4 = class4;
            }
        }

        private class Class4 : Interface4
        {
             
        }
    }
}