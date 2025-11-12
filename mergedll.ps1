$rootFolder = (Get-Item (Get-location))
$pieces = (Get-ChildItem *.dll | Select-Object -Property @{ Name='Path';Expression= { ".$($_.FullName.SubString($rootFolder.FullName.Length))" } }).Path
$params = [System.Collections.Generic.List[string]]::new()

(Get-Item *old.exe) | foreach { rename-item $_.FullName $_.FullName.Replace("old.exe","exe") -ErrorAction SilentlyContinue }

$exe = ".\$((Get-Item *.exe).FullName.SubString($rootFolder.FullName.Length))"
$oldexe = $exe.Replace(".exe","old.exe")

$params.Add("/out:$($exe)")
$params.Add($oldexe)

foreach($piece in $pieces)
{
    $params.Add($piece)
}

rename-item (Get-Item *.exe).FullName (Get-Item *.exe).FullName.Replace(".exe","old.exe")

set-content "ilmerge.log" $params | Out-String

$mergePath = (Get-Item ..\..\..\packages\ILMerge.*\tools\*\ILMerge.exe)

. $mergePath $params.ToArray()

foreach($piece in $pieces) {
    Remove-item $piece
}

remove-item $oldexe