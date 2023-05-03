<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LedgerReport
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.prog = New System.Windows.Forms.ProgressBar()
        Me.billSearch = New System.Windows.Forms.Button()
        Me.dtpfrom = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpto = New System.Windows.Forms.DateTimePicker()
        Me.cball = New System.Windows.Forms.CheckBox()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtaccountno = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.txtaccountno)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.cball)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.dtpto)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.dtpfrom)
        Me.Panel1.Controls.Add(Me.prog)
        Me.Panel1.Controls.Add(Me.billSearch)
        Me.Panel1.Controls.Add(Me.ReportViewer1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(960, 485)
        Me.Panel1.TabIndex = 8
        '
        'prog
        '
        Me.prog.Location = New System.Drawing.Point(761, 13)
        Me.prog.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.prog.Name = "prog"
        Me.prog.Size = New System.Drawing.Size(190, 28)
        Me.prog.TabIndex = 68
        '
        'billSearch
        '
        Me.billSearch.FlatAppearance.BorderSize = 0
        Me.billSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.billSearch.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.billSearch.Location = New System.Drawing.Point(575, 11)
        Me.billSearch.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.billSearch.Name = "billSearch"
        Me.billSearch.Size = New System.Drawing.Size(180, 34)
        Me.billSearch.TabIndex = 40
        Me.billSearch.Text = "Generate"
        Me.billSearch.UseVisualStyleBackColor = True
        '
        'dtpfrom
        '
        Me.dtpfrom.CustomFormat = "yyyy-MM-dd"
        Me.dtpfrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpfrom.Location = New System.Drawing.Point(51, 16)
        Me.dtpfrom.Name = "dtpfrom"
        Me.dtpfrom.Size = New System.Drawing.Size(109, 20)
        Me.dtpfrom.TabIndex = 69
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 15)
        Me.Label1.TabIndex = 70
        Me.Label1.Text = "From"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(166, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(19, 15)
        Me.Label2.TabIndex = 72
        Me.Label2.Text = "To"
        '
        'dtpto
        '
        Me.dtpto.CustomFormat = "yyyy-MM-dd"
        Me.dtpto.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpto.Location = New System.Drawing.Point(191, 16)
        Me.dtpto.Name = "dtpto"
        Me.dtpto.Size = New System.Drawing.Size(109, 20)
        Me.dtpto.TabIndex = 71
        '
        'cball
        '
        Me.cball.AutoSize = True
        Me.cball.Location = New System.Drawing.Point(306, 17)
        Me.cball.Name = "cball"
        Me.cball.Size = New System.Drawing.Size(37, 17)
        Me.cball.TabIndex = 73
        Me.cball.Text = "All"
        Me.cball.UseVisualStyleBackColor = True
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ReportViewer1.Location = New System.Drawing.Point(10, 49)
        Me.ReportViewer1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(941, 432)
        Me.ReportViewer1.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(349, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 15)
        Me.Label3.TabIndex = 74
        Me.Label3.Text = "Account No."
        '
        'txtaccountno
        '
        Me.txtaccountno.Location = New System.Drawing.Point(430, 14)
        Me.txtaccountno.Name = "txtaccountno"
        Me.txtaccountno.Size = New System.Drawing.Size(100, 20)
        Me.txtaccountno.TabIndex = 75
        '
        'LedgerReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(960, 485)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "LedgerReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "LedgerReport"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents prog As ProgressBar
    Friend WithEvents billSearch As Button
    Friend WithEvents dtpfrom As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents dtpto As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents cball As CheckBox
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents Label3 As Label
    Friend WithEvents txtaccountno As TextBox
End Class
