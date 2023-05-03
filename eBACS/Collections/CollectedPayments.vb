Public Class CollectedPayments
    Private Sub CollectedPayments_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.MdiParent = eBACSmain

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        Dim office As New DataTable
        office.Clear()

        stracs = "select Distinct Office FROM Collection_Details"
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
        stracs = "select Distinct Collector FROM Collection_Details"
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

        'cbcashier.SelectedIndex = -1
        'cboffice.SelectedIndex = -1

        cboffice.Text = My.Settings.Office_Name
        cbcashier.Text = My.Settings.Nickname


    End Sub

    Private Sub billSearch_Click(sender As Object, e As EventArgs) Handles billSearch.Click
        Cursor = Cursors.WaitCursor



        Dim sqlstring As String
        Dim collectiondata As New DataTable
        collectiondata.Clear()
        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        sqlstring = "SELECT * FROM Collection_Details"

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
            sqlstring = sqlstring & "AND DAY(PaymentDate) = '" & ReadingDate.Value.ToString("dd") & "' AND MONTH(PaymentDate) = '" & ReadingDate.Value.ToString("MM") & "' AND YEAR(PaymentDate) = '" & ReadingDate.Value.ToString("yyyy") & "' AND NOT Cancelled = 'Yes'  order by CollectionID DESC"

        Else
            sqlstring = sqlstring & " WHERE DAY(PaymentDate) = '" & ReadingDate.Value.ToString("dd") & "' AND MONTH(PaymentDate) = '" & ReadingDate.Value.ToString("MM") & "' AND YEAR(PaymentDate) = '" & ReadingDate.Value.ToString("yyyy") & "' AND NOT Cancelled = 'Yes' order by CollectionID DESC"
        End If

        acscmd.CommandText = sqlstring
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(collectiondata)

        If collectiondata.Rows.Count = 0 Then
            MsgBox("No data found")
            dgvlist.Rows.Clear()
            lbltotal.Text = "Total: 0.00"
        Else
            dgvlist.Rows.Clear()

            Dim total As Decimal
            total = 0
            For h = 0 To collectiondata.Rows.Count - 1
                dgvlist.Rows.Add(collectiondata.Rows(h)("CollectionID"), collectiondata.Rows(h)("CRNo"), collectiondata.Rows(h)("AccountName") _
                 , FormatNumber(collectiondata.Rows(h)("TotalAmountDue")), collectiondata.Rows(h)("CollectionStatus"))
            Next

            For m = 0 To dgvlist.Rows.Count - 1
                total = total + Decimal.Parse(dgvlist.Rows(m).Cells(3).Value)
            Next

            lbltotal.Text = "Total: " & FormatNumber(total)
            totalcount.Text = "Collected Payments: " & dgvlist.Rows.Count

        End If




        Cursor = Cursors.Default
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub dgvlist_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvlist.CellClick

        If dgvlist.Columns(e.ColumnIndex).HeaderText = "CR No" Then



            Collection_CR.Show()
            Collection_CR.Activate()

            Collection_CR.crno.Text = dgvlist.Rows(dgvlist.CurrentCellAddress.Y).Cells(1).Value

            Collection_CR.crno_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))



        End If

    End Sub

    Public MoveFormcollectedpayment As Boolean
    Public Movecollectedpayment_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormcollectedpayment = True
            Me.Cursor = Cursors.NoMove2D
            Movecollectedpayment_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormcollectedpayment Then
            Me.Location = Me.Location + (e.Location - Movecollectedpayment_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormcollectedpayment = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Me.Activate()
    End Sub

    Private Sub CollectedPayments_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub postbill_deactivated(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.BackColor = Color.FromArgb(17, 153, 195)
    End Sub

    Private Sub postbill_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.BackColor = Color.SteelBlue
    End Sub

    Private Sub ControlsClick(sender As Object, e As EventArgs) Handles _
        Panel1.Click, cboffice.Click, cbcashier.Click, ReadingDate.Click, billSearch.Click  ' etc.
        Me.Activate() 'Or Whatever
    End Sub

End Class