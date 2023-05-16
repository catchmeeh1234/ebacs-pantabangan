Imports System.Data.SqlClient

Module connectionsettings

    Public stracs As String
    Public acsconn As New SqlConnection
    Public acscmd As New SqlCommand
    Public acsda As New SqlDataAdapter
    Public acsdr As SqlDataReader
    Public sqlData1, sqldatacons, sqldataSenior, sqldataBillno, sqldatastatus, sqldataAccounts As New DataTable
    Public sqldatasearch, sqldataBilling, sqldataZone, sqldataClass, sqldatasize, sqlDatacharges, sqlDatahistory, sqldataDiscount As New DataTable

    Public Sub connection()

        'acsconn.ConnectionString = "Data source = " & My.Settings.dbServerIp & "; database=eBACS; user id=sa;password = p@$$w0rd;"
        'acsconn.ConnectionString = "Data source = 192.168.1.79; database=eBACS; user id=sa;password = p@$$w0rd;"

        'acsconn.ConnectionString = "Data source = " & My.Settings.dbServerIp & "; database=eBACS; user id=sa;password = p@$$w0rd;"
        acsconn.ConnectionString = "Data source = " & My.Settings.dbServerIp & "; database=pantabangan; user id=sa;password = p@$$w0rd;"
        'acsconn.ConnectionString = "Data source = " & My.Settings.dbServerIp & "; database=eBACS; user id=" & My.Settings.dbID & ";password = " & My.Settings.dbPassword & ";"

        Try

            acsconn.Open()

        Catch ex As Exception

            Dim servername As String = InputBox("Enter Server Name/Server IP Address.", "Update Server Settings")

            If servername = "" Then

            Else

                My.Settings.dbServerIp = servername
                My.Settings.Save()

                Application.Exit()

            End If

        End Try

    End Sub

    Public Sub transferreading()

        stracs = "Update Bills set Bills.Reading= Billstemp.Reading, Bills.Consumption=Billstemp.Consumption, 
                  Bills.AmountDue=Billstemp.AmountDue, Bills.Discount=Billstemp.Discount, Bills.ReadingDate=Billstemp.ReadingDate, 
                  Bills.DueDate=Billstemp.DueDate, Bills.LastDayNOPen=Billstemp.LastDayNOPen, Bills.DiscDate=Billstemp.DiscDate 
				  from Bills join Billstemp on Bills.BillNo = Billstemp.BillNo 
                  where Bills.Cancelled = 'No' and Billstemp.Reading >= 0 and Billstemp.AmountDue <> 0 and Bills.BillStatus = 'Pending'"

        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acscmd.ExecuteNonQuery()
        acscmd.Dispose()

    End Sub


End Module
