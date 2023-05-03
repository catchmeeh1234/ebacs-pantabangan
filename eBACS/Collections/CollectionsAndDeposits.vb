Public Class CollectionsAndDeposits
    Dim officeletter As String
    Private Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click
        'AddDCR.Close()
        'AddDCR.Show()
        'AddDCR.BringToFront()

        AddDCR.btnsave.Text = "Save"
        AddDCR.Label7.Text = "Create New Deposit"

        AddDCR.ShowDialog()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub CollectionsAndDeposits_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain
        cboffice.SelectedIndex = 0
        loaddataa()
    End Sub

    Public Sub loaddataa()

        If cboffice.SelectedIndex = 0 Then
            officeletter = "A"
        Else
            officeletter = "B"
        End If

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try
        Dim dcrdata As New DataTable
        dcrdata.Clear()

        stracs = "SELECT * FROM DCR WHERE MONTH(ReportDate) = '" & Date.Now.ToString("MM") & "' AND YEAR(ReportDate) = '" & Date.Now.ToString("yyyy") & "' AND DCRNo LIKE '" & officeletter & "%' ORDER by DCRId DESC"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(dcrdata)

        dgvlist.Rows.Clear()
        If dcrdata.Rows.Count = 0 Then
        Else
            For l = 0 To dcrdata.Rows.Count - 1
                dgvlist.Rows.Add(dcrdata.Rows(l)("DCRId"), dcrdata.Rows(l)("DCRNo"), Format(dcrdata.Rows(l)("ReportDate"), "short date") _
                , Format(dcrdata.Rows(l)("Deposited"), "standard"), Format(dcrdata.Rows(l)("Undeposited"), "standard"), Format(dcrdata.Rows(l)("PreviousReportDate")), "short date")
            Next
        End If

    End Sub

    Private Sub cboffice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboffice.SelectedIndexChanged
        loaddataa()
    End Sub

    Private Sub ReadingDate_ValueChanged(sender As Object, e As EventArgs) Handles ReadingDate.ValueChanged
        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try
        Dim dcrdata As New DataTable
        dcrdata.Clear()

        stracs = "SELECT * FROM DCR WHERE ReportDate = '" & ReadingDate.Text & "' AND DCRNo LIKE '" & officeletter & "%' ORDER by DCRId DESC"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(dcrdata)

        dgvlist.Rows.Clear()
        If dcrdata.Rows.Count = 0 Then
        Else
            For l = 0 To dcrdata.Rows.Count - 1
                dgvlist.Rows.Add(dcrdata.Rows(l)("DCRId"), dcrdata.Rows(l)("DCRNo"), Format(dcrdata.Rows(l)("ReportDate"), "short date") _
                , dcrdata.Rows(l)("Deposited"), dcrdata.Rows(l)("Undeposited"), Format(dcrdata.Rows(l)("PreviousReportDate"), "short date"))
            Next
        End If
    End Sub

    Private Sub EditToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem1.Click
        If dgvlist.Rows.Count = 0 Then
            MsgBox("Please select record")
        Else

            Dim s As Integer
            s = dgvlist.CurrentRow.Index
            ' MsgBox(dgvlist.Item(2, s).Value)
            'AddDCR.Close()

            'AddDCR.Show()
            'AddDCR.BringToFront()
            AddDCR.id = dgvlist.Item(0, s).Value
            AddDCR.txtreportnumber.Text = dgvlist.Item(1, s).Value
            AddDCR.txtreportdate.Text = Date.Parse(dgvlist.Rows(dgvlist.CurrentCellAddress.Y).Cells(2).Value)
            AddDCR.txtdeposit.Text = dgvlist.Item(3, s).Value


            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try

            Dim lastreport As New DataTable
            lastreport.Clear()

            stracs = "SELECT * FROM DCR WHERE ReportDate = '" & dgvlist.Rows(dgvlist.CurrentCellAddress.Y).Cells(5).Value & "' AND DCRNo LIKE '" & officeletter & "%' ORDER by DCRId DESC"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(lastreport)
            If lastreport.Rows.Count = 0 Then
                AddDCR.txtundepositedlastreport.Text = "0"
            Else
                AddDCR.txtundepositedlastreport.Text = lastreport.Rows(0)("Undeposited")
            End If



            AddDCR.txtundepositedcollectionthisreport.Text = (Decimal.Parse(AddDCR.txtcollectionthisreport.Text) + Decimal.Parse(AddDCR.txtundepositedlastreport.Text)) - Decimal.Parse(AddDCR.txtdeposit.Text)

            AddDCR.btnsave.Text = "Update"
            AddDCR.Label7.Text = "Update"

            AddDCR.ShowDialog()

        End If
    End Sub

    Private Sub CollectionsAndDeposits_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Me.Activate()
    End Sub

    Public MoveFormCAD As Boolean
    Public MoveFormCAD_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormCAD = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormCAD_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormCAD Then
            Me.Location = Me.Location + (e.Location - MoveFormCAD_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormCAD = False
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
        Panel1.Click, cboffice.Click, ReadingDate.Click, btnadd.Click, dgvlist.Click  ' etc.
        Me.Activate() 'Or Whatever
    End Sub

End Class