Option Explicit On
Imports System.IO

Module Module1
    Dim answer As String


    Sub Main()

        Dim x As Integer
        Dim stopNum As Integer
        Dim i As Integer
        Dim copies As Integer
        Dim spath As String
        Dim objDoc As bpac.Document
        Dim bRet As Boolean
        Dim ans As String
        Dim one As String = ""
        Dim two As String = ""
        Dim three As String = ""
        Dim c As String()


        objDoc = New bpac.Document
        bRet = objDoc.Open(spath)

        objDoc = New bpac.Document
        bRet = objDoc.Open(spath)

        Console.WriteLine("Do you want to print a string attached to your number? Enter Y/N")
        ans = Console.ReadLine().ToUpper
        If ans = "Y" Then
            spath = My.Application.Info.DirectoryPath & "\count2.lbx"
            Console.WriteLine("Please enter in up to 3 characters seperated by a ,      example: A,B,C")
            ans = Console.ReadLine()
            c = ans.Split(",")
            If c.Count = 1 Then
                one = c(0)
            ElseIf c.Count = 2 Then
                one = c(0)
                two = c(1)
            ElseIf c.Count = 3 Then
                one = c(0)
                two = c(1)
                three = c(2)
            End If
        Else
            spath = My.Application.Info.DirectoryPath & "\count.lbx"
        End If

        Console.WriteLine("What number do you want to start at?")
        x = Console.ReadLine()
        stopNum = x

        Console.WriteLine("How many numbers do you want to print?")
        i = Console.ReadLine()

        Console.WriteLine("How many copies of each number do you need?")
        copies = Console.ReadLine()


        If (objDoc.Open(spath) <> False) Then
            While x < stopNum + i


                If ans = "N" Then
                    objDoc.GetObject("Text2").Text = x
                    objDoc.StartPrint("", bpac.PrintOptionConstants.bpoDefault)
                    objDoc.PrintOut(copies, bpac.PrintOptionConstants.bpoDefault)
                ElseIf Not one = "" And two = "" Then
                    objDoc.GetObject("Text2").Text = x & one
                    objDoc.StartPrint("", bpac.PrintOptionConstants.bpoDefault)
                    objDoc.PrintOut(copies, bpac.PrintOptionConstants.bpoDefault)
                ElseIf Not one = "" And Not two = "" And three = "" Then
                    objDoc.GetObject("Text2").Text = x & one
                    objDoc.StartPrint("", bpac.PrintOptionConstants.bpoDefault)
                    objDoc.PrintOut(copies, bpac.PrintOptionConstants.bpoDefault)

                    objDoc.GetObject("Text2").Text = x & two
                    objDoc.StartPrint("", bpac.PrintOptionConstants.bpoDefault)
                    objDoc.PrintOut(copies, bpac.PrintOptionConstants.bpoDefault)

                ElseIf Not one = "" And Not two = "" And Not three = "" Then
                    objDoc.GetObject("Text2").Text = x & one
                    objDoc.StartPrint("", bpac.PrintOptionConstants.bpoDefault)
                    objDoc.PrintOut(copies, bpac.PrintOptionConstants.bpoDefault)

                    objDoc.GetObject("Text2").Text = x & two
                    objDoc.StartPrint("", bpac.PrintOptionConstants.bpoDefault)
                    objDoc.PrintOut(copies, bpac.PrintOptionConstants.bpoDefault)

                    objDoc.GetObject("Text2").Text = x & three
                    objDoc.StartPrint("", bpac.PrintOptionConstants.bpoDefault)
                    objDoc.PrintOut(copies, bpac.PrintOptionConstants.bpoDefault)

                End If


                x += 1
            End While
            'objDoc.Close()
        End If




    End Sub



End Module
