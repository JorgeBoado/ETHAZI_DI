Imports MySql.Data.MySqlClient
Public Class Insertar

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Conexion.conectar()
        Dim sql As String
        sql = "Insert into lodging (`signatura`, `name`, `description`, `type`, `phone`, `address`, `marks`, `postalcode`, `municipalitycode`, `coordinates`, `category`, `turismemail`, `web`, `capacity`, `friendlyurl`, `physicalurl`, `zipfile`) values (@signatura, @name, @description, @type, @phone, @address, @marks, @postalcode, @municipalitycode, @coordinates, @category, @turismemail, @web, @capacity, @friendlyurl, @physicalurl, @zipfile)"

        Dim cmd As New MySqlCommand(sql, Conexion.conection)
        cmd.Parameters.AddWithValue("@signatura", Me.Text_firma.Text)
        cmd.Parameters.AddWithValue("@name", Me.Text_nombre.Text)
        cmd.Parameters.AddWithValue("@description", Me.Text_desc.Text)
        cmd.Parameters.AddWithValue("@type", Me.Text_Tipo.Text)
        cmd.Parameters.AddWithValue("@phone", Me.Text_tel.Text)
        cmd.Parameters.AddWithValue("@address", Me.Text_direccion.Text)
        cmd.Parameters.AddWithValue("@marks", Me.Text_marca.Text)
        cmd.Parameters.AddWithValue("@postalcode", Me.ComboBox1.SelectedItem)
        cmd.Parameters.AddWithValue("@municipalitycode", ComboBox2.SelectedItem)
        cmd.Parameters.AddWithValue("@coordinates", Me.Text_coord.Text)
        cmd.Parameters.AddWithValue("@category", Me.txt_categoria.Text)
        cmd.Parameters.AddWithValue("@turismemail", Me.Text_email.Text)
        cmd.Parameters.AddWithValue("@web", Me.Text_web.Text)
        cmd.Parameters.AddWithValue("@capacity", Me.Text_capacity.Text)
        cmd.Parameters.AddWithValue("@friendlyurl", Me.Text_FUrl.Text)
        cmd.Parameters.AddWithValue("@physicalurl", Me.Text_Url.Text)
        cmd.Parameters.AddWithValue("@zipfile", Me.Text_zipfile.Text)

        If (Me.ComboBox1.SelectedItem <> "" And Me.ComboBox2.SelectedItem <> "") Then
            MsgBox(sql)
            cmd.ExecuteNonQuery()
        End If
        Conexion.desconectar()
    End Sub

    Private Sub Insertar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Conexion.conectar()
        Dim cargarCp As String
        cargarCp = "Select postalcode from postalCode"
        Dim cmd As New MySqlCommand(cargarCp, Conexion.conection)
        Dim dr As MySqlDataReader
        dr = cmd.ExecuteReader()
        While dr.Read
            Me.ComboBox1.Items.Add(dr.Item(0))
        End While
        Conexion.desconectar()
    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        e.Handled = True
    End Sub

   

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Me.ComboBox2.Items.Clear()
        Conexion.conectar()
        Dim cargarCp As String
        cargarCp = "Select municipalitycode from postalCode where postalcode = '" & Me.ComboBox1.SelectedItem & "'"

        Dim cmd As New MySqlCommand(cargarCp, Conexion.conection)
        Dim dr As MySqlDataReader
        dr = cmd.ExecuteReader()
        While dr.Read
            MsgBox(dr.Item(0))
            Me.ComboBox2.Items.Add(dr.Item(0))
        End While
        Conexion.desconectar()
    End Sub

    Private Sub ComboBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox2.KeyPress
        e.Handled = True
    End Sub

    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
            Form2.Show()
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        Form2.Show()
    End Sub
End Class