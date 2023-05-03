Public Class ORCancel
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Public cancelmode As String
    Dim totalamount As Double
    Private Sub ORCancel_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        If cancelmode = "Create New" Then

            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Dim getrefno As New DataTable
            stracs = "select number from tbllogicnumbers where id = 10"
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(getrefno)

            RefNo.Text = getrefno(0)("number")
            Label8.Hide()
            CancelDate.Value = Now
            AccountNo.Text = Create_OR.AccountNo.Text
            AccountName.Text = Create_OR.AccName.Text
            CRno.Text = Create_OR.orno.Text
            amountdue.Text = Create_OR.lbltotalamountdue.Text
            paymentfor.Text = Create_OR.paymentfor.Text

        End If

        If cancelmode = "Viewing" Then

            AccountNo.Text = Create_OR.AccountNo.Text
            CRno.Text = Create_OR.orno.Text

            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Dim checking As New DataTable
            checking.Clear()
            stracs = "select * from ORCancelled where AccountNo = '" & AccountNo.Text & "' and ORNo = '" & CRno.Text & "'"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(checking)

            If checking.Rows.Count = 0 Then

            Else

                RefNo.Text = checking(0)("RefNo")

                CancelDate.Value = checking(0)("DateCancelled")
                AccountNo.Text = checking(0)("AccountNo")
                AccountName.Text = Create_OR.AccName.Text
                CRno.Text = checking(0)("ORNo")
                paymentfor.Text = Create_OR.paymentfor.Text
                amountdue.Text = Create_OR.lbltotalamountdue.Text
                Remarks.Text = checking(0)("Remarks")
                Label8.Text = "Cancelled by:" & vbCrLf & checking(0)("CancelledBy")
                Label8.Show()

            End If

        End If

    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click


        If My.Settings.Admin = "Yes" Or My.Settings.Cashier = "Yes" Then

            If Remarks.Text = "" Then

            Else

                Try
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                Catch ex As Exception
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                End Try

                stracs = "insert into ORCancelled ([RefNo],[AccountNo],[ORNo],[PaymentFor],[Remarks],[CancelledBy],[DateCancelled]) values (" _
                    & RefNo.Text & ", '" & AccountNo.Text & "', '" & CRno.Text & "', '" & paymentfor.Text & "', '" & Remarks.Text & "', '" & My.Settings.Nickname & "', '" _
                    & Format(CancelDate.Value, "yyyy-MM-dd") & "')"

                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                stracs = "update OR_Details set Status = 'Posted', Cancelled = 'Yes' where ORNo = '" & CRno.Text & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                stracs = "update ORItems set Cancelled = 'Yes' where ORNo = '" & CRno.Text & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                stracs = "update tbllogicnumbers set number = number + 1 where id = 10"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                getallamount()

                If Create_OR.lblStatus.Text = "Posted" Then

                    stracs = "insert into AccountLedger (ledgerAccountNo, ledgerDate, ledgerRefNo, ledgerParticulars, ledgerReading, ledgerConsumption, ledgerAmount, ledgerDiscount, ledgerBalance) values ('" _
                                    & AccountNo.Text & "', '" _
                                    & Format(Date.Parse(CancelDate.Value), "yyyy-MM-dd") & "', '" _
                                    & RefNo.Text & "', '" _
                                    & "Cancelled OR(" & CRno.Text & ")', '" _
                                    & "', '" _
                                    & "', '" _
                                    & "', '" _
                                    & "', '" _
                                    & "')"

                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                End If


                Me.Close()
                Collection_CR.btnDelete.Hide()
                MsgBox("Collection Cancelled.")

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
        stracs = "select (SUM(AmountDue) + SUm(PenaltyAfterDue)) - (SUm(AdvancePayment) + Sum(Discount)) as totalbillamount from Bills where AccountNumber = '" & AccountNo.Text & "' and BillStatus = 'Posted' and Cancelled = 'No' and IsPaid = 'No' and isPromisorry = 'No'"
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
        Dim totalbill As Decimal = 0

        gettotalchargebalance.Clear()

        If acsconn.State = ConnectionState.Closed Then acsconn.Open()

        sqlData1.Clear()
        stracs = "select SUM(AmountDue + PenaltyAfterDue - Discount) AS BILLTOTAL from Bills where IsPaid = 'No' AND NOT isPromisorry = 'YesPosted' AND AccountNumber = '" & AccountNo.Text & "'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(sqlData1)

        If IsDBNull(sqlData1(0)("BILLTOTAL")) = True Then
            totalbill = 0
        Else
            totalbill = sqlData1(0)("BILLTOTAL")
        End If

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

    Private Sub ORCancel_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Me.Activate()
    End Sub

    Public MoveFormOrCancell As Boolean
    Public MoveFormOrCancell_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormOrCancell = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormOrCancell_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormOrCancell Then
            Me.Location = Me.Location + (e.Location - MoveFormOrCancell_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormOrCancell = False
            Me.Cursor = Cursors.Default
        End If

    End Sub
End Class