Imports Microsoft.Reporting

Public Class Disconnected
    Private Sub Disconnected_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain

        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.ZoomMode.PageWidth)
        Me.ReportViewer1.RefreshReport()

        Dim zoness As New DataTable
        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        zoness.Clear()

        stracs = "select ZoneName from Zone"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(zoness)

        cbzone.Items.Clear()
        cbzone.Items.Add("All")
        If zoness.Rows.Count = 0 Then
        Else

            For x = 0 To zoness.Rows.Count - 1

                cbzone.Items.Add(zoness(x)("ZoneName"))

            Next

        End If

        cbzone.SelectedIndex = 0
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub billSearch_Click(sender As Object, e As EventArgs) Handles billSearch.Click
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.Reset()
        billSearch.Enabled = False
        loaddata()
    End Sub

    Sub loaddata()
        Cursor = Cursors.WaitCursor
        prog.Value = 0
        prog.Visible = True

        Dim dt As New DataTable

        With dt
            .Columns.Add("AccountNo")
            .Columns.Add("StatusDate")
            .Columns.Add("Status")
            .Columns.Add("StatusType")
            .Columns.Add("MeterStatus")
            .Columns.Add("LastReading")
            .Columns.Add("Remarks")
            .Columns.Add("DiscBy")
            .Columns.Add("Accname")
            .Columns.Add("zone")


        End With

        Dim disconnected As New DataTable
        disconnected.Clear()

        If acsconn.State = ConnectionState.Closed Then acsconn.Open()

        If cbzone.SelectedIndex = 0 Then
            stracs = "SELECT AccountStatus.AccountNo,
                    AccountStatus.StatusDate,
                    AccountStatus.Status,
                    AccountStatus.StatusType,
                    AccountStatus.MeterStatus,
                    AccountStatus.LastReading,
                    AccountStatus.Remarks,
                    AccountStatus.DiscBy,
                    Customers.Lastname,
                    Customers.Firstname,
                    Customers.Middlename,
                    Customers.Zone,
                    Zone.ZoneID
                    FROM AccountStatus, Customers, Zone
                    WHERE AccountStatus.Status = 'Disconnected' and AccountStatus.StatusDate <= '" & dtpasof.Text & "' AND Customers.AccountNo = AccountStatus.AccountNo AND Zone.ZoneName = Customers.Zone"
            'WHERE Customers.CustomerStatus = 'Disconnected' and Customers.DateLastDisconnected <= '" & dtpasof.Text & "' AND Customers.AccountNo = AccountStatus.AccountNo AND Zone.ZoneName = Customers.Zone"
            'WHERE AccountStatus.Status = 'Disconnected' and AccountStatus.StatusDate <= '" & dtpasof.Text & "' AND Customers.AccountNo = AccountStatus.AccountNo AND Zone.ZoneName = Customers.Zone"
        Else

            stracs = "SELECT AccountStatus.AccountNo,
                    AccountStatus.StatusDate,
                    AccountStatus.Status,
                    AccountStatus.StatusType,
                    AccountStatus.MeterStatus,
                    AccountStatus.LastReading,
                    AccountStatus.Remarks,
                    AccountStatus.DiscBy,
                    Customers.Lastname,
                    Customers.Firstname,
                    Customers.Middlename,
                    Customers.Zone,
                    Zone.ZoneID
                    FROM AccountStatus, Customers, Zone 
                    WHERE AccountStatus.Status = 'Disconnected' and AccountStatus.StatusDate <= '" & dtpasof.Text & "' AND Customers.AccountNo = AccountStatus.AccountNo AND Customers.Zone = '" & cbzone.Text & "' AND Zone.ZoneName = Customers.Zone"

        End If

        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(disconnected)

        If disconnected.Rows.Count = 0 Then

        Else
            For u = 0 To disconnected.Rows.Count - 1

                dt.Rows.Add(disconnected.Rows(u)("AccountNo"), disconnected.Rows(u)("StatusDate"), disconnected.Rows(u)("Status") _
                            , disconnected.Rows(u)("StatusType"), disconnected.Rows(u)("MeterStatus"), disconnected.Rows(u)("LastReading") _
                            , disconnected.Rows(u)("Remarks"), disconnected.Rows(u)("DiscBy"), disconnected.Rows(u)("Lastname") & " " & disconnected.Rows(u)("Firstname") & " " & disconnected.Rows(u)("Middlename") _
                            , Format(disconnected.Rows(u)("ZoneID"), "00") & " - " & disconnected.Rows(u)("Zone"))

                prog.Value = u / disconnected.Rows.Count * 100
            Next

        End If





        prog.Value = 100

        Dim Curdi As String = My.Application.Info.DirectoryPath
        Dim g As String
        g = Curdi.Replace("bin\Debug", "")

        Dim rds As New Microsoft.Reporting.WinForms.ReportDataSource
        rds.Name = "DataSet1"
        rds.Value = dt

        ReportViewer1.LocalReport.DataSources.Add(rds)
        'ReportViewer1.LocalReport.ReportPath = g & "Disconnected.rdlc"
        ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Disconnected.rdlc"


        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("asof", "As of " & dtpasof.Value.ToString("dddd") & ", " & dtpasof.Value.ToString("MMMM dd, yyyy")))

        If cbzone.SelectedIndex = 0 Then
            ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("zone", "All Zone"))
        Else
            ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("zone", cbzone.Text))
        End If



        ReportViewer1.ZoomMode = WinForms.ZoomMode.PageWidth
        ReportViewer1.RefreshReport()


        billSearch.Enabled = True
        prog.Visible = False
        Cursor = Cursors.Default

    End Sub


    Public MoveFormDisc As Boolean
    Public MoveFormDisc_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormDisc = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormDisc_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormDisc Then
            Me.Location = Me.Location + (e.Location - MoveFormDisc_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormDisc = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Me.Activate()
    End Sub

    Private Sub Disconnected_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub postbill_deactivated(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.BackColor = Color.FromArgb(17, 153, 195)
    End Sub

    Private Sub postbill_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.BackColor = Color.SteelBlue
    End Sub

    Private Sub ControlsClick(sender As Object, e As EventArgs) Handles _
        Panel1.Click, cbzone.Click, dtpasof.Click, billSearch.Click, ReportViewer1.Click ' etc.
        Me.Activate() 'Or Whatever
    End Sub
End Class