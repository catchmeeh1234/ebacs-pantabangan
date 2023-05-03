Imports Microsoft.Reporting

Public Class SeniorCitizenDiscounts
    Private Sub SeniorCitizenDiscounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain
        prog.Visible = False
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.ZoomMode.PageWidth)
        Me.ReportViewer1.RefreshReport()
    End Sub

    Private Sub billSearch_Click(sender As Object, e As EventArgs) Handles billSearch.Click
        Cursor = Cursors.WaitCursor

        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.Reset()

        loaddata()
        Cursor = Cursors.Default
    End Sub

    Sub loaddata()

        Cursor = Cursors.WaitCursor
        prog.Value = 0
        prog.Visible = True
        billSearch.Enabled = False

        Dim dt As New DataTable

        With dt
            .Columns.Add("zone")
            .Columns.Add("conces")
            .Columns.Add("amount")
            .Columns.Add("discount")

        End With

        Dim bills As New DataTable

        bills.Clear()
        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        stracs = "select * FROM Bills,Zone WHERE Bills.BillingDate = '" & ReadingDate.Text & "' AND Bills.Cancelled = 'No' AND NOT Bills.Discount = '0.00' AND Zone.ZoneName = Bills.Zone"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(bills)

        If bills.Rows.Count = 0 Then
            MsgBox("No data found")
        Else

            For i = 0 To bills.Rows.Count - 1
                dt.Rows.Add(Format(bills.Rows(i)("ZoneID"), "00") & " - " & bills.Rows(i)("Zone"), bills.Rows(i)("CustomerName"), FormatNumber(bills.Rows(i)("AmountDue")), FormatNumber(bills.Rows(i)("Discount")))
                prog.Value = i / bills.Rows.Count * 100
            Next

        End If

        ' prog.Value = t / bills.Rows.Count * 100

        prog.Value = 100

        Dim Curdi As String = My.Application.Info.DirectoryPath
        Dim g As String
        g = Curdi.Replace("bin\Debug", "")

        Dim rds As New Microsoft.Reporting.WinForms.ReportDataSource
        rds.Name = "DataSet1"
        rds.Value = dt

        ReportViewer1.LocalReport.DataSources.Add(rds)
        'ReportViewer1.LocalReport.ReportPath = g & "SeniorCitizenDiscounts.rdlc"
        ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\SeniorCitizenDiscounts.rdlc"
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("billingmonth", ReadingDate.Text))

        ReportViewer1.ZoomMode = WinForms.ZoomMode.PageWidth
        ReportViewer1.RefreshReport()

        billSearch.Enabled = True
        prog.Visible = False
        Cursor = Cursors.Default

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) 
        Me.Close()
    End Sub

    Private Sub SeniorCitizenDiscounts_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Me.Activate()
    End Sub

    Public MoveFormSenior As Boolean
    Public MoveFormSenior_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormSenior = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormSenior_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormSenior Then
            Me.Location = Me.Location + (e.Location - MoveFormSenior_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormSenior = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub postbill_deactivated(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.BackColor = Color.FromArgb(17, 153, 195)
    End Sub

    Private Sub postbill_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.BackColor = Color.SteelBlue
    End Sub

    Private Sub ControlsClick(sender As Object, e As EventArgs) Handles _
        Panel1.Click, ReadingDate.Click, billSearch.Click, ReportViewer1.Click ' etc.
        Me.Activate() 'Or Whatever
    End Sub
End Class