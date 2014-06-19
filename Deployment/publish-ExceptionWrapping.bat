C:\Windows\Microsoft.NET\Framework64\v4.0.30319\msbuild "../QueryableInterceptors.ExceptionWrapping/QueryableInterceptors.ExceptionWrapping.csproj" /p:Configuration=Release
..\.nuget\nuget pack "../QueryableInterceptors.ExceptionWrapping/QueryableInterceptors.ExceptionWrapping.csproj" -Properties Configuration=Release
..\.nuget\nuget push *.nupkg
del *.nupkg