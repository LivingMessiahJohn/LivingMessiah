# Menu won't collapse

- See C:\Source\LivingeMessiahBackup\Feature-Ideas\999-navbar-collapse-the-hamburger

## Prompt: Why won't the Menu collapse

### Scenario

- .Net 9 Blazor Web App

#### `App.razor` setup to do SSR
**RenderMode**: InteractiveServer

```html
<!-- ... -->
	<HeadOutlet @rendermode="InteractiveServer" />
</head>

<body>
	<Routes @rendermode="InteractiveServer" />  
 <!-- ... -->
</body>
```

#### Context
This occurs when I'm in the mobile screen size (Media Query == Xs) the menu items in the NavBar are collapsed. 

##### Action to re-create the problem.
1. I click the hamburger button which drops down the menu
2. I select one of the menu items
3. This takes me to the correct page
4. **but** It doesn't **rollup the navbar menus**, it only happens if I manually click the hamburger button again.

Question: Can this be done automatically?

##### A. No, because of `"@rendermode="InteractiveServer"`
To cause the 

## What about using Enhanced Navigation in Blazor SSR 
- created in .NET 8
- first step in making a Blazor SSR **interactive**
- enabled by default in the `blazor.web.js`
- Enhanced Navigation is a partial page reload


#### `App.razor` enables Enhanced Navigation because `blazor.web.js` has been added 
```html
<body>
	<!-- ... -->
	<script src="_framework/blazor.web.js"></script>
</body>
```

#### [.NET 8 | Ep25. Use Enhanced Navigation in Blazor SSR](https://www.youtube.com/watch?v=x84xVgteU78)
This is shown by clicking the various buttons in the purple menu bar, and a whole page refresh does not occur.

`blazor.web.js` interrupts the normal call between the browser and the Web Server and does a **fetch api**

The problem i'm having occurs during navigation, so why doesn't Enhanced Navigation solve my problem?


If you want to revisit this later, there's actually a much simpler approach - 
you could just add a small bit of JavaScript that listens for clicks on navbar links and closes the menu. 
Something like:

```js
document.addEventListener('click', function(e) {
    if (e.target.matches('.navbar-nav a')) {
        $('.navbar-collapse').collapse('hide');
    }
});
```
