
# Example of LoadingComponentLIBRARY usage

```html
<LoadingComponentLIBRARY IsLoading="data == null" TurnSpinnerOff=TurnSpinnerOff>
</LoadingComponentLIBRARY>
```

```csharp
    protected List<FooQuery>? data = new();
    
    bool TurnSpinnerOff = false;
    protected override async Task OnInitializedAsync()
    {
      try
      {
        data = await db!.GetFooList();
      }
      catch (Exception ex)
      {
        Logger!.LogError(ex, "{Method}", nameof(OnInitializedAsync));
        Toast!.ShowError($"{Global.ToastShowError} | {nameof(OnInitializedAsync)}");
      }
      finally
      {
        TurnSpinnerOff = true;
      }
    }
```