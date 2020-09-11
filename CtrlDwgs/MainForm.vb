Public Class MainForm

    Private Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        If FileSelection() = True Then
            Call Process1()
        End If
        If Me.cbExit.Checked = True Then
            Me.Close()
        End If
    End Sub

    Private Sub ClipBoardButton_Click(sender As Object, e As System.EventArgs) Handles ClipBoardButton.Click
        Clipboard.SetText(StatusBox.Text)
    End Sub

    Private Sub MainForm_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        log.WriteLine(Now & " Closing program now. Goodbye.")
        log.Close()
    End Sub

    Private Sub MainForm_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'This runs whenever the form is loaded.
        LogFile = "CtrlDwgs_Log.txt"
        log = My.Computer.FileSystem.OpenTextFileWriter(LogFile, True)
        log.AutoFlush = True
        log.WriteLine()
        log.WriteLine(StrDup(60, "-"))
        If PrepINIFile() = False Then
            log.WriteLine("Error ocurred in function PrepINIFile()")
            log.WriteLine(MsgString)
            log.WriteLine("Exiting at " & Now.ToString)
            StatusBox.AppendText(Now() & " Error ocurred in function PrepINIFile()." & vbCrLf)
            StatusBox.Refresh()
            Exit Sub
        End If
        If GetINIValues() = False Then
            log.WriteLine("Error ocurred in function GetINIValues")
            log.WriteLine(MsgString)
            log.WriteLine("Exiting at " & Now.ToString)
            StatusBox.AppendText(Now() & " Error ocurred in function GetINIValues()." & vbCrLf)
            StatusBox.Refresh()
            Exit Sub
        End If
        If GetCLArgs() = False Then
            log.WriteLine("Error ocurred in function GetCLArgs")
            log.WriteLine(MsgString)
            log.WriteLine("Exiting at " & Now.ToString)
            StatusBox.AppendText(Now() & " Error ocurred in function GetCLArgs()." & vbCrLf)
            StatusBox.Refresh()
            Exit Sub
        End If
        Me.Show()
        Call UpdateMainForm()

        'If command line arguments are provided, run automatically.
        If noArgs = False Then
            Me.cbExit.Checked = True
            log.WriteLine(Now.ToString & " Starting from the command line.")
            If FileSelection() = True Then
                Call Process1()
            End If
            Me.Close()
        End If
    End Sub

    Private Sub cbCopy_CheckedChanged(sender As Object, e As System.EventArgs) Handles cbCopy.CheckedChanged
        If cbCopy.Checked = True Then
            cmdCopyFiles = True
        Else
            cmdCopyFiles = False
        End If
    End Sub

    Private Sub cbDelete_CheckedChanged(sender As Object, e As System.EventArgs) Handles cbDelete.CheckedChanged
        If cbDelete.Checked = True Then
            DelDestDirs = True
        Else
            DelDestDirs = False
        End If
    End Sub

    Private Sub cbConv_CheckedChanged(sender As Object, e As System.EventArgs) Handles cbConv.CheckedChanged
        If cbConv.Checked = True Then
            cmdConvertFiles = True
        Else
            cmdConvertFiles = False
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            exeParam = "allfiles"
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As System.EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            exeParam = "modifiedfiles"
        End If
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As System.EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked = True Then
            exeParam = "selectedfiles"
        End If
    End Sub

    Private Sub HelpButton_Args_Click(sender As Object, e As System.EventArgs) Handles HelpButton_Args.Click
        CommandLineHelpForm.Show()
    End Sub
End Class
