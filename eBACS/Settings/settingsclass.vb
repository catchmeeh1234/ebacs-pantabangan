Public Class settingsclass


    Private Sub settingsclass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain
        refreshfileds()
        loadclass()

    End Sub

    Public Sub refreshfileds()

        txtClassID.Clear()
        txtClassName.Clear()

        lblmode.Hide()
        lblmode.Text = "Mode"

    End Sub

    Public Sub loadclass()

        Try
            sqldataClass.Clear()
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            stracs = "SELECT CustomerTypeID, Type FROM CustomerType"
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(sqldataClass)


            listClass.Items.Clear()

            For i = 0 To sqldataClass.Rows.Count - 1
                With listClass.Items.Add(sqldataClass.Rows(i)("CustomerTypeID"))
                    .SubItems.Add(sqldataClass.Rows(i)("Type"))
                End With
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
            acsconn.Close()
        End Try

    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            lblmode.Text = "Create New"
            lblmode.ForeColor = Color.Green
            txtClassID.Clear()
            txtClassName.Clear()
            txtClassName.ReadOnly = False

            txtClassName.Select()

            lblmode.Show()
        Else
            MsgBox("Your account cannot perform this process.")
        End If


    End Sub

    Private Sub EditToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem1.Click
        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            If txtClassID.Text = "" Then

            Else

                lblmode.Text = "Update Mode"
                lblmode.ForeColor = Color.Orange
                txtClassName.ReadOnly = False

                txtClassName.Select()

                lblmode.Show()

            End If
        Else
            MsgBox("Your account cannot perform this process.")
        End If


    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click

        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            If lblmode.Text = "Create New" Then

                If txtClassName.Text = "" Then
                    MsgBox("Class name Empty.")
                Else

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                    stracs = "select Type from CustomerType where Type = '" & txtClassName.Text & "'"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsdr = acscmd.ExecuteReader

                    If acsdr.Read = True Then
                        acsdr.Close()

                        MsgBox("Class name already used.")

                    Else
                        acsdr.Close()

                        stracs = "insert into CustomerType (Type) values ('" & txtClassName.Text.ToString.Replace("'", "''") & "')"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        refreshfileds()
                        loadclass()

                    End If

                End If

            End If

            If lblmode.Text = "Update Mode" Then

                If txtClassName.Text = "" Then
                    MsgBox("Class name Empty.")
                Else

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                    stracs = "Select Type from CustomerType where Type = '" & txtClassName.Text & "' and CustomerTypeID = " & txtClassID.Text
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsdr = acscmd.ExecuteReader

                    If acsdr.Read = True Then
                        acsdr.Close()

                        refreshfileds()
                        loadclass()

                    Else
                        acsdr.Close()

                        stracs = "select Type from CustomerType where Type = '" & txtClassName.Text & "'"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acsdr = acscmd.ExecuteReader

                        If acsdr.Read = True Then
                            acsdr.Close()

                            MsgBox("Class name already used.")

                        Else
                            acsdr.Close()

                            stracs = "update CustomerType set Type = '" & txtClassName.Text.ToString.Replace("'", "''") & "' where CustomerTypeID = " & txtClassID.Text
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acscmd.ExecuteNonQuery()
                            acscmd.Dispose()

                            refreshfileds()
                            loadclass()

                        End If

                    End If

                End If

            End If
        Else
            MsgBox("Your account cannot perform this process.")
        End If



    End Sub

    Private Sub listZone_DoubleClick(sender As Object, e As EventArgs) Handles listClass.DoubleClick

        txtClassID.Text = listClass.SelectedItems(0).SubItems(0).Text
        txtClassName.Text = listClass.SelectedItems(0).SubItems(1).Text

        lblmode.Hide()
        lblmode.Text = "Mode"

    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        refreshfileds()
        loadclass()
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub settingsclass_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Me.Activate()
    End Sub

    Public MoveFormClass As Boolean
    Public MoveFormClass_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormClass = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormClass_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormClass Then
            Me.Location = Me.Location + (e.Location - MoveFormClass_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormClass = False
            Me.Cursor = Cursors.Default
        End If

    End Sub
End Class