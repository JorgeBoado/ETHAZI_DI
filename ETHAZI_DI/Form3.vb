Imports MySql.Data.MySqlClient
Public Class Form3
    Private primercombo As String
    Private segundocombo As String
    Private IDfijo As Integer
    Private editable As Boolean = True
    Private codeM As String
    Private codeP As String
    Private muni As String = Nothing
    Private tipo As String
    Private categoria As String

    Protected Sub fillMunicipality(Optional cp As String = Nothing, Optional cm As String = Nothing)
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

            Dim sqlMu As String = "SELECT DISTINCT municipality FROM postalCode WHERE municipalitycode='" & cm & "' AND postalcode='" & cp & "' ORDER BY municipality ASC"
            Dim cmd2 As New MySqlCommand(sqlMu, Conexion.conection)
            Dim dr2 As MySqlDataReader = Nothing
            dr2 = cmd2.ExecuteReader

            While dr2.Read
                muni = dr2.Item(0)
            End While
            dr2.Close()

            Me.cmb_municipio.SelectedItem = muni

            fillCM(Me.cmb_municipio.SelectedItem)

        Catch ex As Exception
            MsgBox(ex.Message & " msgbox 1")
        End Try
        Conexion.desconectar()
    End Sub
    Protected Sub fillCM(Optional municipality As String = Nothing)

        Conexion.conectar()
        Try

            Dim sql As String = "SELECT DISTINCT municipalitycode FROM postalCode WHERE municipality = '" & muni & "'"
            Dim cmd As New MySqlCommand(sql, Conexion.conection)
            Dim dr As MySqlDataReader

            dr = cmd.ExecuteReader()

            While dr.Read
                Me.cmb_in.Text = dr.Item(0)
            End While
            dr.Close()

            fillCP(muni)

        Catch ex As Exception
            MsgBox(ex.Message & " msgbox 2")
        End Try
        Conexion.desconectar()
    End Sub
    Protected Sub fillCP(municipality As String)
        Me.cp_in.Items.Clear()
        Me.cp_in.Text = ""
        Conexion.conectar()
        Try

            Dim sql As String = "SELECT DISTINCT postalcode FROM postalCode WHERE municipalitycode='" & Me.cmb_in.Text & "' AND municipality='" & municipality & "' ORDER BY postalcode ASC"
            Dim cmd As New MySqlCommand(sql, Conexion.conection)
            Dim dr As MySqlDataReader

            dr = cmd.ExecuteReader()

            While dr.Read
                Me.cp_in.Items.Add(dr.Item(0))
            End While

            dr.Close()

            cp_in.SelectedItem = codeP

        Catch ex As Exception
            MsgBox(ex.Message & " msgbox 3")
        End Try
        Conexion.desconectar()
    End Sub

    Public Sub datosACargar(id As Integer)
        IDfijo = id
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
                    tipo = ""
                Else
                    tipo = dr.Item(4)
                End If
                If IsDBNull(dr.Item(5)) Or dr.Item(5) Is Nothing Then
                    Me.Text_tel.Text = ""
                Else
                    Me.Text_tel.Text = dr.Item(5)
                End If
                If IsDBNull(dr.Item(6)) Or dr.Item(6) Is Nothing Then
                    Me.Text_direccion.Text = ""
                Else
                    Me.Text_direccion.Text = dr.Item(6)
                End If
                If IsDBNull(dr.Item(7)) Or dr.Item(7) Is Nothing Then
                    Me.Text_marca.Text = ""
                Else
                    Me.Text_marca.Text = dr.Item(7)
                End If
                codeP = dr.Item(8)

                codeM = dr.Item(9)
                If IsDBNull(dr.Item(10)) Or dr.Item(10) Is Nothing Then
                    Me.Text_coord.Text = ""
                Else
                    Me.Text_coord.Text = dr.Item(10)
                End If
                If IsDBNull(dr.Item(11)) Or dr.Item(11) Is Nothing Then
                    categoria = ""
                Else
                    categoria = dr.Item(11)
                End If
                If IsDBNull(dr.Item(12)) Or dr.Item(12) Is Nothing Then
                    Me.Text_email.Text = ""
                Else
                    Me.Text_email.Text = dr.Item(12)
                End If
                If IsDBNull(dr.Item(13)) Or dr.Item(13) Is Nothing Then
                    Me.Text_web.Text = ""
                Else
                    Me.Text_web.Text = dr.Item(13)
                End If
                If IsDBNull(dr.Item(14)) Or dr.Item(14) Is Nothing Then
                    Me.Text_capacity.Text = ""
                Else
                    Me.Text_capacity.Text = dr.Item(14)
                End If
                If IsDBNull(dr.Item(15)) Or dr.Item(15) Is Nothing Then
                    Me.Text_FUrl.Text = ""
                Else
                    Me.Text_FUrl.Text = dr.Item(15)
                End If
                If IsDBNull(dr.Item(16)) Or dr.Item(16) Is Nothing Then
                    Me.Text_Url.Text = ""
                Else
                    Me.Text_Url.Text = dr.Item(16)
                End If
                If IsDBNull(dr.Item(17)) Or dr.Item(17) Is Nothing Then
                    Me.Text_zipfile.Text = ""
                Else
                    Me.Text_zipfile.Text = dr.Item(17)
                End If
            End While
        End If

        Conexion.desconectar()
    End Sub




    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        If Not editable Then
            datosACargar(IDfijo)
            Button2.PerformClick()
        Else
            Form2.Show()
            Me.Close()
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Conexion.conectar()

        Dim sql As String

        If Text_capacity.Enabled = True Then
            Text_capacity.Enabled = False
            Text_coord.Enabled = False

            Text_desc.Enabled = False
            Text_direccion.Enabled = False
            Text_email.Enabled = False
            Text_firma.Enabled = False
            Text_FUrl.Enabled = False
            cp_in.Enabled = False
            Text_marca.Enabled = False
            Text_nombre.Enabled = False
            Text_tel.Enabled = False
            ComboBox1.Enabled = False
            Text_Url.Enabled = False
            Text_web.Enabled = False
            Text_zipfile.Enabled = False
            cmb_municipio.Enabled = False
            ComboBox2.Enabled = False

            Button2.Text = "Editar"
            Button1.Text = "Volver a la vista global"
            editable = True
            sql = "UPDATE lodging SET signatura='" & Me.Text_firma.Text & "',name='" & Me.Text_nombre.Text & "',description='" & Me.Text_desc.Text & "',type='" & Me.ComboBox1.SelectedItem & "',phone='" & Me.Text_tel.Text & "',address='" & Me.Text_direccion.Text & "',marks='" & Me.Text_marca.Text & "',postalcode='" & cp_in.SelectedItem & "',municipalitycode='" & Me.cmb_in.Text & "',coordinates='" & Me.Text_coord.Text & "',category='" & Me.ComboBox2.SelectedItem & "',turismemail='" & Me.Text_email.Text & "',web='" & Me.Text_web.Text & "',capacity=" & Me.Text_capacity.Text & ",friendlyurl='" & Me.Text_FUrl.Text & "',physicalurl='" & Me.Text_Url.Text & "',zipfile='" & Me.Text_zipfile.Text & "' WHERE id = " & Me.TextBox2.Text
            Dim cmd As New MySqlCommand(sql, Conexion.conection)
            cmd.ExecuteNonQuery()


        Else
            Text_capacity.Enabled = True
            Text_coord.Enabled = True
            cp_in.Enabled = True
            Text_desc.Enabled = True
            Text_direccion.Enabled = True
            Text_email.Enabled = True
            Text_firma.Enabled = True
            Text_firma.Enabled = True
            Text_FUrl.Enabled = True
            cmb_municipio.Enabled = True
            Text_marca.Enabled = True
            Text_nombre.Enabled = True
            Text_tel.Enabled = True
            ComboBox1.Enabled = True
            Text_Url.Enabled = True
            Text_web.Enabled = True
            Text_zipfile.Enabled = True
            ComboBox2.Enabled = True

            editable = False
            Button2.Text = "Guardar cambios"
            Button1.Text = "Cancelar cambios"

        End If


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
        ComboBox2.SelectedItem = categoria
        dr.Close()
        Conexion.desconectar()
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
        ComboBox1.SelectedItem = tipo

        dr.Close()
        Conexion.desconectar()
    End Sub

    Private Sub cmb_municipio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_municipio.SelectedIndexChanged
        muni = Me.cmb_municipio.SelectedItem
        fillCM(Me.cmb_municipio.SelectedItem)
    End Sub
   
    Private Sub Form3_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
            Form2.Show()
        End If

    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles Me.Load
        form_center(Me)
        fillMunicipality(codeP, codeM)
        fillCategory()
        fillType()

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