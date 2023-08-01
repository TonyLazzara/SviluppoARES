Option Strict Off

Public Class CMappaUnitaRete
    ' http://forum.html.it/forum/showthread/t-1172227.html

    Structure NETRESOURCE
        Dim dwScope As Integer
        Dim dwType As Integer
        Dim dwDisplayType As Integer
        Dim dwUsage As Integer
        Dim lpLocalName As String
        Dim lpRemoteName As String
        Dim lpComment As String
        Dim lpProvider As String
    End Structure

    Declare Function WNetCancelConnection Lib "mpr.dll" Alias "WNetCancelConnectionA" (ByVal lpszName As String, ByVal bForce As Integer) As Integer
    Declare Function WNetAddConnection2 Lib "mpr.dll" Alias "WNetAddConnection2A" (ByRef lpNetResource As NETRESOURCE, ByVal lpPassword As String, ByVal lpUserName As String, ByVal dwFlags As Integer) As Integer

    Private m_Drive As String
    Private m_Path As String
    Private m_UserName As String
    Private m_Password As String
    Private m_Flag As Integer = 0

    Public Property Drive() As String
        Get
            Return m_Drive
        End Get
        Set(value As String)
            m_Drive = value
        End Set
    End Property

    Public Property Path() As String
        Get
            Return m_Path
        End Get
        Set(value As String)
            m_Path = value
        End Set
    End Property

    Public Property UserName() As String
        Get
            Return m_UserName
        End Get
        Set(value As String)
            m_UserName = value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return m_Password
        End Get
        Set(value As String)
            m_Password = value
        End Set
    End Property

    Public Property Flag() As Integer
        Get
            Return m_Flag
        End Get
        Set(value As Integer)
            m_Flag = value
        End Set
    End Property

    Public Sub CollegaUnita()
        Dim Wnet As Integer
        Dim lpNetResource As NETRESOURCE

        lpNetResource.lpLocalName = Drive
        lpNetResource.lpRemoteName = Path

        Wnet = WNetAddConnection2(lpNetResource, Password, UserName, Flag)

        If Wnet <> 0 AndAlso Wnet <> 85 Then '85=The local device name is already in use
            Throw (New Exception(String.Format("Errore connessione: {0}", Path)))
        End If

        HttpContext.Current.Trace.Write("OK Path: " & Path)
    End Sub

    Public Sub ScollegaUnita()
        WNetCancelConnection(Drive, -1)
    End Sub

End Class
