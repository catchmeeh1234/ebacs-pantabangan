<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OR_Items
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OR_Items))
        Me.rbcharges = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbothers = New System.Windows.Forms.RadioButton()
        Me.rbmaterials = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbitems = New System.Windows.Forms.ComboBox()
        Me.unitcost = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.quantity = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.totalamount = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbladd = New System.Windows.Forms.Label()
        Me.lblid = New System.Windows.Forms.Label()
        Me.itemsdataset = New System.Data.DataSet()
        Me.DataTable1 = New System.Data.DataTable()
        Me.DataColumn1 = New System.Data.DataColumn()
        Me.DataColumn2 = New System.Data.DataColumn()
        Me.DataColumn3 = New System.Data.DataColumn()
        Me.DataColumn4 = New System.Data.DataColumn()
        Me.DataColumn5 = New System.Data.DataColumn()
        Me.DataColumn6 = New System.Data.DataColumn()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.itemsdataset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'rbcharges
        '
        Me.rbcharges.AutoSize = True
        Me.rbcharges.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbcharges.Location = New System.Drawing.Point(52, 29)
        Me.rbcharges.Name = "rbcharges"
        Me.rbcharges.Size = New System.Drawing.Size(70, 19)
        Me.rbcharges.TabIndex = 0
        Me.rbcharges.TabStop = True
        Me.rbcharges.Text = "Charges"
        Me.rbcharges.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbothers)
        Me.GroupBox1.Controls.Add(Me.rbmaterials)
        Me.GroupBox1.Controls.Add(Me.rbcharges)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(11, 13)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(420, 65)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Category"
        '
        'rbothers
        '
        Me.rbothers.AutoSize = True
        Me.rbothers.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbothers.Location = New System.Drawing.Point(314, 29)
        Me.rbothers.Name = "rbothers"
        Me.rbothers.Size = New System.Drawing.Size(60, 19)
        Me.rbothers.TabIndex = 2
        Me.rbothers.TabStop = True
        Me.rbothers.Text = "Others"
        Me.rbothers.UseVisualStyleBackColor = True
        '
        'rbmaterials
        '
        Me.rbmaterials.AutoSize = True
        Me.rbmaterials.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbmaterials.Location = New System.Drawing.Point(177, 29)
        Me.rbmaterials.Name = "rbmaterials"
        Me.rbmaterials.Size = New System.Drawing.Size(74, 19)
        Me.rbmaterials.TabIndex = 1
        Me.rbmaterials.TabStop = True
        Me.rbmaterials.Text = "Materials"
        Me.rbmaterials.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(60, 101)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(28, 15)
        Me.Label1.TabIndex = 48
        Me.Label1.Text = "Item"
        '
        'cbitems
        '
        Me.cbitems.BackColor = System.Drawing.SystemColors.Menu
        Me.cbitems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbitems.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbitems.ForeColor = System.Drawing.Color.Black
        Me.cbitems.FormattingEnabled = True
        Me.cbitems.Location = New System.Drawing.Point(94, 96)
        Me.cbitems.Name = "cbitems"
        Me.cbitems.Size = New System.Drawing.Size(307, 24)
        Me.cbitems.TabIndex = 49
        '
        'unitcost
        '
        Me.unitcost.BackColor = System.Drawing.Color.White
        Me.unitcost.ForeColor = System.Drawing.Color.Black
        Me.unitcost.Location = New System.Drawing.Point(94, 126)
        Me.unitcost.Name = "unitcost"
        Me.unitcost.ReadOnly = True
        Me.unitcost.Size = New System.Drawing.Size(307, 21)
        Me.unitcost.TabIndex = 50
        Me.unitcost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(35, 129)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 15)
        Me.Label6.TabIndex = 51
        Me.Label6.Text = "Unit Cost"
        '
        'quantity
        '
        Me.quantity.BackColor = System.Drawing.Color.White
        Me.quantity.ForeColor = System.Drawing.Color.Black
        Me.quantity.Location = New System.Drawing.Point(94, 153)
        Me.quantity.Name = "quantity"
        Me.quantity.Size = New System.Drawing.Size(307, 21)
        Me.quantity.TabIndex = 52
        Me.quantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(36, 156)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 15)
        Me.Label2.TabIndex = 53
        Me.Label2.Text = "Quantity"
        '
        'totalamount
        '
        Me.totalamount.BackColor = System.Drawing.Color.White
        Me.totalamount.ForeColor = System.Drawing.Color.Black
        Me.totalamount.Location = New System.Drawing.Point(94, 180)
        Me.totalamount.Name = "totalamount"
        Me.totalamount.ReadOnly = True
        Me.totalamount.Size = New System.Drawing.Size(307, 21)
        Me.totalamount.TabIndex = 54
        Me.totalamount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(12, 183)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 15)
        Me.Label3.TabIndex = 55
        Me.Label3.Text = "Total Amount"
        '
        'lbladd
        '
        Me.lbladd.AutoSize = True
        Me.lbladd.Font = New System.Drawing.Font("Century Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbladd.ForeColor = System.Drawing.Color.Black
        Me.lbladd.Location = New System.Drawing.Point(365, 224)
        Me.lbladd.Name = "lbladd"
        Me.lbladd.Size = New System.Drawing.Size(36, 16)
        Me.lbladd.TabIndex = 56
        Me.lbladd.Text = "Add"
        Me.lbladd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblid
        '
        Me.lblid.AutoSize = True
        Me.lblid.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblid.ForeColor = System.Drawing.Color.Black
        Me.lblid.Location = New System.Drawing.Point(18, 99)
        Me.lblid.Name = "lblid"
        Me.lblid.Size = New System.Drawing.Size(17, 15)
        Me.lblid.TabIndex = 58
        Me.lblid.Text = "ID"
        Me.lblid.Visible = False
        '
        'itemsdataset
        '
        Me.itemsdataset.DataSetName = "NewDataSet"
        Me.itemsdataset.Tables.AddRange(New System.Data.DataTable() {Me.DataTable1})
        '
        'DataTable1
        '
        Me.DataTable1.Columns.AddRange(New System.Data.DataColumn() {Me.DataColumn1, Me.DataColumn2, Me.DataColumn3, Me.DataColumn4, Me.DataColumn5, Me.DataColumn6})
        Me.DataTable1.TableName = "Table1"
        '
        'DataColumn1
        '
        Me.DataColumn1.ColumnName = "ChargeID"
        '
        'DataColumn2
        '
        Me.DataColumn2.ColumnName = "Particular"
        '
        'DataColumn3
        '
        Me.DataColumn3.ColumnName = "Particular_Amount"
        '
        'DataColumn4
        '
        Me.DataColumn4.ColumnName = "Amount"
        '
        'DataColumn5
        '
        Me.DataColumn5.ColumnName = "Category"
        '
        'DataColumn6
        '
        Me.DataColumn6.ColumnName = "Entry"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.lblid)
        Me.Panel1.Controls.Add(Me.cbitems)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.lbladd)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.totalamount)
        Me.Panel1.Controls.Add(Me.unitcost)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.quantity)
        Me.Panel1.Location = New System.Drawing.Point(1, 38)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(442, 248)
        Me.Panel1.TabIndex = 59
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
        Me.Button1.Location = New System.Drawing.Point(369, 4)
        Me.Button1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(72, 30)
        Me.Button1.TabIndex = 60
        Me.Button1.Text = " Close"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Century Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(7, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(74, 16)
        Me.Label4.TabIndex = 59
        Me.Label4.Text = "Add Items"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'OR_Items
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(195, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(444, 287)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "OR_Items"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "OR_Items"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.itemsdataset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataTable1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents rbcharges As RadioButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents rbothers As RadioButton
    Friend WithEvents rbmaterials As RadioButton
    Friend WithEvents Label1 As Label
    Friend WithEvents cbitems As ComboBox
    Public WithEvents unitcost As TextBox
    Friend WithEvents Label6 As Label
    Public WithEvents quantity As TextBox
    Friend WithEvents Label2 As Label
    Public WithEvents totalamount As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents lbladd As Label
    Friend WithEvents lblid As Label
    Friend WithEvents itemsdataset As DataSet
    Friend WithEvents DataTable1 As DataTable
    Friend WithEvents DataColumn1 As DataColumn
    Friend WithEvents DataColumn2 As DataColumn
    Friend WithEvents DataColumn3 As DataColumn
    Friend WithEvents DataColumn4 As DataColumn
    Friend WithEvents DataColumn5 As DataColumn
    Friend WithEvents DataColumn6 As DataColumn
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Button1 As Button
    Friend WithEvents Label4 As Label
End Class
