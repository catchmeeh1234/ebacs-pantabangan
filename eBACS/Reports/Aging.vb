Imports Microsoft.Reporting

Public Class Aging
    Private Sub Aging_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain
        prog.Visible = False
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.ZoomMode.PageWidth)
        Me.ReportViewer1.RefreshReport()

        ' cbstatus.SelectedIndex = 0

        'Dim zoness As New DataTable
        'If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        'zoness.Clear()

        'stracs = "select ZoneName from Zone"
        'acscmd.CommandText = stracs
        'acscmd.Connection = acsconn
        'acsda.SelectCommand = acscmd
        'acsda.Fill(zoness)

        'cbzone.Items.Clear()
        'cbzone.Items.Add("All")
        'If zoness.Rows.Count = 0 Then
        'Else

        '    For x = 0 To zoness.Rows.Count - 1

        '        cbzone.Items.Add(zoness(x)("ZoneName"))

        '    Next

        'End If

        'cbzone.SelectedIndex = 0
    End Sub

    Private Sub billSearch_Click(sender As Object, e As EventArgs) Handles billSearch.Click
        Cursor = Cursors.WaitCursor

        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.Reset()
        If Label7.Text = "Aging Report (Detailed)" Then
            loadagingdetailed()
        Else
            loadagingsummary()
        End If

        Cursor = Cursors.Default
    End Sub

    Sub loadagingsummary()
        Cursor = Cursors.WaitCursor
        prog.Value = 0
        prog.Visible = True
        billSearch.Enabled = False

        Dim dt As New DataTable

        With dt
            .Columns.Add("zone")
            .Columns.Add("onetosixtydys")
            .Columns.Add("sixtyonetooneeightydys")
            .Columns.Add("eightyoneyr")
            .Columns.Add("oneyrtotwoyr")
            .Columns.Add("twoyrtothreeyr")
            .Columns.Add("threeyrtofouryr")
            .Columns.Add("fouryrup")
            .Columns.Add("no")

        End With

        Dim count As Integer

        Dim gtotal As Decimal
        gtotal = 0

        count = 0


        Dim one, two, three, four, five, six, seven As Decimal
        one = 0
        two = 0
        three = 0
        four = 0
        five = 0
        six = 0
        seven = 0

        Dim zone As New DataTable
        zone.Clear()

        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        stracs = "select DISTINCT Zone, ZoneID FROM Bills,Zone WHERE Bills.Cancelled = 'No' AND Zone.ZoneName = Bills.Zone ORDER by ZoneID ASC"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(zone)



        If zone.Rows.Count = 0 Then

            MsgBox("No data found")

        Else

            For t = 0 To zone.Rows.Count - 1

                count = 0

                Dim onetotal, twototal, threetotal, fourtotal, fivetotal, sixtotal, seventotal As Decimal
                onetotal = 0
                twototal = 0
                threetotal = 0
                fourtotal = 0
                fivetotal = 0
                sixtotal = 0
                seventotal = 0

                Dim bills As New DataTable
                bills.Clear()

                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                stracs = "select BillNo FROM Bills WHERE BillStatus = 'Posted' and Cancelled = 'No' AND isPromisorry = 'No'  AND Zone = '" & zone.Rows(t)("Zone") & "' AND (datepaid is null OR DatePaid > '" & dtpasof.Text & "') and ReadingDate <= '" & dtpasof.Text & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(bills)

                If bills.Rows.Count = 0 Then

                Else
                    For j = 0 To bills.Rows.Count - 1

                        Dim totalcharges As Decimal

                        totalcharges = 0

                        Dim billamountdt As New DataTable
                        billamountdt.Clear()

                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "select CustomerName,ReadingDate,AmountDue,PenaltyAfterDue,Discount,AdvancePayment,Adjustment FROM Bills WHERE BillNo = '" & bills.Rows(j)("BillNo") & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acsda.SelectCommand = acscmd
                        acsda.Fill(billamountdt)



                        If billamountdt.Rows.Count = 0 Then

                        Else

                            count = count + 1

                            Dim balance As Decimal

                            balance = totalcharges + Decimal.Parse(billamountdt.Rows(0)("Adjustment")) + Decimal.Parse(billamountdt.Rows(0)("AmountDue")) - Decimal.Parse(billamountdt.Rows(0)("Discount")) - Decimal.Parse(billamountdt.Rows(0)("AdvancePayment"))

                            Dim due As Date

                            due = billamountdt.Rows(0)("ReadingDate")

                            Dim datediff As Integer = (Date.Now - due).TotalDays

                            If datediff <= 60 Then
                                one = one + balance
                                onetotal = onetotal + balance
                            ElseIf datediff <= 180 Then
                                two = two + balance
                                twototal = twototal + balance
                            ElseIf datediff <= 365 Then
                                three = three + balance
                                threetotal = threetotal + balance
                            ElseIf datediff <= 730 Then
                                four = four + balance
                                fourtotal = fourtotal + balance
                            ElseIf datediff <= 1095 Then
                                five = five + balance
                                fivetotal = fivetotal + balance
                            ElseIf datediff <= 1460 Then
                                six = six + balance
                                sixtotal = sixtotal + balance
                            Else
                                seven = seven + balance
                                seventotal = seventotal + balance
                            End If
                            gtotal = gtotal + balance

                        End If

                    Next

                End If


                'PN
                Dim pn As New DataTable
                pn.Clear()

                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                stracs = "select a.AccountNumber, a.DateCreated,a.AccountName,a.RefNo,a.Billing,a.Penalty
                            FROM AddAdjustment a join Customers b on b.AccountNo = a.AccountNumber WHERE a.Status = 'Posted' and b.Zone = '" & zone.Rows(t)("Zone") & "' AND (a.DatePaid is null or a.DatePaid > '" & dtpasof.Text & "') and a.DateRead <= '" & dtpasof.Text & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(pn)



                If pn.Rows.Count = 0 Then
                Else

                    For pnn = 0 To pn.Rows.Count - 1
                        If pn.Rows(pnn)("Billing") = "0.00" Then

                        Else
                            count = count + 1

                            Dim balance As Decimal

                            balance = Decimal.Parse(pn.Rows(pnn)("Billing"))

                            Dim due As Date

                            due = pn.Rows(pnn)("DateCreated")

                            Dim datediff As Integer = (Date.Now - due).TotalDays
                            If datediff <= 60 Then
                                one = one + balance
                                onetotal = onetotal + balance
                            ElseIf datediff <= 180 Then
                                two = two + balance
                                twototal = twototal + balance
                            ElseIf datediff <= 365 Then
                                three = three + balance
                                threetotal = threetotal + balance
                            ElseIf datediff <= 730 Then
                                four = four + balance
                                fourtotal = fourtotal + balance
                            ElseIf datediff <= 1095 Then
                                five = five + balance
                                fivetotal = fivetotal + balance
                            ElseIf datediff <= 1460 Then
                                six = six + balance
                                sixtotal = sixtotal + balance
                            Else
                                seven = seven + balance
                                seventotal = seventotal + balance
                            End If
                            gtotal = gtotal + balance

                        End If
                    Next


                End If


                    dt.Rows.Add(Format(zone.Rows(t)("ZoneID"), "00") & " - " & zone.Rows(t)("Zone"), FormatNumber(onetotal), FormatNumber(twototal), FormatNumber(threetotal) _
                            , FormatNumber(fourtotal), FormatNumber(fivetotal), FormatNumber(sixtotal), FormatNumber(seventotal), count)

                prog.Value = t / zone.Rows.Count * 100
            Next

        End If


        prog.Value = 100

        Dim Curdi As String = My.Application.Info.DirectoryPath
        Dim g As String
        g = Curdi.Replace("bin\Debug", "")


        Dim rds As New Microsoft.Reporting.WinForms.ReportDataSource
        rds.Name = "DataSet1"
        rds.Value = dt


        ReportViewer1.LocalReport.DataSources.Add(rds)
        'ReportViewer1.LocalReport.ReportPath = g & "AgingSummary.rdlc"
        ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\AgingSummary.rdlc"
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("asof", "As of " & dtpasof.Value.ToString("dddd") & ", " & dtpasof.Value.ToString("MMMM dd, yyyy")))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("gtotal", FormatNumber(gtotal)))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("one", FormatNumber(one)))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("two", FormatNumber(two)))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("three", FormatNumber(three)))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("four", FormatNumber(four)))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("five", FormatNumber(five)))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("six", FormatNumber(six)))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("seven", FormatNumber(seven)))

        ReportViewer1.ZoomMode = WinForms.ZoomMode.PageWidth
        ReportViewer1.RefreshReport()

        billSearch.Enabled = True
        prog.Visible = False
        Cursor = Cursors.Default

    End Sub

    Sub loadagingdetailed()
        Cursor = Cursors.WaitCursor
        prog.Value = 0
        prog.Visible = True
        billSearch.Enabled = False

        Dim dt As New DataTable

        With dt
            .Columns.Add("zone")
            .Columns.Add("accno")
            .Columns.Add("accname")
            .Columns.Add("onetosixtydys")
            .Columns.Add("sixtyonetooneeightydys")
            .Columns.Add("eightyoneyr")
            .Columns.Add("oneyrtotwoyr")
            .Columns.Add("twoyrtothreeyr")
            .Columns.Add("threeyrtofouryr")
            .Columns.Add("fouryrup")
            .Columns.Add("billno")

        End With

        Dim count As Integer
        'Dim sqlstring As String
        Dim gtotal As Decimal
        gtotal = 0

        count = 0

        Dim one, two, three, four, five, six, seven As Decimal
        one = 0
        two = 0
        three = 0
        four = 0
        five = 0
        six = 0
        seven = 0

        Dim bills As New DataTable
        bills.Clear()

        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        stracs = "select Bills.AccountNumber, Bills.ReadingDate,Bills.CustomerName,Bills.BillNo,Bills.AmountDue,Bills.PenaltyAfterDue,Bills.Discount,Bills.Zone,Bills.AdvancePayment,Bills.Adjustment,Zone.ZoneID
                    FROM Bills,Zone WHERE Cancelled = 'No' AND isPromisorry = 'No' AND (datepaid is null OR DatePaid > '" & dtpasof.Text & "') and Bills.BillStatus = 'Posted' and Bills.ReadingDate <= '" & dtpasof.Text & "' AND Zone.ZoneName = Bills.Zone ORDER by ZoneID ASC"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(bills)

        If bills.Rows.Count = 0 Then
            ' MsgBox("No data")
        Else

            For p = 0 To bills.Rows.Count - 1

                Dim totalcharges As Decimal

                totalcharges = 0


                Dim onetosixtydys, sixtyonetooneeightydys, eightyoneyr, oneyrtotwoyr, twoyrtothreeyr, threeyrtofouryr, fouryrup As Decimal
                onetosixtydys = 0
                sixtyonetooneeightydys = 0
                eightyoneyr = 0
                oneyrtotwoyr = 0
                twoyrtothreeyr = 0
                threeyrtofouryr = 0
                fouryrup = 0

                Dim balance As Decimal

                balance = totalcharges + Decimal.Parse(bills.Rows(p)("Adjustment")) + Decimal.Parse(bills.Rows(p)("AmountDue")) - Decimal.Parse(bills.Rows(p)("Discount")) - Decimal.Parse(bills.Rows(p)("AdvancePayment"))

                Dim due As Date

                due = bills.Rows(p)("ReadingDate")

                Dim datediff As Integer = (Date.Now - due).TotalDays

                If datediff <= 60 Then
                    onetosixtydys = FormatNumber(onetosixtydys + balance)
                    one = one + balance
                ElseIf datediff <= 180 Then
                    sixtyonetooneeightydys = FormatNumber(sixtyonetooneeightydys + balance)
                    two = two + balance
                ElseIf datediff <= 365 Then
                    eightyoneyr = FormatNumber(eightyoneyr + balance)
                    three = three + balance
                ElseIf datediff <= 730 Then
                    oneyrtotwoyr = FormatNumber(oneyrtotwoyr + balance)
                    four = four + balance
                ElseIf datediff <= 1095 Then
                    twoyrtothreeyr = FormatNumber(twoyrtothreeyr + balance)
                    five = five + balance
                ElseIf datediff <= 1460 Then
                    threeyrtofouryr = FormatNumber(threeyrtofouryr + balance)
                    six = six + balance
                Else
                    fouryrup = FormatNumber(fouryrup + balance)
                    seven = seven + balance
                End If

                gtotal = gtotal + balance
                count = count + 1
                dt.Rows.Add(Format(bills.Rows(p)("ZoneID"), "00") & " - " & bills.Rows(p)("Zone"), bills.Rows(p)("AccountNumber"), bills.Rows(p)("CustomerName"), FormatNumber(onetosixtydys) _
                                , FormatNumber(sixtyonetooneeightydys), FormatNumber(eightyoneyr) _
                                , FormatNumber(oneyrtotwoyr), FormatNumber(twoyrtothreeyr) _
                                , FormatNumber(threeyrtofouryr), FormatNumber(fouryrup), bills.Rows(p)("Billno"))
                prog.Value = p / bills.Rows.Count * 100

            Next


        End If


        Dim pn As New DataTable
        pn.Clear()

        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        stracs = "select AddAdjustment.AccountNumber, AddAdjustment.DateCreated,AddAdjustment.AccountName,AddAdjustment.RefNo,AddAdjustment.Billing,AddAdjustment.Penalty,Customers.[Zone],Zone.ZoneID
                    FROM AddAdjustment,[dbo].[Zone],Customers WHERE AddAdjustment.Status = 'Posted' and (AddAdjustment.DatePaid is null or AddAdjustment.DatePaid > '" & dtpasof.Text & "') and AddAdjustment.DateRead <= '" & dtpasof.Text & "' AND AddAdjustment.AccountNumber = Customers.AccountNo and  Zone.ZoneName = Customers.Zone ORDER by Zone.ZoneID ASC"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(pn)

        If pn.Rows.Count = 0 Then

        Else

            For ken = 0 To pn.Rows.Count - 1

                Dim onetosixtydys, sixtyonetooneeightydys, eightyoneyr, oneyrtotwoyr, twoyrtothreeyr, threeyrtofouryr, fouryrup As Decimal
                onetosixtydys = 0
                sixtyonetooneeightydys = 0
                eightyoneyr = 0
                oneyrtotwoyr = 0
                twoyrtothreeyr = 0
                threeyrtofouryr = 0
                fouryrup = 0

                Dim balance As Decimal

                balance = Decimal.Parse(pn.Rows(ken)("Billing"))

                Dim due As Date

                due = pn.Rows(ken)("DateCreated")

                Dim datediff As Integer = (Date.Now - due).TotalDays

                If datediff <= 60 Then
                    onetosixtydys = FormatNumber(onetosixtydys + balance)
                    one = one + balance
                ElseIf datediff <= 180 Then
                    sixtyonetooneeightydys = FormatNumber(sixtyonetooneeightydys + balance)
                    two = two + balance
                ElseIf datediff <= 365 Then
                    eightyoneyr = FormatNumber(eightyoneyr + balance)
                    three = three + balance
                ElseIf datediff <= 730 Then
                    oneyrtotwoyr = FormatNumber(oneyrtotwoyr + balance)
                    four = four + balance
                ElseIf datediff <= 1095 Then
                    twoyrtothreeyr = FormatNumber(twoyrtothreeyr + balance)
                    five = five + balance
                ElseIf datediff <= 1460 Then
                    threeyrtofouryr = FormatNumber(threeyrtofouryr + balance)
                    six = six + balance
                Else
                    fouryrup = FormatNumber(fouryrup + balance)
                    seven = seven + balance
                End If

                gtotal = gtotal + balance
                count = count + 1
                dt.Rows.Add(Format(pn.Rows(ken)("ZoneID"), "00") & " - " & pn.Rows(ken)("Zone"), pn.Rows(ken)("AccountNumber"), pn.Rows(ken)("AccountName"), FormatNumber(onetosixtydys) _
                                , FormatNumber(sixtyonetooneeightydys), FormatNumber(eightyoneyr) _
                                , FormatNumber(oneyrtotwoyr), FormatNumber(twoyrtothreeyr) _
                                , FormatNumber(threeyrtofouryr), FormatNumber(fouryrup), pn.Rows(ken)("RefNo"))
                prog.Value = ken / pn.Rows.Count * 100

            Next
        End If





        prog.Value = 100

        Dim Curdi As String = My.Application.Info.DirectoryPath
        Dim g As String
        g = Curdi.Replace("bin\Debug", "")

        Dim rds As New Microsoft.Reporting.WinForms.ReportDataSource
        rds.Name = "DataSet1"
        rds.Value = dt

        ReportViewer1.LocalReport.DataSources.Add(rds)
        'ReportViewer1.LocalReport.ReportPath = g & "AgingDetailed.rdlc"
        ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\AgingDetailed.rdlc"
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("asof", "As of " & dtpasof.Value.ToString("dddd") & ", " & dtpasof.Value.ToString("MMMM dd, yyyy")))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("gtotal", FormatNumber(gtotal)))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("concestotal", count))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("one", FormatNumber(one)))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("two", FormatNumber(two)))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("three", FormatNumber(three)))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("four", FormatNumber(four)))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("five", FormatNumber(five)))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("six", FormatNumber(six)))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("seven", FormatNumber(seven)))

        ReportViewer1.ZoomMode = WinForms.ZoomMode.PageWidth
        ReportViewer1.RefreshReport()

        billSearch.Enabled = True
        prog.Visible = False
        Cursor = Cursors.Default

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) 
        Me.Close()
    End Sub

    Public MoveFormAging As Boolean
    Public MoveFormAging_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormAging = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormAging_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormAging Then
            Me.Location = Me.Location + (e.Location - MoveFormAging_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormAging = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub Aging_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Me.Activate()
    End Sub

    Private Sub postbill_deactivated(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.BackColor = Color.FromArgb(17, 153, 195)
    End Sub

    Private Sub postbill_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.BackColor = Color.SteelBlue
    End Sub

    Private Sub ControlsClick(sender As Object, e As EventArgs) Handles _
        Panel1.Click, dtpasof.Click, billSearch.Click, ReportViewer1.Click  ' etc.
        Me.Activate() 'Or Whatever
    End Sub
End Class