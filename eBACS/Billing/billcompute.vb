Public Class billcompute

    Private Sub billcompute_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain

        loadrates()

    End Sub

    Sub compute()

        sqldataSenior.Clear()
        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        stracs = "select DiscountPercent, DiscountLimit from Discounts where DiscountName = 'Senior Citizen'"
        acscmd.Connection = acsconn
        acscmd.CommandText = stracs
        acsda.SelectCommand = acscmd
        acsda.Fill(sqldataSenior)

        Dim intValue As Integer

        If Integer.TryParse(billconsumption.Text, intValue) AndAlso intValue >= 0 Then

            If billconsumption.Text < 11 Then

                For y = 0 To conslist.Rows.Count - 1

                    conslist.Rows(y).Cells(2).Value = Format(conslist.Rows(y).Cells(6).Value, "standard")

                    If issenior.CheckState = CheckState.Checked Then

                        If conslist.Rows(y).Cells(0).Value = "Residential" Then
                            conslist.Rows(y).Cells(4).Value = Format(conslist.Rows(y).Cells(2).Value * sqldataSenior(0)("DiscountPercent"), "standard")
                        Else
                            conslist.Rows(y).Cells(4).Value = "0.00"
                        End If
                    Else

                        conslist.Rows(y).Cells(4).Value = "0.00"

                    End If

                    If IsNumeric(otherdisc.Text) = True Then
                        conslist.Rows(y).Cells(4).Value = Format(Double.Parse(conslist.Rows(y).Cells(4).Value) + otherdisc.Text, "standard")
                    Else

                    End If

                    If IsNumeric(fees.Text) = True Then
                        conslist.Rows(y).Cells(3).Value = Format(fees.Text, "standard")
                    Else
                        conslist.Rows(y).Cells(3).Value = ("0.00")
                    End If

                    conslist.Rows(y).Cells(5).Value = Format((Double.Parse(conslist.Rows(y).Cells(2).Value) + Double.Parse(conslist.Rows(y).Cells(3).Value)) - Double.Parse(conslist.Rows(y).Cells(4).Value), "standard")

                Next
            Else

                If billconsumption.Text > 10 And billconsumption.Text < 21 Then

                    For y = 0 To conslist.Rows.Count - 1

                        conslist.Rows(y).Cells(2).Value = Format(conslist.Rows(y).Cells(6).Value + (conslist.Rows(y).Cells(7).Value * (billconsumption.Text - 10)), "standard")

                        If issenior.CheckState = CheckState.Checked Then

                            If conslist.Rows(y).Cells(0).Value = "Residential" Then
                                conslist.Rows(y).Cells(4).Value = Format(conslist.Rows(y).Cells(2).Value * sqldataSenior(0)("DiscountPercent"), "standard")
                            Else

                                conslist.Rows(y).Cells(4).Value = "0.00"
                            End If

                        Else

                            conslist.Rows(y).Cells(4).Value = "0.00"

                        End If

                        If IsNumeric(otherdisc.Text) = True Then
                            conslist.Rows(y).Cells(4).Value = Format(Double.Parse(conslist.Rows(y).Cells(4).Value) + otherdisc.Text, "standard")
                        Else

                        End If

                        If IsNumeric(fees.Text) = True Then
                            conslist.Rows(y).Cells(3).Value = Format(fees.Text, "standard")
                        Else
                            conslist.Rows(y).Cells(3).Value = ("0.00")
                        End If

                        conslist.Rows(y).Cells(5).Value = Format((Double.Parse(conslist.Rows(y).Cells(2).Value) + Double.Parse(conslist.Rows(y).Cells(3).Value)) - Double.Parse(conslist.Rows(y).Cells(4).Value), "standard")

                    Next

                End If

                If billconsumption.Text > 20 And billconsumption.Text < 31 Then

                    For y = 0 To conslist.Rows.Count - 1

                        conslist.Rows(y).Cells(2).Value = Format(conslist.Rows(y).Cells(6).Value + (conslist.Rows(y).Cells(7).Value * 10) + (conslist.Rows(y).Cells(8).Value * (billconsumption.Text - 20)), "standard")

                        If issenior.CheckState = CheckState.Checked Then

                            If conslist.Rows(y).Cells(0).Value = "Residential" Then
                                conslist.Rows(y).Cells(4).Value = Format(conslist.Rows(y).Cells(2).Value * sqldataSenior(0)("DiscountPercent"), "standard")
                            Else
                                conslist.Rows(y).Cells(4).Value = "0.00"
                            End If
                        Else

                            conslist.Rows(y).Cells(4).Value = "0.00"


                        End If

                        If IsNumeric(otherdisc.Text) = True Then
                            conslist.Rows(y).Cells(4).Value = Format(Double.Parse(conslist.Rows(y).Cells(4).Value) + otherdisc.Text, "standard")
                        Else

                        End If

                        If IsNumeric(fees.Text) = True Then
                            conslist.Rows(y).Cells(3).Value = Format(fees.Text, "standard")
                        Else
                            conslist.Rows(y).Cells(3).Value = ("0.00")
                        End If

                        conslist.Rows(y).Cells(5).Value = Format((Double.Parse(conslist.Rows(y).Cells(2).Value) + Double.Parse(conslist.Rows(y).Cells(3).Value)) - Double.Parse(conslist.Rows(y).Cells(4).Value), "standard")

                    Next

                End If

                If billconsumption.Text > 30 And billconsumption.Text < 41 Then

                    For y = 0 To conslist.Rows.Count - 1

                        conslist.Rows(y).Cells(2).Value = Format(conslist.Rows(y).Cells(6).Value + (conslist.Rows(y).Cells(7).Value * 10) + (conslist.Rows(y).Cells(8).Value * 10) + (conslist.Rows(y).Cells(9).Value * (billconsumption.Text - 30)), "standard")
                        conslist.Rows(y).Cells(4).Value = "0.00"

                        If IsNumeric(otherdisc.Text) = True Then
                            conslist.Rows(y).Cells(4).Value = Format(otherdisc.Text, "standard")
                        Else

                        End If

                        If IsNumeric(fees.Text) = True Then
                            conslist.Rows(y).Cells(3).Value = Format(fees.Text, "standard")
                        Else
                            conslist.Rows(y).Cells(3).Value = ("0.00")
                        End If

                        conslist.Rows(y).Cells(5).Value = Format((Double.Parse(conslist.Rows(y).Cells(2).Value) + Double.Parse(conslist.Rows(y).Cells(3).Value)) - Double.Parse(conslist.Rows(y).Cells(4).Value), "standard")

                    Next



                End If

                If billconsumption.Text > 40 And billconsumption.Text < 51 Then

                    For y = 0 To conslist.Rows.Count - 1

                        conslist.Rows(y).Cells(2).Value = Format(conslist.Rows(y).Cells(6).Value + (conslist.Rows(y).Cells(7).Value * 10) + (conslist.Rows(y).Cells(8).Value * 10) + (conslist.Rows(y).Cells(9).Value * 10) + (conslist.Rows(y).Cells(10).Value * (billconsumption.Text - 40)), "standard")
                        conslist.Rows(y).Cells(4).Value = "0.00"

                        If IsNumeric(otherdisc.Text) = True Then
                            conslist.Rows(y).Cells(4).Value = Format(otherdisc.Text, "standard")
                        Else

                        End If

                        If IsNumeric(fees.Text) = True Then
                            conslist.Rows(y).Cells(3).Value = Format(fees.Text, "standard")
                        Else
                            conslist.Rows(y).Cells(3).Value = ("0.00")
                        End If

                        conslist.Rows(y).Cells(5).Value = Format((Double.Parse(conslist.Rows(y).Cells(2).Value) + Double.Parse(conslist.Rows(y).Cells(3).Value)) - Double.Parse(conslist.Rows(y).Cells(4).Value), "standard")

                    Next

                End If

                If billconsumption.Text > 50 Then

                    For y = 0 To conslist.Rows.Count - 1

                        conslist.Rows(y).Cells(2).Value = Format(conslist.Rows(y).Cells(6).Value + (conslist.Rows(y).Cells(7).Value * 10) + (conslist.Rows(y).Cells(8).Value * 10) + (conslist.Rows(y).Cells(9).Value * 10) + (conslist.Rows(y).Cells(10).Value * 10) + (conslist.Rows(y).Cells(11).Value * (billconsumption.Text - 50)), "standard")
                        conslist.Rows(y).Cells(4).Value = "0.00"

                        If IsNumeric(otherdisc.Text) = True Then
                            conslist.Rows(y).Cells(4).Value = Format(otherdisc.Text, "standard")
                        Else
                            conslist.Rows(y).Cells(3).Value = ("0.00")
                        End If

                        If IsNumeric(fees.Text) = True Then
                            conslist.Rows(y).Cells(3).Value = Format(fees.Text, "standard")
                        Else
                            conslist.Rows(y).Cells(3).Value = ("0.00")
                        End If

                        conslist.Rows(y).Cells(5).Value = Format((Double.Parse(conslist.Rows(y).Cells(2).Value) + Double.Parse(conslist.Rows(y).Cells(3).Value)) - Double.Parse(conslist.Rows(y).Cells(4).Value), "standard")

                    Next

                End If


            End If

        End If

    End Sub

    Private Sub billconsumption_KeyDown(sender As Object, e As KeyEventArgs) Handles billconsumption.KeyDown

        If e.KeyValue = Keys.Enter Then

            compute()

        End If

    End Sub

    Sub loadrates()

        Dim ratesdata As New DataTable

        stracs = "select * from RateSchedules order by CustomerType desc, MinimumCharge asc"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(ratesdata)

        For o = 0 To ratesdata.Rows.Count - 1

            conslist.Rows.Add(ratesdata(o)("CustomerType"), ratesdata(o)("MeterSize"), "0.00", "0.00", "0.00", "0.00",
                              ratesdata(o)("MinimumCharge"), ratesdata(o)("twenty"), ratesdata(o)("thirty"),
                              ratesdata(o)("forty"), ratesdata(o)("fifty"), ratesdata(o)("maxx"))

        Next

    End Sub

    Private Sub issenior_CheckedChanged(sender As Object, e As EventArgs) Handles issenior.CheckedChanged

        If issenior.CheckState = CheckState.Checked Then

            compute()
        Else

            compute()

        End If

    End Sub

    Private Sub otherdisc_KeyDown(sender As Object, e As KeyEventArgs) Handles otherdisc.KeyDown

        If e.KeyValue = Keys.Enter Then

            compute()

        End If

    End Sub

    Private Sub fees_KeyDown(sender As Object, e As KeyEventArgs) Handles fees.KeyDown

        If e.KeyValue = Keys.Enter Then
            compute()
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Me.Close()

    End Sub

    Public MoveComputebill As Boolean
    Public MoveComputebill_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveComputebill = True
            Me.Cursor = Cursors.NoMove2D
            MoveComputebill_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveComputebill Then
            Me.Location = Me.Location + (e.Location - MoveComputebill_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveComputebill = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub billinginfo_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.BringToFront()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Me.Activate()
    End Sub

    Private Sub billinginfo_deactivated(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.BackColor = Color.FromArgb(17, 153, 195)
    End Sub

    Private Sub ControlsClick(sender As Object, e As EventArgs) Handles _
        Panel1.Click, conslist.Click, issenior.Click, Label1.Click, Label2.Click, Label3.Click, billconsumption.Click,
        otherdisc.Click, fees.Click  ' etc.
        Me.Activate() 'Or Whatever
    End Sub

    Private Sub billinginfo_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.BackColor = Color.SteelBlue
    End Sub

End Class