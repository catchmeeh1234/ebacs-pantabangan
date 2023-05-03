Imports Microsoft.Reporting

Public Class LedgerReport
    Private Sub LedgerReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain
        prog.Visible = False
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.ZoomMode.PageWidth)
        Me.ReportViewer1.RefreshReport()
    End Sub

    Private Sub billSearch_Click(sender As Object, e As EventArgs) Handles billSearch.Click

        If txtaccountno.Text = "" Then

            MsgBox("Please enter Account Number")
        Else

            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.Reset()

        End If
        loadledger()
    End Sub

    Sub loadledger()
        Cursor = Cursors.WaitCursor
        prog.Value = 0
        prog.Visible = True
        billSearch.Enabled = False

        Dim dt As New DataTable

        With dt

            .Columns.Add("datee")
            .Columns.Add("ref")
            .Columns.Add("particular")
            .Columns.Add("reading")
            .Columns.Add("cons")
            .Columns.Add("billing")
            .Columns.Add("credit")
            .Columns.Add("bal")

        End With

        Dim consinfo As New DataTable

        consinfo.Clear()

        stracs = "select Lastname,Firstname,ServiceAddress,Middlename,CompanyName from Customers where AccountNo = '" & txtaccountno.Text & "'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(consinfo)


        Dim fullname, address As String

        If consinfo.Rows.Count = 0 Then

            fullname = ""
            address = ""
        Else
            If consinfo.Rows(0)("CompanyName") = "" Then

                fullname = consinfo.Rows(0)("Firstname") & " " & consinfo.Rows(0)("Middlename") & " " & consinfo.Rows(0)("Lastname")
                address = consinfo.Rows(0)("ServiceAddress")

            Else
                fullname = consinfo.Rows(0)("CompanyName")
                address = consinfo.Rows(0)("ServiceAddress")
            End If

        End If

        Dim ledgerdata As New DataTable

        ledgerdata.Clear()

        If acsconn.State = ConnectionState.Closed Then acsconn.Open()


        If cball.Checked = True Then
            stracs = "Select * from AccountLedger where ledgerAccountNo = '" & txtaccountno.Text & "' Order by ledgerID asc"
        Else
            stracs = "select * from AccountLedger where ledgerAccountNo = '" & txtaccountno.Text & "' AND CAST(ledgerDate as DATE) BETWEEN '" & dtpfrom.Text & "' AND '" & dtpto.Text & "' Order by ledgerID asc"
        End If

        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(ledgerdata)

        If ledgerdata.Rows.Count = 0 Then
            MsgBox("No data found")
        Else
            For b = 0 To ledgerdata.Rows.Count - 1

                Dim amount, disc, bal As String

                If ledgerdata.Rows(b)("ledgerAmount") = "" Then
                    amount = ledgerdata.Rows(b)("ledgerAmount")

                Else
                    amount = FormatNumber(ledgerdata.Rows(b)("ledgerAmount"))
                End If

                If ledgerdata.Rows(b)("ledgerDiscount") = "" Then
                    disc = ledgerdata.Rows(b)("ledgerDiscount")

                Else
                    disc = FormatNumber(ledgerdata.Rows(b)("ledgerDiscount"))
                End If

                If ledgerdata.Rows(b)("ledgerBalance") = "" Then
                    bal = ledgerdata.Rows(b)("ledgerBalance")
                Else
                    bal = FormatNumber(ledgerdata.Rows(b)("ledgerBalance"))
                End If


                dt.Rows.Add(ledgerdata.Rows(b)("ledgerDate"), ledgerdata.Rows(b)("ledgerRefNo"), ledgerdata.Rows(b)("ledgerParticulars") _
                            , ledgerdata.Rows(b)("ledgerReading"), ledgerdata.Rows(b)("ledgerConsumption"), amount _
                            , disc, bal)
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
        'ReportViewer1.LocalReport.ReportPath = g & "Ledger.rdlc"
        ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Ledger.rdlc"
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("accno", txtaccountno.Text))


        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("accname", fullname))
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("accadd", address))


        If cball.Checked = True Then
            ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("dateee", "All"))

        Else
            ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("dateee", "From: " & dtpfrom.Text & " To: " & dtpto.Text))

        End If


        ReportViewer1.ZoomMode = WinForms.ZoomMode.PageWidth
        ReportViewer1.RefreshReport()





        billSearch.Enabled = True
        prog.Visible = False
        Cursor = Cursors.Default


    End Sub
End Class