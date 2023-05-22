Public Class UnpaidConcessionairesReport
    'Functions
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

        MsgBox(billingDate)
        MsgBox(zone)

    End Sub

    Private Sub UnpaidConcessionairesReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadZones()
    End Sub

End Class