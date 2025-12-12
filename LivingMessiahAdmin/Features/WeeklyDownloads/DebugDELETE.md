
```html

<p>
	IsDisabled: @IsDisabled <br />
	isUploading: @isUploading <br />
	filename: @fileName  <br />
	selectedFile: @(selectedFile is null || string.IsNullOrEmpty(selectedFile.Name) ? "NULL" : selectedFile.Name)
</p> 

```