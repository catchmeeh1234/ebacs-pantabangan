<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class reprintBIllsReport
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
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.datefrom = New System.Windows.Forms.DateTimePicker()
        Me.dateto = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.detailed = New System.Windows.Forms.RadioButton()
        Me.summary = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.meterreader = New System.Windows.Forms.ComboBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Location = New System.Drawing.Point(12, 74)
        Me.ReportViewer1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(904, 456)
        Me.ReportViewer1.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(829, 22)
        Me.Button1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(87, 28)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Generate"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'datefrom
        '
        Me.datefrom.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.datefrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.datefrom.Location = New System.Drawing.Point(108, 24)
        Me.datefrom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.datefrom.Name = "datefrom"
        Me.datefrom.Size = New System.Drawing.Size(115, 21)
        Me.datefrom.TabIndex = 2
        '
        'dateto
        '
        Me.dateto.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dateto.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dateto.Location = New System.Drawing.Point(247, 24)
        Me.dateto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dateto.Name = "dateto"
        Me.dateto.Size = New System.Drawing.Size(115, 21)
        Me.dateto.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 16)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Date Covered"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(229, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(12, 16)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "-"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.detailed)
        Me.GroupBox1.Controls.Add(Me.summary)
        Me.GroupBox1.Location = New System.Drawing.Point(398, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(203, 50)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Report Option"
        '
        'detailed
        '
        Me.detailed.AutoSize = True
        Me.detailed.Location = New System.Drawing.Point(109, 20)
        Me.detailed.Name = "detailed"
        Me.detailed.Size = New System.Drawing.Size(72, 20)
        Me.detailed.TabIndex = 1
        Me.detailed.TabStop = True
        Me.detailed.Text = "Detailed"
        Me.detailed.UseVisualStyleBackColor = True
        '
        'summary
        '
        Me.summary.AutoSize = True
        Me.summary.Location = New System.Drawing.Point(27, 20)
        Me.summary.Name = "summary"
        Me.summary.Size = New System.Drawing.Size(76, 20)
        Me.summary.TabIndex = 0
        Me.summary.TabStop = True
        Me.summary.Text = "Summary"
        Me.summary.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.meterreader)
        Me.GroupBox2.Location = New System.Drawing.Point(607, 6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(203, 50)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Meter Reader"
        '
        'meterreader
        '
        Me.meterreader.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.meterreader.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.meterreader.FormattingEnabled = True
        Me.meterreader.Location = New System.Drawing.Point(6, 19)
        Me.meterreader.Name = "meterreader"
        Me.meterreader.Size = New System.Drawing.Size(191, 24)
        Me.meterreader.TabIndex = 0
        '
        'reprintBIllsReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(928, 554)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dateto)
        Me.Controls.Add(Me.datefrom)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "reprintBIllsReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reprinted Bills Report"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents Button1 As Button
    Friend WithEvents datefrom As DateTimePicker
    Friend WithEvents dateto As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents detailed As RadioButton
    Friend WithEvents summary As RadioButton
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents meterreader As ComboBox
End Class
