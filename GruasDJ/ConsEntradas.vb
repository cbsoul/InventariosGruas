Imports System.Data.SqlClient
Public Class ConsEntradas
    Dim cn As New SqlConnection(cadenasql)

    Public Function TestConexion()
        cn.Open()
        cn.Close()
        Return Nothing
    End Function
    Public Function MostrarEntradas() As DataTable
        Dim da As New SqlDataAdapter("usp_consultaEntradas", cn)
        Dim tbl As New DataTable
        da.Fill(tbl)
        Return (tbl)
    End Function
    Public Function MostrarEntradasChatarra() As DataTable
        Dim da As New SqlDataAdapter("usp_EntradasChatarra", cn)
        Dim tbl As New DataTable
        da.Fill(tbl)
        Return (tbl)
    End Function

    Public Function EntradasCompletas(ByVal IdEntrada As Integer) As DataTable
        Dim cmd As New SqlCommand("usp_EntradasCompletas", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@IdEntrada", SqlDbType.Int).Value = IdEntrada
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim tbl As New DataTable
        tbl.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection))
        Return (tbl)
    End Function

    Public Function EncuentraTodo(ByVal buscar As String) As DataTable
        Dim cmd As New SqlCommand("usp_EncuentraTodo", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@Buscar", SqlDbType.VarChar, 20).Value = buscar
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim tbl As New DataTable
        tbl.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection))
        Return (tbl)
    End Function
    Public Function EncuentraTodoporMes(ByVal buscar As String, ByVal buscarfecha As String) As DataTable
        Dim cmd As New SqlCommand("usp_EncuentraTodoPorMes", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@Buscar", SqlDbType.VarChar, 20).Value = buscar
        cmd.Parameters.Add("@BuscarFecha", SqlDbType.VarChar, 2).Value = buscarfecha
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim tbl As New DataTable
        tbl.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection))
        Return (tbl)
    End Function
End Class
