Public Class TempCR

    Private crnumber As Integer
    Dim maxbill As New DataTable
    Dim searchbill As New DataTable
    Dim reprintcr As String = "Option"

    Public Sub clearallfields()
        billAccountNo.Clear()
        billAddress.Clear()
        billadvancepayment.Text = "0.00"
        billamountdue.Text = "0.00"
        billarrears.Rows.Clear()
        billcharges.Rows.Clear()
        billdiscount.Text = "0.00"
        billName.Clear()
        billZone.Clear()
        CRBillno.Clear()
        billPenalty.Text = "0.00"
        billTotalamountdue.Text = "0.00"
        billbillingmonth.Clear()
        discdate.Clear()

        dgvotherchages.Rows.Clear()
        dgvothers.Rows.Clear()

        billsave.Enabled = False
        billsave.Hide()
        lblaccstatus.Text = "Mode"
        lblaccstatus.Visible = False

    End Sub

    Sub loadcurrentbill()


        Dim billdata As New DataTable
        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        If IsNumeric(CRBillno.Text) = True Then

            billdata.Clear()
            stracs = "select * from Bills where BillNo = " & CRBillno.Text & " AND BillStatus = 'Posted' AND NOT IsCollectionCreated = 'Yes' AND NOT isPromisorry = 'YesPosted'"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(billdata)

            If billdata.Rows.Count = 0 Then
                billcharges.Rows.Clear()
                CRBillno.Text = ""
                billamountdue.Text = "0.00"
                billPenalty.Text = "0.00"
                billdiscount.Text = "0.00"
                billadvancepayment.Text = "0.00"
            Else


                discdate.Text = Format(Date.Parse(billdata(0)("DiscDate")), "short date")
                billamountdue.Text = Format(billdata(0)("AmountDue"), "standard")
                billdiscount.Text = Format(billdata(0)("Discount"), "standard")
                billPenalty.Text = Format(billdata(0)("PenaltyAfterDue"), "standard")
                billadvancepayment.Text = Format(billdata(0)("AdvancePayment"), "standard")
                billbillingmonth.Text = billdata(0)("BillingDate")


                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                sqldataBilling.Clear()

                stracs = "select * from BillCharges where BillNumber = " & CRBillno.Text
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(sqldataBilling)

                billcharges.Rows.Clear()

                If sqldataBilling.Rows.Count = 0 Then

                Else

                    For i = 0 To sqldataBilling.Rows.Count - 1


                        If sqldataBilling(i)("Category") = "Charges" Then

                            dgTemp.Rows.Add(1, billAccountNo.Text, sqldataBilling(i)("Particulars"), Format(sqldataBilling(i)("Amount"), "standard"), sqldataBilling.Rows(i)("BillChargesID"), sqldataBilling.Rows(i)("Category"), sqldataBilling.Rows(i)("Entry"), "Yes")
                            computeall()

                        Else
                            billcharges.Rows.Add(sqldataBilling(i)("Particulars"), Format(sqldataBilling(i)("Amount"), "standard"), sqldataBilling(i)("BillChargesID"))

                        End If

                    Next

                End If

                If maxbill.Rows.Count = 1 And searchbill(0)("CustomerStatus") <> "Disconnected" Then

                    If Format(Date.Parse(billdata(0)("DiscDate")), "yyyy-MM-dd") < Format(Date.Parse(Now), "yyyy-MM-dd") Then

                        Select Case MsgBox("Reopening fee should be added to this bill. Add to OR payment?", MsgBoxStyle.YesNo)
                            Case MsgBoxResult.Yes

                                Dim reconn As New DataTable
                                reconn.Clear()

                                stracs = "select * from reconnectioncharge where ID = 3"
                                acscmd.CommandText = stracs
                                acscmd.Connection = acsconn
                                acsda.SelectCommand = acscmd
                                acsda.Fill(reconn)

                                dgTemp.Rows.Add(1, billAccountNo.Text, reconn.Rows(0)("Particular"), reconn.Rows(0)("Amount"), sqldataBilling.Rows(0)("BillChargesID"), sqldataBilling.Rows(0)("Category"), sqldataBilling.Rows(0)("Entry"))
                                computeall()
                                billAccountNo.Select()
                            Case MsgBoxResult.No
                        End Select


                    End If

                End If

                If maxbill.Rows.Count > 1 And searchbill(0)("CustomerStatus") <> "Disconnected" Then

                    Select Case MsgBox("Reopening fee should be added to this bill. Add to OR payment?", MsgBoxStyle.YesNo)
                        Case MsgBoxResult.Yes


                            Dim reconn As New DataTable
                            reconn.Clear()

                            stracs = "select * from reconnectioncharge where ID = 3"
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acsda.SelectCommand = acscmd
                            acsda.Fill(reconn)

                            dgTemp.Rows.Add(1, billAccountNo.Text, reconn.Rows(0)("Particular"), reconn.Rows(0)("Amount"), sqldataBilling.Rows(0)("BillChargesID"), sqldataBilling.Rows(0)("Category"), sqldataBilling.Rows(0)("Entry"))
                            computeall()
                            'End If

                            billAccountNo.Select()

                        Case MsgBoxResult.No
                    End Select

                End If




            End If

        End If

    End Sub

    Sub loadarrears()

        Dim tempbillno As Integer
        If CRBillno.Text = "" Or IsDBNull(CRBillno.Text) = True Then
            tempbillno = 0
        Else
            tempbillno = CRBillno.Text
        End If

        Dim getarrears As New DataTable
        getarrears.Rows.Clear()
        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try
        stracs = "select BillNo from Bills where AccountNumber = '" & billAccountNo.Text & "' and IsPaid = 'No' and Cancelled = 'No' AND NOT IsCollectionCreated = 'Yes' AND NOT isPromisorry = 'YesPosted' and BillNo < " & tempbillno
        acscmd.Connection = acsconn
        acscmd.CommandText = stracs
        acsda.SelectCommand = acscmd
        acsda.Fill(getarrears)

        If getarrears.Rows.Count = 0 Then
            billarrears.Rows.Clear()
        Else
            billarrears.Rows.Clear()
            For t = 0 To getarrears.Rows.Count - 1
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                Dim loadbillarrears As New DataTable
                loadbillarrears.Clear()
                'stracs = "select a.Billno as Billno, a.BillingDate as BillingDate, (SUM(b.amount) + SUM(a.AmountDue) + SUM(a.PenaltyAfterDue)) - SUM(a.Discount) as amount from Bills a join BillCharges b on a.BillNo = b.BillNumber where a.IsPaid = 'No' and b.IsPaid = 'No' and a.BillNo = " & getarrears.Rows(t)("BillNo") & " and b.BillNumber = " & getarrears.Rows(t)("BillNo") & " group by a.BillNo, a.BillingDate"
                stracs = "select * from Bills where IsPaid = 'No' AND NOT IsCollectionCreated = 'Yes' AND NOT isPromisorry = 'YesPosted' and BillNo = " & getarrears.Rows(t)("BillNo")
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsda.SelectCommand = acscmd
                acsda.Fill(loadbillarrears)

                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                Dim loadchargesarrears As New DataTable
                loadchargesarrears.Clear()
                'stracs = "select a.Billno as Billno, a.BillingDate as BillingDate, (SUM(b.amount) + SUM(a.AmountDue) + SUM(a.PenaltyAfterDue)) - SUM(a.Discount) as amount from Bills a join BillCharges b on a.BillNo = b.BillNumber where a.IsPaid = 'No' and b.IsPaid = 'No' and a.BillNo = " & getarrears.Rows(t)("BillNo") & " and b.BillNumber = " & getarrears.Rows(t)("BillNo") & " group by a.BillNo, a.BillingDate"
                stracs = "select SUM(Amount) as Amount, Particulars from BillCharges where IsPaid = 'No' and Cancelled = 'No' AND NOT IsCollectionCreated = 'Yes' AND Category = 'Others' and BillNumber = " & getarrears.Rows(t)("BillNo") & " group by Particulars, BillChargesID order by BillChargesID desc"
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsda.SelectCommand = acscmd
                acsda.Fill(loadchargesarrears)
                'AmountDue

                Dim arrearscharge As Double
                Dim chargepart As String

                If loadchargesarrears.Rows.Count = 0 Then
                    chargepart = "None"
                    arrearscharge = 0
                Else

                    If IsDBNull(loadchargesarrears.Rows(0)("amount")) = True Then
                        chargepart = "None"
                        arrearscharge = 0
                    Else
                        arrearscharge = loadchargesarrears.Rows(0)("amount")
                        chargepart = loadchargesarrears.Rows(0)("Particulars")
                    End If

                End If



                billarrears.Rows.Add(True, loadbillarrears.Rows(0)("Billno"), loadbillarrears.Rows(0)("BillingDate"), Format(Val((loadbillarrears.Rows(0)("AmountDue")) + Val(arrearscharge) + Val(loadbillarrears.Rows(0)("PenaltyAfterDue"))) - (Val(loadbillarrears.Rows(0)("Discount")) + Val(loadbillarrears.Rows(0)("AdvancePayment"))), "standard"),
                                     Format(Val(loadbillarrears.Rows(0)("AmountDue") + Val(loadbillarrears.Rows(0)("PenaltyAfterDue"))) - (Val(loadbillarrears.Rows(0)("Discount")) + Val(loadbillarrears.Rows(0)("AdvancePayment"))), "standard"), chargepart, Val(arrearscharge))

                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                sqlData1.Clear()
                stracs = "select * from BillCharges where IsPaid = 'No' and Cancelled = 'No' AND NOT IsCollectionCreated = 'Yes' AND Category = 'Charges' and BillNumber = " & getarrears.Rows(t)("BillNo")
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsda.SelectCommand = acscmd
                acsda.Fill(sqlData1)

                If sqlData1.Rows.Count = 0 Then

                Else

                    For y = 0 To sqlData1.Rows.Count - 1

                        dgTemp.Rows.Add(1, billAccountNo.Text, sqlData1.Rows(y)("Particulars"), Format(sqlData1.Rows(y)("Amount"), "standard"), sqldataBilling.Rows(y)("BillChargesID"), sqldataBilling.Rows(y)("Category"), sqldataBilling.Rows(y)("Entry"), "Yes")
                        computeall()
                    Next

                End If


            Next
        End If

    End Sub

    Sub loadothers()

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        Dim otherbills As New DataTable
        otherbills.Clear()
        stracs = "select * from AddAdjustment where AccountNumber = '" & billAccountNo.Text & "' AND Status = 'Posted' AND IsCollectionCreated = 'No'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(otherbills)

        If otherbills.Rows.Count = 0 Then
            dgvothers.Rows.Clear()

        Else
            dgvothers.Rows.Clear()

            For k = 0 To otherbills.Rows.Count - 1

                dgvothers.Rows.Add(True, otherbills.Rows(k)("RefNo"), otherbills.Rows(k)("Particulars"), FormatNumber(Val(otherbills.Rows(k)("Billing")) + Val(otherbills.Rows(k)("Penalty"))), otherbills.Rows(k)("ID"))
                computeall()
            Next

        End If

        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Dim otherbillcharges As New DataTable
        stracs = "select * from BillCharges where AccountNumber = '" & billAccountNo.Text & "' AND Status = 'Posted' AND IsCollectionCreated = 'No' AND isPromisorry = 'YesPosted'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(otherbillcharges)

        If otherbillcharges.Rows.Count = 0 Then

            dgvotherchages.Rows.Clear()
        Else
            dgvotherchages.Rows.Clear()

            For k = 0 To otherbillcharges.Rows.Count - 1

                If otherbillcharges(k)("Category") = "Charges" Then

                    dgTemp.Rows.Add(1, billAccountNo.Text, otherbillcharges.Rows(k)("Particulars"), Format(otherbillcharges.Rows(k)("Amount"), "standard"), sqldataBilling.Rows(k)("BillChargesID"), sqldataBilling.Rows(k)("Category"), sqldataBilling.Rows(k)("Entry"))
                    computeall()
                Else
                    dgvotherchages.Rows.Add(otherbillcharges.Rows(k)("Particulars"), otherbillcharges.Rows(k)("Amount"), otherbillcharges.Rows(k)("BillChargesID"))
                End If

                ' dgvothers.Rows.Add(True, otherbills.Rows(k)("RefNo"), otherbills.Rows(k)("Particulars"), FormatNumber(Val(otherbills.Rows(k)("Billing"))), otherbills.Rows(k)("ID"))
            Next
            Create_OR.compute()
        End If

        computetotalamount()
    End Sub


    Public Sub billAccountNo_KeyDown(sender As Object, e As KeyEventArgs) Handles billAccountNo.KeyDown

        If e.KeyCode = Keys.Enter Then

            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try

            searchbill.Clear()
            stracs = "select * from Customers where AccountNo = '" & billAccountNo.Text & "'"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(searchbill)

            If searchbill.Rows.Count = 0 Then
                MsgBox("No record found.")
                clearallfields()
                billAccountNo.SelectAll()
                currentBilling.CheckState = CheckState.Unchecked
            Else

                If searchbill.Rows(0)("CompanyName") = "" Or IsDBNull(searchbill.Rows(0)("CompanyName")) = True Then
                    billName.Text = searchbill.Rows(0)("Firstname") & " " & searchbill.Rows(0)("Middlename") & " " & searchbill.Rows(0)("Lastname")
                Else
                    billName.Text = searchbill.Rows(0)("CompanyName")
                End If


                billAddress.Text = searchbill.Rows(0)("ServiceAddress")
                billZone.Text = searchbill.Rows(0)("Zone")
                lblaccstatus.Text = searchbill.Rows(0)("CustomerStatus")

                lblaccstatus.Show()

                If lblaccstatus.Text = "Active" Then
                    lblaccstatus.ForeColor = Color.Green
                End If

                If lblaccstatus.Text = "Disconnected" Then
                    lblaccstatus.ForeColor = Color.Red
                End If

                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                maxbill.Clear()

                stracs = "select BillNo from Bills where AccountNumber = '" & billAccountNo.Text & "' AND IsPaid = 'No' AND BillStatus = 'Posted' AND NOT IsCollectionCreated = 'Yes' AND isPromisorry <> 'YesPosted' order by BillNo desc"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(maxbill)


                If maxbill.Rows.Count = 0 Then

                    billcharges.Rows.Clear()
                    CRBillno.Text = ""
                    billamountdue.Text = "0.00"
                    billPenalty.Text = "0.00"
                    billdiscount.Text = "0.00"
                    billadvancepayment.Text = "0.00"

                    currentBilling.Enabled = False
                    loadothers()

                    If dgvothers.Rows.Count = 0 And dgvotherchages.Rows.Count = 0 Then

                        Dim getlastpayment As New DataTable
                        stracs = "select PaymentDate, TotalAmountDue from Collection_Details where CRNo = (select max(CRNo) as maxcrno from Collection_Details where AccountNo = '" & billAccountNo.Text & "')"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acsda.SelectCommand = acscmd
                        acsda.Fill(getlastpayment)

                        If getlastpayment.Rows.Count = 0 Then

                            MsgBox("All water bills are paid.")
                            clearallfields()
                            billAccountNo.Select()

                        Else

                            MsgBox("All water bills are paid." & vbCrLf & vbCrLf & "Last Payment Details:" & vbCrLf & vbCrLf _
                               & "Amount: " & getlastpayment(0)("TotalAmountDue") _
                               & vbCrLf & "Date: " & getlastpayment(0)("PaymentDate"))

                            'MsgBox("All water bills are paid.")
                            clearallfields()
                            billAccountNo.Select()

                        End If

                    Else

                        If searchbill.Rows(0)("CustomerStatus") = "Disconnected" Then

                            Dim asd As DialogResult = MessageBox.Show("Reconnection Fee should be added to this bill. Add to OR payment?", "Electronic Billing and Collection", MessageBoxButtons.YesNo)

                            If asd = DialogResult.Yes Then

                                Create_OR.paymentfor.Text = "Reconnection Fee"

                                Dim reconn As New DataTable
                                reconn.Clear()

                                If Date.Parse(searchbill.Rows(0)("DateLastDisconnected")).AddYears(1) < Date.Parse(Now) Then

                                    stracs = "select * from reconnectioncharge where ID = 1"
                                    acscmd.CommandText = stracs
                                    acscmd.Connection = acsconn
                                    acsda.SelectCommand = acscmd
                                    acsda.Fill(reconn)

                                    'Create_OR.dgvitems.Rows.Add("1", "Reopening Fee", "150", "150", "14", "Charges", "Others")
                                    dgTemp.Rows.Add(1, billAccountNo.Text, reconn.Rows(0)("Particular"), reconn.Rows(0)("Amount"), sqldataBilling.Rows(0)("BillChargesID"), sqldataBilling.Rows(0)("Category"), sqldataBilling.Rows(0)("Entry"))
                                    computeall()
                                Else

                                    stracs = "select * from reconnectioncharge where ID = 2"
                                    acscmd.CommandText = stracs
                                    acscmd.Connection = acsconn
                                    acsda.SelectCommand = acscmd
                                    acsda.Fill(reconn)

                                    'Create_OR.dgvitems.Rows.Add("1", "Reopening Fee", "150", "150", "14", "Charges", "Others")
                                    dgTemp.Rows.Add(1, billAccountNo.Text, reconn.Rows(0)("Particular"), reconn.Rows(0)("Amount"), sqldataBilling.Rows(0)("BillChargesID"), sqldataBilling.Rows(0)("Category"), sqldataBilling.Rows(0)("Entry"))
                                    computeall()
                                End If


                            Else

                            End If

                            'Select Case MsgBox("Reconnection Fee should be added to this bill. Add to OR payment?", MsgBoxStyle.YesNo)
                            '    Case MsgBoxResult.Yes



                            '    Case MsgBoxResult.No
                            'End Select

                        Else

                            loadcurrentbill()
                            loadarrears()
                            loadothers()

                        End If



                    End If

                Else
                    CRBillno.Text = maxbill.Rows(0)("BillNo")
                    currentBilling.Enabled = True

                    If searchbill.Rows(0)("CustomerStatus") = "Disconnected" Then

                        loadcurrentbill()

                        Dim asd As DialogResult = MessageBox.Show("Reconnection Fee should be added to this bill. Add to OR payment?", "Electronic Billing and Collection", MessageBoxButtons.YesNo)


                        If asd = DialogResult.Yes Then


                            Dim reconn As New DataTable
                            reconn.Clear()



                            If Date.Parse(searchbill.Rows(0)("DateLastDisconnected")).AddYears(1) < Date.Parse(Now) Then

                                stracs = "select * from reconnectioncharge where ID = 1"
                                acscmd.CommandText = stracs
                                acscmd.Connection = acsconn
                                acsda.SelectCommand = acscmd
                                acsda.Fill(reconn)

                                'Create_OR.dgvitems.Rows.Add("1", "Reopening Fee", "150", "150", "14", "Charges", "Others")
                                dgTemp.Rows.Add(1, billAccountNo.Text, reconn.Rows(0)("Particular"), reconn.Rows(0)("Amount"), sqldataBilling.Rows(0)("BillChargesID"), sqldataBilling.Rows(0)("Category"), sqldataBilling.Rows(0)("Entry"))
                                computeall()
                            Else

                                stracs = "select * from reconnectioncharge where ID = 2"
                                acscmd.CommandText = stracs
                                acscmd.Connection = acsconn
                                acsda.SelectCommand = acscmd
                                acsda.Fill(reconn)

                                'Create_OR.dgvitems.Rows.Add("1", "Reopening Fee", "150", "150", "14", "Charges", "Others")
                                dgTemp.Rows.Add(1, billAccountNo.Text, reconn.Rows(0)("Particular"), reconn.Rows(0)("Amount"), sqldataBilling.Rows(0)("BillChargesID"), sqldataBilling.Rows(0)("Category"), sqldataBilling.Rows(0)("Entry"))
                                computeall()
                            End If



                            loadarrears()
                            loadothers()

                        Else
                            loadarrears()
                            loadothers()
                        End If

                        'Select Case MsgBox("Reconnection Fee should be added to this bill. Add to OR payment?", MsgBoxStyle.YesNo)
                        '    Case MsgBoxResult.Yes



                        '    Case MsgBoxResult.No
                        'End Select

                    Else

                        loadcurrentbill()
                        loadarrears()
                        loadothers()

                    End If



                    billTotalamountdue.Select()
                    billTotalamountdue.SelectAll()

                    'If billTotalamountdue.Text > 0 Then
                    '    billsave.Visible = True
                    'Else
                    '    billsave.Visible = False
                    'End If

                End If

                'billName.Text = searchbill.Rows(0)("Firstname") & " " & searchbill.Rows(0)("Middlename") & " " & searchbill.Rows(0)("Lastname")
                'billAddress.Text = searchbill.Rows(0)("ServiceAddress")
                'billZone.Text = searchbill.Rows(0)("Zone")
                'lblaccstatus.Text = searchbill.Rows(0)("CustomerStatus")

                If billamountdue.Text = "0.00" Then
                    currentBilling.CheckState = CheckState.Unchecked
                Else

                    currentBilling.CheckState = CheckState.Checked

                End If

            End If

            'If IsDBNull(maxbill(0)("lastbill")) = True Then


            '    Create_OR.clearallfields()
            '    Create_OR.loadornumber()
            '    Create_OR.AccountNo.Text = billAccountNo.Text
            '    Create_OR.loadinfo()
            '    Create_OR.logic = "From CR"

            '    billcharges.Rows.Clear()
            '    CRBillno.Text = ""
            '    billamountdue.Text = "0.00"
            '    billPenalty.Text = "0.00"
            '    billdiscount.Text = "0.00"
            '    billadvancepayment.Text = "0.00"

            '    billarrears.Rows.Clear()

            '    ' loadarrears()
            '    'loadothers()

            'Else

            '    Create_OR.clearallfields()
            '    Create_OR.loadornumber()
            '    Create_OR.AccountNo.Text = billAccountNo.Text
            '    Create_OR.loadinfo()
            '    Create_OR.logic = "From CR"

            '    CRBillno.Text = maxbill.Rows(0)("lastbill")
            '    loadcurrentbill()
            '    loadarrears()
            '    'loadothers()

            'End If

        End If

    End Sub

    Sub computetotalamount()

        'Dim x As Integer
        Dim totalamountdue As Double = 0
        Dim totalcharges As Double = 0
        Dim totalarrear As Double = 0
        Dim totalothers As Double = 0
        Dim totalotherchargee As Double = 0

        If billcharges.Rows.Count = 0 Then
        Else
            For x = 0 To billcharges.Rows.Count - 1

                totalcharges = totalcharges + Double.Parse(billcharges.Rows(x).Cells(1).Value)

            Next
        End If

        If billarrears.Rows.Count = 0 Then
        Else
            For y = 0 To billarrears.Rows.Count - 1

                If billarrears.Rows(y).Cells(0).Value = True Then
                    totalcharges = totalcharges + Double.Parse(billarrears.Rows(y).Cells(3).Value)

                End If

            Next
        End If


        If dgvothers.Rows.Count = 0 Then
        Else
            For y = 0 To dgvothers.Rows.Count - 1

                If dgvothers.Rows(y).Cells(0).Value = True Then
                    totalothers = totalothers + Double.Parse(dgvothers.Rows(y).Cells(3).Value)

                End If

            Next
        End If

        If dgvotherchages.Rows.Count = 0 Then

        Else
            For b = 0 To dgvotherchages.Rows.Count - 1
                totalotherchargee = totalotherchargee + Double.Parse(dgvotherchages.Rows(b).Cells(1).Value)
            Next
        End If

        totalamountdue = Double.Parse(totalcharges + Double.Parse(billamountdue.Text) + Double.Parse(billPenalty.Text) + Double.Parse(totalothers) + Double.Parse(totalotherchargee)) - (Double.Parse(billdiscount.Text) + Double.Parse(billadvancepayment.Text))

        billTotalamountdue.Text = Format(totalamountdue, "Standard")

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub billarrears_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles billarrears.CellValueChanged
        If billarrears.Rows.Count = 0 Then
        Else

            computetotalamount()
        End If

    End Sub
    'This will fire immediately when you click in the cell...
    Private Sub billarrears_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles billarrears.CurrentCellDirtyStateChanged
        If billarrears.IsCurrentCellDirty Then
            billarrears.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub billsave_Click(sender As Object, e As EventArgs) Handles billsave.Click

        dgTemp.Rows.Add(1, billAccountNo.Text, "Bill", Format(Double.Parse(billTotalamountdue.Text), "standard"))
        computeall()
        clearallfields()


        billAccountNo.Select()


    End Sub

    Private Sub TempCR_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.MdiParent = eBACSmain
        clearallfields()


        billAccountNo.Select()

    End Sub

    ''' <summary>
    ''' move form without border
    ''' </summary>

    Public MoveForm As Boolean
    Public MoveForm_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveForm = True
            Me.Cursor = Cursors.NoMove2D
            MoveForm_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveForm Then
            Me.Location = Me.Location + (e.Location - MoveForm_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub dgvothers_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvothers.CellValueChanged
        If dgvothers.Rows.Count = 0 Then
        Else

            computetotalamount()
        End If
    End Sub

    Private Sub dgvothers_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgvothers.CurrentCellDirtyStateChanged
        If dgvothers.IsCurrentCellDirty Then
            dgvothers.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub CRBillno_KeyDown(sender As Object, e As KeyEventArgs) Handles CRBillno.KeyDown
        If e.KeyCode = Keys.Enter Then
            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try
            sqlData1.Clear()
            stracs = "select AccountNumber FROM Bills WHERE BillNo = " & CRBillno.Text & ""
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(sqlData1)

            If sqlData1.Rows.Count = 0 Then
                MsgBox("No Bill Found")
                clearallfields()
            Else
                billAccountNo.Text = sqlData1.Rows(0)("AccountNumber")
                billAccountNo_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))
            End If

        End If

    End Sub

    Public Function PrintCellText(ByVal strValue As String, ByVal x As Integer, ByVal y As Integer,
                            ByVal w As Integer,
                            ByVal e As System.Drawing.Printing.PrintPageEventArgs,
                            ByVal Font As Font, ByVal Format As StringFormat) As Integer

        Dim cellRect As RectangleF = New RectangleF()
        cellRect.Location = New Point(x, y)


        cellRect.Size = New Size(w, CInt(e.Graphics.MeasureString(strValue, Font, w,
                              StringFormat.GenericTypographic).Height))

        e.Graphics.DrawString(strValue, Font, Brushes.Black, cellRect, Format)

        Return y + cellRect.Size.Height

    End Function



    Private Sub accSearch_Click(sender As Object, e As EventArgs) Handles accSearch.Click

        'SearchAccount.Close()
        'SearchAccount.Show()
        'SearchAccount.BringToFront()
        SearchAccount.searchingform = "tempCR"
        SearchAccount.ShowDialog()


    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        clearallfields()

        Calculator.Label1_Click(Nothing, New KeyEventArgs(Keys.Enter))

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs)

        CRCancel.cancelmode = "Create New"
        CRCancel.ShowDialog()


    End Sub

    Private Sub DetailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DetailsToolStripMenuItem.Click

        CRCancel.cancelmode = "Viewing"
        CRCancel.ShowDialog()

    End Sub

    Private Sub Collection_CR_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Me.Activate()
    End Sub

    Private Sub FindToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FindToolStripMenuItem.Click
        accSearch_Click(Nothing, New KeyEventArgs(Keys.Enter))
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        billsave_Click(Nothing, New KeyEventArgs(Keys.Enter))
    End Sub

    Private Sub currentBill_CheckedChanged(sender As Object, e As EventArgs) Handles currentBilling.CheckedChanged

        If currentBilling.CheckState = CheckState.Checked Then

            'currentBill.Enabled = False
            computetotalamount()

        End If

        If currentBilling.CheckState = CheckState.Unchecked Then

            computetotalamount()

            Dim totalcharges, totalcurrent As Double
            If billcharges.Rows.Count = 0 Then
            Else
                For x = 0 To billcharges.Rows.Count - 1

                    totalcharges = totalcharges + Double.Parse(billcharges.Rows(x).Cells(1).Value)

                Next
            End If

            totalcurrent = Double.Parse(billamountdue.Text) + Double.Parse(billPenalty.Text + totalcharges) - (Double.Parse(billdiscount.Text) + Double.Parse(billadvancepayment.Text))

            billTotalamountdue.Text = Format(billTotalamountdue.Text - totalcurrent, "Standard")

        End If



    End Sub

    Private Sub billTotalamountdue_TextChanged(sender As Object, e As EventArgs) Handles billTotalamountdue.TextChanged

        If billTotalamountdue.Text.ToString = "0.00" Then

            billsave.Enabled = False
            billsave.Hide()

        Else

            billsave.Enabled = True
            billsave.Show()
        End If

    End Sub

    Private Sub createcr_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.BackColor = Color.SteelBlue
    End Sub

    Private Sub createcr_Deactivated(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.BackColor = Color.FromArgb(17, 153, 195)
    End Sub

    Private Sub ControlsClick(sender As Object, e As EventArgs) Handles _
         billAccountNo.Click, billName.Click, billAddress.Click, billZone.Click, billbillingmonth.Click, discdate.Click,
        currentBilling.Click, CRBillno.Click, billamountdue.Click, billPenalty.Click, billdiscount.Click, billdiscount.Click,
        billadvancepayment.Click, billcharges.Click, billarrears.Click, dgvothers.Click, dgvotherchages.Click, billTotalamountdue.Click, billsave.Click ' etc.
        Me.Activate() 'Or Whatever
    End Sub

    Sub computeall()

        If dgTemp.Rows.Count = 0 Then
        Else
            Dim qwe As Double = 0
            For x = 0 To dgTemp.Rows.Count - 1

                qwe = qwe + Double.Parse(dgTemp.Rows(x).Cells(3).Value)

            Next

            txtTotalAmount.Text = Format(qwe, "standard")

        End If

    End Sub

End Class