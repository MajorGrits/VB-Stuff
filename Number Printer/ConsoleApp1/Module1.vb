Option Explicit On
Imports System.IO

Module Module1
    Dim answer As String
    Dim objDoc As bpac.Document

    Sub PrintLabel(ByVal newLabelText As String, ByVal copiesToPrint As Integer)
        objDoc.GetObject("Text2").Text = newLabelText
        objDoc.StartPrint("", bpac.PrintOptionConstants.bpoDefault)
        objDoc.PrintOut(copiesToPrint, bpac.PrintOptionConstants.bpoDefault)
    End Sub


    Sub Main()

        Dim beginNum As Integer
        Dim numsToPrint As Integer
        Dim copies As Integer
        Dim labelTemplatePath As String = ""

        Dim bRet As Boolean
        Dim ans As String
        Dim arg1 As String = ""
        Dim arg2 As String = ""
        Dim arg3 As String = ""
        Dim c As String()


        objDoc = New bpac.Document
        bRet = objDoc.Open(labelTemplatePath)

        Console.WriteLine("Do you want to print a string attached to your number? Enter Y/N")
        ans = Console.ReadLine().ToUpper

        If ans = "Y" Then
            labelTemplatePath = My.Application.Info.DirectoryPath & "\count2.lbx"

            Console.WriteLine("Please enter in up to 3 characters seperated by a , example: A,B,C")
            c = Console.ReadLine().Split(",")

            If c.Count = 1 Then
                arg1 = c(0)
            ElseIf c.Count = 2 Then
                arg1 = c(0)
                arg2 = c(1)
            ElseIf c.Count = 3 Then
                arg1 = c(0)
                arg2 = c(1)
                arg3 = c(2)
            End If
        Else
            labelTemplatePath = My.Application.Info.DirectoryPath & "\count.lbx"
        End If

        Console.WriteLine("What number do you want to start at?")
        beginNum = Console.ReadLine()

        Console.WriteLine("How many numbers do you want to print?")
        numsToPrint = Console.ReadLine() + beginNum

        Console.WriteLine("How many copies of each number do you need?")
        copies = Console.ReadLine()


        If (objDoc.Open(labelTemplatePath) <> False) Then
            While beginNum < numsToPrint
                If ans = "N" Then
                    PrintLabel(beginNum, copies)
                Else
                    If arg1.Length > 0 Then
                        PrintLabel(beginNum & arg1, copies)
                    End If

                    If arg2.Length > 0 Then
                        PrintLabel(beginNum & arg2, copies)
                    End If

                    If arg3.Length > 0 Then
                        PrintLabel(beginNum & arg3, copies)
                    End If

                    beginNum += 1
                End If
            End While
        Else
            Throw New Exception("File not found: " + labelTemplatePath)
        End If

        'Clean up resources
        objDoc.Close()
    End Sub
End Module
