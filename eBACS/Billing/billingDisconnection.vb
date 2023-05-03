Public Class billingDisconnection
    Private Sub billingDisconnection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain
        loadzones()
        discOldDate.Value = Now
        discNewDate.Value = Now
    End Sub

    Public Sub loadzones()

        Dim loadcreatezones As New DataTable
        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try
        loadcreatezones.Clear()

        stracs = "select * from Zone"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(loadcreatezones)

        If loadcreatezones.Rows.Count = 0 Then
        Else
            createZone.Rows.Clear()

            For x = 0 To loadcreatezones.Rows.Count - 1

                createZone.Rows.Add(loadcreatezones(x)("ZoneID"), loadcreatezones(x)("ZoneName"), 0)

            Next

        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub discOldDate_ValueChanged(sender As Object, e As EventArgs) Handles discOldDate.ValueChanged
        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try
        Dim searchzones As New DataTable
        stracs = "select distinct Zone from Bills where DiscDate = '" & Format(discOldDate.Value, "yyyy-MM-dd") & "' group by Zone"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(searchzones)

        'kuya nag try po ako mag msgbox ng laman ng zones pero wala yung katuray-wala naman sigulo kinalaman yung allcaps? o may small at big letter?
        'wala naman po kuya sa vb. tas parehas naman po sila naka all caps sa condition e

        For x = 0 To createZone.Rows.Count - 1
            createZone.Rows(x).Cells(2).Value = 0
        Next

        If searchzones.Rows.Count = 0 Then


        Else
            For x = 0 To searchzones.Rows.Count - 1

                If IsDBNull(searchzones.Rows(x)("Zone")) = True Then

                Else

                    Dim oo As Integer = 0

                    Do Until oo = createZone.Rows.Count

                        'MsgBox(searchzones.Rows(x)("Zone").ToString.ToUpper & " - " & createZone.Rows(oo).Cells(1).Value.ToString.ToUpper)

                        If searchzones.Rows(x)("Zone").ToString.ToUpper = createZone.Rows(oo).Cells(1).Value.ToString.ToUpper Then



                            createZone.Rows(oo).Cells(2).Value = 1
                            oo = createZone.Rows.Count

                        Else

                            oo = oo + 1

                        End If

                    Loop

                End If

            Next
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try
        For x = 0 To createZone.Rows.Count - 1

            If createZone.Rows(x).Cells(2).Value = 1 Then

                Dim rowsaffected As Integer = 0

                stracs = "update Bills set DiscDate = '" & Format(discNewDate.Value, "yyyy-MM-dd") & "' where Zone = '" & createZone.Rows(x).Cells(1).Value _
                    & "' and DiscDate = '" & Format(discOldDate.Value, "yyyy-MM-dd") & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                'acscmd.ExecuteNonQuery()

                rowsaffected = rowsaffected + acscmd.ExecuteNonQuery()
                MsgBox(createZone.Rows(x).Cells(1).Value & " - " & rowsaffected)

                'acscmd.Dispose()

            End If

        Next

    End Sub

    Private Sub billingDisconnection_Click(sender As Object, e As EventArgs) Handles Me.Click

        Me.Activate()

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

        Me.Activate()

    End Sub

    Public MoveFormDiscDate As Boolean
    Public MoveFormDiscDate_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormDiscDate = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormDiscDate_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormDiscDate Then
            Me.Location = Me.Location + (e.Location - MoveFormDiscDate_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormDiscDate = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

End Class