Public Class Alojamiento
    Private nombre As String
    Private direccion As String
    Private municipio As String
    Private provincia As String
    Private codPostal As Integer
    Private telefono As Integer
    Private email As String
    Private sitioweb As String



    Public Sub New(ByVal nom As String, ByVal dir As String, ByVal mun As String, ByVal prov As String, ByVal cod As Integer, ByVal tel As Integer, ByVal em As String, ByVal sitio As String)
        nombre = nom
        direccion = dir
        municipio = mun
        provincia = prov
        codPostal = cod
        telefono = tel
        email = em
        sitioweb = sitio
    End Sub

    Sub New()

    End Sub

    Public Function getNombre() As String
        Return nombre
    End Function

    Public Function getDireccion() As String
        Return direccion
    End Function

    Public Function getMunicipio() As String
        Return municipio
    End Function

    Public Function getProvincia() As String
        Return provincia
    End Function

    Public Function getCodPostal() As Integer
        Return codPostal
    End Function

    Public Function getTelefono() As Integer
        Return telefono
    End Function

    Public Function getEmail() As String
        Return email
    End Function

    Public Function getSitioWeb() As String
        Return sitioweb
    End Function

    Public Sub setNombre(ByVal nom As String)
        nombre = nom
    End Sub

    Public Sub setDireccion(ByVal dir As String)
        direccion = dir
    End Sub

    Public Sub setMunicipio(ByVal mun As String)
        municipio = mun
    End Sub

    Public Sub setProvincia(ByVal prov As String)
        provincia = prov
    End Sub

    Public Sub setCodPostal(ByVal cod As Integer)
        codPostal = cod
    End Sub

    Public Sub setTelefono(ByVal tel As Integer)
        telefono = tel
    End Sub

    Public Sub setEmail(ByVal em As String)
        email = em
    End Sub

    Public Sub setSitioWeb(ByVal sw As String)
        sitioweb = sw
    End Sub



End Class
