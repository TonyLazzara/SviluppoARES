Public Class classi_pagamento

    Public MustInherit Class TransazioneBase
        Private m_DataTransazione As DateTime
        Private m_TerminalID As String
        Private m_OperationNumber As String
        Private m_STAN As String
        Private m_TipoCarta As String
        Private m_IDRecord As Integer
        Private m_IDFunzione As Integer
        Private m_ActionCode As String
        Private m_AcquireID As String
        Private m_Intestatario As String

        Public Property ActionCode() As String
            Get
                Return m_ActionCode
            End Get
            Set(ByVal value As String)
                m_ActionCode = value
            End Set
        End Property

        Public Property AcquireID() As String
            Get
                Return m_AcquireID
            End Get
            Set(ByVal value As String)
                m_AcquireID = value
            End Set
        End Property

        Public Property Intestatario() As String
            Get
                Return m_Intestatario
            End Get
            Set(ByVal value As String)
                m_Intestatario = value
            End Set
        End Property

        Public Property IDFunzione() As Integer
            Get
                Return m_IDFunzione
            End Get
            Set(ByVal value As Integer)
                m_IDFunzione = value
            End Set
        End Property

        Public Property IDRecord() As Integer
            Get
                Return m_IDRecord
            End Get
            Set(ByVal value As Integer)
                m_IDRecord = value
            End Set
        End Property

        Public Property TipoCarta() As String
            Get
                Return m_TipoCarta
            End Get
            Set(ByVal value As String)
                m_TipoCarta = value
            End Set
        End Property

        Public Property STAN() As String
            Get
                Return m_STAN
            End Get
            Set(ByVal value As String)
                m_STAN = value
            End Set
        End Property

        Public Property OperationNumber() As String
            Get
                Return m_OperationNumber
            End Get
            Set(ByVal value As String)
                m_OperationNumber = value
            End Set
        End Property

        Public Property DataTransazione() As DateTime
            Get
                Return m_DataTransazione
            End Get
            Set(ByVal value As DateTime)
                m_DataTransazione = value
            End Set
        End Property

        Public Property TerminalID() As String
            Get
                Return m_TerminalID
            End Get
            Set(ByVal value As String)
                m_TerminalID = value
            End Set
        End Property

    End Class

    Public Class TransazioneStorno
        Inherits TransazioneBase

        Private m_IDFunzioneStornata As Integer
        Private m_STANStornato As String

        Public Property IDFunzioneStornata() As Integer
            Get
                Return m_IDFunzioneStornata
            End Get
            Set(ByVal value As Integer)
                m_IDFunzioneStornata = value
            End Set
        End Property

        Public Property STANStornato() As String
            Get
                Return m_STANStornato
            End Get
            Set(ByVal value As String)
                m_STANStornato = value
            End Set
        End Property

    End Class

    Public Class TransazioneIntegrazione
        Inherits TransazioneBase

        Private m_NumeroAutorizzazione As String
        Private m_Importo As Decimal
        Private m_NumeroPreautorizzazione As String

        Public Property NumeroPreautorizzazione() As String
            Get
                Return m_NumeroPreautorizzazione
            End Get
            Set(ByVal value As String)
                m_NumeroPreautorizzazione = value
            End Set
        End Property

        Public Property Importo() As Decimal
            Get
                Return m_Importo
            End Get
            Set(ByVal value As Decimal)
                m_Importo = value
            End Set
        End Property

        Public Property NumeroAutorizzazione() As String
            Get
                Return m_NumeroAutorizzazione
            End Get
            Set(ByVal value As String)
                m_NumeroAutorizzazione = value
            End Set
        End Property

    End Class

    Public Class TransazioneVendita
        Inherits TransazioneBase

        Private m_NumeroAutorizzazione As String
        Private m_ScadenzaCartaMese As Integer
        Private m_ScadenzaCartaAnno As Integer
        Private m_NumeroCarta As String
        Private m_Importo As Decimal
        Private m_IDEnte As Integer
        Private m_IDAcquireCircuito As Integer

        Public Property IDAcquireCircuito() As Integer
            Get
                Return m_IDAcquireCircuito
            End Get
            Set(ByVal value As Integer)
                m_IDAcquireCircuito = value
            End Set
        End Property

        Public Property IDEnte() As Integer
            Get
                Return m_IDEnte
            End Get
            Set(ByVal value As Integer)
                m_IDEnte = value
            End Set
        End Property

        Public Property Importo() As Decimal
            Get
                Return m_Importo
            End Get
            Set(ByVal value As Decimal)
                m_Importo = value
            End Set
        End Property

        Public Property NumeroAutorizzazione() As String
            Get
                Return m_NumeroAutorizzazione
            End Get
            Set(ByVal value As String)
                m_NumeroAutorizzazione = value
            End Set
        End Property

        Public Property ScadenzaCartaMese() As Integer
            Get
                Return m_ScadenzaCartaMese
            End Get
            Set(ByVal value As Integer)
                m_ScadenzaCartaMese = value
            End Set
        End Property

        Public Property ScadenzaCartaAnno() As Integer
            Get
                Return m_ScadenzaCartaAnno
            End Get
            Set(ByVal value As Integer)
                m_ScadenzaCartaAnno = value
            End Set
        End Property

        Public Property NumeroCarta() As String
            Get
                Return m_NumeroCarta
            End Get
            Set(ByVal value As String)
                m_NumeroCarta = value
            End Set
        End Property
    End Class

    Public Class TransazionePreautorizzazione
        Inherits TransazioneBase

        Private m_Importo As Decimal
        Private m_NumeroAutorizzazione As String
        Private m_NumeroCarta As String
        Private m_NumeroPreautorizzazione As String
        Private m_ScadenzaCartaMese As Integer
        Private m_ScadenzaCartaAnno As Integer
        Private m_IDEnte As Integer
        Private m_IDAcquireCircuito As Integer

        Public Property IDAcquireCircuito() As Integer
            Get
                Return m_IDAcquireCircuito
            End Get
            Set(ByVal value As Integer)
                m_IDAcquireCircuito = value
            End Set
        End Property

        Public Property IDEnte() As Integer
            Get
                Return m_IDEnte
            End Get
            Set(ByVal value As Integer)
                m_IDEnte = value
            End Set
        End Property

        Public Property ScadenzaCartaMese() As Integer
            Get
                Return m_ScadenzaCartaMese
            End Get
            Set(ByVal value As Integer)
                m_ScadenzaCartaMese = value
            End Set
        End Property

        Public Property ScadenzaCartaAnno() As Integer
            Get
                Return m_ScadenzaCartaAnno
            End Get
            Set(ByVal value As Integer)
                m_ScadenzaCartaAnno = value
            End Set
        End Property

        Public Property NumeroPreautorizzazione() As String
            Get
                Return m_NumeroPreautorizzazione
            End Get
            Set(ByVal value As String)
                m_NumeroPreautorizzazione = value
            End Set
        End Property

        Public Property NumeroCarta() As String
            Get
                Return m_NumeroCarta
            End Get
            Set(ByVal value As String)
                m_NumeroCarta = value
            End Set
        End Property

        Public Property NumeroAutorizzazione() As String
            Get
                Return m_NumeroAutorizzazione
            End Get
            Set(ByVal value As String)
                m_NumeroAutorizzazione = value
            End Set
        End Property

        Public Property Importo() As Decimal
            Get
                Return m_Importo
            End Get
            Set(ByVal value As Decimal)
                m_Importo = value
            End Set
        End Property
    End Class

    Public Class TransazioneChiusura
        Inherits TransazioneBase

        Private m_Importo As Decimal
        Private m_NumeroPreautorizzazione As String

        Public Property NumeroPreautorizzazione() As String
            Get
                Return m_NumeroPreautorizzazione
            End Get
            Set(ByVal value As String)
                m_NumeroPreautorizzazione = value
            End Set
        End Property

        Public Property Importo() As Decimal
            Get
                Return m_Importo
            End Get
            Set(ByVal value As Decimal)
                m_Importo = value
            End Set
        End Property

    End Class

    Public Class TransazioneRimborso
        Inherits TransazioneBase

        Private m_Importo As Decimal
        Private m_NumeroCarta As String
        Private m_NumeroAutorizzazione As String
        Private m_IDEnte As Integer
        Private m_IDAcquireCircuito As Integer

        Public Property NumeroCarta() As String
            Get
                Return m_NumeroCarta
            End Get
            Set(ByVal value As String)
                m_NumeroCarta = value
            End Set
        End Property

        Public Property IDAcquireCircuito() As Integer
            Get
                Return m_IDAcquireCircuito
            End Get
            Set(ByVal value As Integer)
                m_IDAcquireCircuito = value
            End Set
        End Property

        Public Property IDEnte() As Integer
            Get
                Return m_IDEnte
            End Get
            Set(ByVal value As Integer)
                m_IDEnte = value
            End Set
        End Property

        Public Property NumeroAutorizzazione() As String
            Get
                Return m_NumeroAutorizzazione
            End Get
            Set(ByVal value As String)
                m_NumeroAutorizzazione = value
            End Set
        End Property

        Public Property Importo() As Decimal
            Get
                Return m_Importo
            End Get
            Set(ByVal value As Decimal)
                m_Importo = value
            End Set
        End Property
    End Class


End Class
