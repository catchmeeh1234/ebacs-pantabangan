<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Camera
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Camera))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnretake = New System.Windows.Forms.Button()
        Me.btndone = New System.Windows.Forms.Button()
        Me.pbcrop = New System.Windows.Forms.PictureBox()
        Me.btncapture = New System.Windows.Forms.Button()
        Me.pbimage = New System.Windows.Forms.PictureBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPath = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        CType(Me.pbcrop, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbimage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.txtPath)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnretake)
        Me.Panel1.Controls.Add(Me.btndone)
        Me.Panel1.Controls.Add(Me.pbcrop)
        Me.Panel1.Controls.Add(Me.btncapture)
        Me.Panel1.Controls.Add(Me.pbimage)
        Me.Panel1.Location = New System.Drawing.Point(3, 38)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(690, 335)
        Me.Panel1.TabIndex = 0
        '
        'btnretake
        '
        Me.btnretake.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnretake.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnretake.Location = New System.Drawing.Point(4, 290)
        Me.btnretake.Margin = New System.Windows.Forms.Padding(2)
        Me.btnretake.Name = "btnretake"
        Me.btnretake.Size = New System.Drawing.Size(187, 35)
        Me.btnretake.TabIndex = 31
        Me.btnretake.Text = "Retake"
        Me.btnretake.UseVisualStyleBackColor = True
        '
        'btndone
        '
        Me.btndone.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btndone.Font = New System.Drawing.Font("Microsoft YaHei UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndone.Location = New System.Drawing.Point(609, 291)
        Me.btndone.Margin = New System.Windows.Forms.Padding(2)
        Me.btndone.Name = "btndone"
        Me.btndone.Size = New System.Drawing.Size(74, 38)
        Me.btndone.TabIndex = 30
        Me.btndone.Text = "Save"
        Me.btndone.UseVisualStyleBackColor = True
        '
        'pbcrop
        '
        Me.pbcrop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbcrop.Location = New System.Drawing.Point(400, 2)
        Me.pbcrop.Margin = New System.Windows.Forms.Padding(2)
        Me.pbcrop.Name = "pbcrop"
        Me.pbcrop.Size = New System.Drawing.Size(284, 285)
        Me.pbcrop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbcrop.TabIndex = 29
        Me.pbcrop.TabStop = False
        '
        'btncapture
        '
        Me.btncapture.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btncapture.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncapture.Location = New System.Drawing.Point(209, 290)
        Me.btncapture.Margin = New System.Windows.Forms.Padding(2)
        Me.btncapture.Name = "btncapture"
        Me.btncapture.Size = New System.Drawing.Size(187, 35)
        Me.btncapture.TabIndex = 28
        Me.btncapture.Text = "Capture"
        Me.btncapture.UseVisualStyleBackColor = True
        '
        'pbimage
        '
        Me.pbimage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbimage.Location = New System.Drawing.Point(4, 2)
        Me.pbimage.Margin = New System.Windows.Forms.Padding(2)
        Me.pbimage.Name = "pbimage"
        Me.pbimage.Size = New System.Drawing.Size(392, 285)
        Me.pbimage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbimage.TabIndex = 27
        Me.pbimage.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Century Gothic", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(12, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 17)
        Me.Label5.TabIndex = 50
        Me.Label5.Text = "Camera"
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
        Me.Button1.Location = New System.Drawing.Point(621, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(72, 30)
        Me.Button1.TabIndex = 51
        Me.Button1.Text = " Close"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(401, 290)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 16)
        Me.Label1.TabIndex = 52
        Me.Label1.Text = "Pic. Path"
        '
        'txtPath
        '
        Me.txtPath.Location = New System.Drawing.Point(401, 309)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.ReadOnly = True
        Me.txtPath.Size = New System.Drawing.Size(203, 20)
        Me.txtPath.TabIndex = 53
        '
        'Camera
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(195, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(697, 375)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Camera"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Camera"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.pbcrop, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbimage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents btncapture As Button
    Friend WithEvents pbimage As PictureBox
    Friend WithEvents btndone As Button
    Friend WithEvents pbcrop As PictureBox
    Friend WithEvents btnretake As Button
    Friend WithEvents txtPath As TextBox
    Friend WithEvents Label1 As Label
End Class
