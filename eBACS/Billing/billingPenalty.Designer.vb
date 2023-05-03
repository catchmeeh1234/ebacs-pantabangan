<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class billingPenalty
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(billingPenalty))
        Me.penaltyList = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.billcount = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dateCovered = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dateto = New System.Windows.Forms.DateTimePicker()
        Me.datefrom = New System.Windows.Forms.DateTimePicker()
        Me.penaltymodify = New System.Windows.Forms.CheckBox()
        Me.prepare = New System.Windows.Forms.Button()
        Me.lastdaynopen = New System.Windows.Forms.DateTimePicker()
        Me.postpenalty = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.billYear = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.billMonth = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.penaltyList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.dateCovered.SuspendLayout()
        Me.SuspendLayout()
        '
        'penaltyList
        '
        Me.penaltyList.AllowUserToAddRows = False
        Me.penaltyList.AllowUserToDeleteRows = False
        Me.penaltyList.BackgroundColor = System.Drawing.Color.White
        Me.penaltyList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.penaltyList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column4, Me.Column5, Me.Column6, Me.Column3})
        Me.penaltyList.Location = New System.Drawing.Point(297, 15)
        Me.penaltyList.Name = "penaltyList"
        Me.penaltyList.RowHeadersVisible = False
        Me.penaltyList.Size = New System.Drawing.Size(593, 188)
        Me.penaltyList.TabIndex = 4
        '
        'Column1
        '
        Me.Column1.HeaderText = "Bill No."
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 70
        '
        'Column2
        '
        Me.Column2.HeaderText = "Account No"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 90
        '
        'Column4
        '
        Me.Column4.HeaderText = "Name"
        Me.Column4.Name = "Column4"
        Me.Column4.Width = 200
        '
        'Column5
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column5.DefaultCellStyle = DataGridViewCellStyle1
        Me.Column5.HeaderText = "Billing"
        Me.Column5.Name = "Column5"
        Me.Column5.Width = 70
        '
        'Column6
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column6.DefaultCellStyle = DataGridViewCellStyle2
        Me.Column6.HeaderText = "Penalty"
        Me.Column6.Name = "Column6"
        Me.Column6.Width = 70
        '
        'Column3
        '
        Me.Column3.HeaderText = "Select"
        Me.Column3.Name = "Column3"
        Me.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Column3.Width = 60
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
        Me.Button2.Location = New System.Drawing.Point(828, 3)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(72, 30)
        Me.Button2.TabIndex = 8
        Me.Button2.Text = " Close"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.CheckBox1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Controls.Add(Me.billcount)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.dateCovered)
        Me.Panel1.Controls.Add(Me.penaltymodify)
        Me.Panel1.Controls.Add(Me.prepare)
        Me.Panel1.Controls.Add(Me.lastdaynopen)
        Me.Panel1.Controls.Add(Me.postpenalty)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.billYear)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.billMonth)
        Me.Panel1.Controls.Add(Me.penaltyList)
        Me.Panel1.Location = New System.Drawing.Point(2, 38)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(898, 240)
        Me.Panel1.TabIndex = 9
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(803, 210)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(76, 20)
        Me.CheckBox1.TabIndex = 41
        Me.CheckBox1.Text = "Check All"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(629, 210)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(121, 23)
        Me.ProgressBar1.TabIndex = 40
        Me.ProgressBar1.Visible = False
        '
        'billcount
        '
        Me.billcount.AutoSize = True
        Me.billcount.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.billcount.Location = New System.Drawing.Point(583, 210)
        Me.billcount.Name = "billcount"
        Me.billcount.Size = New System.Drawing.Size(0, 15)
        Me.billcount.TabIndex = 39
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(490, 210)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(87, 15)
        Me.Label4.TabIndex = 38
        Me.Label4.Text = "Number of Bills"
        '
        'dateCovered
        '
        Me.dateCovered.Controls.Add(Me.Label3)
        Me.dateCovered.Controls.Add(Me.dateto)
        Me.dateCovered.Controls.Add(Me.datefrom)
        Me.dateCovered.Enabled = False
        Me.dateCovered.Location = New System.Drawing.Point(13, 121)
        Me.dateCovered.Name = "dateCovered"
        Me.dateCovered.Size = New System.Drawing.Size(278, 57)
        Me.dateCovered.TabIndex = 37
        Me.dateCovered.TabStop = False
        Me.dateCovered.Text = "Billing Date Covered"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(129, 29)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(20, 15)
        Me.Label3.TabIndex = 38
        Me.Label3.Text = "to"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dateto
        '
        Me.dateto.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dateto.Location = New System.Drawing.Point(155, 25)
        Me.dateto.Name = "dateto"
        Me.dateto.Size = New System.Drawing.Size(100, 21)
        Me.dateto.TabIndex = 39
        '
        'datefrom
        '
        Me.datefrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.datefrom.Location = New System.Drawing.Point(23, 25)
        Me.datefrom.Name = "datefrom"
        Me.datefrom.Size = New System.Drawing.Size(100, 21)
        Me.datefrom.TabIndex = 38
        '
        'penaltymodify
        '
        Me.penaltymodify.Appearance = System.Windows.Forms.Appearance.Button
        Me.penaltymodify.AutoSize = True
        Me.penaltymodify.FlatAppearance.BorderSize = 0
        Me.penaltymodify.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightGreen
        Me.penaltymodify.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.penaltymodify.Location = New System.Drawing.Point(13, 89)
        Me.penaltymodify.Name = "penaltymodify"
        Me.penaltymodify.Size = New System.Drawing.Size(55, 26)
        Me.penaltymodify.TabIndex = 36
        Me.penaltymodify.Text = "Modify"
        Me.penaltymodify.UseVisualStyleBackColor = True
        '
        'prepare
        '
        Me.prepare.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.prepare.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.prepare.Location = New System.Drawing.Point(135, 184)
        Me.prepare.Name = "prepare"
        Me.prepare.Size = New System.Drawing.Size(75, 48)
        Me.prepare.TabIndex = 35
        Me.prepare.Text = "Prepare"
        Me.prepare.UseVisualStyleBackColor = True
        '
        'lastdaynopen
        '
        Me.lastdaynopen.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.lastdaynopen.Location = New System.Drawing.Point(144, 45)
        Me.lastdaynopen.Name = "lastdaynopen"
        Me.lastdaynopen.Size = New System.Drawing.Size(147, 21)
        Me.lastdaynopen.TabIndex = 34
        '
        'postpenalty
        '
        Me.postpenalty.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.postpenalty.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.postpenalty.Location = New System.Drawing.Point(216, 184)
        Me.postpenalty.Name = "postpenalty"
        Me.postpenalty.Size = New System.Drawing.Size(75, 48)
        Me.postpenalty.TabIndex = 32
        Me.postpenalty.Text = "Post Penalty"
        Me.postpenalty.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(10, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(128, 15)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "Last Day w/out penalty"
        '
        'billYear
        '
        Me.billYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.billYear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.billYear.FormattingEnabled = True
        Me.billYear.Location = New System.Drawing.Point(221, 15)
        Me.billYear.Name = "billYear"
        Me.billYear.Size = New System.Drawing.Size(70, 24)
        Me.billYear.TabIndex = 27
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(10, 18)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(78, 15)
        Me.Label11.TabIndex = 28
        Me.Label11.Text = "Billing Period"
        '
        'billMonth
        '
        Me.billMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.billMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.billMonth.FormattingEnabled = True
        Me.billMonth.Location = New System.Drawing.Point(94, 15)
        Me.billMonth.Name = "billMonth"
        Me.billMonth.Size = New System.Drawing.Size(121, 24)
        Me.billMonth.TabIndex = 26
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 16)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "Penalty Posting"
        '
        'billingPenalty
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(195, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(902, 280)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Button2)
        Me.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "billingPenalty"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "billingPenalty"
        CType(Me.penaltyList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.dateCovered.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents penaltyList As DataGridView
    Friend WithEvents Button2 As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label11 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents postpenalty As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewCheckBoxColumn
    Friend WithEvents dateCovered As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents dateto As DateTimePicker
    Friend WithEvents datefrom As DateTimePicker
    Friend WithEvents penaltymodify As CheckBox
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents billcount As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents CheckBox1 As CheckBox
    Public WithEvents billYear As ComboBox
    Public WithEvents billMonth As ComboBox
    Public WithEvents prepare As Button
    Public WithEvents lastdaynopen As DateTimePicker
End Class
