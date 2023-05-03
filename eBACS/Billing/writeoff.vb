Public Class writeoff

    Public Sub clearall()

        adjustBills.Rows.Clear()
        totalamount.Text = ""
        adjustRefNo.Clear()
        AdjustRemarks.Clear()
        AdjustDate.Value = Now
        adjustsaving.Hide()
        AdjustName.Clear()
        lblMode.Text = ""
        AdjustRemarks.ReadOnly = True

    End Sub

    Private Sub writeoff_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.MdiParent = eBACSmain

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        Dim autocomplete As New DataTable

        autocomplete.Clear()

        stracs = "SELECT AccountNo FROM Customers"
        acscmd.Connection = acsconn
        acscmd.CommandText = stracs
        acsda.SelectCommand = acscmd
        acsda.Fill(autocomplete)

        adjustAccountNo.AutoCompleteCustomSource.Clear()

        adjustAccountNo.AutoCompleteMode = AutoCompleteMode.None
        adjustAccountNo.AutoCompleteSource = AutoCompleteSource.None

        For x = 0 To autocomplete.Rows.Count - 1
            adjustAccountNo.AutoCompleteCustomSource.Add(autocomplete.Rows(x)("AccountNo"))
        Next

        adjustAccountNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        adjustAccountNo.AutoCompleteSource = AutoCompleteSource.CustomSource

    End Sub

    Private Sub adjustAccountNo_KeyDown(sender As Object, e As KeyEventArgs) Handles adjustAccountNo.KeyDown

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

                stracs = "select RefNo,Remarks, (Billing + Penalty + Charges) as Amount from Writeoff where AccountNumber = '" & adjustAccountNo.Text & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(getrecords)
                AdjustRecords.Rows.Clear()

                For t = 0 To getrecords.Rows.Count - 1

                    AdjustRecords.Rows.Add(getrecords.Rows(t)("RefNo"), getrecords.Rows(t)("Remarks"), Format(getrecords.Rows(t)("Amount"), "Standard"))

                Next

            End If

            adjustBills.Rows.Clear()

        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub NewToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem1.Click

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

            If getaccount.Rows(0)("CustomerStatus") = "Disconnected" Then

                If getaccount.Rows(0)("CompanyName") = "" Or IsDBNull(adjustAccountNo.Text = getaccount.Rows(0)("CompanyName")) = True Then
                    AdjustName.Text = getaccount.Rows(0)("Firstname") & " " & getaccount.Rows(0)("Middlename") & " " & getaccount.Rows(0)("Lastname")
                Else
                    AdjustName.Text = getaccount.Rows(0)("CompanyName")
                End If

                If My.Settings.Cservice = "Yes" Then

                    waitingapproval.trans = "writeoff"
                    waitingapproval.ShowDialog()

                    waitingapproval.TextBox1.Select()
                    waitingapproval.TextBox1.Clear()

                Else

                    MsgBox("Your account cannot perform this process.")

                End If

            End If

        End If

    End Sub

    Public Sub createnew()

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
            adjustBills.Rows.Add(-1, getarrears.Rows(t)("Billno"), getarrears.Rows(t)("BillingDate"), Format((Val(getarrears.Rows(t)("AmountDue")) + Val(getarrears.Rows(t)("Adjustment"))) - (Val(getarrears.Rows(t)("Discount")) + Val(getarrears.Rows(t)("AdvancePayment"))), "standard"), Format(Val(getarrears.Rows(t)("PenaltyAfterDue")), "standard"), Format(Val(arrearscharge), "standard"), Format((Val(getarrears.Rows(t)("AmountDue") + Val(getarrears.Rows(t)("PenaltyAfterDue")) + Val(getarrears.Rows(t)("Adjustment"))) - (Val(getarrears.Rows(t)("Discount")) + Val(getarrears.Rows(t)("AdvancePayment")))) + arrearscharge, "standard"), "Billing")

        Next

        Dim getfb As New DataTable
        getfb.Rows.Clear()
        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        stracs = "select SUM(Billing) as Billing, SUM(Penalty) as Penalty, Particulars, RefNo, BillingDate from AddAdjustment where AccountNumber = '" & adjustAccountNo.Text & "' 
                and Status = 'Posted' and Paid = 'No' and IsCollectionCreated = 'No' group by Particulars, RefNo, BillingDate"
        acscmd.Connection = acsconn
        acscmd.CommandText = stracs
        acsda.SelectCommand = acscmd
        acsda.Fill(getfb)

        If getfb.Rows.Count = 0 Then

        Else

            For o = 0 To getfb.Rows.Count - 1

                Dim loadchargesarrears As New DataTable
                loadchargesarrears.Clear()
                'stracs = "select a.Billno as Billno, a.BillingDate as BillingDate, (SUM(b.amount) + SUM(a.AmountDue) + SUM(a.PenaltyAfterDue)) - SUM(a.Discount) as amount from Bills a join BillCharges b on a.BillNo = b.BillNumber where a.IsPaid = 'No' and b.IsPaid = 'No' and a.BillNo = " & getarrears.Rows(t)("BillNo") & " and b.BillNumber = " & getarrears.Rows(t)("BillNo") & " group by a.BillNo, a.BillingDate"
                stracs = "select SUM(Amount) as Amount from BillCharges where IsPaid = 'No' and Cancelled = 'No' and BillNumber in (select BillNo from Bills where PromisorryNo = " & getfb.Rows(0)("Refno") & ")"
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

                adjustBills.Rows.Add(-1, getfb.Rows(o)("RefNo"), getfb.Rows(o)("BillingDate"), Format(Val(getfb.Rows(o)("Billing")), "standard"), Format(Val(getfb.Rows(o)("Penalty")), "standard"), Format(Val(arrearscharge), "Standard"), Format(Val(getfb.Rows(o)("Billing")) + Val(getfb.Rows(o)("Penalty") + Val(arrearscharge)), "Standard"), "PNFB")

            Next

        End If



        AdjustRemarks.ReadOnly = False

        If adjustBills.Rows.Count = 0 Then

            adjustsaving.Visible = False

            lblMode.Text = "Mode"
            lblMode.Hide()

        Else

            lblMode.Text = "Create New"
            lblMode.Show()

            adjustsaving.Visible = True

        End If

        Dim totallahat As Double

        For p = 0 To adjustBills.Rows.Count - 1

            If adjustBills.Rows(p).Cells(0).Value = -1 Then

                totallahat = totallahat + Double.Parse(adjustBills.Rows(p).Cells(6).Value)

            Else

            End If

        Next

        totalamount.Text = Format(totallahat, "standard")

    End Sub

    Private Sub adjustsaving_Click(sender As Object, e As EventArgs) Handles adjustsaving.Click

        If lblMode.Text = "Create New" Then

            If AdjustRemarks.Text = "" Or adjustBills.Rows.Count = 0 Then

                MsgBox("Remarks or amount balance list empty.")

            Else

                Dim getbilladjustmentno As New DataTable
                Try
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                Catch ex As Exception
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                End Try
                stracs = "select number from tbllogicnumbers where id = 14"
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsda.SelectCommand = acscmd
                acsda.Fill(getbilladjustmentno)

                adjustRefNo.Text = getbilladjustmentno.Rows(0)("number")

                Dim charges As Double = 0
                Dim penalty As Double = 0
                Dim billing As Double = 0
                Dim totalahat As Double = Double.Parse(totalamount.Text)

                For m = 0 To adjustBills.Rows.Count - 1

                    If adjustBills.Rows(m).Cells(7).Value = "Billing" Then

                        stracs = "update Bills set BillStatus = 'Writeoff', Writeoffref = " & adjustRefNo.Text & " where BillNo = " & adjustBills.Rows(m).Cells(1).Value
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        stracs = "update BillCharges set Status = 'Writeoff', Writeoffref = " & adjustRefNo.Text & " where BillNumber = " & adjustBills.Rows(m).Cells(1).Value
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                    Else

                        stracs = "update AddAdjustment set Status = 'Writeoff', Writeoffref = " & adjustRefNo.Text & " where Paid = 'No' and IsCollectionCreated = 'No' and RefNo = " & adjustBills.Rows(m).Cells(1).Value
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        stracs = "update BillCharges set Status = 'Writeoff', Writeoffref = " & adjustRefNo.Text & " where IsPaid = 'No' and IsCollectionCreated = 'No' and PromisorryNo = " & adjustBills.Rows(m).Cells(1).Value
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                    End If

                    billing = billing + adjustBills.Rows(m).Cells(3).Value
                    penalty = penalty + adjustBills.Rows(m).Cells(4).Value
                    charges = charges + adjustBills.Rows(m).Cells(5).Value

                Next



                stracs = " insert into Writeoff (RefNo,AccountNumber,AccountName,Remarks,DateCreated,Billing,Penalty,Charges,CreatedBy) values (" _
                        & adjustRefNo.Text & ", '" & adjustAccountNo.Text & "', '" & AdjustName.Text.Replace("'", "''") & "', '" _
                        & AdjustRemarks.Text.Replace("'", "''") & "', '" & Format(AdjustDate.Value, "yyyy-MM-dd") & "', " _
                        & Double.Parse(billing) & ", " & Double.Parse(penalty) & ", " & Double.Parse(charges) & ", '" & My.Settings.Nickname.ToString.Replace("'", "''") & "')"

                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                If billing > 0 Then

                    stracs = " insert into AccountLedger (ledgerAccountNo,ledgerDate,ledgerRefNo,ledgerParticulars,ledgerReading
                            ,ledgerConsumption,ledgerAmount,ledgerDiscount,ledgerBalance,ledgerCancelled) values ('" _
                        & adjustAccountNo.Text & "', '" & Format(AdjustDate.Value, "yyyy-MM-dd") & "', " & adjustRefNo.Text & ", 'Write-off (Billing)', " _
                        & "'','','','" & Format(billing, "standard") & "', '" & Format(Val(totalahat) - Val(billing), "Standard") & "', 'No')"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                    totalahat = totalahat - billing

                Else

                End If

                If penalty > 0 Then

                    stracs = " insert into AccountLedger (ledgerAccountNo,ledgerDate,ledgerRefNo,ledgerParticulars,ledgerReading
                            ,ledgerConsumption,ledgerAmount,ledgerDiscount,ledgerBalance,ledgerCancelled) values ('" _
                        & adjustAccountNo.Text & "', '" & Format(AdjustDate.Value, "yyyy-MM-dd") & "', " & adjustRefNo.Text & ", 'Adjustment (Penalty)', " _
                        & "'','','','" & Format(penalty, "standard") & "', '" & Format(Val(totalahat) - Val(penalty), "Standard") & "', 'No')"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                    totalahat = totalahat - penalty

                Else

                End If

                If charges > 0 Then

                    stracs = " insert into AccountLedger (ledgerAccountNo,ledgerDate,ledgerRefNo,ledgerParticulars,ledgerReading
                            ,ledgerConsumption,ledgerAmount,ledgerDiscount,ledgerBalance,ledgerCancelled) values ('" _
                        & adjustAccountNo.Text & "', '" & Format(AdjustDate.Value, "yyyy-MM-dd") & "', " & adjustRefNo.Text & ", 'Adjustment (Charges)', " _
                        & "'','','','" & Format(charges, "standard") & "', '" & Format(Val(totalahat) - Val(charges), "Standard") & "', 'No')"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                    totalahat = totalahat - charges

                Else

                End If



                stracs = "update tbllogicnumbers set number = number + 1 where id = 14"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                clearall()
                adjustAccountNo_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))

            End If

        End If

    End Sub

    Private Sub adjustAccountNo_TextChanged(sender As Object, e As EventArgs) Handles adjustAccountNo.TextChanged

        clearall()

    End Sub

    Private Sub AdjustRecords_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles AdjustRecords.CellClick

        If AdjustRecords.Columns(e.ColumnIndex).HeaderText = "Ref No." Then


            lblMode.Visible = False
            lblMode.Text = "Mode"

            adjustRefNo.Text = AdjustRecords.Rows(AdjustRecords.CurrentCellAddress.Y).Cells(0).Value

            adjustRefNo_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))

        End If

    End Sub

    Private Sub adjustRefNo_KeyDown(sender As Object, e As KeyEventArgs) Handles adjustRefNo.KeyDown

        If e.KeyCode = Keys.Enter Then

            Dim searchdata As New DataTable
            searchdata.Clear()

            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try

            stracs = "select * from Writeoff where RefNo = " & adjustRefNo.Text
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(searchdata)

            If searchdata.Rows.Count = 0 Then
            Else

                adjustAccountNo.Text = searchdata.Rows(0)("AccountNumber")
                AdjustName.Text = searchdata.Rows(0)("AccountName")
                AdjustRemarks.Text = searchdata.Rows(0)("Remarks")
                AdjustDate.Value = searchdata.Rows(0)("DateCreated")
                adjustRefNo.Text = searchdata.Rows(0)("RefNo")

                Dim getarrears As New DataTable

                stracs = "select * from Bills where AccountNumber = '" & adjustAccountNo.Text & "' and BillStatus = 'Writeoff' and Writeoffref = " & adjustRefNo.Text & " order by BillNo"
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsda.SelectCommand = acscmd
                acsda.Fill(getarrears)

                adjustBills.Rows.Clear()
                For t = 0 To getarrears.Rows.Count - 1


                    Dim loadchargesarrears As New DataTable
                    loadchargesarrears.Clear()
                    'stracs = "select a.Billno as Billno, a.BillingDate as BillingDate, (SUM(b.amount) + SUM(a.AmountDue) + SUM(a.PenaltyAfterDue)) - SUM(a.Discount) as amount from Bills a join BillCharges b on a.BillNo = b.BillNumber where a.IsPaid = 'No' and b.IsPaid = 'No' and a.BillNo = " & getarrears.Rows(t)("BillNo") & " and b.BillNumber = " & getarrears.Rows(t)("BillNo") & " group by a.BillNo, a.BillingDate"
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

                    'adjustBills.Rows.Add(0, loadbillarrears.Rows(0)("Billno"), loadbillarrears.Rows(0)("BillingDate"), Format((Val(loadbillarrears.Rows(0)("AmountDue"))) - (Val(loadbillarrears.Rows(0)("Discount")) + Val(loadbillarrears.Rows(0)("AdvancePayment"))), "standard"), Format(Val(loadbillarrears.Rows(0)("PenaltyAfterDue")), "standard"), Format(Val(arrearscharge), "standard"), Format(Val((loadbillarrears.Rows(0)("AmountDue")) + Val(arrearscharge) + Val(loadbillarrears.Rows(0)("PenaltyAfterDue"))) - (Val(loadbillarrears.Rows(0)("Discount")) + Val(loadbillarrears.Rows(0)("AdvancePayment"))), "standard"))
                    'adjustBills.Rows.Add(0, getarrears.Rows(t)("Billno"), getarrears.Rows(t)("BillingDate"), Format(Val(getarrears.Rows(t)("AmountDue")) - (Val(getarrears.Rows(t)("Discount")) + Val(getarrears.Rows(t)("AdvancePayment"))), "standard"), Format(Val(getarrears.Rows(t)("PenaltyAfterDue")), "standard"), Format(Val(arrearscharge), "standard"), Format((Val(getarrears.Rows(t)("AmountDue") + Val(getarrears.Rows(t)("PenaltyAfterDue"))) - (Val(getarrears.Rows(t)("Discount")) + Val(getarrears.Rows(t)("AdvancePayment")))) + arrearscharge, "standard"), Date.Parse(getarrears.Rows(t)("ReadingDate")).ToString("yyyy-MM-dd"))
                    adjustBills.Rows.Add(-1, getarrears.Rows(t)("Billno"), getarrears.Rows(t)("BillingDate"), Format((Val(getarrears.Rows(t)("AmountDue")) + Val(getarrears.Rows(t)("Adjustment"))) - (Val(getarrears.Rows(t)("Discount")) + Val(getarrears.Rows(t)("AdvancePayment"))), "standard"), Format(Val(getarrears.Rows(t)("PenaltyAfterDue")), "standard"), Format(Val(arrearscharge), "standard"), Format((Val(getarrears.Rows(t)("AmountDue") + Val(getarrears.Rows(t)("PenaltyAfterDue")) + Val(getarrears.Rows(t)("Adjustment"))) - (Val(getarrears.Rows(t)("Discount")) + Val(getarrears.Rows(t)("AdvancePayment")))) + arrearscharge, "standard"), "Billing")

                Next

                Dim getfb As New DataTable
                getfb.Rows.Clear()
                Try
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                Catch ex As Exception
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                End Try

                stracs = "select SUM(Billing) as Billing, SUM(Penalty) as Penalty, Particulars, RefNo, BillingDate from AddAdjustment where AccountNumber = '" & adjustAccountNo.Text & "' 
                and Status = 'Writeoff' and  Writeoffref = " & adjustRefNo.Text & " group by Particulars, RefNo, BillingDate"
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsda.SelectCommand = acscmd
                acsda.Fill(getfb)

                If getfb.Rows.Count = 0 Then

                Else

                    For o = 0 To getfb.Rows.Count - 1

                        Dim loadchargesarrears As New DataTable
                        loadchargesarrears.Clear()
                        'stracs = "select a.Billno as Billno, a.BillingDate as BillingDate, (SUM(b.amount) + SUM(a.AmountDue) + SUM(a.PenaltyAfterDue)) - SUM(a.Discount) as amount from Bills a join BillCharges b on a.BillNo = b.BillNumber where a.IsPaid = 'No' and b.IsPaid = 'No' and a.BillNo = " & getarrears.Rows(t)("BillNo") & " and b.BillNumber = " & getarrears.Rows(t)("BillNo") & " group by a.BillNo, a.BillingDate"
                        stracs = "select SUM(Amount) as Amount from BillCharges where Status = 'Writeoff' and isPromisorry = 'YesPosted' and Writeoffref = " & adjustRefNo.Text
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

                        adjustBills.Rows.Add(-1, getfb.Rows(o)("RefNo"), getfb.Rows(o)("BillingDate"), Format(Val(getfb.Rows(o)("Billing")), "standard"), Format(Val(getfb.Rows(o)("Penalty")), "standard"), Format(Val(arrearscharge), "Standard"), Format(Val(getfb.Rows(o)("Billing")) + Val(getfb.Rows(o)("Penalty") + Val(arrearscharge)), "Standard"), "PNFB")

                    Next

                End If

                Dim totallahat As Double

                For p = 0 To adjustBills.Rows.Count - 1

                    If adjustBills.Rows(p).Cells(0).Value = -1 Then

                        totallahat = totallahat + Double.Parse(adjustBills.Rows(p).Cells(6).Value)

                    Else

                    End If

                Next

                totalamount.Text = Format(totallahat, "standard")

            End If

        End If

    End Sub

    Private Sub ControlsClick(sender As Object, e As EventArgs) Handles _
        Panel1.Click, adjustAccountNo.Click, AdjustName.Click, accSearch.Click, AdjustRecords.Click, GroupBox3.Click, adjustRefNo.Click,
        AdjustDate.Click, AdjustRemarks.Click, GroupBox1.Click, adjustBills.Click, Label8.Click, totalamount.Click,
        adjustsaving.Click, Label2.Click  ' etc.
        Me.Activate() 'Or Whatever
    End Sub

    Public MoveFormWriteoff As Boolean
    Public MoveWriteoff_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormWriteoff = True
            Me.Cursor = Cursors.NoMove2D
            MoveWriteoff_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMoveWriteoff(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormWriteoff Then
            Me.Location = Me.Location + (e.Location - MoveWriteoff_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUpWriteoff(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormWriteoff = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub writeoff_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.BackColor = Color.SteelBlue
    End Sub

    Private Sub writeoff_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.BackColor = Color.FromArgb(17, 153, 195)
    End Sub

    Private Sub writeoff_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub
End Class