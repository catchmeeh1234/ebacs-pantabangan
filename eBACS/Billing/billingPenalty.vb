Public Class billingPenalty

    Private Sub billingPenalty_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        'lastdaynopen.Text = Now

        penaltymodify.CheckState = CheckState.Unchecked
        penaltyList.Rows.Clear()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Public Sub prepare_Click(sender As Object, e As EventArgs) Handles prepare.Click

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        Dim getbills As New DataTable

        stracs = "select a.[BillNo],a.[AccountNumber],a.[CustomerName] from Bills a join Customers b on a.AccountNumber = b.AccountNo where 
               a.BillingDate = '" & billMonth.Text & " " & billYear.Text & "' and " _
                & "a.LastDayNOPen = '" & Format(Date.Parse(lastdaynopen.Text), "yyyy-MM-dd") & "' and " _
                & "a.IsPaid = 'No' and a.IsCollectionCreated = 'No' and a.isPromisorry = 'No' and a.Cancelled = 'No' 
                and a.PenaltyAfterDue = 0 and a.BillStatus = 'Posted' and b.CustomerStatus = 'Active' and a.DontCharge = 'No' order by BillNo"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(getbills)

        billcount.Text = getbills.Rows.Count
        penaltyList.Rows.Clear()

        If penaltymodify.CheckState = CheckState.Checked Then

            For i = 0 To getbills.Rows.Count - 1

                Dim getbillingarrears As New DataTable
                stracs = "select (SUM(AmountDue) + SUM(PenaltyAfterDue) + SUM(Adjustment)) - SUM(AdvancePayment) as billarrears 
                     from Bills where AccountNumber = '" & getbills.Rows(i)("AccountNumber") & "' and " _
                        & "IsPaid = 'No' and IsCollectionCreated = 'No' and isPromisorry = 'No' and Cancelled = 'No' and BillStatus = 'Posted' and LastDayNOPen <= '" _
                        & Format(Date.Parse(lastdaynopen.Text), "yyyy-MM-dd") & "' and 
                    ReadingDate >= '" & Format(Date.Parse(datefrom.Text), "yyyy-MM-dd") & "' and 
                    ReadingDate <= '" & Format(Date.Parse(dateto.Text), "yyyy-MM-dd") & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(getbillingarrears)

                Dim billarrears As Double

                If IsDBNull(getbillingarrears.Rows(0)("billarrears")) = True Or getbillingarrears.Rows(0)("billarrears") <= 0 Then
                    billarrears = 0
                Else
                    billarrears = Double.Parse(getbillingarrears(0)("billarrears"))
                End If

                Dim getbillingpn As New DataTable
                stracs = "Select (SUM(Billing) + SUM(Penalty)) As billpn 
                        from AddAdjustment where AccountNumber = '" & getbills.Rows(i)("AccountNumber") & "' and " _
                        & "Paid = 'No' and IsCollectionCreated = 'No' and Status = 'Posted' and cast(BillingDate as date) <= '" & Date.Parse(billMonth.Text & " " & billYear.Text) & "'"
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

                If (billarrears + totalpn) <= 0 Then
                Else
                    penaltyList.Rows.Add(getbills.Rows(i)("BillNo"), getbills.Rows(i)("AccountNumber"), getbills.Rows(i)("CustomerName"), Format(billarrears + totalpn, "standard"), Format((billarrears + totalpn) * 0.05, "standard"), -1)
                End If

            Next

        Else

            For i = 0 To getbills.Rows.Count - 1


                Dim getbillingarrears As New DataTable
                stracs = "select ((SUM(AmountDue) + SUM(PenaltyAfterDue) + SUM(Adjustment)) - SUM(AdvancePayment)) as billarrears 
                        from Bills where AccountNumber = '" & getbills.Rows(i)("AccountNumber") & "' and BillStatus = 'Posted' and " _
                        & "IsPaid = 'No' and IsCollectionCreated = 'No' and isPromisorry = 'No' and Cancelled = 'No' and LastDayNOPen <= '" & Format(Date.Parse(lastdaynopen.Text), "yyyy-MM-dd") & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(getbillingarrears)

                'Dim billarrears As Double = Double.Parse(getbillingarrears(0)("billarrears"))

                Dim billarrears As Double

                If IsDBNull(getbillingarrears.Rows(0)("billarrears")) = True Or getbillingarrears.Rows(0)("billarrears") <= 0 Then
                    billarrears = 0
                Else
                    billarrears = Double.Parse(getbillingarrears(0)("billarrears"))
                End If

                Dim getbillingpn As New DataTable

                stracs = "select (SUM(Billing) + SUM(Penalty)) as billpn 
                        from AddAdjustment where AccountNumber = '" & getbills.Rows(i)("AccountNumber") & "' and " _
                        & "Paid = 'No' and IsCollectionCreated = 'No' and Status = 'Posted' and cast(BillingDate as date) <= '" & Date.Parse(billMonth.Text & " " & billYear.Text) & "'"
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

                If (billarrears + totalpn) <= 0 Then
                Else
                    penaltyList.Rows.Add(getbills.Rows(i)("BillNo"), getbills.Rows(i)("AccountNumber"), getbills.Rows(i)("CustomerName"), Format(billarrears + totalpn, "standard"), Format((billarrears + totalpn) * 0.05, "standard"), -1)
                End If

            Next

        End If

    End Sub

    Private Sub postpenalty_Click(sender As Object, e As EventArgs) Handles postpenalty.Click

        ProgressBar1.Value = 0
        ProgressBar1.Maximum = penaltyList.Rows.Count
        ProgressBar1.Visible = True

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        For o = 0 To penaltyList.Rows.Count - 1

            If penaltyList.Rows(o).Cells(5).Value = -1 And penaltyList.Rows(o).Cells(4).Value > 0 Then

                'MsgBox(penaltyList.Rows(o).Cells(4).Value)

                Dim arrearsbill As Double = 0
                Dim arrearscharge As Double = 0

                Dim loadbillarrears As New DataTable
                loadbillarrears.Clear()

                stracs = "select (SUM(AmountDue) + SUM(PenaltyAfterDue)) - (SUM(AdvancePayment) + SUM(Discount)) as amountarrears 
                        from Bills where IsPaid = 'No' and Cancelled = 'No' and AccountNumber = '" _
                        & penaltyList.Rows(o).Cells(1).Value & "' and BillStatus = 'Posted' and IsCollectionCreated = 'No' and isPromisorry = 'No'"
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
                        AccountNumber = '" & penaltyList.Rows(o).Cells(1).Value & "' and Status = 'Posted' and Category = 'Others'
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
                     from AddAdjustment where AccountNumber = '" & penaltyList.Rows(o).Cells(1).Value & "' and " _
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
                        & penaltyList.Rows(o).Cells(1).Value & "', '" _
                        & Format(Date.Parse(Now), "yyyy-MM-dd") & "', '" _
                        & penaltyList.Rows(o).Cells(0).Value & "', '" _
                        & "Penalty', '" _
                        & "', '" _
                        & "', '" _
                        & "', '" _
                        & penaltyList.Rows(o).Cells(4).Value & "', '" _
                        & Format(Val(arrearsbill) + Val(arrearscharge + totalpn) + Double.Parse(penaltyList.Rows(o).Cells(4).Value), "Standard") & "')"

                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                stracs = "update Bills set PenaltyAfterDue = " & Double.Parse(penaltyList.Rows(o).Cells(4).Value) & " where BillNo = " & penaltyList.Rows(o).Cells(0).Value
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

            Else

            End If

            ProgressBar1.Value = ProgressBar1.Value + 1

        Next

        penaltyList.Rows.Clear()
        MsgBox("Penalty posted.")
        ProgressBar1.Hide()

    End Sub

    Private Sub modify_CheckedChanged(sender As Object, e As EventArgs) Handles penaltymodify.CheckedChanged

        If penaltymodify.CheckState = CheckState.Checked Then
            dateCovered.Enabled = True
        Else
            dateCovered.Enabled = False
        End If

    End Sub

    Private Sub datefrom_ValueChanged(sender As Object, e As EventArgs) Handles datefrom.ValueChanged

        If Date.Parse(Format(datefrom.Value, "short date")) > Date.Parse(Format(dateto.Value, "short date")) Then

            MsgBox("The date should not less than date from.")
            datefrom.Value = Now

        Else

        End If

    End Sub

    Private Sub dateto_ValueChanged(sender As Object, e As EventArgs) Handles dateto.ValueChanged

        If Date.Parse(Format(datefrom.Value, "short date")) > Date.Parse(Format(dateto.Value, "short date")) Then

            MsgBox("The date should not less than date from.")
            dateto.Value = Now

        Else

        End If

    End Sub

    Private Sub billingPenalty_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Me.Activate()
    End Sub

    Public MoveFormbillingpenalty As Boolean
    Public Movebillingpenalty_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormbillingpenalty = True
            Me.Cursor = Cursors.NoMove2D
            Movebillingpenalty_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormbillingpenalty Then
            Me.Location = Me.Location + (e.Location - Movebillingpenalty_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormbillingpenalty = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.CheckState = CheckState.Checked Then
            CheckBox1.Text = "Uncheck all"
            If penaltyList.Rows.Count > 0 Then



                For x = 0 To penaltyList.Rows.Count - 1

                    If penaltyList.Rows(x).Cells(5).Value < 0 Then

                    Else
                        penaltyList.Rows(x).Cells(5).Value = -1
                    End If

                Next

            Else

            End If

        Else
            CheckBox1.Text = "Check all"
            If penaltyList.Rows.Count > 0 Then



                For x = 0 To penaltyList.Rows.Count - 1
                    penaltyList.Rows(x).Cells(5).Value = 0
                Next

            Else

            End If

        End If
    End Sub

    Private Sub penalty_deactivated(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.BackColor = Color.FromArgb(17, 153, 195)
    End Sub

    Private Sub penalty_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.BackColor = Color.SteelBlue
    End Sub

    Private Sub ControlsClick(sender As Object, e As EventArgs) Handles _
        Panel1.Click, billMonth.Click, billYear.Click, lastdaynopen.Click, penaltymodify.Click, dateCovered.Click, datefrom.Click,
        dateto.Click, prepare.Click, postpenalty.Click, penaltyList.Click, CheckBox1.Click ' etc.
        Me.Activate() 'Or Whatever
    End Sub



End Class