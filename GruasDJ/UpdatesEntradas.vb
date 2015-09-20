Imports System.Data.SqlClient
Public Class UpdatesEntradas
    Dim cn As New SqlConnection(cadenasql)
    Public Function ConocerIdobservaciones(ByVal IdEntrada As Integer) As String
        Dim cmd As New SqlCommand("usp_ConocerIdOBfromEntradas", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@IdEntrada", SqlDbType.Int).Value = IdEntrada
        cmd.Parameters.Add("@IdOb", SqlDbType.Int).Direction = ParameterDirection.Output
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        cmd.ExecuteNonQuery()
        cn.Close()
        Return cmd.Parameters("@IdOb").Value.ToString
    End Function

    Public Function UpdateObservaciones(ByVal idob As Integer, ByVal Observaciones As String) As String
        Dim cmd As New SqlCommand("usp_updateObservaciones", cn)
        cmd.CommandType = CommandType.StoredProcedure
        'definiendo los parámetros de entrada
        cmd.Parameters.Add("@IdOB", SqlDbType.Int).Value = idob
        cmd.Parameters.Add("@observaciones", SqlDbType.VarChar, 8000).Value = Observaciones
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim nresp As Integer = cmd.ExecuteNonQuery()
        cn.Close()
        Return nresp
    End Function
    Public Function UpdateEntradaEliminar(ByVal IdEntrada As Integer, ByVal IdOb As String) As String
        Dim cmd As New SqlCommand("usp_updateEntradasEliminar", cn)
        cmd.CommandType = CommandType.StoredProcedure
        'definiendo los parámetros de entrada
        cmd.Parameters.Add("@IdEntrada", SqlDbType.Int).Value = IdEntrada
        cmd.Parameters.Add("@idOb", SqlDbType.Int).Value = IdOb
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim nresp As Integer = cmd.ExecuteNonQuery()
        cn.Close()
        Return nresp
    End Function
    Public Function UpdateES(ByVal IdEntrada As Integer, ByVal idSalida As String) As String
        Dim cmd As New SqlCommand("usp_updateES", cn)
        cmd.CommandType = CommandType.StoredProcedure
        'definiendo los parámetros de entrada
        cmd.Parameters.Add("@IdEntrada", SqlDbType.Int).Value = IdEntrada
        cmd.Parameters.Add("@idsalida", SqlDbType.VarChar, 10).Value = idSalida
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim nresp As Integer = cmd.ExecuteNonQuery()
        cn.Close()
        Return nresp
    End Function
    Public Function UpdateEntrada(ByVal Identrada As Integer, ByVal fecha As Date, ByVal IdCorralon As Integer, ByVal IdVehiculo As Integer, _
 ByVal IdMotivo As Integer, ByVal IdAutoridad As Integer, _
 ByVal IdEmpresa As Integer, ByVal IdEncargado As Integer, ByVal IdAseguradora As Integer, _
 ByVal IdOB As Integer) As Integer
        Dim cmd As New SqlCommand("usp_updateEntradas", cn)
        cmd.CommandType = CommandType.StoredProcedure
        'definiendo los parámetros de entrada
        cmd.Parameters.Add("@Identrada", SqlDbType.Int).Value = Identrada
        cmd.Parameters.Add("@fecha", SqlDbType.Date).Value = fecha
        cmd.Parameters.Add("@IdCorralon", SqlDbType.Int).Value = IdCorralon
        cmd.Parameters.Add("@IdVehiculo", SqlDbType.Int).Value = IdVehiculo
        cmd.Parameters.Add("@IdMotivo", SqlDbType.Int).Value = IdMotivo
        cmd.Parameters.Add("@IdAutoridad", SqlDbType.Int).Value = IdAutoridad
        cmd.Parameters.Add("@IdEmpresa", SqlDbType.Int).Value = IdEmpresa
        cmd.Parameters.Add("@IdEncargado", SqlDbType.Int).Value = IdEncargado
        cmd.Parameters.Add("@IdAseguradora", SqlDbType.Int).Value = IdAseguradora
        cmd.Parameters.Add("@IdOB", SqlDbType.Int).Value = IdOB
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim nresp As Integer = cmd.ExecuteNonQuery()
        cn.Close()
        Return nresp
    End Function

    Public Function UpdateDatosV(ByVal IdEntrada As Integer, ByVal placas As String, ByVal Serie As String, ByVal idColor As Integer) As Integer
        Dim cmd As New SqlCommand("usp_updateDatosV", cn)
        cmd.CommandType = CommandType.StoredProcedure
        'definiendo los parámetros de entrada
        cmd.Parameters.Add("@IdDatosV", SqlDbType.Int).Value = IdEntrada
        cmd.Parameters.Add("@placas", SqlDbType.Char, 10).Value = placas
        cmd.Parameters.Add("@serie", SqlDbType.Char, 17).Value = Serie
        cmd.Parameters.Add("@Idcolor", SqlDbType.Int).Value = idColor
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim nresp As Integer = cmd.ExecuteNonQuery()
        cn.Close()
        Return nresp
    End Function

    Public Function Updategrua(ByVal IdEntrada As Integer, ByVal NoGrua As String, ByVal Operador As String) As Integer
        Dim cmd As New SqlCommand("usp_updateGrua", cn)
        cmd.CommandType = CommandType.StoredProcedure
        'definiendo los parámetros de entrada
        cmd.Parameters.Add("@IdGrua", SqlDbType.Int).Value = IdEntrada
        cmd.Parameters.Add("@NoGrua", SqlDbType.Char, 20).Value = NoGrua
        cmd.Parameters.Add("@Operador", SqlDbType.Char, 30).Value = Operador
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim nresp As Integer = cmd.ExecuteNonQuery()
        cn.Close()
        Return nresp
    End Function
    Public Function UpdateCostos(ByVal IdCosto As Integer _
, ByVal Autoridad As String, ByVal Abogado As String, ByVal Policia As String, ByVal Ingresos As String, ByVal Total As String) As Integer
        Dim cmd As New SqlCommand("usp_updateCostos", cn)
        cmd.CommandType = CommandType.StoredProcedure
        'definiendo los parámetros de entrada
        cmd.Parameters.Add("@IdCosto", SqlDbType.Int).Value = IdCosto
        cmd.Parameters.Add("@Autoridad", SqlDbType.VarChar, 20).Value = Autoridad
        cmd.Parameters.Add("@Abogado", SqlDbType.VarChar, 20).Value = Abogado
        cmd.Parameters.Add("@policia", SqlDbType.VarChar, 20).Value = Policia
        cmd.Parameters.Add("@Ingresos", SqlDbType.VarChar, 20).Value = Ingresos
        cmd.Parameters.Add("@total", SqlDbType.VarChar, 30).Value = Total
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim nresp As Integer = cmd.ExecuteNonQuery()
        cn.Close()
        Return nresp
    End Function
    Public Function UpdateInventario(ByVal IdEntrada As Integer, ByVal NoInventario As String, ByVal NoExpediente As String, _
                               ByVal NoSiniestro As String, ByVal NoFolio As String) As Integer
        Dim cmd As New SqlCommand("usp_updateInventario", cn)
        cmd.CommandType = CommandType.StoredProcedure
        'definiendo los parámetros de entrada
        cmd.Parameters.Add("@IdInventario", SqlDbType.Int).Value = IdEntrada
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
End Class
