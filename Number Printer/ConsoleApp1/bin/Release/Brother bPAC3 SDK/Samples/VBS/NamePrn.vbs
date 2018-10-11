'	b-PAC 3.0 Component Sample (Instant Name Plate)
'	(C)Copyright Brother Industries, Ltd. 2009
'
'<SCRIPT LANGUAGE="VBScript">
	' Data Folder
	Const sDataFolder = "C:\Program Files\Brother bPAC3 SDK\Templates\"
	DoPrint(sDataFolder & "NamePlate1.lbx")

	'*******************************************************************
	'	Print Module
	'*******************************************************************
	Sub DoPrint(strFilePath)
		Set ObjDoc = CreateObject("bpac.Document")
		bRet = ObjDoc.Open(strFilePath)
		If (bRet <> False) Then
			ObjDoc.GetObject("objCompany").Text = "Brother Industries, Ltd."
			ObjDoc.GetObject("objName").Text = "John Smith"
			' ObjDoc.SetMediaByName ObjDoc.Printer.GetMediaName(), True
			ObjDoc.StartPrint "", 0
			ObjDoc.PrintOut 1, 0
			ObjDoc.EndPrint
			ObjDoc.Close
		End If
		Set ObjDoc = Nothing
	End Sub
