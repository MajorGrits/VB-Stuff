'*******************************************************************
'
'       b-PAC 3.0 Component Sample (NamePlt)
'
'       (C)Copyright Brother Industries, Ltd. 2009
'
'*******************************************************************

Public Class Form1
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Private WithEvents btnPrint As System.Windows.Forms.Button
    Private WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbTemplate As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCompany As System.Windows.Forms.TextBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnPrint = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbTemplate = New System.Windows.Forms.ComboBox
        Me.txtCompany = New System.Windows.Forms.TextBox
        Me.txtName = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(32, 160)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(104, 24)
        Me.btnPrint.TabIndex = 1
        Me.btnPrint.Text = "Print label"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(184, 160)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(104, 24)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "Exit"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(16, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 16)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Template"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(16, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 16)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Company"
        '
        'cmbTemplate
        '
        Me.cmbTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTemplate.Items.AddRange(New Object() {"Simple", "Decoration Frame"})
        Me.cmbTemplate.Location = New System.Drawing.Point(97, 12)
        Me.cmbTemplate.Name = "cmbTemplate"
        Me.cmbTemplate.Size = New System.Drawing.Size(199, 20)
        Me.cmbTemplate.TabIndex = 5
        '
        'txtCompany
        '
        Me.txtCompany.Location = New System.Drawing.Point(97, 53)
        Me.txtCompany.Name = "txtCompany"
        Me.txtCompany.Size = New System.Drawing.Size(199, 19)
        Me.txtCompany.TabIndex = 6
        Me.txtCompany.Text = ""
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(97, 99)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(199, 19)
        Me.txtName.TabIndex = 8
        Me.txtName.Text = ""
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(16, 104)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 16)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Name"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 12)
        Me.ClientSize = New System.Drawing.Size(320, 205)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.txtCompany)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbTemplate)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnPrint)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Const TEMPLATE_DIRECTORY As String = "C:\\Program Files\\Brother bPAC3 SDK\\Templates\\"
    Private Const TEMPLATE_SIMPLE As String = "NamePlate1.LBX"
    Private Const TEMPLATE_FRAME As String = "NamePlate2.LBX"

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim templatePath As String = TEMPLATE_DIRECTORY
        If cmbTemplate.SelectedIndex = 0 Then
            templatePath += TEMPLATE_SIMPLE
        Else
            templatePath += TEMPLATE_FRAME
        End If
        Dim doc As bpac.DocumentClass = New bpac.DocumentClass
        If doc.Open(templatePath) <> False Then
            doc.GetObject("objCompany").Text = txtCompany.Text
            doc.GetObject("objName").Text = txtName.Text

            ' doc.SetMediaById(doc.Printer.GetMediaId(), True)
            doc.StartPrint("", bpac.PrintOptionConstants.bpoDefault)
            doc.PrintOut(1, bpac.PrintOptionConstants.bpoDefault)
            doc.EndPrint()
            doc.Close()
        Else
            MsgBox("Open() Error: " + doc.ErrorCode)
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbTemplate.SelectedIndex = 0
    End Sub
End Class
