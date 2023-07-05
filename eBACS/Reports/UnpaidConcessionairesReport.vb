Imports Microsoft.Reporting
Public Class UnpaidConcessionairesReport
    'Functions
    Sub loadUnpaidConcessionaires()
        Dim billingDate As String = dtpBillingDate.Text
        Dim zone As String = cmbZone.Text

        Cursor = Cursors.WaitCursor
        prog.Value = 0
        prog.Visible = True
        billSearch.Enabled = False

        Dim dt As New DataTable
        With dt
            .Columns.Add("accountno")
            .Columns.Add("customername")
            .Columns.Add("meterno")
            .Columns.Add("readingseqno")
            .Columns.Add("billingmonth")
        End With

        Dim unpaidConcessionaires As New DataTable

        unpaidConcessionaires.Clear()

        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        stracs = "SELECT *
                FROM Bills JOIN Customers ON Customers.AccountNo = Bills.AccountNumber WHERE IsPaid='No' and BillingDate='" & billingDate & "'
                and Bills.zone='" & zone & "' and IsCollectionCreated='No' and BillStatus='Posted' and CRNo is null and Cancelled='No'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(unpaidConcessionaires)

        If unpaidConcessionaires.Rows.Count = 0 Then
            MsgBox("No data found")
        Else
            dt.Rows.Clear()
            For j = 0 To unpaidConcessionaires.Rows.Count - 1
                dt.Rows.Add(unpaidConcessionaires.Rows(j)("AccountNumber"), unpaidConcessionaires.Rows(j)("CustomerName"), unpaidConcessionaires.Rows(j)("MeterNo"), unpaidConcessionaires.Rows(j)("ReadingSeqNo"), unpaidConcessionaires.Rows(j)("BillingDate"))
                prog.Value = j / unpaidConcessionaires.Rows.Count * 100
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
        'ReportViewer1.LocalReport.ReportPath = g & "DailyCollectionReport.rdlc"
        ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\UnpaidConcessionairesBill.rdlc"
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("asof", dtpBillingDate.Text))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("zonename", cmbZone.Text))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("total", unpaidConcessionaires.Rows.Count))

        ReportViewer1.ZoomMode = WinForms.ZoomMode.PageWidth
        ReportViewer1.RefreshReport()

        billSearch.Enabled = True
        prog.Visible = False
        Cursor = Cursors.Default
    End Sub

    ' Method to merge cells in a DataTable
    Private Sub MergeCells(dataTable As DataTable, startColumn As Integer, mergeColumnCount As Integer, rowIndex As Integer)
        Dim firstCell = dataTable.Rows(rowIndex)(startColumn).ToString()

        ' Merge cells by setting subsequent cells in the range to an empty string
        For i As Integer = 1 To mergeColumnCount
            dataTable.Rows(rowIndex)(startColumn + i) = String.Empty
        Next

        ' Set the value of the first cell in the range
        dataTable.Rows(rowIndex)(startColumn) = firstCell
    End Sub

    Private Sub LoadZones()
        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        sqldataZone.Clear()

        stracs = "SELECT ZoneName FROM Zone"
        acscmd.Connection = acsconn
        acscmd.CommandText = stracs
        acsda.SelectCommand = acscmd
        acsda.Fill(sqldataZone)

        cmbZone.Items.Clear()

        For i = 0 To sqldataZone.Rows.Count - 1
            cmbZone.Items.Add(sqldataZone(i)("ZoneName").ToString.ToUpper)
        Next

        Try
            cmbZone.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadUnpaidBills(selectedZone As String, selectedBilldate As String)
        Dim unpaidBillsTable As New DataTable
        unpaidBillsTable.Clear()

        stracs = "SELECT * FROM Bills WHERE IsPaid='No' and Zone='" & selectedZone & "' and BillingDate='" & selectedBilldate & "'"
        acscmd.Connection = acsconn
        acscmd.CommandText = stracs
        acsda.SelectCommand = acscmd
        acsda.Fill(unpaidBillsTable)

        For i = 0 To unpaidBillsTable.Rows.Count - 1
            cmbZone.Items.Add(unpaidBillsTable(i)("ZoneName").ToString.ToUpper)
        Next
    End Sub
    'end functions


    Private Sub billSearch_Click(sender As Object, e As EventArgs) Handles billSearch.Click
        Dim billingDate As String = dtpBillingDate.Text
        Dim zone As String = cmbZone.Text

        If zone = "" Then
            MsgBox("Please select a zone")
            Exit Sub
        End If
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.Reset()

        loadUnpaidConcessionaires()
    End Sub

    Private Sub UnpaidConcessionairesReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain

        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.ZoomMode.PageWidth)
        Me.ReportViewer1.RefreshReport()

        LoadZones()
    End Sub

End Class