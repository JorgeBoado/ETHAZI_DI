
Imports MySql.Data.MySqlClient
Public Class Filtros
    Public alo As New Alojamiento()
    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        'Module1.Conexion.conectar()

        'Dim sql As String = "SELECT	turismemail FROM Lodging"

      

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


        Form2.Show()
        Form2.filtrar(TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, ComboBox1.SelectedItem)
        Me.Hide()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
       
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
      
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
       
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
        Conexion.conectar()
        Dim sql As String
        sql = "Select distinct type from lodging"
        Dim cmd As New MySqlCommand(sql, Conexion.conection)
        Dim dr As MySqlDataReader
        dr = cmd.ExecuteReader
        While dr.Read
            ComboBox1.Items.Add(dr.Item(0))
        End While


    End Sub
End Class