<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NuevoDB
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.NuevoBTN = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtNuevo = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'NuevoBTN
        '
        Me.NuevoBTN.Location = New System.Drawing.Point(178, 56)
        Me.NuevoBTN.Name = "NuevoBTN"
        Me.NuevoBTN.Size = New System.Drawing.Size(77, 30)
        Me.NuevoBTN.TabIndex = 3
        Me.NuevoBTN.Text = "Nuevo"
        Me.NuevoBTN.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(-1, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "________"
        '
        'txtNuevo
        '
        Me.txtNuevo.Location = New System.Drawing.Point(90, 12)
        Me.txtNuevo.Name = "txtNuevo"
        Me.txtNuevo.Size = New System.Drawing.Size(165, 22)
        Me.txtNuevo.TabIndex = 2
        '
        'NuevoDB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(267, 96)
        Me.Controls.Add(Me.txtNuevo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.NuevoBTN)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "NuevoDB"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Nuevo"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents NuevoBTN As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtNuevo As System.Windows.Forms.TextBox
End Class
