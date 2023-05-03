Imports System.Drawing.Printing
Public Class settingsPrinter
    Private Sub settingsPrinter_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Me.MdiParent = eBACSmain
        Dim pkInstalledPrinters As String

        ' Find all printers installed

        orcrprinter.Items.Clear()

        For Each pkInstalledPrinters In
            PrinterSettings.InstalledPrinters
            orcrprinter.Items.Add(pkInstalledPrinters)
        Next pkInstalledPrinters

        orcrprinter.Text = My.Settings.printerORCR

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        My.Settings.printerORCR = orcrprinter.Text
        My.Settings.Save()
        Me.Close()

    End Sub

    Private Sub settingsPrinter_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Me.Activate()
    End Sub

    Public MoveFormPrinter As Boolean
    Public MoveFormPrinter_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormPrinter = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormPrinter_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormPrinter Then
            Me.Location = Me.Location + (e.Location - MoveFormPrinter_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormPrinter = False
            Me.Cursor = Cursors.Default
        End If

    End Sub
End Class