Imports MySql.Data.MySqlClient
Module Conex
    Public Class Conexion

        Public Shared cadenaconexion As String = "server=kasserver.synology.me;database=reto_gp4;user id=gp4;port=3307; password=MmlYOc8DvJXQns7D;"
        'Para server
        'Dim cadenaconexion As String = "server=kasserver.synology.me;database=reto_gp4;user id=gp4;port=3307; password=MmlYOc8DvJXQns7D;"
        'Para local
        'Me.Contactos_localTableAdapter.Fill(Me.AmigosDataSet.Contactos_local)

        Public Shared conection As New MySqlConnection(cadenaconexion)

        Public Shared Sub conectar()
            Try
                If conection.State = ConnectionState.Closed Then
                    conection.Open()
                End If

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End Sub
        Public Shared Sub desconectar()
            Try
                If conection.State = ConnectionState.Open Then
                    conection.Close()
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub

    End Class

End Module
