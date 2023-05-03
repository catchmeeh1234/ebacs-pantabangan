<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class billingcreatedbills
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(billingcreatedbills))
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.gridDetails = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Createdby = New System.Windows.Forms.ComboBox()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Zones = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.billingMonth = New System.Windows.Forms.ComboBox()
        Me.BillingYear = New System.Windows.Forms.ComboBox()
        Me.Panel1.SuspendLayout()
        CType(Me.gridDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Century Gothic", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(12, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 17)
        Me.Label5.TabIndex = 52
        Me.Label5.Text = "Billing"
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
        Me.Button1.Location = New System.Drawing.Point(476, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(72, 30)
        Me.Button1.TabIndex = 53
        Me.Button1.Text = " Close"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.BillingYear)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.billingMonth)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Zones)
        Me.Panel1.Controls.Add(Me.gridDetails)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Createdby)
        Me.Panel1.Location = New System.Drawing.Point(2, 38)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(546, 380)
        Me.Panel1.TabIndex = 54
        '
        'gridDetails
        '
        Me.gridDetails.AllowUserToAddRows = False
        Me.gridDetails.AllowUserToDeleteRows = False
        Me.gridDetails.BackgroundColor = System.Drawing.Color.White
        Me.gridDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column4, Me.Column2, Me.Column3})
        Me.gridDetails.Location = New System.Drawing.Point(13, 113)
        Me.gridDetails.Name = "gridDetails"
        Me.gridDetails.ReadOnly = True
        Me.gridDetails.RowHeadersVisible = False
        Me.gridDetails.Size = New System.Drawing.Size(519, 254)
        Me.gridDetails.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Created By"
        '
        'Createdby
        '
        Me.Createdby.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Createdby.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Createdby.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Createdby.FormattingEnabled = True
        Me.Createdby.Location = New System.Drawing.Point(93, 43)
        Me.Createdby.Name = "Createdby"
        Me.Createdby.Size = New System.Drawing.Size(212, 24)
        Me.Createdby.TabIndex = 0
        '
        'Column1
        '
        Me.Column1.HeaderText = "Created By"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 120
        '
        'Column4
        '
        Me.Column4.HeaderText = "Date Created"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.HeaderText = "Zone"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 200
        '
        'Column3
        '
        Me.Column3.HeaderText = "Count"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 70
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(52, 76)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 16)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Zone"
        '
        'Zones
        '
        Me.Zones.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Zones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Zones.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Zones.FormattingEnabled = True
        Me.Zones.Location = New System.Drawing.Point(93, 73)
        Me.Zones.Name = "Zones"
        Me.Zones.Size = New System.Drawing.Size(212, 24)
        Me.Zones.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 16)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Billing Period"
        '
        'billingMonth
        '
        Me.billingMonth.BackColor = System.Drawing.Color.WhiteSmoke
        Me.billingMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.billingMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.billingMonth.FormattingEnabled = True
        Me.billingMonth.Location = New System.Drawing.Point(93, 13)
        Me.billingMonth.Name = "billingMonth"
        Me.billingMonth.Size = New System.Drawing.Size(121, 24)
        Me.billingMonth.TabIndex = 5
        '
        'BillingYear
        '
        Me.BillingYear.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BillingYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.BillingYear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BillingYear.FormattingEnabled = True
        Me.BillingYear.Location = New System.Drawing.Point(220, 13)
        Me.BillingYear.Name = "BillingYear"
        Me.BillingYear.Size = New System.Drawing.Size(85, 24)
        Me.BillingYear.TabIndex = 7
        '
        'billingcreatedbills
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.SteelBlue
        Me.ClientSize = New System.Drawing.Size(550, 420)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label5)
        Me.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "billingcreatedbills"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CreatedBills"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.gridDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label5 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents gridDetails As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents Createdby As ComboBox
    Friend WithEvents BillingYear As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents billingMonth As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Zones As ComboBox
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
End Class
