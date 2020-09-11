Public Class CommandLineHelpForm

    Private Sub CommandLineHelpForm_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        On Error GoTo Handler
        Dim SourceFileName, txtString As String
        Dim HF As System.IO.StreamReader
        SourceFileName = Trim(Application.ExecutablePath)
        SourceFileName = Strings.Left(SourceFileName, Len(SourceFileName) - Len(My.Computer.FileSystem.GetFileInfo(SourceFileName).Extension))
        SourceFileName = SourceFileName & ".txt"
        If My.Computer.FileSystem.FileExists(SourceFileName) Then
            HF = My.Computer.FileSystem.OpenTextFileReader(SourceFileName)
            TextBox1.Text = ""
            Do While Not HF.EndOfStream
                TextBox1.AppendText(HF.ReadLine() & vbCrLf)
            Loop
            HF.Close()
        Else
            txtString = "This box should be displaying the help file for command line arguments. It appears this file is not present. "
            txtString = txtString & "This file must be named " & My.Computer.FileSystem.GetFileInfo(SourceFileName).Name
            txtString = txtString & " and it must be located in the same path as the executable."
            TextBox1.Text = txtString
            TextBox1.AppendText(vbCrLf)
            log.WriteLine(Now & " Missing command line help file.")
        End If
        Label1.Text = Now.ToString
        Exit Sub

Handler:
        log.WriteLine(Now & " Error in opening CommandLineHelpForm.")
        log.WriteLine(vbTab & "Error " & Err.Number.ToString & ": " & Err.Description.ToString)
        Me.Close()
    End Sub
End Class