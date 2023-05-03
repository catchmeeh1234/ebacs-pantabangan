Imports AForge
Imports AForge.Video
Imports AForge.Video.DirectShow
Imports System.IO

Imports System.Drawing
Imports System.Drawing.Imaging

Public Class Camera



    Public cropBitmap As Bitmap
    Public cropX, cropY, cropWidth, cropHeight As Integer
    Public cropPen As Pen
    Public croppensize As Integer = 2
    Public cropDashStyle As Drawing2D.DashStyle = Drawing2D.DashStyle.Solid
    Public cropPenColor As Color = Color.Aquamarine
    Public c As Cursor


    Dim cameras As VideoCaptureDeviceForm = New VideoCaptureDeviceForm


    Dim cameraa As VideoCaptureDevice

    Dim bmp As Bitmap
    Private Sub Camera_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        'Dim di As DirectoryInfo = New DirectoryInfo("C:\ProgramData\TempPicture")
        'Try
        '    ' Determine whether the directory exists.
        '    If di.Exists Then
        '        ' Indicate that it already exists.
        '        MsgBox("That path exists already.")

        '    Else
        '        MsgBox("Meron na.")
        '    End If

        '    '' Try to create the directory.
        '    'di.Create()
        '    'Console.WriteLine("The directory was created successfully.")

        '    '' Delete the directory.
        '    'di.Delete()
        '    'Console.WriteLine("The directory was deleted successfully.")

        'Catch ex As Exception
        '    Console.WriteLine("The process failed: {0}", e.ToString())
        'End Try

        If cameras.ShowDialog() = DialogResult.OK Then
            cameraa = cameras.VideoDevice
            AddHandler cameraa.NewFrame, New NewFrameEventHandler(AddressOf Captured)

            cameraa.Start()

        End If

        txtPath.Text = My.Settings.TempPicPath

    End Sub

    Private Sub Captured(sender As Object, eventArgs As NewFrameEventArgs)


        bmp = DirectCast(eventArgs.Frame.Clone(), Bitmap)
        pbimage.Image = DirectCast(eventArgs.Frame.Clone(), Bitmap)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Camera_Closed(sender As Object, e As EventArgs) Handles Me.Closed

        Try
            cameraa.Stop()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btncapture_Click(sender As Object, e As EventArgs) Handles btncapture.Click

        Try
            System.IO.File.Delete(Application.StartupPath.ToString & "\pictemp\" & eBACSmain.lblUserName.Text & ".jpg")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Try

            If pbimage.Image IsNot Nothing Then

                pbimage.Image.Save(Application.StartupPath.ToString & "\pictemp\" & eBACSmain.lblUserName.Text & ".jpg")

            End If

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

        Try

            cameraa.Stop()

        Catch ex As Exception

        End Try

        Try

            Dim img As Image
            Dim br As New IO.BinaryReader(IO.File.Open(Application.StartupPath.ToString & "\pictemp\" & eBACSmain.lblUserName.Text & ".jpg", IO.FileMode.Open), System.Text.Encoding.Default)
            img = Image.FromStream(br.BaseStream)
            br.Close()
            br = Nothing
            'customerinfo.concespic.Image = img.Clone

            'Me.Close()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btndone_Click(sender As Object, e As EventArgs) Handles btndone.Click

        Try
            System.IO.File.Delete(Application.StartupPath.ToString & "\pictemp\" & eBACSmain.lblUserName.Text & ".jpg")
        Catch ex As Exception

        End Try

        Try
            If pbcrop.Image IsNot Nothing Then

                pbcrop.Image.Save(Application.StartupPath.ToString & "\pictemp\" & eBACSmain.lblUserName.Text & ".jpg")

            Else
                MsgBox("No data")
            End If
        Catch ex As Exception

        End Try

        Try
            Dim img As Image
            Dim br As New IO.BinaryReader(IO.File.Open(Application.StartupPath.ToString & "\pictemp\" & eBACSmain.lblUserName.Text & ".jpg", IO.FileMode.Open), System.Text.Encoding.Default)
            img = Image.FromStream(br.BaseStream)
            br.Close()
            br = Nothing
            customerinfo.concespic.Image = img.Clone
            customerinfo.picpath = Application.StartupPath.ToString & "\pictemp\" & eBACSmain.lblUserName.Text & ".jpg"
            Me.Close()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnretake_Click(sender As Object, e As EventArgs) Handles btnretake.Click
        cameraa.Start()
    End Sub

    Private Sub TextBox1_DoubleClick(sender As Object, e As EventArgs) Handles txtPath.DoubleClick

        'Dim FolderBrowserDialog1 As New FolderBrowserDialog
        'If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
        '    txtPath.Text = FolderBrowserDialog1.SelectedPath
        'End If

        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments

        If (OpenFileDialog.ShowDialog(eBACSmain) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName
            'My.Settings.TempPicPath = FileName
            'MsgBox(My.Settings.TempPicPath)
            'My.Settings.Save()
            'MsgBox("The application will close to apply changes.")
            'eBACSmain.Close()

            'Path.GetDirectoryName(OpenFileDialog.FileName)
            txtPath.Text = Path.GetDirectoryName(OpenFileDialog.FileName)
            My.Settings.TempPicPath = txtPath.Text
            MsgBox(My.Settings.TempPicPath)
            My.Settings.Save()

        Else
            MsgBox("Database not configured.")
        End If


    End Sub

    Private Sub pbimage_MouseDown(sender As Object, e As MouseEventArgs) Handles pbimage.MouseDown
        Try

            If e.Button = MouseButtons.Left Then
                cropX = e.X
                cropY = e.Y
                cropPen = New Pen(cropPenColor, croppensize)
                cropPen.DashStyle = cropDashStyle
                Cursor = Cursors.Cross

            End If
            pbimage.Refresh()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub pbimage_MouseUp(sender As Object, e As MouseEventArgs) Handles pbimage.MouseUp
        Try
            Dim rect As Rectangle = New Rectangle(cropX, cropY, cropWidth, cropHeight)
            Dim bit As Bitmap = New Bitmap(pbimage.Image, pbimage.Width, pbimage.Height)
            cropBitmap = New Bitmap(cropWidth, cropHeight)
            Dim g As Graphics = Graphics.FromImage(cropBitmap)
            g.DrawImage(bit, 0, 0, rect, GraphicsUnit.Pixel)
            pbcrop.Image = cropBitmap
            Cursor = Cursors.Default
        Catch ex As Exception

        End Try
    End Sub

    Private Sub pbimage_MouseMove(sender As Object, e As MouseEventArgs) Handles pbimage.MouseMove
        Try
            If pbimage.Image Is Nothing Then Exit Sub
            If e.Button = MouseButtons.Left Then
                pbimage.Refresh()
                cropWidth = e.X - cropX
                cropHeight = e.Y - cropY
                pbimage.CreateGraphics().DrawRectangle(cropPen, cropX, cropY, cropWidth, cropHeight)

            End If
            GC.Collect()

        Catch ex As Exception

        End Try
    End Sub
End Class