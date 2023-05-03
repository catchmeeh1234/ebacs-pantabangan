<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Collection_CR
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Collection_CR))
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.printcrdocs = New System.Drawing.Printing.PrintDocument()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FindToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmsCR = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DetailsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.billamountdue = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.billdiscount = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.billadvancepayment = New System.Windows.Forms.TextBox()
        Me.grouparrears = New System.Windows.Forms.GroupBox()
        Me.dgvothers = New System.Windows.Forms.DataGridView()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewCheckBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.dgvotherchages = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.billarrears = New System.Windows.Forms.DataGridView()
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewLinkColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.billcharges = New System.Windows.Forms.DataGridView()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CRBillno = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.billPenalty = New System.Windows.Forms.TextBox()
        Me.currentBilling = New System.Windows.Forms.CheckBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.billAdjustment = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.billEarlyDisc = New System.Windows.Forms.TextBox()
        Me.crno = New System.Windows.Forms.TextBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.chkmode = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.billAddress = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.billName = New System.Windows.Forms.TextBox()
        Me.billZone = New System.Windows.Forms.TextBox()
        Me.billAccountNo = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.billbillingmonth = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.accSearch = New System.Windows.Forms.PictureBox()
        Me.discdate = New System.Windows.Forms.TextBox()
        Me.lblaccstatus = New System.Windows.Forms.Label()
        Me.paymentgroup = New System.Windows.Forms.GroupBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.billTotalamountdue = New System.Windows.Forms.TextBox()
        Me.billsave = New System.Windows.Forms.Button()
        Me.amountpaid = New System.Windows.Forms.TextBox()
        Me.overpayment = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.reprint = New System.Windows.Forms.Button()
        Me.autoprint = New System.Windows.Forms.CheckBox()
        Me.cashcheck = New System.Windows.Forms.GroupBox()
        Me.billcheckno = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.billcheckdate = New System.Windows.Forms.TextBox()
        Me.rbcash = New System.Windows.Forms.RadioButton()
        Me.rbcheck = New System.Windows.Forms.RadioButton()
        Me.rbOnline = New System.Windows.Forms.RadioButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.MenuStrip1.SuspendLayout()
        Me.cmsCR.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.grouparrears.SuspendLayout()
        CType(Me.dgvothers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvotherchages, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.billarrears, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.billcharges, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.accSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.paymentgroup.SuspendLayout()
        Me.cashcheck.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(195, Byte), Integer))
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(195, Byte), Integer))
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(441, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(72, 30)
        Me.Button1.TabIndex = 47
        Me.Button1.Text = " Close"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Century Gothic", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(12, 11)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(77, 17)
        Me.Label7.TabIndex = 48
        Me.Label7.Text = "Create CR"
        '
        'printcrdocs
        '
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(520, 24)
        Me.MenuStrip1.TabIndex = 49
        Me.MenuStrip1.Text = "MenuStrip1"
        Me.MenuStrip1.Visible = False
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefreshToolStripMenuItem, Me.FindToolStripMenuItem, Me.SaveToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(137, 22)
        Me.RefreshToolStripMenuItem.Text = "Refresh"
        '
        'FindToolStripMenuItem
        '
        Me.FindToolStripMenuItem.Name = "FindToolStripMenuItem"
        Me.FindToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.FindToolStripMenuItem.Size = New System.Drawing.Size(137, 22)
        Me.FindToolStripMenuItem.Text = "Find"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(137, 22)
        Me.SaveToolStripMenuItem.Text = "save"
        '
        'cmsCR
        '
        Me.cmsCR.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DetailsToolStripMenuItem})
        Me.cmsCR.Name = "cmsCR"
        Me.cmsCR.Size = New System.Drawing.Size(117, 26)
        '
        'DetailsToolStripMenuItem
        '
        Me.DetailsToolStripMenuItem.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DetailsToolStripMenuItem.Name = "DetailsToolStripMenuItem"
        Me.DetailsToolStripMenuItem.Size = New System.Drawing.Size(116, 22)
        Me.DetailsToolStripMenuItem.Text = "Details"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.billEarlyDisc)
        Me.GroupBox6.Controls.Add(Me.Label16)
        Me.GroupBox6.Controls.Add(Me.billAdjustment)
        Me.GroupBox6.Controls.Add(Me.Label13)
        Me.GroupBox6.Controls.Add(Me.currentBilling)
        Me.GroupBox6.Controls.Add(Me.billPenalty)
        Me.GroupBox6.Controls.Add(Me.Label1)
        Me.GroupBox6.Controls.Add(Me.Label24)
        Me.GroupBox6.Controls.Add(Me.CRBillno)
        Me.GroupBox6.Controls.Add(Me.billcharges)
        Me.GroupBox6.Controls.Add(Me.billadvancepayment)
        Me.GroupBox6.Controls.Add(Me.Label21)
        Me.GroupBox6.Controls.Add(Me.billdiscount)
        Me.GroupBox6.Controls.Add(Me.Label20)
        Me.GroupBox6.Controls.Add(Me.billamountdue)
        Me.GroupBox6.Controls.Add(Me.Label19)
        Me.GroupBox6.Location = New System.Drawing.Point(8, 145)
        Me.GroupBox6.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox6.Size = New System.Drawing.Size(497, 222)
        Me.GroupBox6.TabIndex = 38
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Current"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(37, 44)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(73, 15)
        Me.Label19.TabIndex = 25
        Me.Label19.Text = "Amount Due"
        '
        'billamountdue
        '
        Me.billamountdue.BackColor = System.Drawing.Color.White
        Me.billamountdue.Location = New System.Drawing.Point(116, 44)
        Me.billamountdue.Name = "billamountdue"
        Me.billamountdue.ReadOnly = True
        Me.billamountdue.Size = New System.Drawing.Size(100, 21)
        Me.billamountdue.TabIndex = 9
        Me.billamountdue.Text = "0.00"
        Me.billamountdue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(37, 98)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(73, 15)
        Me.Label20.TabIndex = 27
        Me.Label20.Text = "S.C discount"
        '
        'billdiscount
        '
        Me.billdiscount.BackColor = System.Drawing.Color.White
        Me.billdiscount.Location = New System.Drawing.Point(116, 98)
        Me.billdiscount.Name = "billdiscount"
        Me.billdiscount.ReadOnly = True
        Me.billdiscount.Size = New System.Drawing.Size(100, 21)
        Me.billdiscount.TabIndex = 11
        Me.billdiscount.Text = "0.00"
        Me.billdiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(70, 152)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(39, 15)
        Me.Label21.TabIndex = 29
        Me.Label21.Text = "Credit"
        '
        'billadvancepayment
        '
        Me.billadvancepayment.BackColor = System.Drawing.Color.White
        Me.billadvancepayment.Location = New System.Drawing.Point(116, 152)
        Me.billadvancepayment.Name = "billadvancepayment"
        Me.billadvancepayment.ReadOnly = True
        Me.billadvancepayment.Size = New System.Drawing.Size(100, 21)
        Me.billadvancepayment.TabIndex = 12
        Me.billadvancepayment.Text = "0.00"
        Me.billadvancepayment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'grouparrears
        '
        Me.grouparrears.Controls.Add(Me.billarrears)
        Me.grouparrears.Location = New System.Drawing.Point(8, 375)
        Me.grouparrears.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grouparrears.Name = "grouparrears"
        Me.grouparrears.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grouparrears.Size = New System.Drawing.Size(497, 103)
        Me.grouparrears.TabIndex = 41
        Me.grouparrears.TabStop = False
        Me.grouparrears.Text = "Arrears"
        Me.grouparrears.Visible = False
        '
        'dgvothers
        '
        Me.dgvothers.AllowUserToAddRows = False
        Me.dgvothers.AllowUserToDeleteRows = False
        Me.dgvothers.AllowUserToResizeColumns = False
        Me.dgvothers.AllowUserToResizeRows = False
        Me.dgvothers.BackgroundColor = System.Drawing.Color.White
        Me.dgvothers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvothers.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewCheckBoxColumn1, Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn4, Me.Column3})
        Me.dgvothers.Location = New System.Drawing.Point(9, 486)
        Me.dgvothers.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgvothers.Name = "dgvothers"
        Me.dgvothers.RowHeadersVisible = False
        Me.dgvothers.Size = New System.Drawing.Size(238, 100)
        Me.dgvothers.TabIndex = 15
        Me.dgvothers.Visible = False
        '
        'Column3
        '
        Me.Column3.HeaderText = "ID"
        Me.Column3.Name = "Column3"
        Me.Column3.Visible = False
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "Amount"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 70
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Particulars"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 70
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Ref. No."
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn1.Width = 50
        '
        'DataGridViewCheckBoxColumn1
        '
        Me.DataGridViewCheckBoxColumn1.HeaderText = " "
        Me.DataGridViewCheckBoxColumn1.Name = "DataGridViewCheckBoxColumn1"
        Me.DataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewCheckBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.DataGridViewCheckBoxColumn1.Width = 30
        '
        'dgvotherchages
        '
        Me.dgvotherchages.AllowUserToAddRows = False
        Me.dgvotherchages.AllowUserToDeleteRows = False
        Me.dgvotherchages.BackgroundColor = System.Drawing.Color.White
        Me.dgvotherchages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvotherchages.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn6, Me.Column1})
        Me.dgvotherchages.Location = New System.Drawing.Point(268, 486)
        Me.dgvotherchages.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgvotherchages.Name = "dgvotherchages"
        Me.dgvotherchages.ReadOnly = True
        Me.dgvotherchages.RowHeadersVisible = False
        Me.dgvotherchages.Size = New System.Drawing.Size(237, 100)
        Me.dgvotherchages.TabIndex = 16
        Me.dgvotherchages.Visible = False
        '
        'Column1
        '
        Me.Column1.HeaderText = "ID"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Visible = False
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.HeaderText = "Amount"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Width = 90
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "Charges"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 130
        '
        'billarrears
        '
        Me.billarrears.AllowUserToAddRows = False
        Me.billarrears.AllowUserToDeleteRows = False
        Me.billarrears.AllowUserToResizeColumns = False
        Me.billarrears.AllowUserToResizeRows = False
        Me.billarrears.BackgroundColor = System.Drawing.Color.White
        Me.billarrears.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.billarrears.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column6, Me.DataGridViewLinkColumn1, Me.Column2, Me.DataGridViewTextBoxColumn3, Me.Column8, Me.Column9, Me.Column10})
        Me.billarrears.Location = New System.Drawing.Point(6, 18)
        Me.billarrears.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.billarrears.Name = "billarrears"
        Me.billarrears.RowHeadersVisible = False
        Me.billarrears.Size = New System.Drawing.Size(483, 80)
        Me.billarrears.TabIndex = 14
        '
        'Column10
        '
        Me.Column10.HeaderText = "cAmount"
        Me.Column10.Name = "Column10"
        Me.Column10.Visible = False
        '
        'Column9
        '
        Me.Column9.HeaderText = "Charges"
        Me.Column9.Name = "Column9"
        Me.Column9.Visible = False
        '
        'Column8
        '
        Me.Column8.HeaderText = "Billing"
        Me.Column8.Name = "Column8"
        Me.Column8.Visible = False
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Amount"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 150
        '
        'Column2
        '
        Me.Column2.HeaderText = "Period"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 150
        '
        'DataGridViewLinkColumn1
        '
        Me.DataGridViewLinkColumn1.HeaderText = "Bill No."
        Me.DataGridViewLinkColumn1.Name = "DataGridViewLinkColumn1"
        Me.DataGridViewLinkColumn1.ReadOnly = True
        Me.DataGridViewLinkColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'Column6
        '
        Me.Column6.HeaderText = " "
        Me.Column6.Name = "Column6"
        Me.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Column6.Width = 50
        '
        'billcharges
        '
        Me.billcharges.AllowUserToAddRows = False
        Me.billcharges.AllowUserToDeleteRows = False
        Me.billcharges.BackgroundColor = System.Drawing.Color.White
        Me.billcharges.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.billcharges.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column4, Me.Column7, Me.Column5})
        Me.billcharges.Location = New System.Drawing.Point(222, 17)
        Me.billcharges.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.billcharges.Name = "billcharges"
        Me.billcharges.ReadOnly = True
        Me.billcharges.RowHeadersVisible = False
        Me.billcharges.Size = New System.Drawing.Size(264, 181)
        Me.billcharges.TabIndex = 13
        '
        'Column5
        '
        Me.Column5.HeaderText = "ID"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Visible = False
        '
        'Column7
        '
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column7.DefaultCellStyle = DataGridViewCellStyle15
        Me.Column7.HeaderText = "Amount"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        Me.Column7.Width = 110
        '
        'Column4
        '
        Me.Column4.HeaderText = "Charges"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 150
        '
        'CRBillno
        '
        Me.CRBillno.BackColor = System.Drawing.Color.White
        Me.CRBillno.Location = New System.Drawing.Point(116, 17)
        Me.CRBillno.Name = "CRBillno"
        Me.CRBillno.Size = New System.Drawing.Size(100, 21)
        Me.CRBillno.TabIndex = 8
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(63, 71)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(47, 15)
        Me.Label24.TabIndex = 31
        Me.Label24.Text = "Penalty"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(65, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 15)
        Me.Label1.TabIndex = 42
        Me.Label1.Text = "Bill No."
        '
        'billPenalty
        '
        Me.billPenalty.BackColor = System.Drawing.Color.White
        Me.billPenalty.Location = New System.Drawing.Point(116, 71)
        Me.billPenalty.Name = "billPenalty"
        Me.billPenalty.ReadOnly = True
        Me.billPenalty.Size = New System.Drawing.Size(100, 21)
        Me.billPenalty.TabIndex = 10
        Me.billPenalty.Text = "0.00"
        Me.billPenalty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'currentBilling
        '
        Me.currentBilling.AutoSize = True
        Me.currentBilling.Location = New System.Drawing.Point(47, 18)
        Me.currentBilling.Name = "currentBilling"
        Me.currentBilling.Size = New System.Drawing.Size(15, 14)
        Me.currentBilling.TabIndex = 43
        Me.currentBilling.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(44, 179)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(66, 15)
        Me.Label13.TabIndex = 45
        Me.Label13.Text = "Adjustment"
        '
        'billAdjustment
        '
        Me.billAdjustment.BackColor = System.Drawing.Color.White
        Me.billAdjustment.Location = New System.Drawing.Point(116, 179)
        Me.billAdjustment.Name = "billAdjustment"
        Me.billAdjustment.ReadOnly = True
        Me.billAdjustment.Size = New System.Drawing.Size(100, 21)
        Me.billAdjustment.TabIndex = 44
        Me.billAdjustment.Text = "0.00"
        Me.billAdjustment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(3, 125)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(107, 15)
        Me.Label16.TabIndex = 45
        Me.Label16.Text = "Early payment disc"
        '
        'billEarlyDisc
        '
        Me.billEarlyDisc.BackColor = System.Drawing.Color.White
        Me.billEarlyDisc.Location = New System.Drawing.Point(116, 125)
        Me.billEarlyDisc.Name = "billEarlyDisc"
        Me.billEarlyDisc.ReadOnly = True
        Me.billEarlyDisc.Size = New System.Drawing.Size(100, 21)
        Me.billEarlyDisc.TabIndex = 44
        Me.billEarlyDisc.Text = "0.00"
        Me.billEarlyDisc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'crno
        '
        Me.crno.BackColor = System.Drawing.Color.White
        Me.crno.Location = New System.Drawing.Point(95, 8)
        Me.crno.Name = "crno"
        Me.crno.Size = New System.Drawing.Size(96, 21)
        Me.crno.TabIndex = 1
        '
        'lblStatus
        '
        Me.lblStatus.Font = New System.Drawing.Font("Century Gothic", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.Green
        Me.lblStatus.Location = New System.Drawing.Point(408, 8)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(94, 31)
        Me.lblStatus.TabIndex = 44
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblStatus.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(21, 11)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(68, 15)
        Me.Label10.TabIndex = 40
        Me.Label10.Text = "CR Number"
        '
        'btnDelete
        '
        Me.btnDelete.BackgroundImage = CType(resources.GetObject("btnDelete.BackgroundImage"), System.Drawing.Image)
        Me.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDelete.FlatAppearance.BorderSize = 0
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelete.Location = New System.Drawing.Point(199, 9)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(20, 19)
        Me.btnDelete.TabIndex = 46
        Me.btnDelete.UseVisualStyleBackColor = True
        Me.btnDelete.Visible = False
        '
        'chkmode
        '
        Me.chkmode.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkmode.FlatAppearance.BorderSize = 0
        Me.chkmode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkmode.Location = New System.Drawing.Point(384, 7)
        Me.chkmode.Name = "chkmode"
        Me.chkmode.Size = New System.Drawing.Size(116, 26)
        Me.chkmode.TabIndex = 47
        Me.chkmode.Text = "Transaction Mode"
        Me.chkmode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkmode.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblaccstatus)
        Me.GroupBox1.Controls.Add(Me.discdate)
        Me.GroupBox1.Controls.Add(Me.accSearch)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.billbillingmonth)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.billAccountNo)
        Me.GroupBox1.Controls.Add(Me.billZone)
        Me.GroupBox1.Controls.Add(Me.billName)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.billAddress)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 36)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(497, 107)
        Me.GroupBox1.TabIndex = 37
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Account Details"
        '
        'billAddress
        '
        Me.billAddress.BackColor = System.Drawing.Color.White
        Me.billAddress.Location = New System.Drawing.Point(87, 76)
        Me.billAddress.Name = "billAddress"
        Me.billAddress.ReadOnly = True
        Me.billAddress.Size = New System.Drawing.Size(201, 21)
        Me.billAddress.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(42, 52)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 15)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Name"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(31, 79)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 15)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Address"
        '
        'billName
        '
        Me.billName.BackColor = System.Drawing.Color.White
        Me.billName.Location = New System.Drawing.Point(87, 49)
        Me.billName.Name = "billName"
        Me.billName.ReadOnly = True
        Me.billName.Size = New System.Drawing.Size(201, 21)
        Me.billName.TabIndex = 3
        '
        'billZone
        '
        Me.billZone.BackColor = System.Drawing.Color.White
        Me.billZone.Location = New System.Drawing.Point(375, 22)
        Me.billZone.Name = "billZone"
        Me.billZone.ReadOnly = True
        Me.billZone.Size = New System.Drawing.Size(116, 21)
        Me.billZone.TabIndex = 5
        '
        'billAccountNo
        '
        Me.billAccountNo.BackColor = System.Drawing.Color.White
        Me.billAccountNo.Location = New System.Drawing.Point(87, 22)
        Me.billAccountNo.Name = "billAccountNo"
        Me.billAccountNo.Size = New System.Drawing.Size(96, 21)
        Me.billAccountNo.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(335, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(34, 15)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Zone"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 15)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Account No."
        '
        'billbillingmonth
        '
        Me.billbillingmonth.BackColor = System.Drawing.Color.White
        Me.billbillingmonth.Location = New System.Drawing.Point(375, 49)
        Me.billbillingmonth.Name = "billbillingmonth"
        Me.billbillingmonth.ReadOnly = True
        Me.billbillingmonth.Size = New System.Drawing.Size(116, 21)
        Me.billbillingmonth.TabIndex = 6
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(294, 52)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(77, 15)
        Me.Label11.TabIndex = 40
        Me.Label11.Text = "Billing Month"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(308, 79)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(61, 15)
        Me.Label12.TabIndex = 42
        Me.Label12.Text = "Disc. Date"
        '
        'accSearch
        '
        Me.accSearch.BackgroundImage = CType(resources.GetObject("accSearch.BackgroundImage"), System.Drawing.Image)
        Me.accSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.accSearch.Location = New System.Drawing.Point(189, 23)
        Me.accSearch.Name = "accSearch"
        Me.accSearch.Size = New System.Drawing.Size(24, 20)
        Me.accSearch.TabIndex = 125
        Me.accSearch.TabStop = False
        '
        'discdate
        '
        Me.discdate.BackColor = System.Drawing.Color.White
        Me.discdate.Location = New System.Drawing.Point(375, 76)
        Me.discdate.Name = "discdate"
        Me.discdate.ReadOnly = True
        Me.discdate.Size = New System.Drawing.Size(116, 21)
        Me.discdate.TabIndex = 7
        '
        'lblaccstatus
        '
        Me.lblaccstatus.AutoSize = True
        Me.lblaccstatus.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblaccstatus.Location = New System.Drawing.Point(219, 25)
        Me.lblaccstatus.Name = "lblaccstatus"
        Me.lblaccstatus.Size = New System.Drawing.Size(87, 15)
        Me.lblaccstatus.TabIndex = 42
        Me.lblaccstatus.Text = "Account Status"
        '
        'paymentgroup
        '
        Me.paymentgroup.Controls.Add(Me.autoprint)
        Me.paymentgroup.Controls.Add(Me.reprint)
        Me.paymentgroup.Controls.Add(Me.Label15)
        Me.paymentgroup.Controls.Add(Me.Label14)
        Me.paymentgroup.Controls.Add(Me.overpayment)
        Me.paymentgroup.Controls.Add(Me.amountpaid)
        Me.paymentgroup.Controls.Add(Me.billsave)
        Me.paymentgroup.Controls.Add(Me.billTotalamountdue)
        Me.paymentgroup.Controls.Add(Me.Label9)
        Me.paymentgroup.Location = New System.Drawing.Point(214, 594)
        Me.paymentgroup.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.paymentgroup.Name = "paymentgroup"
        Me.paymentgroup.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.paymentgroup.Size = New System.Drawing.Size(291, 126)
        Me.paymentgroup.TabIndex = 40
        Me.paymentgroup.TabStop = False
        Me.paymentgroup.Text = "Payment"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(38, 17)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(101, 15)
        Me.Label9.TabIndex = 25
        Me.Label9.Text = "Total Amount Due"
        '
        'billTotalamountdue
        '
        Me.billTotalamountdue.BackColor = System.Drawing.Color.White
        Me.billTotalamountdue.Location = New System.Drawing.Point(145, 14)
        Me.billTotalamountdue.Name = "billTotalamountdue"
        Me.billTotalamountdue.ReadOnly = True
        Me.billTotalamountdue.Size = New System.Drawing.Size(135, 21)
        Me.billTotalamountdue.TabIndex = 21
        Me.billTotalamountdue.Text = "0.00"
        Me.billTotalamountdue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'billsave
        '
        Me.billsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.billsave.Location = New System.Drawing.Point(185, 97)
        Me.billsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.billsave.Name = "billsave"
        Me.billsave.Size = New System.Drawing.Size(95, 23)
        Me.billsave.TabIndex = 24
        Me.billsave.Text = "Save"
        Me.billsave.UseVisualStyleBackColor = True
        Me.billsave.Visible = False
        '
        'amountpaid
        '
        Me.amountpaid.BackColor = System.Drawing.Color.White
        Me.amountpaid.Location = New System.Drawing.Point(145, 41)
        Me.amountpaid.Name = "amountpaid"
        Me.amountpaid.Size = New System.Drawing.Size(135, 21)
        Me.amountpaid.TabIndex = 22
        Me.amountpaid.Text = "0.00"
        Me.amountpaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'overpayment
        '
        Me.overpayment.BackColor = System.Drawing.Color.White
        Me.overpayment.Location = New System.Drawing.Point(145, 68)
        Me.overpayment.Name = "overpayment"
        Me.overpayment.ReadOnly = True
        Me.overpayment.Size = New System.Drawing.Size(135, 21)
        Me.overpayment.TabIndex = 23
        Me.overpayment.Text = "0.00"
        Me.overpayment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(64, 46)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(75, 15)
        Me.Label14.TabIndex = 30
        Me.Label14.Text = "Amount Paid"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(57, 75)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(82, 15)
        Me.Label15.TabIndex = 31
        Me.Label15.Text = "Over Payment"
        '
        'reprint
        '
        Me.reprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.reprint.Location = New System.Drawing.Point(91, 97)
        Me.reprint.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.reprint.Name = "reprint"
        Me.reprint.Size = New System.Drawing.Size(88, 23)
        Me.reprint.TabIndex = 32
        Me.reprint.Text = "Reprint"
        Me.reprint.UseVisualStyleBackColor = True
        Me.reprint.Visible = False
        '
        'autoprint
        '
        Me.autoprint.AutoSize = True
        Me.autoprint.ForeColor = System.Drawing.Color.Black
        Me.autoprint.Location = New System.Drawing.Point(6, 99)
        Me.autoprint.Name = "autoprint"
        Me.autoprint.Size = New System.Drawing.Size(78, 20)
        Me.autoprint.TabIndex = 55
        Me.autoprint.Text = "Auto Print"
        Me.autoprint.UseVisualStyleBackColor = True
        '
        'cashcheck
        '
        Me.cashcheck.Controls.Add(Me.rbOnline)
        Me.cashcheck.Controls.Add(Me.rbcheck)
        Me.cashcheck.Controls.Add(Me.rbcash)
        Me.cashcheck.Controls.Add(Me.billcheckdate)
        Me.cashcheck.Controls.Add(Me.Label8)
        Me.cashcheck.Controls.Add(Me.Label6)
        Me.cashcheck.Controls.Add(Me.billcheckno)
        Me.cashcheck.Location = New System.Drawing.Point(8, 594)
        Me.cashcheck.Name = "cashcheck"
        Me.cashcheck.Size = New System.Drawing.Size(200, 126)
        Me.cashcheck.TabIndex = 45
        Me.cashcheck.TabStop = False
        Me.cashcheck.Text = "Mode of Payment"
        '
        'billcheckno
        '
        Me.billcheckno.BackColor = System.Drawing.Color.White
        Me.billcheckno.Enabled = False
        Me.billcheckno.Location = New System.Drawing.Point(67, 64)
        Me.billcheckno.Name = "billcheckno"
        Me.billcheckno.Size = New System.Drawing.Size(117, 21)
        Me.billcheckno.TabIndex = 19
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Enabled = False
        Me.Label6.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(29, 94)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(32, 15)
        Me.Label6.TabIndex = 31
        Me.Label6.Text = "Date"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Enabled = False
        Me.Label8.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(15, 67)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(46, 15)
        Me.Label8.TabIndex = 27
        Me.Label8.Text = "Ref No."
        '
        'billcheckdate
        '
        Me.billcheckdate.BackColor = System.Drawing.Color.White
        Me.billcheckdate.Enabled = False
        Me.billcheckdate.Location = New System.Drawing.Point(67, 91)
        Me.billcheckdate.Name = "billcheckdate"
        Me.billcheckdate.Size = New System.Drawing.Size(117, 21)
        Me.billcheckdate.TabIndex = 20
        '
        'rbcash
        '
        Me.rbcash.AutoSize = True
        Me.rbcash.Location = New System.Drawing.Point(6, 29)
        Me.rbcash.Name = "rbcash"
        Me.rbcash.Size = New System.Drawing.Size(54, 20)
        Me.rbcash.TabIndex = 17
        Me.rbcash.TabStop = True
        Me.rbcash.Text = "Cash"
        Me.rbcash.UseVisualStyleBackColor = True
        '
        'rbcheck
        '
        Me.rbcheck.AutoSize = True
        Me.rbcheck.Location = New System.Drawing.Point(66, 29)
        Me.rbcheck.Name = "rbcheck"
        Me.rbcheck.Size = New System.Drawing.Size(61, 20)
        Me.rbcheck.TabIndex = 18
        Me.rbcheck.TabStop = True
        Me.rbcheck.Text = "Check"
        Me.rbcheck.UseVisualStyleBackColor = True
        '
        'rbOnline
        '
        Me.rbOnline.AutoSize = True
        Me.rbOnline.Location = New System.Drawing.Point(133, 29)
        Me.rbOnline.Name = "rbOnline"
        Me.rbOnline.Size = New System.Drawing.Size(61, 20)
        Me.rbOnline.TabIndex = 32
        Me.rbOnline.TabStop = True
        Me.rbOnline.Text = "Online"
        Me.rbOnline.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.cashcheck)
        Me.Panel1.Controls.Add(Me.paymentgroup)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.chkmode)
        Me.Panel1.Controls.Add(Me.btnDelete)
        Me.Panel1.Controls.Add(Me.dgvotherchages)
        Me.Panel1.Controls.Add(Me.dgvothers)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.lblStatus)
        Me.Panel1.Controls.Add(Me.crno)
        Me.Panel1.Controls.Add(Me.grouparrears)
        Me.Panel1.Controls.Add(Me.GroupBox6)
        Me.Panel1.Location = New System.Drawing.Point(1, 37)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(512, 728)
        Me.Panel1.TabIndex = 38
        '
        'Collection_CR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(195, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(515, 764)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "Collection_CR"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Collection_CR"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.cmsCR.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.grouparrears.ResumeLayout(False)
        CType(Me.dgvothers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvotherchages, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.billarrears, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.billcharges, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.accSearch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.paymentgroup.ResumeLayout(False)
        Me.paymentgroup.PerformLayout()
        Me.cashcheck.ResumeLayout(False)
        Me.cashcheck.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents printcrdocs As Printing.PrintDocument
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents EditToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RefreshToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents cmsCR As ContextMenuStrip
    Friend WithEvents DetailsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FindToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents GroupBox6 As GroupBox
    Public WithEvents billEarlyDisc As TextBox
    Friend WithEvents Label16 As Label
    Public WithEvents billAdjustment As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents currentBilling As CheckBox
    Public WithEvents billPenalty As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label24 As Label
    Public WithEvents CRBillno As TextBox
    Friend WithEvents billcharges As DataGridView
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column7 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents grouparrears As GroupBox
    Public WithEvents billarrears As DataGridView
    Friend WithEvents Column6 As DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewLinkColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents Column8 As DataGridViewTextBoxColumn
    Friend WithEvents Column9 As DataGridViewTextBoxColumn
    Friend WithEvents Column10 As DataGridViewTextBoxColumn
    Friend WithEvents dgvotherchages As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Public WithEvents dgvothers As DataGridView
    Friend WithEvents DataGridViewCheckBoxColumn1 As DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Public WithEvents billadvancepayment As TextBox
    Friend WithEvents Label21 As Label
    Public WithEvents billdiscount As TextBox
    Friend WithEvents Label20 As Label
    Public WithEvents billamountdue As TextBox
    Friend WithEvents Label19 As Label
    Public WithEvents crno As TextBox
    Public WithEvents lblStatus As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents btnDelete As Button
    Public WithEvents chkmode As CheckBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lblaccstatus As Label
    Public WithEvents discdate As TextBox
    Friend WithEvents accSearch As PictureBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Label11 As Label
    Public WithEvents billbillingmonth As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label5 As Label
    Public WithEvents billAccountNo As TextBox
    Public WithEvents billZone As TextBox
    Public WithEvents billName As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Public WithEvents billAddress As TextBox
    Friend WithEvents paymentgroup As GroupBox
    Friend WithEvents autoprint As CheckBox
    Friend WithEvents reprint As Button
    Friend WithEvents Label15 As Label
    Friend WithEvents Label14 As Label
    Public WithEvents overpayment As TextBox
    Public WithEvents amountpaid As TextBox
    Friend WithEvents billsave As Button
    Public WithEvents billTotalamountdue As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents cashcheck As GroupBox
    Friend WithEvents rbOnline As RadioButton
    Friend WithEvents rbcheck As RadioButton
    Friend WithEvents rbcash As RadioButton
    Public WithEvents billcheckdate As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label6 As Label
    Public WithEvents billcheckno As TextBox
    Friend WithEvents Panel1 As Panel
End Class
