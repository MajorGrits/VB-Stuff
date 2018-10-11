Imports System.IO

Public Class Form2

    Dim auditorDT As New DataTable
    Dim keyDT As New DataTable
    Dim contractDT As New DataTable
    Dim defectTypeDT As New DataTable
    Dim temp As DataTable
    Dim prepackTracking As New DataTable
    Dim tempLineItem As String
    Dim cellTemp As String
    Dim writePath As String
    Dim index As Integer
    Dim paths() As String = {"prepackKey.txt", "contracts.txt", "category.txt", "auditors.txt"}



    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = ""

        auditorDT.Columns.Add("Name")
        auditorDT.PrimaryKey = {auditorDT.Columns("Name")}

        keyDT.Columns.Add("Line Item")
        keyDT.Columns.Add("Color")
        keyDT.Columns.Add("Description")
        keyDT.Columns.Add("Tent Type")
        keyDT.PrimaryKey = {keyDT.Columns("Line Item")}

        contractDT.Columns.Add("Contract")
        contractDT.PrimaryKey = {contractDT.Columns("Contract")}

        defectTypeDT.Columns.Add("Category")
        defectTypeDT.PrimaryKey = {defectTypeDT.Columns("Category")}

        cellTemp = ""

        Dim pptColumns() As String = {"ID", "Auditor(s)", "Date Completed", "Line Item", "Tent Type", "Contract", "Hold", "Repaired", "Reinspected", "Recut", "Released", "(1)Defect", "(1)Category", "(1)Repaired By", "(1)Repaired Date", "(2)Defect", "(2)Category", "(2)Repaired By", "(2)Repaired Date", "(3)Defect", "(3)Category", "(3)Repaired By", "(3)Repaired Date", "(4)Defect", "(4)Category", "(4)Repaired By", "(4)Repaired Date", "(5)Defect", "(5)Category", "(5)Repaired By", "(5)Repaired Date", "QTY", "Deleted"}
        createDataTable(prepackTracking, pptColumns)
        prepackTracking.PrimaryKey = {prepackTracking.Columns("ID")}


        Dim lines As String() = File.ReadAllLines(My.Application.Info.DirectoryPath & "\resources\prepackTracking.txt")
        Dim tempLines As String()
        Dim i As Integer = 0
        For Each line In lines
            Dim R As DataRow = prepackTracking.NewRow
            tempLines = line.Split(",")
            While i < tempLines.Count
                R(i) = tempLines(i)
                i += 1
            End While
            prepackTracking.Rows.Add(R)
            i = 0
        Next

        index = -1
        Me.CenterToScreen()
        hide()

    End Sub

    Private Sub createDataTable(dt As DataTable, columns As String())

        For Each column In columns
            dt.Columns.Add(column, GetType(String))
        Next

    End Sub


    Public Sub hide()

        TextBox1.Hide()
        TextBox2.Hide()
        TextBox3.Hide()
        TextBox4.Hide()
        Label2.Hide()
        Label3.Hide()
        Label4.Hide()
        Label5.Hide()
        clear()

    End Sub

    Public Sub loadDT(path)

        Dim lines() As String = File.ReadAllLines(My.Application.Info.DirectoryPath & "\resources\" & path)

        Select Case path
            Case "prepackKey.txt"

                For Each line In lines
                    Dim R As DataRow = keyDT.NewRow
                    lines = line.Split(",")
                    R(0) = lines(0)
                    R(1) = lines(1)
                    R(2) = lines(2)
                    R(3) = lines(3)
                    keyDT.Rows.Add(R)
                Next
            Case "contracts.txt"

                For Each line In lines
                    Dim R As DataRow = contractDT.NewRow
                    R(0) = line
                    contractDT.Rows.Add(R)
                Next
            Case "category.txt"

                For Each line In lines
                    Dim R As DataRow = defectTypeDT.NewRow
                    R(0) = line
                    defectTypeDT.Rows.Add(R)
                Next
            Case "auditors.txt"

                For Each line In lines
                    Dim R As DataRow = auditorDT.NewRow
                    R(0) = line
                    auditorDT.Rows.Add(R)
                Next
        End Select
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        hide()
        writePath = "auditors.txt"
        Label2.Text = "Auditor"
        Label2.Show()
        TextBox1.Show()
        temp = auditorDT
        temp.Clear()
        loadDT(writePath)
        DataGridView1.DataSource = auditorDT
        temp = auditorDT
        Label1.Text = Button1.Text
        deselect()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        hide()
        writePath = "prepackKey.txt"
        Label2.Text = "Line Item"
        Label2.Show()
        TextBox1.Show()
        Label3.Text = "Color"
        Label3.Show()
        TextBox2.Show()
        Label4.Text = "Description"
        Label4.Show()
        TextBox3.Show()
        Label5.Text = "Tent Type"
        Label5.Show()
        TextBox4.Show()
        temp = keyDT
        temp.Clear()
        loadDT(writePath)

        DataGridView1.DataSource = keyDT
        temp = keyDT
        Label1.Text = Button2.Text
        deselect()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        hide()
        writePath = "contracts.txt"
        Label2.Text = "Contract"
        Label2.Show()
        TextBox1.Show()
        temp = contractDT
        temp.Clear()
        loadDT(writePath)
        DataGridView1.DataSource = contractDT
        temp = contractDT
        Label1.Text = Button3.Text
        deselect()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        hide()
        writePath = "category.txt"
        Label2.Text = "Defect Category"
        Label2.Show()
        TextBox1.Show()
        temp = defectTypeDT
        temp.Clear()
        loadDT(writePath)
        DataGridView1.DataSource = defectTypeDT
        temp = defectTypeDT
        Label1.Text = Button4.Text
        deselect()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick


        index = DataGridView1.CurrentCell.RowIndex
        cellTemp = DataGridView1(0, index).ToString

        If Label2.Text = "Line Item" Then
            TextBox1.Text = DataGridView1(0, index).Value.ToString
            TextBox2.Text = DataGridView1(1, index).Value.ToString
            TextBox3.Text = DataGridView1(2, index).Value.ToString
            TextBox4.Text = DataGridView1(3, index).Value.ToString
        Else
            TextBox1.Text = DataGridView1(0, index).Value.ToString
        End If

        tempLineItem = TextBox1.Text

    End Sub

    Public Sub clear()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click

        clear()

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If temp Is Nothing Then
            MsgBox("Please Select a Data Table")
            Exit Sub
        End If
        Dim index As Integer = DataGridView1.CurrentCell.RowIndex

        If cellTemp = DataGridView1(0, index).ToString Then
            If Label2.Text = "Line Item" Then
                DataGridView1(0, index).Value = TextBox1.Text
                DataGridView1(1, index).Value = TextBox2.Text
                DataGridView1(2, index).Value = TextBox3.Text
                DataGridView1(3, index).Value = TextBox4.Text
            ElseIf Label2.Text = "Auditor" Then
                DataGridView1(0, index).Value = TextBox1.Text
            ElseIf Label2.Text = "Contract" Then
                DataGridView1(0, index).Value = TextBox1.Text
            ElseIf Label2.Text = "Defect Category" Then
                DataGridView1(0, index).Value = TextBox1.Text
            End If
        Else
            MsgBox("Please Select a Row")
            Exit Sub
        End If

        For Each row As DataRow In prepackTracking.Rows
            If row(3).ToString = tempLineItem Then
                row(3) = TextBox1.Text
            End If
        Next
        tempLineItem = ""
        Dim x As Integer
        Using writeUpdate As StreamWriter = New StreamWriter(My.Application.Info.DirectoryPath & "\resources\prepackTracking.txt")
            For Each row As DataRow In prepackTracking.Rows
                x = 0
                While x < 33
                    If x = 32 Then
                        writeUpdate.Write(row(x).ToString.Replace(",", " "))
                        x += 1
                    Else
                        writeUpdate.Write(row(x).ToString.Replace(",", " ") & ",")
                        x += 1
                    End If
                End While
                writeUpdate.Write(Environment.NewLine)
            Next
        End Using

        writeLines()
        clear()

    End Sub

    Public Sub writeLines()
        Using write As StreamWriter = New StreamWriter(My.Application.Info.DirectoryPath & "\resources\" & writePath)
            Dim cols As Integer = DataGridView1.Columns.Count()
            If cols = 4 Then
                For Each row As DataRow In temp.Rows
                    write.Write(row(0).ToString.Replace(",", " ") & "," & row(1).ToString.Replace(",", " ") & "," & row(2).ToString.Replace(",", " ") & "," & row(3).ToString.Replace(",", " ") & Environment.NewLine)
                Next
            Else
                For Each row As DataRow In temp.Rows
                    write.Write(row(0).ToString.Replace(",", " ") & Environment.NewLine)
                Next
            End If
        End Using
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click

        If temp Is Nothing Then
            MsgBox("Please Select a Data Table")
            Exit Sub
        ElseIf textbox1.Text = "" Then
            MsgBox("Please do not leave fields blank")
            Exit Sub
        End If
        Dim R As DataRow = temp.NewRow
        Dim cols As Integer = temp.Columns.Count()
        If cols = 4 Then
            R(0) = TextBox1.Text
            R(1) = TextBox2.Text
            R(2) = TextBox3.Text
            R(3) = TextBox4.Text
        Else
            R(0) = TextBox1.Text
        End If

        temp.Rows.Add(R)
        writeLines()

        clear()

    End Sub

    Private Sub deselect()

        If DataGridView1.Rows.Count > 0 Then
            For Each c As DataGridViewCell In DataGridView1.SelectedCells
                c.Selected = False
            Next
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If temp Is Nothing Then
            MsgBox("Please Select a Data Table")
            Exit Sub
        ElseIf textbox1.text = "" Then
            MsgBox("Please select row")
            Exit Sub
        Else
            index = DataGridView1.CurrentCell.RowIndex
            DataGridView1.Rows.RemoveAt(index)
            clear()
            index = -1
            writeLines()
        End If

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        Form4.Show()
        Me.Close()

    End Sub
End Class