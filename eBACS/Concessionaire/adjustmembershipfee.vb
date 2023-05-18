Public Class adjustmembershipfee
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Sub LoadMembershipAdjustment(ByVal acc_no As String)
        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        Dim loadrecords As New DataTable
        stracs = "select AccountNo, Membership_balance from Customers where AccountNo = '" & acc_no & "'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(loadrecords)

        txtAccountNo.Text = acc_no
        adjustFrom.Text = loadrecords(0)("Membership_balance")
        adjustTo.Text = loadrecords(0)("Membership_balance")
        remarks.Text = ""

        Dim loadMembershipFeeTable As New DataTable
        stracs = "select * from MembershipFeeAdjustment where adjustAccountNo = '" & acc_no & "'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(loadMembershipFeeTable)

        records.Rows.Clear()

        If loadMembershipFeeTable.Rows.Count = 0 Then
        Else

            For t = 0 To loadMembershipFeeTable.Rows.Count - 1

                records.Rows.Add(loadMembershipFeeTable.Rows(t)("id"), Format(loadMembershipFeeTable.Rows(t)("adjustDate"), "short date"), loadMembershipFeeTable.Rows(t)("adjustFrom"), loadMembershipFeeTable.Rows(t)("adjustTo"), loadMembershipFeeTable.Rows(t)("adjustRemarks"), loadMembershipFeeTable.Rows(t)("adjustBy"))

            Next

        End If
    End Sub

    Sub SaveMembershipAdjustment(ByVal membershipfee_balance As Decimal, ByVal account_no As String, ByVal adjustFrom As String, ByVal adjustTo As String, ByVal adjustRemarks As String)
        If adjustFrom = "" Then
            Exit Sub
        End If

        Dim currentDate As String = DateTime.Now.ToString("yyyy-MM-dd")

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try


        stracs = "update Customers set Membership_balance = " & Decimal.Round(membershipfee_balance, 2) & " where AccountNo = '" & account_no.Replace("'", "''") & "'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acscmd.ExecuteNonQuery()
        acscmd.Dispose()

        stracs = "insert into MembershipFeeAdjustment ([adjustAccountNo],[adjustDate],[adjustFrom] ,[adjustTo],[adjustRemarks],[adjustBy]) values 
                          ('" & account_no & "', '" & currentDate & "', " & adjustFrom & ", " & adjustTo & ", '" _
                                  & adjustRemarks & "', '" & My.Settings.Nickname & "')"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acscmd.ExecuteNonQuery()
        acscmd.Dispose()

        MsgBox("Membership balance adjusted")

    End Sub

    Private Sub UnlockFields()
        txtAccountNo.ReadOnly = False
        adjustFrom.ReadOnly = False
        adjustTo.ReadOnly = False
        remarks.ReadOnly = False
    End Sub

    Private Sub LockFields()
        txtAccountNo.ReadOnly = True
        adjustFrom.ReadOnly = True
        adjustTo.ReadOnly = True
        remarks.ReadOnly = True
    End Sub

    Private Sub ClearFields()
        txtAccountNo.Clear()
        adjustFrom.Clear()
        adjustTo.Clear()
        remarks.Clear()
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        LockFields()
        lblmode.Text = ""
        lblmode.Visible = False
        Dim accno As String = txtAccountNo.Text
        SaveMembershipAdjustment(adjustTo.Text, txtAccountNo.Text, adjustFrom.Text, adjustTo.Text, remarks.Text)
        LoadMembershipAdjustment(accno)
    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        lblmode.Text = "Adust Membership Fee"
        lblmode.Visible = True
        UnlockFields()
    End Sub

    Private Sub adjustmembershipfee_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockFields()
        lblmode.Text = ""
        lblmode.Visible = False
    End Sub

    Private Sub CancelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CancelToolStripMenuItem.Click
        LockFields()
        lblmode.Text = ""
        lblmode.Visible = False
    End Sub
End Class