
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

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
      
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form2.Show()
        Me.Hide()
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

        sql = "Select distinct municipality from postalCode"
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
End Class