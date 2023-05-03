Public Class OR_Items

    Dim particu, cate, entryy As String

    Private Sub rbcharges_CheckedChanged(sender As Object, e As EventArgs) Handles rbcharges.CheckedChanged
        loaditems()
    End Sub

    Private Sub rbmaterials_CheckedChanged(sender As Object, e As EventArgs) Handles rbmaterials.CheckedChanged
        loaditems()
    End Sub

    Private Sub rbothers_CheckedChanged(sender As Object, e As EventArgs) Handles rbothers.CheckedChanged
        If rbothers.Checked = True Then
            cbitems.Items.Clear()

            unitcost.Clear()
            quantity.Clear()
            totalamount.Clear()
            cbitems.DropDownStyle = ComboBoxStyle.DropDown
            unitcost.ReadOnly = False
        ElseIf rbothers.Checked = False Then
            cbitems.DropDownStyle = ComboBoxStyle.DropDownList
            unitcost.ReadOnly = True
        End If
    End Sub


    Private Sub quantity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles quantity.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If

    End Sub

    Private Sub unitcost_KeyPress(sender As Object, e As KeyPressEventArgs) Handles unitcost.KeyPress
        If e.KeyChar <> ControlChars.Back Then
            e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = ".")
        End If

    End Sub

    Private Sub cbitems_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbitems.SelectedIndexChanged

        unitcost.Clear()
        totalamount.Clear()

        Dim asd = itemsdataset.Tables(0).Select("Particular_Amount = '" & cbitems.Text.ToString.Replace("'", "''") & "'")

        For Each row In asd
            lblid.Text = row("chargeID")
            particu = row("Particular")
            unitcost.Text = row("Amount")
            cate = row("Category")
            entryy = row("Entry")
        Next


    End Sub

    Private Sub unitcost_TextChanged(sender As Object, e As EventArgs) Handles unitcost.TextChanged

        If quantity.Text = "" Then

        ElseIf unitcost.Text = "" Then

        Else
            totalamount.Text = Val(quantity.Text) * Val(unitcost.Text)
        End If


    End Sub

    Private Sub quantity_TextChanged(sender As Object, e As EventArgs) Handles quantity.TextChanged
        If quantity.Text = "" Then

        ElseIf unitcost.Text = "" Then

        Else
            totalamount.Text = FormatNumber(Val(quantity.Text) * Val(unitcost.Text))
        End If
    End Sub

    Private Sub lbladd_Click(sender As Object, e As EventArgs) Handles lbladd.Click
        If cbitems.Text = "" Then
            MsgBox("Please Select Item")
        ElseIf quantity.Text = "" Then
            MsgBox("Please Enter Quantity")
        ElseIf unitcost.Text = "" Then
            MsgBox("Please Enter UnitCost")

        Else

            If Label4.Text = "Transaction Template" Then

                TransactionTemplate.dgvlist.Rows.Add("", lblid.Text, cate, entryy, TransactionTemplate.cboffice.Text, particu, quantity.Text, FormatNumber(unitcost.Text), FormatNumber(totalamount.Text))

            Else
                If rbothers.Checked = True Then
                    Create_OR.dgvitems.Rows.Add(quantity.Text, cbitems.Text, FormatNumber(unitcost.Text), FormatNumber(totalamount.Text), "", "Others", "Others")
                Else
                    Create_OR.dgvitems.Rows.Add(quantity.Text, particu, FormatNumber(unitcost.Text), FormatNumber(totalamount.Text), lblid.Text, cate, entryy)
                End If

                cbitems.SelectedIndex = -1
                unitcost.Clear()
                quantity.Clear()
                totalamount.Clear()

                Create_OR.compute()
            End If




        End If
    End Sub

    Private Sub OR_Items_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        rbcharges.Checked = True
        loaditems()
    End Sub
    Sub loaditems()

        unitcost.Clear()
        lblid.Text = ""
        totalamount.Clear()
        quantity.Clear()

        If rbcharges.Checked = True Then

            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try

            sqlDatacharges.Clear()

            stracs = "SELECT * FROM Charges order by ChargeID"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            'acsdr = acscmd.ExecuteReader
            acsda.SelectCommand = acscmd
            acsda.Fill(sqlDatacharges)

            itemsdataset.Tables(0).Rows.Clear()
            cbitems.Items.Clear()

            For o = 0 To sqlDatacharges.Rows.Count - 1

                itemsdataset.Tables(0).Rows.Add(sqlDatacharges.Rows(o)("ChargeID"), sqlDatacharges.Rows(o)("Particular"), sqlDatacharges.Rows(o)("Particular") & " - " & sqlDatacharges.Rows(o)("Amount"), sqlDatacharges.Rows(o)("Amount"), sqlDatacharges.Rows(o)("Category"), sqlDatacharges.Rows(o)("Entry"))
                cbitems.Items.Add(sqlDatacharges.Rows(o)("Particular") & " - " & sqlDatacharges.Rows(o)("Amount"))
            Next
        ElseIf rbmaterials.Checked = True Then
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()

            sqlDatacharges.Clear()

            stracs = "SELECT * FROM Materials order by Particulars asc"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            'acsdr = acscmd.ExecuteReader
            acsda.SelectCommand = acscmd
            acsda.Fill(sqlDatacharges)

            itemsdataset.Tables(0).Rows.Clear()
            cbitems.Items.Clear()

            For o = 0 To sqlDatacharges.Rows.Count - 1

                itemsdataset.Tables(0).Rows.Add(sqlDatacharges.Rows(o)("MaterialsID"), sqlDatacharges.Rows(o)("Particulars"), sqlDatacharges.Rows(o)("Particulars") & " - " & sqlDatacharges.Rows(o)("Amount"), sqlDatacharges.Rows(o)("Amount"), "Materials", "Sundries")
                cbitems.Items.Add(sqlDatacharges.Rows(o)("Particulars") & " - " & sqlDatacharges.Rows(o)("Amount"))
            Next

        ElseIf rbothers.Checked = True Then

            itemsdataset.Tables(0).Rows.Clear()
            cbitems.Items.Clear()
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub unitcost_KeyDown(sender As Object, e As KeyEventArgs) Handles unitcost.KeyDown
        If e.KeyCode = Keys.Enter Then
            lbladd_Click(lbladd, Nothing)
        End If

    End Sub

    Private Sub quantity_KeyDown(sender As Object, e As KeyEventArgs) Handles quantity.KeyDown
        If e.KeyCode = Keys.Enter Then
            lbladd_Click(lbladd, Nothing)
        End If
    End Sub
End Class