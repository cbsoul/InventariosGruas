Public NotInheritable Class SplashScreen1

    'TODO: Este formulario se puede establecer fácilmente como pantalla de presentación para la aplicación desde la pestaña "Aplicación"
    '  del Diseñador de proyectos ("Propiedades" bajo el menú "Proyecto").
    Dim conexion As New ConsEntradas


    Private Sub SplashScreen1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Configure el texto del cuadro de diálogo en tiempo de ejecución según la información del ensamblado de la aplicación.  
        ProgressBar1.Value = 0
        ProgressBar1.Value = 10
        ProgressBar1.Value = 20
        ProgressBar1.Value = 30
        ProgressBar1.Value = 40
        ProgressBar1.Value = 50
        Try
            conexion.TestConexion()
        Catch ex As Exception
            MsgBox("No se pudo cargar base de datos" & vbCrLf & ex.Message)
        End Try
        ProgressBar1.Value = 60
        ProgressBar1.Value = 65
        ProgressBar1.Value = 70
        ProgressBar1.Value = 75
        ProgressBar1.Value = 80
        ProgressBar1.Value = 85
        ProgressBar1.Value = 90
        ProgressBar1.Value = 95
        ProgressBar1.Value = 100

    End Sub

    
End Class
