Public Class SettingsAccounts

    Dim officeletter As String
    Dim admin, cservice, cashier, viewer As String
    Private Sub SettingsAccounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain
        loadaccounts()

        Dim officelist As New DataTable
        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        officelist.Rows.Clear()

        stracs = "select * from OfficeSettings"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(officelist)

        If officelist.Rows.Count = 0 Then
            MsgBox("No office found on database")
            Me.Close()
        Else
            cboffice.Items.Clear()

            For u = 0 To officelist.Rows.Count - 1
                cboffice.Items.Add(officelist.Rows(u)("OfficeName"))
            Next
            cboffice.SelectedIndex = 0
        End If

    End Sub

    Sub loadaccounts()
        Dim acclist As New DataTable
        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        acclist.Rows.Clear()

        stracs = "select * from useraccounts"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(acclist)

        dgvlist.Rows.Clear()

        If acclist.Rows.Count = 0 Then
        Else

            For x = 0 To acclist.Rows.Count - 1
                Dim dateeee As String
                If IsDBNull(acclist(x)("LastActivityDate")) = True Then
                    dateeee = ""
                Else
                    dateeee = acclist(x)("LastActivityDate")
                End If

                dgvlist.Rows.Add(acclist(x)("ID"), acclist(x)("username"), acclist(x)("fullname") _
                , acclist(x)("designation"), acclist(x)("office"), dateeee)
            Next
        End If
    End Sub

    Sub clearall()

        readerNo.Clear()
        txtusername.Clear()
        txtpass.Clear()
        txtconfirmpass.Clear()
        txtdesignation.Clear()
        txtfullname.Clear()
        cboffice.SelectedIndex = 0
        txtnickname.Clear()


        txtusername.ReadOnly = True
        txtpass.ReadOnly = True
        txtconfirmpass.ReadOnly = True
        txtdesignation.ReadOnly = True
        txtfullname.ReadOnly = True
        txtnickname.ReadOnly = True
        cboffice.Enabled = False


        cbcashier.Checked = False
        cbviewer.Checked = False
        cbadmin.Checked = False
        cbcservice.Checked = False

        cbcashier.Enabled = False
        cbviewer.Enabled = False
        cbadmin.Enabled = False
        cbcservice.Enabled = False

    End Sub

    Private Sub NewToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem1.Click

        If My.Settings.Admin = "Yes" Then
            clearall()
            lblmode.Text = "Create New"
            lblmode.ForeColor = Color.Green
            lblmode.Visible = True

            txtusername.ReadOnly = False
            txtnickname.ReadOnly = False
            txtpass.ReadOnly = False
            txtconfirmpass.ReadOnly = False
            txtdesignation.ReadOnly = False
            txtfullname.ReadOnly = False
            cboffice.Enabled = True
            cbcashier.Enabled = True
            cbviewer.Enabled = True
            cbadmin.Enabled = True
            cbcservice.Enabled = True
        Else
            MsgBox("Your account cannot perform this process.")
        End If


    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click

        If My.Settings.Admin = "Yes" Then
            If lblmode.Text = "Create New" Then

                If txtpass.Text <> txtconfirmpass.Text Then
                    MsgBox("Password did not match.")
                Else
                    If txtusername.Text = "" Or txtfullname.Text = "" Or txtpass.Text = "" Or txtdesignation.Text = "" Or txtnickname.Text = "" Then

                        If txtusername.Text = "" Then
                            lblusername.ForeColor = Color.Red
                        Else
                            lblusername.ForeColor = Color.Black
                        End If

                        If txtfullname.Text = "" Then
                            lblfullname.ForeColor = Color.Red
                        Else
                            lblfullname.ForeColor = Color.Black
                        End If

                        If txtpass.Text = "" Then
                            lblpassword.ForeColor = Color.Red
                        Else
                            lblpassword.ForeColor = Color.Black
                        End If

                        If txtdesignation.Text = "" Then
                            lbldesignation.ForeColor = Color.Red
                        Else
                            lbldesignation.ForeColor = Color.Black
                        End If

                        If txtnickname.Text = "" Then
                            lblnickname.ForeColor = Color.Red
                        Else
                            lblnickname.ForeColor = Color.Black
                        End If
                        MsgBox("Please complete the information")

                    ElseIf cbadmin.Checked = False AndAlso cbcashier.Checked = False AndAlso cbcservice.Checked = False AndAlso cbviewer.Checked = False Then
                        MsgBox("Please select user permission")
                    Else

                        If cbadmin.Checked = True Then
                            admin = "Yes"
                            cservice = "Yes"
                            cashier = "Yes"
                            viewer = "Yes"
                        Else
                            If cbcashier.Checked = True Then
                                cashier = "Yes"
                            Else
                                cashier = "No"
                            End If

                            If cbviewer.Checked = True Then
                                viewer = "Yes"
                            Else
                                viewer = "No"
                            End If

                            If cbcservice.Checked = True Then
                                cservice = "Yes"
                            Else
                                cservice = "No"
                            End If

                            admin = "No"
                        End If

                        Dim accchecker As New DataTable
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        accchecker.Rows.Clear()

                        stracs = "select username from useraccounts where username = '" & txtusername.Text.Replace("'", "''") & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acsda.SelectCommand = acscmd
                        acsda.Fill(accchecker)

                        If accchecker.Rows.Count = 0 Then

                            stracs = "insert into useraccounts (username,nickname, fullname,password,designation,cashier,cservice,admin,viewer,office,officeletter) 
                        Values ('" & txtusername.Text.Replace("'", "''") & "', '" & txtnickname.Text.Replace("'", "''") & "', '" & txtfullname.Text.Replace("'", "''") & "'
                        , '" & txtpass.Text.Replace("'", "''") & "', '" & txtdesignation.Text.Replace("'", "''") & "', '" & cashier & "'
                        , '" & cservice & "', '" & admin & "', '" & viewer & "', '" & cboffice.Text.Replace("'", "''") & "', '" & officeletter & "')"
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acscmd.ExecuteNonQuery()
                            acscmd.Dispose()

                            clearall()
                            loadaccounts()
                            lblmode.Text = "Mode"
                            lblmode.ForeColor = Color.Black
                            lblmode.Visible = False

                            My.Settings.Office_Code = officeletter
                            My.Settings.Office_Name = cboffice.Text
                            My.Settings.Save()

                            lblnickname.ForeColor = Color.Black
                            lblpassword.ForeColor = Color.Black
                            lblfullname.ForeColor = Color.Black
                            lblusername.ForeColor = Color.Black
                            lbldesignation.ForeColor = Color.Black
                            MsgBox("Account Saved!")

                        Else
                            MsgBox("Username already registered.")
                        End If

                    End If

                End If

            End If

            If lblmode.Text = "Update Mode" Then

                If txtpass.Text <> txtconfirmpass.Text Then
                    MsgBox("Password did not match.")
                Else
                    If txtusername.Text = "" Or txtfullname.Text = "" Or txtpass.Text = "" Or txtdesignation.Text = "" Or txtnickname.Text = "" Then

                        If txtusername.Text = "" Then
                            lblusername.ForeColor = Color.Red
                        Else
                            lblusername.ForeColor = Color.Black
                        End If

                        If txtfullname.Text = "" Then
                            lblfullname.ForeColor = Color.Red
                        Else
                            lblfullname.ForeColor = Color.Black
                        End If

                        If txtpass.Text = "" Then
                            lblpassword.ForeColor = Color.Red
                        Else
                            lblpassword.ForeColor = Color.Black
                        End If

                        If txtdesignation.Text = "" Then
                            lbldesignation.ForeColor = Color.Red
                        Else
                            lbldesignation.ForeColor = Color.Black
                        End If

                        If txtnickname.Text = "" Then
                            lblnickname.ForeColor = Color.Red
                        Else
                            lblnickname.ForeColor = Color.Black
                        End If

                        MsgBox("Please complete the information")

                    ElseIf cbadmin.Checked = False AndAlso cbcashier.Checked = False AndAlso cbcservice.Checked = False AndAlso cbviewer.Checked = False Then
                        MsgBox("Please select user permission")
                    Else
                        If cbadmin.Checked = True Then
                            admin = "Yes"
                            cservice = "Yes"
                            cashier = "Yes"
                            viewer = "Yes"
                        Else
                            If cbcashier.Checked = True Then
                                cashier = "Yes"
                            Else
                                cashier = "No"
                            End If

                            If cbviewer.Checked = True Then
                                viewer = "Yes"
                            Else
                                viewer = "No"
                            End If

                            If cbcservice.Checked = True Then
                                cservice = "Yes"
                            Else
                                cservice = "No"
                            End If
                            admin = "No"
                        End If

                        Dim accchecker As New DataTable
                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        accchecker.Rows.Clear()

                        stracs = "select username,ID from useraccounts where username = '" & txtusername.Text.Replace("'", "''") & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acsda.SelectCommand = acscmd
                        acsda.Fill(accchecker)

                        If accchecker.Rows.Count = 0 Then
                            updatedacc()
                        ElseIf accchecker.Rows(0)("ID") = readerNo.Text Then
                            updatedacc()
                        Else
                            MsgBox("Username already registered.")
                        End If

                    End If

                End If

            End If
        Else
            MsgBox("Your account cannot perform this process.")
        End If

    End Sub

    Sub updatedacc()

        stracs = "update useraccounts set username = '" & txtusername.Text.Replace("'", "''") & "', fullname = '" & txtfullname.Text.Replace("'", "''") & "'
        ,password = '" & txtpass.Text.Replace("'", "''") & "', designation = '" & txtdesignation.Text.Replace("'", "''") & "'
        , cashier = '" & cashier & "', cservice = '" & cservice & "', admin = '" & admin & "', viewer = '" & viewer & "'
        , office = '" & cboffice.Text.Replace("'", "''") & "', officeletter = '" & officeletter & "', nickname = '" & txtnickname.Text.Replace("'", "''") & "' WHERE ID = " & readerNo.Text & ""
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acscmd.ExecuteNonQuery()
        acscmd.Dispose()

        clearall()
        loadaccounts()
        lblmode.Text = "Mode"
        lblmode.ForeColor = Color.Black
        lblmode.Visible = False

        My.Settings.Office_Code = officeletter
        My.Settings.Office_Name = cboffice.Text
        My.Settings.Save()

        lblnickname.ForeColor = Color.Black
        lblpassword.ForeColor = Color.Black
        lblfullname.ForeColor = Color.Black
        lblusername.ForeColor = Color.Black
        lbldesignation.ForeColor = Color.Black
        MsgBox("Account Updated!")

    End Sub
    Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click

        If My.Settings.Admin = "Yes" Then
            If readerNo.Text = "" Then
            Else

                cbcashier.Enabled = True
                cbviewer.Enabled = True
                cbadmin.Enabled = True
                cbcservice.Enabled = True


                txtnickname.ReadOnly = False
                txtusername.ReadOnly = False
                txtpass.ReadOnly = False
                txtconfirmpass.ReadOnly = False
                txtdesignation.ReadOnly = False
                txtfullname.ReadOnly = False
                cboffice.Enabled = True

                lblmode.Text = "Update Mode"
                lblmode.ForeColor = Color.Orange
                lblmode.Visible = True

                txtusername.Select()

            End If
        Else
            MsgBox("Your account cannot perform this process.")
        End If

    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        clearall()
        lblmode.Text = "Mode"
        lblmode.Visible = False
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub dgvlist_DoubleClick(sender As Object, e As EventArgs) Handles dgvlist.DoubleClick
        Dim readerdetails As New DataTable
        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        readerdetails.Rows.Clear()

        stracs = "select * from useraccounts where ID = " & dgvlist.Rows(dgvlist.CurrentCellAddress.Y).Cells(0).Value
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(readerdetails)

        If readerdetails.Rows.Count = 0 Then
        Else

            readerNo.Text = readerdetails(0)("ID")
            txtusername.Text = readerdetails(0)("username")
            txtnickname.Text = readerdetails(0)("nickname")
            txtpass.Text = readerdetails(0)("password")
            txtconfirmpass.Text = readerdetails(0)("password")
            txtfullname.Text = readerdetails(0)("fullname")
            txtdesignation.Text = readerdetails(0)("designation")
            cboffice.Text = readerdetails(0)("office")

            If readerdetails(0)("admin") = "Yes" Then
                cbadmin.Checked = True
            Else
                cbadmin.Checked = False
            End If

            If readerdetails(0)("cashier") = "Yes" Then
                cbcashier.Checked = True
            Else
                cbcashier.Checked = False
            End If

            If readerdetails(0)("cservice") = "Yes" Then
                cbcservice.Checked = True
            Else
                cbcservice.Checked = False
            End If

            If readerdetails(0)("viewer") = "Yes" Then
                cbviewer.Checked = True
            Else
                cbviewer.Checked = False
            End If

        End If
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        Me.Activate()
    End Sub

    Private Sub cboffice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboffice.SelectedIndexChanged

        Dim officelet As New DataTable
        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        officelet.Rows.Clear()
        stracs = "select * from OfficeSettings where OfficeName = '" & cboffice.Text & "'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(officelet)

        If officelet.Rows.Count = 0 Then
        Else
            officeletter = officelet.Rows(0)("OfficeCode")
        End If

    End Sub
    Private Sub cboffice_TextChanged(sender As Object, e As EventArgs) Handles cboffice.TextChanged
        Dim officelet As New DataTable
        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        officelet.Rows.Clear()
        stracs = "select * from OfficeSettings where OfficeName = '" & cboffice.Text & "'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(officelet)

        If officelet.Rows.Count = 0 Then
        Else
            officeletter = officelet.Rows(0)("OfficeCode")
        End If

    End Sub

    Private Sub SettingsAccounts_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Public MoveFormAccounts As Boolean
    Public MoveFormAccounts_MousePosition As Point

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If readerNo.Text = "" Then
        Else

            stracs = "update useraccounts set ActiveSession = 0 where ID = " & readerNo.Text
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()

        End If

    End Sub

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

End Class