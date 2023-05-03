Imports WindowsApplication2.Moletrator.SQLDocumentor
Imports Microsoft.Reporting
Public Class OtherAdjustmentReport
    Private Sub OtherAdjustmentReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain
        prog.Visible = False
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.ZoomMode.PageWidth)
        Me.ReportViewer1.RefreshReport()
    End Sub

    Private Sub billSearch_Click(sender As Object, e As EventArgs) Handles billSearch.Click
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.Reset()

        loadadjustment()
    End Sub

    Sub loadadjustment()
        Cursor = Cursors.WaitCursor
        prog.Value = 0
        prog.Visible = True
        billSearch.Enabled = False

        Dim dt As New DataTable

        With dt

            .Columns.Add("refno")
            .Columns.Add("datecreated")
            .Columns.Add("particulars")
            .Columns.Add("accountno")
            .Columns.Add("accountname")
            .Columns.Add("amount")

        End With

        Dim adjustment As New DataTable

        adjustment.Clear()

        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        stracs = "select RefNo,DateCreated,Particulars,AccountName,AccountNumber,sum(Billing) as Billing from AddAdjustment where MONTH(DateCreated) = '" & ReadingDate.Value.Month & "' and Year(DateCreated) = '" & ReadingDate.Value.Year & "' " _
            & "group by RefNo,DateCreated,Particulars,AccountNumber,AccountName"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(adjustment)

        If adjustment.Rows.Count = 0 Then
            MsgBox("No data found")
        Else
            For b = 0 To adjustment.Rows.Count - 1

                'Dim oldbill, newbill As Decimal
                'oldbill = 0
                'newbill = 0

                'oldbill = (Decimal.Parse(adjustment.Rows(b)("OldAmountDue")) + Decimal.Parse(adjustment.Rows(b)("OldPenalty"))) - (Decimal.Parse(adjustment.Rows(b)("OldAdvance")) + Decimal.Parse(adjustment.Rows(b)("OldDiscount")))
                'newbill = (Decimal.Parse(adjustment.Rows(b)("NewAmountDue")) + Decimal.Parse(adjustment.Rows(b)("NewPenalty"))) - (Decimal.Parse(adjustment.Rows(b)("NewAdvance")) + Decimal.Parse(adjustment.Rows(b)("NewDiscount")))
                dt.Rows.Add(adjustment.Rows(b)("RefNo"), Format(adjustment.Rows(b)("DateCreated"), "Short Date"), adjustment.Rows(b)("Particulars"), adjustment.Rows(b)("AccountNumber"), adjustment.Rows(b)("AccountName"), Format(adjustment.Rows(b)("Billing"), "standard"))
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
        'ReportViewer1.LocalReport.ReportPath = g & "otheradjustment.rdlc"
        ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\otheradjustment.rdlc"
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("monthcovered", ReadingDate.Text))


        ReportViewer1.ZoomMode = WinForms.ZoomMode.PageWidth
        ReportViewer1.RefreshReport()





        billSearch.Enabled = True
        prog.Visible = False
        Cursor = Cursors.Default
    End Sub


End Class