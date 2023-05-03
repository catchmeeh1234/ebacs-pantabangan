<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConsumptionList
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
        Me.components = New System.ComponentModel.Container()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cbmetersize = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbzone = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.billSearch = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ReadingDate = New System.Windows.Forms.DateTimePicker()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cbbulk = New System.Windows.Forms.CheckBox()
        Me.cbselectall = New System.Windows.Forms.CheckBox()
        Me.cbresid = New System.Windows.Forms.CheckBox()
        Me.cbcommB = New System.Windows.Forms.CheckBox()
        Me.cbcommC = New System.Windows.Forms.CheckBox()
        Me.cbcommInd = New System.Windows.Forms.CheckBox()
        Me.cbcommA = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.cbmetersize)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.cbzone)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.billSearch)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.ReadingDate)
        Me.Panel1.Controls.Add(Me.ReportViewer1)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(937, 548)
        Me.Panel1.TabIndex = 3
        '
        'cbmetersize
        '
        Me.cbmetersize.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cbmetersize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbmetersize.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbmetersize.FormattingEnabled = True
        Me.cbmetersize.Location = New System.Drawing.Point(95, 78)
        Me.cbmetersize.Name = "cbmetersize"
        Me.cbmetersize.Size = New System.Drawing.Size(143, 24)
        Me.cbmetersize.TabIndex = 54
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(28, 81)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 15)
        Me.Label3.TabIndex = 53
        Me.Label3.Text = "Meter size"
        '
        'cbzone
        '
        Me.cbzone.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cbzone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbzone.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbzone.FormattingEnabled = True
        Me.cbzone.Location = New System.Drawing.Point(95, 48)
        Me.cbzone.Name = "cbzone"
        Me.cbzone.Size = New System.Drawing.Size(143, 24)
        Me.cbzone.TabIndex = 52
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(55, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 15)
        Me.Label1.TabIndex = 51
        Me.Label1.Text = "Zone"
        '
        'billSearch
        '
        Me.billSearch.FlatAppearance.BorderSize = 0
        Me.billSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.billSearch.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.billSearch.Location = New System.Drawing.Point(651, 48)
        Me.billSearch.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.billSearch.Name = "billSearch"
        Me.billSearch.Size = New System.Drawing.Size(154, 28)
        Me.billSearch.TabIndex = 40
        Me.billSearch.Text = "Generate"
        Me.billSearch.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 15)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Billing Month"
        '
        'ReadingDate
        '
        Me.ReadingDate.CustomFormat = "MMMM yyyy"
        Me.ReadingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ReadingDate.Location = New System.Drawing.Point(95, 21)
        Me.ReadingDate.Name = "ReadingDate"
        Me.ReadingDate.Size = New System.Drawing.Size(143, 21)
        Me.ReadingDate.TabIndex = 4
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ReportViewer1.Location = New System.Drawing.Point(0, 118)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(934, 430)
        Me.ReportViewer1.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cbbulk)
        Me.GroupBox1.Controls.Add(Me.cbselectall)
        Me.GroupBox1.Controls.Add(Me.cbresid)
        Me.GroupBox1.Controls.Add(Me.cbcommB)
        Me.GroupBox1.Controls.Add(Me.cbcommC)
        Me.GroupBox1.Controls.Add(Me.cbcommInd)
        Me.GroupBox1.Controls.Add(Me.cbcommA)
        Me.GroupBox1.Location = New System.Drawing.Point(244, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(401, 100)
        Me.GroupBox1.TabIndex = 50
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Classification"
        '
        'cbbulk
        '
        Me.cbbulk.AutoSize = True
        Me.cbbulk.Location = New System.Drawing.Point(250, 64)
        Me.cbbulk.Name = "cbbulk"
        Me.cbbulk.Size = New System.Drawing.Size(107, 20)
        Me.cbbulk.TabIndex = 49
        Me.cbbulk.Text = "Bulk/Wholesale"
        Me.cbbulk.UseVisualStyleBackColor = True
        '
        'cbselectall
        '
        Me.cbselectall.AutoSize = True
        Me.cbselectall.Location = New System.Drawing.Point(155, 13)
        Me.cbselectall.Name = "cbselectall"
        Me.cbselectall.Size = New System.Drawing.Size(74, 20)
        Me.cbselectall.TabIndex = 43
        Me.cbselectall.Text = "Select All"
        Me.cbselectall.UseVisualStyleBackColor = True
        '
        'cbresid
        '
        Me.cbresid.AutoSize = True
        Me.cbresid.Location = New System.Drawing.Point(26, 37)
        Me.cbresid.Name = "cbresid"
        Me.cbresid.Size = New System.Drawing.Size(85, 20)
        Me.cbresid.TabIndex = 48
        Me.cbresid.Text = "Residential"
        Me.cbresid.UseVisualStyleBackColor = True
        '
        'cbcommB
        '
        Me.cbcommB.AutoSize = True
        Me.cbcommB.Location = New System.Drawing.Point(139, 38)
        Me.cbcommB.Name = "cbcommB"
        Me.cbcommB.Size = New System.Drawing.Size(102, 20)
        Me.cbcommB.TabIndex = 44
        Me.cbcommB.Text = "Commercial-B"
        Me.cbcommB.UseVisualStyleBackColor = True
        '
        'cbcommC
        '
        Me.cbcommC.AutoSize = True
        Me.cbcommC.Location = New System.Drawing.Point(139, 64)
        Me.cbcommC.Name = "cbcommC"
        Me.cbcommC.Size = New System.Drawing.Size(105, 20)
        Me.cbcommC.TabIndex = 47
        Me.cbcommC.Text = "Commercial-C"
        Me.cbcommC.UseVisualStyleBackColor = True
        '
        'cbcommInd
        '
        Me.cbcommInd.AutoSize = True
        Me.cbcommInd.Location = New System.Drawing.Point(250, 38)
        Me.cbcommInd.Name = "cbcommInd"
        Me.cbcommInd.Size = New System.Drawing.Size(145, 20)
        Me.cbcommInd.TabIndex = 45
        Me.cbcommInd.Text = "Commercial/Industrial"
        Me.cbcommInd.UseVisualStyleBackColor = True
        '
        'cbcommA
        '
        Me.cbcommA.AutoSize = True
        Me.cbcommA.Location = New System.Drawing.Point(26, 65)
        Me.cbcommA.Name = "cbcommA"
        Me.cbcommA.Size = New System.Drawing.Size(103, 20)
        Me.cbcommA.TabIndex = 46
        Me.cbcommA.Text = "Commercial-A"
        Me.cbcommA.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Century Gothic", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(668, 27)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(122, 17)
        Me.Label7.TabIndex = 54
        Me.Label7.Text = "Consumption List"
        '
        'Timer1
        '
        '
        'ConsumptionList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(195, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(937, 548)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "ConsumptionList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ConsumptionList"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents billSearch As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents ReadingDate As DateTimePicker
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents cbbulk As CheckBox
    Friend WithEvents cbselectall As CheckBox
    Friend WithEvents cbresid As CheckBox
    Friend WithEvents cbcommB As CheckBox
    Friend WithEvents cbcommC As CheckBox
    Friend WithEvents cbcommInd As CheckBox
    Friend WithEvents cbcommA As CheckBox
    Friend WithEvents Label7 As Label
    Friend WithEvents cbmetersize As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents cbzone As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Timer1 As Timer
End Class
