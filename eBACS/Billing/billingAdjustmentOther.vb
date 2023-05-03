Public Class billingAdjustmentOther
    Private Sub adjustCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles adjustCategory.SelectedIndexChanged

        adjustParticulars.Rows.Clear()

        If adjustCategory.Text = "Promissory" Then

            adjustCovered.Value = Now

            adjustBills.Enabled = True
            adjustParticulars.Rows.Clear()
            Button1.Show()
            'Dim getbilla As New DataTable


            Dim getarrears As New DataTable
            getarrears.Rows.Clear()
            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try

            stracs = "select * from Bills where AccountNumber = '" & adjustAccountNo.Text & "' and BillStatus = 'Posted' and IsPaid = 'No' and Cancelled = 'No' and isPromisorry = 'No' and IsCollectionCreated = 'No' order by BillNo"
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(getarrears)


            adjustBills.Rows.Clear()
            For t = 0 To getarrears.Rows.Count - 1

                'Dim loadbillarrears As New DataTable
                'loadbillarrears.Clear()
                'stracs = "select * from Bills where IsPaid = 'No' and BillNo = " & getarrears.Rows(t)("BillNo")
                'acscmd.Connection = acsconn
                'acscmd.CommandText = stracs
                'acsda.SelectCommand = acscmd
                'acsda.Fill(loadbillarrears)

                Dim loadchargesarrears As New DataTable
                loadchargesarrears.Clear()
                'stracs = "select a.Billno as Billno, a.BillingDate as BillingDate, (SUM(b.amount) + SUM(a.AmountDue) + SUM(a.PenaltyAfterDue)) - SUM(a.Discount) as amount from Bills a join BillCharges b on a.BillNo = b.BillNumber where a.IsPaid = 'No' and b.IsPaid = 'No' and a.BillNo = " & getarrears.Rows(t)("BillNo") & " and b.BillNumber = " & getarrears.Rows(t)("BillNo") & " group by a.BillNo, a.BillingDate"
                stracs = "select SUM(Amount) as Amount from BillCharges where IsPaid = 'No' and Cancelled = 'No' and BillNumber = " & getarrears.Rows(t)("BillNo")
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsda.SelectCommand = acscmd
                acsda.Fill(loadchargesarrears)

                Dim arrearscharge As Double

                If IsDBNull(loadchargesarrears.Rows(0)("amount")) = True Then
                    arrearscharge = 0
                Else
                    arrearscharge = loadchargesarrears.Rows(0)("amount")
                End If

                'adjustBills.Rows.Add(0, loadbillarrears.Rows(0)("Billno"), loadbillarrears.Rows(0)("BillingDate"), Format((Val(loadbillarrears.Rows(0)("AmountDue"))) - (Val(loadbillarrears.Rows(0)("Discount")) + Val(loadbillarrears.Rows(0)("AdvancePayment"))), "standard"), Format(Val(loadbillarrears.Rows(0)("PenaltyAfterDue")), "standard"), Format(Val(arrearscharge), "standard"), Format(Val((loadbillarrears.Rows(0)("AmountDue")) + Val(arrearscharge) + Val(loadbillarrears.Rows(0)("PenaltyAfterDue"))) - (Val(loadbillarrears.Rows(0)("Discount")) + Val(loadbillarrears.Rows(0)("AdvancePayment"))), "standard"))
                'adjustBills.Rows.Add(0, getarrears.Rows(t)("Billno"), getarrears.Rows(t)("BillingDate"), Format(Val(getarrears.Rows(t)("AmountDue")) - (Val(getarrears.Rows(t)("Discount")) + Val(getarrears.Rows(t)("AdvancePayment"))), "standard"), Format(Val(getarrears.Rows(t)("PenaltyAfterDue")), "standard"), Format(Val(arrearscharge), "standard"), Format((Val(getarrears.Rows(t)("AmountDue") + Val(getarrears.Rows(t)("PenaltyAfterDue"))) - (Val(getarrears.Rows(t)("Discount")) + Val(getarrears.Rows(t)("AdvancePayment")))) + arrearscharge, "standard"), Date.Parse(getarrears.Rows(t)("ReadingDate")).ToString("yyyy-MM-dd"))
                adjustBills.Rows.Add(0, getarrears.Rows(t)("Billno"), getarrears.Rows(t)("BillingDate"), Format((Val(getarrears.Rows(t)("AmountDue")) + Val(getarrears.Rows(t)("Adjustment"))) - (Val(getarrears.Rows(t)("Discount")) + Val(getarrears.Rows(t)("AdvancePayment"))), "standard"), Format(Val(getarrears.Rows(t)("PenaltyAfterDue")), "standard"), Format(Val(arrearscharge), "standard"), Format((Val(getarrears.Rows(t)("AmountDue") + Val(getarrears.Rows(t)("PenaltyAfterDue")) + Val(getarrears.Rows(t)("Adjustment"))) - (Val(getarrears.Rows(t)("Discount")) + Val(getarrears.Rows(t)("AdvancePayment")))) + arrearscharge, "standard"), Date.Parse(getarrears.Rows(t)("ReadingDate")).ToString("yyyy-MM-dd"), "Billing")

            Next



            Dim totallahat As Double

            For p = 0 To adjustBills.Rows.Count - 1

                If adjustBills.Rows(p).Cells(0).Value = -1 Then

                    totallahat = totallahat + Double.Parse(adjustBills.Rows(p).Cells(6).Value)

                Else

                End If

            Next

            totalamount.Text = Format(totallahat, "standard")

        ElseIf adjustCategory.Text = "Write-off" Then

            adjustBills.Enabled = True
            adjustParticulars.Rows.Clear()
            adjustPenalty.Clear()
            adjustCharges.Clear()
            adjustBilling.Clear()
            adjustBilling.Enabled = False
            'adjustCovered.SelectedIndex = -1
            adjustCovered.Enabled = False
            Button1.Hide()

            Dim getarrears As New DataTable
            getarrears.Rows.Clear()
            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try
            stracs = "select * from Bills where AccountNumber = '" & adjustAccountNo.Text & "' and BillStatus = 'Posted' and IsPaid = 'No' and Cancelled = 'No' and isPromisorry = 'No' and IsCollectionCreated = 'No' order by BillNo"
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(getarrears)


            adjustBills.Rows.Clear()
            For t = 0 To getarrears.Rows.Count - 1

                'Dim loadbillarrears As New DataTable
                'loadbillarrears.Clear()
                'stracs = "select * from Bills where IsPaid = 'No' and BillNo = " & getarrears.Rows(t)("BillNo")
                'acscmd.Connection = acsconn
                'acscmd.CommandText = stracs
                'acsda.SelectCommand = acscmd
                'acsda.Fill(loadbillarrears)

                Dim loadchargesarrears As New DataTable
                loadchargesarrears.Clear()
                'stracs = "select a.Billno as Billno, a.BillingDate as BillingDate, (SUM(b.amount) + SUM(a.AmountDue) + SUM(a.PenaltyAfterDue)) - SUM(a.Discount) as amount from Bills a join BillCharges b on a.BillNo = b.BillNumber where a.IsPaid = 'No' and b.IsPaid = 'No' and a.BillNo = " & getarrears.Rows(t)("BillNo") & " and b.BillNumber = " & getarrears.Rows(t)("BillNo") & " group by a.BillNo, a.BillingDate"
                stracs = "select SUM(Amount) as Amount from BillCharges where IsPaid = 'No' and Cancelled = 'No' and BillNumber = " & getarrears.Rows(t)("BillNo")
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsda.SelectCommand = acscmd
                acsda.Fill(loadchargesarrears)

                Dim arrearscharge As Double

                If IsDBNull(loadchargesarrears.Rows(0)("amount")) = True Then
                    arrearscharge = 0
                Else
                    arrearscharge = loadchargesarrears.Rows(0)("amount")
                End If

                'adjustBills.Rows.Add(0, loadbillarrears.Rows(0)("Billno"), loadbillarrears.Rows(0)("BillingDate"), Format((Val(loadbillarrears.Rows(0)("AmountDue"))) - (Val(loadbillarrears.Rows(0)("Discount")) + Val(loadbillarrears.Rows(0)("AdvancePayment"))), "standard"), Format(Val(loadbillarrears.Rows(0)("PenaltyAfterDue")), "standard"), Format(Val(arrearscharge), "standard"), Format(Val((loadbillarrears.Rows(0)("AmountDue")) + Val(arrearscharge) + Val(loadbillarrears.Rows(0)("PenaltyAfterDue"))) - (Val(loadbillarrears.Rows(0)("Discount")) + Val(loadbillarrears.Rows(0)("AdvancePayment"))), "standard"))
                adjustBills.Rows.Add(-1, getarrears.Rows(t)("Billno"), getarrears.Rows(t)("BillingDate"), Format((Val(getarrears.Rows(t)("AmountDue")) + Val(getarrears.Rows(t)("Adjustment"))) - (Val(getarrears.Rows(t)("Discount")) + Val(getarrears.Rows(t)("AdvancePayment"))), "standard"), Format(Val(getarrears.Rows(t)("PenaltyAfterDue")), "standard"), Format(Val(arrearscharge), "standard"), Format((Val(getarrears.Rows(t)("AmountDue") + Val(getarrears.Rows(t)("PenaltyAfterDue")) + Val(getarrears.Rows(t)("Adjustment"))) - (Val(getarrears.Rows(t)("Discount")) + Val(getarrears.Rows(t)("AdvancePayment")))) + arrearscharge, "standard"), Date.Parse(getarrears.Rows(t)("ReadingDate")).ToString("yyyy-MM-dd"), "Bill")

            Next

            Dim gettotalfb As New DataTable
            stracs = "select * from AddAdjustment where Paid = 'No' and Status = 'Posted' and IsCollectionCreated = 'No' and AccountNumber = '" & adjustAccountNo.Text & "'"
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(gettotalfb)

            For t = 0 To gettotalfb.Rows.Count - 1


                adjustBills.Rows.Add(-1, gettotalfb.Rows(t)("RefNo"), gettotalfb.Rows(t)("BillingDate"),
                                     Format(gettotalfb.Rows(t)("Billing"), "Standard"),
                                     Format(gettotalfb.Rows(t)("Penalty"), "Standard"),
                                     Format(gettotalfb.Rows(t)("Charges"), "Standard"),
                                     Format(gettotalfb.Rows(t)("Billing") + gettotalfb.Rows(t)("Penalty") + gettotalfb.Rows(t)("Charges"), "Standard"), "", "FB")

            Next


            Dim totallahat As Double

            For p = 0 To adjustBills.Rows.Count - 1

                If adjustBills.Rows(p).Cells(0).Value = -1 Then

                    totallahat = totallahat + Double.Parse(adjustBills.Rows(p).Cells(6).Value)

                Else

                End If

            Next

            totalamount.Text = Format(totallahat, "standard")

        Else

            adjustBills.Rows.Clear()
            adjustParticulars.Rows.Clear()
            adjustBills.Enabled = False
            adjustPenalty.Text = "0.00"
            adjustCharges.Text = "0.00"
            Button1.Show()


            'adjustCovered.Items.Clear()
            'adjustCovered.Items.Add(MonthName(Month(Now)) & " " & Year(Now))
            'adjustCovered.Text = MonthName(Month(Now)) & " " & Year(Now)

        End If

    End Sub

    Public Sub adjustAccountNo_KeyUp(sender As Object, e As KeyEventArgs) Handles adjustAccountNo.KeyDown

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
                AdjustName.Text = getaccount.Rows(0)("Firstname") & " " & getaccount.Rows(0)("Middlename") & " " & getaccount.Rows(0)("Lastname")

                Dim getrecords As New DataTable
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                stracs = "select * from AddAdjustment where AccountNumber = '" & adjustAccountNo.Text & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(getrecords)
                AdjustRecords.Rows.Clear()

                For t = 0 To getrecords.Rows.Count - 1

                    AdjustRecords.Rows.Add(getrecords.Rows(t)("RefNo"), getrecords.Rows(t)("Particulars"), getrecords.Rows(t)("Remarks"), Format(Double.Parse(getrecords.Rows(t)("Billing")) + Double.Parse(getrecords.Rows(t)("Charges")) + Double.Parse(getrecords.Rows(t)("Penalty")), "Standard"), getrecords.Rows(t)("Status"))

                Next

            End If

        End If

    End Sub

    Private Sub accSearch_Click(sender As Object, e As EventArgs) Handles accSearch.Click

        SearchAccount.searchingform = "OtherAdjustment"
        SearchAccount.ShowDialog()

    End Sub

    Private Sub adjustBills_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles adjustBills.CurrentCellDirtyStateChanged

        'RemoveHandler adjustBills.CurrentCellDirtyStateChanged,
        '    AddressOf adjustBills_CurrentCellDirtyStateChanged

        'If TypeOf adjustBills.CurrentCell Is DataGridViewCheckBoxCell Then

        'Dim Checked As Boolean = CType(adjustBills.CurrentCell.Value, Boolean)
        'If Checked Then
        '    MessageBox.Show("You have checked")
        'Else
        '    MessageBox.Show("You have un-checked")
        'End If
        'End If

        'AddHandler adjustBills.CurrentCellDirtyStateChanged,
        'AddressOf adjustBills_CurrentCellDirtyStateChanged

        adjustBills.EndEdit()

        adjustParticulars.Rows.Clear()

        Dim totallahat As Double
        Dim totalpenalty As Double
        Dim totalcharges As Double

        adjustCovered.Value = Now

        For p = 0 To adjustBills.Rows.Count - 1

            If adjustBills.Rows(p).Cells(0).Value = -1 Then

                totallahat = totallahat + Double.Parse(adjustBills.Rows(p).Cells(6).Value)
                totalcharges = totalcharges + Double.Parse(adjustBills.Rows(p).Cells(5).Value)
                totalpenalty = totalpenalty + Double.Parse(adjustBills.Rows(p).Cells(4).Value)

                adjustCovered.Value = adjustBills.Rows(p).Cells(2).Value

            Else

            End If

        Next

        adjustBilling.Text = Format(0, "standard")
        adjustCharges.Text = Format(totalcharges, "standard")
        adjustPenalty.Text = Format(totalpenalty, "standard")
        totalamount.Text = Format(totallahat, "standard")
        adjustCovered.Value = adjustBills.Rows(adjustBills.CurrentCellAddress.Y).Cells(2).Value

    End Sub

    Public Sub clearall()

        adjustRefNo.Clear()
        AdjustRemarks.Clear()
        AdjustDate.Value = Now
        adjustStatus.Text = ""
        adjustApproved.Text = ""
        adjustBills.Rows.Clear()
        adjustCategory.SelectedIndex = -1
        adjustCovered.Value = Now
        adjustBilling.Clear()
        adjustCharges.Clear()
        adjustParticulars.Rows.Clear()

    End Sub

    Public Sub unlockall()

        adjustRefNo.ReadOnly = True
        AdjustRemarks.ReadOnly = False
        AdjustDate.Enabled = True
        adjustBills.Enabled = True
        adjustCategory.Enabled = True
        adjustCovered.Enabled = True
        adjustBilling.ReadOnly = False
        adjustCharges.ReadOnly = False
        adjustParticulars.Enabled = True

    End Sub

    Public Sub lockall()

        adjustRefNo.ReadOnly = False
        AdjustRemarks.ReadOnly = True
        AdjustDate.Enabled = False
        adjustBills.Enabled = False
        adjustCategory.Enabled = False
        adjustCovered.Enabled = False
        adjustBilling.ReadOnly = True
        adjustCharges.ReadOnly = True
        adjustParticulars.Enabled = False

    End Sub

    Public Sub Refreshall()

        lockall()
        clearall()
        lblMode.Text = "Mode"
        lblMode.Hide()
        adjustAccountNo.ReadOnly = False

    End Sub

    Public Sub createnew()

        Dim getaccount As New DataTable
        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try
        getaccount.Rows.Clear()

        stracs = "select * from Customers where AccountNo = '" & adjustAccountNo.Text & "'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(getaccount)

        If getaccount.Rows.Count = 0 Then
            MsgBox("No account found.")
        Else

            Dim getrecs As New DataTable
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            getrecs.Rows.Clear()

            stracs = "select * from AddAdjustment where AccountNumber = '" & adjustAccountNo.Text & "' and Status = 'Pending'"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(getrecs)

            If getrecs.Rows.Count = 0 Then

                adjustAccountNo.ReadOnly = True
                lblMode.Text = "Create New"
                lblMode.ForeColor = Color.Green
                adjustsaving.Show()
                lblMode.Show()
                unlockall()
                clearall()

            Else

                MsgBox("There is a pending adjustment for this account.")

            End If

        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If adjustCategory.SelectedIndex = -1 Or IsNumeric(adjustBilling.Text) = False Or IsNumeric(adjustPenalty.Text) = False Or IsNumeric(adjustCharges.Text) = False Or adjustCovered.Text = "" Then

        Else

            If adjustCategory.Text = "Promissory" Then

                adjustParticulars.Rows.Add(adjustCategory.Text, adjustCovered.Text, Format(adjustBilling.Text, "standard"), Format(adjustPenalty.Text, "standard"), Format(adjustCharges.Text, "standard"), Format(Double.Parse(adjustBilling.Text) + Double.Parse(adjustCharges.Text) + Double.Parse(adjustPenalty.Text), "standard"), adjustBills.Rows(0).Cells(7).Value)
            Else
                adjustParticulars.Rows.Add(adjustCategory.Text, adjustCovered.Text, Format(adjustBilling.Text, "standard"), Format(adjustPenalty.Text, "standard"), Format(adjustCharges.Text, "standard"), Format(Double.Parse(adjustBilling.Text) + Double.Parse(adjustCharges.Text) + Double.Parse(adjustPenalty.Text), "standard"))
            End If

            Dim totalpromi As Double

            For p = 0 To adjustParticulars.Rows.Count - 1

                totalpromi = totalpromi + Double.Parse(adjustParticulars.Rows(p).Cells(5).Value)

            Next
            totalProm.Text = Format(totalpromi, "standard")


            adjustCharges.Text = Format(0, "standard")
            adjustPenalty.Text = Format(0, "standard")

        End If

    End Sub

    Private Sub saving_Click(sender As Object, e As EventArgs) Handles adjustsaving.Click

        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            saving()
        Else
            MsgBox("Your account cannot perform this process.")
        End If

    End Sub

    Public Sub saving()

        If AdjustRemarks.Text = "" Or adjustParticulars.Rows.Count = 0 Then

            MsgBox("Remarks or adjusment list empty.")

        Else

            If adjustCategory.Text = "Promissory" Then

                If totalamount.Text = totalProm.Text Then

                    Dim getbilladjustmentno As New DataTable
                    Try
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    Catch ex As Exception
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    End Try
                    stracs = "select number from tbllogicnumbers where id = 5"
                    acscmd.Connection = acsconn
                    acscmd.CommandText = stracs
                    acsda.SelectCommand = acscmd
                    acsda.Fill(getbilladjustmentno)

                    adjustRefNo.Text = getbilladjustmentno.Rows(0)("number") + 1

                    For m = 0 To adjustBills.Rows.Count - 1

                        If adjustBills.Rows(m).Cells(0).Value = -1 Then

                            stracs = "update Bills set isPromisorry = 'YesPending', PromisorryNo = " & adjustRefNo.Text & " where BillNo = " & adjustBills.Rows(m).Cells(1).Value
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acscmd.ExecuteNonQuery()
                            acscmd.Dispose()

                            stracs = "update BillCharges set isPromisorry = 'YesPending', PromisorryNo = " & adjustRefNo.Text & " where BillNumber = " & adjustBills.Rows(m).Cells(1).Value
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acscmd.ExecuteNonQuery()
                            acscmd.Dispose()

                        Else

                        End If


                    Next

                    For k = 0 To adjustParticulars.Rows.Count - 1

                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "insert into AddAdjustment ([RefNo],[AccountNumber],[AccountName],[Particulars],[Remarks],[DateCreated],[Billing],[Penalty]
                                ,[Charges],[ApprovedBy],[BillingDate],[Paid],[Status],[CreatedBy],DateRead) values (" _
                                & adjustRefNo.Text & ", '" & adjustAccountNo.Text & "', '" & AdjustName.Text.ToString.Replace("'", "''") & "', '" & adjustParticulars.Rows(k).Cells(0).Value & "', '" & AdjustRemarks.Text.ToString.Replace("'", "''") & "', '" _
                                & Format(AdjustDate.Value, "yyyy-MM-dd") & "', " & Double.Parse(adjustParticulars.Rows(k).Cells(2).Value) & ", " & Double.Parse(adjustParticulars.Rows(k).Cells(3).Value) & ", " _
                                & Double.Parse(adjustParticulars.Rows(k).Cells(4).Value) & ", 'Pending', '" & adjustParticulars.Rows(k).Cells(1).Value & "', 'No', 'Pending', '" & My.Settings.Nickname & "','" & adjustParticulars.Rows(k).Cells(6).Value & "')"

                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                    Next

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    stracs = "update tbllogicnumbers set number = number + 1 where id = 5"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                    adjustAccountNo.ReadOnly = False
                    lblMode.Text = "Mode"
                    lblMode.ForeColor = Color.Black
                    adjustsaving.Hide()
                    lblMode.Hide()
                    lockall()

                    adjustRefNo_KeyUp(Nothing, New KeyEventArgs(Keys.Enter))

                Else
                    MsgBox("Total bill amount and total promissory amount not equal.")
                End If



            End If

            If adjustCategory.Text = "Forwarded Balance" Or adjustCategory.Text = "Remaining Cons." Then

                Dim getbilladjustmentno As New DataTable
                Try
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                Catch ex As Exception
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                End Try


                stracs = "select number from tbllogicnumbers where id = 5"
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsda.SelectCommand = acscmd
                acsda.Fill(getbilladjustmentno)

                adjustRefNo.Text = getbilladjustmentno.Rows(0)("number") + 1

                For k = 0 To adjustParticulars.Rows.Count - 1

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    stracs = "insert into AddAdjustment ([RefNo],[AccountNumber],[AccountName],[Particulars],[Remarks],[DateCreated],[Billing],
                            [Penalty],[Charges],[ApprovedBy],[Paid],[Status],[CreatedBy],[BillingDate],DateRead) values (" _
                            & adjustRefNo.Text & ", '" & adjustAccountNo.Text & "', '" & AdjustName.Text.ToString.Replace("'", "''") & "', '" & adjustParticulars.Rows(k).Cells(0).Value & "', '" & AdjustRemarks.Text.ToString.Replace("'", "''") & "', '" _
                            & Format(AdjustDate.Value, "yyyy-MM-dd") & "', " & Double.Parse(adjustParticulars.Rows(k).Cells(2).Value) & ", " _
                            & Double.Parse(adjustParticulars.Rows(k).Cells(3).Value) & ", " & Double.Parse(adjustParticulars.Rows(k).Cells(4).Value) & ", 'Pending', 'No', 'Pending', '" & My.Settings.Nickname & "', '" & adjustParticulars.Rows(k).Cells(1).Value & "' ,'" & AdjustDate.Text & "')"

                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                Next

                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                stracs = "update tbllogicnumbers set number = number + 1 where id = 5"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                adjustAccountNo.ReadOnly = False
                lblMode.Text = "Mode"
                lblMode.ForeColor = Color.Black
                adjustsaving.Hide()
                lblMode.Hide()
                lockall()

                adjustRefNo_KeyUp(Nothing, New KeyEventArgs(Keys.Enter))

            End If

            If adjustCategory.Text = "Write-off" Then

                Dim getbilladjustmentno As New DataTable
                Try
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                Catch ex As Exception
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                End Try

                stracs = "select number from tbllogicnumbers where id = 5"
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsda.SelectCommand = acscmd
                acsda.Fill(getbilladjustmentno)

                adjustRefNo.Text = getbilladjustmentno.Rows(0)("number") + 1

                Try
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                Catch ex As Exception
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                End Try

                Dim checkrefno As New DataTable

                checkrefno.Rows.Clear()
                stracs = "select RefNo from AddAdjustment where RefNo = " & adjustRefNo.Text
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsda.SelectCommand = acscmd
                acsda.Fill(checkrefno)

                If checkrefno.Rows.Count > 0 Then

                    MsgBox("Reference number already used. Please click save again." & vbCrLf & "If this message repeats, please contact admin.")

                Else

                    For m = 0 To adjustBills.Rows.Count - 1

                        If adjustBills.Rows(m).Cells(0).Value = -1 Then

                            If adjustBills.Rows(m).Cells(8).Value = "Bill" Then

                                stracs = "update Bills set Status = 'Write-off', PromisorryNo = " & adjustRefNo.Text & " where BillNo = " & adjustBills.Rows(m).Cells(1).Value
                                acscmd.CommandText = stracs
                                acscmd.Connection = acsconn
                                acscmd.ExecuteNonQuery()
                                acscmd.Dispose()

                                stracs = "update BillCharges set Status = 'Write-off', PromisorryNo = " & adjustRefNo.Text & " where BillNumber = " & adjustBills.Rows(m).Cells(1).Value
                                acscmd.CommandText = stracs
                                acscmd.Connection = acsconn
                                acscmd.ExecuteNonQuery()
                                acscmd.Dispose()

                            End If

                            If adjustBills.Rows(m).Cells(8).Value = "FB" Then

                                stracs = "update AddAdjustment set Status = 'Write-off', CrNo = " & adjustRefNo.Text & " where RefNo = " & adjustBills.Rows(m).Cells(1).Value
                                acscmd.CommandText = stracs
                                acscmd.Connection = acsconn
                                acscmd.ExecuteNonQuery()
                                acscmd.Dispose()

                            End If

                        Else

                        End If


                    Next

                    For k = 0 To adjustParticulars.Rows.Count - 1

                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "insert into AddAdjustment ([RefNo],[AccountNumber],[AccountName],[Particulars],[Remarks],[DateCreated],[Billing],[Penalty]
                                ,[Charges],[ApprovedBy],[BillingDate],[Paid],[Status],[CreatedBy],DateRead) values (" _
                                & adjustRefNo.Text & ", '" & adjustAccountNo.Text & "', '" & AdjustName.Text.ToString.Replace("'", "''") & "', '" & adjustParticulars.Rows(k).Cells(0).Value & "', '" & AdjustRemarks.Text.ToString.Replace("'", "''") & "', '" _
                                & Format(AdjustDate.Value, "yyyy-MM-dd") & "', " & Double.Parse(adjustParticulars.Rows(k).Cells(2).Value) & ", " & Double.Parse(adjustParticulars.Rows(k).Cells(3).Value) & ", " _
                                & Double.Parse(adjustParticulars.Rows(k).Cells(4).Value) & ", 'Pending', '" & adjustParticulars.Rows(k).Cells(1).Value & "', 'No', 'Pending', '" & My.Settings.Nickname & "','" & adjustParticulars.Rows(k).Cells(6).Value & "')"

                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                    Next

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    stracs = "update tbllogicnumbers set number = number + 1 where id = 5"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                    adjustAccountNo.ReadOnly = False
                    lblMode.Text = "Mode"
                    lblMode.ForeColor = Color.Black
                    adjustsaving.Hide()
                    lblMode.Hide()
                    lockall()

                    adjustRefNo_KeyUp(Nothing, New KeyEventArgs(Keys.Enter))

                End If

            End If

        End If
    End Sub

    Private Sub adjustRefNo_KeyUp(sender As Object, e As KeyEventArgs) Handles adjustRefNo.KeyDown

        If Control.ModifierKeys = Keys.Control Then

            If e.KeyCode = Keys.Delete Then
                'MsgBox("asdopia")
            End If

        End If


        If e.KeyValue = Keys.Enter Then

            If IsNumeric(adjustRefNo.Text) = True Then

                Dim searchrefno As New DataTable
                Try
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                Catch ex As Exception
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                End Try

                stracs = "select * from AddAdjustment where RefNo = " & adjustRefNo.Text
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsda.SelectCommand = acscmd
                acsda.Fill(searchrefno)

                If searchrefno.Rows.Count = 0 Then
                    clearall()
                Else
                    adjustParticulars.Rows.Clear()

                    adjustAccountNo.Text = searchrefno(0)("AccountNumber")
                    AdjustName.Text = searchrefno(0)("AccountName")
                    adjustStatus.Text = searchrefno(0)("Status")
                    adjustApproved.Text = searchrefno(0)("ApprovedBy")
                    AdjustDate.Value = searchrefno(0)("DateCreated")
                    AdjustRemarks.Text = searchrefno(0)("Remarks")
                    adjustCategory.Text = searchrefno(0)("Particulars")

                    If searchrefno(0)("Status") = "Pending" Then
                        Posting.Show()
                    Else
                        Posting.Hide()
                        adjustApproved.Text = searchrefno(0)("ApprovedBy") & " / " & searchrefno(0)("DateApproved")
                    End If

                    For t = 0 To searchrefno.Rows.Count - 1
                        adjustParticulars.Rows.Add(searchrefno(t)("Particulars"), searchrefno(t)("BillingDate"), Format(searchrefno(t)("Billing"), "standard"), Format(searchrefno(t)("Penalty"), "standard"), Format(searchrefno(t)("Charges"), "standard"), Format(Val(searchrefno(t)("Billing")) + Val(searchrefno(t)("Penalty")) + Val(searchrefno(t)("Charges")), "standard"))
                    Next

                    Dim getarrears As New DataTable
                    getarrears.Rows.Clear()
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    stracs = "select * from Bills where AccountNumber = '" & adjustAccountNo.Text & "' and IsPaid = 'No' and Cancelled = 'No' and PromisorryNo = " & adjustRefNo.Text
                    acscmd.Connection = acsconn
                    acscmd.CommandText = stracs
                    acsda.SelectCommand = acscmd
                    acsda.Fill(getarrears)

                    adjustBills.Rows.Clear()

                    For t = 0 To getarrears.Rows.Count - 1

                        'Dim loadbillarrears As New DataTable
                        'loadbillarrears.Clear()
                        'stracs = "select * from Bills where IsPaid = 'No' and BillNo = " & getarrears.Rows(t)("BillNo")
                        'acscmd.Connection = acsconn
                        'acscmd.CommandText = stracs
                        'acsda.SelectCommand = acscmd
                        'acsda.Fill(loadbillarrears)

                        Dim loadchargesarrears As New DataTable
                        loadchargesarrears.Clear()
                        stracs = "select SUM(Amount) as Amount from BillCharges where BillNumber = " & getarrears.Rows(t)("BillNo")
                        acscmd.Connection = acsconn
                        acscmd.CommandText = stracs
                        acsda.SelectCommand = acscmd
                        acsda.Fill(loadchargesarrears)

                        Dim arrearscharge As Double

                        If IsDBNull(loadchargesarrears.Rows(0)("amount")) = True Then
                            arrearscharge = 0
                        Else
                            arrearscharge = loadchargesarrears.Rows(0)("amount")
                        End If

                        Dim totalamountdue As Double = Format(Val(getarrears.Rows(t)("AmountDue")) - (Val(getarrears.Rows(t)("Discount")) + Val(getarrears.Rows(t)("AdvancePayment"))), "standard")

                        adjustBills.Rows.Add(-1, getarrears.Rows(t)("Billno"), getarrears.Rows(t)("BillingDate"), Format(Val(getarrears.Rows(t)("AmountDue")) - (Val(getarrears.Rows(t)("Discount")) + Val(getarrears.Rows(t)("AdvancePayment"))), "standard"), Format(Val(getarrears.Rows(t)("PenaltyAfterDue")), "standard"), Format(Val(arrearscharge), "standard"), Format((Val(getarrears.Rows(t)("AmountDue") + Val(getarrears.Rows(t)("PenaltyAfterDue"))) - (Val(getarrears.Rows(t)("Discount")) + Val(getarrears.Rows(t)("AdvancePayment")))) + arrearscharge, "standard"))

                    Next

                    Dim totallahat As Double

                    For p = 0 To adjustBills.Rows.Count - 1

                        If adjustBills.Rows(p).Cells(0).Value = -1 Then

                            totallahat = totallahat + Double.Parse(adjustBills.Rows(p).Cells(6).Value)

                        Else

                        End If

                    Next
                    totalamount.Text = Format(totallahat, "standard")

                    Dim totalpromi As Double

                    For p = 0 To adjustParticulars.Rows.Count - 1

                        totalpromi = totalpromi + Double.Parse(adjustParticulars.Rows(p).Cells(5).Value)

                    Next
                    totalProm.Text = Format(totalpromi, "standard")

                End If

            Else

            End If



        End If



    End Sub

    Private Sub Posting_Click(sender As Object, e As EventArgs) Handles Posting.Click

        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then

            If adjustCategory.Text = "Promissory" Then

                Try
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                Catch ex As Exception
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                End Try

                Dim searchbill As New DataTable

                stracs = "select * from Bills where PromisorryNo = " & adjustRefNo.Text
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(searchbill)

                If searchbill.Rows.Count = 0 Then
                Else

                    If searchbill.Rows(0)("IsCollectionCreated") = "Yes" Or searchbill.Rows(0)("IsPaid") = "Yes" Or searchbill.Rows(0)("Cancelled") = "Yes" Then

                        MsgBox("You cannot post this promissory anymore.")

                    Else

                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "update AddAdjustment set Status = 'Posted', ApprovedBy = '" & My.Settings.Nickname & "', DateApproved = '" & Format(Now, "yyyy-MM-dd") & "' where RefNo = " & adjustRefNo.Text
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        For m = 0 To adjustBills.Rows.Count - 1

                            If adjustBills.Rows(m).Cells(0).Value = -1 Then

                                stracs = "update Bills set isPromisorry = 'YesPosted', PromisorryNo = " & adjustRefNo.Text & " where BillNo = " & adjustBills.Rows(m).Cells(1).Value
                                acscmd.CommandText = stracs
                                acscmd.Connection = acsconn
                                acscmd.ExecuteNonQuery()
                                acscmd.Dispose()

                                stracs = "update BillCharges set isPromisorry = 'YesPosted', PromisorryNo = " & adjustRefNo.Text & " where BillNumber = " & adjustBills.Rows(m).Cells(1).Value
                                acscmd.CommandText = stracs
                                acscmd.Connection = acsconn
                                acscmd.ExecuteNonQuery()
                                acscmd.Dispose()

                            Else

                            End If

                        Next

                        adjustRefNo_KeyUp(Nothing, New KeyEventArgs(Keys.Enter))


                    End If

                End If

            End If

            If adjustCategory.Text = "Forwarded Balance" Or adjustCategory.Text = "Remaining Cons." Then

                Dim searchrefno As New DataTable
                Try
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                Catch ex As Exception
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                End Try

                stracs = "select * from AddAdjustment where RefNo = " & adjustRefNo.Text
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsda.SelectCommand = acscmd
                acsda.Fill(searchrefno)

                If searchrefno.Rows.Count = 0 Then
                    clearall()
                Else

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    stracs = "update AddAdjustment set Status = 'Posted', ApprovedBy = '" & My.Settings.Nickname & "', DateApproved = '" & Format(Now, "yyyy-MM-dd") & "' where RefNo = " & adjustRefNo.Text
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                    For t = 0 To adjustParticulars.Rows.Count - 1

                        Dim arrearsbill As Double = 0
                        Dim arrearscharge As Double = 0

                        Dim loadbillarrears As New DataTable
                        loadbillarrears.Clear()

                        stracs = "select (SUM(AmountDue) + SUM(PenaltyAfterDue)) - (SUM(AdvancePayment) + SUM(Discount)) as amountarrears 
                        from Bills where IsPaid = 'No' and Cancelled = 'No' and AccountNumber = '" _
                                & searchrefno.Rows(0)("AccountNumber") & "' and BillStatus = 'Posted' and IsCollectionCreated = 'No' and isPromisorry = 'No'"
                        acscmd.Connection = acsconn
                        acscmd.CommandText = stracs
                        acsda.SelectCommand = acscmd
                        acsda.Fill(loadbillarrears)

                        If IsDBNull(loadbillarrears(0)("amountarrears")) = True Then
                            arrearsbill = 0
                        Else
                            arrearsbill = loadbillarrears(0)("amountarrears")
                        End If

                        Dim loadchargesarrears As New DataTable
                        loadchargesarrears.Clear()
                        'stracs = "select a.Billno as Billno, a.BillingDate as BillingDate, (SUM(b.amount) + SUM(a.AmountDue) + SUM(a.PenaltyAfterDue)) - SUM(a.Discount) as amount from Bills a join BillCharges b on a.BillNo = b.BillNumber where a.IsPaid = 'No' and b.IsPaid = 'No' and a.BillNo = " & getarrears.Rows(t)("BillNo") & " and b.BillNumber = " & getarrears.Rows(t)("BillNo") & " group by a.BillNo, a.BillingDate"
                        stracs = "select SUM(Amount) as Amount from BillCharges where IsPaid = 'No' and Cancelled = 'No' and 
                        AccountNumber = '" & searchrefno.Rows(0)("AccountNumber") & "' and Status = 'Posted' and Category = 'Others'
                        and IsCollectionCreated = 'No' and isPromisorry = 'No'"
                        acscmd.Connection = acsconn
                        acscmd.CommandText = stracs
                        acsda.SelectCommand = acscmd
                        acsda.Fill(loadchargesarrears)

                        If IsDBNull(loadchargesarrears(0)("Amount")) = True Then
                            arrearscharge = 0
                        Else
                            arrearscharge = loadchargesarrears(0)("Amount")
                        End If

                        Dim getbillingpn As New DataTable
                        stracs = "select (SUM(Billing) + SUM(Penalty)) as billpn 
                     from AddAdjustment where AccountNumber = '" & searchrefno.Rows(0)("AccountNumber") & "' and " _
                            & "Paid = 'No' and IsCollectionCreated = 'No' and Status = 'Posted'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acsda.SelectCommand = acscmd
                        acsda.Fill(getbillingpn)

                        Dim totalpn As Double

                        If IsDBNull(getbillingpn.Rows(0)("billpn")) = True Then
                            totalpn = 0
                        Else
                            totalpn = Double.Parse(getbillingpn(0)("billpn"))
                        End If



                        stracs = "insert into AccountLedger (ledgerAccountNo, ledgerDate, ledgerRefNo, ledgerParticulars, ledgerReading, ledgerConsumption, ledgerDiscount, ledgerAmount, ledgerBalance) values ('" _
                                & searchrefno.Rows(0)("AccountNumber") & "', '" _
                                & Format(Date.Parse(Now), "yyyy-MM-dd") & "', '" _
                                & adjustRefNo.Text & "', '" _
                                & adjustCategory.Text & "', '" _
                                & "', '" _
                                & "', '" _
                                & "', '" _
                                & adjustParticulars.Rows(t).Cells(5).Value & "', '" _
                                & Format(Val(arrearsbill) + Val(arrearscharge) + Val(totalpn), "Standard") & "')"

                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                    Next

                    adjustRefNo_KeyUp(Nothing, New KeyEventArgs(Keys.Enter))

                End If

            End If

        Else
            MsgBox("Your account cannot perform this process.")
        End If



    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            saving_Click(Nothing, New KeyEventArgs(Keys.Enter))
        Else
            MsgBox("Your account cannot perform this process.")
        End If
    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click

        'If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
        '    createnew()
        'Else
        '    MsgBox("Your account cannot perform this process.")
        'End If

        waitingapproval.trans = "adjustpn"
        waitingapproval.ShowDialog()

        waitingapproval.TextBox1.Select()
        waitingapproval.TextBox1.Clear()

    End Sub

    Private Sub billingAdjustmentOther_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain
    End Sub
    Public MoveFormbillingAdjustmentOthers As Boolean
    Public MovebillingAdjustmentOthers_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormbillingAdjustmentOthers = True
            Me.Cursor = Cursors.NoMove2D
            MovebillingAdjustmentOthers_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormbillingAdjustmentOthers Then
            Me.Location = Me.Location + (e.Location - MovebillingAdjustmentOthers_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormbillingAdjustmentOthers = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub billingAdjustmentOther_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Me.Activate()
    End Sub

    Private Sub pn_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.BackColor = Color.SteelBlue
    End Sub

    Private Sub pn_Deactivated(sender As Object, e As EventArgs) Handles Me.Deactivate

        Me.BackColor = Color.FromArgb(17, 153, 195)

    End Sub

    Private Sub ControlsClick(sender As Object, e As EventArgs) Handles _
        Label1.Click, Panel1.Click, adjustAccountNo.Click, AdjustName.Click, AdjustRecords.Click, lblMode.Click,
        GroupBox3.Click, AdjustDate.Click, AdjustRemarks.Click, adjustRefNo.Click, GroupBox1.Click, adjustBills.Click,
        adjustParticulars.Click, adjustCategory.Click, adjustPenalty.Click, adjustCharges.Click, Button1.Click,
        adjustsaving.Click, Posting.Click ' etc.
        Me.Activate() 'Or Whatever
    End Sub

    Private Sub UpdateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateToolStripMenuItem.Click

    End Sub

    Private Sub AdjustDate_ValueChanged(sender As Object, e As EventArgs) Handles AdjustDate.ValueChanged

        'adjustCovered.Value = Now
        'adjustCovered.Items.Add(MonthName(Month(AdjustDate.Value)) & " " & Year(AdjustDate.Value))
        'adjustCovered.Text = MonthName(Month(AdjustDate.Value)) & " " & Year(AdjustDate.Value)

    End Sub

End Class