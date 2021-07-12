Imports MySql.Data.MySqlClient
Imports CRUD.classCRUD
Public Class Form1

    Dim crud As New classCRUD

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load

        Call KondisiAwal()
    End Sub

    Sub KondisiAwal()
        TextBdg.Text = ""
        TextNK.Text = ""
        TextDK.Text = ""
        TextTK.Text = ""
        TextBdg.Enabled = False
        TextNK.Enabled = False
        TextDK.Enabled = False
        TextTK.Enabled = False
        Button1.Text = "INPUT"
        Button2.Text = "EDIT"
        Button3.Text = "HAPUS"
        Button4.Text = "TUTUP"
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True
        crud.Koneksi()
        Dim data = crud.KondisiAwal()
        DataGridView1.DataSource = data.Tables(0)

    End Sub
    Sub FieldAktif()
        TextBdg.Enabled = True
        TextNK.Enabled = True
        TextDK.Enabled = True
        TextTK.Enabled = True
        Button1.Text = "SIMPAN"
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Text = "BATAL"
        TextBdg.Focus()
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Button1.Text = "INPUT" Then
            crud.Koneksi()
            Call FieldAktif()

        Else

            If TextBdg.Text = "" Or TextNK.Text = "" Or TextDK.Text = "" Or TextTK.Text = "" Then
                MsgBox("Pastikan semua field terisi ")
            Else
                crud.Create(TextBdg.Text, TextNK.Text, TextDK.Text, TextTK.Text)
                Call KondisiAwal()
            End If
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If TextBdg.Text = "" Or TextNK.Text = "" Or TextDK.Text = "" Or TextTK.Text = "" Then
            MsgBox("Pastikan semua field terisi ")
        Else
            Try
                crud.Update(TextBdg.Text, TextNK.Text, TextDK.Text, TextTK.Text)
                Call KondisiAwal()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub TextBdg_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBdg.KeyPress
        If e.KeyChar = Chr(13) Then
            Dim Data = crud.Read(TextBdg.Text)
            If Data.Rows.Count > 0 Then
                TextNK.Text = Data.Rows(0)("nama").ToString()
                TextDK.Text = Data.Rows(0)("departemen").ToString()
                TextTK.Text = Data.Rows(0)("nohp").ToString()
            Else
                TextNK.Focus()
            End If

        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If TextBdg.Text = "" Or TextNK.Text = "" Or TextDK.Text = "" Or TextTK.Text = "" Then
            MsgBox("Pastikan data yang aka dihapus terisi ")
        Else
            Try
                crud.Delete(TextBdg.Text)
                Call KondisiAwal()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Button4.Text = "TUTUP" Then
            End
        Else
            Call KondisiAwal()
        End If
    End Sub
End Class