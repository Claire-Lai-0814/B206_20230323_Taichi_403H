Imports System.Collections.Generic
Imports System.Collections.ObjectModel

Module Module_WIN32API
    Dim txtCtrl(10) As TextBox




    ' WINDOWS 32 API �w�q�Ҳ� -----------------------------------------------------------------------Start
    Public Declare Function GetVolumeInformation Lib "kernel32" Alias "GetVolumeInformationA" (ByVal lpRootPathName As String, ByVal lpVolumeNameBuffer As String, ByVal nVolumeNameSize As Integer, ByRef lpVolumeSerialNumber As Integer, ByRef lpMaximumComponentLength As Integer, ByRef lpFileSystemFlags As Integer, ByVal lpFileSystemNameBuffer As String, ByVal nFileSystemNameSize As Integer) As Integer

    ' Ū��ini �ɮפ����ƭȸ��-�۰��নINTEGER�Ʀr, �Y�n�ϥίB�I��,�n�Φr���x�s,�A�ഫ
    Public Declare Function GetPrivateProfileInt Lib "kernel32" Alias "GetPrivateProfileIntA" ( _
                                           ByVal lpApplicationName As String, _
                                           ByVal lpKeyName As String, _
                                           ByVal nDefault As Integer, _
                                           ByVal lpFileName As String) As Integer
    ' Ū��ini �ɮפ����r����
    Public Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" ( _
                                        ByVal lpApplicationName As String, _
                                        ByVal lpKeyName As String, _
                                        ByVal lpDefault As String, _
                                        ByVal lpReturnedString As System.Text.StringBuilder, _
                                        ByVal nSize As Integer, _
                                        ByVal lpFileName As String) As Integer
    ' �g�Jini �ɮפ����r����,�u��H�r�ꫬ�A�g�J
    Public Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" ( _
                                        ByVal lpApplicationName As String, _
                                        ByVal lpKeyName As String, _
                                        ByVal lpString As String, _
                                        ByVal lpFileName As String) As Integer
    ' Ū�� ini �ɮפ����϶��W��
    Public Declare Function GetPrivateProfileSection Lib "kernel32" Alias "GetPrivateProfileSectionA" ( _
                                         ByVal lpAppName As String, _
                                         ByVal lpReturnedString As System.Text.StringBuilder, _
                                         ByVal nSize As Integer, _
                                         ByVal lpFileName As String) As Integer
    ' �g�Jini �ɮפ����϶��W��
    Public Declare Function WritePrivateProfileSection Lib "kernel32" Alias "WritePrivateProfileSectionA" ( _
                                        ByVal lpAppName As String, _
                                        ByVal lpString As String, _
                                        ByVal lpFileName As String) As Integer

    Public Declare Ansi Function FlushPrivateProfileString Lib "kernel32.dll" Alias "WritePrivateProfileStringA" ( _
                                        ByVal lpApplicationName As Integer, _
                                        ByVal lpKeyName As Integer, ByVal lpString As Integer, _
                                        ByVal lpFileName As String) As Integer
    Public Sub Flush(ByVal strFilename As String)
        ' Stores all the cached changes to your INI file
        FlushPrivateProfileString(0, 0, 0, strFilename)
    End Sub

    '����Ƶ{��
    Declare Function GetTickCount Lib "kernel32" () As Integer
    Sub TimeDelay(ByRef t As Integer)
        Dim tt As Integer
        On Error Resume Next
        tt = GetTickCount()
        Do
            System.Windows.Forms.Application.DoEvents()
        Loop Until GetTickCount() - tt >= t
    End Sub
    ' WINDOWS 32 API �w�q�Ҳ� -----------------------------------------------------------------------End 


    Public Declare Function QueryPerformanceCounter Lib "Kernel32" (ByRef X As Long) As Boolean
    Public Declare Function QueryPerformanceFrequency Lib "Kernel32" (ByRef X As Long) As Boolean

    Public Sub PDelay(ByVal dly As Long)
        Dim Ctr1, Ctr2, Freq As Long
        ' �p�ɶ}�l
        QueryPerformanceCounter(Ctr1)
        Do
            QueryPerformanceCounter(Ctr2)
            QueryPerformanceFrequency(Freq)
            If ((Ctr2 - Ctr1) / Freq / 1000) >= dly Then
                Exit Do
            End If
            Application.DoEvents()
        Loop
        ' �p�ɵ���

    End Sub



    'File Copy
    'PURPOSE: COPY FILES
    'PARAMETERS: src: Source File (FullPath)
    'dest: Destination File (FullPath)
    'FailIfDestExists (Optional):
    'Set to true if you don't want to
    'overwrite the destination file if
    'it exists

    'Returns (True if Successful, false otherwise)

    'EXAMPLE:  
    'dim bSuccess as boolean
    'bSuccess = APIFileCopy ("C:\MyFile.txt", "D:\MyFile.txt")
    Private Declare Function CopyFile Lib "kernel32" Alias "CopyFileA" (ByVal lpExistingFileName As String, _
                    ByVal lpNewFileName As String, ByVal bFailIfExists As Long) As Long

    Public Function APIFileCopy(ByVal src As String, ByVal dest As String, Optional ByVal FailIfDestExists As Boolean = True) As Boolean
        Dim lRet As Long
        lRet = CopyFile(src, dest, FailIfDestExists)
        Return (lRet > 0)
    End Function


End Module
