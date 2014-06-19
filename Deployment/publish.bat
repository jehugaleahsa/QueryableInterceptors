C:\Windows\Microsoft.NET\Framework64\v4.0.30319\msbuild ../QueryableInterceptors/QueryableInterceptors.csproj /p:Configuration=Release
..\.nuget\nuget pack ../QueryableInterceptors/QueryableInterceptors.csproj -Properties Configuration=Release
..\.nuget\nuget push *.nupkg
del *.nupkg