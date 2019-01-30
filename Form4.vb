Imports MySql.Data.MySqlClient
Public Class Form4
    Public informeEspecifico As Boolean
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        form_center(Me)
        Conexion.conectar()
        Dim sql As String = "Select name from lodging"
        Dim cmd As New MySqlCommand(sql, Conexion.conection)
        Dim dr As MySqlDataReader
        dr = cmd.ExecuteReader
        While dr.Read
            ComboBox1.Items.Add(dr.Item(0))
        End While
        dr.Close()
        Conexion.desconectar()
        
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim oInforme As New CrystalReport2
        ' establecer la fórmula de selección de registros
        oInforme.RecordSelectionFormula = "{lodging1.name} = '" & Me.ComboBox1.Text & "'"
        Me.CrystalReportViewer1.ReportSource = oInforme

    End Sub
    Public Sub mostrarInformeEspecifico(nombre As String)

        Dim oInforme As New CrystalReport2
        ' establecer la fórmula de selección de registros
        oInforme.RecordSelectionFormula = "{lodging1.name} = '" & nombre & "'"
        Me.CrystalReportViewer1.ReportSource = oInforme



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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MsgBox(informeEspecifico)
        If informeEspecifico Then
            MsgBox("Estoy en el if")
            informeEspecifico = False
            Me.Close()

        Else
            MsgBox("Estoy en el else")
            Me.Close()
            Form2.Show()
        End If
    End Sub
End Class