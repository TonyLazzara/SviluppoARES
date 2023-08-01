Public Class Contatori

    Private Shared Function getContatore(ByVal Tipo As String, Optional ByVal id_stazione As String = "", Optional ByVal anno As String = "") As String
        Dim sqlStr As String

        Dim Dbc As Data.SqlClient.SqlConnection = Nothing
        Dim myTrans As Data.SqlClient.SqlTransaction = Nothing
        Dim Cmd As Data.SqlClient.SqlCommand = Nothing

        Try
            Dim sqlWhere As String = " WHERE tipo = '" & Libreria.formattaSql(Tipo) & "'"
            If id_stazione <> "" Then
                sqlWhere = sqlWhere & " AND id_stazione = '" & id_stazione & "'"
            End If
            If anno <> "" Then
                sqlWhere = sqlWhere & " AND anno = '" & anno & "'"
            End If
            sqlStr = "SELECT contatore FROM contatori" & sqlWhere

            'HttpContext.Current.Trace.Write("getContatore: " & sqlStr)

            Dbc = New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            myTrans = Dbc.BeginTransaction()

            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc, myTrans)
            Dim contatore As String = Cmd.ExecuteScalar & ""
            Cmd.Dispose()
            Cmd = Nothing

            If contatore = "" Then
                contatore = "1"
                Dim val_contatore As String = "2"

                Dim val_id_stazione As String = "NULL"
                If id_stazione <> "" Then
                    val_id_stazione = id_stazione
                End If

                Dim val_anno As String = "NULL"
                If anno <> "" Then
                    val_anno = anno
                End If

                sqlStr = "INSERT INTO contatori (contatore,id_stazione,tipo,anno) VALUES (" & _
                    val_contatore & "," & _
                    val_id_stazione & "," & _
                    "'" & Libreria.formattaSql(Tipo) & "'," & _
                    val_anno & ")"
            Else
                sqlStr = "UPDATE contatori SET contatore = contatore + 1" & sqlWhere
            End If

            'HttpContext.Current.Trace.Write("getContatore: " & sqlStr)

            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc, myTrans)

            Cmd.ExecuteNonQuery()

            myTrans.Commit()

            getContatore = contatore

        Catch ex As Exception
            myTrans.Rollback()
            getContatore = "ERR"
            'HttpContext.Current.Trace.Write("getContatore: Rollback " & ex.Message)
        Finally
            If Not Cmd Is Nothing Then
                Cmd.Dispose()
                Cmd = Nothing
            End If

            If Not myTrans Is Nothing Then
                myTrans.Dispose()
                myTrans = Nothing
            End If

            If Not Dbc Is Nothing Then
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing
            End If
        End Try
    End Function

    Public Shared Function NewCodiceEDP() As String
        Dim sqlStr As String

        Dim Dbc As Data.SqlClient.SqlConnection = Nothing
        Dim myTrans As Data.SqlClient.SqlTransaction = Nothing
        Dim Cmd As Data.SqlClient.SqlCommand = Nothing

        Dim NomeTabella As String = "codice_cliente"
        Dim NomeCampo As String = "codice_cliente"

        Try
            sqlStr = "SELECT TOP 1 " & NomeCampo & " FROM " & NomeTabella & " ORDER BY " & NomeCampo & " DESC"      '21.06.2021 14.00
            'sqlStr = "SELECT TOP 1 codice_cliente FROM codice_cliente ORDER BY codice_cliente"     'inserito 21.06.2021 14.00
            'sqlStr = "Select Case top(1) ID_Cliente  from ditte order by id_cliente desc" 'inserito 21.06.2021 14.00
            'HttpContext.Current.Trace.Write("GeneraCodiceEDP: " & sqlStr)

            Dbc = New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            myTrans = Dbc.BeginTransaction()

            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc, myTrans)
            Dim contatore As String = Cmd.ExecuteScalar & ""
            Cmd.Dispose()
            Cmd = Nothing

            If contatore <> "" Then
                sqlStr = "DELETE FROM " & NomeTabella & " WHERE  " & NomeCampo & " = " & contatore
                'HttpContext.Current.Trace.Write("GeneraCodiceEDP: " & sqlStr)

                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc, myTrans)

                Cmd.ExecuteNonQuery()
            End If

            myTrans.Commit()

            NewCodiceEDP = contatore
            'HttpContext.Current.Response.Write("NewCodice CodiceEDP : " & NewCodiceEDP & "<br/>" & sqlStr & "<br/>") 'test
        Catch ex As Exception
            myTrans.Rollback()
            NewCodiceEDP = "-1"
            'HttpContext.Current.Trace.Write("GeneraCodiceEDP: Rollback " & ex.Message)
            HttpContext.Current.Response.Write("error NewCodice CodiceEDP : " & ex.Message & "<br/>" & sqlStr & "<br/>") 'test

        Finally
            If Not Cmd Is Nothing Then
                Cmd.Dispose()
                Cmd = Nothing
            End If

            If Not myTrans Is Nothing Then
                myTrans.Dispose()
                myTrans = Nothing
            End If

            If Not Dbc Is Nothing Then
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing
            End If
        End Try
    End Function

    Public Shared Function getContatore_pagamenti_extra(ByVal id_stazione As String) As String
        'TIPO: pagamenti_extra
        Dim tipo As String = "pagamenti_extra"

        'IN QUESTO CASO IL CONTATORE E' COMPOSTO DALLE PRIME DUE CIFRE IL CODICE STAZIONE E LE SUCCESSIVE 7 DALL'INCREMENTALE
        Dim contatore As String = getContatore(tipo, id_stazione)
        'HttpContext.Current.Trace.Write("1 getContatore_pagamenti_extra - contatore: " & contatore)
        If contatore <> "ERR" Then
            contatore = id_stazione & Format(Integer.Parse(contatore), "0000000")
            HttpContext.Current.Trace.Write("2 getContatore_pagamenti_extra - contatore: " & contatore)
        End If

        Return contatore
    End Function

    Public Shared Function getContatore_DollarThrifty(Optional id_stazione As Integer = 59) As String
        'TIPO: pagamenti_extra
        Dim tipo As String = "DollarThrifty"

        'IN QUESTO CASO IL CONTATORE E' COMPOSTO DALLE PRIME DUE CIFRE IL CODICE STAZIONE E LE SUCCESSIVE 7 DALL'INCREMENTALE
        Dim contatore As String = getContatore(tipo, id_stazione)
        'HttpContext.Current.Trace.Write("1 getContatore_DollarThrifty - contatore: " & contatore)
        If contatore <> "ERR" Then
            contatore = id_stazione & Format(Integer.Parse(contatore), "000000")
            'HttpContext.Current.Trace.Write("2 getContatore_DollarThrifty - contatore: " & contatore)
        End If

        Return contatore
    End Function

    Public Shared Function getContatore_fatture_web(ByVal anno As String) As String
        'TIPO: fatture_web
        'HttpContext.Current.Trace.Write("getContatore_fatture_web: " & anno)
        Dim tipo As String = "fatture_web"

        Return getContatore(tipo, , anno)
    End Function

    Public Shared Function getContatore_fatture_multe(ByVal anno As String) As String
        'TIPO: fatture_web
        'HttpContext.Current.Trace.Write("getContatore_fatture_multe: " & anno)
        Dim tipo As String = "fatture_nolo"

        Return getContatore(tipo, , anno)
    End Function

    Public Shared Function getContatore_fatture_nolo(ByVal anno As String) As String
        'TIPO: fatture_web
        Dim tipo As String = "fatture_nolo"

        Return getContatore(tipo, , anno)
    End Function

    'Public Shared Function getContatore_multe(nome As Integer, ByVal anno As String) As String
    '    'TIPO: multe_
    '    HttpContext.Current.Trace.Write("getContatore_multe: " & nome & " - " & anno)
    '    Dim tipo As String = "multe_"

    '    Return getContatore(tipo & nome, , anno)
    'End Function

    'Public Shared Function getContatore_gruppo_danni() As String
    '    Dim tipo As String = "gruppo_danni"

    '    Return getContatore(tipo)
    'End Function

    Public Shared Function contatore_pos_virtuale(id_pos As String) As String
        Return getContatore(id_pos, , )
    End Function

    Public Shared Function getContatore_RDS(ByVal id_stazione As String) As String
        'TIPO: RDS
        Dim tipo As String = "RDS"

        'IN QUESTO CASO IL CONTATORE E' COMPOSTO DALLE PRIME DUE CIFRE DEL CODICE STAZIONE
        'L'ANNO IN FORMATO DUE CIFRE
        'L'INCREMENTALE RDS DI 4 CIFRE
        Dim contatore As String = getContatore(tipo, id_stazione, Year(Now))
        'HttpContext.Current.Trace.Write("getContatore_RDS - contatore: " & contatore)
        If contatore <> "ERR" Then
            contatore = id_stazione & Right(Year(Now) & "", 2) & Format(Integer.Parse(contatore), "0000")
            'HttpContext.Current.Trace.Write("getContatore_RDS - contatore: " & contatore)
        End If

        Return contatore
    End Function


    Public Shared Function getContatore_ODL(ByVal id_stazione As String) As String
        'TIPO: RDS
        Dim tipo As String = "ODL"

        'IN QUESTO CASO IL CONTATORE E' COMPOSTO DALLE PRIME DUE CIFRE DEL CODICE STAZIONE
        'L'ANNO IN FORMATO DUE CIFRE
        'L'INCREMENTALE ODL DI 4 CIFRE
        Dim contatore As String = getContatore(tipo, id_stazione, Year(Now))
        'HttpContext.Current.Trace.Write("getContatore_ODL - contatore: " & contatore)
        If contatore <> "ERR" Then
            contatore = id_stazione & Right(Year(Now) & "", 2) & Format(Integer.Parse(contatore), "0000")
            'HttpContext.Current.Trace.Write("getContatore_ODL - contatore: " & contatore)
        End If

        Return contatore
    End Function

    Public Shared Function getContatore_Petty_Cash(ByVal id_stazione As String) As String
        'TIPO: RDS
        Dim tipo As String = "Petty_Cash"

        'IN QUESTO CASO IL CONTATORE E' COMPOSTO DALLE PRIME DUE CIFRE DEL CODICE STAZIONE
        'L'ANNO IN FORMATO DUE CIFRE
        'L'INCREMENTALE Petty_Cash DI 4 CIFRE
        Dim contatore As String = getContatore(tipo, id_stazione, Year(Now))
        'HttpContext.Current.Trace.Write("getContatore_Petty_Cash - contatore: " & contatore)
        If contatore <> "ERR" Then
            contatore = id_stazione & Right(Year(Now) & "", 2) & Format(Integer.Parse(contatore), "0000")
            'HttpContext.Current.Trace.Write("getContatore_Petty_Cash - contatore: " & contatore)
        End If

        Return contatore
    End Function

    Public Shared Function getContatore_Cassa(ByVal id_stazione As String) As String
        'TIPO: RDS
        Dim tipo As String = "CassaStazione"

        'IN QUESTO CASO IL CONTATORE E' COMPOSTO DALLE PRIME DUE CIFRE DEL CODICE STAZIONE
        'L'ANNO IN FORMATO DUE CIFRE
        'L'INCREMENTALE Petty_Cash DI 4 CIFRE
        Dim contatore As String = getContatore(tipo, id_stazione, Year(Now))
        'HttpContext.Current.Trace.Write("getContatore_Cassa - contatore: " & contatore)
        If contatore <> "ERR" Then
            contatore = id_stazione & Right(Year(Now) & "", 2) & Format(Integer.Parse(contatore), "0000")
            'HttpContext.Current.Trace.Write("getContatore_Cassa - contatore: " & contatore)
        End If

        Return contatore
    End Function

    Public Shared Function getContatore_Sospeso(ByVal id_stazione As String) As String
        'TIPO: SospesoStazione
        Dim tipo As String = "SospesoStazione"

        'IN QUESTO CASO IL CONTATORE E' COMPOSTO DALLE PRIME DUE CIFRE DEL CODICE STAZIONE
        'L'ANNO IN FORMATO DUE CIFRE
        'L'INCREMENTALE Sospeso DI 5 CIFRE
        Dim contatore As String = getContatore(tipo, id_stazione, Year(Now))
        'HttpContext.Current.Trace.Write("getContatore_Sospeso - contatore: " & contatore)
        If contatore <> "ERR" Then
            contatore = id_stazione & Right(Year(Now) & "", 2) & Format(Integer.Parse(contatore), "00000")
            'HttpContext.Current.Trace.Write("getContatore_Sospeso - contatore: " & contatore)
        End If

        Return contatore
    End Function
End Class
