<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UnpaidConcessionairesReport
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.prog = New System.Windows.Forms.ProgressBar()
        Me.billSearch = New System.Windows.Forms.Button()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbZone = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.dtpBillingDate = New System.Windows.Forms.DateTimePicker()
        Me.SuspendLayout()
        '
        'prog
        '
        Me.prog.Location = New System.Drawing.Point(603, 10)
        Me.prog.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.prog.Name = "prog"
        Me.prog.Size = New System.Drawing.Size(185, 35)
        Me.prog.TabIndex = 77
        '
        'billSearch
        '
        Me.billSearch.FlatAppearance.BorderSize = 0
        Me.billSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.billSearch.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.billSearch.Location = New System.Drawing.Point(442, 10)
        Me.billSearch.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.billSearch.Name = "billSearch"
        Me.billSearch.Size = New System.Drawing.Size(149, 35)
        Me.billSearch.TabIndex = 76
        Me.billSearch.Text = "Generate"
        Me.billSearch.UseVisualStyleBackColor = True
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Location = New System.Drawing.Point(13, 58)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.ServerReport.BearerToken = Nothing
        Me.ReportViewer1.Size = New System.Drawing.Size(941, 491)
        Me.ReportViewer1.TabIndex = 79
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(17, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 15)
        Me.Label2.TabIndex = 78
        Me.Label2.Text = "Billing Month"
        '
        'cmbZone
        '
        Me.cmbZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbZone.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbZone.FormattingEnabled = True
        Me.cmbZone.Location = New System.Drawing.Point(294, 16)
        Me.cmbZone.Name = "cmbZone"
        Me.cmbZone.Size = New System.Drawing.Size(121, 21)
        Me.cmbZone.TabIndex = 81
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(253, 20)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(34, 15)
        Me.Label10.TabIndex = 82
        Me.Label10.Text = "Zone"
        '
        'dtpBillingDate
        '
        Me.dtpBillingDate.CustomFormat = "MMMM yyyy"
        Me.dtpBillingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpBillingDate.Location = New System.Drawing.Point(100, 17)
        Me.dtpBillingDate.Name = "dtpBillingDate"
        Me.dtpBillingDate.Size = New System.Drawing.Size(143, 20)
        Me.dtpBillingDate.TabIndex = 83
        '
        'UnpaidConcessionairesReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(965, 561)
        Me.Controls.Add(Me.dtpBillingDate)
        Me.Controls.Add(Me.cmbZone)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.prog)
        Me.Controls.Add(Me.billSearch)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Controls.Add(Me.Label2)
        Me.Name = "UnpaidConcessionairesReport"
        Me.Text = "UnpaidConcessionairesReport"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents prog As ProgressBar
    Friend WithEvents billSearch As Button
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbZone As ComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents dtpBillingDate As DateTimePicker
End Class
