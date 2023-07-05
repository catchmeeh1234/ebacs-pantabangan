Imports System.ComponentModel
Imports System.Windows.Forms

Public Class eBACSmain

    Private Sub eBACSmain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If My.Settings.Admin = "Yes" Then

            settData.Visible = True
            UpdatePasswordToolStripMenuItem.Visible = True

        Else
            settData.Visible = False
            UpdatePasswordToolStripMenuItem.Visible = False
        End If

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.Manual
        With Screen.PrimaryScreen.WorkingArea
            Me.SetBounds(.Left, .Top, .Width, .Height)
        End With

        Timer1.Start()
        imageSettings.Location = New Point(Me.Size.Width - imageSettings.Size.Width - 20, 10)
        lblDateTime.Location = New Point(Me.Size.Width - 250, 10)
        lblUserName.Location = New Point(Me.Size.Width - 350, 10)
        Label2.Location = New Point(lblUserName.Location.X - (lblUserName.Width + 30), 10)
        'cellRecthead.Location = New Point(0, 0)

        Dim searchending As New DataTable
        stracs = "select * from Bills where Cancelled = 'No' and BillStatus = 'Pending'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(searchending)

        If searchending.Rows.Count > 0 Then

            'Dim qwe As DialogResult = MessageBox.Show("There are pending bills as of today. Please review Bills.", "Electronic Billing and Collection", MessageBoxButtons.OK)

        Else


        End If

        'Dim searchpenalty As New DataTable

        'If Now.DayOfWeek - 1 = DayOfWeek.Sunday Then
        '    'stracs = "select BillNo from Bills where LastDayNOPen = '" & Format(Now.AddDays(-3), "yyyy-MM-dd") & "' and PenaltyAfterDue = 0 and Cancelled = 'No' 
        '    'stracs = "select BillNo from Bills where LastDayNOPen = '" & Format(Now.AddDays(-3), "yyyy-MM-dd") & "' and PenaltyAfterDue = 0 and Cancelled = 'No' 
        '    '        and IsCollectionCreated = 'No' and IsPaid = 'No' and isPromisorry = 'No' and BillStatus = 'Posted'"
        '    stracs = "select BillNo, AccountNumber from Bills join customers on Bills.AccountNumber = customers.AccountNo where LastDayNOPen = '" & Format(Now.AddDays(-3), "yyyy-MM-dd") & "' and PenaltyAfterDue = 0 and Cancelled = 'No' 
        '               and IsCollectionCreated = 'No' and IsPaid = 'No' and isPromisorry = 'No' and BillStatus = 'Posted' and 
        'customers.CustomerStatus <> 'Disconnected' and Bills.DontCharge = 'No'"

        'ElseIf Now.DayOfWeek - 1 = DayOfWeek.Saturday Then
        '    'stracs = "select BillNo from Bills where LastDayNOPen = '" & Format(Now.AddDays(-2), "yyyy-MM-dd") & "' and PenaltyAfterDue = 0 and Cancelled = 'No' 
        '    'stracs = "select BillNo from Bills where LastDayNOPen = '" & Format(Now.AddDays(-2), "yyyy-MM-dd") & "' and PenaltyAfterDue = 0 and Cancelled = 'No' 
        '    '        and IsCollectionCreated = 'No' and IsPaid = 'No' and isPromisorry = 'No' and BillStatus = 'Posted'"
        '    stracs = "select BillNo, AccountNumber from Bills join customers on Bills.AccountNumber = customers.AccountNo where LastDayNOPen = '" & Format(Now.AddDays(-2), "yyyy-MM-dd") & "' and PenaltyAfterDue = 0 and Cancelled = 'No' 
        '               and IsCollectionCreated = 'No' and IsPaid = 'No' and isPromisorry = 'No' and BillStatus = 'Posted' and 
        'customers.CustomerStatus <> 'Disconnected' and Bills.DontCharge = 'No'"
        'Else
        '    'stracs = "select BillNo from Bills where LastDayNOPen = '" & Format(Now.AddDays(-1), "yyyy-MM-dd") & "' and PenaltyAfterDue = 0 and Cancelled = 'No' 
        '    'stracs = "select BillNo from Bills where LastDayNOPen = '" & Format(Now.AddDays(-1), "yyyy-MM-dd") & "' and PenaltyAfterDue = 0 and Cancelled = 'No' 
        '    '        and IsCollectionCreated = 'No' and IsPaid = 'No' and isPromisorry = 'No' and BillStatus = 'Posted'"
        '    stracs = "select BillNo, AccountNumber from Bills join customers on Bills.AccountNumber = customers.AccountNo where LastDayNOPen = '" & Format(Now.AddDays(-1), "yyyy-MM-dd") & "' and PenaltyAfterDue = 0 and Cancelled = 'No' 
        '               and IsCollectionCreated = 'No' and IsPaid = 'No' and isPromisorry = 'No' and BillStatus = 'Posted' and 
        'customers.CustomerStatus <> 'Disconnected' and Bills.DontCharge = 'No'"
        'End If


        'acscmd.CommandText = stracs
        'acscmd.Connection = acsconn
        'acsda.SelectCommand = acscmd
        'acsda.Fill(searchpenalty)


        'If searchpenalty.Rows.Count > 0 Then

        '    Dim asd As DialogResult = MessageBox.Show("There are bills that should be charged with a penalty today. Apply the penalty now?", "Electronic Billing and Collection", MessageBoxButtons.YesNo)

        '    If asd = DialogResult.Yes Then

        '        billingPenalty.Show()
        '        billingPenalty.Activate()

        '        Dim buwan As Integer

        '        buwan = Month(Now)

        '        If buwan = 1 Then
        '            buwan = 13
        '        Else
        '            buwan = Month(Now)
        '        End If

        '        If Now.DayOfWeek - 1 = DayOfWeek.Sunday Then

        '            If (Month(Now) - 1) = 0 Then

        '                billingPenalty.billMonth.Text = MonthName(12)
        '                billingPenalty.billYear.Text = Year(Now) - 1
        '                billingPenalty.lastdaynopen.Value = Format(Now.AddDays(-3), "Short date")

        '                billingPenalty.prepare_Click(Nothing, New KeyEventArgs(Keys.Enter))

        '            Else
        '                billingPenalty.billMonth.Text = MonthName(Month(Now) - 1)
        '                billingPenalty.billYear.Text = Year(Now)
        '                billingPenalty.lastdaynopen.Value = Format(Now.AddDays(-3), "Short date")

        '                billingPenalty.prepare_Click(Nothing, New KeyEventArgs(Keys.Enter))
        '            End If




        '        ElseIf Now.DayOfWeek - 1 = DayOfWeek.Saturday Then

        '            If (Month(Now) - 1) = 0 Then

        '                billingPenalty.billMonth.Text = MonthName(12)
        '                billingPenalty.billYear.Text = Year(Now) - 1
        '                billingPenalty.lastdaynopen.Value = Format(Now.AddDays(-2), "Short date")

        '                billingPenalty.prepare_Click(Nothing, New KeyEventArgs(Keys.Enter))

        '            Else
        '                billingPenalty.billMonth.Text = MonthName(Month(Now) - 1)
        '                billingPenalty.billYear.Text = Year(Now)
        '                billingPenalty.lastdaynopen.Value = Format(Now.AddDays(-2), "Short date")

        '                billingPenalty.prepare_Click(Nothing, New KeyEventArgs(Keys.Enter))
        '            End If

        '        Else

        '            If (Month(Now) - 1) = 0 Then
        '                billingPenalty.billMonth.Text = MonthName(12)
        '                billingPenalty.billYear.Text = Year(Now) - 1
        '                billingPenalty.lastdaynopen.Value = Format(Now.AddDays(-1), "Short date")

        '                billingPenalty.prepare_Click(Nothing, New KeyEventArgs(Keys.Enter))
        '            Else
        '                billingPenalty.billMonth.Text = MonthName(Month(Now) - 1)
        '                billingPenalty.billYear.Text = Year(Now)
        '                billingPenalty.lastdaynopen.Value = Format(Now.AddDays(-1), "Short date")

        '                billingPenalty.prepare_Click(Nothing, New KeyEventArgs(Keys.Enter))
        '            End If

        '        End If

        '    Else
        '    End If

        'Else

        'End If

        Dim searchpenalty As New DataTable

        stracs = "select AccountNumber, MIN(ReadingDate) as minReadingDate, MAX(ReadingDate) as maxReadingDate, MAX(BillNo) as BIllno, max(lastdateofinterest) as lastdateofinterest from Bills where 
                    IsCollectionCreated = 'No' 
                    and Cancelled = 'No' 
                    and IsPaid = 'No' 
                    and Readingdate is not null 
                    and (lastdateofinterest is null or DATEADD(MONTH, 1, lastdateofinterest) <= cast(getdate() as Date))
                    group by ReadingDate, AccountNumber, lastdateofinterest having DATEADD(MONTH, 1, MAX(ReadingDate)) <= cast(getdate() as Date)"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(searchpenalty)


        If searchpenalty.Rows.Count > 0 Then

            'Dim asd As DialogResult = MessageBox.Show("There are bills that should be charged with an interest. Apply the interest now?", "Electronic Billing and Collection", MessageBoxButtons.YesNo)


            For x = 0 To searchpenalty.Rows.Count - 1

                Dim ilangbuwan As Integer = 0

                Dim days As Long = DateDiff(DateInterval.Month, searchpenalty.Rows(x)("minReadingDate"), Now)


                'Dim startDate As DateTime = Date.Parse(searchpenalty.Rows(x)("minReadingDate"))
                'Dim endDate As DateTime = Date.Parse(Now)
                'Dim months As Integer = Math.Abs(startDate.Subtract(endDate).Days / (365 / 12))



                For y = 0 To days - 1



                Next


            Next

        Else

        End If

        Label1.Select()

    End Sub

    Private Sub imageSettings_Click(sender As Object, e As EventArgs) Handles imageSettings.Click
        contextSettings.Show(MousePosition.X - 100, MousePosition.Y)
    End Sub

    Private Sub settExit_Click(sender As Object, e As EventArgs) Handles settExit.Click
        'Application.Exit()

        Dim unpostedbill As New DataTable
        Dim cunpostedcr As New DataTable
        Dim unpostedor As New DataTable
        Dim unpostedbills, cunpostedcrs, unpostedors As String

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        stracs = "update useraccounts set ActiveSession = 0 where fullName = '" & My.Settings.Fullname & "'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acscmd.ExecuteNonQuery()
        acscmd.Dispose()

        stracs = "select BillStatus from Bills where BillStatus = 'Pending' and Cancelled = 'No'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(unpostedbill)

        If unpostedbill.Rows.Count = 0 Then

            unpostedbills = ""

        Else
            unpostedbills = "(Bills)"
        End If

        stracs = "select CollectionStatus from Collection_Details where CollectionStatus = 'Pending' and Cancelled = 'No'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(cunpostedcr)

        If cunpostedcr.Rows.Count = 0 Then

            cunpostedcrs = ""

        Else
            cunpostedcrs = "(CR)"
        End If

        stracs = "select Status from OR_Details where Status = 'Pending' and Cancelled = 'No'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(unpostedor)

        If unpostedor.Rows.Count = 0 Then

            unpostedors = ""

        Else
            unpostedors = "(OR)"
        End If

        If unpostedbills = "(Bills)" Or cunpostedcrs = "(CR)" Or unpostedors = "(OR)" Then

            Dim asd As DialogResult = MessageBox.Show("There are " & unpostedbills & cunpostedcrs & unpostedors & " not yet posted. Are you sure you want to close eBacs?", "Electronic Billing and Collection", MessageBoxButtons.YesNo)

            If asd = DialogResult.Yes Then
                login.Show()
                login.BringToFront()
                Me.Close()
            Else
                Me.Show()
                Me.Activate()
            End If

        Else

            login.Show()
            login.BringToFront()
            Me.Close()

        End If

    End Sub

    Private Sub btnConcessionaire_Click(sender As Object, e As EventArgs) Handles btnConcessionaire.Click
        cmsConcessionaire.Show(btnConcessionaire.Location.X, btnConcessionaire.Location.Y + 85)
    End Sub

    Private Sub MinimizeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MinimizeToolStripMenuItem.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub ToolStripMenuItem24_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem24.Click
        Collection_CR.Show()
        Collection_CR.Activate()
    End Sub

    Private Sub btnCollections_Click(sender As Object, e As EventArgs) Handles btnCollections.Click
        contextCR.Show(btnCollections.Location.X, btnCollections.Location.Y + 85)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lblDateTime.Text = Format(Now, "MMM dd, yyyy | hh:mm tt")
    End Sub

    Private Sub btnCollections_Click_1(sender As Object, e As EventArgs)
        contextCR.Show(MousePosition)
    End Sub

    Private Sub btnBilling_Click(sender As Object, e As EventArgs) Handles btnBilling.Click
        cmsBilling.Show(btnBilling.Location.X, btnBilling.Location.Y + 85)
    End Sub

    Private Sub btnReceipts_Click(sender As Object, e As EventArgs) Handles btnReceipts.Click
        contextOR.Show(btnReceipts.Location.X, btnReceipts.Location.Y + 85)
    End Sub

    Private Sub btnSettings_Click(sender As Object, e As EventArgs)
        cmsSettings.Show(MousePosition)
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click


        'If ActiveMdiChild.Contains(customerinfo) = True Then

        '    customerinfo.Activate()
        'Else
        'customerinfo.conspanel.Controls.Clear()
        '    concessionaireaccounts.Parent = customerinfo.conspanel
        '    concessionaireaccounts.Dock = DockStyle.Fill
        '    customerinfo.conspanel.Controls.Add(concessionaireaccounts)




        customerinfo.Show()
        customerinfo.Activate()

        'End If




    End Sub

    Private Sub ToolStripMenuItem6_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem6.Click

        connectionsettings.transferreading()

        billinginfo.Show()
        billinginfo.Activate()
        billinginfo.billBillno.Select()

    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        ScheduleOfAR.Close()
        ScheduleOfAR.Show()
        ScheduleOfAR.Activate()
        ScheduleOfAR.Label7.Text = "Schedule of A/R - Water"
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        ScheduleOfAR.Close()
        ScheduleOfAR.Show()
        ScheduleOfAR.Activate()
        ScheduleOfAR.Label7.Text = "Schedule of A/R - Others"
    End Sub

    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        Aging.Show()
        Aging.Activate()
        Aging.Label7.Text = "Aging Report (Detailed)"
    End Sub

    Private Sub ToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem5.Click
        Aging.Show()
        Aging.BringToFront()
        Aging.Label7.Text = "Aging Report (Summary)"
    End Sub

    Private Sub ListOfDisconnectedAccountsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ListOfDisconnectedAccountsToolStripMenuItem.Click
        Disconnected.Show()
        Disconnected.Activate()
        'ListofActiveConnection.Show()
        'ListofActiveConnection.Activate()
        'ListofActiveConnection.Label7.Text = "List of Disconnected Accounts"
    End Sub

    Private Sub ListOfActiveConnectionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ListOfActiveConnectionsToolStripMenuItem.Click
        ListofActiveConnection.Show()
        ListofActiveConnection.Activate()
        ListofActiveConnection.Label7.Text = "List of Active Connections"
    End Sub

    Private Sub MasterlistOfServiceConnectionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterlistOfServiceConnectionToolStripMenuItem.Click
        MasterListofServiceConnections.Show()
        MasterListofServiceConnections.Activate()
    End Sub

    Private Sub ConcessionaireBreakdownByTypeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConcessionaireBreakdownByTypeToolStripMenuItem.Click
        ConcessionaireBreakdownbyType.Show()
        ConcessionaireBreakdownbyType.Activate()
    End Sub

    Private Sub ToolStripMenuItem7_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem7.Click

        connectionsettings.transferreading()

        billingcreatebills.Show()
        billingcreatebills.Activate()
    End Sub

    Private Sub ToolStripMenuItem8_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem8.Click

        connectionsettings.transferreading()

        billingpostbills.Show()
        billingpostbills.Activate()

    End Sub

    Private Sub ToolStripMenuItem20_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem20.Click

        connectionsettings.transferreading()

        billingAdjustmentBill.Show()
        billingAdjustmentBill.Activate()
        billingAdjustmentBill.adjustAccountNo.Select()

    End Sub

    Private Sub ToolStripMenuItem21_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem21.Click

        connectionsettings.transferreading()

        billingAdjustmentOther.Show()

        billingAdjustmentOther.Activate()
        billingAdjustmentOther.adjustAccountNo.Select()

    End Sub

    Private Sub ToolStripMenuItem22_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem22.Click

        connectionsettings.transferreading()

        billingPenalty.Show()
        billingPenalty.Activate()

    End Sub

    Private Sub ToolStripMenuItem9_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem9.Click
        BillingList.Show()
        BillingList.Activate()
    End Sub

    Private Sub ToolStripMenuItem10_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem10.Click
        ConsumptionList.Show()
        ConsumptionList.Activate()
    End Sub

    Private Sub ToolStripMenuItem11_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem11.Click
        BillingSummary.Show()
        BillingSummary.Activate()
    End Sub

    Private Sub ToolStripMenuItem13_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem13.Click
        ListofPenaltyCharges.Show()
        ListofPenaltyCharges.Activate()
    End Sub

    Private Sub MonthlyAdjustmentSummaryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MonthlyAdjustmentSummaryToolStripMenuItem.Click
        AdjustmentSummary.Show()
        AdjustmentSummary.Activate()
    End Sub

    Private Sub ToolStripMenuItem17_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem17.Click
        settingsZone.Show()
        settingsZone.Activate()
    End Sub

    Private Sub ToolStripMenuItem18_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem18.Click
        settingsclass.Show()
        settingsclass.Activate()
    End Sub

    Private Sub ChargesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChargesToolStripMenuItem.Click
        settingscharges.Show()
        settingscharges.Activate()
    End Sub

    Private Sub MaterialsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MaterialsToolStripMenuItem.Click
        settingsMaterials.Show()
        settingsMaterials.Activate()
    End Sub

    Private Sub DiscountsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DiscountsToolStripMenuItem.Click
        settingsdiscount.Show()
        settingsdiscount.Activate()
    End Sub

    Private Sub MeterReaderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MeterReaderToolStripMenuItem.Click
        settingsreaders.Show()
        settingsreaders.Activate()
    End Sub

    Private Sub ToolStripMenuItem29_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem29.Click
        CollectionPost.Show()
        CollectionPost.Activate()
    End Sub

    Private Sub ToolStripMenuItem23_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem23.Click
        Create_OR.Show()
        Create_OR.Activate()
    End Sub

    Private Sub ToolStripMenuItem27_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem27.Click
        ORPost.Show()
        ORPost.Activate()
    End Sub

    Private Sub btnSettings_Click_1(sender As Object, e As EventArgs) Handles btnSettings.Click
        cmsSettings.Show(btnSettings.Location.X, btnSettings.Location.Y + 85)
    End Sub

    Private Sub AccountManagementToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AccountManagementToolStripMenuItem.Click

        If My.Settings.Admin = "Yes" Then
            SettingsAccounts.Show()
            SettingsAccounts.BringToFront()
        Else
            MsgBox("Your account cannot perform this process.")
        End If


    End Sub

    Private Sub DailyCollectionReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DailyCollectionReportToolStripMenuItem.Click
        DailyCollectionReport.Show()
        DailyCollectionReport.BringToFront()
    End Sub

    Private Sub OfficialReceiptsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OfficialReceiptsToolStripMenuItem.Click
        ORCollected.Show()
        ORCollected.BringToFront()
    End Sub

    Private Sub CollectionDepositToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CollectionDepositToolStripMenuItem.Click
        CollectionsAndDeposits.Show()
        CollectionsAndDeposits.BringToFront()
    End Sub

    Private Sub ReportOfCollectionsAndDepositsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportOfCollectionsAndDepositsToolStripMenuItem.Click
        ReportofCollectionsandDeposits.Show()
        ReportofCollectionsandDeposits.BringToFront()
    End Sub

    Private Sub settPrint_Click(sender As Object, e As EventArgs) Handles settPrint.Click
        settingsPrinter.ShowDialog()
    End Sub

    Private Sub DisconnectionListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisconnectionListToolStripMenuItem.Click
        DisconnectionList.Show()
        DisconnectionList.BringToFront()
    End Sub


    Private Sub ToolStripMenuItem15_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem15.Click
        ConsumptionPatternAnalysis.Show()
        ConsumptionPatternAnalysis.BringToFront()
    End Sub

    Private Sub ToolStripMenuItem16_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem16.Click
        SeniorCitizenDiscounts.Show()
        SeniorCitizenDiscounts.BringToFront()
    End Sub

    Private Sub ToolStripMenuItem26_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem26.Click
        CollectedPayments.Show()
        CollectedPayments.BringToFront()
    End Sub

    Private Sub CancelledToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CancelledToolStripMenuItem.Click
        CancelledReceipts.Show()
        CancelledReceipts.BringToFront()
    End Sub

    Private Sub OpenCRAndORWCalcToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenCRAndORWCalcToolStripMenuItem.Click

        Collection_CR.Show()
        Collection_CR.BringToFront()

        'MsgBox(My.Computer.Screen.WorkingArea.Height)
        Collection_CR.Location = New Point(0, ((My.Computer.Screen.WorkingArea.Height / 2) - (Collection_CR.Width / 2)) / 4)
        ' MsgBox(((My.Computer.Screen.WorkingArea.Height / 2) - (Collection_CR.Width / 2)) / 4)


        Create_OR.Show()
        Create_OR.BringToFront()

        Create_OR.Location = New Point(0 + Collection_CR.Width, ((My.Computer.Screen.WorkingArea.Height / 2) - (Collection_CR.Width / 2)) / 4)

        Calculator.Show()
        Calculator.BringToFront()

        Calculator.Location = New Point(0 + Collection_CR.Width + Create_OR.Width, ((My.Computer.Screen.WorkingArea.Height / 2) - (Collection_CR.Width / 2)) / 4)

        Collection_CR.BringToFront()
        Collection_CR.billAccountNo.Select()

    End Sub

    Private Sub ToolStripMenuItem25_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub WaterRatesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WaterRatesToolStripMenuItem.Click
        SettingsRates.Show()
        SettingsRates.Activate()
    End Sub

    Private Sub FiledFindingsTemplateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FiledFindingsTemplateToolStripMenuItem.Click
        SettingsFieldFindings.Show()
        SettingsFieldFindings.Activate()
    End Sub

    Private Sub ToolStripMenuItem30_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem30.Click

        connectionsettings.transferreading()

        billingDisconnection.Show()
        billingDisconnection.Activate()
    End Sub

    Private Sub ToolStripMenuItem28_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem28.Click

        CancellledOR.Show()
        CancellledOR.Activate()

    End Sub

    Private Sub CalculatorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CalculatorToolStripMenuItem.Click

        Calculator.Show()
        Calculator.BringToFront()

    End Sub

    Private Sub TransactionTemplateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransactionTemplateToolStripMenuItem.Click
        TransactionTemplate.Show()
        TransactionTemplate.Activate()
    End Sub

    Private Sub TempCRToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TempCRToolStripMenuItem.Click
        TempCR.ShowDialog()
        TempCR.Activate()
    End Sub

    Private Sub LedgerReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LedgerReportToolStripMenuItem.Click
        LedgerReport.Show()
        LedgerReport.Activate()
    End Sub

    Private Sub ReprintedBillsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReprintedBillsToolStripMenuItem.Click
        reprintBIllsReport.MdiParent = Me
        reprintBIllsReport.Show()
        reprintBIllsReport.Activate()
    End Sub

    Private Sub CancelledBillsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CancelledBillsToolStripMenuItem.Click
        CancelledBill.MdiParent = Me
        CancelledBill.Show()
        CancelledBill.Activate()
    End Sub

    Private Sub UpdatePasswordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdatePasswordToolStripMenuItem.Click

        If My.Settings.Admin = "Yes" Then



        End If

    End Sub

    Private Sub WriteOffToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WriteOffToolStripMenuItem.Click
        writeoff.Show()
        writeoff.Activate()
    End Sub

    Private Sub WriteOffReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WriteOffReportToolStripMenuItem.Click
        writeoffreport.Show()
        writeoffreport.Activate()
    End Sub

    Private Sub settData_Click(sender As Object, e As EventArgs) Handles settData.Click



    End Sub

    Private Sub ViewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewToolStripMenuItem.Click

        MsgBox("Server Name:" & vbCrLf & vbCrLf & My.Settings.dbServerIp)


    End Sub

    Private Sub ChangeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeToolStripMenuItem.Click


        Dim servername As String = InputBox("Enter Server Name/Server IP Address.", "Update Check Number")

        If servername = "" Then

        Else

            My.Settings.dbServerIp = servername
            My.Settings.Save()

            Application.Exit()

        End If

    End Sub

    Private Sub MonthlyPromissoryAndOtherAdjustmentReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MonthlyPromissoryAndOtherAdjustmentReportToolStripMenuItem.Click
        OtherAdjustmentReport.Show()
        OtherAdjustmentReport.Activate()
    End Sub

    Private Sub BillComputationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BillComputationToolStripMenuItem.Click

        billcompute.Show()
        billcompute.Activate()

    End Sub

    Private Sub ToolStripMenuItem12_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem12.Click

        Online_Billing.Show()
        Online_Billing.Activate()

    End Sub

    Private Sub UploadOnlinePaymentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UploadOnlinePaymentToolStripMenuItem.Click

        uploadpayment.Show()
        uploadpayment.Activate()

    End Sub

    Private Sub CreateOfForOtherFeesToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Create_OR.Show()
        Create_OR.Activate()
    End Sub

    Private Sub EditORNumberToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditORNumberToolStripMenuItem.Click
        TempCR.ShowDialog()
        TempCR.Activate()
    End Sub

    Private Sub CancelBillChargesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CancelBillChargesToolStripMenuItem.Click
        CancelBillCharges.Show()
        CancelBillCharges.BringToFront()
    End Sub

    Private Sub UnpaidConcessionairesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UnpaidConcessionairesToolStripMenuItem.Click
        UnpaidConcessionairesReport.Show()
        UnpaidConcessionairesReport.BringToFront()
    End Sub

    Private Sub ReconnectionListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReconnectionListToolStripMenuItem.Click
        ReconnectionList.Show()
        ReconnectionList.BringToFront()
    End Sub

    Private Sub MonthlyCollectionReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MonthlyCollectionReportToolStripMenuItem.Click
        MonthlyCollectionReport.Show()
        MonthlyCollectionReport.BringToFront()
    End Sub
End Class
