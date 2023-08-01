Imports System.IO

Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Enum TipoRiduzione
    NessunaRiduzione = 0
    ServizioPubblicoPiazza = 1
    GPLEsclusivo = 2
    MetanoEsclusivo = 3
    MotoreElettrico = 4
    TrasportiSpecialiInf12 = 5
    NoleggioRimessa = 6
    ScuolaGuida = 7
    AutobusNoleggioRimessa = 8
    AutobusLinea = 9
    TrasportiSpecialiSup12 = 10
    VeicoloImpresaNonEco = 11
    CumulativoRimorchi = 12
End Enum

Public Enum TipoCategoria
    Autoveicolo = 1
    Motoveicolo
    Rimorchio
End Enum

Public Class BolloAuto
    Public ContoCorrente As String = ""
    ' Importo = ImportoTassa + Sanzioni + Interessi
    Public Importo As String = ""
    Public ImportoTassa As String = ""
    Public Sanzioni As String = ""
    Public Interessi As String = ""

    Public IntestatoA As String = ""
    Public Targa As String = ""
    Public ScadenzaAnno As Integer = 0
    Public ScadenzaMese As Integer = 0
    Public MesiValidita As Integer = 0
    Public Riduzione As TipoRiduzione = TipoRiduzione.NessunaRiduzione
    Public CodiceFiscale As String = ""
    Public PIVA As String = ""
    Public Categoria As TipoCategoria = TipoCategoria.Autoveicolo
    Public EseguitoDa As String = ""
    Public Residente As String = ""
    Public Provincia As String = ""
    Public Comune As String = ""
    Public CAP As String = ""
End Class

Public Class CifreLettere
    Private Shared Function Unita(k As Integer) As String
        Dim lettere() As String = {"", "uno", "due", "tre", "quattro", "cinque", "sei", "sette", "otto", "nove", "dieci", "undici", "dodici", "tredici", "quattordici", "quindici", "sedici", "diciassette", "diciotto", "diciannove"}

        If k < 0 Or k > lettere.Length - 1 Then
            Return ""
        End If
        Return lettere(k)
    End Function

    Private Shared Function Decine(k As Integer) As String
        Dim lettere() As String = {"", "dieci", "venti", "trenta", "quaranta", "cinquanta", "sessanta", "settanta", "ottanta", "novanta"}
        If k < 0 Or k > lettere.Length - 1 Then
            Return ""
        End If
        Return lettere(k)
    End Function

    Private Shared Function Migliaia(k As Integer) As String
        Dim lettere() As String = {"", "mille", "unmilione", "unmiliardo", "millemiliardi", "mila", "milioni", "miliardi", "milamiliardi", "milamiliardi", "migliaiadimiliardi"}
        If k < 0 Or k > lettere.Length - 1 Then
            Return ""
        End If
        Return lettere(k)
    End Function

    Public Shared Function SubstringSicuro(input As String, PosIniziale As Integer) As String
        If input Is Nothing Then
            Return ""
        End If
        If PosIniziale < 0 Then
            PosIniziale = 0
        End If
        Dim R As String = input
        If R.Length > PosIniziale Then
            R = R.Substring(PosIniziale)
        End If
        Return R
    End Function

    Public Shared Function SubstringSicuro(input As String, PosIniziale As Integer, MaxLen As Integer) As String
        If input Is Nothing Then
            Return ""
        End If
        If PosIniziale < 0 Then
            MaxLen += PosIniziale
            PosIniziale = 0
        End If
        If MaxLen < 0 Then
            MaxLen = 0
        End If
        Dim R As String = input
        If R.Length > MaxLen Then
            R = R.Substring(PosIniziale, MaxLen)
        End If
        Return R
    End Function

    Public Shared Function CalcolaLettere(importo As Decimal) As String
        Dim result As String = ""
        Dim intero As String = importo.ToString("0.000")
        Dim resto As String = "/" + intero.Substring(intero.Length - 3, 2)
        intero = intero.Substring(0, intero.Length - 4)

        If intero.StartsWith("-") Then
            intero = intero.Substring(1)
        End If

        If importo = 0 Then
            Return "zero/00"
        End If

        Dim mille = -1
        Dim k = intero.Length Mod 3

        If k <> 0 Then
            intero = intero.PadLeft(intero.Length + 3 - k, "0"c)
        End If

        Do While intero <> ""
            mille += 1
            Dim parziale = ""
            Dim tripla As String = SubstringSicuro(intero, intero.Length - 3)
            Dim s As String = ""
            intero = SubstringSicuro(intero, 0, intero.Length - 3)
            Dim tv As Integer = Integer.Parse(tripla)
            Dim td As Integer = tv Mod 100
            Dim tc As Integer = (tv - td) / 100
            If tc <> 0 Then
                parziale = "cento"
                If tc > 1 Then
                    parziale = Unita(tc) + parziale
                End If
            End If
            If td < 20 Then
                parziale += Unita(td)
            Else
                Dim x = td Mod 10
                Dim y = (td - x) / 10
                parziale += Decine(y)
                s = Unita(x)
                If (s.StartsWith("u") Or s.StartsWith("o")) And (y <> 0) Then
                    parziale = SubstringSicuro(parziale, 0, intero.Length - 1)
                End If
                parziale += s
            End If
            s = Migliaia(mille)
            If mille > 0 And parziale <> "" Then
                k = mille
                If parziale <> "uno" Then
                    k += 4
                    s = Migliaia(k)
                    If parziale.EndsWith("uno") Then
                        parziale = SubstringSicuro(parziale, 0, parziale.Length - 1)
                    End If
                Else
                    parziale = ""
                End If
                parziale = parziale + s
            End If
            result = parziale + result
        Loop
        If importo < 0 Then
            result = "meno" + result
        End If

        Return result + resto
    End Function
End Class


Public Class StampaBolloAuto
    Private Shared dimFont As Integer = 10
    Shared bf As BaseFont = BaseFont.CreateFont(BaseFont.COURIER_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)

    Private Shared Function getEuri(Valore As Double) As String
        getEuri = Format(Valore, "0.000")
        Return "#" & getEuri.Split(","c)(0)
    End Function

    Private Shared Function getCentesimi(valore As Double) As String
        getCentesimi = Format(valore, "0.000")
        Return getCentesimi.Split(","c)(1).Substring(0, 2)
    End Function

    Public Shared Sub GeneraBollettino(bollo As BolloAuto, canvas As PdfContentByte)
        Dim i As Integer = 1

        HttpContext.Current.Trace.Write("I: " & i)
        i += 1

        canvas.BeginText()

        canvas.SetFontAndSize(bf, dimFont)

        HttpContext.Current.Trace.Write("I: " & i)
        i += 1

        Dim posX As Single = 0
        Dim posY As Single = 0

        Dim x As Single = 0
        Dim y As Single = 0

        ' stampa conto ed importo (tutte sulla stessa riga e con lo stessa spaziatura tra i caratteri)
        canvas.SetCharacterSpacing(6.3)
        y = 262
        canvas.ShowTextAligned(Element.ALIGN_LEFT, bollo.ContoCorrente, posX + 98, posY + y, 0) ' cedolino 1
        canvas.ShowTextAligned(Element.ALIGN_LEFT, bollo.ContoCorrente, posX + 470.5, posY + y, 0) ' cedolino 2

        canvas.ShowTextAligned(Element.ALIGN_RIGHT, getEuri(bollo.Importo), posX + 332, posY + y, 0) ' cedolino 1
        canvas.ShowTextAligned(Element.ALIGN_RIGHT, getCentesimi(bollo.Importo), posX + 358, posY + y, 0) ' cedolino 1

        canvas.ShowTextAligned(Element.ALIGN_RIGHT, getEuri(bollo.Importo), posX + 799, posY + y, 0) ' cedolino 2
        canvas.ShowTextAligned(Element.ALIGN_RIGHT, getCentesimi(bollo.Importo), posX + 826, posY + y, 0) ' cedolino 2

        ' stampa importo in lettere
        canvas.SetCharacterSpacing(1)
        y = 243
        Dim ImportoLettera As String = "#" & CifreLettere.CalcolaLettere(bollo.Importo)
        Dim dx As Integer = 79
        Dim dx2 As Integer = 478
        If ImportoLettera.Length > 42 Then
            dx = 32
        End If
        If ImportoLettera.Length > 49 Then
            dx = 10
        End If
        If ImportoLettera.Length > 52 Then
            dx2 = 450
        End If
        canvas.ShowTextAligned(Element.ALIGN_LEFT, ImportoLettera, posX + dx, posY + y, 0) ' cedolino 1
        canvas.ShowTextAligned(Element.ALIGN_LEFT, ImportoLettera, posX + dx2, posY + y, 0) ' cedolino 2

        ' stampa intestato a ... dovrei gestire il ritorno a capo se la stringa e lunga...
        ' canvas.ShowTextAligned(Element.ALIGN_LEFT, bollo.IntestatoA, posX + 35, posY + 207, 0) ' cedolino 1 --------------------------------
        ' canvas.ShowTextAligned(Element.ALIGN_LEFT, bollo.IntestatoA, posX + 395, posY + 217, 0) ' cedolino 2

        ' stampa targa
        canvas.SetCharacterSpacing(5.4)
        canvas.SetFontAndSize(bf, dimFont + 3)
        canvas.ShowTextAligned(Element.ALIGN_LEFT, bollo.Targa, posX + 236, posY + 166, 0) ' cedolino 1
        canvas.ShowTextAligned(Element.ALIGN_LEFT, bollo.Targa, posX + 601, posY + 176, 0) ' cedolino 2

        ' stampa mese anno ecc. cedolino 1
        canvas.SetCharacterSpacing(8.5)
        canvas.SetFontAndSize(bf, dimFont)
        y = 166
        canvas.ShowTextAligned(Element.ALIGN_LEFT, Format(bollo.ScadenzaMese, "00"), posX + 72, posY + y, 0)
        canvas.ShowTextAligned(Element.ALIGN_LEFT, bollo.ScadenzaAnno, posX + 123, posY + y, 0)
        y = 142
        canvas.ShowTextAligned(Element.ALIGN_LEFT, Format(bollo.MesiValidita, "00"), posX + 72, posY + y, 0)
        If bollo.Riduzione > TipoRiduzione.NessunaRiduzione Then
            Dim Riduzione As Integer = bollo.Riduzione
            canvas.ShowTextAligned(Element.ALIGN_LEFT, Format(Riduzione, "00"), posX + 123, posY + y, 0)
        End If


        ' stampa altre info (stesso carattere e spacing)
        canvas.SetCharacterSpacing(5.3)
        canvas.SetFontAndSize(bf, dimFont - 1)
        Dim Codice As String
        If bollo.PIVA = "" Then
            Codice = bollo.CodiceFiscale
        Else
            Codice = bollo.PIVA
        End If
        canvas.ShowTextAligned(Element.ALIGN_LEFT, Codice, posX + 31, posY + 121, 0) ' cedolino 1
        canvas.ShowTextAligned(Element.ALIGN_LEFT, Codice, posX + 548.5, posY + 137, 0) ' cedolino 2

        canvas.SetCharacterSpacing(6.85)
        ' anagrafici cedolino 2
        canvas.ShowTextAligned(Element.ALIGN_LEFT, CifreLettere.SubstringSicuro(bollo.EseguitoDa, 0, 23), posX + 548.5, posY + 117, 0) ' cedolino 2
        y = 98
        canvas.ShowTextAligned(Element.ALIGN_LEFT, CifreLettere.SubstringSicuro(bollo.Residente, 0, 20), posX + 548.5, posY + y, 0) ' cedolino 2
        canvas.ShowTextAligned(Element.ALIGN_LEFT, CifreLettere.SubstringSicuro(bollo.Provincia, 0, 2), posX + 804, posY + y, 0) ' cedolino 2
        y = 78
        canvas.ShowTextAligned(Element.ALIGN_LEFT, CifreLettere.SubstringSicuro(bollo.Comune, 0, 17), posX + 548.5, posY + y, 0) ' cedolino 2
        canvas.ShowTextAligned(Element.ALIGN_LEFT, CifreLettere.SubstringSicuro(bollo.CAP, 0, 5), posX + 768.5, posY + y, 0) ' cedolino 2

        ' stampa mese anno ecc. cedolino 2
        canvas.SetCharacterSpacing(8)
        y = 180
        canvas.ShowTextAligned(Element.ALIGN_LEFT, Format(bollo.ScadenzaMese, "00"), posX + 433, posY + y, 0)
        canvas.ShowTextAligned(Element.ALIGN_LEFT, bollo.ScadenzaAnno, posX + 466, posY + y, 0)
        canvas.ShowTextAligned(Element.ALIGN_LEFT, Format(bollo.MesiValidita, "00"), posX + 524, posY + y, 0)
        If bollo.Riduzione > TipoRiduzione.NessunaRiduzione Then
            Dim Riduzione As Integer = bollo.Riduzione
            canvas.ShowTextAligned(Element.ALIGN_LEFT, Format(Riduzione, "00"), posX + 559, posY + y, 0)
        End If

        ' importi cedolino 1
        canvas.SetCharacterSpacing(7)
        y = 40
        If bollo.ImportoTassa <> "" Then
            canvas.ShowTextAligned(Element.ALIGN_RIGHT, getEuri(bollo.ImportoTassa), posX + 79, posY + y, 0)
            canvas.ShowTextAligned(Element.ALIGN_RIGHT, getCentesimi(bollo.ImportoTassa), posX + 108, posY + y, 0)
        End If
        If bollo.Sanzioni <> "" Then
            canvas.ShowTextAligned(Element.ALIGN_RIGHT, getEuri(bollo.Sanzioni), posX + 202, posY + y, 0)
            canvas.ShowTextAligned(Element.ALIGN_RIGHT, getCentesimi(bollo.Sanzioni), posX + 231, posY + y, 0)
        End If
        If bollo.Interessi <> "" Then
            canvas.ShowTextAligned(Element.ALIGN_RIGHT, getEuri(bollo.Interessi), posX + 328, posY + y, 0)
            canvas.ShowTextAligned(Element.ALIGN_RIGHT, getCentesimi(bollo.Interessi), posX + 357, posY + y, 0)
        End If

        ' importi cedolino 2
        y = 186
        If bollo.ImportoTassa <> "" Then
            canvas.ShowTextAligned(Element.ALIGN_RIGHT, getEuri(bollo.ImportoTassa), posX + 795, posY + y, 0)
            canvas.ShowTextAligned(Element.ALIGN_RIGHT, getCentesimi(bollo.ImportoTassa), posX + 823, posY + y, 0)
        End If
        y = 168
        If bollo.Sanzioni <> "" Then
            canvas.ShowTextAligned(Element.ALIGN_RIGHT, getEuri(bollo.Sanzioni), posX + 795, posY + y, 0)
            canvas.ShowTextAligned(Element.ALIGN_RIGHT, getCentesimi(bollo.Sanzioni), posX + 823, posY + y, 0)
        End If
        y = 149
        If bollo.Interessi <> "" Then
            canvas.ShowTextAligned(Element.ALIGN_RIGHT, getEuri(bollo.Interessi), posX + 795, posY + y, 0)
            canvas.ShowTextAligned(Element.ALIGN_RIGHT, getCentesimi(bollo.Interessi), posX + 823, posY + y, 0)
        End If

        ' categoria cedolino 1
        y = 98.5
        'canvas.ShowTextAligned(Element.ALIGN_RIGHT, "X", posX + 119.2, posY + y, 0)
        'canvas.ShowTextAligned(Element.ALIGN_RIGHT, "X", posX + 162, posY + y, 0)
        'canvas.ShowTextAligned(Element.ALIGN_RIGHT, "X", posX + 205.19999999999999, posY + y, 0)
        Select Case bollo.Categoria
            Case TipoCategoria.Autoveicolo
                canvas.ShowTextAligned(Element.ALIGN_RIGHT, "X", posX + 119.2, posY + y, 0)
            Case TipoCategoria.Motoveicolo
                canvas.ShowTextAligned(Element.ALIGN_RIGHT, "X", posX + 162, posY + y, 0)
            Case TipoCategoria.Rimorchio
                canvas.ShowTextAligned(Element.ALIGN_RIGHT, "X", posX + 205.2, posY + y, 0)
        End Select

        ' categoria cedolino 2
        y = 161 ' 466 470
        'canvas.ShowTextAligned(Element.ALIGN_RIGHT, "X", posX + 490.5, posY + y, 0)
        'canvas.ShowTextAligned(Element.ALIGN_RIGHT, "X", posX + 533.10000000000002, posY + y, 0)
        'canvas.ShowTextAligned(Element.ALIGN_RIGHT, "X", posX + 575.5, posY + y, 0)
        Select Case bollo.Categoria
            Case TipoCategoria.Autoveicolo
                canvas.ShowTextAligned(Element.ALIGN_RIGHT, "X", posX + 490.5, posY + y, 0)
            Case TipoCategoria.Motoveicolo
                canvas.ShowTextAligned(Element.ALIGN_RIGHT, "X", posX + 533.1, posY + y, 0)
            Case TipoCategoria.Rimorchio
                canvas.ShowTextAligned(Element.ALIGN_RIGHT, "X", posX + 575.5, posY + y, 0)
        End Select

        ' eseguito da cedolino 1
        'canvas.SetCharacterSpacing(1)
        'canvas.SetFontAndSize(bf, dimFont - 2)
        'canvas.ShowTextAligned(Element.ALIGN_LEFT, bollo.EseguitoDa, posX + 32, posY + 76, 0) ' cedolino 1

        canvas.EndText()

        canvas.SetCharacterSpacing(1)
        canvas.SetFontAndSize(bf, dimFont - 2)

        Dim font As Font = New Font(bf, 9)

        Dim table As PdfPTable = New PdfPTable(1)

        Dim c As PdfPCell = New PdfPCell(New Phrase(bollo.IntestatoA, font))
        c.Border = PdfPCell.NO_BORDER
        c.VerticalAlignment = Element.ALIGN_BOTTOM
        c.HorizontalAlignment = Element.ALIGN_LEFT
        c.SetLeading(3.5, 1)

        table.AddCell(c)

        table.TotalWidth = 300
        table.WriteSelectedRows(0, -1, posX + 32, posY + 222, canvas)
        table.TotalWidth = 370
        table.WriteSelectedRows(0, -1, posX + 395, posY + 233, canvas)

        Dim table2 As PdfPTable = New PdfPTable(1)
        Dim font2 As Font = New Font(bf, 8)
        Dim c2 As PdfPCell = New PdfPCell(New Phrase(bollo.EseguitoDa, font2))
        c2.Border = PdfPCell.NO_BORDER
        c2.VerticalAlignment = Element.ALIGN_BOTTOM
        c2.HorizontalAlignment = Element.ALIGN_LEFT
        c2.SetLeading(1, 1)

        table2.AddCell(c2)
        table2.TotalWidth = 150
        table2.WriteSelectedRows(0, -1, posX + 30, posY + 90, canvas)

    End Sub

    Public Shared Function GeneraStampa(listaBolli As List(Of BolloAuto)) As MemoryStream
        ' http://kuujinbo.info/cs/itext.aspx

        Dim ms As MemoryStream = Nothing
        Dim document As Document = Nothing
        Dim writer As PdfWriter = Nothing

        Try
            ms = New MemoryStream()
            document = New Document(PageSize.A4.Rotate(), 0, 0, 0, 0)
            writer = PdfWriter.GetInstance(document, ms)

            document.Open()

            Dim canvas As PdfContentByte = writer.DirectContent

            'Dim img As Image = Image.GetInstance(Path.Combine("D:\Documents and Settings\K\Documenti\", "BollettinoBolli_02.jpg"))
            'img.ScaleAbsoluteHeight(document.PageSize.Height)
            'img.ScaleAbsoluteWidth(document.PageSize.Width)

            'img.SetAbsolutePosition(0, 0)

            'canvas.AddImage(img)

            For Each mio_bollettino As BolloAuto In listaBolli
                GeneraBollettino(mio_bollettino, canvas)

                document.NewPage()
            Next

            document.AddCreator("ARES")
            document.AddAuthor("Entermed")
            document.Close()
        Catch ex As Exception
            HttpContext.Current.Trace.Write("Errore: " & ex.StackTrace)
        Finally
            If Not document Is Nothing Then
                document = Nothing
            End If
            If Not writer Is Nothing Then
                writer.Close()
                writer = Nothing
            End If
        End Try

        Return ms
    End Function
End Class
