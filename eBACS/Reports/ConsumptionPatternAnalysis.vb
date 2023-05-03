Imports Microsoft.Reporting

Public Class ConsumptionPatternAnalysis
    Private Sub ConsumptionPatternAnalysis_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain
        prog.Visible = False
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.ZoomMode.PageWidth)
        Me.ReportViewer1.RefreshReport()
    End Sub

    Private Sub billSearch_Click(sender As Object, e As EventArgs) Handles billSearch.Click
        Cursor = Cursors.WaitCursor

        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.Reset()

        loaddata()
        Cursor = Cursors.Default
    End Sub

    Sub loaddata()

        Cursor = Cursors.WaitCursor
        prog.Value = 0
        prog.Visible = True
        billSearch.Enabled = False

        Dim dt As New DataTable

        With dt
            .Columns.Add("RateSchedule")
            .Columns.Add("MeterSize")
            .Columns.Add("negative")
            .Columns.Add("zerotofive")
            .Columns.Add("sixtoten")
            .Columns.Add("elvtwen")
            .Columns.Add("twenthir")
            .Columns.Add("thirfort")
            .Columns.Add("forttofift")
            .Columns.Add("fiftup")
            .Columns.Add("total")

        End With

        Dim classtype As New DataTable

        classtype.Clear()
        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        stracs = "select Distinct RateSchedule FROM Bills WHERE BillingDate = '" & ReadingDate.Text & "' Order by RateSchedule ASC"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(classtype)

        If classtype.Rows.Count = 0 Then
            MsgBox("No data Found")
        Else
            For p = 0 To classtype.Rows.Count - 1

                Dim metersize As New DataTable

                metersize.Clear()
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                stracs = "select Distinct MeterSize FROM Bills WHERE BillingDate = '" & ReadingDate.Text & "' AND RateSchedule = '" & classtype.Rows(p)("RateSchedule") & "' Order by MeterSize ASC"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(metersize)

                If metersize.Rows.Count = 0 Then
                Else
                    For i = 0 To metersize.Rows.Count - 1
                        Dim total As Integer

                        total = 0

                        Dim conditionn As String
                        conditionn = " AND BillingDate = '" & ReadingDate.Text & "' AND RateSchedule = '" & classtype.Rows(p)("RateSchedule") & "' AND MeterSize = '" & metersize.Rows(i)("MeterSize") & "' and BillStatus = 'Posted' and Cancelled = 'No'"

                        Dim negativee As New DataTable
                        negativee.Clear()
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "Select count(consumption) As negative from bills where Consumption < 0 " & conditionn
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acsda.SelectCommand = acscmd
                        acsda.Fill(negativee)

                        Dim zerotofive As New DataTable
                        zerotofive.Clear()
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "select count(consumption) as zerotofive from bills where Consumption between 0 and 5 " & conditionn
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acsda.SelectCommand = acscmd
                        acsda.Fill(zerotofive)

                        Dim sixtoten As New DataTable
                        sixtoten.Clear()
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "select count(consumption) as sixtoten from bills where Consumption between 6 and 10 " & conditionn
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acsda.SelectCommand = acscmd
                        acsda.Fill(sixtoten)

                        Dim elvtwen As New DataTable
                        elvtwen.Clear()
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "select count(consumption) as elvtwen from bills where Consumption between 11 and 20 " & conditionn
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acsda.SelectCommand = acscmd
                        acsda.Fill(elvtwen)

                        Dim twenthir As New DataTable
                        twenthir.Clear()
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "select count(consumption) as twenthir from bills where Consumption between 21 and 30 " & conditionn
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acsda.SelectCommand = acscmd
                        acsda.Fill(twenthir)

                        Dim thirfort As New DataTable
                        thirfort.Clear()
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "select count(consumption) as thirfort from bills where Consumption between 31 and 40 " & conditionn
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acsda.SelectCommand = acscmd
                        acsda.Fill(thirfort)

                        Dim forttofift As New DataTable
                        forttofift.Clear()
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "select count(consumption) as forttofift from bills where Consumption between 41 and 50 " & conditionn
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acsda.SelectCommand = acscmd
                        acsda.Fill(forttofift)

                        Dim fiftup As New DataTable
                        fiftup.Clear()
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "select count(consumption) as fiftup from bills where Consumption >=51 " & conditionn
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acsda.SelectCommand = acscmd
                        acsda.Fill(fiftup)


                        total = negativee.Rows(0)("negative") + zerotofive.Rows(0)("zerotofive") + sixtoten.Rows(0)("sixtoten") _
                            + elvtwen.Rows(0)("elvtwen") + twenthir.Rows(0)("twenthir") + thirfort.Rows(0)("thirfort") _
                            + forttofift.Rows(0)("forttofift") + fiftup.Rows(0)("fiftup")

                        dt.Rows.Add(classtype.Rows(p)("RateSchedule"), metersize.Rows(i)("MeterSize"), negativee.Rows(0)("negative") _
                                    , zerotofive.Rows(0)("zerotofive"), sixtoten.Rows(0)("sixtoten"), elvtwen.Rows(0)("elvtwen") _
                                    , twenthir.Rows(0)("twenthir"), thirfort.Rows(0)("thirfort"), forttofift.Rows(0)("forttofift") _
                                    , fiftup.Rows(0)("fiftup"), total)

                    Next
                    prog.Value = p / classtype.Rows.Count * 100
                End If

            Next
        End If



        ' prog.Value = t / bills.Rows.Count * 100

        prog.Value = 100

        Dim Curdi As String = My.Application.Info.DirectoryPath
        Dim g As String
        g = Curdi.Replace("bin\Debug", "")

        Dim rds As New Microsoft.Reporting.WinForms.ReportDataSource
        rds.Name = "DataSet1"
        rds.Value = dt

        ReportViewer1.LocalReport.DataSources.Add(rds)
        'ReportViewer1.LocalReport.ReportPath = g & "ConsumptionPatternAnalysis.rdlc"
        ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\ConsumptionPatternAnalysis.rdlc"
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("billingmonth", ReadingDate.Text))


        ReportViewer1.ZoomMode = WinForms.ZoomMode.PageWidth
        ReportViewer1.RefreshReport()

        billSearch.Enabled = True
        prog.Visible = False
        Cursor = Cursors.Default

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) 
        Me.Close()
    End Sub

    Private Sub ConsumptionPatternAnalysis_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Me.Activate()
    End Sub

    Public MoveFormCPA As Boolean
    Public MoveFormCPA_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormCPA = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormCPA_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormCPA Then
            Me.Location = Me.Location + (e.Location - MoveFormCPA_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormCPA = False
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
        Panel1.Click, ReadingDate.Click, billSearch.Click, ReportViewer1.Click ' etc.
        Me.Activate() 'Or Whatever
    End Sub
End Class