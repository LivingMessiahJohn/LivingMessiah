# Initial Setup | .Net 9 Blazor Project	
- Set **Interactive render mode** to **Server** 
- Set **Interactivity location** to **Global**
- Added Bootstrap 5.3.1 to App.razor

# Bootstrap 5.3.1	
1. Added the following to `App.razor` (Components folder)
```html	
<!-- inside head -->

<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css"
			integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH"
			crossorigin="anonymous" />

<!-- insie body -->

<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
```

2. To make Intelisense work with Bootstrap classes, I added `bootstrap.min.css` to wwwroot/css/bootstrap/ 
```	


# Vertical Slice Architecture 

#### 1. Created a Features folder 
I want the bulk of my components to exist under that folder.
This meant I had to move `_Imports.razor` to the root of the project	

# Nuget Packages	
- Ardalis.SmartEnum


how come when running locally my pages are not interactive when I make a changes? 
is it because the rendermode is globally set to InteractiveServer?

```
...
	<HeadOutlet @rendermode="InteractiveServer" />
</head>

<body>
	<Routes @rendermode="InteractiveServer" />
...
```