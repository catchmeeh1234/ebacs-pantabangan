Imports System.Data.SqlClient
Imports Microsoft.Office.Core
Imports Microsoft.Office.Interop
Imports System.Globalization
Imports System.IO

Public Class Online_Billing
    Private Sub Online_Billing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = eBACSmain

        loadzones()

    End Sub

    Private Sub loadzones()

        Dim loadcreatezones As New DataTable
        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try
        loadcreatezones.Clear()

        stracs = "select * from Zone"
        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(loadcreatezones)

        If loadcreatezones.Rows.Count = 0 Then
        Else
            createZone.Rows.Clear()

            For x = 0 To loadcreatezones.Rows.Count - 1

                createZone.Rows.Add(loadcreatezones(x)("ZoneID"), loadcreatezones(x)("ZoneName"), 0)

            Next

        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub posting_Click(sender As Object, e As EventArgs) Handles posting.Click

        Dim excel As Excel.Application
        Dim wBook As Excel.Workbook
        Dim wSheet As Excel.Worksheet

        excel = CreateObject("Excel.Application")
        wBook = excel.Workbooks.Add
        wSheet = excel.ActiveSheet

        With wSheet.Range("A1", "G1")
            .ColumnWidth = 25
            .Font.Size = 12
            .Font.Bold = True
            .HorizontalAlignment = XlHAlign.xlHAlignCenter
            .VerticalAlignment = XlVAlign.xlVAlignCenter
        End With

        Dim zones As String = ""
        Dim couters As Integer = 0

        For x = 0 To createZone.Rows.Count - 1

            If createZone.Rows(x).Cells(2).Value = -1 Then

                couters = couters + 1

                If couters = 1 Then

                    zones = "'" & createZone.Rows(x).Cells(1).Value & "'"

                Else

                    zones = zones & ", '" & createZone.Rows(x).Cells(1).Value & "'"

                End If

            Else

            End If

        Next

        If acsconn.State = ConnectionState.Closed Then acsconn.Open()

        Dim datatoexport As New DataTable

        '        "SELECT BILLID
        ',a.BillNo
        ',a.AccountNumber
        ',a.CustomerName
        ',a.ReadingDate
        ',a.LastDayNOPen
        ',a.DiscDate
        ',a.BillingDate
        ',a.Zone
        ',a.AmountDue
        ',a.PenaltyAfterDue
        ',a.IsPaid
        ',a.AdvancePayment
        ',a.Discount
        ',a.isPromisorry
        ',billcount = (SELECT COUNT(e.isPaid) AS bcount FROM dbo.Bills e WHERE e.AccountNumber=a.AccountNumber AND e.IsPaid='No')
        ',forwardedbal = (SELECT sum(d.Billing) AS fBilling FROM dbo.AddAdjustment d WHERE d.AccountNumber=a.AccountNumber AND d.Paid = 'No') 
        ',chargeamount = (SELECT sum(c.Amount) AS cAmount FROM dbo.BillCharges c WHERE c.BillNumber=a.BillNo AND c.IsPaid='No' AND a.isPromisorry='No' and c.Status='Posted')
        'FROM dbo.Bills a WHERE BillingDate = '$monthdate' AND Zone IN ($zone) and IsPaid = 'No' AND a.BillStatus = 'Posted' 
        'ORDER BY a.ReadingDate, a.AccountNumber ASC"

        stracs = "SELECT 
      a.AccountNumber
      ,a.CustomerName
      ,CONVERT(DECIMAL(10,2), ((a.AmountDue + a.Adjustment) - (a.AdvancePayment + a.Discount)) + (SELECT isnull(sum(c.Amount), 0) AS cAmount FROM dbo.BillCharges c WHERE c.BillNumber=a.BillNo AND c.IsPaid='No' AND a.isPromisorry='No' and c.Status='Posted' and c.Cancelled='No'))  as BALANCE
      ,DATEADD(day, -2, a.DueDate) as Duedate
      ,FORMAT(cast(a.BillingDate as Date), 'MMMM yyyy') as date
      ,CONVERT(DECIMAL(10,2), (((isnull(a.AmountDue, 0) + isnull(a.Adjustment, 0)) - (isnull(a.AdvancePayment, 0) + isnull(a.Discount, 0))) + (((isnull(a.AmountDue, 0) + isnull(a.Adjustment, 0)) - (isnull(a.AdvancePayment, 0) + isnull(a.Discount, 0))) * .05)) + (SELECT isnull(sum(c.Amount), 0) AS cAmount FROM dbo.BillCharges c WHERE c.BillNumber=a.BillNo AND c.IsPaid='No' AND a.isPromisorry='No' and c.Status='Posted' and c.Cancelled='No')) as AfterDue
      ,DATEADD(day, -2, a.DiscDate) as DiscDate
      from Bills a where BillingDate = '" & billingDate.Text & "' and BillStatus = 'Posted' and IsPaid = 'No' and isPromisorry <> 'YesPosted' and Zone in (" & zones & ") and (SELECT isnull(COUNT(e.isPaid), 0) AS bcount FROM dbo.Bills e WHERE e.AccountNumber=a.AccountNumber AND e.IsPaid='No'  and Cancelled = 'No' and e.BillStatus = 'Posted' and isPromisorry <> 'YesPosted') = 1"

        acscmd.CommandText = stracs
        acscmd.Connection = acsconn
        acsda.SelectCommand = acscmd
        acsda.Fill(datatoexport)

        With wSheet.Range("E2", "E" & datatoexport.Rows.Count + 1)
            .NumberFormat = "MMMM yyyy"
        End With

        wSheet.Cells(1, 1) = "ACCOUNT NO."
        wSheet.Cells(1, 2) = "CONSUMER NAME"
        wSheet.Cells(1, 3) = "BALANCE"
        wSheet.Cells(1, 4) = "DUE DATE"
        wSheet.Cells(1, 5) = "BILL MONTH"
        wSheet.Cells(1, 6) = "AMOUNT AFTER DUE"
        wSheet.Cells(1, 7) = "DUE DATE"

        For x = 0 To datatoexport.Rows.Count - 1

            For y = 0 To datatoexport.Columns.Count - 1

                wSheet.Cells(x + 2, y + 1) = datatoexport.Rows(x)(y)

            Next

        Next

        excel.Visible = True
        excel.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMaximized

    End Sub

    Public MoveOnlineBilling As Boolean
    Public MoveOnlineBilling_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveOnlineBilling = True
            Me.Cursor = Cursors.NoMove2D
            MoveOnlineBilling_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveOnlineBilling Then
            Me.Location = Me.Location + (e.Location - MoveOnlineBilling_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveOnlineBilling = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub Online_Billing_Click(sender As Object, e As EventArgs) Handles Me.Click
        Me.BringToFront()
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        Me.Activate()
    End Sub

    Private Sub Online_Billing_deactivated(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.BackColor = Color.FromArgb(17, 153, 195)
    End Sub

    Private Sub ControlsClick(sender As Object, e As EventArgs) Handles _
        Panel1.Click, Label9.Click, Label11.Click, createZone.Click, billingDate.Click, posting.Click, GroupBox1.Click
        Me.Activate() 'Or Whatever
    End Sub

    Private Sub Online_Billing_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.BackColor = Color.SteelBlue
    End Sub

End Class