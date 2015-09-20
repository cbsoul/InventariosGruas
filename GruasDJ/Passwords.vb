Public Class Passwords
    Public passinput As String = ""
    Private Sub Passwords_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        passinput = ""
        txtContra.Text = ""
    End Sub
   
    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtContra.KeyPress

        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        If KeyAscii = 13 Then
            passinput = txtContra.Text
            Me.Close()
        End If

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        
        passinput = txtContra.Text
        Me.Close()
    End Sub
   
End Class