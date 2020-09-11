<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.StatusBox = New System.Windows.Forms.TextBox()
        Me.ClipBoardButton = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadioButton3 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.cbCopy = New System.Windows.Forms.CheckBox()
        Me.cbConv = New System.Windows.Forms.CheckBox()
        Me.cbExit = New System.Windows.Forms.CheckBox()
        Me.cbDelete = New System.Windows.Forms.CheckBox()
        Me.HelpButton_Args = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(29, 15)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(110, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Begin Processing"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(158, 336)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(400, 20)
        Me.ProgressBar1.TabIndex = 3
        '
        'StatusBox
        '
        Me.StatusBox.Location = New System.Drawing.Point(158, 15)
        Me.StatusBox.Multiline = True
        Me.StatusBox.Name = "StatusBox"
        Me.StatusBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.StatusBox.Size = New System.Drawing.Size(400, 315)
        Me.StatusBox.TabIndex = 4
        Me.StatusBox.TabStop = False
        '
        'ClipBoardButton
        '
        Me.ClipBoardButton.Location = New System.Drawing.Point(29, 44)
        Me.ClipBoardButton.Name = "ClipBoardButton"
        Me.ClipBoardButton.Size = New System.Drawing.Size(110, 23)
        Me.ClipBoardButton.TabIndex = 1
        Me.ClipBoardButton.Text = "Copy to Clipboard"
        Me.ClipBoardButton.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioButton3)
        Me.GroupBox1.Controls.Add(Me.RadioButton2)
        Me.GroupBox1.Controls.Add(Me.RadioButton1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 83)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(130, 97)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "File Selection"
        '
        'RadioButton3
        '
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.Location = New System.Drawing.Point(17, 66)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(91, 17)
        Me.RadioButton3.TabIndex = 2
        Me.RadioButton3.TabStop = True
        Me.RadioButton3.Text = "Selected Files"
        Me.RadioButton3.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(17, 43)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(89, 17)
        Me.RadioButton2.TabIndex = 1
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "Modified Files"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(17, 20)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(60, 17)
        Me.RadioButton1.TabIndex = 0
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "All Files"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'cbCopy
        '
        Me.cbCopy.AutoSize = True
        Me.cbCopy.Location = New System.Drawing.Point(29, 211)
        Me.cbCopy.Name = "cbCopy"
        Me.cbCopy.Size = New System.Drawing.Size(74, 17)
        Me.cbCopy.TabIndex = 4
        Me.cbCopy.Text = "Copy Files"
        Me.cbCopy.UseVisualStyleBackColor = True
        '
        'cbConv
        '
        Me.cbConv.AutoSize = True
        Me.cbConv.Location = New System.Drawing.Point(29, 234)
        Me.cbConv.Name = "cbConv"
        Me.cbConv.Size = New System.Drawing.Size(87, 17)
        Me.cbConv.TabIndex = 5
        Me.cbConv.Text = "Convert Files"
        Me.cbConv.UseVisualStyleBackColor = True
        '
        'cbExit
        '
        Me.cbExit.AutoSize = True
        Me.cbExit.Location = New System.Drawing.Point(29, 257)
        Me.cbExit.Name = "cbExit"
        Me.cbExit.Size = New System.Drawing.Size(74, 17)
        Me.cbExit.TabIndex = 6
        Me.cbExit.Text = "Shutdown"
        Me.cbExit.UseVisualStyleBackColor = True
        '
        'cbDelete
        '
        Me.cbDelete.AutoSize = True
        Me.cbDelete.Location = New System.Drawing.Point(29, 188)
        Me.cbDelete.Name = "cbDelete"
        Me.cbDelete.Size = New System.Drawing.Size(110, 17)
        Me.cbDelete.TabIndex = 3
        Me.cbDelete.Text = "Delete Link Paths"
        Me.cbDelete.UseVisualStyleBackColor = True
        '
        'HelpButton_Args
        '
        Me.HelpButton_Args.Location = New System.Drawing.Point(46, 306)
        Me.HelpButton_Args.Name = "HelpButton_Args"
        Me.HelpButton_Args.Size = New System.Drawing.Size(70, 23)
        Me.HelpButton_Args.TabIndex = 7
        Me.HelpButton_Args.Text = "Help"
        Me.HelpButton_Args.UseVisualStyleBackColor = True
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(590, 368)
        Me.Controls.Add(Me.HelpButton_Args)
        Me.Controls.Add(Me.cbDelete)
        Me.Controls.Add(Me.cbExit)
        Me.Controls.Add(Me.cbConv)
        Me.Controls.Add(Me.cbCopy)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ClipBoardButton)
        Me.Controls.Add(Me.StatusBox)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Button1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.Name = "MainForm"
        Me.Text = "Main Form"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents StatusBox As System.Windows.Forms.TextBox
    Friend WithEvents ClipBoardButton As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton3 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents cbCopy As System.Windows.Forms.CheckBox
    Friend WithEvents cbConv As System.Windows.Forms.CheckBox
    Friend WithEvents cbExit As System.Windows.Forms.CheckBox
    Friend WithEvents cbDelete As System.Windows.Forms.CheckBox
    Friend WithEvents HelpButton_Args As System.Windows.Forms.Button

End Class
