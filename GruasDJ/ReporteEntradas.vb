Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Public Class corralonCH
    Dim obj As New CboxConsultas
    Dim objC As New ConsEntradas
    Dim objUE As New UpdatesEntradas
    'Atención update con Clases recogidas -------------------------------
    Sub costosIinicio()
        CostoAbogado.TextAlign = HorizontalAlignment.Right
        CostoOficial.TextAlign = HorizontalAlignment.Right
        CostoMP.TextAlign = HorizontalAlignment.Right
        CostoTotal.TextAlign = HorizontalAlignment.Right

        If CostosCHB.Checked = False Then
            CostoAbogado.Enabled = False
            CostoMP.Enabled = False
            CostoOficial.Enabled = False
        Else

            CostoMP.Enabled = True
            CostoOficial.Enabled = True
            CostoAbogado.Enabled = True

        End If
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

    Private Sub Costo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CostoAbogado.KeyPress, CostoMP.KeyPress, CostoOficial.KeyPress
        NumConFrac(Me.CostoMP, e)
        NumConFrac(Me.CostoOficial, e)
        NumConFrac(Me.CostoAbogado, e)
    End Sub

    Sub UpdateEntrada(ByVal ColorC As String, ByVal Corralon As String, ByVal Marca As String, ByVal tipo As String, _
                     ByVal motivo As String, ByVal autoridad As String, ByVal empresa As String, _
                     ByVal encargado As String, ByVal aseguradora As String)
        'Carga los contadores de los registros en cada tabla e inserta los Datos 

        Dim ColorId As String
        Dim Idcorralon As String
        Dim idvehiculo As String
        Dim idmotivo As String
        Dim idautoridad As String
        Dim IdEmpresa As String
        Dim idEncargado As String
        Dim IdAseguradora As String
        Dim IdOb As Integer
        Dim idEntrada As Integer = PublicIdEntrada
        Try
            Dim nres As Integer
            'Carga Id's de entrada a Actualizar

            Idcorralon = obj.ConocerIdCorralon(Corralon)
            idvehiculo = obj.ConocerIdVehiculo(Marca, tipo)
            ColorId = obj.ConocerIdColor(ColorC)
            idmotivo = obj.ConocerIdMotivo(motivo)
            idautoridad = obj.ConocerIdAutoridad(autoridad)
            IdEmpresa = obj.ConocerIdEmpresa(empresa)
            idEncargado = obj.ConocerIdEncargado(encargado)
            IdAseguradora = obj.ConocerIdaseguradora(aseguradora)
            IdOb = objUE.ConocerIdobservaciones(idEntrada)

            If ObservacionesCH.Checked = True Then
                If IdOb = 0 Then
                    IdOb = obj.CodigoObservaciones
                    nres = obj.Grabarobservaciones(IdOb, Obtxt.Text)
                Else
                    nres = objUE.UpdateObservaciones(IdOb, Obtxt.Text)

                End If

            Else

            End If

            'Actualiza los datos Entradas
            If CorralonCHB.Checked = True Or MarcaCH.Checked = True Or tipoCH.Checked = True Or _
             EmpresaCH.Checked = True Or AutoridadCH.Checked = True Or AseguradoraCH.Checked = True Or _
             MotivoCH.Checked = True Or EncargadoCH.Checked = True Or ObservacionesCH.Checked = True Or _
             FechaCH.Checked = True Then
                nres = objUE.UpdateEntrada(idEntrada, Fecha.Text, Idcorralon, idvehiculo, idmotivo, idautoridad, IdEmpresa, idEncargado, IdAseguradora, IdOb)
            End If
            'Actualiza datos Inventario
            If inventarioCH.Checked = True Or ExpedienteCH.Checked = True Or FolioCH.Checked = True Or SiniestroCH.Checked = True Then
                nres = objUE.UpdateInventario(idEntrada, UCase(InventarioTXT.Text), UCase(ExpedienteTXT.Text), UCase(SiniestroTXT.Text), UCase(FolioAsTXT.Text))
            End If
            'Actualiza datos Vehículo
            If PlacasCH.Checked = True Or SerieCH.Checked = True Or ColorCH.Checked = True Then
                nres = objUE.UpdateDatosV(idEntrada, UCase(PlacasTXT.Text), UCase(SerieTXT.Text), ColorId)
            End If
            'Actualiza Datos Grua
            If gruaCH.Checked = True Or OperadorCH.Checked = True Then
                nres = objUE.Updategrua(idEntrada, UCase(GruaTXT.Text), UCase(OperadorTXT.Text))
            End If
            If CostosCHB.Checked = True Then
                nres = objUE.UpdateCostos(idEntrada, CostoMP.Text, CostoAbogado.Text, CostoOficial.Text, 0, CostoTotal.Text)
            End If
            MessageBox.Show("Se actualizaron los datos exitosamente.", "Registro Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Fecha.Value = Date.Now
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

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
    Sub serie()
        If SerieCH.Checked = True Then

            If SerieTXT.Text.Length = 0 Then
                SerieTXT.BackColor = Color.White
            ElseIf SerieTXT.Text.Length < 17 Or SerieTXT.Text.Length > 17 Then
                SerieTXT.BackColor = Color.LightPink
            Else
                SerieTXT.BackColor = Color.LightGreen

            End If
        Else
            SerieTXT.BackColor = Color.White
        End If
    End Sub
    Sub ChboxEstado()
        If CorralonCHB.Checked = True Or MarcaCH.Checked = True Or tipoCH.Checked = True Or ColorCH.Checked = True Or _
            PlacasCH.Checked = True Or SerieCH.Checked = True Or inventarioCH.Checked = True Or EmpresaCH.Checked = True Or _
            AutoridadCH.Checked = True Or ExpedienteCH.Checked = True Or gruaCH.Checked = True Or OperadorCH.Checked = True Or _
            AseguradoraCH.Checked = True Or FolioCH.Checked = True Or SiniestroCH.Checked = True Or MotivoCH.Checked = True Or _
            EncargadoCH.Checked = True Or ObservacionesCH.Checked = True Or FechaCH.Checked = True Or CostosCHB.Checked = True Then

            Actualizarbtn.Enabled = True
        Else

            Actualizarbtn.Enabled = False
        End If

    End Sub


    Sub Reporte()
        ChboxEstado()
        Dim IdEntrada As Integer = PublicIdEntrada
        Dim tbl As DataTable

        Try
            tbl = objC.EntradasCompletas(IdEntrada)
            'recuperando los datos de la consulta

            'Vaciando los datos en el Combo Box
            Dim fechaReporte As Date = Date.Parse(tbl.Rows(0)("Fecha").ToString)
            Fecha.Text = (fechaReporte)

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
            CostoTotal.Text = Trim(tbl.Rows(0)("TotalCostos").ToString)
            Obtxt.Text = tbl.Rows(0)("Observaciones").ToString


            CorralonCB.Items.Add(tbl.Rows(0)("NombreCorralon").ToString)
            MarcaCB.Items.Add(tbl.Rows(0)("Marca").ToString)
            TipoCB.Items.Add(tbl.Rows(0)("Tipo").ToString)

            ColorCB.Items.Add(tbl.Rows(0)("Color").ToString)
            EmpresaCB.Items.Add(tbl.Rows(0)("NombreEmp").ToString)
            AutoridadCB.Items.Add(tbl.Rows(0)("NomAutoridad").ToString)
            AseguradoraCB.Items.Add(tbl.Rows(0)("NomAseguradora").ToString)
            MotivoCB.Items.Add(tbl.Rows(0)("Motivo").ToString)
            EncargadoCB.Items.Add(tbl.Rows(0)("Nomencargado").ToString)
            CorralonCB.SelectedIndex = 0
            MarcaCB.SelectedIndex = 0
            TipoCB.SelectedIndex = 0
            ColorCB.SelectedIndex = 0
            EmpresaCB.SelectedIndex = 0
            AutoridadCB.SelectedIndex = 0
            AseguradoraCB.SelectedIndex = 0
            MotivoCB.SelectedIndex = 0
            EncargadoCB.SelectedIndex = 0
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try



    End Sub
    Private Sub ReporteEntradas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Fecha.Format = DateTimePickerFormat.Custom
        Fecha.CustomFormat = "dd/MM/yyyy"
        Fecha.Value = Date.Now
        Reporte()
        costosIinicio()

        If FechaCH.Checked = True Then
            Fecha.Enabled = True
        Else
            Fecha.Enabled = False
        End If
        tipoCH.Checked = True
        tipoCH.Checked = False
    End Sub

    Private Sub FechaCH_CheckedChanged(sender As Object, e As EventArgs) Handles FechaCH.CheckedChanged
        ChboxEstado()
        If FechaCH.Checked = True Then
            Fecha.Enabled = True
        Else
            Fecha.Enabled = False
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CorralonCHB.CheckedChanged
        ChboxEstado()

        If CorralonCHB.Checked = True Then
            CorralonCB.Enabled = True
            BuscarCorralon()
        Else
            CorralonCB.Enabled = False

            Try

                Dim IdEntrada As Integer = PublicIdEntrada
                Dim tbl As DataTable
                tbl = objC.EntradasCompletas(IdEntrada)

                CorralonCB.DataSource = Nothing
                CorralonCB.Items.Add(tbl.Rows(0)("NombreCorralon").ToString)
                CorralonCB.SelectedIndex = 0
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

    End Sub

    Private Sub MarcaCH_CheckedChanged(sender As Object, e As EventArgs) Handles MarcaCH.CheckedChanged
        ChboxEstado()
        If MarcaCH.Checked = True Then
            MarcaCB.Enabled = True
            tipoCH.Checked = True

            BuscarMarca()
        Else
            MarcaCB.Enabled = False

            Try
                Dim IdEntrada As Integer = PublicIdEntrada
                Dim tbl As DataTable
                tbl = objC.EntradasCompletas(IdEntrada)
                MarcaCB.DataSource = Nothing
                TipoCB.DataSource = Nothing
                MarcaCB.Items.Add(tbl.Rows(0)("Marca").ToString)
                MarcaCB.SelectedIndex = 0
                tipoCH.Checked = False
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If
    End Sub

    Private Sub tipoCH_CheckedChanged(sender As Object, e As EventArgs) Handles tipoCH.CheckedChanged
        ChboxEstado()
        If tipoCH.Checked = True Then
            TipoCB.Enabled = True
            Dim Marca As String = MarcaCB.Text.ToString
            Buscartipo(Marca)

        Else
            If tipoCH.Checked = False And MarcaCH.Checked = True Then
                MarcaCH.Checked = False
                tipoCH.Checked = True
                tipoCH.Checked = False
            Else
                Try
                    Dim IdEntrada As Integer = PublicIdEntrada
                    Dim tbl As DataTable
                    tbl = objC.EntradasCompletas(IdEntrada)
                    TipoCB.DataSource = Nothing
                    TipoCB.Items.Add(tbl.Rows(0)("Tipo").ToString)
                    TipoCB.SelectedIndex = 0
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
                TipoCB.Enabled = False
            End If
        End If
    End Sub

    Private Sub ColorCH_CheckedChanged(sender As Object, e As EventArgs) Handles ColorCH.CheckedChanged
        ChboxEstado()
        If ColorCH.Checked = True Then
            ColorCB.Enabled = True
            Buscarcolor()
        Else
            ColorCB.Enabled = False
            Try

                Dim IdEntrada As Integer = PublicIdEntrada
                Dim tbl As DataTable
                tbl = objC.EntradasCompletas(IdEntrada)

                ColorCB.DataSource = Nothing
                ColorCB.Items.Add(tbl.Rows(0)("Color").ToString)
                ColorCB.SelectedIndex = 0
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
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
    Private Sub PlacasCH_CheckedChanged(sender As Object, e As EventArgs) Handles PlacasCH.CheckedChanged
        ChboxEstado()
        If PlacasCH.Checked = True Then
            PlacasTXT.Enabled = True
        Else
            Try
                Dim IdEntrada As Integer = PublicIdEntrada
                Dim tbl As DataTable
                tbl = objC.EntradasCompletas(IdEntrada)
                PlacasTXT.Text = Trim(tbl.Rows(0)("Placas").ToString)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            PlacasTXT.Enabled = False
        End If
    End Sub


    Private Sub SeruieCH_CheckedChanged(sender As Object, e As EventArgs) Handles SerieCH.CheckedChanged
        ChboxEstado()
        If SerieCH.Checked = True Then

            SerieTXT.Enabled = True

        Else
            Try
                Dim IdEntrada As Integer = PublicIdEntrada
                Dim tbl As DataTable
                tbl = objC.EntradasCompletas(IdEntrada)
                SerieTXT.Text = Trim(tbl.Rows(0)("Serie").ToString)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            SerieTXT.Enabled = False
        End If
        serie()

    End Sub

    Private Sub inventarioCH_CheckedChanged(sender As Object, e As EventArgs) Handles inventarioCH.CheckedChanged
        ChboxEstado()
        If inventarioCH.Checked = True Then

            InventarioTXT.Enabled = True
        Else
            Try
                Dim IdEntrada As Integer = PublicIdEntrada
                Dim tbl As DataTable
                tbl = objC.EntradasCompletas(IdEntrada)
                InventarioTXT.Text = Trim(tbl.Rows(0)("NoInventario").ToString)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            InventarioTXT.Enabled = False

        End If


    End Sub

    Private Sub EmpresaCH_CheckedChanged(sender As Object, e As EventArgs) Handles EmpresaCH.CheckedChanged
        ChboxEstado()
        If EmpresaCH.Checked = True Then
            EmpresaCB.Enabled = True
            Buscarempresa()
        Else
            EmpresaCB.Enabled = False
            Try

                Dim IdEntrada As Integer = PublicIdEntrada
                Dim tbl As DataTable
                tbl = objC.EntradasCompletas(IdEntrada)

                EmpresaCB.DataSource = Nothing
                EmpresaCB.Items.Add(tbl.Rows(0)("NombreEmp").ToString)
                EmpresaCB.SelectedIndex = 0
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub AutoridadCH_CheckedChanged(sender As Object, e As EventArgs) Handles AutoridadCH.CheckedChanged
        ChboxEstado()
        If AutoridadCH.Checked = True Then
            AutoridadCB.Enabled = True
            BuscarAutoridad()
        Else
            AutoridadCB.Enabled = False
            Try

                Dim IdEntrada As Integer = PublicIdEntrada
                Dim tbl As DataTable
                tbl = objC.EntradasCompletas(IdEntrada)

                AutoridadCB.DataSource = Nothing
                AutoridadCB.Items.Add(tbl.Rows(0)("NomAutoridad").ToString)
                AutoridadCB.SelectedIndex = 0
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub ExpedienteCH_CheckedChanged(sender As Object, e As EventArgs) Handles ExpedienteCH.CheckedChanged
        ChboxEstado()
        If ExpedienteCH.Checked = True Then

            ExpedienteTXT.Enabled = True
        Else
            Try
                Dim IdEntrada As Integer = PublicIdEntrada
                Dim tbl As DataTable
                tbl = objC.EntradasCompletas(IdEntrada)
                ExpedienteTXT.Text = Trim(tbl.Rows(0)("NoExpediente").ToString)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            ExpedienteTXT.Enabled = False
        End If
    End Sub

    Private Sub gruaCH_CheckedChanged(sender As Object, e As EventArgs) Handles gruaCH.CheckedChanged
        ChboxEstado()
        If gruaCH.Checked = True Then

            GruaTXT.Enabled = True
        Else
            Try
                Dim IdEntrada As Integer = PublicIdEntrada
                Dim tbl As DataTable
                tbl = objC.EntradasCompletas(IdEntrada)
                GruaTXT.Text = Trim(tbl.Rows(0)("NoGrua").ToString)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            GruaTXT.Enabled = False
        End If
    End Sub

    Private Sub OperadorCH_CheckedChanged(sender As Object, e As EventArgs) Handles OperadorCH.CheckedChanged
        ChboxEstado()
        If OperadorCH.Checked = True Then

            OperadorTXT.Enabled = True
        Else
            Try
                Dim IdEntrada As Integer = PublicIdEntrada
                Dim tbl As DataTable
                tbl = objC.EntradasCompletas(IdEntrada)
                OperadorTXT.Text = Trim(tbl.Rows(0)("Operador").ToString)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            OperadorTXT.Enabled = False
        End If
    End Sub

    Private Sub AseguradoraCH_CheckedChanged(sender As Object, e As EventArgs) Handles AseguradoraCH.CheckedChanged
        ChboxEstado()
        If AseguradoraCH.Checked = True Then
            AseguradoraCB.Enabled = True
            Buscaraseguradora()
        Else
            AseguradoraCB.Enabled = False
            Try

                Dim IdEntrada As Integer = PublicIdEntrada
                Dim tbl As DataTable
                tbl = objC.EntradasCompletas(IdEntrada)

                AseguradoraCB.DataSource = Nothing
                AseguradoraCB.Items.Add(tbl.Rows(0)("NomAseguradora").ToString)
                AseguradoraCB.SelectedIndex = 0
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub FoloCH_CheckedChanged(sender As Object, e As EventArgs) Handles FolioCH.CheckedChanged
        ChboxEstado()
        If FolioCH.Checked = True Then

            FolioAsTXT.Enabled = True
        Else
            Try
                Dim IdEntrada As Integer = PublicIdEntrada
                Dim tbl As DataTable
                tbl = objC.EntradasCompletas(IdEntrada)
                FolioAsTXT.Text = Trim(tbl.Rows(0)("NoFolio").ToString)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            FolioAsTXT.Enabled = False
        End If
    End Sub

    Private Sub SiniestroCH_CheckedChanged(sender As Object, e As EventArgs) Handles SiniestroCH.CheckedChanged
        ChboxEstado()
        If SiniestroCH.Checked = True Then

            SiniestroTXT.Enabled = True
        Else
            Try
                Dim IdEntrada As Integer = PublicIdEntrada
                Dim tbl As DataTable
                tbl = objC.EntradasCompletas(IdEntrada)
                SiniestroTXT.Text = Trim(tbl.Rows(0)("Nosiniestro").ToString)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            SiniestroTXT.Enabled = False
        End If
    End Sub

    Private Sub MotivoCH_CheckedChanged(sender As Object, e As EventArgs) Handles MotivoCH.CheckedChanged
        ChboxEstado()
        If MotivoCH.Checked = True Then
            MotivoCB.Enabled = True
            BuscarMotivo()
        Else
            MotivoCB.Enabled = False
            Try

                Dim IdEntrada As Integer = PublicIdEntrada
                Dim tbl As DataTable
                tbl = objC.EntradasCompletas(IdEntrada)

                MotivoCB.DataSource = Nothing
                MotivoCB.Items.Add(tbl.Rows(0)("motivo").ToString)
                MotivoCB.SelectedIndex = 0
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub EncargadoCH_CheckedChanged(sender As Object, e As EventArgs) Handles EncargadoCH.CheckedChanged
        ChboxEstado()
        If EncargadoCH.Checked = True Then
            EncargadoCB.Enabled = True
            Buscarencargado()
        Else
            EncargadoCB.Enabled = False
            Try

                Dim IdEntrada As Integer = PublicIdEntrada
                Dim tbl As DataTable
                tbl = objC.EntradasCompletas(IdEntrada)

                EncargadoCB.DataSource = Nothing
                EncargadoCB.Items.Add(tbl.Rows(0)("NomEncargado").ToString)
                EncargadoCB.SelectedIndex = 0
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If
    End Sub

    Private Sub ObservacionesCH_CheckedChanged(sender As Object, e As EventArgs) Handles ObservacionesCH.CheckedChanged
        ChboxEstado()
        If ObservacionesCH.Checked = True Then
            Obtxt.Enabled = True
        Else
            Obtxt.Enabled = False
            Try

                Dim IdEntrada As Integer = PublicIdEntrada
                Dim tbl As DataTable
                tbl = objC.EntradasCompletas(IdEntrada)
                Obtxt.Text = tbl.Rows(0)("Observaciones").ToString

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub SerieTXT_TextChanged(sender As Object, e As EventArgs) Handles SerieTXT.TextChanged

        serie()

    End Sub

    Private Sub MarcaCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MarcaCB.SelectedIndexChanged
        Dim Marca As String = MarcaCB.Text.ToString
        Buscartipo(Marca)
    End Sub



    Private Sub CostoMP_TextChanged(sender As Object, e As EventArgs) Handles CostoMP.TextChanged
        Dim CMP As Single
        Dim CAB As Single
        Dim COF As Single
        Dim CTotal As Single
        If CostoMP.Text = "0" Then
            ' Seleccionas todo el texto del campo
            CostoMP.SelectAll()
        Else
            CostoMP.Focus()
        End If
        Try
            If CostosCHB.Checked = False Then

            Else

                If CostoMP.Text.Length = 0 Then
                    CostoMP.Text = "0"

                Else

                    CMP = CostoMP.Text
                    CAB = CostoAbogado.Text
                    COF = CostoOficial.Text
                    CTotal = CMP + CAB + COF
                    CostoTotal.Text = CTotal

                End If


            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub



    Private Sub CostoOficial_TextChanged(sender As Object, e As EventArgs) Handles CostoOficial.TextChanged
        Dim CMP As Single
        Dim CAB As Single
        Dim COF As Single
        Dim CTotal As Single
        If CostoOficial.Text = "0" Then
            ' Seleccionas todo el texto del campo
            CostoOficial.SelectAll()
        Else
            CostoOficial.Focus()
        End If

        Try
            If CostosCHB.Checked = False Then

            Else

                If CostoOficial.Text.Length = 0 Then
                    CostoOficial.Text = "0"

                Else

                    CMP = CostoMP.Text
                    CAB = CostoAbogado.Text
                    COF = CostoOficial.Text
                    CTotal = CMP + CAB + COF
                    CostoTotal.Text = CTotal

                End If


            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub



    Private Sub CostoAbogado_TextChanged(sender As Object, e As EventArgs) Handles CostoAbogado.TextChanged
        Dim CMP As Single
        Dim CAB As Single
        Dim COF As Single
        Dim CTotal As Single

        If CostoAbogado.Text = "0" Then
            ' Seleccionas todo el texto del campo
            CostoAbogado.SelectAll()
        Else
            CostoAbogado.Focus()
        End If

        Try
            If CostosCHB.Checked = False Then

            Else

                If CostoAbogado.Text.Length = 0 Then
                    CostoAbogado.Text = "0"

                Else

                    CMP = CostoMP.Text
                    CAB = CostoAbogado.Text
                    COF = CostoOficial.Text
                    CTotal = CMP + CAB + COF
                    CostoTotal.Text = CTotal

                End If


            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub CostosCHB_CheckedChanged(sender As Object, e As EventArgs) Handles CostosCHB.CheckedChanged

        costosIinicio()
        ChboxEstado()

    End Sub

    Private Sub Actualizarbtn_Click(sender As Object, e As EventArgs) Handles Actualizarbtn.Click

        Dim colorC As String = ColorCB.Text
        Dim corralon As String = CorralonCB.Text
        Dim marca As String = MarcaCB.Text
        Dim tipo As String = TipoCB.Text
        Dim motivo As String = MotivoCB.Text
        Dim autoridad As String = AutoridadCB.Text
        Dim empresa As String = EmpresaCB.Text
        Dim encargado As String = EncargadoCB.Text
        Dim aseguradora As String = AseguradoraCB.Text
        Passwords.ShowDialog()
        If Form1.pass = Passwords.passinput Then
            UpdateEntrada(colorC, corralon, marca, tipo, motivo, autoridad, empresa, encargado, aseguradora)
            Form1.MostrarEntradas()
            Me.Close()

        Else
            MessageBox.Show("Contraseña incorrecta el Registro no se ha modificado", "Contraseña incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub
End Class