Public Class updateLedger
    Private Sub updateLedger_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        loadupdates()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            balance.Clear()
            remarks.Clear()
            lblmode.Text = "Create New"
            lblmode.ForeColor = Color.Green
            lblmode.Show()

            remarks.ReadOnly = False
            balance.ReadOnly = False
        Else
            MsgBox("Your account cannot perform this process.")
        End If


    End Sub

    Public Sub loadupdates()

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try
        Dim loadchanges As New DataTable

        stracs = "select * from UpdateLedger where AccountNo = '" & accountNo.Text & "'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(loadchanges)

        records.Rows.Clear()

        For i = 0 To loadchanges.Rows.Count - 1

            records.Rows.Add(loadchanges.Rows(i)("ID"), Format(loadchanges.Rows(i)("updateDate"), "MM/dd/yyyy"), loadchanges.Rows(i)("Balancce"), loadchanges.Rows(i)("Remarks"), loadchanges.Rows(i)("CreatedBy"))

        Next


    End Sub

    Private Sub SaveToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem1.Click

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then

            If IsNumeric(balance.Text) = True Then

                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                stracs = "insert into UpdateLedger ([updateDate],[AccountNo],[Balancce],[Remarks],[CreatedBy]) values ('" _
                & Format(Now, "yyyy-MM-dd") & "', '" & accountNo.Text & "', '" & Format(balance.Text, "standard") & "', '" _
                & remarks.Text.ToString.Replace("'", "''") & "', '" & My.Settings.Nickname & "')"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                stracs = "insert into AccountLedger ([ledgerAccountNo],[ledgerDate],[ledgerRefNo],[ledgerParticulars],[ledgerReading]
                    ,[ledgerConsumption],[ledgerAmount],[ledgerDiscount],[ledgerBalance]) values ('" & accountNo.Text & "', '" _
                    & Format(Now, "yyyy-MM-dd") & "', '', 'Update Ledger', '','','','','" & balance.Text & "')"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                loadupdates()

                customerinfo.loadledgers()

                balance.ReadOnly = True
                remarks.ReadOnly = True
                lblmode.Text = "Mode"
                lblmode.ForeColor = Color.Green
                lblmode.Hide()

            Else

            End If
        Else
            MsgBox("Your account cannot perform this process.")
        End If

    End Sub
End Class