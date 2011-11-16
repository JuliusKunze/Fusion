﻿Imports System.IO
Imports System.Drawing

Public Class MainWindow

    Private Const _TitleBase = "Fusion Ray Tracer"

    Private WithEvents _SaveDescriptionDialog As SaveDescriptionDialog
    Private ReadOnly _OpenDescriptionDialog As OpenDescriptionDialog

    Private WithEvents _Compiler As PictureOrVideoCompiler

    Private WithEvents _Picture As RayTracerPicture(Of RadianceSpectrum)
    Private WithEvents _Video As RayTracerVideo(Of RadianceSpectrum)

    Private _ResultBitmap As System.Drawing.Bitmap

    Private ReadOnly _SavePictureDialog As SavePictureDialog
    Private ReadOnly _SaveVideoDialog As SaveFileDialog

    Private WithEvents _PictureRenderer As PictureRenderer
    Private WithEvents _VideoRenderer As VideoRenderer

    Public Sub New(relativisticRayTracerTermContextBuilder As RelativisticRayTracerTermContextBuilder,
                   initialDirectory As DirectoryInfo)
        Me.InitializeComponent()

        _SavePictureDialog = New SavePictureDialog(Owner:=Me, initalDirectory:=initialDirectory)
        _SaveVideoDialog = New SaveVideoDialog(Owner:=Me, initialDirectory:=initialDirectory)
        _SaveDescriptionDialog = New SaveDescriptionDialog(Owner:=Me, initialDirectory:=initialDirectory)
        _OpenDescriptionDialog = New OpenDescriptionDialog(Owner:=Me, initialDirectory:=initialDirectory)

        Me.ClearDescription()

        _Compiler = Me.CreatePictureOrVideoCompiler(relativisticRayTracerTermContextBuilder)
        Me.Mode = CompileMode.Picture

        AddHandler _CompileVideoMenuItem.Checked, AddressOf _CompileVideoMenuItem_Click
        AddHandler _CompilePictureMenuItem.Checked, AddressOf _CompilePictureMenuItem_Click
    End Sub

    Private Function CreatePictureOrVideoCompiler(relativisticRayTracerTermContextBuilder As RelativisticRayTracerTermContextBuilder) As PictureOrVideoCompiler
        Return New PictureOrVideoCompiler(descriptionBox:=_DescriptionBox,
                                          helpPopup:=_HelpPopup,
                                          helpListBox:=_HelpListBox,
                                          helpScrollViewer:=_HelpScrollViewer,
                                          relativisticRayTracerTermContextBuilder:=relativisticRayTracerTermContextBuilder)
    End Function

    Private Sub RenderButton_Click(sender As System.Object, e As RoutedEventArgs) Handles _RenderButton.Click
        _RenderButton.Visibility = Visibility.Collapsed
        _RenderCancelButton.Visibility = Visibility.Visible
        _RenderProgressBar.Visibility = Visibility.Visible
        Me.TaskbarItemInfo.ProgressState = Shell.TaskbarItemProgressState.Normal

        If Me.Mode = CompileMode.Picture Then
            _PictureRenderer = New PictureRenderer(_Picture)
            _PictureRenderer.RunAsync()
        Else
            _VideoRenderer = New VideoRenderer(_Video, outputFile:=Me.VideoOutputFile)
            _VideoRenderer.RunAsync()
        End If
    End Sub

    Private ReadOnly Property VideoOutputFile As FileInfo
        Get
            Return New FileInfo(_VideoFileBox.Text)
        End Get
    End Property

    Private Sub Compiler_Compiled(e As CompilerResultEventArgs(Of RayTracerPicture(Of RadianceSpectrum))) Handles _Compiler.PictureCompiled
        OnCompiled(e, out_result:=_Picture)
        If e.CompilerResult.WasCompilationSuccessful Then

        End If
    End Sub

    Private Sub Compiler_Compiled(e As CompilerResultEventArgs(Of RayTracerVideo(Of RadianceSpectrum))) Handles _Compiler.VideoCompiled
        OnCompiled(e, out_result:=_Video)
    End Sub

    Private Sub OnCompiled(Of TResult)(ByVal e As CompilerResultEventArgs(Of TResult), ByRef out_result As TResult)
        If e.CompilerResult.WasCompilationSuccessful Then
            out_result = e.CompilerResult.Result
            _CompileLabel.Content = "Compilation succeeded."
            _ErrorTextBox.Text = ""
            Me.SetRenderTabItemVisibility(True)
        Else
            _CompileLabel.Content = "Error:"
            _ErrorTextBox.Text = e.CompilerResult.ErrorMessage
            Me.SetRenderTabItemVisibility(False)
        End If
    End Sub

    Private Sub SaveButton_Click(sender As System.Object, e As System.EventArgs) Handles _SavePictureButton.Click
        If _SavePictureDialog.Show Then
            Select Case _SavePictureDialog.File.Extension
                Case ".png"
                    _ResultBitmap.Save(_SavePictureDialog.File.FullName, format:=System.Drawing.Imaging.ImageFormat.Png)
                Case ".bmp"
                    _ResultBitmap.Save(_SavePictureDialog.File.FullName, format:=System.Drawing.Imaging.ImageFormat.Bmp)
                Case Else
                    Throw New ArgumentOutOfRangeException("_SaveFileDialog.FilterIndex")
            End Select
        End If
    End Sub

    Private Sub CalculateNeededTimeButton_Click(sender As System.Object, e As RoutedEventArgs) Handles _EstimateRenderTimeButton.Click
        If Not _CalculateTimeOptionsDialog.DialogResult Then Return

        Dim estimator = Me.GetRenderTimeEstimator
        Dim result = estimator.Run

        _EstimatedRenderTimePerPixelLabel.Content = "Time per Pixel: " & result.TimePerPixel.TotalMilliseconds.ToString & "ms"
        _TotalEstimatedTimeLabel.Content = "Total time: " & result.TotalTime.ToString
    End Sub

    Private Function GetRenderTimeEstimator() As IRenderTimeEstimator
        If _Compiler.Mode = CompileMode.Picture Then
            Return New PictureRenderTimeEstimator(_Picture, options:=_CalculateTimeOptionsDialog.Options)
        Else
            Return New VideoRenderTimeEstimator(video:=_Video, options:=_CalculateTimeOptionsDialog.Options)
        End If
    End Function

    Private _CalculateTimeOptionsDialog As New RenderTimeEstimationOptionsDialog

    Private Sub CalculateNeededTimeOptionsButton_Click(sender As System.Object, e As RoutedEventArgs) Handles _EstimateRenderTimeOptionsButton.Click
        _CalculateTimeOptionsDialog.ShowDialog()
    End Sub

    Private Sub RenderCancelButton_Click(sender As System.Object, e As RoutedEventArgs) Handles _RenderCancelButton.Click
        Me.CancelRenderAsync()
    End Sub

    Private Sub CancelRenderAsync()
        If Me.Mode = CompileMode.Picture Then
            _PictureRenderer.CancelAsync()
        Else
            _VideoRenderer.CancelAsync()
        End If
    End Sub


    Private Sub RenderProgressBar_ValueChanged(sender As Object, e As System.Windows.RoutedPropertyChangedEventArgs(Of Double)) Handles _RenderProgressBar.ValueChanged
        Me.TaskbarItemInfo.ProgressValue = e.NewValue / 100
    End Sub

    Private Sub MainWindow_Deactivated(sender As Object, e As EventArgs) Handles Me.Deactivated
        _Compiler.Unfocus()
    End Sub

    Private Sub MainWindow_KeyDown(sender As Object, e As System.Windows.Input.KeyEventArgs) Handles Me.KeyDown
        If Keyboard.IsKeyDown(Key.LeftCtrl) OrElse Keyboard.IsKeyDown(Key.RightCtrl) Then
            Select Case e.Key
                Case Key.N
                    Me.TryCloseCurrentDescription()

                    e.Handled = True

                Case Key.S
                    Me.TrySaveDescription()

                    e.Handled = True
                Case Key.O
                    Me.ShowOpenDescriptionDialog()

                    e.Handled = True
            End Select
        Else
            Select Case e.Key
                Case Key.F5
                    _Compiler.Compile()

                    e.Handled = True
                Case Key.F4
                    _Compiler.AutoCompile = Not _Compiler.AutoCompile

                    e.Handled = True
            End Select
        End If
    End Sub

    Private Sub TrySaveDescription()
        _SaveDescriptionDialog.TrySave(Me.Description)
    End Sub

    Private Sub SetRenderTabItemVisibility(visible As Boolean)
        _RenderingTabItem.Visibility = If(visible, Visibility.Visible, Visibility.Collapsed)

        If visible Then
            _RenderButton.IsEnabled = If(Me.Mode = CompileMode.Picture,
                                         True,
                                         Me.IsVideoOutputFileValid)
        End If
    End Sub

    Private Property IsSceneDescriptionChangeable As Boolean
        Get
            Return _DescriptionBox.IsEnabled
        End Get
        Set(value As Boolean)
            _DescriptionBox.IsEnabled = value
        End Set
    End Property

    Private Sub CompileMenuItem_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles _CompileMenuItem.Click
        _Compiler.Compile()
    End Sub

    Private Sub NewMenuItem_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles _NewMenuItem.Click
        Me.TryCloseCurrentDescription()
    End Sub

    Private Sub OpenMenuItem_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles _OpenMenuItem.Click
        Me.ShowOpenDescriptionDialog()
    End Sub

    Private Sub SaveMenuItem_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles _SaveMenuItem.Click
        Me.TrySaveDescription()
    End Sub

    Private Sub SaveAsMenuItem_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles _SaveAsMenuItem.Click
        _SaveDescriptionDialog.ShowAndTrySave(description:=Me.Description)
    End Sub

    Private Sub ShowOpenDescriptionDialog()
        If _OpenDescriptionDialog.Show Then
            If Me.TryCloseCurrentDescription() Then
                Dim description = _OpenDescriptionDialog.OpenDescription()

                _SaveDescriptionDialog.File = _OpenDescriptionDialog.File
                Me.Mode = _OpenDescriptionDialog.Mode
                _SaveDescriptionDialog.FileAccepted = True

                Me.Title = _SaveDescriptionDialog.File.Name & " - " & _TitleBase
                _DescriptionBox.Document = TextOnlyDocument.GetDocumentFromText(description)
                _HasUnsavedChanges = False
            End If
        End If
    End Sub

    Private Sub AutoCompileMenuItem_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles _AutoCompileMenuItem.Click
        Me.AutoCompile = _AutoCompileMenuItem.IsChecked
    End Sub

    Private _HasUnsavedChanges As Boolean

    Private Sub ClearDescription()
        _SaveDescriptionDialog.FileAccepted = False

        Me.Title = _TitleBase
        _DescriptionBox.Document = TextOnlyDocument.GetDocumentFromText("")
        _HasUnsavedChanges = False
    End Sub

    Private Function TryCloseCurrentDescription() As Boolean
        If Not _HasUnsavedChanges Then
            Me.ClearDescription()
            Return True
        End If

        Dim result = MessageBox.Show("Do you want to save the current description?", "Save?", MessageBoxButton.YesNoCancel)

        Select Case result
            Case MessageBoxResult.Yes
                Me.TrySaveDescription()
                Me.ClearDescription()
                Return True

            Case MessageBoxResult.No
                Me.ClearDescription()
                Return True

            Case MessageBoxResult.Cancel
                Return False

            Case Else
                Throw New ArgumentOutOfRangeException("result")
        End Select
    End Function

    Private Sub SceneDescriptionTextBox_TextChanged(sender As Object, e As System.Windows.Controls.TextChangedEventArgs) Handles _DescriptionBox.TextChanged
        If Not Me.IsLoaded OrElse _Compiler.ApplyingTextDecorations Then Return

        _HasUnsavedChanges = True
    End Sub

    Private Property AutoCompile As Boolean
        Get
            Return _Compiler.AutoCompile
        End Get
        Set(value As Boolean)
            _Compiler.AutoCompile = value
            _CompileMenuItem.IsEnabled = Not value
            _ErrorTextBox.IsEnabled = value
            _AutoCompileMenuItem.IsChecked = value
        End Set
    End Property

    Private Sub _VideoOutputFileChangeButton_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles _ChangeVideoFileButton.Click
        If Me.IsVideoOutputFileValid Then
            _SaveVideoDialog.File = Me.VideoOutputFile
        End If
        If _SaveVideoDialog.Show Then
            _VideoFileBox.Text = _SaveVideoDialog.File.FullName
        End If
    End Sub

    Private Property Mode As CompileMode
        Get
            Return _Compiler.Mode
        End Get
        Set(value As CompileMode)
            _Compiler.Mode = value

            _CompilePictureMenuItem.IsChecked = (value = CompileMode.Picture)
            _CompileVideoMenuItem.IsChecked = (value = CompileMode.Video)

            _SaveDescriptionDialog.Mode = value

            Dim isVideo = (_Compiler.Mode = CompileMode.Video)

            _VideoFileGrid.Visibility = If(isVideo, Visibility.Visible, Visibility.Collapsed)
            Me.SetRenderButtonEnabled()
        End Set
    End Property

    Private Sub _Renderer_ProgressChanged(e As ComponentModel.ProgressChangedEventArgs) Handles _PictureRenderer.ProgressChanged, _VideoRenderer.ProgressChanged
        _RenderProgressBar.Value = e.ProgressPercentage
    End Sub

    Private Sub _PictureRenderer_Completed(e As RenderResultEventArgs(Of System.Drawing.Bitmap)) Handles _PictureRenderer.Completed
        OnCompleted(e)

        _ResultBitmap = e.Result
        _ResultImage.Source = New SimpleBitmap(_ResultBitmap).ToBitmapSource

        _AverageElapsedTimePerPixelLabel.Content = "Average elapsed time per pixel: " & (e.ElapsedTime.TotalMilliseconds / (_ResultBitmap.Size.Width * _ResultBitmap.Size.Height)).ToString & "ms"

        _ResultPictureTabItem.IsSelected = True
    End Sub

    Private Sub _VideoRenderer_Completed(e As RenderResultEventArgs(Of Object)) Handles _VideoRenderer.Completed
        OnCompleted(e)

        Dim firstPictureSize = _Video.GetFrame(0).PictureSize

        _AverageElapsedTimePerPixelLabel.Content = "Average elapsed time per pixel: " & (e.ElapsedTime.TotalMilliseconds / (_Video.FrameCount * firstPictureSize.Width * firstPictureSize.Height)).ToString & "ms"
    End Sub

    Private Sub OnCompleted(Of TResult)(e As RenderResultEventArgs(Of TResult))
        _RenderProgressBar.Value = 0
        _RenderProgressBar.Visibility = Visibility.Collapsed
        _RenderCancelButton.Visibility = Visibility.Collapsed
        _RenderingTimeCalculationGroupBox.Visibility = Visibility.Collapsed

        _RenderButton.Visibility = Visibility.Visible
        _ResultPictureTabItem.Visibility = Visibility.Visible

        Me.TaskbarItemInfo.ProgressState = Shell.TaskbarItemProgressState.None

        If e.Cancelled Then
            Me.SetRenderTabItemVisibility(True)
            Return
        End If

        _TotalElapsedTimeLabel.Content = "Total elapsed time: " & e.ElapsedTime.ToString
    End Sub

    Private Function IsVideoOutputFileValid() As Boolean
        Return _VideoFileBox.Text <> ""
    End Function

    Private Sub _VideoFileBox_TextChanged(sender As Object, e As System.Windows.Controls.TextChangedEventArgs) Handles _VideoFileBox.TextChanged
        Me.SetRenderButtonEnabled()
    End Sub

    Private Sub SetRenderButtonEnabled()
        If Not Me.Mode = CompileMode.Video Then Return

        _RenderButton.IsEnabled = Me.IsVideoOutputFileValid
    End Sub

    Private ReadOnly Property Description As String
        Get
            Return New TextOnlyDocument(_DescriptionBox.Document).Text
        End Get
    End Property

    Private Sub _SaveDescriptionDialog_Saved() Handles _SaveDescriptionDialog.Saved
        _HasUnsavedChanges = False
        _OpenDescriptionDialog.File = _SaveDescriptionDialog.File
    End Sub

    Private Sub _CompileVideoMenuItem_Click(sender As Object, e As System.Windows.RoutedEventArgs)
        Me.Mode = CompileMode.Video
    End Sub

    Private Sub _CompilePictureMenuItem_Click(sender As Object, e As System.Windows.RoutedEventArgs)
        Me.Mode = CompileMode.Picture
    End Sub

End Class