﻿Public Class CalculateTimeOptionsForm

    Public Sub New()
        Me.InitializeComponent()

        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub okButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _OkButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK

        If Me.Mode = FixMode.Time Then
            Try
                Dim time = CDbl(Me._FixTimeTextBox.Text)
            Catch
                MessageBox.Show("Invalid fix time.")
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
            End Try
        Else
            Try
                Dim pixelCount = CDbl(Me._FixPixelCountTextBox.Text)
            Catch
                MessageBox.Show("Invalid fix pixel count.")
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
            End Try
        End If

        Me.Close()
    End Sub

    Public ReadOnly Property FixTestTime As Double
        Get
            Return CDbl(_FixTimeTextBox.Text)
        End Get
    End Property

    Public ReadOnly Property FixTestPixelCount As Integer
        Get
            Return CInt(_FixPixelCountTextBox.Text)
        End Get
    End Property

    Public Enum FixMode
        Time
        PixelCount
    End Enum

    Public ReadOnly Property Mode As FixMode
        Get
            If Me._FixTimeRadioButton.Checked Then
                Return FixMode.Time
            Else
                Return FixMode.PixelCount
            End If
        End Get
    End Property

    Private Sub fixTimeRadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _FixTimeRadioButton.CheckedChanged
        _FixTimeTextBox.Enabled = _FixTimeRadioButton.Checked
        _FixPixelCountTextBox.Enabled = _FixPixelCountRadioButton.Checked
    End Sub
End Class