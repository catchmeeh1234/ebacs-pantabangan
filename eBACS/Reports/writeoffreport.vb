Imports Microsoft.Reporting
Public Class writeoffreport
    Private Sub writeoffreport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.MdiParent = eBACSmain

        Me.ReportViewer1.RefreshReport()
    End Sub

    Private Sub billSearch_Click(sender As Object, e As EventArgs) Handles billSearch.Click

        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.Reset()
        loaddata()

    End Sub

    Sub loaddata()

        Cursor = Cursors.WaitCursor

        Dim dt As New DataTable

        With dt

            .Columns.Add("wdate")
            .Columns.Add("accountno")
            .Columns.Add("accountname")
            .Columns.Add("billing")
            .Columns.Add("penalty")
            .Columns.Add("charges")
            .Columns.Add("totalamount")

        End With

        Dim writeoff As New DataTable
        writeoff.Clear()


        stracs = "select * from Writeoff where year(DateCreated) = '" & Format(dtpasof.Value, "yyyy") & "'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(writeoff)

        If writeoff.Rows.Count = 0 Then

        Else
            For u = 0 To writeoff.Rows.Count - 1

                dt.Rows.Add(Format(writeoff.Rows(u)("DateCreated"), "Short Date"), writeoff.Rows(u)("AccountNumber"), writeoff.Rows(u)("AccountName") _
                            , Format(writeoff.Rows(u)("billing"), "Standard"), Format(writeoff.Rows(u)("penalty"), "Standard"), Format(writeoff.Rows(u)("charges"), "Standard") _
                            , Format(Val(writeoff.Rows(u)("billing")) + Val(writeoff.Rows(u)("penalty")) + Val(writeoff.Rows(u)("charges")), "Standard"))

            Next

        End If


        Dim Curdi As String = My.Application.Info.DirectoryPath
        Dim g As String
        g = Curdi.Replace("bin\Debug", "")

        Dim rds As New Microsoft.Reporting.WinForms.ReportDataSource
        rds.Name = "DataSet1"
        rds.Value = dt

        ReportViewer1.LocalReport.DataSources.Add(rds)
        'ReportViewer1.LocalReport.ReportPath = g & "Writeoff.rdlc"
        ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\Writeoff.rdlc"


        'ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("asof", "As of " & dtpasof.Value.ToString("dddd") & ", " & dtpasof.Value.ToString("MMMM dd, yyyy")))

        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("yearcovered", "for the year " & dtpasof.Text))



        ReportViewer1.ZoomMode = WinForms.ZoomMode.PageWidth
        ReportViewer1.RefreshReport()


        billSearch.Enabled = True
        Cursor = Cursors.Default

    End Sub

End Class