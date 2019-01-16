Imports MySql.Data.MySqlClient
Public Class Form3
    Private primercombo As String
    Private segundocombo As String

    Public Sub datosACargar(id As Integer)
        Conexion.conectar()
        Dim sql As String = "Select * from lodging where id= " & id
        Dim cmd As New MySqlCommand(sql, Conexion.conection)

        Dim dr As MySqlDataReader

        dr = cmd.ExecuteReader()
        If dr.HasRows Then
            While dr.Read
                Me.TextBox2.Text = dr.Item(0)
                Me.Text_firma.Text = dr.Item(1)
                If IsDBNull(dr.Item(2)) Or dr.Item(2) Is Nothing Then
                    Me.Text_nombre.Text = ""
                Else
                    Me.Text_nombre.Text = dr.Item(2)
                End If
                If IsDBNull(dr.Item(3)) Or dr.Item(3) Is Nothing Then
                    Me.Text_desc.Text = ""
                Else
                    Me.Text_desc.Text = dr.Item(3)
                End If
                If IsDBNull(dr.Item(4)) Or dr.Item(4) Is Nothing Then
                    Me.Text_Tipo.Text = ""
                Else
                    Me.Text_Tipo.Text = dr.Item(4)
                End If
                If IsDBNull(dr.Item(5)) Or dr.Item(5) Is Nothing Then
                    Me.Text_tel.Text = ""
                Else
                    Me.Text_tel.Text = dr.Item(5)
                End If
                If IsDBNull(dr.Item(6)) Or dr.Item(6) Is Nothing Then
                    Me.Text_loc.Text = ""
                Else
                    Me.Text_loc.Text = dr.Item(6)
                End If
                If IsDBNull(dr.Item(7)) Or dr.Item(7) Is Nothing Then
                    Me.Text_direccion.Text = ""
                Else
                    Me.Text_direccion.Text = dr.Item(7)
                End If
                If IsDBNull(dr.Item(8)) Or dr.Item(8) Is Nothing Then
                    Me.Text_marca.Text = ""
                Else
                    Me.Text_marca.Text = dr.Item(8)
                End If
                If IsDBNull(dr.Item(9)) Or dr.Item(9) Is Nothing Then
                    ComboBox1.Text = ""
                Else
                    ComboBox1.Text = dr.Item(9)
                    primercombo = ComboBox1.Text
                End If
                If IsDBNull(dr.Item(10)) Or dr.Item(10) Is Nothing Then
                    ComboBox2.Text = ""
                Else
                    ComboBox2.Text = dr.Item(10)
                    segundocombo = ComboBox2.Text
                End If
                If IsDBNull(dr.Item(11)) Or dr.Item(11) Is Nothing Then
                    Me.Text_coord.Text = ""
                Else
                    Me.Text_coord.Text = dr.Item(11)
                End If
                If IsDBNull(dr.Item(12)) Or dr.Item(12) Is Nothing Then
                    Me.txt_categoria.Text = ""
                Else
                    Me.txt_categoria.Text = dr.Item(12)
                End If
                If IsDBNull(dr.Item(13)) Or dr.Item(13) Is Nothing Then
                    Me.Text_email.Text = ""
                Else
                    Me.Text_email.Text = dr.Item(13)
                End If
                If IsDBNull(dr.Item(14)) Or dr.Item(14) Is Nothing Then
                    Me.Text_web.Text = ""
                Else
                    Me.Text_web.Text = dr.Item(14)
                End If
                If IsDBNull(dr.Item(15)) Or dr.Item(15) Is Nothing Then
                    Me.Text_capacity.Text = ""
                Else
                    Me.Text_capacity.Text = dr.Item(15)
                End If
                If IsDBNull(dr.Item(16)) Or dr.Item(16) Is Nothing Then
                    Me.Text_FUrl.Text = ""
                Else
                    Me.Text_FUrl.Text = dr.Item(16)
                End If
                If IsDBNull(dr.Item(17)) Or dr.Item(17) Is Nothing Then
                    Me.Text_Url.Text = ""
                Else
                    Me.Text_Url.Text = dr.Item(17)
                End If
                If IsDBNull(dr.Item(18)) Or dr.Item(18) Is Nothing Then
                    Me.Text_zipfile.Text = ""
                Else
                    Me.Text_zipfile.Text = dr.Item(18)
                End If
            End While
        End If
        Conexion.desconectar()
    End Sub

    
   

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click

        Form2.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Conexion.conectar()

        Dim sql As String
        sql = "select distinct postalcode, municipalitycode from lodging"
        Dim cargar As New MySqlCommand(sql, Conexion.conection)
        Dim dr As MySqlDataReader
        dr = cargar.ExecuteReader
        While dr.Read
            ComboBox1.Items.Add(dr.Item(0))
            ComboBox2.Items.Add(dr.Item(1))

        End While
        dr.Close()
        ComboBox1.SelectedItem = primercombo
        ComboBox2.SelectedItem = segundocombo

        If Text_capacity.Enabled = True Then
            Text_capacity.Enabled = False
            Text_coord.Enabled = False
            ComboBox1.Enabled = False
            Text_desc.Enabled = False
            Text_direccion.Enabled = False
            Text_email.Enabled = False
            Text_firma.Enabled = False
            Text_FUrl.Enabled = False
            Text_loc.Enabled = False
            Text_marca.Enabled = False
            Text_nombre.Enabled = False
            Text_tel.Enabled = False
            Text_Tipo.Enabled = False
            Text_Url.Enabled = False
            Text_web.Enabled = False
            Text_zipfile.Enabled = False
            txt_categoria.Enabled = False
            ComboBox2.Enabled = False
            Button2.Text = "Editar"
            sql = "UPDATE lodging SET signatura='" & Me.Text_firma.Text & "',name='" & Me.Text_nombre.Text & "',description='" & Me.Text_desc.Text & "',type='" & Me.Text_Tipo.Text & "',phone='" & Me.Text_tel.Text & "',locality='" & Me.Text_loc.Text & "',address='" & Me.Text_direccion.Text & "',marks='" & Me.Text_marca.Text & "',postalcode='" & ComboBox1.SelectedItem & "',municipalitycode='" & Me.ComboBox2.SelectedItem & "',coordinates='" & Me.Text_coord.Text & "',category='" & Me.txt_categoria.Text & "',turismemail='" & Me.Text_email.Text & "',web='" & Me.Text_web.Text & "',capacity=" & Me.Text_capacity.Text & ",friendlyurl='" & Me.Text_FUrl.Text & "',physicalurl='" & Me.Text_Url.Text & "',zipfile='" & Me.Text_zipfile.Text & "' WHERE id = " & Me.TextBox2.Text
            Dim cmd As New MySqlCommand(sql, Conexion.conection)
            cmd.ExecuteNonQuery()

        Else
            Text_capacity.Enabled = True
            Text_coord.Enabled = True
            ComboBox1.Enabled = True
            Text_desc.Enabled = True
            Text_direccion.Enabled = True
            Text_email.Enabled = True
            Text_firma.Enabled = True
            Text_firma.Enabled = True
            Text_FUrl.Enabled = True
            Text_loc.Enabled = True
            Text_marca.Enabled = True
            Text_nombre.Enabled = True
            Text_tel.Enabled = True
            Text_Tipo.Enabled = True
            Text_Url.Enabled = True
            Text_web.Enabled = True
            Text_zipfile.Enabled = True
            txt_categoria.Enabled = True
            ComboBox2.Enabled = True
            Button2.Text = "Guardar cambios"


        End If


        Conexion.desconectar()


    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

 
   
End Class