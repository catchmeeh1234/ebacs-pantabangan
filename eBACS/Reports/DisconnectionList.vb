Imports Microsoft.Reporting

Public Class DisconnectionList
    Private Sub DisconnectionList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
            .Columns.Add("zone")
            .Columns.Add("accno")
            .Columns.Add("conces")
            .Columns.Add("meterno")
            .Columns.Add("balance")
            .Columns.Add("noofmonths")

        End With
        Dim gtotal As Decimal
        gtotal = 0

        Dim bills As New DataTable
        bills.Clear()

        If cbzone.SelectedIndex = 0 Then

            If acsconn.State = ConnectionState.Closed Then acsconn.Open()

            ''          stracs = "select  a.[Zone],
            ''c.ZoneID,
            ''c.ZoneID,
            ''a.AccountNumber,
            ''a.CustomerName,
            ''a.MeterNumber,
            ''(sum(a.AmountDue) + sum(a.PenaltyAfterDue)) - (sum(a.advancepayment) + sum(a.discount)) as amoundue,
            ''billothers =  (select sum(p.Billing) + sum(p.Penalty) from AddAdjustment p where p.AccountNumber = a.AccountNumber),
            ''billchargerss = (select sum(Amount) from billcharges b where b.AccountNumber = a.AccountNumber and IsPaid = 'No' and Cancelled = 'No'),
            ''count(a.accountNumber) as bilang
            ''      from Bills a join [eBACS].[dbo].[Zone] c on c.ZoneName= a.[Zone] join Customers d on d.AccountNo = a.AccountNumber 
            ''      where a.IsPaid = 'No' AND a.Cancelled = 'No' AND a.DueDate <= '" & Format(dtpasof.Value, "yyyy-MM-dd") & "' and NOT d.CustomerStatus ='Disconnected'  
            ''      group by a.AccountNumber, a.[Zone],c.ZoneID,a.CustomerName,a.MeterNumber,d.ReadingSeqNo order by c.ZoneID, d.ReadingSeqNo asc"

            'stracs = "select  a.[Zone],
            '      c.ZoneID,
            '      a.AccountNumber,
            '      a.CustomerName,
            '      d.MeterNo,
            '      (sum(a.AmountDue) + sum(a.PenaltyAfterDue) + sum(a.Adjustment)) - (sum(a.advancepayment) + sum(a.discount)) as amoundue,
            '      billchargerss = (select sum(Amount) from billcharges b where b.AccountNumber = a.AccountNumber and IsPaid = 'No' and Cancelled = 'No'),
            '      billothers =  (select sum(p.Billing) + sum(p.Penalty) from AddAdjustment p where p.AccountNumber = a.AccountNumber and p.Paid = 'No' and p.IsCollectionCreated = 'No'), 
            '      count(a.accountNumber) as bilang
            '     from Bills a join [eBACS].[dbo].[Zone] c on c.ZoneName= a.[Zone] join Customers d on d.AccountNo = a.AccountNumber where a.billStatus = 'Posted' and a.IsPaid = 'No' AND a.Cancelled = 'No' and a.isPromisorry = 'No' AND a.DueDate <= '" & Format(dtpasof.Value, "yyyy-MM-dd") & "' and NOT d.CustomerStatus ='Disconnected' group by a.AccountNumber, a.[Zone],c.ZoneID,a.CustomerName,d.MeterNo,d.ReadingSeqNo order by c.ZoneID, d.ReadingSeqNo asc"
            'acscmd.CommandText = stracs
            'acscmd.Connection = acsconn
            'acsda.SelectCommand = acscmd
            'acsda.Fill(bills)

            stracs = "select  a.[Zone],
                  c.ZoneID,
                  a.AccountNumber,
                  CustomerName = (SELECT ([Firstname] + ' ' + [Middlename]  + ' ' + [Lastname]) as fullname FROM [eBACS].[dbo].[Customers] WHERE AccountNo = a.AccountNumber),
                  CompanyName = (SELECT CompanyName FROM [eBACS].[dbo].[Customers] WHERE AccountNo = a.AccountNumber),
                  d.MeterNo,
                  (sum(a.AmountDue) + sum(a.PenaltyAfterDue) + sum(a.Adjustment)) - (sum(a.advancepayment) + sum(a.discount)) as amoundue,
                  billchargerss = (select sum(Amount) from billcharges b where b.AccountNumber = a.AccountNumber and IsPaid = 'No' and Cancelled = 'No'),
                  billothers =  (select sum(p.Billing) + sum(p.Penalty) from AddAdjustment p where p.AccountNumber = a.AccountNumber and p.Paid = 'No' and p.IsCollectionCreated = 'No'), 
                  count(a.accountNumber) as bilang
                 from Bills a join [eBACS].[dbo].[Zone] c on c.ZoneName= a.[Zone] join Customers d on d.AccountNo = a.AccountNumber where a.billStatus = 'Posted' and a.IsPaid = 'No' AND a.Cancelled = 'No' and a.isPromisorry = 'No' AND a.DiscDate <= '" & Format(dtpasof.Value, "yyyy-MM-dd") & "' and NOT d.CustomerStatus ='Disconnected' group by a.AccountNumber, a.[Zone],c.ZoneID,d.MeterNo,d.ReadingSeqNo order by c.ZoneID, d.ReadingSeqNo asc"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(bills)

        Else

            If acsconn.State = ConnectionState.Closed Then acsconn.Open()

            ''          stracs = "select  a.[Zone],
            ''c.ZoneID,
            ''a.AccountNumber,
            ''a.CustomerName,
            ''a.MeterNumber,
            ''(sum(a.AmountDue) + sum(a.PenaltyAfterDue)) - (sum(a.advancepayment) + sum(a.discount)) as amoundue,
            ''billothers =  (select sum(p.Billing) + sum(p.Penalty) from AddAdjustment p where p.AccountNumber = a.AccountNumber),
            ''billchargerss = (select sum(Amount) from billcharges b where b.AccountNumber = a.AccountNumber and IsPaid = 'No' and Cancelled = 'No'),
            ''count(a.accountNumber) as bilang
            ''      from Bills a join [eBACS].[dbo].[Zone] c on c.ZoneName= a.[Zone] join Customers d on d.AccountNo = a.AccountNumber 
            ''      where a.IsPaid = 'No' AND a.Cancelled = 'No' AND a.DueDate <= '" & Format(dtpasof.Value, "yyyy-MM-dd") & "' and a.Zone = '" & cbzone.Text & "' and NOT d.CustomerStatus ='Disconnected'  
            ''      group by a.AccountNumber, a.[Zone],c.ZoneID,a.CustomerName,a.MeterNumber,d.ReadingSeqNo order by c.ZoneID, d.ReadingSeqNo asc"

            'stracs = "select  a.[Zone],
            '      c.ZoneID,
            '      a.AccountNumber,
            '      a.CustomerName,
            '      d.MeterNo,
            '      (sum(a.AmountDue) + sum(a.PenaltyAfterDue) + sum(a.Adjustment)) - (sum(a.advancepayment) + sum(a.discount)) as amoundue,
            '      billchargerss = (select sum(Amount) from billcharges b where b.AccountNumber = a.AccountNumber and IsPaid = 'No' and Cancelled = 'No'),
            '      billothers =  (select sum(p.Billing) + sum(p.Penalty) from AddAdjustment p where p.AccountNumber = a.AccountNumber and p.Paid = 'No' and p.IsCollectionCreated = 'No'),
            '      count(a.accountNumber) as bilang
            '        from Bills a join [eBACS].[dbo].[Zone] c on c.ZoneName= a.[Zone] join Customers d on d.AccountNo = a.AccountNumber where BillStatus = 'Posted' and a.IsPaid = 'No' AND a.Cancelled = 'No' and a.isPromisorry = 'No' AND a.DueDate <= '" & Format(dtpasof.Value, "yyyy-MM-dd") & "' and a.Zone = '" & cbzone.Text & "' and NOT d.CustomerStatus ='Disconnected' group by a.AccountNumber, a.[Zone],c.ZoneID,a.CustomerName,d.MeterNo,d.ReadingSeqNo order by c.ZoneID, d.ReadingSeqNo asc"
            'acscmd.CommandText = stracs
            'acscmd.Connection = acsconn
            'acsda.SelectCommand = acscmd
            'acsda.Fill(bills)

            stracs = "select  a.[Zone],
                  c.ZoneID,
                  a.AccountNumber,
                  CustomerName = (SELECT ([Firstname] + ' ' + [Middlename]  + ' ' + [Lastname]) as fullname FROM [eBACS].[dbo].[Customers] WHERE AccountNo = a.AccountNumber),
                  CompanyName = (SELECT CompanyName FROM [eBACS].[dbo].[Customers] WHERE AccountNo = a.AccountNumber),
                  d.MeterNo,
                  (sum(a.AmountDue) + sum(a.PenaltyAfterDue) + sum(a.Adjustment)) - (sum(a.advancepayment) + sum(a.discount)) as amoundue,
                  billchargerss = (select sum(Amount) from billcharges b where b.AccountNumber = a.AccountNumber and IsPaid = 'No' and Cancelled = 'No'),
                  billothers =  (select sum(p.Billing) + sum(p.Penalty) from AddAdjustment p where p.AccountNumber = a.AccountNumber and p.Paid = 'No' and p.IsCollectionCreated = 'No'),
                  count(a.accountNumber) as bilang
                    from Bills a join [eBACS].[dbo].[Zone] c on c.ZoneName= a.[Zone] join Customers d on d.AccountNo = a.AccountNumber where BillStatus = 'Posted' and a.IsPaid = 'No' AND a.Cancelled = 'No' and a.isPromisorry = 'No' AND a.DiscDate <= '" & Format(dtpasof.Value, "yyyy-MM-dd") & "' and a.Zone = '" & cbzone.Text & "' and NOT d.CustomerStatus ='Disconnected' group by a.AccountNumber, a.[Zone],c.ZoneID,d.MeterNo,d.ReadingSeqNo order by c.ZoneID, d.ReadingSeqNo asc"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(bills)

        End If

        If bills.Rows.Count = 0 Then
            MsgBox("No data found")
        Else

            For m = 0 To bills.Rows.Count - 1

                Dim billcharge As Decimal
                Dim billothers As Decimal

                If (IsDBNull(bills.Rows(m)("billchargerss")) = True) Then
                    billcharge = 0
                Else
                    billcharge = bills.Rows(m)("billchargerss")
                End If

                If (IsDBNull(bills.Rows(m)("billothers")) = True) Then
                    billothers = 0
                Else
                    billothers = bills.Rows(m)("billothers")
                End If

                Dim accountname As String

                If IsDBNull(bills.Rows(m)("CompanyName")) = True Or bills.Rows(m)("CompanyName") = "" Then
                    accountname = bills.Rows(m)("CustomerName")
                Else
                    accountname = bills.Rows(m)("CompanyName")
                End If


                dt.Rows.Add(Format(bills.Rows(m)("ZoneID"), "00") & " - " & bills.Rows(m)("Zone"), bills.Rows(m)("AccountNumber"), accountname, bills.Rows(m)("MeterNo"), FormatNumber(bills.Rows(m)("amoundue") + billcharge + billothers), bills.Rows(m)("bilang"))
                gtotal = gtotal + bills.Rows(m)("amoundue") + billcharge + billothers

                prog.Value = m / bills.Rows.Count * 100
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
        'ReportViewer1.LocalReport.ReportPath = g & "DisconnectionList.rdlc"
        ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\DisconnectionList.rdlc"
        If cbzone.SelectedIndex = 0 Then
            ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("zone", "All Zones"))
        Else

            If bills.Rows.Count = 0 Then
                ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("zone", cbzone.Text))
            Else
                ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("zone", Format(bills.Rows(0)("ZoneID"), "00") & " - " & cbzone.Text))
            End If

        End If
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("asof", "As of " & dtpasof.Value.ToString("dddd") & ", " & dtpasof.Value.ToString("MMMM dd, yyyy")))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("gtotal", FormatNumber(gtotal)))


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

    Private Sub DisconnectionList_Click(sender As Object, e As EventArgs) Handles Me.Click
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