Imports System.Windows.Forms
Imports System.Drawing
Imports System.Media
Imports System.Resources.ResXFileRef
Imports NReco.VideoConverter

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Dim inputFile As String
    Dim outputFile As String
    Dim process As Boolean = False
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If process = True Then
            MsgBox("No, I am not frozen. It just need some time...")
        Else
            Dim inputFileDialog As New OpenFileDialog()
            Dim input As String
            With inputFileDialog
                .Title = "Select Video File"
                .Filter = "Video Files (*.mp4, *.avi, *.wmv, *.webm, *.mov, *.h264, *.avi)|*.mp4;*.avi;*.wmv;*.webm;*.mov;*.h264;*.avi"
                If .ShowDialog() = DialogResult.OK Then
                    ' Get the selected video file path
                    Dim inputFilePath As String = .FileName
                    input = .FileName

                    Dim saveFileDialog As New SaveFileDialog()
                    Dim output As String
                    With saveFileDialog
                        .Title = "Save Converted Video"
                        .Filter = "Video Files (*.mp4)|*.mp4"
                        If .ShowDialog() = DialogResult.OK Then
                            ' Get the output file path
                            Dim outputFilePath As String = .FileName
                            output = .FileName
                        End If
                    End With
                    Try
                        'NReco.VideoConverter
                        Dim converter As New NReco.VideoConverter.FFMpegConverter()

                        ' Set conversion parameters
                        Dim targetFileSize As Integer = 300 ' Target file size in MB
                        Dim targetBitrate As Integer = 500 ' Target bitrate in kbps

                        ' Convert the video
                        'converter.ConvertMedia(input, output, "mp4")
                        inputFile = input
                        outputFile = output

                        Dim thr As Threading.Thread = New Threading.Thread(AddressOf startConvert)
                        MsgBox("Convert is starting, I'll let you know when its finished.")
                        thr.Start()
                        process = True
                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.Critical, "Convert fail")
                    End Try
                Else
                    'stop here when no file was selected
                End If
            End With
        End If
    End Sub
    Private Function startConvert(e As NReco.VideoConverter.ConvertProgressEventArgs) As Task
        Dim converter As New NReco.VideoConverter.FFMpegConverter()
        Dim conSettings As New ConvertSettings
        conSettings.VideoFrameRate = 30
        ' Set conversion parameters
        Dim targetFileSize As Integer = 300 ' Target file size in MB
        Dim targetBitrate As Integer = 500 ' Target bitrate in kbps

        ' Convert the video
        converter.ConvertMedia(inputFile, outputFile, "mp4")
        MsgBox("Video is finished!")
        process = False
    End Function
End Class

