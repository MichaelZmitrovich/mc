param(
	[string]$sourcePath = "C:\T\SourceData\",
	[string]$destinationPath = "C:\T\DestData\"
)

[string]$searchPattern = $sourcePath + "*"

Get-ChildItem -Path $searchPattern | ForEach-Object {	
        #Copy files into corresponding folders based on the file date        
        [string]$destFolder = $destinationPath + (Get-Date $_.CreationTime.Date -format "yyMMdd")

        if (-Not(Test-Path $destFolder))
        {
            New-Item -ItemType directory -Path $destFolder
        }
        
        if (-Not(Test-Path ($destFolder + "\" + $_.Name)))
        {
            Copy-Item $_.FullName $destFolder 
        }
	}
