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
        public void GivenTypeIsRegistrered_WhenCreateInstanceOnClassWithInjectionForThatType_ThenObjectIsCreatedWithInjection()
        {
            Botox.Registrer<IFakeClass>(new FakeClass());

            var injectedObject = Botox.CreateInstanceOf<FakeClassInjection>();

            Assert.IsType<FakeClass>(injectedObject.fakeClass);
        }

        [Fact]
        public void GivenTypeHasOnlyDefaultConstructor_WhenCreateInstanceIsCalledOnThatType_ThenExceptionIsThrown()
        {
            Assert.Throws<NotSupportedException>(() => Botox.CreateInstanceOf<FakeClass>());
        }
        
        [Fact]
        public void GivenClassHasConstructorWithNoneRegistreredType_WhenCreateInstanceOnClassWithInjectionForThatType_ThenObjectIsCreatedWithConstructorThatHasValidResolves()
        {
            Botox.Registrer<IFakeClass>(new FakeClass());

            var injectedObject = Botox.CreateInstanceOf<FakeClassDoubleInjection>();

            Assert.IsType<FakeClass>(injectedObject.fakeClass2);
        }

        private class FakeClassInjection
        {
            public readonly IFakeClass fakeClass;

            public FakeClassInjection(IFakeClass fakeClass)
            {
                this.fakeClass = fakeClass;
            }
        }

        private class FakeClassDoubleInjection
        {
            private readonly IFakeClass fakeClass1;

            public readonly IFakeClass fakeClass2;

            public FakeClassDoubleInjection(IFakeClass fakeClass1, IFakeClass fakeClass2, INotImplemented notImplemented)
            {
                
            }

            public FakeClassDoubleInjection(IFakeClass fakeClass2, INotImplemented notImplemented)
            {
                
            }

            public FakeClassDoubleInjection(IFakeClass fakeClass1, IFakeClass fakeClass2)
            {
                this.fakeClass1 = fakeClass1;
                this.fakeClass2 = fakeClass2;
            }

            public FakeClassDoubleInjection(IFakeClass fakeClass)
            {
                this.fakeClass1 = fakeClass;
            }
        }

        private class FakeClass : IFakeClass
        {
        }
    }

    public interface IFakeClass
    {
    }

    public interface INotImplemented
    {}
}