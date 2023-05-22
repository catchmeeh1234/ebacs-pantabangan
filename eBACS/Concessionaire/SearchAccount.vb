Public Class SearchAccount
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
    Public searchid As String
    Public searchingform As String
    Sub searchdata(ByVal search As String)
        'stracs = "select * from WaterMeter where ConsName like '" & search & "%' order by ConsName"

        '"CompanyName Like 'A%'"


        DataSet1.Tables(0).DefaultView.Sort = "FullName ASC"

        Dim asd = DataSet1.Tables(0).Select("FullName Like '" & txtSearch.Text.Replace("'", "''") & "%' or CompanyNames Like '" & txtSearch.Text.Replace("'", "''") & "%' or MeterNumber Like '" & txtSearch.Text.Replace("'", "''") & "%' or AccountNo Like '" & txtSearch.Text.Replace("'", "''") & "%'", "FullName ASC").Take(500)

        listSearch.Items.Clear()

        For Each row In asd

            With listSearch.Items.Add(row("AccountNo"))
                .SubItems.Add(row("Fullname"))
                .SubItems.Add(row("CompanyNames"))
                .SubItems.Add(row("Tirahan"))
                .SubItems.Add(row("MeterNumber"))
            End With

        Next


    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged

        'System.Text.RegularExpressions.Regex.Replace(txtSearch.Text, "[^a-zA-Z0-9]", " ")
        'txtSearch.Text = System.Text.RegularExpressions.Regex.Replace(txtSearch.Text, "[^A-Za-z0-9\-/]", "")

        'txtSearch.Text = System.Text.RegularExpressions.Regex.Replace(txtSearch.Text, "(\ s +|\.|\,|\:|\*|&|\?|\/)", "")

        txtSearch.Text = System.Text.RegularExpressions.Regex.Replace(txtSearch.Text, "[^A-Za-z0-9\-Ññ'.,/ ]", "")

        txtSearch.SelectionStart = txtSearch.Text.Length

        If txtSearch.Text = "" Then
            listSearch.Items.Clear()
        Else
            searchdata(txtSearch.Text.Replace("'", "''"))
        End If

    End Sub

    Private Sub SearchAccount_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtSearch.Clear()

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        sqldatasearch.Clear()

        stracs = "SELECT AccountNo, Lastname, Firstname, Middlename, ServiceAddress, CompanyName, MeterNo FROM Customers"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        'acsdr = acscmd.ExecuteReader
        acsda.SelectCommand = acscmd
        acsda.Fill(sqldatasearch)

        DataSet1.Tables(0).Rows.Clear()

        For o = 0 To sqldatasearch.Rows.Count - 1

            Dim companyname, meternumber, midname, ServiceAddress As String

            If IsDBNull(sqldatasearch.Rows(o)("Middlename")) = True Then
                midname = ""
            Else
                midname = sqldatasearch.Rows(o)("Middlename")
            End If

            If IsDBNull(sqldatasearch.Rows(o)("CompanyName")) = True Then
                companyname = ""
            Else
                companyname = sqldatasearch.Rows(o)("CompanyName")
            End If

            If IsDBNull(sqldatasearch.Rows(o)("MeterNo")) = True Then
                meternumber = ""
            Else
                meternumber = sqldatasearch.Rows(o)("MeterNo")
            End If

            If IsDBNull(sqldatasearch.Rows(o)("ServiceAddress")) = True Then
                ServiceAddress = ""
            Else
                ServiceAddress = sqldatasearch.Rows(o)("ServiceAddress")
            End If

            DataSet1.Tables(0).Rows.Add(sqldatasearch.Rows(o)("AccountNo"), sqldatasearch.Rows(o)("LastName") & " " & sqldatasearch.Rows(o)("FirstName") & " " & sqldatasearch.Rows(o)("MiddleName"), companyname, meternumber, ServiceAddress)

        Next

        txtSearch.Select()

    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown

        If e.KeyCode = Keys.Down Then

            If listSearch.Items.Count = 0 Then

            Else
                listSearch.Select()
                listSearch.FocusedItem = listSearch.Items(0)
                listSearch.Items(0).Selected = True
            End If



        End If

            If e.KeyCode = Keys.Enter Then

            'listSearch.Select()
            'listSearch.FocusedItem = listSearch.Items(0)
            'listSearch.Items(0).Selected = True

            If searchingform = "assocaccounts" Then

                If listSearch.Items.Count = 0 Then

                    MsgBox("No record.")

                Else

                    Dim asd As Integer = MsgBox("Are you sure you want to add this account?", vbQuestion + vbYesNo + vbDefaultButton2, "Confirm")

                    If asd = vbYes Then

                        AssociatedAccounts.addaccountno = listSearch.Items(0).SubItems(0).Text
                        AssociatedAccounts.Addassocaccounts()

                    Else

                    End If


                    Me.Close()

                End If

            End If

            If searchingform = "Accounts" Then

                If listSearch.Items.Count = 0 Then

                    MsgBox("No record.")

                Else

                    customerinfo.lblCreateUpdate.Text = "Mode"
                    customerinfo.Visible = True
                    customerinfo.clearfields()
                    customerinfo.txtAccountNo.Text = listSearch.Items(0).SubItems(0).Text
                    customerinfo.txtAccountNo.Select()
                    customerinfo.Activate()

                    customerinfo.txtAccountNo_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))

                    Me.Close()

                End If

            End If

            If searchingform = "collection" Then

                If listSearch.Items.Count = 0 Then

                    MsgBox("No record.")

                Else


                    Collection_CR.clearallfields()
                    Collection_CR.billAccountNo.Text = listSearch.Items(0).SubItems(0).Text
                    Collection_CR.billAccountNo.Select()


                    Collection_CR.billAccountNo_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))

                    Me.Close()

                End If

            End If

            If searchingform = "create or" Then

                If listSearch.Items.Count = 0 Then

                    MsgBox("No record.")

                Else


                    Create_OR.clearallfields()
                    Create_OR.AccountNo.Text = listSearch.Items(0).SubItems(0).Text
                    Create_OR.AccountNo.Select()


                    Create_OR.loadinfo()

                    Me.Close()

                End If

            End If

            If searchingform = "Bills" Then

                If listSearch.Items.Count = 0 Then

                    MsgBox("No record.")

                Else


                    If searchid = "searchbill" Then


                        billinginfo.accountno.Text = listSearch.Items(0).SubItems(0).Text
                        billinginfo.accountno_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))

                        'If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                        'sqldatasearch.Clear()

                        'stracs = "select * from Bills where AccountNumber = '" & listSearch.Items(0).SubItems(0).Text & "' order by BillNo desc"
        'acscmd.CommandText = stracs
        'acscmd.Connection = acsconn
        ''acsdr = acscmd.ExecuteReader
        'acsda.SelectCommand = acscmd
        'acsda.Fill(sqldatasearch)

        'billinginfo.billList.Rows.Clear()

        'For i = 0 To sqldatasearch.Rows.Count - 1

        '    If sqldatasearch(i)("isPaid") = "Yes" Then
        '        billinginfo.billList.Rows.Add(sqldatasearch(i)("BillNo"), Format(sqldatasearch(i)("ReadingDate"), "short date"), sqldatasearch(i)("CustomerName"), sqldatasearch(i)("Consumption"), "Paid")
        '    Else
        '        If sqldatasearch(i)("isPromisorry") = "YesPosted" Then
        '            billinginfo.billList.Rows.Add(sqldatasearch(i)("BillNo"), Format(sqldatasearch(i)("ReadingDate"), "short date"), sqldatasearch(i)("CustomerName"), sqldatasearch(i)("Consumption"), "PN")
        '        Else

        '            billinginfo.billList.Rows.Add(sqldatasearch(i)("BillNo"), Format(sqldatasearch(i)("ReadingDate"), "short date"), sqldatasearch(i)("CustomerName"), sqldatasearch(i)("Consumption"), sqldatasearch(i)("BillStatus"))
        '        End If

        '    End If

        '    'MainForm.billingdetails.billList.rows.add(sqldatasearch(i)("BillNo"), Format(sqldatasearch(i)("ReadingDate"), "MM/dd/yyyy"), sqldatasearch(i)("CustomerName"), sqldatasearch(i)("Consumption"), sqldatasearch(i)("BillStatus"))

        'Next

        Me.Close()

                    End If

                    If searchid = "newbill" Then

                        billinginfo.createnew()
                        billinginfo.billAccountNo.Text = listSearch.Items(0).SubItems(0).Text
                        billinginfo.billAccountNo_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))

                        Me.Close()

                    End If

                End If

            End If

            If searchingform = "BillAdjustment" Then

                billingAdjustmentBill.adjustAccountNo.Text = listSearch.Items(0).SubItems(0).Text
                billingAdjustmentBill.adjustAccountNo_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))
                Me.Close()

            End If

            If searchingform = "OtherAdjustment" Then

                billingAdjustmentOther.adjustAccountNo.Text = listSearch.Items(0).SubItems(0).Text
                billingAdjustmentOther.adjustAccountNo_KeyUp(Nothing, New KeyEventArgs(Keys.Enter))
                Me.Close()

            End If



        End If

    End Sub

    Private Sub listSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles listSearch.KeyDown

        If listSearch.Items(0).Selected = True Then


            If e.KeyCode = Keys.Up Then

                txtSearch.Select()
                txtSearch.SelectAll()

            End If

        End If

        If e.KeyCode = Keys.Enter Then

            If searchingform = "assocaccounts" Then

                If listSearch.Items.Count = 0 Then

                    MsgBox("No record.")

                Else

                    Dim asd As Integer = MsgBox("Are you sure you want to add this account?", vbQuestion + vbYesNo + vbDefaultButton2, "Confirm")

                    If asd = vbYes Then

                        AssociatedAccounts.addaccountno = listSearch.SelectedItems(0).SubItems(0).Text
                        AssociatedAccounts.Addassocaccounts()

                    Else

                    End If


                    Me.Close()

                End If

            End If

            If searchingform = "Accounts" Then

                If listSearch.Items.Count = 0 Then

                    MsgBox("No record.")

                Else

                    customerinfo.lblCreateUpdate.Text = "Mode"
                    customerinfo.Visible = True
                    customerinfo.clearfields()
                    customerinfo.txtAccountNo.Text = listSearch.SelectedItems(0).SubItems(0).Text
                    customerinfo.txtAccountNo.Select()
                    customerinfo.Activate()

                    customerinfo.txtAccountNo_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))

                    Me.Close()

                End If

            End If

            If searchingform = "collection" Then



                If listSearch.Items.Count = 0 Then

                    MsgBox("No record.")

                Else


                    Collection_CR.clearallfields()
                    Collection_CR.billAccountNo.Text = listSearch.SelectedItems(0).SubItems(0).Text
                    Collection_CR.billAccountNo.Select()


                    Collection_CR.billAccountNo_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))

                    Me.Close()

                End If

            End If

            If searchingform = "create or" Then

                If listSearch.Items.Count = 0 Then

                    MsgBox("No record.")

                Else


                    Create_OR.clearallfields()
                    Create_OR.AccountNo.Text = listSearch.SelectedItems(0).SubItems(0).Text
                    Create_OR.AccountNo.Select()


                    Create_OR.loadinfo()

                    Me.Close()

                End If

            End If

            If searchingform = "Bills" Then

                If listSearch.Items.Count = 0 Then

                    MsgBox("No record.")

                Else

                    If billinginfo.lblMode.Text = "Mode" Then

                        billinginfo.accountno.Text = listSearch.SelectedItems(0).SubItems(0).Text
                        billinginfo.accountno_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))

                        'If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                        'sqldatasearch.Clear()

                        'stracs = "select * from Bills where AccountNumber = '" & listSearch.SelectedItems(0).SubItems(0).Text & "' order by BILLID desc"
                        'acscmd.CommandText = stracs
                        'acscmd.Connection = acsconn
                        ''acsdr = acscmd.ExecuteReader
                        'acsda.SelectCommand = acscmd
                        'acsda.Fill(sqldatasearch)

                        'billinginfo.billList.Rows.Clear()

                        'For i = 0 To sqldatasearch.Rows.Count - 1
                        '    If sqldatasearch(i)("isPaid") = "Yes" Then
                        '        billinginfo.billList.Rows.Add(sqldatasearch(i)("BillNo"), Format(sqldatasearch(i)("ReadingDate"), "short date"), sqldatasearch(i)("CustomerName"), sqldatasearch(i)("Consumption"), "Paid")
                        '    Else
                        '        If sqldatasearch(i)("isPromisorry") = "YesPosted" Then
                        '            billinginfo.billList.Rows.Add(sqldatasearch(i)("BillNo"), Format(sqldatasearch(i)("ReadingDate"), "short date"), sqldatasearch(i)("CustomerName"), sqldatasearch(i)("Consumption"), "PN")
                        '        Else

                        '            billinginfo.billList.Rows.Add(sqldatasearch(i)("BillNo"), Format(sqldatasearch(i)("ReadingDate"), "short date"), sqldatasearch(i)("CustomerName"), sqldatasearch(i)("Consumption"), sqldatasearch(i)("BillStatus"))
                        '        End If

                        '    End If
                        'Next

                        Me.Close()

                    End If

                    If billinginfo.lblMode.Text = "Create New Bill" Then


                        billinginfo.createnew()
                        billinginfo.billAccountNo.Text = listSearch.SelectedItems(0).SubItems(0).Text
                        billinginfo.billAccountNo_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))

                        Me.Close()
                    End If

                End If

            End If

            If searchingform = "BillAdjustment" Then

                billingAdjustmentBill.adjustAccountNo.Text = listSearch.SelectedItems(0).SubItems(0).Text
                billingAdjustmentBill.adjustAccountNo_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))
                Me.Close()

            End If

            If searchingform = "OtherAdjustment" Then

                billingAdjustmentOther.adjustAccountNo.Text = listSearch.SelectedItems(0).SubItems(0).Text
                billingAdjustmentOther.adjustAccountNo_KeyUp(Nothing, New KeyEventArgs(Keys.Enter))
                Me.Close()

            End If

        End If

    End Sub

End Class