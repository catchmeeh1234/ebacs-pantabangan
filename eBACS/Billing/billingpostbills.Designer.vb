<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class billingpostbills
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(billingpostbills))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.billPost = New System.Windows.Forms.Button()
        Me.billZone = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.billYear = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.billMonth = New System.Windows.Forms.ComboBox()
        Me.billList = New System.Windows.Forms.DataGridView()
        Me.billTotalbill = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkHighCons = New System.Windows.Forms.CheckBox()
        Me.chkZero = New System.Windows.Forms.CheckBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Column1 = New System.Windows.Forms.DataGridViewLinkColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Column11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column12 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Column14 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.billList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
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
        Me.Button2.Location = New System.Drawing.Point(834, 4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(72, 30)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = " Close"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button2.UseVisualStyleBackColor = True
        '
        'billPost
        '
        Me.billPost.FlatAppearance.BorderSize = 0
        Me.billPost.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.billPost.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.billPost.Location = New System.Drawing.Point(830, 470)
        Me.billPost.Name = "billPost"
        Me.billPost.Size = New System.Drawing.Size(68, 23)
        Me.billPost.TabIndex = 35
        Me.billPost.Text = "Post"
        Me.billPost.UseVisualStyleBackColor = True
        '
        'billZone
        '
        Me.billZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.billZone.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.billZone.FormattingEnabled = True
        Me.billZone.Location = New System.Drawing.Point(338, 21)
        Me.billZone.Name = "billZone"
        Me.billZone.Size = New System.Drawing.Size(197, 24)
        Me.billZone.TabIndex = 33
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(298, 24)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(34, 15)
        Me.Label12.TabIndex = 37
        Me.Label12.Text = "Zone"
        '
        'billYear
        '
        Me.billYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.billYear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.billYear.FormattingEnabled = True
        Me.billYear.Location = New System.Drawing.Point(222, 21)
        Me.billYear.Name = "billYear"
        Me.billYear.Size = New System.Drawing.Size(70, 24)
        Me.billYear.TabIndex = 32
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(11, 24)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(78, 15)
        Me.Label11.TabIndex = 36
        Me.Label11.Text = "Billing Period"
        '
        'billMonth
        '
        Me.billMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.billMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.billMonth.FormattingEnabled = True
        Me.billMonth.Location = New System.Drawing.Point(95, 21)
        Me.billMonth.Name = "billMonth"
        Me.billMonth.Size = New System.Drawing.Size(121, 24)
        Me.billMonth.TabIndex = 31
        '
        'billList
        '
        Me.billList.AllowUserToAddRows = False
        Me.billList.AllowUserToDeleteRows = False
        Me.billList.AllowUserToResizeColumns = False
        Me.billList.AllowUserToResizeRows = False
        Me.billList.BackgroundColor = System.Drawing.Color.White
        Me.billList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.billList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column7, Me.Column3, Me.Column8, Me.Column5, Me.Column10, Me.Column9, Me.Column4, Me.Column6, Me.Column11, Me.Column13, Me.Column12, Me.Column14})
        Me.billList.Location = New System.Drawing.Point(11, 57)
        Me.billList.Name = "billList"
        Me.billList.RowHeadersVisible = False
        Me.billList.Size = New System.Drawing.Size(887, 404)
        Me.billList.TabIndex = 30
        '
        'billTotalbill
        '
        Me.billTotalbill.AutoSize = True
        Me.billTotalbill.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.billTotalbill.Location = New System.Drawing.Point(762, 474)
        Me.billTotalbill.Name = "billTotalbill"
        Me.billTotalbill.Size = New System.Drawing.Size(0, 15)
        Me.billTotalbill.TabIndex = 39
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(697, 474)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(59, 15)
        Me.Label23.TabIndex = 38
        Me.Label23.Text = "Total Bills:"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.CheckBox1)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.billList)
        Me.Panel1.Controls.Add(Me.billTotalbill)
        Me.Panel1.Controls.Add(Me.billMonth)
        Me.Panel1.Controls.Add(Me.Label23)
        Me.Panel1.Controls.Add(Me.billYear)
        Me.Panel1.Controls.Add(Me.billPost)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.billZone)
        Me.Panel1.Location = New System.Drawing.Point(1, 38)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(909, 504)
        Me.Panel1.TabIndex = 40
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkHighCons)
        Me.GroupBox1.Controls.Add(Me.chkZero)
        Me.GroupBox1.Location = New System.Drawing.Point(646, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(252, 48)
        Me.GroupBox1.TabIndex = 41
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Exclude from Posting"
        '
        'chkHighCons
        '
        Me.chkHighCons.AutoSize = True
        Me.chkHighCons.Location = New System.Drawing.Point(120, 20)
        Me.chkHighCons.Name = "chkHighCons"
        Me.chkHighCons.Size = New System.Drawing.Size(126, 20)
        Me.chkHighCons.TabIndex = 1
        Me.chkHighCons.Text = "High Consumption"
        Me.chkHighCons.UseVisualStyleBackColor = True
        '
        'chkZero
        '
        Me.chkZero.AutoSize = True
        Me.chkZero.Location = New System.Drawing.Point(6, 20)
        Me.chkZero.Name = "chkZero"
        Me.chkZero.Size = New System.Drawing.Size(108, 20)
        Me.chkZero.TabIndex = 0
        Me.chkZero.Text = "0 Consumption"
        Me.chkZero.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(541, 23)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(76, 20)
        Me.CheckBox1.TabIndex = 40
        Me.CheckBox1.Text = "Check All"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(12, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 17)
        Me.Label1.TabIndex = 41
        Me.Label1.Text = "Post Bills"
        '
        'Column1
        '
        Me.Column1.HeaderText = "Bill No."
        Me.Column1.LinkColor = System.Drawing.Color.Black
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Column1.Width = 70
        '
        'Column2
        '
        Me.Column2.HeaderText = "Date"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 80
        '
        'Column7
        '
        Me.Column7.HeaderText = "Account No."
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        Me.Column7.Width = 80
        '
        'Column3
        '
        Me.Column3.HeaderText = "Account Name"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 220
        '
        'Column8
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column8.DefaultCellStyle = DataGridViewCellStyle1
        Me.Column8.HeaderText = "Reading"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        Me.Column8.Width = 60
        '
        'Column5
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column5.DefaultCellStyle = DataGridViewCellStyle2
        Me.Column5.HeaderText = "Cons."
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Width = 50
        '
        'Column10
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column10.DefaultCellStyle = DataGridViewCellStyle3
        Me.Column10.HeaderText = "Credit"
        Me.Column10.Name = "Column10"
        Me.Column10.Width = 70
        '
        'Column9
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column9.DefaultCellStyle = DataGridViewCellStyle4
        Me.Column9.HeaderText = "Amount"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        Me.Column9.Width = 80
        '
        'Column4
        '
        Me.Column4.HeaderText = "Remarks"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Column6
        '
        Me.Column6.HeaderText = "Post"
        Me.Column6.Name = "Column6"
        Me.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Column6.Width = 50
        '
        'Column11
        '
        Me.Column11.HeaderText = "Penalty"
        Me.Column11.Name = "Column11"
        '
        'Column13
        '
        Me.Column13.HeaderText = "Average"
        Me.Column13.Name = "Column13"
        Me.Column13.Visible = False
        '
        'Column12
        '
        Me.Column12.HeaderText = "temptag"
        Me.Column12.Name = "Column12"
        Me.Column12.Visible = False
        '
        'Column14
        '
        Me.Column14.HeaderText = "advancepayment"
        Me.Column14.Name = "Column14"
        '
        'billingpostbills
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(195, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(911, 543)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "billingpostbills"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "postingbills"
        CType(Me.billList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button2 As Button
    Friend WithEvents billPost As Button
    Friend WithEvents billZone As ComboBox
    Friend WithEvents Label12 As Label
    Friend WithEvents billYear As ComboBox
    Friend WithEvents Label11 As Label
    Friend WithEvents billMonth As ComboBox
    Public WithEvents billList As DataGridView
    Friend WithEvents billTotalbill As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents chkZero As CheckBox
    Friend WithEvents chkHighCons As CheckBox
    Friend WithEvents Column1 As DataGridViewLinkColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column7 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column8 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column10 As DataGridViewTextBoxColumn
    Friend WithEvents Column9 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewCheckBoxColumn
    Friend WithEvents Column11 As DataGridViewTextBoxColumn
    Friend WithEvents Column13 As DataGridViewTextBoxColumn
    Friend WithEvents Column12 As DataGridViewCheckBoxColumn
    Friend WithEvents Column14 As DataGridViewTextBoxColumn
End Class
