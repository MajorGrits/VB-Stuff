Option Explicit On
Imports System.IO
Imports System.Threading


Public Class Form4
    'Declare DataTables for key and tracking information
    Dim prepackKey As New DataTable
    Dim prepackTracking As New DataTable
    Dim idNum As Integer = 0
    Dim spath As String = My.Application.Info.DirectoryPath & "\Defect tag.lbx"
    Dim objDoc As bpac.Document
    Dim bRet As Boolean


    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        d13.Items.Add("")
        d17.Items.Add("")
        d21.Items.Add("")
        d25.Items.Add("")
        d29.Items.Add("")

        Timer1.Start()
        objDoc = New bpac.Document
        bRet = objDoc.Open(spath)
        createOrLoad()

        DataGridView1.DataSource = prepackTracking
        DataGridView1.Columns("Deleted").Visible = False
        Dim i As Integer = 0
        For Each row As DataRow In prepackTracking.Rows
            If Not IsDBNull(row.Item("Deleted")) AndAlso row.Item("Deleted") = "Yes" Then
                DataGridView1.CurrentCell = Nothing
                DataGridView1.Rows(i).Visible = False
            End If
            i += 1
        Next

        For Each ctrl As Control In Panel1.Controls
            If ctrl.GetType Is GetType(Label) Then
                Dim num As String = ctrl.Name.Substring(5, Len(ctrl.Name) - 5)
                ctrl.Text = prepackTracking.Columns(CType(num, Integer) - 1).ToString
            ElseIf ctrl.GetType Is GetType(ComboBox) And ctrl.Name.Contains("d13") Or ctrl.Name.Contains("d17") Or ctrl.Name.Contains("d21") Or ctrl.Name.Contains("d25") Or ctrl.Name.Contains("d29") Then
                Dim lines = File.ReadAllLines(My.Application.Info.DirectoryPath & "\resources\category.txt")
                Dim cb As ComboBox = ctrl
                For Each line In lines
                    cb.Items.Add(line)
                Next
            ElseIf ctrl.GetType Is GetType(ComboBox) And ctrl.Name.Contains("d6") Then
                Dim lines = File.ReadAllLines(My.Application.Info.DirectoryPath & "\resources\contracts.txt")
                Dim cb As ComboBox = ctrl
                For Each line In lines
                    cb.Items.Add(line)
                Next
            ElseIf ctrl.Name.Contains("d4") Then
                Dim cb As ComboBox = ctrl
                For Each row As DataRow In prepackKey.Rows
                    cb.Items.Add(row("Line Item").ToString & " " & row("Description").ToString & " " & row("Color").ToString)
                Next
            ElseIf ctrl.Name.Contains("d7") Or ctrl.Name.Contains("d8") Or ctrl.Name.Contains("d9") Or ctrl.Name.Contains("d10") Or ctrl.Name.Contains("d11") Then
                Dim cb As ComboBox = ctrl
                cb.Items.Add("")
                cb.Items.Add("Yes")
                cb.Items.Add("No")
                cb.Text = ""
            ElseIf ctrl.Name.Contains("d") And ctrl.GetType Is GetType(DateTimePicker) Then
                Dim num As String = ctrl.Name.Substring(1, Len(ctrl.Name) - 1)
                Dim dtp As DateTimePicker = ctrl
                dtp.Format = DateTimePickerFormat.Custom
                dtp.CustomFormat = "MM/dd/yyyy"
            ElseIf ctrl.Name.ToString = "d2" Then
                Dim lines = File.ReadAllLines(My.Application.Info.DirectoryPath & "\resources\auditors.txt")
                For Each line In lines
                    d2.Items.Add(line)
                Next
            End If
        Next

        Me.CenterToScreen()
        DataGridView1.ReadOnly = True
        d32.Text = 1
        If Not d2.Items.Count = 0 Then
            d2.Text = d2.Items(0)
        End If

        getIDNum()

        d13.Items.Add("")


    End Sub


    Public Function pkExist()
        Dim x As Integer = 0
        Dim match As DataRow
        match = prepackTracking.Rows.Find(d1.Text)
        If d1.Text = "" Then
            x = 2
        ElseIf Not match Is Nothing Then
            x = 1
        End If
        Return x
    End Function

    Private Sub lineItem_Leave(sender As Object, e As EventArgs) Handles d4.Leave
        Dim match As DataRow
        Dim lines() As String = d4.Text.Split(" ")
        match = prepackKey.Rows.Find(lines(0))
        If Not match Is Nothing Then
            d4.Text = match(0)
            Label5.Text = match("Tent Type")
        End If

    End Sub

    Public Sub writeLines()

        Dim x = 0
        Using writeLine As StreamWriter = New StreamWriter(My.Application.Info.DirectoryPath & "\resources\prepackTracking.txt")
            For Each row As DataRow In prepackTracking.Rows
                x = 0
                While x < 33
                    If x = 32 Then
                        writeLine.Write(row(x).ToString.Replace(",", " "))
                        x += 1
                    Else
                        writeLine.Write(row(x).ToString.Replace(",", " ") & ",")
                        x += 1
                    End If
                End While
                writeLine.Write(Environment.NewLine)
            Next
        End Using
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If d2.Text = "" Then
            MsgBox("Please Select Auditor")
            Exit Sub
        ElseIf d4.Text = "" Then
            MsgBox("Please Select Line Item")
            Exit Sub
        End If

        Dim x = pkExist()
        If x = 1 Then
            MsgBox("ID Already Exists.")
            Exit Sub
        ElseIf x = 2 Then
            MsgBox("Please Enter an ID")
            Exit Sub
        Else
            Dim row As DataRow = prepackTracking.NewRow

            For Each ctrl As Control In Panel1.Controls
                If ctrl.Name.Contains("d") And ctrl.GetType Is GetType(DateTimePicker) Then
                    Dim num As String = ctrl.Name.Substring(1, Len(ctrl.Name) - 1)
                    Dim dtp As DateTimePicker = ctrl
                    Dim strDate As String = dtp.Value.Date
                    row(CType(num, Integer) - 1) = strDate
                ElseIf ctrl.Name.Contains("d") Then
                    Dim num As String = ctrl.Name.Substring(1, Len(ctrl.Name) - 1)
                    row(CType(num, Integer) - 1) = ctrl.Text
                ElseIf ctrl.Name = "Label5" Then
                    row(4) = Label5.Text
                End If
            Next


            prepackTracking.Rows.Add(row)
            Dim i As Integer = 0
            For Each r As DataRow In prepackTracking.Rows
                If Not IsDBNull(r.Item("Deleted")) AndAlso r.Item("Deleted") = "Yes" Then
                    DataGridView1.CurrentCell = Nothing
                    DataGridView1.Rows(i).Visible = False
                End If
                i += 1
            Next
            writeLines()
            clear()

        End If

    End Sub

    Public Sub clear()
        For Each ctrl As Control In Panel1.Controls
            If ctrl.GetType Is GetType(TextBox) Then
                Dim tb As TextBox = ctrl
                tb.Clear()
            ElseIf ctrl.GetType Is GetType(ComboBox) Then
                Dim cb As ComboBox = ctrl
                cb.Text = ""
            ElseIf ctrl.GetType Is GetType(DateTimePicker) Then
                Dim dt As DateTimePicker = ctrl
                dt.Value = Today
            End If
        Next
        Label5.Text = ""
        getIDNum()
        d32.Text = 1

        If Not d2.Items.Count = 0 Then
            d2.Text = d2.Items(0)
        End If

    End Sub

    Private Sub createOrLoad()
        'Create files if they don't currently exist
        'Rerun Sub to add items from text files into datatables.
        Dim resources As String = My.Application.Info.DirectoryPath & "\resources"
        Dim paths() As String = {"\prepackKey.txt", "\prepackTracking.txt"}
        Dim dtList As New List(Of DataTable)
        Dim i As Integer = 0

        If Not Directory.Exists(resources) Then
            Directory.CreateDirectory(resources)
            For Each path In paths
                File.Create(resources & path).Close()
            Next
            File.Create(resources & "\contracts.txt").Close()
            File.Create(resources & "\auditors.txt").Close()
            File.Create(resources & "\category.txt").Close()

            createOrLoad()
        Else
            dtList.Add(prepackKey)
            Dim ppkColumns() As String = {"Line Item", "Color", "Description", "Tent Type"}
            createDataTable(prepackKey, ppkColumns)
            prepackKey.PrimaryKey = {prepackKey.Columns("Line Item")}

            dtList.Add(prepackTracking)
            Dim pptColumns() As String = {"ID", "Auditor(s)", "Date Completed", "Line Item", "Tent Type", "Contract", "Hold", "Repaired", "Reinspected", "Recut", "Released", "(1)Defect", "(1)Category", "(1)Repaired By", "(1)Repaired Date", "(2)Defect", "(2)Category", "(2)Repaired By", "(2)Repaired Date", "(3)Defect", "(3)Category", "(3)Repaired By", "(3)Repaired Date", "(4)Defect", "(4)Category", "(4)Repaired By", "(4)Repaired Date", "(5)Defect", "(5)Category", "(5)Repaired By", "(5)Repaired Date", "QTY", "Deleted"}
            createDataTable(prepackTracking, pptColumns)
            prepackTracking.PrimaryKey = {prepackTracking.Columns("ID")}


            For Each path In paths
                fillDT(resources & path, dtList(i))
                i += 1
            Next

        End If

    End Sub

    Private Sub createDataTable(dt As DataTable, columns As String())

        For Each column In columns
            dt.Columns.Add(column, GetType(String))
        Next

    End Sub

    Private Sub fillDT(path As String, dt As DataTable)
        'Read text files to fill DataTables
        Dim lines() As String = File.ReadAllLines(path)
        Dim count As Integer = 0
        count = lines.Count
        If count = 0 Then
            d1.Text = 1
        Else
            Dim x As Integer = 0
            For Each line In lines
                Dim R As DataRow = dt.NewRow
                lines = line.Split(",")
                count = lines.Count()
                While x < count
                    R(x) = lines(x)
                    x += 1
                End While
                dt.Rows.Add(R)
                x = 0
            Next
            d1.Text = idNum + 1
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        If d2.Text = "" Then
            MsgBox("Please Select Auditor")
            Exit Sub
        ElseIf d4.Text = "" Then
            MsgBox("Please Select Line Item")
            Exit Sub
        End If

        Dim id As String = d1.Text
        Dim match As DataRow
        match = prepackTracking.Rows.Find(id)
        If Not match Is Nothing Then
            For Each ctrl As Control In Panel1.Controls
                If ctrl.Name.Contains("d") And ctrl.GetType Is GetType(DateTimePicker) Then
                    Dim num As String = ctrl.Name.Substring(1, Len(ctrl.Name) - 1)
                    Dim dtp As DateTimePicker = ctrl
                    Dim strDate As String = dtp.Value.Date
                    match(CType(num, Integer) - 1) = strDate
                ElseIf ctrl.Name.Contains("d") Then
                    Dim num As String = ctrl.Name.Substring(1, Len(ctrl.Name) - 1)
                    match(CType(num, Integer) - 1) = ctrl.Text
                ElseIf ctrl.Name = "Label5" Then
                    match(4) = Label5.Text
                End If
            Next
        End If
        writeLines()
        clear()

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        clear()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim id As String = d1.Text
        Dim match As DataRow
        Dim index As Integer
        If id = idNum + 1 Then
            MsgBox("Item does not exist.")
            Exit Sub
        End If
        match = prepackTracking.Rows.Find(id)
        match("Deleted") = "Yes"
        index = prepackTracking.Rows.IndexOf(match)

        If Not match Is Nothing Then
            'Dim myCurrencyManager As CurrencyManager = DirectCast(BindingContext(DataGridView1.DataSource), CurrencyManager)
            'myCurrencyManager.SuspendBinding()
            DataGridView1.CurrentCell = Nothing
            DataGridView1.Rows(index).Visible = False
        End If
        writeLines()
        clear()

    End Sub



    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim i As Integer
        Dim x As Integer = 0
        Dim name As String
        i = DataGridView1.CurrentRow.Index
        While x < 32
            name = "d" & x + 1
            For Each ctrl As Control In Panel1.Controls
                If ctrl.Name = name And ctrl.GetType Is GetType(DateTimePicker) Then
                    Dim dateObj As DateTimePicker
                    dateObj = CType(ctrl, DateTimePicker)
                    If Not IsDBNull(DataGridView1.Item(x, i).Value) Then
                        dateObj.Value = DataGridView1.Item(x, i).Value
                    End If
                ElseIf ctrl.Name = name Then
                    ctrl.Text = DataGridView1.Item(x, i).Value.ToString
                ElseIf ctrl.Name = "Label5" Then
                    ctrl.Text = DataGridView1.Item(4, i).Value.ToString
                End If
            Next
            x += 1
        End While
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        Dim id As String = TextBox1.Text
        Dim match As DataRow
        Dim x As Integer = 0
        match = prepackTracking.Rows.Find(id)
        If Not match Is Nothing Then
            While x < 32
                Name = "d" & x + 1
                For Each ctrl As Control In Panel1.Controls
                    If ctrl.Name = Name And ctrl.GetType Is GetType(DateTimePicker) Then
                        Dim dateObj As DateTimePicker
                        dateObj = CType(ctrl, DateTimePicker)
                        dateObj.Value = match(x)
                    ElseIf ctrl.Name = Name Then
                        ctrl.Text = match(x)
                    ElseIf ctrl.Name = "Label5" Then
                        ctrl.Text = match(4)
                    End If
                Next
                x += 1
            End While
        End If

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If d4.Text = "" Then
            MsgBox("Please Select Line Item")
            Exit Sub
        End If
        DoPrint()

    End Sub

    ' Dim spath As String = My.Application.Info.DirectoryPath & "\Defect tag.lbx"

    Public Sub DoPrint()

        'Dim objDoc As bpac.Document
        'objDoc = New bpac.Document

        'Dim bRet As Boolean
        'bRet = objDoc.Open(spath)

        If (objDoc.Open(spath) <> False) Then
            objDoc.GetObject("Text4").Text = d1.Text
            objDoc.GetObject("Text3").Text = d4.Text
            objDoc.GetObject("Text5").Text = Label5.Text
            objDoc.GetObject("Text9").Text = d3.Value.ToShortDateString
            objDoc.GetObject("Text6").Text = d2.Text
            objDoc.GetObject("Text10").Text = d12.Text
            objDoc.GetObject("Text11").Text = d16.Text
            objDoc.GetObject("Text12").Text = d20.Text
            objDoc.GetObject("Text13").Text = d24.Text
            objDoc.GetObject("Text14").Text = d28.Text

            objDoc.StartPrint("", bpac.PrintOptionConstants.bpoDefault)
            objDoc.PrintOut(1, bpac.PrintOptionConstants.bpoDefault)
            objDoc.EndPrint()
            'objDoc.Close()
        End If

        'objDoc = Nothing
    End Sub


    Public Sub getIDNum()
        idNum = 0
        For Each row As DataRow In prepackTracking.Rows
            If idNum < row(0) Then
                idNum = row(0)
            End If
        Next

        d1.Text = idNum + 1

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Form2.Show()
        Me.Close()

    End Sub

    Private Sub Form4_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        objDoc.Close()
        objDoc = Nothing
        Dim DT As DateTime = Now
        My.Computer.FileSystem.CopyDirectory(My.Application.Info.DirectoryPath & "\resources", "F:\AVANDERPOOL\Quality\Reporting Backup\" & DT.ToString("MM-dd-yyyy H.mm.ss"), False)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Dim DT As DateTime = Now

        My.Computer.FileSystem.CopyDirectory(My.Application.Info.DirectoryPath & "\resources", "F:\AVANDERPOOL\Quality\Reporting Backup\" & DT.ToString("MM-dd-yyyy H.mm.ss"), False)

    End Sub
End Class



