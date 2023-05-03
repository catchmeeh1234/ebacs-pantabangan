Public Class SettingsFieldFindings
    Private Sub SettingsFieldFindings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain
        loadfindings()
    End Sub

    Sub loadfindings()
        Try

            Dim findingsdata As New DataTable
            findingsdata.Clear()
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            stracs = "SELECT * FROM FieldFindingList order by FieldFindingsID"
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(findingsdata)


            listZone.Items.Clear()

            For i = 0 To findingsdata.Rows.Count - 1
                With listZone.Items.Add(findingsdata.Rows(i)("FieldFindingsID"))
                    .SubItems.Add(findingsdata.Rows(i)("Finding"))
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

        lblmode.Hide()
        lblmode.Text = "Mode"

    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click

        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            lblmode.Text = "Create New"
            lblmode.ForeColor = Color.Green
            txtZoneID.Clear()
            txtZoneName.Clear()

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


                txtZoneName.Select()

                lblmode.Show()

            End If
        Else
            MsgBox("Your account cannot perform this process.")
        End If

    End Sub

    Private Sub listZone_DoubleClick(sender As Object, e As EventArgs) Handles listZone.DoubleClick
        txtZoneID.Text = listZone.SelectedItems(0).SubItems(0).Text
        txtZoneName.Text = listZone.SelectedItems(0).SubItems(1).Text


        lblmode.Hide()
        lblmode.Text = "Mode"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click

        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            If lblmode.Text = "Create New" Then
                If txtZoneName.Text = "" Then
                    MsgBox("Findigns Empty.")
                Else

                    Dim findingschecker As New DataTable

                    findingschecker.Clear()
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    stracs = "SELECT * FROM FieldFindingList WHERE Finding = '" & txtZoneName.Text & "'"
                    acscmd.Connection = acsconn
                    acscmd.CommandText = stracs
                    acsda.SelectCommand = acscmd
                    acsda.Fill(findingschecker)
                    If findingschecker.Rows.Count = 0 Then

                        stracs = "insert into FieldFindingList (Finding) values ('" & txtZoneName.Text.ToString.Replace("'", "''") & "')"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        refreshfileds()
                        loadfindings()
                        txtZoneName.ReadOnly = True
                    Else
                        MsgBox("Findings Already in database")
                    End If


                End If
            End If

            If lblmode.Text = "Update Mode" Then
                If txtZoneName.Text = "" Then
                    MsgBox("Findigns Empty.")
                Else

                    stracs = "update FieldFindingList set Finding = '" & txtZoneName.Text.Replace("'", "''") & "' WHERE FieldFindingsID = '" & txtZoneID.Text & "'"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                    refreshfileds()
                    loadfindings()
                    txtZoneName.ReadOnly = True
                End If

            End If
        Else
            MsgBox("Your account cannot perform this process.")
        End If


    End Sub

    Private Sub SettingsFieldFindings_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Me.Activate()
    End Sub

    Public MoveFormFieldFindings As Boolean
    Public MoveFormFieldFindings_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormFieldFindings = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormFieldFindings_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormFieldFindings Then
            Me.Location = Me.Location + (e.Location - MoveFormFieldFindings_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormFieldFindings = False
            Me.Cursor = Cursors.Default
        End If

    End Sub
End Class