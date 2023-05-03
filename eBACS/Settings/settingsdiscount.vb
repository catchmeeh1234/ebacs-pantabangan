Public Class settingsdiscount


    Private Sub discount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain
        loaddiscount()


    End Sub

    Sub loaddiscount()

        Try
            sqldataDiscount.Clear()
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            stracs = "SELECT * FROM Discounts"

            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(sqldataDiscount)

            discountRate.Text = Format(sqldataDiscount.Rows(0)("DiscountPercent") * 100, "00")
            discountLimit.Text = sqldataDiscount.Rows(0)("DiscountLimit")
            penalty.Text = Format(sqldataDiscount.Rows(1)("DiscountPercent") * 100, "00")


        Catch ex As Exception
            MsgBox(ex.Message)
            acsconn.Close()
        End Try

    End Sub

    Private Sub EditToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem1.Click
        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            lblmode.Text = "Update Mode"
            lblmode.ForeColor = Color.Orange
            discountRate.ReadOnly = False
            discountLimit.ReadOnly = False
            penalty.ReadOnly = False

            discountRate.Select()

            lblmode.Show()
        Else
            MsgBox("Your account cannot perform this process.")
        End If


    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            If lblmode.Text = "Update Mode" Then

                Try

                    Integer.Parse(discountRate.Text)
                    Integer.Parse(discountLimit.Text)
                    Integer.Parse(penalty.Text)

                    sqldataDiscount.Clear()
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                    stracs = "update Discounts set DiscountPercent = " & discountRate.Text / 100 & ", DiscountLimit = " & discountLimit.Text & " where DiscountName = 'Senior Citizen'"
                    acscmd.Connection = acsconn
                    acscmd.CommandText = stracs
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                    stracs = "update Discounts set DiscountPercent = " & penalty.Text / 100 & ", DiscountLimit = 0 where DiscountName = 'Penalty'"
                    acscmd.Connection = acsconn
                    acscmd.CommandText = stracs
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                    discountRate.ReadOnly = True
                    discountLimit.ReadOnly = True
                    penalty.ReadOnly = True

                    lblmode.Text = "Mode"
                    lblmode.Hide()

                    loaddiscount()

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Else

            End If
        Else
            MsgBox("Your account cannot perform this process.")
        End If


    End Sub


    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Me.Activate()
    End Sub

    Private Sub settingsdiscount_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Public MoveFormDiscount As Boolean
    Public MoveFormDiscount_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormDiscount = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormDiscount_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormDiscount Then
            Me.Location = Me.Location + (e.Location - MoveFormDiscount_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormDiscount = False
            Me.Cursor = Cursors.Default
        End If

    End Sub
End Class