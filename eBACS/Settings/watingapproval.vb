Public Class waitingapproval
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Public trans As String

    Private Sub watingapproval_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        TextBox1.Select()
        TextBox1.Clear()

        'Timer1.Start()

        'Dim loadapprover As New DataTable
        'loadapprover.Clear()
        'stracs = "select * from useraccounts where cservice = 'Yes'"
        'acscmd.CommandText = stracs
        'acscmd.Connection = acsconn
        'acsda.SelectCommand = acscmd
        'acsda.Fill(loadapprover)

        'ComboBox1.Items.Clear()

        'If loadapprover.Rows.Count = 0 Then

        'Else

        '    For c = 0 To loadapprover.Rows.Count - 1
        '        ComboBox1.Items.Add(loadapprover(c)("fullname"))
        '    Next

        'End If


    End Sub
    Dim counter As Integer = 0
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick



        counter = counter + 1

        If counter = 1 Then
            Label2.Text = "."
        End If

        If counter = 5 Then
            Label2.Text = ". ."
        End If

        If counter = 10 Then
            Label2.Text = ". . ."
        End If

        If counter = 15 Then
            Label2.Text = ". . . ."
        End If
        If counter = 20 Then
            Label2.Text = ". . . . ."
        End If

        If counter = 25 Then
            Label2.Text = ""

        End If

        If counter = 30 Then
            counter = 0
        End If

    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown

        If e.KeyValue = Keys.Enter Then

            Dim checkpass As New DataTable
            checkpass.Clear()
            stracs = "select * from updatepassword where approvepassword = '" & TextBox1.Text.Replace("'", "''") & "'"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(checkpass)

            '

            If checkpass.Rows.Count = 0 Then

                MsgBox("Inorrect password.")
                TextBox1.Clear()
                TextBox1.SelectAll()
                TextBox1.Focus()

            Else



                If trans = "Billing" Then



                    TextBox1.Clear()
                    Me.Close()

                    billinginfo.billcurrent.Select()
                    billinginfo.updatemode()

                End If

                If trans = "adjustbillnew" Then

                    TextBox1.Clear()
                    Me.Close()

                    billingAdjustmentBill.createnew()

                End If

                If trans = "adjustbillupdate" Then

                    TextBox1.Clear()
                    Me.Close()

                    billingAdjustmentBill.updatemode()

                End If

                If trans = "adjustpn" Then

                    TextBox1.Clear()
                    Me.Close()

                    billingAdjustmentOther.createnew()

                End If

                If trans = "accountinfo" Then

                    TextBox1.Clear()
                    Me.Close()

                    customerinfo.updateaccount()

                End If

                If trans = "updatestatnew" Then

                    TextBox1.Clear()
                    Me.Close()

                    updatestatus.newstat()

                End If

                If trans = "updatestatedit" Then

                    TextBox1.Clear()
                    Me.Close()

                    updatestatus.updatestat()

                End If

                If trans = "addpen" Then

                    TextBox1.Clear()
                    Me.Close()

                    customerinfo.addpen()

                End If

                If trans = "updatelastreading" Then

                    TextBox1.Clear()
                    Me.Close()

                    'meterreset.accountNo.Text = customerinfo.txtAccountNo.Text
                    meterreset.createnew()

                End If

                If trans = "addnote" Then

                    TextBox1.Clear()
                    Me.Close()

                    notes.accountNo.Text = customerinfo.txtAccountNo.Text
                    notes.remarks.Focus()

                    notes.ShowDialog()

                End If

                If trans = "addchargesnew" Then

                    TextBox1.Clear()
                    Me.Close()

                    customercharges.newcharges()

                End If

                If trans = "addchargesupdate" Then

                    TextBox1.Clear()
                    Me.Close()

                    customercharges.editcharges()

                End If

                If trans = "writeoff" Then

                    TextBox1.Clear()
                    Me.Close()

                    writeoff.createnew()

                End If

            End If

        End If



    End Sub

End Class