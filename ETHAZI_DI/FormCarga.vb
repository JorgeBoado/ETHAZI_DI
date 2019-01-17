Public Class FormCarga


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer.Enabled = True
        form_center(Me)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        If ProgressBar1.Value < 100 Then
            ProgressBar1.Value = ProgressBar1.Value + 2
            Label1.Text = (ProgressBar1.Value & "%")

            If ProgressBar1.Value = 100 Then

                Form2.Show()
                Me.Close()
            End If
        End If
    End Sub
    Public Sub form_center(ByVal frm As Form, Optional ByVal parent As Form = Nothing)

        Dim x As Integer
        Dim y As Integer
        Dim r As Rectangle

        If Not parent Is Nothing Then
            r = parent.ClientRectangle
            x = r.Width - frm.Width + parent.Left
            y = r.Height - frm.Height + parent.Top
        Else
            r = Screen.PrimaryScreen.WorkingArea
            x = r.Width - frm.Width
            y = r.Height - frm.Height
        End If

        x = CInt(x / 2)
        y = CInt(y / 2)

        frm.StartPosition = FormStartPosition.Manual
        frm.Location = New Point(x, y)
    End Sub

   
End Class