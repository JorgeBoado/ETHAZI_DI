
Imports MySql.Data.MySqlClient
Public Class Filtros
    Public alo As New Alojamiento()
    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs)
        'Module1.Conexion.conectar()

        'Dim sql As String = "SELECT	turismemail FROM Lodging"



    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


        Form2.Show()
        Form2.filtrar(TextBox1.Text, ComboBox3.SelectedItem, TextBox3.Text, ComboBox2.SelectedItem, ComboBox1.SelectedItem)
        Me.Hide()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
       
    End Sub

   

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form2.Show()
        Me.Hide()
    End Sub

    Private Sub Filtros_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        Me.Close()
        Form2.Show()
    End Sub

    Private Sub Filtros_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
            Form2.Show()
        End If

    End Sub

    Private Sub Filtros_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        form_center(Me)
        Conexion.conectar()
        Dim sql As String
        sql = "Select distinct type from lodging"
        Dim cmd As New MySqlCommand(sql, Conexion.conection)
        Dim dr As MySqlDataReader
        dr = cmd.ExecuteReader
        While dr.Read
            ComboBox1.Items.Add(dr.Item(0))
        End While
        dr.Close()

        sql = "Select distinct municipality from postalCode order by municipality asc"
        cmd.Connection = Conexion.conection
        cmd.CommandText = sql

        dr = cmd.ExecuteReader
        While dr.Read
            ComboBox2.Items.Add(dr.Item(0))
        End While
        dr.Close()
        sql = "Select distinct category from lodging"
        cmd.Connection = Conexion.conection
        cmd.CommandText = sql

        dr = cmd.ExecuteReader
        While dr.Read
            ComboBox3.Items.Add(dr.Item(0))
        End While
        Me.ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        Me.ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList
        Me.ComboBox3.DropDownStyle = ComboBoxStyle.DropDownList

        Conexion.desconectar()
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

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        e.Handled = True
    End Sub

    Private Sub ComboBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox2.KeyPress
        e.Handled = True
    End Sub

    Private Sub ComboBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox3.KeyPress
        e.Handled = True
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If (Asc(e.KeyChar) > 65 And Asc(e.KeyChar) < 90) Or (Asc(e.KeyChar) > 97 And Asc(e.KeyChar) < 122) Then
            e.Handled = True
            MsgBox("Este campo es solo numerico")
        Else
            e.Handled = False
        End If

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Me.ComboBox3.SelectedIndex = -1
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Me.ComboBox2.SelectedIndex = -1
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Me.ComboBox1.SelectedIndex = -1
    End Sub

  
End Class