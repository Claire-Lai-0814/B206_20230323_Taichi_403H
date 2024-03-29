Public Class Form1
    Inherits System.Windows.Forms.Form

    Public Timered As String
    Public FirstRunDelay_Status As Boolean
    Friend WithEvents picCopying As System.Windows.Forms.PictureBox
    Friend WithEvents picCelloTitleEng As System.Windows.Forms.PictureBox
    Friend WithEvents picCelloTitle As System.Windows.Forms.PictureBox
    Friend WithEvents picCelloLogo As System.Windows.Forms.PictureBox
    Friend WithEvents lblTypeName As System.Windows.Forms.Label
    Friend WithEvents lblDeviceName As System.Windows.Forms.Label
    Friend WithEvents btnShutdownPC As System.Windows.Forms.Button
    Friend WithEvents btnRestartPC As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnLogin As System.Windows.Forms.Button
    Public WithEvents grpCommTest As System.Windows.Forms.GroupBox
    Friend WithEvents grpLanguage As System.Windows.Forms.GroupBox
    Friend WithEvents pgbLanguage As System.Windows.Forms.ProgressBar
    Friend WithEvents picFlagUSA As System.Windows.Forms.PictureBox
    Friend WithEvents picFlagPRC As System.Windows.Forms.PictureBox
    Friend WithEvents picFlagTaiwan As System.Windows.Forms.PictureBox
    Friend WithEvents radChangeToENG As System.Windows.Forms.RadioButton
    Friend WithEvents radChangeToCHS As System.Windows.Forms.RadioButton
    Friend WithEvents radChangeToCHT As System.Windows.Forms.RadioButton
    Friend WithEvents btnRegister As System.Windows.Forms.Button
    Friend WithEvents lblSystemLoading As System.Windows.Forms.Label
    Public FirstRunDelayTimer As Byte
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents Timer4 As System.Windows.Forms.Timer
    'Friend WithEvents AxWindowsMediaPlayer1 As AxWMPLib.AxWindowsMediaPlayer
    Public SystemLoadOK As Boolean = False
    Public intConter As Integer = 1
    'Public ScreenW As Integer
    'Public ScreenH As Integer
    Private X As Single '當前窗體的寬度
    Private Y As Single '當前窗體的高度
    Private isLoaded As Boolean '// 是否已設定各控制的尺寸資料到Tag屬性
    Private FormW As Integer
    Private FormH As Integer

#Region " Windows Form 設計工具產生的程式碼 "

    Public Sub New()
        MyBase.New()

        '此呼叫為 Windows Form 設計工具的必要項。
        InitializeComponent()
        isLoaded = False
        '在 InitializeComponent() 呼叫之後加入所有的初始設定

    End Sub

    'Form 覆寫 Dispose 以清除元件清單。
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意: 以下為 Windows Form 設計工具所需的程序
    '您可以使用 Windows Form 設計工具進行修改。
    '請勿使用程式碼編輯器來修改這些程序。
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents Timer3 As System.Windows.Forms.Timer
    Friend WithEvents lblModelname As System.Windows.Forms.Label
    Public WithEvents chkPLCTest As System.Windows.Forms.CheckBox
    Friend WithEvents lblExportData As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        Me.lblModelname = New System.Windows.Forms.Label()
        Me.chkPLCTest = New System.Windows.Forms.CheckBox()
        Me.lblExportData = New System.Windows.Forms.Label()
        Me.picCopying = New System.Windows.Forms.PictureBox()
        Me.picCelloTitleEng = New System.Windows.Forms.PictureBox()
        Me.picCelloTitle = New System.Windows.Forms.PictureBox()
        Me.picCelloLogo = New System.Windows.Forms.PictureBox()
        Me.lblTypeName = New System.Windows.Forms.Label()
        Me.lblDeviceName = New System.Windows.Forms.Label()
        Me.btnShutdownPC = New System.Windows.Forms.Button()
        Me.btnRestartPC = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.grpCommTest = New System.Windows.Forms.GroupBox()
        Me.grpLanguage = New System.Windows.Forms.GroupBox()
        Me.pgbLanguage = New System.Windows.Forms.ProgressBar()
        Me.picFlagUSA = New System.Windows.Forms.PictureBox()
        Me.picFlagPRC = New System.Windows.Forms.PictureBox()
        Me.picFlagTaiwan = New System.Windows.Forms.PictureBox()
        Me.radChangeToENG = New System.Windows.Forms.RadioButton()
        Me.radChangeToCHS = New System.Windows.Forms.RadioButton()
        Me.radChangeToCHT = New System.Windows.Forms.RadioButton()
        Me.btnRegister = New System.Windows.Forms.Button()
        Me.lblSystemLoading = New System.Windows.Forms.Label()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.Timer4 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.picCopying, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picCelloTitleEng, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picCelloTitle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picCelloLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCommTest.SuspendLayout()
        Me.grpLanguage.SuspendLayout()
        CType(Me.picFlagUSA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picFlagPRC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picFlagTaiwan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Timer1
        '
        '
        'Timer2
        '
        '
        'Timer3
        '
        '
        'lblModelname
        '
        Me.lblModelname.Font = New System.Drawing.Font("Arial", 14.25!)
        Me.lblModelname.ForeColor = System.Drawing.Color.Red
        Me.lblModelname.Location = New System.Drawing.Point(788, 707)
        Me.lblModelname.Name = "lblModelname"
        Me.lblModelname.Size = New System.Drawing.Size(227, 18)
        Me.lblModelname.TabIndex = 203
        Me.lblModelname.Text = "B122"
        Me.lblModelname.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkPLCTest
        '
        Me.chkPLCTest.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPLCTest.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.chkPLCTest.Location = New System.Drawing.Point(6, 25)
        Me.chkPLCTest.Name = "chkPLCTest"
        Me.chkPLCTest.Size = New System.Drawing.Size(216, 21)
        Me.chkPLCTest.TabIndex = 215
        Me.chkPLCTest.Text = "PLC通訊"
        '
        'lblExportData
        '
        Me.lblExportData.Location = New System.Drawing.Point(3, 1)
        Me.lblExportData.Name = "lblExportData"
        Me.lblExportData.Size = New System.Drawing.Size(44, 47)
        Me.lblExportData.TabIndex = 216
        '
        'picCopying
        '
        Me.picCopying.BackColor = System.Drawing.Color.Transparent
        Me.picCopying.Cursor = System.Windows.Forms.Cursors.Default
        Me.picCopying.Image = Global.CELLO.My.Resources.Resources.Sensor_ON
        Me.picCopying.Location = New System.Drawing.Point(2, 716)
        Me.picCopying.Margin = New System.Windows.Forms.Padding(0)
        Me.picCopying.Name = "picCopying"
        Me.picCopying.Size = New System.Drawing.Size(16, 16)
        Me.picCopying.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picCopying.TabIndex = 528
        Me.picCopying.TabStop = False
        Me.picCopying.Visible = False
        '
        'picCelloTitleEng
        '
        Me.picCelloTitleEng.Image = Global.CELLO.My.Resources.Resources.CELLO_CO_ENG
        Me.picCelloTitleEng.Location = New System.Drawing.Point(585, 192)
        Me.picCelloTitleEng.Name = "picCelloTitleEng"
        Me.picCelloTitleEng.Size = New System.Drawing.Size(428, 56)
        Me.picCelloTitleEng.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picCelloTitleEng.TabIndex = 531
        Me.picCelloTitleEng.TabStop = False
        '
        'picCelloTitle
        '
        Me.picCelloTitle.Image = Global.CELLO.My.Resources.Resources.Cello_Co
        Me.picCelloTitle.Location = New System.Drawing.Point(369, 100)
        Me.picCelloTitle.Name = "picCelloTitle"
        Me.picCelloTitle.Size = New System.Drawing.Size(644, 86)
        Me.picCelloTitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picCelloTitle.TabIndex = 529
        Me.picCelloTitle.TabStop = False
        '
        'picCelloLogo
        '
        Me.picCelloLogo.BackColor = System.Drawing.Color.Transparent
        Me.picCelloLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picCelloLogo.Image = Global.CELLO.My.Resources.Resources.CELLOLOGO01
        Me.picCelloLogo.Location = New System.Drawing.Point(0, 0)
        Me.picCelloLogo.Name = "picCelloLogo"
        Me.picCelloLogo.Size = New System.Drawing.Size(320, 257)
        Me.picCelloLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picCelloLogo.TabIndex = 530
        Me.picCelloLogo.TabStop = False
        '
        'lblTypeName
        '
        Me.lblTypeName.BackColor = System.Drawing.Color.Transparent
        Me.lblTypeName.Font = New System.Drawing.Font("Arial", 24.0!, System.Drawing.FontStyle.Underline)
        Me.lblTypeName.ForeColor = System.Drawing.Color.Red
        Me.lblTypeName.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTypeName.Location = New System.Drawing.Point(305, 341)
        Me.lblTypeName.Name = "lblTypeName"
        Me.lblTypeName.Size = New System.Drawing.Size(621, 50)
        Me.lblTypeName.TabIndex = 533
        Me.lblTypeName.Text = "Type Name"
        Me.lblTypeName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDeviceName
        '
        Me.lblDeviceName.BackColor = System.Drawing.Color.Transparent
        Me.lblDeviceName.Font = New System.Drawing.Font("Arial", 24.0!, System.Drawing.FontStyle.Underline)
        Me.lblDeviceName.ForeColor = System.Drawing.Color.Red
        Me.lblDeviceName.Location = New System.Drawing.Point(305, 391)
        Me.lblDeviceName.Name = "lblDeviceName"
        Me.lblDeviceName.Size = New System.Drawing.Size(621, 50)
        Me.lblDeviceName.TabIndex = 532
        Me.lblDeviceName.Text = "Device Name"
        Me.lblDeviceName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnShutdownPC
        '
        Me.btnShutdownPC.BackColor = System.Drawing.Color.LavenderBlush
        Me.btnShutdownPC.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShutdownPC.Image = Global.CELLO.My.Resources.Resources.Shotdown
        Me.btnShutdownPC.Location = New System.Drawing.Point(212, 653)
        Me.btnShutdownPC.Name = "btnShutdownPC"
        Me.btnShutdownPC.Size = New System.Drawing.Size(161, 60)
        Me.btnShutdownPC.TabIndex = 536
        Me.btnShutdownPC.Text = "系統關機"
        Me.btnShutdownPC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnShutdownPC.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnShutdownPC.UseVisualStyleBackColor = True
        '
        'btnRestartPC
        '
        Me.btnRestartPC.BackColor = System.Drawing.Color.AliceBlue
        Me.btnRestartPC.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRestartPC.Image = Global.CELLO.My.Resources.Resources.Restart
        Me.btnRestartPC.Location = New System.Drawing.Point(12, 653)
        Me.btnRestartPC.Name = "btnRestartPC"
        Me.btnRestartPC.Size = New System.Drawing.Size(161, 60)
        Me.btnRestartPC.TabIndex = 537
        Me.btnRestartPC.Text = "重新開機"
        Me.btnRestartPC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRestartPC.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnRestartPC.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.AutoSize = True
        Me.btnExit.BackColor = System.Drawing.Color.Lavender
        Me.btnExit.BackgroundImage = CType(resources.GetObject("btnExit.BackgroundImage"), System.Drawing.Image)
        Me.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnExit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExit.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(792, 562)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(161, 60)
        Me.btnExit.TabIndex = 535
        Me.btnExit.Text = "離開"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnLogin
        '
        Me.btnLogin.AutoSize = True
        Me.btnLogin.BackColor = System.Drawing.Color.Lavender
        Me.btnLogin.BackgroundImage = Global.CELLO.My.Resources.Resources.Users
        Me.btnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLogin.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogin.Location = New System.Drawing.Point(523, 562)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnLogin.Size = New System.Drawing.Size(161, 60)
        Me.btnLogin.TabIndex = 534
        Me.btnLogin.Text = "登入系統"
        Me.btnLogin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnLogin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnLogin.UseVisualStyleBackColor = True
        '
        'grpCommTest
        '
        Me.grpCommTest.Controls.Add(Me.chkPLCTest)
        Me.grpCommTest.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpCommTest.ForeColor = System.Drawing.Color.Black
        Me.grpCommTest.Location = New System.Drawing.Point(12, 475)
        Me.grpCommTest.Name = "grpCommTest"
        Me.grpCommTest.Size = New System.Drawing.Size(235, 160)
        Me.grpCommTest.TabIndex = 538
        Me.grpCommTest.TabStop = False
        Me.grpCommTest.Text = "通訊測試"
        '
        'grpLanguage
        '
        Me.grpLanguage.Controls.Add(Me.pgbLanguage)
        Me.grpLanguage.Controls.Add(Me.picFlagUSA)
        Me.grpLanguage.Controls.Add(Me.picFlagPRC)
        Me.grpLanguage.Controls.Add(Me.picFlagTaiwan)
        Me.grpLanguage.Controls.Add(Me.radChangeToENG)
        Me.grpLanguage.Controls.Add(Me.radChangeToCHS)
        Me.grpLanguage.Controls.Add(Me.radChangeToCHT)
        Me.grpLanguage.Font = New System.Drawing.Font("Arial", 15.75!)
        Me.grpLanguage.Location = New System.Drawing.Point(12, 263)
        Me.grpLanguage.Name = "grpLanguage"
        Me.grpLanguage.Size = New System.Drawing.Size(235, 206)
        Me.grpLanguage.TabIndex = 539
        Me.grpLanguage.TabStop = False
        Me.grpLanguage.Text = "Language"
        '
        'pgbLanguage
        '
        Me.pgbLanguage.ForeColor = System.Drawing.Color.Lime
        Me.pgbLanguage.Location = New System.Drawing.Point(2, 194)
        Me.pgbLanguage.Name = "pgbLanguage"
        Me.pgbLanguage.Size = New System.Drawing.Size(230, 10)
        Me.pgbLanguage.Step = 1
        Me.pgbLanguage.TabIndex = 213
        '
        'picFlagUSA
        '
        Me.picFlagUSA.Image = Global.CELLO.My.Resources.Resources.United_States_Flag
        Me.picFlagUSA.Location = New System.Drawing.Point(6, 131)
        Me.picFlagUSA.Name = "picFlagUSA"
        Me.picFlagUSA.Size = New System.Drawing.Size(42, 42)
        Me.picFlagUSA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picFlagUSA.TabIndex = 1
        Me.picFlagUSA.TabStop = False
        '
        'picFlagPRC
        '
        Me.picFlagPRC.Image = Global.CELLO.My.Resources.Resources.China_Flag
        Me.picFlagPRC.Location = New System.Drawing.Point(6, 81)
        Me.picFlagPRC.Name = "picFlagPRC"
        Me.picFlagPRC.Size = New System.Drawing.Size(42, 42)
        Me.picFlagPRC.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picFlagPRC.TabIndex = 1
        Me.picFlagPRC.TabStop = False
        '
        'picFlagTaiwan
        '
        Me.picFlagTaiwan.Image = Global.CELLO.My.Resources.Resources.Taiwan_Flag
        Me.picFlagTaiwan.Location = New System.Drawing.Point(6, 31)
        Me.picFlagTaiwan.Name = "picFlagTaiwan"
        Me.picFlagTaiwan.Size = New System.Drawing.Size(42, 42)
        Me.picFlagTaiwan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picFlagTaiwan.TabIndex = 1
        Me.picFlagTaiwan.TabStop = False
        '
        'radChangeToENG
        '
        Me.radChangeToENG.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.radChangeToENG.Location = New System.Drawing.Point(54, 131)
        Me.radChangeToENG.Name = "radChangeToENG"
        Me.radChangeToENG.Size = New System.Drawing.Size(132, 42)
        Me.radChangeToENG.TabIndex = 0
        Me.radChangeToENG.Text = "English"
        Me.radChangeToENG.UseVisualStyleBackColor = True
        '
        'radChangeToCHS
        '
        Me.radChangeToCHS.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.radChangeToCHS.Location = New System.Drawing.Point(54, 81)
        Me.radChangeToCHS.Name = "radChangeToCHS"
        Me.radChangeToCHS.Size = New System.Drawing.Size(132, 42)
        Me.radChangeToCHS.TabIndex = 0
        Me.radChangeToCHS.Text = "简体中文"
        Me.radChangeToCHS.UseVisualStyleBackColor = True
        '
        'radChangeToCHT
        '
        Me.radChangeToCHT.Checked = True
        Me.radChangeToCHT.Location = New System.Drawing.Point(56, 31)
        Me.radChangeToCHT.Name = "radChangeToCHT"
        Me.radChangeToCHT.Size = New System.Drawing.Size(132, 42)
        Me.radChangeToCHT.TabIndex = 0
        Me.radChangeToCHT.TabStop = True
        Me.radChangeToCHT.Text = "繁體中文"
        Me.radChangeToCHT.UseVisualStyleBackColor = True
        '
        'btnRegister
        '
        Me.btnRegister.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnRegister.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRegister.Image = Global.CELLO.My.Resources.Resources.Key
        Me.btnRegister.Location = New System.Drawing.Point(523, 276)
        Me.btnRegister.Name = "btnRegister"
        Me.btnRegister.Size = New System.Drawing.Size(161, 60)
        Me.btnRegister.TabIndex = 540
        Me.btnRegister.Text = "註冊"
        Me.btnRegister.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnRegister.UseVisualStyleBackColor = True
        Me.btnRegister.Visible = False
        '
        'lblSystemLoading
        '
        Me.lblSystemLoading.BackColor = System.Drawing.Color.Transparent
        Me.lblSystemLoading.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSystemLoading.ForeColor = System.Drawing.Color.Blue
        Me.lblSystemLoading.Location = New System.Drawing.Point(519, 500)
        Me.lblSystemLoading.Name = "lblSystemLoading"
        Me.lblSystemLoading.Size = New System.Drawing.Size(470, 50)
        Me.lblSystemLoading.TabIndex = 541
        Me.lblSystemLoading.Text = "System Loading .."
        Me.lblSystemLoading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblVersion
        '
        Me.lblVersion.BackColor = System.Drawing.Color.Transparent
        Me.lblVersion.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.lblVersion.ForeColor = System.Drawing.Color.Black
        Me.lblVersion.Location = New System.Drawing.Point(504, 0)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(511, 30)
        Me.lblVersion.TabIndex = 555
        Me.lblVersion.Text = "B206_20230323_Taichi_403H"
        Me.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Timer4
        '
        '
        'Form1
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1014, 732)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.lblSystemLoading)
        Me.Controls.Add(Me.btnRegister)
        Me.Controls.Add(Me.lblExportData)
        Me.Controls.Add(Me.picCelloLogo)
        Me.Controls.Add(Me.grpLanguage)
        Me.Controls.Add(Me.grpCommTest)
        Me.Controls.Add(Me.btnShutdownPC)
        Me.Controls.Add(Me.btnRestartPC)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.lblTypeName)
        Me.Controls.Add(Me.lblDeviceName)
        Me.Controls.Add(Me.picCelloTitleEng)
        Me.Controls.Add(Me.picCopying)
        Me.Controls.Add(Me.lblModelname)
        Me.Controls.Add(Me.picCelloTitle)
        Me.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "B001"
        Me.TopMost = True
        CType(Me.picCopying, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picCelloTitleEng, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picCelloTitle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picCelloLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCommTest.ResumeLayout(False)
        Me.grpLanguage.ResumeLayout(False)
        CType(Me.picFlagUSA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picFlagPRC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picFlagTaiwan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Me.DesignMode Then Exit Sub

        Me.Text = Program_Title & Format(My.Computer.FileSystem.GetFileInfo(System.Reflection.Assembly.GetEntryAssembly().Location).LastWriteTime, "- yyyyMMddHHmmss")

        lblDeviceName.Text = Program_DeviceName
        lblModelname.Text = Program_ModelName
        lblTypeName.Text = Program_TypeName
        ''Add By Vincent 20190710  ----------------------------------------------------------  Start
        'lblVersion.Text = MachineID & "-" & lblVersion.Text
        ''Add By Vincent 20190710  ----------------------------------------------------------  End

        '設定FORM1 的語言選擇
        If SystemLanguage = 0 Then radChangeToCHT.Checked = True
        If SystemLanguage = 1 Then radChangeToCHS.Checked = True
        If SystemLanguage = 2 Then radChangeToENG.Checked = True

        btnLogin.Enabled = False
        FormTextBoxSave.SetTextBox(Me, Me.Name, FormSaveINIFile)
        'Work_BackDoor = New System.ComponentModel.BackgroundWorker()
        'Work_BackDoor.RunWorkerAsync()

        Button_BackWork = New System.ComponentModel.BackgroundWorker()
        Button_BackWork.RunWorkerAsync()

        ReadRunData() 'Barcode 20160808 by vincent ----------------
        RecipeMapEditor = New CRecipeMap
        CreateTIC_Monitor()

        'RemoteCIM.RecipeData.EQID = "Bonder01"
        'RemoteCIM.RecipeData.DateTime = Now.ToString("yyyyMMddHHmmssfffff")
        'RemoteCIM.RecipeData.ProductCount = "2"
        'RemoteCIM.RecipeData.ProductID(0).InputID = "LotA"
        'RemoteCIM.RecipeData.ProductID(1).InputID = "LotB"
        'RemoteCIM.RecipeData.OPID = "Vincent"
        'RemoteCIM.RecipeData.Send_Run_Request(Application.StartupPath & "\Run_Request.txt")
        'If RemoteCIM.RecipeData.Recieve_Run_Reply(Application.StartupPath & "\Run_Reply.txt") = False Then
        '    MsgBoxLangErr("Product ID Error")
        'End If
        'RemoteCIM.RecipeData.Send_Run_Start(Application.StartupPath & "\Run_Start.txt")
        'RemoteCIM.RecipeData.Send_Run_End(Application.StartupPath & "\Run_End.txt", Application.StartupPath & "\20180702-171911-chesly-Lot-2018_07_02-17_19_09-[0].dat")
        'RemoteCIM.RecipeData.Send_Run_Abort(Application.StartupPath & "\Run_Abort.txt", 0, "User Abort")

        Me.Timer1.Interval = 100
        Me.Timer1.Enabled = True
        Me.Timer2.Interval = 2000
        Me.Timer2.Enabled = True
        Me.Timer3.Interval = 995
        Me.Timer3.Enabled = True
        Me.Timer4.Interval = 100
        Me.Timer4.Enabled = False
        Me.Top = 0
        Me.Left = 0
        X = Me.Width '獲取窗體的寬度
        Y = Me.Height '獲取窗體的高度
        isLoaded = True '已設定各控制項的尺寸到Tag屬性中
        swdog_Restart()
        SetTag(Me) '調用方法
        Debug.Print("Form1_SetTag_Time=" + swdog.ElapsedMilliseconds.ToString)
        'Debug.Print("Form_Load")
    End Sub


    Private Sub Timer4_Tick(sender As System.Object, e As System.EventArgs) Handles Timer4.Tick
        'If PLCComm.IsOpen Then
        '    FBSPLC_Control()
        'End If
        If CommLivePLC Then
            ReadInformation()
            Button_Control()
            'AutoProcess_Task()
            UpDataDigital()
        End If
    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim i As Integer
        chkPLCTest.Checked = CommLivePLC
        If CelloUsbFlag Or Not LC.IsOK Then
            btnRegister.Visible = True     '若未註冊則顯示註冊按鈕及未註冊訊息
        Else
            btnRegister.Visible = False    '若未註冊則顯示註冊按鈕及未註冊訊息
        End If
        If LC.IsExpired Then
            If False Then
                btnLogin.BackColor = System.Drawing.SystemColors.Control
            Else
                btnLogin.BackColor = Color.Pink
            End If
        ElseIf LC.IsTrial Then
            btnLogin.BackColor = Color.LightGreen
        End If
        If LC.IsOK And False Then
            btnLogin.BackColor = System.Drawing.SystemColors.Control
        End If
        picCopying.Visible = CelloUsbCopyFlag
        If FirstRunDelay_Status Then
            AlarmRecord_Task()
        End If

        AutoProcess_Task()
        If CelloUsbFlag Then
            ProcessRecord_Task_200ms()
            DataLog_Task_200ms()
        End If

    End Sub

    Private Sub Timer3_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        Dim a As String
        Dim i As Integer
        On Error Resume Next
        MakeDateData()
        a = Format(Now(), "Long time")
        FormMenus.lblCurrentTime.Text = TTime
        If LC.IsExpired Then
            FormMenus.lblCurrentTime.BackColor = Color.Red
        Else
            If CelloUsbFlag Then
                FormMenus.lblCurrentTime.BackColor = Color.Lime
            Else
                FormMenus.lblCurrentTime.BackColor = Color.FromArgb(255, 192, 255)
            End If
        End If
        If a <> Timered Then
            USB_BackDoor()
            '系統載入
            If CommLivePLC Then
                'Add  by Vincent 20180419  ------------------- Start
                'Output(DoTempTrackDisableIndex).Status = (TempCal(0).CalTableDiff Or TempCal(1).CalTableDiff Or TempCal(2).CalTableDiff)
                'Add  by Vincent 20180419  ------------------- End

                'If SystemLoadOK = False Then
                If FirstRunDelay_Status = False Then
                    lblSystemLoading.Text = lblSystemLoading.Text + "."
                Else
                    btnLogin.Enabled = True
                    lblSystemLoading.Visible = False
                    ReadInformation()
                    For i = 0 To MAXPLATE
                        TopTempSV(i) = TempCal(i).SetTICTopTemp(Get_PLC_R1100(DAProcessTemp01Index + i * 4))
                        BotTempSV(i) = TempCal(i).SetTICBotTemp(Get_PLC_R1100(DAProcessTemp04Index + i * 4)) 'Modified by Vincent 20180419 
                        If GetTrue01Boolean(SystemParameters.SplitTopBotTemp) = False Then
                            BotTempSV(i) = TopTempSV(i)
                        End If
                        'PIDs(i).AutoPIDTop(TopTempPV(i), BotTempPV(i))
                    Next
                    SystemLoadOK = True
                End If
                'End If
            End If

            If FirstRunDelayTimer < 20 Then
                FirstRunDelayTimer = FirstRunDelayTimer + 1
                If FirstRunDelayTimer > 4 Then
                    FirstRunDelay_Status = True
                    btnLogin.Enabled = True
                End If
            End If
            If FirstRunDelay_Status = True And CommLivePLC Then
                '根據目前溫度寫入PID
                For i = 0 To MAXPLATE
                    PIDs(i).AutoPIDTop(TopTempSV(i), BotTempSV(i), False) '-------------------------石英燈PID/Max/Min Power後比對PLC值是否有改變 by Chesly 20181029--------------s
                    'Debug.Print(i.ToString + ",TopTempSV=" + TopTempSV(i).ToString)
                    'Debug.Print(i.ToString + ",BotTempSV=" + BotTempSV(i).ToString)
                    'Debug.Print(i.ToString + ",TopArea0=" + PIDs(i).TopArea0.ToString)
                    'Debug.Print(i.ToString + ",TopArea1=" + PIDs(i).TopArea1.ToString)
                    'Debug.Print(i.ToString + ",TopArea2=" + PIDs(i).TopArea2.ToString)
                    'Debug.Print(i.ToString + ",TopArea3=" + PIDs(i).TopArea3.ToString)
                    'Debug.Print(i.ToString + ",BotArea0=" + PIDs(i).BotArea0.ToString)
                    'Debug.Print(i.ToString + ",BotArea1=" + PIDs(i).BotArea1.ToString)
                    'Debug.Print(i.ToString + ",BotArea2=" + PIDs(i).BotArea2.ToString)
                    'Debug.Print(i.ToString + ",BotArea3=" + PIDs(i).BotArea3.ToString)
                Next

                'AutoPIDTop(Check_PLC_M(DoHeater01Index), TopTempSV, BotTempSV)
                '保養時間記錄

                DataLog_Task()
                ProcessRecord_Task()
                ProcessCurveLog.WriteProcessCurveData(ProcessToStepGo, ProcessCurveIndex, MAX_CURVES, ProcessRecordCurveDir + ProcessPN + ".cuv")
                ProcessCurveCSVLog.WriteProcessCurveData(ProcessToStepGo, ProcessCSVIndex, MAX_CURVES, ProcessRecordCurveDir + ProcessPN + ".csv", True)
                DataLogCurveLog.WriteProcessCurveData(CSVTimerStartPb_Status, DataLogCurveIndex, MAX_CURVES, DataLogRecordDir + DataLogCUVFileName)

                'CSVRecord_Task()
                RealTimeCurveDataSave()
                CTimer1.Run(AutoProcessTimerEnabled, AutoProcessTimer)
                CTimer2.Run(ProcessHoldTimerEnabled, ProcessHoldTimer)
                CTimer3.Run(ProcessMode_RUN, TotalProcessTime, CCountTimer.TimerFunction.PLUS)

                'ProcessTimerPlus(ProcessMode_RUN, TotalProcessTime)
                'ProcessTimer(ProcessHoldTimerEnabled, ProcessHoldTimer)
                'ProcessTimer(AutoProcessTimerEnabled, AutoProcessTimer)

                If Timercount_enable Then
                    TimerCountUp_Down(FormManuals.btnTimerStart, FormManuals.lblTimerMin, FormManuals.lblTimerSec)
                End If
            End If

            LifeTime()
            For i = 0 To MAXPLATE
                CSubAutoProcess(i).ExternlTimeCount()
            Next
            Timered = a
        End If
    End Sub


    Private Sub btnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        Me.Hide()
        '顯示註冊視窗
        FormLicenses.ShowDialog()
    End Sub

    Private Sub lblPLCCOM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblExportData.Click
        Dim filename As String
        If CelloUsbFlag Then
            Select Case SystemLanguage
                Case LANG_CHS
                    filename = CurDir() + "\LANG_CHS.TXT"
                Case LANG_ENG
                    filename = CurDir() + "\LANG_ENG.TXT"
                Case Else
                    filename = CurDir() + "\LANG_CHT.TXT"
            End Select
            If MsgBox("Export Language data to " + filename) = vbOK Then
                If IO.File.Exists(filename) Then
                    IO.File.Delete(filename)
                End If
                GetAllFormText(filename)
            End If
        End If
    End Sub

    Private Sub radChangeToCHS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radChangeToENG.Click, radChangeToCHT.Click, radChangeToCHS.Click

        'Form1s.Enabled = False
        pgbLanguage.Visible = True
        'ClickLanguge()
        ClickLanguge(LangCHTINIFile, LangCHSINIFile, LangEngINIFile) ' 設定語言系統參數
        WriteProgData("PROGRAM", "LANGUAGE", SystemLanguage.ToString, ProgramINIFile)
        ChangeLanguage(LanguageFile, pgbLanguage)
        ReadRecipeColumnData(RecipeINIFile)
        'AlarmResetPb_Status = True
        'Form1s.Enabled = True
        pgbLanguage.Visible = False
    End Sub
    '按下語言選擇, 做語言切換
    Public Sub ClickLanguge(ByVal cht As String, ByVal chs As String, ByVal eng As String)
        If radChangeToCHT.Checked Then
            SystemLanguage = 0
            LanguageFile = cht
        End If
        If radChangeToCHS.Checked Then
            SystemLanguage = 1
            LanguageFile = chs
        End If
        If radChangeToENG.Checked Then
            LanguageFile = eng
            SystemLanguage = 2
        End If
        If SystemLanguage < 0 And SystemLanguage > 2 Then
            SystemLanguage = 0
            LanguageFile = cht
            radChangeToCHT.Checked = True
        End If

    End Sub
    Public Sub CheckLanguge()
        If radChangeToCHS.Checked Then
            SystemLanguage = 1
            LanguageFile = ".\LANG_CHS.ini"
        End If
        If radChangeToCHT.Checked Then
            SystemLanguage = 0
            LanguageFile = ".\LANG_CHT.ini"
        End If
        If radChangeToENG.Checked Then
            LanguageFile = ".\LANG_ENG.ini"
            SystemLanguage = 2
        End If
        If SystemLanguage < 0 And SystemLanguage > 2 Then
            SystemLanguage = 0
            LanguageFile = ".\LANG_CHT.ini"
            radChangeToCHT.Checked = True
        End If

    End Sub


    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        If Not CelloUsbFlag Then
            LC.CheckRegister()
        End If
        If LC.CheckSystemDate = False Then
            MsgBoxLangErr("系統日期不正確, 無法登入!!", "System Date is not Correct, Can't Login!!")
            Exit Sub
        End If
        If CommLivePLC = False Then
            MsgBoxLangOK("PLC 連線異常，請注意!", "PLC Online Error!")
        End If
        'Me.Hide()
        'If LC.IsExpired Then
        '    FormLicenses.ShowDialog()
        'Else
        If LC.IsOK Then
            FormLogins.txtUserName.Text = ""
            FormLogins.txtPassword.Text = ""
            Me.Hide()
            FormLogins.ShowDialog()
        ElseIf LC.IsTrial Then
            FormLogins.txtUserName.Text = ""
            FormLogins.txtPassword.Text = ""
            Me.Hide()
            FormLogins.ShowDialog()
        Else
            MsgBoxLangErr("系統未註冊!", "Not Register!")
            FormLogins.txtUserName.Text = ""
            FormLogins.txtPassword.Text = ""
            Me.Hide()
            FormLogins.ShowDialog()
        End If

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Dim i As Integer
        FormTextBoxSave.FindTextBox(FormManuals, FormManuals.Name, FormSaveINIFile)
        If PLC_Y(DoMPIndex) = "1" Then
            If MsgBoxLangYesNo("真空幫浦運轉中,確定要離開嗎?", "Pump Still ON, Quit?") Then
                If CheckHeaterThenQuit() = False Then
                    Write_PLC_R1100(143, 0)
                    bolQuit = True
                    Threading.Thread.Sleep(1000)
                    Application.Exit()
                    Close()

                End If
            End If
        Else
            CheckHeaterThenQuit()
            If MsgBoxLangYesNo("確定要離開嗎?", "Comfirm Quit?") Then
                Write_PLC_R1100(143, 0)
                bolQuit = True

                Threading.Thread.Sleep(1000)
                Application.Exit()
                Me.Close()
            End If

        End If
    End Sub

    Public Function CheckHeaterThenQuit()
        Dim HeaterStatus, i As Integer
        HeaterStatus = 0
        For i = 0 To MAXPLATE
            If ManualControl(i).GetHeater Then
                HeaterStatus += 1
            End If
        Next
        If HeaterStatus > 0 Then
            If MsgBoxLangYesNo("加熱器加溫中,要離開嗎 ?" + vbCrLf + "(離開程式會關閉加熱器)", "Heater Still ON, Quit?" + vbCrLf + "(Heater will off before quit)") Then
                'For i = 0 To MAXPLATE
                '    ManualControl(i).SetHeater(False)
                'Next
                Do
                    For i = 0 To MAXPLATE
                        ManualControl(i).SetHeater(False)
                    Next
                    HeaterStatus = 0
                    For i = 0 To MAXPLATE
                        If ManualControl(i).GetHeater Then
                            HeaterStatus += 1
                        End If
                    Next
                    Write_PLC_R1100(143, 0)
                    Application.DoEvents()
                    Threading.Thread.Sleep(1)
                Loop Until HeaterStatus = 0
                bolQuit = True
                Threading.Thread.Sleep(1000)
                Application.Exit()
                Close()
            End If
        Else
            Return False
        End If
    End Function

    '重新開機鈕
    Private Sub btnRestartPC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRestartPC.Click
        If MsgBoxLangYesNo("確定要重新開機?", "确定要重新机?", "Restart PC ?") Then
            Shell("C:\WINDOWS\system32\shutdown.exe -r -f -t 0", AppWinStyle.Hide)
        End If
    End Sub
    '關機按鈕
    Private Sub btnShutdownPC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShutdownPC.Click
        If MsgBoxLangYesNo("確定要關機?", "确定要机?", "Shutdown PC ?") Then
            Shell("C:\WINDOWS\system32\shutdown.exe -s -f -t 0", AppWinStyle.Hide)
        End If
    End Sub

    Private Sub picFlagTaiwan_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picFlagTaiwan.DoubleClick
        If CelloUsbFlag Then
            SetupFlag(True)
        End If
    End Sub

    Private Sub lblModelname_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblModelname.DoubleClick
        If CelloUsbFlag Then
            SetupLogo(True)
        End If
    End Sub

    Private Sub picCelloLogo_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picCelloLogo.DoubleClick
        If CelloUsbFlag Then
            SetupFlag(True)
        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Write_PLC_R1100(143, intConter)
        If intConter > 32766 Then intConter = 0
        intConter = intConter + 1
        If bolQuit = True Then Write_PLC_R1100(143, 0)
    End Sub
    Private Sub Form1_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown
        Dim W As Integer
        Dim H As Integer
        FormW = Screen.PrimaryScreen.Bounds.Width
        FormH = Screen.PrimaryScreen.Bounds.Height
        Me.WindowState = FormWindowState.Normal
        swdog_Restart()
        Form1_Resize(Me, e)
        Debug.Print("Form1_Resize_Time=" + swdog.ElapsedMilliseconds.ToString)
        'Debug.Print("Form1_Shown" + ",screen.Width=" + FormW.ToString + ",screen.Height=" + FormH.ToString)
    End Sub
    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If isLoaded Then
            Dim new_x As Single = FormW / X
            Dim new_Y As Single = FormH / Y
            Me.Height = FormH
            Me.Width = FormW
            SetControls(new_x, new_Y, Me, isLoaded)
            'Debug.Print("Form1_Resize  X=" + X.ToString + ",Y=" + Y.ToString + ",Me.Width=" + Me.Width.ToString + ",Me.Height=" + Me.Height.ToString)
        End If
    End Sub
    'Private Sub SetControls(ByVal newx As Single, ByVal newy As Single, ByVal cons As Control)
    '    If isLoaded Then

    '        For Each con As Control In cons.Controls
    '            Dim mytag As String() = con.Tag.ToString().Split(New Char() {":"c})
    '            Dim a As Single = System.Convert.ToSingle(mytag(0)) * newx
    '            con.Width = CInt(a)
    '            a = System.Convert.ToSingle(mytag(1)) * newy
    '            con.Height = CInt((a))
    '            a = System.Convert.ToSingle(mytag(2)) * newx
    '            con.Left = CInt((a))
    '            a = System.Convert.ToSingle(mytag(3)) * newy
    '            con.Top = CInt((a))
    '            Dim currentSize As Single = System.Convert.ToSingle(mytag(4)) * newy
    '            con.Font = New Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit)

    '            If con.Controls.Count > 0 Then
    '                SetControls(newx, newy, con)
    '            End If
    '        Next
    '    End If
    'End Sub
End Class
