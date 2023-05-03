Public Class CollectionPost
    Private Sub CollectionPost_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loaddata()
        Me.MdiParent = eBACSmain
        CRList.Columns(11).ReadOnly = False
    End Sub

    Sub loaddata()

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        Dim postcr As New DataTable
        postcr.Clear()
        stracs = "select * FROM Collection_Details WHERE CollectionStatus = 'Pending' and Cancelled = 'No' and Office = '" & My.Settings.Office_Name & "' order by CollectionID DESC"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(postcr)

        If postcr.Rows.Count = 0 Then
            'MsgBox("No Data found")
            CRList.Rows.Clear()
            totalcollection.Text = "0"
            CheckBox1.Checked = False
        Else

            CRList.Rows.Clear()

            For u = 0 To postcr.Rows.Count - 1
                CRList.Rows.Add(postcr.Rows(u)("CollectionID"), postcr.Rows(u)("CRNo"), postcr.Rows(u)("AccountNo"), postcr.Rows(u)("AccountName"), postcr.Rows(u)("CheckNo"), postcr.Rows(u)("CheckDate") _
                                , FormatNumber(postcr.Rows(u)("TotalAmountDue")), FormatNumber(postcr.Rows(u)("AdvancePayment")), postcr.Rows(u)("PaymentDate"), postcr.Rows(u)("Collector"), postcr.Rows(u)("Office"), False)
            Next
            CheckBox1.Checked = False
            totalcollection.Text = CRList.Rows.Count
        End If

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        If CheckBox1.Checked = True Then

            For q = 0 To CRList.Rows.Count - 1

                CRList.Rows(q).Cells(11).Value = True

            Next

        Else

            For q = 0 To CRList.Rows.Count - 1

                CRList.Rows(q).Cells(11).Value = False

            Next

        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub billSearch_Click(sender As Object, e As EventArgs) Handles billSearch.Click

        Cursor = Cursors.WaitCursor
        If My.Settings.Admin = "Yes" Or My.Settings.Cashier = "Yes" Then

            Cursor = Cursors.WaitCursor

            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try

            If CRList.Rows.Count = 0 Then

            Else

                Dim totalbill As Decimal = 0
                Dim totalbillcharges As Decimal = 0
                Dim totalpn As Decimal = 0
                Dim ledgertotal As Decimal = 0


                For i = 0 To CRList.Rows.Count - 1
                    If CRList.Rows(i).Cells(11).Value = True Then

                        Dim getadvance As New DataTable
                        stracs = "select AdvancePayment from Customers where AccountNo = '" & CRList.Rows(i).Cells(2).Value & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acsda.SelectCommand = acscmd
                        acsda.Fill(getadvance)

                        'update Collection_Details collection status
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "Update Collection_Details Set CollectionStatus = 'Posted' WHERE CollectionID = " & CRList.Rows(i).Cells(0).Value
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()


                        'update CollectionBilling collection status
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "Update CollectionBilling set CollectionBillingStatus = 'Posted' WHERE CRNo = '" & CRList.Rows(i).Cells(1).Value & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()

                        'update CollectionCharges collection status
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "Update CollectionCharges set CollectionChargesStatus = 'Posted' WHERE CRNo = '" & CRList.Rows(i).Cells(1).Value & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()

                        'update Concessionaire advance payment 
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "Update Customers set AdvancePayment = AdvancePayment + " & Double.Parse(CRList.Rows(i).Cells(7).Value) & " WHERE AccountNo = '" & CRList.Rows(i).Cells(2).Value & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()

                        'update Addjustment status and is paid
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "Update AddAdjustment set Paid = 'Yes', DatePaid = '" & Format(Date.Parse(CRList.Rows(i).Cells(8).Value), "yyyy-MM-dd") & "' WHERE CRNo = '" & CRList.Rows(i).Cells(1).Value & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()


                        'Bills
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "Update Bills set IsPaid = 'Yes', DatePaid = '" & Format(Date.Parse(CRList.Rows(i).Cells(8).Value), "yyyy-MM-dd") & "' WHERE CRNo = '" & CRList.Rows(i).Cells(1).Value & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()

                        'Billcharges
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "Update BillCharges set IsPaid = 'Yes' WHERE CRNo = '" & CRList.Rows(i).Cells(1).Value & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()


                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                        sqlData1.Clear()
                        stracs = "select (SUM(AmountDue) + SUM(PenaltyAfterDue) + SUM(Adjustment)) - (SUM(Discount) + SUM(AdvancePayment)) AS BILLTOTAL from Bills where BillStatus = 'Posted' and Cancelled = 'No' and IsCollectionCreated = 'No' and IsPaid = 'No' AND NOT isPromisorry = 'YesPosted' AND AccountNumber = '" & CRList.Rows(i).Cells(2).Value & "'"
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

                        sqlData1.Clear()
                        stracs = "select SUM(Amount) AS BILLChargesTOTAL from BillCharges where IsPaid = 'No' AND Category = 'Others' AND AccountNumber = '" & CRList.Rows(i).Cells(2).Value & "' and Status = 'Posted' and Cancelled = 'No' and IsCollectionCreated = 'No'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acsda.SelectCommand = acscmd
                        acsda.Fill(sqlData1)

                        If IsDBNull(sqlData1(0)("BILLChargesTOTAL")) = True Then
                            totalbillcharges = 0
                        Else
                            totalbillcharges = sqlData1(0)("BILLChargesTOTAL")
                        End If

                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                        sqlData1.Clear()
                        stracs = "select SUM(Billing + Penalty) AS PNTOTAL from AddAdjustment where Paid = 'No' AND AccountNumber = '" & CRList.Rows(i).Cells(2).Value & "' and IsCollectionCreated = 'No' and Status = 'Posted'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acsda.SelectCommand = acscmd
                        acsda.Fill(sqlData1)

                        If IsDBNull(sqlData1(0)("PNTOTAL")) = True Then
                            totalpn = 0
                        Else
                            totalpn = sqlData1(0)("PNTOTAL")
                        End If

                        ledgertotal = (totalpn + totalbill + totalbillcharges) - (Double.Parse(CRList.Rows(i).Cells(7).Value) + getadvance.Rows(0)("AdvancePayment"))
                        sqlData1.Clear()

                        'query Bills base on CRNo
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        Dim billlasttransaction As New DataTable
                        billlasttransaction.Clear()

                        stracs = "select * from Bills where CRNo= '" & CRList.Rows(i).Cells(1).Value & "' order by BillID desc"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acsda.SelectCommand = acscmd
                        acsda.Fill(billlasttransaction)

                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        Dim sqlsurcharge As New DataTable
                        sqlsurcharge.Clear()

                        stracs = "select Surcharge, earlyPaymentDiscount from Bills where CRNo = '" & billlasttransaction(0)("CRNo") & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acsda.SelectCommand = acscmd
                        acsda.Fill(sqlsurcharge)

                        'get collection charges
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        Dim sqlCollectionCharges As New DataTable
                        sqlCollectionCharges.Clear()

                        stracs = "select * from CollectionCharges where BillNo = '" & billlasttransaction(0)("BillNo") & "' AND CRNo='" & billlasttransaction(0)("CRNo") & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acsda.SelectCommand = acscmd
                        acsda.Fill(sqlCollectionCharges)

                        'update ledger discount if there is any
                        'get total balance

                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        Dim total_bill As Double
                        Dim total_billcharges As Double
                        Dim total_pn As Double


                        sqlData1.Clear()
                        stracs = "select (SUM(AmountDue) + SUM(PenaltyAfterDue) + SUM(Adjustment)) - (SUM(Discount) + SUM(AdvancePayment)) AS BILLTOTAL from Bills where CRNo = '" & CRList.Rows(i).Cells(1).Value & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acsda.SelectCommand = acscmd
                        acsda.Fill(sqlData1)

                        If IsDBNull(sqlData1(0)("BILLTOTAL")) = True Then
                            total_bill = 0
                        Else
                            total_bill = sqlData1(0)("BILLTOTAL")
                        End If

                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                        sqlData1.Clear()
                        stracs = "select SUM(Amount) AS BILLChargesTOTAL from BillCharges where CRNo = '" & CRList.Rows(i).Cells(1).Value & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acsda.SelectCommand = acscmd
                        acsda.Fill(sqlData1)

                        If IsDBNull(sqlData1(0)("BILLChargesTOTAL")) = True Then
                            total_billcharges = 0
                        Else
                            total_billcharges = sqlData1(0)("BILLChargesTOTAL")
                        End If

                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                        sqlData1.Clear()
                        stracs = "select SUM(Billing + Penalty) AS PNTOTAL from AddAdjustment where CRNo = '" & CRList.Rows(i).Cells(1).Value & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acsda.SelectCommand = acscmd
                        acsda.Fill(sqlData1)

                        If IsDBNull(sqlData1(0)("PNTOTAL")) = True Then
                            total_pn = 0
                        Else
                            total_pn = sqlData1(0)("PNTOTAL")
                        End If

                        Dim bbalance As Double
                        'Dim dbalance As Double
                        Dim early_paymentdiscount As Double = 0.00
                        For counter As Integer = 0 To sqlsurcharge.Rows.Count - 1
                            Dim early_payment_disc As Double = sqlsurcharge(counter)("earlyPaymentDiscount")
                            early_paymentdiscount = early_paymentdiscount + early_payment_disc
                        Next

                        bbalance = (total_pn + total_bill + total_billcharges) - early_paymentdiscount
                        'bbalance = total_pn + total_bill + total_billcharges

                        If (early_paymentdiscount = 0.00) Then

                        Else
                            If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                            stracs = "insert into AccountLedger (ledgerAccountNo, ledgerDate, ledgerRefNo, ledgerParticulars, ledgerReading, ledgerConsumption, ledgerAmount, ledgerDiscount, ledgerBalance, ledgerCancelled) values ('" _
                                        & CRList.Rows(i).Cells(2).Value & "', '" & Format(Date.Parse(CRList.Rows(i).Cells(8).Value), "yyyy-MM-dd") & "', '" & billlasttransaction(0)("BillNo") & "', 'Early payment discount', '', '', '', '" & Math.Round(early_paymentdiscount, 2).ToString("0.00") & "', '" & Math.Round(bbalance, 2).ToString("0.00") & "', 'No')"
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acscmd.ExecuteNonQuery()
                            acscmd.Dispose()
                        End If


                        'If (sqlsurcharge(0)("Surcharge") = 0.00) Then
                        'Else
                        '    dbalance = Math.Round(sqlsurcharge(0)("Surcharge") + bbalance, 2)

                        '    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        '        stracs = "insert into AccountLedger (ledgerAccountNo, ledgerDate, ledgerRefNo, ledgerParticulars, ledgerReading, ledgerConsumption, ledgerAmount, ledgerDiscount, ledgerBalance, ledgerCancelled) values ('" _
                        '                    & CRList.Rows(i).Cells(2).Value & "', '" & Format(Date.Parse(CRList.Rows(i).Cells(8).Value), "yyyy-MM-dd") & "', '" & billlasttransaction(0)("BillNo") & "', 'Surcharge', '', '', '" & sqlsurcharge(0)("Surcharge") & "', '', '" & dbalance & "', 'No')"
                        '        acscmd.CommandText = stracs
                        '        acscmd.Connection = acsconn
                        '        acscmd.ExecuteNonQuery()
                        '        acscmd.Dispose()
                        '    End If

                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                        'bbalance = (total_pn + total_bill + total_billcharges) - sqlsurcharge(0)("earlyPaymentDiscount")

                        For counter As Integer = 0 To sqlCollectionCharges.Rows.Count - 1
                            Dim particularss As String = sqlCollectionCharges.Rows(counter)("Particulars")

                            If (particularss = "Reconnection Fee" Or particularss = "Surcharge") Then
                                bbalance = Math.Round(bbalance + sqlCollectionCharges.Rows(counter)("Amount"), 2)
                                stracs = "insert into AccountLedger (ledgerAccountNo, ledgerDate, ledgerRefNo, ledgerParticulars, ledgerReading, ledgerConsumption, ledgerAmount, ledgerDiscount, ledgerBalance, ledgerCancelled) values ('" _
                                   & CRList.Rows(i).Cells(2).Value & "', '" & Format(Date.Parse(CRList.Rows(i).Cells(8).Value), "yyyy-MM-dd") & "', '" & billlasttransaction(0)("BillNo") & "', '" & sqlCollectionCharges.Rows(counter)("Particulars") & "', '', '', '" & sqlCollectionCharges.Rows(counter)("Amount") & "', '', '" & bbalance.ToString("0.00") & "', 'No')"
                            End If

                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acscmd.ExecuteNonQuery()
                        Next
                        acscmd.Dispose()

                        'PAID ZERO ON LEDGER'
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                            stracs = "INSERT INTO AccountLedger (ledgerAccountNo,ledgerDate,ledgerRefNo,ledgerParticulars,ledgerReading,ledgerConsumption,ledgerAmount,ledgerDiscount,ledgerBalance)
                        Values ('" & CRList.Rows(i).Cells(2).Value & "','" & Format(Date.Parse(CRList.Rows(i).Cells(8).Value), "yyyy-MM-dd") & "',
                        '" & CRList.Rows(i).Cells(1).Value & "','Collection', '', '', '',
                        '" & Format(Double.Parse(CRList.Rows(i).Cells(6).Value), "Standard") & "','" & FormatNumber(ledgertotal, UseParensForNegativeNumbers:=TriState.True) & "')"
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acscmd.ExecuteNonQuery()

                        End If

                Next

                loaddata()

            End If

            Cursor = Cursors.Default

        Else
            MsgBox("Your account cannot perform this process.")
        End If

        Cursor = Cursors.Default
    End Sub

    Private Sub CollectionPost_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Public MoveFormcrpost As Boolean
    Public MoveFormcrpost_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormcrpost = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormcrpost_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormcrpost Then
            Me.Location = Me.Location + (e.Location - MoveFormcrpost_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormcrpost = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Me.Activate()
    End Sub

    Private Sub CollectedPayments_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub postbill_deactivated(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.BackColor = Color.FromArgb(17, 153, 195)
    End Sub

    Private Sub postbill_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.BackColor = Color.SteelBlue
    End Sub

    Private Sub ControlsClick(sender As Object, e As EventArgs) Handles _
        Panel1.Click, CheckBox1.Click, CRList.Click, billSearch.Click  ' etc.
        Me.Activate() 'Or Whatever
    End Sub

End Class