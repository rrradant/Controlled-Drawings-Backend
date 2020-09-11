Imports System.Diagnostics
Imports System.Configuration

Module Module1
    'General Variables
    Public LogFile As String
    Public log As System.IO.StreamWriter
    Public MsgString As String
    Public INIFileName As String
    Public SelFiles As Integer

    'INI Settings
    Public RootPath As String
    Public TempPath As String
    Public ConvertProg As String
    Public ConvArgs As String
    Public ConvExt As String

    'Command Line Arguments
    Public exeParam As String
    Public DelDestDirs As Boolean
    Public cmdCopyFiles As Boolean
    Public cmdConvertFiles As Boolean
    Public noArgs As Boolean

    Public Function FileSelection() As Boolean
        On Error GoTo Handler
        FileSelection = False
        log.WriteLine(Now.ToString & " Beginning file selection.")
        MainForm.StatusBox.AppendText(vbCrLf)
        MainForm.StatusBox.AppendText(Now() & " Beginning file selection." & vbCrLf)
        MainForm.StatusBox.Refresh()

        If DeselectInactive() = False Then
            log.WriteLine("Error ocurred in function DeselectInactive")
            log.WriteLine(MsgString)
            log.WriteLine("Exiting at " & Now.ToString)
            MainForm.StatusBox.AppendText(Now() & " Error ocurred in function DeselectInactive()." & vbCrLf)
            MainForm.StatusBox.Refresh()
            Exit Function
        End If
        If exeParam = "allfiles" Then
            If SelectAllActive() = False Then
                log.WriteLine("Error ocurred in function SelectAllActive")
                log.WriteLine(MsgString)
                log.WriteLine("Exiting at " & Now.ToString)
                MainForm.StatusBox.AppendText(Now() & " Error ocurred in function SelectAllActive()." & vbCrLf)
                MainForm.StatusBox.Refresh()
                Exit Function
            End If
        ElseIf exeParam = "modifiedfiles" Then
            If SelectModified() = False Then
                log.WriteLine("Error ocurred in function SelectModified")
                log.WriteLine(MsgString)
                log.WriteLine("Exiting at " & Now.ToString)
                MainForm.StatusBox.AppendText(Now() & " Error ocurred in function SelectModified()." & vbCrLf)
                MainForm.StatusBox.Refresh()
                Exit Function
            End If
        ElseIf exeParam = "selectedfiles" Then
            MainForm.StatusBox.AppendText(Now & " Using operator selected files." & vbCrLf)
            MainForm.StatusBox.Refresh()
        Else
            log.WriteLine("Error ocurred selecting files to process. Variable exeParam is invalid.")
            log.WriteLine("Exiting at " & Now.ToString)
            MainForm.StatusBox.AppendText(Now() & " Error ocurred selecting files to process." & vbCrLf)
            MainForm.StatusBox.AppendText(vbTab & " Variable exeParam is invalid." & vbCrLf)
            MainForm.StatusBox.Refresh()
            Exit Function
        End If
        If CountSelected() = 0 Then
            log.WriteLine("No files selected.")
            log.WriteLine("Exiting at " & Now.ToString)
            MainForm.StatusBox.AppendText(Now() & " No files selected. Ending." & vbCrLf)
            MainForm.StatusBox.Refresh()
            Exit Function
        End If

        FileSelection = True
        Exit Function

Handler:
        log.WriteLine("Error " & Err.Number.ToString & ": " & Err.Description.ToString)
    End Function


    Public Sub Process1()
        On Error GoTo Handler
        log.WriteLine(Now.ToString & " Beginning Processing.")
        MainForm.StatusBox.AppendText(Now() & " Beginning processing." & vbCrLf)
        MainForm.StatusBox.Refresh()

        If PrepSeedPath() = False Then
            log.WriteLine("Error ocurred in function PrepSeedPath")
            log.WriteLine(MsgString)
            log.WriteLine("Exiting at " & Now.ToString)
            MainForm.StatusBox.AppendText(Now() & " Error ocurred in function PrepSeedPath()." & vbCrLf)
            MainForm.StatusBox.AppendText(vbTab & "Ending processing." & vbCrLf)
            MainForm.StatusBox.Refresh()
            Exit Sub
        End If
        If PrepSkipPath() = False Then
            log.WriteLine("Error ocurred in function PrepSkipPath")
            log.WriteLine(MsgString)
            log.WriteLine("Exiting at " & Now.ToString)
            MainForm.StatusBox.AppendText(Now() & " Error ocurred in function PrepSkipPath()." & vbCrLf)
            MainForm.StatusBox.AppendText(vbTab & "Ending processing." & vbCrLf)
            MainForm.StatusBox.Refresh()
            Exit Sub
        End If
        If PrepBlockedPath() = False Then
            log.WriteLine("Error ocurred in function PrepBlockedPath")
            log.WriteLine(MsgString)
            log.WriteLine("Exiting at " & Now.ToString)
            MainForm.StatusBox.AppendText(Now() & " Error ocurred in function PrepBlockedPath()." & vbCrLf)
            MainForm.StatusBox.AppendText(vbTab & "Ending processing." & vbCrLf)
            MainForm.StatusBox.Refresh()
            Exit Sub
        End If
        If FillPathList() = False Then
            log.WriteLine("Error ocurred in function FillPathList")
            log.WriteLine(MsgString)
            log.WriteLine("Exiting at " & Now.ToString)
            MainForm.StatusBox.AppendText(Now() & " Error ocurred in function FillPathList()." & vbCrLf)
            MainForm.StatusBox.AppendText(vbTab & "Ending processing." & vbCrLf)
            MainForm.StatusBox.Refresh()
            Exit Sub
        End If
        If FillAllFiles() = False Then
            log.WriteLine("Error ocurred in function FillAllFiles")
            log.WriteLine(MsgString)
            log.WriteLine("Exiting at " & Now.ToString)
            MainForm.StatusBox.AppendText(Now() & " Error ocurred in function FillAllFiles()." & vbCrLf)
            MainForm.StatusBox.AppendText(vbTab & "Ending processing." & vbCrLf)
            MainForm.StatusBox.Refresh()
            Exit Sub
        End If
        If PrepareFileInfo() = False Then
            log.WriteLine("Error ocurred in function PrepareFileInfo")
            log.WriteLine(MsgString)
            log.WriteLine("Exiting at " & Now.ToString)
            MainForm.StatusBox.AppendText(Now() & " Error ocurred in function PrepareFileInfo()." & vbCrLf)
            MainForm.StatusBox.AppendText(vbTab & "Ending processing." & vbCrLf)
            MainForm.StatusBox.Refresh()
            Exit Sub
        End If
        If PrepareOutputPaths() = False Then
            log.WriteLine("Error ocurred in function PrepareOutputPaths")
            log.WriteLine(MsgString)
            log.WriteLine("Exiting at " & Now.ToString)
            MainForm.StatusBox.AppendText(Now() & " Error ocurred in function PrepareOutputPaths()." & vbCrLf)
            MainForm.StatusBox.AppendText(vbTab & "Ending processing." & vbCrLf)
            MainForm.StatusBox.Refresh()
            Exit Sub
        End If
        If cmdCopyFiles = True Then
            If CopyFiles() = False Then
                log.WriteLine("Error ocurred in function CopyFiles")
                log.WriteLine(MsgString)
                log.WriteLine("Exiting at " & Now.ToString)
                MainForm.StatusBox.AppendText(Now() & " Error ocurred in function CopyFiles()." & vbCrLf)
                MainForm.StatusBox.AppendText(vbTab & "Ending processing." & vbCrLf)
                MainForm.StatusBox.Refresh()
                Exit Sub
            End If
        Else
            log.WriteLine("Skipped CopyFiles per cmdCopyFiles variable.")
            MainForm.StatusBox.AppendText(Now() & " Skipping CopyFiles()." & vbCrLf)
            MainForm.StatusBox.Refresh()
        End If
        If cmdConvertFiles = True Then
            If ConvertFiles() = False Then
                log.WriteLine("Error ocurred in function ConvertFiles")
                log.WriteLine(MsgString)
                log.WriteLine("Exiting at " & Now.ToString)
                MainForm.StatusBox.AppendText(Now() & " Error ocurred in function ConvertFiles()." & vbCrLf)
                MainForm.StatusBox.AppendText(vbTab & "Ending processing." & vbCrLf)
                MainForm.StatusBox.Refresh()
                Exit Sub
            End If
            If VerifyConverted() = False Then
                log.WriteLine("Error ocurred in function VerifyConverted")
                log.WriteLine(MsgString)
                log.WriteLine("Exiting at " & Now.ToString)
                MainForm.StatusBox.AppendText(Now() & " Error ocurred in function VerifyConverted()." & vbCrLf)
                MainForm.StatusBox.AppendText(vbTab & "Ending processing." & vbCrLf)
                MainForm.StatusBox.Refresh()
                Exit Sub
            End If
        Else
            log.WriteLine("Skipped ConvertFiles per cmdConvertFiles variable.")
            MainForm.StatusBox.AppendText(Now() & " Skipping ConvertFiles()." & vbCrLf)
            MainForm.StatusBox.Refresh()
        End If

        log.WriteLine(Now.ToString & " Processing All done now." & vbCrLf)

        MainForm.StatusBox.AppendText(Now() & " Processing complete." & vbCrLf)
        MainForm.StatusBox.Refresh()
        Beep()
        Exit Sub

Handler:
        log.WriteLine("Error " & Err.Number.ToString & ": " & Err.Description.ToString)
    End Sub

    Public Function PrepINIFile() As Boolean
        'Creates the filename for the .ini file used bythis program.
        'The .ini file needs to be the same name as the executable but with a .ini extension
        'and needs to be located in the same foilder as the executable.
        'This function simply finds the executable path and file name and relaces the .exe with .ini.
        '***
        On Error GoTo Handler
        PrepINIFile = False
        Dim text1 As String
        log.WriteLine(Now & " Starting PrepINIFile()")
        MainForm.StatusBox.AppendText(Now & " Starting PrepINIFile()" & vbCrLf)
        MainForm.StatusBox.Refresh()

        text1 = Trim(Application.ExecutablePath)
        If UCase(Right(text1, 3)) = "EXE" Then
            INIFileName = text1
            INIFileName = Left(INIFileName, Len(INIFileName) - 3) & "ini"
            PrepINIFile = True
        Else
            PrepINIFile = False
        End If
        Exit Function

Handler:
        MsgString = "Error " & Err.Number.ToString & ": " & Err.Description.ToString
        Return False
    End Function

    Public Function GetINIValues() As Boolean
        'Loades the values from the INIFileName file and stuff....
        'more to come.
        '***
        On Error GoTo Handler
        Dim strSectionName, strConn As String
        GetINIValues = False
        Dim CtrlDwgsINI As New IniFile
        log.WriteLine(Now & " Starting GetINIValues()")
        MainForm.StatusBox.AppendText(Now & " Starting GetINIValues()" & vbCrLf)
        MainForm.StatusBox.Refresh()

        CtrlDwgsINI.Load(INIFileName, False)
        strSectionName = "CtrlDwgs Settings"

        'Connection String
        strConn = Trim(CtrlDwgsINI.GetKeyValue(strSectionName, "strConnection"))
        If String.IsNullOrEmpty(strConn) = True Then
            MsgString = "INI Key strConn is null or empty."
            GetINIValues = False
            Exit Function
        Else
            My.Settings("CtrlDwgsConnectionString") = strConn
        End If

        'RootPath
        RootPath = Trim(CtrlDwgsINI.GetKeyValue(strSectionName, "RootPath"))
        If String.IsNullOrEmpty(RootPath) = True Then
            MsgString = "INI Key RootPath is null or empty."
            GetINIValues = False
            Exit Function
        End If

        'TempPath
        TempPath = Trim(CtrlDwgsINI.GetKeyValue(strSectionName, "TempPath"))
        If String.IsNullOrEmpty(TempPath) = True Then
            MsgString = "INI Key TempPath is null or empty."
            GetINIValues = False
            Exit Function
        End If

        'ConvertProgram
        ConvertProg = Trim(CtrlDwgsINI.GetKeyValue(strSectionName, "ConvertProgram"))
        If String.IsNullOrEmpty(ConvertProg) = True Then
            MsgString = "INI Key ConvertProgram is null or empty."
            GetINIValues = False
            Exit Function
        End If

        'ConvertProgramArguments
        ConvArgs = Trim(CtrlDwgsINI.GetKeyValue(strSectionName, "ConvertProgramArguments"))
        If String.IsNullOrEmpty(ConvArgs) = True Then
            MsgString = "INI Key ConvertProgramArguments is null or empty."
            GetINIValues = False
            Exit Function
        End If

        'ConvertExtension=.pdf
        ConvExt = Trim(CtrlDwgsINI.GetKeyValue(strSectionName, "ConvertExtension"))
        If String.IsNullOrEmpty(ConvExt) = True Then
            MsgString = "INI Key ConvertExtension is null or empty."
            GetINIValues = False
            Exit Function
        End If

        GetINIValues = True

        Exit Function

Handler:
        MsgString = "Error " & Err.Number.ToString & ": " & Err.Description.ToString
        Return False
    End Function

    Public Function PrepSeedPath() As Boolean
        'Reviews the data in the SeedPath table for various criteria.
        'These paths are for where the program will look fort the original drawings under sub directories.
        '---At least one row exists.
        '---At least one of the rows is active
        '---At least one active row is valid
        'Changes the row field Valid accordingly.
        '***
        On Error GoTo Handler
        MsgString = ""
        PrepSeedPath = False

        Dim SeedPath_TA As SeedPathDataSetTableAdapters.SeedPathTableAdapter
        Dim SeedPath_DT As SeedPathDataSet.SeedPathDataTable
        Dim dRow As DataRow

        log.WriteLine(Now & " Starting PrepSeedPath()")
        MainForm.StatusBox.AppendText(Now & " Starting PrepSeedPath()" & vbCrLf)
        MainForm.StatusBox.Refresh()

        SeedPath_TA = New SeedPathDataSetTableAdapters.SeedPathTableAdapter
        SeedPath_DT = SeedPath_TA.GetData

        'If no entries exist in SeedPath then exit function
        If SeedPath_DT.Rows.Count = 0 Then
            MsgString = "No path entries exist in dbo.SeedPath."
            PrepSeedPath = False
            Exit Function
        End If

        'Verify that at least one of the entries in SeedPath are valid.
        Dim dirTrue As Boolean = False
        Dim tmpDir, SysCurrDir As String
        For Each dRow In SeedPath_DT.Rows
            dRow(3) = False 'Sets valid to false by default
            If Directory.Exists(dRow(1).ToString) = True And dRow(2) = True Then
                SysCurrDir = Directory.GetCurrentDirectory()    'This saves the current program path
                Directory.SetCurrentDirectory(dRow(1).ToString) 'Sets current directory to dbo.SeedPath row
                tmpDir = Directory.GetCurrentDirectory()    'Retreives the path, filtered and cleaned
                dRow(1) = tmpDir
                dRow(3) = True  'Valid = true
                dirTrue = True
                Directory.SetCurrentDirectory(SysCurrDir)
            End If
            SeedPath_TA.Update(SeedPath_DT)
        Next

        If dirTrue = False Then
            MsgString = "No path entries in dbo.SeedPath are accessible."
            PrepSeedPath = False
            Exit Function
        End If

        PrepSeedPath = True
        Exit Function

Handler:
        MsgString = "Error " & Err.Number.ToString & ": " & Err.Description.ToString
        Return False
    End Function

    Public Function PrepSkipPath() As Boolean
        'Reviews the records listed in the table SkipPath. These records identify paths
        'that are not to be scanned by the program, making sure they are formatted properly and exist.
        '***
        On Error GoTo Handler
        MsgString = ""

        PrepSkipPath = False

        Dim SkipPath_TA As SkipPathDataSetTableAdapters.SkipPathTableAdapter
        Dim SkipPath_DT As SkipPathDataSet.SkipPathDataTable
        Dim dRow As DataRow
        log.WriteLine(Now & " Starting PrepSkipPath()")
        MainForm.StatusBox.AppendText(Now & " Starting PrepSkipPath()" & vbCrLf)
        MainForm.StatusBox.Refresh()

        SkipPath_TA = New SkipPathDataSetTableAdapters.SkipPathTableAdapter
        SkipPath_DT = SkipPath_TA.GetData

        'Makes sure that none of the paths end with a '\'
        Dim tmpDir, SysCurrDir As String
        For Each dRow In SkipPath_DT.Rows
            If Directory.Exists(dRow(1).ToString) = True And dRow(2) = True Then
                SysCurrDir = Directory.GetCurrentDirectory()    'This saves the current program path
                Directory.SetCurrentDirectory(dRow(1).ToString) 'Sets current directory to dbo.SeedPath row
                tmpDir = Directory.GetCurrentDirectory()    'Retreives the path, filtered and cleaned
                dRow(1) = tmpDir
                SkipPath_TA.Update(SkipPath_DT)
                Directory.SetCurrentDirectory(SysCurrDir)
            End If
        Next

        PrepSkipPath = True
        Exit Function

Handler:
        MsgString = "Error " & Err.Number.ToString & ": " & Err.Description.ToString
        Return False
    End Function

    Public Function PrepBlockedPath() As Boolean
        'The table BlockedPath holds paths that are blocked by the system, preventing
        'the program from accessing their information. These paths are generated each
        'time the program is run during the FillPathList function. This function simply
        'empties the table prior to execution of FillPathList.
        '***
        On Error GoTo Handler
        Dim BlockedPath_TA As BlockedPathDataSetTableAdapters.BlockedPathTableAdapter
        Dim BlockedPath_DT As BlockedPathDataSet.BlockedPathDataTable

        log.WriteLine(Now & " Starting PrepBlockedPath()")
        MainForm.StatusBox.AppendText(Now & " Starting PrepBlockedPath()" & vbCrLf)
        MainForm.StatusBox.Refresh()

        PrepBlockedPath = False

        BlockedPath_TA = New BlockedPathDataSetTableAdapters.BlockedPathTableAdapter
        BlockedPath_DT = BlockedPath_TA.GetData

        'This empties dbo.BlockedPath
        BlockedPath_TA.DeleteQuery()

        PrepBlockedPath = True
        Exit Function

Handler:
        MsgString = "Error " & Err.Number.ToString & ": " & Err.Description.ToString
        Return False
    End Function

    Public Function FillPathList() As Boolean
        'Creates a fresh listing of all valid subdirectories under the paths listed in the table SeedPath.
        'The process is as follows:
        '--- Empty the PathList table.
        '--- Adds any active and valid SeedPath table entry that is not in the BlockedPath table to PathList.
        '--- Recursively processes each entry in PathList, listing all subdirectories.
        '--- Blocked subdirectories are entered in the table BlockedPath
        'Trips error if no paths are found.
        '***
        On Error GoTo Handler
        FillPathList = False
        Dim RetSubDirs As String()
        Dim DirList As String()

        log.WriteLine(Now & " Starting FillPathList()")
        MainForm.StatusBox.AppendText(Now & " Starting FillPathList()" & vbCrLf)
        MainForm.StatusBox.Refresh()

        Dim PathList_TA As PathListDataSetTableAdapters.PathListTableAdapter
        Dim PathList_DT As PathListDataSet.PathListDataTable
        PathList_TA = New PathListDataSetTableAdapters.PathListTableAdapter
        PathList_DT = PathList_TA.GetData

        Dim SeedPath_TA As SeedPathDataSetTableAdapters.SeedPathTableAdapter
        Dim SeedPath_DT As SeedPathDataSet.SeedPathDataTable
        SeedPath_TA = New SeedPathDataSetTableAdapters.SeedPathTableAdapter
        SeedPath_DT = SeedPath_TA.GetData

        Dim SkipPath_TA As SkipPathDataSetTableAdapters.SkipPathTableAdapter
        Dim SkipPath_DT As SkipPathDataSet.SkipPathDataTable
        SkipPath_TA = New SkipPathDataSetTableAdapters.SkipPathTableAdapter
        SkipPath_DT = SkipPath_TA.GetData

        Dim BlockedPath_TA As BlockedPathDataSetTableAdapters.BlockedPathTableAdapter
        Dim BlockedPath_DT As BlockedPathDataSet.BlockedPathDataTable
        BlockedPath_TA = New BlockedPathDataSetTableAdapters.BlockedPathTableAdapter
        BlockedPath_DT = BlockedPath_TA.GetData

        'This iterates through dbo.SeedPath, verifying that the path is not in dbo.SkipPath
        'If good, it is added to array DirList
        ReDim DirList(5000)    'Sets array size to 5,000
        Dim n As Integer = 0
        Dim dRow As DataRow
        Dim MatchRows As DataRow()
        Dim Qry As String
        For Each dRow In SeedPath_DT
            If dRow(2) = True And dRow(3) = True Then 'Active and Valid
                Qry = "SkipPath = '" & dRow(1).ToString & "'"
                MatchRows = SkipPath_DT.Select(Qry)
                If MatchRows.GetUpperBound(0) = -1 Then 'Empty, meaning no list in dbo.SkipPath
                    DirList(n) = dRow(1).ToString
                    n = n + 1
                End If
            End If
            Application.DoEvents()
        Next

        'Array DirList is filled with active valid entries from dbo.SeedPath
        'Now begin iterating through DirList, appending new paths as appropriate
        Dim Entry As String
        Dim SubDir As String
        For Each Entry In DirList
            If Not String.IsNullOrEmpty(Entry) Then
                On Error Resume Next
                RetSubDirs = Directory.GetDirectories(Entry)
                Select Case Err.Number
                    Case 0
                        For Each SubDir In RetSubDirs
                            If n = DirList.GetUpperBound(0) Then
                                ReDim Preserve DirList(n + 1000)
                            End If
                            Qry = "SkipPath = '" & SubDir & "' AND Active = true"
                            MatchRows = SkipPath_DT.Select(Qry)
                            If MatchRows.GetUpperBound(0) = -1 Then 'Empty, meaning no list in dbo.SkipPath
                                If Not String.IsNullOrEmpty(SubDir) Then
                                    n = n + 1
                                    DirList(n) = SubDir
                                End If
                            End If
                        Next
                    Case 5
                        BlockedPath_DT.AddBlockedPathRow(Entry.ToString)
                    Case Else
                        MsgBox("Error " & Err.Number.ToString & ": " & Err.Description.ToString)
                End Select
            End If
            Application.DoEvents()
        Next
        On Error GoTo Handler
        BlockedPath_TA.Update(BlockedPath_DT)


        Dim Npb As Integer
        Npb = 0
        MainForm.ProgressBar1.Minimum = 0
        MainForm.ProgressBar1.Maximum = n
        'This section copies all the directories found and stored in DirList into dbo.PathList
        'First it setas all InPlay bits to false
        'Then, it queries the table sbo.PathList for the entry in DirList
        'If the count is 0, it adds the new row
        'If the count is 1, it sets the InPlay bit to true
        'At the end, any rows with InPlay false are deleted.
        PathList_TA.InPlay2False()
        Dim PathCount As Integer
        For Each Entry In DirList
            If Not String.IsNullOrEmpty(Entry) Then
                If My.Computer.FileSystem.DirectoryExists(Entry) = True Then
                    Npb = Npb + 1
                    If Npb < n Then
                        MainForm.ProgressBar1.Value = Npb
                    End If
                    PathCount = PathList_TA.PathCount(Entry)
                    If PathCount = 0 Then
                        PathList_TA.AddNewPath(Entry)
                    ElseIf PathCount = 1 Then
                        PathList_TA.InPlay2True(Entry)
                    End If
                End If
                Application.DoEvents()
            End If
        Next
        PathList_TA.DeleteFalseInPlay()
        'PathList_TA.Update(PathList_DT)

        MainForm.ProgressBar1.Value = MainForm.ProgressBar1.Maximum
        MainForm.ProgressBar1.Update()
        Application.DoEvents()

        If PathList_TA.AllPathCount = 0 Then
            MsgString = "No paths entered into dboPathList."
            FillPathList = False
            Exit Function
        End If

        log.WriteLine(vbTab & FormatNumber(PathList_TA.AllPathCount, 0, TriState.True, , TriState.True) & " directories processed.")
        MainForm.StatusBox.AppendText(vbTab & FormatNumber(PathList_TA.AllPathCount, 0, TriState.True, , TriState.True) & " directories processed." & vbCrLf)

        FillPathList = True
        Exit Function

Handler:
        MsgString = "Error " & Err.Number.ToString & ": " & Err.Description.ToString
        Return False
    End Function

    Public Function FillAllFiles() As Boolean
        'Fills the table AllFiles with each file in every path listed in table PathList that has a
        'file extension listed in table FileExt that is active. If no extensions are active, then
        'error returned.
        'Each found file is verified that it is not System, Hidden, Read Only, or Temporary.
        'The AllFiles table lists only the file name and extensuion, and a reference to the source path via
        'PathListID
        '***
        'On Error GoTo Handler
        FillAllFiles = False
        Dim RetFiles As String()

        log.WriteLine(Now & " Starting FillAllFiles()")
        MainForm.StatusBox.AppendText(Now & " Starting FillAllFiles()" & vbCrLf)
        MainForm.StatusBox.Refresh()

        Dim PathList_TA As PathListDataSetTableAdapters.PathListTableAdapter
        Dim PathList_DT As PathListDataSet.PathListDataTable
        PathList_TA = New PathListDataSetTableAdapters.PathListTableAdapter
        PathList_DT = PathList_TA.GetData

        Dim FileExt_TA As FileExtDataSetTableAdapters.FileExtTableAdapter
        Dim FileExt_DT As FileExtDataSet.FileExtDataTable
        FileExt_TA = New FileExtDataSetTableAdapters.FileExtTableAdapter
        FileExt_DT = FileExt_TA.GetData

        Dim AllFiles_TA As AllFilesDataSetTableAdapters.AllFilesTableAdapter
        Dim AllFiles_DT As AllFilesDataSet.AllFilesDataTable
        AllFiles_TA = New AllFilesDataSetTableAdapters.AllFilesTableAdapter
        AllFiles_DT = AllFiles_TA.GetData

        Dim MatchRows As DataRow()
        Dim Qry As String

        'First make sure that there are active FileExt fields in dbo.FileExt
        Qry = "Active = true"
        MatchRows = FileExt_DT.Select(Qry)
        If MatchRows.GetUpperBound(0) = -1 Then
            MsgString = "Table FileExt contains no information. Extension definitions are required."
            Exit Function
        End If
        'Fancy
        'AllFiles_TA.DeleteQuery()

        'Cleans up FileExtensions
        For Each ext In FileExt_DT
            ext(1) = Trim(ext(1).ToString)
        Next
        FileExt_TA.Update(FileExt_DT)

        Dim N, Nmax As Integer
        N = 0
        Nmax = AllFiles_TA.AllFileCount
        MainForm.ProgressBar1.Minimum = 0
        MainForm.ProgressBar1.Maximum = Nmax

        'This sets the InPlay bit to false on all files in dbo.AllFiles
        AllFiles_TA.InPlay2False()

        'Cycle through for each FileExt that is active
        Dim FileAttribs As FileAttribute
        Dim UseOK As Boolean
        Dim FileCount As Integer
        Dim ShortName As String
        For Each ext In FileExt_DT
            'Make sure it is a real extension that begins with a '.'
            If Not Mid(ext(1).ToString, 1, 1) = "." Or Len(ext(1).ToString) < 2 Then
                log.WriteLine(vbTab & "File extension " & ext(1).ToString & " is invalid.")
                Continue For
            End If
            'Only process if FileExt active is true
            If ext(2) = True Then
                'This cycles through each PathList entry
                For Each pRows In PathList_DT
                    On Error Resume Next
                    RetFiles = Directory.GetFiles(pRows(1).ToString, "*" & ext(1).ToString)
                    'This appends to dbo.AllFiles the PathListID and file name only of the file
                    For Each filename In RetFiles
                        'This makes sure the files are not Read Only, System, Hidden, or Temporary
                        FileAttribs = File.GetAttributes(filename)
                        If pRows(0) = 5764 Then
                            Beep()
                        End If
                        UseOK = True
                        If ((FileAttribs And FileAttributes.ReadOnly) = FileAttributes.ReadOnly) Then
                            UseOK = False
                        ElseIf ((FileAttribs And FileAttributes.System) = FileAttributes.System) Then
                            UseOK = False
                        ElseIf ((FileAttribs And FileAttributes.Hidden) = FileAttributes.Hidden) Then
                            UseOK = False
                        ElseIf ((FileAttribs And FileAttributes.Temporary) = FileAttributes.Temporary) Then
                            UseOK = False
                        End If
                        If UseOK = True Then
                            N = N + 1
                            If N <= Nmax Then
                                MainForm.ProgressBar1.Value = N
                            End If

                            'This is fancy. It queries dbo.AllFiles and sees how many instances of filename it finds.
                            'If none, then the row is added to the table dbo.AllFiles
                            'If 1 or more are found, those records have their InPlay bit set to true
                            ShortName = Path.GetFileName(filename)
                            FileCount = AllFiles_TA.FileCount(ShortName, pRows(0))
                            If FileCount = 0 Then
                                AllFiles_TA.AddNewFile(ShortName, pRows(0))
                            ElseIf FileCount >= 1 Then
                                AllFiles_TA.InPlay2True(ShortName, pRows(0))
                            End If
                        End If
                        Application.DoEvents()
                    Next
                    On Error GoTo 0
                Next
            End If

        Next
        'Deletes any records where the file was not found, with InPlay set to false
        AllFiles_TA.DeleteFalseInPlay()
        'AllFiles_TA.Update(AllFiles_DT)
        MainForm.ProgressBar1.Value = MainForm.ProgressBar1.Maximum
        MainForm.ProgressBar1.Update()
        Application.DoEvents()
        MainForm.Refresh()
        log.WriteLine(vbTab & FormatNumber(AllFiles_TA.AllFileCount, 0, TriState.True, , TriState.True) & " files indexed.")
        MainForm.StatusBox.AppendText(vbTab & FormatNumber(AllFiles_TA.AllFileCount, 0, TriState.True, , TriState.True) & " files indexed." & vbCrLf)

        FillAllFiles = True
        Exit Function

Handler:
        MsgString = "Error " & Err.Number.ToString & ": " & Err.Description.ToString
        Return False
    End Function

    Public Function PrepareFileInfo() As Boolean
        'Reviews the data in table FileInfo, which has the lists of the actual lists of file to be controlled.
        'First looks at each entry in FileInfo and checks to see if the filename exists in AllFiles table.
        '--- If not found, problem. Prevents it from being processed by setting field FileInfo. Problem to true.
        'Looks for the number of times it is found. Enters this in Instances. Not a problem yet.
        'Looks to see if the listed SourcePath for each entry in FileInfo is valid. Not a problem yet.
        'Makes a decision based on provided information.
        'If FileInfo.Sourcepath is valid and Instances=1 then OK.
        'If FileInfo.Sourcepath is valid and Instances > 1 then use FileInfo.Sourcepath info. OK.
        'If FileInfo.Sourcepath is not valid and Instances=1 then use the one AllFiles path. OK.
        'If FileInfo.Sourcepath is not valid and Instances> 1 then not OK.
        'At end of this, all the rows in table FileInfo have been dispositioned and are ready to be processed.
        '***
        On Error GoTo Handler

        'Status Update
        log.WriteLine(Now & " Starting PrepareFileInfo()")
        MainForm.StatusBox.AppendText(Now & " Starting PrepareFileInfo()" & vbCrLf)
        MainForm.StatusBox.Refresh()

        Dim FileInfo_TA As FileInfoDataSetTableAdapters.FileInfoTableAdapter
        Dim FileInfo_DT As FileInfoDataSet.FileInfoDataTable
        FileInfo_TA = New FileInfoDataSetTableAdapters.FileInfoTableAdapter
        FileInfo_DT = FileInfo_TA.GetData

        Dim AllFiles_TA As AllFilesDataSetTableAdapters.AllFilesTableAdapter
        Dim AllFiles_DT As AllFilesDataSet.AllFilesDataTable
        AllFiles_TA = New AllFilesDataSetTableAdapters.AllFilesTableAdapter
        AllFiles_DT = AllFiles_TA.GetData

        Dim View_FilePaths_TA As View_FilePathsDataSetTableAdapters.View_FilePathsTableAdapter
        Dim View_FilePaths_DT As View_FilePathsDataSet.View_FilePathsDataTable
        View_FilePaths_TA = New View_FilePathsDataSetTableAdapters.View_FilePathsTableAdapter
        View_FilePaths_DT = View_FilePaths_TA.GetData

        Dim Qry As String = "Active = true AND Selected = true"
        Dim ActiveRows As DataRow() = FileInfo_DT.Select(Qry)
        Dim CriteriaQry1, CriteriaQry2 As String

        Dim ExistRows As DataRow()
        Dim PathRows As DataRow()
        Dim PathRow As DataRow
        Dim FoundSourcePath As Boolean

        'Form Status Preparation
        Dim N As Integer
        N = 0
        MainForm.ProgressBar1.Minimum = N
        MainForm.ProgressBar1.Maximum = ActiveRows.Count
        MainForm.StatusBox.AppendText(vbTab & "Processing ActiveRows..." & vbCrLf)
        MainForm.Refresh()

        For Each row In ActiveRows
            N = N + 1
            MainForm.ProgressBar1.Value = N
            'MainForm.ProgressBar1.Update()

            'Check to see if dbo.FileInfo.FileName exists in dbo.AllFiles.FileName
            CriteriaQry1 = "FileName = '" & row("FileName").ToString & "'"
            ExistRows = AllFiles_DT.Select(CriteriaQry1)
            If ExistRows.GetUpperBound(0) = -1 Then 'Does not exist
                row("Instances") = 0
                row("Problem") = True
                row("Selected") = False
                row("ProblemText") = "File does not exist in table dbo.AllFiles"
                Continue For
            Else    'Exists
                row("Instances") = ExistRows.GetUpperBound(0) + 1
                row("Problem") = False
                row("ProblemText") = ""
            End If

            '**********************************************
            'Check to see if dbo.FileInfo.SourcePath exists and matches with dbo.AllFiles.FileName
            CriteriaQry2 = "FileName = '" & row("FileName").ToString & "'"
            PathRows = View_FilePaths_DT.Select(CriteriaQry2)

            If Not IsDBNull(row("SourcePath")) Then 'Makes sure dbo.FileInfo.Source is not empty
                FoundSourcePath = False
                For Each pathname In PathRows 'Iterates through looking for a match between SourcePath and PathName
                    If pathname("PathName") = row("SourcePath") Then
                        FoundSourcePath = True
                        Continue For
                    End If
                Next
                If FoundSourcePath = False Then
                    If PathRows.GetUpperBound(0) = 0 Then 'If only one PathName entry, use that by default.
                        PathRow = PathRows(0)
                        row("SourcePath") = PathRow("PathName")
                        row("Problem") = False
                        row("ProblemText") = ""
                    ElseIf PathRows.GetUpperBound(0) = -1 Then 'No PathName entry is found.
                        row("SourcePath") = ""
                        row("Updated") = False
                        row("UpdatedWhen") = DBNull.Value
                        row("Problem") = True
                        row("ProblemText") = "No Path was found for the file."
                        row("Selected") = False
                    Else    'Multiple PathName entries found.
                        row("SourcePath") = ""
                        row("Updated") = False
                        row("UpdatedWhen") = DBNull.Value
                        row("Problem") = True
                        row("ProblemText") = "Multipe instances of file found. No specific instance specified."
                        row("Selected") = False
                    End If
                End If
            Else 'If dbo.FileInfo.SourcePath is empty, checks to see if there is only one entry in View
                If PathRows.GetUpperBound(0) = 0 Then 'If only one PathName entry, use that by default.
                    PathRow = PathRows(0)
                    row("SourcePath") = PathRow("PathName")
                Else 'Multiple found, flag error
                    row("SourcePath") = ""
                    row("Updated") = False
                    row("UpdatedWhen") = DBNull.Value
                    row("Problem") = True
                    row("ProblemText") = "Multipe instances of file found. No specific instance specified."
                    row("Selected") = False
                End If
            End If
            FileInfo_TA.Update(FileInfo_DT)
            Application.DoEvents()
            'Threading.Thread.Sleep(2)
        Next
        'Finish Progress Bar
        Threading.Thread.Sleep(1000)
        log.WriteLine(vbTab & FormatNumber(N, 0, TriState.True, , TriState.True) & " records processed.")
        MainForm.StatusBox.AppendText(vbTab & FormatNumber(N, 0, TriState.True, , TriState.True) & " records processed." & vbCrLf)
        MainForm.StatusBox.AppendText(vbTab & "Updating FileInfo Table Adapter..." & vbCrLf)
        MainForm.ProgressBar1.Value = MainForm.ProgressBar1.Maximum
        MainForm.ProgressBar1.Update()
        Application.DoEvents()

        FileInfo_TA.Update(FileInfo_DT)
        MainForm.Refresh()
        MainForm.StatusBox.AppendText(vbTab & "Table Adapter update complete." & vbCrLf)
        MainForm.ProgressBar1.Value = 0
        MainForm.Refresh()
        Threading.Thread.Sleep(100)

        PrepareFileInfo = True
        Exit Function

Handler:
        MsgString = "Error " & Err.Number.ToString & ": " & Err.Description.ToString
        Return False
    End Function

    Public Function PrepareOutputPaths() As Boolean
        'Perfomes a lot of path creation activities.
        'Using the RootPath and TempPath variables, it perfroms the following activities for each:
        'Checks to see if it is an existing path. If not, tries to create it. If it can't then an error occurs.
        'For each ClassID in the Class table, a subdirectory sturcture is created based on the 
        'path information in tables Group, Cat, and SubCat
        'Then this structure is created under both RootPath and TempPath
        'Finally the table FileInfo gets the current information added for TempFilePath and OutputPath.
        '***
        On Error GoTo Handler
        PrepareOutputPaths = False

        log.WriteLine(Now & " Starting PrepareOutputPaths()")
        MainForm.StatusBox.AppendText(Now & " Starting PrepareOutputPaths()" & vbCrLf)
        MainForm.StatusBox.Refresh()

        Dim FileInfo_TA As FileInfoDataSetTableAdapters.FileInfoTableAdapter
        Dim FileInfo_DT As FileInfoDataSet.FileInfoDataTable
        FileInfo_TA = New FileInfoDataSetTableAdapters.FileInfoTableAdapter
        FileInfo_DT = FileInfo_TA.GetData

        Dim View_OutPutPath_TA As View_OutputPathDataSetTableAdapters.View_OutPutPathTableAdapter
        Dim View_OutPutPath_DT As View_OutputPathDataSet.View_OutPutPathDataTable
        View_OutPutPath_TA = New View_OutputPathDataSetTableAdapters.View_OutPutPathTableAdapter
        View_OutPutPath_DT = View_OutPutPath_TA.GetData

        Dim mkP As Integer
        'Here the root output dirtectory is verified
        'Checks to make sure it is defined.
        'RootPath = "E:\Working\Ctrl Dwgs\"
        RootPath = Trim(RootPath)
        If String.IsNullOrEmpty(RootPath) Then
            MsgString = "INI vairable RootPath is null or undefined."
            Exit Function
        End If
        'Makes sure it ends with a \
        If Not Right(RootPath, 1) = "\" Then
            RootPath = RootPath & "\"
        End If
        'If DelDestDirs argument is true, then delete RootPath ann all sub's and files.
        If DelDestDirs = True Then
            If My.Computer.FileSystem.DirectoryExists(RootPath) = True Then
                log.WriteLine(vbTab & "Deleting final root directories.")
                MainForm.StatusBox.AppendText(vbTab & "Deleting final root directories." & vbCrLf)
                On Error Resume Next
                My.Computer.FileSystem.DeleteDirectory(RootPath, FileIO.DeleteDirectoryOption.DeleteAllContents)
                If Not Err.Number = 0 Then
                    log.WriteLine("Error ocurred deleting directories RootPath in function PrepOutputPaths().")
                    log.WriteLine("Error: " & Err.Number.ToString & ": " & Err.Description.ToString)
                End If
                On Error GoTo Handler
            End If
        End If
        'Checks to see if it exists. If not, create it.
        If My.Computer.FileSystem.DirectoryExists(RootPath) = False Then
            My.Computer.FileSystem.CreateDirectory(RootPath)
        End If

        'Here the temporary root output dirtectory is verified
        'Checks to make sure it is defined.
        'TempPath = "E:\Working\TempCtrlDwgs\"
        TempPath = Trim(TempPath)
        If String.IsNullOrEmpty(TempPath) Then
            MsgString = "INI vairable TempPath is null or undefined."
            Exit Function
        End If
        'Makes sure it ends with a \
        If Not Right(TempPath, 1) = "\" Then
            TempPath = TempPath & "\"
        End If

        'Checks to see if it exists. If it does it is deleted with all subdirectories and files.
        'Then, it is created fresh.
        If My.Computer.FileSystem.DirectoryExists(TempPath) = True Then
            On Error Resume Next
            My.Computer.FileSystem.DeleteDirectory(TempPath, FileIO.DeleteDirectoryOption.DeleteAllContents)
            If Not Err.Number = 0 Then
                log.WriteLine("Error ocurred deleting directories TempPath in function PrepOutputPaths().")
                log.WriteLine("Error: " & Err.Number.ToString & ": " & Err.Description.ToString)
            End If
            On Error GoTo Handler
        End If
        My.Computer.FileSystem.CreateDirectory(TempPath)

        'This sets up a temporary datatable to store built paths.
        Dim ClassPaths As DataTable = New DataTable()
        ClassPaths.Columns.Add()
        ClassPaths.Columns.Add()
        ClassPaths.Columns.Add()

        ClassPaths.Columns(0).ColumnName = "ClassID"
        ClassPaths.Columns(0).DataType = System.Type.GetType("System.Int32")

        ClassPaths.Columns(1).ColumnName = "BuiltDestPath"
        ClassPaths.Columns(1).DataType = System.Type.GetType("System.String")

        ClassPaths.Columns(2).ColumnName = "BuiltTempPath"
        ClassPaths.Columns(2).DataType = System.Type.GetType("System.String")

        MainForm.StatusBox.AppendText(vbTab & "Building Path Structures..." & vbCrLf)
        MainForm.Refresh()

        'This fills ClassPaths with the built paths
        Dim strPath1, strPath2 As String
        For Each row In View_OutPutPath_DT
            'This sets the GroupPath
            strPath1 = row("GroupPath").ToString
            If Not Right(strPath1, 1) = "\" Then
                strPath1 = strPath1 & "\"
            End If
            If Not UCase(row("CatName").ToString) = "NONE" Then
                strPath1 = strPath1 & row("CatPath").ToString
                If Not Right(strPath1, 1) = "\" Then
                    strPath1 = strPath1 & "\"
                End If
                If Not UCase(row("SubCatName").ToString) = "NONE" Then
                    strPath1 = strPath1 & row("SubCatPath").ToString
                    If Not Right(strPath1, 1) = "\" Then
                        strPath1 = strPath1 & "\"
                    End If
                End If
            End If
            strPath2 = TempPath & strPath1
            strPath1 = RootPath & strPath1
            ClassPaths.Rows.Add(row("ClassID"), strPath1, strPath2)
        Next

        'This creates the destination and temp paths in the file structure
        'This fills table dbo.FileInfo with the Path information for each file

        Dim Qry As String = "Active = true AND Selected = true"
        Dim ActiveRows As DataRow() = FileInfo_DT.Select(Qry)

        Dim N As Integer
        N = 0
        MainForm.ProgressBar1.Minimum = N
        MainForm.ProgressBar1.Maximum = ActiveRows.Count
        MainForm.StatusBox.AppendText(vbTab & "Applying Path Information..." & vbCrLf)
        MainForm.Refresh()

        Dim PathRow As DataRow()

        Dim strQry1 As String
        'For Each Item In FileInfo_DT
        For Each Item In ActiveRows
            N = N + 1
            MainForm.ProgressBar1.Value = N
            'MainForm.ProgressBar1.Update()

            If Item("Active") = True And Item("Problem") = False Then  'Only on active problem free files
                strQry1 = "ClassID = " & Item("ClassID")
                PathRow = ClassPaths.Select(strQry1)
                If PathRow.GetUpperBound(0) = -1 Then   'No matching ClassID found
                    Item("Problem") = 1
                    Item("ProblemText") = "No matching ClassID found in routine PrepareOutputPaths."
                ElseIf PathRow.GetUpperBound(0) > 0 Then 'More than one matching ClassID found
                    Item("Problem") = 1
                    Item("ProblemText") = "Multiple matching ClassIDs found in routine PrepareOutputPaths."
                ElseIf PathRow.GetUpperBound(0) = 0 Then 'Just one found
                    Item("OutputLink") = PathRow(0)("BuiltDestPath").ToString '& "Fuck Nuts"
                    Item("TempFilePath") = PathRow(0)("BuiltTempPath").ToString
                End If
            End If
            FileInfo_TA.Update(FileInfo_DT)
            Application.DoEvents()
            'Threading.Thread.Sleep(2)
        Next
        'Finish Progress Bar
        Threading.Thread.Sleep(1000)
        log.WriteLine(vbTab & FormatNumber(ClassPaths.Rows.Count, 0, TriState.True, , TriState.True) & " directories generated.")
        MainForm.StatusBox.AppendText(vbTab & FormatNumber(ClassPaths.Rows.Count, 0, TriState.True, , TriState.True) & " directories generated." & vbCrLf)
        MainForm.StatusBox.AppendText(vbTab & "Updating FileInfo Table Adapter..." & vbCrLf)
        MainForm.ProgressBar1.Value = MainForm.ProgressBar1.Maximum
        MainForm.ProgressBar1.Update()
        Application.DoEvents()

        FileInfo_TA.Update(FileInfo_DT)

        MainForm.ProgressBar1.Value = 0
        MainForm.Refresh()
        Threading.Thread.Sleep(100)

        MainForm.StatusBox.AppendText(vbTab & "Building Directory Structures..." & vbCrLf)
        MainForm.StatusBox.Refresh()

        Dim pathDest, pathTemp As String
        For Each row In ClassPaths.Rows
            pathDest = row(1).ToString
            pathTemp = row(2).ToString
            If My.Computer.FileSystem.DirectoryExists(pathDest) = False Then
                My.Computer.FileSystem.CreateDirectory(pathDest)
            End If
            If My.Computer.FileSystem.DirectoryExists(pathTemp) = False Then
                My.Computer.FileSystem.CreateDirectory(pathTemp)
            End If
        Next

        MainForm.StatusBox.AppendText(vbTab & "Output Path processing complete." & vbCrLf)
        MainForm.StatusBox.Refresh()

        PrepareOutputPaths = True
        Exit Function

Handler:
        MsgString = "Error " & Err.Number.ToString & ": " & Err.Description.ToString
        Return False
    End Function

    Public Function CopyFiles() As Boolean
        On Error GoTo Handler
        CopyFiles = False

        log.WriteLine(Now & " Starting CopyFiles()")
        MainForm.StatusBox.AppendText(Now & " Starting CopyFiles()" & vbCrLf)
        MainForm.StatusBox.Refresh()

        Dim FileInfo_TA As FileInfoDataSetTableAdapters.FileInfoTableAdapter
        Dim FileInfo_DT As FileInfoDataSet.FileInfoDataTable
        FileInfo_TA = New FileInfoDataSetTableAdapters.FileInfoTableAdapter
        FileInfo_DT = FileInfo_TA.GetData

        Dim FileExt_TA As FileExtDataSetTableAdapters.FileExtTableAdapter
        Dim FileExt_DT As FileExtDataSet.FileExtDataTable
        FileExt_TA = New FileExtDataSetTableAdapters.FileExtTableAdapter
        FileExt_DT = FileExt_TA.GetData

        Dim CopiedFiles_TA As CopiedFilesDataSetTableAdapters.CopiedFilesTableAdapter
        CopiedFiles_TA = New CopiedFilesDataSetTableAdapters.CopiedFilesTableAdapter

        Dim Qry As String = "Active = true AND Selected = true"
        Dim ActiveRows As DataRow() = FileInfo_DT.Select(Qry)
        Dim boo2BConverted As Boolean

        'Filter active FileExt fields in dbo.FileExt
        Dim ActiveExt As DataRow()
        Dim strExt As String
        Qry = "Active = true"
        ActiveExt = FileExt_DT.Select(Qry)

        'Form Status Preparation
        Dim N, intC As Integer
        intC = 0
        N = 0
        MainForm.ProgressBar1.Minimum = N
        MainForm.ProgressBar1.Maximum = ActiveRows.Count
        MainForm.StatusBox.AppendText(vbTab & "Copying Files..." & vbCrLf)
        MainForm.Refresh()

        Dim SourceFile, DestFile, LinkName As String
        For Each Item In ActiveRows
            N = N + 1
            MainForm.ProgressBar1.Value = N
            If Item("Active") = True And Item("Problem") = False Then  'Only on selected active problem free files
                'Build source file string
                SourceFile = Item("SourcePath")
                If Not Right(SourceFile, 1) = "\" Then
                    SourceFile = SourceFile & "\"
                End If
                SourceFile = SourceFile & Item("FileName")

                'This looks at the extension of the file and determines if it will be converted
                'or simply copied, adjusting the OutputLink field accordingly.
                'Create OutputLink File Name, changing it to extension in ConvExt if it is to be converted
                boo2BConverted = False
                LinkName = Trim(Item("FileName"))
                strExt = My.Computer.FileSystem.GetFileInfo(LinkName).Extension
                For Each row In ActiveExt
                    If UCase(strExt) = UCase(row("FileExt")) Then
                        boo2BConverted = row("ConvertFile")
                        Exit For
                    End If
                Next

                If boo2BConverted = True Then
                    'Build DestFile string
                    DestFile = Item("TempFilePath")
                    If Not Right(DestFile, 1) = "\" Then
                        DestFile = DestFile & "\"
                    End If
                    DestFile = DestFile & Item("FileName")
                    'DestFile = Left(DestFile, Len(DestFile) - Len(strExt)) & ConvExt
                    'LinkName = Item("FileName")
                    LinkName = Left(LinkName, Len(LinkName) - Len(strExt)) & ConvExt
                Else
                    DestFile = Item("OutputLink") & Trim(Item("FileName"))
                End If

                'Copy file here
                On Error Resume Next
                My.Computer.FileSystem.CopyFile(SourceFile, DestFile, True)
                If Not Err.Number = 0 Then
                    Item("Problem") = True
                    Item("ProblemText") = "Copy Error #" & Err.Number.ToString & ": " & Err.Description.ToString
                    Item("Updated") = False
                    Item("UpdatedWhen") = DateAndTime.Now
                    Item("Selected") = False
                Else
                    Item("FileLastModified") = File.GetLastWriteTime(SourceFile)
                    Item("Updated") = True
                    'Item("Selected") = False
                    Item("UpdatedWhen") = DateAndTime.Now
                    Item("OutputLink") = Item("OutputLink") & LinkName
                    CopiedFiles_TA.InsertQuery(Item("FileName"))
                    intC = intC + 1
                End If
                On Error GoTo Handler
            End If
            FileInfo_TA.Update(FileInfo_DT)
            Application.DoEvents()
        Next
        'Finish Progress Bar
        Threading.Thread.Sleep(1000)
        log.WriteLine(vbTab & FormatNumber(intC, 0, TriState.True, , TriState.True) & " files copied.")
        MainForm.StatusBox.AppendText(vbTab & FormatNumber(intC, 0, TriState.True, , TriState.True) & " files copied." & vbCrLf)
        MainForm.StatusBox.AppendText(vbTab & "Updating Copy status in FileInfo Table Adapter..." & vbCrLf)
        MainForm.ProgressBar1.Value = MainForm.ProgressBar1.Maximum
        MainForm.ProgressBar1.Update()
        Application.DoEvents()

        FileInfo_TA.Update(FileInfo_DT)

        MainForm.StatusBox.AppendText(vbTab & "Table Adapter update complete." & vbCrLf)
        MainForm.ProgressBar1.Value = 0
        MainForm.Refresh()
        Threading.Thread.Sleep(100)

        CopyFiles = True

        Exit Function

Handler:
        MsgString = "Error " & Err.Number.ToString & ": " & Err.Description.ToString
        Return False
    End Function

    Public Function ConvertFiles() As Boolean
        Dim ConvProc As New Process()

        ConvertFiles = False

        log.WriteLine(Now & " Starting ConvertFiles()")
        MainForm.StatusBox.AppendText(Now & " Starting ConvertFiles()" & vbCrLf)
        MainForm.StatusBox.Refresh()

        Try
            ConvProc.StartInfo.UseShellExecute = False
            ConvProc.StartInfo.FileName = ConvertProg
            ConvProc.StartInfo.Arguments = ConvArgs
            ConvProc.StartInfo.CreateNoWindow = False

            ConvProc.Start()
            ConvProc.WaitForExit()
            log.WriteLine(Now & " Conversion program completed successfully.")
            'log.WriteLine(vbTab & "Exitcode: " & ConvProc.ExitCode.ToString)
            MainForm.StatusBox.AppendText(Now & " Conversion program completed successfully." & vbCrLf)
            'MainForm.StatusBox.AppendText(vbTab & "Exitcode: " & ConvProc.ExitCode.ToString & vbCrLf)
            'MsgBox(ConvProc.ExitCode)
            ConvertFiles = True
        Catch ex As Exception
            MsgString = "Exception: " & ex.Message
            MainForm.StatusBox.AppendText(Now & " Conversion program did not complete successfully." & vbCrLf)
            MainForm.StatusBox.AppendText(vbTab & MsgString & vbCrLf)
            MainForm.StatusBox.AppendText(vbTab & "Exitcode: " & ConvProc.ExitCode.ToString & vbCrLf)
            Return False
        End Try
    End Function

    Public Function VerifyConverted() As Boolean
        On Error GoTo Handler
        VerifyConverted = False

        'Status Update
        log.WriteLine(Now & " Starting VerifyConverted()")
        MainForm.StatusBox.AppendText(Now & " Starting VerifyConverted()" & vbCrLf)
        MainForm.StatusBox.Refresh()

        Dim FileInfo_TA As FileInfoDataSetTableAdapters.FileInfoTableAdapter
        Dim FileInfo_DT As FileInfoDataSet.FileInfoDataTable
        FileInfo_TA = New FileInfoDataSetTableAdapters.FileInfoTableAdapter
        FileInfo_DT = FileInfo_TA.GetData

        Dim NotVerified_TA As NotVerifiedFilesDataSetTableAdapters.NotVerifiedFilesTableAdapter
        NotVerified_TA = New NotVerifiedFilesDataSetTableAdapters.NotVerifiedFilesTableAdapter

        Dim Qry As String = "Active = true AND Updated = true AND Selected = true"
        Dim ActiveRows As DataRow() = FileInfo_DT.Select(Qry)

        Dim strFile As String

        Dim N, N2 As Integer
        N = 0
        N2 = 0
        MainForm.ProgressBar1.Minimum = N
        MainForm.ProgressBar1.Maximum = ActiveRows.Count
        MainForm.StatusBox.AppendText(vbTab & "Verifying converted files..." & vbCrLf)
        MainForm.Refresh()

        For Each row In ActiveRows
            N = N + 1
            MainForm.ProgressBar1.Value = N

            'Checks to see if the file exists in the OutputLink location.
            'If not, update dbo.FileInfo accordingly.
            If My.Computer.FileSystem.DirectoryExists(RootPath) = False Then
                My.Computer.FileSystem.CreateDirectory(RootPath)
            End If

            strFile = row("OutputLink")
            If My.Computer.FileSystem.FileExists(strFile) = False Then
                row("Updated") = False
                row("Problem") = True
                row("ProblemText") = "File copied but not converted."
                N2 = N2 + 1
                NotVerified_TA.InsertQuery(Path.GetFileName(strFile))
            End If
            row("Selected") = False
            FileInfo_TA.Update(FileInfo_DT)
            Application.DoEvents()
        Next

        'Finish Progress Bar
        Threading.Thread.Sleep(1000)
        log.WriteLine(vbTab & FormatNumber(N, 0, TriState.True, , TriState.True) & " records processed.")
        log.WriteLine(vbTab & FormatNumber(N2, 0, TriState.True, , TriState.True) & " files not converted.")
        MainForm.StatusBox.AppendText(vbTab & FormatNumber(N, 0, TriState.True, , TriState.True) & " records processed." & vbCrLf)
        MainForm.StatusBox.AppendText(vbTab & FormatNumber(N2, 0, TriState.True, , TriState.True) & " files not converted." & vbCrLf)
        MainForm.StatusBox.AppendText(vbTab & "Updating FileInfo Table Adapter..." & vbCrLf)
        MainForm.ProgressBar1.Value = MainForm.ProgressBar1.Maximum
        MainForm.ProgressBar1.Update()
        Application.DoEvents()

        FileInfo_TA.Update(FileInfo_DT)

        MainForm.StatusBox.AppendText(vbTab & "Verifying converted files complete." & vbCrLf)
        MainForm.ProgressBar1.Value = 0
        MainForm.Refresh()
        Threading.Thread.Sleep(100)

        VerifyConverted = True

        Exit Function

Handler:
        MsgString = "Error " & Err.Number.ToString & ": " & Err.Description.ToString
        Return False
    End Function

    Public Function SelectModified() As Boolean
        On Error GoTo Handler
        SelectModified = False

        'Status Update
        log.WriteLine(Now & " Starting SelectModified()")
        MainForm.StatusBox.AppendText(Now & " Starting SelectModified()" & vbCrLf)
        MainForm.StatusBox.Refresh()

        Dim FileInfo_TA As FileInfoDataSetTableAdapters.FileInfoTableAdapter
        Dim FileInfo_DT As FileInfoDataSet.FileInfoDataTable
        FileInfo_TA = New FileInfoDataSetTableAdapters.FileInfoTableAdapter
        FileInfo_DT = FileInfo_TA.GetData

        Dim Qry As String = "Active = true"
        Dim ActiveRows As DataRow() = FileInfo_DT.Select(Qry)

        Dim strSourceFile As String
        Dim strSourcePath As String
        Dim strFile As String
        Dim dtDBModified, dtFileModified As DateTime
        Dim booSelect As Boolean
        Dim intSpan As TimeSpan

        Dim N As Integer
        N = 0
        MainForm.ProgressBar1.Minimum = N
        MainForm.ProgressBar1.Maximum = ActiveRows.Count
        MainForm.StatusBox.AppendText(vbTab & "Selecting modified files..." & vbCrLf)
        MainForm.Refresh()
        SelFiles = 0
        For Each row In ActiveRows
            N = N + 1
            MainForm.ProgressBar1.Value = N
            'MainForm.ProgressBar1.Update()

            'Gets Source Path and verifies integrity
            If IsDBNull(row("SourcePath")) Or String.IsNullOrEmpty(row("SourcePath").ToString) Then
                row("Selected") = False
                Continue For
            Else
                strSourcePath = row("SourcePath")
            End If
            If Not Right(strSourcePath, 1) = "\" Then
                strSourcePath = strSourcePath & "\"
            End If
            'Gets Filename and verifies it is not null
            strFile = row("FileName")
            If String.IsNullOrEmpty(strFile) Then
                row("Selected") = False
                Continue For
            End If
            strSourceFile = strSourcePath & strFile

            If IO.File.Exists(strSourceFile) Then
                dtFileModified = File.GetLastWriteTime(strSourceFile)
                If IsDBNull(row("FileLastModified")) Then
                    booSelect = True
                Else
                    dtDBModified = row("FileLastModified")
                    intSpan = dtFileModified - dtDBModified
                    If intSpan.TotalSeconds > 1 Then
                        booSelect = True
                    Else
                        booSelect = False
                    End If
                End If
                If booSelect = True Then
                    row("Selected") = True
                    'MainForm.StatusBox.AppendText(row("Filename") & vbCrLf)
                    SelFiles = SelFiles + 1
                Else
                    row("Selected") = False
                End If
            Else
                row("Selected") = False
            End If
            FileInfo_TA.Update(FileInfo_DT)
            Application.DoEvents()
        Next

        'Finish Progress Bar
        Threading.Thread.Sleep(1000)
        log.WriteLine(vbTab & FormatNumber(SelFiles, 0, TriState.True, , TriState.True) & " files selected.")
        MainForm.StatusBox.AppendText(vbTab & FormatNumber(N, 0, TriState.True, , TriState.True) & " records processed." & vbCrLf)
        MainForm.StatusBox.AppendText(vbTab & FormatNumber(SelFiles, 0, TriState.True, , TriState.True) & " files selected." & vbCrLf)
        MainForm.StatusBox.AppendText(vbTab & "Updating FileInfo Table Adapter..." & vbCrLf)
        MainForm.ProgressBar1.Value = MainForm.ProgressBar1.Maximum
        MainForm.ProgressBar1.Update()
        Application.DoEvents()

        FileInfo_TA.Update(FileInfo_DT)

        MainForm.StatusBox.AppendText(vbTab & "Selecting modified files complete." & vbCrLf)
        MainForm.ProgressBar1.Value = 0
        MainForm.Refresh()
        Threading.Thread.Sleep(100)

        SelectModified = True

        Exit Function

Handler:
        MsgString = "Error " & Err.Number.ToString & ": " & Err.Description.ToString
        Return False
    End Function

    Public Function SelectAllActive() As Boolean
        On Error GoTo Handler
        SelectAllActive = False

        'Status Update
        log.WriteLine(Now & " Starting SelectAllActive()")
        MainForm.StatusBox.AppendText(Now & " Starting SelectAllActive()" & vbCrLf)
        MainForm.StatusBox.AppendText(vbTab & "Selecting all Active files..." & vbCrLf)
        MainForm.ProgressBar1.Value = 0
        MainForm.Refresh()

        Dim FileInfo_TA As FileInfoDataSetTableAdapters.FileInfoTableAdapter
        Dim FileInfo_DT As FileInfoDataSet.FileInfoDataTable
        FileInfo_TA = New FileInfoDataSetTableAdapters.FileInfoTableAdapter
        FileInfo_DT = FileInfo_TA.GetData

        Dim Qry As String = "Active = true"
        Dim ActiveRows As DataRow() = FileInfo_DT.Select(Qry)

        Dim N As Integer
        N = ActiveRows.Count

        FileInfo_TA.UpdateQuery_SelectActive()

        'Finish Progress Bar
        Threading.Thread.Sleep(500)
        log.WriteLine(vbTab & FormatNumber(N, 0, TriState.True, , TriState.True) & " files selected.")
        MainForm.StatusBox.AppendText(vbTab & FormatNumber(N, 0, TriState.True, , TriState.True) & " files selected." & vbCrLf)
        Application.DoEvents()

        MainForm.StatusBox.AppendText(vbTab & "Selecting all Active files complete." & vbCrLf)
        MainForm.ProgressBar1.Value = 0
        MainForm.Refresh()
        Threading.Thread.Sleep(100)

        SelectAllActive = True

        Exit Function

Handler:
        MsgString = "Error " & Err.Number.ToString & ": " & Err.Description.ToString
        Return False
    End Function

    Public Function CountSelected() As Integer
        On Error GoTo Handler

        CountSelected = 0

        Dim FileInfo_TA As FileInfoDataSetTableAdapters.FileInfoTableAdapter
        Dim FileInfo_DT As FileInfoDataSet.FileInfoDataTable
        FileInfo_TA = New FileInfoDataSetTableAdapters.FileInfoTableAdapter
        FileInfo_DT = FileInfo_TA.GetData

        Dim Qry As String = "Active = true AND Selected = true"
        Dim ActiveRows As DataRow() = FileInfo_DT.Select(Qry)

        CountSelected = ActiveRows.Count
        log.WriteLine(Now & " Selection summary...")
        log.WriteLine(vbTab & FormatNumber(CountSelected, 0, TriState.True, , TriState.True) & " files selected.")

        Exit Function

Handler:
        MsgString = "Error " & Err.Number.ToString & ": " & Err.Description.ToString
        Return False

    End Function

    Public Function DeselectInactive() As Boolean
        On Error GoTo Handler
        DeselectInactive = False

        Dim FileInfo_TA As FileInfoDataSetTableAdapters.FileInfoTableAdapter
        Dim FileInfo_DT As FileInfoDataSet.FileInfoDataTable
        FileInfo_TA = New FileInfoDataSetTableAdapters.FileInfoTableAdapter
        FileInfo_DT = FileInfo_TA.GetData

        Dim Qry As String = "Active = False AND Selected = True"
        Dim ActiveRows As DataRow() = FileInfo_DT.Select(Qry)

        Dim strSourceFile As String
        Dim strSourcePath As String

        For Each row In ActiveRows
            row("Selected") = False
        Next

        Application.DoEvents()
        FileInfo_TA.Update(FileInfo_DT)

        DeselectInactive = True

        Exit Function

Handler:
        MsgString = "Error " & Err.Number.ToString & ": " & Err.Description.ToString
        Return False
    End Function

    Public Function GetCLArgs() As Boolean
        On Error GoTo Handler
        GetCLArgs = False

        exeParam = ""
        cmdCopyFiles = True
        cmdConvertFiles = True
        DelDestDirs = False
        noArgs = True

        For Each CLarg As String In Environment.GetCommandLineArgs()
            CLarg = Trim(LCase(CLarg))
            Select Case CLarg
                Case "/a"
                    If String.IsNullOrEmpty(exeParam) = True Then
                        exeParam = "allfiles"
                        noArgs = False
                    End If
                Case "/m"
                    If String.IsNullOrEmpty(exeParam) = True Then
                        exeParam = "modifiedfiles"
                        noArgs = False
                    End If
                Case "/s"
                    If String.IsNullOrEmpty(exeParam) = True Then
                        exeParam = "selectedfiles"
                        noArgs = False
                    End If
                Case "/d"
                    DelDestDirs = True
                    noArgs = False
                Case "/nocopy"
                    cmdCopyFiles = False
                    noArgs = False
                Case "/nocnv"
                    cmdConvertFiles = False
                    noArgs = False
            End Select
        Next CLarg
        If String.IsNullOrEmpty(exeParam) = True Then
            exeParam = "selectedfiles"
        End If
        GetCLArgs = True

        Exit Function

Handler:
        MsgString = "Error " & Err.Number.ToString & ": " & Err.Description.ToString
        Return False
    End Function

    Public Sub UpdateMainForm()
        Select Case exeParam
            Case "allfiles"
                MainForm.RadioButton1.Checked = True
            Case "modifiedfiles"
                MainForm.RadioButton2.Checked = True
            Case "selectedfiles"
                MainForm.RadioButton3.Checked = True
        End Select

        If cmdCopyFiles = True Then
            MainForm.cbCopy.Checked = True
        Else
            MainForm.cbCopy.Checked = False
        End If

        If cmdConvertFiles = True Then
            MainForm.cbConv.Checked = True
        Else
            MainForm.cbConv.Checked = False
        End If
    End Sub

End Module
