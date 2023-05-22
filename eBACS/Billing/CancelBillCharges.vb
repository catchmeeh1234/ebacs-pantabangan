Public Class CancelBillCharges
    Private Sub CancelBillCharges_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub txtBillno_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBillno.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True ' Ignore the key press
        End If
    End Sub

    Public Sub txtBillno_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBillno.KeyDown

        'connectionsettings.connection()

        If e.KeyCode = Keys.Enter Then


            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try

            Dim bill_charge_dataTable As New DataTable
            Dim bill_no As Integer = txtBillno.Text

            bill_charge_dataTable.Clear()

            stracs = "select * FROM BillCharges Where BillNumber = " & bill_no

            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(bill_charge_dataTable)

            billchargeslist.Rows.Clear()

            For i = 0 To bill_charge_dataTable.Rows.Count - 1
                billchargeslist.Rows.Add(bill_charge_dataTable(i)("BillNumber"), bill_charge_dataTable(i)("AccountNumber"), bill_charge_dataTable(i)("AccountName"), bill_charge_dataTable(i)("Category"), bill_charge_dataTable(i)("Entry"), bill_charge_dataTable(i)("Particulars"), bill_charge_dataTable(i)("Amount"))

            Next

        End If
    End Sub


    'Functions
    Public Sub clearfields()
        txtBillno.Clear()
        billchargeslist.Rows.Clear()
    End Sub

    Private Sub billPost_Click(sender As Object, e As EventArgs) Handles billPost.Click
        If billchargeslist.Rows.Count > 0 Then

            For x = 0 To billchargeslist.Rows.Count - 1

                If billchargeslist.Rows(x).Cells(7).Value = -1 Then
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                    stracs = "DELETE FROM BillCharges WHERE BillNumber = " & billchargeslist.Rows(x).Cells(0).Value & " AND Particulars LIKE '%membership%'"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                    stracs = "DELETE FROM AccountLedger WHERE ledgerRefNo = " & billchargeslist.Rows(x).Cells(0).Value & " AND ledgerParticulars LIKE '%membership%'"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                End If
            Next
            clearfields()
            MsgBox("Bill charges cancelled.")
        End If


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class