Public Class settingscharges
    Private Sub charges_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain
        loadcharges()
        refreshfileds()

        chargesName.ReadOnly = True
        chargesAmount.ReadOnly = True
        chargesCat.Enabled = False
        chargesEntry.Enabled = False

    End Sub

    Sub loadcharges()

        Try
            sqlDatacharges.Clear()
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            stracs = "SELECT * FROM Charges order by ChargeID"

            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(sqlDatacharges)

            listZone.Items.Clear()

            For i = 0 To sqlDatacharges.Rows.Count - 1
                With listZone.Items.Add(sqlDatacharges.Rows(i)("ChargeID"))
                    .SubItems.Add(sqlDatacharges.Rows(i)("Category"))
                    .SubItems.Add(sqlDatacharges.Rows(i)("Entry"))
                    .SubItems.Add(sqlDatacharges.Rows(i)("Particular"))
                    .SubItems.Add(sqlDatacharges.Rows(i)("Amount"))
                End With
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
            acsconn.Close()
        End Try

    End Sub

    Public Sub refreshfileds()

        chargesID.Clear()
        chargesName.Clear()
        chargesAmount.Clear()
        chargesCat.SelectedIndex = -1
        chargesEntry.SelectedIndex = -1



        lblmode.Hide()
        lblmode.Text = "Mode"

    End Sub


    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            lblmode.Text = "Create New"
            lblmode.ForeColor = Color.Green
            chargesID.Clear()
            chargesName.Clear()
            chargesAmount.ReadOnly = False
            chargesName.ReadOnly = False
            chargesCat.Enabled = True
            chargesEntry.Enabled = True
            chargesEntry.SelectedIndex = -1
            chargesCat.SelectedIndex = -1
            'txtLastNo.ReadOnly = False

            chargesName.Select()

            lblmode.Show()
        Else
            MsgBox("Your account cannot perform this process.")
        End If


    End Sub

    Private Sub EditToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem1.Click
        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            If chargesID.Text = "" Then

            Else

                lblmode.Text = "Update Mode"
                lblmode.ForeColor = Color.Orange
                chargesName.ReadOnly = False
                chargesAmount.ReadOnly = False
                chargesCat.Enabled = True
                chargesEntry.Enabled = True

                chargesName.Select()

                lblmode.Show()

            End If
        Else
            MsgBox("Your account cannot perform this process.")
        End If


    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click

        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            If lblmode.Text = "Create New" Then

                If chargesName.Text.ToString.Replace("' ", "''") = "" Or chargesCat.Text = "" Or chargesEntry.Text = "" Then
                    MsgBox("Please completer the information.")
                Else

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                    stracs = "select Particular from Charges where Particular = '" & chargesName.Text.ToString.Replace("'", "''") & "'"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsdr = acscmd.ExecuteReader

                    If acsdr.Read = True Then
                        acsdr.Close()

                        MsgBox("Charge name already used.")

                    Else
                        acsdr.Close()

                        If IsNumeric(chargesAmount.Text) = True Then

                            stracs = "insert into Charges (Category, Entry, Particular, Amount) values ('" & chargesCat.Text.ToString.Replace("'", "''") & "', '" & chargesEntry.Text.ToString.Replace("'", "''") & "', '" & chargesName.Text.ToString.Replace("'", "''") & "', " & Double.Parse(chargesAmount.Text) & ")"
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acscmd.ExecuteNonQuery()
                            acscmd.Dispose()

                            refreshfileds()
                            loadcharges()

                        End If

                    End If

                End If

            End If

            If lblmode.Text = "Update Mode" Then

                If chargesName.Text.ToString.Replace("'", "''") = "" Then
                    MsgBox("Zonename Empty.")
                Else

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                    stracs = "select Particular from Charges where Particular = '" & chargesName.Text.ToString.Replace("'", "''") & "' and ChargeID = " & chargesID.Text
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsdr = acscmd.ExecuteReader

                    If acsdr.Read = True Then
                        acsdr.Close()

                        stracs = "update Charges set Amount = " & chargesAmount.Text & ", Category = '" & chargesCat.Text.ToString.Replace("'", "''") & "', " _
                        & "Entry = '" & chargesEntry.Text.ToString.Replace("'", "''") & "' where ChargeID = " & chargesID.Text
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        refreshfileds()
                        loadcharges()

                    Else
                        acsdr.Close()

                        If IsNumeric(chargesAmount.Text) = True Then

                            stracs = "Select Particular from Charges where Particular = '" & chargesName.Text.ToString.Replace("'", "''") & "'"
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acsdr = acscmd.ExecuteReader

                            If acsdr.Read = True Then
                                acsdr.Close()

                                MsgBox("Charge name already used.")

                            Else
                                acsdr.Close()

                                stracs = "update Charges set Particular = '" & chargesName.Text.ToString.Replace("'", "''") & "', Amount = " & chargesAmount.Text & ", " _
                                & " Category = '" & chargesCat.Text.ToString.Replace("'", "''") & "', " _
                                & "Entry = '" & chargesEntry.Text.ToString.Replace("'", "''") & "' where ChargeID = " & chargesID.Text
                                acscmd.CommandText = stracs
                                acscmd.Connection = acsconn
                                acscmd.ExecuteNonQuery()
                                acscmd.Dispose()

                                refreshfileds()
                                loadcharges()

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

        chargesID.Text = listZone.SelectedItems(0).SubItems(0).Text
        chargesCat.Text = listZone.SelectedItems(0).SubItems(1).Text
        chargesEntry.Text = listZone.SelectedItems(0).SubItems(2).Text
        chargesName.Text = listZone.SelectedItems(0).SubItems(3).Text
        chargesAmount.Text = listZone.SelectedItems(0).SubItems(4).Text

        lblmode.Hide()
        lblmode.Text = "Mode"

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click

        lblmode.Hide()
        lblmode.Text = "Mode"

        refreshfileds()

        chargesName.ReadOnly = True
        chargesAmount.ReadOnly = True
        chargesCat.Enabled = False
        chargesEntry.Enabled = False

    End Sub

    Private Sub settingscharges_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Me.Activate()
    End Sub

    Public MoveFormCharges As Boolean
    Public MoveFormCharges_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormCharges = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormCharges_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormCharges Then
            Me.Location = Me.Location + (e.Location - MoveFormCharges_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormCharges = False
            Me.Cursor = Cursors.Default
        End If

    End Sub
End Class