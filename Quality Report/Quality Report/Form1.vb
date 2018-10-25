Imports System.IO

Public Class Form1

    'Path to resource files and grab latest backup files
    Dim resourcePath As String = My.Settings.resources

    'Create dt for tracking information
    Dim reportDT As New DataTable
    Dim tempReportDT As New DataTable
    Dim defectVariables As New Dictionary(Of String, Integer)()
    Dim categories() As String
    Dim DPMO As Integer
    Dim percentage As Decimal
    Dim totCount As Integer = 0
    Dim totDef As Integer = 0
    Dim categoryCount As Integer = defectVariables.Count
    Dim defectCount As Integer = 0
    Dim dateOne As String = ""
    Dim dateTwo As String = ""


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Load latest files from resources
        CheckOrLoad()

    End Sub

    Public Sub CheckOrLoad()

        resourcePathTB.Text = My.Settings.resources
        ComboBox1.Items.Add("")
        If Not My.Settings.resources = "" Then

            Dim backUpDirs = Directory.GetDirectories(resourcePath)
            Dim mostRecent As String = "0"
            Dim splitBUD() As String
            Dim mr() As String
            Dim mrTime As Integer = -1
            Dim tempTime As Integer = -1
            Dim tempdate As String = ""
            Dim mrdate As String = ""

            'Select most recent
            For Each backupdir In backUpDirs
                splitBUD = backupdir.Split("\")
                mr = splitBUD(splitBUD.Count - 1).Split(" ")
                tempdate = mr(0)
                tempTime = mr(1).Replace(".", "")

                If mostRecent = "0" Then
                    mostRecent = backupdir
                    mrdate = tempdate
                    mrTime = tempTime
                ElseIf tempdate = mrdate & tempTime > mrTime Then
                    mostRecent = backupdir
                    mrdate = tempdate
                    mrTime = tempTime
                ElseIf tempdate > mrdate Then
                    mostRecent = backupdir
                    mrdate = tempdate
                    mrTime = tempTime
                End If
            Next

            'Select files
            For Each mrFile In Directory.GetFiles(mostRecent)

                'Fill Tent Types
                If mrFile.Contains("prepackKey.txt") Then
                    Dim lines() = File.ReadAllLines(mrFile)
                    For Each line In lines
                        Dim tempLines() As String
                        tempLines = line.Split(",")
                        If Not tentType.Items.Contains(tempLines(3)) Then
                            tentType.Items.Add(tempLines(3))
                        End If
                    Next

                    'Fill reportDT
                ElseIf mrFile.Contains("prepackTracking.txt") Then
                    CreateDT(reportDT)
                    CreateDT(tempReportDT)
                    Dim cols As Integer = reportDT.Columns.Count
                    Dim i As Integer = 0
                    For Each line In File.ReadAllLines(mrFile)
                        Dim R As DataRow = reportDT.NewRow
                        Dim tempLines() As String
                        tempLines = line.Split(",")
                        If tempLines(32) = "Yes" Then
                            Continue For
                        Else
                            While i < cols
                                R(i) = tempLines(i)
                                i += 1
                            End While
                            i = 0
                            reportDT.Rows.Add(R)
                        End If
                    Next
                ElseIf mrFile.Contains("category.txt") Then
                    For Each category As String In File.ReadAllLines(mrFile)
                        defectVariables(category) = 0
                    Next
                ElseIf mrFile.Contains("contracts.txt") Then
                    For Each contract In File.ReadAllLines(mrFile)
                        ComboBox1.Items.Add(contract)
                    Next
                End If
            Next

        Else
            MsgBox("Please enter file path to resource folder")
            resourcePathLockCB.Text = "Unlocked"
            resourcePathLockCB.Checked = False
            resourcePathTB.ReadOnly = False
            saveBut.Enabled = True
            Exit Sub
        End If


        DataGridView1.DataSource = reportDT


    End Sub

    Public Sub CreateDT(dt As DataTable)
        dt.Columns.Clear()
        dt.Rows.Clear()
        Dim dtColumns() As String = {"ID", "Auditor(s)", "Date Completed", "Line Item", "Tent Type", "Contract", "Hold", "Repaired", "Reinspected", "Recut", "Released", "(1)Defect", "(1)Category", "(1)Repaired By", "(1)Repaired Date", "(2)Defect", "(2)Category", "(2)Repaired By", "(2)Repaired Date", "(3)Defect", "(3)Category", "(3)Repaired By", "(3)Repaired Date", "(4)Defect", "(4)Category", "(4)Repaired By", "(4)Repaired Date", "(5)Defect", "(5)Category", "(5)Repaired By", "(5)Repaired Date", "QTY", "Deleted"}
        For Each col In dtColumns
            dt.Columns.Add(col)
        Next
    End Sub


    Private Sub ResourcePathLockCB_Click(sender As Object, e As EventArgs) Handles resourcePathLockCB.Click

        'Lock resource path
        If resourcePathLockCB.Checked = False Then
            resourcePathLockCB.Text = "Unlocked"
            resourcePathLockCB.Checked = False
            resourcePathTB.ReadOnly = False
            saveBut.Enabled = True
        Else
            resourcePathLockCB.Text = "Locked"
            resourcePathLockCB.Checked = True
            resourcePathTB.ReadOnly = True
            saveBut.Enabled = False
        End If

    End Sub

    Private Sub SaveBut_Click(sender As Object, e As EventArgs) Handles saveBut.Click

        'Saves resource path to settings and update resourcePath
        My.Settings.resources = resourcePathTB.Text
        My.Settings.Save()
        resourcePathLockCB.Text = "Locked"
        resourcePathLockCB.Checked = True
        resourcePathTB.ReadOnly = True
        saveBut.Enabled = False
        resourcePath = My.Settings.resources
        CheckOrLoad()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'grab enteries within date range
        tempReportDT.Clear()
        dateOne = DateTimePicker1.Value.ToString("MM/dd/yyyy")
        dateTwo = DateTimePicker2.Value.ToString("MM/dd/yyyy")

        If dateOne > dateTwo Then
            MsgBox("The first date cannot be greater than the second date")
            Exit Sub
        ElseIf tentType.Text = "" Then
            MsgBox("Please select a tent type")
            Exit Sub
        ElseIf Not ComboBox1.Text = "" Then
            For Each row As DataRow In reportDT.Rows
                If CType(row(2).ToString, Date) >= dateOne And CType(row(2).ToString, Date) <= dateTwo And row(4).ToString = tentType.Text And row(5).ToString = ComboBox1.Text Then
                    Dim R As DataRow = tempReportDT.NewRow
                    Dim x As Integer = 0
                    While x < reportDT.Columns.Count - 1
                        R(x) = row(x)
                        x += 1
                    End While
                    tempReportDT.Rows.Add(R)
                End If
            Next

            DataGridView1.DataSource = tempReportDT

        Else
            For Each row As DataRow In reportDT.Rows
                If CType(row(2).ToString, Date) >= dateOne And CType(row(2).ToString, Date) <= dateTwo And row(4).ToString = tentType.Text Then
                    Dim R As DataRow = tempReportDT.NewRow
                    Dim x As Integer = 0
                    While x < reportDT.Columns.Count - 1
                        R(x) = row(x)
                        x += 1
                    End While
                    tempReportDT.Rows.Add(R)
                End If
            Next
        End If

        DataGridView1.DataSource = tempReportDT

        'Calculate 
        CalDPMOandPercentage()
        CountDefectTypes()
        WriteReport()

    End Sub

    Public Sub CalDPMOandPercentage()

        If tempReportDT.Rows.Count = 0 Then
            MsgBox("No data")
            Exit Sub
        End If

        For Each row As DataRow In tempReportDT.Rows
            totCount += 1
            If Not row(11) = "" Then
                totDef += row(31)
            End If
        Next

        'Abe, buddy. No magic numbers.
        percentage = (totDef / totCount) * 100
        percentage = Math.Round(percentage)
        DPMO = (totDef / totCount) * 1000000

    End Sub

    Public Sub CountDefectTypes()
        Dim i As Integer = tempReportDT.Columns.Count
        categoryCount = defectVariables.Count
        For Each row As DataRow In tempReportDT.Rows
            Dim x As Integer = 0
            While x < i
                If defectVariables.ContainsKey(row(x).ToString) Then
                    defectVariables(row(x).ToString) += 1
                    defectCount += 1
                End If
                x += 1
            End While
        Next
    End Sub

    Public Sub WriteReport()

        File.Create(My.Application.Info.DirectoryPath & "\" & tentType.Text & " Defect Report.txt").Close()

        Using writeTempReport As StreamWriter = New StreamWriter(My.Application.Info.DirectoryPath & "\" & tentType.Text & " Defect Report.txt")
            writeTempReport.WriteLine()
            writeTempReport.WriteLine(tentType.Text & "    " & dateOne & " - " & dateTwo)
            writeTempReport.WriteLine()
            writeTempReport.WriteLine("Defective Pieces Found: " & totDef & "      Pieces Checked: " & totCount & "      Defect %: " & percentage & "%")
            writeTempReport.WriteLine()
            writeTempReport.WriteLine("Total Defects Found: " & defectCount)
            writeTempReport.WriteLine()
            writeTempReport.WriteLine("Defect Category Totals")

            For Each category As KeyValuePair(Of String, Integer) In defectVariables
                writeTempReport.WriteLine("{0} {1}", category.Key, category.Value)
            Next

            writeTempReport.WriteLine()

            If RadioButton2.Checked = True Then
                For Each row As DataRow In tempReportDT.Rows
                    If row(11).ToString = "" Then
                        Continue For
                    Else
                        writeTempReport.WriteLine("{0}  {1}  {2}", row(0), row(2), row(3))
                        writeTempReport.WriteLine(" " & row(11) & " | " & row(12))
                        If row(15) = "" Then
                            writeTempReport.WriteLine()
                            Continue For
                        Else
                            writeTempReport.WriteLine(" " & row(15) & " | " & row(16))
                            If row(19) = "" Then
                                writeTempReport.WriteLine()
                                Continue For
                            Else
                                writeTempReport.WriteLine(" " & row(19) & " | " & row(20))
                                If row(19) = "" Then
                                    writeTempReport.WriteLine()
                                    Continue For
                                Else
                                    writeTempReport.WriteLine(" " & row(19) & " | " & row(20))
                                    If row(27) = "" Then
                                        writeTempReport.WriteLine()
                                        Continue For
                                    Else
                                        writeTempReport.WriteLine(" " & row(27) & " | " & row(28))
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next

            Else


                For Each row As DataRow In tempReportDT.Rows
                    writeTempReport.WriteLine("{0}  {1}  {2}", row(0), row(2), row(3))
                    If row(11).ToString = "" Then
                        writeTempReport.WriteLine()
                        Continue For
                    Else
                        writeTempReport.WriteLine(" " & row(11) & " | " & row(12))
                        If row(15) = "" Then
                            writeTempReport.WriteLine()
                            Continue For
                        Else
                            writeTempReport.WriteLine(" " & row(15) & " | " & row(16))
                            If row(19) = "" Then
                                writeTempReport.WriteLine()
                                Continue For
                            Else
                                writeTempReport.WriteLine(" " & row(19) & " | " & row(20))
                                If row(19) = "" Then
                                    writeTempReport.WriteLine()
                                    Continue For
                                Else
                                    writeTempReport.WriteLine(" " & row(19) & " | " & row(20))
                                    If row(27) = "" Then
                                        writeTempReport.WriteLine()
                                        Continue For
                                    Else
                                        writeTempReport.WriteLine(" " & row(27) & " | " & row(28))
                                    End If
                                End If
                            End If
                        End If
                    End If

                Next
            End If


        End Using


        Dim i As Integer = tempReportDT.Columns.Count
        For Each row As DataRow In tempReportDT.Rows
            Dim x As Integer = 0
            While x < i
                If defectVariables.ContainsKey(row(x).ToString) Then
                    defectVariables(row(x).ToString) = 0
                End If
                x += 1
            End While
        Next

        totCount = 0
        totDef = 0
        defectCount = 0

        Process.Start(My.Application.Info.DirectoryPath & "\" & tentType.Text & " Defect Report.txt")

    End Sub

End Class
