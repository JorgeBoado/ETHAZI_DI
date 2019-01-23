Imports CrystalDecisions.CrystalReports.Engine

Public Class rptPrueba
    Inherits ReportClass

    Public Sub New()
        MyBase.New()
    End Sub

    Public Overrides Property ResourceName As [String]
        Get
            Return "rptPrueba.rpt"
        End Get
        Set(value As [String])

        End Set
    End Property

End Class
