Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Public Class ReporteSalidas

    Dim obj As New CboxConsultas
    Dim objC As New ConsEntradas
    Dim objUE As New UpdatesEntradas
    Dim objS As New ConsSalidas
    'Atención update con Clases recogidas -------------------------------
    Private Sub ObSalidastxt_KeyDown(sender As Object, e As KeyEventArgs) Handles ObSalidasTXT.KeyDown

        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
        End If

    End Sub

    Sub costosIinicio()
        CostoAbogado.TextAlign = HorizontalAlignment.Right
        CostoOficial.TextAlign = HorizontalAlignment.Right
        CostoMP.TextAlign = HorizontalAlignment.Right
        TotalTXT.TextAlign = HorizontalAlignment.Right
        IngresosTXT.TextAlign = HorizontalAlignment.Right
        txtTotalIva.TextAlign = HorizontalAlignment.Right





    End Sub

    Private Sub Obtxt_KeyDown(sender As Object, e As KeyEventArgs)

        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
        End If

    End Sub
    'Sólo Números y punto
    Public Sub NumConFrac(ByVal CajaTexto As Windows.Forms.TextBox, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        ElseIf e.KeyChar = "." Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub CostoAbogado_Click(sender As Object, e As EventArgs) Handles CostoAbogado.Click
        If CostoAbogado.SelectionLength = 0 Then
            ' Seleccionas todo el texto del campo
            CostoAbogado.SelectAll()
        Else
            CostoAbogado.Focus()
        End If
    End Sub
    Private Sub CostoMP_Click(sender As Object, e As EventArgs) Handles CostoMP.Click
        If CostoMP.SelectionLength = 0 Then
            ' Seleccionas todo el texto del campo
            CostoMP.SelectAll()
        Else
            CostoMP.Focus()
        End If
    End Sub
    Private Sub CostoOficial_Click(sender As Object, e As EventArgs) Handles CostoOficial.Click
        If CostoOficial.SelectionLength = 0 Then
            ' Seleccionas todo el texto del campo
            CostoOficial.SelectAll()
        Else
            CostoOficial.Focus()
        End If
    End Sub

    Private Sub Costo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CostoAbogado.KeyPress, CostoMP.KeyPress, CostoOficial.KeyPress, IngresosTXT.KeyPress
        NumConFrac(Me.IngresosTXT, e)
    End Sub


    Sub Buscarencargado()
        Try
            'recuperando los datos de la consulta
            Dim tbl As DataTable = obj.BusEncargados
            'Vaciando los datos en el Combo Box
            EncargadoCB.DataSource = tbl
            EncargadoCB.DisplayMember = "NomEncargado"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Sub BuscarCorralon()
        Try
            'recuperando los datos de la consulta
            Dim tbl As DataTable = obj.BusCorralones
            'Vaciando los datos en el Combo Box
            CorralonCB.DataSource = tbl
            CorralonCB.DisplayMember = "NombreCorralon"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
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

    Sub Buscartipo(ByVal Marca As String)
        Try
            'recuperando los datos de la consulta
            Dim tbl As DataTable = obj.Bustipo(Marca)
            'Vaciando los datos en el Combo Box
            TipoCB.DataSource = tbl
            TipoCB.DisplayMember = "tipo"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Sub Buscarcolor()
        Try
            'recuperando los datos de la consulta
            Dim tbl As DataTable = obj.Buscolor
            'Vaciando los datos en el Combo Box
            ColorCB.DataSource = tbl
            ColorCB.DisplayMember = "color"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Sub Buscarempresa()
        Try
            'recuperando los datos de la consulta
            Dim tbl As DataTable = obj.BusEmpresa
            'Vaciando los datos en el Combo Box
            EmpresaCB.DataSource = tbl
            EmpresaCB.DisplayMember = "NombreEmp"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Sub Buscaraseguradora()
        Try
            'recuperando los datos de la consulta
            Dim tbl As DataTable = obj.BusAseguradora
            'Vaciando los datos en el Combo Box
            AseguradoraCB.DataSource = tbl
            AseguradoraCB.DisplayMember = "NomAseguradora"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub
    Sub BuscarFormaDePago()
        Try
            'recuperando los datos de la consulta
            Dim tbl As DataTable = obj.BusTpago
            'Vaciando los datos en el Combo Box
            FPagoCB.DataSource = tbl
            FPagoCB.DisplayMember = "Fpago"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Sub BuscarAutoridad()
        Try
            'recuperando los datos de la consulta
            Dim tbl As DataTable = obj.BusAutoridad
            'Vaciando los datos en el Combo Box
            AutoridadCB.DataSource = tbl
            AutoridadCB.DisplayMember = "NomAutoridad"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Sub BuscarMotivo()
        Try
            'recuperando los datos de la consulta
            Dim tbl As DataTable = obj.BusMotivo
            'Vaciando los datos en el Combo Box
            MotivoCB.DataSource = tbl
            MotivoCB.DisplayMember = "motivo"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub



    Sub Reporte()
        Dim IdEntrada As String = PublicIdSalida
        Dim tbl As DataTable

        Try
            tbl = objS.SalidasCompletas(IdEntrada)
            'recuperando los datos de la consulta
            FolioSalidatxt.Text = ("Folio: " & tbl.Rows(0)("IdSalida").ToString) 'Folio de la salida.
            'Vaciando los datos en el Combo Box
            Dim fechaReporte As Date = Date.Parse(tbl.Rows(0)("Fecha").ToString)
            FechaEnt.Text = (fechaReporte)

            FolioEntradatxt.Text = Trim("Folio: " & tbl.Rows(0)("IdEntrada").ToString)

            PlacasTXT.Text = Trim(tbl.Rows(0)("Placas").ToString)
            SerieTXT.Text = Trim(tbl.Rows(0)("Serie").ToString)
            InventarioTXT.Text = Trim(tbl.Rows(0)("NoInventario").ToString)
            ExpedienteTXT.Text = Trim(tbl.Rows(0)("NoExpediente").ToString)
            GruaTXT.Text = Trim(tbl.Rows(0)("NoGrua").ToString)
            OperadorTXT.Text = Trim(tbl.Rows(0)("Operador").ToString)
            FolioAsTXT.Text = Trim(tbl.Rows(0)("NoFolio").ToString)
            SiniestroTXT.Text = Trim(tbl.Rows(0)("NoSiniestro").ToString)
            CostoMP.Text = Trim(tbl.Rows(0)("Autoridad").ToString)
            CostoAbogado.Text = Trim(tbl.Rows(0)("Abogado").ToString)
            CostoOficial.Text = Trim(tbl.Rows(0)("Policia").ToString)
            IngresosTXT.Text = Trim(tbl.Rows(0)("Ingresos").ToString)
            TotalTXT.Text = Trim(tbl.Rows(0)("TotalCostos").ToString)
            Obtxt.Text = tbl.Rows(0)("Observaciones").ToString
            ObSalidasTXT.Text = tbl.Rows(0)(26).ToString


            CorralonCB.Items.Add(tbl.Rows(0)("NombreCorralon").ToString)
            MarcaCB.Items.Add(tbl.Rows(0)("Marca").ToString)
            TipoCB.Items.Add(tbl.Rows(0)("Tipo").ToString)
            ColorCB.Items.Add(tbl.Rows(0)("Color").ToString)
            EmpresaCB.Items.Add(tbl.Rows(0)("NombreEmp").ToString)
            AutoridadCB.Items.Add(tbl.Rows(0)("NomAutoridad").ToString)
            AseguradoraCB.Items.Add(tbl.Rows(0)("NomAseguradora").ToString)
            MotivoCB.Items.Add(tbl.Rows(0)("Motivo").ToString)
            EncargadoCB.Items.Add(tbl.Rows(0)("Nomencargado").ToString)
            TipoCB.Items.Add(tbl.Rows(0)("Fpago").ToString)

            If tbl.Rows(0)("Chatarra") = False Then
                ChatarraCHB.Checked = False
            ElseIf tbl.Rows(0)("Chatarra") = True Then
                ChatarraCHB.Checked = True
            End If

            CorralonCB.SelectedIndex = 0
            MarcaCB.SelectedIndex = 0
            TipoCB.SelectedIndex = 0
            ColorCB.SelectedIndex = 0
            EmpresaCB.SelectedIndex = 0
            AutoridadCB.SelectedIndex = 0
            AseguradoraCB.SelectedIndex = 0
            MotivoCB.SelectedIndex = 0
            EncargadoCB.SelectedIndex = 0
            TipoCB.SelectedIndex = 0
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try




    End Sub
    Private Sub Salidas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FechaEnt.Format = DateTimePickerFormat.Custom
        FechaEnt.CustomFormat = "dd/MM/yyyy"
        FechaSalida.Format = DateTimePickerFormat.Custom
        FechaSalida.CustomFormat = "dd/MM/yyyy"
        FechaEnt.Value = Date.Now
        Reporte()
        costosIinicio()
        BuscarFormaDePago()
        txtTotalIva.Text = (TotalTXT.Text * 0.16) + (TotalTXT.Text)

    End Sub




    Private Sub PlacasTXT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PlacasTXT.KeyPress
        If Char.IsLetterOrDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub


    Private Sub SalidaBtn_Click(sender As Object, e As EventArgs) Handles SalidaBtn.Click

        Me.Close()
    End Sub

    Private Sub IngresosTXT_TextChanged(sender As Object, e As EventArgs) Handles IngresosTXT.TextChanged, IngresosTXT.Click


    End Sub

    Private Sub Label23_Click(sender As Object, e As EventArgs) Handles Label23.Click

    End Sub
End Class
