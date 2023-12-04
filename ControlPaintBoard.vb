﻿Imports System.Threading
Imports System.Drawing
Public Class ControlPaintBoard

    Private gPaint As Graphics
    Private gBitmap As Bitmap
    Private gOldBitmap As Bitmap
    Private gPen As Pen
    Private WPen As Pen
    Private gBrush As SolidBrush
    Private gFont As Font

    Private Draw_Flag As Boolean
    Private bEnable As Boolean
    Private bEraser As Boolean
    Private Oldxy As New Point
    Private Newxy As New Point

    'Private PaintThread As System.Threading.Thread
    Private PictureBox1_PaintThread As System.Threading.Thread
    Private bolquit As Boolean
    Private ColorDiag As New ColorDialog
    Private epoint As New Point
    Private bolDown As Boolean
    Private bolMove As Boolean
    Private bolUp As Boolean
    Private bolLoad As Boolean
    Private bolClear As Boolean
    Private bolEraser As Boolean
    'Private objGraph sa 

    Property iWidth() As Integer
        Get
            Return Me.Width
        End Get
        Set(ByVal value As Integer)
            Me.Width = value
        End Set
    End Property

    Property iHeight() As Integer
        Get
            Return Me.Height
        End Get
        Set(ByVal value As Integer)
            Me.Height = value
        End Set
    End Property
    Public Property Enable() As Boolean
        Get
            Return bEnable
        End Get
        Set(ByVal value As Boolean)
            bEnable = value
        End Set
    End Property

    Property Eraser() As Boolean
        Get
            Return bEraser
        End Get
        Set(ByVal value As Boolean)
            bEraser = value
        End Set
    End Property

    Public Sub Clear(ByVal cColor As Color)
        If bEnable Then
            gPaint.Clear(cColor)
            picPaintBoard.Image = gBitmap
        End If
    End Sub
    Public Sub Clear()

        If bEnable Then
            gPaint.Clear(Color.White)
            picPaintBoard.Image = gBitmap
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        gBitmap = New Bitmap(picPaintBoard.Width, picPaintBoard.Height)
        gPaint = Graphics.FromImage(gBitmap)

        gPen = New Pen(Color.Black, 3)
        WPen = New Pen(Color.White, 20)
        gBrush = New SolidBrush(Color.Black)
        gFont = New Font("Arial", 12)
        ColorDiag.FullOpen = True
        Enable = False
        bEraser = False
        btnEnable.BackColor = Color.LightPink
        btnEraser.BackColor = Color.LightBlue
        ResizePic()

        'PaintThread = New Threading.Thread(AddressOf PaintThreadWork)
        'PictureBox1_PaintThread = New Threading.Thread(AddressOf PictureBox1_PaintThreadWork)
    End Sub


    Private Sub ResizePic()
        picPaintBoard.Width = Me.Width
        picPaintBoard.Height = Me.Height - 40
        picPaintBoard.Top = 40
        picPaintBoard.Left = 0

    End Sub

    Private Sub PictureBox1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles picPaintBoard.Paint
        If bEnable Then
            'picPaintBoard.Image = gBitmap
            Timer1.Enabled = True
            'If InvokeRequired Then
            'Invoke(New EventHandler(AddressOf PictureBox1_PaintThreadWork), sender)
            'BeginInvoke(New EventHandler(Of ImageGrabbedEventArgs)(AddressOf OnImageGrabbedGige), sender, e.Clone())
            Return
            'End If
        Else
            Timer1.Enabled = False
        End If
    End Sub

    Private Sub PictureBox1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picPaintBoard.MouseDown
        If bEnable Then
            Oldxy.X = e.X
            Oldxy.Y = e.Y
            Draw_Flag = True
            picPaintBoard.Image = gBitmap
            'bolDown = True
            'PaintThread.Start(e)
            'bolDown = False
        End If

    End Sub

    Private Sub PictureBox1_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picPaintBoard.MouseUp
        If bEnable Then
            If Draw_Flag Then
                Draw_Flag = False
            End If
            'bolUp = True
            'If Not PaintThread.IsAlive Then
            '    PaintThread.Start(e)
            '    'picPaintBoard.Image = gBitmap
            '    bolUp = False
            'End If
        End If
    End Sub

    Private Sub PictureBox1_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picPaintBoard.MouseMove
        If bEnable Then
            If Draw_Flag Then
                'bolMove = True
                'PaintThread.Start(e)
                Newxy.X = e.X
                Newxy.Y = e.Y
                If Eraser Then
                    gPaint.DrawLine(WPen, Newxy, Oldxy)
                Else
                    gPaint.DrawLine(gPen, Newxy, Oldxy)
                End If
                Oldxy.X = e.X
                Oldxy.Y = e.Y
                picPaintBoard.Image = gBitmap
                'bolMove = False
            End If
        End If
    End Sub

    Public Sub New()

        ' 此為 Windows Form 設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。

    End Sub

    Private Sub ControlPaintBoard_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        gBitmap = New Bitmap(picPaintBoard.Width, picPaintBoard.Height)
        gOldBitmap = New Bitmap(picPaintBoard.Width, picPaintBoard.Height)
        gPaint = Graphics.FromImage(gBitmap)

        gPen = New Pen(Color.Black, 3)
        gBrush = New SolidBrush(Color.Black)
        gFont = New Font("Arial", 12)
        ColorDiag.FullOpen = True
    End Sub

    Private Sub btnEnable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnable.Click
        'If ProcessMode_RUN Then
        '    MsgBoxLangOK("製程中無法啟用")
        '    Exit Sub
        'End If
        Enable = Not Enable

        'If Enable Then
        '    btnEnable.BackColor = Color.Lime
        'Else
        '    btnEnable.BackColor = Color.LightPink
        'End If

    End Sub

    Private Sub btnEraser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEraser.Click
        Eraser = Not Eraser
        'If Eraser Then
        '    btnEraser.BackColor = Color.Lime
        'Else
        '    btnEraser.BackColor = Color.LightBlue
        'End If

    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Clear()
    End Sub

    Private Sub ControlPaintBoard_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        ResizePic()
    End Sub
    Private Sub PaintThreadWork(ByVal epoint As Point)
        If bolMove Then
            Newxy.X = epoint.X
            Newxy.Y = epoint.Y
            If Eraser Then
                gPaint.DrawLine(WPen, Newxy, Oldxy)
            Else
                gPaint.DrawLine(gPen, Newxy, Oldxy)
            End If
            Oldxy.X = epoint.X
            Oldxy.Y = epoint.Y
            picPaintBoard.Image = gBitmap
        End If
        If bolDown Then
            Oldxy.X = epoint.X
            Oldxy.Y = epoint.Y
            Draw_Flag = True
            picPaintBoard.Image = gBitmap
        End If
        If bolUp Then
            picPaintBoard.Image = gBitmap
        End If
        gOldBitmap.Dispose()
        'gOldBitmap = gBitmap
    End Sub
    Private Sub PictureBox1_PaintThreadWork()
        'Do
        picPaintBoard.Image = gBitmap
        'gOldBitmap.Dispose()
        'Loop Until Enable = False
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Eraser Then
            btnEraser.BackColor = Color.Lime
        Else
            btnEraser.BackColor = Color.LightBlue
        End If
        If Enable Then
            btnEnable.BackColor = Color.Lime
            'Invoke(New EventHandler(AddressOf PictureBox1_PaintThreadWork), sender)
            'PictureBox1_PaintThreadWork()
            picPaintBoard.Image = gBitmap
        Else
            btnEnable.BackColor = Color.LightPink
        End If

    End Sub
End Class
