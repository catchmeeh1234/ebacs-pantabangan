Public Class billingcreatedbills
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub billingcreatedbills_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim x As Integer = 1
        Dim years As Integer = 2020

        billingMonth.Items.Clear()
        BillingYear.Items.Clear()

        Do Until x = 13

            billingMonth.Items.Add(MonthName(x))
            BillingYear.Items.Add(years)
            years = years + 1
            x = x + 1
        Loop

        billingMonth.Text = MonthName(Month(Now) - 1)
        BillingYear.Text = Format(Now, "yyyy")
        Createdby.SelectedIndex = -1
        loadzones()
        Loadusers()

    End Sub

    Sub Loadusers()

        Try
            sqldataZone.Clear()
            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try
            stracs = "SELECT distinct(CreatedBy) as CreatedBy FROM Bills order by CreatedBy"

            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(sqldataZone)


            Createdby.Items.Clear()
            For i = 0 To sqldataZone.Rows.Count - 1
                Createdby.Items.Add(sqldataZone.Rows(i)("CreatedBy"))
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Sub loadzones()

        Try
            sqldataZone.Clear()
            Try
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            Catch ex As Exception
                If acsconn.State = ConnectionState.Closed Then acsconn.Open()
            End Try
            stracs = "SELECT ZoneName FROM Zone order by ZoneID"

            acscmd.Connection = acsconn
            acscmd.CommandText = stracs
            acsda.SelectCommand = acscmd
            acsda.Fill(sqldataZone)


            Zones.Items.Clear()
            For i = 0 To sqldataZone.Rows.Count - 1
                Zones.Items.Add(sqldataZone.Rows(i)("ZoneName"))
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Createdby_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Createdby.SelectedIndexChanged

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try
        Dim getdata As New DataTable

        stracs = "select CreatedBy, DateCreated, Zone, count(Zone) as billcount from Bills where 
                BillingDate = '" & billingMonth.Text & " " & BillingYear.Text & "' and CreatedBy = '" & Createdby.Text.Replace("'", "''") & "' group by createdby, datecreated, zone"
        acscmd.Connection = acsconn
        acscmd.CommandText = stracs
        acsda.SelectCommand = acscmd
        acsda.Fill(getdata)

        If getdata.Rows.Count = 0 Then

            MsgBox("No record found.")

        Else
            For x = 0 To getdata.Rows.Count - 1

                gridDetails.Rows.Add(getdata.Rows(x)("CreatedBy"), Format(getdata.Rows(x)("DateCreated"), "Short date"), getdata.Rows(x)("Zone"), getdata.Rows(x)("billcount"))

            Next
        End If



    End Sub

    Private Sub Zones_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Zones.SelectedIndexChanged

        Try
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        Catch ex As Exception
            If acsconn.State = ConnectionState.Closed Then acsconn.Open()
        End Try
        Dim getdata As New DataTable

        stracs = "select CreatedBy, DateCreated, Zone, count(Zone) as billcount from Bills where 
                BillingDate = '" & billingMonth.Text & " " & BillingYear.Text & "' and Zone = '" & Zones.Text.Replace("'", "''") & "' group by createdby, datecreated, zone"
        acscmd.Connection = acsconn
        acscmd.CommandText = stracs
        acsda.SelectCommand = acscmd
        acsda.Fill(getdata)

        For x = 0 To getdata.Rows.Count - 1

            gridDetails.Rows.Add(getdata.Rows(x)("CreatedBy"), Format(getdata.Rows(x)("DateCreated"), "Short date"), getdata.Rows(x)("Zone"), getdata.Rows(x)("billcount"))

        Next

    End Sub
End Class