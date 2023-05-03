<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MasterListofServiceConnections
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
        Me.cbstatus = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cbsenior = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbmetersize = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbzone = New System.Windows.Forms.ComboBox()
        Me.billSearch = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
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
        Me.Panel1.Controls.Add(Me.cbstatus)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.cbsenior)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.cbmetersize)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.cbzone)
        Me.Panel1.Controls.Add(Me.billSearch)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.ReportViewer1)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(930, 657)
        Me.Panel1.TabIndex = 3
        '
        'cbstatus
        '
        Me.cbstatus.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cbstatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbstatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbstatus.FormattingEnabled = True
        Me.cbstatus.Items.AddRange(New Object() {"All", "Active", "Disconnected", "Don't Bill", "Closed"})
        Me.cbstatus.Location = New System.Drawing.Point(78, 106)
        Me.cbstatus.Name = "cbstatus"
        Me.cbstatus.Size = New System.Drawing.Size(166, 24)
        Me.cbstatus.TabIndex = 62
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(34, 109)
        Me.Label5.Margin = New System.Windows.Forms.Padding(3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(38, 15)
        Me.Label5.TabIndex = 61
        Me.Label5.Text = "Status"
        '
        'cbsenior
        '
        Me.cbsenior.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cbsenior.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbsenior.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbsenior.FormattingEnabled = True
        Me.cbsenior.Items.AddRange(New Object() {"All", "Yes", "No"})
        Me.cbsenior.Location = New System.Drawing.Point(78, 76)
        Me.cbsenior.Name = "cbsenior"
        Me.cbsenior.Size = New System.Drawing.Size(166, 24)
        Me.cbsenior.TabIndex = 60
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(31, 79)
        Me.Label4.Margin = New System.Windows.Forms.Padding(3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 15)
        Me.Label4.TabIndex = 59
        Me.Label4.Text = "Senior"
        '
        'cbmetersize
        '
        Me.cbmetersize.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cbmetersize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbmetersize.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbmetersize.FormattingEnabled = True
        Me.cbmetersize.Location = New System.Drawing.Point(78, 46)
        Me.cbmetersize.Name = "cbmetersize"
        Me.cbmetersize.Size = New System.Drawing.Size(166, 24)
        Me.cbmetersize.TabIndex = 58
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(11, 49)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 15)
        Me.Label3.TabIndex = 57
        Me.Label3.Text = "Meter size"
        '
        'cbzone
        '
        Me.cbzone.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cbzone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbzone.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbzone.FormattingEnabled = True
        Me.cbzone.Location = New System.Drawing.Point(78, 16)
        Me.cbzone.Name = "cbzone"
        Me.cbzone.Size = New System.Drawing.Size(166, 24)
        Me.cbzone.TabIndex = 56
        '
        'billSearch
        '
        Me.billSearch.FlatAppearance.BorderSize = 0
        Me.billSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.billSearch.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.billSearch.Location = New System.Drawing.Point(737, 46)
        Me.billSearch.Name = "billSearch"
        Me.billSearch.Size = New System.Drawing.Size(180, 44)
        Me.billSearch.TabIndex = 40
        Me.billSearch.Text = "Generate"
        Me.billSearch.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(29, 19)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 15)
        Me.Label1.TabIndex = 55
        Me.Label1.Text = "Zone"
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ReportViewer1.Location = New System.Drawing.Point(3, 145)
        Me.ReportViewer1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(924, 512)
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
        Me.GroupBox1.Location = New System.Drawing.Point(250, 9)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(468, 123)
        Me.GroupBox1.TabIndex = 50
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Classification"
        '
        'cbbulk
        '
        Me.cbbulk.AutoSize = True
        Me.cbbulk.Location = New System.Drawing.Point(292, 79)
        Me.cbbulk.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbbulk.Name = "cbbulk"
        Me.cbbulk.Size = New System.Drawing.Size(107, 20)
        Me.cbbulk.TabIndex = 49
        Me.cbbulk.Text = "Bulk/Wholesale"
        Me.cbbulk.UseVisualStyleBackColor = True
        '
        'cbselectall
        '
        Me.cbselectall.AutoSize = True
        Me.cbselectall.Location = New System.Drawing.Point(181, 16)
        Me.cbselectall.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbselectall.Name = "cbselectall"
        Me.cbselectall.Size = New System.Drawing.Size(74, 20)
        Me.cbselectall.TabIndex = 43
        Me.cbselectall.Text = "Select All"
        Me.cbselectall.UseVisualStyleBackColor = True
        '
        'cbresid
        '
        Me.cbresid.AutoSize = True
        Me.cbresid.Location = New System.Drawing.Point(30, 46)
        Me.cbresid.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbresid.Name = "cbresid"
        Me.cbresid.Size = New System.Drawing.Size(85, 20)
        Me.cbresid.TabIndex = 48
        Me.cbresid.Text = "Residential"
        Me.cbresid.UseVisualStyleBackColor = True
        '
        'cbcommB
        '
        Me.cbcommB.AutoSize = True
        Me.cbcommB.Location = New System.Drawing.Point(162, 47)
        Me.cbcommB.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbcommB.Name = "cbcommB"
        Me.cbcommB.Size = New System.Drawing.Size(102, 20)
        Me.cbcommB.TabIndex = 44
        Me.cbcommB.Text = "Commercial-B"
        Me.cbcommB.UseVisualStyleBackColor = True
        '
        'cbcommC
        '
        Me.cbcommC.AutoSize = True
        Me.cbcommC.Location = New System.Drawing.Point(162, 79)
        Me.cbcommC.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbcommC.Name = "cbcommC"
        Me.cbcommC.Size = New System.Drawing.Size(105, 20)
        Me.cbcommC.TabIndex = 47
        Me.cbcommC.Text = "Commercial-C"
        Me.cbcommC.UseVisualStyleBackColor = True
        '
        'cbcommInd
        '
        Me.cbcommInd.AutoSize = True
        Me.cbcommInd.Location = New System.Drawing.Point(292, 47)
        Me.cbcommInd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbcommInd.Name = "cbcommInd"
        Me.cbcommInd.Size = New System.Drawing.Size(145, 20)
        Me.cbcommInd.TabIndex = 45
        Me.cbcommInd.Text = "Commercial/Industrial"
        Me.cbcommInd.UseVisualStyleBackColor = True
        '
        'cbcommA
        '
        Me.cbcommA.AutoSize = True
        Me.cbcommA.Location = New System.Drawing.Point(30, 80)
        Me.cbcommA.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.Label7.Location = New System.Drawing.Point(687, 113)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(230, 17)
        Me.Label7.TabIndex = 54
        Me.Label7.Text = "Master List of Service Connection"
        '
        'Timer1
        '
        '
        'MasterListofServiceConnections
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(195, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(930, 657)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "MasterListofServiceConnections"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Master List of Service Connections"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents billSearch As Button
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
    Friend WithEvents cbsenior As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents cbstatus As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Timer1 As Timer
End Class
