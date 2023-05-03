Public Class Collection_CR

    Private crnumber As Integer
    Dim maxbill As New DataTable
    Dim searchbill As New DataTable
    Dim reprintcr As String = "Option"

    Public number As Double
    Public ones As Integer
    Public tenten As Integer
    Public hundreds As Integer
    Public thousands As Integer
    Public hundredthou As Integer
    Public callhundred As Integer
    Public millions As Integer
    Public millionsthou As Double
    Public hundredmillion As Integer


    Public tenthousandword As String
    Public hundredthouword As String
    Public thousanword As String
    Public hundredword As String
    Public gansal As String
    Public tentenword As String
    Public wordones As String
    Public hundredwords As String
    Public millionwords As String
    Public millionthousanwords As String
    Public hundredmillionswords As String

    Public convertedamout As String

    Private Sub getcrnumber()

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        crno.Text = My.Settings.orfrom

        'If My.Settings.Office_Code = "A" Then
        '    sqlData1.Clear()
        '    stracs = "select number from tbllogicnumbers where id = '3'"
        '    acscmd.CommandText = stracs
        '    acscmd.Connection = acsconn
        '    acsda.SelectCommand = acscmd
        '    acsda.Fill(sqlData1)

        '    crnumber = sqlData1.Rows(0)("number")

        '    crno.Text = My.Settings.Office_Code & Format(crnumber, "0000000")
        'End If

        'If My.Settings.Office_Code = "B" Then
        '    sqlData1.Clear()
        '    stracs = "select number from tbllogicnumbers where id = '11'"
        '    acscmd.CommandText = stracs
        '    acscmd.Connection = acsconn
        '    acsda.SelectCommand = acscmd
        '    acsda.Fill(sqlData1)

        '    crnumber = sqlData1.Rows(0)("number")

        '    crno.Text = My.Settings.Office_Code & Format(crnumber, "0000000")
        'End If


    End Sub

    Public Sub clearallfields()
        billAccountNo.Clear()
        billAddress.Clear()
        billadvancepayment.Text = "0.00"
        billAdjustment.Text = "0.00"
        billamountdue.Text = "0.00"
        billarrears.Rows.Clear()
        billcharges.Rows.Clear()
        billdiscount.Text = "0.00"
        billEarlyDisc.Text = "0.00"
        billPenalty.Text = "0.00"
        billTotalamountdue.Text = "0.00"
        billcheckno.Clear()
        billcheckdate.Clear()
        billbillingmonth.Clear()
        discdate.Clear()
        billName.Clear()
        billZone.Clear()
        CRBillno.Clear()

        dgvotherchages.Rows.Clear()
        dgvothers.Rows.Clear()
        amountpaid.Text = "0.00"
        overpayment.Text = "0.00"

        billcheckdate.ReadOnly = False
        billcheckno.ReadOnly = False
        billsave.Enabled = False
        billsave.Visible = False
        rbcash.Checked = True
        lblaccstatus.Text = "Mode"
        lblaccstatus.Visible = False

        lblStatus.Text = "Mode"
        lblStatus.Visible = False

        btnDelete.Hide()
        reprint.Hide()

        'Label8.Visible = False
        'Label6.Visible = False
        billcheckno.Clear()
        billcheckdate.Clear()

    End Sub

    Dim billdata As New DataTable
    Dim sur_charge As Double
    Dim reconnectionFee As Double


    Sub loadcurrentbill()

        Dim billdatacharges As New DataTable


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

                Dim d As DateTime = Now
                Dim earlyPaymentDate As DateTime

                If (Date.Parse(billdata(0)("ReadingDate")).ToString("dddd") = "Monday") Then
                    earlyPaymentDate = Date.Parse(billdata(0)("ReadingDate")).AddDays(4)
                Else
                    earlyPaymentDate = Date.Parse(billdata(0)("ReadingDate")).AddDays(6)
                End If

                If (earlyPaymentDate >= d.ToShortDateString) Then
                    Select Case MsgBox("Add early payment discount to this bill?", MsgBoxStyle.YesNo)
                        Case MsgBoxResult.No
                            billEarlyDisc.Text = 0.00
                        Case MsgBoxResult.Yes
                            billEarlyDisc.Text = Math.Round(billamountdue.Text * 0.05, 2).ToString("0.00")
                    End Select

                Else
                    billEarlyDisc.Text = 0.00
                End If

                'If (Format(Date.Parse(billdata(0)("DiscDate")).AddDays(10) == "") Then
                Dim disc_date As DateTime

                If (Date.Parse(billdata(0)("DiscDate")).AddDays(10).ToString("dddd") = "Sunday") Then
                    disc_date = Date.Parse(billdata(0)("DiscDate")).AddDays(11)

                ElseIf (Date.Parse(billdata(0)("DiscDate")).AddDays(10).ToString("dddd") = "Saturday") Then
                    disc_date = Date.Parse(billdata(0)("DiscDate")).AddDays(12)
                Else
                    disc_date = Date.Parse(billdata(0)("DiscDate")).AddDays(10)
                End If

                If (disc_date <= d.ToShortDateString Or maxbill.Rows.Count > 1) Then
                    'MsgBox(maxbill.Rows.Count)
                    billcharges.Rows.Clear()

                    Select Case MsgBox("Surcharge fee should be added to this bill. Add to CR payment?", MsgBoxStyle.YesNo)

                        Case MsgBoxResult.Yes
                            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                            billdatacharges.Clear()

                            stracs = "select * from Charges where Particular = 'Surcharge'"
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acsda.SelectCommand = acscmd
                            acsda.Fill(billdatacharges)

                            If billdatacharges.Rows.Count = 0 Then

                            Else
                                sur_charge = Math.Round((Format(billdata(0)("AmountDue"), "standard") - Double.Parse(billdata(0)("Discount")) + Double.Parse(billdata(0)("ArrearsBill")) + Double.Parse(billdata(0)("ArrearsCharges"))) * Format(billdatacharges(0)("Amount"), "standard"), 2).ToString("0.00")
                                For i = 0 To billdatacharges.Rows.Count - 1
                                    billcharges.Rows.Add(billdatacharges(i)("Particular"), Format(sur_charge, "standard"), billdatacharges(i)("ChargeID"))
                                Next




                            End If

                        Case MsgBoxResult.No
                            sur_charge = 0.00

                    End Select
                Else

                End If


                    If IsDBNull(billdata(0)("Adjustment")) = True Then
                        billAdjustment.Text = "0.00"
                    Else
                        billAdjustment.Text = billdata(0)("Adjustment")
                    End If


                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    sqldataBilling.Clear()

                    stracs = "select * from BillCharges where Category = 'Others' and BillNumber = " & CRBillno.Text
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsda.SelectCommand = acscmd
                    acsda.Fill(sqldataBilling)

                    'billcharges.Rows.Clear()

                    If sqldataBilling.Rows.Count = 0 Then

                    Else

                        For i = 0 To sqldataBilling.Rows.Count - 1




                            billcharges.Rows.Add(sqldataBilling(i)("Particulars"), Format(sqldataBilling(i)("Amount"), "standard"), sqldataBilling(i)("BillChargesID"))


                        Next

                    End If

                    If maxbill.Rows.Count = 1 And searchbill(0)("CustomerStatus") <> "Disconnected" Then

                    If Format(Date.Parse(billdata(0)("DiscDate")), "yyyy-MM-dd") < Format(Date.Parse(Now), "yyyy-MM-dd") Then
                        Select Case MsgBox("Reconnection fee should be added to this bill. Add to payment?", MsgBoxStyle.YesNo)
                            Case MsgBoxResult.Yes
                                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                billdatacharges.Clear()

                                stracs = "select * from Charges where Particular = 'Reconnection Fee'"
                                acscmd.CommandText = stracs
                                acscmd.Connection = acsconn
                                acsda.SelectCommand = acscmd
                                acsda.Fill(billdatacharges)


                                If billdatacharges.Rows.Count = 0 Then

                                Else
                                    reconnectionFee = Math.Round(billdatacharges(0)("Amount"), 2)
                                    Dim reconnectionFeeString As String = reconnectionFee.ToString("0.00")

                                    For i = 0 To billdatacharges.Rows.Count - 1
                                        billcharges.Rows.Add(billdatacharges(i)("Particular"), reconnectionFeeString, billdatacharges(i)("ChargeID"))
                                    Next




                                End If

                            Case MsgBoxResult.No
                                reconnectionFee = 0.00
                                'billcharges.Rows.Clear()

                        End Select

                    End If

                End If

                If maxbill.Rows.Count > 1 And searchbill(0)("CustomerStatus") <> "Disconnected" Then
                    Select Case MsgBox("Reconnection fee should be added to this bill. Add to payment?", MsgBoxStyle.YesNo)
                        Case MsgBoxResult.Yes
                            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                            billdatacharges.Clear()

                            stracs = "select * from Charges where Particular = 'Reconnection Fee'"
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acsda.SelectCommand = acscmd
                            acsda.Fill(billdatacharges)

                            If billdatacharges.Rows.Count = 0 Then

                            Else
                                reconnectionFee = Math.Round(billdatacharges(0)("Amount"), 2)
                                Dim reconnectionFeeString As String = reconnectionFee.ToString("0.00")

                                For i = 0 To billdatacharges.Rows.Count - 1
                                    billcharges.Rows.Add(billdatacharges(i)("Particular"), reconnectionFeeString, billdatacharges(i)("ChargeID"))
                                Next




                            End If

                        Case MsgBoxResult.No
                            reconnectionFee = 0.00
                            'billcharges.Rows.Clear()

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

        stracs = "select BillNo from Bills where AccountNumber = '" & billAccountNo.Text & "' and IsPaid = 'No' and BillStatus = 'Posted' and Cancelled = 'No' AND NOT IsCollectionCreated = 'Yes' AND NOT isPromisorry = 'YesPosted' and BillNo < " & tempbillno & " order by BillNo asc"
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

                If IsDBNull(loadbillarrears.Rows(0)("Adjustment")) = True Then
                    loadbillarrears.Rows(0)("Adjustment") = 0
                Else
                    loadbillarrears.Rows(0)("Adjustment") = loadbillarrears.Rows(0)("Adjustment")
                End If

                billarrears.Rows.Add(True, loadbillarrears.Rows(0)("Billno"), loadbillarrears.Rows(0)("BillingDate"), Format(Val((loadbillarrears.Rows(0)("AmountDue")) + Val(arrearscharge) + Val(loadbillarrears.Rows(0)("ArrearsInterest")) + Val(loadbillarrears.Rows(0)("PenaltyAfterDue")) + Val(loadbillarrears.Rows(0)("Adjustment"))) - (Val(loadbillarrears.Rows(0)("Discount")) + Val(loadbillarrears.Rows(0)("AdvancePayment"))), "standard"),
                                     Format(Val(loadbillarrears.Rows(0)("AmountDue") + Val(loadbillarrears.Rows(0)("PenaltyAfterDue"))) - (Val(loadbillarrears.Rows(0)("Discount")) + Val(loadbillarrears.Rows(0)("AdvancePayment"))), "standard"), chargepart, Val(arrearscharge))

                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                sqlData1.Clear()

                ''stracs = "select * from BillCharges where IsPaid = 'No' and Cancelled = 'No' AND NOT IsCollectionCreated = 'Yes' AND Category = 'Charges' and BillNumber = " & getarrears.Rows(t)("BillNo")
                ''acscmd.Connection = acsconn
                ''acscmd.CommandText = stracs
                ''acsda.SelectCommand = acscmd
                ''acsda.Fill(sqlData1)

                ''If sqlData1.Rows.Count = 0 Then

                ''Else
                ''    'Create_OR.clearallfields()
                ''    Create_OR.AccountNo.Text = billAccountNo.Text
                ''    Create_OR.loadinfo()

                ''    Create_OR.Show()
                ''    Create_OR.BringToFront()

                ''    'Create_OR.dgvitems.Rows.Add("1", sqldataBilling(i)("Particulars"), Format(sqldataBilling(i)("Amount"), "standard"), Format(sqldataBilling(i)("Amount"), "standard"), sqldataBilling.Rows(i)("ChargeID"), sqldataBilling.Rows(i)("Category"), sqldataBilling.Rows(i)("Entry"), "Yes")





                ''    For y = 0 To sqlData1.Rows.Count - 1

                ''        Create_OR.dgvitems.Rows.Add("1", sqlData1.Rows(y)("Particulars"), Format(sqlData1.Rows(y)("Amount"), "standard"), Format(sqlData1.Rows(y)("Amount"), "standard"), sqlData1.Rows(y)("ChargeID"), sqldataBilling.Rows(y)("Category"), sqldataBilling.Rows(y)("Entry"), "Yes")
                ''    Next
                ''    Create_OR.compute()
                ''    billAccountNo.Select()
                ''End If


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
            Next

        End If

        'If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        'Dim loadchargespn As New DataTable
        'loadchargespn.Clear()
        'stracs = "select a.Billno as Billno, a.BillingDate as BillingDate, (SUM(b.amount) + SUM(a.AmountDue) + SUM(a.PenaltyAfterDue)) - SUM(a.Discount) as amount from Bills a join BillCharges b on a.BillNo = b.BillNumber where a.IsPaid = 'No' and b.IsPaid = 'No' and a.BillNo = " & getarrears.Rows(t)("BillNo") & " and b.BillNumber = " & getarrears.Rows(t)("BillNo") & " group by a.BillNo, a.BillingDate"

        'stracs = "select * from BillCharges where Category = 'Others' and isPromisorry = 'YesPosted' and AccountNumber = '" & billAccountNo.Text & "'"
        'acscmd.CommandText = stracs
        'acscmd.Connection = acsconn
        'acsda.SelectCommand = acscmd
        'acsda.Fill(loadchargespn)

        'billcharges.Rows.Clear()
        'If loadchargespn.Rows.Count = 0 Then

        'Else

        '    For i = 0 To loadchargespn.Rows.Count - 1

        '        billcharges.Rows.Add(loadchargespn(i)("Particulars"), Format(loadchargespn(i)("Amount"), "standard"), loadchargespn(i)("BillChargesID"))

        '    Next

        'End If

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

            'Create_OR.AccountNo.Text = billAccountNo.Text
            'Create_OR.loadinfo()

            'Create_OR.Show()
            'Create_OR.BringToFront()

            For k = 0 To otherbillcharges.Rows.Count - 1

                If otherbillcharges(k)("Category") = "Charges" Then

                Else
                    dgvotherchages.Rows.Add(otherbillcharges.Rows(k)("Particulars"), otherbillcharges.Rows(k)("Amount"), otherbillcharges.Rows(k)("BillChargesID"))
                End If

                ' dgvothers.Rows.Add(True, otherbills.Rows(k)("RefNo"), otherbills.Rows(k)("Particulars"), FormatNumber(Val(otherbills.Rows(k)("Billing"))), otherbills.Rows(k)("ID"))
            Next
            Create_OR.compute()
            billAccountNo.Select()
        End If


        'If Create_OR.dgvitems.Rows.Count = 0 Then

        'Else
        '    MsgBox("Meron dapat bayaran sa OR")


        '    'MsgBox(My.Computer.Screen.WorkingArea.Height)
        '    Me.Location = New Point(0, ((My.Computer.Screen.WorkingArea.Height / 2) - (Me.Width / 2)) / 4)
        '    ' MsgBox(((My.Computer.Screen.WorkingArea.Height / 2) - (Collection_CR.Width / 2)) / 4)

        '    Create_OR.logic = "From CR"
        '    Create_OR.Show()
        '    Create_OR.BringToFront()

        '    Create_OR.Location = New Point(0 + Me.Width, ((My.Computer.Screen.WorkingArea.Height / 2) - (Me.Width / 2)) / 4)
        '    Create_OR.compute()



        '    Calculator.Show()
        '    Calculator.BringToFront()

        '    Calculator.Location = New Point(0 + Me.Width + Create_OR.Width, ((My.Computer.Screen.WorkingArea.Height / 2) - (Me.Width / 2)) / 4)
        '    Calculator.dgvcalc.Rows.Clear()
        '    Calculator.lbltotalamountdue.Text = "0.00"
        'End If


        computetotalamount()
    End Sub


    Public Sub billAccountNo_KeyDown(sender As Object, e As KeyEventArgs) Handles billAccountNo.KeyDown

        If e.KeyCode = Keys.Enter Then

            getcrnumber()

            billcharges.Rows.Clear()

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
                btnDelete.Hide()
                billAccountNo.SelectAll()
                currentBilling.CheckState = CheckState.Unchecked
            Else

                'Create_OR.RefereshToolStripMenuItem_Click(Nothing, New KeyEventArgs(Keys.Enter))

                'Create_OR.Show()
                btnDelete.Hide()

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

                stracs = "select BillNo from Bills where AccountNumber = '" & billAccountNo.Text & "' AND IsPaid = 'No' AND BillStatus = 'Posted' AND NOT IsCollectionCreated = 'Yes' AND isPromisorry <> 'YesPosted' and Cancelled = 'No' order by BillNo desc"
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
                        'stracs = "select CRNo,PaymentDate, TotalAmountDue, AdvancePayment from Collection_Details where CRNo = (select max(CRNo) as maxcrno from Collection_Details where AccountNo = '" & billAccountNo.Text & "')"
                        stracs = "select CRNo,PaymentDate, TotalAmountDue, AdvancePayment from Collection_Details where CRNo = (select CRNo from Collection_Details where AccountNo = '" & billAccountNo.Text & "' and CollectionID = (select max(CollectionID) as CollectionID from Collection_Details where AccountNo = '" & billAccountNo.Text & "'))"
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
                               & "CRNo. " & getlastpayment(0)("CRNo") & vbCrLf _
                               & "Amount: " & Format(Val(getlastpayment(0)("TotalAmountDue")), "Standard") _
                               & vbCrLf & "Date: " & getlastpayment(0)("PaymentDate"))

                            'MsgBox("All water bills are paid.")
                            clearallfields()
                            billAccountNo.Select()

                        End If

                    Else

                        If searchbill.Rows(0)("CustomerStatus") = "Disconnected2" Then



                            Dim asd As DialogResult = MessageBox.Show("Reconnection Fee should be added to this bill. Add to OR payment?", "Electronic Billing and Collection", MessageBoxButtons.YesNo)

                            If asd = DialogResult.Yes Then

                                Create_OR.clearallfields()
                                Create_OR.AccountNo.Text = billAccountNo.Text
                                Create_OR.loadinfo()

                                Create_OR.Show()
                                Create_OR.BringToFront()

                                'Create_OR.Location = New Point(0 + Me.Width, ((My.Computer.Screen.WorkingArea.Height / 2) - (Me.Width / 2)) / 4)
                                Create_OR.compute()

                                Calculator.Show()
                                Calculator.BringToFront()

                                'Calculator.Location = New Point(0 + Me.Width + Create_OR.Width, ((My.Computer.Screen.WorkingArea.Height / 2) - (Me.Width / 2)) / 4)
                                'Calculator.dgvcalc.Rows.Clear()
                                'Calculator.totalamount.Text = "0.00"

                                If Create_OR.dgvitems.Rows.Count = 0 Then

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
                                        Create_OR.dgvitems.Rows.Add("1", reconn.Rows(0)("Particular"), reconn.Rows(0)("Amount"), reconn.Rows(0)("Amount"), reconn.Rows(0)("ChargeID"), reconn.Rows(0)("Category"), reconn.Rows(0)("Entry"))
                                        Create_OR.compute()
                                    Else

                                        stracs = "select * from reconnectioncharge where ID = 2"
                                        acscmd.CommandText = stracs
                                        acscmd.Connection = acsconn
                                        acsda.SelectCommand = acscmd
                                        acsda.Fill(reconn)

                                        'Create_OR.dgvitems.Rows.Add("1", "Reopening Fee", "150", "150", "14", "Charges", "Others")
                                        Create_OR.dgvitems.Rows.Add("1", reconn.Rows(0)("Particular"), reconn.Rows(0)("Amount"), reconn.Rows(0)("Amount"), reconn.Rows(0)("ChargeID"), reconn.Rows(0)("Category"), reconn.Rows(0)("Entry"))
                                        Create_OR.compute()
                                    End If

                                Else

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
                                        Create_OR.dgvitems.Rows.Add("1", reconn.Rows(0)("Particular"), reconn.Rows(0)("Amount"), reconn.Rows(0)("Amount"), reconn.Rows(0)("ChargeID"), reconn.Rows(0)("Category"), reconn.Rows(0)("Entry"))
                                        Create_OR.compute()
                                    Else

                                        stracs = "select * from reconnectioncharge where ID = 2"
                                        acscmd.CommandText = stracs
                                        acscmd.Connection = acsconn
                                        acsda.SelectCommand = acscmd
                                        acsda.Fill(reconn)

                                        'Create_OR.dgvitems.Rows.Add("1", "Reopening Fee", "150", "150", "14", "Charges", "Others")
                                        Create_OR.dgvitems.Rows.Add("1", reconn.Rows(0)("Particular"), reconn.Rows(0)("Amount"), reconn.Rows(0)("Amount"), reconn.Rows(0)("ChargeID"), reconn.Rows(0)("Category"), reconn.Rows(0)("Entry"))
                                        Create_OR.compute()
                                    End If

                                End If
                                Me.Activate()
                                billAccountNo.Select()
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

                        If dgvothers.Rows.Count > 0 Then

                            grouparrears.Hide()
                            dgvothers.Show()
                            dgvotherchages.Show()

                            dgvothers.Location = New Point(8, 375)
                            dgvotherchages.Location = New Point(267, 375)

                            cashcheck.Location = New Point(8, 483)
                            paymentgroup.Location = New Point(214, 483)

                            Panel1.Size = New Size(512, 623)
                            Me.Size = New Size(515, 669)

                        Else

                            grouparrears.Hide()
                            dgvothers.Hide()
                            dgvotherchages.Hide()

                            cashcheck.Location = New Point(8, 375)
                            paymentgroup.Location = New Point(214, 375)

                            Panel1.Size = New Size(512, 516)
                            Me.Size = New Size(515, 563)

                        End If

                    End If

                Else
                    CRBillno.Text = maxbill.Rows(0)("BillNo")
                    currentBilling.Enabled = True

                    If searchbill.Rows(0)("CustomerStatus") = "Disconnected2" Then

                        loadcurrentbill()

                        Dim asd As DialogResult = MessageBox.Show("Reconnection Fee should be added to this bill. Add to OR payment?", "Electronic Billing and Collection", MessageBoxButtons.YesNo)


                        If asd = DialogResult.Yes Then

                            Create_OR.clearallfields()
                            Create_OR.AccountNo.Text = billAccountNo.Text
                            Create_OR.loadinfo()

                            Create_OR.Show()
                            Create_OR.BringToFront()

                            'Create_OR.Location = New Point(0 + Me.Width, ((My.Computer.Screen.WorkingArea.Height / 2) - (Me.Width / 2)) / 4)
                            Create_OR.compute()

                            Create_OR.Show()
                            Create_OR.BringToFront()

                            'Create_OR.Location = New Point(0 + Me.Width, ((My.Computer.Screen.WorkingArea.Height / 2) - (Me.Width / 2)) / 4)
                            Create_OR.compute()

                            'Calculator.Show()
                            'Calculator.BringToFront()

                            'Calculator.Location = New Point(0 + Me.Width + Create_OR.Width, ((My.Computer.Screen.WorkingArea.Height / 2) - (Me.Width / 2)) / 4)
                            'Calculator.dgvcalc.Rows.Clear()
                            'Calculator.totalamount.Text = "0.00"

                            If Create_OR.dgvitems.Rows.Count = 0 Then

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
                                    Create_OR.dgvitems.Rows.Add("1", reconn.Rows(0)("Particular"), reconn.Rows(0)("Amount"), reconn.Rows(0)("Amount"), reconn.Rows(0)("ChargeID"), reconn.Rows(0)("Category"), reconn.Rows(0)("Entry"))
                                    Create_OR.compute()
                                Else

                                    stracs = "select * from reconnectioncharge where ID = 2"
                                    acscmd.CommandText = stracs
                                    acscmd.Connection = acsconn
                                    acsda.SelectCommand = acscmd
                                    acsda.Fill(reconn)

                                    'Create_OR.dgvitems.Rows.Add("1", "Reopening Fee", "150", "150", "14", "Charges", "Others")
                                    Create_OR.dgvitems.Rows.Add("1", reconn.Rows(0)("Particular"), reconn.Rows(0)("Amount"), reconn.Rows(0)("Amount"), reconn.Rows(0)("ChargeID"), reconn.Rows(0)("Category"), reconn.Rows(0)("Entry"))
                                    Create_OR.compute()
                                End If

                            Else

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
                                    Create_OR.dgvitems.Rows.Add("1", reconn.Rows(0)("Particular"), reconn.Rows(0)("Amount"), reconn.Rows(0)("Amount"), reconn.Rows(0)("ChargeID"), reconn.Rows(0)("Category"), reconn.Rows(0)("Entry"))
                                    Create_OR.compute()
                                Else

                                    stracs = "select * from reconnectioncharge where ID = 2"
                                    acscmd.CommandText = stracs
                                    acscmd.Connection = acsconn
                                    acsda.SelectCommand = acscmd
                                    acsda.Fill(reconn)

                                    'Create_OR.dgvitems.Rows.Add("1", "Reopening Fee", "150", "150", "14", "Charges", "Others")
                                    Create_OR.dgvitems.Rows.Add("1", reconn.Rows(0)("Particular"), reconn.Rows(0)("Amount"), reconn.Rows(0)("Amount"), reconn.Rows(0)("ChargeID"), reconn.Rows(0)("Category"), reconn.Rows(0)("Entry"))
                                    Create_OR.compute()
                                End If



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

                    If billarrears.Rows.Count = 0 Then

                        If dgvothers.Rows.Count > 0 Then

                            grouparrears.Hide()
                            dgvothers.Show()
                            dgvotherchages.Show()

                            dgvothers.Location = New Point(5, 375)
                            dgvotherchages.Location = New Point(264, 375)

                            cashcheck.Location = New Point(8, 483)
                            paymentgroup.Location = New Point(214, 483)

                            Panel1.Size = New Size(512, 620)
                            Me.Size = New Size(515, 662)

                        Else


                            grouparrears.Hide()
                            dgvothers.Hide()
                            dgvotherchages.Hide()

                            cashcheck.Location = New Point(8, 375)
                            paymentgroup.Location = New Point(214, 375)

                            Panel1.Size = New Size(512, 513)
                            Me.Size = New Size(515, 560)

                        End If

                    Else

                        If dgvothers.Rows.Count > 0 Then

                            grouparrears.Show()
                            dgvothers.Show()
                            dgvotherchages.Show()

                            grouparrears.Location = New Point(8, 375)

                            dgvothers.Location = New Point(9, 486)
                            dgvotherchages.Location = New Point(268, 486)

                            cashcheck.Location = New Point(8, 594)
                            paymentgroup.Location = New Point(214, 594)

                            Panel1.Size = New Size(512, 737)
                            Me.Size = New Size(515, 777)

                        Else

                            grouparrears.Show()
                            dgvothers.Hide()
                            dgvotherchages.Hide()

                            grouparrears.Location = New Point(8, 375)

                            Panel1.Size = New Size(512, 621)
                            Me.Size = New Size(515, 663)

                            cashcheck.Location = New Point(8, 486)
                            paymentgroup.Location = New Point(214, 486)

                        End If

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

                ''Dim getcharges As New DataTable
                ''stracs = "select * from BillCharges where Category = 'Charges' and IsCollectionCreated = 'No' and IsPaid = 'No' and AccountNumber = '" & billAccountNo.Text & "'"
                ''acscmd.CommandText = stracs
                ''acscmd.Connection = acsconn
                ''acsda.SelectCommand = acscmd
                ''acsda.Fill(getcharges)

                ''If getcharges.Rows.Count = 0 Then

                ''Else

                ''    If Create_OR.dgvitems.Rows.Count = 0 Then

                ''        Create_OR.clearallfields()
                ''        Create_OR.AccountNo.Text = billAccountNo.Text
                ''        Create_OR.loadinfo()
                ''        Create_OR.Show()
                ''        Create_OR.BringToFront()
                ''        Create_OR.paymentfor.Clear()

                ''    End If

                ''    For x = 0 To getcharges.Rows.Count - 1

                ''        If Create_OR.paymentfor.Text = "" Then
                ''            Create_OR.paymentfor.Text = getcharges.Rows(x)("Particulars")
                ''        Else
                ''            Create_OR.paymentfor.Text = Create_OR.paymentfor.Text & " and " & getcharges.Rows(x)("Particulars")
                ''        End If


                ''        Create_OR.dgvitems.Rows.Add("1", getcharges(x)("Particulars"), Format(getcharges(x)("Amount"), "standard"), Format(getcharges(x)("Amount"), "standard"), getcharges.Rows(x)("BillChargesID"), getcharges.Rows(x)("Category"), getcharges.Rows(x)("Entry"), "Yes")

                ''    Next

                ''    Create_OR.compute()

                ''    billAccountNo.Select()

                ''End If

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

        totalamountdue = Double.Parse(totalcharges + Double.Parse(billamountdue.Text) + Double.Parse(billPenalty.Text) + Double.Parse(totalothers) + Double.Parse(totalotherchargee) + +Double.Parse(billAdjustment.Text)) - (Double.Parse(billdiscount.Text) + Double.Parse(billEarlyDisc.Text) + Double.Parse(billadvancepayment.Text))

        billTotalamountdue.Text = Format(totalamountdue, "Standard")
        amountpaid.Text = Format(totalamountdue, "Standard")
        overpayment.Text = "0.00"



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

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try



        If chkmode.Text = "Query Mode" Then

            Calculator.dgvcalc.Rows.Add(billAccountNo.Text, "Bill", FormatNumber(amountpaid.Text))
            Calculator.calculate()

            clearallfields()
            billsave.Hide()
            billsave.Enabled = False

            billAccountNo.Select()
            billAccountNo.SelectAll()

        End If

        If chkmode.Text = "Transaction Mode" Then



            If My.Settings.Admin = "Yes" Or My.Settings.Cashier = "Yes" Then

                Dim searchaccountno As New DataTable
                stracs = "Select AccountNo from Customers where AccountNo = '" & billAccountNo.Text.ToString.Replace("'", "''") & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(searchaccountno)

                If searchaccountno.Rows.Count = 0 Then

                    MsgBox("Please check account number.")

                    clearallfields()
                    billsave.Hide()
                    billsave.Enabled = False

                    billAccountNo.Select()
                    billAccountNo.SelectAll()

                Else
                    getcrnumber()

                    Cursor = Cursors.WaitCursor
                    reprintcr = "No"
                    If CRBillno.Text = "" AndAlso dgvotherchages.Rows.Count = 0 AndAlso dgvothers.Rows.Count = 0 AndAlso billcharges.Rows.Count = 0 Then
                        MsgBox("No data found")

                    ElseIf Val(overpayment.Text) < 0 Then
                        MsgBox("Invalid amount paid")
                    Else

                        Try
                            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        Catch ex As Exception
                            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        End Try

                        Dim searchcrno As New DataTable
                        stracs = "select CrNo from Collection_Details where CrNo = '" & crno.Text & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acsda.SelectCommand = acscmd
                        acsda.Fill(searchcrno)

                        If searchcrno.Rows.Count = 0 Then

                            Dim currentbill As String = ""
                            Dim arrearbill As String = ""
                            Dim pnbill As String = ""

                            Dim checkbillspaid As New DataTable

                            If CRBillno.Text = "" Or IsDBNull(CRBillno.Text) = True Then
                                CRBillno.Text = 0
                            Else
                                CRBillno.Text = CRBillno.Text
                            End If

                            Try
                                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                            Catch ex As Exception
                                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                            End Try

                            stracs = "select BillNo from Bills where BillNo = " & CRBillno.Text & " and IsCollectionCreated = 'Yes'"
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acsda.SelectCommand = acscmd
                            acsda.Fill(checkbillspaid)

                            If checkbillspaid.Rows.Count = 0 Then

                                currentbill = "clear"

                            Else
                                currentbill = "unclear"
                            End If

                            If billarrears.Rows.Count = 0 Then
                                arrearbill = "clear"
                            Else

                                Dim x As Integer = 0
                                Do Until x = billarrears.Rows.Count

                                    Try
                                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                    Catch ex As Exception
                                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                    End Try

                                    checkbillspaid.Clear()
                                    stracs = "select BillNo from Bills where BillNo = " & billarrears.Rows(x).Cells(1).Value & " and IsCollectionCreated = 'Yes'"
                                    acscmd.CommandText = stracs
                                    acscmd.Connection = acsconn
                                    acsda.SelectCommand = acscmd
                                    acsda.Fill(checkbillspaid)

                                    If checkbillspaid.Rows.Count = 0 Then
                                        arrearbill = "clear"
                                        x = x + 1
                                    Else
                                        arrearbill = "unclear"
                                        x = billarrears.Rows.Count - 1

                                    End If

                                Loop

                            End If

                            If dgvothers.Rows.Count = 0 Then
                                pnbill = "clear"
                            Else
                                Dim y = 0
                                Do Until y = dgvothers.Rows.Count

                                    Try
                                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                    Catch ex As Exception
                                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                    End Try

                                    checkbillspaid.Clear()
                                    checkbillspaid.Rows.Clear()
                                    stracs = "select ID from AddAdjustment where ID = " & dgvothers.Rows(y).Cells(4).Value & " and IsCollectionCreated = 'Yes'"
                                    acscmd.CommandText = stracs
                                    acscmd.Connection = acsconn
                                    acsda.SelectCommand = acscmd
                                    acsda.Fill(checkbillspaid)

                                    If checkbillspaid.Rows.Count = 0 Then
                                        pnbill = "clear"
                                        y = y + 1
                                    Else
                                        pnbill = "unclear"
                                        y = dgvothers.Rows.Count - 1
                                    End If

                                Loop

                            End If

                            If currentbill = "clear" And arrearbill = "clear" And pnbill = "clear" Then

                                getcrnumber()

                                'If My.Settings.Office_Code = "A" Then

                                '    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                '    stracs = "Update tbllogicnumbers set number = number + 1 WHERE id = 3"
                                '    acscmd.CommandText = stracs
                                '    acscmd.Connection = acsconn
                                '    acscmd.ExecuteNonQuery()

                                'End If

                                'If My.Settings.Office_Code = "B" Then

                                '    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                '    stracs = "Update tbllogicnumbers set number = number + 1 WHERE id = 11"
                                '    acscmd.CommandText = stracs
                                '    acscmd.Connection = acsconn
                                '    acscmd.ExecuteNonQuery()

                                'End If

                                Dim paymenttype As String = ""

                                If rbOnline.Checked = True Then

                                    paymenttype = "Online"

                                End If

                                If rbcash.Checked = True Then

                                    paymenttype = "Cash"

                                End If

                                If rbcheck.Checked = True Then
                                    paymenttype = "Check"

                                End If

                                Try
                                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                Catch ex As Exception
                                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                End Try

                                stracs = "INSERT INTO Collection_Details (CRNo,AccountNo,AccountName,Address,CheckNo,CheckDate,TotalAmountDue,AdvancePayment,PaymentDate,Collector,Office,CollectionStatus,PaymentType)
                     Values ('" & crno.Text & "', '" & billAccountNo.Text & "', '" & billName.Text.ToString.Replace("'", "''") & "', '" & billAddress.Text.ToString.Replace("'", "''") & "',
                    '" & billcheckno.Text & "', '" & billcheckdate.Text & "', " & Double.Parse(amountpaid.Text) & "," & Double.Parse(overpayment.Text) & ", '" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss tt") & "',
                    '" & My.Settings.Nickname & "','" & My.Settings.Office_Name & "','Pending', '" & paymenttype & "')"
                                acscmd.CommandText = stracs
                                acscmd.Connection = acsconn
                                acscmd.ExecuteNonQuery()

                                If CRBillno.Text <> "" And currentBilling.CheckState = CheckState.Checked Then

                                    Try
                                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                    Catch ex As Exception
                                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                    End Try

                                    stracs = "INSERT INTO CollectionBilling (CRNo,AccountNo,AccountName,Address,Zone,BillingDate,PaymentDate,BillType,BillNo,AmountDue,Discount, earlyPaymentDiscount, Penalty, AdvancePayment, CollectionBillingStatus,Adjustment)
                    Values ('" & crno.Text & "','" & billAccountNo.Text & "','" & billName.Text.ToString.Replace("'", "''") & "','" & billAddress.Text.ToString.Replace("'", "''") & "',
                    '" & billZone.Text & "','" & billbillingmonth.Text & "','" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss tt") & "','BillCurrent','" & CRBillno.Text & "','" & Double.Parse(billamountdue.Text) & "',
                    '" & Double.Parse(billdiscount.Text) & "','" & Double.Parse(billEarlyDisc.Text) & "','" & Double.Parse(billPenalty.Text) & "','" & Double.Parse(billadvancepayment.Text) & "','Pending','" & Double.Parse(billAdjustment.Text) & "')"
                                    acscmd.CommandText = stracs
                                    acscmd.Connection = acsconn
                                    acscmd.ExecuteNonQuery()
                                    acscmd.Dispose()

                                    Try
                                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                    Catch ex As Exception
                                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                    End Try

                                    stracs = "UPDATE Bills set IsCollectionCreated = 'Yes', CRNo = '" & crno.Text & "', earlyPaymentDiscount='" & Double.Parse(billEarlyDisc.Text) & "' WHERE BillNo = " & CRBillno.Text
                                    acscmd.CommandText = stracs
                                    acscmd.Connection = acsconn
                                    acscmd.ExecuteNonQuery()
                                    acscmd.Dispose()

                                    If billcharges.Rows.Count = 0 Then

                                    Else
                                        'current billcharges
                                        For x = 0 To billcharges.Rows.Count - 1

                                            If (billcharges.Rows(x).Cells(0).Value = "Surcharge" Or billcharges.Rows(x).Cells(0).Value = "Reconnection Fee") Then
                                                'MsgBox(billcharges.Rows(x).Cells(0).Value)
                                                Try
                                                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                                Catch ex As Exception
                                                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                                End Try

                                                Dim sqlSurCharge As New DataTable
                                                sqlSurCharge.Clear()

                                                stracs = "select * from Charges WHERE ChargeID = '" & billcharges.Rows(x).Cells(2).Value & "'"
                                                acscmd.CommandText = stracs
                                                acscmd.Connection = acsconn
                                                acsda.SelectCommand = acscmd
                                                acsda.Fill(sqlSurCharge)

                                                Try
                                                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                                Catch ex As Exception
                                                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                                End Try

                                                stracs = "INSERT INTO CollectionCharges (CRNo,BillNo,Particulars,Amount,ChargeID,Category,Entry,CollectionChargesStatus) Values
                                                    ('" & crno.Text & "', '" & CRBillno.Text & "', '" & billcharges.Rows(x).Cells(0).Value & "', 
                                                    '" & billcharges.Rows(x).Cells(1).Value & "', '" & billcharges.Rows(x).Cells(2).Value & "', '" & sqlSurCharge.Rows(0)("Category") & "', '" & sqlSurCharge.Rows(0)("Category") & "', 'Pending')"
                                                acscmd.CommandText = stracs
                                                acscmd.Connection = acsconn
                                                acscmd.ExecuteNonQuery()
                                            Else
                                                sqlData1.Clear()
                                                Try
                                                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                                Catch ex As Exception
                                                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                                End Try
                                                stracs = "select * from BillCharges WHERE BillChargesID = " & billcharges.Rows(x).Cells(2).Value
                                                acscmd.CommandText = stracs
                                                acscmd.Connection = acsconn
                                                acsda.SelectCommand = acscmd
                                                acsda.Fill(sqlData1)


                                                Try
                                                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                                Catch ex As Exception
                                                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                                End Try
                                                stracs = "INSERT INTO CollectionCharges (CRNo,BillNo,Particulars,Amount,ChargeID,Category,Entry,CollectionChargesStatus) Values
                                                    ('" & crno.Text & "', '" & CRBillno.Text & "', '" & billcharges.Rows(x).Cells(0).Value & "', 
                                                    '" & billcharges.Rows(x).Cells(1).Value & "', '" & sqlData1.Rows(0)("ChargeID") & "', '" & sqlData1.Rows(0)("Category") & "', '" & sqlData1.Rows(0)("Entry") & "', 'Pending')"
                                                acscmd.CommandText = stracs
                                                acscmd.Connection = acsconn
                                                acscmd.ExecuteNonQuery()

                                                Try
                                                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                                Catch ex As Exception
                                                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                                End Try

                                                stracs = "UPDATE BillCharges set IsCollectionCreated = 'Yes' , CRNo = '" & crno.Text & "' WHERE BillChargesID = " & sqlData1.Rows(0)("BillChargesID")
                                                acscmd.CommandText = stracs
                                                acscmd.Connection = acsconn
                                                acscmd.ExecuteNonQuery()

                                            End If
                                        Next
                                    End If

                                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                    stracs = "UPDATE Bills SET Surcharge = '" & sur_charge & "' where BillNo = '" & CRBillno.Text & "'"
                                    acscmd.CommandText = stracs
                                    acscmd.Connection = acsconn
                                    acscmd.ExecuteNonQuery()
                                    acscmd.Dispose()

                                End If

                                'Arrears bill
                                If billarrears.Rows.Count = 0 Then

                                Else

                                    For k = 0 To billarrears.Rows.Count - 1

                                        If billarrears.Rows(k).Cells(0).Value = True Then

                                            Dim arrearsbill As New DataTable

                                            Try
                                                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                            Catch ex As Exception
                                                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                            End Try
                                            stracs = "select BillNo,AmountDue,PenaltyAfterDue,Discount,AdvancePayment,BillingDate,Adjustment, earlyPaymentDiscount from Bills WHERE BillNo = " & billarrears.Rows(k).Cells(1).Value
                                            acscmd.Connection = acsconn
                                            acscmd.CommandText = stracs
                                            acsda.SelectCommand = acscmd
                                            acsda.Fill(arrearsbill)

                                            'Arrears bill
                                            If arrearsbill.Rows.Count = 0 Then

                                            Else

                                                For x = 0 To arrearsbill.Rows.Count - 1

                                                    Try
                                                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                                    Catch ex As Exception
                                                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                                    End Try
                                                    stracs = "INSERT INTO CollectionBilling (CRNo,AccountNo,AccountName,Address,Zone,BillingDate,PaymentDate,BillType,BillNo,AmountDue,Discount,earlyPaymentDiscount,Penalty,AdvancePayment,CollectionBillingStatus,Adjustment)
                                    Values ('" & crno.Text & "','" & billAccountNo.Text & "','" & billName.Text.ToString.Replace("'", "''") & "','" & billAddress.Text.ToString.Replace("'", "''") & "',
                                    '" & billZone.Text & "','" & arrearsbill.Rows(x)("BillingDate") & "','" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss tt") & "','Bill','" & arrearsbill.Rows(x)("BillNo") & "','" & Double.Parse(arrearsbill.Rows(x)("AmountDue")) & "',
                                    '" & Double.Parse(arrearsbill.Rows(x)("Discount")) & "','" & Double.Parse(arrearsbill.Rows(x)("earlyPaymentDiscount")) & "','" & Double.Parse(arrearsbill.Rows(x)("PenaltyAfterDue")) & "','" & Double.Parse(arrearsbill.Rows(x)("AdvancePayment")) & "','Pending', " & Double.Parse(arrearsbill.Rows(x)("Adjustment")) & ")"
                                                    acscmd.CommandText = stracs
                                                    acscmd.Connection = acsconn
                                                    acscmd.ExecuteNonQuery()

                                                    Try
                                                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                                    Catch ex As Exception
                                                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                                    End Try

                                                    stracs = "UPDATE Bills set IsCollectionCreated = 'Yes', CRNo = '" & crno.Text & "', earlyPaymentDiscount='" & Double.Parse(arrearsbill.Rows(x)("earlyPaymentDiscount")) & "' WHERE BillNo = " & billarrears.Rows(k).Cells(1).Value
                                                    acscmd.CommandText = stracs
                                                    acscmd.Connection = acsconn
                                                    acscmd.ExecuteNonQuery()


                                                    'Arrears billcharges

                                                    Dim arrearsbillcharges As New DataTable

                                                    Try
                                                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                                    Catch ex As Exception
                                                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                                    End Try

                                                    stracs = "select * from BillCharges where Category = 'Others' and BillNumber = " & billarrears.Rows(k).Cells(1).Value
                                                    acscmd.Connection = acsconn
                                                    acscmd.CommandText = stracs
                                                    acsda.SelectCommand = acscmd
                                                    acsda.Fill(arrearsbillcharges)

                                                    If arrearsbillcharges.Rows.Count = 0 Then

                                                    Else

                                                        For d = 0 To arrearsbillcharges.Rows.Count - 1

                                                            Try
                                                                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                                            Catch ex As Exception
                                                                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                                            End Try

                                                            stracs = "INSERT INTO CollectionCharges (CRNo,BillNo,Particulars,Amount,ChargeID,Category,Entry,CollectionChargesStatus) Values
                                            ('" & crno.Text & "', '" & billarrears.Rows(k).Cells(1).Value & "', '" & arrearsbillcharges.Rows(d)("Particulars") & "', 
                                            '" & Double.Parse(arrearsbillcharges.Rows(d)("Amount")) & "', '" & arrearsbillcharges.Rows(d)("ChargeID") & "', '" & arrearsbillcharges.Rows(d)("Category") & "', '" & arrearsbillcharges.Rows(d)("Entry") & "','Pending')"
                                                            acscmd.CommandText = stracs
                                                            acscmd.Connection = acsconn
                                                            acscmd.ExecuteNonQuery()

                                                            Try
                                                                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                                            Catch ex As Exception
                                                                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                                            End Try

                                                            stracs = "UPDATE BillCharges set IsCollectionCreated = 'Yes', CRNo = '" & crno.Text & "' WHERE BillChargesID = " & arrearsbillcharges.Rows(d)("BillChargesID")
                                                            acscmd.CommandText = stracs
                                                            acscmd.Connection = acsconn
                                                            acscmd.ExecuteNonQuery()

                                                        Next

                                                    End If ' END of Arrears billcharges


                                                Next


                                            End If ' END of Arrears bills


                                        End If 'end of datagridview loop

                                    Next

                                End If


                                'others (PN, FB)


                                For m = 0 To dgvotherchages.Rows.Count - 1



                                    sqlData1.Clear()
                                    Try
                                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                    Catch ex As Exception
                                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                    End Try
                                    stracs = "select * FROM BillCharges WHERE BillChargesID = " & dgvotherchages.Rows(m).Cells(2).Value
                                    acscmd.Connection = acsconn
                                    acscmd.CommandText = stracs
                                    acsda.SelectCommand = acscmd
                                    acsda.Fill(sqlData1)

                                    'collection charges
                                    Try
                                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                    Catch ex As Exception
                                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                    End Try
                                    stracs = "INSERT INTO CollectionCharges (CRNo,BillNo,Particulars,Amount,ChargeID,Category,Entry,CollectionChargesStatus) Values
                                    ('" & crno.Text & "', '" & sqlData1.Rows(0)("BillNumber") & "', '" & sqlData1.Rows(0)("Particulars") & "', 
                                    '" & Double.Parse(sqlData1.Rows(0)("Amount")) & "','" & sqlData1.Rows(0)("ChargeID") & "','" & sqlData1.Rows(0)("Category") & "','" & sqlData1.Rows(0)("Entry") & "', 'Pending')"
                                    acscmd.CommandText = stracs
                                    acscmd.Connection = acsconn
                                    acscmd.ExecuteNonQuery()

                                    Try
                                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                    Catch ex As Exception
                                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                    End Try
                                    stracs = "UPDATE BillCharges set IsCollectionCreated = 'Yes', CRNo = '" & crno.Text & "' WHERE BillChargesID =" & dgvotherchages.Rows(m).Cells(2).Value
                                    acscmd.CommandText = stracs
                                    acscmd.Connection = acsconn
                                    acscmd.ExecuteNonQuery()

                                Next

                                If dgvothers.Rows.Count = 0 Then

                                Else
                                    For y = 0 To dgvothers.Rows.Count - 1
                                        If dgvothers.Rows(y).Cells(0).Value = True Then

                                            Dim otherbills As New DataTable
                                            otherbills.Clear()
                                            Try
                                                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                            Catch ex As Exception
                                                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                            End Try
                                            stracs = "select * FROM AddAdjustment WHERE ID = " & dgvothers.Rows(y).Cells(4).Value & ""
                                            acscmd.Connection = acsconn
                                            acscmd.CommandText = stracs
                                            acsda.SelectCommand = acscmd
                                            acsda.Fill(otherbills)


                                            'collection billing
                                            Try
                                                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                            Catch ex As Exception
                                                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                            End Try
                                            stracs = "INSERT INTO CollectionBilling (CRNo,AccountNo,AccountName,Address,Zone,BillingDate,PaymentDate,BillType,BillNo,AmountDue,Penalty,CollectionBillingStatus)
                        Values ('" & crno.Text & "','" & billAccountNo.Text & "','" & billName.Text.ToString.Replace("'", "''") & "','" & billAddress.Text.ToString.Replace("'", "''") & "',
                        '" & billZone.Text & "','" & otherbills.Rows(0)("BillingDate") & "','" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss tt") & "', 'Others', '" & dgvothers.Rows(y).Cells(4).Value & "','" & Double.Parse(otherbills.Rows(0)("Billing")) & "','" & Double.Parse(otherbills.Rows(0)("Penalty")) & "','Pending')"
                                            acscmd.CommandText = stracs
                                            acscmd.Connection = acsconn
                                            acscmd.ExecuteNonQuery()


                                            Try
                                                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                            Catch ex As Exception
                                                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                            End Try
                                            stracs = "UPDATE AddAdjustment set IsCollectionCreated = 'Yes', CRNo = '" & crno.Text & "' WHERE ID = " & dgvothers.Rows(y).Cells(4).Value
                                            acscmd.CommandText = stracs
                                            acscmd.Connection = acsconn
                                            acscmd.ExecuteNonQuery()

                                        End If
                                    Next
                                End If

                                Calculator.dgvcalc.Rows.Add(billAccountNo.Text, crno.Text, FormatNumber(amountpaid.Text))
                                Calculator.calculate()


                                '''''''''''''''''''''''' Bills and billcharges CR Number''''''''''''''''''''''''''''''''''''
                                'If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                'stracs = "Update Bills set IsPaid = 'Yes', CR_No = '" & crno.Text & "' WHERE BillNo = " & billBillno.Text
                                'acscmd.CommandText = stracs
                                'acscmd.Connection = acsconn
                                'acscmd.ExecuteNonQuery()


                                'If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                'stracs = "Update BillCharges set IsPaid = 'Yes', CR_No = '" & crno.Text & "' WHERE BillNumber = " & billBillno.Text
                                'acscmd.CommandText = stracs
                                'acscmd.Connection = acsconn
                                'acscmd.ExecuteNonQuery()



                                'For x = 0 To billarrears.Rows.Count - 1

                                '    If billarrears.Rows(x).Cells(0).Value = True Then
                                '        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                '        stracs = "Update Bills set IsPaid = 'Yes', CR_No = '" & crno.Text & "' WHERE BillNo = " & billarrears.Rows(x).Cells(1).Value
                                '        acscmd.CommandText = stracs
                                '        acscmd.Connection = acsconn
                                '        acscmd.ExecuteNonQuery()

                                '        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                '        stracs = "Update BillCharges set IsPaid = 'Yes', CR_No = '" & crno.Text & "' WHERE BillNumber = " & billarrears.Rows(x).Cells(1).Value
                                '        acscmd.CommandText = stracs
                                '        acscmd.Connection = acsconn
                                '        acscmd.ExecuteNonQuery()
                                '    End If
                                'Next


                                ''''''''''''''''''''''''end of Bills and billcharges CR Number''''''''''''''''''''''''''''''''''''

                                '''''''''''''''''''''''' New Table ''''''''''''''''''''''''''''''''''''

                                Try
                                    If autoprint.CheckState = CheckState.Checked Then

                                        printcrdocs.PrinterSettings.PrinterName = My.Settings.printerORCR
                                        printcrdocs.Print()

                                    Else

                                    End If


                                    'orderPreview.Document = printOrder
                                    'orderPreview.ShowDialog()
                                Catch ex As Exception
                                    MsgBox(ex.Message)
                                End Try

                                My.Settings.orfrom = My.Settings.orfrom + 1
                                My.Settings.Save()

                                getcrnumber()



                                clearallfields()
                                billsave.Hide()
                                billsave.Enabled = False

                                billAccountNo.Select()
                                billAccountNo.SelectAll()

                            Else
                                MsgBox("Posible duplicate of entry.")
                            End If

                        Else

                            MsgBox("Duplicated CR Number. Please contact Admin.")

                        End If

                    End If
                    Cursor = Cursors.Default
                End If

            Else
                MsgBox("Your account cannot perform this process.")
            End If

        End If

    End Sub

    Private Sub Collection_CR_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.MdiParent = eBACSmain
        clearallfields()
        getcrnumber()
        rbcash.Checked = True
        autoprint.CheckState = CheckState.Checked

        cashcheck.Location = New Point(5, 375)
        paymentgroup.Location = New Point(211, 375)

        Panel1.Size = New Size(512, 512)
        Me.Size = New Size(515, 556)

        billsave.Visible = False
        billsave.Enabled = False
        lblStatus.ContextMenuStrip = cmsCR
        billAccountNo.Select()


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

        billAccountNo.AutoCompleteCustomSource.Clear()

        billAccountNo.AutoCompleteMode = AutoCompleteMode.None
        billAccountNo.AutoCompleteSource = AutoCompleteSource.None

        For x = 0 To autocomplete.Rows.Count - 1
            billAccountNo.AutoCompleteCustomSource.Add(autocomplete.Rows(x)("AccountNo"))
        Next


        billAccountNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        billAccountNo.AutoCompleteSource = AutoCompleteSource.CustomSource

    End Sub

    Public Sub crno_KeyDown(sender As Object, e As KeyEventArgs) Handles crno.KeyDown

        If e.KeyCode = Keys.Enter Then

            Dim crnumberdt, maxbill As New DataTable

            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try

            crnumberdt.Clear()
            'stracs = "select * from CollectionBilling where CRNo = '" & crno.Text & "'"
            stracs = "select a.AccountNo,a.AccountName,a.Address,b.Zone,a.Cancelled as Cancelled,a.CollectionStatus as CollectionStatus from Collection_Details a join Customers b on a.AccountNo = b.AccountNo where CRNo = '" & crno.Text & "'"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(crnumberdt)

            If crnumberdt.Rows.Count = 0 Then
                MsgBox("No data Found")
                clearallfields()
                billsave.Hide()
                billsave.Enabled = False
                btnDelete.Hide()
            Else

                billsave.Visible = False
                billsave.Enabled = False

                'billAccountNo.Text = crnumberdt(0)("AccountNo")
                'billName.Text = crnumberdt(0)("AccountName")
                'billAddress.Text = crnumberdt(0)("Address")
                ''billbillingmonth.Text = crnumberdt(0)("BillingDate")

                billAccountNo.Text = crnumberdt(0)("AccountNo")
                billName.Text = crnumberdt(0)("AccountName")
                billAddress.Text = crnumberdt(0)("Address")
                'billbillingmonth.Text = crnumberdt(0)("BillingDate")

                If IsDBNull(crnumberdt(0)("Zone")) = True Then
                    billZone.Text = ""
                Else
                    billZone.Text = crnumberdt(0)("Zone")
                End If

                If crnumberdt.Rows(0)("Cancelled") = "Yes" Then
                    lblStatus.ForeColor = Color.Red
                    lblStatus.Text = "Cancelled"
                    btnDelete.Hide()
                Else
                    If crnumberdt(0)("CollectionStatus") = "Pending" Then
                        lblStatus.ForeColor = Color.Orange
                    End If

                    If crnumberdt(0)("CollectionStatus") = "Posted" Then
                        lblStatus.ForeColor = Color.Green
                    End If
                    lblStatus.Text = crnumberdt(0)("CollectionStatus")
                    btnDelete.Show()
                End If

                lblStatus.Show()


                maxbill.Clear()
                'stracs = "select MAX(BillNo) as lastbill from CollectionBilling where AccountNo = '" & crnumberdt.Rows(0)("AccountNo") & "' AND CRNo = '" & crno.Text & "' and Billtype = 'Bill'"
                'stracs = "select MAX(BillNo) as lastbill from Bills where AccountNo = '" & crnumberdt.Rows(0)("AccountNo") & "' AND CRNo = '" & crno.Text & "'"
                stracs = "select BillNo as lastbill from CollectionBilling where AccountNo = '" & crnumberdt.Rows(0)("AccountNo") & "' AND CRNo = '" & crno.Text & "' and Billtype = 'BillCurrent'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(maxbill)

                If maxbill.Rows.Count = 0 Then

                Else
                    CRBillno.Text = maxbill.Rows(0)("lastbill")
                End If

                Dim billdata As New DataTable
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                If IsNumeric(CRBillno.Text) = True Then

                    currentBilling.CheckState = CheckState.Checked

                    billdata.Clear()
                    stracs = "select * from CollectionBilling where BillNo = " & CRBillno.Text & " and CRno = '" & crno.Text & "'"
                    'stracs = "select * from Bills where BillNo = " & CRBillno.Text
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsda.SelectCommand = acscmd
                    acsda.Fill(billdata)

                    If billdata.Rows.Count = 0 Then
                        'MsgBox("No record found.")

                        billadvancepayment.Text = "0.00"
                        billAdjustment.Text = "0.00"
                        billamountdue.Text = "0.00"
                        billarrears.Rows.Clear()
                        billcharges.Rows.Clear()
                        billdiscount.Text = "0.00"
                        billEarlyDisc.Text = "0.00"
                        billPenalty.Text = "0.00"
                        billTotalamountdue.Text = "0.00"
                        billcheckno.Clear()
                        billcheckdate.Clear()
                        billbillingmonth.Clear()

                    Else


                        billAccountNo.Text = billdata(0)("AccountNo")
                        billName.Text = billdata(0)("AccountName")
                        billAddress.Text = billdata(0)("Address")
                        billbillingmonth.Text = billdata(0)("BillingDate")

                        If IsDBNull(billdata(0)("Zone")) = True Then
                            billZone.Text = ""
                        Else
                            billZone.Text = billdata(0)("Zone")
                        End If

                        billamountdue.Text = Format(billdata(0)("AmountDue"), "standard")
                        billdiscount.Text = Format(billdata(0)("Discount"), "standard")
                            billEarlyDisc.Text = Format(billdata(0)("earlyPaymentDiscount"), "standard")
                            billPenalty.Text = Format(billdata(0)("Penalty"), "standard")
                            billadvancepayment.Text = Format(billdata(0)("AdvancePayment"), "standard")

                            If IsDBNull(billdata(0)("Adjustment")) = True Then
                                billAdjustment.Text = "0.00"
                            Else
                                billAdjustment.Text = Format(billdata(0)("Adjustment"), "standard")
                            End If

                            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                            sqldataBilling.Clear()
                            stracs = "select * from CollectionCharges where BillNo = " & CRBillno.Text & " and CRNo = '" & crno.Text & "'"
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acsda.SelectCommand = acscmd
                            acsda.Fill(sqldataBilling)

                            billcharges.Rows.Clear()

                            For i = 0 To sqldataBilling.Rows.Count - 1

                                billcharges.Rows.Add(sqldataBilling(i)("Particulars"), Format(sqldataBilling(i)("Amount"), "standard"))
                                'totalcharges = totalcharges + Format(sqldataBilling(i)("Amount"), "standard")

                            Next

                        End If
                    End If

                Dim getarrears As New DataTable
                getarrears.Clear()
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                'stracs = "select BillNo from CollectionBilling where BillNo < " & CRBillno.Text & " AND CRNo = '" & crno.Text & "'"
                'stracs = "select BillNo from CollectionBilling where BillNo < " & CRBillno.Text & " and BillType = 'Bill' AND CRNo = '" & crno.Text & "'"
                stracs = "select BillNo from CollectionBilling where BillType = 'Bill' AND CRNo = '" & crno.Text & "'"
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsda.SelectCommand = acscmd
                acsda.Fill(getarrears)

                billarrears.Rows.Clear()

                For t = 0 To getarrears.Rows.Count - 1

                    Dim loadbillarrears As New DataTable
                    loadbillarrears.Clear()
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    stracs = "select * from CollectionBilling where BillNo = " & getarrears.Rows(t)("BillNo")
                    acscmd.Connection = acsconn
                    acscmd.CommandText = stracs
                    acsda.SelectCommand = acscmd
                    acsda.Fill(loadbillarrears)

                    Dim loadchargesarrears As New DataTable
                    loadchargesarrears.Clear()
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    stracs = "select SUM(Amount) as Amount, Particulars from CollectionCharges WHERE BillNo = " & getarrears.Rows(t)("BillNo") & "group by Particulars"
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

                    If IsDBNull(loadbillarrears.Rows(0)("Adjustment")) = True Then

                        loadbillarrears.Rows(0)("Adjustment") = 0
                    Else
                        loadbillarrears.Rows(0)("Adjustment") = loadbillarrears.Rows(0)("Adjustment")
                    End If


                    billarrears.Rows.Add(True, loadbillarrears.Rows(0)("BillNo"), loadbillarrears.Rows(0)("BillingDate"),
                                        Format(Val((loadbillarrears.Rows(0)("AmountDue")) + Val(arrearscharge) + Val(loadbillarrears.Rows(0)("Penalty")) + Val(loadbillarrears.Rows(0)("Adjustment"))) - (Val(loadbillarrears.Rows(0)("Discount")) + Val(loadbillarrears.Rows(0)("AdvancePayment"))), "standard"),
                                        Format(Val(loadbillarrears.Rows(0)("AmountDue") + Val(loadbillarrears.Rows(0)("Penalty"))) - (Val(loadbillarrears.Rows(0)("Discount")) + Val(loadbillarrears.Rows(0)("AdvancePayment"))), "standard"), chargepart, Val(arrearscharge))

                Next

                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                sqlData1.Clear()
                stracs = "select CheckNo,CheckDate,TotalAmountDue,AdvancePayment FROM Collection_Details where CRNo = '" & crno.Text & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(sqlData1)

                If sqlData1.Rows.Count = 0 Then

                Else
                    billcheckno.Text = sqlData1.Rows(0)("CheckNo")
                    billcheckdate.Text = sqlData1.Rows(0)("CheckDate")
                    billTotalamountdue.Text = Format(sqlData1.Rows(0)("TotalAmountDue") - sqlData1.Rows(0)("AdvancePayment"), "Standard")
                    amountpaid.Text = Format(sqlData1.Rows(0)("TotalAmountDue"), "standard")
                End If

                Dim getothers As New DataTable
                stracs = "select a.BillNo as BillNo, a.AmountDue as AmountDue, a.Penalty as Penalty, b.Particulars as Particulars, b.RefNo as RefNo, b.ID as ID from CollectionBilling a join AddAdjustment b on a.BillNo = b.ID where a.CRNo = '" & crno.Text & "' and b.CRNo = '" & crno.Text & "' and a.BillType = 'Others'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(getothers)
                dgvothers.Rows.Clear()

                For x = 0 To getothers.Rows.Count - 1

                    dgvothers.Rows.Add(True, getothers.Rows(x)("RefNo"), getothers.Rows(x)("Particulars"), Format(Val(getothers.Rows(x)("AmountDue")) + Val(getothers.Rows(x)("Penalty")), "standard"), getothers.Rows(x)("ID"))

                Next

                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                sqlData1.Clear()

                stracs = "select A.ChargeID AS ChargeID,a.Particulars as Particulars,a.Amount as Amount 
                        FROM CollectionCharges a join BillCharges b on a.BillNo = b.BillNumber WHERE a.CRNo = '" & crno.Text & "' and b.isPromisorry = 'YesPosted'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(sqlData1)
                dgvotherchages.Rows.Clear()

                If sqlData1.Rows.Count = 0 Then

                Else

                    For t = 0 To sqlData1.Rows.Count - 1
                        dgvotherchages.Rows.Add(sqlData1.Rows(t)("Particulars"), sqlData1.Rows(t)("Amount"), sqlData1.Rows(t)("ChargeID"))
                    Next

                End If

                If billarrears.Rows.Count = 0 Then

                    If dgvothers.Rows.Count > 0 Then

                        grouparrears.Hide()
                        dgvothers.Show()
                        dgvotherchages.Show()

                        dgvothers.Location = New Point(8, 375)
                        dgvotherchages.Location = New Point(267, 375)

                        cashcheck.Location = New Point(8, 483)
                        paymentgroup.Location = New Point(214, 483)

                        Panel1.Size = New Size(512, 620)
                        Me.Size = New Size(515, 662)

                    Else

                        grouparrears.Hide()
                        dgvothers.Hide()
                        dgvotherchages.Hide()

                        cashcheck.Location = New Point(7, 375)
                        paymentgroup.Location = New Point(213, 375)

                        Panel1.Size = New Size(512, 507)
                        Me.Size = New Size(515, 551)

                    End If

                Else

                    If dgvothers.Rows.Count > 0 Then

                        grouparrears.Show()
                        dgvothers.Show()
                        dgvotherchages.Show()

                        grouparrears.Location = New Point(8, 375)

                        dgvothers.Location = New Point(9, 486)
                        dgvotherchages.Location = New Point(268, 486)

                        cashcheck.Location = New Point(8, 594)
                        paymentgroup.Location = New Point(214, 594)

                        Panel1.Size = New Size(512, 737)
                        Me.Size = New Size(515, 777)

                    Else

                        grouparrears.Show()
                        dgvothers.Hide()
                        dgvotherchages.Hide()

                        grouparrears.Location = New Point(8, 375)

                        Panel1.Size = New Size(512, 621)
                        Me.Size = New Size(515, 660)

                        cashcheck.Location = New Point(5, 486)
                        paymentgroup.Location = New Point(211, 486)

                    End If

                End If

                billcheckno.ReadOnly = True
                billcheckdate.ReadOnly = True
                billsave.Hide()
                billsave.Enabled = False
                reprint.Show()

            End If
        End If

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

    Private Sub rbcash_CheckedChanged(sender As Object, e As EventArgs) Handles rbcash.CheckedChanged
        If rbcash.Checked = True Then
            Label8.Enabled = False
            Label6.Enabled = False
            billcheckno.Clear()
            billcheckdate.Clear()
            billcheckno.Enabled = False
            billcheckdate.Enabled = False
        End If
    End Sub

    Private Sub rbcheck_CheckedChanged(sender As Object, e As EventArgs) Handles rbcheck.CheckedChanged
        If rbcheck.Checked = True Then
            Label8.Enabled = True
            Label6.Enabled = True
            billcheckno.Clear()
            billcheckdate.Clear()
            billcheckno.Enabled = True
            billcheckdate.Enabled = True
        End If
    End Sub

    Private Sub rbOnline_CheckedChanged(sender As Object, e As EventArgs) Handles rbOnline.CheckedChanged

        If rbOnline.Checked = True Then
            Label8.Enabled = True
            Label6.Enabled = True
            billcheckno.Clear()
            billcheckdate.Clear()
            billcheckno.Enabled = True
            billcheckdate.Enabled = True
        End If

    End Sub

    Private Sub amountpaid_TextChanged(sender As Object, e As EventArgs) Handles amountpaid.TextChanged

        If amountpaid.Text = "" Or amountpaid.Text = "0.00" Then

            billsave.Hide()
            billsave.Enabled = False

        Else

            overpayment.Text = FormatNumber(Double.Parse(amountpaid.Text) - Double.Parse(billTotalamountdue.Text))

            If overpayment.Text < 0 Then

                billsave.Hide()
                billsave.Enabled = False

            Else

                billsave.Show()
                billsave.Enabled = True

            End If

        End If


    End Sub

    Private Sub amountpaid_KeyPress(sender As Object, e As KeyPressEventArgs) Handles amountpaid.KeyPress
        If e.KeyChar <> ControlChars.Back Then
            e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = ".")
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles reprint.Click
        reprint.Hide()
        reprintcr = "Yes"
        Try
            printcrdocs.PrinterSettings.PrinterName = My.Settings.printerORCR
            printcrdocs.Print()
            'orderPreview.Document = printOrder
            'orderPreview.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

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

    Private Sub printcrdocs_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles printcrdocs.PrintPage

        Dim headFont As New Font("Century Gothic", 10, FontStyle.Bold, GraphicsUnit.Point)
        Dim footFont As New Font("Century Gothic", 7, GraphicsUnit.Point)
        Dim headsubFont As New Font("Century Gothic", 8, GraphicsUnit.Point)
        Dim headsubFontbold As New Font("Century Gothic", 8, FontStyle.Bold, GraphicsUnit.Point)
        Dim headsubFontitalic As New Font("Century Gothic", 8, FontStyle.Italic, GraphicsUnit.Point)
        Dim locationv As Integer = 140

        Dim MidCenterhead As StringFormat = New StringFormat()
        Dim MidLeft As StringFormat = New StringFormat()
        Dim MidRight As StringFormat = New StringFormat()
        Dim lefttop As StringFormat = New StringFormat()


        MidCenterhead.LineAlignment = StringAlignment.Near
        MidCenterhead.Alignment = StringAlignment.Center

        MidLeft.LineAlignment = StringAlignment.Center
        MidLeft.Alignment = StringAlignment.Near

        MidRight.LineAlignment = StringAlignment.Center
        MidRight.Alignment = StringAlignment.Far

        lefttop.LineAlignment = StringAlignment.Near
        lefttop.Alignment = StringAlignment.Near

        'Dim CurX As Integer = 50
        'Dim CurY As Integer = 50
        'Dim iWidth As Integer = 250

        'Dim cellRecthead As RectangleF
        'cellRecthead = New RectangleF()
        'cellRecthead.Location = New Point(0, 0)
        'cellRecthead.Size = New Size(250, 100)

        'Dim CurX As Integer = 0
        'Dim CurY As Integer = 0
        'Dim iWidth As Integer = 250

        'CurY = PrintCellText("Pantabangan Municipal Water System", CurX, CurY, iWidth, e, headFont, MidCenterhead)
        'CurY = PrintCellText("Brgy. East Poblacion", CurX, CurY, iWidth, e, headsubFont, MidCenterhead)
        'CurY = PrintCellText("Pantabangan, Nueva Ecija", CurX, CurY, iWidth, e, headsubFont, MidCenterhead)
        'CurY = PrintCellText("CP. No. 0908 8145 758", CurX, CurY, iWidth, e, headsubFont, MidCenterhead)
        'CurY = PrintCellText("TIN: 004-104-990-000 NON-VAT", CurX, CurY, iWidth, e, headsubFont, MidCenterhead)

        'e.Graphics.DrawString("SANTA ROSA (NE) WATER DISTRICT", headFont, Brushes.Black, cellRecthead, MidCenterhead)
        'e.Graphics.DrawString(vbCrLf & "Santa Rosa -  Fort Magsaysay Road", headsubFont, Brushes.Black, cellRecthead, MidCenterhead)
        'e.Graphics.DrawString(vbCrLf & vbCrLf & "Santa Rosa, Nueva Ecija", headsubFont, Brushes.Black, cellRecthead, MidCenterhead)
        'e.Graphics.DrawString(vbCrLf & vbCrLf & vbCrLf & "Tel. No. (044) 940-6800", headsubFont, Brushes.Black, cellRecthead, MidCenterhead)
        'e.Graphics.DrawString(vbCrLf & vbCrLf & vbCrLf & vbCrLf & "TIN: 004-104-990-000 NON-VAT", headsubFont, Brushes.Black, cellRecthead, MidCenterhead)

        'e.Graphics.DrawString("SANTA ROSA (NE) WATER DISTRICT", headFont, Brushes.Black, 20, 10)
        'e.Graphics.DrawString("Santa Rosa - Fort Magsaysay Road", headsubFont, Brushes.Black, 35, 30)
        'e.Graphics.DrawString("Santa Rosa, Nueva Ecija", headsubFont, Brushes.Black, 60, 45)
        'e.Graphics.DrawString("TIN: 004-104-990-000 NON-VAT", headsubFont, Brushes.Black, 48, 60)

        'e.Graphics.DrawString("OFFICIAL RECEIPT", headFont, Brushes.Black, 56, 80)

        'e.Graphics.DrawString("CR No.:", headsubFont, Brushes.Black, 0, 110)
        'e.Graphics.DrawString(crno.Text, headsubFont, Brushes.Black, 60, 110)
        'e.Graphics.DrawString("Acc. No.:", headsubFont, Brushes.Black, 0, 125)
        'e.Graphics.DrawString(billAccountNo.Text, headsubFont, Brushes.Black, 60, 125)

        'e.Graphics.DrawString("Name:", headsubFont, Brushes.Black, 0, 140)

        If reprintcr = "Yes" Then

            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Dim datetimecashier As New DataTable
            stracs = "select Collector, PaymentDate FROM Collection_Details WHERE CRNo = '" & crno.Text & "'"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(datetimecashier)

            Dim cellRectCashier As RectangleF
            cellRectCashier = New RectangleF()
            cellRectCashier.Location = New Point(230, 735)
            cellRectCashier.Size = New Size(250, 14)

            e.Graphics.DrawString(datetimecashier.Rows(0)("Collector"), headsubFont, Brushes.Black, cellRectCashier, MidLeft)

            'e.Graphics.DrawString(datetimecashier.Rows(0)("Collector"), headsubFont, Brushes.Black, 200, 700)

            e.Graphics.DrawString(Format(datetimecashier.Rows(0)("PaymentDate"), "MM/dd/yyyy hh:mm tt"), headsubFont, Brushes.Black, 30, 175)

        End If

        If reprintcr = "No" Then

            Dim cellRectCashier As RectangleF
            cellRectCashier = New RectangleF()
            cellRectCashier.Location = New Point(230, 735)
            cellRectCashier.Size = New Size(250, 14)

            e.Graphics.DrawString(My.Settings.Nickname, headsubFont, Brushes.Black, cellRectCashier, MidLeft)

            'e.Graphics.DrawString(My.Settings.Nickname, headsubFont, Brushes.Black, 200, 700)

            e.Graphics.DrawString(Format(Now, "MM/dd/yyyy hh:mm tt"), headsubFont, Brushes.Black, 30, 175)

        End If

        e.Graphics.DrawString(crno.Text, headsubFont, Brushes.Black, 250, 180)

        e.Graphics.DrawString(billName.Text, headsubFont, Brushes.Black, 60, 230)
        e.Graphics.DrawString(billAccountNo.Text, headsubFont, Brushes.Black, 60, 245)
        e.Graphics.DrawString(billZone.Text, headsubFont, Brushes.Black, 60, 260)

        'start pangalan

        'Dim pangalanrec As RectangleF
        'pangalanrec = New RectangleF()

        'pangalanrec.Size = New Size(180, pangalanrec.Height)

        'Dim TotalStringHeight As Single = e.Graphics.MeasureString(billName.Text, headsubFont, New SizeF(pangalanrec.Width, pangalanrec.Height), lefttop).Height
        'Dim SingleLineHeight As Single = e.Graphics.MeasureString("T", headsubFont, New SizeF(pangalanrec.Width, pangalanrec.Height), lefttop).Height

        'Dim NumberOfLines As Integer = Convert.ToInt32(TotalStringHeight / SingleLineHeight)

        'pangalanrec.Location = New Point(60, locationv + 1)

        'e.Graphics.DrawString(billName.Text, headsubFont, Brushes.Black, pangalanrec, lefttop)

        'locationv = locationv + (NumberOfLines * 15)

        'end pangalan

        ''If billName.TextLength < 25 Then

        ''    e.Graphics.DrawString(billName.Text.Substring(0, billName.TextLength), headsubFont, Brushes.Black, 60, 140)
        ''    locationv = locationv + 15
        ''Else

        ''    If billName.TextLength >= 25 And billName.TextLength <= 49 Then

        ''        e.Graphics.DrawString(billName.Text.Substring(0, 25).ToUpper, headsubFont, Brushes.Black, 60, 140)
        ''        e.Graphics.DrawString(billName.Text.Substring(25, billName.TextLength - 25).ToUpper, headsubFont, Brushes.Black, 60, 155)
        ''        locationv = locationv + 30
        ''    End If

        ''    If billName.TextLength >= 50 And billName.TextLength <= 74 Then

        ''        e.Graphics.DrawString(billName.Text.Substring(0, 25).ToUpper, headsubFont, Brushes.Black, 60, 140)
        ''        e.Graphics.DrawString(billName.Text.Substring(25, 25).ToUpper, headsubFont, Brushes.Black, 60, 155)
        ''        e.Graphics.DrawString(billName.Text.Substring(50, billName.TextLength - 50).ToUpper, headsubFont, Brushes.Black, 60, 170)
        ''        locationv = locationv + 45
        ''    End If

        ''    If billName.TextLength >= 75 And billName.TextLength <= 99 Then

        ''        e.Graphics.DrawString(billName.Text.Substring(0, 25).ToUpper, headsubFont, Brushes.Black, 60, 140)
        ''        e.Graphics.DrawString(billName.Text.Substring(25, 25).ToUpper, headsubFont, Brushes.Black, 60, 155)
        ''        e.Graphics.DrawString(billName.Text.Substring(50, 25).ToUpper, headsubFont, Brushes.Black, 60, 170)
        ''        e.Graphics.DrawString(billName.Text.Substring(75, billName.TextLength - 75).ToUpper, headsubFont, Brushes.Black, 60, 185)
        ''        locationv = locationv + 60
        ''    End If

        ''    If billName.TextLength >= 100 And billName.TextLength <= 124 Then

        ''        e.Graphics.DrawString(billName.Text.Substring(0, 25).ToUpper, headsubFont, Brushes.Black, 60, 140)
        ''        e.Graphics.DrawString(billName.Text.Substring(25, 25).ToUpper, headsubFont, Brushes.Black, 60, 155)
        ''        e.Graphics.DrawString(billName.Text.Substring(50, 25).ToUpper, headsubFont, Brushes.Black, 60, 170)
        ''        e.Graphics.DrawString(billName.Text.Substring(75, 25).ToUpper, headsubFont, Brushes.Black, 60, 185)
        ''        e.Graphics.DrawString(billName.Text.Substring(100, billName.TextLength - 100).ToUpper, headsubFont, Brushes.Black, 60, 200)
        ''        locationv = locationv + 75
        ''    End If

        ''End If
        'e.Graphics.DrawString("Address:", headsubFont, Brushes.Black, 0, locationv)

        'Dim addrec As RectangleF
        'addrec = New RectangleF()

        'addrec.Size = New Size(180, addrec.Height)

        'Dim TotalStringHeightaddress As Single = e.Graphics.MeasureString(billAddress.Text, headsubFont, New SizeF(addrec.Width, addrec.Height), lefttop).Height
        'Dim SingleLineHeightaddress As Single = e.Graphics.MeasureString("T", headsubFont, New SizeF(addrec.Width, addrec.Height), lefttop).Height

        'Dim NumberOfLinesaddress As Integer = Convert.ToInt32(TotalStringHeightaddress / SingleLineHeightaddress)

        'addrec.Location = New Point(60, locationv + 1)

        'e.Graphics.DrawString(billAddress.Text, headsubFont, Brushes.Black, addrec, lefttop)

        'locationv = locationv + (NumberOfLinesaddress * 15)


        'If billAddress.TextLength < 25 Then

        '    e.Graphics.DrawString(billAddress.Text.Substring(0, billAddress.TextLength), headsubFont, Brushes.Black, 60, locationv)
        '    locationv = locationv + 15
        'Else

        '    If billAddress.TextLength >= 25 And billAddress.TextLength <= 49 Then

        '        e.Graphics.DrawString(billAddress.Text.Substring(0, 25).ToUpper, headsubFont, Brushes.Black, 60, locationv)
        '        e.Graphics.DrawString(billAddress.Text.Substring(25, billAddress.TextLength - 25).ToUpper, headsubFont, Brushes.Black, 60, locationv + 15)
        '        locationv = locationv + 30
        '    End If

        '    If billAddress.TextLength >= 50 And billAddress.TextLength <= 74 Then

        '        e.Graphics.DrawString(billAddress.Text.Substring(0, 25).ToUpper, headsubFont, Brushes.Black, 60, locationv)
        '        e.Graphics.DrawString(billAddress.Text.Substring(25, 25).ToUpper, headsubFont, Brushes.Black, 60, locationv + 15)
        '        e.Graphics.DrawString(billAddress.Text.Substring(50, billAddress.TextLength - 50).ToUpper, headsubFont, Brushes.Black, 60, locationv + 30)
        '        locationv = locationv + 45
        '    End If

        '    If billAddress.TextLength >= 75 And billAddress.TextLength <= 99 Then

        '        e.Graphics.DrawString(billAddress.Text.Substring(0, 25).ToUpper, headsubFont, Brushes.Black, 60, locationv)
        '        e.Graphics.DrawString(billAddress.Text.Substring(25, 25).ToUpper, headsubFont, Brushes.Black, 60, locationv + 15)
        '        e.Graphics.DrawString(billAddress.Text.Substring(50, 25).ToUpper, headsubFont, Brushes.Black, 60, locationv + 30)
        '        e.Graphics.DrawString(billAddress.Text.Substring(75, billAddress.TextLength - 75).ToUpper, headsubFont, Brushes.Black, 60, locationv + 45)
        '        locationv = locationv + 60
        '    End If

        '    If billAddress.TextLength >= 100 And billAddress.TextLength <= 124 Then

        '        e.Graphics.DrawString(billAddress.Text.Substring(0, 25).ToUpper, headsubFont, Brushes.Black, 60, locationv)
        '        e.Graphics.DrawString(billAddress.Text.Substring(25, 25).ToUpper, headsubFont, Brushes.Black, 60, locationv + 15)
        '        e.Graphics.DrawString(billAddress.Text.Substring(50, 25).ToUpper, headsubFont, Brushes.Black, 60, locationv + 30)
        '        e.Graphics.DrawString(billAddress.Text.Substring(75, 25).ToUpper, headsubFont, Brushes.Black, 60, locationv + 45)
        '        e.Graphics.DrawString(billAddress.Text.Substring(100, billAddress.TextLength - 100).ToUpper, headsubFont, Brushes.Black, 60, locationv + 60)
        '        locationv = locationv + 75
        '    End If

        'End If

        'e.Graphics.DrawString("__________________________________________________", headsubFont, Brushes.Black, 0, locationv)
        'locationv = locationv + 20

        'e.Graphics.DrawString("As Payment for ", headsubFont, Brushes.Black, 0, locationv)
        'e.Graphics.DrawString("Amount", headsubFont, Brushes.Black, 190, locationv)

        locationv = 325

        If CRBillno.Text <> "" And currentBilling.CheckState = CheckState.Checked Then

            'e.Graphics.DrawString("Bill No. " & CRBillno.Text, headsubFont, Brushes.Black, 90, locationv)

            If Double.Parse(billamountdue.Text) <= 0 Then
            Else



                'locationv = locationv + 25

                Dim cellRectamount As RectangleF
                cellRectamount = New RectangleF()
                cellRectamount.Location = New Point(20, locationv)
                cellRectamount.Size = New Size(320, 12)

                e.Graphics.DrawString(billbillingmonth.Text, headsubFont, Brushes.Black, cellRectamount, MidLeft)
                e.Graphics.DrawString(billamountdue.Text, headsubFont, Brushes.Black, cellRectamount, MidRight)

                'old
                'e.Graphics.DrawString(billbillingmonth.Text, headsubFont, Brushes.Black, 0, locationv + 25)
                'e.Graphics.DrawString(billamountdue.Text, headsubFont, Brushes.Black, 225 - (billamountdue.Text.Length * 5), locationv + 25)

                If billPenalty.Text = "0.00" Then
                Else
                    locationv = locationv + 24

                    Dim cellRectpenalty As RectangleF
                    cellRectpenalty = New RectangleF()
                    cellRectpenalty.Location = New Point(20, locationv)
                    cellRectpenalty.Size = New Size(320, 12)

                    e.Graphics.DrawString("Penalty", headsubFont, Brushes.Black, cellRectpenalty, MidLeft)
                    e.Graphics.DrawString(billPenalty.Text, headsubFont, Brushes.Black, cellRectpenalty, MidRight)

                    'e.Graphics.DrawString("Penalty", headsubFont, Brushes.Black, 0, locationv)
                    'e.Graphics.DrawString(billPenalty.Text, headsubFont, Brushes.Black, 225 - (billPenalty.Text.Length * 5), locationv)

                End If

                If billdiscount.Text = "0.00" Then
                Else
                    locationv = locationv + 24

                    Dim cellRectdisc As RectangleF
                    cellRectdisc = New RectangleF()
                    cellRectdisc.Location = New Point(20, locationv)
                    cellRectdisc.Size = New Size(320, 12)

                    e.Graphics.DrawString("Senior Citizen Discount", headsubFont, Brushes.Black, cellRectdisc, MidLeft)
                    e.Graphics.DrawString("(" & billdiscount.Text & ")", headsubFont, Brushes.Black, cellRectdisc, MidRight)

                    'e.Graphics.DrawString("Discount", headsubFont, Brushes.Black, 0, locationv)
                    'e.Graphics.DrawString(billdiscount.Text, headsubFont, Brushes.Black, 225 - (billdiscount.Text.Length * 5), locationv)
                End If

                If billEarlyDisc.Text = "0.00" Or billEarlyDisc.Text = "0" Then
                Else
                    locationv = locationv + 24

                    Dim cellRectdisc As RectangleF
                    cellRectdisc = New RectangleF()
                    cellRectdisc.Location = New Point(20, locationv)
                    cellRectdisc.Size = New Size(320, 12)

                    e.Graphics.DrawString("Early Payment Discount", headsubFont, Brushes.Black, cellRectdisc, MidLeft)
                    e.Graphics.DrawString("(" & billEarlyDisc.Text & ")", headsubFont, Brushes.Black, cellRectdisc, MidRight)

                    'e.Graphics.DrawString("Discount", headsubFont, Brushes.Black, 0, locationv)
                    'e.Graphics.DrawString(billdiscount.Text, headsubFont, Brushes.Black, 225 - (billdiscount.Text.Length * 5), locationv)
                End If

                If billAdjustment.Text = "0.00" Then
                Else
                    locationv = locationv + 24

                    Dim cellRectdisc As RectangleF
                    cellRectdisc = New RectangleF()
                    cellRectdisc.Location = New Point(20, locationv)
                    cellRectdisc.Size = New Size(320, 12)

                    e.Graphics.DrawString("Adjustment", headsubFont, Brushes.Black, cellRectdisc, MidLeft)
                    e.Graphics.DrawString(billAdjustment.Text, headsubFont, Brushes.Black, cellRectdisc, MidRight)

                    'e.Graphics.DrawString("Discount", headsubFont, Brushes.Black, 0, locationv)
                    'e.Graphics.DrawString(billdiscount.Text, headsubFont, Brushes.Black, 225 - (billdiscount.Text.Length * 5), locationv)
                End If

                If billadvancepayment.Text = "0.00" Then
                Else
                    locationv = locationv + 24

                    Dim cellRectadvance As RectangleF
                    cellRectadvance = New RectangleF()
                    cellRectadvance.Location = New Point(20, locationv)
                    cellRectadvance.Size = New Size(320, 12)

                    e.Graphics.DrawString("Credit", headsubFont, Brushes.Black, cellRectadvance, MidLeft)
                    e.Graphics.DrawString("(" & billadvancepayment.Text & ")", headsubFont, Brushes.Black, cellRectadvance, MidRight)
                    'e.Graphics.DrawString("Credit", headsubFont, Brushes.Black, 0, locationv)
                    'e.Graphics.DrawString(billadvancepayment.Text, headsubFont, Brushes.Black, 225 - (billadvancepayment.Text.Length * 5), locationv)
                End If

            End If

            If billcharges.Rows.Count > 0 Then

                For v = 0 To billcharges.Rows.Count - 1

                    locationv = locationv + 24

                    Dim cellRectcharge As RectangleF
                    cellRectcharge = New RectangleF()
                    cellRectcharge.Location = New Point(20, locationv)
                    cellRectcharge.Size = New Size(320, 12)

                    e.Graphics.DrawString(billcharges.Rows(v).Cells(0).Value, headsubFont, Brushes.Black, cellRectcharge, MidLeft)
                    e.Graphics.DrawString(billcharges.Rows(v).Cells(1).Value, headsubFont, Brushes.Black, cellRectcharge, MidRight)

                    'e.Graphics.DrawString(billcharges.Rows(v).Cells(0).Value, headsubFont, Brushes.Black, 0, locationv)
                    'e.Graphics.DrawString(billcharges.Rows(v).Cells(1).Value, headsubFont, Brushes.Black, 225 - (billcharges.Rows(v).Cells(1).Value.Length * 5), locationv)

                Next

            End If

        Else

        End If

        If billarrears.Rows.Count > 0 Then

            locationv = locationv + 24
            e.Graphics.DrawString("Arrears:", headsubFont, Brushes.Black, 20, locationv)

            For v = 0 To billarrears.Rows.Count - 1

                If billarrears.Rows(v).Cells(0).Value = -1 Then

                    locationv = locationv + 24

                    Dim cellRectarrears As RectangleF
                    cellRectarrears = New RectangleF()
                    cellRectarrears.Location = New Point(20, locationv)
                    cellRectarrears.Size = New Size(320, 12)

                    e.Graphics.DrawString(billarrears.Rows(v).Cells(2).Value, headsubFont, Brushes.Black, cellRectarrears, MidLeft)
                    e.Graphics.DrawString(billarrears.Rows(v).Cells(3).Value, headsubFont, Brushes.Black, cellRectarrears, MidRight)

                End If

            Next

        End If

        If dgvothers.Rows.Count > 0 Then

            'locationv = locationv + 30
            e.Graphics.DrawString("Others", headsubFont, Brushes.Black, 20, locationv)

            For l = 0 To dgvothers.Rows.Count - 1

                If dgvothers.Rows(l).Cells(0).Value = -1 Then

                    locationv = locationv + 20
                    Dim cellRectothers As RectangleF
                    cellRectothers = New RectangleF()
                    cellRectothers.Location = New Point(20, locationv)
                    cellRectothers.Size = New Size(320, 12)

                    e.Graphics.DrawString(dgvothers.Rows(l).Cells(2).Value, headsubFont, Brushes.Black, cellRectothers, MidLeft)
                    e.Graphics.DrawString(Format(dgvothers.Rows(l).Cells(3).Value, "standard"), headsubFont, Brushes.Black, cellRectothers, MidRight)

                    'e.Graphics.DrawString(dgvothers.Rows(l).Cells(2).Value, headsubFont, Brushes.Black, 0, locationv)
                    'e.Graphics.DrawString(dgvothers.Rows(l).Cells(3).Value, headsubFont, Brushes.Black, 225 - (dgvothers.Rows(l).Cells(3).Value.Length * 5), locationv)
                End If
            Next

        End If

        If dgvotherchages.Rows.Count > 0 Then

            For l = 0 To dgvotherchages.Rows.Count - 1


                locationv = locationv + 20
                Dim cellRectotherscharges As RectangleF
                cellRectotherscharges = New RectangleF()
                cellRectotherscharges.Location = New Point(20, locationv)
                cellRectotherscharges.Size = New Size(320, 12)

                e.Graphics.DrawString(dgvotherchages.Rows(l).Cells(0).Value, headsubFont, Brushes.Black, cellRectotherscharges, MidLeft)
                e.Graphics.DrawString(dgvotherchages.Rows(l).Cells(1).Value, headsubFont, Brushes.Black, cellRectotherscharges, MidRight)
                'e.Graphics.DrawString(dgvotherchages.Rows(l).Cells(0).Value, headsubFont, Brushes.Black, 0, locationv)
                'e.Graphics.DrawString(dgvotherchages.Rows(l).Cells(1).Value, headsubFont, Brushes.Black, 225 - (dgvotherchages.Rows(l).Cells(1).Value.Length * 5), locationv)

            Next

        End If

        'locationv = locationv + 20

        'e.Graphics.DrawString("__________________________________________________", headsubFont, Brushes.Black, 0, locationv)
        'locationv = locationv + 20

        Dim cellRecttotal As RectangleF
        cellRecttotal = New RectangleF()
        cellRecttotal.Location = New Point(20, 520)
        cellRecttotal.Size = New Size(320, 12)

        'e.Graphics.DrawString("Total Amount Paid", headsubFont, Brushes.Black, cellRecttotal, MidLeft)
        e.Graphics.DrawString(amountpaid.Text, headsubFont, Brushes.Black, cellRecttotal, MidRight)

        'e.Graphics.DrawString("Total Amount Paid", headsubFontbold, Brushes.Black, 0, locationv)
        'e.Graphics.DrawString(amountpaid.Text, headsubFont, Brushes.Black, 225 - (amountpaid.Text.Length * 5), locationv)
        locationv = locationv + 20

        If overpayment.Text = 0 Then

        Else

            Dim cellRectadvancep As RectangleF
            cellRectadvancep = New RectangleF()
            cellRectadvancep.Location = New Point(20, locationv)
            cellRectadvancep.Size = New Size(320, 12)

            e.Graphics.DrawString("Advance Payment", headsubFontitalic, Brushes.Black, cellRectadvancep, MidLeft)
            e.Graphics.DrawString("(" & overpayment.Text & ")", headsubFontitalic, Brushes.Black, cellRectadvancep, MidRight)

            'e.Graphics.DrawString("Advance Payment", headsubFontitalic, Brushes.Black, 0, locationv)
            'e.Graphics.DrawString(overpayment.Text, headsubFontitalic, Brushes.Black, 225 - (overpayment.Text.Length * 5), locationv)
            locationv = locationv + 20

        End If

        'footFont

        'e.Graphics.DrawString("BIR CAS Permit No. 23B-CAS-0414-0001", footFont, Brushes.Black, 0, locationv)
        'locationv = locationv + 15

        Dim MidCenter As StringFormat = New StringFormat()
        MidCenter.LineAlignment = StringAlignment.Center
        MidCenter.Alignment = StringAlignment.Center

        'Dim CurX As Integer = 50
        'Dim CurY As Integer = 50
        'Dim iWidth As Integer = 250

        Dim cellRect As RectangleF
        cellRect = New RectangleF()
        cellRect.Location = New Point(0, locationv)
        cellRect.Size = New Size(250, 50)

        converttowords()

        Dim rectowords As RectangleF
        rectowords = New RectangleF()

        rectowords.Size = New Size(350, rectowords.Height)

        Dim TotalStringHeight As Single = e.Graphics.MeasureString(billName.Text, headsubFont, New SizeF(rectowords.Width, rectowords.Height), lefttop).Height
        Dim SingleLineHeight As Single = e.Graphics.MeasureString("T", headsubFont, New SizeF(rectowords.Width, rectowords.Height), lefttop).Height

        Dim NumberOfLines As Integer = Convert.ToInt32(TotalStringHeight / SingleLineHeight)

        rectowords.Location = New Point(10, 560)

        e.Graphics.DrawString(convertedamout, headsubFont, Brushes.Black, rectowords, lefttop)

        'e.Graphics.DrawString("Powered By. IoTee Solutions Inc." & vbCrLf & "THIS DOCUMENT IS NOT VALID" & vbCrLf & "SOURCE OF INPUT TAX", footFont, Brushes.Black, cellRect, MidCenter)


    End Sub

    Sub libolibo()

        thousands = Int(millionsthou / 1000)

        If thousands = 0 Then

            callhundred = Int(millionsthou - (thousands * 1000))

            hundred()

            millionthousanwords = hundredwords

        End If

        If thousands > 19 And thousands < 100 Then

            tenten = thousands \ 10
            tens()

            ones = thousands - ((thousands \ 10) * 10)
            onetonineteen()

            thousanword = tentenword + " " + wordones + " Thousand "

            callhundred = Int(millionsthou - (thousands * 1000))


            hundred()

            millionthousanwords = thousanword + " " + hundredwords

        End If

        If thousands > 99 And thousands < 1000 Then

            ones = thousands \ 100
            onetonineteen()

            hundredthouword = wordones + " Hundred "

            tenten = (thousands - (ones * 100)) \ 10

            If tenten < 2 Then

                ones = Int(thousands - (ones * 100))
                onetonineteen()

                hundredthouword = hundredthouword + wordones + " Thousand "

                callhundred = Int(millionsthou - (thousands * 1000))

                hundred()

                millionthousanwords = hundredthouword + hundredwords

            Else

                tens()

                ones = Int(thousands - ((ones * 100) + (tenten * 10)))
                onetonineteen()

                hundredthouword = hundredthouword + tentenword + " " + wordones + " Thousand "

                callhundred = Int(millionsthou - (thousands * 1000))

                hundred()

                millionthousanwords = hundredthouword + " " + hundredwords

            End If

        End If

        If thousands > 0 And thousands < 20 Then

            ones = thousands
            onetonineteen()

            thousanword = wordones

            hundreds = Int((millionsthou - (ones * 1000)) / 100)

            If hundreds = 0 Then

                tenten = Int((millionsthou - (thousands * 1000) + (hundreds * 100)) / 10)

                If tenten < 2 Then

                    ones = Int(millionsthou - ((thousands * 1000) + (hundreds * 100)))
                    onetonineteen()

                    gansal = Math.Round((number - Int(number)) * 100, 2)

                    If gansal = 0 Then
                        millionthousanwords = thousanword + " Thousand " + wordones + " pesos only"
                    Else
                        millionthousanwords = thousanword + " Thousand " + wordones + " and " + Format(Int(gansal), "00") + "/100 pesos only"
                    End If



                Else

                    tens()

                    ones = Int(millionsthou - ((thousands * 1000) + (hundreds * 100) + (tenten * 10)))
                    onetonineteen()

                    gansal = Math.Round((number - Int(number)) * 100, 2)

                    If gansal = 0 Then
                        millionthousanwords = thousanword + " Thousand " + tentenword + " " + wordones + " pesos only"
                    Else
                        millionthousanwords = thousanword + " Thousand " + tentenword + " " + wordones + " and " + Format(Int(gansal), "00") + "/100 pesos only"
                    End If


                End If

            Else

                ones = hundreds
                onetonineteen()

                hundredword = wordones

                tenten = Int((millionsthou - ((thousands * 1000) + (hundreds * 100))) / 10)

                If tenten < 2 Then

                    ones = Int(millionsthou - ((thousands * 1000) + (hundreds * 100)))
                    onetonineteen()

                    gansal = Math.Round((number - Int(number)) * 100, 2)

                    If gansal = 0 Then
                        millionthousanwords = thousanword + " Thousand " + hundredword + " Hundred " + wordones + " pesos only"
                    Else
                        millionthousanwords = thousanword + " Thousand " + hundredword + " Hundred " + wordones + " and " + Format(Int(gansal), "00") + "/100 pesos only"
                    End If


                Else

                    tens()

                    ones = Int(millionsthou - ((thousands * 1000) + (hundreds * 100) + (tenten * 10)))
                    onetonineteen()

                    gansal = Math.Round((number - Int(number)) * 100, 2)

                    If gansal = 0 Then
                        millionthousanwords = thousanword + " Thousand " + hundredword + " Hundred " + tentenword + " " + wordones + " pesos only"
                    Else
                        millionthousanwords = thousanword + " Thousand " + hundredword + " Hundred " + tentenword + " " + wordones + " and " + Format(Int(gansal), "00") + "/100 pesos only"
                    End If


                End If

            End If

        End If

    End Sub

    Sub hundred()

        ones = Int(callhundred / 100)
        onetonineteen()

        hundredword = wordones
        hundreds = ones

        tenten = Int((callhundred - (ones * 100)) / 10)

        If tenten < 2 Then

            ones = Int(callhundred - (ones * 100))
            onetonineteen()

            gansal = Math.Round((number - Int(number)) * 100, 2)

            If hundreds = 0 Then

                If gansal = 0 Then
                    hundredwords = wordones + " pesos only"
                Else
                    hundredwords = wordones + " and " + Format(Int(gansal), "00") + "/100 pesos only"
                End If

            Else

                If gansal = 0 Then
                    hundredwords = hundredword + " Hundred " + wordones + " pesos only"
                Else
                    hundredwords = hundredword + " Hundred " + wordones + " and " + Format(Int(gansal), "00") + "/100 pesos pesos only"
                End If

            End If

        Else

            tens()

            ones = Int(callhundred - ((hundreds * 100) + (tenten * 10)))
            onetonineteen()

            gansal = Math.Round((number - Int(number)) * 100, 2)

            If hundreds = 0 Then

                If gansal = 0 Then
                    hundredwords = tentenword + " " + wordones + " pesos only"
                Else
                    hundredwords = tentenword + " " + wordones + " and " + Format(Int(gansal), "00") + "/100 pesos only"
                End If

            Else
                If gansal = 0 Then
                    hundredwords = hundredword + " Hundred " + tentenword + " " + wordones + " pesos only"
                Else
                    hundredwords = hundredword + " Hundred " + tentenword + " " + wordones + " and " + Format(Int(gansal), "00") + "/100 pesos only"
                End If

            End If

        End If

    End Sub

    Sub onetonineteen()

        Select Case ones
            Case 0
                wordones = ""
            Case 1
                wordones = "One"
            Case 2
                wordones = "Two"
            Case 3
                wordones = "Three"
            Case 4
                wordones = "Four"
            Case 5
                wordones = "Five"
            Case 6
                wordones = "Six"
            Case 7
                wordones = "Seven"
            Case 8
                wordones = "Eight"
            Case 9
                wordones = "Nine"
            Case 10
                wordones = "Ten"
            Case 11
                wordones = "Eleven"
            Case 12
                wordones = "Twelve"
            Case 13
                wordones = "Thirteen"
            Case 14
                wordones = "Fourteen"
            Case 15
                wordones = "Fifteen"
            Case 16
                wordones = "Sixteen"
            Case 17
                wordones = "Seventeen"
            Case 18
                wordones = "Eighteen"
            Case 19
                wordones = "Nineteen"
        End Select

    End Sub

    Sub tens()

        Select Case tenten
            Case 2
                tentenword = "Twenty"
            Case 3
                tentenword = "Thirty"
            Case 4
                tentenword = "Forty"
            Case 5
                tentenword = "Fifty"
            Case 6
                tentenword = "Sixty"
            Case 7
                tentenword = "Seventy"
            Case 8
                tentenword = "Eighty"
            Case 9
                tentenword = "Ninety"
        End Select
    End Sub
    Private Sub converttowords()

        Try
            Dim number As Double
            number = Double.Parse(billTotalamountdue.Text)

            billTotalamountdue.Text = Format(number, "##,##0.00")

            If number > 0 And number < 20 Then

                ones = Int(number)
                onetonineteen()

                gansal = Math.Round((number - Int(number)) * 100, 2)

                If gansal = 0 Then
                    convertedamout = wordones + " pesos only"
                Else
                    convertedamout = wordones + " and " + Format(Int(gansal), "00") + "/100 pesos only"
                End If


            End If

            If number > 19.99 And number < 100 Then

                tenten = Int(number / 10)
                tens()

                ones = Int(number - (tenten * 10))
                onetonineteen()

                gansal = Math.Round((number - Int(number)) * 100, 2)

                If gansal = 0 Then
                    convertedamout = tentenword + " " + wordones + " pesos only"
                Else
                    convertedamout = tentenword + " " + wordones + " and " + Format(Int(gansal), "00") + "/100 pesos only"
                End If


            End If

            If number > 99.99 And number < 1000 Then

                ones = Int(number / 100)
                onetonineteen()

                hundredword = wordones
                hundreds = ones

                tenten = Int((number - (ones * 100)) / 10)

                If tenten < 2 Then

                    ones = Int(number - (ones * 100))
                    onetonineteen()

                    gansal = Math.Round((number - Int(number)) * 100, 2)

                    If gansal = 0 Then
                        convertedamout = hundredword + " Hundred " + wordones + " pesos only"
                    Else
                        convertedamout = hundredword + " Hundred " + wordones + " and " + Format(Int(gansal), "00") + "/100 pesos only"
                    End If



                Else

                    tens()

                    ones = Int(number - ((hundreds * 100) + (tenten * 10)))
                    onetonineteen()

                    gansal = Math.Round((number - Int(number)) * 100, 2)

                    If gansal = 0 Then
                        convertedamout = hundredword + " Hundred " + tentenword + " " + wordones + " pesos only"
                    Else
                        convertedamout = hundredword + " Hundred " + tentenword + " " + wordones + " and " + Format(Int(gansal), "00") + "/100 pesos only"
                    End If



                End If

            End If

            If number > 999.99 And number < 1000000 Then

                thousands = Int(number / 1000)

                If thousands > 19 And thousands < 100 Then

                    tenten = thousands \ 10
                    tens()

                    ones = thousands - ((thousands \ 10) * 10)
                    onetonineteen()

                    thousanword = tentenword + " " + wordones + " Thousand "

                    callhundred = Int(number - (thousands * 1000))


                    hundred()

                    convertedamout = thousanword + hundredwords

                End If

                If thousands > 99 And thousands < 1000 Then

                    ones = thousands \ 100
                    onetonineteen()

                    hundredthouword = wordones + " Hundred "

                    tenten = (thousands - (ones * 100)) \ 10

                    If tenten < 2 Then

                        ones = Int(thousands - (ones * 100))
                        onetonineteen()

                        hundredthouword = hundredthouword + wordones + " Thousand "

                        callhundred = Int(number - (thousands * 1000))

                        hundred()

                        convertedamout = hundredthouword + hundredwords

                    Else

                        tens()

                        ones = Int(thousands - ((ones * 100) + (tenten * 10)))
                        onetonineteen()

                        hundredthouword = hundredthouword + tentenword + " " + wordones + " Thousand "

                        callhundred = Int(number - (thousands * 1000))

                        hundred()

                        convertedamout = hundredthouword + hundredwords

                    End If

                End If

                If thousands < 20 Then

                    ones = thousands
                    onetonineteen()

                    thousanword = wordones

                    hundreds = Int((number - (ones * 1000)) / 100)

                    If hundreds = 0 Then

                        tenten = Int((number - (thousands * 1000) + (hundreds * 100)) / 10)

                        If tenten < 2 Then

                            ones = Int(number - ((thousands * 1000) + (hundreds * 100)))
                            onetonineteen()

                            gansal = Math.Round((number - Int(number)) * 100, 2)

                            If gansal = 0 Then
                                convertedamout = thousanword + " Thousand " + wordones + " pesos only"
                            Else
                                convertedamout = thousanword + " Thousand " + wordones + " and " + Format(Int(gansal), "00") + "/100 pesos only"
                            End If


                        Else

                            tens()

                            ones = Int(number - ((thousands * 1000) + (hundreds * 100) + (tenten * 10)))
                            onetonineteen()

                            gansal = Math.Round((number - Int(number)) * 100, 2)

                            If gansal = 0 Then
                                convertedamout = thousanword + " Thousand " + tentenword + " " + wordones + " pesos only"
                            Else
                                convertedamout = thousanword + " Thousand " + tentenword + " " + wordones + " and " + Format(Int(gansal), "00") + "/100 pesos only"
                            End If


                        End If

                    Else

                        ones = hundreds
                        onetonineteen()

                        hundredword = wordones

                        tenten = Int((number - ((thousands * 1000) + (hundreds * 100))) / 10)

                        If tenten < 2 Then

                            ones = Int(number - ((thousands * 1000) + (hundreds * 100)))
                            onetonineteen()

                            gansal = Math.Round((number - Int(number)) * 100, 2)

                            If gansal = 0 Then
                                convertedamout = thousanword + " Thousand " + hundredword + " Hundred " + wordones + " pesos only"
                            Else
                                convertedamout = thousanword + " Thousand " + hundredword + " Hundred " + wordones + " and " + Format(Int(gansal), "00") + "/100 pesos only"
                            End If


                        Else

                            tens()

                            ones = Int(number - ((thousands * 1000) + (hundreds * 100) + (tenten * 10)))
                            onetonineteen()

                            gansal = Math.Round((number - Int(number)) * 100, 2)

                            If gansal = 0 Then
                                convertedamout = thousanword + " Thousand " + hundredword + " Hundred " + tentenword + " " + wordones + " pesos only"
                            Else
                                convertedamout = thousanword + " Thousand " + hundredword + " Hundred " + tentenword + " " + wordones + " and " + Format(Int(gansal), "00") + "/100 pesos only"
                            End If


                        End If

                    End If

                End If

            End If

            If number > 999999.99 And number < 1000000000 Then

                millions = Int(number / 1000000)

                If millions < 20 Then

                    ones = millions
                    onetonineteen()

                    millionwords = wordones + " Million "

                    millionsthou = number - (ones * 1000000)

                    libolibo()

                    convertedamout = millionwords + millionthousanwords

                End If

                If millions > 19.99 And millions < 100 Then

                    tenten = Int(millions / 10)
                    tens()

                    ones = Int(millions - (tenten * 10))
                    onetonineteen()

                    millionwords = tentenword + " " + wordones + " Million "

                    millionsthou = number - ((ones + (tenten * 10)) * 1000000)

                    libolibo()

                    convertedamout = millionwords + millionthousanwords

                End If

                If millions > 99.99 And millions < 1000 Then

                    ones = Int(millions \ 100)
                    onetonineteen()

                    hundredmillion = ones
                    hundredmillionswords = wordones + " Hundred "

                    tenten = Int((millions - (hundredmillion * 100)) / 10)

                    If tenten < 2 Then

                        ones = Int(millions - (hundredmillion * 100))
                        onetonineteen()

                        millionwords = hundredmillionswords + wordones + " Million "

                        millionsthou = Int(number - (((hundredmillion * 100) + ones) * 1000000))

                        libolibo()

                        convertedamout = millionwords + millionthousanwords

                    Else

                        tens()

                        ones = Int(millions - ((tenten * 10) + (hundredmillion * 100)))

                        onetonineteen()

                        millionwords = hundredmillionswords + tentenword + " " + wordones + " Million "

                        millionsthou = Int(number - (millions * 1000000))

                        libolibo()

                        convertedamout = millionwords + millionthousanwords

                    End If

                End If

            End If

            If number > 999999999.99 Or number < 1 Then

            Else
                'Me.PrintPreviewControl1.InvalidatePreview()
            End If

        Catch ex As Exception
            MsgBox("Invalid amount")
        End Try

    End Sub

    Private Sub accSearch_Click(sender As Object, e As EventArgs) Handles accSearch.Click

        'SearchAccount.Close()
        'SearchAccount.Show()
        'SearchAccount.BringToFront()
        SearchAccount.searchingform = "collection"
        SearchAccount.ShowDialog()


    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click

        clearallfields()
        getcrnumber()
        btnDelete.Hide()

        Calculator.Label1_Click(Nothing, New KeyEventArgs(Keys.Enter))
        chkmode.CheckState = CheckState.Unchecked

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        CRCancel.cancelmode = "Create New"
        CRCancel.ShowDialog()


    End Sub

    Private Sub DetailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DetailsToolStripMenuItem.Click

        CRCancel.cancelmode = "Viewing"
        CRCancel.ShowDialog()

    End Sub

    Private Sub crno_TextChanged(sender As Object, e As EventArgs) Handles crno.TextChanged
        reprint.Hide()
        btnDelete.Hide()
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

        If billsave.Visible = False Then

        Else
            billsave_Click(Nothing, New KeyEventArgs(Keys.Enter))
        End If





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
            amountpaid.Text = Format(billTotalamountdue.Text, "Standard")


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
        crno.Click, billAccountNo.Click, billName.Click, billAddress.Click, billZone.Click, billbillingmonth.Click, discdate.Click,
        currentBilling.Click, CRBillno.Click, billamountdue.Click, billPenalty.Click, billdiscount.Click, billdiscount.Click,
        billadvancepayment.Click, billcharges.Click, billarrears.Click, dgvothers.Click, dgvotherchages.Click, rbcash.Click, rbcheck.Click,
        rbOnline.Click, billcheckno.Click, billcheckdate.Click, billTotalamountdue.Click, amountpaid.Click, overpayment.Click, autoprint.Click,
        reprint.Click, billsave.Click ' etc.
        Me.Activate() 'Or Whatever
    End Sub

    Private Sub billAccountNo_TextChanged(sender As Object, e As EventArgs) Handles billAccountNo.TextChanged

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
        billcheckno.Clear()
        billcheckdate.Clear()
        billbillingmonth.Clear()
        discdate.Clear()
        billcharges.Rows.Clear()

        dgvotherchages.Rows.Clear()
        dgvothers.Rows.Clear()
        amountpaid.Text = "0.00"
        overpayment.Text = "0.00"

        billcheckdate.ReadOnly = False
        billcheckno.ReadOnly = False
        billsave.Enabled = False
        billsave.Hide()
        rbcash.Checked = True
        lblaccstatus.Text = "Mode"
        lblaccstatus.Visible = False

        lblStatus.Text = "Mode"
        lblStatus.Visible = False

        btnDelete.Hide()
        reprint.Hide()

        'Label8.Visible = False
        'Label6.Visible = False
        billcheckno.Clear()
        billcheckdate.Clear()
        'billcheckno.Visible = False
        'billcheckdate.Visible = False

        billsave.Hide()
        billsave.Enabled = False

    End Sub

    Private Sub chkmode_CheckedChanged(sender As Object, e As EventArgs) Handles chkmode.CheckedChanged


        If chkmode.CheckState = CheckState.Checked Then

            chkmode.BackColor = Color.FromArgb(115, 195, 110)
            chkmode.Text = "Query Mode"
            billsave.Text = "Add"

            billAccountNo.Select()
            billAccountNo.SelectAll()

        Else

            chkmode.BackColor = Color.White
            chkmode.Text = "Transaction Mode"
            billsave.Text = "Save"

            billAccountNo.Select()
            billAccountNo.SelectAll()

        End If

    End Sub

End Class