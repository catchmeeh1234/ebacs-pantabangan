Imports Microsoft.Reporting

Public Class BillingSummary
    Private Sub billSearch_Click(sender As Object, e As EventArgs) Handles billSearch.Click
        Cursor = Cursors.WaitCursor

        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.Reset()

        If cbtype.SelectedIndex = 0 Then
            itemizedsummaryofbilling()
        Else
            summaryofbilling()
        End If

        Cursor = Cursors.Default
    End Sub

    Private Sub BillingSummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.ZoomMode.PageWidth)
        Me.ReportViewer1.RefreshReport()
        Timer1.Start()
        cbtype.SelectedIndex = 0
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) 
        Me.Close()
    End Sub

    Sub summaryofbilling()

        Dim constotalforbilledwater As Decimal

        Dim dt As New DataTable

        With dt
            .Columns.Add("zone")
            .Columns.Add("conn")
            .Columns.Add("cumused")
            .Columns.Add("amount")
            .Columns.Add("totalcharges")
            .Columns.Add("totalamount")


        End With
        constotalforbilledwater = 0

        If acsconn.State = ConnectionState.Closed Then acsconn.Open()


        Dim billsummary As New DataTable


        billsummary.Clear()
        stracs = "SELECT distinct a.Zone, c.ZoneID, count(a.AccountNumber) as bilang, Sum(a.Consumption) as Consumption, SUm(a.AmountDue) as AmountDue,
                  billchargerss = (select sum(Amount) from billcharges b where Zone = a.Zone and b.BillingMonth = '" & ReadingDate.Text & "' AND b.Cancelled = 'No' and Status = 'Posted')
                  FROM Bills a join dbo.Zone c on a.Zone = c.ZoneName where BillingDate = '" & ReadingDate.Text & "' AND a.Cancelled = 'No' and Billstatus = 'Posted' group by a.Zone, c.ZoneID order by c.ZoneID"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(billsummary)

        If billsummary.Rows.Count = 0 Then
            MsgBox("No data Found")
        Else


            For u = 0 To billsummary.Rows.Count - 1


                Dim billcharge As Decimal
                'Dim adjustment As Decimal


                If (IsDBNull(billsummary.Rows(u)("billchargerss")) = True) Then
                    billcharge = 0
                Else
                    billcharge = billsummary.Rows(u)("billchargerss")
                End If

                'If (IsDBNull(billsummary.Rows(u)("adjustment")) = True) Then
                '    adjustment = 0
                'Else
                '    adjustment = billsummary.Rows(u)("adjustment")
                'End If


                dt.Rows.Add(Format(billsummary.Rows(u)("ZoneID"), "00") & " - " & billsummary.Rows(u)("ZONE"), billsummary.Rows(u)("bilang"), billsummary.Rows(u)("Consumption"), FormatNumber(billsummary.Rows(u)("AmountDue")), FormatNumber(billcharge), FormatNumber(billsummary.Rows(u)("AmountDue") + billcharge))
                constotalforbilledwater = constotalforbilledwater + billsummary.Rows(u)("Consumption")
            Next
        End If

        Dim Curdi As String = My.Application.Info.DirectoryPath
        Dim g As String
        g = Curdi.Replace("bin\Debug", "")


        Dim rds As New Microsoft.Reporting.WinForms.ReportDataSource
        rds.Name = "DataSet1"
        rds.Value = dt


        ReportViewer1.LocalReport.DataSources.Add(rds)
        'ReportViewer1.LocalReport.ReportPath = g & "SummaryofBillingSummary.rdlc"
        'ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\SummaryofBillingSummary.rdlc"
        ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\SummaryofBilling.rdlc"

        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("BillMonth", ReadingDate.Text))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("pumpproduction", txtpumpproduction.Text))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("billedwater", constotalforbilledwater))

        ReportViewer1.ZoomMode = WinForms.ZoomMode.PageWidth
        ReportViewer1.RefreshReport()



        'Dim zonelist As New DataTable

        'zonelist.Clear()
        'stracs = "select distinct ZONE FROM BIlls WHERE BillingDate = '" & ReadingDate.Text & "' ORDER by Zone ASC"
        'acscmd.CommandText = stracs
        'acscmd.Connection = acsconn
        'acsda.SelectCommand = acscmd
        'acsda.Fill(zonelist)


        'If zonelist.Rows.Count = 0 Then
        '    MsgBox("No Data Found")


        'Else

        '    For y = 0 To zonelist.Rows.Count - 1

        '        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        '        Dim billno As New DataTable

        '        billno.Clear()
        '        stracs = "select BillNo FROM BIlls WHERE Zone = '" & zonelist.Rows(y)("ZONE") & "' AND BillingDate = '" & ReadingDate.Text & "'"
        '        acscmd.CommandText = stracs
        '        acscmd.Connection = acsconn
        '        acsda.SelectCommand = acscmd
        '        acsda.Fill(billno)

        '        rowcount = billno.Rows.Count

        '        If billno.Rows.Count = 0 Then

        '        Else

        '            constotal = 0
        '            amountduedecimal = 0
        '            amout = 0
        '            advancedicimal = 0
        '            penaltydecimal = 0
        '            discountdecimal = 0
        '            totalchargesdecimal = 0
        '            For t = 0 To billno.Rows.Count - 1


        '                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        '                Dim summarybill As New DataTable

        '                summarybill.Clear()
        '                stracs = "select SUM(Consumption) as ConsTotal FROM BIlls WHERE BillNo = '" & billno.Rows(t)("BillNo") & "'"
        '                acscmd.CommandText = stracs
        '                acscmd.Connection = acsconn
        '                acsda.SelectCommand = acscmd
        '                acsda.Fill(summarybill)

        '                If IsDBNull(summarybill.Rows(0)("ConsTotal")) = True Then
        '                    constotal = constotal + 0
        '                Else
        '                    constotal = constotal + summarybill.Rows(0)("ConsTotal")
        '                End If

        '                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        '                Dim amountdue As New DataTable

        '                amountdue.Clear()
        '                stracs = "select SUM(AmountDue) as AmountDueTotal, SUM(AdvancePayment) as AdvancePayment, SUM(PenaltyAfterDue) as Penalty, SUM(Discount) as Discount FROM BIlls WHERE BillNo = '" & billno.Rows(t)("BillNo") & "'"
        '                acscmd.CommandText = stracs
        '                acscmd.Connection = acsconn
        '                acsda.SelectCommand = acscmd
        '                acsda.Fill(amountdue)

        '                If IsDBNull(amountdue.Rows(0)("AmountDueTotal")) = True Then
        '                    amout = amout + 0
        '                Else
        '                    amout = amout + amountdue.Rows(0)("AmountDueTotal")
        '                End If

        '                If IsDBNull(amountdue.Rows(0)("AdvancePayment")) = True Then
        '                    advancedicimal = advancedicimal + 0
        '                Else
        '                    advancedicimal = advancedicimal + amountdue.Rows(0)("AdvancePayment")
        '                End If

        '                'If IsDBNull(amountdue.Rows(0)("Penalty")) = True Then
        '                '    penaltydecimal = penaltydecimal + 0
        '                'Else
        '                '    penaltydecimal = penaltydecimal + amountdue.Rows(0)("Penalty")
        '                'End If

        '                If IsDBNull(amountdue.Rows(0)("Discount")) = True Then
        '                    discountdecimal = discountdecimal + 0
        '                Else
        '                    discountdecimal = discountdecimal + amountdue.Rows(0)("Discount")
        '                End If

        '                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        '                Dim dttotalcharges As New DataTable

        '                dttotalcharges.Clear()
        '                stracs = "select SUM(Amount) as TotalCharges FROM BillCharges WHERE BillNumber = '" & billno.Rows(t)("BillNo") & "'"
        '                acscmd.CommandText = stracs
        '                acscmd.Connection = acsconn
        '                acsda.SelectCommand = acscmd
        '                acsda.Fill(dttotalcharges)

        '                If IsDBNull(dttotalcharges.Rows(0)("TotalCharges")) = True Then
        '                    totalchargesdecimal = totalchargesdecimal + 0
        '                Else
        '                    totalchargesdecimal = totalchargesdecimal + dttotalcharges.Rows(0)("TotalCharges")
        '                End If

        '            Next

        '        End If

        'amountduedecimal = (amout) - (advancedicimal + discountdecimal)
        'dt.Rows.Add(zonelist.Rows(y)("ZONE"), rowcount, constotal, FormatNumber(amountduedecimal), FormatNumber(totalchargesdecimal), FormatNumber(amountduedecimal + totalchargesdecimal))
        'constotalforbilledwater = constotalforbilledwater + constotal
        '    Next

        'End If







    End Sub

    Sub itemizedsummaryofbilling()

        Dim constotalforbilledwater As Decimal

        Dim dt As New DataTable

        With dt
            .Columns.Add("zone")
            .Columns.Add("conn")
            .Columns.Add("cumused")
            .Columns.Add("amount")
            .Columns.Add("totalcharges")
            .Columns.Add("totalamount")
            .Columns.Add("classs")


        End With

        constotalforbilledwater = 0


        If acsconn.State = ConnectionState.Closed Then acsconn.Open()


        Dim billingitemized As New DataTable

        billingitemized.Clear()
        'stracs = "SELECT distinct a.Zone, a.RateSchedule, c.ZoneID, count(a.AccountNumber) as bilang, Sum(a.Consumption) as Consumption, SUm(a.AmountDue) as AmountDue,
        '            billchargerss = (select sum(Amount) from billcharges b where Zone = a.Zone and b.BillingMonth = '" & ReadingDate.Text & "' and b.billnumber in (SELECT BillNo FROM Bills where zone = a.Zone and RateSchedule in (a.RateSchedule) and BillingDate = '" & ReadingDate.Text & "' and Cancelled = 'No' and Billstatus = 'Posted'))
        '             FROM Bills a join dbo.Zone c on a.Zone = c.ZoneName where BillingDate = '" & ReadingDate.Text & "' and Cancelled = 'No' and Billstatus = 'Posted' group by a.Zone, c.ZoneID, a.RateSchedule order by c.ZoneID"
        'acscmd.CommandText = stracs
        'acscmd.Connection = acsconn
        'acsda.SelectCommand = acscmd

        stracs = "SELECT distinct a.Zone, a.RateSchedule, c.ZoneID, count(a.AccountNumber) as bilang, Sum(a.Consumption) as Consumption, SUm(a.AmountDue) as AmountDue,
                    billchargerss = (select sum(Amount) from billcharges b where Zone = a.Zone and b.BillingMonth = '" & ReadingDate.Text & "' and RateSchedule = a.RateSchedule AND b.Cancelled = 'No' and b.Status = 'Posted')
                     FROM Bills a join dbo.Zone c on a.Zone = c.ZoneName where BillingDate = '" & ReadingDate.Text & "' and Cancelled = 'No' and Billstatus = 'Posted' group by a.Zone, c.ZoneID, a.RateSchedule order by c.ZoneID"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(billingitemized)

        If billingitemized.Rows.Count = 0 Then

        Else
            For y = 0 To billingitemized.Rows.Count - 1

                Dim billcharge As Decimal


                If (IsDBNull(billingitemized.Rows(y)("billchargerss")) = True) Then
                    billcharge = 0
                Else
                    billcharge = billingitemized.Rows(y)("billchargerss")
                End If

                If cbselectall.Checked = True Then
                    dt.Rows.Add(Format(billingitemized.Rows(y)("ZoneID"), "00") & " - " & billingitemized.Rows(y)("Zone"), billingitemized.Rows(y)("bilang") _
                                , billingitemized.Rows(y)("Consumption"), FormatNumber(billingitemized.Rows(y)("AmountDue")), FormatNumber(billcharge) _
                                , FormatNumber(billingitemized.Rows(y)("AmountDue") + billcharge), billingitemized.Rows(y)("RateSchedule"))
                    constotalforbilledwater = constotalforbilledwater + billingitemized.Rows(y)("Consumption")
                Else

                    If cbresid.Checked = True Then
                        If billingitemized.Rows(y)("RateSchedule") = "Residential" Then
                            dt.Rows.Add(Format(billingitemized.Rows(y)("ZoneID"), "00") & " - " & billingitemized.Rows(y)("Zone"), billingitemized.Rows(y)("bilang") _
                                , billingitemized.Rows(y)("Consumption"), FormatNumber(billingitemized.Rows(y)("AmountDue")), FormatNumber(billcharge) _
                                , FormatNumber(billingitemized.Rows(y)("AmountDue") + billcharge), billingitemized.Rows(y)("RateSchedule"))
                            constotalforbilledwater = constotalforbilledwater + billingitemized.Rows(y)("Consumption")
                        End If
                    End If

                    If cbcommInd.Checked = True Then
                        If billingitemized.Rows(y)("RateSchedule") = "Commercial/Industrial" Then
                            dt.Rows.Add(Format(billingitemized.Rows(y)("ZoneID"), "00") & " - " & billingitemized.Rows(y)("Zone"), billingitemized.Rows(y)("bilang") _
                                , billingitemized.Rows(y)("Consumption"), FormatNumber(billingitemized.Rows(y)("AmountDue")), FormatNumber(billcharge) _
                                , FormatNumber(billingitemized.Rows(y)("AmountDue") + billcharge), billingitemized.Rows(y)("RateSchedule"))
                            constotalforbilledwater = constotalforbilledwater + billingitemized.Rows(y)("Consumption")
                        End If
                    End If

                    If cbbulk.Checked = True Then
                        If billingitemized.Rows(y)("RateSchedule") = "Bulk/Wholesale" Then
                            dt.Rows.Add(Format(billingitemized.Rows(y)("ZoneID"), "00") & " - " & billingitemized.Rows(y)("Zone"), billingitemized.Rows(y)("bilang") _
                                , billingitemized.Rows(y)("Consumption"), FormatNumber(billingitemized.Rows(y)("AmountDue")), FormatNumber(billcharge) _
                                , FormatNumber(billingitemized.Rows(y)("AmountDue") + billcharge), billingitemized.Rows(y)("RateSchedule"))
                            constotalforbilledwater = constotalforbilledwater + billingitemized.Rows(y)("Consumption")
                        End If
                    End If

                    If cbcommA.Checked = True Then
                        If billingitemized.Rows(y)("RateSchedule") = "Commercial-A" Then
                            dt.Rows.Add(Format(billingitemized.Rows(y)("ZoneID"), "00") & " - " & billingitemized.Rows(y)("Zone"), billingitemized.Rows(y)("bilang") _
                                , billingitemized.Rows(y)("Consumption"), FormatNumber(billingitemized.Rows(y)("AmountDue")), FormatNumber(billcharge) _
                                , FormatNumber(billingitemized.Rows(y)("AmountDue") + billcharge), billingitemized.Rows(y)("RateSchedule"))
                            constotalforbilledwater = constotalforbilledwater + billingitemized.Rows(y)("Consumption")
                        End If
                    End If

                    If cbcommB.Checked = True Then
                        If billingitemized.Rows(y)("RateSchedule") = "Commercial-B" Then
                            dt.Rows.Add(Format(billingitemized.Rows(y)("ZoneID"), "00") & " - " & billingitemized.Rows(y)("Zone"), billingitemized.Rows(y)("bilang") _
                                , billingitemized.Rows(y)("Consumption"), FormatNumber(billingitemized.Rows(y)("AmountDue")), FormatNumber(billcharge) _
                                , FormatNumber(billingitemized.Rows(y)("AmountDue") + billcharge), billingitemized.Rows(y)("RateSchedule"))
                            constotalforbilledwater = constotalforbilledwater + billingitemized.Rows(y)("Consumption")
                        End If
                    End If

                    If cbcommC.Checked = True Then
                        If billingitemized.Rows(y)("RateSchedule") = "Commercial-C" Then
                            dt.Rows.Add(Format(billingitemized.Rows(y)("ZoneID"), "00") & " - " & billingitemized.Rows(y)("Zone"), billingitemized.Rows(y)("bilang") _
                                , billingitemized.Rows(y)("Consumption"), FormatNumber(billingitemized.Rows(y)("AmountDue")), FormatNumber(billcharge) _
                                , FormatNumber(billingitemized.Rows(y)("AmountDue") + billcharge), billingitemized.Rows(y)("RateSchedule"))
                            constotalforbilledwater = constotalforbilledwater + billingitemized.Rows(y)("Consumption")
                        End If
                    End If

                End If

            Next
        End If




        Dim Curdi As String = My.Application.Info.DirectoryPath
        Dim g As String
        g = Curdi.Replace("bin\Debug", "")


        Dim rds As New Microsoft.Reporting.WinForms.ReportDataSource
        rds.Name = "DataSet1"
        rds.Value = dt


        ReportViewer1.LocalReport.DataSources.Add(rds)
        'ReportViewer1.LocalReport.ReportPath = g & "SummaryofBilling.rdlc"
        ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\SummaryofBilling.rdlc"
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("BillMonth", ReadingDate.Text))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("pumpproduction", txtpumpproduction.Text))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("billedwater", constotalforbilledwater))
        ReportViewer1.ZoomMode = WinForms.ZoomMode.PageWidth
        ReportViewer1.RefreshReport()



        'Dim zonelist As New DataTable

        'zonelist.Clear()
        'stracs = "select distinct ZONE FROM BIlls WHERE BillingDate = '" & ReadingDate.Text & "' ORDER by Zone ASC"
        'acscmd.CommandText = stracs
        'acscmd.Connection = acsconn
        'acsda.SelectCommand = acscmd
        'acsda.Fill(zonelist)


        'If zonelist.Rows.Count = 0 Then
        '    MsgBox("No Data Found")


        'Else

        '    constotalforbilledwater = 0

        '    For y = 0 To zonelist.Rows.Count - 1


        '        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        '        Dim classlist As New DataTable

        '        classlist.Clear()
        '        stracs = "select distinct RateSchedule FROM BIlls WHERE Zone = '" & zonelist.Rows(y)("ZONE") & "' AND BillingDate = '" & ReadingDate.Text & "' ORDER by RateSchedule ASC"
        '        acscmd.CommandText = stracs
        '        acscmd.Connection = acsconn
        '        acsda.SelectCommand = acscmd
        '        acsda.Fill(classlist)

        '        If classlist.Rows.Count = 0 Then

        '        Else

        '            amountduedecimal = 0
        '            constotal = 0

        '            For t = 0 To classlist.Rows.Count - 1


        '                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        '                Dim dtcount As New DataTable

        '                dtcount.Clear()
        '                stracs = "select RateSchedule FROM BIlls WHERE RateSchedule = '" & classlist.Rows(t)("RateSchedule") & "' AND BillingDate = '" & ReadingDate.Text & "' AND Zone = '" & zonelist.Rows(y)("ZONE") & "'"
        '                acscmd.CommandText = stracs
        '                acscmd.Connection = acsconn
        '                acsda.SelectCommand = acscmd
        '                acsda.Fill(dtcount)

        '                rowcount = dtcount.Rows.Count




        '                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        '                Dim summarybill As New DataTable

        '                summarybill.Clear()
        '                stracs = "select SUM(Consumption) as ConsTotal FROM BIlls WHERE RateSchedule = '" & classlist.Rows(t)("RateSchedule") & "' AND BillingDate = '" & ReadingDate.Text & "' AND Zone = '" & zonelist.Rows(y)("ZONE") & "'"
        '                acscmd.CommandText = stracs
        '                acscmd.Connection = acsconn
        '                acsda.SelectCommand = acscmd
        '                acsda.Fill(summarybill)

        '                If IsDBNull(summarybill.Rows(0)("ConsTotal")) = True Then
        '                    constotal = 0
        '                Else
        '                    constotal = summarybill.Rows(0)("ConsTotal")
        '                End If



        '                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        '                Dim amountdue As New DataTable


        '                amountdue.Clear()
        '                stracs = "select SUM(AmountDue) as AmountDueTotal, SUM(AdvancePayment) as AdvancePayment, SUM(PenaltyAfterDue) as Penalty, SUM(Discount) as Discount FROM BIlls WHERE RateSchedule = '" & classlist.Rows(t)("RateSchedule") & "' AND BillingDate = '" & ReadingDate.Text & "' AND Zone = '" & zonelist.Rows(y)("ZONE") & "'"
        '                acscmd.CommandText = stracs
        '                acscmd.Connection = acsconn
        '                acsda.SelectCommand = acscmd
        '                acsda.Fill(amountdue)

        '                If IsDBNull(amountdue.Rows(0)("AmountDueTotal")) = True Then
        '                    amout = 0
        '                Else
        '                    amout = amountdue.Rows(0)("AmountDueTotal")
        '                End If

        '                If IsDBNull(amountdue.Rows(0)("AdvancePayment")) = True Then
        '                    advancedicimal = 0
        '                Else
        '                    advancedicimal = amountdue.Rows(0)("AdvancePayment")
        '                End If

        '                'If IsDBNull(amountdue.Rows(0)("Penalty")) = True Then
        '                '    penaltydecimal = 0
        '                'Else
        '                '    penaltydecimal = amountdue.Rows(0)("Penalty")
        '                'End If

        '                If IsDBNull(amountdue.Rows(0)("Discount")) = True Then
        '                    discountdecimal = 0
        '                Else
        '                    discountdecimal = amountdue.Rows(0)("Discount")
        '                End If

        '                amountduedecimal = (amout) - (advancedicimal + discountdecimal)



        '                If acsconn.State = ConnectionState.Closed Then acsconn.Open()


        '                sqlData1.Clear()
        '                stracs = "select BillNo FROM BIlls WHERE RateSchedule = '" & classlist.Rows(t)("RateSchedule") & "' AND BillingDate = '" & ReadingDate.Text & "' AND Zone = '" & zonelist.Rows(y)("ZONE") & "'"
        '                acscmd.CommandText = stracs
        '                acscmd.Connection = acsconn
        '                acsda.SelectCommand = acscmd
        '                acsda.Fill(sqlData1)
        '                totalchargesdecimal = 0

        '                If sqlData1.Rows.Count = 0 Then

        '                Else
        '                    For r = 0 To sqlData1.Rows.Count - 1


        '                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        '                        Dim dttotalcharges As New DataTable

        '                        dttotalcharges.Clear()
        '                        stracs = "select SUM(Amount) as TotalCharges FROM BillCharges WHERE BillNumber = '" & sqlData1.Rows(r)("BillNo") & "'"
        '                        acscmd.CommandText = stracs
        '                        acscmd.Connection = acsconn
        '                        acsda.SelectCommand = acscmd
        '                        acsda.Fill(dttotalcharges)

        '                        If IsDBNull(dttotalcharges.Rows(0)("TotalCharges")) = True Then
        '                            totalchargesdecimal = totalchargesdecimal + 0
        '                        Else
        '                            totalchargesdecimal = totalchargesdecimal + dttotalcharges.Rows(0)("TotalCharges")
        '                        End If

        '                    Next

        '                End If




        '                If cbselectall.Checked = True Then
        '                    dt.Rows.Add(zonelist.Rows(y)("ZONE"), rowcount, constotal, FormatNumber(amountduedecimal), FormatNumber(totalchargesdecimal), FormatNumber(amountduedecimal + totalchargesdecimal), classlist.Rows(t)("RateSchedule"))
        '                    constotalforbilledwater = constotalforbilledwater + constotal
        '                Else


        '                    If cbresid.Checked = True Then
        '                        If classlist.Rows(t)("RateSchedule") = "Residential" Then
        '                            dt.Rows.Add(zonelist.Rows(y)("ZONE"), rowcount, constotal, FormatNumber(amountduedecimal), FormatNumber(totalchargesdecimal), FormatNumber(amountduedecimal + totalchargesdecimal), classlist.Rows(t)("RateSchedule"))
        '                            constotalforbilledwater = constotalforbilledwater + constotal
        '                        End If
        '                    End If

        '                    If cbcommInd.Checked = True Then
        '                        If classlist.Rows(t)("RateSchedule") = "Commercial/Industrial" Then
        '                            dt.Rows.Add(zonelist.Rows(y)("ZONE"), rowcount, constotal, FormatNumber(amountduedecimal), FormatNumber(totalchargesdecimal), FormatNumber(amountduedecimal + totalchargesdecimal), classlist.Rows(t)("RateSchedule"))
        '                            constotalforbilledwater = constotalforbilledwater + constotal
        '                        End If
        '                    End If

        '                    If cbbulk.Checked = True Then
        '                        If classlist.Rows(t)("RateSchedule") = "Bulk/Wholesale" Then
        '                            dt.Rows.Add(zonelist.Rows(y)("ZONE"), rowcount, constotal, FormatNumber(amountduedecimal), FormatNumber(totalchargesdecimal), FormatNumber(amountduedecimal + totalchargesdecimal), classlist.Rows(t)("RateSchedule"))
        '                            constotalforbilledwater = constotalforbilledwater + constotal
        '                        End If
        '                    End If

        '                    If cbcommA.Checked = True Then
        '                        If classlist.Rows(t)("RateSchedule") = "Commercial-A" Then
        '                            dt.Rows.Add(zonelist.Rows(y)("ZONE"), rowcount, constotal, FormatNumber(amountduedecimal), FormatNumber(totalchargesdecimal), FormatNumber(amountduedecimal + totalchargesdecimal), classlist.Rows(t)("RateSchedule"))
        '                            constotalforbilledwater = constotalforbilledwater + constotal
        '                        End If
        '                    End If

        '                    If cbcommB.Checked = True Then
        '                        If classlist.Rows(t)("RateSchedule") = "Commercial-B" Then
        '                            dt.Rows.Add(zonelist.Rows(y)("ZONE"), rowcount, constotal, FormatNumber(amountduedecimal), FormatNumber(totalchargesdecimal), FormatNumber(amountduedecimal + totalchargesdecimal), classlist.Rows(t)("RateSchedule"))
        '                            constotalforbilledwater = constotalforbilledwater + constotal
        '                        End If
        '                    End If

        '                    If cbcommC.Checked = True Then
        '                        If classlist.Rows(t)("RateSchedule") = "Commercial-C" Then
        '                            dt.Rows.Add(zonelist.Rows(y)("ZONE"), rowcount, constotal, FormatNumber(amountduedecimal), FormatNumber(totalchargesdecimal), FormatNumber(amountduedecimal + totalchargesdecimal), classlist.Rows(t)("RateSchedule"))
        '                            constotalforbilledwater = constotalforbilledwater + constotal
        '                        End If
        '                    End If
        '                End If



        '            Next


        '        End If
        '    Next

        'End If




    End Sub

    Private Sub cbselectall_CheckedChanged(sender As Object, e As EventArgs) Handles cbselectall.CheckedChanged
        If cbselectall.Checked = True Then
            cbresid.Checked = True
            cbcommInd.Checked = True
            cbbulk.Checked = True
            cbcommA.Checked = True
            cbcommB.Checked = True
            cbcommC.Checked = True

        Else
            cbresid.Checked = False
            cbcommInd.Checked = False
            cbbulk.Checked = False
            cbcommA.Checked = False
            cbcommB.Checked = False
            cbcommC.Checked = False
        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtpumpproduction.KeyPress
        If e.KeyChar <> ControlChars.Back Then
            e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = ".")
        End If

    End Sub

    Private Sub cbtype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbtype.SelectedIndexChanged
        If cbtype.SelectedIndex = 1 Then
            cbselectall.Checked = True
            GroupBox1.Enabled = False

        Else

            GroupBox1.Enabled = True
            cbselectall.Checked = False
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If cbselectall.Checked = True Then
            cbresid.Enabled = False
            cbcommInd.Enabled = False
            cbbulk.Enabled = False
            cbcommA.Enabled = False
            cbcommB.Enabled = False
            cbcommC.Enabled = False

        Else
            cbresid.Enabled = True
            cbcommInd.Enabled = True
            cbbulk.Enabled = True
            cbcommA.Enabled = True
            cbcommB.Enabled = True
            cbcommC.Enabled = True
        End If
    End Sub

    Private Sub BillingSummary_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Me.Activate()
    End Sub

    Public MoveFormSum As Boolean
    Public MoveFormSum_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormSum = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormSum_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormSum Then
            Me.Location = Me.Location + (e.Location - MoveFormSum_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormSum = False
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
        Panel1.Click, ReadingDate.Click, cbtype.Click, txtpumpproduction.Click, GroupBox1.Click,
        cbselectall.Click, cbresid.Click, cbcommA.Click, cbcommB.Click, cbcommC.Click,
        cbcommInd.Click, cbbulk.Click, billSearch.Click, ReportViewer1.Click ' etc.
        Me.Activate() 'Or Whatever
    End Sub

End Class