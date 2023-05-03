Imports Microsoft.Reporting

Public Class BillingList
    Private Sub BillingList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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

        If cbzone.SelectedIndex = 0 Then
            billinglist()
        Else
            billinglistspesificzone()
        End If




        Cursor = Cursors.Default
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub


    Sub billinglist()

        Dim totalamountdue As Decimal

        Dim dt As New DataTable

        With dt
            .Columns.Add("zone")
            .Columns.Add("billno")
            .Columns.Add("accno")
            .Columns.Add("consess")
            .Columns.Add("presentrdng")
            .Columns.Add("prvsrdng")
            .Columns.Add("cumused")
            .Columns.Add("amount")
            .Columns.Add("totalcharges")
            .Columns.Add("totalamount")


        End With



        If acsconn.State = ConnectionState.Closed Then acsconn.Open()


        sqlData1.Clear()
        stracs = "SELECT a.[Zone],
	            c.ZoneID,
	            a.BillNo,
	            a.AccountNumber,
	            a.CustomerName,
	            a.Reading,
	            a.PreviousReading,
	            a.Consumption,
	            a.AmountDue,
	            totalcharges=(select sum(Amount) from billcharges b where AccountNumber = a.AccountNumber and b.BillingMonth = '" & ReadingDate.Text & "' AND b.Cancelled = 'No' and b.Status = 'Posted' and b.Cancelled = 'No')
              FROM [eBACS].[dbo].[Bills] a join [dbo].[Zone] c on c.ZoneName = a.[Zone] where a.BillingDate = '" & ReadingDate.Text & "' and a.Cancelled = 'No' and a.BillStatus = 'Posted'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(sqlData1)

        If sqlData1.Rows.Count = 0 Then
            MsgBox("No Data Found")


        Else

            For o = 0 To sqlData1.Rows.Count - 1

                Dim totalcharges, totalamount As Decimal
                totalcharges = 0
                totalamount = 0


                If IsDBNull(sqlData1.Rows(o)("totalcharges")) = True Then
                    totalcharges = 0
                Else
                    totalcharges = sqlData1.Rows(o)("totalcharges")
                End If

                totalamountdue = Double.Parse(sqlData1.Rows(o)("AmountDue"))

                totalamount = totalcharges + Decimal.Parse(totalamountdue)


                dt.Rows.Add(Format(sqlData1.Rows(o)("ZoneID"), "00") & " - " & sqlData1.Rows(o)("Zone"), sqlData1.Rows(o)("BillNo"), sqlData1.Rows(o)("AccountNumber"), sqlData1.Rows(o)("CustomerName") _
                            , sqlData1.Rows(o)("Reading"), sqlData1.Rows(o)("PreviousReading"), sqlData1.Rows(o)("Consumption"), Format(totalamountdue, "standard") _
                            , Format(totalcharges, "standard"), Format(totalamount, "standard"))

            Next


        End If

        Dim Curdi As String = My.Application.Info.DirectoryPath
        Dim g As String
        g = Curdi.Replace("bin\Debug", "")


        Dim rds As New Microsoft.Reporting.WinForms.ReportDataSource
        rds.Name = "DataSet1"
        rds.Value = dt


        ReportViewer1.LocalReport.DataSources.Add(rds)
        'ReportViewer1.LocalReport.ReportPath = g & "BillingList.rdlc"
        ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\BillingList.rdlc"
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("BillMonth", ReadingDate.Text))
        ReportViewer1.ZoomMode = WinForms.ZoomMode.PageWidth
        ReportViewer1.RefreshReport()

    End Sub

    Sub billinglistspesificzone()

        Dim totalamountdue As Decimal

        Dim dt As New DataTable

        With dt
            .Columns.Add("zone")
            .Columns.Add("billno")
            .Columns.Add("accno")
            .Columns.Add("consess")
            .Columns.Add("presentrdng")
            .Columns.Add("prvsrdng")
            .Columns.Add("cumused")
            .Columns.Add("amount")
            .Columns.Add("totalcharges")
            .Columns.Add("totalamount")

        End With

        If acsconn.State = ConnectionState.Closed Then acsconn.Open()

        sqlData1.Clear()
        stracs = "SELECT a.[Zone],
	            c.ZoneID,
	            a.BillNo,
	            a.AccountNumber,
	            a.CustomerName,
	            a.Reading,
	            a.PreviousReading,
	            a.Consumption,
	            a.AmountDue,
	            totalcharges=(select sum(Amount) from billcharges b where AccountNumber = a.AccountNumber and b.BillingMonth = '" & ReadingDate.Text & "' AND b.Cancelled = 'No' and b.Status = 'Posted' and b.Cancelled = 'No')
              FROM [eBACS].[dbo].[Bills] a join [dbo].[Zone] c on c.ZoneName = a.[Zone] where a.zone = '" & cbzone.Text & "' and a.BillingDate = '" & ReadingDate.Text & "' and a.Cancelled = 'No' and a.BillStatus = 'Posted'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(sqlData1)

        If sqlData1.Rows.Count = 0 Then
            MsgBox("No Data Found")

        Else

            For o = 0 To sqlData1.Rows.Count - 1

                Dim totalcharges, totalamount As Decimal
                totalcharges = 0
                totalamount = 0


                If IsDBNull(sqlData1.Rows(o)("totalcharges")) = True Then
                    totalcharges = 0
                Else
                    totalcharges = sqlData1.Rows(o)("totalcharges")
                End If

                totalamountdue = Double.Parse(sqlData1.Rows(o)("AmountDue"))

                totalamount = totalcharges + Decimal.Parse(totalamountdue)


                dt.Rows.Add(Format(sqlData1.Rows(o)("ZoneID"), "00") & " - " & sqlData1.Rows(o)("Zone"), sqlData1.Rows(o)("BillNo"), sqlData1.Rows(o)("AccountNumber"), sqlData1.Rows(o)("CustomerName") _
                            , sqlData1.Rows(o)("Reading"), sqlData1.Rows(o)("PreviousReading"), sqlData1.Rows(o)("Consumption"), Format(totalamountdue, "standard") _
                            , Format(totalcharges, "standard"), Format(totalamount, "standard"))

            Next

        End If

        Dim Curdi As String = My.Application.Info.DirectoryPath
        Dim g As String
        g = Curdi.Replace("bin\Debug", "")


        Dim rds As New Microsoft.Reporting.WinForms.ReportDataSource
        rds.Name = "DataSet1"
        rds.Value = dt


        ReportViewer1.LocalReport.DataSources.Add(rds)
        'ReportViewer1.LocalReport.ReportPath = g & "BillingList.rdlc"
        ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\BillingList.rdlc"

        If sqlData1.Rows.Count = 0 Then
            ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("BillMonth", ReadingDate.Text & " - " & cbzone.Text))
        Else
            ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("BillMonth", ReadingDate.Text & " - " & Format(sqlData1.Rows(0)("ZoneID"), "00") & " - " & cbzone.Text))
        End If

        ReportViewer1.ZoomMode = WinForms.ZoomMode.PageWidth
        ReportViewer1.RefreshReport()

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Me.Activate()
    End Sub

    Public MoveFormBillList As Boolean
    Public MoveFormBillList_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormBillList = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormBillList_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormBillList Then
            Me.Location = Me.Location + (e.Location - MoveFormBillList_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormBillList = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub BillingList_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub postbill_deactivated(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.BackColor = Color.FromArgb(17, 153, 195)
    End Sub

    Private Sub postbill_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.BackColor = Color.SteelBlue
    End Sub

    Private Sub ControlsClick(sender As Object, e As EventArgs) Handles _
        Panel1.Click, cbzone.Click, ReadingDate.Click, billSearch.Click, ReportViewer1.Click  ' etc.
        Me.Activate() 'Or Whatever
    End Sub

End Class