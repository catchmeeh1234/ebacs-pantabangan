Imports System.ComponentModel
Imports System.IO
Imports System.Data.SqlClient

Public Class customerinfo

    Public picpath As String
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

    Private Sub customerinfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        btnCancelShortcut.Enabled = False
        btnEditShortcut.Enabled = True
        btnSaveShortcut.Enabled = False

        Dim autocomplete As New DataTable

        autocomplete.Clear()

        stracs = "SELECT AccountNo FROM Customers"
        acscmd.Connection = acsconn
        acscmd.CommandText = stracs
        acsda.SelectCommand = acscmd
        acsda.Fill(autocomplete)

        txtAccountNo.AutoCompleteCustomSource.Clear()

        txtAccountNo.AutoCompleteMode = AutoCompleteMode.None
        txtAccountNo.AutoCompleteSource = AutoCompleteSource.None

        For x = 0 To autocomplete.Rows.Count - 1
            txtAccountNo.AutoCompleteCustomSource.Add(autocomplete.Rows(x)("AccountNo"))
        Next


        txtAccountNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        txtAccountNo.AutoCompleteSource = AutoCompleteSource.CustomSource

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Public zoneid, accountno, acctype As Integer

    Public Sub clearfields()

        txtlname.Clear()
        txtfname.Clear()
        txtmname.Clear()
        txtCompany.Clear()
        txtAddress.Clear()
        txtLandMark.Clear()
        txtContactNo.Clear()
        txtLastReading.Clear()
        txtAdvance.Clear()
        txtAveCons.Clear()
        tbMembershipfee.Clear()


        lblAccountName.ForeColor = Color.Black
        lblAddress.ForeColor = Color.Black
        lblLandMark.ForeColor = Color.Black
        lblContactNo.ForeColor = Color.Black
        lblMeterSize.ForeColor = Color.Black
        lblSequence.ForeColor = Color.Black

        txtMeterNo.Clear()
        txtSequenceNo.Clear()
        cmbZone.SelectedIndex = -1
        cmbClass.SelectedIndex = -1
        cmbMeterSize.SelectedIndex = -1
        dateCreated.Value = Now

        concespic.Image = Nothing

        lblStatus.Text = ""
        chkSenior.CheckState = CheckState.Unchecked
        chkDontCharge.CheckState = CheckState.Unchecked

        gridLedger.Rows.Clear()
        gridHistory.Rows.Clear()
        gridCharges.Rows.Clear()

    End Sub

    Public Sub lockfields()

        txtlname.ReadOnly = True
        txtfname.ReadOnly = True
        txtmname.ReadOnly = True
        txtCompany.ReadOnly = True
        txtAddress.ReadOnly = True
        txtLandMark.ReadOnly = True
        txtContactNo.ReadOnly = True
        txtMeterNo.ReadOnly = True
        txtSequenceNo.ReadOnly = True
        dateCreated.Enabled = False
        dateInstalled.Enabled = False
        concespic.Image = Nothing


        cmbZone.Enabled = False
        cmbClass.Enabled = False
        cmbMeterSize.Enabled = False

        chkSenior.Enabled = False
        chkDontCharge.Enabled = False

    End Sub

    Public Sub unlockfields()

        txtAccountNo.ReadOnly = False
        txtlname.ReadOnly = False
        txtfname.ReadOnly = False
        txtmname.ReadOnly = False
        txtCompany.ReadOnly = False
        txtAddress.ReadOnly = False
        txtLandMark.ReadOnly = False
        txtContactNo.ReadOnly = False
        txtMeterNo.ReadOnly = False
        txtSequenceNo.ReadOnly = False
        dateCreated.Enabled = False
        dateInstalled.Enabled = True

        cmbZone.Enabled = True
        cmbClass.Enabled = True
        cmbMeterSize.Enabled = True

        chkSenior.Enabled = True
        chkDontCharge.Enabled = True

    End Sub

    Public Sub loadZone()

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        sqldataZone.Clear()

        stracs = "SELECT ZoneName FROM Zone"
        acscmd.Connection = acsconn
        acscmd.CommandText = stracs
        acsda.SelectCommand = acscmd
        acsda.Fill(sqldataZone)

        cmbZone.Items.Clear()

        For i = 0 To sqldataZone.Rows.Count - 1
            cmbZone.Items.Add(sqldataZone(i)("ZoneName").ToString.ToUpper)
        Next

    End Sub

    Public Sub loadClass()

        Try
            sqldataClass.Clear()
            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try
            stracs = "SELECT Type FROM CustomerType"
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(sqldataClass)


            cmbClass.Items.Clear()

            For i = 0 To sqldataClass.Rows.Count - 1
                cmbClass.Items.Add(sqldataClass(i)("Type"))
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
            acsconn.Close()
        End Try

    End Sub

    Public Sub loadsize()

        Try

            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try

            sqldatasize.Clear()

            stracs = "select distinct MeterSize from RateSchedules group by MeterSize"
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(sqldatasize)

            cmbMeterSize.Items.Clear()

            For i = 0 To sqldatasize.Rows.Count - 1
                cmbMeterSize.Items.Add(sqldatasize(i)("MeterSize"))
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
            acsconn.Close()
        End Try

    End Sub

    Public Sub loadledgers()

        Dim accountledger As New DataTable
        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try
        stracs = "select * from AccountLedger where ledgerAccountNo = '" & txtAccountNo.Text.ToString.Replace("'", "''") & "' order by ledgerID"
        acscmd.Connection = acsconn
        acscmd.CommandText = stracs
        acsda.SelectCommand = acscmd
        acsda.Fill(accountledger)
        'accountledger.Load(acscmd.ExecuteReader())
        gridLedger.Rows.Clear()
        'gridLedger.DataSource = accountledger



        For x = 0 To accountledger.Rows.Count - 1

            If x Mod 2 = 0 Then
                gridLedger.Rows.Add(Format(Date.Parse(accountledger(x)("ledgerDate")), "MM/dd/yyyy"), accountledger(x)("ledgerRefNo"), accountledger(x)("ledgerParticulars"), accountledger(x)("ledgerReading"), accountledger(x)("ledgerConsumption"), accountledger(x)("ledgerAmount"), accountledger(x)("ledgerDiscount"), accountledger(x)("ledgerBalance"))
                gridLedger.Rows(x).DefaultCellStyle.BackColor = Color.Gainsboro

            Else
                gridLedger.Rows.Add(Format(Date.Parse(accountledger(x)("ledgerDate")), "MM/dd/yyyy"), accountledger(x)("ledgerRefNo"), accountledger(x)("ledgerParticulars"), accountledger(x)("ledgerReading"), accountledger(x)("ledgerConsumption"), accountledger(x)("ledgerAmount"), accountledger(x)("ledgerDiscount"), accountledger(x)("ledgerBalance"))
                gridLedger.Rows(x).DefaultCellStyle.BackColor = Color.White
            End If

        Next

    End Sub

    Public Sub txtAccountNo_KeyDown(sender As Object, e As KeyEventArgs) Handles txtAccountNo.KeyDown

        'connectionsettings.connection()

        If e.KeyCode = Keys.Enter Then

            clearfields()

            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try

            sqldataAccounts.Clear()

            stracs = "select * from Customers where AccountNo = '" & txtAccountNo.Text.ToString.Replace("'", "''") & "'"
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(sqldataAccounts)

            If sqldataAccounts.Rows.Count = 0 Then

                lblCreateUpdate.Visible = False
                lblCreateUpdate.Text = "Mode"
                txtAccountNo.Enabled = True
                txtAccountNo.Select()
                lockfields()
                concespic.Image = Nothing

                MsgBox("No record found.")

            Else

                If lblCreateUpdate.Text = "Update Mode" Then

                    lblCreateUpdate.Visible = True
                    lblCreateUpdate.Text = "Update Mode"
                    lblCreateUpdate.ForeColor = Color.Orange

                    txtAccountNo.ReadOnly = True
                    unlockfields()

                End If

                txtlname.Text = sqldataAccounts.Rows(0)("Lastname")
                txtfname.Text = sqldataAccounts.Rows(0)("Firstname")
                txtmname.Text = sqldataAccounts.Rows(0)("Middlename")


                If sqldataAccounts.Rows(0)("CompanyName") Is DBNull.Value Then
                    txtCompany.Text = ""
                Else
                    txtCompany.Text = sqldataAccounts.Rows(0)("CompanyName")
                End If

                txtAddress.Text = sqldataAccounts.Rows(0)("ServiceAddress")

                If sqldataAccounts.Rows(0)("LandMark") Is DBNull.Value Or IsDBNull(sqldataAccounts.Rows(0)("LandMark")) = True Then
                    txtLandMark.Text = ""
                Else
                    txtLandMark.Text = sqldataAccounts.Rows(0)("LandMark")
                End If


                If sqldataAccounts.Rows(0)("PicturePath") Is DBNull.Value Then
                    picpath = ""
                Else
                    picpath = sqldataAccounts.Rows(0)("PicturePath")
                End If


                Dim cmd As New SqlCommand
                cmd = New SqlCommand("select ConsImage from Customers where AccountNo = '" & txtAccountNo.Text.ToString.Replace("'", "''") & "'", acsconn)

                If IsDBNull(cmd.ExecuteScalar) = True Then
                Else
                    Dim imageData As Byte() = DirectCast(cmd.ExecuteScalar(), Byte())
                    If Not imageData Is Nothing Then
                        Using ms As New MemoryStream(imageData, 0, imageData.Length)
                            ms.Write(imageData, 0, imageData.Length)
                            concespic.Image = Image.FromStream(ms, True)
                        End Using
                    Else

                    End If
                End If






                'If sqldataAccounts.Rows(0)("ConsImage") Is DBNull.Value Then
                '    picpath = ""
                'Else
                '    picpath = sqldataAccounts.Rows(0)("ConsImage")
                'End If

                txtContactNo.Text = sqldataAccounts.Rows(0)("ContactNo")

                    txtMeterNo.Text = sqldataAccounts.Rows(0)("MeterNo")
                    txtSequenceNo.Text = sqldataAccounts.Rows(0)("ReadingSeqNo")
                    cmbZone.Text = sqldataAccounts.Rows(0)("Zone")
                    cmbClass.Text = sqldataAccounts.Rows(0)("RateSchedule")

                    cmbMeterSize.Text = sqldataAccounts.Rows(0)("MeterSize")
                    dateCreated.Text = Format(sqldataAccounts.Rows(0)("DateCreated"), "short date")

                    If sqldataAccounts.Rows(0)("DateInstalled") Is DBNull.Value Then
                        txtLandMark.Text = Now
                    Else
                        dateInstalled.Text = Format(sqldataAccounts.Rows(0)("DateInstalled"), "short date")
                    End If

                    lblStatus.Text = sqldataAccounts.Rows(0)("CustomerStatus")

                    Select Case sqldataAccounts.Rows(0)("CustomerStatus")
                        Case "Active"
                            lblStatus.ForeColor = Color.Green
                        Case "Don't Bill"
                            lblStatus.ForeColor = Color.Orange
                        Case "Disconnected"
                            lblStatus.ForeColor = Color.Red
                        Case "Closed"
                            lblStatus.ForeColor = Color.Black
                    End Select

                    If sqldataAccounts.Rows(0)("IsSenior") = "Yes" Then

                        chkSenior.CheckState = CheckState.Checked
                    Else
                        chkSenior.CheckState = CheckState.Unchecked

                    End If

                    If sqldataAccounts.Rows(0)("DontCharge") = "Yes" Then

                        chkDontCharge.CheckState = CheckState.Checked
                    Else
                        chkDontCharge.CheckState = CheckState.Unchecked
                    End If

                    txtLastReading.Text = sqldataAccounts.Rows(0)("LastMeterReading")
                    txtAveCons.Text = sqldataAccounts.Rows(0)("Averagee")
                txtAdvance.Text = sqldataAccounts.Rows(0)("AdvancePayment")

                tbMembershipfee.Text = sqldataAccounts.Rows(0)("Membership_balance")


                sqlDatacharges.Clear()

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    stracs = "select * from ScheduleCharges where AccountNumber = '" & txtAccountNo.Text.ToString.Replace("'", "''") & "' and ActiveInactive = 1"
                    acscmd.Connection = acsconn
                    acscmd.CommandText = stracs
                    acsda.SelectCommand = acscmd
                    acsda.Fill(sqlDatacharges)

                    gridCharges.Rows.Clear()

                    For i = 0 To sqlDatacharges.Rows.Count - 1

                        If sqlDatacharges.Rows(i)("Recurring") = "Yes" Then

                            gridCharges.Rows.Add(sqlDatacharges.Rows(i)("Particular"), sqlDatacharges.Rows(i)("Amount"), "Recurring")
                        Else

                            gridCharges.Rows.Add(sqlDatacharges.Rows(i)("Particular"), sqlDatacharges.Rows(i)("Amount"), MonthName(sqlDatacharges.Rows(i)("BillingMonth")) & " - " & sqlDatacharges.Rows(i)("BillingYear"))

                        End If

                    Next

                'Dim historyds As New DataSet
                Dim historyfieldfind As New DataTable
                    Dim historyhighcons As New DataTable
                    sqlDatahistory.Clear()

                'historyds.Tables.Add("Table1")
                'historyds.Tables(0).Columns.Add("historyDate", GetType(Date))
                'historyds.Tables(0).Columns.Add("historyName", GetType(String))

                history.Tables(0).Rows.Clear()

                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    stracs = "select historyDate, historyName from AccountHistory where historyAccountNo = '" & txtAccountNo.Text.ToString.Replace("'", "''") & "'"
                    acscmd.Connection = acsconn
                    acscmd.CommandText = stracs
                    acsda.SelectCommand = acscmd
                    acsda.Fill(sqlDatahistory)

                    For l = 0 To sqlDatahistory.Rows.Count - 1

                    history.Tables(0).Rows.Add(Format(sqlDatahistory.Rows(l)("historyDate"), "yyyy/MM/dd"), sqlDatahistory.Rows(l)("historyName"))

                Next

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    stracs = "select CAST(TimeRead AS DATE) AS TimeRead, Finding from Findings where AccountNumber = '" & txtAccountNo.Text.ToString.Replace("'", "''") & "'"
                    acscmd.Connection = acsconn
                    acscmd.CommandText = stracs
                    acsda.SelectCommand = acscmd
                    acsda.Fill(historyfieldfind)

                    For x = 0 To historyfieldfind.Rows.Count - 1

                    history.Tables(0).Rows.Add(Format(historyfieldfind.Rows(x)("TimeRead"), "yyyy/MM/dd"), historyfieldfind.Rows(x)("Finding"))

                Next

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                stracs = "select CAST(TimeRead AS DATE) AS TimeRead, Consumption, Remarks from MeterReadingReport where AccountNumber = '" & txtAccountNo.Text.ToString.Replace("'", "''") & "'"
                acscmd.Connection = acsconn
                    acscmd.CommandText = stracs
                    acsda.SelectCommand = acscmd
                    acsda.Fill(historyhighcons)

                    For i = 0 To historyhighcons.Rows.Count - 1

                    history.Tables(0).Rows.Add(Format(historyhighcons.Rows(i)("TimeRead"), "yyyy/MM/dd"), historyhighcons.Rows(i)("Remarks") & " - " & historyhighcons.Rows(i)("Consumption"))

                Next

                history.Tables(0).DefaultView.Sort = "historyDate ASC"
                Dim asd2 = history.Tables(0).Select("", "historyDate ASC")

                gridHistory.Rows.Clear()

                    For Each row In asd2

                    gridHistory.Rows.Add(Format(row("historyDate"), "short date"), row("historyName"))

                Next

                    'If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    'stracs = "select * from AccountHistory where historyAccountNo = '" & txtAccountNo.Text.ToString.Replace("'", "''") & "' order by historyID"
                    'acscmd.Connection = acsconn
                    'acscmd.CommandText = stracs
                    'acsda.SelectCommand = acscmd
                    'acsda.Fill(sqlDatahistory)

                    'gridHistory.Rows.Clear()

                    'For i = 0 To sqlDatahistory.Rows.Count - 1

                    '    gridHistory.Rows.Add(Format(sqlDatahistory.Rows(i)("historyDate"), "MM/dd/yyyy"), sqlDatahistory.Rows(i)("historyCategory").ToString.ToUpper & " - " & sqlDatahistory.Rows(i)("historyName"))

                    'Next

                    Try
                        gridHistory.FirstDisplayedScrollingRowIndex = gridHistory.RowCount - 1

                    Catch ex As Exception

                    End Try


                    'Try

                    '    Dim img As Image
                    '    Dim br As New IO.BinaryReader(IO.File.Open(My.Settings.TempPicPath & "\" & txtAccountNo.Text & ".jpg", IO.FileMode.Open), System.Text.Encoding.Default)
                    '    img = Image.FromStream(br.BaseStream)
                    '    br.Close()
                    '    br = Nothing
                    '    concespic.Image = img.Clone


                    'Catch ex As Exception

                    'End Try




                End If

                loadledgers()

            Try
                gridLedger.FirstDisplayedScrollingRowIndex = gridLedger.RowCount - 1

            Catch ex As Exception

            End Try

            gridCharges.ClearSelection()
            gridHistory.ClearSelection()
            txtAccountNo.Select()


            'Try


            '    Dim img As Image
            '    Dim br As New IO.BinaryReader(IO.File.Open(My.Settings.TempPicPath & txtAccountNo.Text & ".jpg", IO.FileMode.Open), System.Text.Encoding.Default)
            '    img = Image.FromStream(br.BaseStream)
            '    br.Close()
            '    br = Nothing
            '    concespic.Image = img.Clone
            '    picpath = My.Settings.PicPath & txtAccountNo.Text & ".jpg"
            'Catch ex As Exception

            'End Try




        End If
    End Sub

    Private Sub concessionaireaccounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        loadZone()
        loadClass()
        loadsize()

        gridCharges.ContextMenuStrip = cmsCharges
        'gridLedger.ContextMenuStrip = cmsLedger
        lblStatus.ContextMenuStrip = cmsStatus
        'txtLastReading.ContextMenuStrip = cmsMeterReset
        txtAccountNo.Select()

    End Sub

    Private Sub accSearch_Click(sender As Object, e As EventArgs) Handles accSearch.Click

        If lblCreateUpdate.Text = "Update Mode" Or lblCreateUpdate.Text = "Create Account" Then

        Else
            SearchAccount.searchingform = "Accounts"
            SearchAccount.ShowDialog()

        End If



    End Sub

    Private Sub cmbClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbClass.SelectedIndexChanged

        'If acsconn.State = ConnectionState.Closed Then acsconn.Open()

        'sqldatasize.Clear()

        'stracs = "select MeterSize from RateSchedules where CustomerType = '" & cmbClass.Text & "'"
        'acscmd.Connection = acsconn
        'acscmd.CommandText = stracs
        'acsda.SelectCommand = acscmd
        'acsda.Fill(sqldatasize)

        'cmbMeterSize.Items.Clear()

        'For i = 0 To sqlData1.Rows.Count - 1
        '    cmbMeterSize.Items.Add(sqldatasize(i)("MeterSize"))
        'Next

        loadsize()

        If lblCreateUpdate.Text = "Create Account" Then
            accountnumbergenerator()
        End If

    End Sub

    Private Sub cmbZone_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbZone.SelectedIndexChanged

        If lblCreateUpdate.Text = "Create Account" Then
            accountnumbergenerator()
        End If

    End Sub

    Private Sub accountnumbergenerator()

        Try

            sqlData1.Clear()
            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try
            stracs = "SELECT ZoneID, LastNumber FROM Zone WHERE ZoneName = '" & cmbZone.Text & "'" 'zone number
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(sqlData1)

            zoneid = sqlData1.Rows(0)("ZoneID")
            accountno = sqlData1.Rows(0)("LastNumber")

            sqlData1.Clear()
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            stracs = "SELECT CustomerTypeID FROM CustomerType WHERE Type = '" & cmbClass.Text & "'" 'connection type
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(sqlData1)

            acctype = sqlData1.Rows(0)("CustomerTypeID")

            txtAccountNo.Text = Format(zoneid, "00") & "-" & Format(Val(accountno), "00000") & "-" & acctype

        Catch ex As Exception
            acsconn.Close()
        End Try
    End Sub

    Public Sub savingaccount()

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        If txtAccountNo.Text = "" Then
        Else

            If lblCreateUpdate.Text = "Create Account" Then

                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                stracs = "select AccountNo from Customers where AccountNo = '" & txtAccountNo.Text.ToString.Replace("'", "''") & "'"
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsdr = acscmd.ExecuteReader

                If acsdr.Read = True Then
                    acsdr.Close()

                    MsgBox("Account number already used. Please contact admin.")

                Else

                    acsdr.Close()

                    If txtlname.Text = "" Or txtfname.Text = "" Or txtAddress.Text = "" Or txtLandMark.Text = "" Or txtContactNo.Text = "" Or txtSequenceNo.Text = "" Or cmbMeterSize.Text = "" Then

                        incompletefields()

                        MsgBox("Please complete the account information.")

                    Else

                        Dim intValue As Integer
                        If Integer.TryParse(txtSequenceNo.Text, intValue) AndAlso intValue > 0 Then

                            Dim senioryn, dontcharge As String

                            If chkSenior.CheckState = CheckState.Checked Then
                                senioryn = "Yes"
                            Else
                                senioryn = "No"
                            End If

                            If chkDontCharge.CheckState = CheckState.Checked Then
                                dontcharge = "Yes"
                            Else
                                dontcharge = "No"
                            End If


                            If concespic.BackgroundImage Is Nothing Then

                                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                stracs = "insert into Customers ([AccountNo],[Lastname],[Firstname],[Middlename],[ServiceAddress]" _
                                & ",[ContactNo],[Averagee],[ReadingSeqNo],[CustomerStatus],[IsSenior],[Zone],[RateSchedule]" _
                                & ",[DateCreated],[MeterNo],[DontCharge],[CompanyName],[CreatedBy],[LastMeterReading],[LandMark],
                                [MeterSize],[AdvancePayment],[DateInstalled],[LasReadingDate], PicturePath, Membership_balance) values (" _
                                & "'" & txtAccountNo.Text & "', " _
                                & "'" & txtlname.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "'" & txtfname.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "'" & txtmname.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "'" & txtAddress.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "'" & txtContactNo.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & txtAveCons.Text & ", " & txtSequenceNo.Text & ", 'Closed', '" & senioryn & "', " _
                                & "'" & cmbZone.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "'" & cmbClass.Text.ToString.Replace("'", "''") & "', " _
                                & "'" & Format(dateCreated.Value, "yyyy-MM-dd") & "', " _
                                & "'" & txtMeterNo.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "'" & dontcharge & "', " _
                                & "'" & txtCompany.Text.ToString.Replace("'", "''").ToUpper & "', '" & My.Settings.Nickname & "', " _
                                & txtLastReading.Text & ", '" & txtLandMark.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "'" & cmbMeterSize.Text.ToString.Replace("'", "''").ToUpper & "', 0, '" & Format(dateInstalled.Value, "yyyy-MM-dd") & "', '" _
                                & Format(dateInstalled.Value, "yyyy-MM-dd") & "', '" _
                                & My.Settings.PicPath & txtAccountNo.Text & ".jpg" & "', 1500.00)"

                                acscmd.Connection = acsconn
                                acscmd.CommandText = stracs
                                acscmd.ExecuteNonQuery()
                                acscmd.Dispose()

                            Else

                                stracs = "insert into Customers ([AccountNo],[Lastname],[Firstname],[Middlename],[ServiceAddress]" _
                                & ",[ContactNo],[Averagee],[ReadingSeqNo],[CustomerStatus],[IsSenior],[Zone],[RateSchedule]" _
                                & ",[DateCreated],[MeterNo],[DontCharge],[CompanyName],[CreatedBy],[LastMeterReading],[LandMark],
                                [MeterSize],[AdvancePayment],[DateInstalled],[LasReadingDate], PicturePath, ConsImage, Membership_balance) values (" _
                                & "'" & txtAccountNo.Text & "', " _
                                & "'" & txtlname.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "'" & txtfname.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "'" & txtmname.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "'" & txtAddress.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "'" & txtContactNo.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & txtAveCons.Text & ", " & txtSequenceNo.Text & ", 'Closed', '" & senioryn & "', " _
                                & "'" & cmbZone.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "'" & cmbClass.Text.ToString.Replace("'", "''") & "', " _
                                & "'" & Format(dateCreated.Value, "yyyy-MM-dd") & "', " _
                                & "'" & txtMeterNo.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "'" & dontcharge & "', " _
                                & "'" & txtCompany.Text.ToString.Replace("'", "''").ToUpper & "', '" & My.Settings.Nickname & "', " _
                                & txtLastReading.Text & ", '" & txtLandMark.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "'" & cmbMeterSize.Text.ToString.Replace("'", "''").ToUpper & "', 0, '" & Format(dateInstalled.Value, "yyyy-MM-dd") & "', '" _
                                & Format(dateInstalled.Value, "yyyy-MM-dd") & "', '" _
                                & My.Settings.PicPath & txtAccountNo.Text & ".jpg" & "', 1500.00)"



                                Dim ms As New MemoryStream()
                                concespic.BackgroundImage.Save(ms, concespic.BackgroundImage.RawFormat)
                                Dim data As Byte() = ms.GetBuffer()
                                Dim p As New SqlParameter("ConsImage", SqlDbType.Image)
                                p.Value = data
                                acscmd.Connection = acsconn
                                acscmd.CommandText = stracs
                                acscmd.Parameters.Add(p)
                                acscmd.ExecuteNonQuery()
                                acscmd.Dispose()


                                acscmd.Connection = acsconn
                                acscmd.CommandText = stracs
                                acscmd.ExecuteNonQuery()
                                acscmd.Dispose()


                            End If

                            ''delete old conces image
                            'Try
                            '    System.IO.File.Delete(My.Settings.PicPath & txtAccountNo.Text & ".jpg")
                            'Catch ex As Exception

                            'End Try

                            ''Save New conces image
                            'Try

                            '    If concespic.Image IsNot Nothing Then
                            '        concespic.Image.Save(My.Settings.PicPath & txtAccountNo.Text & ".jpg")

                            '    End If

                            'Catch ex As Exception

                            'End Try

                            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                            stracs = "update Zone Set LastNumber = LastNumber + 1 where ZoneName = '" & cmbZone.Text & "'"
                            acscmd.Connection = acsconn
                            acscmd.CommandText = stracs
                            acscmd.ExecuteNonQuery()
                            acscmd.Dispose()

                            txtAccountNo.ReadOnly = False
                            lockfields()

                            lblCreateUpdate.Visible = False
                            lblCreateUpdate.Text = "Mode"

                            MsgBox("New account saved.")

                            btnCreateShortcut.Enabled = True
                            btnCancelShortcut.Enabled = False
                            btnSaveShortcut.Enabled = False
                            btnEditShortcut.Enabled = True
                        Else

                            MessageBox.Show("You have entered a negative number or not a whole number.")
                            lblSequence.ForeColor = Color.Red
                            txtSequenceNo.Select()

                        End If

                    End If

                End If

            End If

            If lblCreateUpdate.Text = "Update Mode" Then

                If txtlname.Text = "" Or txtfname.Text = "" Or txtAddress.Text = "" Or txtLandMark.Text = "" Or txtContactNo.Text = "" Or txtSequenceNo.Text = "" Or cmbMeterSize.Text = "" Or cmbZone.Text = "" Then

                    incompletefields()

                    MsgBox("Please complete the account information.")

                Else

                    Dim intValue As Integer
                    If Integer.TryParse(txtSequenceNo.Text, intValue) AndAlso intValue > 0 Then

                        Dim senioryn, dontcharge As String

                        If chkSenior.CheckState = CheckState.Checked Then
                            senioryn = "Yes"
                        Else
                            senioryn = "No"
                        End If

                        If chkDontCharge.CheckState = CheckState.Checked Then
                            dontcharge = "Yes"
                        Else
                            dontcharge = "No"
                        End If

                        If concespic.Image Is Nothing Then

                            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                            stracs = "update Customers set [Lastname] = '" & txtlname.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "[Firstname] = '" & txtfname.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "[Middlename] = '" & txtmname.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "[ServiceAddress] = '" & txtAddress.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "[ContactNo] = '" & txtContactNo.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "[ReadingSeqNo] = " & txtSequenceNo.Text & ", " _
                                & "[IsSenior] = '" & senioryn & "', " _
                                & "[Zone] = '" & cmbZone.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "[RateSchedule] = '" & cmbClass.Text.ToString.Replace("'", "''") & "', " _
                                & "[DateInstalled] = '" & Format(dateInstalled.Value, "yyyy-MM-dd") & "', " _
                                & "[MeterNo] = '" & txtMeterNo.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "[DontCharge] = '" & dontcharge & "', " _
                                & "[CompanyName] = '" & txtCompany.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "[LandMark] = '" & txtLandMark.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "[MeterSize] = '" & cmbMeterSize.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "PicturePath = '" & picpath & "' " _
                                & "where AccountNo = '" & txtAccountNo.Text.ToString.Replace("'", "''") & "'"

                            acscmd.Connection = acsconn
                            acscmd.CommandText = stracs
                            acscmd.ExecuteNonQuery()
                            acscmd.Dispose()

                        Else



                            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                            stracs = "update Customers set [Lastname] = '" & txtlname.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "[Firstname] = '" & txtfname.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "[Middlename] = '" & txtmname.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "[ServiceAddress] = '" & txtAddress.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "[ContactNo] = '" & txtContactNo.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "[ReadingSeqNo] = " & txtSequenceNo.Text & ", " _
                                & "[IsSenior] = '" & senioryn & "', " _
                                & "[Zone] = '" & cmbZone.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "[RateSchedule] = '" & cmbClass.Text.ToString.Replace("'", "''") & "', " _
                                & "[DateInstalled] = '" & Format(dateInstalled.Value, "yyyy-MM-dd") & "', " _
                                & "[MeterNo] = '" & txtMeterNo.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "[DontCharge] = '" & dontcharge & "', " _
                                & "[CompanyName] = '" & txtCompany.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "[LandMark] = '" & txtLandMark.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "[MeterSize] = '" & cmbMeterSize.Text.ToString.Replace("'", "''").ToUpper & "', " _
                                & "PicturePath = '" & picpath & "', " _
                                & "ConsImage = @ConsImages " _
                                & "where AccountNo = '" & txtAccountNo.Text.ToString.Replace("'", "''") & "'"

                            Dim ms As New MemoryStream()
                            concespic.Image.Save(ms, concespic.Image.RawFormat)
                            Dim data As Byte() = ms.GetBuffer()
                            Dim p As New SqlParameter("@ConsImages", SqlDbType.Image)
                            p.Value = data

                            acscmd.Connection = acsconn
                            acscmd.CommandText = stracs
                            acscmd.Parameters.Add(p)
                            acscmd.ExecuteNonQuery()
                            acscmd.Dispose()
                            acscmd.Parameters.Clear()

                        End If





                        'delete old conces image
                        Try
                            System.IO.File.Delete(picpath)
                        Catch ex As Exception

                        End Try

                        'Save New conces image
                        Try

                            If concespic.Image IsNot Nothing Then
                                concespic.Image.Save(picpath)

                            End If

                        Catch ex As Exception

                        End Try

                        updatedetailsrecord()

                        txtAccountNo.Enabled = True
                        txtAccountNo.Select()
                        lockfields()

                        lblCreateUpdate.Visible = False
                        lblCreateUpdate.Text = "Mode"

                        txtAccountNo_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))
                        MsgBox("Account updated.")

                        btnCreateShortcut.Enabled = True
                        btnCancelShortcut.Enabled = False
                        btnSaveShortcut.Enabled = False
                        btnEditShortcut.Enabled = True

                    Else
                        MessageBox.Show("You have entered either a negative number or not an integer.")
                        lblSequence.ForeColor = Color.Red
                        txtSequenceNo.Select()
                    End If

                End If

            End If
            txtAccountNo.Enabled = True
            txtAccountNo.ReadOnly = False
        End If

    End Sub

    Sub incompletefields()

        If cmbMeterSize.Text = "" Then
            lblMeterSize.ForeColor = Color.Red
            cmbMeterSize.Select()
        Else
            lblMeterSize.ForeColor = Color.Black
        End If

        If txtSequenceNo.Text = "" Then
            lblSequence.ForeColor = Color.Red
            txtSequenceNo.Select()
        Else
            lblSequence.ForeColor = Color.Black
        End If

        If txtContactNo.Text = "" Then
            lblContactNo.ForeColor = Color.Red
            txtContactNo.Select()
        Else
            lblContactNo.ForeColor = Color.Black
        End If

        If txtLandMark.Text = "" Then
            lblLandMark.ForeColor = Color.Red
            txtLandMark.Select()
        Else
            lblLandMark.ForeColor = Color.Black
        End If

        If txtAddress.Text = "" Then
            lblAddress.ForeColor = Color.Red
            txtAddress.Select()
        Else
            lblAddress.ForeColor = Color.Black
        End If

        If txtfname.Text = "" Then
            lblAccountName.ForeColor = Color.Red
            txtfname.Select()
        Else
            lblAccountName.ForeColor = Color.Black
        End If

        If txtlname.Text = "" Then
            lblAccountName.ForeColor = Color.Red
            txtlname.Select()
        Else
            lblAccountName.ForeColor = Color.Black
        End If

    End Sub

    Private Sub gridCharges_DoubleClick(sender As Object, e As EventArgs) Handles gridCharges.DoubleClick

    End Sub

    Public Sub addcharges()

        If txtAccountNo.Text = "" Then
        Else

            Dim searchaccount As New DataTable
            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try

            searchaccount.Clear()

            stracs = "select * from Customers where AccountNo = '" & txtAccountNo.Text.ToString.Replace("'", "''") & "'"
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(searchaccount)

            If searchaccount.Rows.Count = 0 Then

                MsgBox("Record not found.")

            Else
                customercharges.ShowDialog()
            End If

        End If

    End Sub

    Private Sub ToolStripMenuItem17_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem17.Click

        addcharges()


    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click

        If txtAccountNo.Text = "" Then
        Else

            If sqldataAccounts.Rows.Count = 0 Then
            Else
                updateLedger.accountNo.Text = sqldataAccounts.Rows(0)("AccountNo")
                updateLedger.ShowDialog()
            End If



        End If



    End Sub

    Public Sub updatestat()

        If txtAccountNo.Text = "" Then
        Else

            Dim searchaccount As New DataTable
            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try

            searchaccount.Clear()

            stracs = "select * from Customers where AccountNo = '" & txtAccountNo.Text.ToString.Replace("'", "''") & "'"
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(searchaccount)

            If searchaccount.Rows.Count = 0 Then

                MsgBox("Record not found.")

            Else
                updatestatus.ShowDialog()
            End If

        End If

    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles cmsupdatestatus.Click

        If txtAccountNo.Text = "" Then

            MsgBox("Account number empty.")

        Else

            updatestat()



        End If




    End Sub

    Private Sub cmsStatus_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmsStatus.Opening

        If txtAccountNo.Text = "" Then

        Else

            If lblStatus.Text = "Disconnected" Then

                Dim searchaccount As New DataTable
                Try
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                Catch ex As Exception
                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                End Try

                searchaccount.Clear()

                stracs = "select DateLastDisconnected from Customers where AccountNo = '" & txtAccountNo.Text.ToString.Replace("'", "''") & "'"
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsda.SelectCommand = acscmd
                acsda.Fill(searchaccount)

                If searchaccount.Rows.Count = 0 Then

                Else

                    If Date.Parse(searchaccount.Rows(0)("DateLastDisconnected")).AddMonths(5) < Date.Parse(Now) Then

                        Dim searchbil, others As New DataTable

                        stracs = "select * from Bills where AccountNumber = '" & txtAccountNo.Text.ToString.Replace("'", "''") & "' and IsPaid = 'No'"
                        acscmd.Connection = acsconn
                        acscmd.CommandText = stracs
                        acsda.SelectCommand = acscmd
                        acsda.Fill(searchbil)

                        stracs = "select * from AddAdjustment where AccountNumber = '" & txtAccountNo.Text.ToString.Replace("'", "''") & "' and Paid = 'No'"
                        acscmd.Connection = acsconn
                        acscmd.CommandText = stracs
                        acsda.SelectCommand = acscmd
                        acsda.Fill(others)

                        If searchbil.Rows.Count = 0 And others.Rows.Count = 0 Then

                        Else
                            additionalpenalty.Visible = True
                        End If

                    Else

                    End If

                End If

            Else

            End If

        End If

    End Sub


    Public Sub addpen()

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try
        Dim asd As DialogResult = MessageBox.Show("Are you sure you want to add addtional penalty?", "Electronic Billing and Collection", MessageBoxButtons.YesNo)

        If asd = DialogResult.Yes Then

            Dim searchmaxbill As New DataTable
            stracs = "select max(Billno) as Billno from Bills where AccountNumber = '" & txtAccountNo.Text & "' and IsPaid = 'No' and IsCollectionCreated = 'No' 
                        and isPromisorry = 'No' and Cancelled = 'No'"
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(searchmaxbill)

            If IsDBNull(searchmaxbill.Rows(0)("Billno")) = True Or searchmaxbill.Rows.Count = 0 Then

                Dim charges, pnsum, addpen As Double

                Dim maxid As New DataTable
                stracs = "select max(ID) as maxid from AddAdjustment where AccountNumber = '" & txtAccountNo.Text & "' and Paid = 'No' and IsCollectionCreated = 'No'"
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsda.SelectCommand = acscmd
                acsda.Fill(maxid)

                Dim coumputepenaltypn As New DataTable
                stracs = "select SUM(Billing) + Sum(Penalty) as Billing from AddAdjustment where 
                        AccountNumber = '" & txtAccountNo.Text & "' and Paid = 'No' and IsCollectionCreated = 'No' and Status = 'Posted'"
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsda.SelectCommand = acscmd
                acsda.Fill(coumputepenaltypn)

                If IsDBNull(coumputepenaltypn.Rows(0)("Billing")) = True Then
                    pnsum = 0
                Else
                    pnsum = coumputepenaltypn.Rows(0)("Billing")
                End If


                Dim loadchargesarrears As New DataTable
                loadchargesarrears.Clear()
                'stracs = "select a.Billno as Billno, a.BillingDate as BillingDate, (SUM(b.amount) + SUM(a.AmountDue) + SUM(a.PenaltyAfterDue)) - SUM(a.Discount) as amount from Bills a join BillCharges b on a.BillNo = b.BillNumber where a.IsPaid = 'No' and b.IsPaid = 'No' and a.BillNo = " & getarrears.Rows(t)("BillNo") & " and b.BillNumber = " & getarrears.Rows(t)("BillNo") & " group by a.BillNo, a.BillingDate"
                stracs = "select SUM(Amount) as Amount from BillCharges where IsPaid = 'No' and Cancelled = 'No' and 
                        AccountNumber = '" & txtAccountNo.Text & "' and Status = 'Posted' and Category = 'Others'
                        and IsCollectionCreated = 'No' and isPromisorry = 'No'"
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsda.SelectCommand = acscmd
                acsda.Fill(loadchargesarrears)

                If IsDBNull(loadchargesarrears.Rows(0)("Amount")) = True Then
                    charges = 0
                Else
                    charges = loadchargesarrears.Rows(0)("Amount")
                End If

                addpen = pnsum * 0.05

                stracs = "update AddAdjustment set Penalty = " & addpen & " where ID = " & maxid.Rows(0)("maxid")
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                stracs = "insert into AccountLedger (ledgerAccountNo, ledgerDate, ledgerRefNo, ledgerParticulars, ledgerReading, ledgerConsumption, ledgerDiscount, ledgerAmount, ledgerBalance) values ('" _
                        & txtAccountNo.Text & "', '" _
                        & Format(Date.Parse(Now), "yyyy-MM-dd") & "', '" _
                        & searchmaxbill.Rows(0)("Billno") & "', '" _
                        & "Additional Penalty', '" _
                        & "', '" _
                        & "', '" _
                        & "', '" _
                        & Format(addpen, "standard") & "', '" _
                        & Format(Val(pnsum) + Val(charges) + Val(addpen), "Standard") & "')"

                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                loadledgers()

            Else

                Dim amountpen As Double
                Dim coumputepenalty As New DataTable
                stracs = "select (SUM(AmountDue) + Sum(PenaltyAfterDue)) - (SUM(AdvancePayment) + SUM(Discount)) as AmountDue, Sum(PenaltyAfterDue) as PenaltyAfterDue 
                        from Bills where  AccountNumber = '" & txtAccountNo.Text & "' and IsPaid = 'No' and 
                        IsCollectionCreated = 'No' and isPromisorry = 'No' and Cancelled = 'No' and BillStatus = 'Posted'"
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsda.SelectCommand = acscmd
                acsda.Fill(coumputepenalty)

                If IsDBNull(coumputepenalty.Rows(0)) = True Then
                    amountpen = 0
                Else
                    amountpen = (coumputepenalty.Rows(0)("AmountDue"))
                End If

                Dim coumputepenaltypn As New DataTable
                stracs = "select SUM(Billing) + Sum(Penalty) as Billing from AddAdjustment where 
                        AccountNumber = '" & txtAccountNo.Text & "' and Paid = 'No' and IsCollectionCreated = 'No' and Status = 'Posted'"
                acscmd.Connection = acsconn
                acscmd.CommandText = stracs
                acsda.SelectCommand = acscmd
                acsda.Fill(coumputepenaltypn)

                If IsDBNull(coumputepenaltypn.Rows(0)("Billing")) = True Then
                    amountpen = amountpen + 0
                Else
                    amountpen = amountpen + coumputepenaltypn.Rows(0)("Billing")
                End If

                If amountpen = 0 Then
                Else
                    Dim addpen, charges As Double

                    addpen = amountpen * 0.05

                    Dim penalty As Double

                    If IsDBNull(coumputepenalty.Rows(0)("PenaltyAfterDue")) = True Then
                        penalty = 0
                    Else
                        penalty = coumputepenalty.Rows(0)("PenaltyAfterDue")
                    End If

                    stracs = "Update Bills set PenaltyAfterDue = PenaltyAfterDue + " & Double.Parse(addpen) & " where BillNo = " & searchmaxbill.Rows(0)("Billno")
                    acscmd.Connection = acsconn
                    acscmd.CommandText = stracs
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                    Dim loadchargesarrears As New DataTable
                    loadchargesarrears.Clear()
                    'stracs = "select a.Billno as Billno, a.BillingDate as BillingDate, (SUM(b.amount) + SUM(a.AmountDue) + SUM(a.PenaltyAfterDue)) - SUM(a.Discount) as amount from Bills a join BillCharges b on a.BillNo = b.BillNumber where a.IsPaid = 'No' and b.IsPaid = 'No' and a.BillNo = " & getarrears.Rows(t)("BillNo") & " and b.BillNumber = " & getarrears.Rows(t)("BillNo") & " group by a.BillNo, a.BillingDate"
                    stracs = "select SUM(Amount) as Amount from BillCharges where IsPaid = 'No' and Cancelled = 'No' and 
                        AccountNumber = '" & txtAccountNo.Text & "' and Status = 'Posted' and Category = 'Others'
                        and IsCollectionCreated = 'No'"
                    acscmd.Connection = acsconn
                    acscmd.CommandText = stracs
                    acsda.SelectCommand = acscmd
                    acsda.Fill(loadchargesarrears)

                    If IsDBNull(loadchargesarrears.Rows(0)("Amount")) = True Then
                        charges = 0
                    Else
                        charges = loadchargesarrears.Rows(0)("Amount")
                    End If

                    stracs = "insert into AccountLedger (ledgerAccountNo, ledgerDate, ledgerRefNo, ledgerParticulars, ledgerReading, ledgerConsumption, ledgerDiscount, ledgerAmount, ledgerBalance) values ('" _
                        & txtAccountNo.Text & "', '" _
                        & Format(Date.Parse(Now), "yyyy-MM-dd") & "', '" _
                        & searchmaxbill.Rows(0)("Billno") & "', '" _
                        & "Additional Penalty', '" _
                        & "', '" _
                        & "', '" _
                        & "', '" _
                        & Format(addpen, "standard") & "', '" _
                        & Format(Val(amountpen) + Val(charges) + Val(addpen), "Standard") & "')"

                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()
                    acscmd.Dispose()

                    loadledgers()

                End If

            End If


        Else

        End If

    End Sub

    Private Sub additionalpenalty_Click(sender As Object, e As EventArgs) Handles additionalpenalty.Click

        If txtAccountNo.Text = "" Then

            MsgBox("Account number empty.")
        Else

            waitingapproval.trans = "addpen"
            waitingapproval.ShowDialog()

            waitingapproval.TextBox1.Select()
            waitingapproval.TextBox1.Clear()

        End If

    End Sub

    Private Sub ToolStripMenuItem2_Click_1(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click

        meterreset.accountNo.Text = txtAccountNo.Text
        meterreset.ShowDialog()

    End Sub

    Private Sub txtLastReading_MouseDown(sender As Object, e As MouseEventArgs) Handles txtLastReading.MouseDown

        If e.Button = MouseButtons.Right Then

            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try

            Dim searchaccount As New DataTable
            searchaccount.Clear()
            stracs = "select * from Customers where AccountNo = '" & txtAccountNo.Text.Replace("'", "''") & "'"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(searchaccount)

            If searchaccount.Rows.Count = 0 Then
                txtLastReading.ContextMenuStrip = Nothing
            Else
                meterreset.resetfrom.Text = searchaccount.Rows(0)("LastMeterReading")
                txtLastReading.ContextMenuStrip = cmsMeterReset
            End If

        Else



        End If

    End Sub

    Private Sub tbMembershipfee_MouseDown(sender As Object, e As MouseEventArgs) Handles tbMembershipfee.MouseDown

        If e.Button = MouseButtons.Right Then

            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try

            If txtAccountNo.Text <> "" Then
                adjustmembershipfee.LoadMembershipAdjustment(txtAccountNo.Text)

                adjustmembershipfee.ShowDialog()
                adjustmembershipfee.BringToFront()

            Else



            End If
        End If


    End Sub


    Public Sub createnew()

        clearfields()
        unlockfields()

        txtAccountNo.Clear()
        txtAccountNo.Enabled = False

        lblStatus.Text = "Closed"
        lblStatus.ForeColor = Color.Black

        txtLastReading.Text = "0"
        txtAveCons.Text = "0"
        txtAdvance.Text = "0"
        dateCreated.Text = Now

        lblCreateUpdate.Visible = True
        lblCreateUpdate.Text = "Create Account"
        lblCreateUpdate.ForeColor = Color.Green

        btnCreateShortcut.Enabled = False
        btnEditShortcut.Enabled = False
        btnCancelShortcut.Enabled = True
        btnSaveShortcut.Enabled = True

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Me.Activate()
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            savingaccount()

        Else
            MsgBox("Your account cannot perform this process.")
        End If

    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click

        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            createnew()
        Else
            MsgBox("Your account cannot perform this process.")
        End If

    End Sub

    Private Sub btnCreateShortcut_Click(sender As Object, e As EventArgs) Handles btnCreateShortcut.Click
        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            createnew()
        Else
            MsgBox("Your account cannot perform this process.")
        End If
    End Sub

    Private Sub btnCancelShortcut_Click(sender As Object, e As EventArgs) Handles btnCancelShortcut.Click
        clearfields()
        lockfields()

        txtAccountNo.Clear()
        txtAccountNo.Enabled = True
        txtAccountNo.Select()

        lblCreateUpdate.Visible = False
        lblCreateUpdate.Text = "Mode"
        lblCreateUpdate.ForeColor = Color.Black

        btnCreateShortcut.Enabled = True
        btnCancelShortcut.Enabled = False
        btnSaveShortcut.Enabled = False
        btnEditShortcut.Enabled = True
    End Sub

    Private Sub btnSaveShortcut_Click(sender As Object, e As EventArgs) Handles btnSaveShortcut.Click
        If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
            savingaccount()

        Else
            MsgBox("Your account cannot perform this process.")
        End If
    End Sub

    Private Sub btnEditShortcut_Click(sender As Object, e As EventArgs) Handles btnEditShortcut.Click
        If txtAccountNo.Text = "" Then

            MsgBox("Account number empty.")
        Else
            waitingapproval.trans = "accountinfo"
            waitingapproval.ShowDialog()

            waitingapproval.TextBox1.Select()
            waitingapproval.TextBox1.Clear()

        End If
    End Sub

    Private Sub UpdateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateToolStripMenuItem.Click

        'If My.Settings.Admin = "Yes" Or My.Settings.Cservice = "Yes" Then
        '    updateaccount()
        'Else
        '    MsgBox("Your account cannot perform this process.")
        'End If

        If txtAccountNo.Text = "" Then

            MsgBox("Account number empty.")
        Else
            waitingapproval.trans = "accountinfo"
            waitingapproval.ShowDialog()

            waitingapproval.TextBox1.Select()
            waitingapproval.TextBox1.Clear()
        End If

    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        clearfields()
        lockfields()

        txtAccountNo.Clear()
        txtAccountNo.Enabled = True
        txtAccountNo.Select()

        lblCreateUpdate.Visible = False
        lblCreateUpdate.Text = "Mode"
        lblCreateUpdate.ForeColor = Color.Black
    End Sub

    Private Sub FindToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FindToolStripMenuItem.Click
        accSearch_Click(Nothing, New KeyEventArgs(Keys.Enter))
        'accSearch_Click(Nothing, New EventArgs(eve.Enter))
    End Sub

    Public Sub updateaccount()

        'lblCreateUpdate.Visible = True
        lblCreateUpdate.Text = "Update Mode"
        'lblCreateUpdate.ForeColor = Color.Orange

        txtAccountNo_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))

        btnCreateShortcut.Enabled = False
        btnCancelShortcut.Enabled = True
        btnSaveShortcut.Enabled = True
        btnEditShortcut.Enabled = False

    End Sub

    Private Sub concespic_Click(sender As Object, e As EventArgs) Handles concespic.Click

        If lblCreateUpdate.Text = "Update Mode" Or lblCreateUpdate.Text = "Create Account" Then
            Camera.ShowDialog()
            Camera.BringToFront()

        End If

    End Sub

    Private Sub customerinfo_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub updatedetailsrecord()

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try


        If txtfname.Text.ToString.Replace("'", "''") & " " & txtmname.Text.ToString.Replace("'", "''") & " " & txtlname.Text.ToString.Replace("'", "''") =
            sqldataAccounts.Rows(0)("Firstname").ToString.Replace("'", "''") & " " _
            & sqldataAccounts.Rows(0)("Middlename").ToString.Replace("'", "''") & " " _
            & sqldataAccounts.Rows(0)("Lastname").ToString.Replace("'", "''") Then
        Else

            stracs = "insert into AccountHistory (historyDate,historyAccountNo,historyCategory,historyName,historyRemarks,historyCreatedBy) values " _
                   & "('" & Format(Now, "yyyy-MM-dd") & "', '" & txtAccountNo.Text & "', 'Account Update', '" _
                   & "Updated Name from(" & sqldataAccounts.Rows(0)("Firstname").ToString.Replace("'", "''") & " " _
            & sqldataAccounts.Rows(0)("Middlename").ToString.Replace("'", "''") & " " _
            & sqldataAccounts.Rows(0)("Lastname").ToString.Replace("'", "''") & ")', '', '" & My.Settings.Nickname & "')"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()

        End If

        If txtAddress.Text = sqldataAccounts.Rows(0)("ServiceAddress") Then
        Else

            stracs = "insert into AccountHistory (historyDate,historyAccountNo,historyCategory,historyName,historyRemarks,historyCreatedBy) values " _
                   & "('" & Format(Now, "yyyy-MM-dd") & "', '" & txtAccountNo.Text & "', 'Account Update', '" _
                   & "Updated Address(" & sqldataAccounts.Rows(0)("ServiceAddress").ToString.Replace("'", "''") & " - " & txtAddress.Text.ToString.Replace("'", "''") & ")', '', '" & My.Settings.Nickname & "')"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()
        End If

        If txtContactNo.Text = sqldataAccounts.Rows(0)("ContactNo") Then
        Else

            stracs = "insert into AccountHistory (historyDate,historyAccountNo,historyCategory,historyName,historyRemarks,historyCreatedBy) values " _
                   & "('" & Format(Now, "yyyy-MM-dd") & "', '" & txtAccountNo.Text & "', 'Account Update', '" _
                   & "Updated Contact No.(" & sqldataAccounts.Rows(0)("ContactNo").ToString.Replace("'", "''") & " - " & txtContactNo.Text.ToString.Replace("'", "''") & ")', '', '" & My.Settings.Nickname & "')"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()
        End If

        If txtSequenceNo.Text = sqldataAccounts.Rows(0)("ReadingSeqNo") Then
        Else

            stracs = "insert into AccountHistory (historyDate,historyAccountNo,historyCategory,historyName,historyRemarks,historyCreatedBy) values " _
                   & "('" & Format(Now, "yyyy-MM-dd") & "', '" & txtAccountNo.Text & "', 'Account Update', '" _
                   & "Updated Sequence No.(" & sqldataAccounts.Rows(0)("ReadingSeqNo").ToString.Replace("'", "''") & " - " & txtSequenceNo.Text.ToString.Replace("'", "''") & ")', '', '" & My.Settings.Nickname & "')"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()
        End If

        If cmbZone.Text = sqldataAccounts.Rows(0)("Zone") Then
        Else

            stracs = "insert into AccountHistory (historyDate,historyAccountNo,historyCategory,historyName,historyRemarks,historyCreatedBy) values " _
                   & "('" & Format(Now, "yyyy-MM-dd") & "', '" & txtAccountNo.Text & "', 'Account Update', '" _
                   & "Updated Zone(" & sqldataAccounts.Rows(0)("Zone").ToString.Replace("'", "''") & " - " & cmbZone.Text.ToString.Replace("'", "''") & ")', '', '" & My.Settings.Nickname & "')"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()
        End If

        If cmbClass.Text = sqldataAccounts.Rows(0)("RateSchedule") Then
        Else

            stracs = "insert into AccountHistory (historyDate,historyAccountNo,historyCategory,historyName,historyRemarks,historyCreatedBy) values " _
                   & "('" & Format(Now, "yyyy-MM-dd") & "', '" & txtAccountNo.Text & "', 'Account Update', '" _
                   & "Updated Clasification(" & sqldataAccounts.Rows(0)("RateSchedule").ToString.Replace("'", "''") & " - " & cmbClass.Text.ToString.Replace("'", "''") & ")', '', '" & My.Settings.Nickname & "')"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()
        End If

        If Format(dateInstalled.Value, "yyyy-MM-dd") = Format(sqldataAccounts.Rows(0)("DateInstalled"), "yyyy-MM-dd") Then
        Else

            stracs = "insert into AccountHistory (historyDate,historyAccountNo,historyCategory,historyName,historyRemarks,historyCreatedBy) values " _
                   & "('" & Format(Now, "yyyy-MM-dd") & "', '" & txtAccountNo.Text & "', 'Account Update', '" _
                   & "Updated Date Installed(" & Format(sqldataAccounts.Rows(0)("DateInstalled"), "Short Date") & " - " & Format(dateInstalled.Value, "Short Date") & ")', '', '" & My.Settings.Nickname & "')"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()
        End If

        If txtMeterNo.Text = sqldataAccounts.Rows(0)("MeterNo") Then
        Else

            stracs = "insert into AccountHistory (historyDate,historyAccountNo,historyCategory,historyName,historyRemarks,historyCreatedBy) values " _
                   & "('" & Format(Now, "yyyy-MM-dd") & "', '" & txtAccountNo.Text & "', 'Account Update', '" _
                   & "Updated Meter No.(" & sqldataAccounts.Rows(0)("MeterNo").ToString.Replace("'", "''") & " - " & txtMeterNo.Text.ToString.Replace("'", "''") & ")', '', '" & My.Settings.Nickname & "')"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()
        End If

        If txtCompany.Text = sqldataAccounts.Rows(0)("CompanyName") Then
        Else

            stracs = "insert into AccountHistory (historyDate,historyAccountNo,historyCategory,historyName,historyRemarks,historyCreatedBy) values " _
                   & "('" & Format(Now, "yyyy-MM-dd") & "', '" & txtAccountNo.Text & "', 'Account Update', '" _
                   & "Updated Company Name(" & sqldataAccounts.Rows(0)("CompanyName").ToString.Replace("'", "''") & " - " & txtCompany.Text.ToString.Replace("'", "''") & ")', '', '" & My.Settings.Nickname & "')"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()
        End If

        If txtLandMark.Text = sqldataAccounts.Rows(0)("LandMark") Then
        Else

            stracs = "insert into AccountHistory (historyDate,historyAccountNo,historyCategory,historyName,historyRemarks,historyCreatedBy) values " _
                   & "('" & Format(Now, "yyyy-MM-dd") & "', '" & txtAccountNo.Text & "', 'Account Update', '" _
                   & "Updated Land Mark(" & sqldataAccounts.Rows(0)("LandMark").ToString.Replace("'", "''") & " - " & txtLandMark.Text.ToString.Replace("'", "''") & ")', '', '" & My.Settings.Nickname & "')"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()
        End If

        If cmbMeterSize.Text = sqldataAccounts.Rows(0)("MeterSize") Then
        Else

            stracs = "insert into AccountHistory (historyDate,historyAccountNo,historyCategory,historyName,historyRemarks,historyCreatedBy) values " _
                   & "('" & Format(Now, "yyyy-MM-dd") & "', '" & txtAccountNo.Text & "', 'Account Update', '" _
                   & "Updated Meter Size(" & sqldataAccounts.Rows(0)("MeterSize").ToString.Replace("'", "''") & " - " & cmbMeterSize.Text.ToString.Replace("'", "''") & ")', '', '" & My.Settings.Nickname & "')"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()
        End If

        Dim senioryn As String
        If chkSenior.CheckState = CheckState.Checked Then
            senioryn = "Yes"
        Else
            senioryn = "No"
        End If

        If senioryn = sqldataAccounts.Rows(0)("IsSenior") Then
        Else

            stracs = "insert into AccountHistory (historyDate,historyAccountNo,historyCategory,historyName,historyRemarks,historyCreatedBy) values " _
                   & "('" & Format(Now, "yyyy-MM-dd") & "', '" & txtAccountNo.Text & "', 'Account Update', '" _
                   & "Updated Senior(" & sqldataAccounts.Rows(0)("IsSenior").ToString.Replace("'", "''") & " - " & senioryn.ToString.Replace("'", "''") & ")', '', '" & My.Settings.Nickname & "')"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()
        End If

        Dim dontcharge As String
        If chkDontCharge.CheckState = CheckState.Checked Then
            dontcharge = "Yes"
        Else
            dontcharge = "No"
        End If

        If dontcharge = sqldataAccounts.Rows(0)("DontCharge") Then
        Else

            stracs = "insert into AccountHistory (historyDate,historyAccountNo,historyCategory,historyName,historyRemarks,historyCreatedBy) values " _
                   & "('" & Format(Now, "yyyy-MM-dd") & "', '" & txtAccountNo.Text & "', 'Account Update', '" _
                   & "Updated Dont Charge(" & sqldataAccounts.Rows(0)("DontCharge").ToString.Replace("'", "''") & " - " & dontcharge.ToString.Replace("'", "''") & ")', '', '" & My.Settings.Nickname & "')"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acscmd.ExecuteNonQuery()
            acscmd.Dispose()

        End If

        'txtlname.Text = sqldataAccounts.Rows(0)("IsSenior")

        'txtmname.Text = sqldataAccounts.Rows(0)("DontCharge")


    End Sub


    Private Sub accountinfo_deactivated(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.BackColor = Color.FromArgb(17, 153, 195)
    End Sub

    Private Sub gridHistory_MouseDown(sender As Object, e As MouseEventArgs) Handles gridHistory.MouseDown

        If e.Button = MouseButtons.Right Then

            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try

            Dim searchaccount As New DataTable
            searchaccount.Clear()
            stracs = "select * from Customers where AccountNo = '" & txtAccountNo.Text.Replace("'", "''") & "'"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(searchaccount)

            If searchaccount.Rows.Count = 0 Then
                gridHistory.ContextMenuStrip = Nothing
            Else
                meterreset.resetfrom.Text = searchaccount.Rows(0)("LastMeterReading")
                gridHistory.ContextMenuStrip = cmsNotes
            End If

        Else



        End If

    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click

        'notes.accountNo.Text = txtAccountNo.Text
        'notes.remarks.Focus()
        'notes.ShowDialog()


        waitingapproval.trans = "addnote"
        waitingapproval.ShowDialog()

        waitingapproval.TextBox1.Select()
        waitingapproval.TextBox1.Clear()

    End Sub

    Private Sub assocatedaccounts_Click(sender As Object, e As EventArgs) Handles assocacc.Click

        If txtAccountNo.Text = "" Then
        Else

            Dim checkacc As New DataTable

            stracs = "select * from Customers where AccountNo = '" & txtAccountNo.Text.ToString.Replace("'", "''") & "'"
            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(checkacc)

            If checkacc.Rows.Count = 0 Then
            Else

                If txtCompany.Text = "" Or IsDBNull(txtCompany.Text) Then
                    AssociatedAccounts.Label2.Text = checkacc(0)("Firstname") & " " & checkacc(0)("Middlename") & " " & checkacc(0)("Lastname") & " (" & txtAccountNo.Text & ")"

                    If IsDBNull(checkacc(0)("AssocID")) = True Then
                        AssociatedAccounts.associd = 0
                    Else
                        AssociatedAccounts.associd = checkacc(0)("AssocID")
                    End If

                    AssociatedAccounts.ShowDialog()
                Else
                    AssociatedAccounts.Label2.Text = checkacc(0)("CompanyName") & " (" & txtAccountNo.Text & ")"

                    If IsDBNull(checkacc(0)("AssocID")) = True Then
                        AssociatedAccounts.associd = ""
                    Else
                        AssociatedAccounts.associd = checkacc(0)("AssocID")
                    End If

                    AssociatedAccounts.ShowDialog()
                End If






            End If

            End If

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles lblmemfeebal.Click

    End Sub


    Private Sub accountginfo_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.BackColor = Color.SteelBlue
    End Sub

    Private Sub ControlsClick(sender As Object, e As EventArgs) Handles _
        conspanel.Click, GroupBox1.Click, txtAccountNo.Click, txtlname.Click, txtfname.Click, txtCompany.Click, txtAddress.Click,
        txtLandMark.Click, txtContactNo.Click, accSearch.Click, GroupBox2.Click, gridLedger.Click, GroupBox3.Click,
        txtMeterNo.Click, txtSequenceNo.Click, cmbZone.Click, cmbClass.Click, cmbMeterSize.Click, dateCreated.Click,
        dateInstalled.Click, GroupBox4.Click, lblStatus.Click, chkSenior.Click, chkDontCharge.Click, txtLastReading.Click,
        txtAveCons.Click, GroupBox5.Click, GroupBox6.Click, gridCharges.Click, txtAdvance.Click, gridHistory.Click, tbMembershipfee.Click ' etc.
        Me.Activate() 'Or Whatever
    End Sub

End Class