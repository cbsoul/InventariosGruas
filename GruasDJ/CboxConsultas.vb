Imports System.Data.SqlClient
Imports System.IO.FileInfo
Public Class CboxConsultas

    Dim cn As New SqlConnection(cadenasql)
    Public Function CrearRespaldo()
        Dim Path As String, Titulo As String
        Titulo = "Selecciona por favor una carpeta"
        On Error Resume Next 'por si el usuario pulsa {esc} y no selecciona nada
        With CreateObject("shell.application")
            Path = .BrowseForFolder(0, Titulo, 0).Items.Item.Path
        End With : On Error GoTo 0
        If Path = "" Then
            MsgBox("No se ha seleccionado ningun directorio.", , "Operacion cancelada !!!")
        Else
            Dim NombreBKP As String = (Date.Today.Day.ToString & "-" & Date.Today.Month.ToString & "-" & Date.Today.Year.ToString & "-" & Date.Now.Hour.ToString & "-" & Date.Now.Minute.ToString & "-" & Date.Now.Second.ToString & "-(Respaldo DB).bak")
            Dim QueryBKP As String = "BACKUP DATABASE [InventariosDJ] TO  DISK = N'" & Path & "\" & NombreBKP & " ' WITH NOFORMAT, NOINIT,  NAME = N'InventariosDJ-Completa Base de datos Copia de seguridad', SKIP, NOREWIND, NOUNLOAD,  STATS = 10"
            Dim cmd As SqlCommand = New SqlCommand(QueryBKP, cn)
            cn.Open()
            Dim nres As Integer = cmd.ExecuteNonQuery()
            MsgBox("La copia de seguridad se ha realizado correctamente", MsgBoxStyle.OkOnly, "Exitosa")
            Return nres
        End If
        cn.Close()
    End Function

    Public Function BusTpago() As DataTable
        Dim cmd As New SqlCommand("select Fpago from Tpago", cn)
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim tbl As New DataTable
        tbl.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection))
        cn.Close()
        Return (tbl)
    End Function
    Public Function BusEncargados() As DataTable
        Dim cmd As New SqlCommand("select NomEncargado from Encargado", cn)
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim tbl As New DataTable
        tbl.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection))
        Return (tbl)
    End Function
    Public Function BusCorralones() As DataTable
        Dim cmd As New SqlCommand("select nombrecorralon from corralon", cn)
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim tbl As New DataTable
        tbl.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection))
        Return (tbl)
    End Function

    Public Function BusMarca() As DataTable
        Dim cmd As New SqlCommand("select marca from vehiculos group by marca", cn)
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim tbl As New DataTable
        tbl.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection))
        Return (tbl)
    End Function
    Public Function Bustipo(ByVal Marca As String) As DataTable
        Dim cmd As New SqlCommand("usp_tipo", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@marca", SqlDbType.Char, 20).Value = Marca
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim tbl As New DataTable
        tbl.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection))
        Return (tbl)
    End Function

    Public Function Buscolor() As DataTable
        Dim cmd As New SqlCommand("select color from colores", cn)
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim tbl As New DataTable
        tbl.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection))
        Return (tbl)
    End Function

    Public Function BusEmpresa() As DataTable
        Dim cmd As New SqlCommand("select NombreEmp from empresa", cn)
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim tbl As New DataTable
        tbl.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection))
        Return (tbl)
    End Function
    Public Function BusAseguradora() As DataTable
        Dim cmd As New SqlCommand("select NomAseguradora from Aseguradora", cn)
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim tbl As New DataTable
        tbl.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection))
        Return (tbl)
    End Function

    Public Function BusAutoridad() As DataTable
        Dim cmd As New SqlCommand("select NomAutoridad from autoridad", cn)
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim tbl As New DataTable
        tbl.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection))
        Return (tbl)
    End Function

    Public Function BusMotivo() As DataTable
        Dim cmd As New SqlCommand("select motivo from motivos", cn)
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim tbl As New DataTable
        tbl.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection))
        Return (tbl)
    End Function
    'Codigo Costos
    Public Function CodigoCostos() As String
        Dim cmd As New SqlCommand("usp_generacodigoCostos", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@idcostos", SqlDbType.Int).Direction = ParameterDirection.Output
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        cmd.ExecuteNonQuery()
        cn.Close()
        Return cmd.Parameters("@idcostos").Value
    End Function
    
    Public Function GrabarCostos(ByVal IdCosto As Integer _
, ByVal Autoridad As String, ByVal Abogado As String, ByVal Policia As String, ByVal Ingresos As String, ByVal Total As String) As Integer
        Dim cmd As New SqlCommand("usp_insertcosto", cn)
        cmd.CommandType = CommandType.StoredProcedure
        'definiendo los parámetros de entrada
        cmd.Parameters.Add("@IdCosto", SqlDbType.Int).Value = IdCosto
        cmd.Parameters.Add("@Autoridad", SqlDbType.VarChar, 20).Value = Autoridad
        cmd.Parameters.Add("@Abogado", SqlDbType.VarChar, 20).Value = Abogado
        cmd.Parameters.Add("@policia", SqlDbType.VarChar, 20).Value = Policia
        cmd.Parameters.Add("@Ingresos", SqlDbType.VarChar, 20).Value = Ingresos
        cmd.Parameters.Add("@totalCostos", SqlDbType.VarChar, 30).Value = Total
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim nresp As Integer = cmd.ExecuteNonQuery()
        cn.Close()
        Return nresp
    End Function
    Public Function GrabarIngresos(ByVal IdCosto As Integer _
, ByVal ingresos As String, ByVal Total As String) As Integer
        Dim cmd As New SqlCommand("usp_insertcosto", cn)
        cmd.CommandType = CommandType.StoredProcedure
        'definiendo los parámetros de entrada
        cmd.Parameters.Add("@IdCosto", SqlDbType.Int).Value = IdCosto
        cmd.Parameters.Add("@ingresos", SqlDbType.VarChar, 20).Value = ingresos
        cmd.Parameters.Add("@TotalIngreso", SqlDbType.VarChar, 20).Value = Total

        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim nresp As Integer = cmd.ExecuteNonQuery()
        cn.Close()
        Return nresp
    End Function
    'Genera N° de los Datos del vehículo
    Public Function CodigoDatosVehiculo() As String
        Dim cmd As New SqlCommand("usp_generacodigoDatosVehiculos", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@IdDatosV", SqlDbType.Int).Direction = ParameterDirection.Output
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        cmd.ExecuteNonQuery()
        cn.Close()
        Return cmd.Parameters("@IdDatosV").Value
    End Function
    Public Function ConocerIdColor(ByVal ColorC As String) As String
        Dim cmd As New SqlCommand("usp_ConocerIdColor", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@Color", SqlDbType.Char, 15).Value = ColorC
        cmd.Parameters.Add("@IdColor", SqlDbType.Int).Direction = ParameterDirection.Output
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        cmd.ExecuteNonQuery()
        cn.Close()
        Return cmd.Parameters("@IdColor").Value.ToString
    End Function
   

    'Graba datos de los Datos del vehículo 
    Public Function GrabarDatosV(ByVal IdDatosV As Integer _
, ByVal Placas As String, ByVal serie As String, ByVal IdColor As Integer) As Integer
        Dim cmd As New SqlCommand("usp_insertDatosV", cn)
        cmd.CommandType = CommandType.StoredProcedure
        'definiendo los parámetros de entrada
        cmd.Parameters.Add("@IdDatosV", SqlDbType.Int).Value = IdDatosV
        cmd.Parameters.Add("@Placas", SqlDbType.Char, 10).Value = Placas
        cmd.Parameters.Add("@Serie", SqlDbType.Char, 17).Value = serie
        cmd.Parameters.Add("@IdColor", SqlDbType.Int).Value = IdColor
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim nresp As Integer = cmd.ExecuteNonQuery()
        cn.Close()
        Return nresp
    End Function
    Public Function CodigoObservaciones() As String
        Dim cmd As New SqlCommand("usp_generacodigoObservaciones", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@Observaciones", SqlDbType.Int).Direction = ParameterDirection.Output
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        cmd.ExecuteNonQuery()
        cn.Close()
        Return cmd.Parameters("@Observaciones").Value
    End Function
    Public Function CodigoObservacionesSalidas() As String
        Dim cmd As New SqlCommand("usp_generacodigoObservacionesSalidas", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@Observaciones", SqlDbType.Int).Direction = ParameterDirection.Output
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        cmd.ExecuteNonQuery()
        cn.Close()
        Return cmd.Parameters("@Observaciones").Value
    End Function
    Public Function Grabarobservaciones(ByVal IdOb As Integer _
, ByVal Observaciones As String) As Integer
        Dim cmd As New SqlCommand("usp_insertObservaciones", cn)
        cmd.CommandType = CommandType.StoredProcedure
        'definiendo los parámetros de entrada
        cmd.Parameters.Add("@IdOB", SqlDbType.Int).Value = IdOb
        cmd.Parameters.Add("@Observaciones", SqlDbType.VarChar, 8000).Value = Observaciones
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim nresp As Integer = cmd.ExecuteNonQuery()
        cn.Close()
        Return nresp
    End Function
    Public Function GrabarobservacionesSalidas(ByVal IdOb As Integer _
, ByVal Observaciones As String) As Integer
        Dim cmd As New SqlCommand("usp_insertObservacionesSalidas", cn)
        cmd.CommandType = CommandType.StoredProcedure
        'definiendo los parámetros de entrada
        cmd.Parameters.Add("@IdOB", SqlDbType.Int).Value = IdOb
        cmd.Parameters.Add("@Observaciones", SqlDbType.VarChar, 8000).Value = Observaciones
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim nresp As Integer = cmd.ExecuteNonQuery()
        cn.Close()
        Return nresp
    End Function
    Public Function CodigoGruas() As String
        Dim cmd As New SqlCommand("usp_generacodigoGruas", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@IdGrua", SqlDbType.Int).Direction = ParameterDirection.Output
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        cmd.ExecuteNonQuery()
        cn.Close()
        Return cmd.Parameters("@IdGrua").Value
    End Function
    Public Function GrabarGrua(ByVal IdGrua As Integer _
, ByVal Tipo As String, ByVal Operador As String) As Integer
        Dim cmd As New SqlCommand("usp_insertGrua", cn)
        cmd.CommandType = CommandType.StoredProcedure
        'definiendo los parámetros de entrada
        cmd.Parameters.Add("@IdGrua", SqlDbType.Int).Value = IdGrua
        cmd.Parameters.Add("@Tipo", SqlDbType.Char, 20).Value = Tipo
        cmd.Parameters.Add("@Operador", SqlDbType.Char, 30).Value = Operador
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim nresp As Integer = cmd.ExecuteNonQuery()
        cn.Close()
        Return nresp
    End Function

    Public Function GrabarInventario(ByVal IdInventario As String _
, ByVal NoInventario As String, ByVal NoExpediente As String, ByVal NoSiniestro As String, _
 ByVal NoFolio As String) As Integer
        Dim cmd As New SqlCommand("usp_insertInventario", cn)
        cmd.CommandType = CommandType.StoredProcedure
        'definiendo los parámetros de entrada
        cmd.Parameters.Add("@IdInventario", SqlDbType.Char, 20).Value = IdInventario
        cmd.Parameters.Add("@NoInventario", SqlDbType.Char, 20).Value = NoInventario
        cmd.Parameters.Add("@NoExpediente", SqlDbType.Char, 20).Value = NoExpediente
        cmd.Parameters.Add("@NoSiniestro", SqlDbType.Char, 20).Value = NoSiniestro
        cmd.Parameters.Add("@NoFolio", SqlDbType.Char, 20).Value = NoFolio
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim nresp As Integer = cmd.ExecuteNonQuery()
        cn.Close()
        Return nresp
    End Function
    Public Function CodigoInventario() As String
        Dim cmd As New SqlCommand("usp_generacodigoInventario", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@IdInventario", SqlDbType.Int).Direction = ParameterDirection.Output
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        cmd.ExecuteNonQuery()
        cn.Close()
        Return cmd.Parameters("@IdInventario").Value
    End Function

    Public Function CodigoEntrada() As String
        Dim cmd As New SqlCommand("usp_generacodigoEntradas", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@IdEntrada", SqlDbType.Int).Direction = ParameterDirection.Output
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        cmd.ExecuteNonQuery()
        cn.Close()
        Return cmd.Parameters("@IdEntrada").Value
    End Function
    Public Function CodigoSalida() As String
        Dim cmd As New SqlCommand("usp_generacodigoSalidas", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@IdSalida", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        cmd.ExecuteNonQuery()
        cn.Close()
        Return cmd.Parameters("@IdSalida").Value
    End Function

    'Conocer ID's
    Public Function ConocerIdCorralon(ByVal Corralon As String) As String
        Dim cmd As New SqlCommand("usp_ConocerIdCorralon", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@NombreCorralon", SqlDbType.Char, 15).Value = Corralon
        cmd.Parameters.Add("@IdCorralon", SqlDbType.Int).Direction = ParameterDirection.Output
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        cmd.ExecuteNonQuery()
        cn.Close()
        Return cmd.Parameters("@IdCorralon").Value.ToString
    End Function

    Public Function ConocerIdVehiculo(ByVal marca As String, ByVal tipo As String) As String
        Dim cmd As New SqlCommand("usp_ConocerIdVehiculo", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@Marca", SqlDbType.Char, 15).Value = marca
        cmd.Parameters.Add("@tipo ", SqlDbType.Char, 15).Value = tipo
        cmd.Parameters.Add("@IdVehiculo", SqlDbType.Int).Direction = ParameterDirection.Output
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        cmd.ExecuteNonQuery()
        cn.Close()
        Return cmd.Parameters("@IdVehiculo").Value.ToString
    End Function

    Public Function ConocerIdMotivo(ByVal Motivo As String) As String
        Dim cmd As New SqlCommand("usp_ConocerIdmotivo", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@Motivo", SqlDbType.Char, 20).Value = Motivo
        cmd.Parameters.Add("@IdMotivo", SqlDbType.Int).Direction = ParameterDirection.Output
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        cmd.ExecuteNonQuery()
        cn.Close()
        Return cmd.Parameters("@IdMotivo").Value.ToString
    End Function

    Public Function ConocerIdAutoridad(ByVal autoridad As String) As String
        Dim cmd As New SqlCommand("usp_ConocerIdAutoridad", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@NomAutoridad", SqlDbType.Char, 20).Value = autoridad
        cmd.Parameters.Add("@IdAutoridad", SqlDbType.Int).Direction = ParameterDirection.Output
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        cmd.ExecuteNonQuery()
        cn.Close()
        Return cmd.Parameters("@IdAutoridad").Value.ToString
    End Function


    Public Function ConocerIdEmpresa(ByVal empresa As String) As String
        Dim cmd As New SqlCommand("usp_ConocerIdEmpresa", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@NombreEmp", SqlDbType.Char, 20).Value = empresa
        cmd.Parameters.Add("@IdEmpresa", SqlDbType.Int).Direction = ParameterDirection.Output
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        cmd.ExecuteNonQuery()
        cn.Close()
        Return cmd.Parameters("@IdEmpresa").Value.ToString
    End Function
    Public Function ConocerIdEncargado(ByVal Encargado As String) As String
        Dim cmd As New SqlCommand("usp_ConocerIdEncargado", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@NomEncargado", SqlDbType.Char, 20).Value = Encargado
        cmd.Parameters.Add("@IdEncargado", SqlDbType.Int).Direction = ParameterDirection.Output
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        cmd.ExecuteNonQuery()
        cn.Close()
        Return cmd.Parameters("@IdEncargado").Value.ToString
    End Function

    Public Function ConocerIdaseguradora(ByVal aseguradora As String) As String
        Dim cmd As New SqlCommand("usp_ConocerIdAseguradora", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@NomAseguradora", SqlDbType.Char, 20).Value = aseguradora
        cmd.Parameters.Add("@IdAseguradora", SqlDbType.Int).Direction = ParameterDirection.Output
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        cmd.ExecuteNonQuery()
        cn.Close()
        Return cmd.Parameters("@IdAseguradora").Value.ToString
    End Function
    Public Function ConocerIdTpago(ByVal Fpago As String) As String
        Dim cmd As New SqlCommand("usp_ConocerIdTpago", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@Fpago", SqlDbType.VarChar, 20).Value = Fpago
        cmd.Parameters.Add("@IdTpago", SqlDbType.Int).Direction = ParameterDirection.Output
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        cmd.ExecuteNonQuery()
        cn.Close()
        Return cmd.Parameters("@IdTpago").Value
    End Function
    Public Function GrabarEntrada(ByVal IdEntrada As Integer _
, ByVal Fecha As String, ByVal FechaCap As String, ByVal IdCorralon As Integer, ByVal IdVehiculo As Integer, _
 ByVal IdDatosv As Integer, ByVal IdMotivo As Integer, ByVal IdAutoridad As Integer, _
 ByVal IdInventario As Integer, ByVal IdEmpresa As Integer, ByVal IdGrua As Integer, _
 ByVal IdEncargado As Integer, ByVal IdAseguradora As Integer, ByVal IdCostos As Integer, ByVal IdOB As Integer) As Integer
        Dim cmd As New SqlCommand("usp_insertEntradas", cn)
        cmd.CommandType = CommandType.StoredProcedure
        'definiendo los parámetros de entrada
        cmd.Parameters.Add("@IdEntrada", SqlDbType.Int).Value = IdEntrada
        cmd.Parameters.Add("@Fecha", SqlDbType.Char).Value = Fecha
        cmd.Parameters.Add("@FechaCap", SqlDbType.Char).Value = FechaCap
        cmd.Parameters.Add("@IdCorralon", SqlDbType.Int).Value = IdCorralon
        cmd.Parameters.Add("@IdVehiculo", SqlDbType.Int).Value = IdVehiculo
        cmd.Parameters.Add("@IdDatosV", SqlDbType.Int).Value = IdDatosv
        cmd.Parameters.Add("@IdMotivo", SqlDbType.Int).Value = IdMotivo
        cmd.Parameters.Add("@IdAutoridad", SqlDbType.Int).Value = IdAutoridad
        cmd.Parameters.Add("@Idinventario", SqlDbType.Int).Value = IdInventario
        cmd.Parameters.Add("@IdEmpresa", SqlDbType.Int).Value = IdEmpresa
        cmd.Parameters.Add("@IdGrua", SqlDbType.Int).Value = IdGrua
        cmd.Parameters.Add("@IdEncargado", SqlDbType.Int).Value = IdEncargado
        cmd.Parameters.Add("@IdAseguradora", SqlDbType.Int).Value = IdAseguradora
        cmd.Parameters.Add("@IdCostos", SqlDbType.Int).Value = IdCostos
        cmd.Parameters.Add("@IdOB", SqlDbType.Int).Value = IdOB
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim nresp As Integer = cmd.ExecuteNonQuery()
        cn.Close()
        Return nresp
    End Function

    Public Function GrabarSalida(ByVal IdSalida As String, ByVal IdEntrada As Integer _
, ByVal Fecha As String, ByVal IdCorralon As Integer, ByVal IdVehiculo As Integer, _
 ByVal IdDatosv As Integer, ByVal IdMotivo As Integer, ByVal IdAutoridad As Integer, _
 ByVal IdInventario As Integer, ByVal IdEmpresa As Integer, ByVal IdGrua As Integer, _
 ByVal IdEncargado As Integer, ByVal IdAseguradora As Integer, ByVal IdCostos As Integer, _
 ByVal IdTpago As Integer, ByVal IdFactura As Integer, ByVal IdOB As Integer, ByVal chatarra As Integer) As Integer
        Dim cmd As New SqlCommand("usp_insertSalidas", cn)
        cmd.CommandType = CommandType.StoredProcedure
        'definiendo los parámetros de salida
        cmd.Parameters.Add("@IdSalida", SqlDbType.VarChar, 10).Value = IdSalida
        cmd.Parameters.Add("@IdEntrada", SqlDbType.Int).Value = IdEntrada
        cmd.Parameters.Add("@Fecha", SqlDbType.Char).Value = Fecha
        cmd.Parameters.Add("@IdCorralon", SqlDbType.Int).Value = IdCorralon
        cmd.Parameters.Add("@IdVehiculo", SqlDbType.Int).Value = IdVehiculo
        cmd.Parameters.Add("@IdDatosV", SqlDbType.Int).Value = IdDatosv
        cmd.Parameters.Add("@IdMotivo", SqlDbType.Int).Value = IdMotivo
        cmd.Parameters.Add("@IdAutoridad", SqlDbType.Int).Value = IdAutoridad
        cmd.Parameters.Add("@Idinventario", SqlDbType.Int).Value = IdInventario
        cmd.Parameters.Add("@IdEmpresa", SqlDbType.Int).Value = IdEmpresa
        cmd.Parameters.Add("@IdGrua", SqlDbType.Int).Value = IdGrua
        cmd.Parameters.Add("@IdEncargado", SqlDbType.Int).Value = IdEncargado
        cmd.Parameters.Add("@IdAseguradora", SqlDbType.Int).Value = IdAseguradora
        cmd.Parameters.Add("@IdCostos", SqlDbType.Int).Value = IdCostos
        cmd.Parameters.Add("@Idtpago", SqlDbType.Int).Value = IdTpago
        cmd.Parameters.Add("@IdFactura", SqlDbType.Int).Value = IdFactura
        cmd.Parameters.Add("@IdOB", SqlDbType.Int).Value = IdOB
        cmd.Parameters.Add("@Chatarra", SqlDbType.Bit).Value = chatarra
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim nresp As Integer = cmd.ExecuteNonQuery()
        cn.Close()
        Return nresp
    End Function


End Class



