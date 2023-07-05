Imports System.Globalization
Imports Microsoft.Reporting

Public Class DailyCollectionReport
    Private Sub DailyCollectionReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain

        prog.Visible = False
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.ZoomMode.PageWidth)
        Me.ReportViewer1.RefreshReport()


        Dim office As New DataTable
        office.Clear()
        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        stracs = "select Distinct Office FROM Collection_Details"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(office)

        If office.Rows.Count = 0 Then
        Else
            cboffice.Items.Clear()


            For t = 0 To office.Rows.Count - 1
                cboffice.Items.Add(office.Rows(t)("Office"))
            Next

        End If

        If cboffice.Items.Count = 0 Then
            cboffice.SelectedIndex = -1
        Else
            cboffice.SelectedIndex = 0
        End If





        Me.ReportViewer1.RefreshReport()
        Me.ReportViewer1.RefreshReport()
        Me.ReportViewer1.RefreshReport()
    End Sub

    Private Sub billSearch_Click(sender As Object, e As EventArgs) Handles billSearch.Click
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.Reset()

        loaddata()
    End Sub

    Sub loaddata()

        Cursor = Cursors.WaitCursor
        prog.Value = 0
        prog.Visible = True
        billSearch.Enabled = False

        Dim dt As New DataTable

        With dt
            .Columns.Add("crno")
            .Columns.Add("conces")
            .Columns.Add("accno")
            .Columns.Add("current")
            .Columns.Add("cyarrear")
            .Columns.Add("pyarrear")
            .Columns.Add("penalty")
            .Columns.Add("charges")
            .Columns.Add("surcharge")
            .Columns.Add("earlyPayment")
            .Columns.Add("total")

        End With


        'Dim designation As New DataTable
        'Dim desig As String
        'desig = ""
        'designation.Clear()
        'If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        'stracs = "select designation FROM useraccounts WHERE fullname = '" & eBACSmain.lblUserName.Text & "'"
        'acscmd.CommandText = stracs
        'acscmd.Connection = acsconn
        'acsda.SelectCommand = acscmd
        'acsda.Fill(designation)

        'If designation.Rows.Count = 0 Then
        'Else
        '    desig = designation.Rows(0)("designation")
        'End If


        Dim collectiondata As New DataTable
        collectiondata.Clear()
        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        'stracs = "select * FROM Collection_Details WHERE YEAR(PaymentDate) = '" & txtdate.Value.ToString("yyyy") & "' AND MONTH(PaymentDate) = '" & txtdate.Value.ToString("MM") & "'
        'AND DAY(PaymentDate) = '" & txtdate.Value.ToString("dd") & "' AND Office = '" & cboffice.Text & "' and CollectionStatus = 'Posted'"

        stracs = "select * FROM Collection_Details WHERE CAST(PaymentDate as Date) = '" & Format(txtdate.Value, "yyyy-MM-dd") & "' AND Office = '" & cboffice.Text & "' and CollectionStatus = 'Posted'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(collectiondata)

        If collectiondata.Rows.Count = 0 Then
            MsgBox("No data found")
        Else
            Dim orData As New DataTable
            orData.Clear()
            stracs = "select * FROM OR_Details WHERE CAST(PaymentDate as Date) = '" & Format(txtdate.Value, "yyyy-MM-dd") & "' AND Office = '" & cboffice.Text & "' and Status = 'Posted' and Cancelled ='No'"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(orData)

            For q = 0 To orData.Rows.Count - 1
                Dim or_items_table As New DataTable
                or_items_table.Clear()
                Dim total_or_items As Decimal = 0.00
                stracs = "select * FROM ORItems WHERE ORNo = '" & orData.Rows(q)("ORNo") & "' and Cancelled ='No' AND Particular LIKE '%membership%'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(or_items_table)

                For f = 0 To or_items_table.Rows.Count - 1
                    total_or_items = total_or_items + or_items_table.Rows(f)("Total")
                Next

                Dim readingseqno As New DataTable
                readingseqno.Clear()
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                stracs = "select ReadingSeqNo FROM Customers WHERE AccountNo = '" & orData.Rows(q)("AccountNo") & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(readingseqno)

                For Each row As DataRow In readingseqno.Rows
                    ' Access values in each row and perform operations
                    Dim seqNo As String = row("ReadingSeqNo")
                    dt.Rows.Add(orData.Rows(q)("ORNo"), orData.Rows(q)("AccountName"), orData.Rows(q)("AccountNo") & "(" & seqNo & ")", "0.00" _
                            , "0.00", "0.00", "0.00", total_or_items, "0.00", "0.00", total_or_items)

                Next
            Next

            For j = 0 To collectiondata.Rows.Count - 1


                If collectiondata.Rows(j)("Cancelled") = "Yes" Then


                    dt.Rows.Add(collectiondata.Rows(j)("CRNo"), "CANCELLED", collectiondata.Rows(j)("AccountNo") _
                                    , "0.00", "0.00", "0.00", "0.00" _
                                    , "0.00", "0.00", "0.00", "0.00")

                Else

                    Dim currentbill, cyarrear, pyarrear, penalty, charges, surcharge, sundries, earlyPayment, disc_senior, specialDisc, total As Decimal
                    currentbill = 0
                    cyarrear = 0
                    pyarrear = 0
                    penalty = 0
                    charges = 0
                    surcharge = 0
                    sundries = 0
                    earlyPayment = 0
                    disc_senior = 0
                    specialDisc = 0
                    total = 0
                    Dim natitira As Double = 0

                    Dim billsdata As New DataTable
                    billsdata.Clear()
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    stracs = "select * FROM Bills WHERE CRNo = '" & collectiondata.Rows(j)("CRNo") & "' ORDER by BILLID DESC"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsda.SelectCommand = acscmd
                    acsda.Fill(billsdata)

                    If billsdata.Rows.Count = 0 Then
                    Else



                        For y = 0 To billsdata.Rows.Count - 1
                            Dim paymentdate, billdate As Date
                            billdate = billsdata.Rows(y)("ReadingDate")
                            paymentdate = collectiondata.Rows(j)("PaymentDate")

                            If IsDBNull(billsdata.Rows(y)("Surcharge")) = True Then
                                surcharge = surcharge + 0
                            Else
                                surcharge = surcharge + billsdata.Rows(y)("Surcharge")
                            End If

                            If IsDBNull(billsdata.Rows(y)("earlyPaymentDiscount")) = True Then
                                earlyPayment = earlyPayment + 0
                            Else
                                earlyPayment = earlyPayment + billsdata.Rows(y)("earlyPaymentDiscount")
                            End If

                            If IsDBNull(billsdata.Rows(y)("Discount")) = True Then
                                disc_senior = disc_senior + 0
                            Else
                                disc_senior = disc_senior + billsdata.Rows(y)("Discount")
                            End If

                            If IsDBNull(billsdata.Rows(y)("specialDiscount")) = True Then
                                specialDisc = specialDisc + 0
                            Else
                                specialDisc = specialDisc + billsdata.Rows(y)("specialDiscount")
                            End If

                            If IsDBNull(billsdata.Rows(y)("Adjustment")) = True Then
                                billsdata.Rows(y)("Adjustment") = 0
                            Else
                                billsdata.Rows(y)("Adjustment") = billsdata.Rows(y)("Adjustment")
                            End If

                            If ((billsdata.Rows(y)("AmountDue") + billsdata.Rows(y)("Adjustment")) - (billsdata.Rows(y)("Discount") + billsdata.Rows(y)("specialDiscount") + billsdata.Rows(y)("AdvancePayment"))) <= 0 Then


                                Dim charges2 As Double = 0
                                Dim chargesdata2 As New DataTable
                                chargesdata2.Clear()
                                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                stracs = "select SUM(Amount) AS Totalcharges FROM BillCharges WHERE BillNumber = '" & billsdata.Rows(y)("BillNo") & "' AND isPromisorry = 'No' AND Category = 'Others' AND Entry = 'Others' AND Particulars NOT LIKE '%membership%'"
                                acscmd.CommandText = stracs
                                acscmd.Connection = acsconn
                                acsda.SelectCommand = acscmd
                                acsda.Fill(chargesdata2)

                                If IsDBNull(chargesdata2.Rows(0)("Totalcharges")) = True Then
                                    charges2 = 0
                                Else
                                    charges2 = chargesdata2.Rows(0)("Totalcharges")

                                End If


                                natitira = (natitira + ((billsdata.Rows(y)("AmountDue") + billsdata.Rows(y)("Adjustment")) - (billsdata.Rows(y)("Discount") + billsdata.Rows(y)("specialDiscount") + billsdata.Rows(y)("AdvancePayment")))) + charges2


                            Else

                                If Val(billdate.ToString("yyyy")) < Val(paymentdate.ToString("yyyy")) Then
                                    'pyarrears
                                    pyarrear = pyarrear + (billsdata.Rows(y)("AmountDue") + billsdata.Rows(y)("Adjustment")) - (billsdata.Rows(y)("Discount") + billsdata.Rows(y)("specialDiscount") + billsdata.Rows(y)("AdvancePayment"))
                                    penalty = penalty + billsdata.Rows(y)("PenaltyAfterDue")
                                Else
                                    If billdate.ToString("MM") = paymentdate.ToString("MM") AndAlso billdate.ToString("yyyy") = paymentdate.ToString("yyyy") Then
                                        'current
                                        currentbill = (currentbill + +billsdata.Rows(y)("Adjustment") + billsdata.Rows(y)("AmountDue") + natitira) - (billsdata.Rows(y)("Discount") + billsdata.Rows(y)("specialDiscount") + billsdata.Rows(y)("AdvancePayment"))
                                        penalty = penalty + billsdata.Rows(y)("PenaltyAfterDue")

                                    Else
                                        'cyarrear
                                        cyarrear = cyarrear + billsdata.Rows(y)("AmountDue") + billsdata.Rows(y)("Adjustment") - (billsdata.Rows(y)("Discount") + billsdata.Rows(y)("specialDiscount") + billsdata.Rows(y)("AdvancePayment"))
                                        penalty = penalty + billsdata.Rows(y)("PenaltyAfterDue")
                                    End If
                                End If


                                Dim chargesdata As New DataTable
                                chargesdata.Clear()
                                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                stracs = "select SUM(Amount) AS Totalcharges FROM BillCharges WHERE BillNumber = '" & billsdata.Rows(y)("BillNo") & "' AND isPromisorry = 'No' AND Category = 'Others' AND Entry = 'Others' AND Particulars NOT LIKE '%membership%'"
                                acscmd.CommandText = stracs
                                acscmd.Connection = acsconn
                                acsda.SelectCommand = acscmd
                                acsda.Fill(chargesdata)

                                If IsDBNull(chargesdata.Rows(0)("Totalcharges")) = True Then
                                    charges = charges + 0
                                Else
                                    charges = charges + chargesdata.Rows(0)("Totalcharges")

                                End If

                            End If



                        Next


                    End If


                    Dim PNdata As New DataTable
                    PNdata.Clear()
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    stracs = "select * FROM AddAdjustment WHERE CRNo = '" & collectiondata.Rows(j)("CRNo") & "' ORDER by ID DESC"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsda.SelectCommand = acscmd
                    acsda.Fill(PNdata)

                    If PNdata.Rows.Count = 0 Then
                    Else


                        For b = 0 To PNdata.Rows.Count - 1


                            Dim fullMonthName As DateTime
                            Dim paymentdate As Date


                            fullMonthName = DateTime.ParseExact(PNdata.Rows(b)("BillingDate"), "MMMM yyyy", CultureInfo.InvariantCulture)
                            paymentdate = collectiondata.Rows(j)("PaymentDate")

                            If PNdata.Rows(b)("Particulars") = "Forwarded Balance" Or PNdata.Rows(b)("Particulars") = "Remaining Cons." Then


                                If Val(fullMonthName.ToString("yyyy")) < Val(paymentdate.ToString("yyyy")) Then
                                    'pyarrears
                                    pyarrear = pyarrear + PNdata.Rows(b)("Billing")
                                    penalty = penalty + PNdata.Rows(b)("Penalty")

                                Else



                                    Dim bmno As Integer = Month(fullMonthName)
                                    Dim paymno As Integer = Month(paymentdate) - 1

                                    If bmno >= paymno Then
                                        'current
                                        currentbill = currentbill + PNdata.Rows(b)("Billing")
                                        penalty = penalty + PNdata.Rows(b)("Penalty")

                                    Else
                                        'cyarrear
                                        cyarrear = cyarrear + PNdata.Rows(b)("Billing")
                                        penalty = penalty + PNdata.Rows(b)("Penalty")

                                    End If

                                    'If fullMonthName.ToString("MM") = paymentdate.ToString("MM") AndAlso fullMonthName.ToString("yyyy") = paymentdate.ToString("yyyy") Then
                                    '    'current
                                    '    currentbill = currentbill + PNdata.Rows(b)("Billing")
                                    '    penalty = penalty + PNdata.Rows(b)("Penalty")

                                    'Else
                                    '    'cyarrear
                                    '    cyarrear = cyarrear + PNdata.Rows(b)("Billing")
                                    '    penalty = penalty + PNdata.Rows(b)("Penalty")
                                    'End If

                                End If

                            End If

                            If PNdata.Rows(b)("Particulars") = "Promissory" Then

                                If Val(fullMonthName.ToString("yyyy")) < Val(paymentdate.ToString("yyyy")) Then
                                    'pyarrears
                                    pyarrear = pyarrear + PNdata.Rows(b)("Billing")
                                    penalty = penalty + PNdata.Rows(b)("Penalty")
                                Else

                                    Dim billingdate As Date = paymentdate.AddMonths(-1)


                                    If fullMonthName.ToString("MM") = billingdate.ToString("MM") AndAlso fullMonthName.ToString("yyyy") = billingdate.ToString("yyyy") Then
                                        'current
                                        currentbill = currentbill + PNdata.Rows(b)("Billing")
                                        penalty = penalty + PNdata.Rows(b)("Penalty")

                                    Else
                                        'cyarrear
                                        cyarrear = cyarrear + PNdata.Rows(b)("Billing")
                                        penalty = penalty + PNdata.Rows(b)("Penalty")
                                    End If
                                End If

                            End If

                        Next

                    End If

                    Dim PNcharges As New DataTable
                    PNcharges.Clear()
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    stracs = "select SUM(Amount) AS Totalcharges FROM BillCharges WHERE CRNo = '" & collectiondata.Rows(j)("CRNo") & "' AND isPromisorry = 'YesPosted' "
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsda.SelectCommand = acscmd
                    acsda.Fill(PNcharges)

                    If IsDBNull(PNcharges.Rows(0)("Totalcharges")) = True Then
                        charges = charges + 0
                    Else
                        charges = charges + PNcharges.Rows(0)("Totalcharges")

                    End If

                    'get reconnection fee
                    Dim collection_charges As New DataTable
                    collection_charges.Clear()
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    stracs = "select SUM(Amount) AS TotalCollectionCharges FROM CollectionCharges WHERE CRNo = '" & collectiondata.Rows(j)("CRNo") & "' AND Particulars = 'Reconnection Fee'"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsda.SelectCommand = acscmd
                    acsda.Fill(collection_charges)

                    If IsDBNull(collection_charges.Rows(0)("TotalCollectionCharges")) = True Then
                        charges = charges + 0
                    Else
                        charges = charges + collection_charges.Rows(0)("TotalCollectionCharges")

                    End If

                    Dim readingseqno As New DataTable
                    readingseqno.Clear()
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    stracs = "select ReadingSeqNo FROM Customers WHERE AccountNo = '" & collectiondata.Rows(j)("AccountNo") & "'"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsda.SelectCommand = acscmd
                    acsda.Fill(readingseqno)

                    sundries = collectiondata.Rows(j)("AdvancePayment")
                    total = collectiondata.Rows(j)("TotalAmountDue")

                    For Each row As DataRow In readingseqno.Rows
                        ' Access values in each row and perform operations
                        Dim seqNo As String = row("ReadingSeqNo")
                        dt.Rows.Add(collectiondata.Rows(j)("CRNo"), collectiondata.Rows(j)("AccountName"), collectiondata.Rows(j)("AccountNo") & "(" & seqNo & ")" _
                                    , FormatNumber(currentbill + natitira), FormatNumber(cyarrear), FormatNumber(pyarrear), FormatNumber(penalty) _
                                    , FormatNumber(charges), FormatNumber(surcharge), FormatNumber(disc_senior + earlyPayment + specialDisc), FormatNumber(total))
                    Next

                    prog.Value = j / collectiondata.Rows.Count * 100

                End If
            Next

        End If

        dt.DefaultView.Sort = "crno ASC"
        dt = dt.DefaultView.ToTable


        ' prog.Value = t / bills.Rows.Count * 100

        prog.Value = 100

        Dim Curdi As String = My.Application.Info.DirectoryPath
        Dim g As String
        g = Curdi.Replace("bin\Debug", "")

        Dim rds As New Microsoft.Reporting.WinForms.ReportDataSource
        rds.Name = "DataSet1"
        rds.Value = dt

        ReportViewer1.LocalReport.DataSources.Add(rds)
        'ReportViewer1.LocalReport.ReportPath = g & "DailyCollectionReport.rdlc"
        ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\DailyCollectionReport.rdlc"
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("dcr", txtdcrno.Text))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("asof", txtdate.Value.ToString("dddd") & ", " & txtdate.Value.ToString("MMMM dd, yyyy")))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("office", cboffice.Text))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("user", My.Settings.Fullname))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("designation", My.Settings.Designation))


        ReportViewer1.ZoomMode = WinForms.ZoomMode.PageWidth
        ReportViewer1.RefreshReport()

        billSearch.Enabled = True
        prog.Visible = False
        Cursor = Cursors.Default

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub DailyCollectionReport_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Me.Activate()
    End Sub

    Public MoveFormDCR As Boolean
    Public MoveFormDCR_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormDCR = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormDCR_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormDCR Then
            Me.Location = Me.Location + (e.Location - MoveFormDCR_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormDCR = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub postbill_deactivated(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.BackColor = Color.FromArgb(17, 153, 195)
    End Sub

    Private Sub postbill_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.BackColor = Color.SteelBlue
    End Sub

    Private Sub ControlsClick(sender As Object, e As EventArgs) Handles _
        Panel1.Click, txtdate.Click, txtdcrno.Click, cboffice.Click, billSearch.Click ' etc.
        Me.Activate() 'Or Whatever
    End Sub

    Private Sub prog_Click(sender As Object, e As EventArgs) Handles prog.Click

    End Sub

    Private Sub ReportViewer1_Load(sender As Object, e As EventArgs) Handles ReportViewer1.Load

    End Sub
End Class