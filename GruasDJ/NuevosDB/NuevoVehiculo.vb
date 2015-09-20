Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Public Class NuevoVehiculo
    Dim objDB As New InsertarElementosDB
    Dim obj As New CboxConsultas
    Private Sub NuevoVehiculo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtModelo.Clear()
        marcaCB.Enabled = False
        txtMarca.Enabled = False
        conocidaCHB.Checked = False
        nuevaCHB.Checked = False
        txtMarca.Clear()
        Try
            marcaCB.DataSource = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim Pregunta As Integer
        Dim Marca As String = Form1.MarcaCB.Text.ToString
        Dim tipo As String = txtModelo.Text.ToString
        Dim idvehiculo As Integer

        Try
            idvehiculo = obj.ConocerIdVehiculo(marcaCB.Text,txtModelo.Text )
            If idvehiculo > 0 Then
                Label1.Text = idvehiculo
                MessageBox.Show("Ese tipo de vehículo ya existe")
                Me.Dispose()
            End If
        Catch ex As Exception

            If txtModelo.Text = "" Then
                MsgBox("No has insertado un tipo de vehículo, por favor ingresa un tipo.", MsgBoxStyle.OkOnly, "Falta Tipo")

            ElseIf conocidaCHB.Checked = False And nuevaCHB.Checked = False Then
                MsgBox("No has insertado una Marca, por favor ingresa una Marca.", MsgBoxStyle.OkOnly, "Falta Marca")
            Else
                If nuevaCHB.Checked = True Then
                    Pregunta = MsgBox("¿Seguro que deseas agregar un " & txtMarca.Text & " " & txtModelo.Text & " ?", vbYesNo + vbExclamation + vbDefaultButton2, "Agregar Vehículo")
                Else : conocidaCHB.Checked = True
                    Pregunta = MsgBox("¿Seguro que deseas agregar un " & marcaCB.Text & txtModelo.Text & " ?", vbYesNo + vbExclamation + vbDefaultButton2, "Agregar Vehículo")
                End If

                If Pregunta = vbYes Then

                    If nuevaCHB.Checked = True Then
                        objDB.InsertarVehiculosDB(txtMarca.Text, txtModelo.Text)
                    ElseIf conocidaCHB.Checked = True Then
                        objDB.InsertarVehiculosDB(marcaCB.Text, txtModelo.Text)
                    End If
                    Me.Close()
                Else
                    MsgBox("El vehículo no se há agregado", MsgBoxStyle.OkOnly, "Registro no insertado")
                    Me.Close()
                End If
            End If
        End Try




        Form1.Buscartipo(Marca)
        Form1.BuscarMarca()



    End Sub

    Sub BuscarMarca()
        Try
            'recuperando los datos de la consulta
            Dim tbl As DataTable = obj.BusMarca
            'Vaciando los datos en el Combo Box
            MarcaCB.DataSource = tbl
            MarcaCB.DisplayMember = "Marca"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

   

    Private Sub conocidaCHB_CheckedChanged(sender As Object, e As EventArgs) Handles conocidaCHB.CheckedChanged
        If conocidaCHB.Checked = True Then
            marcaCB.Enabled = True
            txtMarca.Enabled = False
            nuevaCHB.Checked = False
            BuscarMarca()
        End If

    End Sub

    Private Sub nuevaCHB_CheckedChanged(sender As Object, e As EventArgs) Handles nuevaCHB.CheckedChanged
        If nuevaCHB.Checked = True Then
            txtMarca.Enabled = True
            marcaCB.Enabled = False
            conocidaCHB.Checked = False
            Try
                marcaCB.DataSource = Nothing
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub
End Class