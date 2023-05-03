Imports Microsoft.Reporting

Public Class CancelledReceipts
    Private Sub CancelledReceipts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain
        prog.Visible = False
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.ZoomMode.PageWidth)
        Me.ReportViewer1.RefreshReport()


        Dim office As New DataTable
        office.Clear()
        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        stracs = "select Distinct Office FROM Collection_Details"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(office)

        If office.Rows.Count = 0 Then
        Else

            For t = 0 To office.Rows.Count - 1
                cboffice.Items.Add(office.Rows(t)("Office"))
            Next

        End If

        Try
            cboffice.SelectedIndex = 0
        Catch ex As Exception

        End Try
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

        Dim monthh, yearr As Integer

        monthh = dtpfrom.Value.ToString("MM")
        yearr = dtpfrom.Value.ToString("yyyy")

        Dim dt As New DataTable

        With dt
            .Columns.Add("crno")
            .Columns.Add("datee")
            .Columns.Add("accno")
            .Columns.Add("conces")
            .Columns.Add("amount")
            .Columns.Add("collector")

        End With

        'Cancelled CR

        Dim collectiondata As New DataTable

        collectiondata.Clear()

        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        stracs = "select * FROM Collection_Details WHERE Office  = '" & cboffice.Text & "' AND cast(PaymentDate as date)  BETWEEN '" & dtpfrom.Text & "' and '" & dtpto.Text & "' AND Cancelled = 'Yes'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(collectiondata)

        If collectiondata.Rows.Count = 0 Then

        Else

            Dim paymentdate As Date


            For j = 0 To collectiondata.Rows.Count - 1
                paymentdate = collectiondata.Rows(j)("PaymentDate")
                dt.Rows.Add(collectiondata.Rows(j)("CRNo"), paymentdate.ToString("MM/dd/yyyy"), collectiondata.Rows(j)("AccountNo") _
                            , collectiondata.Rows(j)("AccountName"), FormatNumber(collectiondata.Rows(j)("TotalAmountDue")), collectiondata.Rows(j)("Collector"))
                prog.Value = j / collectiondata.Rows.Count * 100
            Next
        End If


        'Cancelled OR

        Dim ordata As New DataTable

        ordata.Clear()

        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        stracs = "select * FROM OR_Details WHERE Office  = '" & cboffice.Text & "' AND cast(PaymentDate as date)  BETWEEN '" & dtpfrom.Text & "' and '" & dtpto.Text & "' AND Cancelled = 'Yes'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(ordata)

        If ordata.Rows.Count = 0 Then


        Else

            Dim paymentdate As Date


            For j = 0 To ordata.Rows.Count - 1
                paymentdate = ordata.Rows(j)("PaymentDate")
                dt.Rows.Add(ordata.Rows(j)("ORNo"), paymentdate.ToString("MM/dd/yyyy"), ordata.Rows(j)("AccountNo") _
                            , ordata.Rows(j)("AccountName"), FormatNumber(ordata.Rows(j)("TotalAmountDue")), ordata.Rows(j)("Collector"))
                prog.Value = j / ordata.Rows.Count * 100
            Next
        End If

        dt.DefaultView.Sort = "datee ASC"
        dt = dt.DefaultView.ToTable


        'Dim designation As New DataTable
        'Dim desig As String
        'desig = ""
        'designation.Clear()
        'If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        'stracs = "select designation FROM useraccounts WHERE fullname = '" & My.Settings.Fullname & "'"
        'acscmd.CommandText = stracs
        'acscmd.Connection = acsconn
        'acsda.SelectCommand = acscmd
        'acsda.Fill(designation)

        'If designation.Rows.Count = 0 Then
        'Else
        '    desig = designation.Rows(0)("designation")
        'End If


        ' prog.Value = t / bills.Rows.Count * 100

        prog.Value = 100

        Dim Curdi As String = My.Application.Info.DirectoryPath
        Dim g As String
        g = Curdi.Replace("bin\Debug", "")

        Dim rds As New Microsoft.Reporting.WinForms.ReportDataSource
        rds.Name = "DataSet1"
        rds.Value = dt

        ReportViewer1.LocalReport.DataSources.Add(rds)
        'ReportViewer1.LocalReport.ReportPath = g & "CancelledReceipts.rdlc"
        ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\CancelledReceipts.rdlc"
        If dtpto.Text = dtpfrom.Text Then
            ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("billingmonth", dtpfrom.Value.ToString("dddd, MM-dd-yyyy")))
        Else
            ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("billingmonth", dtpfrom.Value.ToString("dddd, MM-dd-yyyy") & " - " & dtpto.Value.ToString("dddd, MM-dd-yyyy")))
        End If
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("user", My.Settings.Fullname))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("designation", My.Settings.Designation))


        ReportViewer1.ZoomMode = WinForms.ZoomMode.PageWidth
        ReportViewer1.RefreshReport()

        billSearch.Enabled = True
        prog.Visible = False
        Cursor = Cursors.Default

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) 
        Me.Close()
    End Sub

    Private Sub CancelledReceipts_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) 
        Me.Activate()
    End Sub

    Public MoveFormCancelledCR As Boolean
    Public MoveFormCancelledCR_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormCancelledCR = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormCancelledCR_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormCancelledCR Then
            Me.Location = Me.Location + (e.Location - MoveFormCancelledCR_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormCancelledCR = False
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
        Panel1.Click, dtpfrom.Click, dtpto.Click, cboffice.Click, billSearch.Click, ReportViewer1.Click ' etc.
        Me.Activate() 'Or Whatever
    End Sub
End Class