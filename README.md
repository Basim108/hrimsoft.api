# Hrimsoft.Api
![license MIT](https://img.shields.io/badge/license-MIT-green)
![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Hrimsoft.Api)
![Nuget](https://img.shields.io/nuget/dt/Hrimsoft.Api)

Some useful helpers, configurations, and extensions for web api projects.  

## Installing ##

To install [NuGet package Hrimsoft.Api](https://www.nuget.org/packages/Hrimsoft.Api) run the following command in the Package Manager Console:

```
PM> Install-Package Hrimsoft.Api
```

## Supported Features
In this package you can find a preset for 
- snake case json serialization 
- swagger setup
- problem details
- some useful json converters for 
  - DateTime that cut micro and nano seconds
  - Enum values to snake_cased strings
- DataAnnotation attributes
  - StringArrayLengthAttribute that validates each item in array that it has length less or equal to some limit.
## License

Hrimsoft.Api is licensed under the MIT License. See [LICENSE](LICENSE) for details.

Copyright (c) Basim Al-Jawahery
