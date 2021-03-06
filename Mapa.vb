﻿Imports MySql.Data.MySqlClient
Public Class Mapa
    Public alojamientoEspecifico As Boolean
    Private Sub Mapa_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
            Form2.Show()
        End If

    End Sub
    Public Sub mostrarAlojamiento(id As Integer)
        Conexion.conectar()
        Dim sql As String = "select coordinates from lodging where id = " & id
        Dim cmd As New MySqlCommand(sql, Conexion.conection)
        Dim coord As String = ""

        Dim dr As MySqlDataReader
        dr = cmd.ExecuteReader
        While dr.Read
            coord = dr.Item(0)
        End While

        Dim urlMaps As String
        'Concatenamos la dirección con el Textbox añadimos la última sentencia para indicar que sólo se muestre el mapa
        'urlMaps = "http://maps.google.es/maps?q=" & txtdireccion.Text & "&output=embed" 'Creamos una variable direccion para que el WebBrowser lo pueda abrir puesto que no puede abrir directamente un string
        urlMaps = "http://maps.google.es/maps?q=" & coord
        Dim direccion As New Uri(urlMaps)
        'ASignamos como URL la direccion
        WebBrowser1.Url = direccion
        alojamientoEspecifico = True
        Conexion.desconectar()
    End Sub

    Private Sub Mapa_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        form_center(Me)
        Conexion.conectar()
        Dim sql As String
        sql = "select name from lodging"
        Dim cmd As New MySqlCommand(sql, Conexion.conection)
        Dim dr As MySqlDataReader
        dr = cmd.ExecuteReader
        While dr.Read
            Me.ToolStripComboBox1.Items.Add(dr.Item(0))
        End While



        Conexion.desconectar()
    End Sub



    Private Sub ToolStripComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        Conexion.conectar()
        Dim coord As String = ""
        Dim sql As String
        sql = "select coordinates from lodging where name = '" & Me.ToolStripComboBox1.SelectedItem & "'"
        Dim cmd As New MySqlCommand(sql, Conexion.conection)
        Dim dr As MySqlDataReader
        dr = cmd.ExecuteReader
        While dr.Read

            coord = dr.Item(0)
        End While

        'Creamos variable para almacenar la url
        Dim urlMaps As String
        'Concatenamos la dirección con el Textbox añadimos la última sentencia para indicar que sólo se muestre el mapa
        'urlMaps = "http://maps.google.es/maps?q=" & txtdireccion.Text & "&output=embed" 'Creamos una variable direccion para que el WebBrowser lo pueda abrir puesto que no puede abrir directamente un string
        urlMaps = "http://maps.google.es/maps?q=" & coord
        Dim direccion As New Uri(urlMaps)
        'ASignamos como URL la direccion
        WebBrowser1.Url = direccion
        Conexion.desconectar()
    End Sub

    Private Sub AtrasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AtrasToolStripMenuItem.Click
        Me.Close()
        Form2.Show()
    End Sub
    Public Sub form_center(ByVal frm As Form, Optional ByVal parent As Form = Nothing)
        'Esta funcion es para que el formulario aparezca en el centro de la pantalla
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