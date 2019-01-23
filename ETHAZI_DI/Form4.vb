Imports MySql.Data.MySqlClient
Public Class Form4

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
End Class