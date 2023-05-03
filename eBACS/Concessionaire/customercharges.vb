Public Class customercharges

    Public chargeid As Integer
    Public chargeparticulars As String = ""
    Public chargeamount As Double
    Public chargeCat As String = ""
    Public chargeEntry As String = ""
    Public editid As Integer

    Private Sub customercharges_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        loadchargesoption()
        loadaccountcharges()
        chargelock()

        customerinfo.lblCreateUpdate.Text = "Mode"

        Dim x As Integer = 1
        Dim years As Integer = 2021

        chargeMonth.Items.Clear()
        chargeYear.Items.Clear()

        Do Until x = 13

            chargeMonth.Items.Add(MonthName(x))
            chargeYear.Items.Add(years)
            years = years + 1
            x = x + 1
        Loop

        'chargeMonth.Text = MonthName(Month(Now))

    End Sub

    Sub loadaccountcharges()

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        sqlDatacharges.Clear()

        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        stracs = "select * from ScheduleCharges where AccountNumber = '" & customerinfo.txtAccountNo.Text.ToString.Replace("'", "''") & "'"
        acscmd.Connection = acsconn
        acscmd.CommandText = stracs
        acsda.SelectCommand = acscmd
        acsda.Fill(sqlDatacharges)

        chargeGrid.Rows.Clear()

        For i = 0 To sqlDatacharges.Rows.Count - 1



            If sqlDatacharges.Rows(i)("Recurring") = "Yes" Then

                chargeGrid.Rows.Add(sqlDatacharges.Rows(i)("ScheduleChargesID"), sqlDatacharges.Rows(i)("ChargeID"), Format(sqlDatacharges.Rows(i)("DateCreated"), "short date"), sqlDatacharges.Rows(i)("Category"), sqlDatacharges.Rows(i)("Particular"), sqlDatacharges.Rows(i)("Amount"), "Yes", "", "", sqlDatacharges.Rows(i)("Remarks"), sqlDatacharges.Rows(i)("ActiveInactive"))
            Else

                chargeGrid.Rows.Add(sqlDatacharges.Rows(i)("ScheduleChargesID"), sqlDatacharges.Rows(i)("ChargeID"), Format(sqlDatacharges.Rows(i)("DateCreated"), "short date"), sqlDatacharges.Rows(i)("Category"), sqlDatacharges.Rows(i)("Particular"), sqlDatacharges.Rows(i)("Amount"), "No", MonthName(sqlDatacharges.Rows(i)("BillingMonth")), sqlDatacharges.Rows(i)("BillingYear"), sqlDatacharges.Rows(i)("Remarks"), sqlDatacharges.Rows(i)("ActiveInactive"))

            End If

        Next

    End Sub

    Sub loadchargesoption()

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        sqlDatacharges.Clear()

        stracs = "SELECT * FROM Charges order by ChargeID"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        'acsdr = acscmd.ExecuteReader
        acsda.SelectCommand = acscmd
        acsda.Fill(sqlDatacharges)

        chargedataset.Tables(0).Rows.Clear()
        chargeParticular.Items.Clear()

        For o = 0 To sqlDatacharges.Rows.Count - 1

            chargedataset.Tables(0).Rows.Add(sqlDatacharges.Rows(o)("ChargeID"), sqlDatacharges.Rows(o)("Category"), sqlDatacharges.Rows(o)("Entry"), sqlDatacharges.Rows(o)("Particular"), sqlDatacharges.Rows(o)("Category") & " - " & sqlDatacharges.Rows(o)("Particular") & " - " & sqlDatacharges.Rows(o)("Amount"), sqlDatacharges.Rows(o)("Amount"))
            chargeParticular.Items.Add(sqlDatacharges.Rows(o)("Category") & " - " & sqlDatacharges.Rows(o)("Particular") & " - " & sqlDatacharges.Rows(o)("Amount"))
        Next

    End Sub


    Private Sub recurring_CheckedChanged(sender As Object, e As EventArgs) Handles chargeRecurring.CheckedChanged

        If chargeRecurring.Checked = True Then

            chargeMonth.Enabled = False
            chargeMonth.SelectedIndex = -1
            chargeYear.Enabled = False
            chargeYear.SelectedIndex = -1

        Else

            chargeMonth.Enabled = True
            chargeMonth.Text = Format(Now, "MMMM")
            chargeYear.Enabled = True
            chargeYear.Text = Year(Now)

        End If

    End Sub

    Public Sub newcharges()

        lblmode.Text = "Add New"
        lblmode.ForeColor = Color.Green
        lblmode.Show()
        chargeunlock()
        chargeParticular.Select()
        chargeActive.CheckState = CheckState.Checked

    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click

        If My.Settings.Cservice = "Yes" Then

            If customerinfo.txtAccountNo.Text = "" Then
                MsgBox("Account number empty.")
            Else
                waitingapproval.trans = "addchargesnew"
                waitingapproval.ShowDialog()

                waitingapproval.TextBox1.Select()
                waitingapproval.TextBox1.Clear()
            End If


        Else
            MsgBox("Your account cannot perform this process.")
        End If



    End Sub

    Sub chargelock()

        chargeParticular.SelectedIndex = -1
        chargeRecurring.CheckState = CheckState.Unchecked
        chargeActive.CheckState = CheckState.Unchecked
        chargeYear.SelectedIndex = -1
        chargeMonth.SelectedIndex = -1
        chargeRemarks.Clear()

        chargeParticular.Enabled = False
        chargeRecurring.Enabled = False
        chargeActive.Enabled = False
        chargeYear.Enabled = False
        chargeMonth.Enabled = False
        chargeRemarks.ReadOnly = True

        lblmode.Text = "Mode"
        lblmode.ForeColor = Color.Green
        lblmode.Hide()

        chargeRemarks.Select()
        chargeGrid.ClearSelection()

    End Sub

    Sub chargeunlock()

        chargeParticular.SelectedIndex = -1
        chargeRecurring.CheckState = CheckState.Unchecked
        chargeActive.CheckState = CheckState.Unchecked
        chargeYear.SelectedIndex = -1
        chargeMonth.SelectedIndex = -1
        chargeRemarks.Clear()

        chargeParticular.Enabled = True
        chargeRecurring.Enabled = True
        chargeActive.Enabled = True
        chargeYear.Enabled = True
        chargeMonth.Enabled = True
        chargeRemarks.ReadOnly = False

    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click

        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then

            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try

            If lblmode.Text = "Add New" Then

                If chargeParticular.Text = "" Then

                    MsgBox("Please select charge.")

                Else

                    If chargeRecurring.CheckState = CheckState.Checked Then


                        Dim recurringstate As String = "Yes"


                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                        Dim asd = chargedataset.Tables(0).Select("chargeParticularAll = '" & chargeParticular.Text.ToString.Replace("'", "''") & "'")



                        For Each row In asd
                            chargeid = row("chargeID")
                            chargeCat = row("chargeCategory")
                            chargeEntry = row("chargeEntry")
                            chargeparticulars = row("chargeParticular")
                            chargeamount = row("chargeAmount")
                        Next

                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                        stracs = "insert into ScheduleCharges (ChargeID, Category, Entry, Particular, AccountNumber, Amount, Remarks, Recurring, Monthh, BillingMonth, BillingYear, DateCreated, ActiveInactive, CreatedBy) values (" _
                            & chargeid & ", '" & chargeCat.ToString.Replace("'", "''") & "', '" & chargeEntry.ToString.Replace("'", "''") & "', '" & chargeparticulars.ToString.Replace("'", "''") & "', '" & customerinfo.txtAccountNo.Text & "', " _
                            & chargeamount & ", '" & chargeRemarks.Text.ToString.Replace("'", "''") & "', '" & recurringstate & "', '0', 0, 0, '" & Format(Now, "yyyy-MM-dd") & "', " & chargeActive.CheckState & ", '" & My.Settings.Nickname & "')"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        'If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        'stracs = "insert into AccountHistory (historyDate, historyAccountNo, historyCategory, historyName, historyRemarks, historyCreatedBy) values ('" _
                        '    & Format(statusdate.Value, "short date") & "', '" & MainForm.concessionaireaccounts.txtAccountNo.Text.ToString.Replace("'", "''") & "', " _
                        '    & "'status', '" & statusname.Text.ToString.Replace("'", "''") & "', '" & statusremarks.Text.ToString.Replace("'", "''") & "', 'Admin')"
                        'acscmd.Connection = acsconn
                        'acscmd.CommandText = stracs
                        'acscmd.ExecuteNonQuery()
                        'acscmd.Dispose()

                        lblmode.Text = "Mode"
                        lblmode.Hide()

                        loadaccountcharges()
                        chargelock()
                        customerinfo.txtAccountNo_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))
                    Else

                        If chargeMonth.SelectedIndex = -1 Or chargeYear.SelectedIndex = -1 Then

                            MsgBox("Please select Month and Year.")

                        Else
                            Dim recurringstate As String = "No"

                            Dim chargeamount As Double

                            If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                            Dim asd = chargedataset.Tables(0).Select("chargeParticularAll = '" & chargeParticular.Text.ToString.Replace("'", "''") & "'")

                            For Each row In asd
                                chargeid = row("chargeID")
                                chargeCat = row("chargeCategory")
                                chargeEntry = row("chargeEntry")
                                chargeparticulars = row("chargeParticular")
                                chargeamount = row("chargeAmount")
                            Next

                            If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                            stracs = "insert into ScheduleCharges (ChargeID, Category, Entry, Particular, AccountNumber, Amount, Remarks, Recurring, Monthh, BillingMonth, BillingYear, DateCreated, ActiveInactive, CreatedBy) values (" _
                            & chargeid & ", '" & chargeCat.ToString.Replace("'", "''") & "', '" & chargeEntry.ToString.Replace("'", "''") & "', '" & chargeparticulars.ToString.Replace("'", "''") & "', '" & customerinfo.txtAccountNo.Text & "', " _
                            & chargeamount & ", '" & chargeRemarks.Text.ToString.Replace("'", "''") & "', '" & recurringstate & "', '1', " & Month(chargeMonth.Text & " 1, 2021") & ", " & chargeYear.Text & ", '" & Format(Now, "yyyy-MM-dd") & "', " & chargeActive.CheckState & ", '" & My.Settings.Nickname & "')"
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acscmd.ExecuteNonQuery()
                            acscmd.Dispose()

                            lblmode.Text = "Mode"
                            lblmode.Hide()

                            loadaccountcharges()
                            chargelock()
                            customerinfo.txtAccountNo_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))
                        End If

                    End If

                End If
            End If

            If lblmode.Text = "Edit Mode" Then

                If chargeParticular.Text = "" Then

                    MsgBox("Please select charge.")

                Else

                    If chargeRecurring.CheckState = CheckState.Checked Then


                        Dim recurringstate As String = "Yes"


                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                        Dim asd = chargedataset.Tables(0).Select("chargeParticularAll = '" & chargeParticular.Text.ToString.Replace("'", "''") & "'")

                        For Each row In asd
                            chargeid = row("chargeID")
                            chargeCat = row("chargeCategory")
                            chargeEntry = row("chargeEntry")
                            chargeparticulars = row("chargeParticular")
                            chargeamount = row("chargeAmount")
                        Next



                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                        stracs = "update ScheduleCharges set ChargeID = " & chargeid & ", " _
                            & "Category = '" & chargeCat.ToString.Replace("'", "''") & "', " _
                            & "Entry = '" & chargeEntry.ToString.Replace("'", "''") & "', " _
                            & "Particular = '" & chargeparticulars.ToString.Replace("'", "''") & "', " _
                            & "AccountNumber = '" & customerinfo.txtAccountNo.Text & "', " _
                            & "Amount = " & chargeamount & ", " _
                            & "Remarks = '" & chargeRemarks.Text.ToString.Replace("'", "''") & "', " _
                            & "Recurring = '" & recurringstate & "', " _
                            & "Monthh = '1', BillingMonth = 1, BillingYear = 2021, ActiveInactive = " & chargeActive.CheckState _
                            & " where ScheduleChargesID = " & editid
                        '& chargeparticulars.ToString.Replace("'", "''") & "', '" & MainForm.concessionaireaccounts.txtAccountNo.text & "', " _
                        '& chargeamount & ", '" & txtremarks.Text.ToString.Replace("'", "''") & "', '" & recurringstate & "', '1', 1, 2021)"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        lblmode.Text = "Mode"
                        lblmode.Hide()

                        loadaccountcharges()
                        chargelock()

                    Else

                        If chargeMonth.SelectedIndex = -1 Or chargeYear.SelectedIndex = -1 Then

                            MsgBox("Please select Month and Year.")

                        Else

                            Dim recurringstate As String = "No"

                            Dim chargeamount As Double

                            If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                            Dim asd = chargedataset.Tables(0).Select("chargeParticularAll = '" & chargeParticular.Text.ToString.Replace("'", "''") & "'")

                            For Each row In asd
                                chargeid = row("chargeID")
                                chargeCat = row("chargeCategory")
                                chargeEntry = row("chargeEntry")
                                chargeparticulars = row("chargeParticular")
                                chargeamount = row("chargeAmount")
                            Next

                            If acsconn.State = ConnectionState.Closed Then acsconn.Open()



                            stracs = "update ScheduleCharges set ChargeID = " & chargeid & ", " _
                            & "Category = '" & chargeCat.ToString.Replace("'", "''") & "', " _
                            & "Entry = '" & chargeEntry.ToString.Replace("'", "''") & "', " _
                            & "Particular = '" & chargeparticulars.ToString.Replace("'", "''") & "', " _
                            & "AccountNumber = '" & customerinfo.txtAccountNo.Text & "', " _
                            & "Amount = " & chargeamount & ", " _
                            & "Remarks = '" & chargeRemarks.Text.ToString.Replace("'", "''") & "', " _
                            & "Recurring = '" & recurringstate & "', " _
                            & "Monthh = '1', " _
                            & "BillingMonth = " & Month(chargeMonth.Text & " 1, 2021") & ", " _
                            & "BillingYear = " & chargeYear.Text & ", ActiveInactive = " & chargeActive.CheckState _
                            & " where ScheduleChargesID = " & editid
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acscmd.ExecuteNonQuery()
                            acscmd.Dispose()


                            lblmode.Text = "Mode"
                            lblmode.Hide()

                            loadaccountcharges()
                            chargelock()

                        End If

                    End If
                    customerinfo.txtAccountNo_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))
                End If

            End If

        Else
            MsgBox("Your account cannot perform this process.")
        End If



    End Sub

    Public Sub editcharges()

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        If chargeGrid.SelectedRows.Count = 0 Then

            MsgBox("Please select item to edit")

        Else

            lblmode.Text = "Edit Mode"
            lblmode.ForeColor = Color.Orange
            lblmode.Show()
            chargeunlock()

            chargeParticular.Text = chargeGrid.Rows(chargeGrid.CurrentCellAddress.Y).Cells(3).Value & " - " & chargeGrid.Rows(chargeGrid.CurrentCellAddress.Y).Cells(4).Value & " - " & chargeGrid.Rows(chargeGrid.CurrentCellAddress.Y).Cells(5).Value

            If chargeGrid.Rows(chargeGrid.CurrentCellAddress.Y).Cells(6).Value = "Yes" Then
                chargeRecurring.CheckState = CheckState.Checked
            Else
                chargeRecurring.CheckState = CheckState.Unchecked
                chargeMonth.Text = chargeGrid.Rows(chargeGrid.CurrentCellAddress.Y).Cells(7).Value
                chargeYear.Text = chargeGrid.Rows(chargeGrid.CurrentCellAddress.Y).Cells(8).Value
            End If

            If chargeGrid.Rows(chargeGrid.CurrentCellAddress.Y).Cells(10).Value = -1 Then
                chargeActive.CheckState = CheckState.Checked
            Else
                chargeActive.CheckState = CheckState.Unchecked
            End If

            chargeRemarks.Text = chargeGrid.Rows(chargeGrid.CurrentCellAddress.Y).Cells(9).Value
            editid = chargeGrid.Rows(chargeGrid.CurrentCellAddress.Y).Cells(0).Value

            chargeParticular.Select()

        End If

    End Sub

    Private Sub EditToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem1.Click

        If My.Settings.Cservice = "Yes" Then

            If customerinfo.txtAccountNo.Text = "" Then
                MsgBox("Account number empty.")
            Else
                waitingapproval.trans = "addchargesupdate"
                waitingapproval.ShowDialog()

                waitingapproval.TextBox1.Select()
                waitingapproval.TextBox1.Clear()
            End If

        Else
            MsgBox("Your account cannot perform this process.")
        End If



    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub


End Class