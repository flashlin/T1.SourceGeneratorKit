
```
<ItemGroup>
  <ProjectReference Include="..\T1.CodeSourceGenerator\T1.CodeSourceGenerator.csproj"
                    OutputItemType="Analyzer" ReferenceOutputAssembly="false" />
</ItemGroup>
```
這裡的 OutputItemType="Analyzer" 用來設定 MySourceGenerator 專案是一個「分析器」專案。
而 ReferenceOutputAssembly="false" 則是設定該專案不會成為該專案的參考組件



```
dotnet add package Microsoft.CodeAnalysis.CSharp
dotnet add package Microsoft.CodeAnalysis.Analyzers
```


變更 Source Generator 專案中的類別，並不會導致 Generator 自動重新建置！
你必須變更主要專案(ConsoleApp1)的原始碼，或是清空主要專案，才會讓 Source Generator 重新執行！

如果主要專案建置失敗，那麼每次建置都會重新執行 Source Generator
你的 Visual Studio 2019 會一直在背景重新建置專案，此時你在編輯程式碼打字的過程中，就會導致 Generator 類別不斷重複執行！
從 Visual Studio 2019 的輸出視窗可以很輕易的看出 Source Generator 的執行錯誤！