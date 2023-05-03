Public Class TempCR
    Private Sub billsave_Click(sender As Object, e As EventArgs) Handles billsave.Click





        Dim qwe As Integer

        If Integer.TryParse(startofor.Text, qwe) Then

            If Integer.Parse(startofor.Text) > Integer.Parse(endofor.Text) Then



                MsgBox("Beginning of OR Number should not be greater than Ending OR number.")

            Else
                My.Settings.orfrom = startofor.Text
                My.Settings.orto = endofor.Text
                My.Settings.Save()

                MsgBox("Bill number updated.")
            End If
        Else
            MsgBox("Please enter correct OR Number")
        End If



    End Sub

    Private Sub TempCR_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        startofor.Text = My.Settings.orfrom
        endofor.Text = My.Settings.orto

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class