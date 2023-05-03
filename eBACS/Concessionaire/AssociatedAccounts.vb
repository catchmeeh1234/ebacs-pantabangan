Public Class AssociatedAccounts

    Public associd As Integer
    Public addaccountno As String
    Public addfullname As String
    Public mainfullname As String

    Private Sub AssociatedAccounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        listofassocacc.ContextMenuStrip = removeacc

        listofassocacc.Rows.Clear()
        assocrecords.Rows.Clear()
        loadassocaccounts()

    End Sub

    Public Sub loadassocaccounts()


        Dim getassocid As New DataTable

        stracs = "select AssocID from Customers where AccountNo = '" & customerinfo.txtAccountNo.Text & "'"
        acscmd.Connection = acsconn
        acscmd.CommandText = stracs
        acsda.SelectCommand = acscmd
        acsda.Fill(getassocid)

        If IsDBNull(getassocid(0)("AssocID")) = True Then

            associd = 0

        Else

            Dim loadassocaccounts As New DataTable
            stracs = "select * from Customers where AssocID = " & associd
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(loadassocaccounts)

            listofassocacc.Rows.Clear()

            If loadassocaccounts.Rows.Count = 0 Then
            Else

                For x = 0 To loadassocaccounts.Rows.Count - 1

                    If IsDBNull(loadassocaccounts(x)("CompanyName")) = True Or loadassocaccounts(x)("CompanyName") = "" Then

                        listofassocacc.Rows.Add(loadassocaccounts(x)("AccountNo"), loadassocaccounts(x)("Firstname").ToString.ToUpper & " " & loadassocaccounts(x)("Middlename").ToString.ToUpper & " " & loadassocaccounts(x)("Lastname").ToString.ToUpper, loadassocaccounts(x)("AssocID"))
                    Else
                        listofassocacc.Rows.Add(loadassocaccounts(x)("AccountNo"), loadassocaccounts(x)("CompanyName").ToString.ToUpper, loadassocaccounts(x)("AssocID"))
                    End If

                    associd = loadassocaccounts(x)("AssocID")

                Next

            End If

            Dim getrecordsdata As New DataTable

            stracs = "select * from Assocaccount where AssocID = " & associd & " order by id"
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(getrecordsdata)

            If getrecordsdata.Rows.Count = 0 Then
            Else
                'assocrecords
                assocrecords.Rows.Clear()

                For x = 0 To getrecordsdata.Rows.Count - 1


                    assocrecords.Rows.Add(getrecordsdata(x)("accountno"), getrecordsdata(x)("accountname"), getrecordsdata(x)("status"), Format(getrecordsdata(x)("dateupdated"), "short date"), getrecordsdata(x)("updatedby"))


                Next

            End If

        End If



    End Sub

    Public Sub Addassocaccounts()

        If associd = 0 Then

            Dim getassocno As New DataTable
            stracs = "select number from tbllogicnumbers where id = 15"
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(getassocno)

            stracs = "update Customers set AssocID = " & getassocno(0)("Number") & " where AccountNo in ('" & customerinfo.txtAccountNo.Text & "', '" & addaccountno & "')"
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()

            stracs = "insert into Assocaccount (associd,accountno,accountname,dateupdated,updatedby,status) 
                     select " & getassocno(0)("Number") & ", AccountNo, (Firstname + ' ' + Middlename + ' ' + LastName), '" & Format(Now, "yyyy-MM-dd") & "', '" & My.Settings.Nickname & "', 'Added' from Customers where AccountNo = '" & customerinfo.txtAccountNo.Text & "'"
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()

            stracs = "insert into Assocaccount (associd,accountno,accountname,dateupdated,updatedby,status) 
                     select " & getassocno(0)("Number") & ", AccountNo, (Firstname + ' ' + Middlename + ' ' + LastName), '" & Format(Now, "yyyy-MM-dd") & "', '" & My.Settings.Nickname & "', 'Added' from Customers where AccountNo = '" & addaccountno & "'"
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()

            stracs = "update tbllogicnumbers set number = number + 1 where id = 15"
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()

            addaccountno = ""

            loadassocaccounts()

        Else

            stracs = "update Customers set AssocID = " & associd & " where AccountNo = '" & addaccountno & "'"
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()

            stracs = "insert into Assocaccount (associd,accountno,accountname,dateupdated,updatedby,status) 
                     select " & associd & ", AccountNo, (Firstname + ' ' + Middlename + ' ' + LastName), '" & Format(Now, "yyyy-MM-dd") & "', '" & My.Settings.Nickname & "', 'Added' from Customers where AccountNo = '" & addaccountno & "'"
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()

            addaccountno = ""

            loadassocaccounts()

        End If



    End Sub

    Private Sub DeleteAccountToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteAccountToolStripMenuItem.Click



    End Sub

    Private Sub listofassocacc_CellContentClick(sender As Object, e As KeyEventArgs) Handles listofassocacc.KeyUp

        If e.KeyValue = Keys.Delete Then

            If listofassocacc.Rows.GetRowCount(DataGridViewElementStates.Selected) = 0 Then

            Else

                Dim qweqwe As New DataTable

                stracs = "select accountno from Customers where accountno = '" & listofassocacc.Rows(listofassocacc.CurrentCellAddress.Y).Cells(0).Value & "'"
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsda.SelectCommand = acscmd
                acsda.Fill(qweqwe)

                If qweqwe.Rows.Count = 0 Then
                Else

                    Dim asd As Integer = MsgBox("Are you sure you want to add this account?", vbQuestion + vbYesNo + vbDefaultButton2, "Confirm")

                    If asd = vbYes Then

                        stracs = "update Customers set AssocID = NULL where AccountNo = '" & listofassocacc.Rows(listofassocacc.CurrentCellAddress.Y).Cells(0).Value & "'"
                        acscmd.Connection = acsconn
                        acscmd.CommandText = stracs
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        stracs = "insert into Assocaccount (associd,accountno,accountname,dateupdated,updatedby,status) 
                     select " & listofassocacc.Rows(listofassocacc.CurrentCellAddress.Y).Cells(2).Value & ", AccountNo, (Firstname + ' ' + Middlename + ' ' + LastName), '" & Format(Now, "yyyy-MM-dd") & "', '" & My.Settings.Nickname & "', 'Removed' from Customers where AccountNo = '" & listofassocacc.Rows(listofassocacc.CurrentCellAddress.Y).Cells(0).Value & "'"
                        acscmd.Connection = acsconn
                        acscmd.CommandText = stracs
                        acscmd.ExecuteNonQuery()
                        acscmd.Dispose()

                        listofassocacc.Rows.Clear()
                        assocrecords.Rows.Clear()
                        loadassocaccounts()

                    Else

                    End If



                End If

            End If

        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SearchAccount.searchingform = "assocaccounts"
        SearchAccount.ShowDialog()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Public MoveFormassoc As Boolean
    Public Moveassoc_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormassoc = True
            Me.Cursor = Cursors.NoMove2D
            Moveassoc_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormassoc Then
            Me.Location = Me.Location + (e.Location - Moveassoc_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormassoc = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub billinginfo_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.BringToFront()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Me.Activate()
    End Sub

    Private Sub billinginfo_deactivated(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.BackColor = Color.FromArgb(17, 153, 195)
    End Sub

    Private Sub ControlsClick(sender As Object, e As EventArgs) Handles _
        Panel1.Click, Label5.Click, Label1.Click, Label2.Click, TabControl1.Click, TabPage1.Click, TabPage2.Click,
        listofassocacc.Click, assocrecords.Click
        Me.Activate() 'Or Whatever
    End Sub

    Private Sub billinginfo_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.BackColor = Color.SteelBlue
    End Sub

End Class