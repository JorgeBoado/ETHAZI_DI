Imports MySql.Data.MySqlClient
Public Class Insertar
    Private editable As Boolean

    Protected Sub fillMunicipality()
        Conexion.conectar()
        Try

            Dim sql As String = "SELECT DISTINCT municipality FROM postalCode ORDER BY municipality ASC"
            Dim cmd As New MySqlCommand(sql, Conexion.conection)
            Dim dr As MySqlDataReader

            dr = cmd.ExecuteReader()

            While dr.Read
                Me.cmb_municipio.Items.Add(dr.Item(0))
            End While
            dr.Close()
            Me.cmb_municipio.SelectedIndex = 1
            fillCM(Me.cmb_municipio.SelectedItem)


        Catch ex As Exception
            MsgBox(ex.Message & " msgbox 1")
        End Try
        Conexion.desconectar()
    End Sub
    Protected Sub fillCM(municipality As String)
        Conexion.conectar()
        Try

            Dim sql As String = "SELECT DISTINCT municipalitycode FROM postalCode WHERE municipality = '" & municipality & "'"
            Dim cmd As New MySqlCommand(sql, Conexion.conection)
            Dim dr As MySqlDataReader

            dr = cmd.ExecuteReader()

            While dr.Read
                Me.cmb_in.Text = dr.Item(0)
            End While
            dr.Close()
            fillCP(municipality)

        Catch ex As Exception
            MsgBox(ex.Message & " msgbox 2")
        End Try
        Conexion.desconectar()
    End Sub
    Protected Sub fillCP(municipality As String)
        Me.cp_in.Items.Clear()
        Conexion.conectar()
        Try

            Dim sql As String = "SELECT DISTINCT postalcode FROM postalCode WHERE municipalitycode=" & Me.cmb_in.Text & " AND municipality='" & municipality & "' ORDER BY postalcode ASC"
            Dim cmd As New MySqlCommand(sql, Conexion.conection)
            Dim dr As MySqlDataReader

            dr = cmd.ExecuteReader()

            While dr.Read
                Me.cp_in.Items.Add(dr.Item(0))
            End While

            dr.Close()

        Catch ex As Exception
            MsgBox(ex.Message & " msgbox 3")
        End Try
        Conexion.desconectar()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Conexion.conectar()
        Dim nombreErr, firmaErr, tipoErr, cpErr, cmErr, muniErr, descErr, friendlyErr, physicalErr, zipErr, aceptado As Boolean
        Dim sql As String
        If Text_firma.Text = "" Then
            firmaErr = True
        Else
            firmaErr = False
        End If
        If Text_nombre.Text = "" Then
            nombreErr = True
        Else
            nombreErr = False
        End If
        If cmb_municipio.Text = "" Then
            muniErr = True
        Else
            muniErr = False
        End If
        If cmb_in.Text = "" Then
            cmErr = True
        Else
            cmErr = False
        End If
        If cp_in.Text = "" Then
            cpErr = True
        Else
            cpErr = False
        End If
        If Text_desc.Text = "" Then
            descErr = True
        Else
            descErr = False
        End If
        If Text_FUrl.Text = "" Then
            friendlyErr = True
        Else
            friendlyErr = False
        End If
        If Text_Url.Text = "" Then
            physicalErr = True
        Else
            physicalErr = False
        End If
        If Text_zipfile.Text = "" Then
            zipErr = True
        Else
            zipErr = False
        End If
        If ComboBox1.Text = "" Then
            tipoErr = True
        Else
            tipoErr = False
        End If
        If Not nombreErr And Not firmaErr And Not tipoErr And Not cpErr And Not cmErr And Not muniErr And Not descErr And Not friendlyErr And Not physicalErr And Not zipErr Then
            aceptado = True
        End If

        If aceptado Then


            Try


                sql = "Insert into lodging (`signatura`, `name`, `description`, `type`, `phone`, `address`, `marks`, `postalcode`, `municipalitycode`, `coordinates`, `category`, `turismemail`, `web`, `capacity`, `friendlyurl`, `physicalurl`, `zipfile`) values (@signatura, @name, @description, @type, @phone, @address, @marks, @postalcode, @municipalitycode, @coordinates, @category, @turismemail, @web, @capacity, @friendlyurl, @physicalurl, @zipfile)"

                Dim cmd As New MySqlCommand(sql, Conexion.conection)
                cmd.Parameters.AddWithValue("@signatura", Me.Text_firma.Text)
                cmd.Parameters.AddWithValue("@name", Me.Text_nombre.Text)
                cmd.Parameters.AddWithValue("@description", Me.Text_desc.Text)
                cmd.Parameters.AddWithValue("@type", Me.ComboBox1.SelectedItem)
                cmd.Parameters.AddWithValue("@phone", Me.Text_tel.Text)
                cmd.Parameters.AddWithValue("@address", Me.Text_direccion.Text)
                cmd.Parameters.AddWithValue("@marks", Me.Text_marca.Text)
                cmd.Parameters.AddWithValue("@postalcode", Me.cp_in.SelectedItem)
                cmd.Parameters.AddWithValue("@municipalitycode", cmb_in.Text)
                cmd.Parameters.AddWithValue("@coordinates", Me.Text_coord.Text)
                cmd.Parameters.AddWithValue("@category", Me.ComboBox2.SelectedItem)
                cmd.Parameters.AddWithValue("@turismemail", Me.Text_email.Text)
                cmd.Parameters.AddWithValue("@web", Me.Text_web.Text)
                cmd.Parameters.AddWithValue("@capacity", Me.Text_capacity.Text)
                cmd.Parameters.AddWithValue("@friendlyurl", Me.Text_FUrl.Text)
                cmd.Parameters.AddWithValue("@physicalurl", Me.Text_Url.Text)
                cmd.Parameters.AddWithValue("@zipfile", Me.Text_zipfile.Text)



                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox(firmaErr)
            If zipErr Then
                picZip.Visible = True
            Else
                picZip.Visible = False
            End If
            If descErr Then
                picDesc.Visible = True
            Else
                picDesc.Visible = False
            End If
            If physicalErr Then
                picPhy.Visible = True
            Else
                picPhy.Visible = False
            End If
            If friendlyErr Then
                picFriend.Visible = True
            Else
                picFriend.Visible = False
            End If
            If cpErr Then
                picCP.Visible = True
            Else
                picCP.Visible = False
            End If
            If cmErr Then
                picCM.Visible = True
            Else
                picCM.Visible = False
            End If
            If tipoErr Then
                picType.Visible = True
            Else
                picType.Visible = False
            End If
            If firmaErr Then
                picSign.Visible = True
            Else
                picSign.Visible = False
            End If
            If nombreErr Then
                picName.Visible = True
            Else
                picName.Visible = False
            End If
        End If


        Conexion.desconectar()
    End Sub

    Private Sub Insertar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        form_center(Me)
        Conexion.conectar()
        fillMunicipality()
        fillType()
        fillCategory()
        Conexion.desconectar()
    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs)
        e.Handled = True
    End Sub



    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)

        Conexion.conectar()
        Dim cargarCp As String
        cargarCp = "Select municipalitycode from postalCode where postalcode = '" & Me.cp_in.SelectedItem & "'"

        Dim cmd As New MySqlCommand(cargarCp, Conexion.conection)
        Dim dr As MySqlDataReader
        dr = cmd.ExecuteReader()
        While dr.Read

            Me.cmb_in.Text = dr.Item(0)
        End While
        dr.Close()
        Conexion.desconectar()
    End Sub

    Private Sub ComboBox2_KeyPress(sender As Object, e As KeyPressEventArgs)
        e.Handled = True
    End Sub

    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
            Form2.Show()
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        
            Form2.Show()
            Me.Close()
    End Sub

    Private Sub Text_tel_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub cmb_municipio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_municipio.SelectedIndexChanged
        fillCM(Me.cmb_municipio.SelectedItem)
    End Sub

    Private Sub fillType()
        Conexion.conectar()
        Dim sql As String
        sql = "select distinct type from lodging"
        Dim cmd As New MySqlCommand(sql, Conexion.conection)
        Dim dr As MySqlDataReader
        dr = cmd.ExecuteReader
        While dr.Read()
            Me.ComboBox1.Items.Add(dr.Item(0))
        End While
        dr.Close()
        Conexion.desconectar()
    End Sub

    Private Sub fillCategory()
        Conexion.conectar()
        Dim sql As String
        sql = "select distinct category from lodging"
        Dim cmd As New MySqlCommand(sql, Conexion.conection)
        Dim dr As MySqlDataReader
        dr = cmd.ExecuteReader
        While dr.Read()
            Me.ComboBox2.Items.Add(dr.Item(0))
        End While
        dr.Close()
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