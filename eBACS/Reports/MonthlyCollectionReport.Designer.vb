<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MonthlyCollectionReport
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.prog = New System.Windows.Forms.ProgressBar()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.dtpMonth = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbZone = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'prog
        '
        Me.prog.Location = New System.Drawing.Point(668, 15)
        Me.prog.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.prog.Name = "prog"
        Me.prog.Size = New System.Drawing.Size(271, 28)
        Me.prog.TabIndex = 77
        '
        'btnGenerate
        '
        Me.btnGenerate.FlatAppearance.BorderSize = 0
        Me.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnGenerate.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerate.Location = New System.Drawing.Point(466, 13)
        Me.btnGenerate.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(180, 34)
        Me.btnGenerate.TabIndex = 76
        Me.btnGenerate.Text = "Generate"
        Me.btnGenerate.UseVisualStyleBackColor = True
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Location = New System.Drawing.Point(12, 58)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.ServerReport.BearerToken = Nothing
        Me.ReportViewer1.Size = New System.Drawing.Size(1030, 549)
        Me.ReportViewer1.TabIndex = 78
        '
        'dtpMonth
        '
        Me.dtpMonth.CustomFormat = "MMMM yyyy"
        Me.dtpMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpMonth.Location = New System.Drawing.Point(99, 21)
        Me.dtpMonth.Name = "dtpMonth"
        Me.dtpMonth.Size = New System.Drawing.Size(143, 20)
        Me.dtpMonth.TabIndex = 85
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 15)
        Me.Label2.TabIndex = 84
        Me.Label2.Text = "Month"
        '
        'cbZone
        '
        Me.cbZone.BackColor = System.Drawing.Color.White
        Me.cbZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbZone.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbZone.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold)
        Me.cbZone.FormattingEnabled = True
        Me.cbZone.Location = New System.Drawing.Point(308, 20)
        Me.cbZone.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbZone.Name = "cbZone"
        Me.cbZone.Size = New System.Drawing.Size(140, 23)
        Me.cbZone.TabIndex = 87
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(263, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 15)
        Me.Label1.TabIndex = 86
        Me.Label1.Text = "Zone"
        '
        'MonthlyCollectionReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1054, 666)
        Me.Controls.Add(Me.cbZone)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtpMonth)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.prog)
        Me.Controls.Add(Me.btnGenerate)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Name = "MonthlyCollectionReport"
        Me.Text = "MonthlyCollectionReport"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents prog As ProgressBar
    Friend WithEvents btnGenerate As Button
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents dtpMonth As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents cbZone As ComboBox
    Friend WithEvents Label1 As Label
End Class
