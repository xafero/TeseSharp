# TeseSharp
A .NET port of text serializer library

## How to use?
```csharp
var adr = new Address { 
				City = new City {
					Name = "Berlin", State = State.UT, Code = 12345
				},
				Street = "Main Road", Number = 21, Postal = 42
};
			
var tese = new Tese();
using (var writer = File.OpenWrite("test.txt")) {
    tese.Serialize(adr, writer);
}
```
