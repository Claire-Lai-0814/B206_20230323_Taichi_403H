Imports System.ComponentModel
Imports System.IO
Imports System.Threading
Imports System.Collections.Generic
Imports System.Windows.Forms
Module Comm_PLC_SERIAL

    '���� M ON/OFF���R�O�CM0-M95
    'DeviceM(DoRobotInIndex).SettingCmd = DEVICE_ON   �]�wM�OON OR OFF�C
    'DeviceM(DoRobotInIndex).StatusUpdated = True     ��R�O�e�X�C

    '���� R Write���R�O�CR1100-R1195
    'PLCRValue(D2AGas1Index).RValue = 100                        �PPLC�������e�ۦP�A���B�z�C
    'PLCRUpData D2AGas1Index                                     R1100-R1113��D/A�]�w�ΡA�|�ۦ氵�B��B�z�C

    'Ū��X���R�O�CX0-X47
    ' PLC_X(DiCDAIndex)  ���r�� "1"or "0"

    'Ū��Y���R�O�CY0-Y71
    'PLC_Y(DoV1Index)    ���r��"1" or "0"

    'Ū��M���R�O�CM0-M95
    'PLC_M(DoV1Index)    ���r��"1" or "0"

    'Ū��S���R�O�CS0-S95
    'PLC_S(Index)    ���r��"1" or "0"

    'Ū��R���R�O�CR1000-R1095
    'PLC_R_READ(A2DGAS1Index)   �PPLC�������e�ۦP�A���B�z�C

    'Ū��R���R�O�CR1100-R1195
    'PLC_R_SetRead(A2DGAS1Index)   �PPLC�������e�ۦP�A���B�z�AŪ���]�w�ȡC

    'Load Recipe to plc R1200 - R1488 PLCRecipe(288)
    'LoadRecipeStatus=TRUE


    Private R_R1100_Read As New Dictionary(Of Int32, Int32) '�إߥ��� table  R_R1100_Read(key,value)
    '����192�ӼȦs���O�_���Q��� R1100_Changed(2)=true �N�� R1102���Q��ʱN�Q���,��粒OK=false
    Public R1100_Changed(256) As Boolean
    Public R1100_ErrorNum(256) As Integer '�����~����
    Public bolonebyone As Boolean = False '�O�_�@�����g�JR  False=�s��g�J
    '--------------------------------------
    Public Const PLCSetMaxCount As Integer = 2000      'PLC �g�J��C�̤j��
    Public Const PLCMSetMaxCount As Integer = 1000     'PLC M �g�J��C�̤j��
    Public Const PLCRSetMaxCount As Integer = 100   'PLC R �g�J��C�̤j��
    Private Const PLCWatchDogTimeSet As Integer = 10       'PLC �g�J��C�̤j��
    Private CommandDelay As Integer = 50                 'PLC���O�ǿ驵��ɶ�( FX2N �ϥ�)

    Private PLCWatchDogTimer As System.Windows.Forms.Timer                   'PLC �q�T�ݪ����p�ɾ�
    'Private PLCComm As New MSCommLib.MSComm              'PLC �q�T����-- �ϥ� MSCOMM32.OCX, �ϥνХ����U��WINDOWS��,�äޤJVB2008
    Public WithEvents PLCComm As New System.IO.Ports.SerialPort   'PLC �q�T����-- �ϥ� ���� SERIALPORTS ����


    Private FirstPLCConnect As Boolean                   '�{���Ĥ@����PLC�s�u�X��
    Private PLCWatchDog As Integer                       'PLC �ݪ����p��        
    Private STX As String ' Chr(2)                       'PLC �e�m�r��
    Private ETX As String ' Chr(3)                       'PLC ��m�r��
    Private Slaveno As String                            'PLC ����
    Private CmdREADSTATUS As String                      'PLC Ū�����A���O
    Private CmdPOWERON As String                         'PLC POWER ON ���O
    Private CmdREADPOINTS As String                      'PLC Ū���I���O(M,X,Y)
    Private CmdWRITEPOINTS As String                     'PLC �g���I���O(M,X,Y)
    Private CmdREADREGISTERS As String                   'PLC Ū�Ȧs�����O(R,D)
    Private CmdWRITEREGISTERS As String                  'PLC �g�Ȧs�����O(R,D)
    Private CmdManyWRITEREGISTERS As String              'PLC �h�I�g�Ȧs�����O(R,D)
    Private rx_str As String                             'PLC �q�T��J�w�İ�
    Private Rx_msgPLC As String                          'PLC �q�T��J�w�İ�
    Private Comm_StatePLC As Integer                     'PLC �q�T�{�����A�����ܼ�
    Public SQHeadPLC As Integer
    Public SQTailPLC As Integer
    Public M_SQHeadPLC As Integer
    Public M_SQTailPLC As Integer
    Public R_SQHeadPLC As Integer
    Public R_SQTailPLC As Integer
    Public Const DEVICE_ON As String = "1"
    Public Const DEVICE_OFF As String = "0"
    Public PLC_X(97) As String                  '�����ܼ� X��J�I���A
    Public PLC_Y(97) As String                  '�����ܼ� Y��X�I���A
    Public PLC_M(97) As String
    Public PLC_XCheckCount(97) As Byte
    Public PLC_R_READ(256) As String             'R1000~R1095 ����l��
    Public PLC_R_SetRead(256) As String          'R1100~R1195 ����l��
    Public PLC_R_OrgSetRead(256) As String       'R1100~R1195 ����l�]�w��
    Public CommLivePLC As Boolean                       'PLC �q�T��
    Public PLCLinkErr_Status As Boolean     'PLC �q�T���`
    Public PLCLinkErrCount As Integer      'PLC �q�T���`����
    'claire..........
    Private arr(100) As Int32
    Private wdog As New Stopwatch
    Private wdog1 As New Stopwatch
    Private swdog As New Stopwatch
    'Public WithEvents RS232_BackWork As System.ComponentModel.BackgroundWorker

    'Public Rs232Thread As New System.Threading.Thread(AddressOf Rs232ThreadWork)

    Public Rs232Thread As System.Threading.Thread

    Public file_old As System.IO.StreamWriter
    Private ii As Integer = 0
    Public bolQuit As Boolean = False
    'claire..............
    'FX Only  -- Start
    Private ACK As String
    Private EndStr As String
    Public bolWriteROK As Boolean = True
    Private strErr As String
    'Private bolManyWrite As Boolean = True
    'Fx Only  --End
    'Public Class R1100
    '    Public R_R1100_Read As New Dictionary(Of Int32, Int32)
    '    Public R1100_ErrorNum(256) As Integer
    'End Class

    Structure PLC_INPUT_X_STRUCTURE
        Public INDEX As Integer
        Public NO As String
        Public NAME As String
    End Structure
    Public PLC_INPUT_X(95) As PLC_INPUT_X_STRUCTURE


    Structure PLC_INPUT_Y_STRUCTURE
        Public INDEX As Integer
        Public NO As String
        Public NAME As String
    End Structure
    Public PLC_INPUT_Y(95) As PLC_INPUT_Y_STRUCTURE

    Structure PLCSetParameter
        Public PLCSetType As Integer
        Public PLCSetCH As Integer
        Public PLCSetValue As String
    End Structure
    Public PLCSet(PLCSetMaxCount) As PLCSetParameter
    Structure PLCMSetParameter
        'Public PLCSetType As Integer
        Public PLCMSetCH As Integer
        Public PLCMSetValue As String
    End Structure
    Public PLCMSet(PLCMSetMaxCount) As PLCMSetParameter

    Structure PLCRSetParameter
        'Public PLCSetType As Integer
        Public PLCRSetCH As Integer
        Public PLCRSetValue As String
    End Structure
    Public PLCRSet(PLCMSetMaxCount) As PLCRSetParameter
    'PLC �]�w BIT �ε��c
    Structure OutDevicesM 'set��   M0-95
        Public Name As String
        Public SettingCmd As String
        Public StatusUpdated As Boolean
    End Structure
    Public DeviceM(95) As OutDevicesM

    'PLC Ū���Ȧs�����c
    Structure PLC_R_Value
        Public Name As String           'AD/DA ���W��
        Public RValue As Double         'AD/DA �ഫ�ᤧ��
        Public Original As Long         'AD/DA ��l��
        Public FullScale As Double      'AD/DA AD ����פ��ƭ�(�ѪR��)
        Public Offset As Double         'AD/DA ��ᤧ���v��
        Public Zero As Double           'AD/DA AD ���s�I�ƭ�
        Public Min As Long              'AD/DA �ഫ�ᤧ���̤p��: �p0 (sccm)
        Public Max As Long              'AD/DA �ഫ�ᤧ���̤j��: �p100 (sccm)
        Public Factor As Double         'AD/DA �����  (�O�d����)
        Public Unit As String           'AD/DA ����� 
    End Structure
    '�]�w��    R1100-R1163
    Public PLCRValue(256) As PLC_R_Value
    'Ū���� R1000-R1063
    Public PLCRReadValue(256) As PLC_R_Value

    '�ˬdY��X�O�_�� "1"
    Public Function Check_PLC_Y(ByVal index As Integer) As Boolean
        If index < 0 Then Exit Function
        If PLC_Y(index) = "1" Then
            Return True
        Else
            Return False
        End If
    End Function

    '�ˬdX��J�O�_�� "1"
    Public Function Check_PLC_X(ByVal index As Integer) As Boolean
        If index < 0 Then Exit Function
        If PLC_X(index) = "1" Then
            Return True
        Else
            Return False
        End If
    End Function

    '�ˬdM�O�_�� "1"
    Public Function Check_PLC_M(ByVal index As Integer) As Boolean
        If index < 0 Then Exit Function
        If PLC_M(index) = "1" Then
            Return True
        Else
            Return False
        End If
    End Function

    'Ū�� R1000~R1095 R1200~R1263
    Public Function Get_PLC_R1000(ByVal index As Integer) As Integer
        If index < 0 Then Exit Function
        Return Val(PLC_R_READ(index))
    End Function

    'Ū�� R1100~R1195 R1300~R1395
    Public Function Get_PLC_R1100(ByVal index As Integer) As Integer
        If index < 0 Then Exit Function
        Return Val(PLC_R_SetRead(index))
    End Function

    'PLC �]�w�Ȧs�� R01100,  SetPLCRValue(�Ȧs���s��(�q0�}�l), �Ʀr�r��,10�i��),���ഫ
    Public Sub SetPLCRValue(ByVal index As Integer, ByVal value As String)
        If Get_PLC_R1100(index) = value Then Exit Sub
        If (PLCRValue(index).Max > 0) Then
            PLCRValue(index).RValue = PLCRValue(index).FullScale * (Val(value) / (PLCRValue(index).Max - PLCRValue(index).Min))
        Else
            PLCRValue(index).RValue = Val(value)
        End If
        PLCRUpData(index)
    End Sub
    'PLC �]�w�Ȧs�� R01100,  SetPLCRValue(�Ȧs���s��(�q0�}�l), �Ʀr�r��,10�i��),�L�ഫ
    Public Sub Write_PLC_R1100(ByVal index As Integer, ByVal value As Integer)
        'If Get_PLC_R1100(index) = value Then Exit Sub
        PLCRValue(index).RValue = CDbl(value)
        addOrUpdate(R_R1100_Read, index, value) '��sR_Read(,) dictionary
        PLCRUpData(index)
    End Sub
    '��sR_Read(,) dictionary....>�إ߹q������ Table
    Private Sub addOrUpdate(ByVal dic As Dictionary(Of Integer, Integer), ByVal key As Integer, ByVal newValue As Integer)
        Dim val As Integer

        If dic.TryGetValue(key, val) Then '�o�Ȧs���̭�����
            dic(key) = newValue
            R1100_Changed(key) = True
            'Debug.Print("index=" + key.ToString + ",�ק�=" + newValue.ToString())
        Else
            dic.Add(key, newValue)
            R1100_Changed(key) = False
            'R1100_Changed(key) = True
            'bolWriteROK = True
            'Debug.Print("index=" + key.ToString + ",�s�W=" + newValue.ToString())
        End If
    End Sub
    Private Sub ReadOriginalR1100()
        Dim i As Integer
        For i = 0 To 256
            'If i = 96 Then
            '    'Debug.Print("96")
            'End If
            'If PLC_R_SetRead(i) <> 0 Then
            addOrUpdate(R_R1100_Read, i, PLC_R_SetRead(i))
            'End If
        Next
    End Sub


    Private Sub R1100_Value_Compare(ByVal dic As Dictionary(Of Integer, Integer), ByVal istart As Integer, ByVal iEnd As Integer)
        Dim val As Integer
        Dim i As Integer

        For i = istart To iEnd
            If (R1100_Changed(i)) Then '��update
                'Debug.Print("Change_=" + i.ToString)
                If dic.TryGetValue(i, val) Then '�����g�J���� ����table val
                    If PLC_R_SetRead(i) <> val Then 'Ū�XPLC��<>����table val

                        'Debug.Print("Compare_Errx=" + i.ToString)
                        R1100_ErrorNum(i) = R1100_ErrorNum(i) + 1 '��1 2....
                        If R1100_ErrorNum(i) > 2 Then '>��2
                            Write_PLC_R1100(i, val) '�A�g�@�M
                            Debug.Print("Write again =" + i.ToString)
                            PLCAlarm_Log("R_index=" + i.ToString + " Compare Err")
                            R1100_ErrorNum(i) = 0
                        End If

                    Else
                        R1100_Changed(i) = False '��粒OK
                        'Debug.Print("index=" + i.ToString + " Compare OK")
                        bolWriteROK = False
                        R1100_ErrorNum(i) = 0

                    End If
                End If
            End If
        Next

    End Sub

    'PLC �]�w�Ȧs�� R01100,  SetPLCRValue(�Ȧs���s��(�q0�}�l), �Ʀr�r��,10�i��),�۩w�ഫ
    Public Sub Write_PLC_R1100(ByVal index As Integer, ByVal value As Integer, ByVal max As Double, ByVal min As Double, ByVal fullscale As Double)
        'If Get_PLC_R1100(index) = value Then Exit Sub
        If max - min <> 0 Then
            PLCRValue(index).RValue = fullscale * (value / (max - min))
            PLCRUpData(index)
        End If
    End Sub
    'PLC Ū���Ȧs�� R01100,  SetPLCRValue(�Ȧs���s��(�q0�}�l), �Ʀr�r��,10�i��),�۩w�ഫ
    Public Function Read_PLC_R1100(ByVal index As Integer, ByVal value As Integer, ByVal max As Double, ByVal min As Double, ByVal fullscale As Double) As Double
        If max - min <> 0 Then
            Return (Val(PLC_R_SetRead(index)) / fullscale) * (max - min)
        End If
    End Function
    'PLC ���o�Ȧs�� R01100,  GetPLCRValue(�Ȧs���s��(�q0�}�l), �Ʀr�r��,10�i��),�g�L�ഫ
    Public Function GetPLCRValue(ByVal index As Integer) As Double
        Dim aa As Double
        Dim value As String
        value = PLC_R_SetRead(index)
        If (PLCRValue(index).Max > 0) Then
            aa = (Val(value) * (PLCRValue(index).Max - PLCRValue(index).Min)) / PLCRValue(index).FullScale
        Else
            Return Val(value)
        End If
        Return aa
    End Function
    'PLC ���o�Ȧs�� R01000,  GetPLCRValue(�Ȧs���s��(�q0�}�l), �Ʀr�r��,10�i��),�g�L�ഫ
    Public Function GetPLCReadValue(ByVal index As Integer) As Double
        Dim aa As Double
        Dim value As String
        value = PLC_R_READ(index)
        If (PLCRReadValue(index).Max > 0) Then
            aa = (Val(value) * (PLCRReadValue(index).Max - PLCRReadValue(index).Min)) / PLCRReadValue(index).FullScale
        Else
            Return Val(value)
        End If
        Return aa
    End Function

    'PLC �]�wM��X,M0~M95,  Set_MBit(M�I�s��(�q0�}�l), "1" OR "0")
    Public Sub Set_MBit(ByVal index As Integer, ByVal on_off As String)
        DeviceM(index).SettingCmd = on_off
        DeviceM(index).StatusUpdated = True
    End Sub

    ' PLC BIT ��Ƨ��ܰO��
    Public Y_Changed(95) As Boolean
    Public X_Changed(95) As Boolean

    'For PLC type Read
    'PLC ��Ƴ]�w
    Public PLC_TYPE As String           'PLC ����
    Public PLC_SETTING As String        'PLC �q�T�]�w
    Public PLC_COMPORT As Integer       ' PLC �q�T��s��
    Public PLC_NUMBER As Integer        ' PLC �������X, 1=FATEK, 2=FX3U
    Public TOTALX As Integer        ' PLC �������X, 1=FATEK, 2=FX3U
    Public TOTALY As Integer        ' PLC �������X, 1=FATEK, 2=FX3U

    'PLC Ū����l�Ƴ]�w���
    Public Sub PLC_INIT(ByVal sfile As String)
        'Read PLC Setup INI file
        Dim fileNumber As Integer
        '�]�w INI �ɦW, ��m������ɦb�P�@�h�ؿ��U
        'PLC_INI_FILE_NAME = CurDir() + "\" + "PLC_SETUP.INI"
        If Not (System.IO.File.Exists(sfile)) Then
            ' �Y�ɮפ��s�b�h�إ߷s��, �æs�J�Ѽg�d��
            WriteProgData("PLC_TYPE", "PLC_MODEL", "FX3U", sfile)
            WriteProgData("PLC_TYPE", "PLC_COMPORT", "1", sfile)
            WriteProgData("PLC_TYPE", "PLC_SETTING", "9600,e,7,1", sfile)
        End If
        PLC_TYPE = ReadProgData("PLC_TYPE", "PLC_MODEL", "FATEK", sfile)
        PLC_COMPORT = Val(ReadProgData("PLC_TYPE", "PLC_COMPORT", "1", sfile))
        PLC_SETTING = ReadProgData("PLC_TYPE", "PLC_SETTING", "9600,e,7,1", sfile)
        TOTALX = Val(ReadProgData("PLC_TYPE", "TOTALX", "11", sfile))
        TOTALY = Val(ReadProgData("PLC_TYPE", "TOTALY", "8", sfile))
        If PLC_TYPE.ToUpper = "FATEK" Then
            PLC_NUMBER = 1          ' FB PLC
        ElseIf PLC_TYPE.ToUpper = "FX3U" Then
            PLC_NUMBER = 2          ' FX3U
        ElseIf PLC_TYPE.ToUpper = "FX2N" Then
            PLC_NUMBER = 3
        End If
    End Sub

    ' �q�T�ѼƳ]�m
    Public Sub PLCSetting(ByVal sfile As String)
        On Error Resume Next
        PLC_INIT(sfile)                                          'PLC Ū���]�w�ɮת�l��
        PLCWatchDogTimer = New System.Windows.Forms.Timer
        PLCWatchDogTimer.Interval = 950
        AddHandler PLCWatchDogTimer.Tick, AddressOf WatchDog_Tick
        PLCWatchDogTimer.Enabled = True
        PLCWatchDog = PLCWatchDogTimeSet

        PLCSetting_FB()        ' �ϥ� FATECK �]�w

        ' PLC MODEL SELECT
    End Sub
    Public Sub PLCSetting_FB()
        'Dim i As Integer
        'Dim comport As Integer
        'Dim setting As String
        'Dim comset() As String
        ''Add by Libra at 97/06/16 Start ------------------------------------------------------------------------------ 
        'On Error Resume Next
        ''Add by Libra at 97/06/16 End --------------------------------------------------------------------------------

        '' for Fatek PLC
        'Slaveno = "01"
        'CmdREADSTATUS = "40"
        'CmdPOWERON = "41"
        'CmdREADPOINTS = "44"
        'CmdWRITEPOINTS = "45"
        'CmdREADREGISTERS = "46"
        'CmdWRITEREGISTERS = "47"

        'PLCComm.PortName = "COM" & PLC_COMPORT.ToString
        'comset = Split(PLC_SETTING, ",")
        'If comset.Length >= 4 Then
        '    If Val(comset(0)) > 0 Then
        '        PLCComm.BaudRate = Val(comset(0))
        '    End If
        '    Select Case comset(1)
        '        Case "O"
        '            PLCComm.Parity = IO.Ports.Parity.Odd
        '        Case "o"
        '            PLCComm.Parity = IO.Ports.Parity.Odd
        '        Case "E"
        '            PLCComm.Parity = IO.Ports.Parity.Even
        '        Case "e"
        '            PLCComm.Parity = IO.Ports.Parity.Even
        '        Case Else
        '            PLCComm.Parity = IO.Ports.Parity.None
        '    End Select
        '    If Val(comset(2)) > 0 Then
        '        PLCComm.DataBits = Val(comset(2))
        '    End If

        '    If Val(comset(2)) > 0 Then
        '        PLCComm.DataBits = Val(comset(2))
        '    End If
        'End If
        'PLCComm.RtsEnable = False
        'PLCComm.DtrEnable = False
        'PLCComm.Encoding = System.Text.Encoding.Default
        'PLCComm.Handshake = IO.Ports.Handshake.None
        'AddHandler PLCComm.DataReceived, AddressOf PLC_ONComm_FB
        'If Not PLCComm.IsOpen Then PLCComm.Open()

        ''PLCComm.CommPort = PLC_COMPORT
        ''PLCComm.Settings = PLC_SETTING
        ''PLCComm.RThreshold = 1
        ''PLCComm.RTSEnable = True
        ''PLCComm.DTREnable = False
        ''PLCComm.Handshaking = MSCommLib.HandshakeConstants.comNone
        ''AddHandler PLCComm.OnComm, AddressOf PLC_ONComm_FB
        ''If PLCComm.PortOpen = False Then PLCComm.PortOpen = True

        'STX = Chr(2)
        'ETX = Chr(3)
        ''PLCIOADDefine()


        ''PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdPOWERON, "1", ETX)
        'PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADSTATUS, "", ETX))
        'Comm_StatePLC = 0

        ' for Fatek PLC
        Slaveno = "01"
        CmdREADSTATUS = "40"
        CmdPOWERON = "41"
        CmdREADPOINTS = "44"
        CmdWRITEPOINTS = "45"
        CmdREADREGISTERS = "46"
        CmdWRITEREGISTERS = "47"
        CmdManyWRITEREGISTERS = "49"
        STX = Chr(2)
        ETX = Chr(3)

        Dim comset() As String
        Dim ii As IO.Ports.Parity
        comset = Split(PLC_SETTING, ",")
        PLCComm = New System.IO.Ports.SerialPort
        PLCComm.PortName = "COM" + PLC_COMPORT.ToString
        PLCComm.BaudRate = CInt(comset(0))
        If comset(1).ToUpper = "N" Then ii = IO.Ports.Parity.None
        If comset(1).ToUpper = "E" Then ii = IO.Ports.Parity.Even
        If comset(1).ToUpper = "O" Then ii = IO.Ports.Parity.Odd
        PLCComm.Parity = ii
        PLCComm.DataBits = CInt(comset(2))
        PLCComm.StopBits = CInt(comset(3))
        PLCComm.RtsEnable = False
        PLCComm.DtrEnable = False
        PLCComm.ReceivedBytesThreshold = 1
        'PLCComm.WriteTimeout = 2000
        PLCComm.Handshake = IO.Ports.Handshake.None
        PLCComm.Encoding = System.Text.Encoding.ASCII
        'AddHandler PLCComm.DataReceived, AddressOf PLC_ONComm_FB


        Try
            PLCComm.Open()
            PLCComm.DiscardInBuffer()
            PLCComm.DiscardOutBuffer()
            'If PLCComm.IsOpen Then PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdPOWERON, "1", ETX))
            If PLCComm.IsOpen Then PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADSTATUS, "1", ETX))
            Comm_StatePLC = 1
            'Debug.WriteLine(" PLCComm.Open()")
        Catch ex As TimeoutException
            Debug.WriteLine("FB Serial Write Timeout")
        End Try

        'AddHandler PLCComm.DataReceived, AddressOf FBPLCCommDataReceived
        'file_old = My.Computer.FileSystem.OpenTextFileWriter("D:\arr1000_old.txt", True)
        'If (File.Exists("D:\arr1000_old.txt")) Then
        '    File.Delete("D:\arr1000_old.txt")
        'End If
        'RS232_BackWork.WorkerSupportsCancellation = False
        'RS232_BackWork.RunWorkerAsync()
        Rs232Thread = New Threading.Thread(New Threading.ThreadStart(AddressOf Rs232ThreadWork))
        'Rs232Thread = New Threading.Thread(AddressOf Rs232ThreadWork) With {
        '    .Priority = Threading.ThreadPriority.Normal
        '}
        Rs232Thread.IsBackground = True
        Rs232Thread.Start()
    End Sub
    Public Sub RestartCommPLC_FB()
        Dim rx_str As String

        Try
            Rx_msgPLC = ""
            'PLCWatchDog = PLCWatchDogTimeSet
            ' for Fatek PLC
            If PLCComm.IsOpen Then
                'PLCComm.Close()
                'System.Threading.Thread.Sleep(100)
                'PLCComm.Open()
                'PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60X0000", ETX)) 'Ū��X0-X47
                'Comm_StatePLC = 2

                'PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADSTATUS, "", ETX))
                'Comm_StatePLC = 1
                If bolQuit = True Then
                    Exit Sub
                End If
                If Rs232Thread.IsAlive Then
                    Exit Sub
                End If
                Rs232Thread = New Thread(AddressOf Rs232ThreadWork)
                'Debug.Print("thread restart PLCWatchDog=" + PLCWatchDog.ToString)
                PLCAlarm_Log("thread_Retry")

                Rs232Thread.Start()
                Thread.Sleep(100)
                PLCComm.DiscardInBuffer()
                Comm_StatePLC = 1
                PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADSTATUS, "", ETX))

                'System.Threading.Thread.Sleep(100)
            Else
                PLCComm.Open()
                Rs232Thread = New Thread(AddressOf Rs232ThreadWork)
                'Debug.Print("thread restart + port open")
                PLCAlarm_Log("thread_Retry + port open")
                Thread.Sleep(100)
                PLCComm.DiscardInBuffer()
                Comm_StatePLC = 1
                PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADSTATUS, "", ETX))
                Rs232Thread.Start()
            End If
        Catch ex As TimeoutException
            Debug.WriteLine("FB Serial Write Timeout")
        End Try
    End Sub

    '���� PLC�q�T
    Public Sub RestartCommPLC()
        Call RestartCommPLC_FB()        ' �ϥ� FATECK �]�w
    End Sub

    '�ݪ����p�ɾ�
    Private Sub WatchDog_Tick()
        PLCWatchDog = PLCWatchDog - 1
        'Debug.Print("PLCWatchDog=" + PLCWatchDog.ToString)
        If PLCWatchDog = 7 Or PLCWatchDog = 3 Then
            RestartCommPLC()
            'Debug.Print("PLCWatchDog=RestartCommPLC" + PLCWatchDog.ToString)
        End If
        If PLCWatchDog = 0 Then
            CommLivePLC = False
            RestartCommPLC()
            Comm_StatePLC = 0
            PLCWatchDog = PLCWatchDogTimeSet
            PLCLinkErrCount = PLCLinkErrCount + 1
            If PLCLinkErrCount > 2 Then
                PLCLinkErr_Status = True
            End If
        Else
            If CommLivePLC Then
                PLCLinkErr_Status = False
                PLCLinkErrCount = 0
            End If
        End If

        'claire test
        'If CommLivePLC Then

        '    If Output(DoVentIndex).Status Then
        '        Output(DoVentIndex).Status = False
        '        Write_PLC_R1100(DATopP1Index, 999)
        '    Else
        '        Output(DoVentIndex).Status = True
        '        Output(DoRVIndex).Status = False
        '        Output(DoVentIndex).Status = True
        '        Write_PLC_R1100(DATopP1Index, 100)

        '    End If
        'End If

    End Sub

    Public Function PLCSendCmd(ByVal StartX As String, ByVal SlaveNumber As String, ByVal PlcCmd As String, ByVal sData As String, ByVal EndX As String) As String
        Dim sstr As String = ""
        'If PlcCmd = "49" Then
        '    Debug.Print("49")
        'End If
        sstr = PLCSendCmd_FB(StartX, SlaveNumber, PlcCmd, sData, EndX)  '�ϥ� FATECK �]�w
        Return sstr
    End Function
    Public Function PLCSendCmd_FB(ByVal StartX As String, ByVal SlaveNumber As String, ByVal PlcCmd As String, ByVal sData As String, ByVal EndX As String) As String
        Dim i As Integer
        Dim str As String
        Dim iCheckSum As Integer
        Dim sCheckSum As String
        ' For FBPLC PLC Checksum
        str = StartX & SlaveNumber & PlcCmd & sData
        For i = 1 To Len(str)
            iCheckSum = iCheckSum + Asc(Mid(str, i, 1))
        Next i
        'sCheckSum = Right(Hex(iCheckSum), 2)
        sCheckSum = FillZero(iCheckSum And &HFF, 2)
        PLCSendCmd_FB = str + sCheckSum + EndX
    End Function

    Private Sub WriteMToPLCFB()
        Dim cmdstr As String
        If bolonebyone = True Then
            cmdstr = "01M" & Format(PLCSet(SQTailPLC).PLCSetCH, "0000") + PLCSet(SQTailPLC).PLCSetValue
            PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdWRITEPOINTS, cmdstr, ETX))

        Else
            cmdstr = "01M" & Format(PLCMSet(M_SQTailPLC).PLCMSetCH, "0000") + PLCMSet(M_SQTailPLC).PLCMSetValue
            PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdWRITEPOINTS, cmdstr, ETX))
        End If
        'System.Threading.Thread.Sleep(5)
    End Sub
    Private Sub WriteDToPLCFB()
        Dim cmdstr As String
        Dim m As String
        m = FillZero(Val(PLCSet(SQTailPLC).PLCSetValue), 4)
        'TimeDelay(CommandDelay)
        If PLCSet(SQTailPLC).PLCSetCH < 96 Then
            cmdstr = "01R" & Format(PLCSet(SQTailPLC).PLCSetCH + 1100, "00000") + m
        Else
            cmdstr = "01R" & Format(PLCSet(SQTailPLC).PLCSetCH - 96 + 1300, "00000") + m
        End If
        'm = FillZero(Val(PLCRSet(R_SQTailPLC).PLCRSetValue), 4)
        ''m = FillZero(Val(R_R1100_Read(R_SQTailPLC).PLCRSetValue), 4)
        ''TimeDelay(CommandDelay)
        'If PLCRSet(R_SQTailPLC).PLCRSetCH < 96 Then
        '    cmdstr = "01R" & Format(PLCRSet(R_SQTailPLC).PLCRSetCH + 1100, "00000") + m
        'Else
        '    cmdstr = "01R" & Format(PLCRSet(R_SQTailPLC).PLCRSetCH - 96 + 1300, "00000") + m
        'End If
        'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdWRITEREGISTERS, cmdstr, ETX)
        PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdWRITEREGISTERS, cmdstr, ETX))
        'System.Threading.Thread.Sleep(5)
    End Sub
    Private Sub WriteManyDToPLCFB()
        Dim cmdstr As String
        Dim m As String
        'm = FillZero(Val(PLCSet(R_SQTailPLC).PLCSetValue), 4)
        ''TimeDelay(CommandDelay)
        'If PLCSet(R_SQTailPLC).PLCSetCH < 96 Then
        '    cmdstr = "01R" & Format(PLCSet(R_SQTailPLC).PLCSetCH + 1100, "00000") + m
        'Else
        '    cmdstr = "01R" & Format(PLCSet(R_SQTailPLC).PLCSetCH - 96 + 1300, "00000") + m
        'End If

        Dim intHead, intTail, i, j As Integer

        'intHead = R_SQHeadPLC
        intTail = R_SQTailPLC
        intHead = R_SQHeadPLC

        If intHead < intTail Then
            intHead = intHead + PLCRSetMaxCount
        End If

        '�P�_�O�_�W�L15��
        If (intHead - intTail) > 15 Then
            'If R_SQHeadPLC > R_SQTailPLC + 15 Then
            'intHead = (R_SQTailPLC + 15) Mod PLCRSetMaxCount
            intHead = (R_SQTailPLC + 15)
            'Else
            '    intHead = R_SQHeadPLC
        End If
        j = intHead - intTail
        'Debug.Print("����=" + j.ToString)
        cmdstr = FillZero(j, 2)
        For i = intTail + 1 To intHead
            m = FillZero(Val(PLCRSet(i Mod PLCRSetMaxCount).PLCRSetValue), 4)
            'If R_R1100_Read.TryGetValue(ch, Val) Then
            '    m = Format(val, "0000")
            'End If

            'TimeDelay(CommandDelay)
            If PLCRSet(i Mod PLCRSetMaxCount).PLCRSetCH < 96 Then
                cmdstr += "R" + Format(PLCRSet(i Mod PLCRSetMaxCount).PLCRSetCH + 1100, "00000") + m
            Else
                cmdstr += "R" + Format(PLCRSet(i Mod PLCRSetMaxCount).PLCRSetCH - 96 + 1300, "00000") + m
            End If
        Next
        'Debug.Print("WriteR OK " + (R_SQTailPLC + 1).ToString() + "~" + R_SQHeadPLC.ToString())
        R_SQTailPLC = intHead Mod PLCRSetMaxCount
        'Debug.Print("R_SQTailPLC �� =" + intTail.ToString)
        'If cmdstr.Length < 12 Then
        '    Debug.Print("Err")
        'End If

        'Debug.Print("cmdstr=" + cmdstr)
        'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdWRITEREGISTERS, cmdstr, ETX)
        PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdManyWRITEREGISTERS, cmdstr, ETX))
        'Debug.Print("WriteR many " + (intTail + 1).ToString() + "~" + intHead.ToString())
        'PLCAlarm_Log("WriteR many " + intTail.ToString() + "~" + intHead.ToString())
        'System.Threading.Thread.Sleep(5)
    End Sub

    ' For Choose PLC Type
    'Private Sub PLC_ONComm()

    '    PLC_ONComm_FB()   ' FATEK

    'End Sub
    Public FBPLC_ACK As Boolean = False
    Public Sub FBPLCCommDataReceived() 'Handles PLCComm.DataReceived             'Read PLC data
        Dim i As Integer                              'Step control of received string
        Dim cnt As Integer                            'Recording the length of received string
        Dim rx_str As String                          'Recording the recived string
        Dim ch As String

        'rx_str = PLCComm.ReadExisting
        rx_str = PLCComm.ReadTo(Chr(3)) & Chr(3)
        Rx_msgPLC = rx_str
        FBPLC_ACK = True

        'cnt = Len(rx_str)
        'For i = 1 To cnt
        '    ch = Mid(rx_str, i, 1)
        '    Rx_msgPLC = Rx_msgPLC + ch

        '    If ch = ETX Then
        '        FBPLC_ACK = True
        '        'FBSPLC_Control()
        '    End If

        'Next i

    End Sub
    'Public Sub FBSPLC_Control()
    '    Dim i As Integer           ' Step control of received string
    '    Dim cnt As Integer      ' Recording the length of received string
    '    Dim rx_str As String = ""  ' Recording the recived string
    '    Dim ch As String
    '    Dim j As Integer
    '    Dim k As Integer
    '    Dim Tmpstr As String
    '    Dim L As Integer
    '    Dim m As String
    '    Dim km As Integer
    '    'On Error Resume Next
    '    Select Case Comm_StatePLC
    '        Case 0
    '            If FBPLC_ACK Then
    '                For km = 0 To 95
    '                    Y_Changed(km) = True
    '                Next km
    '                For km = 0 To 95
    '                    X_Changed(km) = True
    '                Next km
    '                If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                    PLCWatchDog = PLCWatchDogTimeSet
    '                    'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADSTATUS, "", ETX)
    '                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADSTATUS, "", ETX))
    '                    Comm_StatePLC = 1
    '                End If
    '                Rx_msgPLC = ""
    '            End If
    '        Case 1
    '            'Readstatus
    '            If FBPLC_ACK Then

    '                If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                    PLCWatchDog = PLCWatchDogTimeSet
    '                    Rx_msgPLC = ""
    '                    'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60X0000", ETX)  'Ū��X0-X47
    '                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60X0000", ETX))
    '                    Comm_StatePLC = 2
    '                End If
    '            End If
    '        Case 2
    '            If FBPLC_ACK Then
    '                If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                    PLCWatchDog = PLCWatchDogTimeSet
    '                    UpDataPLC_FB(0)

    '                    'claire
    '                    'If ii <= 100 Then
    '                    '    arr(ii) = wdog.ElapsedMilliseconds
    '                    '    ii = ii + 1
    '                    '    wdog.Restart()
    '                    'End If
    '                    'If ii = 101 Then

    '                    '    For ii As Integer = 0 To 100
    '                    '        file_old.WriteLine("arr(" + ii.ToString + ")=" + arr(ii).ToString)
    '                    '        Debug.Print("arr(" + ii.ToString + ")=" + arr(ii).ToString)
    '                    '    Next
    '                    '    file_old.Close()
    '                    '    ii = ii + 1
    '                    'End If

    '                    'claire
    '                    Rx_msgPLC = ""
    '                    If SQTailPLC <> SQHeadPLC Then
    '                        SQTailPLC = (SQTailPLC + 1) Mod PLCSetMaxCount
    '                        If PLCSet(SQTailPLC).PLCSetType = 1 Then
    '                            WriteMToPLCFB()
    '                            Comm_StatePLC = 21
    '                        End If
    '                        If PLCSet(SQTailPLC).PLCSetType = 2 Then
    '                            WriteDToPLCFB()
    '                            Comm_StatePLC = 21
    '                        End If

    '                    Else
    '                        'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60Y0000", ETX) 'Ū��Y0-Y71
    '                        PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60Y0000", ETX)) 'Ū��Y0-Y71
    '                        Comm_StatePLC = 3
    '                    End If
    '                End If
    '            End If
    '        Case 21
    '            'Readstatus
    '            If FBPLC_ACK Then
    '                If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                    PLCWatchDog = PLCWatchDogTimeSet
    '                    Rx_msgPLC = ""
    '                    'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60Y0000", ETX) 'Ū��Y0-Y71
    '                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60Y0000", ETX)) 'Ū��Y0-Y71
    '                    Comm_StatePLC = 3
    '                End If
    '            End If
    '        Case 3
    '            If FBPLC_ACK Then
    '                If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                    PLCWatchDog = PLCWatchDogTimeSet
    '                    UpDataPLC_FB(1)
    '                    Rx_msgPLC = ""
    '                    If SQTailPLC <> SQHeadPLC Then
    '                        SQTailPLC = (SQTailPLC + 1) Mod PLCSetMaxCount
    '                        If PLCSet(SQTailPLC).PLCSetType = 1 Then
    '                            WriteMToPLCFB()
    '                            Comm_StatePLC = 31
    '                        End If
    '                        If PLCSet(SQTailPLC).PLCSetType = 2 Then
    '                            WriteDToPLCFB()
    '                            Comm_StatePLC = 31
    '                        End If
    '                    Else
    '                        'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60M0000", ETX) 'Ū��M0-M96
    '                        PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60M0000", ETX))
    '                        Comm_StatePLC = 4
    '                    End If
    '                End If
    '            End If
    '        Case 31
    '            'Readstatus
    '            If FBPLC_ACK Then
    '                If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                    PLCWatchDog = PLCWatchDogTimeSet
    '                    Rx_msgPLC = ""
    '                    'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60M0000", ETX) 'Ū��M0-M96
    '                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60M0000", ETX))
    '                    Comm_StatePLC = 4
    '                End If
    '            End If
    '        Case 4
    '            If FBPLC_ACK Then
    '                If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                    PLCWatchDog = PLCWatchDogTimeSet
    '                    UpDataPLC_FB(2)
    '                    Rx_msgPLC = ""
    '                    If SQTailPLC <> SQHeadPLC Then
    '                        SQTailPLC = (SQTailPLC + 1) Mod PLCSetMaxCount
    '                        If PLCSet(SQTailPLC).PLCSetType = 1 Then
    '                            WriteMToPLCFB()
    '                            Comm_StatePLC = 41
    '                        End If
    '                        If PLCSet(SQTailPLC).PLCSetType = 2 Then
    '                            WriteDToPLCFB()
    '                            Comm_StatePLC = 41
    '                        End If
    '                    Else
    '                        'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01000", ETX) 'Ū��R1000-R1031
    '                        PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01000", ETX))
    '                        Comm_StatePLC = 5
    '                    End If
    '                End If
    '            End If
    '        Case 41
    '            'Readstatus
    '            If FBPLC_ACK Then
    '                If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                    PLCWatchDog = PLCWatchDogTimeSet
    '                    Rx_msgPLC = ""
    '                    'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01000", ETX) 'Ū��R1000-R1031
    '                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01000", ETX))
    '                    Comm_StatePLC = 5
    '                End If
    '            End If
    '        Case 5
    '            If FBPLC_ACK Then
    '                If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                    PLCWatchDog = PLCWatchDogTimeSet
    '                    UpDataPLC_FB(3)
    '                    Rx_msgPLC = ""
    '                    If SQTailPLC <> SQHeadPLC Then
    '                        SQTailPLC = (SQTailPLC + 1) Mod PLCSetMaxCount
    '                        If PLCSet(SQTailPLC).PLCSetType = 1 Then
    '                            WriteMToPLCFB()
    '                            Comm_StatePLC = 51
    '                        End If
    '                        If PLCSet(SQTailPLC).PLCSetType = 2 Then
    '                            WriteDToPLCFB()
    '                            Comm_StatePLC = 51
    '                        End If
    '                    Else
    '                        'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01100", ETX) 'Ū��R1100-R1131
    '                        PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01100", ETX))
    '                        Comm_StatePLC = 6
    '                    End If
    '                End If
    '            End If
    '        Case 51
    '            'Readstatus
    '            If FBPLC_ACK Then
    '                If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                    PLCWatchDog = PLCWatchDogTimeSet
    '                    Rx_msgPLC = ""
    '                    'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01100", ETX) 'Ū��R1100-R1131
    '                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01100", ETX))
    '                    Comm_StatePLC = 6
    '                End If
    '            End If
    '        Case 6
    '            If FBPLC_ACK Then
    '                If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                    PLCWatchDog = PLCWatchDogTimeSet
    '                    UpDataPLC_FB(5)
    '                    Rx_msgPLC = ""
    '                    If SQTailPLC <> SQHeadPLC Then
    '                        SQTailPLC = (SQTailPLC + 1) Mod PLCSetMaxCount
    '                        If PLCSet(SQTailPLC).PLCSetType = 1 Then
    '                            WriteMToPLCFB()
    '                            Comm_StatePLC = 61
    '                        End If
    '                        If PLCSet(SQTailPLC).PLCSetType = 2 Then
    '                            WriteDToPLCFB()
    '                            Comm_StatePLC = 61
    '                        End If
    '                    Else
    '                        'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01032", ETX) 'Ū��R1032-R1095
    '                        PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01032", ETX))
    '                        Comm_StatePLC = 7
    '                    End If
    '                End If
    '            End If
    '        Case 61
    '            'Readstatus
    '            If FBPLC_ACK Then
    '                If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                    PLCWatchDog = PLCWatchDogTimeSet
    '                    Rx_msgPLC = ""
    '                    'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01032", ETX) 'Ū��R1032-R1095
    '                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01032", ETX))
    '                    Comm_StatePLC = 7
    '                End If
    '            End If
    '        Case 7
    '            If FBPLC_ACK Then

    '                If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                    PLCWatchDog = PLCWatchDogTimeSet
    '                    UpDataPLC_FB(6)
    '                    Rx_msgPLC = ""
    '                    CommLivePLC = True
    '                    If SQTailPLC <> SQHeadPLC Then
    '                        SQTailPLC = (SQTailPLC + 1) Mod PLCSetMaxCount
    '                        If PLCSet(SQTailPLC).PLCSetType = 1 Then
    '                            WriteMToPLCFB()
    '                            Comm_StatePLC = 71
    '                        End If
    '                        If PLCSet(SQTailPLC).PLCSetType = 2 Then
    '                            WriteDToPLCFB()
    '                            Comm_StatePLC = 71
    '                        End If
    '                    Else
    '                        'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01132", ETX) 'Ū��R1132-R1195
    '                        PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01132", ETX))
    '                        Comm_StatePLC = 8
    '                    End If
    '                End If
    '            End If
    '        Case 71
    '            'Readstatus
    '            If FBPLC_ACK Then
    '                If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                    PLCWatchDog = PLCWatchDogTimeSet
    '                    Rx_msgPLC = ""
    '                    'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01132", ETX) 'Ū��R1132-R1195
    '                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01132", ETX))
    '                    Comm_StatePLC = 8
    '                End If
    '            End If
    '        Case 8
    '            If FBPLC_ACK Then
    '                If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                    PLCWatchDog = PLCWatchDogTimeSet
    '                    UpDataPLC_FB(7)
    '                    Rx_msgPLC = ""
    '                    If SQTailPLC <> SQHeadPLC Then
    '                        SQTailPLC = (SQTailPLC + 1) Mod PLCSetMaxCount
    '                        If PLCSet(SQTailPLC).PLCSetType = 1 Then
    '                            WriteMToPLCFB()
    '                            Comm_StatePLC = 81
    '                        End If
    '                        If PLCSet(SQTailPLC).PLCSetType = 2 Then
    '                            WriteDToPLCFB()
    '                            Comm_StatePLC = 81
    '                        End If
    '                    Else
    '                        'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01200", ETX) 'Ū��R1200-R1263 -> PLC_R_READ(96~159)
    '                        PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01200", ETX))
    '                        Comm_StatePLC = 9
    '                    End If
    '                End If
    '            End If
    '        Case 81
    '            'Readstatus
    '            If FBPLC_ACK Then
    '                If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                    PLCWatchDog = PLCWatchDogTimeSet
    '                    Rx_msgPLC = ""
    '                    'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01200", ETX) 'Ū��R1200-R1263 -> PLC_R_READ(96~159)
    '                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01200", ETX))
    '                    Comm_StatePLC = 9
    '                End If
    '            End If
    '        Case 9
    '            If FBPLC_ACK Then

    '                If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                    PLCWatchDog = PLCWatchDogTimeSet
    '                    UpDataPLC_FB(8)
    '                    Rx_msgPLC = ""
    '                    If SQTailPLC <> SQHeadPLC Then
    '                        SQTailPLC = (SQTailPLC + 1) Mod PLCSetMaxCount
    '                        If PLCSet(SQTailPLC).PLCSetType = 1 Then
    '                            WriteMToPLCFB()
    '                            Comm_StatePLC = 91
    '                        End If
    '                        If PLCSet(SQTailPLC).PLCSetType = 2 Then
    '                            WriteDToPLCFB()
    '                            Comm_StatePLC = 91
    '                        End If
    '                    Else
    '                        'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01300", ETX) 'Ū��R1300-R1363 -> PLC_R_SET(96~159)
    '                        PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01300", ETX)) 'Ū��R1300-R1363

    '                        Comm_StatePLC = 10
    '                    End If
    '                End If
    '            End If
    '        Case 91
    '            'Readstatus
    '            If FBPLC_ACK Then
    '                If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                    PLCWatchDog = PLCWatchDogTimeSet
    '                    Rx_msgPLC = ""
    '                    'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01300", ETX) 'Ū��R1300-R1363 -> PLC_R_SET(96~159)
    '                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01300", ETX)) 'Ū��X0-X47
    '                    Comm_StatePLC = 10
    '                End If
    '            End If
    '        Case 10
    '            If FBPLC_ACK Then

    '                If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                    PLCWatchDog = PLCWatchDogTimeSet
    '                    UpDataPLC_FB(9)
    '                    Rx_msgPLC = ""
    '                    If SQTailPLC <> SQHeadPLC Then
    '                        SQTailPLC = (SQTailPLC + 1) Mod PLCSetMaxCount
    '                        If PLCSet(SQTailPLC).PLCSetType = 1 Then
    '                            WriteMToPLCFB()
    '                            Comm_StatePLC = 101
    '                        End If
    '                        If PLCSet(SQTailPLC).PLCSetType = 2 Then
    '                            WriteDToPLCFB()
    '                            Comm_StatePLC = 101
    '                        End If
    '                    Else
    '                        'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01364", ETX) 'Ū��R1364-R1396 -> PLC_R_SET(160~192)
    '                        PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01364", ETX)) 'Ū��1364-R1396

    '                        Comm_StatePLC = 11
    '                    End If
    '                End If
    '            End If
    '        Case 101
    '            'Readstatus
    '            If FBPLC_ACK Then
    '                If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                    PLCWatchDog = PLCWatchDogTimeSet
    '                    Rx_msgPLC = ""
    '                    'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01364", ETX) 'Ū��R1364-R1396 -> PLC_R_SET(160~192)
    '                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01364", ETX)) 'Ū��X0-X47

    '                    Comm_StatePLC = 11
    '                End If
    '            End If
    '        Case 11
    '            If FBPLC_ACK Then
    '                'plccount = plccount + 1
    '                If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                    PLCWatchDog = PLCWatchDogTimeSet
    '                    UpDataPLC_FB(10)
    '                    Rx_msgPLC = ""
    '                    CommLivePLC = True
    '                    If SQTailPLC <> SQHeadPLC Then
    '                        SQTailPLC = (SQTailPLC + 1) Mod PLCSetMaxCount
    '                        If PLCSet(SQTailPLC).PLCSetType = 1 Then
    '                            WriteMToPLCFB()
    '                            Comm_StatePLC = 1
    '                        End If
    '                        If PLCSet(SQTailPLC).PLCSetType = 2 Then
    '                            WriteDToPLCFB()
    '                            Comm_StatePLC = 1
    '                        End If
    '                    Else
    '                        'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60X0000", ETX)  'Ū��X0-X47
    '                        PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60X0000", ETX)) 'Ū��X0-X47
    '                        Comm_StatePLC = 2
    '                    End If
    '                End If
    '            End If

    '    End Select
    'End Sub

    Public Sub wdog_Restart()
        wdog.Stop()
        wdog.Reset()
        wdog.Start()
    End Sub
    Public Sub wdog1_Restart()
        wdog1.Stop()
        wdog1.Reset()
        wdog1.Start()
    End Sub
    Public Sub swdog_Restart()
        swdog.Stop()
        swdog.Reset()
        swdog.Start()
    End Sub
    Public Sub Rs232ThreadWork()

        Dim i As Integer = 0
        Dim boolfirst As Boolean
        System.Threading.Thread.Sleep(100)

        If bolonebyone = True Then
            Try
                If PLCComm.IsOpen() Then
                    wdog_Restart()

                    Do
                        Rx_msgPLC = Rx_msgPLC + PLCComm.ReadExisting()
                        If wdog.ElapsedMilliseconds > 1000 Then
                            Rs232Thread.Abort()
                        End If
                    Loop Until (InStr(1, Rx_msgPLC, ETX) > 0)

                    CommLivePLC = True
                    boolfirst = True
                Else
                    CommLivePLC = False
                End If
                checkWriteMR()

                '��R R1000-R1031
                Rx_msgPLC = PLCComm.ReadExisting()
                Rx_msgPLC = ""
                PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01000", ETX)) 'Ū��R1000-R1031
                Do

                    'Debug.Print("All_Time=" + wdog1.ElapsedMilliseconds.ToString)
                    'wdog1_Restart()
                    '��X
                    Rx_msgPLC = PLCComm.ReadExisting()
                    Rx_msgPLC = ""
                    PLCWatchDog = PLCWatchDogTimeSet
                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60X0000", ETX)) 'Ū��X0-X47 '44

                    wdog_Restart()
                    Do
                        Rx_msgPLC = Rx_msgPLC + PLCComm.ReadExisting()
                        If wdog.ElapsedMilliseconds > 1000 Then
                            Rs232Thread.Abort()
                        End If
                    Loop Until (InStr(1, Rx_msgPLC, ETX) > 0)
                    CommLivePLC = True
                    'Debug.Print("Rx_msgPLC__X =" + Rx_msgPLC)
                    If InStr(1, Rx_msgPLC, "0144") > 0 Then
                        UpDataPLC_FB(0)
                    End If
                    checkWriteMR()

                    '��Y
                    Rx_msgPLC = PLCComm.ReadExisting()
                    Rx_msgPLC = ""
                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60Y0000", ETX)) 'Ū��Y0-Y71 44

                    wdog_Restart()
                    Do
                        Rx_msgPLC = Rx_msgPLC + PLCComm.ReadExisting()
                        If wdog.ElapsedMilliseconds > 1000 Then
                            Rs232Thread.Abort()
                        End If
                    Loop Until (InStr(1, Rx_msgPLC, ETX) > 0)
                    'Debug.Print("Rx_msgPLC__Y =" + Rx_msgPLC)
                    If InStr(1, Rx_msgPLC, "0144") > 0 Then
                        UpDataPLC_FB(1)
                    End If
                    checkWriteMR()

                    '��M
                    Rx_msgPLC = PLCComm.ReadExisting()
                    Rx_msgPLC = ""
                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60M0000", ETX)) 'Ū��M0-M96 44

                    wdog_Restart()
                    Do
                        Rx_msgPLC = Rx_msgPLC + PLCComm.ReadExisting()
                        If wdog.ElapsedMilliseconds > 1000 Then
                            Rs232Thread.Abort()
                        End If
                    Loop Until (InStr(1, Rx_msgPLC, ETX) > 0)
                    'Debug.Print("Rx_msgPLC__M =" + Rx_msgPLC)
                    If InStr(1, Rx_msgPLC, "0144") > 0 Then
                        UpDataPLC_FB(2)
                    End If
                    checkWriteMR()

                    '��R R1000-R1031
                    Rx_msgPLC = PLCComm.ReadExisting()
                    Rx_msgPLC = ""
                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01000", ETX)) 'Ū��R1000-R1031
                    wdog_Restart()
                    Do
                        Rx_msgPLC = Rx_msgPLC + PLCComm.ReadExisting()
                        If wdog.ElapsedMilliseconds > 1000 Then
                            Rs232Thread.Abort()
                        End If
                    Loop Until (InStr(1, Rx_msgPLC, ETX) > 0)
                    'Debug.Print("Rx_msgPLC__R =" + Rx_msgPLC)
                    If InStr(1, Rx_msgPLC, "0146") > 0 Then
                        UpDataPLC_FB(3)
                    End If
                    checkWriteMR()

                    '��R R1032-R1095
                    Rx_msgPLC = PLCComm.ReadExisting()
                    Rx_msgPLC = ""
                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01032", ETX)) ''Ū��R1032-R1095
                    wdog_Restart()
                    Do
                        Rx_msgPLC = Rx_msgPLC + PLCComm.ReadExisting()
                        If wdog.ElapsedMilliseconds > 1000 Then
                            Rs232Thread.Abort()
                        End If
                    Loop Until (InStr(1, Rx_msgPLC, ETX) > 0)
                    'Debug.Print("Rx_msgPLC__R =" + Rx_msgPLC)
                    If InStr(1, Rx_msgPLC, "0146") > 0 Then
                        UpDataPLC_FB(6)
                    End If
                    checkWriteMR()

                    '��R R1100-R11031
                    Rx_msgPLC = PLCComm.ReadExisting()
                    Rx_msgPLC = ""
                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01100", ETX)) 'Ū��R1100-R1131
                    wdog_Restart()
                    Do
                        Rx_msgPLC = Rx_msgPLC + PLCComm.ReadExisting()
                        If wdog.ElapsedMilliseconds > 1000 Then
                            Rs232Thread.Abort()
                        End If
                    Loop Until (InStr(1, Rx_msgPLC, ETX) > 0)
                    'Debug.Print("Rx_msgPLC__R =" + Rx_msgPLC)
                    If InStr(1, Rx_msgPLC, "0146") > 0 Then
                        UpDataPLC_FB(5)
                    End If
                    checkWriteMR()

                    '��R R1132-R1195
                    Rx_msgPLC = PLCComm.ReadExisting()
                    Rx_msgPLC = ""
                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01132", ETX)) 'Ū��R1132-R11095
                    wdog_Restart()
                    Do
                        Rx_msgPLC = Rx_msgPLC + PLCComm.ReadExisting()
                        If wdog.ElapsedMilliseconds > 1000 Then
                            Rs232Thread.Abort()
                        End If
                    Loop Until (InStr(1, Rx_msgPLC, ETX) > 0)
                    'Debug.Print("Rx_msgPLC__R =" + Rx_msgPLC)
                    If InStr(1, Rx_msgPLC, "0146") > 0 Then
                        UpDataPLC_FB(7)
                    End If
                    checkWriteMR()

                    '��R R1200-R1263
                    Rx_msgPLC = PLCComm.ReadExisting()
                    Rx_msgPLC = ""
                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01200", ETX)) 'Ū��1200-R1263
                    wdog_Restart()
                    Do
                        Rx_msgPLC = Rx_msgPLC + PLCComm.ReadExisting()
                        If wdog.ElapsedMilliseconds > 1000 Then
                            Rs232Thread.Abort()
                        End If
                    Loop Until (InStr(1, Rx_msgPLC, ETX) > 0)
                    'Debug.Print("Rx_msgPLC__R =" + Rx_msgPLC)
                    If InStr(1, Rx_msgPLC, "0146") > 0 Then
                        UpDataPLC_FB(8)
                    End If
                    checkWriteMR()

                    '��R R1300-R1363
                    Rx_msgPLC = PLCComm.ReadExisting()
                    Rx_msgPLC = ""
                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01300", ETX)) 'Ū��R1300-R1363
                    wdog_Restart()
                    Do
                        Rx_msgPLC = Rx_msgPLC + PLCComm.ReadExisting()
                        If wdog.ElapsedMilliseconds > 1000 Then
                            Rs232Thread.Abort()
                        End If
                    Loop Until (InStr(1, Rx_msgPLC, ETX) > 0)
                    'Debug.Print("Rx_msgPLC__R =" + Rx_msgPLC)
                    If InStr(1, Rx_msgPLC, "0146") > 0 Then
                        UpDataPLC_FB(9)
                    End If
                    checkWriteMR()

                    '��R R1364-R1396
                    Rx_msgPLC = PLCComm.ReadExisting()
                    Rx_msgPLC = ""
                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01364", ETX)) 'Ū��RR1364-R1396
                    wdog_Restart()
                    Do
                        Rx_msgPLC = Rx_msgPLC + PLCComm.ReadExisting()
                        If wdog.ElapsedMilliseconds > 1000 Then
                            Rs232Thread.Abort()
                        End If
                    Loop Until (InStr(1, Rx_msgPLC, ETX) > 0)
                    'Debug.Print("Rx_msgPLC__R =" + Rx_msgPLC)
                    If InStr(1, Rx_msgPLC, "0146") > 0 Then
                        UpDataPLC_FB(10)
                    End If
                    checkWriteMR()
                    If boolfirst Then
                        ReadOriginalR1100()
                        boolfirst = False
                    Else

                        R1100_Value_Compare(R_R1100_Read, 0, 191)
                    End If
                    'System.Threading.Thread.Sleep(2)

                Loop Until (bolQuit = True)
                PLCComm.Close()
                Debug.Print("Quit")
                Application.Exit()
            Catch __unusedThreadAbortException2__ As ThreadAbortException
                Debug.Print("thread_ abort")
            End Try
        Else '�s��g�J

            If bolQuit = True Then
                Exit Sub
            End If
            Try
                If PLCComm.IsOpen() Then
                    wdog_Restart()
                    'swdog_Restart()
                    Do
                        Rx_msgPLC = Rx_msgPLC + PLCComm.ReadExisting()
                        If wdog.ElapsedMilliseconds > 2000 Then
                            'PLCAlarm_Log("Thread abort__status read ")
                            strErr = "Thread abort__status read "
                            Rs232Thread.Abort()
                        End If

                        'System.Threading.Thread.Sleep(2000)
                        'Application.DoEvents()
                    Loop Until (InStr(1, Rx_msgPLC, ETX) > 0)
                    'Debug.Print("status read ok")
                    CommLivePLC = True
                    boolfirst = True
                Else
                    'CommLivePLC = False
                    strErr = "PLCComm.IsOpen=false"
                    'Debug.Print("PLCComm.IsOpen=false")
                    Rs232Thread.Abort()
                End If

                Do
                    '��X
                    'Debug.Print("ask x ok")
                    'swdog_Restart()
                    Rx_msgPLC = PLCComm.ReadExisting()
                    Rx_msgPLC = ""
                    PLCWatchDog = PLCWatchDogTimeSet
                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60X0000", ETX)) 'Ū��X0-X47 '44

                    'Debug.Print("All_time=" + wdog1.ElapsedMilliseconds.ToString)
                    'wdog1_Restart()
                    wdog_Restart()
                    'swdog_Restart()
                    Do
                        Rx_msgPLC = Rx_msgPLC + PLCComm.ReadExisting()
                        If wdog.ElapsedMilliseconds > 1000 Then
                            strErr = "Thread abort__X read "
                            'PLCAlarm_Log("Thread abort__X read ")
                            Rs232Thread.Abort()
                        End If
                    Loop Until (InStr(1, Rx_msgPLC, ETX) > 0)
                    'System.Threading.Thread.Sleep(2)
                    'Debug.Print("read x ok")
                    CommLivePLC = True

                    If InStr(1, Rx_msgPLC, "0144") > 0 Then
                        UpDataPLC_FB(0)
                    End If
                    'Debug.Print("��X= " + swdog.ElapsedMilliseconds.ToString)
                    checkWriteM()
                    checkWriteR()
                    'swdog_Restart()

                    '��Y
                    Rx_msgPLC = PLCComm.ReadExisting()
                    Rx_msgPLC = ""
                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60Y0000", ETX)) 'Ū��Y0-Y71 44
                    wdog_Restart()
                    Do
                        Rx_msgPLC = Rx_msgPLC + PLCComm.ReadExisting()
                        If wdog.ElapsedMilliseconds > 1000 Then
                            'PLCAlarm_Log("Thread abort__Y read ")
                            strErr = "Thread abort__Y read "
                            Rs232Thread.Abort()
                        End If
                    Loop Until (InStr(1, Rx_msgPLC, ETX) > 0)
                    'System.Threading.Thread.Sleep(2)
                    'Debug.Print("read y ok")
                    If InStr(1, Rx_msgPLC, "0144") > 0 Then
                        UpDataPLC_FB(1)
                    End If
                    'Debug.Print("��Y= " + swdog.ElapsedMilliseconds.ToString)
                    '��M
                    Rx_msgPLC = PLCComm.ReadExisting()
                    Rx_msgPLC = ""
                    'swdog_Restart()
                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60M0000", ETX)) 'Ū��M0-M96 44
                    wdog_Restart()
                    Do
                        Rx_msgPLC = Rx_msgPLC + PLCComm.ReadExisting()
                        If wdog.ElapsedMilliseconds > 1000 Then
                            strErr = "Thread abort__M0-96 read "
                            'PLCAlarm_Log("Thread abort__M0-96 read ")
                            Rs232Thread.Abort()
                        End If
                    Loop Until (InStr(1, Rx_msgPLC, ETX) > 0)
                    'System.Threading.Thread.Sleep(2)
                    'Debug.Print("read m ok")
                    If InStr(1, Rx_msgPLC, "0144") > 0 Then
                        UpDataPLC_FB(2)
                    End If

                    '��R R1000-R1031

                    Rx_msgPLC = PLCComm.ReadExisting()
                    Rx_msgPLC = ""
                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01000", ETX)) 'Ū��R1000-R1019
                    wdog_Restart()
                    Do
                        Rx_msgPLC = Rx_msgPLC + PLCComm.ReadExisting()
                        If wdog.ElapsedMilliseconds > 1000 Then
                            strErr = "Thread abort__R1000-R1019 read "
                            'PLCAlarm_Log("Thread abort__R1000-R1019 read ")
                            Rs232Thread.Abort()
                        End If
                    Loop Until (InStr(1, Rx_msgPLC, ETX) > 0)
                    'System.Threading.Thread.Sleep(2)
                    'Debug.Print("read R1000-R1019 ok")
                    If InStr(1, Rx_msgPLC, "0146") > 0 Then
                        UpDataPLC_FB(3)
                    End If

                    '��R R1032-R1095

                    Rx_msgPLC = PLCComm.ReadExisting()
                    Rx_msgPLC = ""
                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01032", ETX)) ''Ū��R1032-R1071
                    wdog_Restart()
                    Do
                        Rx_msgPLC = Rx_msgPLC + PLCComm.ReadExisting()
                        If wdog.ElapsedMilliseconds > 1000 Then
                            strErr = "Thread abort__R1032-R1071 read "
                            'PLCAlarm_Log("Thread abort__R1032-R1071 read ")
                            Rs232Thread.Abort()
                        End If
                    Loop Until (InStr(1, Rx_msgPLC, ETX) > 0)
                    'System.Threading.Thread.Sleep(2)
                    'Debug.Print("read _R1032-R1071 ok")
                    If InStr(1, Rx_msgPLC, "0146") > 0 Then
                        UpDataPLC_FB(6)
                    End If
                    checkWriteM()

                    '��R R1100-R11031

                    Rx_msgPLC = PLCComm.ReadExisting()
                    Rx_msgPLC = ""
                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01100", ETX)) 'Ū��R1100-R1131(PLC_R_SetRead(0)~(31))
                    wdog_Restart()
                    Do
                        Rx_msgPLC = Rx_msgPLC + PLCComm.ReadExisting()
                        If wdog.ElapsedMilliseconds > 1000 Then
                            strErr = "Thread abort__R1100-1131 read "
                            'PLCAlarm_Log("Thread abort__R1100-1131 read ")
                            Rs232Thread.Abort()
                        End If
                    Loop Until (InStr(1, Rx_msgPLC, ETX) > 0)
                    'System.Threading.Thread.Sleep(2)
                    'Debug.Print("read R1100-1131 ok")
                    If InStr(1, Rx_msgPLC, "0146") > 0 Then
                        UpDataPLC_FB(5)
                    End If


                    '��R R1132-R1195

                    Rx_msgPLC = PLCComm.ReadExisting()
                    Rx_msgPLC = ""
                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01132", ETX)) 'Ū��R1132-R1195(PLC_R_SetRead(32)~(95))
                    wdog_Restart()
                    Do
                        Rx_msgPLC = Rx_msgPLC + PLCComm.ReadExisting()
                        If wdog.ElapsedMilliseconds > 1000 Then
                            strErr = "Thread abort__R1132-R1195 read "
                            'PLCAlarm_Log("Thread abort__R1132-R1195 read ")
                            Rs232Thread.Abort()
                        End If
                    Loop Until (InStr(1, Rx_msgPLC, ETX) > 0)
                    'System.Threading.Thread.Sleep(2)
                    'Debug.Print("read R1132-R1195 ok")
                    If InStr(1, Rx_msgPLC, "0146") > 0 Then
                        UpDataPLC_FB(7)
                    End If

                    checkWriteM()

                    '��R R1200-R1263

                    Rx_msgPLC = PLCComm.ReadExisting()
                    Rx_msgPLC = ""
                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01200", ETX)) 'Ū��1200-R1263
                    wdog_Restart()
                    Do
                        Rx_msgPLC = Rx_msgPLC + PLCComm.ReadExisting()
                        If wdog.ElapsedMilliseconds > 1000 Then
                            strErr = "Thread abort__R1200-1263 read "
                            'PLCAlarm_Log("Thread abort__R1200-1263 read ")
                            Rs232Thread.Abort()
                        End If
                    Loop Until (InStr(1, Rx_msgPLC, ETX) > 0)
                    'System.Threading.Thread.Sleep(2)
                    'Debug.Print("read R1200-1263 ok")
                    If InStr(1, Rx_msgPLC, "0146") > 0 Then
                        UpDataPLC_FB(8)
                    End If

                    '��R R1300-R1363
                    Rx_msgPLC = PLCComm.ReadExisting()
                    Rx_msgPLC = ""

                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01300", ETX)) 'Ū��R1300-R1363(PLC_R_SetRead(96)~(159))
                    wdog_Restart()
                    Do
                        Rx_msgPLC = Rx_msgPLC + PLCComm.ReadExisting()
                        If wdog.ElapsedMilliseconds > 1000 Then
                            'PLCAlarm_Log("Thread abort__R1300-R1363 read ")
                            strErr = "Thread abort__R1300-R1363 read "
                            Rs232Thread.Abort()
                        End If
                    Loop Until (InStr(1, Rx_msgPLC, ETX) > 0)
                    'System.Threading.Thread.Sleep(2)
                    'Debug.Print("read R1300-R1363 ok")
                    If InStr(1, Rx_msgPLC, "0146") > 0 Then
                        UpDataPLC_FB(9)
                    End If
                    checkWriteM()

                    '��R R1364-R1395

                    Rx_msgPLC = PLCComm.ReadExisting()
                    Rx_msgPLC = ""

                    PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01364", ETX)) 'Ū��R1364-R1395(PLC_R_SetRead(160)~(191))
                    wdog_Restart()
                    Do
                        Rx_msgPLC = Rx_msgPLC + PLCComm.ReadExisting()
                        If wdog.ElapsedMilliseconds > 1000 Then
                            'PLCAlarm_Log("Thread abort__R1364-R1395 read ")
                            strErr = "Thread abort__R1364-R1395 read "
                            Rs232Thread.Abort()
                        End If
                    Loop Until (InStr(1, Rx_msgPLC, ETX) > 0)
                    'System.Threading.Thread.Sleep(2)
                    'Debug.Print("read R1364-R1395 ok")
                    If InStr(1, Rx_msgPLC, "0146") > 0 Then
                        UpDataPLC_FB(10)
                    End If

                    'System.Threading.Thread.Sleep(2)

                    checkWriteR()
                    If boolfirst Then
                        ReadOriginalR1100()
                        boolfirst = False
                    Else
                        R1100_Value_Compare(R_R1100_Read, 0, 191)
                    End If
                    'System.Threading.Thread.Sleep(2)

                Loop Until (bolQuit = True)
                PLCComm.Close()
                Debug.Print("Quit")
                Application.Exit()

            Catch __unusedThreadAbortException2__ As ThreadAbortException
                Debug.Print("thread_ abort " + strErr)
                If PLCWatchDog = 0 Then PLCAlarm_Log(strErr)
            End Try
        End If
    End Sub


    Public Sub checkWriteMR()
        Rx_msgPLC = PLCComm.ReadExisting()
        Rx_msgPLC = ""
        If SQTailPLC <> SQHeadPLC Then
            Debug.Print("SQTailPLC =  " + SQTailPLC.ToString() + "    SQHeadPLC=  " + SQHeadPLC.ToString())
            SQTailPLC = (SQTailPLC + 1) Mod PLCSetMaxCount
            If PLCSet(SQTailPLC).PLCSetType = 1 Then
                WriteMToPLCFB()
            End If
            If PLCSet(SQTailPLC).PLCSetType = 2 Then
                WriteDToPLCFB()
            End If
            Do
                Rx_msgPLC = Rx_msgPLC + PLCComm.ReadExisting()
                If wdog.ElapsedMilliseconds > 1000 Then
                    Rs232Thread.Abort()
                End If
            Loop Until (InStr(1, Rx_msgPLC, ETX) > 0)

            'check ��Ʈ榡 error code
            If PLCSet(SQTailPLC).PLCSetType = 2 Then
                If Rx_msgPLC.Substring(1, 5) <> "01470" Then
                    Debug.Print("err code=" + Rx_msgPLC.Substring(1, 5))
                    PLCAlarm_Log("err code=" + Rx_msgPLC.Substring(1, 5))
                End If
            End If

        End If
    End Sub

    Public Sub checkWriteM()
        Rx_msgPLC = PLCComm.ReadExisting()
        Rx_msgPLC = ""
        If M_SQTailPLC <> M_SQHeadPLC Then
            'Debug.Print("M_SQTailPLC =  " + M_SQTailPLC.ToString() + "    M_SQHeadPLC=  " + M_SQHeadPLC.ToString())
            M_SQTailPLC = (M_SQTailPLC + 1) Mod PLCMSetMaxCount
            WriteMToPLCFB()
            Do
                Rx_msgPLC = Rx_msgPLC + PLCComm.ReadExisting()
                If wdog.ElapsedMilliseconds > 1000 Then
                    strErr = "Thread abort__M Write "
                    PLCAlarm_Log("Thread abort__M Write ")
                    Rs232Thread.Abort()
                End If
            Loop Until (InStr(1, Rx_msgPLC, ETX) > 0)
            System.Threading.Thread.Sleep(2)
        End If
    End Sub
    Public Sub checkWriteR()
        Rx_msgPLC = PLCComm.ReadExisting()
        Rx_msgPLC = ""
        Dim intHead, intTail, i As Integer
        If R_SQTailPLC <> R_SQHeadPLC Then
            wdog.Start()
            'Debug.Print("�Z��  R_SQTailPLC =  " + R_SQTailPLC.ToString() + "    R_SQHeadPLC=  " + R_SQHeadPLC.ToString())
            'If R_SQTailPLC > 31 Then swdog.Stop()
            'Debug.Print("Start_TTime=" + TTime)
            'If R_SQTailPLC <> R_SQHeadPLC Or RChanged() Then
            intHead = R_SQHeadPLC '�j
            intTail = R_SQTailPLC
            If bolonebyone = True Then
                'Debug.Print("R_SQTailPLC =  " + R_SQTailPLC.ToString() + "    R_SQHeadPLC=  " + R_SQHeadPLC.ToString())
                For i = intTail To intHead
                    R_SQTailPLC = (R_SQTailPLC + 1) Mod PLCRSetMaxCount
                    WriteDToPLCFB()
                    'Debug.Print("WriteR OK " + R_SQTailPLC.ToString() + "~" + R_SQHeadPLC.ToString())
                    'Debug.Print("WriteR OK TTime=" + TTime)
                    'Debug.Print("1-1 �Ӯ�=" + swdog.ElapsedMilliseconds.ToString)
                Next
                R_SQTailPLC = R_SQHeadPLC
            Else
                WriteManyDToPLCFB()

                'Debug.Print("WriteR OK TTime=" + TTime)
                'Debug.Print("1-�h �Ӯ�=" + swdog.ElapsedMilliseconds.ToString)
                'Debug.Print("WriteR many " + intTail.ToString() + "~" + intHead.ToString())
                'R_SQTailPLC = R_SQHeadPLC
            End If
            Do
                Rx_msgPLC = Rx_msgPLC + PLCComm.ReadExisting()
                If wdog.ElapsedMilliseconds > 1000 Then
                    strErr = "Thread abort__R Write "
                    PLCAlarm_Log("Thread abort__R Write ")
                    PLCAlarm_Log("WriteR ERR " + intTail.ToString() + "~" + intHead.ToString())
                    Debug.Print("Write R NG")
                    Rs232Thread.Abort()
                    'Else
                    '    PLCAlarm_Log("WriteR OK " + R_SQTailPLC.ToString() + "~" + R_SQHeadPLC.ToString())
                End If
            Loop Until (InStr(1, Rx_msgPLC, ETX) > 0)

            System.Threading.Thread.Sleep(2)
            'Debug.Print("Write R OK")
            bolWriteROK = True
            'PLCAlarm_Log("WriteR OK " + R_SQTailPLC.ToString() + "~" + R_SQHeadPLC.ToString())
            'R_SQTailPLC = R_SQHeadPLC
            'Debug.Print("R_SQTailPLC �Y=  " + R_SQHeadPLC.ToString)
            'check ��Ʈ榡 error code
            If Rx_msgPLC.Substring(1, 5) <> "01490" Then
                'Debug.Print("err code=" + Rx_msgPLC.Substring(1, 5))
                PLCAlarm_Log("err code=" + Rx_msgPLC.Substring(1, 5))
            End If
        End If
    End Sub
    'Public Function RChanged() As Boolean
    '    Dim i As Integer
    '    RChanged = False
    '    For i = 0 To 256
    '        If R1100_Changed(i) = True Then
    '            Return True
    '        End If
    '    Next

    'End Function


    'Public Sub PLC_ONComm_FB()
    '    Dim i As Integer           ' Step control of received string
    '    Dim cnt As Integer      ' Recording the length of received string
    '    Dim rx_str As String = ""  ' Recording the recived string
    '    Dim ch As String
    '    Dim j As Integer
    '    Dim k As Integer
    '    Dim Tmpstr As String
    '    Dim L As Integer
    '    Dim m As String
    '    Dim km As Integer
    '    'On Error Resume Next

    '    'rx_str = PLCComm.Input
    '    'System.Threading.Thread.Sleep(0)
    '    rx_str = PLCComm.ReadTo(Chr(3)) & Chr(3)
    '    'cnt = Len(rx_str)
    '    'For i = 1 To cnt
    '    '    ch = Mid(rx_str, i, 1)
    '    '    Rx_msgPLC = Rx_msgPLC + ch
    '    If InStr(rx_str, STX) > 0 And InStr(rx_str, ETX) > 0 Then
    '        'System.Threading.Thread.Sleep(30)
    '        'Dim aa As Integer = rx_str.Length / 3
    '        'If aa < 10 Then aa = 10
    '        'If aa > 50 Then aa = 50
    '        'System.Threading.Thread.Sleep(aa)
    '        Rx_msgPLC = rx_str
    '        rx_str = ""
    '        PLCWatchDog = PLCWatchDogTimeSet
    '        Select Case Comm_StatePLC
    '            Case 0
    '                If InStr(Rx_msgPLC, ETX) > 0 Then
    '                    For km = 0 To 95
    '                        Y_Changed(km) = True
    '                    Next km
    '                    For km = 0 To 95
    '                        X_Changed(km) = True
    '                    Next km
    '                    If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                        PLCWatchDog = PLCWatchDogTimeSet
    '                        'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADSTATUS, "", ETX)
    '                        PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADSTATUS, "", ETX))
    '                        Comm_StatePLC = 1
    '                    End If
    '                    Rx_msgPLC = ""
    '                    FBPLC_ACK = False
    '                End If
    '            Case 1
    '                'Readstatus
    '                If InStr(Rx_msgPLC, ETX) > 0 Then
    '                    If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                        PLCWatchDog = PLCWatchDogTimeSet
    '                        Rx_msgPLC = ""
    '                        FBPLC_ACK = False
    '                        'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60X0000", ETX)  'Ū��X0-X47
    '                        PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60X0000", ETX))
    '                        Comm_StatePLC = 2
    '                    End If
    '                End If
    '            Case 2
    '                If InStr(Rx_msgPLC, ETX) > 0 Then
    '                    If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                        PLCWatchDog = PLCWatchDogTimeSet
    '                        UpDataPLC_FB(0)
    '                        Rx_msgPLC = ""
    '                        FBPLC_ACK = False
    '                        If SQTailPLC <> SQHeadPLC Then
    '                            SQTailPLC = (SQTailPLC + 1) Mod PLCSetMaxCount
    '                            If PLCSet(SQTailPLC).PLCSetType = 1 Then
    '                                WriteMToPLCFB()
    '                                Comm_StatePLC = 21
    '                            End If
    '                            If PLCSet(SQTailPLC).PLCSetType = 2 Then
    '                                WriteDToPLCFB()
    '                                Comm_StatePLC = 21
    '                            End If

    '                        Else
    '                            'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60Y0000", ETX) 'Ū��Y0-Y71
    '                            PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60Y0000", ETX)) 'Ū��Y0-Y71
    '                            Comm_StatePLC = 3
    '                        End If
    '                    End If
    '                End If
    '            Case 21
    '                'Readstatus
    '                If InStr(Rx_msgPLC, ETX) > 0 Then
    '                    If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                        PLCWatchDog = PLCWatchDogTimeSet
    '                        Rx_msgPLC = ""
    '                        FBPLC_ACK = False
    '                        'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60Y0000", ETX) 'Ū��Y0-Y71
    '                        PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60Y0000", ETX)) 'Ū��Y0-Y71
    '                        Comm_StatePLC = 3
    '                    End If
    '                End If
    '            Case 3
    '                If InStr(Rx_msgPLC, ETX) > 0 Then
    '                    If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                        PLCWatchDog = PLCWatchDogTimeSet
    '                        UpDataPLC_FB(1)
    '                        Rx_msgPLC = ""
    '                        FBPLC_ACK = False
    '                        If SQTailPLC <> SQHeadPLC Then
    '                            SQTailPLC = (SQTailPLC + 1) Mod PLCSetMaxCount
    '                            If PLCSet(SQTailPLC).PLCSetType = 1 Then
    '                                WriteMToPLCFB()
    '                                Comm_StatePLC = 31
    '                            End If
    '                            If PLCSet(SQTailPLC).PLCSetType = 2 Then
    '                                WriteDToPLCFB()
    '                                Comm_StatePLC = 31
    '                            End If
    '                        Else
    '                            'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60M0000", ETX) 'Ū��M0-M96
    '                            PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60M0000", ETX))
    '                            Comm_StatePLC = 4
    '                        End If
    '                    End If
    '                End If
    '            Case 31
    '                'Readstatus
    '                If InStr(Rx_msgPLC, ETX) > 0 Then
    '                    If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                        PLCWatchDog = PLCWatchDogTimeSet
    '                        Rx_msgPLC = ""
    '                        FBPLC_ACK = False
    '                        'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60M0000", ETX) 'Ū��M0-M96
    '                        PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60M0000", ETX))
    '                        Comm_StatePLC = 4
    '                    End If
    '                End If
    '            Case 4
    '                If InStr(Rx_msgPLC, ETX) > 0 Then
    '                    If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                        PLCWatchDog = PLCWatchDogTimeSet
    '                        UpDataPLC_FB(2)
    '                        Rx_msgPLC = ""
    '                        FBPLC_ACK = False
    '                        If SQTailPLC <> SQHeadPLC Then
    '                            SQTailPLC = (SQTailPLC + 1) Mod PLCSetMaxCount
    '                            If PLCSet(SQTailPLC).PLCSetType = 1 Then
    '                                WriteMToPLCFB()
    '                                Comm_StatePLC = 41
    '                            End If
    '                            If PLCSet(SQTailPLC).PLCSetType = 2 Then
    '                                WriteDToPLCFB()
    '                                Comm_StatePLC = 41
    '                            End If
    '                        Else
    '                            'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01000", ETX) 'Ū��R1000-R1031
    '                            PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01000", ETX))
    '                            Comm_StatePLC = 5
    '                        End If
    '                    End If
    '                End If
    '            Case 41
    '                'Readstatus
    '                If InStr(Rx_msgPLC, ETX) > 0 Then
    '                    If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                        PLCWatchDog = PLCWatchDogTimeSet
    '                        Rx_msgPLC = ""
    '                        FBPLC_ACK = False
    '                        'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01000", ETX) 'Ū��R1000-R1031
    '                        PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01000", ETX))
    '                        Comm_StatePLC = 5
    '                    End If
    '                End If
    '            Case 5
    '                If InStr(Rx_msgPLC, ETX) > 0 Then
    '                    If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                        PLCWatchDog = PLCWatchDogTimeSet
    '                        UpDataPLC_FB(3)
    '                        Rx_msgPLC = ""
    '                        FBPLC_ACK = False
    '                        If SQTailPLC <> SQHeadPLC Then
    '                            SQTailPLC = (SQTailPLC + 1) Mod PLCSetMaxCount
    '                            If PLCSet(SQTailPLC).PLCSetType = 1 Then
    '                                WriteMToPLCFB()
    '                                Comm_StatePLC = 51
    '                            End If
    '                            If PLCSet(SQTailPLC).PLCSetType = 2 Then
    '                                WriteDToPLCFB()
    '                                Comm_StatePLC = 51
    '                            End If
    '                        Else
    '                            'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01100", ETX) 'Ū��R1100-R1131
    '                            PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01100", ETX))
    '                            Comm_StatePLC = 6
    '                        End If
    '                    End If
    '                End If
    '            Case 51
    '                'Readstatus
    '                If InStr(Rx_msgPLC, ETX) > 0 Then
    '                    If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                        PLCWatchDog = PLCWatchDogTimeSet
    '                        Rx_msgPLC = ""
    '                        FBPLC_ACK = False
    '                        'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01100", ETX) 'Ū��R1100-R1131
    '                        PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01100", ETX))
    '                        Comm_StatePLC = 6
    '                    End If
    '                End If
    '            Case 6
    '                If InStr(Rx_msgPLC, ETX) > 0 Then
    '                    If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                        PLCWatchDog = PLCWatchDogTimeSet
    '                        UpDataPLC_FB(5)
    '                        Rx_msgPLC = ""
    '                        FBPLC_ACK = False
    '                        If SQTailPLC <> SQHeadPLC Then
    '                            SQTailPLC = (SQTailPLC + 1) Mod PLCSetMaxCount
    '                            If PLCSet(SQTailPLC).PLCSetType = 1 Then
    '                                WriteMToPLCFB()
    '                                Comm_StatePLC = 61
    '                            End If
    '                            If PLCSet(SQTailPLC).PLCSetType = 2 Then
    '                                WriteDToPLCFB()
    '                                Comm_StatePLC = 61
    '                            End If
    '                        Else
    '                            'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01032", ETX) 'Ū��R1032-R1095
    '                            PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01032", ETX))
    '                            Comm_StatePLC = 7
    '                        End If
    '                    End If
    '                End If
    '            Case 61
    '                'Readstatus
    '                If InStr(Rx_msgPLC, ETX) > 0 Then
    '                    If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                        PLCWatchDog = PLCWatchDogTimeSet
    '                        Rx_msgPLC = ""
    '                        FBPLC_ACK = False
    '                        'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01032", ETX) 'Ū��R1032-R1095
    '                        PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01032", ETX))
    '                        Comm_StatePLC = 7
    '                    End If
    '                End If
    '            Case 7
    '                If InStr(Rx_msgPLC, ETX) > 0 Then

    '                    If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                        PLCWatchDog = PLCWatchDogTimeSet
    '                        UpDataPLC_FB(6)
    '                        Rx_msgPLC = ""
    '                        FBPLC_ACK = False
    '                        CommLivePLC = True
    '                        If SQTailPLC <> SQHeadPLC Then
    '                            SQTailPLC = (SQTailPLC + 1) Mod PLCSetMaxCount
    '                            If PLCSet(SQTailPLC).PLCSetType = 1 Then
    '                                WriteMToPLCFB()
    '                                Comm_StatePLC = 71
    '                            End If
    '                            If PLCSet(SQTailPLC).PLCSetType = 2 Then
    '                                WriteDToPLCFB()
    '                                Comm_StatePLC = 71
    '                            End If
    '                        Else
    '                            'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01132", ETX) 'Ū��R1132-R1195
    '                            PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01132", ETX))
    '                            Comm_StatePLC = 8
    '                        End If
    '                    End If
    '                End If
    '            Case 71
    '                'Readstatus
    '                If InStr(Rx_msgPLC, ETX) > 0 Then
    '                    If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                        PLCWatchDog = PLCWatchDogTimeSet
    '                        Rx_msgPLC = ""
    '                        FBPLC_ACK = False
    '                        'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01132", ETX) 'Ū��R1132-R1195
    '                        PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01132", ETX))
    '                        Comm_StatePLC = 8
    '                    End If
    '                End If
    '            Case 8
    '                If InStr(Rx_msgPLC, ETX) > 0 Then
    '                    If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                        PLCWatchDog = PLCWatchDogTimeSet
    '                        UpDataPLC_FB(7)
    '                        Rx_msgPLC = ""
    '                        FBPLC_ACK = False
    '                        If SQTailPLC <> SQHeadPLC Then
    '                            SQTailPLC = (SQTailPLC + 1) Mod PLCSetMaxCount
    '                            If PLCSet(SQTailPLC).PLCSetType = 1 Then
    '                                WriteMToPLCFB()
    '                                Comm_StatePLC = 81
    '                            End If
    '                            If PLCSet(SQTailPLC).PLCSetType = 2 Then
    '                                WriteDToPLCFB()
    '                                Comm_StatePLC = 81
    '                            End If
    '                        Else
    '                            'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01200", ETX) 'Ū��R1200-R1263 -> PLC_R_READ(96~159)
    '                            PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01200", ETX))
    '                            Comm_StatePLC = 9
    '                        End If
    '                    End If
    '                End If
    '            Case 81
    '                'Readstatus
    '                If InStr(Rx_msgPLC, ETX) > 0 Then
    '                    If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                        PLCWatchDog = PLCWatchDogTimeSet
    '                        Rx_msgPLC = ""
    '                        FBPLC_ACK = False
    '                        'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01200", ETX) 'Ū��R1200-R1263 -> PLC_R_READ(96~159)
    '                        PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01200", ETX))
    '                        Comm_StatePLC = 9
    '                    End If
    '                End If
    '            Case 9
    '                If InStr(Rx_msgPLC, ETX) > 0 Then

    '                    If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                        PLCWatchDog = PLCWatchDogTimeSet
    '                        UpDataPLC_FB(8)
    '                        Rx_msgPLC = ""
    '                        FBPLC_ACK = False
    '                        If SQTailPLC <> SQHeadPLC Then
    '                            SQTailPLC = (SQTailPLC + 1) Mod PLCSetMaxCount
    '                            If PLCSet(SQTailPLC).PLCSetType = 1 Then
    '                                WriteMToPLCFB()
    '                                Comm_StatePLC = 91
    '                            End If
    '                            If PLCSet(SQTailPLC).PLCSetType = 2 Then
    '                                WriteDToPLCFB()
    '                                Comm_StatePLC = 91
    '                            End If
    '                        Else
    '                            'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01300", ETX) 'Ū��R1300-R1363 -> PLC_R_SET(96~159)
    '                            PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01300", ETX)) 'Ū��X0-X47

    '                            Comm_StatePLC = 10
    '                        End If
    '                    End If
    '                End If
    '            Case 91
    '                'Readstatus
    '                If InStr(Rx_msgPLC, ETX) > 0 Then
    '                    If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                        PLCWatchDog = PLCWatchDogTimeSet
    '                        Rx_msgPLC = ""
    '                        FBPLC_ACK = False
    '                        'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01300", ETX) 'Ū��R1300-R1363 -> PLC_R_SET(96~159)
    '                        PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "40R01300", ETX)) 'Ū��X0-X47
    '                        Comm_StatePLC = 10
    '                    End If
    '                End If
    '            Case 10
    '                If InStr(Rx_msgPLC, ETX) > 0 Then

    '                    If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                        PLCWatchDog = PLCWatchDogTimeSet
    '                        UpDataPLC_FB(9)
    '                        Rx_msgPLC = ""
    '                        FBPLC_ACK = False
    '                        If SQTailPLC <> SQHeadPLC Then
    '                            SQTailPLC = (SQTailPLC + 1) Mod PLCSetMaxCount
    '                            If PLCSet(SQTailPLC).PLCSetType = 1 Then
    '                                WriteMToPLCFB()
    '                                Comm_StatePLC = 101
    '                            End If
    '                            If PLCSet(SQTailPLC).PLCSetType = 2 Then
    '                                WriteDToPLCFB()
    '                                Comm_StatePLC = 101
    '                            End If
    '                        Else
    '                            'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01364", ETX) 'Ū��R1364-R1396 -> PLC_R_SET(160~192)
    '                            PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01364", ETX)) 'Ū��R1364-R1396

    '                            Comm_StatePLC = 11
    '                        End If
    '                    End If
    '                End If
    '            Case 101
    '                'Readstatus
    '                If InStr(Rx_msgPLC, ETX) > 0 Then
    '                    If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                        PLCWatchDog = PLCWatchDogTimeSet
    '                        Rx_msgPLC = ""
    '                        FBPLC_ACK = False
    '                        'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01364", ETX) 'Ū��R1364-R1396 -> PLC_R_SET(160~192)
    '                        PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADREGISTERS, "20R01364", ETX)) 'Ū��R1364-R1396

    '                        Comm_StatePLC = 11
    '                    End If
    '                End If
    '            Case 11
    '                If InStr(Rx_msgPLC, ETX) > 0 Then
    '                    'plccount = plccount + 1
    '                    If (Mid(Rx_msgPLC, 6, 1) = "0") Then
    '                        PLCWatchDog = PLCWatchDogTimeSet
    '                        UpDataPLC_FB(10)
    '                        Rx_msgPLC = ""
    '                        FBPLC_ACK = False
    '                        CommLivePLC = True
    '                        If SQTailPLC <> SQHeadPLC Then
    '                            SQTailPLC = (SQTailPLC + 1) Mod PLCSetMaxCount
    '                            If PLCSet(SQTailPLC).PLCSetType = 1 Then
    '                                WriteMToPLCFB()
    '                                Comm_StatePLC = 1
    '                            End If
    '                            If PLCSet(SQTailPLC).PLCSetType = 2 Then
    '                                WriteDToPLCFB()
    '                                Comm_StatePLC = 1
    '                            End If
    '                        Else
    '                            'PLCComm.Output = PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60X0000", ETX)  'Ū��X0-X47
    '                            PLCComm.Write(PLCSendCmd(STX, Slaveno, CmdREADPOINTS, "60X0000", ETX)) 'Ū��X0-X47
    '                            Comm_StatePLC = 2
    '                        End If
    '                    End If
    '                End If

    '        End Select
    '        'Debug.Print("CommState:" & Comm_StatePLC.ToString)
    '        'System.Threading.Thread.Sleep(5)
    '    End If
    '    'Next i

    'End Sub

    'PLC �ǤJ��ƸѽX FATEK
    Public Sub UpDataPLC_FB(ByVal InType As Integer)
        Dim i As Integer
        Dim iValue As String
        Dim jValue As Integer
        Dim j As Integer
        Dim x As String
        Dim y As String
        Static Interlock2Index As Byte
        Dim k As String
        Select Case InType
            Case 0 ' PLC X updata  
                iValue = Mid(Rx_msgPLC, 7, 96)
                For i = 0 To 95
                    x = Mid(iValue, i + 1, 1)
                    If x <> "0" Then
                        If x <> "1" Then
                            Exit Select
                        End If
                    End If
                Next
                For i = 0 To 95
                    If PLC_X(i) <> Mid(iValue, i + 1, 1) Then
                        PLC_XCheckCount(i) = PLC_XCheckCount(i) + 1
                        If PLC_XCheckCount(i) = 2 Then
                            PLC_X(i) = Mid(iValue, i + 1, 1)
                            x = PLC_X(i)
                            X_Changed(i) = True
                            PLC_XCheckCount(i) = 0
                        End If
                    End If
                Next i

            Case 1 'PLC Y updata
                iValue = Mid(Rx_msgPLC, 7, 96)
                For i = 0 To 95
                    y = Mid(iValue, i + 1, 1)
                    If y <> "0" Then
                        If y <> "1" Then
                            Exit Select
                        End If
                    End If
                Next
                For i = 0 To 95
                    If PLC_Y(i) <> Mid(iValue, i + 1, 1) Then
                        Y_Changed(i) = True
                    End If
                    PLC_Y(i) = Mid(iValue, i + 1, 1)
                Next i

            Case 2 'PLC M updata
                iValue = Mid(Rx_msgPLC, 7, 96)
                For i = 0 To 95
                    PLC_M(i) = Mid(iValue, i + 1, 1)
                Next i
                If FirstPLCConnect = False Then         '��l�ơC
                    For i = 0 To 95
                        DeviceM(i).SettingCmd = PLC_M(i)
                    Next i
                    FirstPLCConnect = True
                End If
            Case 3 'PLC R1000-R1032 updata  
                iValue = Mid(Rx_msgPLC, 7, 128)
                'Debug.Print("R1000-R1031_Len=" + Len(Rx_msgPLC).ToString())
                For i = 0 To 124 Step 4
                    j = i / 4
                    PLC_R_READ(j) = Format(Val("&H" + Mid(iValue, i + 1, 4)))
                Next i

            Case 5 'PLC R1100 -R1131 updata  
                iValue = Mid(Rx_msgPLC, 7, 128)
                For i = 0 To 124 Step 4
                    j = i / 4
                    PLC_R_SetRead(j) = Format(Val("&H" + Mid(iValue, i + 1, 4)))
                Next i

            Case 6 'PLC R1032-1095 UPDATA PLC R1020-1059 UPDATA
                iValue = Mid(Rx_msgPLC, 7, 256)
                For i = 0 To 252 Step 4
                    j = i / 4 + 32
                    PLC_R_READ(j) = Format(Val("&H" + Mid(iValue, i + 1, 4)))
                Next i

            Case 7 'PLC R1132-1195 UPDATA
                iValue = Mid(Rx_msgPLC, 7, 256)
                For i = 0 To 252 Step 4
                    j = i / 4 + 32
                    PLC_R_SetRead(j) = Format(Val("&H" + Mid(iValue, i + 1, 4)))
                Next i
                j = 0
            Case 8 'PLC R1200-R1263 UPDATA
                iValue = Mid(Rx_msgPLC, 7, 256)
                For i = 0 To 252 Step 4
                    j = i / 4 + 96
                    PLC_R_READ(j) = Format(Val("&H" + Mid(iValue, i + 1, 4)))
                Next i
            Case 9 'PLC R1300-1363 
                iValue = Mid(Rx_msgPLC, 7, 256)
                For i = 0 To 252 Step 4
                    j = i / 4 + 96
                    PLC_R_SetRead(j) = Format(Val("&H" + Mid(iValue, i + 1, 4)))
                Next i
            Case 10 'PLC R1364-1396
                iValue = Mid(Rx_msgPLC, 7, 128)
                For i = 0 To 124 Step 4
                    j = i / 4 + 160
                    PLC_R_SetRead(j) = Format(Val("&H" + Mid(iValue, i + 1, 4)))
                Next i
        End Select
    End Sub
    'PLC �ǤJ��ƸѽX FX
    Public Sub UpDataPLC_FX(ByVal InType As Integer)
        Dim i As Integer
        Dim iValue As String
        Dim jValue As Integer
        Dim j As Integer
        Dim x As String
        Dim y As String
        Static Interlock2Index As Byte
        Dim k As String

        Select Case InType

            Case 0 ' PLC X updata  
                iValue = Mid(Rx_msgPLC, 6, 96)
                For i = 0 To 95
                    x = Mid(iValue, i + 1, 1)
                    If x <> "0" Then
                        If x <> "1" Then
                            Exit Select
                        End If
                    End If
                Next
                For i = 0 To 95
                    If PLC_X(i) <> Mid(iValue, i + 1, 1) Then
                        PLC_XCheckCount(i) = PLC_XCheckCount(i) + 1
                        If PLC_XCheckCount(i) = 2 Then
                            PLC_X(i) = Mid(iValue, i + 1, 1)
                            x = PLC_X(i)
                            X_Changed(i) = True
                            PLC_XCheckCount(i) = 0
                        End If
                    End If
                Next i

            Case 1 'PLC Y updata
                iValue = Mid(Rx_msgPLC, 6, 96)
                For i = 0 To 95
                    y = Mid(iValue, i + 1, 1)
                    If y <> "0" Then
                        If y <> "1" Then
                            Exit Select
                        End If
                    End If
                Next
                For i = 0 To 95
                    If PLC_Y(i) <> Mid(iValue, i + 1, 1) Then
                        Y_Changed(i) = True
                    End If
                    PLC_Y(i) = Mid(iValue, i + 1, 1)
                Next i

            Case 2 'PLC M updata
                iValue = Mid(Rx_msgPLC, 6, 96)
                For i = 0 To 95
                    PLC_M(i) = Mid(iValue, i + 1, 1)
                Next i

                If FirstPLCConnect = False Then         '��l�ơC
                    For i = 0 To 95
                        DeviceM(i).SettingCmd = PLC_M(i)
                    Next i
                    FirstPLCConnect = True
                End If


            Case 3 'PLC R1000 updata
                iValue = Mid(Rx_msgPLC, 6, 128)
                For i = 0 To 124 Step 4
                    j = i / 4
                    PLC_R_READ(j) = Val("&H" + Mid(iValue, i + 1, 4))
                Next i

            Case 5 'PLC D1100 -R1132updata
                iValue = Mid(Rx_msgPLC, 6, 128)
                For i = 0 To 124 Step 4
                    j = i / 4
                    PLC_R_SetRead(j) = Format(Val("&H" + Mid(iValue, i + 1, 4)))
                Next i
            Case 6 'PLC D1032-D1063 UPDATA
                iValue = Mid(Rx_msgPLC, 6, 256)
                For i = 0 To 252 Step 4
                    j = i / 4 + 32
                    PLC_R_READ(j) = Val("&H" + Mid(iValue, i + 1, 4))
                Next i

            Case 7 'PLC D1132-D1163 UPDATA
                iValue = Mid(Rx_msgPLC, 6, 256)
                For i = 0 To 252 Step 4
                    j = i / 4 + 32
                    PLC_R_SetRead(j) = Format(Val("&H" + Mid(iValue, i + 1, 4)))
                Next i
        End Select
    End Sub

    '��s M���
    Public Sub UpDataDigital()
        Dim DigitalChannelValue As String
        Dim DigitalUpData1 As Boolean
        Dim i As Integer
        Dim j As Integer

        If CommLivePLC = True Then
            For i = 0 To 95
                DigitalChannelValue = ""
                If DeviceM(i).StatusUpdated Then
                    DigitalUpData1 = True
                    DeviceM(i).StatusUpdated = False
                End If
                If DeviceM(i).SettingCmd = "1" Then
                    DigitalChannelValue = "1"
                Else
                    DigitalChannelValue = "0"
                End If
                If DigitalUpData1 Then
                    If bolonebyone = True Then

                        SQHeadPLC = (SQHeadPLC + 1) Mod PLCSetMaxCount
                        PLCSet(SQHeadPLC).PLCSetType = 1
                        PLCSet(SQHeadPLC).PLCSetCH = i
                        PLCSet(SQHeadPLC).PLCSetValue = DigitalChannelValue
                    Else
                        M_SQHeadPLC = (M_SQHeadPLC + 1) Mod PLCMSetMaxCount
                        PLCMSet(M_SQHeadPLC).PLCMSetCH = i
                        PLCMSet(M_SQHeadPLC).PLCMSetValue = DigitalChannelValue
                        DigitalUpData1 = False
                    End If
                End If
            Next i
        End If
    End Sub

    '�g�J PLC �Ȧs�� --> �ƶi��C��
    Public Sub PLCRUpData(ByVal ch As Integer)
        Dim Val As Integer
        If CommLivePLC = True Then
            If bolonebyone = True Then
                SQHeadPLC = (SQHeadPLC + 1) Mod PLCSetMaxCount
                PLCSet(SQHeadPLC).PLCSetType = 2
                PLCSet(SQHeadPLC).PLCSetCH = ch
                PLCSet(SQHeadPLC).PLCSetValue = Format((PLCRValue(ch).RValue), "0000")
            Else
                R_SQHeadPLC = (R_SQHeadPLC + 1) Mod PLCRSetMaxCount
                'PLCSet(R_SQHeadPLC).PLCSetType = 2
                PLCRSet(R_SQHeadPLC).PLCRSetCH = ch
                'PLCRSet(R_SQHeadPLC).PLCRSetValue = Format((PLCRValue(ch).RValue), "0000")
                If R_R1100_Read.TryGetValue(ch, Val) Then
                    PLCRSet(R_SQHeadPLC).PLCRSetValue = Format(Val, "0000")
                End If
            End If
        End If
    End Sub

End Module
