Public Class billingcreatebills


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Me.Close()

    End Sub



    Private Sub billingcreatebills_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        createReadingDate.Value = Now.AddDays(-30)
        createReadingDate.CustomFormat = "MMMM yyyy"



        Me.MdiParent = eBACSmain
        loadzones()
        loadreaders()
        loadannouncementandcontactno()



        createPreparedBills.Rows.Clear()

        For b = 0 To createZone.Rows.Count - 1

            createZone.Rows(b).Cells(2).Value = 0

        Next

    End Sub

    Public Sub loadannouncementandcontactno()

        Dim getancon As New DataTable

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        stracs = "select * from tblannouncement"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(getancon)

        If getancon.Rows(0)("Announce") = "" Or IsDBNull(getancon.Rows(0)("Announce")) = True Then

            announcement.Text = ""

        Else
            announcement.Text = getancon.Rows(0)("Announce")
        End If

        If getancon.Rows(1)("Announce") = "" Or IsDBNull(getancon.Rows(1)("Announce")) = True Then
            contactno.Text = ""
        Else
            contactno.Text = getancon.Rows(1)("Announce")
        End If

    End Sub

    Public Sub loadreaders()

        Dim readers As New DataTable
        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try
        readers.Clear()

        stracs = "select * from MeterReader"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(readers)

        createReader.Items.Clear()

        If readers.Rows.Count = 0 Then
        Else

            For x = 0 To readers.Rows.Count - 1

                createReader.Items.Add(readers(x)("readerName"))

            Next

        End If

    End Sub

    Public Sub loadzones()

        Dim loadcreatezones As New DataTable
        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try
        loadcreatezones.Clear()

        stracs = "select * from Zone"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(loadcreatezones)

        If loadcreatezones.Rows.Count = 0 Then
        Else
            createZone.Rows.Clear()

            For x = 0 To loadcreatezones.Rows.Count - 1

                createZone.Rows.Add(loadcreatezones(x)("ZoneID"), loadcreatezones(x)("ZoneName"), 0)

            Next

        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Cursor = Cursors.WaitCursor
        If createReader.SelectedIndex = -1 Then

            MsgBox("Please select Meter Reader.")
        Else
            createPreparedBills.Rows.Clear()
            For x = 0 To createZone.Rows.Count - 1

                If createZone.Rows(x).Cells(2).Value = -1 Then

                    Dim checkpending As New DataTable
                    Try
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    Catch ex As Exception
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    End Try
                    checkpending.Rows.Clear()

                    stracs = "select * from Bills where BillStatus = 'Pending' and Cancelled = 'No' and Zone = '" & createZone.Rows(x).Cells(1).Value & "'"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsda.SelectCommand = acscmd
                    acsda.Fill(checkpending)

                    If checkpending.Rows.Count = 0 Then

                        Dim billzone As New DataTable
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        billzone.Clear()

                        stracs = "select * from Customers where Zone = '" & createZone.Rows(x).Cells(1).Value & "' and CustomerStatus = 'Active' order by ReadingSeqNo asc"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acsda.SelectCommand = acscmd
                        acsda.Fill(billzone)

                        ProgressBar1.Value = 0
                        ProgressBar1.Maximum = billzone.Rows.Count
                        ProgressBar1.Visible = True

                        If billzone.Rows.Count = 0 Then
                        Else

                            For y = 0 To billzone.Rows.Count - 1

                                Dim searchbillperiod As New DataTable
                                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                searchbillperiod.Clear()

                                stracs = "select * from Bills where AccountNumber = '" & billzone.Rows(y)("AccountNo") & "' and BillingDate = '" & createReadingDate.Text & "' and Cancelled = 'No'"
                                acscmd.CommandText = stracs
                                acscmd.Connection = acsconn
                                acsda.SelectCommand = acscmd
                                acsda.Fill(searchbillperiod)

                                If searchbillperiod.Rows.Count = 0 Then

                                    Dim accountname As String
                                    If IsDBNull(billzone(y)("CompanyName")) = True Or billzone(y)("CompanyName") = "" Then
                                        accountname = billzone(y)("Firstname") & " " & billzone(y)("Middlename") & " " & billzone(y)("Lastname")
                                    Else
                                        accountname = billzone(y)("CompanyName")
                                    End If

                                    Dim arrearsbill As Double = 0
                                    Dim otherarrears As Double = 0
                                    Dim arrearscharge As Double = 0


                                    Dim loadbillarrears As New DataTable
                                    Dim otherbalance As New DataTable
                                    loadbillarrears.Clear()

                                    stracs = "select (SUM(AmountDue) + SUM(PenaltyAfterDue)) - (SUM(AdvancePayment) + SUM(Discount)) as amountarrears from Bills where IsPaid = 'No' and Cancelled = 'No' and AccountNumber = '" & billzone.Rows(y)("AccountNo") & "' and BillStatus = 'Posted' and IsCollectionCreated = 'No' and isPromisorry = 'No'"
                                    acscmd.Connection = acsconn
                                    acscmd.CommandText = stracs
                                    acsda.SelectCommand = acscmd
                                    acsda.Fill(loadbillarrears)

                                    If IsDBNull(loadbillarrears(0)("amountarrears")) = True Then
                                        arrearsbill = 0
                                    Else
                                        arrearsbill = loadbillarrears(0)("amountarrears")
                                    End If

                                    stracs = "select SUM(Billing) + SUM(Penalty) as otherarrears from AddAdjustment where Paid = 'No' and Status = 'Posted' and AccountNumber = '" & billzone.Rows(y)("AccountNo") & "' and IsCollectionCreated = 'No'"
                                    acscmd.Connection = acsconn
                                    acscmd.CommandText = stracs
                                    acsda.SelectCommand = acscmd
                                    acsda.Fill(otherbalance)

                                    If IsDBNull(otherbalance(0)("otherarrears")) = True Then
                                        otherarrears = 0
                                    Else
                                        otherarrears = otherbalance(0)("otherarrears")
                                    End If


                                    Dim loadchargesarrears As New DataTable
                                    loadchargesarrears.Clear()
                                    'stracs = "select a.Billno as Billno, a.BillingDate as BillingDate, (SUM(b.amount) + SUM(a.AmountDue) + SUM(a.PenaltyAfterDue)) - SUM(a.Discount) as amount from Bills a join BillCharges b on a.BillNo = b.BillNumber where a.IsPaid = 'No' and b.IsPaid = 'No' and a.BillNo = " & getarrears.Rows(t)("BillNo") & " and b.BillNumber = " & getarrears.Rows(t)("BillNo") & " group by a.BillNo, a.BillingDate"
                                    stracs = "select SUM(Amount) as Amount from BillCharges where IsPaid = 'No' and Cancelled = 'No' and AccountNumber = '" & billzone.Rows(y)("AccountNo") & "' and Status = 'Posted' and Category = 'Others' and IsCollectionCreated = 'No' and isPromisorry = 'No'"
                                    acscmd.Connection = acsconn
                                    acscmd.CommandText = stracs
                                    acsda.SelectCommand = acscmd
                                    acsda.Fill(loadchargesarrears)

                                    If IsDBNull(loadchargesarrears(0)("Amount")) = True Then
                                        arrearscharge = 0
                                    Else
                                        arrearscharge = loadchargesarrears(0)("Amount")
                                    End If

                                    createPreparedBills.Rows.Add(billzone(y)("AccountNo"), accountname, billzone(y)("ServiceAddress"), Format(billzone(y)("LasReadingDate"), "short date"),
                                            "", "", billzone(y)("LastMeterReading"), 0,
                                            0, "", createReadingDate.Text, "Pending", billzone(y)("RateSchedule"),
                                            billzone(y)("MeterSize"), billzone(y)("Zone"), billzone(y)("IsSenior"), 0, 0, "No", createReader.Text.ToString.Replace("'", "''"),
                                            billzone(y)("AdvancePayment"), billzone(y)("Averagee"), 0, billzone(y)("DontCharge"), "No", billzone(y)("MeterNo"), arrearsbill + otherarrears, arrearscharge, "", "", billzone(y)("isSpecialDiscount"))

                                Else
                                    'MsgBox("meron")
                                End If

                                ProgressBar1.Value = ProgressBar1.Value + 1

                            Next

                            ProgressBar1.Value = 0
                            ProgressBar1.Visible = False


                        End If

                    Else

                        MsgBox("There is a pending bill in " & createZone.Rows(x).Cells(1).Value & ". " & vbCrLf _
                            & "Please update all pending bills before creating new for this zone.")

                    End If

                Else

                End If

            Next
            createNoofaccounts.Text = createPreparedBills.Rows.Count
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Cursor = Cursors.WaitCursor
        If createPreparedBills.Rows.Count = 0 Then
        Else

            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try
            Dim getlastbillno As New DataTable

            getlastbillno.Rows.Clear()
            stracs = "select number from tbllogicnumbers where id = 1"
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(getlastbillno)

            Dim billno As Integer = getlastbillno(0)("number")

            ProgressBar1.Value = 0
            ProgressBar1.Maximum = createPreparedBills.Rows.Count
            ProgressBar1.Visible = True

            For x = 0 To createPreparedBills.Rows.Count - 1

                billno = billno + 1

                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                stracs = "insert into Bills ([BillNo], [AccountNumber], [CustomerName],[CustomerAddress],[DateFrom],[PreviousReading]
                    ,[Reading],[Consumption],[BillingDate],[BillStatus],[RateSchedule],[MeterSize],[Zone],[isSenior]
                    ,[AmountDue],[PenaltyAfterDue],[IsPaid],[MeterReader],[AdvancePayment],[AverageCons],[Discount],[DontCharge],[Cancelled],[MeterNumber]
                    ,[ArrearsBill],[ArrearsCharges],[CreatedBy],[DateCreated], [ArrearsInterest], [isSpecialDiscount]) values (" _
                   & billno & ", " _
                   & "'" & createPreparedBills.Rows(x).Cells(0).Value & "', " _
                   & "'" & createPreparedBills.Rows(x).Cells(1).Value.ToString.Replace("'", "''") & "', " _
                   & "'" & createPreparedBills.Rows(x).Cells(2).Value.ToString.Replace("'", "''") & "', " _
                   & "'" & Format(Date.Parse(createPreparedBills.Rows(x).Cells(3).Value), "yyyy-MM-dd") & "', " _
                   & Val(createPreparedBills.Rows(x).Cells(6).Value) & ", " _
                   & Val(createPreparedBills.Rows(x).Cells(7).Value) & ", " _
                   & Val(createPreparedBills.Rows(x).Cells(8).Value) & ", " _
                   & "'" & createPreparedBills.Rows(x).Cells(10).Value & "', " _
                   & "'" & createPreparedBills.Rows(x).Cells(11).Value & "', " _
                   & "'" & createPreparedBills.Rows(x).Cells(12).Value & "', " _
                   & "'" & createPreparedBills.Rows(x).Cells(13).Value & "', " _
                   & "'" & createPreparedBills.Rows(x).Cells(14).Value & "', " _
                   & "'" & createPreparedBills.Rows(x).Cells(15).Value & "', " _
                   & Val(createPreparedBills.Rows(x).Cells(16).Value) & ", " _
                   & Val(createPreparedBills.Rows(x).Cells(17).Value) & ", " _
                   & "'" & createPreparedBills.Rows(x).Cells(18).Value & "', " _
                   & "'" & createPreparedBills.Rows(x).Cells(19).Value.ToString.Replace("'", "''") & "', " _
                   & Val(createPreparedBills.Rows(x).Cells(20).Value) & ", " _
                   & Val(createPreparedBills.Rows(x).Cells(21).Value) & ", " _
                   & Val(createPreparedBills.Rows(x).Cells(22).Value) & ", " _
                   & "'" & createPreparedBills.Rows(x).Cells(23).Value & "', " _
                   & "'" & createPreparedBills.Rows(x).Cells(24).Value & "', " _
                   & "'" & createPreparedBills.Rows(x).Cells(25).Value.ToString.Replace("'", "''") & "', " _
                   & Val(createPreparedBills.Rows(x).Cells(26).Value) & ", " _
                   & Val(createPreparedBills.Rows(x).Cells(27).Value) & ", '" _
                   & My.Settings.Nickname & "', '" & Format(Now, "yyyy-MM-dd") & "', " & Val(createPreparedBills.Rows(x).Cells(29).Value) & ", '" & createPreparedBills.Rows(x).Cells(30).Value & "')"

                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                Dim getcharges As New DataTable

                getcharges.Rows.Clear()
                stracs = "select * from ScheduleCharges where AccountNumber = '" & createPreparedBills.Rows(x).Cells(0).Value & "' and ActiveInactive = 1"
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsda.SelectCommand = acscmd
                acsda.Fill(getcharges)

                If getcharges.Rows.Count = 0 Then
                    Dim membershipTable As New DataTable

                    membershipTable.Rows.Clear()
                    stracs = "select Membership_balance from Customers where AccountNo = '" & createPreparedBills.Rows(x).Cells(0).Value & "'"
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

                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "insert into BillCharges (BillNumber, AccountNumber, AccountName, BillingMonth, Zone, ChargeID, Category, Entry, Particulars, Amount, IsPaid, Reader, RateSchedule) values (" _
                                    & billno & ", '" & createPreparedBills.Rows(x).Cells(0).Value & "', '" & createPreparedBills.Rows(x).Cells(1).Value.ToString.Replace("'", "''") & "', '" & createPreparedBills.Rows(x).Cells(10).Value & "', '" _
                                    & createPreparedBills.Rows(x).Cells(14).Value & "', " & 0 & ", 'Others', 'Others', 'Membership Fee', " & billcharge_membership_fee & ", 'No', " & "'" & createPreparedBills.Rows(x).Cells(19).Value.ToString.Replace("'", "''") & "', '" _
                                    & createPreparedBills.Rows(x).Cells(12).Value & "')"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()
                    End If
                Else

                    For b = 0 To getcharges.Rows.Count - 1

                        If getcharges.Rows(b)("Recurring") = "Yes" Then

                            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                            stracs = "insert into BillCharges (BillNumber, AccountNumber, AccountName, BillingMonth, Zone, ChargeID, Category, Entry, Particulars, Amount, IsPaid, Reader, RateSchedule) values (" _
                                                & billno & ", '" & createPreparedBills.Rows(x).Cells(0).Value & "', '" & createPreparedBills.Rows(x).Cells(1).Value.ToString.Replace("'", "''") & "', '" & createPreparedBills.Rows(x).Cells(10).Value & "', '" _
                                                & createPreparedBills.Rows(x).Cells(14).Value & "', " & getcharges.Rows(b)("ChargeID") & ", '" & getcharges.Rows(b)("Category").ToString.Replace("'", "''") & "', '" & getcharges.Rows(b)("Entry").ToString.Replace("'", "''") & "', '" & getcharges.Rows(b)("Particular").ToString.Replace("'", "''") & "', " & Val(Format(getcharges(b)("Amount"), "standard")) & ", 'No', " & "'" & createPreparedBills.Rows(x).Cells(19).Value.ToString.Replace("'", "''") & "', '" _
                                                & createPreparedBills.Rows(x).Cells(12).Value & "')"
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acscmd.ExecuteNonQuery()
                            acscmd.Dispose()

                        Else

                            If MonthName(getcharges.Rows(b)("BillingMonth")) & " " & getcharges.Rows(b)("BillingYear") = createPreparedBills.Rows(x).Cells(10).Value Then

                                stracs = "insert into BillCharges (BillNumber, AccountNumber, AccountName, BillingMonth, Zone, ChargeID, Category, Entry, Particulars, Amount, IsPaid, Reader, RateSchedule) values (" _
                                                & billno & ", '" & createPreparedBills.Rows(x).Cells(0).Value & "', '" & createPreparedBills.Rows(x).Cells(1).Value.ToString.Replace("'", "''") & "', '" & createPreparedBills.Rows(x).Cells(10).Value & "', '" _
                                                & createPreparedBills.Rows(x).Cells(14).Value & "', " & getcharges.Rows(b)("ChargeID") & ", '" & getcharges.Rows(b)("Category").ToString.Replace("'", "''") & "', '" & getcharges.Rows(b)("Entry").ToString.Replace("'", "''") & "', '" & getcharges.Rows(b)("Particular").ToString.Replace("'", "''") & "', " & Val(Format(getcharges(b)("Amount"), "standard")) & ", 'No', " & "'" & createPreparedBills.Rows(x).Cells(19).Value.ToString.Replace("'", "''") & "','" _
                                                & createPreparedBills.Rows(x).Cells(12).Value & "')"
                                acscmd.CommandText = stracs
                                acscmd.Connection = acsconn
                                acscmd.ExecuteNonQuery()
                                acscmd.Dispose()

                            Else

                            End If

                        End If

                    Next
                End If

                ProgressBar1.Value = ProgressBar1.Value + 1

            Next

            If acsconn.State = ConnectionState.Closed Then acsconn.Open()

            Dim zones As String = ""
            Dim couters As Integer = 0

            For x = 0 To createZone.Rows.Count - 1

                If createZone.Rows(x).Cells(2).Value = -1 Then

                    couters = couters + 1

                    If couters = 1 Then

                        zones = "'" & createZone.Rows(x).Cells(1).Value & "'"

                    Else

                        zones = zones & ", '" & createZone.Rows(x).Cells(1).Value & "'"

                    End If


                Else

                End If

            Next

            stracs = "insert into Billstemp (
                       BILLID,BillNo,AccountNumber,CustomerName,CustomerAddress,DateFrom,ReadingDate,DueDate,LastDayNOPen,DiscDate 
                      ,PreviousReading,Reading,BillingDate,ForTheMonthOf,BillStatus,RateSchedule,MeterSize,Zone,isSenior,Consumption,AmountDue 
                      ,PenaltyAfterDue,IsPaid,MeterReader,AdvancePayment,AverageCons,LastCons,Discount,Adjustment,DontCharge,Cancelled,MeterNumber 
                      ,ArrearsBill,ArrearsCharges,CRNo,IsCollectionCreated,isPromisorry,PromisorryNo,CreatedBy,PostedBy,DateCreated,DatePaid,Writeoffref, isSpecialDiscount) 
                      SELECT 
                       BILLID,BillNo,AccountNumber,CustomerName,CustomerAddress,DateFrom,ReadingDate,DueDate,LastDayNOPen,DiscDate 
                      ,PreviousReading,Reading,BillingDate,ForTheMonthOf,BillStatus,RateSchedule,MeterSize,Zone,isSenior,Consumption,AmountDue 
                      ,PenaltyAfterDue,IsPaid,MeterReader,AdvancePayment,AverageCons,LastCons,Discount,Adjustment,DontCharge,Cancelled,MeterNumber 
                      ,ArrearsBill,ArrearsCharges,CRNo,IsCollectionCreated,isPromisorry,PromisorryNo,CreatedBy,PostedBy,DateCreated,DatePaid,Writeoffref, isSpecialDiscount 
                      FROM Bills where BillStatus = 'Pending' and Zone in (" & zones & ") and BillingDate = '" & createReadingDate.Text & "'"

            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()

            ProgressBar1.Visible = False

            stracs = "update tbllogicnumbers set number = " & billno & " where id = 1"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()

            MsgBox(createPreparedBills.Rows.Count & " bills created.")
            createPreparedBills.Rows.Clear()
            loadzones()

        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        Me.Activate()
    End Sub

    Public MoveFormbillingCreate As Boolean
    Public MovebillingCreate_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormbillingCreate = True
            Me.Cursor = Cursors.NoMove2D
            MovebillingCreate_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormbillingCreate Then
            Me.Location = Me.Location + (e.Location - MovebillingCreate_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormbillingCreate = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub billingcreatebills_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
        Me.BringToFront()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        stracs = "update tblannouncement set Announce = '" & announcement.Text.ToString.Replace("'", "''") & "' where AnnounceID = 1"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acscmd.ExecuteNonQuery()
        acscmd.Dispose()

        stracs = "update tblannouncement set Announce = '" & contactno.Text.ToString.Replace("'", "''") & "' where AnnounceID = 2"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acscmd.ExecuteNonQuery()
        acscmd.Dispose()

        loadannouncementandcontactno()

    End Sub

    Private Sub billingcreatebills_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.BackColor = Color.SteelBlue
    End Sub

    Private Sub billingcreatebills_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.BackColor = Color.FromArgb(17, 153, 195)
    End Sub

    Private Sub ControlsClick(sender As Object, e As EventArgs) Handles _
        Panel1.Click, Button1.Click, createZone.Click, Panel1.Click, createPreparedBills.Click, announcement.Click, createReadingDate.Click, createReader.Click,
        Button1.Click, Button3.Click, Button4.Click ' etc.
        Me.Activate() 'Or Whatever
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        billingcreatedbills.ShowDialog()

    End Sub

    Private Sub createZone_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles createZone.CurrentCellDirtyStateChanged

        If createZone.IsCurrentCellDirty Then
            createZone.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If

    End Sub

    Private Sub createZone_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles createZone.CellValueChanged

        createPreparedBills.Rows.Clear()

    End Sub


End Class