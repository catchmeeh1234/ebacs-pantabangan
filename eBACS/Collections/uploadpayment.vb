Imports System.Globalization
Imports System.IO
Imports ExcelDataReader
Public Class uploadpayment

    Private Sub uploadpayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.MdiParent = eBACSmain

    End Sub

    Dim tables As DataTableCollection

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        onlinepayments.Rows.Clear()

        Dim searchfile As New OpenFileDialog

        If My.Settings.uploaddefpath = "" Then

            searchfile.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            searchfile.Filter = "Excel Workbook|*.xlsx|Excel 97-2003 Workbook|*.xls|All Files (*.*)|*.*"

        Else

            searchfile.InitialDirectory = My.Settings.uploaddefpath
            searchfile.Filter = "Excel Workbook|*.xlsx|Excel 97-2003 Workbook|*.xls|All Files (*.*)|*.*"

        End If

        If searchfile.ShowDialog() = DialogResult.OK Then

            My.Settings.uploaddefpath = System.IO.Path.GetDirectoryName(searchfile.FileName)
            My.Settings.Save()

            TextBox1.Text = Path.GetFileName(searchfile.FileName)

            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try

            Dim searchdata As New DataTable

            stracs = "select filename from uploadedpayment where filename = '" & TextBox1.Text.Replace("'", "''") & "'"
            acscmd.CommandText = stracs
            acscmd.Connection = acsconn
            acsda.SelectCommand = acscmd
            acsda.Fill(searchdata)

            If searchdata.Rows.Count = 0 Then
            Else
                MsgBox(TextBox1.Text & " already uploaded. Please double check payments before posting.")
            End If

            Try

                Using stream = File.Open(searchfile.FileName, FileMode.Open, FileAccess.Read)

                    Using reader As IExcelDataReader = ExcelReaderFactory.CreateReader(stream)
                        Dim result As DataSet = reader.AsDataSet(New ExcelDataSetConfiguration() With {
                                                                 .ConfigureDataTable = Function(__) New ExcelDataTableConfiguration() With {.UseHeaderRow = True}})

                        tables = result.Tables

                    End Using

                End Using





                Dim dt As DataTable = tables(0)
                'DataGridView1.DataSource = dt

                For x = 0 To dt.Rows.Count - 1

                    Dim formats() As String = {"dd/MM/yyyy", "MM/dd/yyyy"}

                    Dim thisDt As Date

                    If IsDBNull(dt.Rows(x)("Column3")) = True Then

                        'DataGridView1.Rows.Add(dt.Rows(x)("Column3"))

                    Else
                        If DateTime.TryParseExact(dt.Rows(x)("Column3"), formats,
                                   Globalization.CultureInfo.InvariantCulture,
                                   DateTimeStyles.None, thisDt) Then

                            Dim checkref As New DataTable
                            Dim refcheck As String

                            stracs = "select AccountNo from Collection_Details where OnlineRefNo = '" & dt.Rows(x)("Column5").ToString & "'"
                            acscmd.Connection = acsconn
                            acscmd.CommandText = stracs
                            acsda.SelectCommand = acscmd
                            acsda.Fill(checkref)

                            If checkref.Rows.Count = 0 Then

                                refcheck = ""

                            Else

                                refcheck = "Posted"

                            End If

                            Dim totalamount As New DataTable

                            stracs = "SELECT isnull(isnull((SUM(a.AmountDue) + sum(a.Adjustment) + sum(a.PenaltyAfterDue)) - isnull((sum(a.AdvancePayment) + sum(a.Discount)),0),0)
                              +isnull((select ISNULL(sum(b.Amount),0) from BillCharges b where b.IsPaid = 'No' and Cancelled = 'No' and b.Status = 'Posted' and b.AccountNumber = a.AccountNumber group by b.AccountNumber),0)
                              +isnull((select isnull(sum(c.Billing) + sum(c.Penalty) + sum(c.Charges), 0) from AddAdjustment c where c.Status = 'Posted' and c.AccountNumber = a.AccountNumber and c.Paid = 'No' group by c.AccountNumber),0),0) as totalamount, 
                              count(a.AccountNumber) as bilang
                              FROM [eBACS].[dbo].[Bills] a where a.AccountNumber = '" & dt.Rows(x)("Column7").ToString & "' and a.IsPaid = 'No' and a.Cancelled = 'No' and a.isPromisorry <> 'YesPosted' and a.IsCollectionCreated = 'No' and BillStatus = 'Posted' group by a.AccountNumber"
                            acscmd.Connection = acsconn
                            acscmd.CommandText = stracs
                            acsda.SelectCommand = acscmd
                            acsda.Fill(totalamount)

                            Dim amounttotal As Double
                            Dim bilang As Integer = 0
                            Dim statuses As String = ""

                            If totalamount.Rows.Count = 0 Then

                                amounttotal = 0
                                bilang = 0
                                statuses = "Paid"

                            Else

                                amounttotal = totalamount(0)("totalamount")
                                bilang = totalamount(0)("bilang")

                            End If

                            Dim fullnametable As New DataTable

                            stracs = "select Firstname, Middlename, Lastname from Customers where AccountNo = '" & dt.Rows(x)("Column7").ToString & "'"
                            acscmd.Connection = acsconn
                            acscmd.CommandText = stracs
                            acsda.SelectCommand = acscmd
                            acsda.Fill(fullnametable)

                            Dim fullnames As String = fullnametable(0)("Firstname") & " " & fullnametable(0)("Middlename") & " " & fullnametable(0)("Lastname")

                            'If IsDBNull(totalamount(0)("bilang")) = True Then
                            '    totalamount(0)("bilang") = 0
                            'Else
                            '    totalamount(0)("bilang") = totalamount(0)("bilang")
                            'End If

                            'MsgBox(totalamount(0)("bilang").ToString)

                            If amounttotal = dt.Rows(x)("Column9") And bilang <= 1 Then
                                onlinepayments.Rows.Add(Format(Date.Parse(dt.Rows(x)("Column3")), "yyyy-MM-dd") & " " & Format(DateTime.Parse(dt.Rows(x)("Column4")), "HH:mm:ss"), dt.Rows(x)("Column7"), fullnames, dt.Rows(x)("Column8").ToString.ToUpper, Format(Double.Parse(dt.Rows(x)("Column9")), "Standard"), Format(amounttotal, "Standard"), -1, dt.Rows(x)("Column5"), bilang, refcheck)
                            Else
                                onlinepayments.Rows.Add(Format(Date.Parse(dt.Rows(x)("Column3")), "yyyy-MM-dd") & " " & Format(DateTime.Parse(dt.Rows(x)("Column4")), "HH:mm:ss"), dt.Rows(x)("Column7"), fullnames, dt.Rows(x)("Column8").ToString.ToUpper, Format(Double.Parse(dt.Rows(x)("Column9")), "Standard"), Format(amounttotal, "Standard"), 0, dt.Rows(x)("Column5"), bilang, refcheck)
                            End If

                        Else

                        End If

                    End If

                Next

                Dim counter As Integer = 0

                For y = 0 To onlinepayments.Rows.Count - 1

                    If onlinepayments.Rows(y).Cells(5).Value = -1 Then

                        counter = counter + 1

                    Else

                    End If

                Next

                tutal.Text = "Total Payments: " & counter

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If

    End Sub

    Private Sub onlinepayments_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles onlinepayments.CurrentCellDirtyStateChanged

        If onlinepayments.IsCurrentCellDirty Then
            onlinepayments.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim crnumber As String
        Dim tempcount As Integer = 0

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try

        If My.Settings.Admin = "Yes" Or My.Settings.Cashier = "Yes" Then

            If onlinepayments.Rows.Count = 0 Then



            Else

                For x = 0 To onlinepayments.Rows.Count - 1

                    If onlinepayments.Rows(x).Cells(6).Value = -1 Then

                        If onlinepayments.Rows(x).Cells(4).Value < onlinepayments.Rows(x).Cells(5).Value Then

                        Else

                            Dim getcrno As New DataTable
                            getcrno.Clear()
                            stracs = "select number from tbllogicnumbers where id = '11'"
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acsda.SelectCommand = acscmd
                            acsda.Fill(getcrno)

                            tempcount = getcrno.Rows(0)("number")

                            Dim searchcrno As New DataTable
                            stracs = "select CrNo from Collection_Details where CrNo = 'B" & Format(tempcount, "0000000") & "'"
                            acscmd.CommandText = stracs
                            acscmd.Connection = acsconn
                            acsda.SelectCommand = acscmd
                            acsda.Fill(searchcrno)

                            If searchcrno.Rows.Count = 0 Then

                                crnumber = "B" & Format(tempcount, "0000000")

                                Dim getlastbillno As New DataTable



                                stracs = "select BillNo, AccountNumber, CustomerName, CustomerAddress from Bills 
                                    where BillNo = (select MAX(BillNo) from Bills where BillStatus = 'Posted' and Cancelled = 'No' and IsCollectionCreated = 'No' and 
                                    isPromisorry = 'No' and BillStatus = 'Posted' and AccountNumber = '" & onlinepayments.Rows(x).Cells(1).Value & "')"
                                acscmd.CommandText = stracs
                                acscmd.Connection = acsconn
                                acsda.SelectCommand = acscmd
                                acsda.Fill(getlastbillno)


                                If getlastbillno.Rows.Count = 0 Then

                                    Dim getdetails As New DataTable
                                    Dim fullname As String

                                    stracs = "select Firstname,Middlename,Lastname,CompanyName,ServiceAddress from Customers where AccountNo = '" & onlinepayments.Rows(x).Cells(1).Value & "'"
                                    acscmd.CommandText = stracs
                                    acscmd.Connection = acsconn
                                    acsda.SelectCommand = acscmd
                                    acsda.Fill(getdetails)

                                    If IsDBNull(getdetails.Rows(0)("CompanyName")) = True Or getdetails.Rows(0)("CompanyName") = "" Then

                                        fullname = getdetails.Rows(0)("Firstname") & " " & getdetails.Rows(0)("Middlename") & " " & getdetails.Rows(0)("Lastname")

                                    Else
                                        fullname = getdetails.Rows(0)("CompanyName")
                                    End If

                                    stracs = "INSERT INTO Collection_Details (CRNo,AccountNo,AccountName,Address,CheckNo,CheckDate,TotalAmountDue,AdvancePayment,PaymentDate,Collector,Office,CollectionStatus,PaymentType,OnlineRefNo)
                                        Values ('" & crnumber & "', '" & onlinepayments.Rows(x).Cells(1).Value & "', '" & fullname.ToString.Replace("'", "''") & "', '" & getdetails.Rows(0)("ServiceAddress").ToString.Replace("'", "''") & "',
                                        '', '', " & Double.Parse(onlinepayments.Rows(x).Cells(4).Value) & "," & Double.Parse(onlinepayments.Rows(x).Cells(4).Value - onlinepayments.Rows(x).Cells(5).Value) & ", '" & Format(DateTime.Parse(onlinepayments.Rows(x).Cells(0).Value), "yyyy-MM-dd hh:mm:ss tt") & "',
                                        'ECPAY','Mapalad Office','Pending', 'Cash','" & onlinepayments.Rows(x).Cells(7).Value & "')"
                                    acscmd.CommandText = stracs
                                    acscmd.Connection = acsconn
                                    acscmd.ExecuteNonQuery()
                                    acscmd.Dispose()

                                Else

                                    stracs = "INSERT INTO Collection_Details (CRNo,AccountNo,AccountName,Address,CheckNo,CheckDate,TotalAmountDue,AdvancePayment,PaymentDate,Collector,Office,CollectionStatus,PaymentType,OnlineRefNo)
                                        Values ('" & crnumber & "', '" & onlinepayments.Rows(x).Cells(1).Value & "', '" & getlastbillno(0)("CustomerName").ToString.Replace("'", "''") & "', '" & getlastbillno(0)("CustomerAddress").ToString.Replace("'", "''") & "',
                                        '', '', " & Double.Parse(onlinepayments.Rows(x).Cells(4).Value) & "," & Double.Parse(onlinepayments.Rows(x).Cells(4).Value - onlinepayments.Rows(x).Cells(5).Value) & ", '" & Format(DateTime.Parse(onlinepayments.Rows(x).Cells(0).Value), "yyyy-MM-dd hh:mm:ss tt") & "',
                                        'ECPAY','Mapalad Office','Pending', 'Cash','" & onlinepayments.Rows(x).Cells(7).Value & "')"
                                    acscmd.CommandText = stracs
                                    acscmd.Connection = acsconn
                                    acscmd.ExecuteNonQuery()
                                    acscmd.Dispose()

                                    stracs = "INSERT INTO CollectionBilling (CRNo,AccountNo,AccountName,Address,Zone,BillingDate,PaymentDate,BillType,BillNo,AmountDue,Discount,Penalty,AdvancePayment,CollectionBillingStatus,Adjustment,OnlineRefNo)
                                        select '" & crnumber & "', AccountNumber,CustomerName,CustomerAddress,Zone,BillingDate,'" & Format(DateTime.Parse(onlinepayments.Rows(x).Cells(0).Value), "yyyy-MM-dd hh:mm:ss tt") & "', 
                                        'BillCurrent',BillNo,AmountDue,Discount,PenaltyAfterDue,AdvancePayment,'Pending',Adjustment,'" & onlinepayments.Rows(x).Cells(7).Value & "' From Bills Where BillNo = " & getlastbillno(0)("BillNo")
                                    acscmd.CommandText = stracs
                                    acscmd.Connection = acsconn
                                    acscmd.ExecuteNonQuery()
                                    acscmd.Dispose()

                                    stracs = "UPDATE Bills Set IsCollectionCreated = 'Yes', CRNo = '" & crnumber & "', OnlineRefNo = '" & onlinepayments.Rows(x).Cells(7).Value & "' WHERE BillNo = " & getlastbillno(0)("BillNo")
                                    acscmd.CommandText = stracs
                                    acscmd.Connection = acsconn
                                    acscmd.ExecuteNonQuery()
                                    acscmd.Dispose()

                                    stracs = "INSERT INTO CollectionCharges (CRNo,BillNo,Particulars,Amount,ChargeID,Category,Entry,CollectionChargesStatus,OnlineRefNo) 
                                    select '" & crnumber & "',BillNumber,Particulars,Amount,ChargeID,Category,Entry,'Pending','" & onlinepayments.Rows(x).Cells(7).Value & "' from BillCharges where BillNumber = " & getlastbillno(0)("BillNo")
                                    acscmd.CommandText = stracs
                                    acscmd.Connection = acsconn
                                    acscmd.ExecuteNonQuery()
                                    acscmd.Dispose()

                                    stracs = "UPDATE BillCharges set IsCollectionCreated = 'Yes' , CRNo = '" & crnumber & "' WHERE BillNumber = " & getlastbillno(0)("BillNo")
                                    acscmd.CommandText = stracs
                                    acscmd.Connection = acsconn
                                    acscmd.ExecuteNonQuery()
                                    acscmd.Dispose()

                                End If

                                stracs = "update tbllogicnumbers set number = number + 1 where id = '11'"
                                acscmd.CommandText = stracs
                                acscmd.Connection = acsconn
                                acscmd.ExecuteNonQuery()
                                acscmd.Dispose()

                            Else

                                MsgBox("Duplicated CR Number. Please contact Admin.")

                            End If

                            tempcount = tempcount + 1

                        End If

                    End If

                Next

                stracs = "insert into uploadedpayment (filename,dateuploaded,uploadedby) values ('" _
                       & TextBox1.Text.Replace("'", "''") & "', '" & Format(Now, "yyyy-MM-dd") & "','" & My.Settings.Nickname & "')"
                acscmd.CommandText = stracs
                acscmd.Connection = acsconn
                acscmd.ExecuteNonQuery()
                acscmd.Dispose()

                MsgBox("Upload payment successful.")

            End If

        End If

    End Sub

    Private Sub onlinepayments_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles onlinepayments.CellClick


        If onlinepayments.Columns(e.ColumnIndex).HeaderText = "" Then

            If onlinepayments.Rows.Count = 0 Then

            Else

                If onlinepayments.Rows(onlinepayments.CurrentCellAddress.Y).Cells(6).Value = False Then

                    If onlinepayments.Rows(onlinepayments.CurrentCellAddress.Y).Cells(4).Value < onlinepayments.Rows(onlinepayments.CurrentCellAddress.Y).Cells(5).Value _
                        Or onlinepayments.Rows(onlinepayments.CurrentCellAddress.Y).Cells(9).Value = "Posted" Then


                        onlinepayments.Rows(onlinepayments.CurrentCellAddress.Y).Cells(6).Value = False

                    Else

                        onlinepayments.Rows(onlinepayments.CurrentCellAddress.Y).Cells(6).Value = True

                    End If
                Else

                    onlinepayments.Rows(onlinepayments.CurrentCellAddress.Y).Cells(6).Value = False

                End If

                Dim counter As Integer = 0

                For y = 0 To onlinepayments.Rows.Count - 1

                    If onlinepayments.Rows(y).Cells(6).Value = -1 Then

                        counter = counter + 1

                    Else

                    End If

                Next

                tutal.Text = "Total Payments: " & counter

            End If

        End If

    End Sub

    Public Moveuploadpayment As Boolean
    Public Moveuploadpayment_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            Moveuploadpayment = True
            Me.Cursor = Cursors.NoMove2D
            Moveuploadpayment_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If Moveuploadpayment Then
            Me.Location = Me.Location + (e.Location - Moveuploadpayment_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            Moveuploadpayment = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub uploadpayment_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.BringToFront()
    End Sub

    'Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
    '    Me.Activate()
    'End Sub

    Private Sub uploadpayment_deactivated(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.BackColor = Color.FromArgb(17, 153, 195)
    End Sub

    Private Sub ControlsClick(sender As Object, e As EventArgs) Handles _
        TextBox1.Click, Button1.Click, onlinepayments.Click, tutal.Click, Button2.Click
        Me.Activate() 'Or Whatever
    End Sub

    Private Sub Online_Billing_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.BackColor = Color.SteelBlue
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

End Class