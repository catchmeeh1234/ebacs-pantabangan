Public Class billingAdjustmentBill
    Public totalamount As Double = 0
    Public associd As Integer
    Public Sub adjustAccountNo_KeyDown(sender As Object, e As KeyEventArgs) Handles adjustAccountNo.KeyDown

        If e.KeyCode = Keys.Enter Then

            Dim getaccount As New DataTable

            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try

            stracs = "select * from Customers where AccountNo = '" & adjustAccountNo.Text & "'"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(getaccount)

            If getaccount.Rows.Count = 0 Then

                MsgBox("No account found.")

            Else

                adjustAccountNo.Text = getaccount.Rows(0)("AccountNo")

                If getaccount.Rows(0)("CompanyName") Is DBNull.Value Or getaccount.Rows(0)("CompanyName") = "" Then
                    AdjustName.Text = getaccount.Rows(0)("Firstname") & " " & getaccount.Rows(0)("Middlename") & " " & getaccount.Rows(0)("Lastname")
                Else
                    AdjustName.Text = getaccount.Rows(0)("CompanyName")
                End If

                'AdjustName.Text = getaccount.Rows(0)("Firstname") & " " & getaccount.Rows(0)("Middlename") & " " & getaccount.Rows(0)("Lastname")

                Dim getbills As New DataTable
                Try
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                Catch ex As Exception
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                End Try

                stracs = "select Billno from Bills where AccountNumber = '" & adjustAccountNo.Text & "' and IsPaid = 'No' and Cancelled = 'No' and BillStatus = 'Posted'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(getbills)

                adjustBillnos.Items.Clear()

                If getbills.Rows.Count = 0 Then

                    MsgBox("No available bills for adjustment.")

                Else

                    For o = 0 To getbills.Rows.Count - 1
                        adjustBillnos.Items.Add(getbills.Rows(o)("BillNo"))
                    Next

                End If

                Dim loadadjust As New DataTable
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                stracs = "select * from BIllAdjustment where AccountNo = '" & adjustAccountNo.Text & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(loadadjust)

                AdjustRecords.Rows.Clear()

                If loadadjust.Rows.Count = 0 Then
                Else

                    For x = 0 To loadadjust.Rows.Count - 1

                        Dim totalold As Double = 0
                        Dim totalnew As Double = 0

                        totalold = (Val(loadadjust.Rows(x)("oldAmountDue")) + Val(loadadjust.Rows(x)("oldPenalty"))) - (Val(loadadjust.Rows(x)("oldDiscount")) + Val(loadadjust.Rows(x)("oldAdvance")))
                        totalnew = (Val(loadadjust.Rows(x)("newAmountDue")) + Val(loadadjust.Rows(x)("newPenalty"))) - (Val(loadadjust.Rows(x)("newDiscount")) + Val(loadadjust.Rows(x)("newAdvance")))

                        AdjustRecords.Rows.Add(loadadjust.Rows(x)("RefNo"), loadadjust.Rows(x)("BillNo"), loadadjust.Rows(x)("Remarks"), Format(Val(totalnew) - Val(totalold), "standard"), loadadjust.Rows(x)("Status"))

                    Next

                End If

            End If

        End If

    End Sub

    Private Sub accSearch_Click(sender As Object, e As EventArgs) Handles accSearch.Click
        SearchAccount.searchingform = "BillAdjustment"
        SearchAccount.ShowDialog()
    End Sub

    Public Sub createnew()

        Dim getaccount As New DataTable
        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try
        getaccount.Rows.Clear()

        stracs = "select isnull(AssocID, 0) as AssocID from Customers where AccountNo = '" & adjustAccountNo.Text & "'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(getaccount)

        If getaccount.Rows.Count = 0 Then
            MsgBox("No account found.")
        Else

            If getaccount.Rows(0)("AssocID") = 0 Then

                associd = 0
                adjustAccountNo.ReadOnly = True
                lblMode.Text = "Create New"
                lblMode.ForeColor = Color.Green
                saving.Show()
                lblMode.Show()
                unlockall()

            Else
                associd = getaccount.Rows(0)("AssocID")
                adjustAccountNo.ReadOnly = True
                lblMode.Text = "Create New"
                lblMode.ForeColor = Color.Green
                saving.Show()
                lblMode.Show()
                unlockall()
            End If



        End If

    End Sub

    Public Sub clearall()

        lblMode.Text = "Mode"
        lblMode.Hide()

        adjustCovered.Clear()
        adjustAmountdue.Clear()
        adjustDiscount.Clear()
        adjustPenalty.Clear()
        adjustAdvance.Clear()
        AdjustRemarks.Clear()
        AdjustDate.Value = Now
        adjustBillnos.SelectedIndex = -1
        adjustAccountNo.Clear()
        AdjustName.Clear()
        adjustRefNo.Clear()
        AdjustRecords.Rows.Clear()
        AdjustRemarks.Clear()
        billamountdue.Clear()
        billdiscount.Clear()
        billPenalty.Clear()
        billadvancepayment.Clear()
        billcharges.Rows.Clear()
        billTotal.Text = "0.00"
        lblnewtotal.Text = "0.00"
        adjustRefNo.ReadOnly = False



    End Sub

    Public Sub lockall()

        adjustAmountdue.ReadOnly = True
        adjustDiscount.ReadOnly = True
        adjustPenalty.ReadOnly = True
        adjustAdvance.ReadOnly = True
        AdjustRemarks.ReadOnly = True
        AdjustDate.Enabled = False
        adjustBillnos.Enabled = False
        adjustAccountNo.ReadOnly = False

    End Sub

    Public Sub unlockall()

        adjustAmountdue.ReadOnly = False
        adjustDiscount.ReadOnly = False
        adjustPenalty.ReadOnly = False
        adjustAdvance.ReadOnly = False
        AdjustRemarks.ReadOnly = False
        AdjustDate.Enabled = True
        adjustBillnos.Enabled = True

    End Sub

    Private Sub adjustBillnos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles adjustBillnos.SelectedIndexChanged

        If lblMode.Text = "Create New" Then

            Dim getbilldetails As New DataTable
            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try

            stracs = "select * from Bills where BillNo = " & adjustBillnos.Text
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(getbilldetails)

            If getbilldetails.Rows.Count = 0 Then
            Else

                If getbilldetails.Rows(0)("isSenior") = "Yes" Then
                    adjustSenior.CheckState = CheckState.Checked
                Else
                    adjustSenior.CheckState = CheckState.Unchecked
                End If

                adjustCovered.Text = getbilldetails.Rows(0)("BillingDate")

                billamountdue.Text = Format(getbilldetails.Rows(0)("AmountDue"), "standard")
                billdiscount.Text = Format(getbilldetails.Rows(0)("Discount"), "standard")
                billPenalty.Text = Format(getbilldetails.Rows(0)("PenaltyAfterDue"), "standard")
                billadvancepayment.Text = Format(getbilldetails.Rows(0)("AdvancePayment"), "standard")

                adjustAmountdue.Text = Format(getbilldetails.Rows(0)("AmountDue"), "standard")
                adjustDiscount.Text = Format(getbilldetails.Rows(0)("Discount"), "standard")
                adjustPenalty.Text = Format(getbilldetails.Rows(0)("PenaltyAfterDue"), "standard")
                adjustAdvance.Text = Format(getbilldetails.Rows(0)("AdvancePayment"), "standard")

                Dim getbillchargedetails As New DataTable
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                stracs = "select * from BillCharges where Category = 'Others' and BillNumber = " & adjustBillnos.Text
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(getbillchargedetails)

                If getbillchargedetails.Rows.Count = 0 Then
                Else

                    billcharges.Rows.Clear()

                    For x = 0 To getbillchargedetails.Rows.Count - 1
                        billcharges.Rows.Add(getbillchargedetails(x)("Particulars"), Format(getbillchargedetails(x)("Amount"), "standard"))
                        'adjustCharges.Rows.Add(getbillchargedetails(x)("Particulars"), Format(getbillchargedetails(x)("Amount"), "standard"))
                    Next

                End If

            End If

            computetotalamount()
            computenewtotalamount()

        End If

    End Sub

    Sub computetotalamount()

        Dim totalamountdue As Double = 0
        Dim totalcharges As Double = 0
        Dim totalarrear As Double = 0

        For x = 0 To billcharges.Rows.Count - 1

            totalcharges = totalcharges + Double.Parse(billcharges.Rows(x).Cells(1).Value)

        Next

        totalamountdue = Double.Parse(totalcharges + Double.Parse(billamountdue.Text) + Double.Parse(billPenalty.Text)) - (Double.Parse(billdiscount.Text) + Double.Parse(billadvancepayment.Text))

        billTotal.Text = Format(totalamountdue, "Standard")

    End Sub

    Sub computenewtotalamount()

        If IsNumeric(adjustAmountdue.Text) = False Or IsNumeric(adjustDiscount.Text) = False Or IsNumeric(adjustPenalty.Text) = False Or IsNumeric(adjustAdvance.Text) = False Then

        Else

            Dim totalamountdue As Double = 0
            Dim totalcharges As Double = 0
            Dim totalarrear As Double = 0

            For x = 0 To billcharges.Rows.Count - 1

                totalcharges = totalcharges + Double.Parse(billcharges.Rows(x).Cells(1).Value)

            Next

            totalamountdue = Double.Parse(totalcharges + Double.Parse(adjustAmountdue.Text) + Double.Parse(adjustPenalty.Text)) - (Double.Parse(adjustDiscount.Text) + Double.Parse(adjustAdvance.Text))

            lblnewtotal.Text = Format(totalamountdue, "Standard")

        End If

        'Dim x As Integer


    End Sub

    Public Sub updatemode()

        If adjustRefNo.Text = "" Then

        Else

            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try
            Dim searchadjust As New DataTable

            stracs = "select * from BIllAdjustment where RefNo = " & adjustRefNo.Text
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(searchadjust)

            If searchadjust.Rows.Count = 0 Then

                MsgBox("Please check Reference Number.")

            Else

                If searchadjust.Rows(0)("Status") = "Posted" Then
                    MsgBox("Adjustment already posted.")
                Else

                    unlockall()
                    adjustAccountNo.ReadOnly = True
                    adjustRefNo.ReadOnly = True
                    adjustBillnos.Enabled = False
                    lblMode.Text = "Update Mode"
                    lblMode.ForeColor = Color.Orange
                    saving.Show()
                    lblMode.Show()


                End If



            End If

        End If

    End Sub

    Public Sub savingadjustment()

        If IsNumeric(adjustAmountdue.Text) = False Or IsNumeric(adjustDiscount.Text) = False Or IsNumeric(adjustPenalty.Text) = False Or IsNumeric(adjustAdvance.Text) = False Then

            MsgBox("Please check the information")

        Else

            If lblnewtotal.Text = billTotal.Text Then
                MsgBox("Nothing is changed.")
            Else

                If adjustCategory.SelectedIndex = -1 Then

                    MsgBox("Please select category.")

                Else


                    If AdjustRemarks.Text = "" Then
                        MsgBox("Please check remarks.")
                    Else

                        If lblMode.Text = "Create New" Then

                            Try
                                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                            Catch ex As Exception
                                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                            End Try

                            Dim getbilladjustmentno As New DataTable

                            stracs = "select number from tbllogicnumbers where id = 4"
                            acscmd.Connection = acsconn
                            acscmd.CommandText = stracs
                            acsda.SelectCommand = acscmd
                            acsda.Fill(getbilladjustmentno)

                            adjustRefNo.Text = getbilladjustmentno.Rows(0)("number") + 1

                            stracs = "insert into BIllAdjustment ([RefNo],[AccountNo],[AccountName],[Remarks],[BillNo],[Date],[Status]
                     ,[CreatedBy],[ApprovedBy],[BillCovered],[OldAmountDue],[OldDiscount],[OldPenalty],[OldAdvance],[NewAmountDue],[NewDiscount]
                     ,[NewPenalty],[NewAdvance],[Category],[AssocID]) values (" _
                            & adjustRefNo.Text & ", '" & adjustAccountNo.Text & "', '" & AdjustName.Text.ToString.Replace("'", "''") & "', '" & AdjustRemarks.Text.ToString.Replace("'", "''") & "', " & adjustBillnos.Text & ", '" _
                            & Format(AdjustDate.Value, "yyyy-MM-dd") & "', 'Pending', '" & My.Settings.Nickname & "', 'Pending', '" & adjustCovered.Text & "', " _
                            & Double.Parse(billamountdue.Text) & ", " & Double.Parse(billdiscount.Text) & ", " _
                            & Double.Parse(billPenalty.Text) & ", " & Double.Parse(billadvancepayment.Text) & ", " _
                            & Double.Parse(adjustAmountdue.Text) & ", " & Double.Parse(adjustDiscount.Text) & ", " _
                            & Double.Parse(adjustPenalty.Text) & ", " & Double.Parse(adjustAdvance.Text) & ", '" & adjustCategory.Text & "', " & associd & ")"

                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acscmd.ExecuteNonQuery()
                            acscmd.Dispose()

                            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                            stracs = "update tbllogicnumbers set number = number + 1 where id = 4"
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acscmd.ExecuteNonQuery()
                            acscmd.Dispose()

                            lblMode.Text = "Mode"
                            lblMode.Hide()
                            adjustAccountNo.ReadOnly = False
                            adjustRefNo.ReadOnly = False
                            saving.Hide()

                            adjustRefNo_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))
                            lockall()
                            MsgBox("Adjustment saved.")

                        End If

                        If lblMode.Text = "Update Mode" Then

                            Try
                                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                            Catch ex As Exception
                                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                            End Try

                            stracs = "update BIllAdjustment set NewAmountDue = " & adjustAmountdue.Text & ", NewDiscount = " & adjustDiscount.Text _
                            & ", NewPenalty = " & adjustPenalty.Text & ", [NewAdvance] = " & adjustAdvance.Text & " where RefNo = " & adjustRefNo.Text
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acscmd.ExecuteNonQuery()
                            acscmd.Dispose()

                            lblMode.Text = "Mode"
                            lblMode.Hide()
                            adjustAccountNo.ReadOnly = False
                            adjustRefNo.ReadOnly = False

                            adjustRefNo_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))
                            lockall()
                            saving.Hide()
                            MsgBox("Adjustment saved.")

                        End If

                    End If
                End If
            End If

        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles saving.Click

        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            savingadjustment()
        Else
            MsgBox("Your account is not allowed for this process")
        End If


    End Sub

    Private Sub adjustAmountdue_TextChanged(sender As Object, e As EventArgs) Handles adjustAmountdue.TextChanged

        Dim intValue As Integer

        If Double.TryParse(adjustAmountdue.Text, intValue) AndAlso intValue >= 0 Then
            adjustAmountdue.ForeColor = Color.Black
            computenewtotalamount()
        Else
            adjustAmountdue.ForeColor = Color.Red
        End If

    End Sub

    Private Sub adjustDiscount_TextChanged(sender As Object, e As EventArgs) Handles adjustDiscount.TextChanged

        Dim intValue As Integer
        If Double.TryParse(adjustDiscount.Text, intValue) AndAlso intValue >= 0 Then
            adjustDiscount.ForeColor = Color.Black
            computenewtotalamount()
        Else
            adjustDiscount.ForeColor = Color.Red
        End If

    End Sub

    Private Sub adjustPenalty_TextChanged(sender As Object, e As EventArgs) Handles adjustPenalty.TextChanged
        Dim intValue As Integer


        If Double.TryParse(adjustPenalty.Text, intValue) AndAlso intValue >= 0 Then
            adjustPenalty.ForeColor = Color.Black
            computenewtotalamount()
        Else
            adjustPenalty.ForeColor = Color.Red
        End If

    End Sub

    Private Sub adjustAdvance_TextChanged(sender As Object, e As EventArgs) Handles adjustAdvance.TextChanged

        Dim intValue As Integer

        If Double.TryParse(adjustAdvance.Text, intValue) AndAlso intValue >= 0 Then
            adjustAdvance.ForeColor = Color.Black
            computenewtotalamount()
        Else
            adjustAdvance.ForeColor = Color.Red
        End If

    End Sub

    Private Sub adjustRefNo_KeyDown(sender As Object, e As KeyEventArgs) Handles adjustRefNo.KeyDown

        If e.KeyValue = Keys.Enter Then

            Dim intValue As Integer

            If Integer.TryParse(adjustRefNo.Text, intValue) Then

                Dim searchadjust As New DataTable
                Try
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                Catch ex As Exception
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                End Try
                searchadjust.Clear()

                stracs = "Select * from BIllAdjustment where RefNo = " & adjustRefNo.Text
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(searchadjust)

                If searchadjust.Rows.Count = 0 Then

                    MsgBox("No record found.")

                Else

                    adjustAccountNo.Text = searchadjust.Rows(0)("AccountNo")
                    AdjustName.Text = searchadjust.Rows(0)("AccountName")

                    AdjustDate.Value = searchadjust.Rows(0)("Date")
                    AdjustRemarks.Text = searchadjust.Rows(0)("Remarks")
                    adjustStatus.Text = searchadjust.Rows(0)("Status")
                    adjustApproved.Text = searchadjust.Rows(0)("ApprovedBy")

                    adjustCovered.Text = searchadjust.Rows(0)("BIllCovered")
                    billamountdue.Text = searchadjust.Rows(0)("oldAmountDue")
                    billdiscount.Text = searchadjust.Rows(0)("oldDiscount")
                    billPenalty.Text = searchadjust.Rows(0)("oldPenalty")
                    billadvancepayment.Text = searchadjust.Rows(0)("oldAdvance")
                    adjustAmountdue.Text = searchadjust.Rows(0)("NewAmountDue")
                    adjustDiscount.Text = searchadjust.Rows(0)("NewDiscount")
                    adjustPenalty.Text = searchadjust.Rows(0)("NewPenalty")
                    adjustAdvance.Text = searchadjust.Rows(0)("NewAdvance")

                    adjustBillnos.Items.Clear()

                    adjustBillnos.Items.Add(searchadjust.Rows(0)("BillNo"))

                    adjustBillnos.Text = searchadjust.Rows(0)("BillNo")

                    Dim getbillchargedetails As New DataTable
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                    stracs = "Select * from BillCharges where BillNumber = " & adjustBillnos.Text
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsda.SelectCommand = acscmd
                    acsda.Fill(getbillchargedetails)

                    If getbillchargedetails.Rows.Count = 0 Then
                    Else

                        billcharges.Rows.Clear()

                        For x = 0 To getbillchargedetails.Rows.Count - 1
                            billcharges.Rows.Add(getbillchargedetails(x)("Particulars"), Format(getbillchargedetails(x)("Amount"), "standard"))
                            'adjustCharges.Rows.Add(getbillchargedetails(x)("Particulars"), Format(getbillchargedetails(x)("Amount"), "standard"))
                        Next

                    End If

                    computetotalamount()
                    computenewtotalamount()

                    If searchadjust.Rows(0)("Status") = "Pending" Then
                        Posting.Show()
                    Else
                        Posting.Hide()
                    End If

                End If

            Else

            End If

        End If

    End Sub

    Private Sub Posting_Click(sender As Object, e As EventArgs) Handles Posting.Click

        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then

            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try
            Dim searchadjust As New DataTable

            stracs = "Select * from BIllAdjustment where RefNo = " & adjustRefNo.Text
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(searchadjust)

            If searchadjust.Rows.Count = 0 Then

                MsgBox("Please check Reference Number.")

            Else

                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                gettotalamount()

                stracs = "update BIllAdjustment Set Status = 'Posted', DatePosted = '" & Format(Now, "yyyy-MM-dd") & "', ApprovedBy = '" & My.Settings.Nickname & "' where RefNo = " & adjustRefNo.Text
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                stracs = "insert into AccountLedger ([ledgerAccountNo],[ledgerDate],[ledgerRefNo],[ledgerParticulars],[ledgerReading],
                    [ledgerConsumption],[ledgerAmount],[ledgerDiscount],[ledgerBalance]) values ('" _
                    & adjustAccountNo.Text & "', '" & Format(Now, "yyyy-MM-dd") & "', '" & adjustRefNo.Text & "', 'Bill Adjustment', " _
                    & "'', '', '" & Format(Double.Parse(lblnewtotal.Text) - Double.Parse(billTotal.Text), "standard") & "', '', '" _
                    & Format((Double.Parse(totalamount) + (Double.Parse(lblnewtotal.Text)) - Double.Parse(billTotal.Text)), "standard") & "')"

                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                stracs = "update Bills set " _
                    & "Adjustment = " & Double.Parse(adjustAmountdue.Text) - Double.Parse(billamountdue.Text) & ", " _
                    & "Discount = " & Double.Parse(adjustDiscount.Text) & ", " _
                    & "PenaltyAfterDue = " & Double.Parse(adjustPenalty.Text) & ", " _
                    & "AdvancePayment = " & Double.Parse(adjustAdvance.Text) & " where BillNo = " & adjustBillnos.Text
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                lockall()

                MsgBox("Adjustment posted.")
                adjustRefNo_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))

            End If

        Else

            MsgBox("Your account is not allowed for this process")

        End If

    End Sub

    Sub gettotalamount()

        Dim gettotalbillbalance As New DataTable
        Dim totalbillbalance As Double = 0

        gettotalbillbalance.Clear()
        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try
        'stracs = "select SUM(AmountDue), S from Bills where AccountNumber = '" & billList.Rows(x).Cells(2).Value & "' and BillStatus = 'Posted' and Cancelled = 'No' and IsPaid = 'No'"
        stracs = "select SUM(AmountDue) as amountdue, SUm(AdvancePayment) as advance, Sum(Discount) as discount, SUm(PenaltyAfterDue) as penalty from Bills where AccountNumber = '" & adjustAccountNo.Text & "' and BillStatus = 'Posted' and Cancelled = 'No' and IsPaid = 'No' and isPromisorry = 'No'"
        acscmd.Connection = acsconn
        acscmd.CommandText = stracs
        acsda.SelectCommand = acscmd
        acsda.Fill(gettotalbillbalance)

        If gettotalbillbalance.Rows.Count = 0 Then
            totalbillbalance = 0
        Else
            totalbillbalance = Val(gettotalbillbalance(0)("amountdue") + gettotalbillbalance(0)("penalty")) - Val(gettotalbillbalance(0)("advance") + gettotalbillbalance(0)("discount"))

        End If

        Dim gettotalchargebalance As New DataTable
        Dim totalbillchargebalance As Double = 0

        gettotalchargebalance.Clear()

        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        stracs = "select SUM(Amount) as amount from BillCharges where AccountNumber = '" & adjustAccountNo.Text & "' and Status = 'Posted' and Cancelled = 'No' and IsPaid = 'No' and Category = 'Others'"
        acscmd.Connection = acsconn
        acscmd.CommandText = stracs
        acsda.SelectCommand = acscmd
        acsda.Fill(gettotalchargebalance)

        If gettotalchargebalance.Rows.Count = 0 Then
            totalbillchargebalance = 0
        Else

            If IsDBNull(gettotalchargebalance(0)("amount")) = True Then
                totalbillchargebalance = 0
            Else
                totalbillchargebalance = Val(gettotalchargebalance(0)("amount"))
            End If

        End If

        Dim gettotalpn As New DataTable
        Dim totalpn As Double = 0
        stracs = "select SUM(Billing + Penalty) AS PNTOTAL from AddAdjustment where IsCollectionCreated = 'No' and Status = 'Posted' and Paid = 'No' AND AccountNumber = '" & adjustAccountNo.Text & "'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(gettotalpn)

        If IsDBNull(gettotalpn(0)("PNTOTAL")) = True Then
            totalpn = 0
        Else
            totalpn = gettotalpn(0)("PNTOTAL")
        End If

        totalamount = Val(totalbillbalance + totalbillchargebalance + totalpn)

    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click

        'If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
        '    createnew()
        'Else
        '    MsgBox("Your account cannot perform this process.")
        'End If



        waitingapproval.trans = "adjustbillnew"
        waitingapproval.ShowDialog()

        waitingapproval.TextBox1.Select()
        waitingapproval.TextBox1.Clear()


    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click

        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            Button1_Click(Nothing, New KeyEventArgs(Keys.Enter))
        Else
            MsgBox("Your account cannot perform this process.")
        End If


    End Sub

    Private Sub UpdateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateToolStripMenuItem.Click

        'If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
        '    updatemode()
        'Else
        '    MsgBox("Your account cannot perform this process.")
        'End If

        waitingapproval.trans = "adjustbillupdate"
        waitingapproval.ShowDialog()

        waitingapproval.TextBox1.Select()
        waitingapproval.TextBox1.Clear()

    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        clearall()
        lockall()
    End Sub

    Private Sub billingAdjustmentBill_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain
    End Sub
    Public MoveFormbillingAdjustmentBill As Boolean
    Public MovebillingAdjustmentBill_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormbillingAdjustmentBill = True
            Me.Cursor = Cursors.NoMove2D
            MovebillingAdjustmentBill_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormbillingAdjustmentBill Then
            Me.Location = Me.Location + (e.Location - MovebillingAdjustmentBill_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormbillingAdjustmentBill = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub billingAdjustmentBill_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Me.Activate()
    End Sub

    Private Sub adjustbill_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.BackColor = Color.SteelBlue
    End Sub

    Private Sub adjustbill_Deactivated(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.BackColor = Color.FromArgb(17, 153, 195)
    End Sub

    Private Sub ControlsClick(sender As Object, e As EventArgs) Handles _
        Label1.Click, Panel1.Click, GroupBox4.Click, AdjustRecords.Click, GroupBox3.Click, lblMode.Click,
        adjustRefNo.Click, AdjustDate.Click, AdjustRemarks.Click, adjustBillnos.Click, adjustCovered.Click, adjustSenior.Click,
        GroupBox6.Click, GroupBox2.Click, adjustAmountdue.Click, adjustPenalty.Click, adjustDiscount.Click, adjustAdvance.Click,
        billamountdue.Click, billadvancepayment.Click, billdiscount.Click, billPenalty.Click, billcharges.Click, saving.Click, Posting.Click ' etc.
        Me.Activate() 'Or Whatever
    End Sub

    Private Sub adjustCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles adjustCategory.SelectedIndexChanged

        If adjustCategory.Text = "Due to Leak" Then

            Dim checkid As New DataTable

            If associd = 0 Then
                stracs = "select AccountNo from BIllAdjustment where Category = 'Due to Leak' and AccountNo = '" & adjustAccountNo.Text & "'"
            Else
                stracs = "select AccountNo from BIllAdjustment where Category = 'Due to Leak' and AssocID = " & associd
            End If


            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(checkid)

            If checkid.Rows.Count > 0 Then

                MsgBox("One of the associated accounts or this account has a discount due to leak." & vbCrLf & "Adding discount due to leak for this account is no longer possible.")
                adjustCategory.SelectedIndex = -1

            Else



            End If


        End If

        If adjustCategory.Text = "Billing" Then

        End If

    End Sub

End Class