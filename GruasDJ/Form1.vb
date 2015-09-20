Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Public Class Form1
    'Crea objeto para la clase con las consultas SQL 
    Dim obj As New CboxConsultas
    Dim objC As New ConsEntradas
    Dim objUe As New UpdatesEntradas
    Dim objCS As New ConsSalidas
    'Desactiva el enter en el textbox multilinea
    Public pass As String = "JACKY85"
    Public passcapturista As String = "conrado123"
    Private Sub Obtxt_KeyDown(sender As Object, e As KeyEventArgs) Handles Obtxt.KeyDown

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

   

   
    Sub costosIinicio()
        CostoAbogado.TextAlign = HorizontalAlignment.Right
        CostoOficial.TextAlign = HorizontalAlignment.Right
        CostoMP.TextAlign = HorizontalAlignment.Right
        CostoTotal.TextAlign = HorizontalAlignment.Right
        CostoAbogado.Text = "0"
        CostoMP.Text = "0"
        CostoOficial.Text = "0"
        CostoTotal.Text = "0"

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
    Sub EliminarEntrada()
        
        Dim IdOb As Integer
        Dim idEntrada As Integer = PublicIdEntrada
        Dim Eliminado As String = "Registro Eliminado"
        Try
            Dim nres As Integer
            'Carga Id's de entrada a Actualizar


            IdOb = objUe.ConocerIdobservaciones(idEntrada)

            If IdOb = 0 Then
                IdOb = obj.CodigoObservaciones()
                nres = obj.Grabarobservaciones(IdOb, Eliminado)
                nres = objUe.UpdateEntradaEliminar(idEntrada, IdOb)
            Else
                nres = objUe.UpdateObservaciones(IdOb, Eliminado)
            End If

            MessageBox.Show("El registro se ha elimnado", "Registro eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Sub NuevaEntrada(ByVal ColorC As String, ByVal Corralon As String, ByVal Marca As String, ByVal tipo As String, _
                     ByVal motivo As String, ByVal autoridad As String, ByVal empresa As String, _
                     ByVal encargado As String, ByVal aseguradora As String)
        'Carga los contadores de los registros en cada tabla e inserta los Datos 
        Dim Idcostos As Integer
        Dim ColorId As String
        Dim IdGrua As Integer
        Dim Idcorralon As String
        Dim idvehiculo As String
        Dim IdDatosVehiculo As Integer
        Dim idmotivo As String
        Dim idautoridad As String
        Dim IdInventario As Integer
        Dim IdEmpresa As String
        Dim idEncargado As String
        Dim IdAseguradora As String = "1"
        Dim IdOb As Integer
        Dim idEntrada As Integer
        Try
            'Carga contadores y crea los Id's
            Idcostos = obj.CodigoCostos
            idEntrada = obj.CodigoEntrada
            IdDatosVehiculo = obj.CodigoDatosVehiculo
            ColorId = obj.ConocerIdColor(ColorC)
            IdGrua = obj.CodigoGruas
            IdInventario = obj.CodigoInventario
            Idcorralon = obj.ConocerIdCorralon(Corralon)
            idvehiculo = obj.ConocerIdVehiculo(Marca, tipo)
            idmotivo = obj.ConocerIdMotivo(motivo)
            idautoridad = obj.ConocerIdAutoridad(autoridad)
            IdEmpresa = obj.ConocerIdEmpresa(empresa)
            idEncargado = obj.ConocerIdEncargado(encargado)
            If SeguroCHB.Checked = True Then
                IdAseguradora = obj.ConocerIdaseguradora(aseguradora)
            End If
            If Obtxt.Text = "" Then
            Else
                IdOb = obj.CodigoObservaciones
            End If

            'Inserta datos 
            Dim nres As Integer
            nres = obj.GrabarCostos(Idcostos, CostoMP.Text, CostoAbogado.Text, CostoOficial.Text, 0, CostoTotal.Text)
            nres = obj.GrabarDatosV(IdDatosVehiculo, UCase(PlacasTXT.Text.ToUpper), UCase(SerieTXT.Text.ToUpper), ColorId)
            nres = obj.GrabarGrua(IdGrua, UCase(GruaTXT.Text.ToUpper), UCase(OperadorTXT.Text.ToUpper))
            nres = obj.GrabarInventario(IdInventario, UCase(InventarioTXT.Text.ToUpper), UCase(ExpedienteTXT.Text.ToUpper), UCase(SiniestroTXT.Text.ToUpper), UCase(FolioAsTXT.Text.ToUpper))
            If Obtxt.Text = "" Then

            Else
                nres = obj.Grabarobservaciones(IdOb, Obtxt.Text.ToUpper)
            End If

            nres = obj.GrabarEntrada(idEntrada, Fecha.Text, Date.Today.ToShortDateString, Idcorralon, idvehiculo, IdDatosVehiculo, idmotivo, _
                                             idautoridad, IdInventario, IdEmpresa, IdGrua, idEncargado, IdAseguradora, Idcostos, IdOb)

            MessageBox.Show("Entrada registrada exitosamente.")
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
            CorralonTXT.DataSource = tbl
            CorralonTXT.DisplayMember = "NombreCorralon"
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

    Public Function activaraseguradora()
        'Llamando los sub 

        Buscarcolor()
        BuscarCorralon()
        BuscarMarca()
        Buscarempresa()
        BuscarAutoridad()
        BuscarMotivo()
        Buscarencargado()
        'Condicionante para activar y desactivar los campos de la Aseguradora.

        FolioEntradatxt.Text = ("Folio: " & obj.CodigoEntrada)
        Return True
    End Function
    Public Function seguroChBox()

        If SeguroCHB.Checked = False Then
            AseguradoraCB.DataSource = Nothing
            AseguradoraCB.Enabled = False
            FolioAsTXT.Enabled = False
            SiniestroTXT.Enabled = False
            Label12.Enabled = False
            Label13.Enabled = False
            Label14.Enabled = False
        Else
            Buscaraseguradora()
            AseguradoraCB.Enabled = True
            FolioAsTXT.Enabled = True
            SiniestroTXT.Enabled = True
            Label12.Enabled = True
            Label13.Enabled = True
            Label14.Enabled = True
        End If
        Return True
    End Function

    Private Sub contextmenu_click(ByVal sender As System.Object, ByVal e As ToolStripItemClickedEventArgs)
        Try

            Select Case e.ClickedItem.Text


                Case "Copiar"

                    If dgvEntradas.SelectedRows.Count > 1 Then
                        Dim objData As DataObject = dgvEntradas.GetClipboardContent

                        If objData IsNot Nothing Then
                            Clipboard.SetDataObject(objData)
                        End If
                    ElseIf DgvSalidas.SelectedRows.Count > 1 Then
                        Dim objData As DataObject = DgvSalidas.GetClipboardContent

                        If objData IsNot Nothing Then
                            Clipboard.SetDataObject(objData)
                        End If
                    End If
                    dgvEntradas.ClearSelection()
                    DgvSalidas.ClearSelection()

                Case "Eliminar"
                    Dim pass As String = "prueba"
                    Dim result As Integer = MessageBox.Show("¿Realmente deseas eliminar el siguiente registro?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                    If result = DialogResult.No Then

                    ElseIf result = DialogResult.Yes Then

                        Passwords.ShowDialog()

                        If pass = Passwords.passinput Then
                            EliminarEntrada()
                            MostrarEntradas()
                        Else
                            MessageBox.Show("Contraseña incorrecta el Registro no se ha eliminado", "Contraseña incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If

                    End If

                Case "Editar..."
                    Passwords.ShowDialog()
                    If Passwords.passinput = pass Then

                        corralonCH.Show()
                    Else
                        MsgBox("Contraseña incorrecta, Favor de verificarla.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Constraseña incorrecta")
                    End If


            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try

    End Sub

   
    Private Sub DataGridView1_CellMouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvEntradas.CellMouseDown
        Dim IdEntrada As Integer
        Dim tbl As DataTable
        Try


            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then
                Return

            ElseIf e.RowIndex > 0 Then

                IdEntrada = dgvEntradas.Rows(e.RowIndex).Cells(0).Value
                PublicIdEntrada = IdEntrada
                tbl = objC.EntradasCompletas(IdEntrada)

                If dgvEntradas.SelectedRows.Count > 1 Then
                    CmStrip()
                Else
                    dgvEntradas.ClearSelection()
                    dgvEntradas.Rows(e.RowIndex).Selected = True
                    CmStrip()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub
    Sub CmStrip()
        Dim contextmenu As New ContextMenuStrip
        contextmenu.Items.Add(New ToolStripSeparator())
        contextmenu.Items.Add("Copiar")
        contextmenu.Items.Add(New ToolStripSeparator())
        contextmenu.Items.Add("Eliminar")


        AddHandler contextmenu.ItemClicked, AddressOf contextmenu_click
        For Each rw As DataGridViewRow In dgvEntradas.Rows
            For Each c As DataGridViewCell In rw.Cells
                c.ContextMenuStrip = contextmenu
            Next
        Next
    End Sub
    Sub CmStripSalidas()
        Dim contextmenu As New ContextMenuStrip
        contextmenu.Items.Add(New ToolStripSeparator())
        contextmenu.Items.Add("Copiar")

        AddHandler contextmenu.ItemClicked, AddressOf contextmenu_click
        For Each rw As DataGridViewRow In DgvSalidas.Rows
            For Each c As DataGridViewCell In rw.Cells
                c.ContextMenuStrip = contextmenu
            Next
        Next
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        
        Fecha.Format = DateTimePickerFormat.Custom
        Fecha.CustomFormat = "dd/MM/yyyy"
        Fecha.Value = Date.Now
        activaraseguradora()
        seguroChBox()
        costosIinicio()

        '---------------
        '---------------
        '---------------
        '--------------
        filtrarmeses()
        BuscarBTN.Enabled = False
        MostrarEntradas()
        MostrarSalidas()
        dgvEntradas.ClearSelection()
        DgvSalidas.ClearSelection()
    End Sub

    'conocer el ID del Color 
    Private Sub ColorCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ColorCB.SelectedIndexChanged



    End Sub
    Private Sub MarcaCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MarcaCB.SelectedIndexChanged
        Dim Marca As String = MarcaCB.Text.ToString
        Buscartipo(Marca)
    End Sub

    Private Sub SerieTXT_TextChanged(sender As Object, e As EventArgs) Handles SerieTXT.TextChanged
        If SerieTXT.Text.Length = 0 Then
            SerieTXT.BackColor = Color.White
        ElseIf SerieTXT.Text.Length < 17 Or SerieTXT.Text.Length > 17 Then
            SerieTXT.BackColor = Color.LightPink
        Else
            SerieTXT.BackColor = Color.LightGreen

        End If

    End Sub




    Private Sub CorralonTXT_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CorralonTXT.SelectedIndexChanged

    End Sub

    Private Sub TipoCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TipoCB.SelectedIndexChanged

    End Sub

    Private Sub SeguroCHB_CheckedChanged_1(sender As Object, e As EventArgs) Handles SeguroCHB.CheckedChanged
        seguroChBox()

    End Sub

    Private Sub AutoridadCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles AutoridadCB.SelectedIndexChanged

    End Sub

    Private Sub EmpresaCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles EmpresaCB.SelectedIndexChanged

    End Sub

    Private Sub AseguradoraCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles AseguradoraCB.SelectedIndexChanged

    End Sub

    Private Sub MotivoCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MotivoCB.SelectedIndexChanged
    End Sub
    '------------------------------------------------------------------------------------------------------------------------
    '-----------------------------------------------Pestaña 2 ---------------------------------------------------------------
    '------------------------------------------------------------------------------------------------------------------------
    Sub filtrarmesesAccion()
        Dim tbl As DataTable
        If cbChatarra.Checked = False Then
           MostrarEntradas()
        Else
            Try
                tbl = objC.MostrarEntradasChatarra()
                dgvEntradas.DataSource = tbl
                dgvEntradas.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
                dgvEntradas.DefaultCellStyle.BackColor = Color.LightPink
                dgvEntradas.AlternatingRowsDefaultCellStyle.BackColor = Color.PaleVioletRed
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        If CHBMeses.Checked = False And BuscarTXT.Text = Nothing Then

        ElseIf CHBMeses.Checked = False Then
            Try
                tbl = objC.EncuentraTodo(BuscarTXT.Text)
                dgvEntradas.DataSource = tbl
                dgvEntradas.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
                dgvEntradas.DefaultCellStyle.BackColor = Color.LightGray
                dgvEntradas.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf CHBMeses.Checked = True Then

            If CBMeses.SelectedIndex + 1 <= 9 Then
                Try
                    tbl = objC.EncuentraTodoporMes(BuscarTXT.Text, "0" & CBMeses.SelectedIndex + 1)
                    dgvEntradas.DataSource = tbl
                    dgvEntradas.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
                    dgvEntradas.DefaultCellStyle.BackColor = Color.LightGray
                    dgvEntradas.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            Else
                Try
                    tbl = objC.EncuentraTodoporMes(BuscarTXT.Text, CBMeses.SelectedIndex + 1)
                    dgvEntradas.DataSource = tbl
                    dgvEntradas.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
                    dgvEntradas.DefaultCellStyle.BackColor = Color.LightGray
                    dgvEntradas.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End If

            If BuscarTXT.TextLength = 0 Then
                BuscarBTN.Enabled = False
                BuscarBTN.BackgroundImage = My.Resources.lupa

            Else
                BuscarBTN.BackgroundImage = My.Resources.cross
                BuscarBTN.Enabled = True
            End If
        End If



    End Sub
    Sub filtrarmeses()
        If CHBMeses.Checked = True Then
            CBMeses.Enabled = True
            CBMeses.Items.Clear()
            CBMeses.Items.Add("Enero")
            CBMeses.Items.Add("Febrero")
            CBMeses.Items.Add("Marzo")
            CBMeses.Items.Add("Abril")
            CBMeses.Items.Add("Mayo")
            CBMeses.Items.Add("Junio")
            CBMeses.Items.Add("Julio")
            CBMeses.Items.Add("Agosto")
            CBMeses.Items.Add("Septiembre")
            CBMeses.Items.Add("Octubre")
            CBMeses.Items.Add("Noviembre")
            CBMeses.Items.Add("Diciembre")

        Else
            CBMeses.Enabled = False
            CBMeses.Items.Clear()
            CBMeses.Items.Add("Filtrar por Mes")
            CBMeses.SelectedIndex = 0
        End If


    End Sub
    Sub MostrarEntradas()
       
        Dim tbl As DataTable = objC.MostrarEntradas
        dgvEntradas.DataSource = tbl
        'redimendisonando las celdas
        dgvEntradas.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
        dgvEntradas.DefaultCellStyle.BackColor = Color.LightGray
        dgvEntradas.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow

       
    End Sub

    

    Private Sub dgvEntradas_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEntradas.CellContentDoubleClick
        'Obtiene el valor del Id de Entrada
        Dim IdEntrada As Integer
        Dim tbl As DataTable
        Try
            IdEntrada = dgvEntradas.Rows(e.RowIndex).Cells(0).Value
            PublicIdEntrada = IdEntrada
            tbl = objC.EntradasCompletas(IdEntrada)
            'recuperando los datos de la consulta
            'Vaciando los datos en el Combo Box
            Salidas.Show()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub dgvSalidas_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvSalidas.CellContentDoubleClick
        'Obtiene el valor del Id de Entrada
        Dim IdSalida As String
        Dim tbl As DataTable
        Try
            IdSalida = DgvSalidas.Rows(e.RowIndex).Cells(0).Value
            PublicIdSalida = IdSalida
            tbl = objCS.SalidasCompletas(IdSalida)
            PublicIdEntrada = tbl.Rows(0)(1)
            'recuperando los datos de la consulta
            'Vaciando los datos en el Combo Box
            ReporteSalidas.Show()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub dgvEntradas_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEntradas.CellDoubleClick
      

        
    End Sub

    Private Sub dgvEntradas_RowHeaderMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvEntradas.RowHeaderMouseDoubleClick
        'Obtiene el valor del Id de Entrada
        Dim IdEntrada As Integer
        Dim tbl As DataTable
        Try
            IdEntrada = dgvEntradas.Rows(e.RowIndex).Cells(0).Value
            PublicIdEntrada = IdEntrada
            tbl = objC.EntradasCompletas(IdEntrada)
            'recuperando los datos de la consulta
            'Vaciando los datos en el Combo Box

            Passwords.ShowDialog()
            If Passwords.passinput = pass Then
                corralonCH.Show()
            Else
                MsgBox("Contraseña incorrecta, Favor de verificarla.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Constraseña incorrecta")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dgvEntradas_RowHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvEntradas.RowHeaderMouseClick

    End Sub

    Private Sub BuscarTXT_TextChanged(sender As Object, e As EventArgs) Handles BuscarTXT.TextChanged
        filtrarmesesAccion()

    End Sub

    Private Sub BuscarBTN_Click(sender As Object, e As EventArgs) Handles BuscarBTN.Click
        BuscarTXT.Clear()
        BuscarTXT.Focus()
    End Sub

    Private Sub CHBMeses_CheckedChanged(sender As Object, e As EventArgs) Handles CHBMeses.CheckedChanged
        filtrarmeses()
    End Sub
    Private Sub CBMeses_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBMeses.SelectedIndexChanged

        filtrarmesesAccion()


    End Sub
    Private Sub ImprimirToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ImprimirToolStripMenuItem1.Click
        Try


            PrintDocument1.DefaultPageSettings.Landscape = True
            If Me.PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Me.PrintDocument1.Print()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim i As Integer = 0

        Dim printFont As New Font("Arial", 8)
        Dim topMargin As Single = 40 'e.MarginBounds.Top
        Dim yPos As Single = 0
        Dim xposHead As Single = 0
        Dim linesPerPage As Single = 0
        Dim count As Integer = 0
        Dim texto As String = ""
        Dim header As String = ""
        Dim row As DataGridViewRow
        Dim p As New Pen(Brushes.Black, 2.5F)

        linesPerPage = e.MarginBounds.Height / printFont.GetHeight(e.Graphics)
        Try


            For Each column As DataGridViewColumn In dgvEntradas.Columns

                If column.HeaderText.Length <= 8 Then
                    header += "   " & Microsoft.VisualBasic.Trim(column.HeaderText) & vbTab & "     -     "
                Else
                    header += Microsoft.VisualBasic.Trim(column.HeaderText) & "    -    "
                End If

            Next

            e.Graphics.FillRectangle(Brushes.LightGray, New Rectangle(40, 0, 1200, 25))
            e.Graphics.DrawString(header, dgvEntradas.Font, Brushes.Black, 70, 10)
            i = 0

            While count < linesPerPage AndAlso i < Me.dgvEntradas.Rows.Count
                row = dgvEntradas.Rows(i)
                texto = ""



                For Each celda As DataGridViewCell In row.Cells

                    If Microsoft.VisualBasic.Trim(celda.Value).Length <= 8 Then
                        texto += Microsoft.VisualBasic.Trim(celda.Value) & vbTab & vbTab
                    Else
                        texto += Microsoft.VisualBasic.Trim(celda.Value) & vbTab
                    End If

                    'Recortar unos caracteres si sobrepasan los 10 ..
                Next


                yPos = topMargin + (count * printFont.GetHeight(e.Graphics))
                e.Graphics.DrawString(texto, printFont, Brushes.Black, 80, yPos)
                count += 1
                i += 1

            End While

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        If i < Me.dgvEntradas.Rows.Count Then
            e.HasMorePages = True
        Else
            e.HasMorePages = False
            i = 0
        End If

    End Sub
    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        Try

            PrintDocument2.DefaultPageSettings.Landscape = True
            If Me.PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Me.PrintDocument2.Print()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub PrintDocument2_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument2.PrintPage
        Dim i As Integer = 0

        Dim printFont As New Font("Arial", 8)
        Dim topMargin As Single = 40 'e.MarginBounds.Top
        Dim yPos As Single = 0
        Dim xposHead As Single = 0
        Dim linesPerPage As Single = 0
        Dim count As Integer = 0
        Dim texto As String = ""
        Dim header As String = ""
        Dim row As DataGridViewRow
        Dim p As New Pen(Brushes.Black, 2.5F)

        linesPerPage = e.MarginBounds.Height / printFont.GetHeight(e.Graphics)
        Try


            For Each column As DataGridViewColumn In DgvSalidas.Columns

                If column.HeaderText.Length <= 8 Then
                    header += "   " & Microsoft.VisualBasic.Trim(column.HeaderText) & vbTab & "     -     "
                Else
                    header += Microsoft.VisualBasic.Trim(column.HeaderText) & "    -    "
                End If

            Next

            e.Graphics.FillRectangle(Brushes.LightGray, New Rectangle(40, 0, 1200, 25))
            e.Graphics.DrawString(header, DgvSalidas.Font, Brushes.Black, 70, 10)
            i = 0

            While count < linesPerPage AndAlso i < Me.DgvSalidas.Rows.Count
                row = DgvSalidas.Rows(i)
                texto = ""



                For Each celda As DataGridViewCell In row.Cells

                    If Microsoft.VisualBasic.Trim(celda.Value).Length <= 8 Then
                        texto += Microsoft.VisualBasic.Trim(celda.Value) & vbTab & vbTab
                    Else
                        texto += Microsoft.VisualBasic.Trim(celda.Value) & vbTab
                    End If

                    'Recortar unos caracteres si sobrepasan los 10 ..
                Next


                yPos = topMargin + (count * printFont.GetHeight(e.Graphics))
                e.Graphics.DrawString(texto, printFont, Brushes.Black, 80, yPos)
                count += 1
                i += 1

            End While

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        If i < Me.DgvSalidas.Rows.Count Then
            e.HasMorePages = True
        Else
            e.HasMorePages = False
            i = 0
        End If


    End Sub
   

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        'Insertar datos
        Dim colorC As String = ColorCB.Text
        Dim corralon As String = CorralonTXT.Text
        Dim marca As String = MarcaCB.Text
        Dim tipo As String = TipoCB.Text
        Dim motivo As String = MotivoCB.Text
        Dim autoridad As String = AutoridadCB.Text
        Dim empresa As String = EmpresaCB.Text
        Dim encargado As String = EncargadoCB.Text
        Dim aseguradora As String = AseguradoraCB.Text
        If SerieTXT.Text.Length = 17 Then
            NuevaEntrada(colorC, corralon, marca, tipo, motivo, autoridad, empresa, encargado, aseguradora)
            'Restablecer campos

            PlacasTXT.Clear()
            SerieTXT.Clear()
            GruaTXT.Clear()
            OperadorTXT.Clear()
            SeguroCHB.Checked = False
            Obtxt.Clear()
            FolioAsTXT.Clear()
            SiniestroTXT.Clear()
            InventarioTXT.Clear()
            ExpedienteTXT.Clear()
            activaraseguradora()
            CostosCHB.Checked = False
            MostrarEntradas()
        Else
            MessageBox.Show("Verifica el número de serie")
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
    End Sub

    Private Sub FolioEntradatxt_TextChanged(sender As Object, e As EventArgs) Handles FolioEntradatxt.TextChanged

    End Sub

    '___________________________________________________Salidas_____________________________________________
    '_______________________________________________________________________________________________________
    '_______________________________________________________________________________________________________
    Sub MostrarSalidas()

        Dim tbl As DataTable = objCS.MostrarSalidas
        DgvSalidas.DataSource = tbl


        'redimendisonando las celdas
        dgvEntradas.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
        dgvEntradas.DefaultCellStyle.BackColor = Color.LightGray
        dgvEntradas.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow

    End Sub

    Private Sub cbChatarra_CheckedChanged(sender As Object, e As EventArgs) Handles cbChatarra.CheckedChanged
        If cbChatarra.Checked = True Then
            CHBMeses.Enabled = False
            BuscarTXT.Enabled = False
            filtrarmesesAccion()
        Else
            CHBMeses.Enabled = True
            BuscarTXT.Enabled = True
            filtrarmesesAccion()
        End If
    End Sub




    Private Sub DgvSalidas_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DgvSalidas.CellMouseDown
        Dim IdSalida As String
        Dim tbl As DataTable
        Try


            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then
                Return

            ElseIf e.RowIndex > 0 Then

                IdSalida = DgvSalidas.Rows(e.RowIndex).Cells(0).Value
                PublicIdSalida = IdSalida
                tbl = objCS.SalidasCompletas(IdSalida)

                If DgvSalidas.SelectedRows.Count > 1 Then
                    CmStripSalidas()
                Else
                    DgvSalidas.ClearSelection()
                    DgvSalidas.Rows(e.RowIndex).Selected = True
                    CmStripSalidas()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub NewVehiculoBTN_Click(sender As Object, e As EventArgs) Handles NewVehiculoBTN.Click
        Passwords.ShowDialog()
        If Passwords.passinput = passcapturista Then
            NuevoVehiculo.ShowDialog()
        Else
            MsgBox("Contraseña incorrecta, Favor de verificarla.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Constraseña incorrecta")
        End If



    End Sub

    Private Sub NewCorralonBTN_Click(sender As Object, e As EventArgs) Handles NewCorralonBTN.Click
        Passwords.ShowDialog()
        If Passwords.passinput = passcapturista Then
            NuevoDB.caso = 1
            NuevoDB.ShowDialog()
        Else
            MsgBox("Contraseña incorrecta, Favor de verificarla.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Constraseña incorrecta")
        End If
        
    End Sub

    Private Sub NewAutoridadBTN_Click(sender As Object, e As EventArgs) Handles NewAutoridadBTN.Click
        Passwords.ShowDialog()
        If Passwords.passinput = passcapturista Then
            NuevoDB.caso = 2
            NuevoDB.ShowDialog()
        Else
            MsgBox("Contraseña incorrecta, Favor de verificarla.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Constraseña incorrecta")
        End If
    End Sub

    Private Sub NewEmpresaBTN_Click(sender As Object, e As EventArgs) Handles NewEmpresaBTN.Click
        Passwords.ShowDialog()
        If Passwords.passinput = passcapturista Then
            NuevoDB.caso = 3
            NuevoDB.ShowDialog()
        Else
            MsgBox("Contraseña incorrecta, Favor de verificarla.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Constraseña incorrecta")
        End If
    End Sub

    Private Sub NewAseguradora_Click(sender As Object, e As EventArgs) Handles NewAseguradora.Click
        Passwords.ShowDialog()
        If Passwords.passinput = passcapturista Then
            NuevoDB.caso = 4
            NuevoDB.ShowDialog()
        Else
            MsgBox("Contraseña incorrecta, Favor de verificarla.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Constraseña incorrecta")
        End If
    End Sub

    Private Sub NewMotivoBTN_Click(sender As Object, e As EventArgs) Handles NewMotivoBTN.Click
        Passwords.ShowDialog()
        If Passwords.passinput = passcapturista Then
            NuevoDB.caso = 5
            NuevoDB.ShowDialog()
        Else
            MsgBox("Contraseña incorrecta, Favor de verificarla.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Constraseña incorrecta")
        End If
    End Sub

    Private Sub NewEncargadoBTN_Click(sender As Object, e As EventArgs) Handles NewEncargadoBTN.Click
        Passwords.ShowDialog()
        If Passwords.passinput = passcapturista Then
            NuevoDB.caso = 6
            NuevoDB.ShowDialog()
        Else
            MsgBox("Contraseña incorrecta, Favor de verificarla.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Constraseña incorrecta")
        End If
    End Sub

    Private Sub NewColorBTN_Click(sender As Object, e As EventArgs) Handles NewColorBTN.Click
        Passwords.ShowDialog()
        If Passwords.passinput = passcapturista Then
            NuevoDB.caso = 7
            NuevoDB.ShowDialog()
        Else
            MsgBox("Contraseña incorrecta, Favor de verificarla.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Constraseña incorrecta")
        End If
    End Sub

    Private Sub CrearBkpDbBTN_Click(sender As Object, e As EventArgs) Handles CrearBkpDbBTN.Click
        Passwords.ShowDialog()
        If Passwords.passinput = pass Then
            Try
                obj.CrearRespaldo()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Else
            MsgBox("Contraseña incorrecta, Favor de verificarla.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Constraseña incorrecta")
        End If

    End Sub

    
    Private Sub MsSalidas_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MsSalidas.ItemClicked

    End Sub
End Class
