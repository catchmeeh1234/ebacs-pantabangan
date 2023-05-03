<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SettingsAccounts
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SettingsAccounts))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtnickname = New System.Windows.Forms.TextBox()
        Me.lblnickname = New System.Windows.Forms.Label()
        Me.cbviewer = New System.Windows.Forms.CheckBox()
        Me.cbadmin = New System.Windows.Forms.CheckBox()
        Me.cbcservice = New System.Windows.Forms.CheckBox()
        Me.cbcashier = New System.Windows.Forms.CheckBox()
        Me.cboffice = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtdesignation = New System.Windows.Forms.TextBox()
        Me.lbldesignation = New System.Windows.Forms.Label()
        Me.lblmode = New System.Windows.Forms.Label()
        Me.lblpassword = New System.Windows.Forms.Label()
        Me.txtpass = New System.Windows.Forms.TextBox()
        Me.readerNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblusername = New System.Windows.Forms.Label()
        Me.txtfullname = New System.Windows.Forms.TextBox()
        Me.txtusername = New System.Windows.Forms.TextBox()
        Me.lblfullname = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtconfirmpass = New System.Windows.Forms.TextBox()
        Me.dgvlist = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvlist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.MenuStrip1)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.dgvlist)
        Me.Panel1.Location = New System.Drawing.Point(2, 38)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(934, 329)
        Me.Panel1.TabIndex = 10
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(7, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(934, 24)
        Me.MenuStrip1.TabIndex = 13
        Me.MenuStrip1.Text = "MenuStrip1"
        Me.MenuStrip1.Visible = False
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem1, Me.EditToolStripMenuItem, Me.SaveToolStripMenuItem, Me.DeleteToolStripMenuItem, Me.RefreshToolStripMenuItem})
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
        Me.NewToolStripMenuItem.Text = "file"
        '
        'NewToolStripMenuItem1
        '
        Me.NewToolStripMenuItem1.Name = "NewToolStripMenuItem1"
        Me.NewToolStripMenuItem1.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.NewToolStripMenuItem1.Size = New System.Drawing.Size(189, 22)
        Me.NewToolStripMenuItem1.Text = "new"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.EditToolStripMenuItem.Text = "edit"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.SaveToolStripMenuItem.Text = "save"
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.Delete), System.Windows.Forms.Keys)
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.DeleteToolStripMenuItem.Text = "delete"
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.RefreshToolStripMenuItem.Text = "refresh"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.txtnickname)
        Me.GroupBox1.Controls.Add(Me.lblnickname)
        Me.GroupBox1.Controls.Add(Me.cbviewer)
        Me.GroupBox1.Controls.Add(Me.cbadmin)
        Me.GroupBox1.Controls.Add(Me.cbcservice)
        Me.GroupBox1.Controls.Add(Me.cbcashier)
        Me.GroupBox1.Controls.Add(Me.cboffice)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtdesignation)
        Me.GroupBox1.Controls.Add(Me.lbldesignation)
        Me.GroupBox1.Controls.Add(Me.lblmode)
        Me.GroupBox1.Controls.Add(Me.lblpassword)
        Me.GroupBox1.Controls.Add(Me.txtpass)
        Me.GroupBox1.Controls.Add(Me.readerNo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.lblusername)
        Me.GroupBox1.Controls.Add(Me.txtfullname)
        Me.GroupBox1.Controls.Add(Me.txtusername)
        Me.GroupBox1.Controls.Add(Me.lblfullname)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtconfirmpass)
        Me.GroupBox1.Location = New System.Drawing.Point(568, 4)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(356, 316)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Reader Information"
        '
        'txtnickname
        '
        Me.txtnickname.BackColor = System.Drawing.Color.White
        Me.txtnickname.Location = New System.Drawing.Point(140, 175)
        Me.txtnickname.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtnickname.Name = "txtnickname"
        Me.txtnickname.ReadOnly = True
        Me.txtnickname.Size = New System.Drawing.Size(201, 21)
        Me.txtnickname.TabIndex = 28
        '
        'lblnickname
        '
        Me.lblnickname.AutoSize = True
        Me.lblnickname.Location = New System.Drawing.Point(13, 179)
        Me.lblnickname.Name = "lblnickname"
        Me.lblnickname.Size = New System.Drawing.Size(66, 16)
        Me.lblnickname.TabIndex = 29
        Me.lblnickname.Text = "Nick Name"
        '
        'cbviewer
        '
        Me.cbviewer.AutoSize = True
        Me.cbviewer.Enabled = False
        Me.cbviewer.Location = New System.Drawing.Point(85, 276)
        Me.cbviewer.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbviewer.Name = "cbviewer"
        Me.cbviewer.Size = New System.Drawing.Size(63, 20)
        Me.cbviewer.TabIndex = 27
        Me.cbviewer.Text = "Viewer"
        Me.cbviewer.UseVisualStyleBackColor = True
        '
        'cbadmin
        '
        Me.cbadmin.AutoSize = True
        Me.cbadmin.Enabled = False
        Me.cbadmin.Location = New System.Drawing.Point(157, 276)
        Me.cbadmin.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbadmin.Name = "cbadmin"
        Me.cbadmin.Size = New System.Drawing.Size(61, 20)
        Me.cbadmin.TabIndex = 26
        Me.cbadmin.Text = "Admin"
        Me.cbadmin.UseVisualStyleBackColor = True
        '
        'cbcservice
        '
        Me.cbcservice.AutoSize = True
        Me.cbcservice.Enabled = False
        Me.cbcservice.Location = New System.Drawing.Point(229, 276)
        Me.cbcservice.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbcservice.Name = "cbcservice"
        Me.cbcservice.Size = New System.Drawing.Size(121, 20)
        Me.cbcservice.TabIndex = 25
        Me.cbcservice.Text = "Customer Service"
        Me.cbcservice.UseVisualStyleBackColor = True
        '
        'cbcashier
        '
        Me.cbcashier.AutoSize = True
        Me.cbcashier.Enabled = False
        Me.cbcashier.Location = New System.Drawing.Point(7, 276)
        Me.cbcashier.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbcashier.Name = "cbcashier"
        Me.cbcashier.Size = New System.Drawing.Size(67, 20)
        Me.cbcashier.TabIndex = 24
        Me.cbcashier.Text = "Cashier"
        Me.cbcashier.UseVisualStyleBackColor = True
        '
        'cboffice
        '
        Me.cboffice.BackColor = System.Drawing.Color.White
        Me.cboffice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboffice.Enabled = False
        Me.cboffice.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboffice.FormattingEnabled = True
        Me.cboffice.Location = New System.Drawing.Point(140, 236)
        Me.cboffice.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cboffice.Name = "cboffice"
        Me.cboffice.Size = New System.Drawing.Size(201, 24)
        Me.cboffice.TabIndex = 23
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 240)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 16)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Office"
        '
        'txtdesignation
        '
        Me.txtdesignation.BackColor = System.Drawing.Color.White
        Me.txtdesignation.Location = New System.Drawing.Point(140, 204)
        Me.txtdesignation.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtdesignation.Name = "txtdesignation"
        Me.txtdesignation.ReadOnly = True
        Me.txtdesignation.Size = New System.Drawing.Size(201, 21)
        Me.txtdesignation.TabIndex = 19
        '
        'lbldesignation
        '
        Me.lbldesignation.AutoSize = True
        Me.lbldesignation.Location = New System.Drawing.Point(13, 208)
        Me.lbldesignation.Name = "lbldesignation"
        Me.lbldesignation.Size = New System.Drawing.Size(71, 16)
        Me.lbldesignation.TabIndex = 20
        Me.lbldesignation.Text = "Designation"
        '
        'lblmode
        '
        Me.lblmode.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmode.Location = New System.Drawing.Point(192, 12)
        Me.lblmode.Name = "lblmode"
        Me.lblmode.Size = New System.Drawing.Size(89, 31)
        Me.lblmode.TabIndex = 18
        Me.lblmode.Text = "Mode"
        Me.lblmode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblmode.Visible = False
        '
        'lblpassword
        '
        Me.lblpassword.AutoSize = True
        Me.lblpassword.Location = New System.Drawing.Point(13, 83)
        Me.lblpassword.Name = "lblpassword"
        Me.lblpassword.Size = New System.Drawing.Size(59, 16)
        Me.lblpassword.TabIndex = 11
        Me.lblpassword.Text = "Password"
        '
        'txtpass
        '
        Me.txtpass.BackColor = System.Drawing.Color.White
        Me.txtpass.Location = New System.Drawing.Point(140, 79)
        Me.txtpass.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtpass.Name = "txtpass"
        Me.txtpass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtpass.ReadOnly = True
        Me.txtpass.Size = New System.Drawing.Size(201, 21)
        Me.txtpass.TabIndex = 4
        '
        'readerNo
        '
        Me.readerNo.BackColor = System.Drawing.Color.White
        Me.readerNo.Location = New System.Drawing.Point(140, 17)
        Me.readerNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.readerNo.Name = "readerNo"
        Me.readerNo.ReadOnly = True
        Me.readerNo.Size = New System.Drawing.Size(46, 21)
        Me.readerNo.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "No."
        '
        'lblusername
        '
        Me.lblusername.AutoSize = True
        Me.lblusername.Location = New System.Drawing.Point(13, 50)
        Me.lblusername.Name = "lblusername"
        Me.lblusername.Size = New System.Drawing.Size(62, 16)
        Me.lblusername.TabIndex = 3
        Me.lblusername.Text = "Username"
        '
        'txtfullname
        '
        Me.txtfullname.BackColor = System.Drawing.Color.White
        Me.txtfullname.Location = New System.Drawing.Point(140, 146)
        Me.txtfullname.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtfullname.Name = "txtfullname"
        Me.txtfullname.ReadOnly = True
        Me.txtfullname.Size = New System.Drawing.Size(201, 21)
        Me.txtfullname.TabIndex = 6
        '
        'txtusername
        '
        Me.txtusername.BackColor = System.Drawing.Color.White
        Me.txtusername.Location = New System.Drawing.Point(140, 46)
        Me.txtusername.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtusername.Name = "txtusername"
        Me.txtusername.ReadOnly = True
        Me.txtusername.Size = New System.Drawing.Size(201, 21)
        Me.txtusername.TabIndex = 3
        '
        'lblfullname
        '
        Me.lblfullname.AutoSize = True
        Me.lblfullname.Location = New System.Drawing.Point(13, 150)
        Me.lblfullname.Name = "lblfullname"
        Me.lblfullname.Size = New System.Drawing.Size(60, 16)
        Me.lblfullname.TabIndex = 7
        Me.lblfullname.Text = "Full Name"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 116)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(103, 16)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Confirm Password"
        '
        'txtconfirmpass
        '
        Me.txtconfirmpass.BackColor = System.Drawing.Color.White
        Me.txtconfirmpass.Location = New System.Drawing.Point(140, 113)
        Me.txtconfirmpass.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtconfirmpass.Name = "txtconfirmpass"
        Me.txtconfirmpass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtconfirmpass.ReadOnly = True
        Me.txtconfirmpass.Size = New System.Drawing.Size(201, 21)
        Me.txtconfirmpass.TabIndex = 5
        '
        'dgvlist
        '
        Me.dgvlist.AllowUserToAddRows = False
        Me.dgvlist.AllowUserToDeleteRows = False
        Me.dgvlist.BackgroundColor = System.Drawing.Color.White
        Me.dgvlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvlist.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6})
        Me.dgvlist.Location = New System.Drawing.Point(9, 11)
        Me.dgvlist.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgvlist.Name = "dgvlist"
        Me.dgvlist.ReadOnly = True
        Me.dgvlist.RowHeadersVisible = False
        Me.dgvlist.Size = New System.Drawing.Size(553, 309)
        Me.dgvlist.TabIndex = 1
        '
        'Column1
        '
        Me.Column1.HeaderText = "No."
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Visible = False
        Me.Column1.Width = 50
        '
        'Column2
        '
        Me.Column2.HeaderText = "User Name"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.HeaderText = "Full Name"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 120
        '
        'Column4
        '
        Me.Column4.HeaderText = "Designation"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Column5
        '
        Me.Column5.HeaderText = "Office"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Width = 80
        '
        'Column6
        '
        Me.Column6.HeaderText = "Last Activity Date"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        Me.Column6.Width = 125
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Century Gothic", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(12, 11)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(158, 17)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "User Accounts Settings"
        '
        'Button2
        '
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(195, Byte), Integer))
        Me.Button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(195, Byte), Integer))
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button2.Location = New System.Drawing.Point(864, 6)
        Me.Button2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(68, 27)
        Me.Button2.TabIndex = 12
        Me.Button2.Text = " Close"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(286, 15)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(63, 23)
        Me.Button1.TabIndex = 30
        Me.Button1.Text = "Unlock"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'SettingsAccounts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(195, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(938, 369)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "SettingsAccounts"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SettingsAccounts"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvlist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lblmode As Label
    Friend WithEvents lblpassword As Label
    Friend WithEvents txtpass As TextBox
    Friend WithEvents readerNo As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lblusername As Label
    Friend WithEvents txtfullname As TextBox
    Friend WithEvents txtusername As TextBox
    Friend WithEvents lblfullname As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtconfirmpass As TextBox
    Friend WithEvents dgvlist As DataGridView
    Friend WithEvents Label9 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents cboffice As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtdesignation As TextBox
    Friend WithEvents lbldesignation As Label
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents NewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NewToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RefreshToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents cbviewer As CheckBox
    Friend WithEvents cbadmin As CheckBox
    Friend WithEvents cbcservice As CheckBox
    Friend WithEvents cbcashier As CheckBox
    Friend WithEvents txtnickname As TextBox
    Friend WithEvents lblnickname As Label
    Friend WithEvents Button1 As Button
End Class
