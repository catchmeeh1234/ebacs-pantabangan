Public Class settingsMaterials
    Private Sub settingsMaterials_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain
        loadmaterials()
    End Sub

    Sub loadmaterials()

        Try
            sqlDatacharges.Clear()
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            stracs = "SELECT MaterialsID, Particulars, Amount FROM Materials order by MaterialsID"

            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(sqlDatacharges)


            listZone.Items.Clear()

            For i = 0 To sqlDatacharges.Rows.Count - 1
                With listZone.Items.Add(sqlDatacharges.Rows(i)("MaterialsID"))
                    .SubItems.Add(sqlDatacharges.Rows(i)("Particulars"))
                    .SubItems.Add(sqlDatacharges.Rows(i)("Amount"))
                End With
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
            acsconn.Close()
        End Try

    End Sub

    Public Sub refreshfileds()

        txtZoneID.Clear()
        txtZoneName.Clear()
        txtLastNo.Clear()

        lblmode.Hide()
        lblmode.Text = "Mode"

    End Sub


    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click

        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            lblmode.Text = "Create New"
            lblmode.ForeColor = Color.Green
            txtZoneID.Clear()
            txtZoneName.Clear()
            txtLastNo.ReadOnly = False
            txtZoneName.ReadOnly = False
            'txtLastNo.ReadOnly = False

            txtLastNo.Clear()
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
                    MsgBox("Charge name Empty.")
                Else

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                    stracs = "select Particulars from Materials where Particulars = '" & txtZoneName.Text & "'"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsdr = acscmd.ExecuteReader

                    If acsdr.Read = True Then
                        acsdr.Close()

                        MsgBox("Charge name already used.")

                    Else
                        acsdr.Close()

                        If IsNumeric(txtLastNo.Text) = True Then

                            stracs = "insert into Materials (Particulars, Amount) values ('" & txtZoneName.Text.ToString.Replace("'", "''") & "', " & txtLastNo.Text & ")"
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acscmd.ExecuteNonQuery()
                            acscmd.Dispose()

                            refreshfileds()
                            loadmaterials()

                        End If

                    End If

                End If

            End If

            If lblmode.Text = "Update Mode" Then

                If txtZoneName.Text = "" Then
                    MsgBox("Materials name Empty.")
                Else

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                    stracs = "select Particulars from Materials where Particulars = '" & txtZoneName.Text & "' and MaterialsID = " & txtZoneID.Text
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsdr = acscmd.ExecuteReader

                    If acsdr.Read = True Then
                        acsdr.Close()

                        stracs = "update Materials set Amount = " & txtLastNo.Text & " where MaterialsID = " & txtZoneID.Text
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        refreshfileds()
                        loadmaterials()

                    Else
                        acsdr.Close()

                        If IsNumeric(txtLastNo.Text) = True Then

                            stracs = "select Particulars from Materials where Particulars = '" & txtZoneName.Text & "'"
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acsdr = acscmd.ExecuteReader

                            If acsdr.Read = True Then
                                acsdr.Close()

                                MsgBox("Material name already used.")

                            Else
                                acsdr.Close()

                                stracs = "update Materials set Particulars = '" & txtZoneName.Text.ToString.Replace("'", "''") & "', Amount = " & txtLastNo.Text _
                                & " where MaterialsID = " & txtZoneID.Text
                                acscmd.CommandText = stracs
                                acscmd.Connection = acsconn
                                acscmd.ExecuteNonQuery()
                                acscmd.Dispose()

                                refreshfileds()
                                loadmaterials()

                            End If

                        End If

                    End If

                End If

            End If
        Else
            MsgBox("Your account cannot perform this process.")
        End If


    End Sub

    Private Sub listZone_DoubleClick(sender As Object, e As EventArgs) Handles listZone.DoubleClick

        txtZoneID.Text = listZone.SelectedItems(0).SubItems(0).Text
        txtZoneName.Text = listZone.SelectedItems(0).SubItems(1).Text
        txtLastNo.Text = listZone.SelectedItems(0).SubItems(2).Text

        lblmode.Hide()
        lblmode.Text = "Mode"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub settingsMaterials_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Me.Activate()
    End Sub

    Public MoveFormMaterials As Boolean
    Public MoveFormMaterials_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormMaterials = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormMaterials_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormMaterials Then
            Me.Location = Me.Location + (e.Location - MoveFormMaterials_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormMaterials = False
            Me.Cursor = Cursors.Default
        End If

    End Sub
End Class