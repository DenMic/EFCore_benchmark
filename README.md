# EFCore Benchmark
Benchmark of various EF core 5 implementations.

Insert implementations is done with InMemoryDatabase, so the result may not be perfect.
The benchmark on select can be run either with SQLite Database (which creates a file locally), or with MSSql (to use this DB change the connection string present in the Service project in Helper/ContextHelper.cs, the creation scripts of the db can be found in the Infrastructure project in DBScript)

_**Work in Progress**_

## Nuget Package
- EF core 5: inMemory, MSSql e Sqlite
- BenchmarkDotNet

## Result

**Insert 100000 row**
![image](https://user-images.githubusercontent.com/53993253/126069711-a3c80f8c-84c0-4ba0-9174-236079696df6.png)


**Select with traking, lazy loading, split query in sqlite **
![image](https://user-images.githubusercontent.com/53993253/126878583-2acee92f-7501-432e-ac96-c0722d56df20.png)

