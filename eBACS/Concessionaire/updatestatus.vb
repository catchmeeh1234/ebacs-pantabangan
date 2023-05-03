Public Class updatestatus


    Private Sub updatestatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        statusDate.Value = Now
        'MainForm.concessionaireaccounts.lblCreateUpdate.Text = "Mode"
        lblmode.Text = "Mode"
        lblmode.Hide()
        loadstatus()

    End Sub

    Sub loadstatus()

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try
        sqldatastatus.Clear()
        stracs = "select * from AccountStatus where AccountNo = '" & customerinfo.txtAccountNo.Text.ToString.Replace("'", "''") & "' order by ID"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(sqldatastatus)


        listStatus.Items.Clear()

        If sqldatastatus.Rows.Count = 0 Then

        Else

            For i = 0 To sqldatastatus.Rows.Count - 1

                If sqldatastatus.Rows(i)("Status") = "Active" Or sqldatastatus.Rows(i)("Status") = "Don't Bill" Then

                    With listStatus.Items.Add(sqldatastatus.Rows(i)("ID"))
                        .subitems.add(sqldatastatus.Rows(i)("statusDate"))
                        .subitems.add(sqldatastatus.Rows(i)("Status"))
                        .subitems.add("")
                        .subitems.add("")
                        .subitems.add((""))
                        .subitems.add(sqldatastatus.Rows(i)("Remarks"))
                        .subitems.add("")
                        .subitems.add(sqldatastatus.Rows(i)("UpdatedBy"))
                    End With

                Else

                    With listStatus.Items.Add(sqldatastatus.Rows(i)("ID"))
                        .subitems.add(sqldatastatus.Rows(i)("statusDate"))
                        .subitems.add(sqldatastatus.Rows(i)("Status"))
                        .subitems.add(sqldatastatus.Rows(i)("StatusType"))
                        .subitems.add(sqldatastatus.Rows(i)("MeterStatus"))
                        .subitems.add(sqldatastatus.Rows(i)("LastReading"))
                        .subitems.add(sqldatastatus.Rows(i)("Remarks"))
                        .subitems.add(sqldatastatus.Rows(i)("DiscBy"))
                        .subitems.add(sqldatastatus.Rows(i)("UpdatedBy"))
                    End With

                End If



            Next
        End If

    End Sub

    Sub statusclear()

        statusname.SelectedIndex = -1
        statusname.Enabled = False
        statusremarks.ReadOnly = True
        statusremarks.Clear()

    End Sub

    Public Sub newstat()

        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            statusname.SelectedIndex = -1
            disctype.SelectedIndex = -1
            meterstatus.SelectedIndex = -1
            statusremarks.Clear()
            lastreading.Clear()
            Performedby.Clear()

            statusname.Enabled = True
            disctype.Enabled = True
            meterstatus.Enabled = True
            lastreading.ReadOnly = False
            statusremarks.ReadOnly = False
            Performedby.ReadOnly = False


            lblmode.Text = "New Status"
            lblmode.ForeColor = Color.Green

            statusname.Select()


            lblmode.Show()
        Else
            MsgBox("Your account cannot perform this process.")
        End If

    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click


        waitingapproval.trans = "updatestatnew"
        waitingapproval.ShowDialog()

        waitingapproval.TextBox1.Select()
        waitingapproval.TextBox1.Clear()


    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click

        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then

            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try

            If lblmode.Text = "New Status" Then

                If statusname.Text = "" Then

                    If statusname.Text = "" Then
                        lblstatus.ForeColor = Color.Red
                    Else
                        lblstatus.ForeColor = Color.Black
                    End If

                Else

                    If statusname.Text = "Active" Or statusname.Text = "Don't Bill" Then

                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                        stracs = "insert into AccountStatus (StatusDate,AccountNo,Status,Remarks,UpdatedBy) values ('" _
                            & Format(statusDate.Value, "yyyy-MM-dd") & "', '" & customerinfo.txtAccountNo.Text.ToString.Replace("'", "''") & "', '" _
                            & statusname.Text.ToString.Replace("'", "''") & "', '" & statusremarks.Text.ToString.Replace("'", "''") & "', '" _
                            & My.Settings.Nickname & "')"
                        acscmd.Connection = acsconn
                        acscmd.CommandText = stracs
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        stracs = "insert into AccountHistory (historyDate, historyAccountNo, historyCategory, historyName, historyRemarks, historyCreatedBy) values ('" _
                            & Format(Now, "yyyy-MM-dd") & "', '" & customerinfo.txtAccountNo.Text.ToString.Replace("'", "''") & "', " _
                            & "'status', '" & statusname.Text.ToString.Replace("'", "''") & "', '" & statusremarks.Text.ToString.Replace("'", "''") & "', '" & My.Settings.Nickname & "')"
                        acscmd.Connection = acsconn
                        acscmd.CommandText = stracs
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        If statusname.Text = "Disconnected" Then

                            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                            stracs = "update Customers set DateLastDisconnected = '" & Format(statusDate.Value, "yyyy-MM-dd") & "', CustomerStatus = '" & statusname.Text.ToString.Replace("'", "''") & "' where AccountNo = '" & customerinfo.txtAccountNo.Text.ToString.Replace("'", "''") & "'"
                            acscmd.Connection = acsconn
                            acscmd.CommandText = stracs
                            acscmd.ExecuteNonQuery()
                            acscmd.Dispose()

                        Else

                            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                            stracs = "update Customers set CustomerStatus = '" & statusname.Text.ToString.Replace("'", "''") & "' where AccountNo = '" & customerinfo.txtAccountNo.Text.ToString.Replace("'", "''") & "'"
                            acscmd.Connection = acsconn
                            acscmd.CommandText = stracs
                            acscmd.ExecuteNonQuery()
                            acscmd.Dispose()

                        End If

                        'MainForm.concessionaireaccounts.lblCreateUpdate.Text = "Mode"
                        customerinfo.txtAccountNo_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))

                        statusDate.Value = Now
                        statusname.SelectedIndex = -1
                        disctype.SelectedIndex = -1
                        meterstatus.SelectedIndex = -1
                        statusremarks.Clear()
                        lastreading.Clear()
                        Performedby.Clear()

                        statusname.Enabled = False
                        disctype.Enabled = False
                        meterstatus.Enabled = False
                        lastreading.ReadOnly = True
                        statusremarks.ReadOnly = True
                        Performedby.ReadOnly = True

                        lblmode.Hide()
                        lblmode.Text = "Mode"

                        lblstatus.ForeColor = Color.Black
                        lblremarks.ForeColor = Color.Black

                        loadstatus()

                        statusclear()

                        MsgBox("Status Updated.")

                    Else

                        If IsNumeric(lastreading.Text) = False Or statusname.Text = "" Or disctype.Text = "" Or meterstatus.Text = "" Or lastreading.Text = "" Then

                            If statusname.Text = "" Then
                                lblstatus.ForeColor = Color.Red
                            Else
                                lblstatus.ForeColor = Color.Black
                            End If

                            If disctype.Text = "" Then
                                lbldisctype.ForeColor = Color.Red
                            Else
                                lbldisctype.ForeColor = Color.Black
                            End If

                            If meterstatus.Text = "" Then
                                lblmeterstatus.ForeColor = Color.Red
                            Else
                                lblmeterstatus.ForeColor = Color.Black
                            End If

                            If Performedby.Text = "" Then
                                Performedby.ForeColor = Color.Red
                            Else
                                Performedby.ForeColor = Color.Black
                            End If

                            If lastreading.Text = "" Or IsNumeric(lastreading.Text) = False Then
                                lblLastreading.ForeColor = Color.Red
                            Else
                                lblLastreading.ForeColor = Color.Black
                            End If

                        Else

                            If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                            stracs = "insert into AccountStatus (StatusDate,AccountNo,Status,StatusType,MeterStatus,LastReading,Remarks,DiscBy,UpdatedBy) values ('" _
                                & Format(statusDate.Value, "yyyy-MM-dd") & "', '" & customerinfo.txtAccountNo.Text.ToString.Replace("'", "''") & "', '" _
                                & statusname.Text.ToString.Replace("'", "''") & "', '" & disctype.Text.ToString.Replace("'", "''") & "', '" _
                                & meterstatus.Text.ToString.Replace("'", "''") & "', " & lastreading.Text & ", '" & statusremarks.Text.ToString.Replace("'", "''") & "', '" _
                                & Performedby.Text.ToString.Replace("'", "''") & "', '" & My.Settings.Nickname & "')"
                            acscmd.Connection = acsconn
                            acscmd.CommandText = stracs
                            acscmd.ExecuteNonQuery()
                            acscmd.Dispose()

                            stracs = "insert into AccountHistory (historyDate, historyAccountNo, historyCategory, historyName, historyRemarks, historyCreatedBy) values ('" _
                                & Format(Now, "yyyy-MM-dd") & "', '" & customerinfo.txtAccountNo.Text.ToString.Replace("'", "''") & "', " _
                                & "'status', '" & statusname.Text.ToString.Replace("'", "''") & "', '" & statusremarks.Text.ToString.Replace("'", "''") & "', '" & My.Settings.Nickname & "')"
                            acscmd.Connection = acsconn
                            acscmd.CommandText = stracs
                            acscmd.ExecuteNonQuery()
                            acscmd.Dispose()

                            If statusname.Text = "Disconnected" Then

                                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                stracs = "update Customers set DateLastDisconnected = '" & Format(statusDate.Value, "yyyy-MM-dd") & "', CustomerStatus = '" & statusname.Text.ToString.Replace("'", "''") & "' where AccountNo = '" & customerinfo.txtAccountNo.Text.ToString.Replace("'", "''") & "'"
                                acscmd.Connection = acsconn
                                acscmd.CommandText = stracs
                                acscmd.ExecuteNonQuery()
                                acscmd.Dispose()

                            Else

                                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                stracs = "update Customers set CustomerStatus = '" & statusname.Text.ToString.Replace("'", "''") & "' where AccountNo = '" & customerinfo.txtAccountNo.Text.ToString.Replace("'", "''") & "'"
                                acscmd.Connection = acsconn
                                acscmd.CommandText = stracs
                                acscmd.ExecuteNonQuery()
                                acscmd.Dispose()

                            End If

                            'MainForm.concessionaireaccounts.lblCreateUpdate.Text = "Mode"
                            customerinfo.txtAccountNo_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))

                            statusDate.Value = Now
                            statusname.SelectedIndex = -1
                            disctype.SelectedIndex = -1
                            meterstatus.SelectedIndex = -1
                            statusremarks.Clear()
                            lastreading.Clear()
                            Performedby.Clear()

                            statusname.Enabled = False
                            disctype.Enabled = False
                            meterstatus.Enabled = False
                            lastreading.ReadOnly = True
                            statusremarks.ReadOnly = True
                            Performedby.ReadOnly = True

                            lblmode.Hide()
                            lblmode.Text = "Mode"

                            lblstatus.ForeColor = Color.Black
                            lblremarks.ForeColor = Color.Black

                            loadstatus()

                            statusclear()

                            MsgBox("Status Updated.")

                        End If



                    End If



                End If

            End If

            If lblmode.Text = "Update Mode" Then

                If statusname.Text = "Active" Or statusname.Text = "Don't Bill" Then

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    stracs = "update AccountStatus set StatusDate = '" & Format(statusDate.Value, "yyyy-MM-dd") & "', " _
                    & "Remarks = '" & statusremarks.Text.ToString.Replace("'", "''") & "', " _
                    & "UpdatedBy = '" & My.Settings.Nickname & "' where ID = " & lblID.Text
                    acscmd.Connection = acsconn
                    acscmd.CommandText = stracs
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                Else

                    If IsNumeric(lastreading.Text) = False Or statusname.Text = "" Or disctype.Text = "" Or meterstatus.Text = "" Or lastreading.Text = "" Then

                        If statusname.Text = "" Then
                            lblstatus.ForeColor = Color.Red
                        Else
                            lblstatus.ForeColor = Color.Black
                        End If

                        If disctype.Text = "" Then
                            lbldisctype.ForeColor = Color.Red
                        Else
                            lbldisctype.ForeColor = Color.Black
                        End If

                        If meterstatus.Text = "" Then
                            lblmeterstatus.ForeColor = Color.Red
                        Else
                            lblmeterstatus.ForeColor = Color.Black
                        End If

                        If Performedby.Text = "" Then
                            Performedby.ForeColor = Color.Red
                        Else
                            Performedby.ForeColor = Color.Black
                        End If

                        If lastreading.Text = "" Or IsNumeric(lastreading.Text) = False Then
                            lblLastreading.ForeColor = Color.Red
                        Else
                            lblLastreading.ForeColor = Color.Black
                        End If
                    Else

                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "update AccountStatus set StatusDate = '" & Format(statusDate.Value, "yyyy-MM-dd") & "', " _
                        & "StatusType = '" & disctype.Text & "', " _
                        & "MeterStatus = '" & meterstatus.Text & "', " _
                        & "LastReading = " & lastreading.Text & ", " _
                        & "Remarks = '" & statusremarks.Text.ToString.Replace("'", "''") & "', " _
                        & "DiscBy = '" & Performedby.Text.ToString.Replace("'", "''") & "', " _
                        & "UpdatedBy = '" & My.Settings.Nickname & "' where ID = " & lblID.Text
                        acscmd.Connection = acsconn
                        acscmd.CommandText = stracs
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()
                    End If



                    lblmode.Hide()
                    lblmode.Text = "Mode"

                    statusDate.Value = Now
                    statusname.SelectedIndex = -1
                    disctype.SelectedIndex = -1
                    meterstatus.SelectedIndex = -1
                    statusremarks.Clear()
                    lastreading.Clear()
                    Performedby.Clear()

                    statusname.Enabled = False
                    disctype.Enabled = False
                    meterstatus.Enabled = False
                    lastreading.ReadOnly = True
                    statusremarks.ReadOnly = True
                    Performedby.ReadOnly = True

                    MsgBox("Record Updated.")

                    statusclear()

                End If

            End If
        Else

            MsgBox("Your account cannot perform this process.")

        End If

    End Sub

    Public Sub updatestat()

        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            If listStatus.SelectedItems.Count = 0 Then
            Else

                If statusname.Text = "Active" Or statusname.Text = "Don't Bill" Then

                End If

                lblmode.Text = "Update Mode"
                lblmode.ForeColor = Color.Orange
                lblmode.Show()


                statusname.Enabled = False
                disctype.Enabled = True
                meterstatus.Enabled = True
                lastreading.ReadOnly = False
                statusremarks.ReadOnly = False
                Performedby.ReadOnly = False

                statusname.Enabled = False
                statusremarks.ReadOnly = False
                statusremarks.Select()

                lblID.Text = listStatus.SelectedItems(0).SubItems(0).Text
                statusDate.Value = listStatus.SelectedItems(0).SubItems(1).Text
                statusname.Text = listStatus.SelectedItems(0).SubItems(2).Text
                disctype.Text = listStatus.SelectedItems(0).SubItems(3).Text

                meterstatus.Text = listStatus.SelectedItems(0).SubItems(4).Text
                lastreading.Text = listStatus.SelectedItems(0).SubItems(5).Text
                statusremarks.Text = listStatus.SelectedItems(0).SubItems(6).Text
                Performedby.Text = listStatus.SelectedItems(0).SubItems(7).Text

            End If
        Else
            MsgBox("Your account cannot perform this process.")
        End If

    End Sub

    Private Sub EditToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem1.Click

        waitingapproval.trans = "updatestatedit"
        waitingapproval.ShowDialog()

        waitingapproval.TextBox1.Select()
        waitingapproval.TextBox1.Clear()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        customerinfo.txtAccountNo.Select()
        Me.Close()

    End Sub

    Private Sub statusname_SelectedIndexChanged(sender As Object, e As EventArgs) Handles statusname.SelectedIndexChanged

        If statusname.Text = "Active" Or statusname.Text = "Don't Bill" Then

            disctype.SelectedIndex = -1
            disctype.Enabled = False

            meterstatus.SelectedIndex = -1
            meterstatus.Enabled = False

            Performedby.Clear()
            Performedby.Enabled = False

            lastreading.Clear()
            lastreading.Enabled = False

            statusremarks.Clear()

        Else

            disctype.SelectedIndex = -1
            disctype.Enabled = True

            meterstatus.SelectedIndex = -1
            meterstatus.Enabled = True

            Performedby.Clear()
            Performedby.Enabled = True

            lastreading.Clear()
            lastreading.Enabled = True

            statusremarks.Clear()

        End If





    End Sub
End Class