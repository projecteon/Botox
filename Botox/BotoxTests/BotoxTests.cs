namespace BotoxTests
{
    using System;

    using Botox;

    using Xunit;

    public class BotoxTests
    {
        public BotoxTests()
        {
            Botox.ClearAll();
        }

        [Fact]
        public void GivenTypeIsNotRegistrered_WhenResolveIsCalledForThatType_ThenExceptionIsThrown()
        {
            Assert.Throws<NotSupportedException>(() => Botox.Resolve<IFakeClass>());
        }

        [Fact]
        public void GivenTypeIsRegistrered_WhenResolveIsCalledForThatType_ThenObjectOfThatTypeIsReturned()
        {
            Botox.Registrer<IFakeClass>(new FakeClass());

            var fakeClass = Botox.Resolve<IFakeClass>();

            Assert.IsType<FakeClass>(fakeClass);
        }

        [Fact]
        public void GivenTypeIsRegistreredGeneric_WhenResolveIsCalledForThatType_ThenObjectOfThatTypeIsReturned()
        {
            Botox.Registrer<IFakeClass, FakeClass>(new FakeClass());

            var fakeClass = Botox.Resolve<IFakeClass>();

            Assert.IsType<FakeClass>(fakeClass);
        }

        [Fact]
        public void GivenTypeIsRegistrered_WhenGenericCreateInstanceOnClassWithInjectionForThatType_ThenObjectIsCreatedWithInjection()
        {
            Botox.Registrer<IFakeClass>(new FakeClass());

            var injectedObject = Botox.CreateInstanceOf<FakeClassInjection>();

            Assert.IsType<FakeClass>(injectedObject.FakeClass);
        }

        [Fact]
        public void GivenTypeHasOnlyDefaultConstructor_WhenGenericCreateInstanceIsCalledOnThatType_ThenExceptionIsThrown()
        {
            Assert.Throws<NotSupportedException>(() => Botox.CreateInstanceOf<FakeClass>());
        }
        
        [Fact]
        public void GivenClassHasConstructorWithNoneRegistreredType_WhenGenericCreateInstanceOnClassWithInjectionForThatType_ThenObjectIsCreatedWithConstructorThatHasValidResolves()
        {
            Botox.Registrer<IFakeClass>(new FakeClass());

            var injectedObject = Botox.CreateInstanceOf<FakeClassDoubleInjection>();

            Assert.IsType<FakeClass>(injectedObject.FakeClass2);
        }

        [Fact]
        public void GivenTypeIsRegistrered_WhenCreateInstanceOnClassWithInjectionForThatType_ThenObjectIsCreatedWithInjection()
        {
            Botox.Registrer<IFakeClass>(new FakeClass());

            var injectedObject = Botox.CreateInstanceOf<IFakeClassInjection>(typeof(FakeClassInjection));

            Assert.IsType<FakeClass>(injectedObject.FakeClass);
        }

        [Fact]
        public void GivenTypeIsNotAssignableFromGeneric_WhenCreateInstanceOnClassWithInjectionForThatType_ThenExceptionIsThrown()
        {
            Assert.Throws<InvalidCastException>(() => Botox.CreateInstanceOf<IFakeClass>(typeof(FakeClassInjection)));
        }

        [Fact]
        public void GivenTypeHasOnlyDefaultConstructor_WhenCreateInstanceIsCalledOnThatType_ThenExceptionIsThrown()
        {
            Assert.Throws<NotSupportedException>(() => Botox.CreateInstanceOf<IFakeClass>(typeof(FakeClass)));
        }
        
        [Fact]
        public void GivenClassHasConstructorWithNoneRegistreredType_WhenCreateInstanceOnClassWithInjectionForThatType_ThenObjectIsCreatedWithConstructorThatHasValidResolves()
        {
            Botox.Registrer<IFakeClass>(new FakeClass());

            var injectedObject = Botox.CreateInstanceOf<IFakeClassDoubleInjection>(typeof(FakeClassDoubleInjection));

            Assert.IsType<FakeClass>(injectedObject.FakeClass2);
        }

        private class FakeClassInjection : IFakeClassInjection
        {
            public IFakeClass FakeClass { get; private set; }

            public FakeClassInjection(IFakeClass fakeClass)
            {
                this.FakeClass = fakeClass;
            }
        }

        private class FakeClassDoubleInjection : IFakeClassDoubleInjection
        {
            private IFakeClass FakeClass1 { get; set; }

            public IFakeClass FakeClass2 { get; private set; }

            public FakeClassDoubleInjection(IFakeClass fakeClass1, IFakeClass fakeClass2, INotImplemented notImplemented)
            {
                
            }

            public FakeClassDoubleInjection(IFakeClass fakeClass2, INotImplemented notImplemented)
            {
                
            }

            public FakeClassDoubleInjection(IFakeClass fakeClass1, IFakeClass fakeClass2)
            {
                this.FakeClass1 = fakeClass1;
                this.FakeClass2 = fakeClass2;
            }

            public FakeClassDoubleInjection(IFakeClass fakeClass)
            {
                this.FakeClass1 = fakeClass;
            }
        }

        private class FakeClass : IFakeClass
        {
        }
    }

    internal interface IFakeClassDoubleInjection
    {
         IFakeClass FakeClass2 { get; }
    }

    internal interface IFakeClassInjection
    {
        IFakeClass FakeClass { get; }
    }

    public interface IFakeClass
    {
    }

    public interface INotImplemented
    {}
}