Imports System.IO
Imports ConvertImgTransparent

Partial Class ConvertToImage
    Inherits System.Web.UI.Page


    Protected Sub Button1_Click(sender As Object, e As EventArgs)

        Dim pf_src As String
        pf_src = "D:\Siti2\Sicilirentcar.it.ares\firme_contratti\ml2.png"
        Dim pf_dst As String
        pf_dst = "D:\Siti2\Sicilirentcar.it.ares\firme_contratti\ml-t.png"


        Dim esito As Boolean = ConvertImgTransparent.ConvertImage(pf_src, pf_dst)


        'Dim fs As FileStream = New FileStream(pf_dst, FileMode.OpenOrCreate, FileAccess.Write)
        'fs.Write(ni, 0, Convert.ToInt32(ni.Length))
        'fs.Close()

        If esito = True Then
            lbl_msg.Text = "OK Img convertita : " & Date.Now.ToString()
        Else
            lbl_msg.Text = "KO Img NON convertita : " & Date.Now.ToString()
        End If

        lbl_msg.Text += " -->  " & pf_dst


    End Sub





End Class
