Imports MySql.Data.MySqlClient
Public Class Form2


    'host: kasserver.synology.me
    'port: 3307
    'db:         reto_gp4
    'user:       gp4
    'pass:       MmlYOc8DvJXQns7D


    Friend conexion As MySqlConnection
    Dim hexColor As Color
    Dim das1 As New DataSet

    Public Sub mostrarTabla()

        Try
            'Para server
            Dim cadenaconexion As String = "server=kasserver.synology.me;database=reto_gp4;user id=gp4;port=3307; password=MmlYOc8DvJXQns7D;"
            'Para local

            'Dim cadenaconexion As String = "server=localhost;database=elorrieta;user id=root; password=;"


            conexion = New MySqlConnection(cadenaconexion)

            conexion.Open()

        Catch ex As MySqlException

        End Try
        Dim sql As String
        Dim mistring As String = ""
        sql = " SELECT id, signatura, name, type, phone, address, postalcode, turismemail, capacity,type FROM lodging"
        Dim commando As New MySqlCommand
        Dim adapter As New MySqlDataAdapter

        commando.Connection = conexion
        commando.CommandText = sql
        adapter.SelectCommand = commando
        'Dim dato As MySqlDataReader
        'Dim cont As Integer = 0
        das1.Clear()

        adapter.Fill(das1, "Cliente")

        Me.DataGridView2.DataSource = das1.Tables("Cliente")
        Me.DataGridView2.Columns(0).HeaderText = "Id"
        Me.DataGridView2.Columns(0).Width = 50
        Me.DataGridView2.Columns(1).HeaderText = "Name"
        Me.DataGridView2.Columns(2).HeaderText = "Signatura"
        Me.DataGridView2.Columns(3).HeaderText = "Type"
        Me.DataGridView2.Columns(4).HeaderText = "Phone"
        Me.DataGridView2.Columns(5).HeaderText = "Address"
        Me.DataGridView2.Columns(6).HeaderText = "Postalcode"
        Me.DataGridView2.Columns(7).HeaderText = "Turismemail"
        Me.DataGridView2.Columns(8).HeaderText = "Capacity"
        Me.DataGridView2.Columns(8).Width = 50
        Me.DataGridView2.Columns(9).HeaderText = "Type"
        Me.DataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.Gold
        Me.DataGridView2.RowsDefaultCellStyle.Font = New Font("Arial", 8, FontStyle.Bold)

    End Sub

    Private Sub Form2_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        Application.Exit()
    End Sub

    Private Sub Form2_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Application.Exit()
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        form_center(Me)
        mostrarTabla()

        hexColor = ColorTranslator.FromHtml("#ffd11a")



    End Sub


    Private Sub QueTalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QueTalToolStripMenuItem.Click
        Insertar.Show()
        Me.Hide()
    End Sub


    Private Sub CerrarSesionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CerrarSesionToolStripMenuItem.Click
        Form1.TextBox1.Focus()
        Me.Hide()
        Conex.Conexion.desconectar()

        Form1.Show()
    End Sub

    Private Sub BorrarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BorrarToolStripMenuItem.Click
        Conex.Conexion.conectar()
        Dim mifila As Integer
        mifila = DataGridView2.CurrentRow.Index
        Dim id As Integer
        Try

            id = DataGridView2.Item(0, mifila).Value

        Catch ex As Exception

        End Try
        Dim sql As String
        sql = "Delete from lodging where id = " & id
        Dim cmd As New MySqlCommand(sql, Conex.Conexion.conection)
        MsgBox("Registro " & id & " eliminado")
        cmd.ExecuteNonQuery()
        Conex.Conexion.desconectar()
        mostrarTabla()
    End Sub

    Private Sub VerDetalleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VerDetalleToolStripMenuItem.Click
        Dim mifila As Integer
        mifila = DataGridView2.CurrentRow.Index
        Dim id As Integer
        'Recorremos todas las columnas de la fila seleccionada en busca de un valor nulo

        Try

            id = DataGridView2.Item(0, mifila).Value

        Catch ex As Exception

        End Try


        Form3.datosACargar(id)
        Form3.Show()
        Me.Hide()
    End Sub


    Public Sub filtrar(nombre As String, categoria As String, capacidad As String, direccion As String, tipo As String)
        Conex.Conexion.conectar()
        Dim sql As String
        sql = "select municipalitycode from lodging where municipalitycode = (Select distinct municipalityCode from postalCode where municipality = '" & direccion & "')"
        Dim codigoMunicipal As Integer
        Dim cmd2 As New MySqlCommand(sql, conexion)
        Dim dr As MySqlDataReader
        dr = cmd2.ExecuteReader
        While dr.Read
            codigoMunicipal = dr.Item(0)
        End While
        dr.Close()

        If tipo <> "" Then
            If capacidad <> "" Then
                If direccion <> "" Then
                    sql = "SELECT id, signatura, name, type, phone, address, postalcode, turismemail, capacity, type FROM lodging where name like '" & nombre & "%' and category like '%" & categoria & "%' and capacity >= " & capacidad & " and municipalityCode = " & codigoMunicipal & " and type = '" & tipo & "'"
                Else
                    sql = "SELECT id, signatura, name, type, phone, address, postalcode, turismemail, capacity, type FROM lodging where name like '" & nombre & "%' and category like '%" & categoria & "%' and capacity >= " & capacidad & " and type = '" & tipo & "'"
                End If
            ElseIf direccion <> "" Then
                sql = "SELECT id, signatura, name, type, phone, address, postalcode, turismemail, capacity, type FROM lodging where name like '" & nombre & "%' and category like '%" & categoria & "%' and municipalityCode = " & codigoMunicipal & " and type = '" & tipo & "'"
            Else
                sql = "SELECT id, signatura, name, type, phone, address, postalcode, turismemail, capacity, type FROM lodging where name like '" & nombre & "%' and category like '%" & categoria & "%' and type = '" & tipo & "'"
            End If
        ElseIf capacidad <> "" Then
            If direccion <> "" Then
                sql = "SELECT id, signatura, name, type, phone, address, postalcode, turismemail, capacity, type FROM lodging where name like '" & nombre & "%' and category like '%" & categoria & "%' and capacity >= " & capacidad & " and municipalityCode = " & codigoMunicipal & ""
            Else
                sql = "SELECT id, signatura, name, type, phone, address, postalcode, turismemail, capacity, type FROM lodging where name like '" & nombre & "%' and category like '%" & categoria & "%' and capacity >= " & capacidad & ""
            End If
        ElseIf direccion <> "" Then

            sql = "SELECT id, signatura, name, type, phone, address, postalcode, turismemail, capacity, type FROM lodging where name like '" & nombre & "%' and category like '%" & categoria & "%' and municipalityCode = " & codigoMunicipal & ""

        Else
            sql = "SELECT id, signatura, name, type, phone, address, postalcode, turismemail, capacity, type FROM lodging where name like '" & nombre & "%' and category like '%" & categoria & "%'"

        End If

        Dim cmd As New MySqlCommand(sql, conexion)
        Dim adapter As New MySqlDataAdapter
        adapter.SelectCommand = cmd
        'Dim dato As MySqlDataReader
        'Dim cont As Integer = 0
        das1.Clear()

        adapter.Fill(das1, "Cliente")
        Me.DataGridView2.DataSource = das1.Tables("Cliente")
        Conex.Conexion.desconectar()


    End Sub

    Private Sub FiltrarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FiltrarToolStripMenuItem.Click
        Filtros.Show()
        Me.Hide()
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

    Private Sub MapaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MapaToolStripMenuItem.Click
        Mapa.alojamientoEspecifico = False
        Mapa.mostrarPorDefecto()
        Me.Hide()
        Mapa.Show()
    End Sub

    Private Sub ReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportToolStripMenuItem.Click
        Form4.Show()
        Me.Hide()
    End Sub
End Class