# Swagger .NET Core Sample
This directory contains a proof of concept that you can use Swashbuckler (https://github.com/domaindrivendev/Swashbuckle).

[![Build Status](https://travis-ci.org/luiz-moreira-avalara/swagger-poc.svg?branch=master)](https://travis-ci.org/luiz-moreira-avalara/swagger-poc)

Check it out at [https://swagger-poc.herokuapp.com/api-docs](https://swagger-poc.herokuapp.com/api-docs).
Check [apiary doc](http://docs.swashbucklepoc.apiary.io) format.
## How to run the sample?

In order to run this sample, you first need to [install .NET Core](http://dotnet.github.io/getting-started/). After that, you can clone this repo, go into the src folder:

* Run from source using the following commands:
	* `cd src`
	* `dotnet restore`
	* `cd Api`
	* `dotnet run`
  	* access [http://localhost:5000/api-docs](http://localhost:5000/api-docs).
* Compile and run using the following commands
	* `cd src`
	* `dotnet restore`
	* `dotnet build`
	* `dotnet Api/bin/Debug/[framework]/Api.dll`
  	* access [http://localhost:5000/api-docs](http://localhost:5000/api-docs).

Using tokens below different endpoint will be showen in Swagger.

## Tokens

Authenticated user:
```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.e30.xG4_EsxN0MIV4DEbkUVyFWm-zR3E6ayA-NOaz6wpQHM
```

With scope `user`:
```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzY29wZSI6InVzZXIifQ.7CC9yTVTF3KJBBOxtyurj9ZaUMDk7jlChyJiAm6imqk
```

With scope `store`:
```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzY29wZSI6InN0b3JlIn0.O-LJS3h_3sIorggV2i8Cko0Tg4pZKp2VaDaOMVFnkos
```

With scope `pet`:
```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzY29wZSI6InBldCJ9.Ymp788iA-u77z90Itzk1jzOQ2o5iqjfDUJjk_-9YLPo
```

With scopes `user` and `store`:
```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzY29wZSI6WyJ1c2VyIiwic3RvcmUiXX0.gbZvSumHt38-xmmNQx39NiAKrQtGqDbIgZ8T0yB2wIA
```

With scopes `user` and `pet`:
```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzY29wZSI6WyJ1c2VyIiwicGV0Il19.2TWLa4hdiUK4emebcQmK7F-qIR0QP1G1i9W7AFv23AA
```

With scopes `store` and `pet`:
```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzY29wZSI6WyJzdG9yZSIsInBldCJdfQ.p6i4oXTZ01tSRzgSm1EqlZFXfY4SxpV8jgOXiwIwHew
```

With all scopes `user`, `store` and `store`:
```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzY29wZSI6WyJ1c2VyIiwic3RvcmUiLCJwZXQiXX0.AIDulefvT7C-bh9MeyCaEaqJck9tJS7bqiD0EYeglmo
```

## Reference:
  * https://github.com/jenyayel/SwaggerSecurityTrimming
  * http://petstore.swagger.io/
  * https://github.com/domaindrivendev/Swashbuckle.AspNetCore
