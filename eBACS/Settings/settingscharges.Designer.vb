<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class settingscharges
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(settingscharges))
        Me.lblmode = New System.Windows.Forms.Label()
        Me.EditToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chargesAmount = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chargesName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chargesID = New System.Windows.Forms.TextBox()
        Me.listZone = New System.Windows.Forms.ListView()
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbByCons = New System.Windows.Forms.TextBox()
        Me.tbByAmount = New System.Windows.Forms.TextBox()
        Me.rbByConsumption = New System.Windows.Forms.RadioButton()
        Me.rbByAmount = New System.Windows.Forms.RadioButton()
        Me.rbFixed = New System.Windows.Forms.RadioButton()
        Me.chargesEntry = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.chargesCat = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblmode
        '
        Me.lblmode.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmode.ForeColor = System.Drawing.Color.Black
        Me.lblmode.Location = New System.Drawing.Point(138, 18)
        Me.lblmode.Name = "lblmode"
        Me.lblmode.Size = New System.Drawing.Size(85, 25)
        Me.lblmode.TabIndex = 27
        Me.lblmode.Text = "Update Mode"
        Me.lblmode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblmode.Visible = False
        '
        'EditToolStripMenuItem1
        '
        Me.EditToolStripMenuItem1.Name = "EditToolStripMenuItem1"
        Me.EditToolStripMenuItem1.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.EditToolStripMenuItem1.Size = New System.Drawing.Size(189, 22)
        Me.EditToolStripMenuItem1.Text = "edit"
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.NewToolStripMenuItem.Text = "new"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.SaveToolStripMenuItem.Text = "save"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveToolStripMenuItem, Me.NewToolStripMenuItem, Me.EditToolStripMenuItem1, Me.DeleteToolStripMenuItem, Me.RefreshToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.EditToolStripMenuItem.Text = "edit"
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
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(760, 24)
        Me.MenuStrip1.TabIndex = 26
        Me.MenuStrip1.Text = "MenuStrip1"
        Me.MenuStrip1.Visible = False
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Entry"
        Me.ColumnHeader3.Width = 80
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Category"
        Me.ColumnHeader2.Width = 80
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "ID"
        Me.ColumnHeader1.Width = 50
        '
        'chargesAmount
        '
        Me.chargesAmount.BackColor = System.Drawing.Color.White
        Me.chargesAmount.Location = New System.Drawing.Point(370, 49)
        Me.chargesAmount.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.chargesAmount.Name = "chargesAmount"
        Me.chargesAmount.Size = New System.Drawing.Size(140, 21)
        Me.chargesAmount.TabIndex = 23
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(31, 114)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 15)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "Name"
        '
        'chargesName
        '
        Me.chargesName.BackColor = System.Drawing.Color.White
        Me.chargesName.Location = New System.Drawing.Point(76, 111)
        Me.chargesName.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.chargesName.Name = "chargesName"
        Me.chargesName.ReadOnly = True
        Me.chargesName.Size = New System.Drawing.Size(164, 21)
        Me.chargesName.TabIndex = 21
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(10, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 15)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Charge ID"
        '
        'chargesID
        '
        Me.chargesID.BackColor = System.Drawing.Color.White
        Me.chargesID.Location = New System.Drawing.Point(76, 20)
        Me.chargesID.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.chargesID.Name = "chargesID"
        Me.chargesID.ReadOnly = True
        Me.chargesID.Size = New System.Drawing.Size(41, 21)
        Me.chargesID.TabIndex = 19
        '
        'listZone
        '
        Me.listZone.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5})
        Me.listZone.FullRowSelect = True
        Me.listZone.GridLines = True
        Me.listZone.HideSelection = False
        Me.listZone.Location = New System.Drawing.Point(17, 142)
        Me.listZone.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.listZone.Name = "listZone"
        Me.listZone.Size = New System.Drawing.Size(493, 143)
        Me.listZone.TabIndex = 18
        Me.listZone.UseCompatibleStateImageBehavior = False
        Me.listZone.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Name"
        Me.ColumnHeader4.Width = 107
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Amount"
        Me.ColumnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ColumnHeader5.Width = 100
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
        Me.Button2.Location = New System.Drawing.Point(460, 4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(72, 30)
        Me.Button2.TabIndex = 28
        Me.Button2.Text = " Close"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.tbByCons)
        Me.Panel1.Controls.Add(Me.tbByAmount)
        Me.Panel1.Controls.Add(Me.rbByConsumption)
        Me.Panel1.Controls.Add(Me.rbByAmount)
        Me.Panel1.Controls.Add(Me.rbFixed)
        Me.Panel1.Controls.Add(Me.chargesEntry)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.chargesCat)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.lblmode)
        Me.Panel1.Controls.Add(Me.listZone)
        Me.Panel1.Controls.Add(Me.chargesAmount)
        Me.Panel1.Controls.Add(Me.chargesID)
        Me.Panel1.Controls.Add(Me.chargesName)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Location = New System.Drawing.Point(2, 38)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(530, 358)
        Me.Panel1.TabIndex = 29
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(367, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 15)
        Me.Label3.TabIndex = 38
        Me.Label3.Text = "Rate/Amount"
        '
        'tbByCons
        '
        Me.tbByCons.BackColor = System.Drawing.Color.White
        Me.tbByCons.Location = New System.Drawing.Point(370, 111)
        Me.tbByCons.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.tbByCons.Name = "tbByCons"
        Me.tbByCons.Size = New System.Drawing.Size(140, 21)
        Me.tbByCons.TabIndex = 37
        '
        'tbByAmount
        '
        Me.tbByAmount.BackColor = System.Drawing.Color.White
        Me.tbByAmount.Location = New System.Drawing.Point(370, 80)
        Me.tbByAmount.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.tbByAmount.Name = "tbByAmount"
        Me.tbByAmount.Size = New System.Drawing.Size(140, 21)
        Me.tbByAmount.TabIndex = 35
        '
        'rbByConsumption
        '
        Me.rbByConsumption.AutoSize = True
        Me.rbByConsumption.Location = New System.Drawing.Point(251, 112)
        Me.rbByConsumption.Name = "rbByConsumption"
        Me.rbByConsumption.Size = New System.Drawing.Size(113, 20)
        Me.rbByConsumption.TabIndex = 34
        Me.rbByConsumption.TabStop = True
        Me.rbByConsumption.Text = "By Consumption"
        Me.rbByConsumption.UseVisualStyleBackColor = True
        '
        'rbByAmount
        '
        Me.rbByAmount.AutoSize = True
        Me.rbByAmount.Location = New System.Drawing.Point(251, 81)
        Me.rbByAmount.Name = "rbByAmount"
        Me.rbByAmount.Size = New System.Drawing.Size(83, 20)
        Me.rbByAmount.TabIndex = 33
        Me.rbByAmount.TabStop = True
        Me.rbByAmount.Text = "By Amount"
        Me.rbByAmount.UseVisualStyleBackColor = True
        '
        'rbFixed
        '
        Me.rbFixed.AutoSize = True
        Me.rbFixed.Location = New System.Drawing.Point(251, 50)
        Me.rbFixed.Name = "rbFixed"
        Me.rbFixed.Size = New System.Drawing.Size(54, 20)
        Me.rbFixed.TabIndex = 32
        Me.rbFixed.TabStop = True
        Me.rbFixed.Text = "Fixed"
        Me.rbFixed.UseVisualStyleBackColor = True
        '
        'chargesEntry
        '
        Me.chargesEntry.Enabled = False
        Me.chargesEntry.FormattingEnabled = True
        Me.chargesEntry.Items.AddRange(New Object() {"Misc.", "Penalty", "Others", "Charges", "Sundries"})
        Me.chargesEntry.Location = New System.Drawing.Point(76, 79)
        Me.chargesEntry.Name = "chargesEntry"
        Me.chargesEntry.Size = New System.Drawing.Size(164, 24)
        Me.chargesEntry.TabIndex = 31
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(37, 82)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(33, 15)
        Me.Label6.TabIndex = 30
        Me.Label6.Text = "Entry"
        '
        'chargesCat
        '
        Me.chargesCat.Enabled = False
        Me.chargesCat.FormattingEnabled = True
        Me.chargesCat.Items.AddRange(New Object() {"Charges", "Others"})
        Me.chargesCat.Location = New System.Drawing.Point(76, 49)
        Me.chargesCat.Name = "chargesCat"
        Me.chargesCat.Size = New System.Drawing.Size(164, 24)
        Me.chargesCat.TabIndex = 29
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(14, 52)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 15)
        Me.Label5.TabIndex = 28
        Me.Label5.Text = "Category"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Century Gothic", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(11, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(119, 17)
        Me.Label4.TabIndex = 28
        Me.Label4.Text = "Charges Settings"
        '
        'settingscharges
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(195, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(534, 426)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "settingscharges"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "charges"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblmode As Label
    Friend WithEvents EditToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents NewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents chargesAmount As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents chargesName As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents chargesID As TextBox
    Friend WithEvents listZone As ListView
    Friend WithEvents Button2 As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents chargesCat As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents chargesEntry As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents RefreshToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label3 As Label
    Friend WithEvents tbByCons As TextBox
    Friend WithEvents tbByAmount As TextBox
    Friend WithEvents rbByConsumption As RadioButton
    Friend WithEvents rbByAmount As RadioButton
    Friend WithEvents rbFixed As RadioButton
End Class
