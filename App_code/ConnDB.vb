Public Class ConnDB
    Private _strConn As String = ""
    Public Property StrConn As String
        Set(value As String)
            _strConn = value
        End Set
        Get
            Return _strConn
        End Get
    End Property

    Public Function sqlExecuteScalar(sql As String) As String
        Using Dbc As New System.Data.SqlClient.SqlConnection(StrConn)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sql, Dbc)
                sqlExecuteScalar = Cmd.ExecuteScalar() & ""
            End Using
        End Using
    End Function

    Public Function sqlExecuteNonQuery(sql As String) As Integer
        Using Dbc As New System.Data.SqlClient.SqlConnection(StrConn)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sql, Dbc)
                sqlExecuteNonQuery = Cmd.ExecuteNonQuery
            End Using
        End Using
    End Function
End Class
