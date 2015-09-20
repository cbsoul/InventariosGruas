Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Public Class NuevoDB
    Public caso As Integer = 0
    Dim objDB As New InsertarElementosDB
    Dim obj As New CboxConsultas
    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNuevo.KeyPress
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        If KeyAscii = 13 Then
            ExecuteNuevo()
        End If

    End Sub
    Private Sub NuevoBTN_Click(sender As Object, e As EventArgs) Handles NuevoBTN.Click
        ExecuteNuevo()
    End Sub
    Sub ExecuteNuevo()
        Dim id As String

        If txtNuevo.Text = "" Or txtNuevo.TextLength > 20 Then
            MsgBox("El cuadro de texto está vacío o excede la longitud predeterminada.", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "Valores incorrectos.")
        Else
            If caso = 1 Then

                Try
                    id = obj.ConocerIdCorralon(txtNuevo.Text)
                    If id <> "" Then
                        MessageBox.Show("Ese corralón ya existe.")
                    Else
                        objDB.InsertarCorralonDB(txtNuevo.Text.ToUpper)
                        MsgBox("Se ha ingresado correctamente " & txtNuevo.Text & " a la Base de Datos", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "Captura Exitosa")
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try

                Me.Close()
                Form1.BuscarCorralon()

            ElseIf caso = 2 Then
                Try
                    id = obj.ConocerIdAutoridad(txtNuevo.Text)
                    If id <> "" Then
                        MessageBox.Show("Esa Autoridad ya existe.")
                    Else
                        objDB.InsertarAutoridadDB(txtNuevo.Text.ToUpper)
                        MsgBox("Se ha ingresado correctamente " & txtNuevo.Text & " a la Base de Datos", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "Captura Exitosa")
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
                Me.Close()
                Form1.BuscarAutoridad()
            ElseIf caso = 3 Then
                Try
                    id = obj.ConocerIdEmpresa(txtNuevo.Text)
                    If id <> "" Then
                        MessageBox.Show("Esa Empresa y/o Municipio ya existe.")
                    Else
                        objDB.InsertarEmpresaDB(txtNuevo.Text.ToUpper)
                        MsgBox("Se ha ingresado correctamente " & txtNuevo.Text & " a la Base de Datos", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "Captura Exitosa")
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
                Me.Close()
                Form1.Buscarempresa()
            ElseIf caso = 4 Then
                Try
                    id = obj.ConocerIdaseguradora(txtNuevo.Text)
                    If id <> "" Then
                        MessageBox.Show("Esa Aseguradora ya existe.")
                    Else
                        objDB.InsertarAseguradoraDB(txtNuevo.Text.ToUpper)
                        MsgBox("Se ha ingresado correctamente " & txtNuevo.Text & " a la Base de Datos", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "Captura Exitosa")
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
                Me.Close()
                Form1.Buscaraseguradora()
            ElseIf caso = 5 Then
                Try
                    id = obj.ConocerIdMotivo(txtNuevo.Text)
                    If id <> "" Then
                        MessageBox.Show("Ese Motivo ya existe.")
                    Else
                        objDB.InsertarMotivoDB(txtNuevo.Text.ToUpper)
                        MsgBox("Se ha ingresado correctamente " & txtNuevo.Text & " a la Base de Datos", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "Captura Exitosa")
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
                Me.Close()
                Form1.BuscarMotivo()
            ElseIf caso = 6 Then
                Try
                    id = obj.ConocerIdEncargado(txtNuevo.Text)
                    If id <> "" Then
                        MessageBox.Show("Ese Encargado ya existe.")
                    Else
                        objDB.InsertarEncargadoDB(txtNuevo.Text.ToUpper)
                        MsgBox("Se ha ingresado correctamente " & txtNuevo.Text & " a la Base de Datos", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "Captura Exitosa")
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
                Me.Close()
                Form1.Buscarencargado()
            ElseIf caso = 7 Then
                Try
                    id = obj.ConocerIdColor(txtNuevo.Text)
                    If id <> "" Then
                        MessageBox.Show("Ese Color ya existe")
                    Else
                        objDB.InsertarColorDB(txtNuevo.Text.ToUpper)
                        MsgBox("Se ha ingresado correctamente " & txtNuevo.Text & " a la Base de Datos", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "Captura Exitosa")
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
                Me.Close()
                Form1.Buscarcolor()
            End If
        End If

    End Sub
    Private Sub NuevoDB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtNuevo.Clear()
        If caso = 1 Then
            Label1.Text = "Corralón:"
            Me.Text = "Nuevo Corralón"
        ElseIf caso = 2 Then
            Label1.Text = "Autoridad:"
            Me.Text = "Nueva Autoridad"
        ElseIf caso = 3 Then
            Label1.Text = "Empresa:"
            Me.Text = "Nueva Empresa"
        ElseIf caso = 4 Then
            Label1.Text = "Aseguradora:"
            Me.Text = "Nueva Aseguradora"
        ElseIf caso = 5 Then
            Label1.Text = "Motivo:"
            Me.Text = "Nuevo Motivo"
        ElseIf caso = 6 Then
            Label1.Text = "Encargado:"
            Me.Text = "Nuevo Encargado"
        ElseIf caso = 7 Then
            Label1.Text = "Color:"
            Me.Text = "Nuevo Color"
        End If


    End Sub
End Class