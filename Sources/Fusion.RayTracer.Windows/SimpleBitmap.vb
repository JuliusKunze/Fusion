﻿Public Class SimpleBitmap
    Private ReadOnly _Width As Integer
    Private ReadOnly _Height As Integer
    Private ReadOnly _Bytes As Byte()

    Private Shared ReadOnly _PixelFormat As PixelFormat = PixelFormats.Rgb24
    Private ReadOnly _Stride As Integer

    Public Sub New(bitmap As System.Drawing.Bitmap)
        Me.New(bitmap.Width, bitmap.Height)

        For x = 0 To _Width - 1
            For y = 0 To _Height - 1
                Me.SetPixel(x, y, color:=bitmap.GetPixel(x, y))
            Next
        Next
    End Sub

    Public Sub New(size As System.Drawing.Size)
        Me.New(width:=size.Width, height:=size.Height)
    End Sub

    Public Sub New(width As Integer, height As Integer)
        _Width = width
        _Height = height

        _Stride = 3 * width + (width * 3) Mod 4
        ReDim _Bytes(_Stride * _Height)
    End Sub

    Public Sub Clear(color As System.Drawing.Color)
        For x = 0 To _Width - 1
            For y = 0 To _Height - 1
                Me.SetPixel(x:=x, y:=y, color:=color)
            Next
        Next
    End Sub

    Public Function ToBitmapSource() As BitmapSource
        Dim dpiX As Double
        Dim dpiY As Double
        Try
            Application.Current.Windows(0).GetPixelFactor(out_dpiX:=dpiX, out_dpiY:=dpiY)
        Catch ex As NullReferenceException
            dpiX = 120
            dpiY = 120
        End Try

        Return BitmapSource.Create(pixelWidth:=_Width,
                                   pixelHeight:=_Height,
                                   dpiX:=dpiX,
                                   dpiY:=dpiY,
                                   pixelFormat:=_PixelFormat,
                                   palette:=Nothing,
                                   pixels:=_Bytes,
                                   stride:=_Stride)
    End Function

    Public Sub SetPixel(x As Integer, y As Integer, color As System.Drawing.Color)
        _Bytes(y * _Stride + 3 * x) = color.R
        _Bytes(y * _Stride + 3 * x + 1) = color.G
        _Bytes(y * _Stride + 3 * x + 2) = color.B
    End Sub
End Class
