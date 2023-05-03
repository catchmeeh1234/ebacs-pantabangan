Public Class ORCollected
    Private Sub ORCollected_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain
        Dim office As New DataTable
        office.Clear()
        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try
        stracs = "select Distinct Office FROM OR_Details"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(office)

        If office.Rows.Count = 0 Then
        Else
            cboffice.Items.Clear()
            cboffice.Items.Add("All")

            For t = 0 To office.Rows.Count - 1
                cboffice.Items.Add(office.Rows(t)("Office"))
            Next

        End If

        Dim cashier As New DataTable
        cashier.Clear()
        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        stracs = "select Distinct Collector FROM OR_Details"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(cashier)

        If cashier.Rows.Count = 0 Then
        Else
            cbcashier.Items.Clear()
            cbcashier.Items.Add("All")

            For t = 0 To cashier.Rows.Count - 1
                cbcashier.Items.Add(cashier.Rows(t)("Collector"))
            Next

        End If

        'cbcashier.SelectedIndex = 0
        'cboffice.SelectedIndex = 0

        cboffice.Text = My.Settings.Office_Name
        cbcashier.Text = My.Settings.Nickname

    End Sub

    Private Sub billSearch_Click(sender As Object, e As EventArgs) Handles billSearch.Click
        Cursor = Cursors.WaitCursor

        Dim sqlstring As String
        Dim ordata As New DataTable
        ordata.Clear()

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        sqlstring = "SELECT * FROM OR_Details "

        If cboffice.SelectedIndex = 0 Then

        Else

            If sqlstring.Contains("WHERE") Then
                sqlstring = sqlstring & " Office = '" & cboffice.Text & "'"

            Else
                sqlstring = sqlstring & " WHERE Office = '" & cboffice.Text & "'"
            End If

        End If

        If cbcashier.SelectedIndex = 0 Then

        Else

            If sqlstring.Contains("WHERE") Then
                sqlstring = sqlstring & "AND Collector = '" & cbcashier.Text & "'"

            Else
                sqlstring = sqlstring & " WHERE Collector = '" & cbcashier.Text & "'"
            End If

        End If

        If sqlstring.Contains("WHERE") Then
            sqlstring = sqlstring & "AND DAY(PaymentDate) = '" & ReadingDate.Value.ToString("dd") & "' AND MONTH(PaymentDate) = '" & ReadingDate.Value.ToString("MM") & "' AND YEAR(PaymentDate) = '" & ReadingDate.Value.ToString("yyyy") & "' AND NOT Cancelled = 'Yes' order by OR_DetailsID desc"

        Else
            sqlstring = sqlstring & " WHERE DAY(PaymentDate) = '" & ReadingDate.Value.ToString("dd") & "' AND MONTH(PaymentDate) = '" & ReadingDate.Value.ToString("MM") & "' AND YEAR(PaymentDate) = '" & ReadingDate.Value.ToString("yyyy") & "' AND NOT Cancelled = 'Yes' order by OR_DetailsID desc"
        End If

        acscmd.CommandText = sqlstring
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(ordata)

        If ordata.Rows.Count = 0 Then
            MsgBox("No data found")
            dgvlist.Rows.Clear()
            lbltotal.Text = "Total: 0.00"
        Else
            dgvlist.Rows.Clear()

            Dim total As Decimal
            total = 0
            For h = 0 To ordata.Rows.Count - 1
                dgvlist.Rows.Add(ordata.Rows(h)("OR_DetailsID"), ordata.Rows(h)("ORNo"), ordata.Rows(h)("AccountName") _
                 , FormatNumber(ordata.Rows(h)("TotalAmountDue")), ordata.Rows(h)("Status"))
            Next

            For m = 0 To dgvlist.Rows.Count - 1
                total = total + Decimal.Parse(dgvlist.Rows(m).Cells(3).Value)
            Next

            lbltotal.Text = "Total: " & FormatNumber(total)
            lbltotalrow.Text = dgvlist.Rows.Count

        End If

        Cursor = Cursors.Default
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub ORCollected_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Me.Activate()
    End Sub


    Public MoveFormOrCollect As Boolean
    Public MoveFormOrCollect_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormOrCollect = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormOrCollect_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormOrCollect Then
            Me.Location = Me.Location + (e.Location - MoveFormOrCollect_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormOrCollect = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub dgvlist_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvlist.CellClick

        If dgvlist.Columns(e.ColumnIndex).HeaderText = "OR No" Then



            Create_OR.Show()
            Create_OR.Activate()

            Create_OR.orno.Text = dgvlist.Rows(dgvlist.CurrentCellAddress.Y).Cells(1).Value

            Create_OR.orno_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))



        End If

    End Sub

    Private Sub postbill_deactivated(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.BackColor = Color.FromArgb(17, 153, 195)
    End Sub

    Private Sub postbill_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.BackColor = Color.SteelBlue
    End Sub

    Private Sub ControlsClick(sender As Object, e As EventArgs) Handles _
        Panel1.Click, cboffice.Click, ReadingDate.Click, dgvlist.Click  ' etc.
        Me.Activate() 'Or Whatever
    End Sub

End Class