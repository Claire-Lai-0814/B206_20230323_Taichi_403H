Public Class FormKeyinVideoName
    Inherits System.Windows.Forms.Form

#Region " Windows Form �]�p�u�㲣�ͪ��{���X "

    Public Sub New()
        MyBase.New()

        '���I�s�� Windows Form �]�p�u�㪺���n���C
        InitializeComponent()

        '�b InitializeComponent() �I�s����[�J�Ҧ�����l�]�w

    End Sub

    'Form �мg Dispose �H�M������M��C
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    '�� Windows Form �]�p�u�㪺���n��
    Private components As System.ComponentModel.IContainer

    '�`�N: �H�U�� Windows Form �]�p�u��һݪ��{��
    '�z�i�H�ϥ� Windows Form �]�p�u��i��ק�C
    '�ФŨϥε{���X�s�边�ӭק�o�ǵ{�ǡC
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnEnter As System.Windows.Forms.Button
    Friend WithEvents txtVideoFileName As System.Windows.Forms.TextBox
    Friend WithEvents lblVideoFileName As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnEnter = New System.Windows.Forms.Button
        Me.txtVideoFileName = New System.Windows.Forms.TextBox
        Me.lblVideoFileName = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.SystemColors.Control
        Me.btnExit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExit.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(180, 89)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(130, 49)
        Me.btnExit.TabIndex = 11
        Me.btnExit.Text = "���}  "
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btnEnter
        '
        Me.btnEnter.BackColor = System.Drawing.SystemColors.Control
        Me.btnEnter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEnter.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEnter.Location = New System.Drawing.Point(20, 89)
        Me.btnEnter.Name = "btnEnter"
        Me.btnEnter.Size = New System.Drawing.Size(130, 49)
        Me.btnEnter.TabIndex = 10
        Me.btnEnter.Text = "��J"
        Me.btnEnter.UseVisualStyleBackColor = False
        '
        'txtVideoFileName
        '
        Me.txtVideoFileName.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtVideoFileName.Location = New System.Drawing.Point(3, 48)
        Me.txtVideoFileName.Name = "txtVideoFileName"
        Me.txtVideoFileName.Size = New System.Drawing.Size(333, 30)
        Me.txtVideoFileName.TabIndex = 9
        '
        'lblVideoFileName
        '
        Me.lblVideoFileName.BackColor = System.Drawing.SystemColors.Control
        Me.lblVideoFileName.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVideoFileName.ForeColor = System.Drawing.Color.Black
        Me.lblVideoFileName.Location = New System.Drawing.Point(3, 13)
        Me.lblVideoFileName.Name = "lblVideoFileName"
        Me.lblVideoFileName.Size = New System.Drawing.Size(333, 32)
        Me.lblVideoFileName.TabIndex = 8
        Me.lblVideoFileName.Text = "�п�J��v�ɦW:"
        '
        'FormKeyinVideoName
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(336, 150)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnEnter)
        Me.Controls.Add(Me.txtVideoFileName)
        Me.Controls.Add(Me.lblVideoFileName)
        Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "FormKeyinVideoName"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Keyin Video Name"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region
    Private Sub btnEnter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnter.Click
        Me.Hide()
        If txtVideoFileName.Text = "" Then
            txtVideoFileName.Text = Format(Now(), "Long Date")
        End If
        VideoFileName = txtVideoFileName.Text
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Hide()
    End Sub
    Private Sub txtVideoFileName_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtVideoFileName.MouseDown
        CallKeyboard1(sender, False)
    End Sub


End Class
