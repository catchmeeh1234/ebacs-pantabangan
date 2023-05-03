Imports Microsoft.Reporting

Public Class ConcessionaireBreakdownbyType
    Private Sub ConcessionaireBreakdownbyType_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub billSearch_Click(sender As Object, e As EventArgs) Handles billSearch.Click
        Cursor = Cursors.WaitCursor

        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.Reset()

        Dim dt As New DataTable

        With dt
            .Columns.Add("zone")
            .Columns.Add("bulkactive")
            .Columns.Add("bulkinactive")
            .Columns.Add("commindactive")
            .Columns.Add("commindinactive")
            .Columns.Add("commaactive")
            .Columns.Add("commainactive")
            .Columns.Add("commbactive")
            .Columns.Add("commbinactive")
            .Columns.Add("commcactive")
            .Columns.Add("commcinactive")
            .Columns.Add("residactive")
            .Columns.Add("residinactive")

        End With

        Dim zonelist As New DataTable

        If cbzone.SelectedIndex = 0 Then

            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            zonelist.Clear()
            stracs = "select Distinct Customers.Zone, Zone.ZoneID from Customers,Zone WHERE Zone.ZoneName = Customers.Zone ORDER by Zone.ZoneID ASC"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(zonelist)

        Else

            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            zonelist.Clear()
            stracs = "select Distinct Customers.Zone from Customers,Zone WHERE Customers.Zone = '" & cbzone.Text & "' AND Zone.ZoneName = Customers.Zone"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(zonelist)
        End If

        If zonelist.Rows.Count = 0 Then
            MsgBox("No data found")
        Else

            For k = 0 To zonelist.Rows.Count - 1
                'MsgBox(zonelist.Rows(k)("Zone"))
                Dim bulkk, commindd, commma, commmb, commmc, residd As Integer

                bulkk = 0
                commindd = 0
                commma = 0
                commmb = 0
                commmc = 0
                residd = 0

                Dim bulkactive As New DataTable
                bulkactive.Clear()
                'bulk wholesale active
                stracs = "select * from Customers WHERE Zone = '" & zonelist.Rows(k)("Zone") & "' AND RateSchedule = 'Bulk/Wholesale' AND (CustomerStatus = 'Don''t Bill' or CustomerStatus = 'Active') AND DateInstalled <= '" & txtasof.Text & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(bulkactive)

                Dim bulk As New DataTable
                bulk.Clear()
                'bulk wholesale 
                stracs = "select * from Customers WHERE Zone = '" & zonelist.Rows(k)("Zone") & "' AND RateSchedule = 'Bulk/Wholesale' AND (CustomerStatus = 'Disconnected' or CustomerStatus = 'Closed') AND DateInstalled <= '" & txtasof.Text & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(bulk)

                If bulk.Rows.Count = 0 Then
                    bulkk = 0
                Else
                    bulkk = bulk.Rows.Count
                End If

                Dim commindactive As New DataTable
                commindactive.Clear()
                'Commercial/Industrial active
                stracs = "select * from Customers WHERE Zone = '" & zonelist.Rows(k)("Zone") & "' AND RateSchedule = 'Commercial/Industrial' AND (CustomerStatus = 'Don''t Bill' or CustomerStatus = 'Active') AND DateInstalled <= '" & txtasof.Text & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(commindactive)

                Dim commind As New DataTable
                commind.Clear()
                'Commercial/Industrial
                stracs = "select * from Customers WHERE Zone = '" & zonelist.Rows(k)("Zone") & "' AND RateSchedule = 'Commercial/Industrial' AND (CustomerStatus = 'Disconnected' or CustomerStatus = 'Closed') AND DateInstalled <= '" & txtasof.Text & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(commind)

                If commind.Rows.Count = 0 Then
                    commindd = 0
                Else
                    commindd = commind.Rows.Count
                End If

                Dim commaactive As New DataTable
                commaactive.Clear()
                'CommercialA active
                stracs = "select * from Customers WHERE Zone = '" & zonelist.Rows(k)("Zone") & "' AND RateSchedule = 'Commercial-A' AND (CustomerStatus = 'Don''t Bill' or CustomerStatus = 'Active') AND DateInstalled <= '" & txtasof.Text & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(commaactive)

                Dim comma As New DataTable
                comma.Clear()
                'CommercialA
                stracs = "select * from Customers WHERE Zone = '" & zonelist.Rows(k)("Zone") & "' AND RateSchedule = 'Commercial-A' AND (CustomerStatus = 'Disconnected' or CustomerStatus = 'Closed') AND DateInstalled <= '" & txtasof.Text & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(comma)

                If comma.Rows.Count = 0 Then
                    commma = 0
                Else
                    commma = comma.Rows.Count
                End If

                Dim commbactive As New DataTable
                commbactive.Clear()
                'CommercialB active
                stracs = "select * from Customers WHERE Zone = '" & zonelist.Rows(k)("Zone") & "' AND RateSchedule = 'Commercial-B' AND (CustomerStatus = 'Don''t Bill' or CustomerStatus = 'Active') AND DateInstalled <= '" & txtasof.Text & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(commbactive)

                Dim commb As New DataTable
                commb.Clear()
                'CommercialB
                stracs = "select * from Customers WHERE Zone = '" & zonelist.Rows(k)("Zone") & "' AND RateSchedule = 'Commercial-B' AND (CustomerStatus = 'Disconnected' or CustomerStatus = 'Closed') AND DateInstalled <= '" & txtasof.Text & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(commb)

                If commb.Rows.Count = 0 Then
                    commmb = 0
                Else
                    commmb = commb.Rows.Count
                End If

                Dim commcactive As New DataTable
                commcactive.Clear()
                'CommercialC active
                stracs = "select * from Customers WHERE Zone = '" & zonelist.Rows(k)("Zone") & "' AND RateSchedule = 'Commercial-C' AND (CustomerStatus = 'Don''t Bill' or CustomerStatus = 'Active') AND DateInstalled <= '" & txtasof.Text & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(commcactive)

                Dim commc As New DataTable
                commc.Clear()
                'CommercialC
                stracs = "select * from Customers WHERE Zone = '" & zonelist.Rows(k)("Zone") & "' AND RateSchedule = 'Commercial-C' AND (CustomerStatus = 'Disconnected' or CustomerStatus = 'Closed') AND DateInstalled <= '" & txtasof.Text & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(commc)

                If commc.Rows.Count = 0 Then
                    commmc = 0
                Else
                    commmc = commc.Rows.Count
                End If

                Dim residactive As New DataTable
                residactive.Clear()
                'Residential active
                stracs = "select * from Customers WHERE Zone = '" & zonelist.Rows(k)("Zone") & "' AND RateSchedule = 'Residential' AND (CustomerStatus = 'Don''t Bill' or CustomerStatus = 'Active') AND DateInstalled <= '" & txtasof.Text & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(residactive)

                Dim resid As New DataTable
                resid.Clear()
                'Residential
                stracs = "select * from Customers WHERE Zone = '" & zonelist.Rows(k)("Zone") & "' AND RateSchedule = 'Residential' AND (CustomerStatus = 'Disconnected' or CustomerStatus = 'Closed') AND DateInstalled <= '" & txtasof.Text & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(resid)

                If resid.Rows.Count = 0 Then
                    residd = 0
                Else
                    residd = resid.Rows.Count
                End If

                dt.Rows.Add(Format(zonelist.Rows(k)("ZoneID"), "00") & " - " & zonelist.Rows(k)("Zone"), bulkactive.Rows.Count, bulkk, commindactive.Rows.Count, commindd _
                        , commaactive.Rows.Count, commma, commbactive.Rows.Count, commmb, commcactive.Rows.Count, commmc _
                        , residactive.Rows.Count, residd)

            Next

        End If


        Dim Curdi As String = My.Application.Info.DirectoryPath
        Dim g As String
        g = Curdi.Replace("bin\Debug", "")


        Dim rds As New Microsoft.Reporting.WinForms.ReportDataSource
        rds.Name = "DataSet1"
        rds.Value = dt


        ReportViewer1.LocalReport.DataSources.Add(rds)
        'ReportViewer1.LocalReport.ReportPath = g & "ConcessionaireBreakdownbyType.rdlc"
        ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\ConcessionaireBreakdownbyType.rdlc"
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("asof", "as of " & txtasof.Value.ToString("dddd") & ", " & txtasof.Value.ToString("MMMM dd, yyyy")))

        ReportViewer1.ZoomMode = WinForms.ZoomMode.PageWidth
        ReportViewer1.RefreshReport()

        Cursor = Cursors.Default
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) 
        Me.Close()
    End Sub

    Private Sub ConcessionaireBreakdownbyType_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Me.Activate()
    End Sub

    Public MoveFormconsb As Boolean
    Public MoveFormconsb_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormconsb = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormconsb_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormconsb Then
            Me.Location = Me.Location + (e.Location - MoveFormconsb_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormconsb = False
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
        Panel1.Click, cbzone.Click, txtasof.Click, billSearch.Click, ReportViewer1.Click ' etc.
        Me.Activate() 'Or Whatever
    End Sub

End Class