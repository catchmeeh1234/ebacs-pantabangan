Public Class settingsDataSett

    Private Sub settingsDataSett_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        servername.Text = My.Settings.dbServerIp
        dbID.Text = My.Settings.dbID
        dbpassword.Text = My.Settings.dbPassword

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Application.Exit()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        My.Settings.dbServerIp = servername.Text
        My.Settings.dbID = dbID.Text
        My.Settings.dbPassword = dbpassword.Text
        My.Settings.Save()

    End Sub
End Class