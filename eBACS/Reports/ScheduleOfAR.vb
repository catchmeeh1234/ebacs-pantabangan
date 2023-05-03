Imports Microsoft.Reporting

Public Class ScheduleOfAR
    Private Sub ScheduleOfAR_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.MdiParent = eBACSmain

        prog.Visible = False
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.ZoomMode.PageWidth)
        Me.ReportViewer1.RefreshReport()


        cbstatus.SelectedIndex = 0
    End Sub

    Private Sub billSearch_Click(sender As Object, e As EventArgs) Handles billSearch.Click
        Cursor = Cursors.WaitCursor

        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.Reset()
        If Label7.Text = "Schedule of A/R - Water" Then
            loadarwater()
        Else
            loadarothers()
        End If

        Cursor = Cursors.Default
    End Sub

    Sub loadarwater()

        Cursor = Cursors.WaitCursor
        prog.Value = 0
        prog.Visible = True
        billSearch.Enabled = False

        Dim dt As New DataTable

        With dt
            .Columns.Add("zone")
            .Columns.Add("accno")
            .Columns.Add("accname")
            .Columns.Add("address")
            .Columns.Add("noofmonths")
            .Columns.Add("amount")

        End With


        Dim gtotal As Decimal
        gtotal = 0

        Dim bills As New DataTable
        bills.Clear()


        If cbstatus.SelectedIndex = 0 Then

            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            stracs = "SELECT 
						
                        a.Zone,
                        b.zoneID,
                        a.AccountNumber,
                        a.CustomerName,
                        a.CustomerAddress,
                        count(a.AccountNumber) as ilan,
                        sum(a.Amountdue) - sum(a.Discount) - sum(a.AdvancePayment) as amountdue
      
                        FROM Bills a join Zone b on a.Zone =  b.ZoneName where isPromisorry = 'No' and a.Cancelled = 'No' and (a.datepaid is null OR a.DatePaid > '" & dtpasof.Text & "') and a.ReadingDate <= '" & dtpasof.Text & "' group by a.Zone, a.AccountNumber, a.CustomerName, a.CustomerAddress,b.zoneID order by b.ZoneID ASC"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(bills)

        Else

            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            stracs = "SELECT 
                       
                        a.Zone,
                        b.zoneID,
                        a.AccountNumber,
                        a.CustomerName,
                        a.CustomerAddress,
                        count(a.AccountNumber) as ilan,
                        sum(a.Amountdue) - sum(a.Discount) - sum(a.AdvancePayment) as amountdue
      
                        FROM Bills a join Zone b on a.Zone =  b.ZoneName join Customers c on a.AccountNumber = c.AccountNo where a.isPromisorry = 'No' and a.Cancelled = 'No' and (a.datepaid is null OR a.DatePaid > '" & dtpasof.Text & "') and a.ReadingDate <= '" & dtpasof.Text & "' and c.CustomerStatus = '" & cbstatus.Text.Replace("'", "''") & "' group by a.Zone, a.AccountNumber, a.CustomerName, a.CustomerAddress,b.zoneID order by b.ZoneID ASC"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(bills)
        End If




        If bills.Rows.Count = 0 Then
        Else

            For t = 0 To bills.Rows.Count - 1

                Dim balance As Decimal
                balance = 0

                balance = balance + Decimal.Parse(bills.Rows(t)("amountdue"))

                'If bills.Rows(t)("AccountNumber") = "33-00138-1" Then
                '    MsgBox("Eto yung sa bills")
                'End If

                dt.Rows.Add(Format(bills.Rows(t)("zoneID"), "00") & " - " & bills.Rows(t)("Zone"), bills.Rows(t)("AccountNumber"), bills.Rows(t)("CustomerName"), bills.Rows(t)("CustomerAddress"), bills.Rows(t)("ilan"), FormatNumber(balance))
                gtotal = gtotal + balance

                prog.Value = t / bills.Rows.Count * 100
            Next
        End If



        'PN  
        Dim pn As New DataTable
        pn.Clear()
        If cbstatus.SelectedIndex = 0 Then

            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            stracs = "select AddAdjustment.AccountNumber, AddAdjustment.DateCreated,AddAdjustment.AccountName,AddAdjustment.RefNo,AddAdjustment.Billing,AddAdjustment.Penalty,Zone.ZoneID,Customers.[Zone],Customers.ServiceAddress
                    FROM AddAdjustment,Customers,Zone WHERE  (AddAdjustment.DatePaid is null or AddAdjustment.DatePaid > '" & dtpasof.Text & "') and AddAdjustment.DateRead <= '" & dtpasof.Text & "' AND Customers.AccountNo = AddAdjustment.AccountNumber
                    and Zone.ZoneName = Customers.[Zone]"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(pn)

        Else


            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            stracs = "select AddAdjustment.AccountNumber, AddAdjustment.DateCreated,AddAdjustment.AccountName,AddAdjustment.RefNo,AddAdjustment.Billing,AddAdjustment.Penalty,Zone.ZoneID,Customers.[Zone],Customers.ServiceAddress
                    FROM AddAdjustment,Customers,Zone WHERE  (AddAdjustment.DatePaid is null or AddAdjustment.DatePaid > '" & dtpasof.Text & "') and AddAdjustment.DateRead <= '" & dtpasof.Text & "' AND Customers.AccountNo = AddAdjustment.AccountNumber
                    and Customers.CustomerStatus = '" & cbstatus.Text.Replace("'", "''") & "' and Zone.ZoneName = Customers.[Zone]"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(pn)

        End If

        If pn.Rows.Count = 0 Then
        Else

            For tt = 0 To pn.Rows.Count - 1

                Dim balance As Decimal
                balance = 0

                balance = balance + Decimal.Parse(pn.Rows(tt)("Billing"))

                'If pn.Rows(tt)("AccountNumber") = "33-00138-1" Then
                '    MsgBox("Eto yung sa PN")
                'End If

                dt.Rows.Add(Format(pn.Rows(tt)("zoneID"), "00") & " - " & pn.Rows(tt)("Zone"), pn.Rows(tt)("AccountNumber"), pn.Rows(tt)("AccountName"), pn.Rows(tt)("ServiceAddress"), "PN", FormatNumber(balance))
                gtotal = gtotal + balance

                prog.Value = tt / pn.Rows.Count * 100
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
        'ReportViewer1.LocalReport.ReportPath = g & "ScheduleofAR.rdlc"
        ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\ScheduleofAR.rdlc"
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("asof", "As of " & dtpasof.Value.ToString("dddd") & ", " & dtpasof.Value.ToString("MMMM dd, yyyy")))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("header", Label7.Text))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("gtotal", FormatNumber(gtotal)))

        ReportViewer1.ZoomMode = WinForms.ZoomMode.PageWidth
        ReportViewer1.RefreshReport()

        billSearch.Enabled = True
        prog.Visible = False
        Cursor = Cursors.Default

    End Sub

    Sub loadarothers()

        Cursor = Cursors.WaitCursor
        prog.Value = 0
        prog.Visible = True
        billSearch.Enabled = False

        Dim dt As New DataTable

        With dt
            .Columns.Add("zone")
            .Columns.Add("accno")
            .Columns.Add("accname")
            .Columns.Add("address")
            .Columns.Add("noofmonths")
            .Columns.Add("amount")

        End With

        Dim gtotal As Decimal
        gtotal = 0

        Dim bills As New DataTable
        bills.Clear()

        If cbstatus.SelectedIndex = 0 Then

            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            stracs = "SELECT 
						
                    a.Zone,
                    b.zoneID,
                    a.AccountNumber,
                    a.CustomerName,
                    a.CustomerAddress,
                    count(a.AccountNumber) as ilan,
                    sum(d.Amount) as amountdue
      
                    FROM Bills a join [dbo].[Zone] b on a.[Zone] =  b.ZoneName join BillCharges d on a.BillNo = d.BillNumber where a.IsPaid = 'No' and a.Cancelled = 'No' and (a.datepaid is null OR a.DatePaid > '" & dtpasof.Text & "') and a.ReadingDate <= '" & dtpasof.Text & "' group by a.[Zone], a.AccountNumber, a.CustomerName, a.CustomerAddress,b.zoneID order by b.ZoneID asc"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(bills)

        Else
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            stracs = "SELECT 
						
                    a.Zone,
                    b.zoneID,
                    a.AccountNumber,
                    a.CustomerName,
                    a.CustomerAddress,
                    count(a.AccountNumber) as ilan,
                    sum(d.Amount) as amountdue
      
                    FROM Bills a join [dbo].[Zone] b on a.[Zone] =  b.ZoneName join Customers c on a.AccountNumber = c.AccountNo join BillCharges d on a.BillNo = d.BillNumber where a.IsPaid = 'No' and a.Cancelled = 'No' and (a.datepaid is null OR a.DatePaid > '" & dtpasof.Text & "') and a.ReadingDate <= '" & dtpasof.Text & "' and c.CustomerStatus = '" & cbstatus.Text.Replace("'", "''") & "'  group by a.[Zone], a.AccountNumber, a.CustomerName, a.CustomerAddress,b.zoneID order by b.ZoneID asc"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(bills)
        End If

        If bills.Rows.Count = 0 Then
        Else

            For t = 0 To bills.Rows.Count - 1

                Dim balance As Decimal
                balance = 0

                balance = balance + Decimal.Parse(bills.Rows(t)("amountdue"))

                dt.Rows.Add(Format(bills.Rows(t)("zoneID"), "00") & " - " & bills.Rows(t)("Zone"), bills.Rows(t)("AccountNumber"), bills.Rows(t)("CustomerName"), bills.Rows(t)("CustomerAddress"), bills.Rows(t)("ilan"), FormatNumber(balance))
                gtotal = gtotal + balance
                prog.Value = t / bills.Rows.Count * 100


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
        'ReportViewer1.LocalReport.ReportPath = g & "ScheduleofAR.rdlc"
        ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\ScheduleofAR.rdlc"
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("asof", "As of " & Date.Now.ToString("dddd") & ", " & Date.Now.ToString("MMMM dd, yyyy")))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("header", Label7.Text))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("gtotal", FormatNumber(gtotal)))

        ReportViewer1.ZoomMode = WinForms.ZoomMode.PageWidth
        ReportViewer1.RefreshReport()

        billSearch.Enabled = True
        prog.Visible = False
        Cursor = Cursors.Default

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) 
        Me.Close()
    End Sub

    Public MoveFormAR As Boolean
    Public MoveFormAR_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormAR = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormAR_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormAR Then
            Me.Location = Me.Location + (e.Location - MoveFormAR_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormAR = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub ScheduleOfAR_Click(sender As Object, e As EventArgs) Handles Me.Click
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
        Panel1.Click, dtpasof.Click, cbstatus.Click, billSearch.Click, ReportViewer1.Click ' etc.
        Me.Activate() 'Or Whatever
    End Sub

End Class