Public Class meterreset
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Me.Close()

    End Sub

    Sub loadmeterreset()

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        Dim loadrecords As New DataTable
        stracs = "select * from MeterReset where resetAccountNo = '" & accountNo.Text & "'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(loadrecords)

        records.Rows.Clear()

        If loadrecords.Rows.Count = 0 Then
        Else

            For t = 0 To loadrecords.Rows.Count - 1

                records.Rows.Add(loadrecords.Rows(t)("ID"), Format(loadrecords.Rows(t)("resetDate"), "short date"), loadrecords.Rows(t)("resetFrom"), loadrecords.Rows(t)("resetTo"), loadrecords.Rows(t)("resetRemarks"), loadrecords.Rows(t)("resetBy"))

            Next

        End If

    End Sub

    Private Sub meterreset_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        loadmeterreset()

    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        If My.Settings.Cservice = "Yes" Then

            If lblmode.Text = "Update Last Reading" Then

                If IsNumeric(resetto.Text) = True Then

                    If remarks.Text <> "" Then

                        stracs = "update Customers set LastMeterReading = " & Val(resetto.Text) & " where AccountNo = '" & accountNo.Text.Replace("'", "''") & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        stracs = "insert into MeterReset ([resetAccountNo],[resetDate],[resetFrom] ,[resetTo],[resetRemarks],[resetBy]) values 
                          ('" & accountNo.Text & "', '" & Format(Now, "yyyy-MM-dd") & "', " & resetfrom.Text & ", " & resetto.Text & ", '" _
                                  & remarks.Text & "', '" & My.Settings.Nickname & "')"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        lblmode.Text = "Mode"
                        lblmode.Hide()

                        resetto.Clear()
                        remarks.Clear()

                        resetto.ReadOnly = False
                        remarks.ReadOnly = False

                        loadmeterreset()
                        customerinfo.txtAccountNo_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))

                    End If

                Else


                End If
            End If

        Else
            MsgBox("Your account cannot perform this process.")
        End If


    End Sub

    Sub createnew()

        lblmode.Text = "Update Last Reading"
        lblmode.Show()
        resetto.Clear()
        remarks.Clear()

        resetto.ReadOnly = False
        remarks.ReadOnly = False

    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click

        'If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
        '    createnew()
        'Else
        '    MsgBox("Your account cannot perform this process.")
        'End If

        If customerinfo.txtAccountNo.Text = "" Then
            MsgBox("Account number empty.")
        Else
            waitingapproval.trans = "updatelastreading"
            waitingapproval.ShowDialog()

            waitingapproval.TextBox1.Select()
            waitingapproval.TextBox1.Clear()
        End If


    End Sub

    Private Sub CancelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CancelToolStripMenuItem.Click
        lblmode.Text = "Mode"
        lblmode.Hide()

        resetto.Clear()
        remarks.Clear()

        resetto.ReadOnly = False
        remarks.ReadOnly = False

        loadmeterreset()
    End Sub

End Class