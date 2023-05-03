Imports Microsoft.Reporting

Public Class AdjustmentSummary
    Private Sub AdjustmentSummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain
        prog.Visible = False
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.ZoomMode.PageWidth)
        Me.ReportViewer1.RefreshReport()

        'Me.StartPosition = FormStartPosition.Manual

        'Me.Size = New Size(eBACSmain.Panel2.Size)
        'Me.Location = New Point(eBACSmain.Panel2.Left)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Me.Close()
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
            .Columns.Add("datee")
            .Columns.Add("accno")
            .Columns.Add("cat")
            .Columns.Add("approved")
            .Columns.Add("amount")

        End With

        Dim adjustment As New DataTable

        adjustment.Clear()

        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        stracs = "select * from BIllAdjustment where MONTH(Date) = '" & ReadingDate.Value.Month & "' and Year(Date) = '" & ReadingDate.Value.Year & "'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(adjustment)

        If adjustment.Rows.Count = 0 Then
            MsgBox("No data found")
        Else
            For b = 0 To adjustment.Rows.Count - 1

                Dim oldbill, newbill As Decimal
                oldbill = 0
                newbill = 0

                oldbill = (Decimal.Parse(adjustment.Rows(b)("OldAmountDue")) + Decimal.Parse(adjustment.Rows(b)("OldPenalty"))) - (Decimal.Parse(adjustment.Rows(b)("OldAdvance")) + Decimal.Parse(adjustment.Rows(b)("OldDiscount")))
                newbill = (Decimal.Parse(adjustment.Rows(b)("NewAmountDue")) + Decimal.Parse(adjustment.Rows(b)("NewPenalty"))) - (Decimal.Parse(adjustment.Rows(b)("NewAdvance")) + Decimal.Parse(adjustment.Rows(b)("NewDiscount")))
                dt.Rows.Add(adjustment.Rows(b)("RefNo"), adjustment.Rows(b)("DatePosted"), adjustment.Rows(b)("AccountNo"), adjustment.Rows(b)("Remarks"), adjustment.Rows(b)("ApprovedBy"), FormatNumber(oldbill - newbill, UseParensForNegativeNumbers:=TriState.True))
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
        'ReportViewer1.LocalReport.ReportPath = g & "AdjustmentSummary.rdlc"
        ReportViewer1.LocalReport.ReportPath = My.Application.Info.DirectoryPath & "\AdjustmentSummary.rdlc"
        ReportViewer1.LocalReport.SetParameters(New WinForms.ReportParameter("billingmonth", ReadingDate.Text))


        ReportViewer1.ZoomMode = WinForms.ZoomMode.PageWidth
        ReportViewer1.RefreshReport()





        billSearch.Enabled = True
        prog.Visible = False
        Cursor = Cursors.Default
    End Sub


    Private Sub Label7_Click(sender As Object, e As EventArgs)
        Me.Activate()
    End Sub

    Public MoveFormAdjust As Boolean
    Public MoveFormAdjust_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormAdjust = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormAdjust_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormAdjust Then
            Me.Location = Me.Location + (e.Location - MoveFormAdjust_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormAdjust = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub AdjustmentSummary_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub postbill_deactivated(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.BackColor = Color.FromArgb(17, 153, 195)
    End Sub

    Private Sub postbill_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.BackColor = Color.SteelBlue
    End Sub

    Private Sub ControlsClick(sender As Object, e As EventArgs) Handles _
        Panel1.Click, ReadingDate.Click, billSearch.Click, ReportViewer1.Click  ' etc.
        Me.Activate() 'Or Whatever
    End Sub
End Class