Imports MySql.Data.MySqlClient
Public Class Form2


    'host: kasserver.synology.me
    'port: 3307
    'db:         reto_gp4
    'user:       gp4
    'pass:       MmlYOc8DvJXQns7D


    Friend conexion As MySqlConnection
    Dim das1 As New DataSet

    Private Sub mostrarTabla()
        Try
            'Para server
            Dim cadenaconexion As String = "server=kasserver.synology.me;database=reto_gp4;user id=gp4;port=3307; password=MmlYOc8DvJXQns7D;"
            'Para local

            'Dim cadenaconexion As String = "server=localhost;database=elorrieta;user id=root; password=;"


            conexion = New MySqlConnection(cadenaconexion)

            conexion.Open()

        Catch ex As MySqlException

        End Try
        Label1.Parent = PictureBox1
        Label1.BackColor = Color.Transparent
        Dim sql As String
        Dim mistring As String = ""
        sql = " SELECT id, signatura, name, type, phone, address, postalcode, turismemail FROM lodging"
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


    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        
        mostrarTabla()


    End Sub


    Private Sub QueTalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QueTalToolStripMenuItem.Click
        Insertar.Show()
        Me.Hide()
    End Sub

   
    Private Sub CerrarSesionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CerrarSesionToolStripMenuItem.Click
        Me.Close()
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
        Me.Close()
    End Sub


    Public Sub filtrar(nombre As String, categoria As String, capacidad As String, direccion As String)
        Conex.Conexion.conectar()
        Dim sql As String

        sql = "SELECT id, signatura, name, type, phone, address, postalcode, turismemail FROM lodging where name like '%" & nombre & "%' and category like '%" & categoria & "%' and capacity like '%" & capacidad & "%' and address like '%" & direccion & "%'"
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
        Me.Close()
    End Sub
End Class