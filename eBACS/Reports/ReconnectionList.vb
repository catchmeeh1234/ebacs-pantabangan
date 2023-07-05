Imports System.Globalization
Imports Microsoft.Reporting
Public Class ReconnectionList
    Private Sub ReconnectionList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain

        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.ZoomMode.PageWidth)
        Me.ReportViewer1.RefreshReport()

        Dim zone As New DataTable
        zone.Clear()
        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        stracs = "select * FROM Zone"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(zone)

        If zone.Rows.Count = 0 Then
        Else
            cbZone.Items.Add("All")
            For t = 0 To zone.Rows.Count - 1
                cbZone.Items.Add(zone.Rows(t)("ZoneName"))
            Next

        End If

        Try
            cbZone.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Public MoveFormReconnectionList As Boolean
    Public MoveFormReconnectionList_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormReconnectionList = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormReconnectionList_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormReconnectionList Then
            Me.Location = Me.Location + (e.Location - MoveFormReconnectionList_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormReconnectionList = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.Reset()

        loadReconnectionList()
    End Sub

    Sub loadReconnectionList()
        Cursor = Cursors.WaitCursor
        prog.Value = 0
        prog.Visible = True
        btnGenerate.Enabled = False



        Dim dt As New DataTable

        With dt
            .Columns.Add("accno")
            .Columns.Add("conces")
            .Columns.Add("meterno")
            .Columns.Add("readingseqno")
            .Columns.Add("billingmonth")
            .Columns.Add("zone")
        End With

        Dim collection_charges As New DataTable

        collection_charges.Clear()

        If acsconn.State = ConnectionState.Closed Then acsconn.Open()

        If cbZone.SelectedIndex = 0 Then
            stracs = "SELECT Zone.ZoneName, Zone.ZoneID, Customers.AccountNo, Collection_Details.AccountName, Customers.MeterNo, Customers.ReadingSeqNo, CollectionBilling.BillingDate, Collection_Details.PaymentDate
                    FROM [CollectionCharges] INNER JOIN [Collection_Details] ON [CollectionCharges].CRNo = [Collection_Details].CRNo 
                    LEFT JOIN Customers ON Customers.AccountNo = Collection_Details.AccountNo LEFT JOIN CollectionBilling ON CollectionBilling.CRNo = Collection_Details.Crno
                    LEFT JOIN Zone ON Customers.Zone = Zone.ZoneName
                    where CollectionCharges.Particulars='Reconnection Fee' and CollectionCharges.Cancelled is null and CAST(Collection_Details.PaymentDate AS DATE) = '" & dtpSelectedDate.Text & "'"
        Else
            stracs = "SELECT Zone.ZoneName, Zone.ZoneID, Customers.AccountNo, Collection_Details.AccountName, Customers.MeterNo, Customers.ReadingSeqNo, CollectionBilling.BillingDate, Collection_Details.PaymentDate
                    FROM [CollectionCharges] INNER JOIN [Collection_Details] ON [CollectionCharges].CRNo = [Collection_Details].CRNo 
                    LEFT JOIN Customers ON Customers.AccountNo = Collection_Details.AccountNo LEFT JOIN CollectionBilling ON CollectionBilling.CRNo = Collection_Details.Crno
                    LEFT JOIN Zone ON Customers.Zone = Zone.ZoneName
                    where CollectionCharges.Particulars='Reconnection Fee' and Customers.Zone='" & cbZone.Text & "' AND CollectionCharges.Cancelled is null and CAST(Collection_Details.PaymentDate AS DATE) = '" & dtpSelectedDate.Text & "'"

        End If

        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(collection_charges)


        If collection_charges.Rows.Count = 0 Then
            MsgBox("No data found")

        Else

            Dim paymentdate As Date


            For j = 0 To collection_charges.Rows.Count - 1
                paymentdate = collection_charges.Rows(j)("PaymentDate")
                dt.Rows.Add(collection_charges.Rows(j)("AccountNo"), collection_charges.Rows(j)("AccountName"), collection_charges.Rows(j)("MeterNo") _
                            , collection_charges.Rows(j)("ReadingSeqNo"), collection_charges.Rows(j)("BillingDate"), Format(collection_charges.Rows(j)("ZoneID"), "00") & " - " & collection_charges.Rows(j)("ZoneName"))
                prog.Value = j / collection_charges.Rows.Count * 100
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
        ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\ReconnectionList.rdlc"
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("asof", dtpSelectedDate.Text))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("zonename", cbZone.Text))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("total", collection_charges.Rows.Count))

        ReportViewer1.ZoomMode = WinForms.ZoomMode.PageWidth
        ReportViewer1.RefreshReport()

        btnGenerate.Enabled = True
        prog.Visible = False
        Cursor = Cursors.Default
    End Sub
End Class