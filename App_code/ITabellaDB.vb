<SerializableAttribute()> _
Public MustInherit Class ITabellaDB

    Protected Shared Sub addParametro(Cmd As System.Data.SqlClient.SqlCommand, NomePar As String, Tipo As System.Data.SqlDbType, Valore As Object)
        If Valore Is DBNull.Value Or Valore Is Nothing Then
            'HttpContext.Current.Trace.Write("addParametro DBNull: " & NomePar & " - " & Tipo & " - " & Valore)
            Cmd.Parameters.Add(NomePar, Tipo).Value = DBNull.Value
        Else
            'HttpContext.Current.Trace.Write("addParametro Valore: " & NomePar & " - " & Tipo & " - " & Valore)
            Cmd.Parameters.Add(NomePar, Tipo).Value = Valore
        End If
    End Sub

    Protected Shared Function SubstringSicuroNothing(Valore As Object, MaxLen As Integer) As Object
        If Valore Is Nothing Then
            Return Valore
        End If
        Return Libreria.SubstringSicuro(Valore, MaxLen)
    End Function

    Protected Shared Function getValueOrNohing(Valore As Object) As Object
        If Valore Is DBNull.Value Then
            Return Nothing
        End If
        Return Valore
    End Function

    Protected Shared Function getDoubleOrNohing(Valore As Object) As Double?
        If Valore Is DBNull.Value Then
            Return Nothing
        End If
        Return CType(Valore, Double)
    End Function

    Public MustOverride Function SalvaRecord(Optional RecuperaId As Boolean = False) As Integer
End Class
