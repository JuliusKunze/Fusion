﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RyForm
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
        Me._startButton = New System.Windows.Forms.Button()
        Me.buttonPanel = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me._screenSizeRadioButton = New System.Windows.Forms.RadioButton()
        Me._windowSizeRadioButton = New System.Windows.Forms.RadioButton()
        Me._CustomSizeRadioButton = New System.Windows.Forms.RadioButton()
        Me._CustomSizeTextBox = New System.Windows.Forms.TextBox()
        Me.calculateTimePanel = New System.Windows.Forms.GroupBox()
        Me._CalculateTimeOptions = New System.Windows.Forms.Button()
        Me._CalculateTimeButton = New System.Windows.Forms.Button()
        Me.testedPixelCountLabel = New System.Windows.Forms.Label()
        Me.calculatedOverallTimeLabel = New System.Windows.Forms.Label()
        Me.calculatedTimePerPixelLabel = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me._TimePerPixelLabel = New System.Windows.Forms.Label()
        Me._ElapsedTimeLabel = New System.Windows.Forms.Label()
        Me._VideoButton = New System.Windows.Forms.Button()
        Me._SaveButton = New System.Windows.Forms.Button()
        Me._ProgressBar = New System.Windows.Forms.ProgressBar()
        Me._PictureBox = New System.Windows.Forms.PictureBox()
        Me.colorPanel = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me._SaveFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.buttonPanel.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.calculateTimePanel.SuspendLayout()
        CType(Me._PictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'startButton
        '
        Me._StartButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me._StartButton.Location = New System.Drawing.Point(3, 3)
        Me._StartButton.Name = "_StartButton"
        Me._StartButton.Size = New System.Drawing.Size(284, 28)
        Me._StartButton.TabIndex = 0
        Me._StartButton.Text = "Trace tha rays!"
        Me._StartButton.UseVisualStyleBackColor = True
        '
        'buttonPanel
        '
        Me.buttonPanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.buttonPanel.Controls.Add(Me.GroupBox1)
        Me.buttonPanel.Controls.Add(Me.calculateTimePanel)
        Me.buttonPanel.Controls.Add(Me.Label2)
        Me.buttonPanel.Controls.Add(Me._TimePerPixelLabel)
        Me.buttonPanel.Controls.Add(Me._ElapsedTimeLabel)
        Me.buttonPanel.Controls.Add(Me._VideoButton)
        Me.buttonPanel.Controls.Add(Me._SaveButton)
        Me.buttonPanel.Controls.Add(Me._ProgressBar)
        Me.buttonPanel.Controls.Add(Me._StartButton)
        Me.buttonPanel.Location = New System.Drawing.Point(518, 12)
        Me.buttonPanel.Name = "buttonPanel"
        Me.buttonPanel.Size = New System.Drawing.Size(290, 406)
        Me.buttonPanel.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me._ScreenSizeRadioButton)
        Me.GroupBox1.Controls.Add(Me._windowSizeRadioButton)
        Me.GroupBox1.Controls.Add(Me._CustomSizeRadioButton)
        Me.GroupBox1.Controls.Add(Me._CustomSizeTextBox)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 100)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(284, 101)
        Me.GroupBox1.TabIndex = 21
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Size"
        '
        'screenSizeRadioButton
        '
        Me._ScreenSizeRadioButton.AutoSize = True
        Me._ScreenSizeRadioButton.Location = New System.Drawing.Point(6, 76)
        Me._ScreenSizeRadioButton.Name = "_ScreenSizeRadioButton"
        Me._ScreenSizeRadioButton.Size = New System.Drawing.Size(74, 21)
        Me._ScreenSizeRadioButton.TabIndex = 20
        Me._ScreenSizeRadioButton.TabStop = True
        Me._ScreenSizeRadioButton.Text = "Screen"
        Me._ScreenSizeRadioButton.UseVisualStyleBackColor = True
        '
        'windowSizeRadioButton
        '
        Me._windowSizeRadioButton.AutoSize = True
        Me._windowSizeRadioButton.Checked = True
        Me._windowSizeRadioButton.Location = New System.Drawing.Point(6, 49)
        Me._windowSizeRadioButton.Name = "_WindowSizeRadioButton"
        Me._windowSizeRadioButton.Size = New System.Drawing.Size(78, 21)
        Me._windowSizeRadioButton.TabIndex = 8
        Me._windowSizeRadioButton.TabStop = True
        Me._windowSizeRadioButton.Text = "Window"
        Me._windowSizeRadioButton.UseVisualStyleBackColor = True
        '
        'customSizeRadioButton
        '
        Me._CustomSizeRadioButton.AutoSize = True
        Me._CustomSizeRadioButton.Location = New System.Drawing.Point(6, 22)
        Me._CustomSizeRadioButton.Name = "_CustomSizeRadioButton"
        Me._CustomSizeRadioButton.Size = New System.Drawing.Size(76, 21)
        Me._CustomSizeRadioButton.TabIndex = 9
        Me._CustomSizeRadioButton.Text = "Custom"
        Me._CustomSizeRadioButton.UseVisualStyleBackColor = True
        '
        'customSizeTextBox
        '
        Me._CustomSizeTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me._CustomSizeTextBox.Enabled = False
        Me._CustomSizeTextBox.Location = New System.Drawing.Point(88, 21)
        Me._CustomSizeTextBox.Name = "_CustomSizeTextBox"
        Me._CustomSizeTextBox.Size = New System.Drawing.Size(190, 22)
        Me._CustomSizeTextBox.TabIndex = 10
        '
        'calculateTimePanel
        '
        Me.calculateTimePanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.calculateTimePanel.Controls.Add(Me._CalculateTimeOptions)
        Me.calculateTimePanel.Controls.Add(Me._CalculateTimeButton)
        Me.calculateTimePanel.Controls.Add(Me.testedPixelCountLabel)
        Me.calculateTimePanel.Controls.Add(Me.calculatedOverallTimeLabel)
        Me.calculateTimePanel.Controls.Add(Me.calculatedTimePerPixelLabel)
        Me.calculateTimePanel.Location = New System.Drawing.Point(6, 293)
        Me.calculateTimePanel.Name = "calculateTimePanel"
        Me.calculateTimePanel.Size = New System.Drawing.Size(281, 110)
        Me.calculateTimePanel.TabIndex = 19
        Me.calculateTimePanel.TabStop = False
        Me.calculateTimePanel.Text = "Calculate time to draw picture"
        '
        'calculateTimeOptions
        '
        Me._CalculateTimeOptions.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me._CalculateTimeOptions.Location = New System.Drawing.Point(198, 21)
        Me._CalculateTimeOptions.Name = "_CalculateTimeOptions"
        Me._CalculateTimeOptions.Size = New System.Drawing.Size(77, 27)
        Me._CalculateTimeOptions.TabIndex = 19
        Me._CalculateTimeOptions.Text = "Options..."
        Me._CalculateTimeOptions.UseVisualStyleBackColor = True
        '
        'calculateTimeButton
        '
        Me._CalculateTimeButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me._CalculateTimeButton.Location = New System.Drawing.Point(6, 21)
        Me._CalculateTimeButton.Name = "_CalculateTimeButton"
        Me._CalculateTimeButton.Size = New System.Drawing.Size(186, 27)
        Me._CalculateTimeButton.TabIndex = 13
        Me._CalculateTimeButton.Text = "Calculate time"
        Me._CalculateTimeButton.UseVisualStyleBackColor = True
        '
        'testedPixelCountLabel
        '
        Me.testedPixelCountLabel.AutoSize = True
        Me.testedPixelCountLabel.Location = New System.Drawing.Point(6, 85)
        Me.testedPixelCountLabel.Name = "testedPixelCountLabel"
        Me.testedPixelCountLabel.Size = New System.Drawing.Size(105, 17)
        Me.testedPixelCountLabel.TabIndex = 18
        Me.testedPixelCountLabel.Text = "(Tested pixels:)"
        '
        'calculatedOverallTimeLabel
        '
        Me.calculatedOverallTimeLabel.AutoSize = True
        Me.calculatedOverallTimeLabel.Location = New System.Drawing.Point(6, 51)
        Me.calculatedOverallTimeLabel.Name = "calculatedOverallTimeLabel"
        Me.calculatedOverallTimeLabel.Size = New System.Drawing.Size(91, 17)
        Me.calculatedOverallTimeLabel.TabIndex = 14
        Me.calculatedOverallTimeLabel.Text = "Overall time: "
        '
        'calculatedTimePerPixelLabel
        '
        Me.calculatedTimePerPixelLabel.AutoSize = True
        Me.calculatedTimePerPixelLabel.Location = New System.Drawing.Point(6, 68)
        Me.calculatedTimePerPixelLabel.Name = "calculatedTimePerPixelLabel"
        Me.calculatedTimePerPixelLabel.Size = New System.Drawing.Size(100, 17)
        Me.calculatedTimePerPixelLabel.TabIndex = 16
        Me.calculatedTimePerPixelLabel.Text = "Time per pixel:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 239)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 17)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Pixels: "
        '
        'timePerPixelLabel
        '
        Me._TimePerPixelLabel.AutoSize = True
        Me._TimePerPixelLabel.Location = New System.Drawing.Point(3, 273)
        Me._TimePerPixelLabel.Name = "_TimePerPixelLabel"
        Me._TimePerPixelLabel.Size = New System.Drawing.Size(100, 17)
        Me._TimePerPixelLabel.TabIndex = 12
        Me._TimePerPixelLabel.Text = "Time per pixel:"
        '
        'elapsedTimeLabel
        '
        Me._ElapsedTimeLabel.AutoSize = True
        Me._ElapsedTimeLabel.Location = New System.Drawing.Point(3, 256)
        Me._ElapsedTimeLabel.Name = "_ElapsedTimeLabel"
        Me._ElapsedTimeLabel.Size = New System.Drawing.Size(47, 17)
        Me._ElapsedTimeLabel.TabIndex = 11
        Me._ElapsedTimeLabel.Text = "Time: "
        '
        'VideoButton
        '
        Me._VideoButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me._VideoButton.Location = New System.Drawing.Point(3, 207)
        Me._VideoButton.Name = "_VideoButton"
        Me._VideoButton.Size = New System.Drawing.Size(284, 27)
        Me._VideoButton.TabIndex = 6
        Me._VideoButton.Text = "Trace'da'vid"
        Me._VideoButton.UseVisualStyleBackColor = True
        '
        'saveButton
        '
        Me._SaveButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me._SaveButton.Enabled = False
        Me._SaveButton.Location = New System.Drawing.Point(3, 66)
        Me._SaveButton.Name = "_SaveButton"
        Me._SaveButton.Size = New System.Drawing.Size(284, 28)
        Me._SaveButton.TabIndex = 7
        Me._SaveButton.Text = "Save..."
        Me._SaveButton.UseVisualStyleBackColor = True
        '
        'progressBar
        '
        Me._ProgressBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me._ProgressBar.Location = New System.Drawing.Point(3, 37)
        Me._ProgressBar.Name = "_ProgressBar"
        Me._ProgressBar.Size = New System.Drawing.Size(284, 23)
        Me._ProgressBar.TabIndex = 6
        '
        'pictureBox
        '
        Me._PictureBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me._PictureBox.BackColor = System.Drawing.Color.Black
        Me._PictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me._PictureBox.Location = New System.Drawing.Point(12, 12)
        Me._PictureBox.Name = "_PictureBox"
        Me._PictureBox.Size = New System.Drawing.Size(500, 500)
        Me._PictureBox.TabIndex = 3
        Me._PictureBox.TabStop = False
        '
        'colorPanel
        '
        Me.colorPanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.colorPanel.Location = New System.Drawing.Point(631, 424)
        Me.colorPanel.Name = "colorPanel"
        Me.colorPanel.Size = New System.Drawing.Size(177, 34)
        Me.colorPanel.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(518, 424)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 17)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Color Panel"
        '
        'saveFileDialog
        '
        Me._SaveFileDialog.DefaultExt = "bmp"
        Me._SaveFileDialog.FileName = "ray tracing picture"
        '
        'RyForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(820, 524)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.colorPanel)
        Me.Controls.Add(Me._PictureBox)
        Me.Controls.Add(Me.buttonPanel)
        Me.Name = "RyForm"
        Me.Text = "Ry"
        Me.buttonPanel.ResumeLayout(False)
        Me.buttonPanel.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.calculateTimePanel.ResumeLayout(False)
        Me.calculateTimePanel.PerformLayout()
        CType(Me._PictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents _StartButton As System.Windows.Forms.Button
    Private WithEvents buttonPanel As System.Windows.Forms.Panel
    Private WithEvents _PictureBox As System.Windows.Forms.PictureBox
    Private WithEvents colorPanel As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents _ProgressBar As System.Windows.Forms.ProgressBar
    Private WithEvents _SaveButton As System.Windows.Forms.Button
    Private WithEvents _SaveFileDialog As System.Windows.Forms.SaveFileDialog
    Private WithEvents _CustomSizeTextBox As System.Windows.Forms.TextBox
    Private WithEvents _CustomSizeRadioButton As System.Windows.Forms.RadioButton
    Private WithEvents _WindowSizeRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents _VideoButton As System.Windows.Forms.Button
    Friend WithEvents _ElapsedTimeLabel As System.Windows.Forms.Label
    Friend WithEvents _TimePerPixelLabel As System.Windows.Forms.Label
    Friend WithEvents _CalculateTimeButton As System.Windows.Forms.Button
    Friend WithEvents calculatedOverallTimeLabel As System.Windows.Forms.Label
    Friend WithEvents calculateTimePanel As System.Windows.Forms.GroupBox
    Friend WithEvents testedPixelCountLabel As System.Windows.Forms.Label
    Friend WithEvents calculatedTimePerPixelLabel As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents _CalculateTimeOptions As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents _ScreenSizeRadioButton As System.Windows.Forms.RadioButton

End Class
