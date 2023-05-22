Public Class billinginfo
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub billinginfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain
    End Sub

    Dim sqldata2 As New DataTable
    Dim testzne As String

    Private Sub billingdetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
        billStatus.Text = "All"
        lblStatus.ContextMenuStrip = cancelstrip
        loadzones()

        Dim autocomplete As New DataTable

        autocomplete.Clear()

        stracs = "SELECT AccountNo FROM Customers"
        acscmd.Connection = acsconn
        acscmd.CommandText = stracs
        acsda.SelectCommand = acscmd
        acsda.Fill(autocomplete)

        accountno.AutoCompleteCustomSource.Clear()

        accountno.AutoCompleteMode = AutoCompleteMode.None
        accountno.AutoCompleteSource = AutoCompleteSource.None

        For x = 0 To autocomplete.Rows.Count - 1
            accountno.AutoCompleteCustomSource.Add(autocomplete.Rows(x)("AccountNo"))
        Next


        accountno.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        accountno.AutoCompleteSource = AutoCompleteSource.CustomSource

    End Sub

    Sub loadzones()

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

    Private Sub billSearch_Click(sender As Object, e As EventArgs) Handles billSearch.Click

        SearchAccount.searchingform = "Bills"
        SearchAccount.searchid = "searchbill"
        SearchAccount.ShowDialog()

    End Sub

    Private Sub billZone_SelectedIndexChanged(sender As Object, e As EventArgs) Handles billZone.SelectedIndexChanged

        searchbyzonedate()

    End Sub

    Sub searchbyzonedate()

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        sqldataZone.Clear()

        If billZone.Text = "All" Then


            If billStatus.Text = "All" Then
                stracs = "select * from Bills where BIllingDate = '" & billMonth.Text & " " & billYear.Text & "' order by BILLID"
            Else

                If billStatus.Text = "Cancelled" Then
                    stracs = "select * from Bills where BIllingDate = '" & billMonth.Text & " " & billYear.Text & "' and Cancelled = 'Yes' order by BILLID"
                ElseIf billStatus.Text = "Unread" Then
                    stracs = "select * from Bills where BIllingDate = '" & billMonth.Text & " " & billYear.Text & "' and Reading = 0 and AmountDue = 0.00 order by BILLID"
                Else

                    stracs = "select * from Bills where BIllingDate = '" & billMonth.Text & " " & billYear.Text & "' and BillStatus = '" & billStatus.Text & "' and Cancelled = 'No' order by BILLID"
                End If

            End If

        Else

            If billStatus.Text = "All" Then
                stracs = "select * from Bills where Zone = '" & billZone.Text.ToString.Replace("'", "''") & "' and BIllingDate = '" & billMonth.Text & " " & billYear.Text & "' order by BILLID"
            Else
                If billStatus.Text = "Cancelled" Then
                    stracs = "select * from Bills where Zone = '" & billZone.Text.ToString.Replace("'", "''") & "' and BIllingDate = '" & billMonth.Text & " " & billYear.Text & "' and Cancelled = 'Yes' order by BILLID"

                ElseIf billStatus.Text = "Unread" Then
                    stracs = "select * from Bills where Zone = '" & billZone.Text.ToString.Replace("'", "''") & "' and BIllingDate = '" & billMonth.Text & " " & billYear.Text & "' and Reading = 0 and AmountDue = 0.00 order by BILLID"
                Else

                    stracs = "select * from Bills where Zone = '" & billZone.Text.ToString.Replace("'", "''") & "' and BIllingDate = '" & billMonth.Text & " " & billYear.Text & "' and BillStatus = '" & billStatus.Text & "' and Cancelled = 'No' order by BILLID"
                End If

            End If

        End If

        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(sqldataZone)

        billList.Rows.Clear()

        For i = 0 To sqldataZone.Rows.Count - 1

            If i Mod 2 = 0 Then

                If IsDBNull(sqldataZone(i)("ReadingDate")) = True Then

                    sqldataZone(i)("ReadingDate") = sqldataZone(i)("DateFrom")

                Else
                    sqldataZone(i)("ReadingDate") = sqldataZone(i)("ReadingDate")
                End If

                If sqldataZone(i)("Cancelled") = "Yes" Then

                    billList.Rows.Add(sqldataZone(i)("BillNo"), Format(sqldataZone(i)("ReadingDate"), "short date"), sqldataZone(i)("CustomerName"), sqldataZone(i)("Consumption"), "Cancelled")

                Else
                    If sqldataZone(i)("isPaid") = "Yes" Then
                        billList.Rows.Add(sqldataZone(i)("BillNo"), Format(sqldataZone(i)("ReadingDate"), "short date"), sqldataZone(i)("CustomerName"), sqldataZone(i)("Consumption"), "Paid")
                    Else
                        If sqldataZone(i)("isPromisorry") = "YesPosted" Then
                            billList.Rows.Add(sqldataZone(i)("BillNo"), Format(sqldataZone(i)("ReadingDate"), "short date"), sqldataZone(i)("CustomerName"), sqldataZone(i)("Consumption"), "PN")
                        Else
                            billList.Rows.Add(sqldataZone(i)("BillNo"), Format(sqldataZone(i)("ReadingDate"), "short date"), sqldataZone(i)("CustomerName"), sqldataZone(i)("Consumption"), sqldataZone(i)("BillStatus"))
                        End If

                    End If
                End If

                billList.Rows(i).DefaultCellStyle.BackColor = Color.Gainsboro

            Else

                If IsDBNull(sqldataZone(i)("ReadingDate")) = True Then

                    sqldataZone(i)("ReadingDate") = sqldataZone(i)("DateFrom")

                Else
                    sqldataZone(i)("ReadingDate") = sqldataZone(i)("ReadingDate")
                End If

                If sqldataZone(i)("Cancelled") = "Yes" Then

                    billList.Rows.Add(sqldataZone(i)("BillNo"), Format(sqldataZone(i)("ReadingDate"), "short date"), sqldataZone(i)("CustomerName"), sqldataZone(i)("Consumption"), "Cancelled")

                Else
                    If sqldataZone(i)("isPaid") = "Yes" Then
                        billList.Rows.Add(sqldataZone(i)("BillNo"), Format(sqldataZone(i)("ReadingDate"), "short date"), sqldataZone(i)("CustomerName"), sqldataZone(i)("Consumption"), "Paid")
                    Else
                        If sqldataZone(i)("isPromisorry") = "YesPosted" Then
                            billList.Rows.Add(sqldataZone(i)("BillNo"), Format(sqldataZone(i)("ReadingDate"), "short date"), sqldataZone(i)("CustomerName"), sqldataZone(i)("Consumption"), "PN")
                        Else
                            billList.Rows.Add(sqldataZone(i)("BillNo"), Format(sqldataZone(i)("ReadingDate"), "short date"), sqldataZone(i)("CustomerName"), sqldataZone(i)("Consumption"), sqldataZone(i)("BillStatus"))
                        End If

                    End If
                End If

                billList.Rows(i).DefaultCellStyle.BackColor = Color.White

            End If

        Next

        billTotalbill.Text = sqldataZone.Rows.Count

    End Sub

    Private Sub billMonth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles billMonth.SelectedIndexChanged
        searchbyzonedate()
    End Sub

    Private Sub billYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles billYear.SelectedIndexChanged
        searchbyzonedate()
    End Sub

    Private Sub billStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles billStatus.SelectedIndexChanged
        searchbyzonedate()
    End Sub

    Sub loadreaders()

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        Dim readerdata As New DataTable
        readerdata.Clear()
        stracs = "select distinct(MeterReader) as MeterReader from Bills group by MeterReader"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(readerdata)

        If readerdata.Rows.Count = 0 Then
        Else

            meterreader.Items.Clear()
            For x = 0 To readerdata.Rows.Count - 1
                meterreader.Items.Add(readerdata.Rows(x)("MeterReader"))
            Next
        End If

    End Sub

    Sub loadreadersnew()

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        Dim readerdata As New DataTable
        readerdata.Clear()
        stracs = "select readerName from MeterReader"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(readerdata)

        If readerdata.Rows.Count = 0 Then
        Else

            meterreader.Items.Clear()
            For x = 0 To readerdata.Rows.Count - 1
                meterreader.Items.Add(readerdata.Rows(x)("readerName"))
            Next
        End If

    End Sub

    Public Sub billBillno_KeyDown(sender As Object, e As KeyEventArgs) Handles billBillno.KeyDown

        If e.KeyCode = Keys.Enter Then

            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try

            If IsNumeric(billBillno.Text) = True Then

                sqldataBilling.Clear()
                stracs = "select * from Bills where BillNo = " & billBillno.Text
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                'acsdr = acscmd.ExecuteReader
                acsda.SelectCommand = acscmd
                acsda.Fill(sqldataBilling)

                If sqldataBilling.Rows.Count = 0 Then
                    MsgBox("No record found.")
                Else

                    testzne = sqldataBilling.Rows(0)("Zone")

                    If sqldataBilling(0)("Cancelled") = "Yes" Then
                        lblStatus.Text = "Cancelled"
                        ToolTip1.RemoveAll()
                    Else

                        If sqldataBilling(0)("isPromisorry") = "YesPosted" Then

                            lblStatus.Text = "PN"
                            ToolTip1.RemoveAll()

                        Else

                            If sqldataBilling(0)("IsPaid") = "Yes" Then
                                lblStatus.Text = "Paid"

                                ToolTip1.SetToolTip(lblStatus, sqldataBilling(0)("CRNo") & " - " & sqldataBilling(0)("DatePaid"))

                            Else

                                lblStatus.Text = sqldataBilling(0)("BillStatus")
                                ToolTip1.RemoveAll()

                            End If

                        End If

                    End If

                    If sqldataBilling(0)("BillStatus") = "Pending" Then
                        lblStatus.ForeColor = Color.Orange
                        posting.Show()
                    End If

                    If sqldataBilling(0)("BillStatus") = "Posted" Then
                        lblStatus.ForeColor = Color.Green
                        posting.Hide()
                    End If

                    If lblStatus.Text = "Cancelled" Then
                        lblStatus.ForeColor = Color.Red
                        posting.Hide()
                    End If

                    billAccountNo.Text = sqldataBilling(0)("AccountNumber")
                    billName.Text = sqldataBilling(0)("CustomerName")
                    billAddress.Text = sqldataBilling(0)("CustomerAddress")

                    If IsDBNull(sqldataBilling(0)("MeterNumber")) = True Then
                        billMeterno.Text = ""
                    Else
                        billMeterno.Text = sqldataBilling(0)("MeterNumber")
                    End If


                    loadreaders()

                    billcurrent.Text = sqldataBilling(0)("Reading")
                    billprevious.Text = sqldataBilling(0)("PreviousReading")
                    billconsumption.Text = sqldataBilling(0)("Consumption")
                    meterreader.Text = sqldataBilling(0)("MeterReader")

                    If IsDBNull(sqldataBilling(0)("AverageCons")) = True Then
                        billaverage.Text = ""
                    Else
                        billaverage.Text = sqldataBilling(0)("AverageCons")
                    End If

                    If IsDBNull(sqldataBilling(0)("ReadingDate")) = True Then

                        sqldataBilling(0)("ReadingDate") = Format(Now, "Short date")

                    Else
                        sqldataBilling(0)("ReadingDate") = sqldataBilling(0)("ReadingDate")
                    End If

                    If IsDBNull(sqldataBilling(0)("LastDayNoPen")) = True Then
                        Dim lastDayNoPen As DateTime = Date.Parse(sqldataBilling(0)("ReadingDate")).AddDays(11)

                        If lastDayNoPen.DayOfWeek = DayOfWeek.Saturday Then
                            lastDayNoPen = lastDayNoPen.AddDays(2) ' Move to Monday
                        ElseIf lastDayNoPen.DayOfWeek = DayOfWeek.Sunday Then
                            lastDayNoPen = lastDayNoPen.AddDays(1) ' Move to Monday
                        End If
                        sqldataBilling(0)("LastDayNoPen") = lastDayNoPen

                    Else
                        sqldataBilling(0)("LastDayNoPen") = sqldataBilling(0)("LastDayNoPen")
                    End If

                    If IsDBNull(sqldataBilling(0)("DueDate")) = True Then
                        Dim dueDate As DateTime = Date.Parse(sqldataBilling(0)("ReadingDate")).AddDays(11)

                        If dueDate.DayOfWeek = DayOfWeek.Saturday Then
                            dueDate = dueDate.AddDays(2) ' Move to Monday
                        ElseIf dueDate.DayOfWeek = DayOfWeek.Sunday Then
                            dueDate = dueDate.AddDays(1) ' Move to Monday
                        End If

                        sqldataBilling(0)("DueDate") = dueDate

                    Else
                        sqldataBilling(0)("DueDate") = sqldataBilling(0)("DueDate")
                    End If



                    billCovered.Text = sqldataBilling(0)("BillingDate")
                    billdatefrom.Value = Format(sqldataBilling(0)("DateFrom"), "short date")
                    billdateto.Value = Format(sqldataBilling(0)("ReadingDate"), "short date")
                    billdateduedate.Value = Format(sqldataBilling(0)("DueDate"), "short date")
                    billdatelastday.Value = Format(sqldataBilling(0)("LastDayNoPen"), "short date")

                    If IsDBNull(sqldataBilling(0)("isSenior")) = True Then
                        billsenior.CheckState = CheckState.Unchecked
                    Else
                        If sqldataBilling(0)("isSenior") = "Yes" Then
                            billsenior.CheckState = CheckState.Checked
                        Else
                            billsenior.CheckState = CheckState.Unchecked
                        End If
                    End If

                    If sqldataBilling(0)("DontCharge") = "Yes" Then
                        billdontcharge.CheckState = CheckState.Checked
                    Else
                        billdontcharge.CheckState = CheckState.Unchecked
                    End If

                    If sqldataBilling(0)("Cancelled") = "Yes" Then
                        billcancelled.CheckState = CheckState.Checked
                    Else
                        billcancelled.CheckState = CheckState.Unchecked
                    End If

                    billamountdue.Text = Format(sqldataBilling(0)("AmountDue"), "standard")
                    billdiscount.Text = Format(sqldataBilling(0)("Discount"), "standard")
                    billPenalty.Text = Format(sqldataBilling(0)("PenaltyAfterDue"), "standard")
                    billadvancepayment.Text = Format(sqldataBilling(0)("AdvancePayment"), "standard")

                    If IsDBNull(sqldataBilling(0)("Adjustment")) = True Then
                        billAdjustment.Text = "0.00"

                    Else
                        billAdjustment.Text = Format(sqldataBilling(0)("Adjustment"), "standard")
                    End If



                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    sqldataBilling.Clear()
                    stracs = "select * from BillCharges where BillNumber = " & billBillno.Text
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsda.SelectCommand = acscmd
                    acsda.Fill(sqldataBilling)

                    billcharges.Rows.Clear()

                    'Dim x As String = 0
                    'Dim totalamountdue As Double = 0
                    'Dim totalcharges As Double = 0
                    'Dim totalarrears As Double = 0

                    For i = 0 To sqldataBilling.Rows.Count - 1

                        billcharges.Rows.Add(sqldataBilling(i)("ChargeID"), sqldataBilling(i)("Category"), sqldataBilling(i)("Entry"), sqldataBilling(i)("Particulars"), Format(sqldataBilling(i)("Amount"), "standard"))
                        'totalcharges = totalcharges + Format(sqldataBilling(i)("Amount"), "standard")

                    Next


                    'stracs = "select a.Billno as Billno, a.BillingDate as BillingDate, (SUM(b.amount) + SUM(a.AmountDue) + SUM(a.PenaltyAfterDue)) - SUM(a.Discount) as amount from Bills a join BillCharges b on a.AccountNumber = b.AccountNumber where a.IsPaid = 'No' and b.IsPaid = 'No' and a.Billno < " & billBillno.Text & " and b.BillNumber < " & billBillno.Text & " group by a.BillNo, a.BillingDate"

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    Dim getarrears As New DataTable
                    getarrears.Rows.Clear()

                    stracs = "select BillNo from Bills where AccountNumber = '" & billAccountNo.Text & "' and IsPaid = 'No' and Cancelled = 'No' and isPromisorry <> 'YesPosted' and BillNo < " & billBillno.Text
                    acscmd.Connection = acsconn
                    acscmd.CommandText = stracs
                    acsda.SelectCommand = acscmd
                    acsda.Fill(getarrears)

                    billarrears.Rows.Clear()

                    For t = 0 To getarrears.Rows.Count - 1

                        Dim loadbillarrears As New DataTable
                        loadbillarrears.Clear()
                        'stracs = "select a.Billno as Billno, a.BillingDate as BillingDate, (SUM(b.amount) + SUM(a.AmountDue) + SUM(a.PenaltyAfterDue)) - SUM(a.Discount) as amount from Bills a join BillCharges b on a.BillNo = b.BillNumber where a.IsPaid = 'No' and b.IsPaid = 'No' and a.BillNo = " & getarrears.Rows(t)("BillNo") & " and b.BillNumber = " & getarrears.Rows(t)("BillNo") & " group by a.BillNo, a.BillingDate"
                        stracs = "select * from Bills where IsPaid = 'No' and BillNo = " & getarrears.Rows(t)("BillNo")
                        acscmd.Connection = acsconn
                        acscmd.CommandText = stracs
                        acsda.SelectCommand = acscmd
                        acsda.Fill(loadbillarrears)

                        Dim loadchargesarrears As New DataTable
                        loadchargesarrears.Clear()
                        'stracs = "select a.Billno as Billno, a.BillingDate as BillingDate, (SUM(b.amount) + SUM(a.AmountDue) + SUM(a.PenaltyAfterDue)) - SUM(a.Discount) as amount from Bills a join BillCharges b on a.BillNo = b.BillNumber where a.IsPaid = 'No' and b.IsPaid = 'No' and a.BillNo = " & getarrears.Rows(t)("BillNo") & " and b.BillNumber = " & getarrears.Rows(t)("BillNo") & " group by a.BillNo, a.BillingDate"
                        stracs = "select SUM(Amount) as Amount from BillCharges where IsPaid = 'No' and Cancelled = 'No' and BillNumber = " & getarrears.Rows(t)("BillNo")
                        acscmd.Connection = acsconn
                        acscmd.CommandText = stracs
                        acsda.SelectCommand = acscmd
                        acsda.Fill(loadchargesarrears)
                        'AmountDue

                        Dim arrearscharge As Double

                        If IsDBNull(loadchargesarrears.Rows(0)("amount")) = True Then
                            arrearscharge = 0
                        Else
                            arrearscharge = loadchargesarrears.Rows(0)("amount")
                        End If

                        billarrears.Rows.Add(loadbillarrears.Rows(0)("Billno"), loadbillarrears.Rows(0)("BillingDate"), Format((Val(loadbillarrears.Rows(0)("AmountDue")) + Val(arrearscharge) + Val(loadbillarrears.Rows(0)("PenaltyAfterDue")) + Val(loadbillarrears.Rows(0)("Adjustment"))) - (Val(loadbillarrears.Rows(0)("Discount")) + Val(loadbillarrears.Rows(0)("AdvancePayment"))), "standard"))

                    Next

                    If lblStatus.Text = "PN" Then
                    Else

                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        Dim getpromisorry As New DataTable
                        getpromisorry.Rows.Clear()

                        stracs = "select * from AddAdjustment where AccountNumber = '" & billAccountNo.Text.ToString.Replace("'", "''") & "' and Status = 'Posted' and Paid = 'No'"
                        acscmd.Connection = acsconn
                        acscmd.CommandText = stracs
                        acsda.SelectCommand = acscmd
                        acsda.Fill(getpromisorry)

                        billpromisorry.Rows.Clear()
                        If getpromisorry.Rows.Count = 0 Then
                        Else

                            'billCovered.Text = MonthName(12) & " " & Year(billdateto.Value) - 1
                            'billdateduedate.Value = billdateto.Value.AddDays(14)

                            For h = 0 To getpromisorry.Rows.Count - 1

                                If getpromisorry.Rows(h)("Particulars") = "Forwarded Balance" Or getpromisorry.Rows(h)("Particulars") = "Remaining Cons." Then

                                    If getpromisorry.Rows(h)("BillingDate") = billCovered.Text Then

                                        billpromisorry.Rows.Add(getpromisorry.Rows(h)("ID"), getpromisorry.Rows(h)("RefNo"), getpromisorry.Rows(h)("Particulars"), Format(Val(getpromisorry.Rows(h)("Billing")) + Val(getpromisorry.Rows(h)("Charges")) + Val(getpromisorry.Rows(h)("Penalty")), "standard"))

                                    Else

                                    End If

                                    'billpromisorry.Rows.Add(getpromisorry.Rows(h)("ID"), getpromisorry.Rows(h)("RefNo"), getpromisorry.Rows(h)("Particulars"), Format(Val(getpromisorry.Rows(h)("Billing")) + Val(getpromisorry.Rows(h)("Charges")) + Val(getpromisorry.Rows(h)("Penalty")), "standard"))

                                Else
                                    If MonthName(Month(getpromisorry.Rows(h)("DateCreated")) - 1) & " " & Year(getpromisorry.Rows(h)("DateCreated")) >= billCovered.Text Then

                                        billpromisorry.Rows.Add(getpromisorry.Rows(h)("ID"), getpromisorry.Rows(h)("RefNo"), getpromisorry.Rows(h)("Particulars"), Format(Val(getpromisorry.Rows(h)("Billing")) + Val(getpromisorry.Rows(h)("Charges")) + Val(getpromisorry.Rows(h)("Penalty")), "standard"))

                                    Else

                                    End If
                                End If

                            Next

                        End If

                    End If

                    computetotalamount()

                End If

            End If

        End If

    End Sub

    Private Sub billList_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles billList.CellClick

        If billList.Columns(e.ColumnIndex).HeaderText = "Bill No." Then

            clearfields()
            lblMode.Visible = False
            lblMode.Text = "Mode"

            billBillno.Text = billList.Rows(billList.CurrentCellAddress.Y).Cells(0).Value

            billBillno_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))

        End If

    End Sub

    Public Sub reset()

        billList.Rows.Clear()

        billMonth.Text = Format(Now, "MMMMM")
        billYear.Text = Format(Now, "yyyy")
        billStatus.Text = "All"
        billZone.SelectedIndex = -1
        billBillno.Clear()

        billBillno.ReadOnly = False
        billBillno.Select()

        lblMode.Visible = False
        lblMode.Text = "Mode"

        ToolTip1.RemoveAll()

    End Sub

    Public Sub lockfields()

        billcurrent.ReadOnly = True
        billdatefrom.Enabled = False
        billdateto.Enabled = False
        billdatelastday.Enabled = False

        billdontcharge.Enabled = False
        billcancelled.Enabled = False
        meterreader.Enabled = False

        ToolTip1.RemoveAll()

    End Sub

    Public Sub clearfields()

        lblStatus.Text = ""
        lblStatus.ForeColor = Color.Orange
        billAccountNo.Clear()
        billName.Clear()
        billAddress.Clear()
        billMeterno.Clear()
        billcurrent.Clear()
        billprevious.Clear()
        billconsumption.Clear()
        billaverage.Clear()
        billCovered.Clear()
        billdatefrom.Value = Now
        billdateto.Value = Now
        billdateduedate.Value = billdateduedate.Value.AddDays(15)
        billdatelastday.Value = billdateduedate.Value
        billsenior.CheckState = CheckState.Unchecked
        billdontcharge.CheckState = CheckState.Unchecked
        billcancelled.CheckState = CheckState.Unchecked
        billamountdue.Text = "0.00"
        billdiscount.Text = "0.00"
        billPenalty.Text = "0.00"
        billadvancepayment.Text = "0.00"
        billtotalamount.Text = "0.00"
        billAdjustment.Text = "0.00"
        meterreader.SelectedIndex = -1

        billcurrent.ReadOnly = True
        billdatefrom.Enabled = False
        billdateto.Enabled = False
        billdatelastday.Enabled = False

        billdontcharge.Enabled = False
        billcancelled.Enabled = False

        lblRemarks.Hide()

        billcharges.Rows.Clear()
        billarrears.Rows.Clear()
        billpromisorry.Rows.Clear()

        ToolTip1.RemoveAll()

    End Sub

    Public Sub createnew()

        billBillno.Clear()
        billBillno.ReadOnly = True
        lblStatus.ForeColor = Color.Orange

        lblMode.Text = "Create New Bill"
        lblMode.ForeColor = Color.Green

        billcurrent.ReadOnly = False
        billdatefrom.Enabled = False
        billdateto.Enabled = True
        billdatelastday.Enabled = True
        meterreader.Enabled = True

        Dim date_to As DateTime = billdateto.Value.AddDays(11)

        If date_to.DayOfWeek = DayOfWeek.Saturday Then
            date_to = date_to.AddDays(2) ' Move to Monday
        ElseIf date_to.DayOfWeek = DayOfWeek.Sunday Then
            date_to = date_to.AddDays(1) ' Move to Monday
        End If

        billdateduedate.Value = date_to
        billdatelastday.Value = date_to

        ToolTip1.RemoveAll()

    End Sub

    Public Sub billAccountNo_KeyDown(sender As Object, e As KeyEventArgs) Handles billAccountNo.KeyDown

        If e.KeyCode = Keys.Enter Then
            sqldata2.Clear()

            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try

            If lblMode.Text = "Create New Bill" Then

                If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                sqldataBilling.Clear()

                stracs = "Select * from Customers where AccountNo = '" & billAccountNo.Text.ToString.Replace("'", "''") & "'"
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsda.SelectCommand = acscmd
                acsda.Fill(sqldataBilling)

                If sqldataBilling.Rows(0)("CustomerStatus") = "Active" Then

                    Dim checkpending As New DataTable
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    checkpending.Rows.Clear()

                    stracs = "select * from Bills where BillStatus = 'Pending' and Cancelled = 'No' and AccountNumber = '" & billAccountNo.Text.ToString.Replace("'", "''") & "'"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsda.SelectCommand = acscmd
                    acsda.Fill(checkpending)

                    If checkpending.Rows.Count = 0 Then

                        Dim sqldatasearchbill As New DataTable
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "select * from Bills where AccountNumber = '" & billAccountNo.Text.ToString.Replace("'", "''") & "' and BillingDate = '" & billCovered.Text & "' and Cancelled = 'No'"
                        acscmd.Connection = acsconn
                        acscmd.CommandText = stracs
                        acsda.SelectCommand = acscmd
                        acsda.Fill(sqldatasearchbill)

                        If sqldatasearchbill.Rows.Count = 0 Then

                            lblStatus.Text = "Pending"
                            lblMode.Visible = True

                            If sqldataBilling.Rows(0)("CompanyName") Is DBNull.Value Or sqldataBilling.Rows(0)("CompanyName") = "" Then
                                billName.Text = sqldataBilling.Rows(0)("Firstname") & " " & sqldataBilling.Rows(0)("Middlename") & " " & sqldataBilling.Rows(0)("Lastname")
                            Else
                                billName.Text = sqldataBilling.Rows(0)("CompanyName")
                            End If

                            billAddress.Text = sqldataBilling.Rows(0)("ServiceAddress")
                            billMeterno.Text = sqldataBilling.Rows(0)("MeterNo")

                            If sqldataBilling.Rows(0)("IsSenior") = "Yes" Then

                                billsenior.CheckState = CheckState.Checked
                            Else
                                billsenior.CheckState = CheckState.Unchecked

                            End If

                            If sqldataBilling.Rows(0)("DontCharge") = "Yes" Then

                                billdontcharge.CheckState = CheckState.Checked
                            Else
                                billdontcharge.CheckState = CheckState.Unchecked
                            End If

                            loadreadersnew()

                            billcurrent.Text = "0"
                            billprevious.Text = sqldataBilling.Rows(0)("LastMeterReading")
                            billaverage.Text = sqldataBilling.Rows(0)("Averagee")
                            billadvancepayment.Text = Format(sqldataBilling.Rows(0)("AdvancePayment"), "standard")
                            billconsumption.Text = billcurrent.Text - billprevious.Text



                            If (Month(billdateto.Value) - 1) = 0 Then

                                billCovered.Text = MonthName(12) & " " & Year(billdateto.Value) - 1

                            Else
                                billCovered.Text = MonthName(Month(billdateto.Value) - 1) & " " & Year(billdateto.Value)
                            End If



                            billdatefrom.Value = Format(sqldataBilling.Rows(0)("LasReadingDate"), "short date")

                                sqldata2.Clear()

                                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                stracs = "select * from ScheduleCharges where AccountNumber = '" & billAccountNo.Text.ToString.Replace("'", "''") & "' and ActiveInactive = 1"
                                acscmd.Connection = acsconn
                                acscmd.CommandText = stracs
                                acsda.SelectCommand = acscmd
                                acsda.Fill(sqldata2)

                            billcharges.Rows.Clear()

                            If sqldata2.Rows.Count = 0 Then
                                Dim membershipTable As New DataTable

                                membershipTable.Rows.Clear()
                                stracs = "select Membership_balance from Customers where AccountNo = '" & billAccountNo.Text.ToString.Replace("'", "''") & "'"
                                acscmd.Connection = acsconn
                                acscmd.CommandText = stracs
                                acsda.SelectCommand = acscmd
                                acsda.Fill(membershipTable)



                                If membershipTable(0)("Membership_balance") > 0.00 Then
                                    Dim membership_fee_divider As Integer
                                    Dim billcharge_membership_fee As Decimal
                                    If membershipTable(0)("Membership_balance") <= 1500.0 And membershipTable(0)("Membership_balance") >= 1000.0 Then
                                        membership_fee_divider = 4
                                    ElseIf membershipTable(0)("Membership_balance") < 1000.0 Then
                                        membership_fee_divider = 2
                                    Else
                                        membership_fee_divider = 1
                                        MsgBox("Something Went wrong")
                                    End If
                                    billcharge_membership_fee = membershipTable(0)("Membership_balance") / membership_fee_divider

                                    billcharges.Rows.Add(0, "Others", "Others", "Membership Fee", Format(billcharge_membership_fee, "standard"))

                                End If

                            End If

                            For i = 0 To sqldata2.Rows.Count - 1

                                    If sqldata2.Rows(i)("Recurring") = "Yes" Then
                                        billcharges.Rows.Add(sqldata2.Rows(i)("ChargeID"), sqldata2.Rows(i)("Category"), sqldata2.Rows(i)("Entry"), sqldata2.Rows(i)("Particular"), Format(sqldata2(i)("Amount"), "standard"))
                                    Else

                                        If sqldata2.Rows(i)("BillingMonth") = Month(billCovered.Text) And sqldata2.Rows(i)("BillingYear") = Year(billCovered.Text) Then
                                            billcharges.Rows.Add(sqldata2.Rows(i)("ChargeID"), sqldata2.Rows(i)("Category"), sqldata2.Rows(i)("Entry"), sqldata2.Rows(i)("Particular"), Format(sqldata2(i)("Amount"), "standard"))
                                        Else

                                        End If

                                    End If

                                Next

                                Dim getarrears As New DataTable
                                getarrears.Rows.Clear()

                                stracs = "select BillNo from Bills where AccountNumber = '" & billAccountNo.Text & "' and IsPaid = 'No' and isPromisorry <> 'YesPosted' and Cancelled = 'No'"
                                acscmd.Connection = acsconn
                                acscmd.CommandText = stracs
                                acsda.SelectCommand = acscmd
                                acsda.Fill(getarrears)

                                For t = 0 To getarrears.Rows.Count - 1

                                    Dim loadbillarrears As New DataTable
                                    loadbillarrears.Clear()

                                    stracs = "select * from Bills where IsPaid = 'No' and BillNo = " & getarrears.Rows(t)("BillNo")
                                    acscmd.Connection = acsconn
                                    acscmd.CommandText = stracs
                                    acsda.SelectCommand = acscmd
                                    acsda.Fill(loadbillarrears)

                                    Dim loadchargesarrears As New DataTable
                                    loadchargesarrears.Clear()

                                    stracs = "select SUM(Amount) as Amount from BillCharges where IsPaid = 'No' and BillNumber = " & getarrears.Rows(t)("BillNo")
                                    acscmd.Connection = acsconn
                                    acscmd.CommandText = stracs
                                    acsda.SelectCommand = acscmd
                                    acsda.Fill(loadchargesarrears)

                                    Dim totalcharges = 0
                                    If IsDBNull(loadchargesarrears.Rows(0)("amount")) = True Then

                                        totalcharges = 0

                                    Else
                                        totalcharges = loadchargesarrears.Rows(0)("amount")
                                    End If

                                    billarrears.Rows.Add(loadbillarrears.Rows(0)("Billno"), loadbillarrears.Rows(0)("BillingDate"), Format((loadbillarrears.Rows(0)("AmountDue") + totalcharges + loadbillarrears.Rows(0)("PenaltyAfterDue") + loadbillarrears.Rows(0)("Discount")) - (loadbillarrears.Rows(0)("AdvancePayment")), "standard"))

                                Next

                                Dim getpromisorry As New DataTable
                                getpromisorry.Rows.Clear()

                                stracs = "select * from AddAdjustment where AccountNumber = '" & billAccountNo.Text & "' and Status = 'Posted' and Paid = 'No'"
                                acscmd.Connection = acsconn
                                acscmd.CommandText = stracs
                                acsda.SelectCommand = acscmd
                                acsda.Fill(getpromisorry)

                                billpromisorry.Rows.Clear()
                                If getpromisorry.Rows.Count = 0 Then
                                Else

                                    For h = 0 To getpromisorry.Rows.Count - 1
                                        billpromisorry.Rows.Add(getpromisorry.Rows(h)("ID"), getpromisorry.Rows(h)("RefNo"), getpromisorry.Rows(h)("Particulars"), Format(Val(getpromisorry.Rows(h)("Billing")) + Val(getpromisorry.Rows(h)("Charges")), "standard"))
                                    Next

                                End If



                                billcharges.ClearSelection()

                                computetotalamount()

                                billcurrent.Select()

                            Else

                                clearfields()
                            reset()

                            If (Month(billdateto.Value) - 1) = 0 Then
                                MsgBox("Bill for the month of " & MonthName(12) & " " & (Year(billdateto.Value)) - 1 & " has already been created.")
                            Else
                                MsgBox("Bill for the month of " & MonthName(Month(billdateto.Value) - 1) & " " & Year(billdateto.Value) & " has already been created.")
                            End If



                        End If

                    Else

                        clearfields()
                        reset()
                        MsgBox("There is a pending bill on this account. " & vbCrLf _
                           & "Please update all pending bills before creating new bill.")

                    End If

                Else

                    MsgBox("This account is not in active status. " & vbCrLf & "Please update account status before creating bill.")
                    clearfields()
                    reset()

                End If

            End If

            If lblMode.Text = "Update Mode" Then

            End If

        End If

    End Sub

    Private Sub billdateto_ValueChanged(sender As Object, e As EventArgs) Handles billdateto.ValueChanged

        If Date.Parse(Format(billdatefrom.Value, "short date")) > Date.Parse(Format(billdateto.Value, "short date")) Then

            MsgBox("The date should not less than date from.")
            billdateto.Value = billdatefrom.Value

        Else

            If lblMode.Text = "Update Mode" Then

                Dim date_to As DateTime = billdateto.Value.AddDays(11)

                If date_to.DayOfWeek = DayOfWeek.Saturday Then
                    date_to = date_to.AddDays(2) ' Move to Monday
                ElseIf date_to.DayOfWeek = DayOfWeek.Sunday Then
                    date_to = date_to.AddDays(1) ' Move to Monday
                End If

                billdateduedate.Value = date_to
                billdatelastday.Value = date_to
            Else

                Dim buwan As Integer

                buwan = Month(billdateto.Value) - 1

                If buwan = 0 Then

                    billCovered.Text = MonthName(12) & " " & (Year(billdateto.Value)) - 1

                    Dim date_to As DateTime = billdateto.Value.AddDays(11)

                    If date_to.DayOfWeek = DayOfWeek.Saturday Then
                        date_to = date_to.AddDays(2) ' Move to Monday
                    ElseIf date_to.DayOfWeek = DayOfWeek.Sunday Then
                        date_to = date_to.AddDays(1) ' Move to Monday
                    End If

                    billdateduedate.Value = date_to

                Else
                    billCovered.Text = MonthName(Month(billdateto.Value) - 1) & " " & Year(billdateto.Value)

                    Dim date_to As DateTime = billdateto.Value.AddDays(11)

                    If date_to.DayOfWeek = DayOfWeek.Saturday Then
                        date_to = date_to.AddDays(2) ' Move to Monday
                    ElseIf date_to.DayOfWeek = DayOfWeek.Sunday Then
                        date_to = date_to.AddDays(1) ' Move to Monday
                    End If

                    billdateduedate.Value = date_to
                End If

                'billdateduedate.Value = billdateto.Value.AddDays(14)
                'billdatelastday.Value = billdateto.Value.AddDays(14)


                billcharges.Rows.Clear()

                For i = 0 To sqldata2.Rows.Count - 1

                    If sqldata2.Rows(i)("Recurring") = "Yes" Then
                        billcharges.Rows.Add(sqldata2.Rows(i)("ChargeID"), sqldata2.Rows(i)("Category"), sqldata2.Rows(i)("Entry"), sqldata2.Rows(i)("Particular"), Format(sqldata2(i)("Amount"), "standard"))
                    Else

                        If sqldata2.Rows(i)("BillingMonth") = Month(billCovered.Text) And sqldata2.Rows(i)("BillingYear") = Year(billCovered.Text) Then
                            billcharges.Rows.Add(sqldata2.Rows(i)("ChargeID"), sqldata2.Rows(i)("Category"), sqldata2.Rows(i)("Entry"), sqldata2.Rows(i)("Particular"), Format(sqldata2(i)("Amount"), "standard"))
                        Else

                        End If

                    End If

                Next



            End If

        End If

    End Sub

    Sub computetotalamount()

        'Dim x As Integer
        Dim totalamountdue As Double = 0
        Dim totalcharges As Double = 0
        Dim totalarrear As Double = 0
        Dim totalothers As Double = 0

        For x = 0 To billcharges.Rows.Count - 1

            totalcharges = totalcharges + Double.Parse(billcharges.Rows(x).Cells(4).Value)

        Next

        For y = 0 To billarrears.Rows.Count - 1

            totalarrear = totalarrear + Double.Parse(billarrears.Rows(y).Cells(2).Value)

        Next

        For b = 0 To billpromisorry.Rows.Count - 1

            totalothers = totalothers + Double.Parse(billpromisorry.Rows(b).Cells(3).Value)

        Next



        totalamountdue = (Double.Parse(totalarrear) + Double.Parse(totalcharges) + Double.Parse(billamountdue.Text) + Double.Parse(billPenalty.Text) + Double.Parse(billAdjustment.Text) + Double.Parse(totalothers)) - (Double.Parse(billdiscount.Text) + Double.Parse(billadvancepayment.Text))

        billtotalamount.Text = Format(totalamountdue, "Standard")

    End Sub

    Public Sub savingbill()

        If lblMode.Text = "Create New Bill" Then

            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try

            Try

                Dim intValue As Integer

                If meterreader.SelectedIndex = -1 Then

                    MsgBox("Please select meter reader.")

                Else



                    If Date.Parse(Format(billdatefrom.Value, "yyyy-MM-dd")) >= Date.Parse(Format(billdateto.Value, "yyyy-MM-dd")) Then

                        MsgBox("Please check the Date to.")


                    Else


                        If Integer.TryParse(billcurrent.Text, intValue) AndAlso intValue >= Val(billprevious.Text) Then

                            Dim searchbillmoth As New DataTable
                            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                            searchbillmoth.Rows.Clear()



                            stracs = "select * from Bills where BillingDate = '" & billCovered.Text & "' and AccountNumber = '" & billAccountNo.Text & "' and Cancelled = 'No'"
                            acscmd.Connection = acsconn
                            acscmd.CommandText = stracs
                            acsda.SelectCommand = acscmd
                            acsda.Fill(searchbillmoth)

                            If searchbillmoth.Rows.Count = 0 Then

                                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                sqldataBillno.Rows.Clear()
                                stracs = "select number from tbllogicnumbers where id = 1"
                                acscmd.Connection = acsconn
                                acscmd.CommandText = stracs
                                acsda.SelectCommand = acscmd
                                acsda.Fill(sqldataBillno)

                                Dim checkbillno As New DataTable
                                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                checkbillno.Rows.Clear()

                                stracs = "select * from Bills where BillNo = " & sqldataBillno.Rows(0)("number") + 1
                                acscmd.Connection = acsconn
                                acscmd.CommandText = stracs
                                acsda.SelectCommand = acscmd
                                acsda.Fill(checkbillno)

                                If checkbillno.Rows.Count = 0 Then

                                    billBillno.Text = sqldataBillno.Rows(0)("number") + 1

                                    Dim issenior, dontcharge As String

                                    If billsenior.CheckState = CheckState.Checked Then
                                        issenior = "Yes"
                                    Else
                                        issenior = "No"
                                    End If

                                    If billdontcharge.CheckState = CheckState.Checked Then
                                        dontcharge = "Yes"
                                    Else
                                        dontcharge = "No"
                                    End If

                                    Dim totalarrears As Double = 0

                                    For t = 0 To billarrears.Rows.Count - 1
                                        totalarrears = Val(totalarrears) + Val(billarrears.Rows(t).Cells(2).Value)
                                    Next

                                    For t = 0 To billpromisorry.Rows.Count - 1
                                        totalarrears = Val(totalarrears) + Val(billpromisorry.Rows(t).Cells(2).Value)
                                    Next

                                    Dim disco_date As DateTime = billdateto.Value.AddDays(11)

                                    If disco_date.DayOfWeek = DayOfWeek.Saturday Then
                                        disco_date = disco_date.AddDays(2) ' Move to Monday
                                    ElseIf disco_date.DayOfWeek = DayOfWeek.Sunday Then
                                        disco_date = disco_date.AddDays(1) ' Move to Monday
                                    End If

                                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                    stracs = "insert into Bills (BillNo, AccountNumber, CustomerName, CustomerAddress, DateFrom, ReadingDate, DueDate, PreviousReading, Reading, " _
                                        & "Consumption, LastDayNOPen, BillingDate, ForTheMonthOf, BillStatus, RateSchedule, MeterSize, Zone, isSenior," _
                                        & "AmountDue, PenaltyAfterDue, IsPaid, MeterReader, AdvancePayment, AverageCons, Discount, DontCharge, " _
                                        & "Cancelled, MeterNumber, ArrearsBill, DiscDate, CreatedBy) values (" _
                                        & billBillno.Text & ", '" & billAccountNo.Text & "', '" & billName.Text.ToString.Replace("'", "''") & "', '" _
                                        & billAddress.Text.ToString.Replace("'", "''") & "', '" & Format(Date.Parse(billdatefrom.Value), "yyyy-MM-dd") & "', '" _
                                        & Format(Date.Parse(billdateto.Value), "yyyy-MM-dd") & "', '" & Format(Date.Parse(billdateduedate.Value), "yyyy-MM-dd") & "', " & billprevious.Text & ", " _
                                        & billcurrent.Text & ", " & billconsumption.Text & ", '" & Format(Date.Parse(billdatelastday.Value), "yyyy-MM-dd") & "', '" & billCovered.Text & "', '" _
                                        & Month(billdateto.Value) - 1 & "', '" & lblStatus.Text & "', '" & sqldataBilling.Rows(0)("RateSchedule") & "', '" & sqldataBilling.Rows(0)("MeterSize") & "', '" _
                                        & sqldataBilling.Rows(0)("Zone") & "', '" & issenior & "', " & Double.Parse(billamountdue.Text) & ", " & Double.Parse(billPenalty.Text) & ", 'No', '" & meterreader.Text & "', " _
                                        & Double.Parse(billadvancepayment.Text) & ", " & billaverage.Text & ", " & Double.Parse(billdiscount.Text) & ", '" & dontcharge & "', 'No', '" _
                                        & billMeterno.Text.ToString.Replace("'", "''") & "', " & totalarrears & ", '" & Format(disco_date, "yyyy-MM-dd") & "', '" _
                                        & My.Settings.Nickname & "')"
                                    acscmd.CommandText = stracs
                                    acscmd.Connection = acsconn
                                    acscmd.ExecuteNonQuery()
                                    acscmd.Dispose()

                                    For x = 0 To billcharges.Rows.Count - 1

                                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                        stracs = "insert into BillCharges (BillNumber, AccountNumber, AccountName, BillingMonth, Zone, ChargeID, Category, Entry, Particulars, Amount, IsPaid, RateSchedule) values (" _
                                            & billBillno.Text & ", '" & billAccountNo.Text & "', '" & billName.Text.ToString.Replace("'", "''") & "', '" & billCovered.Text & "', '" _
                                            & sqldataBilling.Rows(0)("Zone") & "', " & billcharges.Rows(x).Cells(0).Value & ", '" & billcharges.Rows(x).Cells(1).Value.ToString.Replace("'", "''") & "', '" & billcharges.Rows(x).Cells(2).Value.ToString.Replace("'", "''") & "', '" & billcharges.Rows(x).Cells(3).Value.ToString.Replace("'", "''") & "', " & Double.Parse(billcharges.Rows(x).Cells(4).Value) & ", 'No','" _
                                            & sqldataBilling.Rows(0)("RateSchedule") & "')"
                                        acscmd.CommandText = stracs
                                        acscmd.Connection = acsconn
                                        acscmd.ExecuteNonQuery()
                                        acscmd.Dispose()

                                    Next

                                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                    stracs = "update tbllogicnumbers set number = number + 1 where id = 1"
                                    acscmd.CommandText = stracs
                                    acscmd.Connection = acsconn
                                    acscmd.ExecuteNonQuery()
                                    acscmd.Dispose()

                                    lblMode.Text = "Mode"
                                    lblMode.Hide()
                                    billBillno.ReadOnly = False

                                Else

                                    MsgBox("Bill number already used. Please contact system administrator.")

                                End If

                            Else

                                MsgBox("Bill for " & billCovered.Text & " already created.")

                            End If

                        Else

                            MsgBox("Negative Consupmtion.")

                        End If

                    End If
                End If

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If

        If lblMode.Text = "Update Mode" Then

            If billcancelled.CheckState = CheckState.Checked Then

                BillingCancel.cancelmode = "Create New"
                BillingCancel.ShowDialog()

            Else

                Dim intValue As Integer


                If meterreader.SelectedIndex = -1 Then
                    MsgBox("Please select meter reader.")
                Else

                    If Date.Parse(Format(billdatefrom.Value, "yyyy-MM-dd")) >= Date.Parse(Format(billdateto.Value, "yyyy-MM-dd")) Then

                        MsgBox("Please check the Date to.")


                    Else


                        If Integer.TryParse(billcurrent.Text, intValue) AndAlso intValue >= Val(billprevious.Text) Then

                            Dim issenior, dontcharge As String

                            If billsenior.CheckState = CheckState.Checked Then
                                issenior = "Yes"
                            Else
                                issenior = "No"
                            End If

                            If billdontcharge.CheckState = CheckState.Checked Then
                                dontcharge = "Yes"
                            Else
                                dontcharge = "No"
                            End If

                            If lblStatus.Text = "Pending" Then


                                If billcancelled.CheckState = CheckState.Checked Then
                                Else
                                    stracs = "update Bills set Reading = " & billcurrent.Text & ", " _
                                & "Consumption = " & billconsumption.Text & ", " _
                                & "ReadingDate = '" & Format(Date.Parse(billdateto.Value), "yyyy-MM-dd") & "', " _
                                & "DueDate = '" & Format(Date.Parse(billdateduedate.Value), "yyyy-MM-dd") & "', " _
                                & "LastDayNOPen = '" & Format(Date.Parse(billdatelastday.Value), "yyyy-MM-dd") & "', " _
                                & "AmountDue = " & Double.Parse(billamountdue.Text) & ", " _
                                & "PenaltyAfterDue = " & Double.Parse(billPenalty.Text) & ", " _
                                & "Discount = " & Double.Parse(billdiscount.Text) & ", " _
                                & "MeterReader = '" & meterreader.Text & "', " _
                                & "DiscDate = '" & Format(Date.Parse(billdateduedate.Value), "yyyy-MM-dd") & "', " _
                                & "DontCharge = '" & dontcharge & "' where BillNo = " & billBillno.Text
                                    acscmd.CommandText = stracs
                                    acscmd.Connection = acsconn
                                    acscmd.ExecuteNonQuery()
                                    acscmd.Dispose()

                                    lockfields()

                                    lblMode.Text = "Mode"
                                    lblMode.Hide()

                                    billBillno.ReadOnly = False

                                    MsgBox("Record Updated.")

                                End If

                            End If

                        Else

                            MsgBox("Please check current reading")

                        End If
                    End If
                End If

            End If

        End If

        If lblMode.Text = "Update Paid" Then

            If billcancelled.CheckState = CheckState.Checked Then

                BillingCancel.cancelmode = "Create New"
                BillingCancel.ShowDialog()

            Else

                lockfields()

                lblMode.Text = "Mode"
                lblMode.Hide()

                billBillno.ReadOnly = False



            End If

        End If

    End Sub

    Public Sub updatemode()

        If IsNumeric(billBillno.Text) = True Then

            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try

            Dim searchbillno As New DataTable

            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            stracs = "select BillNo, isPromisorry from Bills where BillNo = " & billBillno.Text
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(searchbillno)

            If searchbillno.Rows.Count = 0 Then

                MsgBox("No record Found.")

            Else
                billBillno_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))

                If searchbillno.Rows(0)("isPromisorry") = "YesPending" Or searchbillno.Rows(0)("isPromisorry") = "YesPosted" Then

                    MsgBox("Promissory for this bill applied.")

                Else



                    If billcancelled.CheckState = CheckState.Checked Then

                        MsgBox("Updating cancelled bill is not allowed.")

                        lblMode.Text = "Mode"
                        lblMode.ForeColor = Color.Black
                        lblMode.Hide()

                        billcurrent.Select()

                    Else

                        If lblStatus.Text = "Pending" Then

                            billcurrent.Focus()
                            billcurrent.Select()

                            billBillno.ReadOnly = True

                            billcurrent.ReadOnly = False
                            billdateto.Enabled = True
                            billdatelastday.Enabled = True
                            billcancelled.Enabled = True
                            billdontcharge.Enabled = True
                            meterreader.Enabled = True

                            lblMode.Text = "Update Mode"
                            lblMode.ForeColor = Color.Orange
                            lblMode.Show()



                        End If

                        If lblStatus.Text = "Posted" Then

                            billBillno.ReadOnly = True

                            'billcurrent.ReadOnly = False
                            'billdateto.Enabled = True
                            billdatelastday.Enabled = True
                            billcancelled.Enabled = True
                            meterreader.Enabled = True

                            lblMode.Text = "Update Mode"
                            lblMode.ForeColor = Color.Orange
                            lblMode.Show()

                            billcurrent.Select()

                        End If

                        If lblStatus.Text = "Paid" Then



                            Dim getbinno As New DataTable
                            stracs = "select * from Bills where Billno = " & billBillno.Text
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acsda.SelectCommand = acscmd
                            acsda.Fill(getbinno)

                            If getbinno.Rows.Count = 0 Then

                            Else

                                If IsDBNull(getbinno(0)("CRNo")) = True Then

                                    billBillno.ReadOnly = True

                                    'billcurrent.ReadOnly = False
                                    'billdateto.Enabled = True
                                    'billdatelastday.Enabled = True
                                    billcancelled.Enabled = True
                                    'meterreader.Enabled = True

                                    lblMode.Text = "Update Paid"
                                    lblMode.ForeColor = Color.Orange
                                    lblMode.Show()

                                    'billcurrent.Select()

                                Else

                                    MsgBox("Updating posted paid bills are not allowed.")

                                End If


                            End If



                        End If

                    End If


                End If

            End If

        Else
            MsgBox("No record Found.")
        End If



    End Sub

    Private Sub billcurrent_TextChanged(sender As Object, e As EventArgs) Handles billcurrent.TextChanged

        Dim intValue As Integer

        If Integer.TryParse(billcurrent.Text, intValue) AndAlso intValue >= Val(billprevious.Text) Then

            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try


            billcurrent.ForeColor = Color.Black

            If lblMode.Text = "Create New Bill" Or lblMode.Text = "Update Mode" Then

                Dim accountrate As New DataTable
                accountrate.Clear()
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                stracs = "select * from Customers where AccountNo = '" & billAccountNo.Text.ToString.Replace("'", "''") & "'"
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsda.SelectCommand = acscmd
                acsda.Fill(accountrate)

                billconsumption.Text = Val(billcurrent.Text) - Val(billprevious.Text)

                sqldataSenior.Clear()
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                stracs = "select DiscountPercent, DiscountLimit from Discounts where DiscountName = 'Senior Citizen'"
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsda.SelectCommand = acscmd
                acsda.Fill(sqldataSenior)

                If billconsumption.Text > 50 Then

                    sqldatacons.Clear()
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    stracs = "select MinimumCharge, twenty, thirty, forty, fifty, maxx from RateSchedules where CustomerType = '" & accountrate.Rows(0)("RateSchedule") & "' and MeterSize = '" & accountrate.Rows(0)("MeterSize") & "'"
                    acscmd.Connection = acsconn
                    acscmd.CommandText = stracs
                    acsda.SelectCommand = acscmd
                    acsda.Fill(sqldatacons)

                    billamountdue.Text = Format(sqldatacons.Rows(0)("MinimumCharge") + (sqldatacons.Rows(0)("twenty") * 10) + (sqldatacons.Rows(0)("thirty") * 10) + (sqldatacons.Rows(0)("forty") * 10) + (sqldatacons.Rows(0)("fifty") * 10) + (sqldatacons.Rows(0)("maxx") * (billconsumption.Text - 50)), "standard")

                    If billsenior.CheckState = CheckState.Checked Then

                        If billconsumption.Text > sqldataSenior(0)("DiscountLimit") Then
                            billdiscount.Text = "0.00"
                        Else
                            billdiscount.Text = Format(billamountdue.Text * sqldataSenior(0)("DiscountPercent"), "standard")
                        End If
                    Else
                        billdiscount.Text = "0.00"
                    End If

                    computetotalamount()

                Else

                    'Select Case billconsumption.Text

                    If billconsumption.Text >= 0 And billconsumption.Text <= 10 Then

                        sqldatacons.Clear()
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "select MinimumCharge from RateSchedules where CustomerType = '" & accountrate.Rows(0)("RateSchedule") & "' and MeterSize = '" & accountrate.Rows(0)("MeterSize") & "'"
                        acscmd.Connection = acsconn
                        acscmd.CommandText = stracs
                        acsda.SelectCommand = acscmd
                        acsda.Fill(sqldatacons)

                        billamountdue.Text = Format(sqldatacons.Rows(0)("MinimumCharge"), "standard")

                        If billsenior.CheckState = CheckState.Checked Then

                            If billconsumption.Text > sqldataSenior(0)("DiscountLimit") Then
                                billdiscount.Text = "0.00"
                            Else
                                billdiscount.Text = Format(billamountdue.Text * sqldataSenior(0)("DiscountPercent"), "standard")
                            End If
                        Else
                            billdiscount.Text = "0.00"
                        End If

                        computetotalamount()
                    End If

                    If billconsumption.Text >= 11 And billconsumption.Text <= 20 Then

                        sqldatacons.Clear()
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "select MinimumCharge, twenty from RateSchedules where CustomerType = '" & accountrate.Rows(0)("RateSchedule") & "' and MeterSize = '" & accountrate.Rows(0)("MeterSize") & "'"
                        acscmd.Connection = acsconn
                        acscmd.CommandText = stracs
                        acsda.SelectCommand = acscmd
                        acsda.Fill(sqldatacons)

                        'MsgBox(sqldatacons.Rows(0)("MinimumCharge") & "---" & sqldatacons.Rows(0)("twenty"))

                        billamountdue.Text = Format(sqldatacons.Rows(0)("MinimumCharge") + (sqldatacons.Rows(0)("twenty") * (billconsumption.Text - 10)), "standard")

                        If billsenior.CheckState = CheckState.Checked Then

                            If billconsumption.Text > sqldataSenior(0)("DiscountLimit") Then
                                billdiscount.Text = "0.00"
                            Else
                                billdiscount.Text = Format(billamountdue.Text * sqldataSenior(0)("DiscountPercent"), "standard")
                            End If
                        Else
                            billdiscount.Text = "0.00"
                        End If

                        computetotalamount()
                    End If

                    If billconsumption.Text >= 21 And billconsumption.Text <= 30 Then

                        sqldatacons.Clear()
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "select MinimumCharge, twenty, thirty from RateSchedules where CustomerType = '" & accountrate.Rows(0)("RateSchedule") & "' and MeterSize = '" & accountrate.Rows(0)("MeterSize") & "'"
                        acscmd.Connection = acsconn
                        acscmd.CommandText = stracs
                        acsda.SelectCommand = acscmd
                        acsda.Fill(sqldatacons)

                        billamountdue.Text = Format(sqldatacons.Rows(0)("MinimumCharge") + (sqldatacons.Rows(0)("twenty") * 10) + (sqldatacons.Rows(0)("thirty") * (billconsumption.Text - 20)), "standard")

                        If billsenior.CheckState = CheckState.Checked Then

                            If billconsumption.Text > sqldataSenior(0)("DiscountLimit") Then
                                billdiscount.Text = "0.00"
                            Else
                                billdiscount.Text = Format(billamountdue.Text * sqldataSenior(0)("DiscountPercent"), "standard")
                            End If
                        Else
                            billdiscount.Text = "0.00"
                        End If

                        computetotalamount()

                    End If

                    If billconsumption.Text >= 31 And billconsumption.Text <= 40 Then
                        sqldatacons.Clear()
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "select MinimumCharge, twenty, thirty, forty from RateSchedules where CustomerType = '" & accountrate.Rows(0)("RateSchedule") & "' and MeterSize = '" & accountrate.Rows(0)("MeterSize") & "'"
                        acscmd.Connection = acsconn
                        acscmd.CommandText = stracs
                        acsda.SelectCommand = acscmd
                        acsda.Fill(sqldatacons)

                        'MsgBox(sqldatacons.Rows(0)("thirty") * 10)

                        billamountdue.Text = Format(sqldatacons.Rows(0)("MinimumCharge") + (sqldatacons.Rows(0)("twenty") * 10) + (sqldatacons.Rows(0)("thirty") * 10) + (sqldatacons.Rows(0)("forty") * (billconsumption.Text - 30)), "standard")

                        If billsenior.CheckState = CheckState.Checked Then

                            If billconsumption.Text > sqldataSenior(0)("DiscountLimit") Then
                                billdiscount.Text = "0.00"
                            Else
                                billdiscount.Text = Format(billamountdue.Text * sqldataSenior(0)("DiscountPercent"), "standard")
                            End If
                        Else
                            billdiscount.Text = "0.00"
                        End If

                        computetotalamount()

                    End If

                    If billconsumption.Text >= 41 And billconsumption.Text <= 50 Then

                        sqldatacons.Clear()
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "select MinimumCharge, twenty, thirty, forty, fifty from RateSchedules where CustomerType = '" & accountrate.Rows(0)("RateSchedule") & "' and MeterSize = '" & accountrate.Rows(0)("MeterSize") & "'"
                        acscmd.Connection = acsconn
                        acscmd.CommandText = stracs
                        acsda.SelectCommand = acscmd
                        acsda.Fill(sqldatacons)

                        billamountdue.Text = Format(sqldatacons.Rows(0)("MinimumCharge") + (sqldatacons.Rows(0)("twenty") * 10) + (sqldatacons.Rows(0)("thirty") * 10) + (sqldatacons.Rows(0)("forty") * 10) + (sqldatacons.Rows(0)("fifty") * (billconsumption.Text - 40)), "standard")

                        If billsenior.CheckState = CheckState.Checked Then

                            If billconsumption.Text > sqldataSenior(0)("DiscountLimit") Then
                                billdiscount.Text = "0.00"
                            Else
                                billdiscount.Text = Format(billamountdue.Text * sqldataSenior(0)("DiscountPercent"), "standard")
                            End If
                        Else
                            billdiscount.Text = "0.00"
                        End If

                        computetotalamount()

                    End If

                End If

            End If

            If Date.Parse(Format(Now, "yyyy-MM-dd")) > Date.Parse(Format(billdatelastday.Value, "yyyy-MM-dd")) Then

                'If billdontcharge.CheckState = CheckState.Checked Then
                '    billPenalty.Text = "0.00"
                'Else
                '    billPenalty.Text = Format(billamountdue.Text * 0.05, "standard")
                'End If

                Dim getbillingarrears As New DataTable
                stracs = "select ((isnull(SUM(AmountDue),0) + isnull(SUM(PenaltyAfterDue),0) + isnull(SUM(Adjustment),0)) - isnull(SUM(AdvancePayment),0)) as billarrears 
                        from Bills where AccountNumber = '" & billAccountNo.Text.ToString.Replace("'", "''") & "' and BillStatus = 'Posted' and " _
                        & "IsPaid = 'No' and IsCollectionCreated = 'No' and isPromisorry = 'No' and Cancelled = 'No' and BillNo < " & billBillno.Text
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
                        from AddAdjustment where AccountNumber = '" & billAccountNo.Text.ToString.Replace("'", "''") & "' and " _
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

                If (billarrears + totalpn) <= 0 Then
                Else
                    billPenalty.Text = Format((billarrears + totalpn + Double.Parse(billamountdue.Text)) * 0.05, "Standard")
                End If



            Else
                billPenalty.Text = "0.00"
            End If

        Else

            billcurrent.ForeColor = Color.Red

        End If

    End Sub

    Private Sub billdatelastday_ValueChanged(sender As Object, e As EventArgs) Handles billdatelastday.ValueChanged

        If lblStatus.Text = "Pending" Then

            If Date.Parse(Format(billdateduedate.Value, "yyyy-MM-dd")) > Date.Parse(Format(billdatelastday.Value, "yyyy-MM-dd")) Then

                MsgBox("The date should not less than due date.")
                billdatelastday.Value = billdateduedate.Value

            End If

            If Date.Parse(Format(Now, "yyyy-MM-dd")) > Date.Parse(Format(billdatelastday.Value, "yyyy-MM-dd")) Then

                '    If IsNumeric(billamountdue.Text) Then

                '        If billdontcharge.CheckState = CheckState.Checked Then
                '            billPenalty.Text = "0.00"
                '        Else
                '            billPenalty.Text = Format(billamountdue.Text * 0.05, "standard")
                '        End If


                '    End If

                'Else
                '    billPenalty.Text = "0.00"
                'End If

                Dim getbillingarrears As New DataTable
                stracs = "select ((isnull(SUM(AmountDue),0) + isnull(SUM(PenaltyAfterDue),0) + isnull(SUM(Adjustment),0)) - isnull(SUM(AdvancePayment),0)) as billarrears 
                        from Bills where AccountNumber = '" & billAccountNo.Text.ToString.Replace("'", "''") & "' and BillStatus = 'Posted' and " _
                        & "IsPaid = 'No' and IsCollectionCreated = 'No' and isPromisorry = 'No' and Cancelled = 'No' and BillNo < " & billBillno.Text
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(getbillingarrears)

                'Dim billarrears As Double = Double.Parse(getbillingarrears(0)("billarrears"))



                Dim billarrears As Double


                If getbillingarrears.Rows.Count = 0 Then
                    billarrears = 0
                Else
                    If IsDBNull(getbillingarrears.Rows(0)("billarrears")) = True Or getbillingarrears.Rows(0)("billarrears") <= 0 Then
                        billarrears = 0
                    Else
                        billarrears = Double.Parse(getbillingarrears(0)("billarrears"))
                    End If
                End If



                Dim getbillingpn As New DataTable

                stracs = "select (SUM(Billing) + SUM(Penalty)) as billpn 
                        from AddAdjustment where AccountNumber = '" & billAccountNo.Text.ToString.Replace("'", "''") & "' and " _
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

                If (billarrears + totalpn) <= 0 Then
                Else
                    billPenalty.Text = Format((billarrears + totalpn + Double.Parse(billamountdue.Text)) * 0.05, "Standard")
                End If

            Else
                billPenalty.Text = "0.00"
            End If

        End If


    End Sub

    Private Sub DetailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DetailsToolStripMenuItem.Click

        If billAccountNo.Text = "" Or billBillno.Text = "" Then
        Else

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

                BillingCancel.cancelmode = "Viewing"
                BillingCancel.ShowDialog()

            End If

        End If

    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click

        If My.Settings.Cservice = "Yes" Then

            billcurrent.Select()
            clearfields()
            lblMode.Text = "Create New Bill"
            lblMode.Hide()

            SearchAccount.searchingform = "Bills"
            SearchAccount.searchid = "newbill"
            SearchAccount.ShowDialog()

        Else
            MsgBox("Your account cannot perform this process.")
        End If



    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click

        If My.Settings.Cservice = "Yes" Then
            savingbill()
        Else
            MsgBox("Your account cannot perform this process.")
        End If



    End Sub

    Private Sub UpdateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateToolStripMenuItem.Click


        If My.Settings.Cservice = "Yes" Then

            If lblMode.Text = "" Or lblMode.Text = "" Then

                MsgBox("Please complete the " & lblMode.Text & " before updating.")

            Else

                If billBillno.Text = "" Then

                    MsgBox("Bill number empty.")

                Else

                    billcurrent.Select()

                    waitingapproval.trans = "Billing"
                    waitingapproval.ShowDialog()

                    waitingapproval.TextBox1.Select()
                    waitingapproval.TextBox1.Clear()

                End If



                'If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
                '    updatemode()
                'Else
                '    'MsgBox("Your account cannot perform this process.")

                '    Dim asd As DialogResult = MessageBox.Show("Your account cannot perform this process. Ask for approval?", "Electronic Billing and Collection", MessageBoxButtons.YesNo)

                '    If asd = DialogResult.Yes Then

                '        'Try
                '        '    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                '        'Catch ex As Exception
                '        '    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                '        'End Try

                '        'Dim getrefno As New DataTable
                '        'getrefno.Clear()
                '        'stracs = "select * from tbllogicnumbers where id = 13"
                '        'acscmd.CommandText = stracs
                '        'acscmd.Connection = acsconn
                '        'acsda.SelectCommand = acscmd
                '        'acsda.Fill(getrefno)

                '        'stracs = "insert into TaskRequest ([refNo],[taskRefno],[accountNo],[task],[reqDate],[RequestBy],[Approvedby],[Status]) values (" _
                '        '    & getrefno.Rows(0)("number") & ", '" & billBillno.Text & "', '" & billAccountNo.Text & "', 'Update Bill', '" & Format(Now, "yyyy-MM-dd") & "', '" & My.Settings.Nickname & "','Pending')"
                '        'acscmd.CommandText = stracs
                '        'acscmd.Connection = acsconn
                '        'acscmd.ExecuteNonQuery()
                '        'acscmd.Dispose()

                '        waitingapproval.ShowDialog()

                '    Else

                '    End If

                'End If


            End If

        Else

            MsgBox("Your account cannot perform this process.")

        End If





    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        clearfields()
        reset()
    End Sub


    Public MoveFormbillinginfo As Boolean
    Public Movebillinginfo_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormbillinginfo = True
            Me.Cursor = Cursors.NoMove2D
            Movebillinginfo_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormbillinginfo Then
            Me.Location = Me.Location + (e.Location - Movebillinginfo_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormbillinginfo = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub billinginfo_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.BringToFront()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Me.Activate()
    End Sub

    Private Sub billinginfo_deactivated(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.BackColor = Color.FromArgb(17, 153, 195)
    End Sub

    Private Sub ControlsClick(sender As Object, e As EventArgs) Handles _
        Panel1.Click, billMonth.Click, billZone.Click, billStatus.Click, billSearch.Click, billBillno.Click, billAccountNo.Click,
        billName.Click, billcurrent.Click, billprevious.Click, billconsumption.Click, billaverage.Click, billamountdue.Click,
        billPenalty.Click, billdiscount.Click, billadvancepayment.Click, billCovered.Click, billdatefrom.Click, billdateto.Click,
        billdatelastday.Click, billdateduedate.Click, billarrears.Click, billpromisorry.Click, billtotalamount.Click, billsenior.Click,
        billdontcharge.Click, billcancelled.Click, billList.Click  ' etc.
        Me.Activate() 'Or Whatever
    End Sub

    Private Sub billinginfo_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.BackColor = Color.SteelBlue
    End Sub

    Private Sub posting_Click(sender As Object, e As EventArgs) Handles posting.Click

        If billcurrent.Text > 0 Or billconsumption.Text < 0 Then

            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try

            If lblMode.Text = "Mode" Then

                If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                stracs = "update Bills set BillStatus = 'Posted', PostedBy = '" & My.Settings.Nickname & "' where BillNo = " & billBillno.Text
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                Dim getaverage As New DataTable
                Dim newaverage As Integer = 0

                getaverage.Clear()
                stracs = "SELECT top 3 Consumption From Bills Where Cancelled = 'No' and BillStatus = 'Posted' and AccountNumber = '" & billAccountNo.Text & "' Order By BillNo desc"
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsda.SelectCommand = acscmd
                acsda.Fill(getaverage)

                For y = 0 To getaverage.Rows.Count - 1

                    newaverage = newaverage + getaverage(y)("Consumption")

                Next

                newaverage = CInt(newaverage / getaverage.Rows.Count)

                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                stracs = "update Customers set Averagee = " & newaverage & ", LastMeterReading = " & billcurrent.Text & ", LastBill = '" & billBillno.Text & "', LasReadingDate = '" & Format(Date.Parse(billdateto.Value), "yyyy-MM-dd") & "', AdvancePayment = AdvancePayment - " & Double.Parse(billadvancepayment.Text) & " where AccountNo = '" & billAccountNo.Text & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                Dim gettotalbillbalance As New DataTable
                Dim totalbillbalance As Double = 0

                gettotalbillbalance.Clear()
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                'stracs = "select SUM(AmountDue), S from Bills where AccountNumber = '" & billList.Rows(x).Cells(2).Value & "' and BillStatus = 'Posted' and Cancelled = 'No' and IsPaid = 'No'"
                stracs = "select SUM(AmountDue) as amountdue, SUm(AdvancePayment) as advance, Sum(Discount) as discount, SUm(PenaltyAfterDue) as penalty from Bills where AccountNumber = '" & billAccountNo.Text & "' and BillStatus = 'Posted' and Cancelled = 'No' and IsPaid = 'No' and isPromisorry = 'No'"
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsda.SelectCommand = acscmd
                acsda.Fill(gettotalbillbalance)

                If gettotalbillbalance.Rows.Count = 0 Then
                    totalbillbalance = 0
                Else
                    totalbillbalance = Val(gettotalbillbalance(0)("amountdue") + gettotalbillbalance(0)("penalty")) - Val(gettotalbillbalance(0)("advance") + gettotalbillbalance(0)("discount"))

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

                If Double.Parse(billPenalty.Text) > 0 Then

                    stracs = "insert into AccountLedger (ledgerAccountNo, ledgerDate, ledgerRefNo, ledgerParticulars, ledgerReading, ledgerConsumption, ledgerDiscount, ledgerAmount, ledgerBalance) values ('" _
                        & billAccountNo.Text & "', '" _
                        & Format(Date.Parse(billdateto.Value), "yyyy-MM-dd") & "', '" _
                        & billBillno.Text & "', '" _
                        & "Billing', '" _
                        & billcurrent.Text & "', '" _
                        & billconsumption.Text & "', '" _
                        & Format(Val(billdiscount.Text) + Val(billadvancepayment.Text), "standard") & "', '" _
                        & billamountdue.Text & "', '" _
                        & FormatNumber((Val(totalbillbalance) + Val(totalbillchargebalance) + Val(totalpn)) - Val(billPenalty.Text), UseParensForNegativeNumbers:=TriState.True) & "')"

                    'FormatNumber(Double.Parse(Val(newchargesamount) + Val(totalbillbalance) + Val(totalpn)), UseParensForNegativeNumbers:=TriState.True)

                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                    stracs = "insert into AccountLedger (ledgerAccountNo, ledgerDate, ledgerRefNo, ledgerParticulars, ledgerReading, ledgerConsumption, ledgerDiscount, ledgerAmount, ledgerBalance) values ('" _
                        & billAccountNo.Text & "', '" _
                        & Format(Date.Parse(billdateto.Value), "yyyy-MM-dd") & "', '" _
                        & billBillno.Text & "', '" _
                        & "Penalty', '" _
                        & "', '" _
                        & "', '" _
                        & "', '" _
                        & billPenalty.Text & "', '" _
                        & FormatNumber(Val(totalbillbalance) + Val(totalbillchargebalance) + Val(totalpn), UseParensForNegativeNumbers:=TriState.True) & "')"



                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                Else

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    stracs = "insert into AccountLedger (ledgerAccountNo, ledgerDate, ledgerRefNo, ledgerParticulars, ledgerReading, ledgerConsumption, ledgerDiscount, ledgerAmount, ledgerBalance) values ('" _
                        & billAccountNo.Text & "', '" _
                        & Format(Date.Parse(billdateto.Value), "yyyy-MM-dd") & "', '" _
                        & billBillno.Text & "', '" _
                        & "Billing', '" _
                        & billcurrent.Text & "', '" _
                        & billconsumption.Text & "', '" _
                        & Format(Val(billdiscount.Text) + Val(billadvancepayment.Text), "standard") & "', '" _
                        & billamountdue.Text & "', '" _
                        & FormatNumber(Val(totalbillbalance) + Val(totalbillchargebalance + Val(totalpn)), UseParensForNegativeNumbers:=TriState.True) & "')"



                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                End If

                Dim getcharges As New DataTable
                Dim newchargesamount As Double = totalbillchargebalance
                stracs = "select * from BillCharges where BillNumber = " & billBillno.Text
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsda.SelectCommand = acscmd
                acsda.Fill(getcharges)

                For i = 0 To getcharges.Rows.Count - 1

                    stracs = "update BillCharges set Status = 'Posted' where BillNumber = " & billBillno.Text
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                    newchargesamount = Val(newchargesamount) + Val(getcharges.Rows(i)("Amount"))
                    'format(Val(newchargesamount) + Val(totalbillbalance), "Standard")

                    If getcharges.Rows(i)("Category") = "Others" Then

                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "insert into AccountLedger (ledgerAccountNo, ledgerDate, ledgerRefNo, ledgerParticulars, ledgerReading, ledgerConsumption, ledgerDiscount, ledgerAmount, ledgerBalance) values ('" _
                            & billAccountNo.Text & "', '" _
                            & Format(Date.Parse(billdateto.Value), "yyyy-MM-dd") & "', '" _
                            & billBillno.Text & "', '" _
                            & getcharges.Rows(i)("Particulars") & "', '" _
                            & "', '" _
                            & "', '" _
                            & "', '" _
                            & Format(Val(getcharges.Rows(i)("Amount")), "Standard") & "', '" _
                            & FormatNumber(Double.Parse(Val(newchargesamount) + Val(totalbillbalance) + Val(totalpn)), UseParensForNegativeNumbers:=TriState.True) & "')"



                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                    Else

                    End If

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    stracs = "update ScheduleCharges set ActiveInactive = 0 where Recurring = 'No' and Category = '" & getcharges.Rows(i)("Category") & "' and Particular = '" & getcharges.Rows(i)("Particulars") & "' and AccountNumber = '" & billAccountNo.Text & "' and BillingMonth = " & Month(getcharges.Rows(i)("BillingMonth")) & " and BillingYear = " & Year(getcharges.Rows(i)("BillingMonth"))
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                Next

                stracs = "delete from BillsTemp where BillNo = " & billBillno.Text
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                If Val(newchargesamount) + Val(totalbillbalance) + Val(totalpn) <= 0 Then

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                    stracs = "update Bills set IsPaid = 'Yes', IsCollectionCreated = 'Yes' where BillNo = " & billBillno.Text
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                    stracs = "update BillCharges set IsPaid = 'Yes', IsCollectionCreated = 'Yes' where BillNumber = " & billBillno.Text
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                    stracs = "update Customers set AdvancePayment = " & FormatNumber(Double.Parse(Val(newchargesamount) + Val(totalbillbalance) + Val(totalpn)), UseParensForNegativeNumbers:=TriState.True) & " where AccountNo = '" & billAccountNo.Text & "'"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                End If


                billBillno_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))

            Else
                MsgBox("Please finish updating first before posting.")
            End If

            Else

            MsgBox("Posting no reading and negative consumption are not allowed.")

        End If

    End Sub

    Public Sub accountno_KeyDown(sender As Object, e As KeyEventArgs) Handles accountno.KeyDown

        If e.KeyCode = Keys.Enter Then

            If acsconn.State = ConnectionState.Closed Then acsconn.Open()

            sqldatasearch.Clear()

            stracs = "select * from Bills where AccountNumber = '" & accountno.Text & "' order by BillNo desc"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(sqldatasearch)

            billList.Rows.Clear()

            If sqldatasearch.Rows.Count = 0 Then
                MsgBox("No Record Found.")
            Else
                For i = 0 To sqldatasearch.Rows.Count - 1

                    If sqldatasearch(i)("Cancelled") = "Yes" Then

                        If IsDBNull(sqldatasearch(i)("ReadingDate")) = True Then
                            billList.Rows.Add(sqldatasearch(i)("BillNo"), sqldatasearch(i)("ReadingDate"), sqldatasearch(i)("CustomerName"), sqldatasearch(i)("Consumption"), "Cancelled")
                        Else
                            billList.Rows.Add(sqldatasearch(i)("BillNo"), Format(sqldatasearch(i)("ReadingDate"), "short date"), sqldatasearch(i)("CustomerName"), sqldatasearch(i)("Consumption"), "Cancelled")
                        End If


                    Else
                        If sqldatasearch(i)("isPaid") = "Yes" Then
                            billList.Rows.Add(sqldatasearch(i)("BillNo"), Format(sqldatasearch(i)("ReadingDate"), "short date"), sqldatasearch(i)("CustomerName"), sqldatasearch(i)("Consumption"), "Paid")
                        Else
                            If sqldatasearch(i)("isPromisorry") = "YesPosted" Then
                                billList.Rows.Add(sqldatasearch(i)("BillNo"), Format(sqldatasearch(i)("ReadingDate"), "short date"), sqldatasearch(i)("CustomerName"), sqldatasearch(i)("Consumption"), "PN")
                            Else

                                If IsDBNull(sqldatasearch(i)("ReadingDate")) = True Then
                                    billList.Rows.Add(sqldatasearch(i)("BillNo"), Format(sqldatasearch(i)("DateFrom"), "short date"), sqldatasearch(i)("CustomerName"), sqldatasearch(i)("Consumption"), sqldatasearch(i)("BillStatus"))
                                Else
                                    billList.Rows.Add(sqldatasearch(i)("BillNo"), Format(sqldatasearch(i)("ReadingDate"), "short date"), sqldatasearch(i)("CustomerName"), sqldatasearch(i)("Consumption"), sqldatasearch(i)("BillStatus"))
                                End If


                            End If

                            End If
                    End If

                Next

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

    Private Sub printcrdocs_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles printcrdocs.PrintPage

        If IsNumeric(billBillno.Text) = True Then

            Dim reprintdata As New DataTable
            reprintdata.Rows.Clear()
            stracs = "select * from Bills where BillNo = " & billBillno.Text
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            'acsdr = acscmd.ExecuteReader
            acsda.SelectCommand = acscmd
            acsda.Fill(reprintdata)

            If reprintdata.Rows.Count = 0 Then
                MsgBox("No record found.")
            Else

                billBillno_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))

                Dim headFont As New Font("Segoe UI", 9, FontStyle.Bold, GraphicsUnit.Point)
                Dim footFont As New Font("Segoe UI", 7, GraphicsUnit.Point)
                Dim headsubFont As New Font("Segoe UI", 8, GraphicsUnit.Point)
                Dim headsubFontbold As New Font("Segoe UI", 8, FontStyle.Bold, GraphicsUnit.Point)
                Dim headsubFontitalic As New Font("Segoe UI", 8, FontStyle.Italic, GraphicsUnit.Point)
                Dim locationv As Integer = 140

                Dim MidCenterhead As StringFormat = New StringFormat()
                Dim MidLeft As StringFormat = New StringFormat()
                Dim MidRight As StringFormat = New StringFormat()
                Dim nearnear As StringFormat = New StringFormat()
                Dim nearfar As StringFormat = New StringFormat()

                Dim arrears, pns, chargess As Double

                MidCenterhead.LineAlignment = StringAlignment.Near
                MidCenterhead.Alignment = StringAlignment.Center

                MidLeft.LineAlignment = StringAlignment.Center
                MidLeft.Alignment = StringAlignment.Near

                MidRight.LineAlignment = StringAlignment.Center
                MidRight.Alignment = StringAlignment.Far

                nearnear.LineAlignment = StringAlignment.Near
                nearnear.Alignment = StringAlignment.Near

                nearfar.LineAlignment = StringAlignment.Near
                nearfar.Alignment = StringAlignment.Far

                'Dim CurX As Integer = 50
                'Dim CurY As Integer = 50
                'Dim iWidth As Integer = 250

                Dim cellRecthead As RectangleF
                cellRecthead = New RectangleF()
                cellRecthead.Location = New Point(0, 0)
                cellRecthead.Size = New Size(350, 100)

                Dim CurX As Integer = 0
                Dim CurY As Integer = 0
                Dim iWidth As Integer = 275

                CurY = PrintCellText("Republic of the Philippines", CurX, CurY, iWidth, e, headFont, MidCenterhead)
                CurY = PrintCellText("Pantabangan Water System", CurX, CurY, iWidth, e, headFont, MidCenterhead)
                CurY = PrintCellText("Barangay Poblacion", CurX, CurY, iWidth, e, headsubFont, MidCenterhead)
                CurY = PrintCellText("Pantabangan, Nueva Ecija", CurX, CurY, iWidth, e, headsubFont, MidCenterhead)
                'CurY = PrintCellText("Contact Number : 0908 8145 758", CurX, CurY, iWidth, e, headsubFont, MidCenterhead)

                Dim cellRectWaterBill As RectangleF
                cellRectWaterBill = New RectangleF()
                cellRectWaterBill.Location = New Point(0, 90)
                cellRectWaterBill.Size = New Size(270, 15)

                e.Graphics.DrawString("WATER BILL", headFont, Brushes.Black, cellRectWaterBill, MidCenterhead)

                e.Graphics.DrawString("ACCOUNT INFORMATION", headsubFont, Brushes.Black, 0, 130)
                e.Graphics.DrawString("BILL NO. : ", headsubFont, Brushes.Black, 160, 130)
                e.Graphics.DrawString(reprintdata(0)("BillNo"), headsubFont, Brushes.Black, 220, 130)

                e.Graphics.DrawString("Account No.", headsubFont, Brushes.Black, 0, 150)
                e.Graphics.DrawString(":", headsubFont, Brushes.Black, 80, 150)
                e.Graphics.DrawString(reprintdata(0)("AccountNumber"), headsubFont, Brushes.Black, 95, 150)

                e.Graphics.DrawString("Name", headsubFont, Brushes.Black, 0, 165)
                e.Graphics.DrawString(":", headsubFont, Brushes.Black, 80, 165)

                billName.Text = reprintdata(0)("CustomerName")
                billAddress.Text = reprintdata(0)("CustomerAddress")

                locationv = 165

                If billName.TextLength < 25 Then

                    e.Graphics.DrawString(billName.Text.Substring(0, billName.TextLength), headsubFont, Brushes.Black, 95, 165)
                    locationv = locationv + 15
                Else

                    If billName.TextLength >= 25 And billName.TextLength <= 49 Then

                        e.Graphics.DrawString(billName.Text.Substring(0, 25).ToUpper, headsubFont, Brushes.Black, 95, 165)
                        e.Graphics.DrawString(billName.Text.Substring(25, billName.TextLength - 25).ToUpper, headsubFont, Brushes.Black, 95, 180)
                        locationv = locationv + 30
                    End If

                    If billName.TextLength >= 50 And billName.TextLength <= 74 Then

                        e.Graphics.DrawString(billName.Text.Substring(0, 25).ToUpper, headsubFont, Brushes.Black, 95, 165)
                        e.Graphics.DrawString(billName.Text.Substring(25, 25).ToUpper, headsubFont, Brushes.Black, 95, 180)
                        e.Graphics.DrawString(billName.Text.Substring(50, billName.TextLength - 50).ToUpper, headsubFont, Brushes.Black, 95, 195)
                        locationv = locationv + 45
                    End If

                    If billName.TextLength >= 75 And billName.TextLength <= 99 Then

                        e.Graphics.DrawString(billName.Text.Substring(0, 25).ToUpper, headsubFont, Brushes.Black, 95, 165)
                        e.Graphics.DrawString(billName.Text.Substring(25, 25).ToUpper, headsubFont, Brushes.Black, 95, 180)
                        e.Graphics.DrawString(billName.Text.Substring(50, 25).ToUpper, headsubFont, Brushes.Black, 95, 195)
                        e.Graphics.DrawString(billName.Text.Substring(75, billName.TextLength - 75).ToUpper, headsubFont, Brushes.Black, 95, 210)
                        locationv = locationv + 60
                    End If

                    If billName.TextLength >= 100 And billName.TextLength <= 124 Then

                        e.Graphics.DrawString(billName.Text.Substring(0, 25).ToUpper, headsubFont, Brushes.Black, 95, 165)
                        e.Graphics.DrawString(billName.Text.Substring(25, 25).ToUpper, headsubFont, Brushes.Black, 95, 180)
                        e.Graphics.DrawString(billName.Text.Substring(50, 25).ToUpper, headsubFont, Brushes.Black, 95, 195)
                        e.Graphics.DrawString(billName.Text.Substring(75, 25).ToUpper, headsubFont, Brushes.Black, 95, 210)
                        e.Graphics.DrawString(billName.Text.Substring(100, billName.TextLength - 100).ToUpper, headsubFont, Brushes.Black, 95, 225)
                        locationv = locationv + 75
                    End If

                End If

                e.Graphics.DrawString("Address", headsubFont, Brushes.Black, 0, locationv)
                e.Graphics.DrawString(":", headsubFont, Brushes.Black, 80, locationv)

                If billAddress.TextLength < 25 Then

                    e.Graphics.DrawString(billAddress.Text.Substring(0, billAddress.TextLength), headsubFont, Brushes.Black, 95, locationv)
                    locationv = locationv + 15
                Else

                    If billAddress.TextLength >= 25 And billAddress.TextLength <= 49 Then

                        e.Graphics.DrawString(billAddress.Text.Substring(0, 25).ToUpper, headsubFont, Brushes.Black, 95, locationv)
                        e.Graphics.DrawString(billAddress.Text.Substring(25, billAddress.TextLength - 25).ToUpper, headsubFont, Brushes.Black, 95, locationv + 15)
                        locationv = locationv + 30
                    End If

                    If billAddress.TextLength >= 50 And billAddress.TextLength <= 74 Then

                        e.Graphics.DrawString(billAddress.Text.Substring(0, 25).ToUpper, headsubFont, Brushes.Black, 95, locationv)
                        e.Graphics.DrawString(billAddress.Text.Substring(25, 25).ToUpper, headsubFont, Brushes.Black, 95, locationv + 15)
                        e.Graphics.DrawString(billAddress.Text.Substring(50, billAddress.TextLength - 50).ToUpper, headsubFont, Brushes.Black, 95, locationv + 30)
                        locationv = locationv + 45
                    End If

                    If billAddress.TextLength >= 75 And billAddress.TextLength <= 99 Then

                        e.Graphics.DrawString(billAddress.Text.Substring(0, 25).ToUpper, headsubFont, Brushes.Black, 95, locationv)
                        e.Graphics.DrawString(billAddress.Text.Substring(25, 25).ToUpper, headsubFont, Brushes.Black, 95, locationv + 15)
                        e.Graphics.DrawString(billAddress.Text.Substring(50, 25).ToUpper, headsubFont, Brushes.Black, 95, locationv + 30)
                        e.Graphics.DrawString(billAddress.Text.Substring(75, billAddress.TextLength - 75).ToUpper, headsubFont, Brushes.Black, 95, locationv + 45)
                        locationv = locationv + 60
                    End If

                    If billAddress.TextLength >= 100 And billAddress.TextLength <= 124 Then

                        e.Graphics.DrawString(billAddress.Text.Substring(0, 25).ToUpper, headsubFont, Brushes.Black, 95, locationv)
                        e.Graphics.DrawString(billAddress.Text.Substring(25, 25).ToUpper, headsubFont, Brushes.Black, 95, locationv + 15)
                        e.Graphics.DrawString(billAddress.Text.Substring(50, 25).ToUpper, headsubFont, Brushes.Black, 95, locationv + 30)
                        e.Graphics.DrawString(billAddress.Text.Substring(75, 25).ToUpper, headsubFont, Brushes.Black, 95, locationv + 45)
                        e.Graphics.DrawString(billAddress.Text.Substring(100, billAddress.TextLength - 100).ToUpper, headsubFont, Brushes.Black, 95, locationv + 60)
                        locationv = locationv + 75
                    End If

                End If

                e.Graphics.DrawString("Class", headsubFont, Brushes.Black, 0, locationv)
                e.Graphics.DrawString(":", headsubFont, Brushes.Black, 80, locationv)
                e.Graphics.DrawString(reprintdata(0)("RateSchedule"), headsubFont, Brushes.Black, 95, locationv)

                locationv = locationv + 15

                e.Graphics.DrawString("Meter No.", headsubFont, Brushes.Black, 0, locationv)
                e.Graphics.DrawString(":", headsubFont, Brushes.Black, 80, locationv)
                e.Graphics.DrawString(reprintdata(0)("MeterNumber"), headsubFont, Brushes.Black, 95, locationv)

                locationv = locationv + 15

                e.Graphics.DrawString("Avg. Cons.", headsubFont, Brushes.Black, 0, locationv)
                e.Graphics.DrawString(":", headsubFont, Brushes.Black, 80, locationv)
                e.Graphics.DrawString(reprintdata(0)("AverageCons"), headsubFont, Brushes.Black, 95, locationv)

                locationv = locationv + 30

                Dim cellRectBillDetails As RectangleF
                cellRectBillDetails = New RectangleF()
                cellRectBillDetails.Location = New Point(0, locationv)
                cellRectBillDetails.Size = New Size(270, 15)

                e.Graphics.DrawString("BILLING DETAILS", headFont, Brushes.Black, cellRectBillDetails, MidCenterhead)

                'e.Graphics.DrawString("BILLING DETAILS", headFont, Brushes.Black, 56, locationv)

                locationv = locationv + 30

                e.Graphics.DrawString("Present Reading", headsubFont, Brushes.Black, 0, locationv)
                e.Graphics.DrawString(":", headsubFont, Brushes.Black, 100, locationv)
                e.Graphics.DrawString(reprintdata(0)("Reading"), headsubFont, Brushes.Black, 110, locationv)

                locationv = locationv + 15

                e.Graphics.DrawString("Previous Reading", headsubFont, Brushes.Black, 0, locationv)
                e.Graphics.DrawString(":", headsubFont, Brushes.Black, 100, locationv)
                e.Graphics.DrawString(reprintdata(0)("PreviousReading"), headsubFont, Brushes.Black, 110, locationv)

                locationv = locationv + 15

                e.Graphics.DrawString("Consumption", headsubFont, Brushes.Black, 0, locationv)
                e.Graphics.DrawString(":", headsubFont, Brushes.Black, 100, locationv)
                e.Graphics.DrawString(reprintdata(0)("Consumption"), headsubFont, Brushes.Black, 110, locationv)

                locationv = locationv + 30

                Dim cellRectPeriodCovered As RectangleF
                cellRectPeriodCovered = New RectangleF()
                cellRectPeriodCovered.Location = New Point(0, locationv)
                cellRectPeriodCovered.Size = New Size(270, 15)

                e.Graphics.DrawString("PERIOD COVERED", headFont, Brushes.Black, cellRectPeriodCovered, MidCenterhead)

                'e.Graphics.DrawString("PERIOD COVERED", headFont, Brushes.Black, 56, locationv)

                locationv = locationv + 30

                e.Graphics.DrawString("FROM", headsubFont, Brushes.Black, 20, locationv)
                e.Graphics.DrawString("|", headsubFont, Brushes.Black, 130, locationv)
                e.Graphics.DrawString("TO", headsubFont, Brushes.Black, 180, locationv)

                locationv = locationv + 15

                e.Graphics.DrawString(reprintdata(0)("DateFrom"), headsubFont, Brushes.Black, 20, locationv)
                e.Graphics.DrawString("|", headsubFont, Brushes.Black, 130, locationv)
                e.Graphics.DrawString(reprintdata(0)("ReadingDate"), headsubFont, Brushes.Black, 180, locationv)

                locationv = locationv + 30

                e.Graphics.DrawString("Due Date", headsubFont, Brushes.Black, 20, locationv)
                e.Graphics.DrawString("|", headsubFont, Brushes.Black, 130, locationv)
                e.Graphics.DrawString("Discon Date", headsubFont, Brushes.Black, 180, locationv)

                locationv = locationv + 15

                If reprintdata(0)("IsPaid") = "Yes" Then

                    e.Graphics.DrawString(reprintdata(0)("DueDate"), headsubFont, Brushes.Black, 20, locationv)
                    e.Graphics.DrawString("|", headsubFont, Brushes.Black, 130, locationv)
                    e.Graphics.DrawString(reprintdata(0)("DiscDate"), headsubFont, Brushes.Black, 180, locationv)

                Else

                    If reprintdata(0)("DueDate") < Now Then

                        e.Graphics.DrawString("Immediately", headsubFont, Brushes.Black, 20, locationv)
                        e.Graphics.DrawString("|", headsubFont, Brushes.Black, 130, locationv)
                        e.Graphics.DrawString("Immediately", headsubFont, Brushes.Black, 180, locationv)

                    Else

                        If billarrears.Rows.Count = 0 Then
                            e.Graphics.DrawString(reprintdata(0)("DueDate"), headsubFont, Brushes.Black, 20, locationv)
                            e.Graphics.DrawString("|", headsubFont, Brushes.Black, 130, locationv)
                            e.Graphics.DrawString(reprintdata(0)("DiscDate"), headsubFont, Brushes.Black, 180, locationv)
                        Else

                            e.Graphics.DrawString("Immediately", headsubFont, Brushes.Black, 20, locationv)
                            e.Graphics.DrawString("|", headsubFont, Brushes.Black, 130, locationv)
                            e.Graphics.DrawString("Immediately", headsubFont, Brushes.Black, 180, locationv)

                        End If


                    End If

                End If

                locationv = locationv + 20

                e.Graphics.DrawString("________________________________________________________", headsubFont, Brushes.Black, 0, locationv)

                locationv = locationv + 25

                e.Graphics.DrawString("BILLING SUMMARY", headsubFont, Brushes.Black, 0, locationv)
                e.Graphics.DrawString("AMOUNT", headsubFont, Brushes.Black, 200, locationv)

                locationv = locationv + 25

                Dim cellRectCurrent As RectangleF
                cellRectCurrent = New RectangleF()
                cellRectCurrent.Location = New Point(0, locationv)
                cellRectCurrent.Size = New Size(100, 15)

                e.Graphics.DrawString("Current Billing", headsubFont, Brushes.Black, cellRectCurrent, nearnear)
                e.Graphics.DrawString("|", headsubFont, Brushes.Black, 130, locationv)

                cellRectCurrent.Location = New Point(150, locationv)

                e.Graphics.DrawString(Format(reprintdata(0)("AmountDue"), "Standard"), headsubFont, Brushes.Black, cellRectCurrent, nearfar)

                locationv = locationv + 15

                Dim reprintmetering As New DataTable
                reprintmetering.Rows.Clear()
                stracs = "select * from BillCharges where BillNumber = " & billBillno.Text & " and Particulars = 'Metering Fee'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                'acsdr = acscmd.ExecuteReader
                acsda.SelectCommand = acscmd
                acsda.Fill(reprintmetering)

                If reprintmetering.Rows.Count = 0 Then
                Else
                    Dim cellRectMetering As RectangleF
                    cellRectMetering = New RectangleF()
                    cellRectMetering.Location = New Point(0, locationv)
                    cellRectMetering.Size = New Size(100, 15)

                    e.Graphics.DrawString("Metering Fee", headsubFont, Brushes.Black, cellRectMetering, nearnear)
                    e.Graphics.DrawString("|", headsubFont, Brushes.Black, 130, locationv)

                    cellRectMetering.Location = New Point(150, locationv)

                    e.Graphics.DrawString(reprintmetering(0)("Amount"), headsubFont, Brushes.Black, cellRectMetering, nearfar)

                    chargess = reprintmetering(0)("Amount")

                    locationv = locationv + 15
                End If

                If reprintdata(0)("Discount") = 0 Then
                Else

                    Dim cellRectSenior As RectangleF
                    cellRectSenior = New RectangleF()
                    cellRectSenior.Location = New Point(0, locationv)
                    cellRectSenior.Size = New Size(100, 15)

                    e.Graphics.DrawString("Senior Citizen Discount", headsubFont, Brushes.Black, cellRectSenior, nearnear)
                    e.Graphics.DrawString("|", headsubFont, Brushes.Black, 130, locationv)

                    cellRectSenior.Location = New Point(150, locationv)

                    e.Graphics.DrawString(reprintdata(0)("Discount"), headsubFont, Brushes.Black, cellRectSenior, nearfar)

                    locationv = locationv + 15

                End If

                If billarrears.Rows.Count = 0 Then
                Else


                    Dim arrearsamount As Double
                    For x = 0 To billarrears.Rows.Count - 1
                        arrearsamount = arrearsamount + Double.Parse(billarrears.Rows(x).Cells(2).Value)
                    Next

                    arrears = arrearsamount


                End If

                If billpromisorry.Rows.Count = 0 Then
                Else



                    Dim arrearsamount As Double
                    For x = 0 To billpromisorry.Rows.Count - 1
                        arrearsamount = arrearsamount + Double.Parse(billpromisorry.Rows(x).Cells(3).Value)
                    Next

                    pns = arrearsamount



                End If

                If (arrears + pns) = 0 Then

                Else

                    Dim cellRectArrears As RectangleF
                    cellRectArrears = New RectangleF()
                    cellRectArrears.Location = New Point(0, locationv)
                    cellRectArrears.Size = New Size(100, 15)

                    e.Graphics.DrawString("Arrears", headsubFont, Brushes.Black, cellRectArrears, nearnear)
                    e.Graphics.DrawString("|", headsubFont, Brushes.Black, 130, locationv)

                    cellRectArrears.Location = New Point(150, locationv)

                    e.Graphics.DrawString(Format(arrears + pns, "standard"), headsubFont, Brushes.Black, cellRectArrears, nearfar)

                    locationv = locationv + 15

                End If

                locationv = locationv + 15

                    Dim cellRectTotal As RectangleF
                    cellRectTotal = New RectangleF()
                    cellRectTotal.Location = New Point(0, locationv)
                    cellRectTotal.Size = New Size(100, 15)

                    e.Graphics.DrawString("Total Amount Due", headsubFont, Brushes.Black, cellRectTotal, nearnear)
                    e.Graphics.DrawString("|", headsubFont, Brushes.Black, 130, locationv)

                    cellRectTotal.Location = New Point(150, locationv)

                Dim totalamountdue As Double

                totalamountdue = (reprintdata(0)("AmountDue") - (reprintdata(0)("AdvancePayment") + reprintdata(0)("Discount"))) + chargess + arrears + pns

                    e.Graphics.DrawString(Format(totalamountdue, "standard"), headsubFont, Brushes.Black, cellRectTotal, nearfar)

                    locationv = locationv + 15

                Dim penafterdue As Double

                If reprintdata(0)("PenaltyAfterDue") = 0 Then



                    Dim getbillingarrears As New DataTable
                    stracs = "select ((SUM(AmountDue) + SUM(PenaltyAfterDue)) - SUM(AdvancePayment)) as billarrears 
                        from Bills where AccountNumber = '" & reprintdata(0)("AccountNumber") & "' and " _
                        & "IsPaid = 'No' and IsCollectionCreated = 'No' and isPromisorry = 'No' and Cancelled = 'No' and LastDayNOPen <= '" & Format(Date.Parse(reprintdata(0)("LastDayNOPen")), "yyyy-MM-dd") & "'"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsda.SelectCommand = acscmd
                    acsda.Fill(getbillingarrears)

                    'Dim billarrears As Double = Double.Parse(getbillingarrears(0)("billarrears"))

                    Dim billarrears As Double

                    If IsDBNull(getbillingarrears.Rows(0)("billarrears")) = True Then
                        billarrears = 0
                    Else
                        billarrears = Double.Parse(getbillingarrears(0)("billarrears"))
                    End If

                    Dim getbillingpn As New DataTable
                    stracs = "select (SUM(Billing) + SUM(Penalty)) as billpn 
                        from AddAdjustment where AccountNumber = '" & reprintdata(0)("AccountNumber") & "' and " _
                        & "Paid = 'No' and IsCollectionCreated = 'No' and Status = 'Posted' and cast(BillingDate as date) <= '" & Date.Parse(billCovered.Text) & "'"
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

                    penafterdue = (billarrears + totalpn) * 0.15

                Else
                    penafterdue = reprintdata(0)("PenaltyAfterDue")
                End If

                Dim cellRectAfterDue As RectangleF
                    cellRectAfterDue = New RectangleF()
                    cellRectAfterDue.Location = New Point(0, locationv)
                    cellRectAfterDue.Size = New Size(100, 15)

                e.Graphics.DrawString("Surcharge", headsubFont, Brushes.Black, cellRectAfterDue, nearnear)
                e.Graphics.DrawString("|", headsubFont, Brushes.Black, 130, locationv)

                    cellRectAfterDue.Location = New Point(150, locationv)

                    e.Graphics.DrawString(Format(penafterdue, "standard"), headsubFont, Brushes.Black, cellRectAfterDue, nearfar)

                    locationv = locationv + 15

                    e.Graphics.DrawString("________________________________________________________", headsubFont, Brushes.Black, 0, locationv)

                    locationv = locationv + 20

                    Dim cellRectAmountAfter As RectangleF
                    cellRectAmountAfter = New RectangleF()
                    cellRectAmountAfter.Location = New Point(0, locationv)
                    cellRectAmountAfter.Size = New Size(100, 15)

                    e.Graphics.DrawString("Amount After Due", headsubFont, Brushes.Black, cellRectAmountAfter, MidLeft)
                    e.Graphics.DrawString("|", headsubFont, Brushes.Black, 130, locationv)

                    cellRectAmountAfter.Location = New Point(150, locationv)

                    e.Graphics.DrawString(Format(Double.Parse(totalamountdue) + Double.Parse(penafterdue), "standard"), headsubFont, Brushes.Black, cellRectAmountAfter, MidRight)

                    locationv = locationv + 10

                    e.Graphics.DrawString("________________________________________________________", headsubFont, Brushes.Black, 0, locationv)

                    locationv = locationv + 20

                    Dim cellRectreprinted As RectangleF
                    cellRectreprinted = New RectangleF()
                    cellRectreprinted.Location = New Point(0, locationv)
                    cellRectreprinted.Size = New Size(280, 35)

                    e.Graphics.DrawString("----- Reprinted -----" & vbCrLf & Format(Now, "Short date"), headsubFont, Brushes.Black, cellRectreprinted, MidCenterhead)

                    locationv = locationv + 30

                    Dim getancon As New DataTable

                    Try
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    Catch ex As Exception
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    End Try
                    getancon.Rows.Clear()
                    stracs = "select * from tblannouncement"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsda.SelectCommand = acscmd
                    acsda.Fill(getancon)

                    If getancon.Rows(0)("Announce") = "" Or IsDBNull(getancon.Rows(0)("Announce")) = True Then

                    Else

                        locationv = locationv + 20


                        Dim cellRectAnouncement As RectangleF
                        cellRectAnouncement = New RectangleF()
                        cellRectAnouncement.Location = New Point(0, locationv)
                        cellRectAnouncement.Size = New Size(280, cellRectAnouncement.Height)

                        Dim TotalStringHeight As Single = e.Graphics.MeasureString(getancon.Rows(0)("Announce"), headsubFont, New SizeF(cellRectAnouncement.Width, cellRectAnouncement.Height), MidCenterhead).Height
                        Dim SingleLineHeight As Single = e.Graphics.MeasureString("T", headsubFont, New SizeF(cellRectAnouncement.Width, cellRectAnouncement.Height), MidCenterhead).Height

                        Dim NumberOfLines As Integer = Convert.ToInt32(TotalStringHeight / SingleLineHeight)

                        e.Graphics.DrawString(getancon.Rows(0)("Announce"), headsubFont, Brushes.Black, cellRectAnouncement, MidCenterhead)

                        locationv = locationv + 10

                    End If

                    If reprintdata(0)("MeterReader") = "Manual" Then

                        Dim testings As New DataTable
                        Dim meterreader As String

                        stracs = "select MeterReader from Bills where BillingDate = '" & billCovered.Text & "' and Zone = '" & testzne & "' and MeterReader <> 'Manual'"
                        acscmd.Connection = acsconn
                        acscmd.CommandText = stracs
                        acsda.SelectCommand = acscmd
                        acsda.Fill(testings)

                        If testings.Rows.Count = 0 Then
                            meterreader = "Manual"
                        Else
                            meterreader = testings.Rows(0)("MeterReader")
                        End If

                        stracs = "insert into ReprintRecord (BIllNo,AccountNo,DateReprint,MeterReader,ZoneName) values (" _
                    & reprintdata(0)("BillNo") & ", '" & reprintdata(0)("AccountNumber") & "', '" & Format(Now, "yyyy-MM-dd") & "', '" & meterreader & "','" & reprintdata(0)("Zone") & "')"

                    Else
                        stracs = "insert into ReprintRecord (BIllNo,AccountNo,DateReprint,MeterReader,ZoneName) values (" _
                    & reprintdata(0)("BillNo") & ", '" & reprintdata(0)("AccountNumber") & "', '" & Format(Now, "yyyy-MM-dd") & "', '" _
                    & reprintdata(0)("MeterReader") & "', '" & reprintdata(0)("Zone") & "')"

                    End If

                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                End If

                Else

            billBillno.Clear()
            billBillno.Select()

        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnReprint.Click

        Try

            printcrdocs.PrinterSettings.PrinterName = My.Settings.printerORCR
            printcrdocs.Print()

            'orderPreview.Document = printOrder
            'orderPreview.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

End Class