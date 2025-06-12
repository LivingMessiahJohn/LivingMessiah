# `SmartEnum` code generation Prompt

Periodically I need to add a new set of code blocks to a csharp files that is an `abstract` of a class called `SmartEnum`  

In this case it's for the `Nav.cs` class (located in Enums/Nav.cs) 

### These `SmartEnum` inherited files have four regions

1. `region Id's`
2. `region Declared Public Instances`
3. `region Extra Fields`
4. `region Private Instantiation`

For this task, number 3. `region Extra Fields` can be ignored.

The file is setup so that I can copy a line (or a block of lines) of code from the "last entry" and than paste it right below the copied text
I then change this copied line to what to i need it to be.

## Region Details
Now I will explain each region and what gets duplicated

### 1. `region Id's` 
This region has is a class defined as `private static class Id` and has an entry for each field.

Here's a template of this region with `Foo` being the thing that changes

```csharp
	private static class Id
	{
    internal const int Foo = 1;
    // more fields that are similar.
  }
```

Here's an example with `Foo` replaced with `Home`
```csharp
	private static class Id
	{
    internal const int Home = 1;
    // more fields that are similar.
  }
```

#### Rules for `region Id's`
- Each field is defined as `internal const int`
- The name of each must be unique
- The value of each field is in sequential order starting with 1

### 2. `region Declared Public Instances`

These are instances of the `Nav` class 

Here are rules for this region
- The number of entries must match the number of fields in `region Id's` 
- The names of each `Nav` instance is the same name of the fields in `region Id's` 

Here's a template of this region with `Foo` being the thing that changes

```csharp
public static readonly Nav Foo = new FooSE();
```

If `Home` was the first field in `region Id's` then it would look like this...

```csharp
public static readonly Nav Home = new HomeSE();
```

### 4. `region Private Instantiation`
This region is different than 1 and 2 because it's not just one line that's being copied/pasted.
That's because it's a `class` and not simple fields or members so the whole class is duplicated.

Here's a template of what this region looks like with `Foo` being the thing that changes.

```csharp
private sealed class FooSE : Nav
{
  public FooSE() : base($"{nameof(Id.Foo)}", Id.Foo) { }
// bunch of code
}
```

Here's what it will look like after `Foo` is replaced with `Home` 
```csharp
private sealed class HomeSE : Nav
{
  public HomeSE() : base($"{nameof(Id.Home)}", Id.Home) { }
// bunch of code
}
```
