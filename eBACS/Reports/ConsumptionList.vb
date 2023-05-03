Imports Microsoft.Reporting

Public Class ConsumptionList
    Private Sub ConsumptionList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.ZoomMode.PageWidth)
        Me.ReportViewer1.RefreshReport()
        Timer1.Start()
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) 
        Me.Close()
    End Sub

    Private Sub billSearch_Click(sender As Object, e As EventArgs) Handles billSearch.Click

        Cursor = Cursors.WaitCursor
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.Reset()

        Dim dt As New DataTable

        With dt
            .Columns.Add("zone")
            .Columns.Add("classs")
            .Columns.Add("metersize")
            .Columns.Add("billno")
            .Columns.Add("concess")
            .Columns.Add("previouss")
            .Columns.Add("present")
            .Columns.Add("cumused")
            .Columns.Add("address")
            .Columns.Add("meterno")
            .Columns.Add("accno")


        End With




        Dim zonelist, metersize As New DataTable


        If cbzone.SelectedIndex = 0 Then

            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            zonelist.Clear()
            stracs = "select Distinct Zone from Bills WHERE BillingDate = '" & ReadingDate.Text & "' ORDER by Zone ASC"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(zonelist)


        Else

            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            zonelist.Clear()
            stracs = "select Distinct Zone from Bills WHERE Zone = '" & cbzone.Text & "' AND BillingDate = '" & ReadingDate.Text & "'  ORDER by Zone ASC"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(zonelist)


        End If

        For u = 0 To zonelist.Rows.Count - 1

            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Dim ratesched As New DataTable
            ratesched.Clear()
            stracs = "select Distinct RateSchedule from Bills WHERE Zone = '" & zonelist.Rows(u)("Zone") & "' AND BillingDate = '" & ReadingDate.Text & "' ORDER by RateSchedule ASC"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(ratesched)

            For ratesc = 0 To ratesched.Rows.Count - 1

                If cbmetersize.SelectedIndex = 0 Then
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    metersize.Clear()
                    stracs = "select Distinct MeterSize from Bills WHERE Zone = '" & zonelist.Rows(u)("Zone") & "' AND BillingDate = '" & ReadingDate.Text & "' AND RateSchedule = '" & ratesched.Rows(ratesc)("RateSchedule") & "' ORDER by MeterSize ASC"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsda.SelectCommand = acscmd
                    acsda.Fill(metersize)


                Else
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    metersize.Clear()
                    stracs = "select Distinct MeterSize from Bills WHERE Zone = '" & zonelist.Rows(u)("Zone") & "' AND MeterSize = '" & cbmetersize.Text & "' AND BillingDate = '" & ReadingDate.Text & "'AND RateSchedule = '" & ratesched.Rows(ratesc)("RateSchedule") & "' ORDER by MeterSize ASC"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsda.SelectCommand = acscmd
                    acsda.Fill(metersize)
                End If



                For p = 0 To metersize.Rows.Count - 1

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    Dim billdata As New DataTable
                    billdata.Clear()

                    stracs = "select * from Bills,Zone WHERE Bills.Zone = '" & zonelist.Rows(u)("Zone") & "' AND Bills.MeterSize = '" & metersize.Rows(p)("MeterSize") & "' AND Bills.BillingDate = '" & ReadingDate.Text & "' AND NOT Bills.Cancelled = 'Yes' AND Bills.RateSchedule = '" & ratesched.Rows(ratesc)("RateSchedule") & "' AND Zone.ZoneName = Bills.Zone and Billstatus = 'Posted'"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsda.SelectCommand = acscmd
                    acsda.Fill(billdata)


                    If billdata.Rows.Count = 0 Then

                    Else


                        For j = 0 To billdata.Rows.Count - 1

                            If cbselectall.Checked = True Then
                                dt.Rows.Add(Format(billdata.Rows(j)("ZoneID"), "00") & " - " & zonelist.Rows(u)("Zone"), ratesched.Rows(ratesc)("RateSchedule"), metersize.Rows(p)("MeterSize"), billdata.Rows(j)("BillNo"), billdata.Rows(j)("CustomerName"), billdata.Rows(j)("PreviousReading"), billdata.Rows(j)("Reading"), billdata.Rows(j)("Consumption"), billdata.Rows(j)("CustomerAddress"), billdata.Rows(j)("MeterNumber"), billdata.Rows(j)("AccountNumber"))
                            Else


                                If cbresid.Checked = True Then
                                    If billdata.Rows(j)("RateSchedule") = "Residential" Then
                                        dt.Rows.Add(Format(billdata.Rows(j)("ZoneID"), "00") & " - " & zonelist.Rows(u)("Zone"), ratesched.Rows(ratesc)("RateSchedule"), metersize.Rows(p)("MeterSize"), billdata.Rows(j)("BillNo"), billdata.Rows(j)("CustomerName"), billdata.Rows(j)("PreviousReading"), billdata.Rows(j)("Reading"), billdata.Rows(j)("Consumption"), billdata.Rows(j)("CustomerAddress"), billdata.Rows(j)("MeterNumber"), billdata.Rows(j)("AccountNumber"))
                                    End If
                                End If

                                If cbcommInd.Checked = True Then
                                    If billdata.Rows(j)("RateSchedule") = "Commercial/Industrial" Then
                                        dt.Rows.Add(Format(billdata.Rows(j)("ZoneID"), "00") & " - " & zonelist.Rows(u)("Zone"), ratesched.Rows(ratesc)("RateSchedule"), metersize.Rows(p)("MeterSize"), billdata.Rows(j)("BillNo"), billdata.Rows(j)("CustomerName"), billdata.Rows(j)("PreviousReading"), billdata.Rows(j)("Reading"), billdata.Rows(j)("Consumption"), billdata.Rows(j)("CustomerAddress"), billdata.Rows(j)("MeterNumber"), billdata.Rows(j)("AccountNumber"))
                                    End If
                                End If

                                If cbbulk.Checked = True Then
                                    If billdata.Rows(j)("RateSchedule") = "Bulk/Wholesale" Then
                                        dt.Rows.Add(Format(billdata.Rows(j)("ZoneID"), "00") & " - " & zonelist.Rows(u)("Zone"), ratesched.Rows(ratesc)("RateSchedule"), metersize.Rows(p)("MeterSize"), billdata.Rows(j)("BillNo"), billdata.Rows(j)("CustomerName"), billdata.Rows(j)("PreviousReading"), billdata.Rows(j)("Reading"), billdata.Rows(j)("Consumption"), billdata.Rows(j)("CustomerAddress"), billdata.Rows(j)("MeterNumber"), billdata.Rows(j)("AccountNumber"))
                                    End If
                                End If

                                If cbcommA.Checked = True Then
                                    If billdata.Rows(j)("RateSchedule") = "Commercial-A" Then
                                        dt.Rows.Add(Format(billdata.Rows(j)("ZoneID"), "00") & " - " & zonelist.Rows(u)("Zone"), ratesched.Rows(ratesc)("RateSchedule"), metersize.Rows(p)("MeterSize"), billdata.Rows(j)("BillNo"), billdata.Rows(j)("CustomerName"), billdata.Rows(j)("PreviousReading"), billdata.Rows(j)("Reading"), billdata.Rows(j)("Consumption"), billdata.Rows(j)("CustomerAddress"), billdata.Rows(j)("MeterNumber"), billdata.Rows(j)("AccountNumber"))
                                    End If
                                End If

                                If cbcommB.Checked = True Then
                                    If billdata.Rows(j)("RateSchedule") = "Commercial-B" Then
                                        dt.Rows.Add(Format(billdata.Rows(j)("ZoneID"), "00") & " - " & zonelist.Rows(u)("Zone"), ratesched.Rows(ratesc)("RateSchedule"), metersize.Rows(p)("MeterSize"), billdata.Rows(j)("BillNo"), billdata.Rows(j)("CustomerName"), billdata.Rows(j)("PreviousReading"), billdata.Rows(j)("Reading"), billdata.Rows(j)("Consumption"), billdata.Rows(j)("CustomerAddress"), billdata.Rows(j)("MeterNumber"), billdata.Rows(j)("AccountNumber"))
                                    End If
                                End If

                                If cbcommC.Checked = True Then
                                    If billdata.Rows(j)("RateSchedule") = "Commercial-C" Then
                                        dt.Rows.Add(Format(billdata.Rows(j)("ZoneID"), "00") & " - " & zonelist.Rows(u)("Zone"), ratesched.Rows(ratesc)("RateSchedule"), metersize.Rows(p)("MeterSize"), billdata.Rows(j)("BillNo"), billdata.Rows(j)("CustomerName"), billdata.Rows(j)("PreviousReading"), billdata.Rows(j)("Reading"), billdata.Rows(j)("Consumption"), billdata.Rows(j)("CustomerAddress"), billdata.Rows(j)("MeterNumber"), billdata.Rows(j)("AccountNumber"))
                                    End If
                                End If
                            End If

                        Next

                    End If

                Next

            Next


        Next


        Dim Curdi As String = My.Application.Info.DirectoryPath
        Dim g As String
        g = Curdi.Replace("bin\Debug", "")


        Dim rds As New Microsoft.Reporting.WinForms.ReportDataSource
        rds.Name = "DataSet1"
        rds.Value = dt


        ReportViewer1.LocalReport.DataSources.Add(rds)
        'ReportViewer1.LocalReport.ReportPath = g & "ConsumptionList.rdlc"
        ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\ConsumptionList.rdlc"
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("BillMonth", ReadingDate.Text))
        ReportViewer1.ZoomMode = WinForms.ZoomMode.PageWidth
        ReportViewer1.RefreshReport()

        Cursor = Cursors.Default
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

    Private Sub ConsumptionList_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Me.Activate()
    End Sub


    Public MoveFormConsump As Boolean
    Public MoveFormConsump_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormConsump = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormConsump_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormConsump Then
            Me.Location = Me.Location + (e.Location - MoveFormConsump_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormConsump = False
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
        Panel1.Click, ReadingDate.Click, GroupBox1.Click, cbzone.Click, cbmetersize.Click,
        cbselectall.Click, cbresid.Click, cbcommA.Click, cbcommB.Click, cbcommC.Click,
        cbcommInd.Click, cbbulk.Click, billSearch.Click, ReportViewer1.Click ' etc.
        Me.Activate() 'Or Whatever
    End Sub

End Class