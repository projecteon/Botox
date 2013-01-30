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

            Assert.NotNull(Botox.Resolve<IFakeClass>());
        }

        [Fact]
        public void GivenTypeIsRegistreredGeneric_WhenResolveIsCalledForThatType_ThenObjectOfThatTypeIsReturned()
        {
            Botox.Registrer<IFakeClass, FakeClass>(new FakeClass());

            Assert.NotNull(Botox.Resolve<IFakeClass>());
        }

        [Fact]
        public void GivenTypeIsRegistrered_WhenCreateInstanceOnClassWithInjectionForThatType_ThenObjectIsCreatedWithInjection()
        {
            Botox.Registrer<IFakeClass>(new FakeClass());

            var injectedObject = Botox.CreateInstanceOf<FakeClassInjection>();

            Assert.NotNull(injectedObject.fakeClass);
        }

        private class FakeClassInjection
        {
            public readonly IFakeClass fakeClass;

            public FakeClassInjection(IFakeClass fakeClass)
            {
                this.fakeClass = fakeClass;
            }
        }

        private class FakeClass : IFakeClass
        {
        }
    }

    public interface IFakeClass
    {
    }
}