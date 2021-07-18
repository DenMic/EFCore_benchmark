# EFCore Benchmark
Benchmark of various EF core 5 implementations.

All implementations are done with InMemoryDatabase, so the result may not be perfect.

_**Work in Progress**_

## Nuget Package
- EF core 5
- BenchmarkDotNet

## Result

**Insert 100000 row**
![image](https://user-images.githubusercontent.com/53993253/126069711-a3c80f8c-84c0-4ba0-9174-236079696df6.png)


**Select with traking enabled and disabled**
![image](https://user-images.githubusercontent.com/53993253/126069491-1e26c58b-777d-4c04-995a-8faddc825242.png)
