Imports Microsoft.Reporting
Public Class CancelledBill

    Private Sub CancelledBill_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        statusbox.Items.Clear()
        statusbox.Items.Add("All")
        statusbox.Items.Add("Posted")
        statusbox.Items.Add("Pending")

        statusbox.Text = "All"

        Me.MdiParent = eBACSmain
        prog.Visible = False
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.ZoomMode.PageWidth)
        Me.ReportViewer1.RefreshReport()

    End Sub

    Private Sub billSearch_Click(sender As Object, e As EventArgs) Handles billSearch.Click

        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.Reset()

        loadadcancelledbill()

    End Sub

    Sub loadadcancelledbill()

        Cursor = Cursors.WaitCursor
        prog.Value = 0
        prog.Visible = True
        billSearch.Enabled = False

        Dim dt As New DataTable
        dt.Clear()

        With dt

            .Columns.Add("refno")
            .Columns.Add("billno")
            .Columns.Add("petsa")
            .Columns.Add("accno")
            .Columns.Add("remarks")
            .Columns.Add("approved")
            .Columns.Add("amountbill")
            .Columns.Add("amountcharge")
            .Columns.Add("totalamount")

        End With

        Dim adjustment As New DataTable

        adjustment.Clear()

        If acsconn.State = ConnectionState.Closed Then acsconn.Open()

        If statusbox.Text = "" Then

            MsgBox("Please select status.")

        Else

            Select Case statusbox.Text

                Case "All"
                    stracs = "Select a.RefNo as RefNo,a.AccountNo as AccountNo,a.BillNo as BillNo,a.Remarks as Remarks,a.CancelledBy as CancelledBy,
                a.DateCancelled as DateCancelled,b.AmountDue as AmountDue,c.Amount as Amount 
                from BillCancelled a join Bills b On a.BillNo = b.BillNo join BillCharges c On b.BillNo = c.BillNumber 
                where MONTH(a.DateCancelled) = '" & ReadingDate.Value.Month & "' and YEAR(a.DateCancelled) = '" & ReadingDate.Value.Year & "' order by RefNo asc"

                Case "Posted"
                    stracs = "Select a.RefNo as RefNo,a.AccountNo as AccountNo,a.BillNo as BillNo,a.Remarks as Remarks,a.CancelledBy as CancelledBy,
                a.DateCancelled as DateCancelled,b.AmountDue as AmountDue,c.Amount as Amount 
                from BillCancelled a join Bills b On a.BillNo = b.BillNo join BillCharges c On b.BillNo = c.BillNumber 
                where MONTH(a.DateCancelled) = '" & ReadingDate.Value.Month & "' and YEAR(a.DateCancelled) = '" & ReadingDate.Value.Year & "' and b.BillStatus = 'Posted' order by RefNo asc"

                Case "Pending"
                    stracs = "Select a.RefNo as RefNo,a.AccountNo as AccountNo,a.BillNo as BillNo,a.Remarks as Remarks,a.CancelledBy as CancelledBy,
                a.DateCancelled as DateCancelled,b.AmountDue as AmountDue,c.Amount as Amount 
                from BillCancelled a join Bills b On a.BillNo = b.BillNo join BillCharges c On b.BillNo = c.BillNumber 
                where MONTH(a.DateCancelled) = '" & ReadingDate.Value.Month & "' and YEAR(a.DateCancelled) = '" & ReadingDate.Value.Year & "' and b.BillStatus = 'Pending' order by RefNo asc"

            End Select

            'stracs = "select * from BillCancelled where BillCovered = '" & ReadingDate.Text & "'"

            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(adjustment)

            '[RefNo]
            ',[AccountNo]
            ',[BillNo]
            ',[Remarks]
            ',[CancelledBy]
            ',[DateCancelled]
            ',[Billcovered]

            If adjustment.Rows.Count = 0 Then
                MsgBox("No data found")
            Else

                For b = 0 To adjustment.Rows.Count - 1

                    Dim oldbill, newbill As Decimal
                    oldbill = 0
                    newbill = 0

                    dt.Rows.Add(adjustment.Rows(b)("RefNo"),
                                adjustment.Rows(b)("BillNo"),
                                Format(adjustment.Rows(b)("DateCancelled"), "short date"),
                                adjustment.Rows(b)("AccountNo"),
                                adjustment.Rows(b)("Remarks"),
                                adjustment.Rows(b)("CancelledBy"),
                                FormatNumber(adjustment.Rows(b)("AmountDue"), UseParensForNegativeNumbers:=TriState.True),
                                FormatNumber(adjustment.Rows(b)("Amount"), UseParensForNegativeNumbers:=TriState.True),
                                FormatNumber(Format(Double.Parse(adjustment.Rows(b)("AmountDue")) + Double.Parse(adjustment.Rows(b)("Amount")), "standard"), UseParensForNegativeNumbers:=TriState.True))
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
            'ReportViewer1.LocalReport.ReportPath = g & "CancelledBill.rdlc"
            ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\CancelledBill.rdlc"
            ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("billingmonth", ReadingDate.Text))

            ReportViewer1.ZoomMode = WinForms.ZoomMode.PageWidth
            ReportViewer1.RefreshReport()

            billSearch.Enabled = True
            prog.Visible = False
            Cursor = Cursors.Default

        End If



    End Sub


End Class