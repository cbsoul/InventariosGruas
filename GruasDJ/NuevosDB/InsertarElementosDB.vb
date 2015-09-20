Imports System.Data.SqlClient
Public Class InsertarElementosDB
    Dim cn As New SqlConnection(cadenasql)
    Public Function InsertarVehiculosDB(ByVal Marca As String, ByVal Tipo As String) As Integer
        Dim cmd As New SqlCommand("usp_insertVehiculo", cn)
        cmd.CommandType = CommandType.StoredProcedure
        'definiendo los parámetros de entrada
        cmd.Parameters.Add("@Marca", SqlDbType.VarChar, 20).Value = Marca
        cmd.Parameters.Add("@Tipo", SqlDbType.VarChar, 20).Value = Tipo
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim nresp As Integer = cmd.ExecuteNonQuery()
        cn.Close()
        Return nresp
    End Function

    Public Function InsertarCorralonDB(ByVal corralon As String) As Integer
        Dim cmd As New SqlCommand("usp_insertCorralon", cn)
        cmd.CommandType = CommandType.StoredProcedure
        'definiendo los parámetros de entrada
        cmd.Parameters.Add("@corralon", SqlDbType.VarChar, 20).Value = corralon
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim nresp As Integer = cmd.ExecuteNonQuery()
        cn.Close()
        Return nresp
    End Function
    Public Function InsertarAutoridadDB(ByVal valor As String) As Integer
        Dim cmd As New SqlCommand("usp_insertAutoridad", cn)
        cmd.CommandType = CommandType.StoredProcedure
        'definiendo los parámetros de entrada
        cmd.Parameters.Add("@Valor", SqlDbType.VarChar, 20).Value = valor
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim nresp As Integer = cmd.ExecuteNonQuery()
        cn.Close()
        Return nresp
    End Function
    Public Function InsertarEmpresaDB(ByVal valor As String) As Integer
        Dim cmd As New SqlCommand("usp_insertEmpresa", cn)
        cmd.CommandType = CommandType.StoredProcedure
        'definiendo los parámetros de entrada
        cmd.Parameters.Add("@Valor", SqlDbType.VarChar, 20).Value = valor
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim nresp As Integer = cmd.ExecuteNonQuery()
        cn.Close()
        Return nresp
    End Function
    Public Function InsertarAseguradoraDB(ByVal valor As String) As Integer
        Dim cmd As New SqlCommand("usp_insertAseguradora", cn)
        cmd.CommandType = CommandType.StoredProcedure
        'definiendo los parámetros de entrada
        cmd.Parameters.Add("@Valor", SqlDbType.VarChar, 20).Value = valor
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim nresp As Integer = cmd.ExecuteNonQuery()
        cn.Close()
        Return nresp
    End Function
    Public Function InsertarMotivoDB(ByVal valor As String) As Integer
        Dim cmd As New SqlCommand("usp_insertMotivo", cn)
        cmd.CommandType = CommandType.StoredProcedure
        'definiendo los parámetros de entrada
        cmd.Parameters.Add("@Valor", SqlDbType.VarChar, 20).Value = valor
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim nresp As Integer = cmd.ExecuteNonQuery()
        cn.Close()
        Return nresp
    End Function
    Public Function InsertarEncargadoDB(ByVal valor As String) As Integer
        Dim cmd As New SqlCommand("usp_insertEncargado", cn)
        cmd.CommandType = CommandType.StoredProcedure
        'definiendo los parámetros de entrada
        cmd.Parameters.Add("@Valor", SqlDbType.VarChar, 20).Value = valor
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim nresp As Integer = cmd.ExecuteNonQuery()
        cn.Close()
        Return nresp
    End Function
    Public Function InsertarColorDB(ByVal valor As String) As Integer
        Dim cmd As New SqlCommand("usp_insertcolor", cn)
        cmd.CommandType = CommandType.StoredProcedure
        'definiendo los parámetros de entrada
        cmd.Parameters.Add("@Valor", SqlDbType.VarChar, 20).Value = valor
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim nresp As Integer = cmd.ExecuteNonQuery()
        cn.Close()
        Return nresp
    End Function
End Class
