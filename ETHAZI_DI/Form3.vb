Imports MySql.Data.MySqlClient
Imports System.Text.RegularExpressions

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
        'En este metodo relleno el combobox de la municipalidad en base al codigo postal
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
            MsgBox(ex.Message)
        End Try
        Conexion.desconectar()
    End Sub
    Protected Sub fillCM(Optional municipality As String = Nothing)
        'En este metodo relleno el codigo municipal dependiendo de la municipalidad
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
            MsgBox(ex.Message)
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
            MsgBox(ex.Message)
        End Try
        Conexion.desconectar()
    End Sub

    Public Sub datosACargar(id As Integer)
        'Este metodo es el que llamo desde la vista del gridview(Form2) para llenar todos los datos de la vista del detalle, 
        'comprobando que no haya valores nulos para evitar excepciones

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
        'En este metodo dependiendo de si ha dado a editar datos o no hago diferentes cosas
        'Si esta en la parte de editar datos, revertira todos los cambios que haya hecho el usuario, por si se ha equivocado
        If Not editable Then
            datosACargar(IDfijo)
            Button2.PerformClick()
        Else
            'Si no esta en la parte de editar datos te devuelve a la ventana del gridview recargandolo por si ha editado algun valor
            Form2.mostrarTabla()
            Form2.Show()
            Me.Close()

        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'En este metodo hago editables o no los campos
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

            Button2.Text = "Modify"
            Button1.Text = "Back to global view"
            editable = True
            ejecutarQuery()

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
            Button2.Text = "Save changes"
            Button1.Text = "Cancel"

        End If


            Conexion.desconectar()


    End Sub

    Private Sub fillCategory()
        'Aqui se llena el combobox de las categorias
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
        'Aqui se llena el combobox de los tipos
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
        'Aqui llamo al metodo de cargar el codigo municipalidad pasandole por parametro el municipio
        muni = Me.cmb_municipio.SelectedItem
        fillCM(Me.cmb_municipio.SelectedItem)
    End Sub

    Private Sub Form3_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        Me.Close()
        Form2.Show()
    End Sub

    Private Sub Form3_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        'Este codigo es para que si el usuario da al boton escape, la visa de detalle se cierre automaticamente
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
        'Este metodo centra los formularios
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
    Private Function validar_Web(ByVal sWeb As String) As Boolean
        'Metodo para validar la web
        Dim valido As Boolean
        ' retorna true o false   
        Dim web() As String
        If Text_web.Text.Contains(".") Then
            web = Split(Text_web.Text, ".")
            If web(1) <> "" Then
                If web.Length > 1 Then
                    valido = True
                Else
                    valido = False
                End If

            End If
        Else
            valido = False
        End If


        Return valido
    End Function

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Metodo para mostrar el mapa del alojamiento cargado en la vista del detalle
        Mapa.alojamientoEspecifico = True
        Mapa.mostrarAlojamiento(Me.TextBox2.Text)
        Mapa.ShowDialog()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'Metodo para mostrar el informe del alojamiento cargado en la vista del detalle
        Form4.mostrarInformeEspecifico(Me.Text_nombre.Text)
        Form4.informeEspecifico = True
        Form4.ShowDialog()

    End Sub
    Private Sub Text_capacity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Text_capacity.KeyPress
        'Metodo para controlar la capacidad

        If (Asc(e.KeyChar) > 65 And Asc(e.KeyChar) < 90) Or (Asc(e.KeyChar) > 97 And Asc(e.KeyChar) < 122) Then
            e.Handled = True
            MsgBox("Este campo es solo numerico")

        Else

            e.Handled = False
        End If
       
    End Sub
    Private Function validar_Mail(ByVal sMail As String) As Boolean
        ' retorna true o false   
        Return Regex.IsMatch(sMail, _
                "^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$")
    End Function

    Private Sub Text_tel_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Text_tel.KeyPress
        'Metodo para validar el telefono
        If Text_tel.Text.Length < 9 Then
            If (Asc(e.KeyChar) > 65 And Asc(e.KeyChar) < 90) Or (Asc(e.KeyChar) > 97 And Asc(e.KeyChar) < 122) Then
                e.Handled = True
                MsgBox("Este campo es solo numerico")

            Else

                e.Handled = False
            End If
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub ejecutarQuery()
        'Metodo en el que si todos los valores son correctos, ejecute la query, y si no lo son, muestre cuales tienen errores
        Dim nombreErr, firmaErr, tipoErr, cpErr, cmErr, muniErr, descErr, friendlyErr, physicalErr, zipErr, aceptado As Boolean
        Dim sql As String
        Dim sMail As String
        Dim mailCorrecto, webCorrecta As Boolean
        sMail = Me.Text_email.Text
        If validar_Web(Text_web.Text) Then
            webCorrecta = True
        Else
            webCorrecta = False
        End If

        If Not validar_Mail(sMail) Or Me.Text_email.Text = "" Then

            PictureBox1.Visible = True
            mailCorrecto = False
        Else
            mailCorrecto = True
            PictureBox1.Visible = False
        End If
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
        If webCorrecta And Not nombreErr And Not firmaErr And Not tipoErr And Not cpErr And Not cmErr And Not muniErr And Not descErr And Not friendlyErr And Not physicalErr And Not zipErr And mailCorrecto Then
            aceptado = True
        End If

        If aceptado Then
            PictureBox2.Visible = False
            sql = "UPDATE lodging SET signatura='" & Me.Text_firma.Text & "',name='" & Me.Text_nombre.Text & "',description='" & Me.Text_desc.Text & "',type='" & Me.ComboBox1.SelectedItem & "',phone='" & Me.Text_tel.Text & "',address='" & Me.Text_direccion.Text & "',marks='" & Me.Text_marca.Text & "',postalcode='" & cp_in.SelectedItem & "',municipalitycode='" & Me.cmb_in.Text & "',coordinates='" & Me.Text_coord.Text & "',category='" & Me.ComboBox2.SelectedItem & "',turismemail='" & Me.Text_email.Text & "',web='" & Me.Text_web.Text & "',capacity=" & Me.Text_capacity.Text & ",friendlyurl='" & Me.Text_FUrl.Text & "',physicalurl='" & Me.Text_Url.Text & "',zipfile='" & Me.Text_zipfile.Text & "' WHERE id = " & Me.TextBox2.Text
            Dim cmd As New MySqlCommand(sql, Conexion.conection)
            cmd.ExecuteNonQuery()
        Else

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
            If Not webCorrecta Then
                PictureBox2.Visible = True
            Else
                PictureBox2.Visible = False
            End If
        End If
    End Sub

End Class