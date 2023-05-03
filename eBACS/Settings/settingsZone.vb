Public Class settingsZone
    Private Sub settingsZone_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain
        loadzones()
    End Sub

    Public Sub refreshfileds()

        txtZoneID.Clear()
        txtZoneName.Clear()
        txtLastNo.Clear()

        lblmode.Hide()
        lblmode.Text = "Mode"

    End Sub

    Public Sub loadzones()

        Try
            sqldataZone.Clear()
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            stracs = "SELECT ZoneID,ZoneName,LastNumber FROM Zone"

            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(sqldataZone)


            listZone.Items.Clear()

            For i = 0 To sqldataZone.Rows.Count - 1
                With listZone.Items.Add(sqldataZone.Rows(i)("ZoneID"))
                    .SubItems.Add(sqldataZone.Rows(i)("ZoneName"))
                    .SubItems.Add(sqldataZone.Rows(i)("LastNumber"))
                End With
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
            acsconn.Close()
        End Try

    End Sub


    Private Sub listZone_DoubleClick(sender As Object, e As EventArgs) Handles listZone.DoubleClick

        txtZoneID.Text = listZone.SelectedItems(0).SubItems(0).Text
        txtZoneName.Text = listZone.SelectedItems(0).SubItems(1).Text
        txtLastNo.Text = listZone.SelectedItems(0).SubItems(2).Text

        lblmode.Hide()
        lblmode.Text = "Mode"

    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            lblmode.Text = "Create New"
            lblmode.ForeColor = Color.Green
            txtZoneID.Clear()
            txtZoneName.Clear()
            txtLastNo.Text = 1
            txtZoneName.ReadOnly = False
            'txtLastNo.ReadOnly = False

            txtZoneName.Select()

            lblmode.Show()
        Else
            MsgBox("Your account cannot perform this process.")
        End If


    End Sub

    Private Sub EditToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem1.Click
        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            If txtZoneID.Text = "" Then

            Else

                lblmode.Text = "Update Mode"
                lblmode.ForeColor = Color.Orange
                txtZoneName.ReadOnly = False
                txtLastNo.ReadOnly = False

                txtZoneName.Select()

                lblmode.Show()

            End If
        Else
            MsgBox("Your account cannot perform this process.")
        End If


    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            If lblmode.Text = "Create New" Then

                If txtZoneName.Text = "" Then
                    MsgBox("Zone name Empty.")
                Else

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                    stracs = "select ZoneName from Zone where ZoneName = '" & txtZoneName.Text & "'"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsdr = acscmd.ExecuteReader

                    If acsdr.Read = True Then
                        acsdr.Close()

                        MsgBox("Zone name already used.")

                    Else
                        acsdr.Close()

                        If IsNumeric(txtLastNo.Text) = True Then

                            stracs = "insert into Zone (ZoneName, LastNumber) values ('" & txtZoneName.Text.ToString.Replace("'", "''") & "', " & txtLastNo.Text & ")"
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acscmd.ExecuteNonQuery()
                            acscmd.Dispose()

                            refreshfileds()
                            loadzones()

                        End If

                    End If

                End If

            End If

            If lblmode.Text = "Update Mode" Then

                If txtZoneName.Text = "" Then
                    MsgBox("Zone name Empty.")
                Else

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                    stracs = "select ZoneName from Zone where ZoneName = '" & txtZoneName.Text & "' and ZoneID = " & txtZoneID.Text
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsdr = acscmd.ExecuteReader

                    If acsdr.Read = True Then
                        acsdr.Close()

                        stracs = "update Zone set LastNumber = " & txtLastNo.Text & " where ZoneID = " & txtZoneID.Text
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        refreshfileds()
                        loadzones()

                    Else
                        acsdr.Close()

                        If IsNumeric(txtLastNo.Text) = True Then

                            stracs = "select ZoneName from Zone where ZoneName = '" & txtZoneName.Text & "'"
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acsdr = acscmd.ExecuteReader

                            If acsdr.Read = True Then
                                acsdr.Close()

                                MsgBox("Zone name already used.")

                            Else
                                acsdr.Close()

                                stracs = "update Zone set ZoneName = '" & txtZoneName.Text.ToString.Replace("'", "''") & "', LastNumber = " & txtLastNo.Text _
                                & " where ZoneID = " & txtZoneID.Text
                                acscmd.CommandText = stracs
                                acscmd.Connection = acsconn
                                acscmd.ExecuteNonQuery()
                                acscmd.Dispose()

                                refreshfileds()
                                loadzones()

                            End If

                        End If

                    End If

                End If

            End If
        Else
            MsgBox("Your account cannot perform this process.")
        End If


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub settingsZone_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Me.Activate()
    End Sub

    Public MoveFormZone As Boolean
    Public MoveFormZone_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormZone = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormZone_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormZone Then
            Me.Location = Me.Location + (e.Location - MoveFormZone_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormZone = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

End Class