Imports System.Data.SqlClient
Public Class ConsSalidas

    Dim cn As New SqlConnection(cadenasql)
    Public Function MostrarSalidas() As DataTable
        Dim da As New SqlDataAdapter("usp_consultaSalidas", cn)
        Dim tbl As New DataTable
        da.Fill(tbl)
        Return (tbl)
    End Function
    Public Function SalidasCompletas(ByVal IdSalida As String) As DataTable
        Dim cmd As New SqlCommand("usp_SalidasCompletas", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@IdSalidas", SqlDbType.VarChar, 10).Value = IdSalida
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
        cn.Open()
        Dim tbl As New DataTable
        tbl.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection))
        Return (tbl)
    End Function
End Class
