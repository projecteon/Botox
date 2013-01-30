Botox
========

Visit the [Botox website](https://github.com/projecteon/Botox) for more information.

### What is it?
Botox is designed to be a ultra light weigth depency injection framework for the .NET portable class libraries.  

### Basic use

Registrer a a resolution with an interface:

<!-- {% examplecode csharp %} -->
    Botox.Registrer<IFakeClass, FakeClass>(new FakeClass());
<!-- {% endexamplecode %} -->

Or

<!-- {% examplecode csharp %} -->
	Botox.Registrer<IFakeClass>(new FakeClass());
<!-- {% endexamplecode %} -->

Resolve:
<!-- {% examplecode csharp %} -->
	Botox.Resolve<IFakeClass>()
<!-- {% endexamplecode %} -->

The framework also supports creating instances of objects with constructor injection:

<!-- {% examplecode csharp %} -->
	private class FakeClassInjection
        {
            public readonly IFakeClass fakeClass;

            public FakeClassInjection(IFakeClass fakeClass)
            {
                this.fakeClass = fakeClass;
            }
        }

	var fakeClassInjection = Botox.CreateInstanceOf<FakeClassInjection>();
<!-- {% endexamplecode %} -->

###Motivation
The project was created due to the lack of injection frameworks available at the time with support for portable class libraries.

###Installation
The project can be installed easiliy via NuGet or by downloading and compiling the source yourself.

###License
See Lisence.txt