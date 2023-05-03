<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class billingAdjustmentBill
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(billingAdjustmentBill))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.adjustCategory = New System.Windows.Forms.ComboBox()
        Me.Posting = New System.Windows.Forms.Button()
        Me.adjustApproved = New System.Windows.Forms.Label()
        Me.adjustStatus = New System.Windows.Forms.Label()
        Me.saving = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.AdjustRemarks = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.adjustRefNo = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.adjustSenior = New System.Windows.Forms.CheckBox()
        Me.lblnewtotal = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.adjustDiscount = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.adjustPenalty = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.adjustAmountdue = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.adjustAdvance = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.adjustCovered = New System.Windows.Forms.TextBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.billdiscount = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.billPenalty = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.billamountdue = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.billadvancepayment = New System.Windows.Forms.TextBox()
        Me.billTotal = New System.Windows.Forms.Label()
        Me.billcharges = New System.Windows.Forms.DataGridView()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.adjustBillnos = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.AdjustDate = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.AdjustRecords = New System.Windows.Forms.DataGridView()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblMode = New System.Windows.Forms.Label()
        Me.AdjustName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.adjustAccountNo = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.accSearch = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UpdateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CancelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        CType(Me.billcharges, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        CType(Me.AdjustRecords, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.accSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Controls.Add(Me.GroupBox4)
        Me.Panel1.Controls.Add(Me.lblMode)
        Me.Panel1.Controls.Add(Me.AdjustName)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.adjustAccountNo)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.accSearch)
        Me.Panel1.Location = New System.Drawing.Point(1, 39)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1070, 473)
        Me.Panel1.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.adjustCategory)
        Me.GroupBox3.Controls.Add(Me.Posting)
        Me.GroupBox3.Controls.Add(Me.adjustApproved)
        Me.GroupBox3.Controls.Add(Me.adjustStatus)
        Me.GroupBox3.Controls.Add(Me.saving)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Controls.Add(Me.Label15)
        Me.GroupBox3.Controls.Add(Me.AdjustRemarks)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.adjustRefNo)
        Me.GroupBox3.Controls.Add(Me.GroupBox1)
        Me.GroupBox3.Controls.Add(Me.AdjustDate)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Location = New System.Drawing.Point(437, 45)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(625, 419)
        Me.GroupBox3.TabIndex = 152
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Bill Adjustment"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(41, 66)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(56, 15)
        Me.Label13.TabIndex = 161
        Me.Label13.Text = "Category"
        '
        'adjustCategory
        '
        Me.adjustCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.adjustCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.adjustCategory.FormattingEnabled = True
        Me.adjustCategory.Items.AddRange(New Object() {"Due to Leak", "Billing"})
        Me.adjustCategory.Location = New System.Drawing.Point(103, 63)
        Me.adjustCategory.Name = "adjustCategory"
        Me.adjustCategory.Size = New System.Drawing.Size(244, 24)
        Me.adjustCategory.TabIndex = 160
        '
        'Posting
        '
        Me.Posting.Location = New System.Drawing.Point(532, 386)
        Me.Posting.Name = "Posting"
        Me.Posting.Size = New System.Drawing.Size(75, 23)
        Me.Posting.TabIndex = 157
        Me.Posting.Text = "Post"
        Me.Posting.UseVisualStyleBackColor = True
        Me.Posting.Visible = False
        '
        'adjustApproved
        '
        Me.adjustApproved.AutoSize = True
        Me.adjustApproved.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.adjustApproved.Location = New System.Drawing.Point(464, 66)
        Me.adjustApproved.Name = "adjustApproved"
        Me.adjustApproved.Size = New System.Drawing.Size(0, 15)
        Me.adjustApproved.TabIndex = 156
        '
        'adjustStatus
        '
        Me.adjustStatus.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.adjustStatus.Location = New System.Drawing.Point(463, 39)
        Me.adjustStatus.Name = "adjustStatus"
        Me.adjustStatus.Size = New System.Drawing.Size(104, 15)
        Me.adjustStatus.TabIndex = 155
        Me.adjustStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'saving
        '
        Me.saving.Location = New System.Drawing.Point(451, 386)
        Me.saving.Name = "saving"
        Me.saving.Size = New System.Drawing.Size(75, 23)
        Me.saving.TabIndex = 154
        Me.saving.Text = "Save"
        Me.saving.UseVisualStyleBackColor = True
        Me.saving.Visible = False
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(370, 66)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(76, 15)
        Me.Label16.TabIndex = 152
        Me.Label16.Text = "Approved By"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(370, 39)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(38, 15)
        Me.Label15.TabIndex = 150
        Me.Label15.Text = "Status"
        '
        'AdjustRemarks
        '
        Me.AdjustRemarks.BackColor = System.Drawing.Color.White
        Me.AdjustRemarks.Location = New System.Drawing.Point(77, 388)
        Me.AdjustRemarks.Name = "AdjustRemarks"
        Me.AdjustRemarks.ReadOnly = True
        Me.AdjustRemarks.Size = New System.Drawing.Size(368, 21)
        Me.AdjustRemarks.TabIndex = 149
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(19, 391)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(52, 15)
        Me.Label14.TabIndex = 148
        Me.Label14.Text = "Remarks"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Reference No."
        '
        'adjustRefNo
        '
        Me.adjustRefNo.BackColor = System.Drawing.Color.White
        Me.adjustRefNo.Location = New System.Drawing.Point(103, 36)
        Me.adjustRefNo.Name = "adjustRefNo"
        Me.adjustRefNo.Size = New System.Drawing.Size(100, 21)
        Me.adjustRefNo.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.adjustSenior)
        Me.GroupBox1.Controls.Add(Me.lblnewtotal)
        Me.GroupBox1.Controls.Add(Me.Label26)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.adjustCovered)
        Me.GroupBox1.Controls.Add(Me.GroupBox6)
        Me.GroupBox1.Controls.Add(Me.billTotal)
        Me.GroupBox1.Controls.Add(Me.billcharges)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.adjustBillnos)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 90)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(613, 290)
        Me.GroupBox1.TabIndex = 140
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Billing Details"
        '
        'adjustSenior
        '
        Me.adjustSenior.AutoSize = True
        Me.adjustSenior.Enabled = False
        Me.adjustSenior.Location = New System.Drawing.Point(368, 31)
        Me.adjustSenior.Name = "adjustSenior"
        Me.adjustSenior.Size = New System.Drawing.Size(98, 20)
        Me.adjustSenior.TabIndex = 160
        Me.adjustSenior.Text = "Senior Citizen"
        Me.adjustSenior.UseVisualStyleBackColor = True
        '
        'lblnewtotal
        '
        Me.lblnewtotal.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblnewtotal.Location = New System.Drawing.Point(474, 248)
        Me.lblnewtotal.Name = "lblnewtotal"
        Me.lblnewtotal.Size = New System.Drawing.Size(85, 15)
        Me.lblnewtotal.TabIndex = 159
        Me.lblnewtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(336, 248)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(132, 16)
        Me.Label26.TabIndex = 157
        Me.Label26.Text = "New Total Amount Due"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.adjustDiscount)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.adjustPenalty)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.adjustAmountdue)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.adjustAdvance)
        Me.GroupBox2.Location = New System.Drawing.Point(318, 60)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(241, 138)
        Me.GroupBox2.TabIndex = 156
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "New Amount"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(50, 23)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(73, 15)
        Me.Label8.TabIndex = 134
        Me.Label8.Text = "Amount Due"
        '
        'adjustDiscount
        '
        Me.adjustDiscount.BackColor = System.Drawing.Color.White
        Me.adjustDiscount.Location = New System.Drawing.Point(129, 47)
        Me.adjustDiscount.Name = "adjustDiscount"
        Me.adjustDiscount.ReadOnly = True
        Me.adjustDiscount.Size = New System.Drawing.Size(100, 21)
        Me.adjustDiscount.TabIndex = 133
        Me.adjustDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(69, 50)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(54, 15)
        Me.Label9.TabIndex = 137
        Me.Label9.Text = "Discount"
        '
        'adjustPenalty
        '
        Me.adjustPenalty.BackColor = System.Drawing.Color.White
        Me.adjustPenalty.Location = New System.Drawing.Point(129, 74)
        Me.adjustPenalty.Name = "adjustPenalty"
        Me.adjustPenalty.ReadOnly = True
        Me.adjustPenalty.Size = New System.Drawing.Size(100, 21)
        Me.adjustPenalty.TabIndex = 135
        Me.adjustPenalty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(18, 104)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(105, 15)
        Me.Label10.TabIndex = 138
        Me.Label10.Text = "Advance payment"
        '
        'adjustAmountdue
        '
        Me.adjustAmountdue.BackColor = System.Drawing.Color.White
        Me.adjustAmountdue.Location = New System.Drawing.Point(129, 20)
        Me.adjustAmountdue.Name = "adjustAmountdue"
        Me.adjustAmountdue.ReadOnly = True
        Me.adjustAmountdue.Size = New System.Drawing.Size(100, 21)
        Me.adjustAmountdue.TabIndex = 132
        Me.adjustAmountdue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(76, 77)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(47, 15)
        Me.Label12.TabIndex = 139
        Me.Label12.Text = "Penalty"
        '
        'adjustAdvance
        '
        Me.adjustAdvance.BackColor = System.Drawing.Color.White
        Me.adjustAdvance.Location = New System.Drawing.Point(129, 101)
        Me.adjustAdvance.Name = "adjustAdvance"
        Me.adjustAdvance.ReadOnly = True
        Me.adjustAdvance.Size = New System.Drawing.Size(100, 21)
        Me.adjustAdvance.TabIndex = 136
        Me.adjustAdvance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(215, 33)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(40, 15)
        Me.Label7.TabIndex = 155
        Me.Label7.Text = "Bill for"
        '
        'adjustCovered
        '
        Me.adjustCovered.BackColor = System.Drawing.Color.White
        Me.adjustCovered.Location = New System.Drawing.Point(261, 30)
        Me.adjustCovered.Name = "adjustCovered"
        Me.adjustCovered.ReadOnly = True
        Me.adjustCovered.Size = New System.Drawing.Size(94, 21)
        Me.adjustCovered.TabIndex = 156
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.Label19)
        Me.GroupBox6.Controls.Add(Me.billdiscount)
        Me.GroupBox6.Controls.Add(Me.Label20)
        Me.GroupBox6.Controls.Add(Me.billPenalty)
        Me.GroupBox6.Controls.Add(Me.Label21)
        Me.GroupBox6.Controls.Add(Me.billamountdue)
        Me.GroupBox6.Controls.Add(Me.Label24)
        Me.GroupBox6.Controls.Add(Me.billadvancepayment)
        Me.GroupBox6.Location = New System.Drawing.Point(71, 60)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(241, 138)
        Me.GroupBox6.TabIndex = 154
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Amount"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(50, 23)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(73, 15)
        Me.Label19.TabIndex = 134
        Me.Label19.Text = "Amount Due"
        '
        'billdiscount
        '
        Me.billdiscount.BackColor = System.Drawing.Color.White
        Me.billdiscount.Location = New System.Drawing.Point(129, 47)
        Me.billdiscount.Name = "billdiscount"
        Me.billdiscount.ReadOnly = True
        Me.billdiscount.Size = New System.Drawing.Size(100, 21)
        Me.billdiscount.TabIndex = 133
        Me.billdiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(69, 50)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(54, 15)
        Me.Label20.TabIndex = 137
        Me.Label20.Text = "Discount"
        '
        'billPenalty
        '
        Me.billPenalty.BackColor = System.Drawing.Color.White
        Me.billPenalty.Location = New System.Drawing.Point(129, 74)
        Me.billPenalty.Name = "billPenalty"
        Me.billPenalty.ReadOnly = True
        Me.billPenalty.Size = New System.Drawing.Size(100, 21)
        Me.billPenalty.TabIndex = 135
        Me.billPenalty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(18, 104)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(105, 15)
        Me.Label21.TabIndex = 138
        Me.Label21.Text = "Advance payment"
        '
        'billamountdue
        '
        Me.billamountdue.BackColor = System.Drawing.Color.White
        Me.billamountdue.Location = New System.Drawing.Point(129, 20)
        Me.billamountdue.Name = "billamountdue"
        Me.billamountdue.ReadOnly = True
        Me.billamountdue.Size = New System.Drawing.Size(100, 21)
        Me.billamountdue.TabIndex = 132
        Me.billamountdue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(76, 77)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(47, 15)
        Me.Label24.TabIndex = 139
        Me.Label24.Text = "Penalty"
        '
        'billadvancepayment
        '
        Me.billadvancepayment.BackColor = System.Drawing.Color.White
        Me.billadvancepayment.Location = New System.Drawing.Point(129, 101)
        Me.billadvancepayment.Name = "billadvancepayment"
        Me.billadvancepayment.ReadOnly = True
        Me.billadvancepayment.Size = New System.Drawing.Size(100, 21)
        Me.billadvancepayment.TabIndex = 136
        Me.billadvancepayment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'billTotal
        '
        Me.billTotal.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.billTotal.Location = New System.Drawing.Point(474, 223)
        Me.billTotal.Name = "billTotal"
        Me.billTotal.Size = New System.Drawing.Size(85, 15)
        Me.billTotal.TabIndex = 145
        Me.billTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'billcharges
        '
        Me.billcharges.AllowUserToAddRows = False
        Me.billcharges.AllowUserToDeleteRows = False
        Me.billcharges.BackgroundColor = System.Drawing.Color.White
        Me.billcharges.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.billcharges.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column4, Me.Column7})
        Me.billcharges.Location = New System.Drawing.Point(71, 204)
        Me.billcharges.Name = "billcharges"
        Me.billcharges.ReadOnly = True
        Me.billcharges.RowHeadersVisible = False
        Me.billcharges.Size = New System.Drawing.Size(241, 76)
        Me.billcharges.TabIndex = 142
        '
        'Column4
        '
        Me.Column4.HeaderText = "Charges"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 140
        '
        'Column7
        '
        Me.Column7.HeaderText = "Amount"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        Me.Column7.Width = 80
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(364, 223)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(104, 16)
        Me.Label6.TabIndex = 144
        Me.Label6.Text = "Total Amount Due"
        '
        'adjustBillnos
        '
        Me.adjustBillnos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.adjustBillnos.Enabled = False
        Me.adjustBillnos.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.adjustBillnos.FormattingEnabled = True
        Me.adjustBillnos.Location = New System.Drawing.Point(119, 30)
        Me.adjustBillnos.Name = "adjustBillnos"
        Me.adjustBillnos.Size = New System.Drawing.Size(90, 24)
        Me.adjustBillnos.TabIndex = 130
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(68, 33)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(45, 15)
        Me.Label5.TabIndex = 131
        Me.Label5.Text = "Bill No."
        '
        'AdjustDate
        '
        Me.AdjustDate.Enabled = False
        Me.AdjustDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.AdjustDate.Location = New System.Drawing.Point(247, 35)
        Me.AdjustDate.Name = "AdjustDate"
        Me.AdjustDate.Size = New System.Drawing.Size(100, 21)
        Me.AdjustDate.TabIndex = 129
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(209, 39)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(32, 15)
        Me.Label4.TabIndex = 128
        Me.Label4.Text = "Date"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.AdjustRecords)
        Me.GroupBox4.Location = New System.Drawing.Point(11, 83)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(420, 381)
        Me.GroupBox4.TabIndex = 153
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Bill Adjustment Record"
        '
        'AdjustRecords
        '
        Me.AdjustRecords.AllowUserToAddRows = False
        Me.AdjustRecords.AllowUserToDeleteRows = False
        Me.AdjustRecords.BackgroundColor = System.Drawing.Color.White
        Me.AdjustRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.AdjustRecords.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column3, Me.Column5, Me.Column6, Me.Column8, Me.Column9})
        Me.AdjustRecords.Location = New System.Drawing.Point(6, 26)
        Me.AdjustRecords.Name = "AdjustRecords"
        Me.AdjustRecords.ReadOnly = True
        Me.AdjustRecords.RowHeadersVisible = False
        Me.AdjustRecords.Size = New System.Drawing.Size(408, 347)
        Me.AdjustRecords.TabIndex = 0
        '
        'Column3
        '
        Me.Column3.HeaderText = "Ref No."
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 60
        '
        'Column5
        '
        Me.Column5.HeaderText = "Bill No."
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Width = 70
        '
        'Column6
        '
        Me.Column6.HeaderText = "Remarks"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        Me.Column6.Width = 120
        '
        'Column8
        '
        Me.Column8.HeaderText = "Adjustment"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        Me.Column8.Width = 80
        '
        'Column9
        '
        Me.Column9.HeaderText = "Status"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        Me.Column9.Width = 60
        '
        'lblMode
        '
        Me.lblMode.Font = New System.Drawing.Font("Century Gothic", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMode.Location = New System.Drawing.Point(681, 13)
        Me.lblMode.Name = "lblMode"
        Me.lblMode.Size = New System.Drawing.Size(130, 25)
        Me.lblMode.TabIndex = 154
        Me.lblMode.Text = "Mode"
        Me.lblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblMode.Visible = False
        '
        'AdjustName
        '
        Me.AdjustName.BackColor = System.Drawing.Color.White
        Me.AdjustName.Location = New System.Drawing.Point(119, 42)
        Me.AdjustName.Name = "AdjustName"
        Me.AdjustName.ReadOnly = True
        Me.AdjustName.Size = New System.Drawing.Size(181, 21)
        Me.AdjustName.TabIndex = 151
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(25, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 15)
        Me.Label3.TabIndex = 150
        Me.Label3.Text = "Account Name"
        '
        'adjustAccountNo
        '
        Me.adjustAccountNo.Location = New System.Drawing.Point(119, 15)
        Me.adjustAccountNo.Name = "adjustAccountNo"
        Me.adjustAccountNo.Size = New System.Drawing.Size(100, 21)
        Me.adjustAccountNo.TabIndex = 148
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(14, 18)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(99, 15)
        Me.Label11.TabIndex = 147
        Me.Label11.Text = "Account Number"
        '
        'accSearch
        '
        Me.accSearch.BackgroundImage = CType(resources.GetObject("accSearch.BackgroundImage"), System.Drawing.Image)
        Me.accSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.accSearch.Location = New System.Drawing.Point(306, 42)
        Me.accSearch.Name = "accSearch"
        Me.accSearch.Size = New System.Drawing.Size(24, 20)
        Me.accSearch.TabIndex = 149
        Me.accSearch.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Century Gothic", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(12, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(108, 17)
        Me.Label2.TabIndex = 158
        Me.Label2.Text = "Bill Adjustment"
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
        Me.Button2.Location = New System.Drawing.Point(999, 3)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(72, 30)
        Me.Button2.TabIndex = 157
        Me.Button2.Text = " Close"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button2.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1072, 24)
        Me.MenuStrip1.TabIndex = 159
        Me.MenuStrip1.Text = "MenuStrip1"
        Me.MenuStrip1.Visible = False
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveToolStripMenuItem, Me.NewToolStripMenuItem, Me.UpdateToolStripMenuItem, Me.RefreshToolStripMenuItem, Me.CancelToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.SaveToolStripMenuItem.Text = "Save"
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.NewToolStripMenuItem.Text = "New"
        '
        'UpdateToolStripMenuItem
        '
        Me.UpdateToolStripMenuItem.Name = "UpdateToolStripMenuItem"
        Me.UpdateToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.UpdateToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.UpdateToolStripMenuItem.Text = "Update"
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.RefreshToolStripMenuItem.Text = "Refresh"
        '
        'CancelToolStripMenuItem
        '
        Me.CancelToolStripMenuItem.Name = "CancelToolStripMenuItem"
        Me.CancelToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.CancelToolStripMenuItem.Text = "Cancel"
        '
        'billingAdjustmentBill
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(195, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1072, 513)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "billingAdjustmentBill"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "billingAdjustmentBill"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        CType(Me.billcharges, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.AdjustRecords, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.accSearch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Posting As Button
    Friend WithEvents adjustApproved As Label
    Friend WithEvents adjustStatus As Label
    Friend WithEvents saving As Button
    Friend WithEvents Label16 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents AdjustRemarks As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents adjustRefNo As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents adjustSenior As CheckBox
    Friend WithEvents lblnewtotal As Label
    Friend WithEvents Label26 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label8 As Label
    Public WithEvents adjustDiscount As TextBox
    Friend WithEvents Label9 As Label
    Public WithEvents adjustPenalty As TextBox
    Friend WithEvents Label10 As Label
    Public WithEvents adjustAmountdue As TextBox
    Friend WithEvents Label12 As Label
    Public WithEvents adjustAdvance As TextBox
    Friend WithEvents Label7 As Label
    Public WithEvents adjustCovered As TextBox
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents Label19 As Label
    Public WithEvents billdiscount As TextBox
    Friend WithEvents Label20 As Label
    Public WithEvents billPenalty As TextBox
    Friend WithEvents Label21 As Label
    Public WithEvents billamountdue As TextBox
    Friend WithEvents Label24 As Label
    Public WithEvents billadvancepayment As TextBox
    Friend WithEvents billTotal As Label
    Friend WithEvents billcharges As DataGridView
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column7 As DataGridViewTextBoxColumn
    Friend WithEvents Label6 As Label
    Friend WithEvents adjustBillnos As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents AdjustDate As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents AdjustRecords As DataGridView
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents Column8 As DataGridViewTextBoxColumn
    Friend WithEvents Column9 As DataGridViewTextBoxColumn
    Public WithEvents lblMode As Label
    Public WithEvents AdjustName As TextBox
    Friend WithEvents Label3 As Label
    Public WithEvents adjustAccountNo As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents accSearch As PictureBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents EditToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RefreshToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CancelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UpdateToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label13 As Label
    Friend WithEvents adjustCategory As ComboBox
End Class
