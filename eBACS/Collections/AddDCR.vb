Public Class AddDCR
    Dim previousreportdate As Date
    Dim logicnumber As Integer
    Dim office As Integer
    Dim officeletter As String

    Public id As String
    Private Sub AddDCR_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim reportnumber As New DataTable
        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        If CollectionsAndDeposits.cboffice.SelectedIndex = 0 Then
            office = "8"
            officeletter = "A"
        Else
            office = "9"
            officeletter = "B"

        End If

        reportnumber.Clear()
        stracs = "select number from tbllogicnumbers where id = '" & office & "'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(reportnumber)

        If reportnumber.Rows.Count = 0 Then
        Else

            logicnumber = reportnumber.Rows(0)("number")
            txtreportnumber.Text = officeletter & Date.Now.ToString("yyyy") & "-" & Date.Now.ToString("MM") & "-" & Format(logicnumber, "000")

        End If

        If Label7.Text = "Create New Deposit" Then

            loaddata()

        End If



    End Sub

    Sub loaddata()

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try
        Dim collectiontoday As New DataTable
        collectiontoday.Clear()

        stracs = "select SUM(TotalAmountDue) AS totalcollected, SUM(AdvancePayment) as advancetotal from Collection_Details where Cast(PaymentDate as Date) = '" & txtreportdate.Value & "' 
        AND Cancelled = 'No' AND Office = '" & CollectionsAndDeposits.cboffice.Text & "'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(collectiontoday)

        Dim totalcollect, advancetotal As Decimal
        totalcollect = 0

        advancetotal = 0

        If IsDBNull(collectiontoday.Rows(0)("totalcollected")) = True Then
            totalcollect = 0
        Else
            totalcollect = collectiontoday.Rows(0)("totalcollected")
        End If

        If IsDBNull(collectiontoday.Rows(0)("advancetotal")) = True Then
            advancetotal = 0
        Else
            advancetotal = collectiontoday.Rows(0)("advancetotal")
        End If

        Dim ortoday As New DataTable
        ortoday.Clear()
        stracs = "select SUM(TotalAmountDue) AS totalor from OR_Details where Cast(PaymentDate as Date) = '" & txtreportdate.Value & "' 
        AND Cancelled = 'No' AND Office = '" & CollectionsAndDeposits.cboffice.Text & "'"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(ortoday)

        Dim totalor As Decimal
        totalor = 0

        If IsDBNull(ortoday.Rows(0)("totalor")) = True Then
            totalor = 0
        Else
            totalor = ortoday.Rows(0)("totalor")
        End If

        txtcollectionthisreport.Text = Format(totalcollect + totalor, "standard")

        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Dim undepositedlastreport As New DataTable
        undepositedlastreport.Clear()
        stracs = "SELECT * FROM DCR WHERE DCRNo LIKE '" & officeletter & "%' ORDER by DCRId DESC"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(undepositedlastreport)

        If undepositedlastreport.Rows.Count = 0 Then
            txtundepositedlastreport.Text = "0"
            previousreportdate = Date.Now
        Else
            txtundepositedlastreport.Text = Format(undepositedlastreport.Rows(0)("Undeposited"), "standard")
            previousreportdate = Format(undepositedlastreport.Rows(0)("ReportDate"), "standard")

            'txtundepositedlastreport.Text = undepositedlastreport.Rows(0)("Undeposited")
            'previousreportdate = undepositedlastreport.Rows(0)("ReportDate")
        End If

        txtundepositedcollectionthisreport.Text = ""
        txtdeposit.Text = ""

    End Sub

    Private Sub txtdeposit_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtdeposit.KeyPress

        If e.KeyChar <> ControlChars.Back Then
            e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = ".")
        End If

    End Sub

    Private Sub txtdeposit_TextChanged(sender As Object, e As EventArgs) Handles txtdeposit.TextChanged

        If IsNumeric(txtdeposit.Text) Then

            txtundepositedcollectionthisreport.Text = Format((Double.Parse(txtcollectionthisreport.Text) + Double.Parse(txtundepositedlastreport.Text)) - Double.Parse(txtdeposit.Text), "standard")

        Else

        End If


    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        If txtdeposit.Text = "" Then
            MsgBox("Please Enter Deposit Amount")
        Else

            If IsNumeric(txtdeposit.Text) = True Then

                If btnsave.Text = "Update" Then

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    stracs = "UPDATE DCR SET Deposited = " & Double.Parse(txtdeposit.Text) & ", Undeposited = " & Double.Parse(txtundepositedcollectionthisreport.Text) & " WHERE DCRId = " & id & ""
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acscmd.ExecuteNonQuery()

                    CollectionsAndDeposits.loaddataa()
                    MsgBox("Update Complete")
                    Me.Close()

                Else

                    If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                    Dim qwe As New DataTable
                    stracs = "SELECT * FROM DCR WHERE ReportDate = '" & txtreportdate.Value & "' and OfficeName = '" & CollectionsAndDeposits.cboffice.Text & "'"
                    acscmd.CommandText = stracs
                    acscmd.Connection = acsconn
                    acsda.SelectCommand = acscmd
                    acsda.Fill(qwe)

                    If qwe.Rows.Count = 0 Then

                        stracs = "INSERT INTO DCR (DCRNo,ReportDate,Deposited,Undeposited,PreviousReportDate,EncodedBy,OfficeName) VALUES 
                ('" & txtreportnumber.Text & "', '" & Format(Date.Parse(txtreportdate.Value), "yyyy-MM-dd") & "', " & Double.Parse(txtdeposit.Text) & "
                , '" & Double.Parse(txtundepositedcollectionthisreport.Text) & "', '" & Format(Date.Parse(previousreportdate), "yyyy-MM-dd") & "', '" & eBACSmain.lblUserName.Text & "','" & CollectionsAndDeposits.cboffice.Text & "')"
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()

                        logicnumber = logicnumber + 1

                        If acsconn.State = ConnectionState.Closed Then acsconn.Open()
                        stracs = "UPDATE tbllogicnumbers SET number = " & logicnumber & " WHERE id = " & office & ""
                        acscmd.CommandText = stracs
                        acscmd.Connection = acsconn
                        acscmd.ExecuteNonQuery()

                        CollectionsAndDeposits.loaddataa()
                        MsgBox("Save Complete")
                        Me.Close()

                    Else
                        MsgBox("Report already created for this date.")
                    End If

                End If

            Else

                MsgBox("Invalid amount.")

            End If


        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub txtreportdate_ValueChanged(sender As Object, e As EventArgs) Handles txtreportdate.ValueChanged
        loaddata()
    End Sub

    Public MoveFormADCR As Boolean
    Public MoveFormCAD_MousePositionADCR As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveFormADCR = True
            Me.Cursor = Cursors.NoMove2D
            MoveFormCAD_MousePositionADCR = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMoveADCR(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveFormADCR Then
            Me.Location = Me.Location + (e.Location - MoveFormCAD_MousePositionADCR)
        End If

    End Sub

    Public Sub MoveForm_MouseUpADCR(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveFormADCR = False
            Me.Cursor = Cursors.Default
        End If

    End Sub


End Class