'*******************************************************************
'
'       b-PAC 3.0 Component Sample (Badge)
'
'       (C)Copyright Brother Industries, Ltd. 2009
'
'*******************************************************************
Option Explicit On

Public Class Badge
    Const sPath = "C:\Program Files\Brother bPAC3 SDK\Templates\Badge.lbx"

    '********************************************************
    '   Open and Print a spcified file.
    '********************************************************
    Public Sub DoPrint()
        Dim objDoc As bpac.Document
        objDoc = CreateObject("bpac.Document")
        If (objDoc.Open(sPath) <> False) Then
            objDoc.GetObject("objName").Text = txtName.Text
            objDoc.GetObject("objCompany").Text = txtCompany.Text

            'objDoc.SetMediaByName(objDoc.Printer.GetMediaName, True)
            objDoc.StartPrint("", bpac.PrintOptionConstants.bpoDefault)
            objDoc.PrintOut(1, bpac.PrintOptionConstants.bpoDefault)
            objDoc.EndPrint()
            objDoc.Close()
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        DoPrint()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
