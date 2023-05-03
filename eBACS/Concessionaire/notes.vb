Public Class notes

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click

        If My.Settings.Cservice = "Yes" Then

            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try

            If remarks.Text = "" Or IsDBNull(remarks.Text) = True Then

                MsgBox("Note empty.")

            Else

                stracs = "insert into AccountHistory (historyDate,historyAccountNo,historyCategory,historyName,historyRemarks,historyCreatedBy) values " _
                       & "('" & Format(Now, "yyyy-MM-dd") & "', '" & accountNo.Text & "', 'Notes', '" _
                       & remarks.Text.Replace("'", "''") & "', '', '" & My.Settings.Nickname & "')"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                customerinfo.txtAccountNo_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))
                Me.Close()

            End If

        Else
            MsgBox("Your account cannot perform this process.")
        End If



    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub notes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        remarks.Clear()
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click



    End Sub
End Class