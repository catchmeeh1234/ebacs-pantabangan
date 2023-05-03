Public Class ORPost
    Private Sub ORPost_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain
        loaddata()
        CRList.Columns(9).ReadOnly = False
    End Sub

    Sub loaddata()


        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try
        Dim postcr As New DataTable
        postcr.Clear()
        stracs = "select * FROM OR_Details WHERE Status = 'Pending' and Cancelled = 'No' and Office = '" & My.Settings.Office_Name & "' order by OR_DetailsID DESC "
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(postcr)

        If postcr.Rows.Count = 0 Then
            'MsgBox("No Data found")
            CRList.Rows.Clear()
            totalcollection.Text = "0"
            CheckBox1.Checked = False
        Else

            CRList.Rows.Clear()

            For u = 0 To postcr.Rows.Count - 1
                CRList.Rows.Add(postcr.Rows(u)("OR_DetailsID"), postcr.Rows(u)("ORNo"), postcr.Rows(u)("AccountNo"), postcr.Rows(u)("AccountName"), postcr.Rows(u)("TransactionType"),
                                FormatNumber(postcr.Rows(u)("TotalAmountDue")), postcr.Rows(u)("PaymentDate"), postcr.Rows(u)("Collector"), postcr.Rows(u)("Office"), False)
            Next
            CheckBox1.Checked = False
            totalcollection.Text = CRList.Rows.Count
        End If



    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then

            For q = 0 To CRList.Rows.Count - 1

                CRList.Rows(q).Cells(9).Value = True

            Next

        Else
            For q = 0 To CRList.Rows.Count - 1

                CRList.Rows(q).Cells(9).Value = False

            Next
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub billSearch_Click(sender As Object, e As EventArgs) Handles billSearch.Click
        Cursor = Cursors.WaitCursor
        If My.Settings.Admin = "Yes" Or My.Settings.Cashier = "Yes" Then

            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try

            Cursor = Cursors.WaitCursor


            If CRList.Rows.Count = 0 Then

            Else


                For i = 0 To CRList.Rows.Count - 1

                    If CRList.Rows(i).Cells(9).Value = True Then

                        If CRList.Rows(i).Cells(2).Value = "" Or IsDBNull(CRList.Rows(i).Cells(2).Value) = True Or CRList.Rows(i).Cells(2).Value = "No Account" Then

                            stracs = "Update OR_Details Set Status = 'Posted' WHERE OR_DetailsID = " & CRList.Rows(i).Cells(0).Value
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acscmd.ExecuteNonQuery()

                        Else


                            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                            Dim sqlsearchorno As New DataTable
                            sqlsearchorno.Clear()
                            stracs = "select * FROM ORItems WHERE ORNo = '" & CRList.Rows(i).Cells(1).Value & "'"
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acsda.SelectCommand = acscmd
                            acsda.Fill(sqlsearchorno)

                            If sqlsearchorno.Rows.Count = 0 Then

                            Else

                                For u = 0 To sqlsearchorno.Rows.Count - 1

                                    If sqlsearchorno.Rows(u)("Charges") = "Yes" Then
                                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                        stracs = "Update BillCharges set IsPaid = 'Yes' WHERE BillChargesID = '" & sqlsearchorno.Rows(u)("ChargeID") & "'"
                                        acscmd.CommandText = stracs
                                        acscmd.Connection = acsconn
                                        acscmd.ExecuteNonQuery()

                                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                        stracs = "INSERT INTO AccountLedger (ledgerAccountNo,ledgerDate,ledgerRefNo,ledgerParticulars,ledgerReading,ledgerConsumption,ledgerAmount,ledgerDiscount,ledgerBalance)
                                        Values ('" & CRList.Rows(i).Cells(2).Value & "','" & Format(Date.Parse(CRList.Rows(i).Cells(6).Value), "yyyy-MM-dd") & "',
                                        '" & CRList.Rows(i).Cells(1).Value & "','" & sqlsearchorno.Rows(u)("Particular") & ": " & FormatNumber(sqlsearchorno.Rows(u)("Total")) & "', '', '', '','','')"
                                        acscmd.CommandText = stracs
                                        acscmd.Connection = acsconn
                                        acscmd.ExecuteNonQuery()

                                    Else

                                        If CRList.Rows(i).Cells(2).Value = "" Then

                                        Else


                                            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                                            stracs = "INSERT INTO AccountLedger (ledgerAccountNo,ledgerDate,ledgerRefNo,ledgerParticulars,ledgerReading,ledgerConsumption,ledgerAmount,ledgerDiscount,ledgerBalance)
                                            Values ('" & CRList.Rows(i).Cells(2).Value & "','" & Format(Date.Parse(CRList.Rows(i).Cells(6).Value), "yyyy-MM-dd") & "',
                                            '" & CRList.Rows(i).Cells(1).Value & "','" & sqlsearchorno.Rows(u)("Particular") & ": " & FormatNumber(sqlsearchorno.Rows(u)("Total")) & "', '', '', '','','')"
                                            acscmd.CommandText = stracs
                                            acscmd.Connection = acsconn
                                            acscmd.ExecuteNonQuery()

                                        End If

                                    End If

                                Next


                            End If

                            'update OR_Details status
                            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                            stracs = "Update OR_Details set Status = 'Posted' WHERE OR_DetailsID = " & CRList.Rows(i).Cells(0).Value
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acscmd.ExecuteNonQuery()

                        End If

                    End If

                Next

                loaddata()
            End If

            Cursor = Cursors.Default

        Else
            MsgBox("Your account cannot perform this process.")
        End If


        Cursor = Cursors.Default
    End Sub

    Public MoveFormOrposts As Boolean
    Public MoveFormOrposts_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormOrposts = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormOrposts_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormOrposts Then
            Me.Location = Me.Location + (e.Location - MoveFormOrposts_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormOrposts = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub ORPost_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.Activate()
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Me.Activate()
    End Sub

    Private Sub postbill_deactivated(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.BackColor = Color.FromArgb(17, 153, 195)
    End Sub

    Private Sub postbill_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.BackColor = Color.SteelBlue
    End Sub

    Private Sub ControlsClick(sender As Object, e As EventArgs) Handles _
        Panel1.Click, CRList.Click, billSearch.Click  ' etc.
        Me.Activate() 'Or Whatever
    End Sub
End Class