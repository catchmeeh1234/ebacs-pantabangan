Public Class settingsreaders
    Private Sub settingsreaders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain
        loadreaders()
    End Sub

    Public Sub loadreaders()

        Dim readerlistName As New DataTable
        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        readerlistName.Rows.Clear()

        stracs = "select * from MeterReader"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(readerlistName)

        If readerlistName.Rows.Count = 0 Then
        Else

            readerList.Rows.Clear()

            For x = 0 To readerlistName.Rows.Count - 1

                readerList.Rows.Add(readerlistName(x)("readerID"), readerlistName(x)("readerName"))

            Next
        End If

    End Sub

    Sub clearall()

        readerNo.Clear()
        readerUsername.Clear()
        readerPassword.Clear()
        readerRepassword.Clear()
        readerFullName.Clear()


        readerUsername.ReadOnly = True
        readerPassword.ReadOnly = True
        readerRepassword.ReadOnly = True
        readerFullName.ReadOnly = True


    End Sub

    Private Sub NewToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem1.Click

        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            clearall()
            lblmode.Text = "Create New"
            lblmode.ForeColor = Color.Green
            lblmode.Visible = True

            readerUsername.ReadOnly = False
            readerPassword.ReadOnly = False
            readerRepassword.ReadOnly = False
            readerFullName.ReadOnly = False
        Else
            MsgBox("Your account cannot perform this process.")
        End If



    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            If lblmode.Text = "Create New" Then

                Dim readercheckName As New DataTable
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                readercheckName.Rows.Clear()

                stracs = "select * from MeterReader where readerName = '" & readerFullName.Text.ToString.Replace("'", "''") & "' or readerUsername  = '" & readerUsername.Text.ToString.Replace("'", "''") & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(readercheckName)

                If readercheckName.Rows.Count = 0 Then

                    If readerPassword.Text <> readerRepassword.Text Then

                        MsgBox("Password did not match.")

                    Else

                        If readerUsername.Text = "" Or readerFullName.Text = "" Or readerPassword.Text = "" Then


                            If readerUsername.Text = "" Then

                                lblusername.ForeColor = Color.Red

                            Else
                                lblusername.ForeColor = Color.Black
                            End If

                            If readerFullName.Text = "" Then
                                lblfullname.ForeColor = Color.Red
                            Else
                                lblfullname.ForeColor = Color.Black
                            End If

                            If readerPassword.Text = "" Then
                                lblpassword.ForeColor = Color.Red
                            Else
                                lblpassword.ForeColor = Color.Black
                            End If

                            MsgBox("Please complete the information")

                        Else

                            stracs = "insert into MeterReader (readerName, readerUsername, readerPassword) values ('" _
                            & readerFullName.Text.ToString.Replace("'", "''") & "', '" _
                            & readerUsername.Text.ToString.Replace("'", "''") & "', '" _
                            & readerPassword.Text.ToString.Replace("'", "''") & "')"
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acscmd.ExecuteNonQuery()
                            acscmd.Dispose()

                            clearall()
                            loadreaders()
                            lblmode.Text = "Mode"
                            lblmode.ForeColor = Color.Black
                            lblmode.Visible = False


                            lblpassword.ForeColor = Color.Black
                            lblfullname.ForeColor = Color.Black
                            lblusername.ForeColor = Color.Black

                        End If

                    End If

                Else

                    If readercheckName.Rows(0)("readerName") = readerFullName.Text Then
                        MsgBox("Name already registered.")
                    End If

                    If readercheckName.Rows(0)("readerUsername") = readerUsername.Text Then
                        MsgBox("Username already registered.")
                    End If

                End If

            End If

            If lblmode.Text = "Update Mode" Then

                Dim readerdetails As New DataTable
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                readerdetails.Rows.Clear()

                stracs = "select * from MeterReader where readerID = " & readerNo.Text
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(readerdetails)

                If readerdetails.Rows.Count = 1 Then

                    If readerUsername.Text = "" Or readerFullName.Text = "" Or readerPassword.Text = "" Then


                        If readerUsername.Text = "" Then

                            lblusername.ForeColor = Color.Red

                        Else
                            lblusername.ForeColor = Color.Black
                        End If

                        If readerFullName.Text = "" Then
                            lblfullname.ForeColor = Color.Red
                        Else
                            lblfullname.ForeColor = Color.Black
                        End If

                        If readerPassword.Text = "" Then
                            lblpassword.ForeColor = Color.Red
                        Else
                            lblpassword.ForeColor = Color.Black
                        End If

                        MsgBox("Please complete the information")

                    Else

                        If readerdetails.Rows(0)("readerID") = readerNo.Text And readerdetails.Rows(0)("readerUsername") = readerUsername.Text And readerdetails.Rows(0)("readerName") = readerFullName.Text Then
                            MsgBox(readerdetails.Rows(0)("readerID"))
                            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                            readerdetails.Rows.Clear()

                            stracs = "update MeterReader set readerPassword = '" & readerPassword.Text.ToString.Replace("'", "''") & "' where readerID = " & readerNo.Text
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acscmd.ExecuteNonQuery()
                            acscmd.Dispose()

                            clearall()
                            loadreaders()
                            lblmode.Text = "Mode"
                            lblmode.ForeColor = Color.Black
                            lblmode.Visible = False

                            lblpassword.ForeColor = Color.Black
                            lblfullname.ForeColor = Color.Black
                            lblusername.ForeColor = Color.Black

                        Else

                            If readerdetails.Rows(0)("readerUsername") = readerUsername.Text And readerdetails.Rows(0)("readerName") <> readerFullName.Text Then

                                readerdetails.Rows.Clear()
                                stracs = "select * from MeterReader where readerName  = '" & readerFullName.Text.ToString.Replace("'", "''") & "'"
                                acscmd.CommandText = stracs
                                acscmd.Connection = acsconn
                                acsda.SelectCommand = acscmd
                                acsda.Fill(readerdetails)

                                If readerdetails.Rows.Count = 0 Then

                                    stracs = "update MeterReader set readerName = '" & readerFullName.Text.ToString.Replace("'", "''") & "', " _
                                    & "readerPassword = '" & readerPassword.Text.ToString.Replace("'", "''") & "' where readerID = " & readerNo.Text

                                    acscmd.CommandText = stracs
                                    acscmd.Connection = acsconn
                                    acscmd.ExecuteNonQuery()
                                    acscmd.Dispose()

                                    clearall()
                                    loadreaders()
                                    lblmode.Text = "Mode"
                                    lblmode.ForeColor = Color.Black
                                    lblmode.Visible = False


                                    lblpassword.ForeColor = Color.Black
                                    lblfullname.ForeColor = Color.Black
                                    lblusername.ForeColor = Color.Black

                                Else
                                    MsgBox("Name already registered.")
                                End If

                            End If

                            If readerdetails.Rows(0)("readerUsername") <> readerUsername.Text And readerdetails.Rows(0)("readerName") = readerFullName.Text Then

                                readerdetails.Rows.Clear()
                                stracs = "select * from MeterReader where readerUsername  = '" & readerUsername.Text.ToString.Replace("'", "''") & "'"
                                acscmd.CommandText = stracs
                                acscmd.Connection = acsconn
                                acsda.SelectCommand = acscmd
                                acsda.Fill(readerdetails)

                                If readerdetails.Rows.Count = 0 Then

                                    stracs = "update MeterReader set readerUsername = '" & readerUsername.Text.ToString.Replace("'", "''") & "', " _
                                    & "readerPassword = '" & readerPassword.Text.ToString.Replace("'", "''") & "' where readerID = " & readerNo.Text

                                    acscmd.CommandText = stracs
                                    acscmd.Connection = acsconn
                                    acscmd.ExecuteNonQuery()
                                    acscmd.Dispose()

                                    clearall()
                                    loadreaders()
                                    lblmode.Text = "Mode"
                                    lblmode.ForeColor = Color.Black
                                    lblmode.Visible = False


                                    lblpassword.ForeColor = Color.Black
                                    lblfullname.ForeColor = Color.Black
                                    lblusername.ForeColor = Color.Black

                                Else
                                    MsgBox("Userame already registered.")
                                End If

                            End If


                        End If

                    End If

                Else

                End If

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

    Private Sub readerList_DoubleClick(sender As Object, e As EventArgs) Handles readerList.DoubleClick

        Dim readerdetails As New DataTable
        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        readerdetails.Rows.Clear()

        stracs = "select * from MeterReader where readerID = " & readerList.Rows(readerList.CurrentCellAddress.Y).Cells(0).Value
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(readerdetails)

        If readerdetails.Rows.Count = 0 Then
        Else

            readerNo.Text = readerdetails(0)("readerID")
            readerUsername.Text = readerdetails(0)("readerUsername")
            readerPassword.Text = readerdetails(0)("readerPassword")
            readerRepassword.Text = readerdetails(0)("readerPassword")
            readerFullName.Text = readerdetails(0)("readerName")


        End If

    End Sub

    Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click

        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            If readerNo.Text = "" Then
            Else

                readerUsername.ReadOnly = False
                readerPassword.ReadOnly = False
                readerRepassword.ReadOnly = False
                readerFullName.ReadOnly = False


                lblmode.Text = "Update Mode"
                lblmode.ForeColor = Color.Orange
                lblmode.Visible = True

                readerUsername.Select()

            End If
        Else
            MsgBox("Your account cannot perform this process.")
        End If



    End Sub

    Private Sub settingsreaders_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        Me.Activate()
    End Sub

    Public MoveFormReader As Boolean
    Public MoveFormReader_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormReader = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormReader_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormReader Then
            Me.Location = Me.Location + (e.Location - MoveFormReader_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormReader = False
            Me.Cursor = Cursors.Default
        End If

    End Sub
End Class