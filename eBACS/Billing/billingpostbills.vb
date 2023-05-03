Public Class billingpostbills
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Sub searchbyzonedate()

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        sqldataZone.Clear()

        If billZone.Text = "All" Then

            stracs = "select BillNo,ReadingDate,AccountNumber,CustomerName,Reading,Consumption,Discount,AdvancePayment,AmountDue,BillStatus,PenaltyAfterDue,AverageCons 
                    from Bills where Cancelled = 'No' and Consumption >= 0 and AmountDue <> 0 and Reading >= 0 and 
                    BIllingDate = '" & billMonth.Text & " " & billYear.Text & "' and BillStatus = 'Pending' order by BILLID"

        Else

            stracs = "select BillNo,ReadingDate,AccountNumber,CustomerName,Reading,Consumption,Discount,AdvancePayment,AmountDue,BillStatus,PenaltyAfterDue,AverageCons 
                    from Bills where Cancelled = 'No' and Consumption >= 0 and AmountDue <> 0 and Reading >= 0 and BillStatus = 'Pending' and 
                    Zone = '" & billZone.Text.ToString.Replace("'", "''") & "' and BIllingDate = '" & billMonth.Text & " " & billYear.Text & "' order by BILLID"

        End If

        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(sqldataZone)

        billList.Rows.Clear()

        For i = 0 To sqldataZone.Rows.Count - 1

            If i Mod 2 = 0 Then
                billList.Rows.Add(sqldataZone(i)("BillNo"), Format(sqldataZone(i)("ReadingDate"), "short date"), sqldataZone(i)("AccountNumber"),
                              sqldataZone(i)("CustomerName"), sqldataZone(i)("Reading"), sqldataZone(i)("Consumption"),
                              Format(Val(sqldataZone(i)("Discount")) + Val(sqldataZone(i)("AdvancePayment")), "standard"),
                              Format(Val(sqldataZone(i)("AmountDue")), "Standard"), sqldataZone(i)("BillStatus"), 0, sqldataZone(i)("PenaltyAfterDue"), sqldataZone(i)("AverageCons"), 0, Format(Val(sqldataZone(i)("AdvancePayment")), "standard"))
                billList.Rows(i).DefaultCellStyle.BackColor = Color.Gainsboro

            Else
                billList.Rows.Add(sqldataZone(i)("BillNo"), Format(sqldataZone(i)("ReadingDate"), "short date"), sqldataZone(i)("AccountNumber"),
                              sqldataZone(i)("CustomerName"), sqldataZone(i)("Reading"), sqldataZone(i)("Consumption"),
                              Format(Val(sqldataZone(i)("Discount")) + Val(sqldataZone(i)("AdvancePayment")), "standard"),
                              Format(Val(sqldataZone(i)("AmountDue")), "Standard"), sqldataZone(i)("BillStatus"), 0, sqldataZone(i)("PenaltyAfterDue"), sqldataZone(i)("AverageCons"), 0, Format(Val(sqldataZone(i)("AdvancePayment")), "standard"))
                billList.Rows(i).DefaultCellStyle.BackColor = Color.White
            End If

        Next

        billTotalbill.Text = sqldataZone.Rows.Count

    End Sub

    Private Sub postingbills_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain
        Dim x As Integer = 1
        Dim years As Integer = 2020

        billMonth.Items.Clear()
        billYear.Items.Clear()

        Do Until x = 13

            billMonth.Items.Add(MonthName(x))
            billYear.Items.Add(years)
            years = years + 1
            x = x + 1
        Loop

        billMonth.Text = Format(Now, "MMMMM")
        billYear.Text = Format(Now, "yyyy")

        loadzones()



    End Sub

    Sub loadzones()

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        Try
            sqldataZone.Clear()
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            stracs = "SELECT ZoneName FROM Zone"

            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(sqldataZone)


            billZone.Items.Clear()
            billZone.Items.Add("All")
            For i = 0 To sqldataZone.Rows.Count - 1
                billZone.Items.Add(sqldataZone.Rows(i)("ZoneName"))
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
            acsconn.Close()
        End Try

    End Sub

    Private Sub billMonth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles billMonth.SelectedIndexChanged
        searchbyzonedate()
        CheckBox1.CheckState = CheckState.Unchecked
    End Sub

    Private Sub billYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles billYear.SelectedIndexChanged
        searchbyzonedate()
        CheckBox1.CheckState = CheckState.Unchecked
    End Sub

    Private Sub billZone_SelectedIndexChanged(sender As Object, e As EventArgs) Handles billZone.SelectedIndexChanged
        searchbyzonedate()
        CheckBox1.CheckState = CheckState.Unchecked
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.CheckState = CheckState.Checked Then

            If billList.Rows.Count > 0 Then

                CheckBox1.Text = "Uncheck all"

                For x = 0 To billList.Rows.Count - 1

                    If billList.Rows(x).Cells(9).Value < 0 Then

                    Else
                        billList.Rows(x).Cells(9).Value = -1
                    End If

                Next

            Else

            End If

        Else

            If billList.Rows.Count > 0 Then

                CheckBox1.Text = "Check all"

                For x = 0 To billList.Rows.Count - 1
                    billList.Rows(x).Cells(9).Value = 0
                Next

            Else

            End If

        End If
    End Sub

    Private Sub billSearch_Click(sender As Object, e As EventArgs) Handles billPost.Click
        Cursor = Cursors.WaitCursor
        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        If billList.Rows.Count > 0 Then

            For x = 0 To billList.Rows.Count - 1

                If billList.Rows(x).Cells(9).Value = -1 Then

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                    stracs = "update Bills set BillStatus = 'Posted', PostedBy = '" & My.Settings.Nickname & "' where BillNo = " & billList.Rows(x).Cells(0).Value
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                    Dim getaverage As New DataTable
                    Dim newaverage As Integer = 0

                    getaverage.Clear()
                    stracs = "SELECT top 3 Consumption From Bills Where Cancelled = 'No' and BillStatus = 'Posted' and AccountNumber = '" & billList.Rows(x).Cells(2).Value & "' Order By BillNo desc"
                    acscmd.Connection = acsconn
                    acscmd.CommandText = stracs
                    acsda.SelectCommand = acscmd
                    acsda.Fill(getaverage)

                    For y = 0 To getaverage.Rows.Count - 1

                        newaverage = newaverage + getaverage(y)("Consumption")

                    Next

                    newaverage = CInt(newaverage / getaverage.Rows.Count)


                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    stracs = "update Customers set Averagee = " & newaverage & ", LastMeterReading = " & billList.Rows(x).Cells(4).Value & ", LastBill = '" & billList.Rows(x).Cells(0).Value & "', LasReadingDate = '" & Format(Date.Parse(billList.Rows(x).Cells(1).Value), "yyyy-MM-dd") & "', AdvancePayment = AdvancePayment - " & Double.Parse(billList.Rows(x).Cells(13).Value) & " where AccountNo = '" & billList.Rows(x).Cells(2).Value & "'"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                    Dim gettotalbillbalance As New DataTable
                    Dim totalbillbalance As Double = 0

                    gettotalbillbalance.Clear()
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    'stracs = "select SUM(AmountDue), S from Bills where AccountNumber = '" & billList.Rows(x).Cells(2).Value & "' and BillStatus = 'Posted' and Cancelled = 'No' and IsPaid = 'No'"
                    stracs = "select SUM(AmountDue) as amountdue, SUm(AdvancePayment) as advance, Sum(Discount) as discount, SUm(PenaltyAfterDue) as penalty, SUm(Adjustment) as Adjustment from Bills where AccountNumber = '" & billList.Rows(x).Cells(2).Value & "' and BillStatus = 'Posted' and Cancelled = 'No' and IsPaid = 'No' and isPromisorry = 'No'"
                    acscmd.Connection = acsconn
                    acscmd.CommandText = stracs
                    acsda.SelectCommand = acscmd
                    acsda.Fill(gettotalbillbalance)

                    If gettotalbillbalance.Rows.Count = 0 Then
                        totalbillbalance = 0
                    Else
                        totalbillbalance = Val(gettotalbillbalance(0)("amountdue") + gettotalbillbalance(0)("penalty") + gettotalbillbalance(0)("Adjustment")) - (Val(gettotalbillbalance(0)("advance") + gettotalbillbalance(0)("discount")))

                    End If

                    Dim gettotalchargebalance As New DataTable
                    Dim totalbillchargebalance As Double = 0

                    gettotalchargebalance.Clear()

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    stracs = "select SUM(Amount) as amount from BillCharges where AccountNumber = '" & billList.Rows(x).Cells(2).Value & "' and Status = 'Posted' and Cancelled = 'No' and IsPaid = 'No' and Category = 'Others'"
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

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    Dim gettotalpn As New DataTable
                    Dim totalpn As Double = 0
                    stracs = "select SUM(Billing + Penalty) AS PNTOTAL from AddAdjustment where IsCollectionCreated = 'No' and Status = 'Posted' and Paid = 'No' AND AccountNumber = '" & billList.Rows(x).Cells(2).Value & "'"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsda.SelectCommand = acscmd
                    acsda.Fill(gettotalpn)

                    If IsDBNull(gettotalpn(0)("PNTOTAL")) = True Then
                        totalpn = 0
                    Else
                        totalpn = gettotalpn(0)("PNTOTAL")
                    End If

                    If Double.Parse(billList.Rows(x).Cells(10).Value) > 0 Then
                        MsgBox("inserted 1")
                        stracs = "insert into AccountLedger (ledgerAccountNo, ledgerDate, ledgerRefNo, ledgerParticulars, ledgerReading, ledgerConsumption, ledgerDiscount, ledgerAmount, ledgerBalance) values ('" _
                            & billList.Rows(x).Cells(2).Value & "', '" _
                            & Format(Date.Parse(billList.Rows(x).Cells(1).Value), "yyyy-MM-dd") & "', '" _
                            & billList.Rows(x).Cells(0).Value & "', '" _
                            & "Billing', '" _
                            & billList.Rows(x).Cells(4).Value & "', '" _
                            & billList.Rows(x).Cells(5).Value & "', '" _
                            & billList.Rows(x).Cells(6).Value & "', '" _
                            & billList.Rows(x).Cells(7).Value & "', '" _
                            & Format((Val(totalbillbalance) + Val(totalbillchargebalance) + Val(totalpn)) - Val(billList.Rows(x).Cells(10).Value), "Standard") & "')"

                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        stracs = "insert into AccountLedger (ledgerAccountNo, ledgerDate, ledgerRefNo, ledgerParticulars, ledgerReading, ledgerConsumption, ledgerDiscount, ledgerAmount, ledgerBalance) values ('" _
                            & billList.Rows(x).Cells(2).Value & "', '" _
                            & Format(Date.Parse(billList.Rows(x).Cells(1).Value), "yyyy-MM-dd") & "', '" _
                            & billList.Rows(x).Cells(0).Value & "', '" _
                            & "Penalty', '" _
                            & "', '" _
                            & "', '" _
                            & "', '" _
                            & billList.Rows(x).Cells(10).Value & "', '" _
                            & Format(Val(totalbillbalance) + Val(totalbillchargebalance) + Val(totalpn), "Standard") & "')"

                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                    Else

                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "insert into AccountLedger (ledgerAccountNo, ledgerDate, ledgerRefNo, ledgerParticulars, ledgerReading, ledgerConsumption, ledgerDiscount, ledgerAmount, ledgerBalance) values ('" _
                            & billList.Rows(x).Cells(2).Value & "', '" _
                            & Format(Date.Parse(billList.Rows(x).Cells(1).Value), "yyyy-MM-dd") & "', '" _
                            & billList.Rows(x).Cells(0).Value & "', '" _
                            & "Billing', '" _
                            & billList.Rows(x).Cells(4).Value & "', '" _
                            & billList.Rows(x).Cells(5).Value & "', '" _
                            & billList.Rows(x).Cells(6).Value & "', '" _
                            & billList.Rows(x).Cells(7).Value & "', '" _
                            & Format(Val(totalbillbalance) + Val(totalbillchargebalance + Val(totalpn)), "Standard") & "')"

                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                    End If

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    Dim getcharges As New DataTable
                    Dim newchargesamount As Double = totalbillchargebalance
                    stracs = "select * from BillCharges where BillNumber = " & billList.Rows(x).Cells(0).Value
                    acscmd.Connection = acsconn
                    acscmd.CommandText = stracs
                    acsda.SelectCommand = acscmd
                    acsda.Fill(getcharges)

                    For i = 0 To getcharges.Rows.Count - 1

                        stracs = "update BillCharges set Status = 'Posted' where BillNumber = " & billList.Rows(x).Cells(0).Value
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        newchargesamount = Val(newchargesamount) + Val(getcharges.Rows(i)("Amount"))
                        'format(Val(newchargesamount) + Val(totalbillbalance), "Standard")

                        If getcharges.Rows(i)("Category") = "Others" Then

                            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                            stracs = "insert into AccountLedger (ledgerAccountNo, ledgerDate, ledgerRefNo, ledgerParticulars, ledgerReading, ledgerConsumption, ledgerDiscount, ledgerAmount, ledgerBalance) values ('" _
                                & billList.Rows(x).Cells(2).Value & "', '" _
                                & Format(Date.Parse(billList.Rows(x).Cells(1).Value), "yyyy-MM-dd") & "', '" _
                                & billList.Rows(x).Cells(0).Value & "', '" _
                                & getcharges.Rows(i)("Particulars") & "', '" _
                                & "', '" _
                                & "', '" _
                                & "', '" _
                                & Format(Val(getcharges.Rows(i)("Amount")), "Standard") & "', '" _
                                & Format(Val(newchargesamount) + Val(totalbillbalance + Val(totalpn)), "Standard") & "')"

                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acscmd.ExecuteNonQuery()
                            acscmd.Dispose()

                        Else

                        End If

                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        'stracs = "Update ScheduleCharges set ActiveInactive = 0 where AccountNumber = '" & billList.Rows(x).Cells(2).Value & "' and BillingMonth = " & "" & " and BillingYear = "
                        stracs = "update ScheduleCharges set ActiveInactive = 0 where Recurring = 'No' and Category = '" & getcharges.Rows(i)("Category") & "' and Particular = '" & getcharges.Rows(i)("Particulars") & "' and AccountNumber = '" & billList.Rows(x).Cells(2).Value & "' and BillingMonth = " & Month(getcharges.Rows(i)("BillingMonth")) & " and BillingYear = " & Year(getcharges.Rows(i)("BillingMonth"))
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                    Next

                    stracs = "delete from BillsTemp where BillNo = " & billList.Rows(x).Cells(0).Value
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                    If Val(newchargesamount) + Val(totalbillbalance) + Val(totalpn) <= 0 Then

                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                        stracs = "update Bills set IsPaid = 'Yes', IsCollectionCreated = 'Yes' where BillNo = " & billList.Rows(x).Cells(0).Value
                        acscmd.CommandText = stracs
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        stracs = "update BillCharges set IsPaid = 'Yes', IsCollectionCreated = 'Yes' where BillNumber = " & billList.Rows(x).Cells(0).Value
                        acscmd.CommandText = stracs
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        stracs = "update Customers set AdvancePayment = " & FormatNumber(Double.Parse(Val(newchargesamount) + Val(totalbillbalance) + Val(totalpn)), UseParensForNegativeNumbers:=TriState.True) & " where AccountNo = '" & billList.Rows(x).Cells(2).Value & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                    End If

                Else

                End If

            Next

            searchbyzonedate()
            CheckBox1.CheckState = CheckState.Unchecked
            MsgBox("Bills posted.")

        Else

        End If
        Cursor = Cursors.Default
    End Sub

    Public MoveFormbillingpost As Boolean
    Public Movebillingpost_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormbillingpost = True
            Me.Cursor = Cursors.NoMove2D
            Movebillingpost_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormbillingpost Then
            Me.Location = Me.Location + (e.Location - Movebillingpost_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormbillingpost = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub billingpostbills_CausesValidationChanged(sender As Object, e As EventArgs) Handles Me.CausesValidationChanged
        Me.Activate()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Me.Activate()
    End Sub

    Private Sub postbill_deactivated(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.BackColor = Color.FromArgb(17, 153, 195)
    End Sub

    Private Sub postbill_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.BackColor = Color.SteelBlue
    End Sub

    Private Sub ControlsClick(sender As Object, e As EventArgs) Handles _
        Panel1.Click, billMonth.Click, billYear.Click, billZone.Click, CheckBox1.Click, billList.Click, billPost.Click  ' etc.
        Me.Activate() 'Or Whatever
    End Sub

    Private Sub chkZero_CheckedChanged(sender As Object, e As EventArgs) Handles chkZero.CheckedChanged

        If chkZero.CheckState = CheckState.Checked Then

            For x = 0 To billList.Rows.Count - 1

                If billList.Rows(x).Cells(5).Value = 0 Then

                    billList.Rows(x).Cells(9).Value = 0

                End If

            Next

        Else

            For x = 0 To billList.Rows.Count - 1

                If billList.Rows(x).Cells(5).Value = 0 Then

                    billList.Rows(x).Cells(9).Value = -1

                End If

            Next

        End If

    End Sub

    Private Sub chkHighCons_CheckedChanged(sender As Object, e As EventArgs) Handles chkHighCons.CheckedChanged

        If chkHighCons.CheckState = CheckState.Checked Then

            For x = 0 To billList.Rows.Count - 1

                If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                If billList.Rows(x).Cells(5).Value >= Val(billList.Rows(x).Cells(11).Value) * 2 And billList.Rows(x).Cells(5).Value > 10 Then

                    billList.Rows(x).Cells(9).Value = 0
                    billList.Rows(x).Cells(12).Value = -1

                End If

            Next

        Else

            For x = 0 To billList.Rows.Count - 1

                If billList.Rows(x).Cells(12).Value = -1 Then

                    billList.Rows(x).Cells(9).Value = -1
                    billList.Rows(x).Cells(12).Value = 0

                End If

            Next

        End If

    End Sub
End Class