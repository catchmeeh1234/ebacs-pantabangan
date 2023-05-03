Imports System.Globalization
Imports Microsoft.Reporting

Public Class ReportofCollectionsandDeposits
    Private Sub ReportofCollectionsandDeposits_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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



    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) 
        Me.Close()
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
            .Columns.Add("orno")
            .Columns.Add("conces")
            .Columns.Add("paymentdetails")
            .Columns.Add("current")
            .Columns.Add("cyarrear")
            .Columns.Add("pyarrear")
            .Columns.Add("penalty")
            .Columns.Add("charges")
            .Columns.Add("sundries")
            .Columns.Add("total")
            .Columns.Add("misc")
            .Columns.Add("others")

        End With

        Dim designation As New DataTable
        Dim desig As String
        desig = ""
        designation.Clear()
        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        stracs = "select Designation FROM tblaccounts WHERE FullName = '" & eBACSmain.lblUserName.Text & "'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(designation)

        If designation.Rows.Count = 0 Then
        Else
            desig = designation.Rows(0)("Designation")
        End If


        Dim ordata As New DataTable
        ordata.Clear()
        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        stracs = "select * FROM OR_Details WHERE cast(PaymentDate as date) = '" & txtdate.Text & "' AND Office = '" & cboffice.Text & "' and Status = 'Posted'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(ordata)
        If ordata.Rows.Count = 0 Then
            ' MsgBox("No data found")
        Else

            For ke = 0 To ordata.Rows.Count - 1


                If ordata.Rows(ke)("Cancelled") = "Yes" Then

                    dt.Rows.Add(ordata.Rows(ke)("ORNo"), ordata.Rows(ke)("AccountName"), "CANCELLED" _
                                           , "0.00", "0.00", "0.00", "0.00" _
                                           , "0.00", "0.00", "0.00" _
                                           , "0.00", "0.00")

                Else

                    If ordata.Rows(ke)("Status") = "Pending" Then

                    Else


                        Dim oritems As New DataTable
                        oritems.Clear()
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "select * FROM ORItems WHERE ORNo = '" & ordata.Rows(ke)("ORNo") & "' and Cancelled = 'No'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acsda.SelectCommand = acscmd
                        acsda.Fill(oritems)

                        If oritems.Rows.Count = 0 Then

                        Else

                            Dim orcharges, orothers, orsundries, ormisc, ortotal As Decimal
                            orcharges = 0
                            orothers = 0
                            orsundries = 0
                            ormisc = 0
                            ortotal = 0

                            For kk = 0 To oritems.Rows.Count - 1


                                If oritems.Rows(kk)("Entry") = "Charges" Then
                                    orcharges = orcharges + Decimal.Parse(oritems.Rows(kk)("Total"))
                                    ortotal = ortotal + Decimal.Parse(oritems.Rows(kk)("Total"))

                                ElseIf oritems.Rows(kk)("Entry") = "Others" Then
                                    orothers = orothers + Decimal.Parse(oritems.Rows(kk)("Total"))
                                    ortotal = ortotal + Decimal.Parse(oritems.Rows(kk)("Total"))

                                ElseIf oritems.Rows(kk)("Entry") = "Sundries" Then
                                    orsundries = orsundries + Decimal.Parse(oritems.Rows(kk)("Total"))
                                    ortotal = ortotal + Decimal.Parse(oritems.Rows(kk)("Total"))

                                ElseIf oritems.Rows(kk)("Entry") = "Misc." OrElse oritems.Rows(kk)("Entry") = "Misc" Then
                                    ormisc = ormisc + Decimal.Parse(oritems.Rows(kk)("Total"))
                                    ortotal = ortotal + Decimal.Parse(oritems.Rows(kk)("Total"))
                                End If

                            Next

                            dt.Rows.Add(ordata.Rows(ke)("ORNo"), ordata.Rows(ke)("AccountName"), ordata.Rows(ke)("TransactionType") _
                                           , "0.00", "0.00", "0.00", "0.00" _
                                           , FormatNumber(orcharges), FormatNumber(orsundries), FormatNumber(ortotal) _
                                           , FormatNumber(ormisc), FormatNumber(orothers))


                        End If

                    End If
                End If

            Next

        End If

        Dim currentbill, cyarrear, pyarrear, penalty, charges, sundries, total As Decimal
        currentbill = 0
        cyarrear = 0
        pyarrear = 0
        penalty = 0
        charges = 0
        sundries = 0
        total = 0
        Dim natitira As Double = 0


        Dim collectiondata As New DataTable
        collectiondata.Clear()
        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        stracs = "select * FROM Collection_Details WHERE cast(PaymentDate as date) = '" & txtdate.Text & "' AND Cancelled = 'No' AND Office = '" & cboffice.Text & "' AND CollectionStatus = 'Posted'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(collectiondata)

        If collectiondata.Rows.Count = 0 Then
            ' MsgBox("No data found")
        Else
            For j = 0 To collectiondata.Rows.Count - 1


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

                        'Dim fullMonthName As DateTime
                        'Dim paymentdate As Date


                        'fullMonthName = DateTime.ParseExact(billsdata.Rows(y)("BillingDate"), "MMMM yyyy", CultureInfo.InvariantCulture)

                        'Dim billdate As Date
                        'billdate = billsdata.Rows(y)("BillingDate")
                        'paymentdate = collectiondata.Rows(j)("PaymentDate")

                        'Dim bmno As Integer = Month(fullMonthName)
                        'Dim paymno As Integer = Month(collectiondata.Rows(j)("PaymentDate")) - 1

                        'If Val(billdate.ToString("yyyy")) < Val(paymentdate.ToString("yyyy")) Then
                        '    'pyarrears
                        '    pyarrear = pyarrear + billsdata.Rows(y)("AmountDue") - (billsdata.Rows(y)("Discount") + billsdata.Rows(y)("AdvancePayment"))
                        '    penalty = penalty + billsdata.Rows(y)("PenaltyAfterDue")
                        'Else
                        '    If bmno >= paymno Then
                        '        'current
                        '        currentbill = currentbill + billsdata.Rows(y)("AmountDue") - (billsdata.Rows(y)("Discount") + billsdata.Rows(y)("AdvancePayment"))
                        '        penalty = penalty + billsdata.Rows(y)("PenaltyAfterDue")

                        '    Else
                        '        'cyarrear
                        '        cyarrear = cyarrear + billsdata.Rows(y)("AmountDue") - (billsdata.Rows(y)("Discount") + billsdata.Rows(y)("AdvancePayment"))
                        '        penalty = penalty + billsdata.Rows(y)("PenaltyAfterDue")
                        '    End If
                        'End If

                        Dim paymentdate, billdate As Date
                        billdate = billsdata.Rows(y)("ReadingDate")
                        paymentdate = collectiondata.Rows(j)("PaymentDate")

                        If IsDBNull(billsdata.Rows(y)("Adjustment")) = True Then
                            billsdata.Rows(y)("Adjustment") = 0
                        Else
                            billsdata.Rows(y)("Adjustment") = billsdata.Rows(y)("Adjustment")
                        End If

                        If ((billsdata.Rows(y)("AmountDue") + billsdata.Rows(y)("Adjustment")) - (billsdata.Rows(y)("Discount") + billsdata.Rows(y)("AdvancePayment"))) <= 0 Then

                            Dim charges2 As Double = 0
                            Dim chargesdata2 As New DataTable
                            chargesdata2.Clear()
                            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                            stracs = "select SUM(Amount) AS Totalcharges FROM BillCharges WHERE BillNumber = '" & billsdata.Rows(y)("BillNo") & "' AND isPromisorry = 'No' AND Category = 'Others' AND Entry = 'Others'"
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acsda.SelectCommand = acscmd
                            acsda.Fill(chargesdata2)

                            If IsDBNull(chargesdata2.Rows(0)("Totalcharges")) = True Then
                                charges2 = 0
                            Else
                                charges2 = chargesdata2.Rows(0)("Totalcharges")

                            End If

                            currentbill = currentbill + ((billsdata.Rows(y)("AmountDue") + billsdata.Rows(y)("Adjustment")) - (billsdata.Rows(y)("Discount") + billsdata.Rows(y)("AdvancePayment"))) + charges2

                        Else

                            If Val(billdate.ToString("yyyy")) < Val(paymentdate.ToString("yyyy")) Then
                                'pyarrears
                                pyarrear = pyarrear + (billsdata.Rows(y)("AmountDue") + billsdata.Rows(y)("Adjustment")) - (billsdata.Rows(y)("Discount") + billsdata.Rows(y)("AdvancePayment"))
                                penalty = penalty + billsdata.Rows(y)("PenaltyAfterDue")
                        Else
                            If billdate.ToString("MM") = paymentdate.ToString("MM") AndAlso billdate.ToString("yyyy") = paymentdate.ToString("yyyy") Then
                                    'current
                                    currentbill = currentbill + (billsdata.Rows(y)("AmountDue") + billsdata.Rows(y)("Adjustment")) - (billsdata.Rows(y)("Discount") + billsdata.Rows(y)("AdvancePayment"))
                                    penalty = penalty + billsdata.Rows(y)("PenaltyAfterDue")

                            Else
                                    'cyarrear
                                    cyarrear = cyarrear + (billsdata.Rows(y)("AmountDue") + billsdata.Rows(y)("Adjustment")) - (billsdata.Rows(y)("Discount") + billsdata.Rows(y)("AdvancePayment"))
                                    penalty = penalty + billsdata.Rows(y)("PenaltyAfterDue")
                            End If
                        End If


                        Dim chargesdata As New DataTable
                        chargesdata.Clear()
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "select SUM(Amount) AS Totalcharges FROM BillCharges WHERE BillNumber = '" & billsdata.Rows(y)("BillNo") & "' AND Category = 'Others' AND Entry = 'Others'"
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


                        Dim billdate As Date
                            'billdate = billsdata.Rows(b)("BillingDate")
                            'paymentdate = collectiondata.Rows(j)("PaymentDate")

                            billdate = PNdata.Rows(b)("BillingDate")
                            paymentdate = PNdata.Rows(b)("DatePaid")



                            Dim bmno As Integer = Month(fullMonthName)
                        Dim paymno As Integer = Month(collectiondata.Rows(j)("PaymentDate")) - 1


                        If Val(fullMonthName.ToString("yyyy")) < Val(paymentdate.ToString("yyyy")) Then
                            'pyarrears
                            pyarrear = pyarrear + PNdata.Rows(b)("Billing")
                            penalty = penalty + PNdata.Rows(b)("Penalty")
                        Else
                            'If fullMonthName.ToString("MM") = paymentdate.ToString("MM") AndAlso fullMonthName.ToString("yyyy") = paymentdate.ToString("yyyy") Then
                            If bmno >= paymno Then
                                'current
                                currentbill = currentbill + PNdata.Rows(b)("Billing")
                                penalty = penalty + PNdata.Rows(b)("Penalty")

                            Else
                                'cyarrear
                                cyarrear = cyarrear + PNdata.Rows(b)("Billing")
                                penalty = penalty + PNdata.Rows(b)("Penalty")
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

                sundries = sundries + collectiondata.Rows(j)("AdvancePayment")
                total = total + collectiondata.Rows(j)("TotalAmountDue")


                prog.Value = j / collectiondata.Rows.Count * 100
            Next
        End If

        ' prog.Value = t / bills.Rows.Count * 100

        Dim deposited As New DataTable
        deposited.Clear()

        Dim lastdeposit As New DataTable
        lastdeposit.Clear()


        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        stracs = "select * FROM DCR WHERE ReportDate = '" & txtdate.Value.ToString("yyyy-MM-dd") & "' AND OfficeName = '" & cboffice.Text & "'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(deposited)

        If deposited.Rows.Count = 0 Then

        Else



            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            stracs = "select * FROM DCR WHERE ReportDate = '" & deposited.Rows(0)("PreviousReportDate") & "' AND OfficeName = '" & cboffice.Text & "'"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(lastdeposit)


        End If


        Dim crfrom, crto, orfrom, orto As String

        Dim crnumbers As New DataTable
        crnumbers.Clear()

        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        stracs = "select * FROM Collection_Details WHERE cast(PaymentDate as date) = '" & txtdate.Value.ToString("yyyy-MM-dd") & "' AND Office = '" & cboffice.Text & "' AND CollectionStatus = 'Posted' ORDER by CollectionID ASC"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(crnumbers)

        If crnumbers.Rows.Count = 0 Then
            crfrom = ""
            crto = ""
        Else
            crfrom = crnumbers.Rows(0)("CRNo")
            crto = crnumbers.Rows(crnumbers.Rows.Count - 1)("CRNo")
        End If


        Dim ornumbers As New DataTable
        ornumbers.Clear()

        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        stracs = "select * FROM OR_Details WHERE cast(PaymentDate as date) = '" & txtdate.Value.ToString("yyyy-MM-dd") & "' AND Office = '" & cboffice.Text & "' AND Status = 'Posted' ORDER by OR_DetailsID ASC"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(ornumbers)

        If ornumbers.Rows.Count = 0 Then
            orfrom = ""
            orto = ""
        Else
            orfrom = ornumbers.Rows(0)("ORNo")
            orto = ornumbers.Rows(ornumbers.Rows.Count - 1)("ORNo")
        End If


        dt.DefaultView.Sort = "orno ASC"
        dt = dt.DefaultView.ToTable



        prog.Value = 100

        Dim Curdi As String = My.Application.Info.DirectoryPath
        Dim g As String
        g = Curdi.Replace("bin\Debug", "")

        Dim rds As New Microsoft.Reporting.WinForms.ReportDataSource
        rds.Name = "DataSet1"
        rds.Value = dt


        ReportViewer1.LocalReport.DataSources.Add(rds)
        'ReportViewer1.LocalReport.ReportPath = g & "ReportofCollectionsandDeposits.rdlc"
        ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\ReportofCollectionsandDeposits.rdlc"

        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("asof", txtdate.Value.ToString("dddd") & ", " & txtdate.Value.ToString("MMMM dd, yyyy")))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("office", cboffice.Text))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("billof", crnumbers.Rows.Count))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("current", FormatNumber(currentbill)))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("cyarrear", FormatNumber(cyarrear)))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("pyarrear", FormatNumber(pyarrear)))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("penalty", FormatNumber(penalty)))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("charges", FormatNumber(charges)))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("sundries", FormatNumber(sundries)))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("total", FormatNumber(total)))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("datee", txtdate.Value.ToString("MM-dd-yyyy")))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("user", My.Settings.Fullname))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("designation", My.Settings.Designation))

        If deposited.Rows.Count = 0 Then

            ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("undepositedlastcollection", "0.00"))
            ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("deposits", "0.00"))
            ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("dateee", ""))
            ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("dcr", ""))
            ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("dcrr", ""))

        Else

            ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("undepositedlastcollection", FormatNumber(lastdeposit.Rows(0)("Undeposited"))))
            ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("deposits", FormatNumber(deposited.Rows(0)("Deposited"))))
            ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("dateee", Format(Date.Parse(lastdeposit.Rows(0)("ReportDate")), "MMMM dd, yyyy")))
            ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("dcr", deposited.Rows(0)("DCRNo").ToString()))
            ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("dcrr", deposited.Rows(0)("DCRNo").ToString()))

        End If

        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("crfrom", crfrom))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("crto", crto))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("orfrom", orfrom))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("orto", orto))

        ReportViewer1.ZoomMode = WinForms.ZoomMode.PageWidth
        ReportViewer1.RefreshReport()

        billSearch.Enabled = True
        prog.Visible = False
        Cursor = Cursors.Default

    End Sub

    Private Sub ReportofCollectionsandDeposits_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Me.Activate()
    End Sub

    Public MoveFormRCD As Boolean
    Public MoveFormRCD_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormRCD = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormRCD_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormRCD Then
            Me.Location = Me.Location + (e.Location - MoveFormRCD_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormRCD = False
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
        Panel1.Click, txtdate.Click, cboffice.Click, billSearch.Click, ReportViewer1.Click ' etc.
        Me.Activate() 'Or Whatever
    End Sub
End Class