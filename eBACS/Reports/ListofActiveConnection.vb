Imports Microsoft.Reporting

Public Class ListofActiveConnection
    Private Sub ListofActiveConnection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.ZoomMode.PageWidth)
        Me.ReportViewer1.RefreshReport()

        Dim zoness As New DataTable
        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        zoness.Clear()

        stracs = "select ZoneName from Zone"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(zoness)

        cbzone.Items.Clear()
        cbzone.Items.Add("All")
        If zoness.Rows.Count = 0 Then
        Else

            For x = 0 To zoness.Rows.Count - 1

                cbzone.Items.Add(zoness(x)("ZoneName"))

            Next

        End If

        cbzone.SelectedIndex = 0

    End Sub

    Private Sub billSearch_Click(sender As Object, e As EventArgs) Handles billSearch.Click


        Cursor = Cursors.WaitCursor
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.Reset()

        If Label7.Text = "List of Active Connections" Then
            loaddata()
        Else
            loaddisconnected()
        End If



        Cursor = Cursors.Default
    End Sub

    Sub loaddata()

        Dim dt As New DataTable

        With dt
            .Columns.Add("zone")
            .Columns.Add("accno")
            .Columns.Add("accname")
            .Columns.Add("address")
            .Columns.Add("instdate")
            .Columns.Add("meterno")


        End With


        If cbzone.SelectedIndex = 0 Then
            Dim activelist As New DataTable

            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            activelist.Clear()
            stracs = "select AccountNo,Lastname,Firstname,Middlename,ServiceAddress,Zone,MeterNo,DateInstalled from Customers WHERE CustomerStatus = 'Active' ORDER by Zone ASC, AccountNo ASC"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(activelist)


            If activelist.Rows.Count = 0 Then
                MsgBox("No data Found")
            Else

                For p = 0 To activelist.Rows.Count - 1

                    dt.Rows.Add(activelist.Rows(p)("Zone"), activelist.Rows(p)("AccountNo"), activelist.Rows(p)("Firstname") _
                                & " " & activelist.Rows(p)("Middlename") & " " & activelist.Rows(p)("Lastname") _
                                , activelist.Rows(p)("ServiceAddress"), activelist.Rows(p)("DateInstalled"), activelist.Rows(p)("MeterNo"))
                Next


            End If

        Else

            Dim activelist As New DataTable

            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            activelist.Clear()
            stracs = "select AccountNo,Lastname,Firstname,Middlename,ServiceAddress,Zone,MeterNo,DateInstalled from Customers WHERE CustomerStatus = 'Active' AND Zone = '" & cbzone.Text & "' ORDER by Zone ASC, AccountNo ASC"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(activelist)


            If activelist.Rows.Count = 0 Then
                MsgBox("No data Found")
            Else

                For p = 0 To activelist.Rows.Count - 1

                    dt.Rows.Add(activelist.Rows(p)("Zone"), activelist.Rows(p)("AccountNo"), activelist.Rows(p)("Firstname") _
                                & " " & activelist.Rows(p)("Middlename") & " " & activelist.Rows(p)("Lastname") _
                                , activelist.Rows(p)("ServiceAddress"), activelist.Rows(p)("DateInstalled"), activelist.Rows(p)("MeterNo"))
                Next


            End If

        End If




        Dim Curdi As String = My.Application.Info.DirectoryPath
        Dim g As String
        g = Curdi.Replace("bin\Debug", "")


        Dim rds As New Microsoft.Reporting.WinForms.ReportDataSource
        rds.Name = "DataSet1"
        rds.Value = dt


        ReportViewer1.LocalReport.DataSources.Add(rds)
        'ReportViewer1.LocalReport.ReportPath = g & "ListofActiveConnection.rdlc"
        ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\ListofActiveConnection.rdlc"
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("zone", cbzone.Text.ToUpper & " Zone"))
        ReportViewer1.ZoomMode = WinForms.ZoomMode.PageWidth
        ReportViewer1.RefreshReport()


    End Sub

    Sub loaddisconnected()
        Dim dt As New DataTable

        With dt
            .Columns.Add("zone")
            .Columns.Add("accno")
            .Columns.Add("accname")
            .Columns.Add("dateofdisconnection")
            .Columns.Add("lastbillconsump")
            .Columns.Add("lasttotalamountdue")
            .Columns.Add("balance")


        End With


        Dim disconnected As New DataTable
        Dim balancetotal As Double

        If cbzone.SelectedIndex = 0 Then

            balancetotal = 0

            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            disconnected.Clear()
            stracs = "select AccountNo,Lastname,Firstname,Middlename,DateLastDisconnected,Zone from Customers WHERE CustomerStatus = 'Disconnected'  ORDER by Zone ASC, AccountNo ASC"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(disconnected)


            If disconnected.Rows.Count = 0 Then
                MsgBox("No data Found")
            Else

                For p = 0 To disconnected.Rows.Count - 1

                    Dim lastconsumpt As Integer
                    Dim billbal, chargebal, lastamountdue As Decimal

                    lastconsumpt = 0
                    billbal = 0
                    chargebal = 0
                    lastamountdue = 0

                    Dim billdata As New DataTable
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    billdata.Clear()
                    stracs = "select Consumption, AmountDue, BillNo FROM Bills WHERE AccountNumber = '" & disconnected.Rows(p)("AccountNo") & "' AND IsPaid = 'No'  ORDER by BillNo DESC"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsda.SelectCommand = acscmd
                    acsda.Fill(billdata)


                    If billdata.Rows.Count = 0 Then

                        lastconsumpt = 0
                        lastamountdue = 0
                    Else
                        lastconsumpt = billdata.Rows(0)("Consumption")
                        lastamountdue = billdata.Rows(0)("AmountDue")
                        For u = 0 To billdata.Rows.Count - 1
                            Dim chargebalance As New DataTable
                            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                            chargebalance.Clear()
                            stracs = "select SUM(Amount) as chargeamount FROM BillCharges WHERE BillNumber = '" & billdata.Rows(u)("BillNo") & "'"
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acsda.SelectCommand = acscmd
                            acsda.Fill(chargebalance)


                            If IsDBNull(chargebalance.Rows(0)("chargeamount")) = True Then
                                chargebal = chargebal + 0

                            Else
                                chargebal = chargebal + chargebalance.Rows(0)("chargeamount")
                            End If


                        Next

                    End If

                    Dim billbalance As New DataTable
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    billbalance.Clear()
                    stracs = "select SUM(AmountDue) AS Amountdue FROM Bills WHERE AccountNumber = '" & disconnected.Rows(p)("AccountNo") & "' AND IsPaid = 'No'"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsda.SelectCommand = acscmd
                    acsda.Fill(billbalance)


                    If IsDBNull(billbalance.Rows(0)("Amountdue")) = True Then
                        billbal = 0

                    Else
                        billbal = billbalance.Rows(0)("Amountdue")
                    End If



                    dt.Rows.Add(disconnected.Rows(p)("Zone"), disconnected.Rows(p)("AccountNo"), disconnected.Rows(p)("Firstname") _
                                & " " & disconnected.Rows(p)("Middlename") & " " & disconnected.Rows(p)("Lastname") _
                                , disconnected.Rows(p)("DateLastDisconnected"), lastconsumpt, FormatNumber(lastamountdue), FormatNumber(billbal + chargebal))

                    balancetotal = balancetotal + (billbal + chargebal)


                Next


            End If

        Else

            balancetotal = 0

            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            disconnected.Clear()
            stracs = "select AccountNo,Lastname,Firstname,Middlename,DateLastDisconnected,Zone from Customers WHERE CustomerStatus = 'Disconnected' AND Zone = '" & cbzone.Text & "'  ORDER by Zone ASC, AccountNo ASC"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(disconnected)


            If disconnected.Rows.Count = 0 Then
                MsgBox("No data Found")
            Else

                For p = 0 To disconnected.Rows.Count - 1

                    Dim lastconsumpt As Integer
                    Dim billbal, chargebal, lastamountdue As Decimal

                    lastconsumpt = 0
                    billbal = 0
                    chargebal = 0
                    lastamountdue = 0

                    Dim billdata As New DataTable
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    billdata.Clear()
                    stracs = "select Consumption, AmountDue, BillNo FROM Bills WHERE AccountNumber = '" & disconnected.Rows(p)("AccountNo") & "' AND IsPaid = 'No'  ORDER by BillNo DESC"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsda.SelectCommand = acscmd
                    acsda.Fill(billdata)


                    If billdata.Rows.Count = 0 Then

                        lastconsumpt = 0
                        lastamountdue = 0
                    Else
                        lastconsumpt = billdata.Rows(0)("Consumption")
                        lastamountdue = billdata.Rows(0)("AmountDue")
                        For u = 0 To billdata.Rows.Count - 1
                            Dim chargebalance As New DataTable
                            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                            chargebalance.Clear()
                            stracs = "select SUM(Amount) as chargeamount FROM BillCharges WHERE BillNumber = '" & billdata.Rows(u)("BillNo") & "'"
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acsda.SelectCommand = acscmd
                            acsda.Fill(chargebalance)


                            If IsDBNull(chargebalance.Rows(0)("chargeamount")) = True Then
                                chargebal = chargebal + 0

                            Else
                                chargebal = chargebal + chargebalance.Rows(0)("chargeamount")
                            End If


                        Next

                    End If

                    Dim billbalance As New DataTable
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    billbalance.Clear()
                    stracs = "select SUM(AmountDue) AS Amountdue FROM Bills WHERE AccountNumber = '" & disconnected.Rows(p)("AccountNo") & "' AND IsPaid = 'No'"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsda.SelectCommand = acscmd
                    acsda.Fill(billbalance)


                    If IsDBNull(billbalance.Rows(0)("Amountdue")) = True Then
                        billbal = 0

                    Else
                        billbal = billbalance.Rows(0)("Amountdue")
                    End If


                    dt.Rows.Add(disconnected.Rows(p)("Zone"), disconnected.Rows(p)("AccountNo"), disconnected.Rows(p)("Firstname") _
                                & " " & disconnected.Rows(p)("Middlename") & " " & disconnected.Rows(p)("Lastname") _
                                , disconnected.Rows(p)("DateLastDisconnected"), lastconsumpt, FormatNumber(lastamountdue), FormatNumber(billbal + chargebal))

                    balancetotal = balancetotal + (billbal + chargebal)


                Next


            End If

        End If



        Dim Curdi As String = My.Application.Info.DirectoryPath
        Dim g As String
        g = Curdi.Replace("bin\Debug", "")


        Dim rds As New Microsoft.Reporting.WinForms.ReportDataSource
        rds.Name = "DataSet1"
        rds.Value = dt


        ReportViewer1.LocalReport.DataSources.Add(rds)
        'ReportViewer1.LocalReport.ReportPath = g & "ListofDisconnectedAccounts.rdlc"
        ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\ListofDisconnectedAccounts.rdlc"
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("asof", "Date Printed " & Date.Now.ToString("dddd") & ", " & Date.Now.ToString("MMMM dd, yyyy")))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("rowtotal", disconnected.Rows.Count))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("balancetotal", FormatNumber(balancetotal)))
        ReportViewer1.ZoomMode = WinForms.ZoomMode.PageWidth
        ReportViewer1.RefreshReport()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) 
        Me.Close()
    End Sub



    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Me.Activate()
    End Sub

    Public MoveFormActive As Boolean
    Public MoveFormActive_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormActive = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormActive_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormActive Then
            Me.Location = Me.Location + (e.Location - MoveFormActive_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormActive = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub ListofActiveConnection_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub postbill_deactivated(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.BackColor = Color.FromArgb(17, 153, 195)
    End Sub

    Private Sub postbill_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.BackColor = Color.SteelBlue
    End Sub

    Private Sub ControlsClick(sender As Object, e As EventArgs) Handles _
        Panel1.Click, cbzone.Click, billSearch.Click, ReportViewer1.Click ' etc.
        Me.Activate() 'Or Whatever
    End Sub
End Class