Public Class Calculator
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()

    End Sub

    Public Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        dgvcalc.Rows.Clear()
        itemcount.Text = 0
        totalamount.Text = "0.00"
        tendered.Text = "0.00"
        sukli.Text = "0.00"
    End Sub

    Public Sub calculate()
        If dgvcalc.Rows.Count = 0 Then
            totalamount.Text = "0.00"
            itemcount.Text = 0
        Else

            Dim totalll As Double

            totalll = 0

            For u = 0 To dgvcalc.Rows.Count - 1

                If IsNumeric(dgvcalc.Rows(u).Cells(2).Value) = True Then
                    totalll = totalll + Double.Parse(dgvcalc.Rows(u).Cells(2).Value)
                Else

                End If

            Next

            totalamount.Text = FormatNumber(totalll)
            itemcount.Text = dgvcalc.Rows.Count - 1

        End If
    End Sub



    ''' <summary>
    ''' move form without border
    ''' </summary>

    Public MoveForm As Boolean
    Public MoveForm_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveForm = True
            Me.Cursor = Cursors.NoMove2D
            MoveForm_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveForm Then
            Me.Location = Me.Location + (e.Location - MoveForm_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
        End If

    End Sub
    '''''''''''''''''''''''''''''''''''
    Private Sub dgvcalc_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvcalc.CellValueChanged
        calculate()
    End Sub

    Private Sub dgvcalc_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvcalc.KeyDown
        If e.KeyCode = Keys.Delete Then
            If dgvcalc.Rows.Count = 0 Then

            Else

                Try
                    dgvcalc.Rows.Remove(dgvcalc.CurrentRow)
                Catch ex As Exception

                End Try

                calculate()
            End If

        End If

    End Sub

    Private Sub Calculator_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Size = New Size(352, 573)

        totalamount.Text = "0.00"
        tendered.Text = "0.00"

        Label7.Select()
        Me.MdiParent = eBACSmain
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        If dgvcalc.Rows.Count = 0 Then

        Else
            For h = 0 To dgvcalc.Rows.Count - 1
                If IsNumeric(dgvcalc.Rows(h).Cells(2).Value) = True Then
                    dgvcalc.Rows(h).Cells(2).Style.ForeColor = Color.Black
                Else
                    dgvcalc.Rows(h).Cells(2).Style.ForeColor = Color.Red
                End If
            Next

        End If


    End Sub

    Private Sub Calculator_CausesValidationChanged(sender As Object, e As EventArgs) Handles Me.CausesValidationChanged
        Me.Activate()
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Me.Activate()
    End Sub

    Private Sub tendered_TextChanged(sender As Object, e As EventArgs) Handles tendered.TextChanged

        If IsNumeric(tendered.Text) = True Then

            tendered.ForeColor = Color.FromArgb(17, 153, 195)
            sukli.Text = Format(Double.Parse(tendered.Text) - Double.Parse(totalamount.Text), "standard")
        Else
            tendered.ForeColor = Color.Red
            sukli.Text = 0.00
        End If

    End Sub



    Private Sub calc_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.BackColor = Color.SteelBlue
    End Sub

    Private Sub calc_Deactivated(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.BackColor = Color.FromArgb(17, 153, 195)
    End Sub

    Private Sub ControlsClick(sender As Object, e As EventArgs) Handles _
        Label1.Click, Panel1.Click, dgvcalc.Click, totalamount.Click, tendered.Click, sukli.Click, Me.Click ' etc.
        Me.Activate() 'Or Whatever
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

        'cashcheck.Location = New Point(8, 572)
        'paymentgroup.Location = New Point(214, 572)

        Me.Size = New Size(553, 573)
        Button1.Location = New Point(465, 6)

    End Sub

    Private Sub Label18_Click(sender As Object, e As EventArgs) Handles Label18.Click

        Me.Size = New Size(352, 573)
        Button1.Location = New Point(267, 6)

        Dim tutal As Double = 0

        tutal = tendered.Text

        den1000.Clear()
        den500.Clear()
        den200.Clear()
        den100.Clear()
        den50.Clear()
        den20.Clear()
        den10.Clear()
        den5.Clear()
        den1.Clear()

        tendered.Text = Format(tutal, "Standard")
        tendered.Select()

    End Sub

    Public Sub computedenom()


        tendered.Text = Format(libo + limadaan + duwadaan + isadaan + pipti + binti + sampu + lima + piso, "standard")


    End Sub

    Dim libo As Integer = 0
    Dim limadaan As Integer = 0
    Dim duwadaan As Integer = 0
    Dim isadaan As Integer = 0
    Dim pipti As Integer = 0
    Dim binti As Integer = 0
    Dim sampu As Integer = 0
    Dim lima As Integer = 0
    Dim piso As Integer = 0

    Private Sub den1000_TextChanged(sender As Object, e As EventArgs) Handles den1000.TextChanged

        den1000.Text = System.Text.RegularExpressions.Regex.Replace(den1000.Text, "[^0-9]", "")
        den1000.SelectionStart = den1000.Text.Length

        If IsNumeric(den1000.Text) = True Then
            libo = den1000.Text * 1000
        Else
            libo = 0
        End If

        computedenom()

    End Sub

    Private Sub den500_TextChanged(sender As Object, e As EventArgs) Handles den500.TextChanged
        den500.Text = System.Text.RegularExpressions.Regex.Replace(den500.Text, "[^0-9]", "")
        den500.SelectionStart = den500.Text.Length

        If IsNumeric(den500.Text) = True Then
            limadaan = den500.Text * 500
        Else
            limadaan = 0
        End If

        computedenom()
    End Sub

    Private Sub den200_TextChanged(sender As Object, e As EventArgs) Handles den200.TextChanged
        den200.Text = System.Text.RegularExpressions.Regex.Replace(den200.Text, "[^0-9]", "")
        den200.SelectionStart = den200.Text.Length

        If IsNumeric(den200.Text) = True Then
            duwadaan = den200.Text * 200
        Else
            duwadaan = 0
        End If

        computedenom()
    End Sub

    Private Sub den100_TextChanged(sender As Object, e As EventArgs) Handles den100.TextChanged
        den100.Text = System.Text.RegularExpressions.Regex.Replace(den100.Text, "[^0-9]", "")
        den100.SelectionStart = den100.Text.Length

        If IsNumeric(den100.Text) = True Then
            isadaan = den100.Text * 100
        Else
            isadaan = 0
        End If

        computedenom()
    End Sub

    Private Sub den50_TextChanged(sender As Object, e As EventArgs) Handles den50.TextChanged
        den50.Text = System.Text.RegularExpressions.Regex.Replace(den50.Text, "[^0-9]", "")
        den50.SelectionStart = den50.Text.Length

        If IsNumeric(den50.Text) = True Then
            pipti = den50.Text * 50
        Else
            pipti = 0
        End If

        computedenom()
    End Sub

    Private Sub den20_TextChanged(sender As Object, e As EventArgs) Handles den20.TextChanged
        den20.Text = System.Text.RegularExpressions.Regex.Replace(den20.Text, "[^0-9]", "")
        den20.SelectionStart = den20.Text.Length

        If IsNumeric(den20.Text) = True Then
            binti = den20.Text * 20
        Else
            binti = 0
        End If

        computedenom()
    End Sub

    Private Sub den10_TextChanged(sender As Object, e As EventArgs) Handles den10.TextChanged
        den10.Text = System.Text.RegularExpressions.Regex.Replace(den10.Text, "[^0-9]", "")
        den10.SelectionStart = den10.Text.Length

        If IsNumeric(den10.Text) = True Then
            sampu = den10.Text * 10
        Else
            sampu = 0
        End If

        computedenom()
    End Sub

    Private Sub den5_TextChanged(sender As Object, e As EventArgs) Handles den5.TextChanged
        den5.Text = System.Text.RegularExpressions.Regex.Replace(den5.Text, "[^0-9]", "")
        den5.SelectionStart = den5.Text.Length

        If IsNumeric(den5.Text) = True Then
            lima = den5.Text * 5
        Else
            lima = 0
        End If

        computedenom()
    End Sub

    Private Sub den1_TextChanged(sender As Object, e As EventArgs) Handles den1.TextChanged
        den1.Text = System.Text.RegularExpressions.Regex.Replace(den1.Text, "[^0-9]", "")
        den1.SelectionStart = den1.Text.Length

        If IsNumeric(den1.Text) = True Then
            piso = den1.Text
        Else
            piso = 0
        End If

        computedenom()
    End Sub
End Class