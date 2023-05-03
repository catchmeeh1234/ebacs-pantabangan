Public Class TransactionTemplate
    Private Sub TransactionTemplate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain
        loadtemplate()


    End Sub

    Sub loadtemplate()
        Dim templatee As New DataTable
        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        templatee.Rows.Clear()

        stracs = "select DISTINCT TransactionType FROM TransactionTemplate ORDER BY TransactionType"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(templatee)

        If templatee.Rows.Count = 0 Then

        Else
            cboffice.Items.Clear()

            For u = 0 To templatee.Rows.Count - 1
                cboffice.Items.Add(templatee.Rows(u)("TransactionType"))
            Next
            cboffice.SelectedIndex = 0
        End If

    End Sub

    Private Sub cboffice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboffice.SelectedIndexChanged


        If lblmode.Text = "Mode" Then





            Dim transactionTemp As New DataTable
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            transactionTemp.Rows.Clear()

            stracs = "select * FROM TransactionTemplate where TransactionType = '" & cboffice.Text & "'"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(transactionTemp)

            dgvlist.Rows.Clear()

            If transactionTemp.Rows.Count = 0 Then

            Else

                For p = 0 To transactionTemp.Rows.Count - 1
                    dgvlist.Rows.Add(transactionTemp.Rows(p)("TransTempID"), transactionTemp.Rows(p)("ChargeID"), transactionTemp.Rows(p)("Category") _
                                     , transactionTemp.Rows(p)("Entry"), transactionTemp.Rows(p)("TransactionType"), transactionTemp.Rows(p)("Particular") _
                                     , transactionTemp.Rows(p)("Quantity"), transactionTemp.Rows(p)("UnitCost"), transactionTemp.Rows(p)("Amount"))
                Next

            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Public MoveFormAccounts As Boolean
    Public MoveFormAccounts_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormAccounts = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormAccounts_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormAccounts Then
            Me.Location = Me.Location + (e.Location - MoveFormAccounts_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormAccounts = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        Me.Activate()
    End Sub

    Private Sub TransactionTemplate_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub NewToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem1.Click

        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            lblmode.Text = "Create New"
            lblmode.ForeColor = Color.Green
            lblmode.Visible = True


            lbladditem.Visible = True
            cboffice.Text = ""
            cboffice.Items.Clear()
            cboffice.DropDownStyle = ComboBoxStyle.DropDown
            dgvlist.Rows.Clear()
        Else
            MsgBox("Your account cannot perform this process.")
        End If

    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        lblmode.Text = "Mode"
        lblmode.Visible = False
        cboffice.DropDownStyle = ComboBoxStyle.DropDownList
        loadtemplate()
        lbladditem.Visible = False

    End Sub

    Private Sub lbladditem_Click(sender As Object, e As EventArgs) Handles lbladditem.Click
        If cboffice.Text = "" Then
            MsgBox("Please add template name first")
        Else
            OR_Items.Show()
            OR_Items.BringToFront()
            OR_Items.Label4.Text = "Transaction Template"
            OR_Items.rbmaterials.Visible = False
            OR_Items.rbothers.Visible = False
        End If


    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click

        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            If lblmode.Text = "Create New" Then
                If cboffice.Text = "" Then
                    MsgBox("Please enter template name")
                ElseIf dgvlist.Rows.Count = 0 Then
                    MsgBox("Please add items")
                Else


                    For r = 0 To dgvlist.Rows.Count - 1
                        stracs = "INSERT INTO TransactionTemplate (ChargeID,Category,Entry,TransactionType,Particular,Quantity,UnitCost,Amount)
                            VALUES ('" & dgvlist.Rows(r).Cells(1).Value & "','" & dgvlist.Rows(r).Cells(2).Value & "'
                                    ,'" & dgvlist.Rows(r).Cells(3).Value & "','" & cboffice.Text & "'
                                    ,'" & dgvlist.Rows(r).Cells(5).Value & "','" & dgvlist.Rows(r).Cells(6).Value & "'
                                    ,'" & dgvlist.Rows(r).Cells(7).Value & "','" & dgvlist.Rows(r).Cells(8).Value & "')"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()


                    Next

                    MsgBox("Template Saved!")
                    lblmode.Text = "Mode"
                    lblmode.Visible = False
                    loadtemplate()
                    lbladditem.Visible = False

                End If
            End If
        Else
            MsgBox("Your account cannot perform this process.")
        End If


    End Sub

    Private Sub dgvlist_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvlist.KeyDown

        If lblmode.Text = "Mode" Then

        Else
            If e.KeyCode = Keys.Delete Then
                If dgvlist.Rows.Count = 0 Then

                Else

                    Try
                        dgvlist.Rows.Remove(dgvlist.CurrentRow)
                    Catch ex As Exception

                    End Try


                End If

            End If
        End If


    End Sub

End Class