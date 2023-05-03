Imports Microsoft.Reporting

Public Class MasterListofServiceConnections
    Private Sub MasterListofServiceConnections_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.MdiParent = eBACSmain

        Timer1.Start()

        cbsenior.SelectedIndex = 0
        cbstatus.SelectedIndex = 0

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

        Dim metersize As New DataTable
        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        metersize.Clear()

        stracs = "select Distinct MeterSize from RateSchedules"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(metersize)

        cbmetersize.Items.Clear()
        cbmetersize.Items.Add("All")
        If metersize.Rows.Count = 0 Then
        Else

            For r = 0 To metersize.Rows.Count - 1

                cbmetersize.Items.Add(metersize(r)("MeterSize"))

            Next

        End If

        cbmetersize.SelectedIndex = 0
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If cbselectall.Checked = True Then
            cbresid.Enabled = False
            cbcommInd.Enabled = False
            cbbulk.Enabled = False
            cbcommA.Enabled = False
            cbcommB.Enabled = False
            cbcommC.Enabled = False
        Else
            cbresid.Enabled = True
            cbcommInd.Enabled = True
            cbbulk.Enabled = True
            cbcommA.Enabled = True
            cbcommB.Enabled = True
            cbcommC.Enabled = True
        End If
    End Sub

    Private Sub cbselectall_CheckedChanged(sender As Object, e As EventArgs) Handles cbselectall.CheckedChanged
        If cbselectall.Checked = True Then
            cbresid.Checked = True
            cbcommInd.Checked = True
            cbbulk.Checked = True
            cbcommA.Checked = True
            cbcommB.Checked = True
            cbcommC.Checked = True
        Else
            cbresid.Checked = False
            cbcommInd.Checked = False
            cbbulk.Checked = False
            cbcommA.Checked = False
            cbcommB.Checked = False
            cbcommC.Checked = False
        End If
    End Sub

    Sub loadreport()

        Cursor = Cursors.WaitCursor
        Dim dt As New DataTable

        With dt

            .Columns.Add("zone")
            .Columns.Add("accno")
            .Columns.Add("concess")
            .Columns.Add("address")
            .Columns.Add("classs")
            .Columns.Add("seqno")
            .Columns.Add("meterno")
            .Columns.Add("dateinstall")
            .Columns.Add("senior")
            .Columns.Add("status")
            .Columns.Add("mtrsize")
            .Columns.Add("lastmeterreading")

        End With

        Dim count As Integer
        Dim sqlstring As String

        count = 0
        sqlstring = "SELECT * FROM Customers, Zone"

        If cbzone.SelectedIndex = 0 Then

        Else

            If sqlstring.Contains("WHERE") Then
                sqlstring = sqlstring & " Customers.Zone = '" & cbzone.Text & "'"

            Else
                sqlstring = sqlstring & " WHERE Customers.Zone = '" & cbzone.Text & "'"
            End If

        End If

        If cbmetersize.SelectedIndex = 0 Then

        Else
            If sqlstring.Contains("WHERE") Then
                sqlstring = sqlstring & " AND Customers.MeterSize = '" & cbmetersize.Text & "'"
            Else
                sqlstring = sqlstring & " WHERE Customers.MeterSize = '" & cbmetersize.Text & "'"
            End If
        End If

        If cbsenior.SelectedIndex = 0 Then

        Else
            If sqlstring.Contains("WHERE") Then
                sqlstring = sqlstring & " AND Customers.IsSenior = '" & cbsenior.Text & "'"
            Else
                sqlstring = sqlstring & " WHERE Customers.IsSenior = '" & cbsenior.Text & "'"
            End If
        End If

        If cbstatus.SelectedIndex = 0 Then

        Else
            If sqlstring.Contains("WHERE") Then
                sqlstring = sqlstring & " AND Customers.CustomerStatus = '" & cbstatus.Text.Replace("'", "''") & "'"
            Else
                sqlstring = sqlstring & " WHERE Customers.CustomerStatus = '" & cbstatus.Text.Replace("'", "''") & "'"
            End If
        End If

        If sqlstring.Contains("WHERE") Then
            sqlstring = sqlstring & " AND Zone.ZoneName = Customers.Zone ORDER by Customers.Lastname, Customers.Firstname, Customers.Middlename, Cast(Zone.ZoneID AS INT), Customers.ReadingSeqNo"
        Else
            sqlstring = sqlstring & " WHERE Zone.ZoneName = Customers.Zone ORDER by Customers.Lastname, Customers.Firstname, Customers.Middlename, Cast(Zone.ZoneID AS INT), Customers.ReadingSeqNo"
        End If

        Dim masterlist As New DataTable
        masterlist.Clear()

        If acsconn.State = ConnectionState.Closed Then acsconn.Open()

        acscmd.CommandText = sqlstring
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(masterlist)


        If masterlist.Rows.Count = 0 Then
            MsgBox("No data found")
        Else

            For j = 0 To masterlist.Rows.Count - 1

                Dim accountname As String

                If masterlist.Rows(j)("CompanyName") <> "" Then
                    accountname = masterlist.Rows(j)("CompanyName")
                Else
                    accountname = masterlist.Rows(j)("Lastname") _
                        & " " & masterlist.Rows(j)("Firstname") & " " & masterlist.Rows(j)("Middlename")
                End If

                If cbselectall.Checked = True Then
                    dt.Rows.Add(Format(masterlist.Rows(j)("ZoneID"), "00") & " - " & masterlist.Rows(j)("Zone"), masterlist.Rows(j)("AccountNo"), accountname _
                        , masterlist.Rows(j)("ServiceAddress"), masterlist.Rows(j)("RateSchedule") _
                        , masterlist.Rows(j)("ReadingSeqNo"), masterlist.Rows(j)("MeterNo") _
                        , Format(Date.Parse(masterlist.Rows(j)("DateInstalled")), "yyyy-MM-dd"), masterlist.Rows(j)("IsSenior") _
                        , masterlist.Rows(j)("CustomerStatus"), masterlist.Rows(j)("MeterSize"), masterlist.Rows(j)("LastMeterReading"))
                    count = count + 1
                Else

                    If cbresid.Checked = True Then
                        If masterlist.Rows(j)("RateSchedule") = "Residential" Then
                            dt.Rows.Add(masterlist.Rows(j)("Zone"), masterlist.Rows(j)("AccountNo"), accountname _
                            , masterlist.Rows(j)("ServiceAddress"), masterlist.Rows(j)("RateSchedule") _
                            , masterlist.Rows(j)("ReadingSeqNo"), masterlist.Rows(j)("MeterNo") _
                            , Format(Date.Parse(masterlist.Rows(j)("DateInstalled")), "yyyy-MM-dd"), masterlist.Rows(j)("IsSenior") _
                            , masterlist.Rows(j)("CustomerStatus"), masterlist.Rows(j)("MeterSize"), masterlist.Rows(j)("LastMeterReading"))
                            count = count + 1
                        End If
                    End If

                    If cbcommInd.Checked = True Then
                        If masterlist.Rows(j)("RateSchedule") = "Commercial/Industrial" Then
                            dt.Rows.Add(masterlist.Rows(j)("Zone"), masterlist.Rows(j)("AccountNo"), accountname _
                            , masterlist.Rows(j)("ServiceAddress"), masterlist.Rows(j)("RateSchedule") _
                            , masterlist.Rows(j)("ReadingSeqNo"), masterlist.Rows(j)("MeterNo") _
                            , Format(Date.Parse(masterlist.Rows(j)("DateInstalled")), "yyyy-MM-dd"), masterlist.Rows(j)("IsSenior") _
                            , masterlist.Rows(j)("CustomerStatus"), masterlist.Rows(j)("MeterSize"), masterlist.Rows(j)("LastMeterReading"))
                            count = count + 1
                        End If
                    End If

                    If cbbulk.Checked = True Then
                        If masterlist.Rows(j)("RateSchedule") = "Bulk/Wholesale" Then
                            dt.Rows.Add(masterlist.Rows(j)("Zone"), masterlist.Rows(j)("AccountNo"), accountname _
                            , masterlist.Rows(j)("ServiceAddress"), masterlist.Rows(j)("RateSchedule") _
                            , masterlist.Rows(j)("ReadingSeqNo"), masterlist.Rows(j)("MeterNo") _
                            , Format(Date.Parse(masterlist.Rows(j)("DateInstalled")), "yyyy-MM-dd"), masterlist.Rows(j)("IsSenior") _
                            , masterlist.Rows(j)("CustomerStatus"), masterlist.Rows(j)("MeterSize"), masterlist.Rows(j)("LastMeterReading"))
                            count = count + 1
                        End If
                    End If

                    If cbcommA.Checked = True Then
                        If masterlist.Rows(j)("RateSchedule") = "Commercial-A" Then
                            dt.Rows.Add(masterlist.Rows(j)("Zone"), masterlist.Rows(j)("AccountNo"), accountname _
                            , masterlist.Rows(j)("ServiceAddress"), masterlist.Rows(j)("RateSchedule") _
                            , masterlist.Rows(j)("ReadingSeqNo"), masterlist.Rows(j)("MeterNo") _
                            , Format(Date.Parse(masterlist.Rows(j)("DateInstalled")), "yyyy-MM-dd"), masterlist.Rows(j)("IsSenior") _
                            , masterlist.Rows(j)("CustomerStatus"), masterlist.Rows(j)("MeterSize"), masterlist.Rows(j)("LastMeterReading"))
                            count = count + 1
                        End If
                    End If

                    If cbcommB.Checked = True Then
                        If masterlist.Rows(j)("RateSchedule") = "Commercial-B" Then
                            dt.Rows.Add(masterlist.Rows(j)("Zone"), masterlist.Rows(j)("AccountNo"), accountname _
                            , masterlist.Rows(j)("ServiceAddress"), masterlist.Rows(j)("RateSchedule") _
                            , masterlist.Rows(j)("ReadingSeqNo"), masterlist.Rows(j)("MeterNo") _
                            , Format(Date.Parse(masterlist.Rows(j)("DateInstalled")), "yyyy-MM-dd"), masterlist.Rows(j)("IsSenior") _
                            , masterlist.Rows(j)("CustomerStatus"), masterlist.Rows(j)("MeterSize"), masterlist.Rows(j)("LastMeterReading"))
                            count = count + 1
                        End If
                    End If

                    If cbcommC.Checked = True Then
                        If masterlist.Rows(j)("RateSchedule") = "Commercial-C" Then
                            dt.Rows.Add(masterlist.Rows(j)("Zone"), masterlist.Rows(j)("AccountNo"), accountname _
                            , masterlist.Rows(j)("ServiceAddress"), masterlist.Rows(j)("RateSchedule") _
                            , masterlist.Rows(j)("ReadingSeqNo"), masterlist.Rows(j)("MeterNo") _
                            , Format(Date.Parse(masterlist.Rows(j)("DateInstalled")), "yyyy-MM-dd"), masterlist.Rows(j)("IsSenior") _
                            , masterlist.Rows(j)("CustomerStatus"), masterlist.Rows(j)("MeterSize"), masterlist.Rows(j)("LastMeterReading"))
                            count = count + 1
                        End If
                    End If

                End If

            Next

        End If

        Dim Curdi As String = My.Application.Info.DirectoryPath
        Dim g As String
        g = Curdi.Replace("bin\Debug", "")


        Dim rds As New Microsoft.Reporting.WinForms.ReportDataSource
        rds.Name = "DataSet1"
        rds.Value = dt


        ReportViewer1.LocalReport.DataSources.Add(rds)
        'ReportViewer1.LocalReport.ReportPath = g & "MasterListofServiceConnetion.rdlc"
        ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\MasterListofServiceConnetion.rdlc"

        If cbstatus.SelectedIndex = 0 Then
            ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("statuss", "Status: ALL"))
        Else
            ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("statuss", "Status: " & cbstatus.Text))
        End If

        If cbsenior.SelectedIndex = 0 Then
            ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("senior", "IsSenior: Yes/No"))
        Else
            ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("senior", "IsSenior: " & cbsenior.Text))
        End If

        If cbzone.SelectedIndex = 0 Then
            ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("zone", "Zone: ALL"))
        Else
            ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("zone", "Zone: " & cbzone.Text))
        End If


        If cbmetersize.SelectedIndex = 0 Then
            ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("metersize", "Meter Size: ALL"))
        Else
            ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("metersize", "Meter Size: " & cbmetersize.Text))
        End If


        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("asof", "Date Printed " & Date.Now.ToString("dddd") & ", " & Date.Now.ToString("MMMM dd, yyyy")))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("totalcount", count))
        ReportViewer1.ZoomMode = WinForms.ZoomMode.PageWidth
        ReportViewer1.RefreshReport()


        Cursor = Cursors.Default
    End Sub

    Private Sub billSearch_Click(sender As Object, e As EventArgs) Handles billSearch.Click
        Cursor = Cursors.WaitCursor

        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.Reset()
        loadreport()

        Cursor = Cursors.Default

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) 
        Me.Close()
    End Sub

    Public MoveFormmaster As Boolean
    Public MoveFormMaster_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormmaster = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormMaster_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormmaster Then
            Me.Location = Me.Location + (e.Location - MoveFormMaster_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormmaster = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub MasterListofServiceConnections_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Me.Activate()
    End Sub

    Private Sub postbill_deactivated(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.BackColor = Color.FromArgb(17, 153, 195)
    End Sub

    Private Sub postbill_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.BackColor = Color.SteelBlue
    End Sub

    Private Sub ControlsClick(sender As Object, e As EventArgs) Handles _
        Panel1.Click, cbzone.Click, cbmetersize.Click, cbsenior.Click, cbstatus.Click, GroupBox1.Click,
        cbselectall.Click, cbresid.Click, cbcommA.Click, cbcommB.Click, cbcommC.Click,
        cbcommInd.Click, cbbulk.Click, billSearch.Click, ReportViewer1.Click ' etc.
        Me.Activate() 'Or Whatever
    End Sub

End Class