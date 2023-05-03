Public Class BillingCancel
    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click

        If cancelmode = "Create New" Then

            If billinginfo.lblStatus.Text = "Pending" Then

                If Remarks.Text = "" Then
                Else

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                    stracs = "insert into BillCancelled ([RefNo],[AccountNo],[BillNo],[Remarks],[CancelledBy],[DateCancelled],[Billcovered]) values (" _
                        & RefNo.Text & ", '" & billAccountNo.Text & "', " & Val(billBillno.Text) & ", '" & Remarks.Text.ToString.Replace("'", "''") & "', '" & My.Settings.Nickname & "', '" _
                        & Format(CancelDate.Value, "yyyy-MM-dd") & "', '" & covered.Text & "')"

                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                    stracs = "update Bills set Cancelled = 'Yes' where BillNo = " & billBillno.Text
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                    stracs = "update BillCharges set Cancelled = 'Yes' where BillNumber = " & billBillno.Text
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    stracs = "update tbllogicnumbers set number = number + 1 where id = 6"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                    billinginfo.lockfields()

                    billinginfo.lblMode.Text = "Mode"
                    billinginfo.lblMode.Hide()

                    billinginfo.billBillno.ReadOnly = False
                    billinginfo.billBillno_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))
                    Me.Close()
                    MsgBox("Bill Cancelled.")

                End If

            End If

            If billinginfo.lblStatus.Text = "Posted" Then

                If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                Dim gettotalbillbalance As New DataTable
                Dim totalbillbalance As Double = 0

                gettotalbillbalance.Clear()
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                'stracs = "select SUM(AmountDue), S from Bills where AccountNumber = '" & billList.Rows(x).Cells(2).Value & "' and BillStatus = 'Posted' and Cancelled = 'No' and IsPaid = 'No'"
                stracs = "select isnull(SUM(AmountDue),0) as amountdue, isnull(SUm(AdvancePayment),0) as advance, Sum(Discount) as discount, 
                          isnull(SUm(PenaltyAfterDue),0) as penalty, isnull(SUm(Adjustment),0) as Adjustment from Bills 
                          where AccountNumber = '" & billAccountNo.Text & "' and BillStatus = 'Posted' and Cancelled = 'No' and IsPaid = 'No' 
                          and IsCollectionCreated = 'No' and isPromisorry = 'No'"
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsda.SelectCommand = acscmd
                acsda.Fill(gettotalbillbalance)

                If gettotalbillbalance.Rows.Count = 0 Then
                    totalbillbalance = 0
                Else
                    totalbillbalance = Val(gettotalbillbalance(0)("amountdue") + gettotalbillbalance(0)("penalty") + gettotalbillbalance(0)("Adjustment")) - Val(gettotalbillbalance(0)("advance") + gettotalbillbalance(0)("discount"))

                End If

                Dim gettotalchargebalance As New DataTable
                Dim totalbillchargebalance As Double = 0

                gettotalchargebalance.Clear()

                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                stracs = "select SUM(Amount) as amount from BillCharges where AccountNumber = '" & billAccountNo.Text & "' and Status = 'Posted' and Cancelled = 'No' and IsPaid = 'No' and Category = 'Others'"
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
                stracs = "select SUM(Billing + Penalty) AS PNTOTAL from AddAdjustment where IsCollectionCreated = 'No' and Status = 'Posted' and Paid = 'No' AND AccountNumber = '" & billAccountNo.Text & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(gettotalpn)

                If IsDBNull(gettotalpn(0)("PNTOTAL")) = True Then
                    totalpn = 0
                Else
                    totalpn = gettotalpn(0)("PNTOTAL")
                End If

                stracs = "insert into BillCancelled ([RefNo],[AccountNo],[BillNo],[Remarks],[CancelledBy],[DateCancelled],[Billcovered]) values (" _
                        & RefNo.Text & ", '" & billAccountNo.Text & "', " & Val(billBillno.Text) & ", '" & Remarks.Text & "', '" & My.Settings.Nickname & "', '" _
                        & Format(CancelDate.Value, "yyyy-MM-dd") & "', '" & covered.Text & "')"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                stracs = "update Bills set Cancelled = 'Yes' where BillNo = " & billBillno.Text
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                stracs = "update BillCharges set Cancelled = 'Yes' where BillNumber = " & billBillno.Text
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                stracs = "update Customers set Customers.AdvancePayment = Bills.AdvancePayment from Customers join Bills 
                            on Bills.AccountNumber = Customers.AccountNo where Bills.BillNo = " & billBillno.Text
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                stracs = "update Customers set LasReadingDate = (select ReadingDate from Bills where BillNo = (select max(BillNo) as BillNo from Bills where AccountNumber = '" & billAccountNo.Text & "' and Cancelled = 'No')) where AccountNo = '" & billAccountNo.Text & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                stracs = "insert into AccountLedger (ledgerAccountNo, ledgerDate, ledgerRefNo, ledgerParticulars, ledgerReading, ledgerConsumption, ledgerDiscount, ledgerAmount, ledgerBalance) values ('" _
                                & billAccountNo.Text & "', '" _
                                & Format(Date.Parse(CancelDate.Value), "yyyy-MM-dd") & "', '" _
                                & RefNo.Text & "', '" _
                                & "Cancelled Bill(" & billBillno.Text & ")', '" _
                                & "', '" _
                                & "', '" _
                                & Format(Double.Parse(amountdue.Text), "Standard") & "', '" _
                                & "', '" _
                                & Format(((Val(totalbillbalance) + Val(totalbillchargebalance) + Val(totalpn)) - Double.Parse(amountdue.Text)), "Standard") & "')"

                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                stracs = "update tbllogicnumbers set number = number + 1 where id = 6"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                billinginfo.lockfields()

                billinginfo.lblMode.Text = "Mode"
                billinginfo.lblMode.Hide()

                billinginfo.billBillno.ReadOnly = False

                billinginfo.billBillno_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))

                Me.Close()
                MsgBox("Bill Cancelled.")

            End If

        End If

        If billinginfo.lblStatus.Text = "Paid" Then

            If acsconn.State = ConnectionState.Closed Then acsconn.Open()

            stracs = "update Bills set Cancelled = 'Yes' where BillNo = " & billBillno.Text
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()

            stracs = "update BillCharges set Cancelled = 'Yes' where BillNumber = " & billBillno.Text
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()

            Dim gettotalbillbalance As New DataTable
            Dim totalbillbalance As Double = 0

            gettotalbillbalance.Clear()
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            'stracs = "select SUM(AmountDue), S from Bills where AccountNumber = '" & billList.Rows(x).Cells(2).Value & "' and BillStatus = 'Posted' and Cancelled = 'No' and IsPaid = 'No'"
            stracs = "select isnull(SUM(AmountDue), 0) as amountdue, isnull(SUm(AdvancePayment),0) as advance, isnull(Sum(Discount),0) as discount, 
                             isnull(SUm(PenaltyAfterDue),0) as penalty, isnull(SUm(Adjustment),0) as Adjustment from Bills 
                             where AccountNumber = '" & billAccountNo.Text & "' and BillStatus = 'Posted' and Cancelled = 'No' and IsPaid = 'No' 
                             and IsCollectionCreated = 'No' and isPromisorry = 'No'"
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(gettotalbillbalance)

            If gettotalbillbalance.Rows.Count = 0 Then
                totalbillbalance = 0
            Else

                'MsgBox(gettotalbillbalance(0)("amountdue") & " - " & gettotalbillbalance(0)("penalty") & vbCrLf & gettotalbillbalance(0)("Adjustment") & vbCrLf & gettotalbillbalance(0)("advance") & vbCrLf & gettotalbillbalance(0)("discount"))
                totalbillbalance = Val(gettotalbillbalance(0)("amountdue") + gettotalbillbalance(0)("penalty") + gettotalbillbalance(0)("Adjustment")) - (Val(gettotalbillbalance(0)("advance") + gettotalbillbalance(0)("discount")))

            End If

            Dim gettotalchargebalance As New DataTable
            Dim totalbillchargebalance As Double = 0

            gettotalchargebalance.Clear()

            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            stracs = "select SUM(Amount) as amount from BillCharges where AccountNumber = '" & billAccountNo.Text & "' and Status = 'Posted' and Cancelled = 'No' and IsPaid = 'No' and Category = 'Others'"
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
            stracs = "select SUM(Billing + Penalty) AS PNTOTAL from AddAdjustment where IsCollectionCreated = 'No' and Status = 'Posted' and Paid = 'No' AND AccountNumber = '" & billAccountNo.Text & "'"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(gettotalpn)

            If IsDBNull(gettotalpn(0)("PNTOTAL")) = True Then
                totalpn = 0
            Else
                totalpn = gettotalpn(0)("PNTOTAL")
            End If

            stracs = "insert into BillCancelled ([RefNo],[AccountNo],[BillNo],[Remarks],[CancelledBy],[DateCancelled],[Billcovered]) values (" _
                    & RefNo.Text & ", '" & billAccountNo.Text & "', " & Val(billBillno.Text) & ", '" & Remarks.Text & "', '" & My.Settings.Nickname & "', '" _
                    & Format(CancelDate.Value, "yyyy-MM-dd") & "', '" & covered.Text & "')"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()



            Dim billchargis As Double = 0

            For x = 0 To billinginfo.billcharges.Rows.Count - 1

                billchargis = billchargis + Double.Parse(billinginfo.billcharges.Rows(x).Cells(4).Value)

            Next

            Dim tots As Double = (Double.Parse(billinginfo.billamountdue.Text) + (Double.Parse(billinginfo.billAdjustment.Text) + Double.Parse(billinginfo.billPenalty.Text) + Double.Parse(billchargis)) - Double.Parse(billinginfo.billdiscount.Text))

            If Double.Parse(billinginfo.billadvancepayment.Text) - tots < 0 Then
                stracs = "update Customers set Customers.AdvancePayment = Customers.AdvancePayment from Customers join Bills 
                            on Bills.AccountNumber = Customers.AccountNo where Bills.BillNo = " & billBillno.Text
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()
            Else

                stracs = "update Customers set Customers.AdvancePayment = Customers.AdvancePayment + " & tots & " from Customers join Bills 
                            on Bills.AccountNumber = Customers.AccountNo where Bills.BillNo = " & billBillno.Text
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

            End If

            stracs = "update Customers set LasReadingDate = (select ReadingDate from Bills where BillNo = (select max(BillNo) as BillNo from Bills where AccountNumber = '" & billAccountNo.Text & "' and Cancelled = 'No')) where AccountNo = '" & billAccountNo.Text & "'"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()

            Dim getsungadvance As New DataTable

            stracs = "select AdvancePayment from Customers where AccountNo = '" & billAccountNo.Text & "'"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(getsungadvance)

            stracs = "insert into AccountLedger (ledgerAccountNo, ledgerDate, ledgerRefNo, ledgerParticulars, ledgerReading, ledgerConsumption, ledgerDiscount, ledgerAmount, ledgerBalance) values ('" _
                            & billAccountNo.Text & "', '" _
                            & Format(Date.Parse(CancelDate.Value), "yyyy-MM-dd") & "', '" _
                            & RefNo.Text & "', '" _
                            & "Cancelled Bill(" & billBillno.Text & ")', '" _
                            & "', '" _
                            & "', '" _
                            & Format(Double.Parse(amountdue.Text), "Standard") & "', '" _
                            & "', '" _
                            & FormatNumber(((Val(totalbillbalance) + Val(totalbillchargebalance) + Val(totalpn)) - Double.Parse(getsungadvance.Rows(0)("AdvancePayment"))), UseParensForNegativeNumbers:=TriState.True) & "')"
            'FormatNumber((Val(totalbillbalance) + Val(totalbillchargebalance) + Val(totalpn)) - Val(billPenalty.Text), UseParensForNegativeNumbers:=TriState.True)

            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()

            stracs = "update tbllogicnumbers set number = number + 1 where id = 6"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()

            billinginfo.lockfields()

            billinginfo.lblMode.Text = "Mode"
            billinginfo.lblMode.Hide()

            billinginfo.billBillno.ReadOnly = False

            billinginfo.billBillno_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))

            Me.Close()
            MsgBox("Bill Cancelled.")

        End If

    End Sub

    Public cancelmode As String

    Private Sub BillingCancel_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Me.MdiParent = eBACSmain

        If cancelmode = "Create New" Then

            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try
            Dim getrefno As New DataTable
            stracs = "select number from tbllogicnumbers where id = 6"
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(getrefno)

            RefNo.Text = getrefno(0)("number")
            Label8.Hide()
            CancelDate.Value = Now

            Dim tutalbill As Double = 0
            Dim tutalcharge As Double = 0

            Dim totalbillbalance As New DataTable
            Dim totalchargebalance As New DataTable

            'stracs = "select ((a.AmountDue + a.PenaltyAfterDue) - (a.AdvancePayment + a.Discount) + b.Amount) as tutal 
            '         from Bills a join BillCharges b on a.BillNo = b.BillNumber where a.BillNo = '" & billinginfo.billBillno.Text & "'"

            stracs = "select ((a.AmountDue + a.PenaltyAfterDue + a.Adjustment) - a.Discount) as tutal 
                     from Bills a where a.BillNo = '" & billinginfo.billBillno.Text & "'"
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(totalbillbalance)

            If totalbillbalance.Rows.Count = 0 Then

                tutalbill = 0

            Else

                tutalbill = totalbillbalance.Rows(0)("tutal")

            End If

            stracs = "select Amount from BillCharges where BillNumber = '" & billinginfo.billBillno.Text & "'"
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(totalchargebalance)

            If totalchargebalance.Rows.Count = 0 Then

                tutalcharge = 0
            Else
                tutalcharge = totalchargebalance.Rows(0)("Amount")

            End If

            billAccountNo.Text = billinginfo.billAccountNo.Text
            billName.Text = billinginfo.billName.Text
            billBillno.Text = billinginfo.billBillno.Text
            covered.Text = billinginfo.billCovered.Text
            amountdue.Text = Format(tutalbill + tutalcharge, "Standard")

        End If

            If cancelmode = "Viewing" Then

            billAccountNo.Text = billinginfo.billAccountNo.Text
            billBillno.Text = billinginfo.billBillno.Text

            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try
            Dim checking As New DataTable
            checking.Clear()
            stracs = "select * from BillCancelled where AccountNo = '" & billAccountNo.Text & "' and BillNo = " & billBillno.Text
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(checking)

            If checking.Rows.Count = 0 Then

            Else

                Dim chargesbill As Double = 0

                For x = 0 To billinginfo.billcharges.Rows.Count - 1
                    chargesbill = chargesbill + Double.Parse(billinginfo.billcharges.Rows(x).Cells(4).Value)
                Next

                RefNo.Text = checking(0)("RefNo")
                CancelDate.Value = checking(0)("DateCancelled")
                billAccountNo.Text = checking(0)("AccountNo")
                billName.Text = billinginfo.billName.Text
                billBillno.Text = checking(0)("BillNo")
                amountdue.Text = Format((Double.Parse(billinginfo.billamountdue.Text) + Double.Parse(billinginfo.billPenalty.Text) + chargesbill) - (Double.Parse(billinginfo.billdiscount.Text)), "standard")
                Remarks.Text = checking(0)("Remarks")
                covered.Text = checking(0)("Billcovered")
                Label8.Text = "Cancelled by:" & vbCrLf & checking(0)("CancelledBy")

            End If

        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Public MoveFormbillingCancel As Boolean
    Public MovebillingCancel_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormbillingCancel = True
            Me.Cursor = Cursors.NoMove2D
            MovebillingCancel_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormbillingCancel Then
            Me.Location = Me.Location + (e.Location - MovebillingCancel_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormbillingCancel = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub BillingCancel_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Me.Activate()
    End Sub

End Class