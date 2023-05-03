Public Class SettingsRates
    Private Sub SettingsRates_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain
        Dim customertype As New DataTable
        customertype.Clear()
        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        stracs = "SELECT Distinct CustomerType FROM RateSchedules order by CustomerType"
        acscmd.Connection = acsconn
        acscmd.CommandText = stracs
        acsda.SelectCommand = acscmd
        acsda.Fill(customertype)

        If customertype.Rows.Count = 0 Then
        Else
            cbcustomertype.Items.Clear()
            For j = 0 To customertype.Rows.Count - 1
                cbcustomertype.Items.Add(customertype(j)("CustomerType"))
            Next
            cbcustomertype.SelectedIndex = 0
        End If


        Dim metersize As New DataTable
        metersize.Clear()
        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        stracs = "SELECT Distinct MeterSize FROM RateSchedules order by MeterSize"
        acscmd.Connection = acsconn
        acscmd.CommandText = stracs
        acsda.SelectCommand = acscmd
        acsda.Fill(metersize)

        If metersize.Rows.Count = 0 Then
        Else
            cbmetersize.Items.Clear()
            For k = 0 To metersize.Rows.Count - 1
                cbmetersize.Items.Add(metersize(k)("MeterSize"))
            Next
            cbmetersize.SelectedIndex = 0
        End If

        cbcustomertype.Enabled = False
        cbmetersize.Enabled = False


        loaddata()

    End Sub


    Sub loaddata()

        Dim ratedata As New DataTable
        ratedata.Clear()
        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        stracs = "SELECT * FROM RateSchedules order by CustomerType"
        acscmd.Connection = acsconn
        acscmd.CommandText = stracs
        acsda.SelectCommand = acscmd
        acsda.Fill(ratedata)

        If ratedata.Rows.Count = 0 Then
        Else
            dgvlist.Rows.Clear()

            For u = 0 To ratedata.Rows.Count - 1
                dgvlist.Rows.Add(ratedata(u)("RateSchedulesID"), ratedata(u)("CustomerType") _
                                 , ratedata(u)("MeterSize"), FormatNumber(ratedata(u)("MinimumCharge")), FormatNumber(ratedata(u)("twenty")) _
                                 , FormatNumber(ratedata(u)("thirty")), FormatNumber(ratedata(u)("forty")) _
                                 , FormatNumber(ratedata(u)("fifty")), FormatNumber(ratedata(u)("maxx")))
            Next
        End If

    End Sub

    Public Sub refreshfileds()

        txtid.Clear()
        txtmincharge.Clear()
        txteleven.Clear()
        txttwentyone.Clear()
        txtthirtyone.Clear()
        txtfortyone.Clear()
        txtfiftyone.Clear()


        cbcustomertype.SelectedIndex = 0
        cbmetersize.SelectedIndex = 0

        cbcustomertype.Enabled = False
        cbmetersize.Enabled = False

        lblmode.Hide()
        lblmode.Text = "Mode"

    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click


        'refreshfileds()

        'lblmode.Text = "Create New"
        'lblmode.ForeColor = Color.Green
        'txtmincharge.ReadOnly = False
        'txteleven.ReadOnly = False
        'txttwentyone.ReadOnly = False
        'txtthirtyone.ReadOnly = False
        'txtfortyone.ReadOnly = False
        'txtfiftyone.ReadOnly = False

        'cbcustomertype.Enabled = True
        'cbmetersize.Enabled = True


        'lblmode.Show()
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        lblmode.Hide()
        lblmode.Text = "Mode"

        txtmincharge.ReadOnly = True
        txteleven.ReadOnly = True
        txttwentyone.ReadOnly = True
        txtthirtyone.ReadOnly = True
        txtfortyone.ReadOnly = True
        txtfiftyone.ReadOnly = True

        cbcustomertype.Enabled = False
        cbmetersize.Enabled = False
    End Sub

    Private Sub txtmincharge_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtmincharge.KeyPress
        If e.KeyChar <> ControlChars.Back Then
            e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = ".")
        End If

    End Sub

    Private Sub txteleven_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txteleven.KeyPress
        If e.KeyChar <> ControlChars.Back Then
            e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = ".")
        End If

    End Sub

    Private Sub txttwentyone_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txttwentyone.KeyPress
        If e.KeyChar <> ControlChars.Back Then
            e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = ".")
        End If

    End Sub

    Private Sub txtthirtyone_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtthirtyone.KeyPress
        If e.KeyChar <> ControlChars.Back Then
            e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = ".")
        End If

    End Sub

    Private Sub txtfortyone_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtfortyone.KeyPress
        If e.KeyChar <> ControlChars.Back Then
            e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = ".")
        End If

    End Sub

    Private Sub txtfiftyone_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtfiftyone.KeyPress
        If e.KeyChar <> ControlChars.Back Then
            e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = ".")
        End If

    End Sub

    Private Sub EditToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem1.Click

        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            If txtid.Text = "" Then
                MsgBox("No data selected")
            Else

                lblmode.Text = "Update Mode"
                lblmode.ForeColor = Color.Orange


                txtmincharge.ReadOnly = False
                txteleven.ReadOnly = False
                txttwentyone.ReadOnly = False
                txtthirtyone.ReadOnly = False
                txtfortyone.ReadOnly = False
                txtfiftyone.ReadOnly = False

                cbcustomertype.Enabled = True
                cbmetersize.Enabled = True

                lblmode.Show()

            End If
        Else
            MsgBox("Your account cannot perform this process.")
        End If


    End Sub

    Private Sub dgvlist_DoubleClick(sender As Object, e As EventArgs) Handles dgvlist.DoubleClick
        'dgvlist.Rows(dgvlist.CurrentCellAddress.Y).Cells(0).Value

        Dim selectrate As New DataTable
        selectrate.Clear()
        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        stracs = "SELECT * FROM RateSchedules WHERE RateSchedulesID = '" & dgvlist.Rows(dgvlist.CurrentCellAddress.Y).Cells(0).Value & "'"
        acscmd.Connection = acsconn
        acscmd.CommandText = stracs
        acsda.SelectCommand = acscmd
        acsda.Fill(selectrate)

        If selectrate.Rows.Count = 0 Then
        Else

            txtid.Text = dgvlist.Rows(dgvlist.CurrentCellAddress.Y).Cells(0).Value
            txtmincharge.Text = selectrate.Rows(0)("MinimumCharge")
            txteleven.Text = selectrate.Rows(0)("twenty")
            txttwentyone.Text = selectrate.Rows(0)("thirty")
            txtthirtyone.Text = selectrate.Rows(0)("forty")
            txtfortyone.Text = selectrate.Rows(0)("fifty")
            txtfiftyone.Text = selectrate.Rows(0)("maxx")

            cbcustomertype.Text = selectrate.Rows(0)("CustomerType")
            cbmetersize.Text = selectrate.Rows(0)("MeterSize")

        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click

        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            If lblmode.Text = "Update Mode" Then

                txtmincharge.Text = txtmincharge.Text.Replace("'", "''")
                txteleven.Text = txteleven.Text.Replace("'", "''")
                txttwentyone.Text = txttwentyone.Text.Replace("'", "''")
                txtthirtyone.Text = txtthirtyone.Text.Replace("'", "''")
                txtfortyone.Text = txtfortyone.Text.Replace("'", "''")
                txtfiftyone.Text = txtfiftyone.Text.Replace("'", "''")

                cbcustomertype.Text = cbcustomertype.Text.Replace("'", "''")
                cbmetersize.Text = cbmetersize.Text.Replace("'", "''")


                stracs = "update RateSchedules set CustomerType = '" & cbcustomertype.Text & "' 
                        , MeterSize = '" & cbmetersize.Text & "' 
                        , MinimumCharge = '" & txtmincharge.Text & "' 
                        , twenty = '" & txteleven.Text & "' 
                        , thirty = '" & txttwentyone.Text & "' 
                        , forty = '" & txtthirtyone.Text & "' 
                        , fifty = '" & txtfortyone.Text & "' 
                        , maxx = '" & txtfiftyone.Text & "' 
                        where RateSchedulesID = " & txtid.Text
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                MsgBox("Update Complete")

                loaddata()

                refreshfileds()

            End If
        Else
            MsgBox("Your account cannot perform this process.")
        End If


    End Sub

    Private Sub SettingsRates_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Me.Activate()
    End Sub

    Public MoveFormRates As Boolean
    Public MoveFormRates_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormRates = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormRates_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormRates Then
            Me.Location = Me.Location + (e.Location - MoveFormRates_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormRates = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

End Class