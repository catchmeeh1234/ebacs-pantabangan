<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class updatestatus
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(updatestatus))
        Me.lblmode = New System.Windows.Forms.Label()
        Me.EditToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblremarks = New System.Windows.Forms.Label()
        Me.statusremarks = New System.Windows.Forms.TextBox()
        Me.listStatus = New System.Windows.Forms.ListView()
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblstatus = New System.Windows.Forms.Label()
        Me.statusname = New System.Windows.Forms.ComboBox()
        Me.lblID = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Performedby = New System.Windows.Forms.TextBox()
        Me.lblmeterstatus = New System.Windows.Forms.Label()
        Me.meterstatus = New System.Windows.Forms.ComboBox()
        Me.lbldisctype = New System.Windows.Forms.Label()
        Me.disctype = New System.Windows.Forms.ComboBox()
        Me.lblLastreading = New System.Windows.Forms.Label()
        Me.lastreading = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.statusDate = New System.Windows.Forms.DateTimePicker()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblmode
        '
        Me.lblmode.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmode.Location = New System.Drawing.Point(200, 20)
        Me.lblmode.Name = "lblmode"
        Me.lblmode.Size = New System.Drawing.Size(85, 25)
        Me.lblmode.TabIndex = 35
        Me.lblmode.Text = "Mode"
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
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveToolStripMenuItem, Me.NewToolStripMenuItem, Me.EditToolStripMenuItem1, Me.DeleteToolStripMenuItem})
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
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1155, 24)
        Me.MenuStrip1.TabIndex = 34
        Me.MenuStrip1.Text = "MenuStrip1"
        Me.MenuStrip1.Visible = False
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Status"
        Me.ColumnHeader2.Width = 100
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Date"
        Me.ColumnHeader1.Width = 80
        '
        'lblremarks
        '
        Me.lblremarks.AutoSize = True
        Me.lblremarks.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblremarks.Location = New System.Drawing.Point(33, 192)
        Me.lblremarks.Name = "lblremarks"
        Me.lblremarks.Size = New System.Drawing.Size(52, 15)
        Me.lblremarks.TabIndex = 32
        Me.lblremarks.Text = "Remarks"
        '
        'statusremarks
        '
        Me.statusremarks.BackColor = System.Drawing.Color.White
        Me.statusremarks.Location = New System.Drawing.Point(91, 192)
        Me.statusremarks.Multiline = True
        Me.statusremarks.Name = "statusremarks"
        Me.statusremarks.ReadOnly = True
        Me.statusremarks.Size = New System.Drawing.Size(180, 63)
        Me.statusremarks.TabIndex = 7
        '
        'listStatus
        '
        Me.listStatus.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader4, Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader6, Me.ColumnHeader7, Me.ColumnHeader5, Me.ColumnHeader3, Me.ColumnHeader9, Me.ColumnHeader8})
        Me.listStatus.FullRowSelect = True
        Me.listStatus.GridLines = True
        Me.listStatus.HideSelection = False
        Me.listStatus.Location = New System.Drawing.Point(291, 21)
        Me.listStatus.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.listStatus.MultiSelect = False
        Me.listStatus.Name = "listStatus"
        Me.listStatus.Size = New System.Drawing.Size(849, 236)
        Me.listStatus.TabIndex = 8
        Me.listStatus.UseCompatibleStateImageBehavior = False
        Me.listStatus.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "ID"
        Me.ColumnHeader4.Width = 0
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Disc. Type"
        Me.ColumnHeader6.Width = 100
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Meter Status"
        Me.ColumnHeader7.Width = 100
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Last Reading"
        Me.ColumnHeader5.Width = 90
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Remarks"
        Me.ColumnHeader3.Width = 150
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "Performed by"
        Me.ColumnHeader9.Width = 100
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "Updated By"
        Me.ColumnHeader8.Width = 100
        '
        'lblstatus
        '
        Me.lblstatus.AutoSize = True
        Me.lblstatus.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstatus.Location = New System.Drawing.Point(47, 51)
        Me.lblstatus.Name = "lblstatus"
        Me.lblstatus.Size = New System.Drawing.Size(38, 15)
        Me.lblstatus.TabIndex = 37
        Me.lblstatus.Text = "Status"
        '
        'statusname
        '
        Me.statusname.BackColor = System.Drawing.SystemColors.Control
        Me.statusname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.statusname.Enabled = False
        Me.statusname.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.statusname.FormattingEnabled = True
        Me.statusname.Items.AddRange(New Object() {"Active", "Don't Bill", "Disconnected"})
        Me.statusname.Location = New System.Drawing.Point(91, 48)
        Me.statusname.Name = "statusname"
        Me.statusname.Size = New System.Drawing.Size(180, 24)
        Me.statusname.TabIndex = 2
        '
        'lblID
        '
        Me.lblID.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblID.Location = New System.Drawing.Point(3, 0)
        Me.lblID.Name = "lblID"
        Me.lblID.Size = New System.Drawing.Size(17, 25)
        Me.lblID.TabIndex = 38
        Me.lblID.Text = "Mode"
        Me.lblID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblID.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Performedby)
        Me.Panel1.Controls.Add(Me.lblmeterstatus)
        Me.Panel1.Controls.Add(Me.lblID)
        Me.Panel1.Controls.Add(Me.meterstatus)
        Me.Panel1.Controls.Add(Me.lbldisctype)
        Me.Panel1.Controls.Add(Me.disctype)
        Me.Panel1.Controls.Add(Me.lblLastreading)
        Me.Panel1.Controls.Add(Me.lastreading)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.statusDate)
        Me.Panel1.Controls.Add(Me.lblstatus)
        Me.Panel1.Controls.Add(Me.listStatus)
        Me.Panel1.Controls.Add(Me.statusname)
        Me.Panel1.Controls.Add(Me.statusremarks)
        Me.Panel1.Controls.Add(Me.lblremarks)
        Me.Panel1.Controls.Add(Me.lblmode)
        Me.Panel1.Location = New System.Drawing.Point(2, 37)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1151, 266)
        Me.Panel1.TabIndex = 39
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(7, 141)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(78, 15)
        Me.Label3.TabIndex = 47
        Me.Label3.Text = "Performed by"
        '
        'Performedby
        '
        Me.Performedby.BackColor = System.Drawing.Color.White
        Me.Performedby.Location = New System.Drawing.Point(91, 138)
        Me.Performedby.Name = "Performedby"
        Me.Performedby.ReadOnly = True
        Me.Performedby.Size = New System.Drawing.Size(180, 21)
        Me.Performedby.TabIndex = 5
        '
        'lblmeterstatus
        '
        Me.lblmeterstatus.AutoSize = True
        Me.lblmeterstatus.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmeterstatus.Location = New System.Drawing.Point(13, 111)
        Me.lblmeterstatus.Name = "lblmeterstatus"
        Me.lblmeterstatus.Size = New System.Drawing.Size(72, 15)
        Me.lblmeterstatus.TabIndex = 45
        Me.lblmeterstatus.Text = "Meter Status"
        '
        'meterstatus
        '
        Me.meterstatus.BackColor = System.Drawing.SystemColors.Control
        Me.meterstatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.meterstatus.Enabled = False
        Me.meterstatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.meterstatus.FormattingEnabled = True
        Me.meterstatus.Items.AddRange(New Object() {"Pulled Out", "Locked", "Permanent"})
        Me.meterstatus.Location = New System.Drawing.Point(91, 108)
        Me.meterstatus.Name = "meterstatus"
        Me.meterstatus.Size = New System.Drawing.Size(180, 24)
        Me.meterstatus.TabIndex = 4
        '
        'lbldisctype
        '
        Me.lbldisctype.AutoSize = True
        Me.lbldisctype.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldisctype.Location = New System.Drawing.Point(24, 81)
        Me.lbldisctype.Name = "lbldisctype"
        Me.lbldisctype.Size = New System.Drawing.Size(61, 15)
        Me.lbldisctype.TabIndex = 43
        Me.lbldisctype.Text = "Disc. Type"
        '
        'disctype
        '
        Me.disctype.BackColor = System.Drawing.SystemColors.Control
        Me.disctype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.disctype.Enabled = False
        Me.disctype.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.disctype.FormattingEnabled = True
        Me.disctype.Items.AddRange(New Object() {"Voluntary", "Involuntary"})
        Me.disctype.Location = New System.Drawing.Point(91, 78)
        Me.disctype.Name = "disctype"
        Me.disctype.Size = New System.Drawing.Size(180, 24)
        Me.disctype.TabIndex = 3
        '
        'lblLastreading
        '
        Me.lblLastreading.AutoSize = True
        Me.lblLastreading.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastreading.Location = New System.Drawing.Point(10, 168)
        Me.lblLastreading.Name = "lblLastreading"
        Me.lblLastreading.Size = New System.Drawing.Size(75, 15)
        Me.lblLastreading.TabIndex = 41
        Me.lblLastreading.Text = "Last Reading"
        '
        'lastreading
        '
        Me.lastreading.BackColor = System.Drawing.Color.White
        Me.lastreading.Location = New System.Drawing.Point(91, 165)
        Me.lastreading.Name = "lastreading"
        Me.lastreading.ReadOnly = True
        Me.lastreading.Size = New System.Drawing.Size(180, 21)
        Me.lastreading.TabIndex = 6
        Me.lastreading.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(53, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 15)
        Me.Label2.TabIndex = 39
        Me.Label2.Text = "Date"
        '
        'statusDate
        '
        Me.statusDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.statusDate.Location = New System.Drawing.Point(91, 21)
        Me.statusDate.Name = "statusDate"
        Me.statusDate.Size = New System.Drawing.Size(102, 21)
        Me.statusDate.TabIndex = 1
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
        Me.Button1.Location = New System.Drawing.Point(1079, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(72, 30)
        Me.Button1.TabIndex = 48
        Me.Button1.Text = " Close"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 17)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "Update Status"
        '
        'updatestatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(195, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1155, 305)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "updatestatus"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "updatestatus"
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
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents lblremarks As Label
    Friend WithEvents statusremarks As TextBox
    Friend WithEvents listStatus As ListView
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents lblstatus As Label
    Friend WithEvents statusname As ComboBox
    Friend WithEvents lblID As Label
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Button1 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents ColumnHeader7 As ColumnHeader
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents lblmeterstatus As Label
    Friend WithEvents meterstatus As ComboBox
    Friend WithEvents lbldisctype As Label
    Friend WithEvents disctype As ComboBox
    Friend WithEvents lblLastreading As Label
    Friend WithEvents lastreading As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents statusDate As DateTimePicker
    Friend WithEvents ColumnHeader8 As ColumnHeader
    Friend WithEvents Label3 As Label
    Friend WithEvents Performedby As TextBox
    Friend WithEvents ColumnHeader9 As ColumnHeader
End Class
