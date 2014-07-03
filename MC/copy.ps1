param(
	[string]$sourcePath = "C:\T\SourceData\",
	[string]$destinationPath = "C:\T\DestData\"
)

Get-ChildItem -Path $temp | 
	ForEach-Object
	{	
		if(($_.Attributes -eq "Directory") -and ($_.Name -eq "Bin"))
		{
			LogMessage ("Copying Bin directory to Destination : {0}" -f $_.Name)
			Copy-Item $_.FullName $destinationPath -recurse -force
		}
		elseif($_.GetType().Name -eq "FileInfo")
		{
	
			#Copy Autoconfig files    
			if(($_.Name -eq "Web.config") -or ($_.Name -eq "Web.connectionstrings.config"))
			{
				if($forceConfig -eq "Y")
				{
					LogMessage ("Copying Config file to Destination : {0}" -f $_.Name)
					Copy-Item $_.FullName $destinationPath -recurse -force
				}
				
			}
	
			#Copy Service files    
			if($_.Name -like "*.svc")
			{
				LogMessage ("Copying service file to Destination : {0}" -f $_.Name)
				Copy-Item $_.FullName $destinationPath -recurse -force
			}
	
		}
	}
