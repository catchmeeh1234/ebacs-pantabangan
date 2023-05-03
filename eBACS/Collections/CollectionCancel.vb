Public Class CRCancel
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Public cancelmode As String
    Dim totalamount As Double
    Private Sub CollectionCancel_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        If cancelmode = "Create New" Then

            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Dim getrefno As New DataTable
            stracs = "select number from tbllogicnumbers where id = 7"
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(getrefno)

            RefNo.Text = getrefno(0)("number")
            Label8.Hide()
            CancelDate.Value = Now
            AccountNo.Text = Collection_CR.billAccountNo.Text
            AccountName.Text = Collection_CR.billName.Text
            CRno.Text = Collection_CR.crno.Text
            amountdue.Text = Collection_CR.amountpaid.Text

        End If

        If cancelmode = "Viewing" Then

            AccountNo.Text = Collection_CR.billAccountNo.Text
            CRno.Text = Collection_CR.crno.Text

            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Dim checking As New DataTable
            checking.Clear()
            stracs = "select * from CollectionCancelled where AccountNo = '" & AccountNo.Text & "' and CRNo = '" & CRno.Text & "'"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(checking)

            If checking.Rows.Count = 0 Then

            Else

                RefNo.Text = checking(0)("RefNo")

                CancelDate.Value = checking(0)("DateCancelled")
                AccountNo.Text = checking(0)("AccountNo")
                AccountName.Text = Collection_CR.billName.Text
                CRno.Text = checking(0)("CRNo")
                amountdue.Text = Collection_CR.amountpaid.Text
                Remarks.Text = checking(0)("Remarks")
                Label8.Text = "Cancelled by:" & vbCrLf & checking(0)("CancelledBy")
                Label8.Show()

            End If

        End If

    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click

        If My.Settings.Admin = "Yes" Or My.Settings.Cashier = "Yes" Then

            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try

            If cancelmode = "Create New" Then

                If Collection_CR.lblStatus.Text = "Pending" Then

                    If Remarks.Text = "" Then
                    Else

                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                        stracs = "insert into CollectionCancelled ([RefNo],[AccountNo],[CRNo],[Remarks],[CancelledBy],[DateCancelled]) values (" _
                            & RefNo.Text & ", '" & AccountNo.Text & "', '" & CRno.Text & "', '" & Remarks.Text & "', '" & My.Settings.Nickname & "', '" _
                            & Format(CancelDate.Value, "yyyy-MM-dd") & "')"

                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        stracs = "update Collection_Details set CollectionStatus = 'Posted', Cancelled = 'Yes' where CRNo = '" & CRno.Text & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        stracs = "update CollectionBilling set CollectionBillingStatus = 'Posted', Cancelled = 'Yes' where CRNo = '" & CRno.Text & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        stracs = "update CollectionCharges set CollectionChargesStatus = 'Posted', Cancelled = 'Yes' where CRNo = '" & CRno.Text & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        stracs = "update Bills set IsCollectionCreated = 'No', CRNo = NULL where CRNo = '" & CRno.Text & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        stracs = "update BillCharges set IsCollectionCreated = 'No', CRNo = NULL where CRNo = '" & CRno.Text & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        stracs = "update AddAdjustment set IsCollectionCreated = 'No', CRNo = NULL where CRNo = '" & CRno.Text & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "update tbllogicnumbers set number = number + 1 where id = 7"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        Me.Close()
                        MsgBox("Collection Cancelled.")
                        Collection_CR.btnDelete.Hide()
                    End If

                End If

                If Collection_CR.lblStatus.Text = "Posted" Then

                    If Remarks.Text = "" Then
                    Else

                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                        stracs = "insert into CollectionCancelled ([RefNo],[AccountNo],[CRNo],[Remarks],[CancelledBy],[DateCancelled]) values (" _
                            & RefNo.Text & ", '" & AccountNo.Text & "', '" & CRno.Text & "', '" & Remarks.Text & "', '" & My.Settings.Nickname & "', '" _
                            & Format(CancelDate.Value, "yyyy-MM-dd") & "')"

                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        stracs = "update Collection_Details set Cancelled = 'Yes' where CRNo = '" & CRno.Text & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        stracs = "update CollectionBilling set Cancelled = 'Yes' where CRNo = '" & CRno.Text & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        stracs = "update CollectionCharges set Cancelled = 'Yes' where CRNo = '" & CRno.Text & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        stracs = "update Bills set IsPaid = 'No', IsCollectionCreated = 'No', CRNo = NULL where CRNo = '" & CRno.Text & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        stracs = "update BillCharges set IsPaid = 'No', IsCollectionCreated = 'No', CRNo = NULL where CRNo = '" & CRno.Text & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        stracs = "update AddAdjustment set Paid = 'No', IsCollectionCreated = 'No', CRNo = NULL where CRNo = '" & CRno.Text & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        'If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        'stracs = "Update Customers set AdvancePayment = AdvancePayment - " & Double.Parse(Collection_CR.overpayment.Text) & " WHERE AccountNo = '" & AccountNo.Text & "'"
                        'acscmd.CommandText = stracs
                        'acscmd.Connection = acsconn
                        'acscmd.ExecuteNonQuery()

                        getallamount()

                        Dim getadvance As New DataTable
                        stracs = "select (b.AdvancePayment - a.AdvancePayment) as newadvance from Collection_Details a join Customers b on a.AccountNo = b.AccountNo where a.CRNo = '" & CRno.Text & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acsda.SelectCommand = acscmd
                        acsda.Fill(getadvance)

                        stracs = "update Customers set AdvancePayment = " & getadvance.Rows(0)("newadvance") & " where AccountNo = '" & AccountNo.Text & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()


                        stracs = "insert into AccountLedger (ledgerAccountNo, ledgerDate, ledgerRefNo, ledgerParticulars, ledgerReading, ledgerConsumption, ledgerAmount, ledgerDiscount, ledgerBalance) values ('" _
                                    & AccountNo.Text & "', '" _
                                    & Format(Date.Parse(CancelDate.Value), "yyyy-MM-dd") & "', '" _
                                    & RefNo.Text & "', '" _
                                    & "Cancelled Coll.(" & CRno.Text & ")', '" _
                                    & "', '" _
                                    & "', '" _
                                    & Format(Double.Parse(amountdue.Text), "standard") & "', '" _
                                    & "', '" _
                                    & FormatNumber(Double.Parse(totalamount) - getadvance.Rows(0)("newadvance"), UseParensForNegativeNumbers:=TriState.True) & "')"

                        'FormatNumber(Double.Parse(totalamount) - getadvance.Rows(0)("newadvance"), UseParensForNegativeNumbers:=TriState.True)

                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "update tbllogicnumbers set number = number + 1 where id = 7"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        Me.Close()
                        MsgBox("Collection Cancelled.")
                        Collection_CR.btnDelete.Hide()
                    End If

                End If

            End If

        Else
            MsgBox("Your account cannot perform this process.")
        End If



    End Sub

    Sub getallamount()

        Dim gettotalbillbalance As New DataTable
        Dim totalbillbalance As Double = 0

        gettotalbillbalance.Clear()

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        'stracs = "select SUM(AmountDue), S from Bills where AccountNumber = '" & billList.Rows(x).Cells(2).Value & "' and BillStatus = 'Posted' and Cancelled = 'No' and IsPaid = 'No'"
        stracs = "select (isnull(SUM(AmountDue),0) + isnull(SUm(PenaltyAfterDue),0)) - (isnull(SUm(AdvancePayment),0) + isnull(Sum(Discount),0)) as totalbillamount from Bills where AccountNumber = '" & AccountNo.Text & "' and BillStatus = 'Posted' and Cancelled = 'No' and IsPaid = 'No' and isPromisorry = 'No' and IsCollectionCreated = 'No'"
        acscmd.Connection = acsconn
        acscmd.CommandText = stracs
        acsda.SelectCommand = acscmd
        acsda.Fill(gettotalbillbalance)

        If gettotalbillbalance.Rows.Count = 0 Then
            totalbillbalance = 0
        Else

            If IsDBNull(gettotalbillbalance(0)("totalbillamount")) = True Then
                totalbillbalance = 0
            Else
                totalbillbalance = Val(gettotalbillbalance(0)("totalbillamount"))
            End If


        End If

        Dim gettotalchargebalance As New DataTable
        Dim totalbillchargebalance As Double = 0

        gettotalchargebalance.Clear()

        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        stracs = "select SUM(Amount) as amount from BillCharges where AccountNumber = '" & AccountNo.Text & "' and Status = 'Posted' and Cancelled = 'No' and IsPaid = 'No' and Category = 'Others' and isPromisorry = 'No'"
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
        stracs = "select SUM(Billing + Penalty) AS PNTOTAL from AddAdjustment where IsCollectionCreated = 'No' and Status = 'Posted' and Paid = 'No' AND AccountNumber = '" & AccountNo.Text & "'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(gettotalpn)

        If IsDBNull(gettotalpn(0)("PNTOTAL")) = True Then
            totalpn = 0
        Else

            If IsDBNull(gettotalpn(0)("PNTOTAL")) = True Then
                totalpn = 0
            Else
                totalpn = gettotalpn(0)("PNTOTAL")
            End If

            totalpn = gettotalpn(0)("PNTOTAL")
        End If

        totalamount = Double.Parse(totalpn + totalbillchargebalance + totalbillbalance)

    End Sub

End Class