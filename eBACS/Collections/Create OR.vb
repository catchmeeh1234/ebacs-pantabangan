Imports System.Drawing
Public Class Create_OR

    Private ornumber As Integer

    Public logic As String
    Dim accoundetails As New DataTable
    Dim reprintor As String = "Mode"

    Private Sub Create_OR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain
        autoprint.CheckState = CheckState.Checked

        cbtrans.SelectedIndex = 0

        loadornumber()

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        sqlData1.Clear()
        stracs = "select distinct TransactionType FROM TransactionTemplate"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(sqlData1)

        If sqlData1.Rows.Count = 0 Then

        Else
            For q = 0 To sqlData1.Rows.Count - 1
                cbtrans.Items.Add(sqlData1.Rows(q)("TransactionType"))
            Next
        End If

        lblStatus.ContextMenuStrip = cmsCR

    End Sub

    'Public Sub loadaccountno()

    '    Dim autoaccount As New DataTable
    '    stracs = "select AccountNo from Customers"
    '    acscmd.CommandText = stracs
    '    acscmd.Connection = acsconn
    '    acsda.SelectCommand = acscmd
    '    acsda.Fill(autoaccount)

    '    AccountNo.AutoCompleteMode = AutoCompleteMode.None
    '    AccountNo.AutoCompleteSource = AutoCompleteSource.None

    '    For x = 0 To autoaccount.Rows.Count - 1

    '        AccountNo.AutoCompleteCustomSource.Add(autoaccount(x)("AccountNo"))

    '    Next

    '    AccountNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend
    '    AccountNo.AutoCompleteSource = AutoCompleteSource.CustomSource

    'End Sub

    Public Sub loadornumber()

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        If My.Settings.Office_Code = "A" Then

            sqlData1.Clear()
            stracs = "select number from tbllogicnumbers where id = '2'"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(sqlData1)

            ornumber = sqlData1.Rows(0)("number")

            orno.Text = My.Settings.Office_Code & Format(ornumber, "0000000")

        End If

        If My.Settings.Office_Code = "B" Then

            sqlData1.Clear()
            stracs = "select number from tbllogicnumbers where id = '12'"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(sqlData1)

            ornumber = sqlData1.Rows(0)("number")

            orno.Text = My.Settings.Office_Code & Format(ornumber, "0000000")

        End If


    End Sub


    Public Sub clearallfields()
        loadornumber()
        AccountNo.Clear()
        AccName.Clear()
        Address.Clear()
        Zone.Clear()

        checkdate.Clear()
        checkno.Clear()

        lblitems.Text = "0"
        logic = ""
        cbtrans.SelectedIndex = -1
        dgvitems.Rows.Clear()
        lbltotalamountdue.Text = "0.00"


        checknoaccount.CheckState = CheckState.Unchecked
        lblName.ForeColor = Color.Black
        btnCancel.Hide()

        lblStatus.Hide()
        lblStatus.Text = "Mode"
        paymentfor.Clear()

    End Sub


    Public Sub loadinfo()

        Dim temtan As String
        temtan = AccountNo.Text
        reprint.Hide()
        clearallfields()

        AccountNo.Text = temtan

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        accoundetails.Clear()
        stracs = "SELECT AccountNo,Firstname,Middlename,Lastname,ServiceAddress,Zone,CompanyName From Customers WHERE AccountNo = '" & temtan & "'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(accoundetails)

        If accoundetails.Rows.Count = 0 Then
            MsgBox("No Data Found")
            AccountNo.Select()

        Else

            If accoundetails.Rows(0)("CompanyName") = "" Or IsDBNull(accoundetails.Rows(0)("CompanyName")) = True Then

                AccName.Text = accoundetails.Rows(0)("Firstname") & " " & accoundetails.Rows(0)("Middlename") & " " & accoundetails.Rows(0)("Lastname")

            Else
                AccName.Text = accoundetails.Rows(0)("CompanyName")
            End If


            Address.Text = accoundetails.Rows(0)("ServiceAddress")
            Zone.Text = accoundetails.Rows(0)("Zone")
            cbtrans.SelectedIndex = 0
            cbtrans.Select()
        End If

    End Sub

    Private Sub lbladditems_Click(sender As Object, e As EventArgs) Handles lbladditems.Click
        OR_Items.ShowDialog()

        OR_Items.rbcharges.Checked = True
    End Sub

    Private Sub cbtrans_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbtrans.SelectedIndexChanged

        If cbtrans.SelectedIndex = 0 Then

            If logic = "From CR" Then

            Else
                dgvitems.Rows.Clear()
                lblitems.Text = "0"
                lbltotalamountdue.Text = "0.00"
                paymentfor.Text = "Others"
            End If

            paymentfor.ReadOnly = False

        Else

            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try

            sqlData1.Clear()
            stracs = "SELECT * From TransactionTemplate WHERE TransactionType = '" & cbtrans.Text & "'"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(sqlData1)

            If sqlData1.Rows.Count = 0 Then
            Else
                paymentfor.Text = cbtrans.Text
                dgvitems.Rows.Clear()

                For l = 0 To sqlData1.Rows.Count - 1
                    dgvitems.Rows.Add(sqlData1.Rows(l)("Quantity"), sqlData1.Rows(l)("Particular"), sqlData1.Rows(l)("UnitCost"), sqlData1.Rows(l)("Amount"), sqlData1.Rows(l)("ChargeID"), sqlData1.Rows(l)("Category"), sqlData1.Rows(l)("Entry"))
                Next
                compute()

            End If

        End If


    End Sub

    Public Sub compute()

        If dgvitems.Rows.Count = 0 Then
            lblitems.Text = "0"
            lbltotalamountdue.Text = "0.00"

        Else

            Dim totall As Double

            totall = 0
            For r = 0 To dgvitems.Rows.Count - 1

                totall = totall + Double.Parse(dgvitems.Rows(r).Cells(3).Value)
            Next
            lbltotalamountdue.Text = FormatNumber(totall)
            lblitems.Text = dgvitems.Rows.Count
        End If
    End Sub

    Private Sub billsave_Click(sender As Object, e As EventArgs) Handles billsave.Click

        loadornumber()
        reprintor = "No"
        If AccName.Text = "" Then

            lblName.ForeColor = Color.Red

        Else

            If AccountNo.Text = "No Account" Then

                If paymentfor.Text = "" Then
                    MsgBox("Please add items.")
                Else
                    If dgvitems.Rows.Count = 0 Then
                        MsgBox("Please add items")
                    Else
                        save()
                    End If
                End If

            Else

                If AccountNo.Text = "" And Address.Text = "" And Zone.Text = "" Then

                    If paymentfor.Text = "" Then
                        MsgBox("Please add items.")
                    Else
                        If dgvitems.Rows.Count = 0 Then
                            MsgBox("Please add items")
                        Else
                            save()
                        End If
                    End If

                Else

                    If (accoundetails(0)("AccountNo") = AccountNo.Text And accoundetails.Rows(0)("Firstname") & " " & accoundetails.Rows(0)("Middlename") & " " & accoundetails.Rows(0)("Lastname") = AccName.Text) Or (accoundetails(0)("AccountNo") = AccountNo.Text And accoundetails.Rows(0)("CompanyName") = AccName.Text) Then

                        If paymentfor.Text = "" Then
                            MsgBox("Please add items.")
                        Else
                            If dgvitems.Rows.Count = 0 Then
                                MsgBox("Please add items")
                            Else
                                save()
                            End If
                        End If

                    Else

                        MsgBox("Account Number and Account Name did not match.")

                    End If

                End If

            End If



        End If

    End Sub

    Sub save()
        loadornumber()

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        stracs = "INSERT INTO OR_Details (ORNo,AccountNo,AccountName,AccountAddress,Zone,TransactionType,
        CheckNo,CheckDate,TotalItems,TotalAmountDue,PaymentDate,Office,Collector,Status) VALUES ('" & orno.Text & "',
        '" & AccountNo.Text & "', '" & AccName.Text.ToString.Replace("'", "''") & "', '" & Address.Text.ToString.Replace("'", "''") & "', '" & Zone.Text.ToString.Replace("'", "''") & "',
        '" & paymentfor.Text.ToString.Replace("'", "''") & "', '" & checkno.Text & "', '" & checkdate.Text & "', '" & lblitems.Text.ToString.Replace("'", "''") & "',
        '" & Double.Parse(lbltotalamountdue.Text) & "','" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss tt") & "', '" & My.Settings.Office_Name & "','" & My.Settings.Nickname & "','Pending')"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acscmd.ExecuteNonQuery()

        For q = 0 To dgvitems.Rows.Count - 1

            If acsconn.State = ConnectionState.Closed Then acsconn.Open()

            stracs = "INSERT INTO ORItems (ORNo,ChargeID,Particular,Quantity,UnitCost,Total,Category,Entry,Charges) VALUES ('" & orno.Text & "',
            '" & dgvitems.Rows(q).Cells(4).Value & "','" & dgvitems.Rows(q).Cells(1).Value.ToString.Replace("'", "''") & "', '" & dgvitems.Rows(q).Cells(0).Value & "',
            '" & Double.Parse(dgvitems.Rows(q).Cells(2).Value) & "','" & Double.Parse(dgvitems.Rows(q).Cells(3).Value) & "', '" & dgvitems.Rows(q).Cells(5).Value & "', '" & dgvitems.Rows(q).Cells(6).Value & "', '" & dgvitems.Rows(q).Cells(7).Value & "')"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acscmd.ExecuteNonQuery()

            If dgvitems.Rows(q).Cells(7).Value = "Yes" Then

                If acsconn.State = ConnectionState.Closed Then acsconn.Open()

                stracs = "UPDATE BillCharges set ORNo = '" & orno.Text & "', IsCollectionCreated = 'Yes' WHERE BillChargesID = " & dgvitems.Rows(q).Cells(4).Value
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()

            End If

        Next

        Try
            If autoprint.CheckState = CheckState.Checked Then
                printest.PrinterSettings.PrinterName = My.Settings.printerORCR
                printest.Print()
            Else

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        ornumber = ornumber + 1

        If acsconn.State = ConnectionState.Closed Then acsconn.Open()

        If My.Settings.Office_Code = "A" Then

            stracs = "UPDATE tbllogicnumbers set number = " & ornumber & " WHERE id = 2"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()

        End If

        If My.Settings.Office_Code = "B" Then

            stracs = "UPDATE tbllogicnumbers set number = " & ornumber & " WHERE id = 12"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()

        End If




        'MsgBox("Saved!")

        Calculator.dgvcalc.Rows.Add(AccountNo.Text, orno.Text, FormatNumber(lbltotalamountdue.Text))
        Calculator.calculate()
        orno.Text = My.Settings.Office_Code & Format(ornumber, "0000000")



        cbtrans.SelectedIndex = -1
        paymentfor.Clear()
        AccountNo.Select()

        AccName.ReadOnly = True
        Address.ReadOnly = True

        billsave.Enabled = False
        billsave.Hide()

        clearallfields()

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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub AccName_DoubleClick(sender As Object, e As EventArgs) Handles AccName.DoubleClick
        clearallfields()
        AccName.ReadOnly = False
        Address.ReadOnly = False
    End Sub

    Private Sub dgvitems_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvitems.KeyDown
        If e.KeyCode = Keys.Delete Then
            If dgvitems.Rows.Count = 0 Then

            Else

                Try
                    dgvitems.Rows.Remove(dgvitems.CurrentRow)
                Catch ex As Exception

                End Try

                compute()
            End If

        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles reprint.Click

        reprint.Hide()
        reprintor = "Yes"
        Try
            'reprint.Hide()
            printest.PrinterSettings.PrinterName = My.Settings.printerORCR
            printest.Print()
        Catch ex As Exception

        End Try

    End Sub

    Public Function PrintCellText(ByVal strValue As String, ByVal x As Integer, ByVal y As Integer,
                            ByVal w As Integer,
                            ByVal e As System.Drawing.Printing.PrintPageEventArgs,
                            ByVal Font As Font, ByVal Format As StringFormat) As Integer

        Dim cellRect As RectangleF = New RectangleF()
        cellRect.Location = New Point(x, y)


        cellRect.Size = New Size(w, CInt(e.Graphics.MeasureString(strValue, Font, w,
                              StringFormat.GenericTypographic).Height))

        e.Graphics.DrawString(strValue, Font, Brushes.Black, cellRect, Format)

        Return y + cellRect.Size.Height
    End Function

    Private Sub printOR_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles printOR.PrintPage

        Dim headFont As New Font("Century Gothic", 10, FontStyle.Bold, GraphicsUnit.Point)
        Dim footFont As New Font("Century Gothic", 7, GraphicsUnit.Point)
        Dim headsubFont As New Font("Century Gothic", 8, GraphicsUnit.Point)
        Dim headsubFontbold As New Font("Century Gothic", 8, FontStyle.Bold, GraphicsUnit.Point)
        Dim headsubFontitalic As New Font("Century Gothic", 8, FontStyle.Italic, GraphicsUnit.Point)
        Dim locationv As Integer = 140

        Dim MidCenterhead As StringFormat = New StringFormat()
        Dim MidLeft As StringFormat = New StringFormat()
        Dim MidRight As StringFormat = New StringFormat()
        Dim leftside As StringFormat = New StringFormat()
        Dim rightright As StringFormat = New StringFormat()

        MidCenterhead.LineAlignment = StringAlignment.Near
        MidCenterhead.Alignment = StringAlignment.Center

        MidLeft.LineAlignment = StringAlignment.Center
        MidLeft.Alignment = StringAlignment.Near

        MidRight.LineAlignment = StringAlignment.Center
        MidRight.Alignment = StringAlignment.Far

        leftside.LineAlignment = StringAlignment.Near
        leftside.Alignment = StringAlignment.Near

        rightright.LineAlignment = StringAlignment.Near
        rightright.Alignment = StringAlignment.Far

        'Dim CurX As Integer = 50
        'Dim CurY As Integer = 50
        'Dim iWidth As Integer = 250

        Dim cellRecthead As RectangleF
        cellRecthead = New RectangleF()
        cellRecthead.Location = New Point(0, 0)
        cellRecthead.Size = New Size(250, 100)

        Dim CurX As Integer = 0
        Dim CurY As Integer = 0
        Dim iWidth As Integer = 250

        CurY = PrintCellText("SANTA ROSA (NE) WATER DISTRICT", CurX, CurY, iWidth, e, headFont, MidCenterhead)
        CurY = PrintCellText("Santa Rosa -  Fort Magsaysay Road", CurX, CurY, iWidth, e, headsubFont, MidCenterhead)
        CurY = PrintCellText("Santa Rosa, Nueva Ecija", CurX, CurY, iWidth, e, headsubFont, MidCenterhead)
        CurY = PrintCellText("Tel. No. (044) 940-6800", CurX, CurY, iWidth, e, headsubFont, MidCenterhead)
        CurY = PrintCellText("TIN: 004-104-990-000 NON-VAT", CurX, CurY, iWidth, e, headsubFont, MidCenterhead)

        e.Graphics.DrawString("OFFICIAL RECEIPT", headFont, Brushes.Black, 60, 80)

        e.Graphics.DrawString("CR No.:", headsubFont, Brushes.Black, 0, 110)
        e.Graphics.DrawString(orno.Text, headsubFont, Brushes.Black, 60, 110)
        e.Graphics.DrawString("Acc. No.:", headsubFont, Brushes.Black, 0, 125)
        e.Graphics.DrawString(AccountNo.Text, headsubFont, Brushes.Black, 60, 125)

        e.Graphics.DrawString("Name:", headsubFont, Brushes.Black, 0, 140)

        If AccName.TextLength < 25 Then

            e.Graphics.DrawString(AccName.Text.Substring(0, AccName.TextLength), headsubFont, Brushes.Black, 60, 140)
            locationv = locationv + 15
        Else

            If AccName.TextLength >= 25 And AccName.TextLength <= 49 Then

                e.Graphics.DrawString(AccName.Text.Substring(0, 25).ToUpper, headsubFont, Brushes.Black, 60, 140)
                e.Graphics.DrawString(AccName.Text.Substring(25, AccName.TextLength - 25).ToUpper, headsubFont, Brushes.Black, 60, 155)
                locationv = locationv + 30
            End If

            If AccName.TextLength >= 50 And AccName.TextLength <= 74 Then

                e.Graphics.DrawString(AccName.Text.Substring(0, 25).ToUpper, headsubFont, Brushes.Black, 60, 140)
                e.Graphics.DrawString(AccName.Text.Substring(25, 25).ToUpper, headsubFont, Brushes.Black, 60, 155)
                e.Graphics.DrawString(AccName.Text.Substring(50, AccName.TextLength - 50).ToUpper, headsubFont, Brushes.Black, 60, 170)
                locationv = locationv + 45
            End If

            If AccName.TextLength >= 75 And AccName.TextLength <= 99 Then

                e.Graphics.DrawString(AccName.Text.Substring(0, 25).ToUpper, headsubFont, Brushes.Black, 60, 140)
                e.Graphics.DrawString(AccName.Text.Substring(25, 25).ToUpper, headsubFont, Brushes.Black, 60, 155)
                e.Graphics.DrawString(AccName.Text.Substring(50, 25).ToUpper, headsubFont, Brushes.Black, 60, 170)
                e.Graphics.DrawString(AccName.Text.Substring(75, AccName.TextLength - 75).ToUpper, headsubFont, Brushes.Black, 60, 185)
                locationv = locationv + 60
            End If

            If AccName.TextLength >= 100 And AccName.TextLength <= 124 Then

                e.Graphics.DrawString(AccName.Text.Substring(0, 25).ToUpper, headsubFont, Brushes.Black, 60, 140)
                e.Graphics.DrawString(AccName.Text.Substring(25, 25).ToUpper, headsubFont, Brushes.Black, 60, 155)
                e.Graphics.DrawString(AccName.Text.Substring(50, 25).ToUpper, headsubFont, Brushes.Black, 60, 170)
                e.Graphics.DrawString(AccName.Text.Substring(75, 25).ToUpper, headsubFont, Brushes.Black, 60, 185)
                e.Graphics.DrawString(AccName.Text.Substring(100, AccName.TextLength - 100).ToUpper, headsubFont, Brushes.Black, 60, 200)
                locationv = locationv + 75
            End If

        End If
        e.Graphics.DrawString("Address:", headsubFont, Brushes.Black, 0, locationv)

        If Address.TextLength < 25 Then

            e.Graphics.DrawString(Address.Text.Substring(0, Address.TextLength), headsubFont, Brushes.Black, 60, locationv)
            locationv = locationv + 15
        Else

            If Address.TextLength >= 25 And Address.TextLength <= 49 Then

                e.Graphics.DrawString(Address.Text.Substring(0, 25).ToUpper, headsubFont, Brushes.Black, 60, locationv)
                e.Graphics.DrawString(Address.Text.Substring(25, Address.TextLength - 25).ToUpper, headsubFont, Brushes.Black, 60, locationv + 15)
                locationv = locationv + 30
            End If

            If Address.TextLength >= 50 And Address.TextLength <= 74 Then

                e.Graphics.DrawString(Address.Text.Substring(0, 25).ToUpper, headsubFont, Brushes.Black, 60, locationv)
                e.Graphics.DrawString(Address.Text.Substring(25, 25).ToUpper, headsubFont, Brushes.Black, 60, locationv + 15)
                e.Graphics.DrawString(Address.Text.Substring(50, Address.TextLength - 50).ToUpper, headsubFont, Brushes.Black, 60, locationv + 30)
                locationv = locationv + 45
            End If

            If Address.TextLength >= 75 And Address.TextLength <= 99 Then

                e.Graphics.DrawString(Address.Text.Substring(0, 25).ToUpper, headsubFont, Brushes.Black, 60, locationv)
                e.Graphics.DrawString(Address.Text.Substring(25, 25).ToUpper, headsubFont, Brushes.Black, 60, locationv + 15)
                e.Graphics.DrawString(Address.Text.Substring(50, 25).ToUpper, headsubFont, Brushes.Black, 60, locationv + 30)
                e.Graphics.DrawString(Address.Text.Substring(75, Address.TextLength - 75).ToUpper, headsubFont, Brushes.Black, 60, locationv + 45)
                locationv = locationv + 60
            End If

            If Address.TextLength >= 100 And Address.TextLength <= 124 Then

                e.Graphics.DrawString(Address.Text.Substring(0, 25).ToUpper, headsubFont, Brushes.Black, 60, locationv)
                e.Graphics.DrawString(Address.Text.Substring(25, 25).ToUpper, headsubFont, Brushes.Black, 60, locationv + 15)
                e.Graphics.DrawString(Address.Text.Substring(50, 25).ToUpper, headsubFont, Brushes.Black, 60, locationv + 30)
                e.Graphics.DrawString(Address.Text.Substring(75, 25).ToUpper, headsubFont, Brushes.Black, 60, locationv + 45)
                e.Graphics.DrawString(Address.Text.Substring(100, Address.TextLength - 100).ToUpper, headsubFont, Brushes.Black, 60, locationv + 60)
                locationv = locationv + 75
            End If

        End If

        e.Graphics.DrawString("__________________________________________________", headsubFont, Brushes.Black, 0, locationv)
        locationv = locationv + 20

        e.Graphics.DrawString("As Payment for:", headsubFont, Brushes.Black, 0, locationv)
        'e.Graphics.DrawString(paymentfor.Text, headsubFontbold, Brushes.Black, 90, locationv)
        locationv = locationv + 20

        'Dim aspf As RectangleF
        'aspf = New RectangleF()

        'aspf.Location = New Point(0, locationv)
        'aspf.Size = New Size(250, 10)

        'e.Graphics.DrawString(paymentfor.Text, headsubFont, Brushes.Black, aspf, MidLeft)
        'locationv = locationv + 40



        If paymentfor.TextLength < 35 Then

            e.Graphics.DrawString(paymentfor.Text.Substring(0, paymentfor.TextLength), headsubFont, Brushes.Black, 0, locationv)
            locationv = locationv + 15
        Else

            If paymentfor.TextLength >= 35 And paymentfor.TextLength <= 69 Then

                e.Graphics.DrawString(paymentfor.Text.Substring(0, 35).ToUpper, headsubFont, Brushes.Black, 0, locationv)
                e.Graphics.DrawString(paymentfor.Text.Substring(35, paymentfor.TextLength - 35).ToUpper, headsubFont, Brushes.Black, 0, locationv + 15)
                locationv = locationv + 30

            End If

            If paymentfor.TextLength >= 70 And paymentfor.TextLength <= 104 Then

                e.Graphics.DrawString(paymentfor.Text.Substring(0, 35).ToUpper, headsubFont, Brushes.Black, 0, locationv)
                e.Graphics.DrawString(paymentfor.Text.Substring(35, 35).ToUpper, headsubFont, Brushes.Black, 0, locationv + 15)
                e.Graphics.DrawString(paymentfor.Text.Substring(70, paymentfor.TextLength - 70).ToUpper, headsubFont, Brushes.Black, 0, locationv + 30)
                locationv = locationv + 45
            End If

            If paymentfor.TextLength >= 105 And paymentfor.TextLength <= 139 Then

                e.Graphics.DrawString(paymentfor.Text.Substring(0, 35).ToUpper, headsubFont, Brushes.Black, 0, locationv)
                e.Graphics.DrawString(paymentfor.Text.Substring(35, 35).ToUpper, headsubFont, Brushes.Black, 0, locationv + 15)
                e.Graphics.DrawString(paymentfor.Text.Substring(70, 35).ToUpper, headsubFont, Brushes.Black, 0, locationv + 30)
                e.Graphics.DrawString(paymentfor.Text.Substring(105, paymentfor.TextLength - 105).ToUpper, headsubFont, Brushes.Black, 0, locationv + 45)
                locationv = locationv + 60
            End If

            If paymentfor.TextLength >= 140 And paymentfor.TextLength <= 174 Then

                e.Graphics.DrawString(paymentfor.Text.Substring(0, 35).ToUpper, headsubFont, Brushes.Black, 0, locationv)
                e.Graphics.DrawString(paymentfor.Text.Substring(35, 35).ToUpper, headsubFont, Brushes.Black, 0, locationv + 15)
                e.Graphics.DrawString(paymentfor.Text.Substring(70, 35).ToUpper, headsubFont, Brushes.Black, 0, locationv + 30)
                e.Graphics.DrawString(paymentfor.Text.Substring(105, 35).ToUpper, headsubFont, Brushes.Black, 0, locationv + 45)
                e.Graphics.DrawString(paymentfor.Text.Substring(140, paymentfor.TextLength - 140).ToUpper, headsubFont, Brushes.Black, 0, locationv + 60)
                locationv = locationv + 75
            End If

        End If

        locationv = locationv + 5

        e.Graphics.DrawString("Qty", headsubFont, Brushes.Black, 5, locationv)
        e.Graphics.DrawString("Particulars", headsubFont, Brushes.Black, 40, locationv)
        e.Graphics.DrawString("Amount", headsubFont, Brushes.Black, 180, locationv)

        locationv = locationv + 20


        Dim cellamount, quan, part As RectangleF
        cellamount = New RectangleF()
        quan = New RectangleF()
        part = New RectangleF()

        'e.Graphics.DrawString(billPenalty.Text, headsubFont, Brushes.Black, cellRectpenalty, MidRight)

        For p = 0 To dgvitems.Rows.Count - 1

            quan.Location = New Point(0, locationv)
            quan.Size = New Size(30, 15)

            part.Location = New Point(40, locationv)
            part.Size = New Size(150, 15)

            cellamount.Location = New Point(130, locationv)
            cellamount.Size = New Size(100, 15)

            'e.Graphics.DrawString(dgvitems.Rows(p).Cells(0).Value, headsubFont, Brushes.Black, 5, locationv)
            'e.Graphics.DrawString(dgvitems.Rows(p).Cells(1).Value, headsubFont, Brushes.Black, 40, locationv)
            'e.Graphics.DrawString(dgvitems.Rows(p).Cells(3).Value, headsubFont, Brushes.Black, 180, locationv)
            e.Graphics.DrawString(dgvitems.Rows(p).Cells(0).Value, headsubFont, Brushes.Black, quan, rightright)

            Dim asd As String
            asd = dgvitems.Rows(p).Cells(1).Value



            If asd.Length < 20 Then

                e.Graphics.DrawString(asd.Substring(0, asd.Length), headsubFont, Brushes.Black, 40, locationv)
                locationv = locationv + 15
            Else

                If asd.Length >= 20 And asd.Length <= 39 Then

                    e.Graphics.DrawString(asd.Substring(0, 20).ToUpper, headsubFont, Brushes.Black, 40, locationv)
                    e.Graphics.DrawString(asd.Substring(20, asd.Length - 20).ToUpper, headsubFont, Brushes.Black, 40, locationv + 15)
                    locationv = locationv + 30

                End If

                If asd.Length >= 40 And asd.Length <= 59 Then

                    e.Graphics.DrawString(asd.Substring(0, 20).ToUpper, headsubFont, Brushes.Black, 40, locationv)
                    e.Graphics.DrawString(asd.Substring(20, 20).ToUpper, headsubFont, Brushes.Black, 40, locationv + 15)
                    e.Graphics.DrawString(asd.Substring(40, asd.Length - 40).ToUpper, headsubFont, Brushes.Black, 40, locationv + 30)
                    locationv = locationv + 45
                End If

                If asd.Length >= 60 And asd.Length <= 79 Then

                    e.Graphics.DrawString(asd.Substring(0, 20).ToUpper, headsubFont, Brushes.Black, 40, locationv)
                    e.Graphics.DrawString(asd.Substring(20, 20).ToUpper, headsubFont, Brushes.Black, 40, locationv + 15)
                    e.Graphics.DrawString(asd.Substring(40, 20).ToUpper, headsubFont, Brushes.Black, 40, locationv + 30)
                    e.Graphics.DrawString(asd.Substring(60, asd.Length - 60).ToUpper, headsubFont, Brushes.Black, 40, locationv + 45)
                    locationv = locationv + 60
                End If

                If asd.Length >= 140 And asd.Length <= 174 Then

                    e.Graphics.DrawString(asd.Substring(0, 35).ToUpper, headsubFont, Brushes.Black, 40, locationv)
                    e.Graphics.DrawString(asd.Substring(35, 35).ToUpper, headsubFont, Brushes.Black, 40, locationv + 15)
                    e.Graphics.DrawString(asd.Substring(70, 35).ToUpper, headsubFont, Brushes.Black, 40, locationv + 30)
                    e.Graphics.DrawString(asd.Substring(105, 35).ToUpper, headsubFont, Brushes.Black, 40, locationv + 45)
                    e.Graphics.DrawString(asd.Substring(140, asd.Length - 140).ToUpper, headsubFont, Brushes.Black, 40, locationv + 60)
                    locationv = locationv + 75
                End If

            End If

            'e.Graphics.DrawString(dgvitems.Rows(p).Cells(1).Value, headsubFont, Brushes.Black, part, MidLeft)
            e.Graphics.DrawString(dgvitems.Rows(p).Cells(3).Value, headsubFont, Brushes.Black, cellamount, rightright)
            'locationv = locationv + 15
        Next

        e.Graphics.DrawString("__________________________________________________", headsubFont, Brushes.Black, 0, locationv)
        locationv = locationv + 20

        Dim totalamount, totalabel As RectangleF
        totalamount = New RectangleF()
        totalabel = New RectangleF()

        totalamount.Location = New Point(130, locationv)
        totalamount.Size = New Size(100, 10)

        totalabel.Location = New Point(0, locationv)
        totalabel.Size = New Size(100, 10)

        e.Graphics.DrawString("Amount Paid", headsubFontbold, Brushes.Black, totalabel, MidLeft)
        e.Graphics.DrawString(lbltotalamountdue.Text, headsubFontbold, Brushes.Black, totalamount, MidRight)

        locationv = locationv + 10

        e.Graphics.DrawString("__________________________________________________", headsubFont, Brushes.Black, 0, locationv)
        locationv = locationv + 20

        If reprintor = "Yes" Then

            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Dim datetimecashier As New DataTable
            stracs = "select Collector, PaymentDate FROM OR_Details WHERE ORNo = '" & orno.Text & "'"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(datetimecashier)

            e.Graphics.DrawString("Cashier:  " & datetimecashier.Rows(0)("Collector"), headsubFont, Brushes.Black, 0, locationv)
            locationv = locationv + 15

            e.Graphics.DrawString("Date:     " & Format(datetimecashier.Rows(0)("PaymentDate"), "MM/dd/yyyy hh:mm tt"), headsubFont, Brushes.Black, 0, locationv)
            locationv = locationv + 25

        End If

        If reprintor = "No" Then

            e.Graphics.DrawString("Cashier:  " & My.Settings.Nickname, headsubFont, Brushes.Black, 0, locationv)
            locationv = locationv + 15

            e.Graphics.DrawString("Date:     " & Format(Now, "MM/dd/yyyy hh:mm tt"), headsubFont, Brushes.Black, 0, locationv)
            locationv = locationv + 25

        End If


        'footFont

        e.Graphics.DrawString("BIR CAS Permit No. 23B-CAS-0414-0001", footFont, Brushes.Black, 0, locationv)
        locationv = locationv + 15

        Dim footer As RectangleF
        footer = New RectangleF()

        footer.Location = New Point(0, locationv)
        footer.Size = New Size(250, 100)

        'e.Graphics.DrawString(dgvitems.Rows(p).Cells(0).Value, headsubFont, Brushes.Black, quan, MidRight)

        e.Graphics.DrawString("Note: This Document is not a valid source of input tax.", footFont, Brushes.Black, footer, leftside)
        locationv = locationv + 15

    End Sub

    Private Sub accSearch_Click(sender As Object, e As EventArgs) Handles accSearch.Click
        SearchAccount.Show()
        SearchAccount.BringToFront()
        SearchAccount.searchingform = "create or"
    End Sub

    Public Sub orno_KeyDown(sender As Object, e As KeyEventArgs) Handles orno.KeyDown

        If e.KeyValue = Keys.Enter Then

            Dim searchOrno As New DataTable
            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try
            searchOrno.Clear()
            stracs = "SELECT * From OR_Details WHERE ORNo = '" & orno.Text & "'"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(searchOrno)

            If searchOrno.Rows.Count = 0 Then
                MsgBox("No record found.")
                clearallfields()
            Else

                If searchOrno.Rows(0)("Cancelled") = "Yes" Then
                    btnCancel.Hide()
                    lblStatus.Text = "Cancelled"
                    lblStatus.ForeColor = Color.Red
                    lblStatus.Show()
                Else

                    lblStatus.Text = searchOrno.Rows(0)("Status")

                    btnCancel.Show()

                    If searchOrno.Rows(0)("Status") = "Pending" Then
                        lblStatus.ForeColor = Color.Orange
                    End If

                    If searchOrno.Rows(0)("Status") = "Posted" Then
                        lblStatus.ForeColor = Color.Green
                    End If
                    lblStatus.Show()
                End If

                reprint.Show()
                billsave.Visible = False
                billsave.Enabled = False
                AccountNo.Text = searchOrno.Rows(0)("AccountNo")
                AccName.Text = searchOrno.Rows(0)("AccountName")
                Address.Text = searchOrno.Rows(0)("AccountAddress")
                Zone.Text = searchOrno.Rows(0)("Zone")
                paymentfor.Text = searchOrno.Rows(0)("TransactionType")

                Dim searchitems As New DataTable
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                searchOrno.Clear()
                stracs = "SELECT * From ORItems WHERE ORNo = '" & orno.Text & "'"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acsda.SelectCommand = acscmd
                acsda.Fill(searchitems)

                dgvitems.Rows.Clear()

                For i = 0 To searchitems.Rows.Count - 1

                    dgvitems.Rows.Add(searchitems.Rows(i)("Quantity"), searchitems.Rows(i)("Particular"),
                                      Format(searchitems.Rows(i)("UnitCost"), "standard"), Format(searchitems.Rows(i)("Total"), "standard"),
                                      searchitems.Rows(i)("ChargeID"), searchitems.Rows(i)("Category"),
                                      searchitems.Rows(i)("Entry"))

                Next

            End If
            compute()
        End If

    End Sub

    Private Sub AccountNo_KeyDown(sender As Object, e As KeyEventArgs) Handles AccountNo.KeyDown

        If e.KeyValue = Keys.Enter Then

            loadornumber()
            loadinfo()

        End If

    End Sub

    Public Sub RefereshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefereshToolStripMenuItem.Click

        clearallfields()
        AccountNo.Select()

    End Sub

    Private Sub checknoaccount_CheckedChanged(sender As Object, e As EventArgs) Handles checknoaccount.CheckedChanged

        If checknoaccount.CheckState = CheckState.Checked Then

            AccountNo.Text = "No Account"
            AccName.Clear()
            Address.Clear()
            Zone.Clear()

            lblitems.Text = "0"
            logic = ""
            cbtrans.SelectedIndex = -1
            dgvitems.Rows.Clear()
            lbltotalamountdue.Text = "0.00"
            paymentfor.Clear()

            AccountNo.ReadOnly = True
            AccName.ReadOnly = False
            Address.ReadOnly = False
            AccName.Select()

            billsave.Show()

        Else

            AccountNo.Clear()
            AccName.Clear()
            Address.Clear()
            Zone.Clear()
            lblName.ForeColor = Color.Black
            lblitems.Text = "0"
            logic = ""
            cbtrans.SelectedIndex = -1
            dgvitems.Rows.Clear()
            lbltotalamountdue.Text = "0.00"
            paymentfor.Clear()

            AccountNo.ReadOnly = False
            AccName.ReadOnly = True
            Address.ReadOnly = True
            AccountNo.Select()

        End If

    End Sub

    Private Sub AccName_TextChanged(sender As Object, e As EventArgs) Handles AccName.TextChanged

        If lblStatus.Text = "Mode" Then

            If AccName.Text = "" Then
                lblName.ForeColor = Color.Red
                billsave.Visible = False
                billsave.Enabled = False
            Else
                lblName.ForeColor = Color.Black
                billsave.Visible = True
                billsave.Enabled = True
            End If

        Else

        End If



    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        ORCancel.cancelmode = "Create New"
        ORCancel.ShowDialog()

        'orno_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))



    End Sub

    Private Sub orno_TextChanged(sender As Object, e As EventArgs) Handles orno.TextChanged

        btnCancel.Hide()
        reprint.Hide()

    End Sub


    Private Sub Create_OR_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Me.Activate()
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click

        billsave_Click(Nothing, New KeyEventArgs(Keys.Enter))

    End Sub

    Private Sub FindToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FindToolStripMenuItem.Click
        accSearch_Click(Nothing, New KeyEventArgs(Keys.Enter))
    End Sub

    Private Sub createor_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.BackColor = Color.SteelBlue
    End Sub

    Private Sub createor_Deactivated(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.BackColor = Color.FromArgb(17, 153, 195)
    End Sub

    Private Sub ControlsClick(sender As Object, e As EventArgs) Handles _
        orno.Click, AccountNo.Click, AccName.Click, Address.Click, Zone.Click, GroupBox1.Click, GroupBox2.Click,
        checknoaccount.Click, cbtrans.Click, paymentfor.Click, GroupBox7.Click, Panel1.Click, GroupBox3.Click,
        rbcash.Click, rbcheck.Click, autoprint.Click, reprint.Click, billsave.Click ' etc.
        Me.Activate() 'Or Whatever
    End Sub

    Private Sub DetailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DetailsToolStripMenuItem.Click

        ORCancel.cancelmode = "Viewing"
        ORCancel.ShowDialog()

    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles printest.PrintPage


        Dim headFont As New Font("Century Gothic", 10, FontStyle.Bold, GraphicsUnit.Point)
        Dim footFont As New Font("Century Gothic", 7, GraphicsUnit.Point)
        Dim headsubFont As New Font("Century Gothic", 8, GraphicsUnit.Point)
        Dim headsubFontbold As New Font("Century Gothic", 8, FontStyle.Bold, GraphicsUnit.Point)
        Dim headsubFontitalic As New Font("Century Gothic", 8, FontStyle.Italic, GraphicsUnit.Point)
        Dim locationv As Integer = 140

        Dim MidCenterhead As StringFormat = New StringFormat()
        Dim MidLeft As StringFormat = New StringFormat()
        Dim MidRight As StringFormat = New StringFormat()
        Dim leftside As StringFormat = New StringFormat()
        Dim rightright As StringFormat = New StringFormat()

        MidCenterhead.LineAlignment = StringAlignment.Near
        MidCenterhead.Alignment = StringAlignment.Center

        MidLeft.LineAlignment = StringAlignment.Center
        MidLeft.Alignment = StringAlignment.Near

        MidRight.LineAlignment = StringAlignment.Center
        MidRight.Alignment = StringAlignment.Far

        leftside.LineAlignment = StringAlignment.Near
        leftside.Alignment = StringAlignment.Near

        rightright.LineAlignment = StringAlignment.Near
        rightright.Alignment = StringAlignment.Far

        'Dim CurX As Integer = 50
        'Dim CurY As Integer = 50
        'Dim iWidth As Integer = 250

        Dim cellRecthead As RectangleF
        cellRecthead = New RectangleF()
        cellRecthead.Location = New Point(0, 0)
        cellRecthead.Size = New Size(250, 100)

        Dim CurX As Integer = 0
        Dim CurY As Integer = 0
        Dim iWidth As Integer = 250

        CurY = PrintCellText("SANTA ROSA (NE) WATER DISTRICT", CurX, CurY, iWidth, e, headFont, MidCenterhead)
        CurY = PrintCellText("Santa Rosa -  Fort Magsaysay Road", CurX, CurY, iWidth, e, headsubFont, MidCenterhead)
        CurY = PrintCellText("Santa Rosa, Nueva Ecija", CurX, CurY, iWidth, e, headsubFont, MidCenterhead)
        CurY = PrintCellText("Tel. No. (044) 940-0142", CurX, CurY, iWidth, e, headsubFont, MidCenterhead)
        CurY = PrintCellText("TIN: 004-104-990-000 NON-VAT", CurX, CurY, iWidth, e, headsubFont, MidCenterhead)

        e.Graphics.DrawString("OFFICIAL RECEIPT", headFont, Brushes.Black, 60, 80)

        e.Graphics.DrawString("CR No.:", headsubFont, Brushes.Black, 0, 110)
        e.Graphics.DrawString(orno.Text, headsubFont, Brushes.Black, 60, 110)
        e.Graphics.DrawString("Acc. No.:", headsubFont, Brushes.Black, 0, 125)
        e.Graphics.DrawString(AccountNo.Text, headsubFont, Brushes.Black, 60, 125)

        e.Graphics.DrawString("Name:", headsubFont, Brushes.Black, 0, 140)

        'start ng name

        Dim pangalanrec As RectangleF
        pangalanrec = New RectangleF()

        pangalanrec.Size = New Size(180, pangalanrec.Height)

        Dim TotalStringHeight As Single = e.Graphics.MeasureString(AccName.Text, headsubFont, New SizeF(pangalanrec.Width, pangalanrec.Height), leftside).Height
        Dim SingleLineHeight As Single = e.Graphics.MeasureString("T", headsubFont, New SizeF(pangalanrec.Width, pangalanrec.Height), leftside).Height

        Dim NumberOfLines As Integer = Convert.ToInt32(TotalStringHeight / SingleLineHeight)

        pangalanrec.Location = New Point(60, locationv + 1)

        e.Graphics.DrawString(AccName.Text, headsubFont, Brushes.Black, pangalanrec, leftside)

        locationv = locationv + (NumberOfLines * 15)

        'end ng name

        e.Graphics.DrawString("Address:", headsubFont, Brushes.Black, 0, locationv)

        'start ng address

        Dim addrec As RectangleF
        addrec = New RectangleF()

        addrec.Size = New Size(180, addrec.Height)

        Dim TotalStringHeightaddress As Single = e.Graphics.MeasureString(Address.Text, headsubFont, New SizeF(addrec.Width, addrec.Height), leftside).Height
        Dim SingleLineHeightaddress As Single = e.Graphics.MeasureString("T", headsubFont, New SizeF(addrec.Width, addrec.Height), leftside).Height

        Dim NumberOfLinesaddress As Integer = Convert.ToInt32(TotalStringHeightaddress / SingleLineHeightaddress)

        addrec.Location = New Point(60, locationv + 1)

        e.Graphics.DrawString(Address.Text, headsubFont, Brushes.Black, addrec, leftside)

        locationv = locationv + (NumberOfLinesaddress * 15)

        'end address

        locationv = locationv + 5
        e.Graphics.DrawString("__________________________________________________", headsubFont, Brushes.Black, 0, locationv)
        locationv = locationv + 20

        e.Graphics.DrawString("As Payment for:", headsubFont, Brushes.Black, 0, locationv)
        'e.Graphics.DrawString(paymentfor.Text, headsubFontbold, Brushes.Black, 90, locationv)
        locationv = locationv + 20

        'as payment for start

        Dim aspf As RectangleF
        aspf = New RectangleF()

        'Dim sukatan As Integer
        'Dim gansal As Double

        'sukatan = (paymentfor.TextLength / 50)
        'gansal = (paymentfor.TextLength / 50) - sukatan

        'If sukatan = 0 Then

        '    aspf.Size = New Size(250, 15)

        'Else
        '    If gansal > 0 Then
        '        sukatan = sukatan + 1
        '    Else

        '    End If

        '    aspf.Size = New Size(250, sukatan * 15)

        'End If


        aspf.Size = New Size(250, aspf.Height)

        Dim TotalStringHeightaspf As Single = e.Graphics.MeasureString(paymentfor.Text, headsubFont, New SizeF(aspf.Width, aspf.Height), leftside).Height
        Dim SingleLineHeightaspf As Single = e.Graphics.MeasureString("T", headsubFont, New SizeF(aspf.Width, aspf.Height), leftside).Height

        Dim NumberOfLinesaspf As Integer = Convert.ToInt32(TotalStringHeightaspf / SingleLineHeightaspf)


        aspf.Location = New Point(0, locationv + 1)

        e.Graphics.DrawString(paymentfor.Text, headsubFont, Brushes.Black, aspf, leftside)

        locationv = locationv + (NumberOfLinesaspf * 15)

        'as payment for end

        locationv = locationv + 5

        e.Graphics.DrawString("Qty", headsubFont, Brushes.Black, 5, locationv)
        e.Graphics.DrawString("Particulars", headsubFont, Brushes.Black, 40, locationv)
        e.Graphics.DrawString("Amount", headsubFont, Brushes.Black, 200, locationv)

        locationv = locationv + 20

        Dim cellamount, quan, part As RectangleF
        cellamount = New RectangleF()
        quan = New RectangleF()
        part = New RectangleF()

        'e.Graphics.DrawString(billPenalty.Text, headsubFont, Brushes.Black, cellRectpenalty, MidRight)



        For p = 0 To dgvitems.Rows.Count - 1

            quan.Location = New Point(0, locationv)
            quan.Size = New Size(30, 15)

            'part.Location = New Point(40, locationv)
            'part.Size = New Size(150, 15)

            cellamount.Location = New Point(145, locationv)
            cellamount.Size = New Size(100, 15)

            'e.Graphics.DrawString(dgvitems.Rows(p).Cells(0).Value, headsubFont, Brushes.Black, 5, locationv)
            'e.Graphics.DrawString(dgvitems.Rows(p).Cells(1).Value, headsubFont, Brushes.Black, 40, locationv)
            'e.Graphics.DrawString(dgvitems.Rows(p).Cells(3).Value, headsubFont, Brushes.Black, 180, locationv)
            e.Graphics.DrawString(dgvitems.Rows(p).Cells(0).Value, headsubFont, Brushes.Black, quan, rightright)

            'start ng partculars


            'Dim sukatan2 As Integer
            'Dim gansal2 As Double

            'sukatan2 = (dgvitems.Rows(p).Cells(1).Value.ToString.Length / 30)
            'gansal2 = (dgvitems.Rows(p).Cells(1).Value.ToString.Length / 30) - sukatan2

            'If sukatan2 = 0 Then

            '    part.Size = New Size(150, 15)

            'Else
            '    If gansal2 > 0 Then
            '        sukatan2 = sukatan2 + 1
            '    Else

            '    End If

            '    part.Size = New Size(150, sukatan2 * 17)

            'End If

            part.Size = New Size(150, part.Height)

            Dim TotalStringHeightpart As Single = e.Graphics.MeasureString(dgvitems.Rows(p).Cells(1).Value, headsubFont, New SizeF(part.Width, part.Height), leftside).Height
            Dim SingleLineHeightpart As Single = e.Graphics.MeasureString("T", headsubFont, New SizeF(part.Width, part.Height), leftside).Height

            Dim NumberOfLinespart As Integer = Convert.ToInt32(TotalStringHeightpart / SingleLineHeightpart)


            part.Location = New Point(40, locationv + 1)



            e.Graphics.DrawString(dgvitems.Rows(p).Cells(1).Value, headsubFont, Brushes.Black, part, leftside)

            locationv = locationv + (NumberOfLinespart * 15)

            'end ng partculars

            'e.Graphics.DrawString(dgvitems.Rows(p).Cells(1).Value, headsubFont, Brushes.Black, part, MidLeft)
            e.Graphics.DrawString(dgvitems.Rows(p).Cells(3).Value, headsubFont, Brushes.Black, cellamount, rightright)
            'locationv = locationv + 15
        Next

        e.Graphics.DrawString("__________________________________________________", headsubFont, Brushes.Black, 0, locationv)
        locationv = locationv + 20

        Dim totalamount, totalabel As RectangleF
        totalamount = New RectangleF()
        totalabel = New RectangleF()

        totalamount.Location = New Point(145, locationv)
        totalamount.Size = New Size(100, 10)

        totalabel.Location = New Point(0, locationv)
        totalabel.Size = New Size(100, 10)

        e.Graphics.DrawString("Amount Paid", headsubFontbold, Brushes.Black, totalabel, MidLeft)
        e.Graphics.DrawString(lbltotalamountdue.Text, headsubFontbold, Brushes.Black, totalamount, MidRight)

        locationv = locationv + 10

        e.Graphics.DrawString("__________________________________________________", headsubFont, Brushes.Black, 0, locationv)
        locationv = locationv + 20

        If reprintor = "Yes" Then

            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Dim datetimecashier As New DataTable
            stracs = "select Collector, PaymentDate FROM OR_Details WHERE ORNo = '" & orno.Text & "'"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(datetimecashier)

            e.Graphics.DrawString("Cashier:  " & datetimecashier.Rows(0)("Collector"), headsubFont, Brushes.Black, 0, locationv)
            locationv = locationv + 15

            e.Graphics.DrawString("Date:     " & Format(datetimecashier.Rows(0)("PaymentDate"), "MM/dd/yyyy hh:mm tt"), headsubFont, Brushes.Black, 0, locationv)
            locationv = locationv + 25

        End If

        If reprintor = "No" Then

            e.Graphics.DrawString("Cashier:  " & My.Settings.Nickname, headsubFont, Brushes.Black, 0, locationv)
            locationv = locationv + 15

            e.Graphics.DrawString("Date:     " & Format(Now, "MM/dd/yyyy hh:mm tt"), headsubFont, Brushes.Black, 0, locationv)
            locationv = locationv + 25

        End If


        'footFont

        e.Graphics.DrawString("BIR CAS Permit No. 23B-CAS-0414-0001", footFont, Brushes.Black, 0, locationv)
        locationv = locationv + 15

        Dim footer As RectangleF
        footer = New RectangleF()

        footer.Location = New Point(0, locationv)
        footer.Size = New Size(250, paymentfor.TextLength \ 250)

        'e.Graphics.DrawString(dgvitems.Rows(p).Cells(0).Value, headsubFont, Brushes.Black, quan, MidRight)

        e.Graphics.DrawString("Note: This Document is not a valid source of input tax.", footFont, Brushes.Black, footer, leftside)
        locationv = locationv + footer.Height + 15

    End Sub
End Class