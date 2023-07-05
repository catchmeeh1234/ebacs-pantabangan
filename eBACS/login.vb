Public Class login

    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See https://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Cursor = Cursors.WaitCursor



        Dim acc As New DataTable
        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try
        acc.Rows.Clear()

        stracs = "select * from useraccounts WHERE username = '" & Username.Text & "' AND password = '" & Password.Text & "'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(acc)

        If acc.Rows.Count = 0 Then
            MsgBox("No Account Found")
            Username.Clear()
            Password.Clear()
            Username.Select()
        Else

            If acc.Rows(0)("ActiveSession") = 1 Then

                If acc.Rows(0)("cashier") = "Yes" Or acc.Rows(0)("viewer") = "Yes" Then

                    My.Settings.Office_Name = acc.Rows(0)("office")
                    My.Settings.Office_Code = acc.Rows(0)("officeletter")

                    My.Settings.Nickname = acc.Rows(0)("nickname")
                    My.Settings.Fullname = acc.Rows(0)("fullname")

                    My.Settings.Designation = acc.Rows(0)("designation")

                    eBACSmain.lblUserName.Text = acc.Rows(0)("nickname")


                    My.Settings.Admin = acc.Rows(0)("admin")
                    My.Settings.Cashier = acc.Rows(0)("cashier")

                    My.Settings.Cservice = acc.Rows(0)("cservice")
                    My.Settings.Viewer = acc.Rows(0)("viewer")

                    stracs = "update useraccounts set ActiveSession = 1 where ID = " & acc.Rows(0)("ID")
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                    eBACSmain.Show()

                    'If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    'stracs = "update useraccounts set LastActivityDate = '" & Date.Now & "' WHERE ID = '" & acc.Rows(0)("ID") & "'"
                    'acscmd.CommandText = stracs
                    'acscmd.Connection = acsconn
                    'acscmd.ExecuteNonQuery()
                    'acscmd.Dispose()


                    Password.Clear()
                    Me.Hide()

                Else
                    MsgBox("Your account is locked or logged in to another device.")
                    Application.Exit()
                End If

            Else

                My.Settings.Office_Name = acc.Rows(0)("office")
                My.Settings.Office_Code = acc.Rows(0)("officeletter")

                My.Settings.Nickname = acc.Rows(0)("nickname")
                My.Settings.Fullname = acc.Rows(0)("fullname")

                My.Settings.Designation = acc.Rows(0)("designation")
                eBACSmain.lblUserName.Text = acc.Rows(0)("nickname")

                My.Settings.Admin = acc.Rows(0)("admin")
                My.Settings.Cashier = acc.Rows(0)("cashier")

                My.Settings.Cservice = acc.Rows(0)("cservice")
                My.Settings.Viewer = acc.Rows(0)("viewer")

                stracs = "update useraccounts set ActiveSession = 1 where ID = " & acc.Rows(0)("ID")
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                eBACSmain.Show()

                'If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                'stracs = "update useraccounts set LastActivityDate = '" & Date.Now & "' WHERE ID = '" & acc.Rows(0)("ID") & "'"
                'acscmd.CommandText = stracs
                'acscmd.Connection = acsconn
                'acscmd.ExecuteNonQuery()
                'acscmd.Dispose()


                Password.Clear()
                Me.Hide()

            End If

        End If

        Cursor = Cursors.Default

    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

    Private Sub login_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        connectionsettings.connection()

        If acsconn.State = ConnectionState.Closed Then

            MsgBox("Please configure data setting.")
            Me.Hide()
            settingsDataSett.Show()
            settingsDataSett.Activate()

        Else

        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Application.Exit()
    End Sub

    Private Sub login_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        Try

        Catch ex As Exception
            stracs = "update useraccounts set ActiveSession = 0 where fullName = '" & My.Settings.Fullname & "'"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()
        End Try



    End Sub
End Class
