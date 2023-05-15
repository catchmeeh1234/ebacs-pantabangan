<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Create_OR
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Create_OR))
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.orno = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.checknoaccount = New System.Windows.Forms.CheckBox()
        Me.accSearch = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.AccountNo = New System.Windows.Forms.TextBox()
        Me.Zone = New System.Windows.Forms.TextBox()
        Me.AccName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.Address = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.lblitems = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.autoprint = New System.Windows.Forms.CheckBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.rbcheck = New System.Windows.Forms.RadioButton()
        Me.checkdate = New System.Windows.Forms.TextBox()
        Me.rbcash = New System.Windows.Forms.RadioButton()
        Me.checkno = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.paymentfor = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbtrans = New System.Windows.Forms.ComboBox()
        Me.reprint = New System.Windows.Forms.Button()
        Me.billsave = New System.Windows.Forms.Button()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.lbladditems = New System.Windows.Forms.Label()
        Me.dgvitems = New System.Windows.Forms.DataGridView()
        Me.lbltotalamountdue = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefereshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FindToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.printOR = New System.Drawing.Printing.PrintDocument()
        Me.cmsCR = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DetailsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.printest = New System.Drawing.Printing.PrintDocument()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1.SuspendLayout()
        CType(Me.accSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        CType(Me.dgvitems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.cmsCR.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Century Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(195, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(8, 20)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(50, 18)
        Me.Label11.TabIndex = 48
        Me.Label11.Text = "Items:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(16, 16)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(69, 15)
        Me.Label10.TabIndex = 42
        Me.Label10.Text = "OR Number"
        '
        'orno
        '
        Me.orno.BackColor = System.Drawing.Color.White
        Me.orno.Location = New System.Drawing.Point(90, 13)
        Me.orno.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.orno.Name = "orno"
        Me.orno.Size = New System.Drawing.Size(74, 21)
        Me.orno.TabIndex = 43
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.checknoaccount)
        Me.GroupBox1.Controls.Add(Me.accSearch)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.AccountNo)
        Me.GroupBox1.Controls.Add(Me.Zone)
        Me.GroupBox1.Controls.Add(Me.AccName)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.lblName)
        Me.GroupBox1.Controls.Add(Me.Address)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 44)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(336, 136)
        Me.GroupBox1.TabIndex = 38
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Account Details"
        '
        'checknoaccount
        '
        Me.checknoaccount.AutoSize = True
        Me.checknoaccount.Location = New System.Drawing.Point(232, 26)
        Me.checknoaccount.Name = "checknoaccount"
        Me.checknoaccount.Size = New System.Drawing.Size(91, 20)
        Me.checknoaccount.TabIndex = 127
        Me.checknoaccount.Text = "No Account"
        Me.checknoaccount.UseVisualStyleBackColor = True
        '
        'accSearch
        '
        Me.accSearch.BackgroundImage = CType(resources.GetObject("accSearch.BackgroundImage"), System.Drawing.Image)
        Me.accSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.accSearch.Location = New System.Drawing.Point(178, 26)
        Me.accSearch.Name = "accSearch"
        Me.accSearch.Size = New System.Drawing.Size(24, 20)
        Me.accSearch.TabIndex = 126
        Me.accSearch.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(7, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 15)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Account No."
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(48, 109)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(34, 15)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Zone"
        '
        'AccountNo
        '
        Me.AccountNo.BackColor = System.Drawing.Color.White
        Me.AccountNo.Location = New System.Drawing.Point(88, 25)
        Me.AccountNo.Name = "AccountNo"
        Me.AccountNo.Size = New System.Drawing.Size(84, 21)
        Me.AccountNo.TabIndex = 7
        '
        'Zone
        '
        Me.Zone.BackColor = System.Drawing.Color.White
        Me.Zone.Location = New System.Drawing.Point(88, 106)
        Me.Zone.Name = "Zone"
        Me.Zone.ReadOnly = True
        Me.Zone.Size = New System.Drawing.Size(146, 21)
        Me.Zone.TabIndex = 10
        '
        'AccName
        '
        Me.AccName.BackColor = System.Drawing.Color.White
        Me.AccName.Location = New System.Drawing.Point(88, 52)
        Me.AccName.Name = "AccName"
        Me.AccName.ReadOnly = True
        Me.AccName.Size = New System.Drawing.Size(235, 21)
        Me.AccName.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(32, 82)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 15)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Address"
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.Location = New System.Drawing.Point(43, 55)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(39, 15)
        Me.lblName.TabIndex = 6
        Me.lblName.Text = "Name"
        '
        'Address
        '
        Me.Address.BackColor = System.Drawing.Color.White
        Me.Address.Location = New System.Drawing.Point(88, 79)
        Me.Address.Name = "Address"
        Me.Address.ReadOnly = True
        Me.Address.Size = New System.Drawing.Size(235, 21)
        Me.Address.TabIndex = 9
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Century Gothic", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(8, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(78, 17)
        Me.Label7.TabIndex = 53
        Me.Label7.Text = "Create OR"
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
        Me.Button1.Location = New System.Drawing.Point(287, 3)
        Me.Button1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(72, 30)
        Me.Button1.TabIndex = 52
        Me.Button1.Text = " Close"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button1.UseVisualStyleBackColor = True
        '
        'lblitems
        '
        Me.lblitems.AutoSize = True
        Me.lblitems.Font = New System.Drawing.Font("Century Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblitems.ForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(195, Byte), Integer))
        Me.lblitems.Location = New System.Drawing.Point(64, 20)
        Me.lblitems.Name = "lblitems"
        Me.lblitems.Size = New System.Drawing.Size(16, 18)
        Me.lblitems.TabIndex = 49
        Me.lblitems.Text = "0"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.autoprint)
        Me.Panel1.Controls.Add(Me.btnCancel)
        Me.Panel1.Controls.Add(Me.lblStatus)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.reprint)
        Me.Panel1.Controls.Add(Me.orno)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.billsave)
        Me.Panel1.Controls.Add(Me.GroupBox7)
        Me.Panel1.Controls.Add(Me.lbltotalamountdue)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.MenuStrip1)
        Me.Panel1.Location = New System.Drawing.Point(2, 33)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(357, 629)
        Me.Panel1.TabIndex = 51
        '
        'autoprint
        '
        Me.autoprint.AutoSize = True
        Me.autoprint.ForeColor = System.Drawing.Color.Black
        Me.autoprint.Location = New System.Drawing.Point(101, 604)
        Me.autoprint.Name = "autoprint"
        Me.autoprint.Size = New System.Drawing.Size(78, 20)
        Me.autoprint.TabIndex = 54
        Me.autoprint.Text = "Auto Print"
        Me.autoprint.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.BackgroundImage = CType(resources.GetObject("btnCancel.BackgroundImage"), System.Drawing.Image)
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Location = New System.Drawing.Point(170, 15)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(20, 19)
        Me.btnCancel.TabIndex = 53
        Me.btnCancel.UseVisualStyleBackColor = True
        Me.btnCancel.Visible = False
        '
        'lblStatus
        '
        Me.lblStatus.Font = New System.Drawing.Font("Century Gothic", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.Green
        Me.lblStatus.Location = New System.Drawing.Point(250, 8)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(94, 31)
        Me.lblStatus.TabIndex = 51
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblStatus.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.rbcheck)
        Me.GroupBox3.Controls.Add(Me.checkdate)
        Me.GroupBox3.Controls.Add(Me.rbcash)
        Me.GroupBox3.Controls.Add(Me.checkno)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Location = New System.Drawing.Point(8, 491)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(174, 110)
        Me.GroupBox3.TabIndex = 50
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Mode of Payment"
        '
        'rbcheck
        '
        Me.rbcheck.AutoSize = True
        Me.rbcheck.Location = New System.Drawing.Point(99, 25)
        Me.rbcheck.Name = "rbcheck"
        Me.rbcheck.Size = New System.Drawing.Size(61, 20)
        Me.rbcheck.TabIndex = 33
        Me.rbcheck.Text = "Check"
        Me.rbcheck.UseVisualStyleBackColor = True
        '
        'checkdate
        '
        Me.checkdate.BackColor = System.Drawing.Color.White
        Me.checkdate.Enabled = False
        Me.checkdate.Location = New System.Drawing.Point(82, 80)
        Me.checkdate.Name = "checkdate"
        Me.checkdate.Size = New System.Drawing.Size(84, 21)
        Me.checkdate.TabIndex = 43
        '
        'rbcash
        '
        Me.rbcash.AutoSize = True
        Me.rbcash.Checked = True
        Me.rbcash.Location = New System.Drawing.Point(39, 25)
        Me.rbcash.Name = "rbcash"
        Me.rbcash.Size = New System.Drawing.Size(54, 20)
        Me.rbcash.TabIndex = 32
        Me.rbcash.TabStop = True
        Me.rbcash.Text = "Cash"
        Me.rbcash.UseVisualStyleBackColor = True
        '
        'checkno
        '
        Me.checkno.BackColor = System.Drawing.Color.White
        Me.checkno.Enabled = False
        Me.checkno.Location = New System.Drawing.Point(82, 51)
        Me.checkno.Name = "checkno"
        Me.checkno.Size = New System.Drawing.Size(84, 21)
        Me.checkno.TabIndex = 42
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(6, 80)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(70, 15)
        Me.Label8.TabIndex = 45
        Me.Label8.Text = "Check Date"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(13, 54)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(64, 15)
        Me.Label9.TabIndex = 44
        Me.Label9.Text = "Check No."
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.paymentfor)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.cbtrans)
        Me.GroupBox2.Location = New System.Drawing.Point(10, 187)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(336, 83)
        Me.GroupBox2.TabIndex = 40
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Transaction Type"
        '
        'paymentfor
        '
        Me.paymentfor.BackColor = System.Drawing.Color.White
        Me.paymentfor.Location = New System.Drawing.Point(120, 50)
        Me.paymentfor.Name = "paymentfor"
        Me.paymentfor.ReadOnly = True
        Me.paymentfor.Size = New System.Drawing.Size(203, 21)
        Me.paymentfor.TabIndex = 40
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(22, 53)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(92, 15)
        Me.Label6.TabIndex = 41
        Me.Label6.Text = "As Payment For:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(108, 15)
        Me.Label1.TabIndex = 40
        Me.Label1.Text = "Select Transaction:"
        '
        'cbtrans
        '
        Me.cbtrans.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbtrans.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbtrans.FormattingEnabled = True
        Me.cbtrans.Items.AddRange(New Object() {"Others"})
        Me.cbtrans.Location = New System.Drawing.Point(120, 20)
        Me.cbtrans.Name = "cbtrans"
        Me.cbtrans.Size = New System.Drawing.Size(203, 24)
        Me.cbtrans.TabIndex = 40
        '
        'reprint
        '
        Me.reprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.reprint.Location = New System.Drawing.Point(247, 551)
        Me.reprint.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.reprint.Name = "reprint"
        Me.reprint.Size = New System.Drawing.Size(101, 29)
        Me.reprint.TabIndex = 49
        Me.reprint.Text = "Reprint"
        Me.reprint.UseVisualStyleBackColor = True
        Me.reprint.Visible = False
        '
        'billsave
        '
        Me.billsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.billsave.Location = New System.Drawing.Point(247, 588)
        Me.billsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.billsave.Name = "billsave"
        Me.billsave.Size = New System.Drawing.Size(101, 29)
        Me.billsave.TabIndex = 48
        Me.billsave.Text = "Save"
        Me.billsave.UseVisualStyleBackColor = True
        Me.billsave.Visible = False
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.lblitems)
        Me.GroupBox7.Controls.Add(Me.Label11)
        Me.GroupBox7.Controls.Add(Me.lbladditems)
        Me.GroupBox7.Controls.Add(Me.dgvitems)
        Me.GroupBox7.Location = New System.Drawing.Point(8, 277)
        Me.GroupBox7.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox7.Size = New System.Drawing.Size(336, 207)
        Me.GroupBox7.TabIndex = 44
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Items"
        '
        'lbladditems
        '
        Me.lbladditems.Font = New System.Drawing.Font("Century Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbladditems.ForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(195, Byte), Integer))
        Me.lbladditems.Location = New System.Drawing.Point(220, 18)
        Me.lbladditems.Name = "lbladditems"
        Me.lbladditems.Size = New System.Drawing.Size(105, 24)
        Me.lbladditems.TabIndex = 46
        Me.lbladditems.Text = "Add Items"
        Me.lbladditems.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dgvitems
        '
        Me.dgvitems.AllowUserToAddRows = False
        Me.dgvitems.AllowUserToDeleteRows = False
        Me.dgvitems.AllowUserToResizeColumns = False
        Me.dgvitems.AllowUserToResizeRows = False
        Me.dgvitems.BackgroundColor = System.Drawing.Color.White
        Me.dgvitems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvitems.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6, Me.Column7, Me.Column8})
        Me.dgvitems.Location = New System.Drawing.Point(9, 46)
        Me.dgvitems.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgvitems.Name = "dgvitems"
        Me.dgvitems.RowHeadersVisible = False
        Me.dgvitems.Size = New System.Drawing.Size(316, 153)
        Me.dgvitems.TabIndex = 37
        '
        'lbltotalamountdue
        '
        Me.lbltotalamountdue.AutoSize = True
        Me.lbltotalamountdue.Font = New System.Drawing.Font("Century Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltotalamountdue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(195, Byte), Integer))
        Me.lbltotalamountdue.Location = New System.Drawing.Point(240, 488)
        Me.lbltotalamountdue.Name = "lbltotalamountdue"
        Me.lbltotalamountdue.Size = New System.Drawing.Size(36, 18)
        Me.lbltotalamountdue.TabIndex = 47
        Me.lbltotalamountdue.Text = "0.00"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Century Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(195, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(188, 488)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(46, 18)
        Me.Label12.TabIndex = 46
        Me.Label12.Text = "Total:"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(357, 24)
        Me.MenuStrip1.TabIndex = 52
        Me.MenuStrip1.Text = "MenuStrip1"
        Me.MenuStrip1.Visible = False
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefereshToolStripMenuItem, Me.SaveToolStripMenuItem, Me.FindToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.EditToolStripMenuItem.Text = "edit"
        '
        'RefereshToolStripMenuItem
        '
        Me.RefereshToolStripMenuItem.Name = "RefereshToolStripMenuItem"
        Me.RefereshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.RefereshToolStripMenuItem.Size = New System.Drawing.Size(138, 22)
        Me.RefereshToolStripMenuItem.Text = "Referesh"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(138, 22)
        Me.SaveToolStripMenuItem.Text = "save"
        '
        'FindToolStripMenuItem
        '
        Me.FindToolStripMenuItem.Name = "FindToolStripMenuItem"
        Me.FindToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.FindToolStripMenuItem.Size = New System.Drawing.Size(138, 22)
        Me.FindToolStripMenuItem.Text = "find"
        '
        'printOR
        '
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
        'printest
        '
        '
        'Column1
        '
        Me.Column1.HeaderText = "QTY"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 45
        '
        'Column2
        '
        Me.Column2.HeaderText = "Particular"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 110
        '
        'Column3
        '
        Me.Column3.HeaderText = "Cost"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 70
        '
        'Column4
        '
        Me.Column4.HeaderText = "Amount"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 70
        '
        'Column5
        '
        Me.Column5.HeaderText = "ID"
        Me.Column5.Name = "Column5"
        '
        'Column6
        '
        Me.Column6.HeaderText = "Category"
        Me.Column6.Name = "Column6"
        Me.Column6.Visible = False
        '
        'Column7
        '
        Me.Column7.HeaderText = "Entry"
        Me.Column7.Name = "Column7"
        Me.Column7.Visible = False
        '
        'Column8
        '
        Me.Column8.HeaderText = "charge"
        Me.Column8.Name = "Column8"
        Me.Column8.Visible = False
        '
        'Create_OR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(195, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(362, 665)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Button1)
        Me.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "Create_OR"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Create_OR"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.accSearch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        CType(Me.dgvitems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.cmsCR.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Public WithEvents orno As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label5 As Label
    Public WithEvents AccountNo As TextBox
    Public WithEvents Zone As TextBox
    Public WithEvents AccName As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents lblName As Label
    Public WithEvents Address As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents lblitems As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lbltotalamountdue As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Public WithEvents checkdate As TextBox
    Friend WithEvents Label8 As Label
    Public WithEvents checkno As TextBox
    Friend WithEvents Label9 As Label
    Public WithEvents paymentfor As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents cbtrans As ComboBox
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents lbladditems As Label
    Public WithEvents dgvitems As DataGridView
    Friend WithEvents billsave As Button
    Friend WithEvents reprint As Button
    Friend WithEvents printOR As Printing.PrintDocument
    Friend WithEvents accSearch As PictureBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents rbcheck As RadioButton
    Friend WithEvents rbcash As RadioButton
    Public WithEvents lblStatus As Label
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents EditToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RefereshToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents checknoaccount As CheckBox
    Friend WithEvents btnCancel As Button
    Friend WithEvents autoprint As CheckBox
    Friend WithEvents SaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FindToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents cmsCR As ContextMenuStrip
    Friend WithEvents DetailsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents printest As Printing.PrintDocument
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents Column7 As DataGridViewTextBoxColumn
    Friend WithEvents Column8 As DataGridViewTextBoxColumn
End Class
