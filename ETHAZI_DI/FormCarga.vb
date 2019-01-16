Public Class FormCarga


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer.Enabled = True
        
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

   
End Class