Imports Microsoft.Reporting
Public Class reprintBIllsReport
    Private Sub reprintReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim meterreaderlist As New DataTable

        stracs = "SELECT distinct(MeterReader) as MeterReader FROM [eBACS].[dbo].[ReprintRecord] group by MeterReader"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(meterreaderlist)

        meterreader.Items.Clear()
        meterreader.Items.Add("All")
        For x = 0 To meterreaderlist.Rows.Count - 1
            meterreader.Items.Add(meterreaderlist.Rows(x)("MeterReader"))
        Next

        meterreader.Text = "All"
        summary.Checked = True

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If summary.Checked = True Then

            Dim dt As New DataTable

            With dt

                .Columns.Add("Billno")
                .Columns.Add("AccountNo")
                .Columns.Add("DateReprint")
                .Columns.Add("MeterReader")
                .Columns.Add("Zone")
                .Columns.Add("ReprintedCount")

            End With

            Dim countreprint As New DataTable

            countreprint.Clear()

            If meterreader.Text = "All" Then

                stracs = "SELECT distinct(MeterReader) as MeterReader, Count(MeterReader) as reprintCount FROM [eBACS].[dbo].[ReprintRecord] where DateReprint >= '" _
                & Format(datefrom.Value, "yyyy-MM-dd") & "' and DateReprint <= '" & Format(dateto.Value, "yyyy-MM-dd") & "' group by MeterReader"

            Else

                stracs = "SELECT distinct(MeterReader) as MeterReader, Count(MeterReader) as reprintCount FROM [eBACS].[dbo].[ReprintRecord] where DateReprint >= '" _
                & Format(datefrom.Value, "yyyy-MM-dd") & "' and DateReprint <= '" & Format(dateto.Value, "yyyy-MM-dd") & "' and MeterReader = '" _
                & meterreader.Text & "' group by MeterReader"

            End If



            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(countreprint)

            If countreprint.Rows.Count = 0 Then
                MsgBox("No Record Found")
            Else


                For b = 0 To countreprint.Rows.Count - 1

                    dt.Rows.Add("", "", "", countreprint.Rows(b)("MeterReader"), "", countreprint.Rows(b)("reprintCount"))

                Next

                Dim Curdi As String = My.Application.Info.DirectoryPath
                Dim g As String
                g = Curdi.Replace("bin\Debug", "")

                ReportViewer1.LocalReport.DataSources.Clear()
                Dim rds As New Microsoft.Reporting.WinForms.ReportDataSource
                rds.Name = "DataSet1"

                rds.Value = dt



                ReportViewer1.LocalReport.DataSources.Add(rds)
                'ReportViewer1.LocalReport.ReportPath = g & "reprintReport.rdlc"
                ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\reprintReport.rdlc"

                ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("datefromto", Format(datefrom.Value, "MMMM dd, yyyy") & " - " & Format(dateto.Value, "MMMM dd, yyyy")))

                ReportViewer1.ZoomMode = WinForms.ZoomMode.PageWidth
                ReportViewer1.RefreshReport()

            End If



        End If

        If detailed.Checked = True Then

            Dim dt As New DataTable

            With dt

                .Columns.Add("Billno")
                .Columns.Add("AccountNo")
                .Columns.Add("DateReprint")
                .Columns.Add("MeterReader")
                .Columns.Add("Zone")
                .Columns.Add("ReprintedCount")

            End With

            Dim countreprint As New DataTable

            countreprint.Clear()

            If meterreader.Text = "All" Then
                stracs = "SELECT BIllNo,AccountNo,DateReprint,ZoneName,MeterReader FROM [eBACS].[dbo].[ReprintRecord] where DateReprint >= '" _
                & Format(datefrom.Value, "yyyy-MM-dd") & "' and DateReprint <= '" & Format(dateto.Value, "yyyy-MM-dd") & "'"
            Else
                stracs = "SELECT BIllNo,AccountNo,DateReprint,ZoneName,MeterReader FROM [eBACS].[dbo].[ReprintRecord] where DateReprint >= '" _
                & Format(datefrom.Value, "yyyy-MM-dd") & "' and DateReprint <= '" & Format(dateto.Value, "yyyy-MM-dd") & "' and MeterReader = '" & meterreader.Text & "'"
            End If

            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(countreprint)

            If countreprint.Rows.Count = 0 Then
                MsgBox("No Record Found")
            Else

                For b = 0 To countreprint.Rows.Count - 1

                    dt.Rows.Add(countreprint.Rows(b)("Billno"), countreprint.Rows(b)("AccountNo"), Format(countreprint.Rows(b)("DateReprint"), "Short Date"), countreprint.Rows(b)("MeterReader"), countreprint.Rows(b)("ZoneName"), "1")

                Next

                Dim Curdi As String = My.Application.Info.DirectoryPath
                Dim g As String
                g = Curdi.Replace("bin\Debug", "")

                ReportViewer1.LocalReport.DataSources.Clear()
                Dim rds As New Microsoft.Reporting.WinForms.ReportDataSource
                rds.Name = "DataSet1"
                rds.Value = dt

                ReportViewer1.LocalReport.DataSources.Add(rds)
                'ReportViewer1.LocalReport.ReportPath = g & "reprintDetailed.rdlc"
                ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\reprintDetailed.rdlc"

                ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("datefromto", Format(datefrom.Value, "MMMM dd, yyyy") & " - " & Format(datefrom.Value, "MMMM dd, yyyy")))
                ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("meterreader", meterreader.Text))
                ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("totalcount", countreprint.Rows.Count))

                ReportViewer1.ZoomMode = WinForms.ZoomMode.PageWidth
                ReportViewer1.RefreshReport()

            End If



        End If

    End Sub
End Class