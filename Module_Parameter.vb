Imports System.Runtime.InteropServices

Module Module_Parameter
    Public ColorOn As Color = Color.Lime
    Public ColorOff As Color = Color.FromArgb(255, 255, 192)


    '�q�y�ʱ�
    Public MonitorCurrent As Double
    Public MonitorPower As Double
    Public MonitorCurrentLimit As Double
    '�ؿ����ɦW�]�w
    'Public ProgramDir As String
    Public PersonDir As String                  '�ϥΪ��v���ɮ�
    Public AlarmRecordDir As String             '���` �O����Ƨ�
    Public OperatorRecordDir As String          '�ϥΪ� �O����Ƨ�
    Public PurgeGasRecordDir As String          'PURGE �O����Ƨ�
    Public WorkingDir As String                 '�ثe�u�@�ؿ�   
    Public ProgramDir As String                 '�ثe�u�@�ؿ�   
    Public ProcessRecordDir As String           '�s�{�O����Ƨ�
    Public ProcessRecordCurveDir As String      '�s�{�O�����u��Ƨ�
    Public ProcessEeventDir As String           '�s�{�ƥ�O����Ƨ�
    Public ProcessCSVDir As String           '�s�{�ƥ�O����Ƨ�
    Public RecipeDir As String                  '�t���Ƨ�
    Public PIDsDir As String                    'PID �O����Ƨ�
    Public VideoRecordDir As String             '���v�O����Ƨ�
    Public JPGDir As String                     '�ϥΪ��v���ɮ�
    Public JPGFileName As String                  '�ϥΪ��v���ɮ�

    Public RecipeLongFileName As String
    Public RecipeFileName As String
    Public RecipeName As String

    Public ProcessRecipeLongFileName As String
    Public ProcessRecipeFileName As String
    Public ProcessRecipeName As String



    Public ProcessPN As String
    Public ParaVacuumMode As Integer
    Public ParaSyncMode As Integer
    Public ProcessMode_RUN As Boolean
    Public ProcessMode_Abort As Boolean
    Public ProcessNormalEnd As Boolean 'Add by Vincent TCPIP 20200716  

    Public LangStr() As String = {"_CHT", "_CHS", "ENG"}
    Public total_Process_Num As Integer
    Public ProcessStatusList(2, 99) As String


    '�t�ΰѼ�
    <StructLayout(LayoutKind.Sequential)> Structure SystemParameterss
        '�ű����Ѽ�-----------------------------------START
        '�T�ӽd�� PID�]�w'
        Public TopP1 As String
        Public TopP2 As String
        Public TopP3 As String
        Public BotP1 As String
        Public BotP2 As String
        Public BotP3 As String
        Public TopI1 As String
        Public TopI2 As String
        Public TopI3 As String
        Public BotI1 As String
        Public BotI2 As String
        Public BotI3 As String
        Public TopD1 As String
        Public TopD2 As String
        Public TopD3 As String
        Public BotD1 As String
        Public BotD2 As String
        Public BotD3 As String
        '�T�ӽd�� �̤j/�̤p�\�v�]�w
        Public TopMax1 As String
        Public TopMax2 As String
        Public TopMax3 As String
        Public BotMax1 As String
        Public BotMax2 As String
        Public BotMax3 As String
        Public TopMin1 As String
        Public TopMin2 As String
        Public TopMin3 As String
        Public BotMin1 As String
        Public BotMin2 As String
        Public BotMin3 As String
        '�d�����
        Public TopLowRange As String
        Public TopHighRange As String
        Public BotLowRange As String
        Public BotHighRange As String
        '�C�Ůɳ̤j��X
        Public TopLimitPowerTemp As String
        Public TopLimitPower As String
        Public BotLimitPowerTemp As String
        Public BotLimitPower As String
        '���y�]�w
        Public TopFlowMeterHz() As String
        Public BotFlowMeterHz() As String
        Public LowFlowAlarm As String
        '�ű����Ѽ�-----------------------------------END




        '�ū׮ե�-----------------------------------START
        '�W�ū׮ե�
        Public TopTempCal1 As String
        Public TopTempCal2 As String
        Public TopTempCal3 As String
        Public TopTempCal4 As String
        Public TopTempCal5 As String
        Public TopTempCalX1 As String
        Public TopTempCalX2 As String
        Public TopTempCalX3 As String
        Public TopTempCalX4 As String
        Public TopTempCalX5 As String
        '�U�ū׮ե�
        Public BotTempCal1 As String
        Public BotTempCal2 As String
        Public BotTempCal3 As String
        Public BotTempCal4 As String
        Public BotTempCal5 As String
        Public BotTempCalX1 As String
        Public BotTempCalX2 As String
        Public BotTempCalX3 As String
        Public BotTempCalX4 As String
        Public BotTempCalX5 As String
        '�ū׮ե�-----------------------------------END

        '���O�ե�-----------------------------------START
        '���O�ե�
        Public PressureCal1 As String
        Public PressureCal2 As String
        Public PressureCal3 As String
        Public PressureCal4 As String
        Public PressureCal5 As String

        Public PressureCalX1 As String
        Public PressureCalX2 As String
        Public PressureCalX3 As String
        Public PressureCalX4 As String
        Public PressureCalX5 As String

        Public PressFullScale As String
        Public PressZero As String

        '���O�ײv
        Public PressureRamp As String
        '���O������ (�`�ƶ�)
        Public PressOffset As String
        '����
        Public PressureNet As String

        '���O�ե�-----------------------------------END
        '�����{�� ------------------------------------------------
        Public KgDARatio As String  '���O/�o����

        Public Ctrl1stRange As String  '���O/�o����
        Public Ctrl1stTime As String  '���O/�o����
        Public Ctrl2ndtRange As String  '���O/�o����
        Public Ctrl2ndtTime As String  '���O/�o����
        Public Ctrl1stDiv As String  '�����Ĥ@�q��X���O����

        Public ThresholdPress As String  '���O����
        Public ThresholdPressDiv As String  '���O���ɰ���
        Public ThresholdPressRatio As String  '���O���ɥH�W��X���

        '�������X�{�� ------------------------------------------------START
        '��Ĳ�̤j���O
        Public PlateUpPreset As String
        '��1�q���ODA
        Public Press1st As String
        '��2�q���ODA
        Public Press2nd As String
        '��l�K�X���O
        Public Press3rd As String

        '�������X ------------------------------------------------END


        'ĵ���]�w----------------------------------------------------
        '�����q�y����
        Public PumpCurrentLimit As String
        Public PumpCurrentAlarmTime As String
        '�}���N�o�ū�
        Public CoolingTemperature As String
        '����ĵ��
        Public HighTempLimit As String
        Public HighPressureLimit As String
        '���ĵ���ɶ�
        Public PumpingTime As String
        '�s�{���� ALARM TIME
        Public ProcessEndBZonTime As String
        '�s�{���� ALARM
        Public ProcessEndAlarm As String

        'ĵ���]�w----------------------------------------------------


        '�u�ų��
        Public PressureUnit As String
        '�u�Ÿ��v
        Public VacuumOffset As String

        '�s�{���� ----------------------------------------------------START
        '�s�{�~�����O�d��
        Public PressureRange As String
        '�s�{�ū׽d��
        Public ProcessTempRange As String
        '���O�P�B--�s�{����
        'Public SyncPressure As String
        ''�ūצP�B--�s�{����
        'Public SyncTemp As String
        ''�ɶ��P�B--�s�{����
        'Public SyncTime As String
        Public PressAverage As String
        '�s�{���� ----------------------------------------------------END


        '�۰� �{��-------------------------------START
        '��u�Ůɦ۰� PURGE-------------------------------
        '�۰�PURGE
        'Public AutoPurge As String
        '�۰� PURGE �g��
        Public AutoPurgeCycle As String
        '�۰� PURGE ON �ɶ�
        Public AutoPurgeOnTime As String
        '�۰� PURGE OFF �ɶ�
        Public AutoPurgeOFFTime As String

        '�s�{���� ALARM
        'Public BondBeforeVacuum As String

        '��u�Ůɦ۰� PURGE-------------------------------

        '�����۰ʩ�u��
        'Public AutoVacuum As String
        '�s�{���۰ʩ�u��--PUMP�|��
        Public ProcessAutoVacuum As String
        Public Vac1ATM_Select As String

        '��u�Ůɦ۰ʯu�ŭ�
        Public ProcessVacuumAutoVac As String
        '��u�Ůɦ۰ʯu�ŭ�
        ' Modified 990303 -Start
        Public ProcessVacuumAutoVacLo As String
        'VENT �ɶ�
        Public VentTime As String
        '������۰� VENT(�}��)
        'Public AutoVent As String
        '�s�{���_����
        Public AbortPressureRange As String
        Public AbortPressureRangeFlag As String
        Public AbortTempRange As String
        Public AbortTempRangeFlag As String

        '�۰� �{��-------------------------------END
        Public HeaterCount As String
        Public HeaterPower As String
        Public HeaterVoltage As String
        Public HeaterMonitorPower As String
        Public HeaterCurrentRate As String

        '�o������m
        Public Position01Set As String
        Public Position02Set As String
        Public Position03Set As String
        Public Position04Set As String
        Public Position05Set As String
        Public Position06Set As String

        Public DistanceSet As String

        Public BarcodeOnly As String
        Public AutoRecordData As String
        Public SplitTopBotTemp As String 'Add  by Vincent 20180419  ------------------- Start
        Public SplitTopBotTempEnable As String
        Public RunConfirm As String 'Add  by Claire 20230620 
        Public WebPath As String
        'Add  by Vincent 20181016  ���O�׾�\�� ------------------- Start
        Public PressureAverageTimes As String
        Public PressureAverageEnable As String
        Public PeakClearEnable As String
        Public PressureAdjust As String
        Public PeakLimit As String
        Public PeakTimes As String
        'Add  by Vincent 20181016  ���O�׾�\�� ------------------- End
        Public VentOffDelay As String
        Public RPONPressure As String
        Public DPWaterFlowHz As String
        Public DPWaterFlowHzMax As String
        Public DPWaterFlowAlarm As String
        'Add  by claire 20230620  �[�ʲv�\�� ------------------- End
        Public StartLog_Time As String
        Public bolDAlog As Boolean '�bLog�ɬO�_�n����DA��
    End Structure
    Public SystemParameters As SystemParameterss

    <StructLayout(LayoutKind.Sequential)> Structure ProcessRecord
        Public ProcessStep As String
        Public ProcessTime As String
        Public StepTime As String
        Public TopTemperature As String
        Public BotTemperature As String
        Public TopCurrent As String
        Public BotCurrent As String
        Public BondingPressure As String
        Public DCPower As String
        Public DCCurrent As String
        Public DPCurrent As String
        Public Vacuum As String
    End Structure
    Public ProcessRecords As ProcessRecord
    Public ProcessRecordsIndex As Long
    Public ProcessRecordsIndex_222 As Long
    Public ProcessRecordsIndex1 As Long
    Public ProcessRecordShow As ProcessRecord

    Public Sub ReadParameterFromFile(ByVal sfile As String)

        SystemParameters.LowFlowAlarm = ReadProgData("PARAMETER", "LOWFLOWALARM", "60", sfile)
        SystemParameters.PressFullScale = ReadProgData("PARAMETER", "PRESSFULLSCALE", "15000", sfile)
        SystemParameters.PressZero = ReadProgData("PARAMETER", "PRESSZERO", "0", sfile)
        SystemParameters.PressureRamp = ReadProgData("PARAMETER", "PRESSURERAMP", "0", sfile)
        SystemParameters.PressOffset = ReadProgData("PARAMETER", "PRESSOFFSET", "0", sfile)
        SystemParameters.PressureNet = ReadProgData("PARAMETER", "PRESSURENET", "0", sfile)
        SystemParameters.KgDARatio = ReadProgData("PARAMETER", "KGDARATIO", "0", sfile)
        SystemParameters.Ctrl1stRange = ReadProgData("PARAMETER", "CTRL1STRANGE", "0", sfile)
        SystemParameters.Ctrl1stTime = ReadProgData("PARAMETER", "CTRL1STTIME", "0", sfile)
        SystemParameters.Ctrl2ndtRange = ReadProgData("PARAMETER", "CTRL2NDTRANGE", "0", sfile)
        SystemParameters.Ctrl2ndtTime = ReadProgData("PARAMETER", "CTRL2NDTTIME", "0", sfile)
        SystemParameters.Ctrl1stDiv = ReadProgData("PARAMETER", "CTRL1STDIV", "0", sfile)
        SystemParameters.ThresholdPress = ReadProgData("PARAMETER", "THRESHOLDPRESS", "0", sfile)
        SystemParameters.ThresholdPressDiv = ReadProgData("PARAMETER", "THRESHOLDPRESSDIV", "0", sfile)
        SystemParameters.ThresholdPressRatio = ReadProgData("PARAMETER", "THRESHOLDPRESSRATIO", "0", sfile)
        SystemParameters.PressAverage = ReadProgData("PARAMETER", "PRESSAVERAGE", "0", sfile)
        SystemParameters.PlateUpPreset = ReadProgData("PARAMETER", "PLATEUPPRESET", "0", sfile)
        SystemParameters.Press1st = ReadProgData("PARAMETER", "PRESS1ST", "0", sfile)
        SystemParameters.Press2nd = ReadProgData("PARAMETER", "PRESS2ND", "0", sfile)
        SystemParameters.Press3rd = ReadProgData("PARAMETER", "PRESS3RD", "0", sfile)
        SystemParameters.PumpCurrentLimit = ReadProgData("PARAMETER", "PUMPCURRENTLIMIT", "5", sfile)
        SystemParameters.PumpCurrentAlarmTime = ReadProgData("PARAMETER", "PUMPCURRENTALARMTIME", "10", sfile)
        FormParameters.txtDpCurrentLimit.Text = SystemParameters.PumpCurrentLimit
        FormParameters.txtDpCurrentAlarmTime.Text = SystemParameters.PumpCurrentAlarmTime

        SystemParameters.CoolingTemperature = ReadProgData("PARAMETER", "COOLINGTEMPERATURE", "0", sfile)
        SystemParameters.HighTempLimit = ReadProgData("PARAMETER", "HIGHTEMPLIMIT", "0", sfile)
        SystemParameters.HighPressureLimit = ReadProgData("PARAMETER", "HighPressureLimit", "16000", sfile)

        SystemParameters.PumpingTime = ReadProgData("PARAMETER", "PUMPINGTIME", "0", sfile)
        SystemParameters.ProcessEndBZonTime = ReadProgData("PARAMETER", "PROCESSENDBZONTIME", "0", sfile)
        SystemParameters.ProcessEndAlarm = ReadProgData("PARAMETER", "PROCESSENDALARM", "0", sfile)
        SystemParameters.PressureRange = ReadProgData("PARAMETER", "PRESSURERANGE", "0", sfile)
        SystemParameters.ProcessTempRange = ReadProgData("PARAMETER", "PROCESSTEMPRANGE", "0", sfile)
        'SystemParameters.AutoPurge = ReadProgData("PARAMETER", "AUTOPURGE", "0", sfile)
        SystemParameters.AutoPurgeCycle = ReadProgData("PARAMETER", "AUTOPURGECYCLE", "0", sfile)
        SystemParameters.AutoPurgeOnTime = ReadProgData("PARAMETER", "AUTOPURGEONTIME", "0", sfile)
        SystemParameters.AutoPurgeOFFTime = ReadProgData("PARAMETER", "AUTOPURGEOFFTIME", "0", sfile)
        SystemParameters.ProcessVacuumAutoVac = ReadProgData("PARAMETER", "PROCESSAUTOVACUUMVAC", "2.0E-2", sfile)
        SystemParameters.VentTime = ReadProgData("PARAMETER", "VENTTIME", "0", sfile)
        'SystemParameters.AutoVacuum = ReadProgData("PARAMETER", "AUTOVACUUM", "0", sfile)
        SystemParameters.ProcessAutoVacuum = ReadProgData("PARAMETER", "PROCESSAUTOVACUUM", "0", sfile)
        SystemParameters.ProcessVacuumAutoVacLo = ReadProgData("PARAMETER", "PROCESSAUTOVACUUMVACLO", "1.0E-01", sfile)
        SystemParameters.Vac1ATM_Select = ReadProgData("PARAMETER", "VAC1ATMSELECT", "0", sfile)
        Vac1ATM_Select = GetTrue01Boolean(SystemParameters.Vac1ATM_Select)

        '----------------------------------------

        ReadHeaterCalData(ParameterINIFile)       'Ū���ū׮ե����

        ProcessOkALMEnabled_Status = GetTrue01Boolean(SystemParameters.ProcessEndAlarm)
        ProcessAutoVacuum_Status = GetTrue01Boolean(SystemParameters.ProcessAutoVacuum)

        SystemParameters.HeaterCount = ReadProgData("PARAMETER", "HEATERCOUNT", "4", sfile)
        SystemParameters.HeaterPower = ReadProgData("PARAMETER", "HEATERPOWER", "750", sfile)
        SystemParameters.HeaterVoltage = ReadProgData("PARAMETER", "HEATERVOLTAGE", "220", sfile)
        SystemParameters.HeaterMonitorPower = ReadProgData("PARAMETER", "HEATERMONITORPOWER", "60", sfile)
        SystemParameters.HeaterCurrentRate = ReadProgData("PARAMETER", "HEATERCURRENTRATE", "80", sfile)
        If Val(SystemParameters.HeaterVoltage) <= 0 Then SystemParameters.HeaterVoltage = "220"
        MonitorPower = Val(SystemParameters.HeaterMonitorPower)
        MonitorCurrent = Val(SystemParameters.HeaterCount) * Val(SystemParameters.HeaterPower) / Val(SystemParameters.HeaterVoltage)
        FormParameters.lblFullCurrent.Text = Format(MonitorCurrent * MonitorPower / 100, "0.00")
        MonitorCurrentLimit = MonitorCurrent * Val(SystemParameters.HeaterCurrentRate) / 100
        FormParameters.lblHeaterCurrent.Text = Format(MonitorCurrentLimit, "0.00")

        '�s�{���_�]�w
        SystemParameters.AbortPressureRange = ReadProgData("PARAMETER", "ABORTPRESSURERANGE", "100", sfile)
        SystemParameters.AbortTempRange = ReadProgData("PARAMETER", "ABORTTEMPRANGE", "30", sfile)

        SystemParameters.AbortPressureRangeFlag = ReadProgData("PARAMETER", "ABORTPRESSURERANGEFLAG", "0", sfile)
        SystemParameters.AbortTempRangeFlag = ReadProgData("PARAMETER", "ABORTTEMPRANGEFLAG", "0", sfile)

        FormParameters.txtAbortPressureRange.Text = SystemParameters.AbortPressureRange
        FormParameters.txtAbortTempRange.Text = SystemParameters.AbortTempRange

        FormParameters.chkPressureAbort.Checked = GetTrue01Boolean(SystemParameters.AbortPressureRangeFlag)
        FormParameters.chkTempAbort.Checked = GetTrue01Boolean(SystemParameters.AbortTempRangeFlag)

        '�o����{�]�w 99.02.03  start
        SystemParameters.Position01Set = ReadProgData("PARAMETER", "POSITIONSET01", "45", sfile)
        SystemParameters.Position02Set = ReadProgData("PARAMETER", "POSITIONSET02", "45", sfile)
        SystemParameters.Position03Set = ReadProgData("PARAMETER", "POSITIONSET03", "45", sfile)
        SystemParameters.Position04Set = ReadProgData("PARAMETER", "POSITIONSET04", "45", sfile)
        SystemParameters.Position05Set = ReadProgData("PARAMETER", "POSITIONSET05", "45", sfile)
        SystemParameters.Position06Set = ReadProgData("PARAMETER", "POSITIONSET06", "45", sfile)

        FormParameters.txtPosition01Set.Text = SystemParameters.Position01Set
        FormParameters.txtPosition02Set.Text = SystemParameters.Position02Set
        FormParameters.txtPosition03Set.Text = SystemParameters.Position03Set
        FormParameters.txtPosition04Set.Text = SystemParameters.Position04Set
        FormParameters.txtPosition05Set.Text = SystemParameters.Position05Set
        FormParameters.txtPosition06Set.Text = SystemParameters.Position06Set

        SystemParameters.DistanceSet = ReadProgData("PARAMETER", "DISTANCESET", "90", sfile)
        FormParameters.txtDistanceSet.Text = SystemParameters.DistanceSet
        '�o����{�]�w 99.02.03  End

        SystemParameters.BarcodeOnly = ReadProgData("PARAMETER", "BarcodeOnly", "0", sfile)
        SystemParameters.AutoRecordData = ReadProgData("PARAMETER", "AutoRecordData", "0", sfile)
        SystemParameters.SplitTopBotTemp = ReadProgData("PARAMETER", "SplitTopBotTemp", "0", sfile) 'Add  by Vincent 20180419  ------------------- Start
        SystemParameters.SplitTopBotTempEnable = ReadProgData("PARAMETER", "SplitTopBotTempEnable", "0", sfile)

        'WriteParameterToFile(sfile)
        'Add  by Vincent 20181016  ���O�׾�\�� ------------------- Start
        SystemParameters.PressureAverageTimes = ReadProgData("PARAMETER", "PressureAverageTimes", "1", sfile)
        SystemParameters.PressureAverageEnable = ReadProgData("PARAMETER", "PressureAverageEnable", "0", sfile)
        SystemParameters.PeakClearEnable = ReadProgData("PARAMETER", "PeakClearEnable", "0", sfile)
        SystemParameters.PressureAdjust = ReadProgData("PARAMETER", "PressureAdjust", "0", sfile)
        SystemParameters.PeakLimit = ReadProgData("PARAMETER", "PeakLimit", "0", sfile)
        SystemParameters.PeakTimes = ReadProgData("PARAMETER", "PeakTimes", "3", sfile)
        'Add  by Vincent 20181016  ���O�׾�\�� ------------------- End

        SystemParameters.VentOffDelay = ReadProgData("PARAMETER", "VentOffDelay", "10", sfile)  'Add  by Vincent 20210318
        SystemParameters.RPONPressure = ReadProgData("PARAMETER", "RPONPressure", "50", sfile)  'Add  by Vincent 20210318
        SystemParameters.DPWaterFlowHz = ReadProgData("PARAMETER", "DPWaterFlowHz", "135", sfile)  'Add  by Vincent 20210318
        SystemParameters.DPWaterFlowHzMax = ReadProgData("PARAMETER", "DPWaterFlowHzMax", "20", sfile)  'Add  by Vincent 20210318
        SystemParameters.DPWaterFlowAlarm = ReadProgData("PARAMETER", "DPWaterFlowAlarm", "4.0", sfile)  'Add  by Vincent 20210318

        SystemParameters.StartLog_Time = ReadProgData("PARAMETER", "StartLog_Time", "0", sfile)  'Add  by claire 20230620
        SystemParameters.RunConfirm = ReadProgData("PARAMETER", "RunConfirm", "0", sfile)
        SystemParameters.WebPath = ReadProgData("PARAMETER", "WebPath", "", sfile)
        If (SystemParameters.WebPath = "") Then MessageBox.Show("���ˬd���� �[�ʲvLog�s�ɦ�m")
        If Val(SystemParameters.DPWaterFlowHz) <= 0 Then
            SystemParameters.DPWaterFlowHz = "135"
        End If
        If Val(SystemParameters.DPWaterFlowHzMax) <= 0 Then
            SystemParameters.DPWaterFlowHzMax = "20"
        End If
        SystemParameters.bolDAlog = ReadProgData("PARAMETER", "DALog", "0", sfile)  'Add  by claire 20230901



    End Sub
    '�N��Ƶ��c�g�J�ɮפ�
    Public Sub WriteParameterToFile(ByVal sfile As String)
        Dim i As Integer


        WriteProgData("PARAMETER", "LOWFLOWALARM", SystemParameters.LowFlowAlarm, sfile)

        WriteHeaterCalData(sfile)
        WritePressCalData(sfile)
        WriteProgData("PARAMETER", "PRESSFULLSCALE", SystemParameters.PressFullScale, sfile)
        WriteProgData("PARAMETER", "PRESSZERO", SystemParameters.PressZero, sfile)

        WriteProgData("PARAMETER", "PRESSURERAMP", SystemParameters.PressureRamp, sfile)
        WriteProgData("PARAMETER", "PRESSOFFSET", SystemParameters.PressOffset, sfile)
        WriteProgData("PARAMETER", "PRESSURENET", SystemParameters.PressureNet, sfile)
        WriteProgData("PARAMETER", "KGDARATIO", SystemParameters.KgDARatio, sfile)
        WriteProgData("PARAMETER", "CTRL1STRANGE", SystemParameters.Ctrl1stRange, sfile)
        WriteProgData("PARAMETER", "CTRL1STTIME", SystemParameters.Ctrl1stTime, sfile)
        WriteProgData("PARAMETER", "CTRL2NDTRANGE", SystemParameters.Ctrl2ndtRange, sfile)
        WriteProgData("PARAMETER", "CTRL2NDTTIME", SystemParameters.Ctrl2ndtTime, sfile)
        WriteProgData("PARAMETER", "CTRL1STDIV", SystemParameters.Ctrl1stDiv, sfile)
        WriteProgData("PARAMETER", "THRESHOLDPRESS", SystemParameters.ThresholdPress, sfile)
        WriteProgData("PARAMETER", "THRESHOLDPRESSDIV", SystemParameters.ThresholdPressDiv, sfile)
        WriteProgData("PARAMETER", "THRESHOLDPRESSRATIO", SystemParameters.ThresholdPressRatio, sfile)
        WriteProgData("PARAMETER", "PRESSAVERAGE", SystemParameters.PressAverage, sfile)
        WriteProgData("PARAMETER", "PLATEUPPRESET", SystemParameters.PlateUpPreset, sfile)
        WriteProgData("PARAMETER", "PRESS1ST", SystemParameters.Press1st, sfile)
        WriteProgData("PARAMETER", "PRESS2ND", SystemParameters.Press2nd, sfile)
        WriteProgData("PARAMETER", "PRESS3RD", SystemParameters.Press3rd, sfile)
        WriteProgData("PARAMETER", "PUMPCURRENTLIMIT", SystemParameters.PumpCurrentLimit, sfile)
        WriteProgData("PARAMETER", "PUMPCURRENTALARMTIME", SystemParameters.PumpCurrentAlarmTime, sfile)

        WriteProgData("PARAMETER", "COOLINGTEMPERATURE", SystemParameters.CoolingTemperature, sfile)
        WriteProgData("PARAMETER", "HIGHTEMPLIMIT", SystemParameters.HighTempLimit, sfile)
        WriteProgData("PARAMETER", "HighPressureLimit", SystemParameters.HighPressureLimit, sfile)

        WriteProgData("PARAMETER", "PUMPINGTIME", SystemParameters.PumpingTime, sfile)
        WriteProgData("PARAMETER", "PROCESSENDBZONTIME", SystemParameters.ProcessEndBZonTime, sfile)
        WriteProgData("PARAMETER", "PROCESSENDALARM", SystemParameters.ProcessEndAlarm, sfile)
        WriteProgData("PARAMETER", "PRESSURERANGE", SystemParameters.PressureRange, sfile)
        WriteProgData("PARAMETER", "PROCESSTEMPRANGE", SystemParameters.ProcessTempRange, sfile)
        WriteProgData("PARAMETER", "AUTOPURGECYCLE", SystemParameters.AutoPurgeCycle, sfile)
        WriteProgData("PARAMETER", "AUTOPURGEONTIME", SystemParameters.AutoPurgeOnTime, sfile)
        WriteProgData("PARAMETER", "AUTOPURGEOFFTIME", SystemParameters.AutoPurgeOFFTime, sfile)
        WriteProgData("PARAMETER", "PROCESSAUTOVACUUMVAC", SystemParameters.ProcessVacuumAutoVac, sfile)
        WriteProgData("PARAMETER", "VENTTIME", SystemParameters.VentTime, sfile)
        WriteProgData("PARAMETER", "PROCESSAUTOVACUUM", SystemParameters.ProcessAutoVacuum, sfile)
        WriteProgData("PARAMETER", "PROCESSAUTOVACUUMVACLO", SystemParameters.ProcessVacuumAutoVacLo, sfile)
        '�۰�PURGE
        WriteProgData("PARAMETER", "VAC1ATMSELECT", SystemParameters.Vac1ATM_Select, sfile)
        WriteProgData("PARAMETER", "AUTOVENT", GetTrue01String(AutoVent_Status), sfile)
        WriteProgData("PARAMETER", "AUTOPURGE", GetTrue01String(AutoPurge_Status), sfile)
        WriteProgData("PARAMETER", "AUTOPURGEOFFTIME", SystemParameters.AutoPurgeOFFTime, sfile)
        WriteProgData("PARAMETER", "AUTOPURGEONTIME", SystemParameters.AutoPurgeOnTime, sfile)
        WriteProgData("PARAMETER", "AUTOPURGECYCLE", SystemParameters.AutoPurgeCycle, sfile)

        WriteProgData("PARAMETER", "HEATERCOUNT", SystemParameters.HeaterCount, sfile)
        WriteProgData("PARAMETER", "HEATERPOWER", SystemParameters.HeaterPower, sfile)
        WriteProgData("PARAMETER", "HEATERVOLTAGE", SystemParameters.HeaterVoltage, sfile)
        WriteProgData("PARAMETER", "HEATERMONITORPOWER", SystemParameters.HeaterMonitorPower, sfile)
        WriteProgData("PARAMETER", "HEATERCURRENTRATE", SystemParameters.HeaterCurrentRate, sfile)

        '�s�{���_�]�w
        WriteProgData("PARAMETER", "ABORTPRESSURERANGE", SystemParameters.AbortPressureRange, sfile)
        WriteProgData("PARAMETER", "ABORTTEMPRANGE", SystemParameters.AbortTempRange, sfile)

        WriteProgData("PARAMETER", "ABORTPRESSURERANGEFLAG", SystemParameters.AbortPressureRangeFlag, sfile)
        WriteProgData("PARAMETER", "ABORTTEMPRANGEFLAG", SystemParameters.AbortTempRangeFlag, sfile)

        '�o����{�]�w
        WriteProgData("PARAMETER", "POSITIONSET01", SystemParameters.Position01Set, sfile)
        WriteProgData("PARAMETER", "POSITIONSET02", SystemParameters.Position02Set, sfile)
        WriteProgData("PARAMETER", "POSITIONSET03", SystemParameters.Position03Set, sfile)
        WriteProgData("PARAMETER", "POSITIONSET04", SystemParameters.Position04Set, sfile)
        WriteProgData("PARAMETER", "POSITIONSET05", SystemParameters.Position05Set, sfile)
        WriteProgData("PARAMETER", "POSITIONSET06", SystemParameters.Position06Set, sfile)
        WriteProgData("PARAMETER", "DISTANCESET", SystemParameters.DistanceSet, sfile)


        WriteProgData("PARAMETER", "BarcodeOnly", SystemParameters.BarcodeOnly, sfile)
        WriteProgData("PARAMETER", "AutoRecordData", SystemParameters.AutoRecordData, sfile)
        WriteProgData("PARAMETER", "SplitTopBotTemp", SystemParameters.SplitTopBotTemp, sfile) 'Add  by Vincent 20180419  ------------------- Start
        WriteProgData("PARAMETER", "VentOffDelay", SystemParameters.VentOffDelay, sfile) 'Add  by Vincent 20210318  
        WriteProgData("PARAMETER", "RPONPressure", SystemParameters.RPONPressure, sfile) 'Add  by Vincent 20210318  
        WriteProgData("PARAMETER", "DPWaterFlowHz", SystemParameters.DPWaterFlowHz, sfile) 'Add  by Vincent 20210318  
        WriteProgData("PARAMETER", "DPWaterFlowHzMax", SystemParameters.DPWaterFlowHzMax, sfile) 'Add  by Vincent 20210318  
        WriteProgData("PARAMETER", "DPWaterFlowAlarm", SystemParameters.DPWaterFlowAlarm, sfile) 'Add  by Vincent 20210318
        '
        WriteProgData("PARAMETER", "StartLog_Time", SystemParameters.StartLog_Time, sfile) 'Add  by claire 20230620
        WriteProgData("PARAMETER", "RunConfirm", SystemParameters.RunConfirm, sfile)
        WriteProgData("PARAMETER", "WebPath", SystemParameters.WebPath, sfile)

        WriteProgData("PARAMETER", "DALog", SystemParameters.bolDAlog, sfile)
    End Sub


    'Ū���s�{�Ҧ��r��
    Public Sub ReadProcessString()
        'Ū���s�{�r��
        total_Process_Num = ReadStatusString("PROCESS", ProcessStatusList, ProcessINIFile)    'Ū���s�{�r�� 3 �y
    End Sub
    'Ū�����r��,3�y
    'process = �s�{�W��,INI �ɮ׳]�w��==>  �Y process= "PROCESS" [process"_
    'ReadStatusString (�s�{�W,�x�s3�y��2���}�C, �ɮצW) �Ǧ^�r���`�� --> �w�q�� XXXX_=40
    '����r��w�q�p�U, ²��h�� _CHS, �^�嬰 _ENG
    '[PROCESS_CHT]   
    'PROCESS_NUM=10
    'PROCESS00=�s�{�}�l
    'PROCESS01=��u��
    '.....�l����
    '�@��Ū�T�ػy���s�J�}�C��
    Public Function ReadStatusString(ByVal process As String, ByRef sstr(,) As String, ByVal sfile As String) As Integer
        Dim para, section As String
        Dim i, j, num As Integer
        section = process & LangStr(0)
        num = Val(ReadProgData(section, process & "_NUM", "0", sfile))
        If num > 0 Then
            For j = 0 To 2
                Select Case j
                    Case 0
                        section = process + "_CHT"
                    Case 1
                        section = process + "_CHT"
                    Case 2
                        section = process + "_ENG"
                End Select
                For i = 0 To num
                    para = process & Format(i, "00")
                    If j = 1 Then
                        sstr(j, i) = StrConv(sstr(0, i), VbStrConv.SimplifiedChinese, 2052)
                    Else
                        sstr(j, i) = ReadProgData(section, para, "No Message.", sfile)
                    End If
                Next
            Next j
        End If
        Return num
    End Function


    Public FlowMeterMode As Boolean

    Public Sub ReadFlowMeterMode(ByVal sfile As String)
        FlowMeterMode = GetTrue01Boolean(ReadProgData("FLOW_SETUP", "FLOWMETERMODE", "0", sfile))
        FlowMeterMode = True
        ReDim SystemParameters.TopFlowMeterHz(6)
        ReDim SystemParameters.BotFlowMeterHz(6)
        WriteProgData("FLOW_SETUP", "FLOWMETERMODE", GetTrue01String(FlowMeterMode), sfile)
    End Sub

    Public Sub WriteFlowMeterMode(ByVal sfile As String)
        WriteProgData("FLOW_SETUP", "FLOWMETERMODE", GetTrue01String(FlowMeterMode), sfile)
    End Sub

    Public Sub ReadIniFile()
        'PLC_IO_MAPPING(PLCIOMappingINIFile)
        ReadFormAlarmstring(AlarmINIFile)
        ReadProgramCaption(ProgramINIFile)
        ReadProgramMode(ProgramINIFile)
        ReadVacuumSetup(ProgramINIFile)         'Ū���u�ŭp�]�w
        DeviceLifeTimeInit()                    '��l�Ƹ˸m�ةR
        PLCADDADefine(ADDAINIFile)                            '�]�w PLC ��AD/DA ���
        ReadProcessString()
        ReadIOName(LangCHTINIFile, LangCHSINIFile, LangEngINIFile)
        'Ū�����u�ɦW
    End Sub

End Module
