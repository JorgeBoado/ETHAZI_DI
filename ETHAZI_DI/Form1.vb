Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class Form1
    Dim dr As MySqlDataReader
    Dim commando As New MySqlCommand
    Dim adapter As New MySqlDataAdapter
    Dim sql As String
    Dim usuarios As New ArrayList
    Dim passwords As New ArrayList
    Dim ojito As Boolean = True
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'If TextBox1.Text = "" Then
        '    Form2.Show()
        '    Me.Hide()
        'End If

        Dim sqlcmd As New MySqlCommand(sql, conexion)
        Dim cadena As String = ""
        Dim nombrecorrecto As Boolean
        Dim passcorrecta As Boolean
        Dim hola As String
        hola = encriptar(Me.TextBox2.Text)

        sql = " SELECT apellido FROM Empleados"
        For Each item In usuarios
            If TextBox1.Text = item Then
                nombrecorrecto = True
            End If
        Next
        For Each item2 In passwords
            If hola = item2 Then
                passcorrecta = True
            End If
        Next
        If nombrecorrecto And passcorrecta Then

            Form2.Show()
            Form2.Hide()
            FormCarga.Show()
            TextBox1.Text = ""
            TextBox2.Text = ""
            Me.Label3.Text = ""
            Me.TextBox1.Focus()
            Me.Hide()
        Else
            Me.Label3.Text = "Incorrect user or password"
            Me.TextBox1.Text = ""
        End If
        Label3.Visible = True
    End Sub
    Friend conexion As MySqlConnection
    Dim das1 As New DataSet
    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
    
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        form_center(Me)
        Me.TextBox2.PasswordChar = "*"

        Try
            'Para server

            'Para local

            'Dim cadenaconexion As String = "server=localhost;database=elorrieta;user id=root; password=;"
            Dim cadenaconexion As String = "server=kasserver.synology.me;database=reto_gp4;user id=gp4;port=3307; password=MmlYOc8DvJXQns7D;"

            conexion = New MySqlConnection(cadenaconexion)

            conexion.Open()

        Catch ex As MySqlException

        End Try

        Dim sql As String
        Dim mistring As String = ""
        sql = " SELECT nick, pass FROM admins"


        commando.Connection = conexion
        commando.CommandText = sql
        adapter.SelectCommand = commando
        'Dim dato As MySqlDataReader
        'Dim cont As Integer = 0
        das1.Clear()

        adapter.Fill(das1, "Cliente")

        Dim sqlcmd As New MySqlCommand(sql, conexion)

        dr = sqlcmd.ExecuteReader
        Dim linea As String = ""
        Dim pass As String = ""
        While dr.Read

            linea = dr(0)
            pass = dr(1)
            usuarios.Add(linea)
            passwords.Add(pass)
        End While
    End Sub

    Function encriptar(pass As String) As String
        Dim SHA256 As SHA256 = SHA256Managed.Create()
        Dim hash() As Byte = SHA256.ComputeHash(Encoding.UTF8.GetBytes(pass))

        Dim res As String = ""
        For i = 0 To hash.Length - 1
            res &= hash(i).ToString("X2")
        Next

        'Console.WriteLine(res.ToLower)
        Return res.ToLower
    End Function

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        If ojito Then
            PictureBox2.Image = My.Resources.pupila_del_ojo
            TextBox2.PasswordChar = ""
            ojito = False
        Else
            PictureBox2.Image = My.Resources.ojo_cerrado
            TextBox2.PasswordChar = "*"
            ojito = True
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Application.Exit()
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
